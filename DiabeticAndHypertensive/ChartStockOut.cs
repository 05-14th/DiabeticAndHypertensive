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
using System.Windows.Forms.DataVisualization.Charting;

namespace DiabeticAndHypertensive
{
    public partial class ChartStockOut : Form
    {
        //private string connectionString = "Data Source=LAPTOP-03VSR27V\\SQLEXPRESS1;Initial Catalog=had;Integrated Security=True";
        private string connectionString = "Data Source=localhost\\MSSQLSERVER01;Initial Catalog=had;Integrated Security=True";
        private int DiabeticMedicineStockOutCount;
        private int HypertensiveMedicineStockOutCount;
        public ChartStockOut()
        {
            InitializeComponent();
            StockOutCounts();
        }

        private void ChartStockOut_Load(object sender, EventArgs e)
        {
            LoadHypertensiveStockOut();
            LoadDiabeticStockOut();
            DisplayStockOutChart();
        }
        private void LoadDiabeticStockOut()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Load data from the first table
                SqlDataAdapter adapterDiabetic = new SqlDataAdapter("SELECT * FROM StockOutDiabetic ORDER BY ddate DESC", connection);
                DataTable dtDiabetic = new DataTable();
                adapterDiabetic.Fill(dtDiabetic);

                // Bind the DataTable to the DataGridView
                dgvSODiabetic.DataSource = dtDiabetic;

