using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DiabeticAndHypertensive
{
    public partial class PatientInformationLists : Form
    {
        //string connectionString = "Data Source=LAPTOP-03VSR27V\\SQLEXPRESS1;Initial Catalog=had;Integrated Security=True";
        string connectionString = "Data Source=localhost\\MSSQLSERVER01;Initial Catalog=had;Integrated Security=True";
        private Timer refreshTimer;
        private DataTable dtPatient = new DataTable();
        private SqlDataAdapter adapterPatient = new SqlDataAdapter();
        private int Id;
        private int phealth;
        private string category;
        ToolTip toolTip;
        private DataTable dtDiabetic = new DataTable();
        private DataTable dtHypertensive = new DataTable();
        private SqlDataAdapter adapterDiabetic = new SqlDataAdapter();
        private SqlDataAdapter adapterHypertensive = new SqlDataAdapter();

        public PatientInformationLists()
        {
            InitializeComponent();
            loadPatient();
            toolTip = new ToolTip();
            toolTip.SetToolTip(btnAdd, "Add Patient");
            
        }
        private void dgvPI_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvPI.SelectedRows.Count > 0)
            {
                string selectedValue = dgvPI.SelectedRows[0].Cells[0].Value.ToString();
                PatientInformationView patientInformationView = new PatientInformationView(selectedValue);
                int row = dgvPI.CurrentRow.Index;
                patientInformationView.PatientInfo(Convert.ToString(dgvPI[3, row].Value), Convert.ToString(dgvPI[12, row].Value), Convert.ToString(dgvPI[1, row].Value), Convert.ToString(dgvPI[4, row].Value), Convert.ToString(dgvPI[5, row].Value), Convert.ToString(dgvPI[12, row].Value), Convert.ToString(dgvPI[6, row].Value), Convert.ToString(dgvPI[8, row].Value), Convert.ToString(dgvPI[9, row].Value), Convert.ToString(dgvPI[10, row].Value), Convert.ToDateTime(dgvPI[7, row].Value), "");
                patientInformationView.ShowDialog();
            }
        }
        private void InitializeDataGridViewRow()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                adapterDiabetic = new SqlDataAdapter("SELECT * FROM TablePatient ORDER BY fullname ", connection);
                adapterHypertensive = new SqlDataAdapter("SELECT * FROM TablePatient ORDER BY fullname ", connection);

                dtDiabetic.Clear();
                dtHypertensive.Clear();
                adapterDiabetic.Fill(dtDiabetic);
                adapterHypertensive.Fill(dtHypertensive);

                dtDiabetic.Merge(dtHypertensive);

                // No need to format 'day' and 'month' columns in the DataTable

                if (dgvPI.Columns.Count == 0)
                {
                    dgvPI.DataSource = dtDiabetic;
                }

                connection.Close();

                dgvPI.Columns["Id"].HeaderText = "Id";
                dgvPI.Columns["category"].HeaderText = "Patient Type";
                dgvPI.Columns["fullname"].HeaderText = "Patient Name";
                dgvPI.Columns["lname"].HeaderText = "Last Name";
                dgvPI.Columns["fname"].HeaderText = "First Name";
                dgvPI.Columns["mi"].HeaderText = "M.I";
                dgvPI.Columns["suffix"].HeaderText = "Suffix";
                dgvPI.Columns["dob"].HeaderText = "Date of Birth";
                dgvPI.Columns["age"].HeaderText = "Age";
                dgvPI.Columns["sex"].HeaderText = "Sex";
                dgvPI.Columns["brgy"].HeaderText = "Barangay";
                dgvPI.Columns["phealth"].HeaderText = "Philhealth ID";
                dgvPI.Columns["contact"].HeaderText = "Contact";
                dgvPI.Columns["medname"].HeaderText = "Prescribed Medicine";
                dgvPI.Columns["day"].HeaderText = "Frequency Intake";
                dgvPI.Columns["month"].HeaderText = "Monthly Consumption";
                dgvPI.Columns["bp"].HeaderText = "BP Monitoring";
                dgvPI.Columns["smoker"].HeaderText = "Smoker?";
                dgvPI.Columns["exercise"].HeaderText = "Exercise";
                dgvPI.Columns["date"].HeaderText = "Date Added";
                dgvPI.Columns["history"].HeaderText = "History";
                dgvPI.Columns["image"].HeaderText = "Image";

                dgvPI.Columns["category"].DisplayIndex = 0;
                dgvPI.Columns["phealth"].DisplayIndex = 1;
                dgvPI.Columns["fullname"].DisplayIndex = 2;
                dgvPI.Columns["age"].DisplayIndex = 3;
                dgvPI.Columns["sex"].DisplayIndex = 4;
                dgvPI.Columns["medname"].DisplayIndex = 5;
                dgvPI.Columns["day"].DisplayIndex = 6;
                dgvPI.Columns["brgy"].DisplayIndex = 7;
                dgvPI.Columns["lname"].DisplayIndex = 8;
                dgvPI.Columns["fname"].DisplayIndex = 9;
                dgvPI.Columns["mi"].DisplayIndex = 10;
                dgvPI.Columns["suffix"].DisplayIndex = 11;
                dgvPI.Columns["dob"].DisplayIndex = 12;
                dgvPI.Columns["month"].DisplayIndex = 13;
                dgvPI.Columns["bp"].DisplayIndex = 14;
                dgvPI.Columns["smoker"].DisplayIndex = 15;
                dgvPI.Columns["exercise"].DisplayIndex = 16;
                dgvPI.Columns["contact"].DisplayIndex = 17;
                dgvPI.Columns["history"].DisplayIndex = 18;
                dgvPI.Columns["image"].DisplayIndex = 19;
                dgvPI.Columns["date"].DisplayIndex = 20;

                dgvPI.Columns["category"].Visible = false;
                dgvPI.Columns["Id"].Visible = false;
                dgvPI.Columns["lname"].Visible = true;
                dgvPI.Columns["fname"].Visible = false;
                dgvPI.Columns["mi"].Visible = false;
                dgvPI.Columns["suffix"].Visible = false;
                dgvPI.Columns["dob"].Visible = false;
                dgvPI.Columns["contact"].Visible = false;
                dgvPI.Columns["month"].Visible = false;
                dgvPI.Columns["bp"].Visible = false;
                dgvPI.Columns["smoker"].Visible = false;
                dgvPI.Columns["exercise"].Visible = false;
                dgvPI.Columns["history"].Visible = false;
                dgvPI.Columns["date"].Visible = false;
                dgvPI.Columns["image"].Visible = false;

                dgvPI.Columns["medname"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

                dgvPI.Columns["age"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvPI.Columns["sex"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvPI.Columns["phealth"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvPI.Columns["day"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvPI.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvPI.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

                // Set the Resizable property to False for each column you don't want to be resizable
                dgvPI.Columns["Id"].Resizable = DataGridViewTriState.False;
                dgvPI.Columns["category"].Resizable = DataGridViewTriState.False;
                dgvPI.Columns["fullname"].Resizable = DataGridViewTriState.False;
                dgvPI.Columns["lname"].Resizable = DataGridViewTriState.False;
                dgvPI.Columns["fname"].Resizable = DataGridViewTriState.False;
                dgvPI.Columns["mi"].Resizable = DataGridViewTriState.False;
                dgvPI.Columns["suffix"].Resizable = DataGridViewTriState.False;
                dgvPI.Columns["dob"].Resizable = DataGridViewTriState.False;
                dgvPI.Columns["age"].Resizable = DataGridViewTriState.False;
                dgvPI.Columns["sex"].Resizable = DataGridViewTriState.False;
                dgvPI.Columns["brgy"].Resizable = DataGridViewTriState.False;
                dgvPI.Columns["phealth"].Resizable = DataGridViewTriState.False;
                dgvPI.Columns["contact"].Resizable = DataGridViewTriState.False;
                dgvPI.Columns["medname"].Resizable = DataGridViewTriState.True;
                dgvPI.Columns["day"].Resizable = DataGridViewTriState.False;
                dgvPI.Columns["month"].Resizable = DataGridViewTriState.False;
                dgvPI.Columns["bp"].Resizable = DataGridViewTriState.False;
                dgvPI.Columns["smoker"].Resizable = DataGridViewTriState.False;
                dgvPI.Columns["exercise"].Resizable = DataGridViewTriState.False;
                dgvPI.Columns["date"].Resizable = DataGridViewTriState.False;
                dgvPI.Columns["history"].Resizable = DataGridViewTriState.False;


                dgvPI.CellDoubleClick += dgvPI_CellDoubleClick;
            }
        }



        private void PatientInformationLists_Load(object sender, EventArgs e)
        {

        }
        private void loadPatient()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                adapterPatient = new SqlDataAdapter("SELECT * FROM TablePatient ORDER BY fullname ", connection);



                adapterPatient.Fill(dtPatient);

                dgvPI.DataSource = dtPatient;// No need to format 'day' and 'month' columns in the DataTable



                connection.Close();

                dgvPI.Columns["Id"].HeaderText = "Id";
                dgvPI.Columns["category"].HeaderText = "Patient Type";
                dgvPI.Columns["fullname"].HeaderText = "Patient Name";
                dgvPI.Columns["lname"].HeaderText = "Last Name";
                dgvPI.Columns["fname"].HeaderText = "First Name";
                dgvPI.Columns["mi"].HeaderText = "M.I";
                dgvPI.Columns["suffix"].HeaderText = "Suffix";
                dgvPI.Columns["dob"].HeaderText = "Date of Birth";
                dgvPI.Columns["age"].HeaderText = "Age";
                dgvPI.Columns["sex"].HeaderText = "Sex";
                dgvPI.Columns["brgy"].HeaderText = "Barangay";
                dgvPI.Columns["phealth"].HeaderText = "Philhealth ID";
                dgvPI.Columns["contact"].HeaderText = "Contact";
                dgvPI.Columns["medname"].HeaderText = "Prescribed Medicine";
                dgvPI.Columns["day"].HeaderText = "Frequency Intake";
                dgvPI.Columns["month"].HeaderText = "Monthly Consumption";
                dgvPI.Columns["bp"].HeaderText = "BP Monitoring";
                dgvPI.Columns["smoker"].HeaderText = "Smoker?";
                dgvPI.Columns["exercise"].HeaderText = "Exercise";
                dgvPI.Columns["date"].HeaderText = "Date Added";
                dgvPI.Columns["history"].HeaderText = "History";
                dgvPI.Columns["image"].HeaderText = "Image";

                dgvPI.Columns["category"].DisplayIndex = 0;
                dgvPI.Columns["phealth"].DisplayIndex = 1;
                dgvPI.Columns["fullname"].DisplayIndex = 2;
                dgvPI.Columns["age"].DisplayIndex = 3;
                dgvPI.Columns["sex"].DisplayIndex = 4;
                dgvPI.Columns["medname"].DisplayIndex = 5;
                dgvPI.Columns["day"].DisplayIndex = 6;
                dgvPI.Columns["brgy"].DisplayIndex = 7;
                dgvPI.Columns["lname"].DisplayIndex = 8;
                dgvPI.Columns["fname"].DisplayIndex = 9;
                dgvPI.Columns["mi"].DisplayIndex = 10;
                dgvPI.Columns["suffix"].DisplayIndex = 11;
                dgvPI.Columns["dob"].DisplayIndex = 12;
                dgvPI.Columns["month"].DisplayIndex = 13;
                dgvPI.Columns["bp"].DisplayIndex = 14;
                dgvPI.Columns["smoker"].DisplayIndex = 15;
                dgvPI.Columns["exercise"].DisplayIndex = 16;
                dgvPI.Columns["contact"].DisplayIndex = 17;
                dgvPI.Columns["history"].DisplayIndex = 18;
                dgvPI.Columns["image"].DisplayIndex = 19;
                dgvPI.Columns["date"].DisplayIndex = 20;

                dgvPI.Columns["category"].Visible = false;
                dgvPI.Columns["Id"].Visible = false;
                dgvPI.Columns["lname"].Visible = true;
                dgvPI.Columns["fname"].Visible = false;
                dgvPI.Columns["mi"].Visible = false;
                dgvPI.Columns["suffix"].Visible = false;
                dgvPI.Columns["dob"].Visible = false;
                dgvPI.Columns["contact"].Visible = false;
                dgvPI.Columns["month"].Visible = false;
                dgvPI.Columns["bp"].Visible = false;
                dgvPI.Columns["smoker"].Visible = false;
                dgvPI.Columns["exercise"].Visible = false;
                dgvPI.Columns["history"].Visible = false;
                dgvPI.Columns["date"].Visible = false;
                dgvPI.Columns["image"].Visible = false;

                dgvPI.Columns["medname"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

                dgvPI.Columns["age"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvPI.Columns["sex"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvPI.Columns["phealth"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvPI.Columns["day"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvPI.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvPI.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

                // Set the Resizable property to False for each column you don't want to be resizable
                dgvPI.Columns["Id"].Resizable = DataGridViewTriState.False;
                dgvPI.Columns["category"].Resizable = DataGridViewTriState.False;
                dgvPI.Columns["fullname"].Resizable = DataGridViewTriState.False;
                dgvPI.Columns["lname"].Resizable = DataGridViewTriState.False;
                dgvPI.Columns["fname"].Resizable = DataGridViewTriState.False;
                dgvPI.Columns["mi"].Resizable = DataGridViewTriState.False;
                dgvPI.Columns["suffix"].Resizable = DataGridViewTriState.False;
                dgvPI.Columns["dob"].Resizable = DataGridViewTriState.False;
                dgvPI.Columns["age"].Resizable = DataGridViewTriState.False;
                dgvPI.Columns["sex"].Resizable = DataGridViewTriState.False;
                dgvPI.Columns["brgy"].Resizable = DataGridViewTriState.False;
                dgvPI.Columns["phealth"].Resizable = DataGridViewTriState.False;
                dgvPI.Columns["contact"].Resizable = DataGridViewTriState.False;
                dgvPI.Columns["medname"].Resizable = DataGridViewTriState.True;
                dgvPI.Columns["day"].Resizable = DataGridViewTriState.False;
                dgvPI.Columns["month"].Resizable = DataGridViewTriState.False;
                dgvPI.Columns["bp"].Resizable = DataGridViewTriState.False;
                dgvPI.Columns["smoker"].Resizable = DataGridViewTriState.False;
                dgvPI.Columns["exercise"].Resizable = DataGridViewTriState.False;
                dgvPI.Columns["date"].Resizable = DataGridViewTriState.False;
                dgvPI.Columns["history"].Resizable = DataGridViewTriState.False;


                dgvPI.CellDoubleClick += dgvPI_CellDoubleClick;
            }
        }

        private void FilterData(string[] columns, string searchKeyword, DateTime fromDate, DateTime toDate)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Construct the WHERE clause dynamically based on the specified columns
                string whereClause = string.Join(" OR ", columns.Select(column => $"{column} LIKE @searchKeyword"));
                whereClause += " AND [date] BETWEEN @fromDate AND @toDate"; // date range

                // Modify the SQL query based on your table structure and columns
                string query = $"SELECT * FROM TablePatient WHERE {whereClause} " +
                               "UNION " +
                               $"SELECT * FROM TablePatient WHERE {whereClause}";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@searchKeyword", $"%{searchKeyword}%");
                    cmd.Parameters.AddWithValue("@fromDate", fromDate);
                    cmd.Parameters.AddWithValue("@toDate", toDate);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvPI.DataSource = dt;
                }
                connection.Close();
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string searchKeyword = txtSearch.Text;
            DateTime fromDate = dtpFromDate.Value;
            DateTime toDate = dtpToDate.Value;
            FilterData(new[] { "category", "medname", "fullname", "lname", "fname", "mi", "suffix", "dob", "age", "sex", "brgy", "phealth" }, searchKeyword, fromDate, toDate);

        }

        private void LoadDateAdded_Click(object sender, EventArgs e)
        {

            DateTime fromDate = dtpFromDate.Value;
            DateTime toDate = dtpToDate.Value;
            FilterData(new[] { "date" }, "", fromDate, toDate);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            PatientInformation parentForm = this.ParentForm as PatientInformation;
            if (parentForm != null)
            {
                parentForm.OpenPanelContent(new PatientInformationAdd());
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

        private void btnAdd_MouseHover(object sender, EventArgs e)
        {
            toolTip.Show("Add New Patient", btnAdd, -btnAdd.Width, -btnAdd.Height);
        }

        private void btnAdd_MouseLeave(object sender, EventArgs e)
        {
            toolTip.Hide(btnAdd);
        }

        private void dgvPI_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
