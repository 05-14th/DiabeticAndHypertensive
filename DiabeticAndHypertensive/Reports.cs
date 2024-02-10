using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OfficeOpenXml;

namespace DiabeticAndHypertensive
{
    public partial class Reports : Form
    {
        string connectionString = "Data Source=localhost\\MSSQLSERVER01;Initial Catalog=had;Integrated Security=True;";
        //private string connectionString = "Data Source=LAPTOP-03VSR27V\\SQLEXPRESS1;Initial Catalog=had;Integrated Security=True";
        public Reports()
        {
            InitializeComponent();
        }

        private void Reports_Load(object sender, EventArgs e)
        {
           // OpenPanelContent(new RFront());
            cbChooseReports.SelectedIndex = 0;
        }
        private Form activeForm = null;
        private void OpenPanelContent(Form panelContent)
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

        private void UpdateReport(string tableName, string parameter,string condition, string condition2 = " ")
        {
            DataTable dataTable = new DataTable();
            string query = $"SELECT * FROM {tableName} WHERE {parameter} = '{condition}' OR {parameter} = '{condition2}'";

            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {
                    adapter.Fill(dataTable);
                }

                connection.Close();
            }

            reportGrid.DataSource = dataTable;
        }

       private void ExportToCSV(DataGridView dataGridView, string filePath)
        {
            try
            {
                StringBuilder sb = new StringBuilder();

                // Column headers
                foreach (DataGridViewColumn column in dataGridView.Columns)
                {
                    sb.Append(column.HeaderText + ",");
                }

                sb.AppendLine();

                // Rows
                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        sb.Append(cell.Value + ",");
                    }
                    sb.AppendLine();
                }

                // Write to file
                File.WriteAllText(filePath, sb.ToString());

                MessageBox.Show("CSV file exported successfully!", "Export to CSV", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error exporting to CSV: " + ex.Message, "Export to CSV", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbChooseReports_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedReport = cbChooseReports.SelectedItem.ToString();

            if (selectedReport == "Diabetic Patient List")
            {
                //OpenPanelContent(new RPatientDiabetic());
                UpdateReport("TablePatient", "category", "Diabetic Patient", "Both Patient");
            }
            else if (selectedReport == "Hypertensive Patient List")
            {
                //OpenPanelContent(new RPatientHypertensive());
                UpdateReport("TablePatient", "category", "Hypertensive Patient", "Both Patient");
            }
            else if (selectedReport == "Diabetic Medicine Inventory List")
            {
                //OpenPanelContent(new RInventoryDiabetic());
                UpdateReport("StockInDiabetic", "category", "Diabetic Medicine");
            }
            else if (selectedReport == "Hypertensive Medicine Inventory List")
            {
                //OpenPanelContent(new RInventoryHypertensive());
                UpdateReport("StockInDiabetic", "category", "Hypertensive Medicine");
            }
            else if (selectedReport == "Diabetic Distribution List")
            {
                //OpenPanelContent(new RDistributionDiabetic());
                UpdateReport("StockOutDiabetic", "category", "Diabetic Medicine");
            }
            else if (selectedReport == "Hypertensive Distribution List")
            {
                UpdateReport("StockOutDiabetic", "category", "Hypertensive Medicine");
            }    
        }

        private void export_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "CSV Files (*.csv)|*.csv";
            saveFileDialog.Title = "Save CSV File";
            saveFileDialog.ShowDialog();

            if (saveFileDialog.FileName != "")
            {
                ExportToCSV(reportGrid, saveFileDialog.FileName);
            }
        }
    }
}
