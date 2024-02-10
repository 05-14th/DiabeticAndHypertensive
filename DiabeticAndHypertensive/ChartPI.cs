using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace DiabeticAndHypertensive
{
    public partial class ChartPI : Form
    {
        //private readonly string connectionString = "Data Source=LAPTOP-03VSR27V\\SQLEXPRESS1;Initial Catalog=had;Integrated Security=True";
        private string connectionString = "Data Source=localhost\\MSSQLSERVER01;Initial Catalog=had;Integrated Security=True";
        private int patientInformationDiabeticCount;
        private int patientInformationHypertensiveCount;
        private int totalPatientCount;

        private Series seriesDiabetic;
        private Series seriesHypertensive;


        public ChartPI()
        {
            InitializeComponent();
            PatientCounts();
        }

        private void PIchart_Load(object sender, EventArgs e)
        {
            DisplayPatientChart();
            ChartDisplaybarangay();
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
                patientInformationDiabeticCount = ExecuteScalar<int>("SELECT COUNT(*) FROM PatientDiabetic", conn);
                patientInformationHypertensiveCount = ExecuteScalar<int>("SELECT COUNT(*) FROM PatientHypertensive", conn);
                totalPatientCount = patientInformationDiabeticCount + patientInformationHypertensiveCount;
            }
        }
        private void DisplayPatientChart()
        {
            ChartP.Series.Clear();

            Series series = new Series("Patients");



            ChartP.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.FromArgb(128, Color.Gray);
            ChartP.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.FromArgb(128, Color.Gray);

            DataPoint diabeticPoint = new DataPoint(1, patientInformationDiabeticCount)
            {
                AxisLabel = "Diabetic Patients",
                Font = new Font("Arial", 10, FontStyle.Regular),
                Color = Color.LightSeaGreen
                
            };
            series.Points.Add(diabeticPoint);

            DataPoint hypertensivePoint = new DataPoint(0, patientInformationHypertensiveCount)
            {
                AxisLabel = "Hypertensive Patients",
                Font = new Font("Arial", 10, FontStyle.Regular),
                Color = Color.ForestGreen
                
            };
            series.Points.Add(hypertensivePoint);

            ChartP.Series.Add(series);

            series.ChartType = SeriesChartType.Bar;
            series.IsValueShownAsLabel = true;
            // Set PointWidth to 1.0 to remove spacing between bars
            series["PointWidth"] = "1.0";
            // Hide the legend for the "Patients" series
            series.IsVisibleInLegend = false;
            ChartP.Titles.Add("Patient");
            ChartP.Titles[0].Font = new Font("Arial", 13, FontStyle.Bold);
            series.Font = new Font("Arial", 10, FontStyle.Regular);


            ChartP.Legends[0].Font = new Font("Arial", 8, FontStyle.Regular);
        }

        private void ChartDisplaybarangay()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                var counts = CountPatientsInEachBarangay(conn);
                var diabeticCounts = counts.Item1;
                var hypertensiveCounts = counts.Item2;

                chartBarangay.Series.Clear();
                chartBarangay.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.FromArgb(128, Color.Gray);
                chartBarangay.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.FromArgb(128, Color.Gray);

                seriesDiabetic = new Series("Diabetic Patients")
                {
                    ChartType = SeriesChartType.Column,
                    IsValueShownAsLabel = true,
                    Color = Color.LightSeaGreen,
                    IsVisibleInLegend = false
                };

                foreach (var barangay in BarangayList())
                {
                    int count = diabeticCounts.ContainsKey(barangay) ? diabeticCounts[barangay] : 0;
                    seriesDiabetic.Points.AddXY(barangay, count);
                }

                seriesHypertensive = new Series("Hypertensive Patients")
                {
                    ChartType = SeriesChartType.Column,
                    IsValueShownAsLabel = true,
                    Color = Color.ForestGreen,
                    IsVisibleInLegend = false
                };

                foreach (var barangay in BarangayList())
                {
                    int count = hypertensiveCounts.ContainsKey(barangay) ? hypertensiveCounts[barangay] : 0;
                    seriesHypertensive.Points.AddXY(barangay, count);
                }

                chartBarangay.Series.Add(seriesDiabetic);
                chartBarangay.Series.Add(seriesHypertensive);

                // Set axis titles and other formatting as needed
                chartBarangay.ChartAreas[0].AxisX.Title = "Barangays";
                chartBarangay.ChartAreas[0].AxisY.Title = "Patient Counts";

                // Explicitly set x-axis labels
                chartBarangay.ChartAreas[0].AxisX.CustomLabels.Clear();
                for (int i = 0; i < BarangayList().Count; i++)
                {
                    chartBarangay.ChartAreas[0].AxisX.CustomLabels.Add(i + 0.5, i + 1.5, BarangayList()[i]);
                }
            }
        }
        private Tuple<Dictionary<string, int>, Dictionary<string, int>> CountPatientsInEachBarangay(SqlConnection conn)
        {
            Dictionary<string, int> diabeticCounts = new Dictionary<string, int>();
            Dictionary<string, int> hypertensiveCounts = new Dictionary<string, int>();

            // Assuming you have a column 'brgy' in your patient tables
            string patientCountQueryDiabetic = "SELECT brgy, COUNT(*) as Count FROM PatientDiabetic GROUP BY brgy";
            string patientCountQueryHypertensive = "SELECT brgy, COUNT(*) as Count FROM PatientHypertensive GROUP BY brgy";

            using (SqlCommand command = new SqlCommand(patientCountQueryDiabetic, conn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string barangay = reader["brgy"].ToString();
                        int count = Convert.ToInt32(reader["Count"]);

                        diabeticCounts.Add(barangay, count);
                    }
                }
            }

            using (SqlCommand command = new SqlCommand(patientCountQueryHypertensive, conn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string barangay = reader["brgy"].ToString();
                        int count = Convert.ToInt32(reader["Count"]);

                        hypertensiveCounts.Add(barangay, count);
                    }
                }
            }

            return Tuple.Create(diabeticCounts, hypertensiveCounts);
        }

        // ... rest of the class ...
    

    private List<string> BarangayList()
        {
            return new List<string>
            {
                "Araw at Bituin", "Bagong Sikat", "Banaag ng Pag-asa", "Binacas", "Cabra", "Likas ng Silangan", "Maguinhawa", "Maligaya", "Maliig",
                "Ninikat ng Pag-asa", "Paraiso", "Sorville", "Tagbac", "Tangal", "Tilik", "Tumibo", "Vigo"
            };
        }

    }
}