                // Set column headers
                dgvSODiabetic.Columns["Id"].HeaderText = "Id";
                dgvSODiabetic.Columns["Id"].Visible = false;
                dgvSODiabetic.Columns["category"].HeaderText = "Category";
                dgvSODiabetic.Columns["category"].Visible = false;
                dgvSODiabetic.Columns["ddate"].HeaderText = "Dispensed Date";
                dgvSODiabetic.Columns["ddate"].Visible = false;
                dgvSODiabetic.Columns["dname"].HeaderText = "Dispensed By";
                dgvSODiabetic.Columns["dname"].Visible = false;
                dgvSODiabetic.Columns["code"].HeaderText = "Code";
                dgvSODiabetic.Columns["code"].Visible = false;
                dgvSODiabetic.Columns["medname"].HeaderText = "Medicine Name";
                dgvSODiabetic.Columns["brand"].HeaderText = "Brand";
                dgvSODiabetic.Columns["brand"].Visible = false;
                dgvSODiabetic.Columns["rname"].HeaderText = "Reciever's name";
                dgvSODiabetic.Columns["rname"].Visible = false;
                dgvSODiabetic.Columns["phealth"].HeaderText = "Philhealth";
                dgvSODiabetic.Columns["phealth"].Visible = false;
                dgvSODiabetic.Columns["brgy"].HeaderText = "Barangay";
                dgvSODiabetic.Columns["brgy"].Visible = false;
                dgvSODiabetic.Columns["qty"].HeaderText = "Quantity";
                dgvSODiabetic.Columns["exdate"].HeaderText = "Expiration Date";
                dgvSODiabetic.Columns["exdate"].Visible = false;
            }
        }
        private void LoadHypertensiveStockOut()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Load data from the first table
                SqlDataAdapter adapterHypertensive = new SqlDataAdapter("SELECT * FROM StockOutHypertensive ORDER BY ddate DESC", connection);
                DataTable dtHypertensive = new DataTable();
                adapterHypertensive.Fill(dtHypertensive);

                // Bind the DataTable to the DataGridView
                dgvSOHypertensive.DataSource = dtHypertensive;

                // Set column headers
                dgvSOHypertensive.Columns["Id"].HeaderText = "Id";
                dgvSOHypertensive.Columns["Id"].Visible = false;
                dgvSOHypertensive.Columns["category"].HeaderText = "Category";
                dgvSOHypertensive.Columns["category"].Visible = false;
                dgvSOHypertensive.Columns["ddate"].HeaderText = "Dispensed Date";
                dgvSOHypertensive.Columns["ddate"].Visible = false;
                dgvSOHypertensive.Columns["dname"].HeaderText = "Dispensed By";
                dgvSOHypertensive.Columns["dname"].Visible = false;
                dgvSOHypertensive.Columns["code"].HeaderText = "Code";
                dgvSOHypertensive.Columns["code"].Visible = false;
                dgvSOHypertensive.Columns["medname"].HeaderText = "Medicine Name";
                dgvSOHypertensive.Columns["brand"].HeaderText = "Brand";
                dgvSOHypertensive.Columns["brand"].Visible = false;
                dgvSOHypertensive.Columns["rname"].HeaderText = "Reciever's name";
                dgvSOHypertensive.Columns["rname"].Visible = false;
                dgvSOHypertensive.Columns["phealth"].HeaderText = "Philhealth";
                dgvSOHypertensive.Columns["phealth"].Visible = false;
                dgvSOHypertensive.Columns["brgy"].HeaderText = "Barangay";
                dgvSOHypertensive.Columns["brgy"].Visible = false;
                dgvSOHypertensive.Columns["qty"].HeaderText = "Quantity";
                dgvSOHypertensive.Columns["exdate"].HeaderText = "Expiration Date";
                dgvSOHypertensive.Columns["exdate"].Visible = false;
            }
        }
        private void StockOutCounts()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                DiabeticMedicineStockOutCount = ExecuteScalar<int>("SELECT SUM(qty) FROM StockOutDiabetic", conn);
                HypertensiveMedicineStockOutCount = ExecuteScalar<int>("SELECT SUM(qty) FROM StockOutHypertensive", conn);

                int totalStockIn = DiabeticMedicineStockOutCount + HypertensiveMedicineStockOutCount;

                // lblHMedicine.Text = $" Available\nDiabetic Medicines\n\n{DiabeticMedicineInventoryCount}";
                // lblDMedicine.Text = $" Available\nHypertensive Medicines\n\n{HypertensiveMedicineInventoryCount}";
                //lblTotalMedicine.Text = $" Available\nTotal Medicines\n\n{totalInventory}";

                
            }
        }
        private void DisplayStockOutChart()
        {
            chart1.Series.Clear();

            Series series = new Series("Medicine Stock-Out");

            chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.FromArgb(128, Color.Gray);
            chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.FromArgb(128, Color.Gray);

            DataPoint diabeticPoint = new DataPoint(1, DiabeticMedicineStockOutCount)
            {
                AxisLabel = "Diabetic Medicine",
                Font = new Font("Arial", 10, FontStyle.Regular),
                Color = Color.LightSeaGreen
            };
            series.Points.Add(diabeticPoint);

            DataPoint hypertensivePoint = new DataPoint(0, HypertensiveMedicineStockOutCount)
            {
                AxisLabel = "Hypertensive Medicine",
                Font = new Font("Arial", 10, FontStyle.Regular),
                Color = Color.ForestGreen
            };
            series.Points.Add(hypertensivePoint);

            chart1.Series.Add(series);
            series.ChartType = SeriesChartType.Bar;
            series.IsValueShownAsLabel = true;
            series["PointWidth"] = "1.0";
            series.IsVisibleInLegend = false;
            chart1.Titles.Add("Medicine Stock-Out");
            chart1.Titles[0].Font = new Font("Arial", 13, FontStyle.Bold);
            //series.Font = new Font("Arial", 10, FontStyle.Regular);
            //chartInventory.Legends[0].Font = new Font("Arial", 8, FontStyle.Regular);
        }
        private T ExecuteScalar<T>(string query, SqlConnection connection, params SqlParameter[] parameters)
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddRange(parameters);
                object result = command.ExecuteScalar();
                return result == DBNull.Value ? default : (T)Convert.ChangeType(result, typeof(T));
            }
        }

        private void dgvSODiabetic_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == dgvSODiabetic.Columns["qty"].Index )
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

        private void dgvSOHypertensive_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == dgvSOHypertensive.Columns["qty"].Index)
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
    }
}
