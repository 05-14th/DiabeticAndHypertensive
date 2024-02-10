using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace DiabeticAndHypertensive
{
    public partial class MIInventory : Form
    {
        string connectionString = "Server=localhost\\MSSQLSERVER01;Initial Catalog=had;Integrated Security=True";
        //private string connectionString = "Data Source=LAPTOP-03VSR27V\\SQLEXPRESS1;Initial Catalog=had;Integrated Security=True";

        public MIInventory()
        {
            InitializeComponent();
        }

        private void MIInventory_Load(object sender, EventArgs e)
        {
     //       LoadAllInventory();
        }
       




        private void LoadAllInventory()
        {
            dgvMI.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            DataTable dtCombined = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Load data from the first table
                SqlDataAdapter adapterDiabetic = new SqlDataAdapter("SELECT * FROM InventoryDiabetic ORDER BY code DESC", connection);
                adapterDiabetic.Fill(dtCombined);

                // Load data from the second table
                SqlDataAdapter adapterHypertensive = new SqlDataAdapter("SELECT * FROM InventoryHypertensive ORDER BY code DESC", connection);
                DataTable dtHypertensive = new DataTable();
                adapterHypertensive.Fill(dtHypertensive);

                // Clear the contents of dtCombined to avoid duplicates
                dtCombined.Clear();

                // Merge the data from both tables into the combined DataTable
                dtCombined.Merge(dtHypertensive);
            }

            dgvMI.DataSource = dtCombined;

            dgvMI.Columns["code"].HeaderText = "Code";
            dgvMI.Columns["category"].HeaderText = "Medicine Category";
            dgvMI.Columns["medname"].HeaderText = "Medicine Name";
            dgvMI.Columns["brand"].HeaderText = "Medicine Brand";
            dgvMI.Columns["in"].HeaderText = "Stock-In";
            dgvMI.Columns["out"].HeaderText = "Stock-Out";
            dgvMI.Columns["cstock"].HeaderText = "Current Stocks";
            dgvMI.RowTemplate.Height = 40;
            dgvMI.Columns["in"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvMI.Columns["out"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvMI.Columns["cstock"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvMI.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMI.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            // Set the Resizable property to False for each column you don't want to be resizable
            dgvMI.Columns["code"].Resizable = DataGridViewTriState.False;
            dgvMI.Columns["medname"].Resizable = DataGridViewTriState.False;
            dgvMI.Columns["category"].Resizable = DataGridViewTriState.False;
            dgvMI.Columns["in"].Resizable = DataGridViewTriState.False;
            dgvMI.Columns["out"].Resizable = DataGridViewTriState.False;
            dgvMI.Columns["cstock"].Resizable = DataGridViewTriState.False;
            dgvMI.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);


        }
        private void FilterData(string[] columns, string searchKeyword)
        {
            DataTable dtCombined = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Construct the WHERE clause dynamically based on the specified columns
                string whereClause = string.Join(" OR ", columns.Select(column => $"{column} LIKE '{searchKeyword}'"));
                whereClause = $"{whereClause}"; // Wrap the entire clause in parentheses

                // Modify the SQL query based on your table structure and columns
                string queryDiabetic = $"SELECT * FROM StockInDiabetic";
                string queryHypertensive = $"SELECT * FROM StockInHypertensive";

                using (SqlDataAdapter adapterDiabetic = new SqlDataAdapter(queryDiabetic, connection))
                using (SqlDataAdapter adapterHypertensive = new SqlDataAdapter(queryHypertensive, connection))
                {
                    // Use SqlParameter for the search keyword to avoid SQL injection
                    SqlParameter parameterDiabetic = new SqlParameter("@searchKeyword", SqlDbType.NVarChar);
                    parameterDiabetic.Value = $"%{searchKeyword}%";

                    SqlParameter parameterHypertensive = new SqlParameter("@searchKeyword", SqlDbType.NVarChar);
                    parameterHypertensive.Value = $"%{searchKeyword}%";

                    adapterDiabetic.SelectCommand.Parameters.Add(parameterDiabetic);
                    adapterHypertensive.SelectCommand.Parameters.Add(parameterHypertensive);

                    adapterDiabetic.Fill(dtCombined);

                    DataTable dtHypertensive = new DataTable();
                    adapterHypertensive.Fill(dtHypertensive);

                    // Merge the data from both tables into the combined DataTable
                    dtCombined.Merge(dtHypertensive);
                }

                dgvMI.DataSource = dtCombined;
            }
        }



        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string searchKeyword = txtSearch.Text;

            FilterData(new[] { "code", "medname", "brand", "category", "qty", "exdate", "name", "date" }, searchKeyword);

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

        private void dgvMI_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == dgvMI.Columns["qty"].Index)
            {
                if (e.Value != null && e.Value != DBNull.Value)
                {
                    if (int.TryParse(e.Value.ToString(), out int intValue))
                    {
                        e.Value = intValue.ToString("N0", CultureInfo.InvariantCulture);
                        e.FormattingApplied = true;
                    }
                }
            }
        }

        private void dgvMI_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
