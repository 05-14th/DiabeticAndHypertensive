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
    public partial class RStockInDiabetic : Form
    {
        private string connectionString = "Data Source=LAPTOP-03VSR27V\\SQLEXPRESS1;Initial Catalog=had;Integrated Security=True";

        public RStockInDiabetic()
        {
            InitializeComponent();
        }

        private void RStockInDiabetic_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
            StockInLists();
        }
        private void StockInLists()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM StockInDiabetic", conn);
                DataTable datatable = new DataTable();
                adapter.Fill(datatable);

                ReportDataSource rds = new ReportDataSource("DataSet1", datatable);
                reportViewer1.LocalReport.ReportPath = @"C:\Users\Hann Aldrich\Desktop\DiabeticAndHypertensive\DiabeticAndHypertensive\RStockInDiabetic.rdlc";
                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.DataSources.Add(rds);
                reportViewer1.RefreshReport();
            }
        }
    }
}
