using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System.Linq;
using System.Drawing;
using System.IO;
using static System.Net.Mime.MediaTypeNames;

namespace DiabeticAndHypertensive
{
    public partial class PatientInformationView : Form
    {
        string connectionString = "Data Source=localhost\\MSSQLSERVER01;Initial Catalog=had;Integrated Security=True;";
        //private string connectionString = "Data Source=LAPTOP-03VSR27V\\SQLEXPRESS1;Initial Catalog=had;Integrated Security=True";
        private SqlConnection connection;
        private int Id;

        public void LoadFormInPanel(Panel panel)
        {
            // Clear panel and add this form to it
            panel.Controls.Clear();
            TopLevel = false;
            FormBorderStyle = FormBorderStyle.None;
            Dock = DockStyle.Fill;
            panel.Controls.Add(this);
            Show();
        }

        public PatientInformationView(string value)
        {
            InitializeComponent();
            
            // Assign the pnlContent passed from PatientInformation
            connection = new SqlConnection(connectionString);
            PopulateFormData(value); 
        }

        private void PopulateFormData(string id)
        {
            // SQL query to fetch data based on patientId
            string query = "";

            // Determine which table to query based on the data available
            if (!id.Equals(null))
            {
                query = $"SELECT * FROM TablePatient WHERE Id = '{id}'";
            }
            else
            {
                // Handle the case where patientId doesn't match either table
                MessageBox.Show("Patient not found in the table.");
                return;
            }

            // Create connection and command objects
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameter for patientId
                    command.Parameters.AddWithValue("@Id", Id);

                    try
                    {
                        // Open connection
                        connection.Open();

                        // Execute the command and get the SqlDataReader
                        SqlDataReader reader = command.ExecuteReader();

                        // Check if there are rows returned
                        if (reader.Read())
                        {
                            // Populate text boxes with data
                            cbCategory.Text = reader["category"].ToString();
                            txtLname.Text = reader["lname"].ToString();
                            txtFname.Text = reader["fname"].ToString();
                            txtMI.Text = reader["mi"].ToString();
                            cbSuffix.Text = reader["suffix"].ToString();
                            dtpDob.Text = reader["dob"].ToString();
                            txtAge.Text = reader["age"].ToString();
                            cbSex.Text = reader["sex"].ToString();
                            cbBarangay.Text = reader["brgy"].ToString();
                            txtPhealth.Text = reader["phealth"].ToString();
                            txtContact.Text = reader["contact"].ToString();
                            // Medicine names
                            txtmed1.Text = reader["medname"].ToString();
                            txtmed2.Text = reader["medname"].ToString();
                            txtmed3.Text = reader["medname"].ToString();
                            txtmed4.Text = reader["medname"].ToString();
                            txtmed5.Text = reader["medname"].ToString();
                            txtmed6.Text = reader["medname"].ToString();
                            txtmed7.Text = reader["medname"].ToString();
                            txtmed8.Text = reader["medname"].ToString();
                            txtmed9.Text = reader["medname"].ToString();
                            // Daily frequency
                            txtDaily1.Text = reader["day"].ToString();
                            txtDaily2.Text = reader["day"].ToString();
                            txtDaily3.Text = reader["day"].ToString();
                            txtDaily4.Text = reader["day"].ToString();
                            txtDaily5.Text = reader["day"].ToString();
                            txtDaily6.Text = reader["day"].ToString();
                            txtDaily7.Text = reader["day"].ToString();
                            txtDaily8.Text = reader["day"].ToString();
                            txtDaily9.Text = reader["day"].ToString();

                            // Monthly frequency
                            txtMonthly1.Text = reader["month"].ToString();
                            txtMonthly3.Text = reader["month"].ToString();
                            txtMonthly4.Text = reader["month"].ToString();
                            txtMonthly5.Text = reader["month"].ToString();
                            txtMonthly6.Text = reader["month"].ToString();
                            txtMonthly7.Text = reader["month"].ToString();
                            txtMonthly8.Text = reader["month"].ToString();
                            txtMonthly9.Text = reader["month"].ToString();

                            cbBp.Text = reader["bp"].ToString();
                            cbExercise.Text = reader["exercise"].ToString();
                            txtHistory.Text = reader["history"].ToString();
                            // Populate image if applicable
                            if (reader["image"] != DBNull.Value)
                            {
                                byte[] imageData = (byte[])reader["image"];
                                using (MemoryStream ms = new MemoryStream(imageData))
                                {
                                    patientImage.Image = System.Drawing.Image.FromStream(ms);
                                }
                            }
                        }
                        else
                        {
                            // Handle the case where no data is found for the patientId
                            MessageBox.Show("No data found for the specified patient.");
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle exception
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }

        // Function to check if patient is diabetic based on patientId
        private bool isPatientDiabetic(int Id)
        {
            return true;
            // Your logic to determine if the patient is diabetic based on patientId
        }

        // Function to check if patient is hypertensive based on patientId
        private bool isPatientHypertensive(int Id)
        {
            return true;
            // Your logic to determine if the patient is hypertensive based on patientId
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PatientInformationView_Load(object sender, EventArgs e)
        {
            PrescribedMedicines();
            PrescribedIndividualMedicines();
        }

        private HashSet<string> addedMedicines = new HashSet<string>();
        private void PrescribedMedicines()
        {
            try
            {
                connection.Open();

                string queryDiabetic = "SELECT medname FROM InventoryDiabetic";

                using (SqlCommand cmd = new SqlCommand(queryDiabetic, connection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string medname = reader["medname"].ToString();
                            if (!checkedListBoxDiabetic.Items.Contains(medname))
                            {
                                checkedListBoxDiabetic.Items.Add(medname);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading diabetic medicines: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

            try
            {
                connection.Open();

                string queryHypertensive = "SELECT medname FROM InventoryHypertensive";

                using (SqlCommand cmd = new SqlCommand(queryHypertensive, connection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string medname = reader["medname"].ToString();
                            if (!checkedListBoxHypertensive.Items.Contains(medname))
                            {
                                checkedListBoxHypertensive.Items.Add(medname);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading hypertensive medicines: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
        private void PrescribedIndividualMedicines()
        {
            try
            {
                connection.Open();

                string queryDiabetic = "SELECT medname FROM InventoryDiabetic";

                using (SqlCommand cmd = new SqlCommand(queryDiabetic, connection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string medname = reader["medname"].ToString();
                            if (!clbDiabeticMedicine.Items.Contains(medname))
                            {
                                clbDiabeticMedicine.Items.Add(medname);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading diabetic medicines: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

            try
            {
                connection.Open();

                string queryHypertensive = "SELECT medname FROM InventoryHypertensive";

                using (SqlCommand cmd = new SqlCommand(queryHypertensive, connection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string medname = reader["medname"].ToString();
                            if (!clbHypertensive.Items.Contains(medname))
                            {
                                clbHypertensive.Items.Add(medname);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading hypertensive medicines: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        private void btnAddPrescribedMedicine_Click(object sender, EventArgs e)
        {
            ClearTextBoxes();

            // Populate textboxes based on checked items in Diabetic list box
            PopulateTextBoxes(txtmed1, txtmed2, txtmed3, txtmed4, txtmed5, txtmed6, txtmed7, txtmed8, txtmed9, checkedListBoxDiabetic);
            PopulateTextBoxes(txtmed1, txtmed2, txtmed3, txtmed4, txtmed5, txtmed6, txtmed7, txtmed8, txtmed9, clbDiabeticMedicine);

            // Populate textboxes based on checked items in Hypertensive list box
            PopulateTextBoxes(txtmed1, txtmed2, txtmed3, txtmed4, txtmed5, txtmed6, txtmed7, txtmed8, txtmed9, checkedListBoxHypertensive);
            PopulateTextBoxes(txtmed1, txtmed2, txtmed3, txtmed4, txtmed5, txtmed6, txtmed7, txtmed8, txtmed9, clbHypertensive);
        }
        private void ClearTextBoxes()
        {
            // Clear all textboxes
            txtmed1.Text = string.Empty;
            txtmed2.Text = string.Empty;
            txtmed3.Text = string.Empty;
            txtmed4.Text = string.Empty;
            txtmed5.Text = string.Empty;
            txtmed6.Text = string.Empty;
            txtmed7.Text = string.Empty;
            txtmed8.Text = string.Empty;
            txtmed9.Text = string.Empty;

        }
        private void PopulateTextBoxes(TextBox textBox1, TextBox textBox2, TextBox textBox3, TextBox textBox4, TextBox textBox5, TextBox textBox6, TextBox textBox7, TextBox textBox8, TextBox textBox9, CheckedListBox checkedListBox)
        {
            // Iterate through checked items and populate corresponding textboxes
            for (int i = 0; i < Math.Min(checkedListBox.CheckedItems.Count, 9); i++)
            {
                string itemName = checkedListBox.CheckedItems[i].ToString();
                switch (i)
                {
                    case 0:
                        textBox1.Text = itemName;
                        break;
                    case 1:
                        textBox2.Text = itemName;
                        break;
                    case 2:
                        textBox3.Text = itemName;
                        break;
                    case 3:
                        textBox4.Text = itemName;
                        break;
                    case 4:
                        textBox5.Text = itemName;
                        break;
                    case 5:
                        textBox6.Text = itemName;
                        break;
                    case 6:
                        textBox7.Text = itemName;
                        break;
                    case 7:
                        textBox8.Text = itemName;
                        break;
                    case 8:
                        textBox9.Text = itemName;
                        break;
                }
            }
        }
        private void btnRemovePrescribedMedicine_Click(object sender, EventArgs e)
        {
            RemovePrescribedMedicine(txtmed1, clbDiabeticMedicine, clbHypertensive, checkedListBoxDiabetic, checkedListBoxHypertensive);
            RemovePrescribedMedicine(txtmed2, clbDiabeticMedicine, clbHypertensive, checkedListBoxDiabetic, checkedListBoxHypertensive);
            RemovePrescribedMedicine(txtmed3, clbDiabeticMedicine, clbHypertensive, checkedListBoxDiabetic, checkedListBoxHypertensive);
            RemovePrescribedMedicine(txtmed4, clbDiabeticMedicine, clbHypertensive, checkedListBoxDiabetic, checkedListBoxHypertensive);
            RemovePrescribedMedicine(txtmed5, clbDiabeticMedicine, clbHypertensive, checkedListBoxDiabetic, checkedListBoxHypertensive);
            RemovePrescribedMedicine(txtmed6, clbDiabeticMedicine, clbHypertensive, checkedListBoxDiabetic, checkedListBoxHypertensive);
            RemovePrescribedMedicine(txtmed7, clbDiabeticMedicine, clbHypertensive, checkedListBoxDiabetic, checkedListBoxHypertensive);
            RemovePrescribedMedicine(txtmed8, clbDiabeticMedicine, clbHypertensive, checkedListBoxDiabetic, checkedListBoxHypertensive);
            RemovePrescribedMedicine(txtmed9, clbDiabeticMedicine, clbHypertensive, checkedListBoxDiabetic, checkedListBoxHypertensive);
        }
        private void RemovePrescribedMedicine(TextBox textBox, CheckedListBox clbDiabeticMedicine, CheckedListBox clbHypertensive, CheckedListBox checkedListBoxDiabetic, CheckedListBox checkedListBoxHypertensive)
        {
            // Check if the TextBox has a value
            if (!string.IsNullOrWhiteSpace(textBox.Text))
            {
                // Prompt the user for confirmation
                DialogResult result = MessageBox.Show($"Do you want to remove the prescribed medicine '{textBox.Text}'?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Uncheck the corresponding items in the CheckedListBoxes
                    UncheckItem(textBox.Text, clbDiabeticMedicine);
                    UncheckItem(textBox.Text, clbHypertensive);
                    UncheckItem(textBox.Text, checkedListBoxDiabetic);
                    UncheckItem(textBox.Text, checkedListBoxHypertensive);

                    // Clear the TextBox
                    textBox.Clear();
                }
            }
            else
            {

            }

        }
        private void UncheckItem(string itemName, CheckedListBox checkedListBox)
        {
            // Uncheck the corresponding item in the CheckedListBox
            for (int i = 0; i < checkedListBox.Items.Count; i++)
            {
                if (checkedListBox.GetItemChecked(i) && checkedListBox.Items[i].ToString() == itemName)
                {
                    checkedListBox.SetItemChecked(i, false);
                    break; // Break the loop after unchecking the item
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {

        }

        public void PatientInfo(String str, String str2, String str3,
            String str4, String str5, String str6, String str7, String str8,
            String str9, String str10, DateTime dt, String str11)
        {
            txtLname.Text = str;
            txtPhealth.Text = str2;
            cbCategory.Text = str3;
            txtFname.Text = str4;
            txtMI.Text = str5;
            txtContact.Text = str6;
            cbSuffix.Text = str7;
            txtAge.Text = str8;
            cbSex.Text = str9;
            cbBarangay.Text = str10;
            dtpDob.Value = dt;
            if(str11 == "GLIBENCLAMIDE 5 MG")
            {

            }
        }
    }

    
}
