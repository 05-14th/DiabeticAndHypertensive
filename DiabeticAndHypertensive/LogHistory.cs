using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace DiabeticAndHypertensive
{
    public partial class LogHistory : Form
    {
        //private string connectionString = "Data Source=LAPTOP-03VSR27V\\SQLEXPRESS1;Initial Catalog=had;Integrated Security=True";
        string connectionString = "Data Source=localhost\\MSSQLSERVER01;Initial Catalog=had;Integrated Security=True;";
        public LogHistory()
        {
            InitializeComponent();
        }

        private void LogHistory_Load(object sender, EventArgs e)
        {
            LoadActivity();
            dgvActivity.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }
        private void LoadActivity()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM ActivityLog ORDER BY date DESC", connection);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                dgvActivity.DataSource = dt;

                dgvActivity.Columns["username"].HeaderText = "Employee Name";
                dgvActivity.Columns["date"].HeaderText = "Date";
                dgvActivity.Columns["activity"].HeaderText = "Activity Performed";
                dgvActivity.RowTemplate.Height = 40;
                dgvActivity.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvActivity.SelectedRows.Count > 0)
            {
                // Assuming the primary key of your table is 'username' and 'date'
                string selectedUsername = dgvActivity.SelectedRows[0].Cells["username"].Value.ToString();
                DateTime selectedDate = Convert.ToDateTime(dgvActivity.SelectedRows[0].Cells["date"].Value);


                DialogResult result = MessageBox.Show("Are you sure you want to delete this record?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    // Call the delete function with the selected values
                    DeleteActivity(selectedUsername, selectedDate);

                    // Reload data after deletion
                    LoadActivity();
                }
            }
            else
            {
                MessageBox.Show("Please select a row to delete.", "Delete Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to delete all history records?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                // Call the delete all function
                DeleteAllActivity();

                // Reload data after deletion
                LoadActivity();
            }
        }
        private void DeleteActivity(string username, DateTime date)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string deleteQuery = "DELETE FROM ActivityLog WHERE username = @username AND date = @date";
                SqlCommand cmd = new SqlCommand(deleteQuery, connection);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@date", date);

                cmd.ExecuteNonQuery();
            }
        }
        private void DeleteAllActivity()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string deleteAllQuery = "DELETE FROM ActivityLog";
                SqlCommand cmd = new SqlCommand(deleteAllQuery, connection);

                cmd.ExecuteNonQuery();
            }
        }
    }
}
