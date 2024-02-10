using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DiabeticAndHypertensive
{
    public partial class MIExpiredMedicine : Form
    {
        string connectionString = "Data Source=localhost\\MSSQLSERVER01;Initial Catalog=had;Integrated Security=True";
        //private string connectionString = "Data Source=LAPTOP-03VSR27V\\SQLEXPRESS1;Initial Catalog=had;Integrated Security=True";

        public MIExpiredMedicine()
        {
            InitializeComponent();
            loadAllExpiredMedicine();

        }

        private void MIExpiredMedicine_Load(object sender, EventArgs e)
        {
            GetStockWithinOneMonth();
        }
        private void loadAllExpiredMedicine()
        {
            dgvExMed.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DataTable dtExpiredMedicine = GetStockWithinOneMonth();
            dgvExMed.DataSource = dtExpiredMedicine;
            dgvExMed.Columns["category"].HeaderText = "Medicine Category";
            dgvExMed.Columns["date"].HeaderText = "Date Received";
            dgvExMed.Columns["name"].HeaderText = "Receiver's Name";
            dgvExMed.Columns["code"].HeaderText = "Medicine Code";
            dgvExMed.Columns["medname"].HeaderText = "Medicine Name";
            dgvExMed.Columns["brand"].HeaderText = "Brand";
            dgvExMed.Columns["qty"].HeaderText = "Quantity";
            dgvExMed.Columns["exdate"].HeaderText = "Expiration Date";
            dgvExMed.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvExMed.RowTemplate.Height = 35;

            dgvExMed.Columns["code"].DisplayIndex = 0;
            dgvExMed.Columns["medname"].DisplayIndex = 1;
            dgvExMed.Columns["brand"].DisplayIndex = 2;
            dgvExMed.Columns["qty"].DisplayIndex = 3;
            dgvExMed.Columns["exdate"].DisplayIndex = 4;
            dgvExMed.Columns["category"].DisplayIndex = 5;
            dgvExMed.Columns["name"].DisplayIndex = 6;
            dgvExMed.Columns["date"].DisplayIndex = 7;

            dgvExMed.Columns["qty"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvExMed.Columns["exdate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvExMed.Columns["date"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvExMed.Columns["code"].Resizable = DataGridViewTriState.False;
            dgvExMed.Columns["medname"].Resizable = DataGridViewTriState.False;
            dgvExMed.Columns["brand"].Resizable = DataGridViewTriState.False;
            dgvExMed.Columns["qty"].Resizable = DataGridViewTriState.False;
            dgvExMed.Columns["exdate"].Resizable = DataGridViewTriState.False;
            dgvExMed.Columns["category"].Resizable = DataGridViewTriState.False;
            dgvExMed.Columns["name"].Resizable = DataGridViewTriState.False;
            dgvExMed.Columns["date"].Resizable = DataGridViewTriState.False;
        }
        private DateTime OneMonthBeforeExpiration()
        {
            // Get the current date
            DateTime currentDate = DateTime.Now;

            // Calculate the date one month before the expiration(subtracting one month from the current date)
            return currentDate.AddMonths(-1);
        }
        private DataTable GetStockWithinOneMonth()
        {
            DateTime currentDate = DateTime.Now;

            // Calculate the date one month before the expiration
            DateTime oneMonthBeforeExpiration = OneMonthBeforeExpiration();

            DataTable dtResult = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Query to select stock records within one month before expiration
                string query = "SELECT category, date, name, code, medname, brand, qty, exdate FROM (SELECT *, ROW_NUMBER() OVER (PARTITION BY category, code ORDER BY exdate DESC) AS rn FROM " +
                                       "(SELECT 'Diabetic Medicine' AS category, date, name, code, medname, brand, qty, exdate FROM StockInDiabetic " +
                                       "UNION ALL " +
                                       "SELECT 'Hypertensive Medicine' AS category, date, name, code, medname, brand, qty, exdate FROM StockInHypertensive) AS AllStocks) AS Ranked " +
                                       "WHERE rn = 1 AND exdate BETWEEN @OneMonthBefore AND @CurrentDate";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@OneMonthBefore", oneMonthBeforeExpiration);
                    cmd.Parameters.AddWithValue("@CurrentDate", currentDate);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dtResult);



                }
            }

            return dtResult;
        }

        private void dgvExMed_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            var targetColumns = new[] { "qty" };

            if (targetColumns.Contains(dgvExMed.Columns[e.ColumnIndex].Name) && e.Value != null)
            {
                if (int.TryParse(e.Value.ToString(), out int quantity))
                {
                    // Format the quantity with commas
                    e.Value = $"{quantity:n0}";
                    e.FormattingApplied = true; // Set this to true to indicate that the formatting is applied
                }
            }
        }

        private void txtSearch_Click(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            phSearch.Visible = false;
            if (textBox.Text == phSearch.Text)
            {
                textBox.Text = string.Empty;
                textBox.ForeColor = SystemColors.WindowText; // Restore the original text color
            }
        }

        private void txtSearch_Enter(object sender, EventArgs e)
        {
            if (txtSearch.Text == "Search")
            {
                txtSearch.Text = "";
                txtSearch.ForeColor = Color.Black;
            }
        }

        private void txtSearch_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                txtSearch.Text = "Search";
                txtSearch.ForeColor = Color.Black;
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text.Trim();

            ((DataTable)dgvExMed.DataSource).DefaultView.RowFilter = $"code LIKE '%{searchText}%' OR medname LIKE '%{searchText}%' OR brand LIKE '%{searchText}%' OR  category LIKE '%{searchText}%' OR name LIKE '%{searchText}%'";
        }
    }
}
