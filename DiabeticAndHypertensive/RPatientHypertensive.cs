using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiabeticAndHypertensive
{
    public partial class RPatientHypertensive : Form
    {
        private string connectionString = "Data Source=LAPTOP-03VSR27V\\SQLEXPRESS1;Initial Catalog=had;Integrated Security=True";

        public RPatientHypertensive()
        {
            InitializeComponent();
        }

        private void RPatientHypertensive_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
            PatientLists();
            cbBarangay.SelectedIndex = 0;
        }
        private void PatientLists()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM PatientHypertensive", conn);
                DataTable datatable = new DataTable();
                adapter.Fill(datatable);

                ReportDataSource rds = new ReportDataSource("DataSet1", datatable);
                reportViewer1.LocalReport.ReportPath = @"C:\Users\Hann Aldrich\Desktop\DiabeticAndHypertensive\DiabeticAndHypertensive\RPatientH.rdlc";
                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.DataSources.Add(rds);
                reportViewer1.RefreshReport();
            }
        }

        private void btnfilterbrgy_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM PatientHypertensive WHERE brgy LIKE '%" + cbBarangay.SelectedItem + "%'", conn);
                DataTable datatable = new DataTable();
                adapter.Fill(datatable);

                ReportDataSource rds = new ReportDataSource("DataSet1", datatable);
                reportViewer1.LocalReport.ReportPath = @"C:\Users\Hann Aldrich\Desktop\DiabeticAndHypertensive\DiabeticAndHypertensive\RPatientH.rdlc";
                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.DataSources.Add(rds);
                reportViewer1.RefreshReport();
            }
        }

        private void btnfiltermonthly_Click(object sender, EventArgs e)
        {
            // Get the selected month from the ComboBox
            string selectedMonth = cbMonthly.SelectedItem as string;

            if (!string.IsNullOrEmpty(selectedMonth))
            {
                // Perform the filtering based on the selected month
                FilterDataByMonth(selectedMonth);
            }
        }
        private void FilterDataByMonth(string selectedMonth)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Use a parameterized query to avoid SQL injection
                string query = "SELECT * FROM PatientHypertensive WHERE YEAR(date) = YEAR(GETDATE()) AND MONTH(date) = MONTH(@SelectedMonth)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Assuming "date" is the name of the column containing the date
                    cmd.Parameters.AddWithValue("@SelectedMonth", DateTime.ParseExact(selectedMonth, "MMMM", CultureInfo.CurrentCulture).Month);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    ReportDataSource rds = new ReportDataSource("DataSet1", dataTable);
                    reportViewer1.LocalReport.ReportPath = @"C:\Users\Hann Aldrich\source\repos\DiabeticAndHypertensive\DiabeticAndHypertensive\RPatientHypertensive.rdlc";
                    reportViewer1.LocalReport.DataSources.Clear();
                    reportViewer1.LocalReport.DataSources.Add(rds);
                    reportViewer1.RefreshReport();
                }
            }
        }

    }
}
