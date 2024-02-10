using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace DiabeticAndHypertensive
{
    public partial class ChartStockIn : Form
    {
        private string connectionString = "Data Source=localhost\\MSSQLSERVER01;Initial Catalog=had;Integrated Security=True";
        //private string connectionString = "Data Source=LAPTOP-03VSR27V\\SQLEXPRESS1;Initial Catalog=had;Integrated Security=True";
        private int DiabeticMedicineStockInCount;
        private int HypertensiveMedicineStockInCount;

        public ChartStockIn()
        {
            InitializeComponent();
            // Subscribe to the CellFormatting event
            StockInCounts();
        }

        private void ChartStockIn_Load(object sender, EventArgs e)
        {
            LoadHypertensiveStockIn();
            LoadDiabeticStockIn();
        }
        private void LoadDiabeticStockIn()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Load data from the first table
                SqlDataAdapter adapterDiabetic = new SqlDataAdapter("SELECT * FROM StockInDiabetic ORDER BY category", connection);
                DataTable dtDiabetic = new DataTable();
                adapterDiabetic.Fill(dtDiabetic);

                // Bind the DataTable to the DataGridView
                dgvSIDiabetic.DataSource = dtDiabetic;

                // Set column headers
                dgvSIDiabetic.Columns["Id"].HeaderText = "Id";
                dgvSIDiabetic.Columns["Id"].Visible = false;
                dgvSIDiabetic.Columns["category"].HeaderText = "Medicine Category";
                dgvSIDiabetic.Columns["category"].Visible = false;
                dgvSIDiabetic.Columns["date"].HeaderText = "Received date";
                dgvSIDiabetic.Columns["date"].Visible = false;
                dgvSIDiabetic.Columns["name"].HeaderText = "Name";
                dgvSIDiabetic.Columns["name"].Visible = false;
                dgvSIDiabetic.Columns["code"].HeaderText = "Code";
                dgvSIDiabetic.Columns["code"].Visible = false;
                dgvSIDiabetic.Columns["medname"].HeaderText = "Medicine Name";
                dgvSIDiabetic.Columns["brand"].HeaderText = "Brand Name";
                dgvSIDiabetic.Columns["qty"].HeaderText = "Quantity";
                dgvSIDiabetic.Columns["exdate"].HeaderText = "Current Stocks";
                dgvSIDiabetic.Columns["exdate"].Visible = false;
                dgvSIHypertensive.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                dgvSIDiabetic.RowTemplate.Height = 40;
                dgvSIDiabetic.ColumnHeadersHeight = 40;
            }
        }
        private void LoadHypertensiveStockIn()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Load data from the first table
                SqlDataAdapter adapterHypertensive = new SqlDataAdapter("SELECT * FROM StockInHypertensive ORDER BY category", connection);
                DataTable dtHypertensive = new DataTable();
                adapterHypertensive.Fill(dtHypertensive);

                // Bind the DataTable to the DataGridView
                dgvSIHypertensive.DataSource = dtHypertensive;

                // Set column headers
                dgvSIHypertensive.Columns["Id"].HeaderText = "Id";
                dgvSIHypertensive.Columns["Id"].Visible = false;
                dgvSIHypertensive.Columns["category"].HeaderText = "Medicine Category";
                dgvSIHypertensive.Columns["category"].Visible = false;
                dgvSIHypertensive.Columns["date"].HeaderText = "Received date";
                dgvSIHypertensive.Columns["date"].Visible = false;
                dgvSIHypertensive.Columns["name"].HeaderText = "Name";
                dgvSIHypertensive.Columns["name"].Visible = false;
                dgvSIHypertensive.Columns["code"].HeaderText = "Code";
                dgvSIHypertensive.Columns["code"].Visible = false;
                dgvSIHypertensive.Columns["medname"].HeaderText = "Medicine Name";
                dgvSIHypertensive.Columns["brand"].HeaderText = "Brand Name";
                dgvSIHypertensive.Columns["qty"].HeaderText = "Quantity";
                dgvSIHypertensive.Columns["exdate"].HeaderText = "Current Stocks";
                dgvSIHypertensive.Columns["exdate"].Visible = false;
                dgvSIHypertensive.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                dgvSIHypertensive.RowTemplate.Height = 40;
                dgvSIHypertensive.ColumnHeadersHeight = 40;
            }
        }
        private void StockInCounts()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                DiabeticMedicineStockInCount = ExecuteScalar<int>("SELECT SUM(qty) FROM StockInDiabetic", conn);
                HypertensiveMedicineStockInCount = ExecuteScalar<int>("SELECT SUM(qty) FROM StockInHypertensive", conn);

                int totalStockIn = DiabeticMedicineStockInCount + HypertensiveMedicineStockInCount;

                // lblHMedicine.Text = $" Available\nDiabetic Medicines\n\n{DiabeticMedicineInventoryCount}";
                // lblDMedicine.Text = $" Available\nHypertensive Medicines\n\n{HypertensiveMedicineInventoryCount}";
                //lblTotalMedicine.Text = $" Available\nTotal Medicines\n\n{totalInventory}";

                DisplayStockInChart();
            }
        }
        private void DisplayStockInChart()
        {
            chart1.Series.Clear();

            Series series = new Series("Medicine StockIn");

            chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.FromArgb(128, Color.Gray);
            chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.FromArgb(128, Color.Gray);

            DataPoint diabeticPoint = new DataPoint(1, DiabeticMedicineStockInCount)
            {
                AxisLabel = "Diabetic Medicine",
                Font = new Font("Arial", 10, FontStyle.Regular),
                Color = Color.LightSeaGreen
            };
            series.Points.Add(diabeticPoint);

            DataPoint hypertensivePoint = new DataPoint(0, HypertensiveMedicineStockInCount)
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
            chart1.Titles.Add("Medicine Stock-In");
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

        private void dgvSIDiabetic_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == dgvSIDiabetic.Columns["qty"].Index)
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

        private void dgvSIHypertensive_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == dgvSIHypertensive.Columns["qty"].Index )
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
