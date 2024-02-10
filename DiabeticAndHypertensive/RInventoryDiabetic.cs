using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiabeticAndHypertensive
{
    public partial class RInventoryDiabetic : Form
    {
        //private string connectionString = "Data Source=LAPTOP-03VSR27V\\SQLEXPRESS1;Initial Catalog=had;Integrated Security=True";
        private string connectionString = "Data Source=localhost\\MSSQLSERVER01;Initial Catalog=had;Integrated Security=True";
        public RInventoryDiabetic()
        {
            InitializeComponent();
        }

        private void RInventoryDiabetic_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport(); this.reportViewer1.RefreshReport();
            InventoryLists();
        }
        private void InventoryLists()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM InventoryHypertensive", conn);
                DataTable datatable = new DataTable();
                adapter.Fill(datatable);

                ReportDataSource rds = new ReportDataSource("DataSet1", datatable);
                reportViewer1.LocalReport.ReportPath = @"C:\Users\Hann Aldrich\Desktop\DiabeticAndHypertensive\DiabeticAndHypertensive\RInventoryHypertensive.rdlc";
                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.DataSources.Add(rds);
                reportViewer1.RefreshReport();
            }
        }
    }
}