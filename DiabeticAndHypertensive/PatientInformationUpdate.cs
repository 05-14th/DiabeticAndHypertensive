using AForge.Video.DirectShow;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiabeticAndHypertensive
{
    public partial class PatientInformationUpdate : Form
    {
        //private string connectionString = "Data Source=LAPTOP-03VSR27V\\SQLEXPRESS1;Initial Catalog=had;Integrated Security=True";
        string connectionString = "Server=localhost\\MSSQLSERVER01;Initial Catalog=had;Integrated Security=True;";
        private SqlConnection connection;
        private Panel panelContent;
        private FilterInfoCollection videoDevices;
        private VideoCaptureDevice videoSource;
        public static Panel PnlContent { get; set; }
        public PatientInformationUpdate()
        {
            InitializeComponent();
            
        }
        public void LoadFormInPanel(Panel panel)
        {
            // Clear the panel and add this form to it
            panel.Controls.Clear();
            TopLevel = false;
            FormBorderStyle = FormBorderStyle.None;
            Dock = DockStyle.Fill;
            panel.Controls.Add(this);
            Show();
        }
        private void PatientInformationUpdate_Load(object sender, EventArgs e)
        {
            LoadAllPatients();
            PrescribedMedicines();
            PrescribedIndividualMedicines();
          
        }
        private string GetValue(DataGridViewCell cell)
        {
            return cell?.Value?.ToString() ?? string.Empty;
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
        private void btnUploadImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.jpg; *.jpeg; *.png; *.bmp)|*.jpg; *.jpeg; *.png; *.bmp|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Get the selected image file path
                string imagePath = openFileDialog.FileName;

                // Display the selected image in the pictureBoxUser
                patientImage.Image = Image.FromFile(imagePath);

                // Optionally, store the image data in a variable for later use
                byte[] imageData = ConvertImageToByteArray(patientImage.Image);
            }
        }
        private byte[] ConvertImageToByteArray(Image image)
        {
            if (image == null)
            {
                // Handle the case where the image is null, return an empty byte array or handle it as needed
                return Array.Empty<byte>();
            }

            using (MemoryStream memoryStream = new MemoryStream())
            {
                image.Save(memoryStream, ImageFormat.Png); // Change the format as needed
                return memoryStream.ToArray();
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            string category = cbCategory.Text;
            string fullname = $"{txtLname.Text} {txtFname.Text} {txtMI.Text} {cbSuffix.Text}";
            string lname = txtLname.Text;
            string fname = txtFname.Text;
            string mi = txtMI.Text;
            string suffix = cbSuffix.Text;
            DateTime dob = dtpDob.Value.Date;
            string age = txtAge.Text;
            string sex = cbSex.Text;
            string brgy = cbBarangay.Text;
            string phealth = txtPhealth.Text;
            string contact = txtContact.Text;
            string mednameDiabetic = string.Join(Environment.NewLine, new[] { txtmed1.Text, txtmed2.Text, txtmed3.Text, txtmed4.Text, txtmed5.Text, txtmed6.Text, txtmed7.Text, txtmed8.Text, txtmed9.Text }.Select(x => x.ToString()));
            string mednameHypertensive = string.Join(Environment.NewLine, new[] { txtmed1.Text, txtmed2.Text, txtmed3.Text, txtmed4.Text, txtmed5.Text, txtmed6.Text, txtmed7.Text, txtmed8.Text, txtmed9.Text }.Select(x => x.ToString()));
            string day = string.Join(Environment.NewLine, new[] { txtDaily1.Text, txtDaily2.Text, txtDaily3.Text, txtDaily4.Text, txtDaily5.Text, txtDaily6.Text, txtDaily7.Text, txtDaily8.Text, txtDaily9.Text }.Select(x => x.ToString()));
            string month = string.Join(Environment.NewLine, new[] { txtMonthly1.Text, txtMonthly2.Text, txtMonthly3.Text, txtMonthly4.Text, txtMonthly5.Text, txtMonthly6.Text, txtMonthly7.Text, txtMonthly8.Text, txtMonthly9.Text }.Select(x => x.ToString()));
            string bp = cbBp.Text;
            string smoker = cbSmoker.Text;
            string exercise = cbExercise.Text;
            string history = txtHistory.Text;
            byte[] imageBytes = ConvertImageToByteArray(patientImage.Image);

            if (string.IsNullOrWhiteSpace(cbCategory.Text))
            {
                HighlightField(cbCategory, "Please select patient type");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtLname.Text))
            {
                HighlightField(txtLname, "Last name is required");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtFname.Text))
            {
                HighlightField(txtFname, "First name is required");
                return;
            }
            if (string.IsNullOrWhiteSpace(dtpDob.Text))
            {
                HighlightField(dtpDob, "Birthdate is required");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtAge.Text))
            {
                HighlightField(txtAge, "Age is required");
                return;
            }

            if (string.IsNullOrWhiteSpace(cbSex.Text))
            {
                HighlightField(cbSex, "Gender is required");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtPhealth.Text))
            {
                HighlightField(txtPhealth, "PhilHealth ID Number is required");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtmed1.Text))
            {
                HighlightField(txtmed1, "Prescribed Medicine is required");
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string selectedCategory = cbCategory.SelectedItem.ToString();

                string updatequery = "";

                switch (selectedCategory)
                {
                    case "Diabetic Patient":
                        updatequery = "UPDATE PatientDiabetic SET category = @category, fullname = @fullname,lname = @lname, fname = @fname, mi = @mi, suffix = @suffix, dob = @dob, age = @age, sex = @sex, brgy = @brgy, phealth = @phealth, contact = @contact, medname = @medname, day = @day, month = @month, bp = @bp, smoker = @smoker, exercise = @exercise ,history = @history, image = @imageBytes WHERE Id = @Id";
                        break;

                    case "Hypertensive Patient":
                        updatequery = "UPDATE PatientHypertensive SET category = @category,fullname = @fullname, lname = @lname, fname = @fname, mi = @mi, suffix = @suffix, dob = @dob, age = @age, sex = @sex, brgy = @brgy, phealth = @phealth, contact = @contact, medname = @medname, day = @day, month = @month, bp = @bp, smoker = @smoker, exercise = @exercise,history = @history, image = @imageBytes WHERE Id = @Id";
                        break;
                }

                using (SqlCommand cmd = new SqlCommand(updatequery, connection))
                {
                    cmd.Parameters.AddWithValue("@category", category);
                    cmd.Parameters.AddWithValue("@fullname", fullname);
                    cmd.Parameters.AddWithValue("@lname", lname);
                    cmd.Parameters.AddWithValue("@fname", fname);
                    cmd.Parameters.AddWithValue("@mi", mi);
                    cmd.Parameters.AddWithValue("@suffix", suffix);
                    cmd.Parameters.AddWithValue("@dob", dob);
                    cmd.Parameters.AddWithValue("@age", age);
                    cmd.Parameters.AddWithValue("@sex", sex);
                    cmd.Parameters.AddWithValue("@brgy", brgy);
                    cmd.Parameters.AddWithValue("@phealth", phealth);
                    cmd.Parameters.AddWithValue("@contact", contact);
                    cmd.Parameters.AddWithValue("@medname", mednameDiabetic);
                    cmd.Parameters.AddWithValue("@medname", mednameHypertensive);
                    cmd.Parameters.AddWithValue("@day", day);
                    cmd.Parameters.AddWithValue("@month", month);
                    cmd.Parameters.AddWithValue("@bp", bp);
                    cmd.Parameters.AddWithValue("@smoker", smoker);
                    cmd.Parameters.AddWithValue("@exercise", exercise);
                    cmd.Parameters.AddWithValue("@history", history);
                    cmd.Parameters.AddWithValue("@image", imageBytes);
                   // cmd.Parameters.AddWithValue("@Id", selectedRow.Cells["Id"].Value);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    // Check if both updates were successful
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Patient information updated successfully!\n\nPress OK to continue.", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Failed to update patient information!\n\nPress OK to continue. ", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        private void HighlightField(Control control, string message)
        {
            // Set the forecolor to red
            control.ForeColor = Color.Red;

            // Show the validation message in MessageBox or any other way you prefer
            MessageBox.Show(message, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void LoadAllPatients()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlDataAdapter adapterDiabetic = new SqlDataAdapter("SELECT * FROM TableDiabeticPatient", connection);
                SqlDataAdapter adapterHypertensive = new SqlDataAdapter("SELECT * FROM TableHypertensivePatient", connection);

                DataTable dtDiabetic = new DataTable();
                adapterDiabetic.Fill(dtDiabetic);
                DataTable dtHypertensive = new DataTable();
                adapterHypertensive.Fill(dtHypertensive);

                // Merge the data from both tables
                dtDiabetic.Merge(dtHypertensive);
            }
        }
        private HashSet<string> addedMedicines = new HashSet<string>();
        private void PrescribedMedicines()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
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
        }

        private void PrescribedIndividualMedicines()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
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
        }

        private void dtpDob_ValueChanged(object sender, EventArgs e)
        {
            DateTime time_start = Convert.ToDateTime(dtpDob.Value);
            DateTime time_end = DateTime.Today;
            TimeSpan span = time_end - time_start;
            var daysTotal = span.TotalDays;
            var yearsTotal = Math.Truncate(daysTotal / 365);
            txtAge.Text = Convert.ToString(yearsTotal);

        }

        private void txtPhealth_TextChanged(object sender, EventArgs e)
        {
            if (txtPhealth.Text.Any(char.IsLetter))
            {
                txtPhealth.Clear();
            }
            else
            {
                string inputText = new string(txtPhealth.Text.Where(char.IsDigit).ToArray());

                // Check if the input length is within the expected range
                if (inputText.Length >= 12)
                {
                    // Format the numeric string as desired
                    string formattedText = string.Format("{0} {1} {2}", inputText.Substring(0, 4), inputText.Substring(4, 4), inputText.Substring(8));

                    // Update the text box with the formatted text
                    txtPhealth.Text = formattedText;

                    // Set the cursor position to the end of the text box
                    txtPhealth.SelectionStart = txtPhealth.Text.Length;
                }
            }
        }

        private void txtFname_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtFname.Text))
            {
                TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                txtFname.Text = textInfo.ToTitleCase(txtFname.Text);

                txtFname.SelectionStart = txtFname.Text.Length;
            }
        }

        private void txtLname_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtLname.Text))
            {
                TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                txtLname.Text = textInfo.ToTitleCase(txtLname.Text);

                txtLname.SelectionStart = txtLname.Text.Length;
            }
        }

        private void txtContact_TextChanged(object sender, EventArgs e)
        {
            if (txtContact.Text.Any(char.IsLetter))
            {
                txtContact.Clear();
            }
            else
            {
                string inputText = new string(txtContact.Text.Where(char.IsDigit).ToArray());

                if (inputText.Length >= 11)
                {
                    // Format the numeric string as desired
                    string formattedText = string.Format("{0} {1} {2}", inputText.Substring(0, 4), inputText.Substring(4, 4), inputText.Substring(8));

                    txtContact.Text = formattedText;

                    // Set the cursor position to the end of the text box
                    txtContact.SelectionStart = txtContact.Text.Length;
                }
            }
        }

        private void txtDaily9_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(txtDaily9.Text, out int medicationFrequency))
            {
                // Calculate the Monthly Consumption based on the Medication Frequency
                int daysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
                int monthlyConsumption = medicationFrequency * daysInMonth;

                // Display the result in textBox_tabmonconsumtion
                txtMonthly9.Text = monthlyConsumption.ToString();
            }
            else
            {
                txtMonthly9.Text = ""; // Clear the Monthly Consumption if the input is not valid.
            }
        }

        private void txtDaily8_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(txtDaily8.Text, out int medicationFrequency))
            {
                // Calculate the Monthly Consumption based on the Medication Frequency
                int daysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
                int monthlyConsumption = medicationFrequency * daysInMonth;

                // Display the result in textBox_tabmonconsumtion
                txtMonthly8.Text = monthlyConsumption.ToString();
            }
            else
            {
                txtMonthly8.Text = ""; // Clear the Monthly Consumption if the input is not valid.
            }
        }

        private void txtDaily7_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(txtDaily7.Text, out int medicationFrequency))
            {
                // Calculate the Monthly Consumption based on the Medication Frequency
                int daysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
                int monthlyConsumption = medicationFrequency * daysInMonth;

                // Display the result in textBox_tabmonconsumtion
                txtMonthly7.Text = monthlyConsumption.ToString();
            }
            else
            {
                txtMonthly7.Text = ""; // Clear the Monthly Consumption if the input is not valid.
            }
        }

        private void txtDaily6_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(txtDaily6.Text, out int medicationFrequency))
            {
                // Calculate the Monthly Consumption based on the Medication Frequency
                int daysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
                int monthlyConsumption = medicationFrequency * daysInMonth;

                // Display the result in textBox_tabmonconsumtion
                txtMonthly6.Text = monthlyConsumption.ToString();
            }
            else
            {
                txtMonthly6.Text = ""; // Clear the Monthly Consumption if the input is not valid.
            }
        }

        private void txtDaily5_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(txtDaily5.Text, out int medicationFrequency))
            {
                // Calculate the Monthly Consumption based on the Medication Frequency
                int daysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
                int monthlyConsumption = medicationFrequency * daysInMonth;

                // Display the result in textBox_tabmonconsumtion
                txtMonthly5.Text = monthlyConsumption.ToString();
            }
            else
            {
                txtMonthly5.Text = ""; // Clear the Monthly Consumption if the input is not valid.
            }
        }

        private void txtDaily4_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(txtDaily4.Text, out int medicationFrequency))
            {
                // Calculate the Monthly Consumption based on the Medication Frequency
                int daysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
                int monthlyConsumption = medicationFrequency * daysInMonth;

                // Display the result in textBox_tabmonconsumtion
                txtMonthly4.Text = monthlyConsumption.ToString();
            }
            else
            {
                txtMonthly4.Text = ""; // Clear the Monthly Consumption if the input is not valid.
            }
        }

        private void txtDaily3_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(txtDaily3.Text, out int medicationFrequency))
            {
                // Calculate the Monthly Consumption based on the Medication Frequency
                int daysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
                int monthlyConsumption = medicationFrequency * daysInMonth;

                // Display the result in textBox_tabmonconsumtion
                txtMonthly3.Text = monthlyConsumption.ToString();
            }
            else
            {
                txtMonthly3.Text = ""; // Clear the Monthly Consumption if the input is not valid.
            }
        }
        private void txtDaily2_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(txtDaily2.Text, out int medicationFrequency))
            {
                // Calculate the Monthly Consumption based on the Medication Frequency
                int daysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
                int monthlyConsumption = medicationFrequency * daysInMonth;

                // Display the result in textBox_tabmonconsumtion
                txtMonthly2.Text = monthlyConsumption.ToString();
            }
            else
            {
                txtMonthly2.Text = ""; // Clear the Monthly Consumption if the input is not valid.
            }
        }
        private void txtDaily1_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(txtDaily1.Text, out int medicationFrequency))
            {
                // Calculate the Monthly Consumption based on the Medication Frequency
                int daysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
                int monthlyConsumption = medicationFrequency * daysInMonth;

                // Display the result in textBox_tabmonconsumtion
                txtMonthly1.Text = monthlyConsumption.ToString();
            }
            else
            {
                txtMonthly1.Text = ""; // Clear the Monthly Consumption if the input is not valid.
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            string phealth = txtPhealth.Text;
            string category = cbCategory.Text;

            // Confirm deletion
            DialogResult result = MessageBox.Show("Do you want to delete this patient record?", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Delete the record from the database
                if (DeleteRecord(phealth, category))
                {
                    if (category == "Both Patient")
                    {
                        // If category is "Both Patient", call DeleteRecord for the other category as well
                        if (DeleteRecord(phealth, "Diabetic Patient") && DeleteRecord(phealth, "Hypertensive Patient"))
                        {
                            MessageBox.Show("Patient record deleted.", "Delete Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Failed to delete patient record.", "Delete Record", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Patient record deleted.", "Delete Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Failed to delete patient record.", "Delete Record", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (result == DialogResult.No)
            {
                // User chose not to delete
                MessageBox.Show("Patient record not deleted.", "Delete Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private bool DeleteRecord(string phealth, string category)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string deleteQuery = "";

                    switch (category)
                    {
                        case "Diabetic Patient":
                            deleteQuery = "DELETE FROM TableDiabeticPatient WHERE phealth = @phealth";
                            break;

                        case "Hypertensive Patient":
                            deleteQuery = "DELETE FROM TableHypertensivePatient WHERE phealth = @phealth";
                            break;

                        case "Both Patient":
                            // Do not execute the query here for "Both Patient"
                            break;

                            // Add more cases for other categories if needed
                    }

                    if (!category.Equals("Both Patient"))
                    {
                        using (SqlCommand cmd = new SqlCommand(deleteQuery, connection))
                        {
                            cmd.Parameters.AddWithValue("@phealth", phealth);

                            cmd.ExecuteNonQuery();
                        }
                    }

                    return true; // Return true on successful deletion
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting patient record: {ex.Message}", "Delete Record", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false; // Return false on error
            }
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            
        }

        
    }
}
        


       