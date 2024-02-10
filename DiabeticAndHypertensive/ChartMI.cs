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
    public partial class ChartMI : Form
    {
        //private string connectionString = "Data Source=LAPTOP-03VSR27V\\SQLEXPRESS1;Initial Catalog=had;Integrated Security=True";
        private string connectionString = "Data Source=localhost\\MSSQLSERVER01;Initial Catalog=had;Integrated Security=True";
        private int DiabeticMedicineInventoryCount;
        private int HypertensiveMedicineInventoryCount;
        public ChartMI()
        {
            InitializeComponent();
         //   InventoryCounts();
        }

        private void ChartMI_Load(object sender, EventArgs e)
        {
      //      LoadHypertensiveInventory();
      //      LoadDiabeticInventory();
        }
        private void LoadDiabeticInventory()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Load data from the first table
                SqlDataAdapter adapterDiabetic = new SqlDataAdapter("SELECT * FROM InventoryDiabetic", connection);
                DataTable dtDiabetic = new DataTable();
                adapterDiabetic.Fill(dtDiabetic);

                // Bind the DataTable to the DataGridView
                dgvMIDiabetic.DataSource = dtDiabetic;

                // Set column headers        
                dgvMIDiabetic.Columns["medname"].HeaderText = "Medicine Name";                         
                dgvMIDiabetic.Columns["cstock"].HeaderText = "Current Stocks";

                dgvMIDiabetic.Columns["code"].Visible = false;
                dgvMIDiabetic.Columns["category"].Visible = false;
                dgvMIDiabetic.Columns["in"].Visible = false;
                dgvMIDiabetic.Columns["out"].Visible = false;
                dgvMIDiabetic.Columns["brand"].Visible = false;

                dgvMIDiabetic.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                //dgvMIDiabetic.RowTemplate.Height = 40;
                //dgvMIDiabetic.ColumnHeadersHeight = 40;
            }
        }
        private void LoadHypertensiveInventory()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Load data from the first table
                SqlDataAdapter adapterHypertensive = new SqlDataAdapter("SELECT * FROM InventoryHypertensive", connection);
                DataTable dtHypertensive = new DataTable();
                adapterHypertensive.Fill(dtHypertensive);

                // Bind the DataTable to the DataGridView
                dgvMIHypertensive.DataSource = dtHypertensive;

                
                dgvMIHypertensive.Columns["medname"].HeaderText = "Medicine Name";
                dgvMIHypertensive.Columns["cstock"].HeaderText = "Current Stocks";
                dgvMIHypertensive.Columns["medname"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill; // Fill remaining space
                dgvMIHypertensive.Columns["brand"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill; // Fill remaining space

                
                dgvMIHypertensive.Columns["code"].Visible = false;
                dgvMIHypertensive.Columns["category"].Visible = false;
                dgvMIHypertensive.Columns["in"].Visible = false;
                dgvMIHypertensive.Columns["out"].Visible = false;
                dgvMIHypertensive.Columns["brand"].Visible = false;

               
                dgvMIHypertensive.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                //dgvMIHypertensive.RowTemplate.Height = 40;
                //dgvMIHypertensive.ColumnHeadersHeight = 100;
            }
        }



        private void InventoryCounts()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                DiabeticMedicineInventoryCount = ExecuteScalar<int>("SELECT SUM([in]) FROM InventoryDiabetic", conn);
                HypertensiveMedicineInventoryCount = ExecuteScalar<int>("SELECT SUM([in]) FROM InventoryHypertensive", conn);

                int totalInventory = DiabeticMedicineInventoryCount + HypertensiveMedicineInventoryCount;

                // lblHMedicine.Text = $" Available\nDiabetic Medicines\n\n{DiabeticMedicineInventoryCount}";
                // lblDMedicine.Text = $" Available\nHypertensive Medicines\n\n{HypertensiveMedicineInventoryCount}";
                //lblTotalMedicine.Text = $" Available\nTotal Medicines\n\n{totalInventory}";

                DisplayInventoryChart();
            }
        }
        private void DisplayInventoryChart()
        {
            chartInventory.Series.Clear();

            Series series = new Series("Medicine Inventory");

            chartInventory.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.FromArgb(128, Color.Gray);
            chartInventory.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.FromArgb(128, Color.Gray);

            DataPoint diabeticPoint = new DataPoint(1, DiabeticMedicineInventoryCount)
            {
                AxisLabel = "Diabetic Medicine",
                Font = new Font("Arial", 10, FontStyle.Regular),
                Color = Color.LightSeaGreen
            };
            series.Points.Add(diabeticPoint);

            DataPoint hypertensivePoint = new DataPoint(0, HypertensiveMedicineInventoryCount)
            {
                AxisLabel = "Hypertensive Medicine",
                Font = new Font("Arial", 10, FontStyle.Regular),
                Color = Color.ForestGreen
            };
            series.Points.Add(hypertensivePoint);

            chartInventory.Series.Add(series);
            series.ChartType = SeriesChartType.Bar;
            series.IsValueShownAsLabel = true;
            series["PointWidth"] = "1.0";
            series.IsVisibleInLegend = false;
            chartInventory.Titles.Add("Medicine Inventory");
            chartInventory.Titles[0].Font = new Font("Arial", 13, FontStyle.Bold);
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

        private void dgvMIDiabetic_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex >= 0)
            {
                DataGridViewColumn column = dgvMIDiabetic.Columns[e.ColumnIndex];

                if (column.DataPropertyName == "in" || column.DataPropertyName == "out" || column.DataPropertyName == "cstock")
                {
                    e.PaintBackground(e.CellBounds, true);

                    using (StringFormat format = new StringFormat())
                    {
                        format.Alignment = StringAlignment.Center;
                        format.LineAlignment = StringAlignment.Center;

                        RectangleF headerRect = e.CellBounds;
                        headerRect.Inflate(-2, -2);

                        e.Graphics.DrawString(column.HeaderText, e.CellStyle.Font, Brushes.Black, headerRect, format);
                    }

                    e.Handled = true;
                }
            }
        }

       

        private void dgvMIDiabetic_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == dgvMIDiabetic.Columns["cstock"].Index)
            //e.ColumnIndex == dgvMIDiabetic.Columns["out"].Index ||
            //e.ColumnIndex == dgvMIDiabetic.Columns["in"].Index)
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

        private void dgvMIHypertensive_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == dgvMIHypertensive.Columns["cstock"].Index)//||
            //e.ColumnIndex == dgvMIHypertensive.Columns["out"].Index ||
            //e.ColumnIndex == dgvMIHypertensive.Columns["in"].Index)
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