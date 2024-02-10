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
    public partial class Dashboard : Form
    {
        //private readonly string connectionString = "Data Source=LAPTOP-03VSR27V\\SQLEXPRESS1;Initial Catalog=had;Integrated Security=True";
        private string connectionString = "Data Source=localhost\\MSSQLSERVER01;Initial Catalog=had;Integrated Security=True";
        private int patientInformationCount;
        private int patientInformationHypertensiveCount;
        private int totalPatientCount;

        private int inventoryDiabeticCount;
        private int inventoryHypertensiveCount;
        private int totalInventoryCount;

        private int stockInDiabeticCount;
        private int stockInHypertensiveCount;
        private int totalstockInCount;

        private int stockOutDiabeticCount;
        private int stockOutHypertensiveCount;
        private int totalstockOutCount;

        public Dashboard()
        {
            InitializeComponent();
        }
        private void Dashboard_Load(object sender, EventArgs e)
        {
            OpenPanelContent(new ChartPI());
            PatientCounts();
            //MedicineInventory();
            StockInMedicine();
            StockOutMedicine();
        }
        
        public Form activeForm = null;
        public void OpenPanelContent(Form panelContent)
        {
            if (activeForm != null)
                activeForm.Close();
            activeForm = panelContent;
            panelContent.TopLevel = false;
            panelContent.FormBorderStyle = FormBorderStyle.None;
            panelContent.Dock = DockStyle.Fill;
            pnlContent.Controls.Add(panelContent);
            pnlContent.Tag = panelContent;
            panelContent.BringToFront();
            panelContent.Show();
        }

        private T ExecuteScalar<T>(string query, SqlConnection connection, params SqlParameter[] parameters)
        {
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddRange(parameters);
                var result = command.ExecuteScalar();
                return result == DBNull.Value ? default : (T)Convert.ChangeType(result, typeof(T));
            }
        }

        private void PatientCounts()
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                patientInformationCount = ExecuteScalar<int>("SELECT COUNT(*) FROM TablePatient", conn);
                totalPatientCount = patientInformationCount;
                btnTotalPI.Text = String.Format("{0:N0}", totalPatientCount);
            }
        }

        private int ExecuteScalarInt(string query, SqlConnection connection)
        {
            using (var command = new SqlCommand(query, connection))
            {
                var result = command.ExecuteScalar();
                return result == DBNull.Value ? 0 : Convert.ToInt32(result);
            }
        }
        

        private void StockInMedicine()
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                stockInDiabeticCount = ExecuteScalarInt("SELECT SUM(qty) FROM StockInDiabetic", conn);
                totalstockInCount = stockInDiabeticCount;
                btnSI.Text = String.Format("{0:N0}", totalstockInCount);
            }
        }
        private void StockOutMedicine()
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                stockOutDiabeticCount = ExecuteScalarInt("SELECT SUM(qty) FROM StockOutDiabetic", conn);
                totalstockOutCount = stockOutDiabeticCount;
                btnDM.Text = String.Format("{0:N0}", totalstockOutCount);
            }
        }

        private void btnTotalPI_Click(object sender, EventArgs e)
        {
            OpenPanelContent(new ChartPI());
        }

        private void btnTotalInventory_Click(object sender, EventArgs e)
        {
            OpenPanelContent(new ChartMI());
        }

        private void btnSI_Click(object sender, EventArgs e)
        {
            OpenPanelContent(new ChartStockIn());
        }

        private void btnDM_Click(object sender, EventArgs e)
        {
            OpenPanelContent(new ChartStockOut());
        }
    }
}
