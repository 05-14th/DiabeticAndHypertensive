using AForge.Video;
using AForge.Video.DirectShow;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using MediaTypeNames = System.Net.Mime.MediaTypeNames;

namespace DiabeticAndHypertensive
{
    public partial class PatientInformationAdd : Form
    {
        string connectionString = "Server=localhost\\MSSQLSERVER01;Initial Catalog=had;Integrated Security=True";
        //private string connectionString = "Data Source=LAPTOP-03VSR27V\\SQLEXPRESS1;Initial Catalog=had;Integrated Security=True";
        private SqlConnection connection;
        private Panel panelContent;
        private FilterInfoCollection videoDevices;
        private VideoCaptureDevice videoSource;
        private string logUsername;

        public PatientInformationAdd()
        {
            InitializeComponent();

            connection = new SqlConnection(connectionString);
            PrescribedMedicines();
            PrescribedIndividualMedicines();


            // Initialize camera
            InitializeCamera();
        }
        private void InitializeCamera()
        {
            // Enumerate video devices
            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            // Check if there is at least one camera
            if (videoDevices.Count > 0)
            {
                // Use the first camera found
                videoSource = new VideoCaptureDevice(videoDevices[0].MonikerString);
                videoSource.NewFrame += VideoSource_NewFrame;
            }
            else
            {
                MessageBox.Show("No camera found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnCamera.Enabled = false;
            }
        }
        private void VideoSource_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            // Display the captured frame in a PictureBox or process it as needed
            // For example, you can display it in a PictureBox named "pictureBoxCamera"
            patientImage.Image = (Bitmap)eventArgs.Frame.Clone();
        }
        private void PatientInformationAdd_Load(object sender, EventArgs e)
        {

        }
        private void btnCamera_Click(object sender, EventArgs e)
        {
            pnlTakephoto.SendToBack();
            if (videoSource != null && !videoSource.IsRunning)
            {
                videoSource.Start();
            }
        }
        private void btnCapture_Click(object sender, EventArgs e)
        {
            // Stop capturing
            if (videoSource != null && videoSource.IsRunning)
            {
                videoSource.SignalToStop();
                videoSource.WaitForStop();
            }

            // Check if there is a captured frame
            if (patientImage.Image != null)
            {
                // Optionally, you can save the image or perform further processing
                // For now, let's just display the image in a PictureBox named "pictureBoxCaptured"
                patientImage.Image = (Bitmap)patientImage.Image.Clone();
            }
            else
            {
                MessageBox.Show("No captured image available.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void btnCancelImage_Click(object sender, EventArgs e)
        {
            pnlTakephoto.BringToFront();
            if (videoSource != null && !videoSource.IsRunning)
            {
                videoSource.Stop();
            }
        }
        private void PatientInformationAdd_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Stop capturing when the form is closing
            if (videoSource != null && videoSource.IsRunning)
            {
                videoSource.SignalToStop();
                videoSource.WaitForStop();
            }
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
        private byte[] ConvertImageToByteArray(System.Drawing.Image image)
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

        private void btnInsert_Cl(object sender, EventArgs e)
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

                string query = "";
                string diabeticInsertQuery = "INSERT INTO PatientDiabetic (category,fullname, lname, fname, mi, suffix, dob, age, sex, brgy, phealth, contact,medname,day,month, bp, smoker, exercise, date, history,image) " +
                                        "VALUES (@category, @fullname, @lname, @fname, @mi, @suffix, @dob, @age, @sex, @brgy, @phealth, @contact, @mednameDiabetic, @day, @month, @bp, @smoker, @exercise, @date, @history, @image)";

                string hypertensiveInsertQuery = "INSERT INTO PatientHypertensive (category,fullname, lname, fname, mi, suffix, dob, age, sex, brgy, phealth, contact,medname,day,month, bp, smoker, exercise, date, history,image) " +
                                        "VALUES (@category, @fullname, @lname, @fname, @mi, @suffix, @dob, @age, @sex, @brgy, @phealth, @contact, @mednameHypertensive, @day, @month, @bp, @smoker, @exercise, @date, @history, @image)";

                string bothInsertQueryDiabetic = "INSERT INTO PatientDiabetic (category,fullname, lname, fname, mi, suffix, dob, age, sex, brgy, phealth, contact,medname,day,month, bp, smoker, exercise, date, history,image) " +
                                        "VALUES (@category, @fullname, @lname, @fname, @mi, @suffix, @dob, @age, @sex, @brgy, @phealth, @contact, @mednameDiabetic, @day, @month, @bp, @smoker, @exercise, @date, @history, @image)";

                string bothInsertQueryHypertensive = "INSERT INTO PatientHypertensive (category,fullname, lname, fname, mi, suffix, dob, age, sex, brgy, phealth, contact,medname,day,month, bp, smoker, exercise, date, history,image) " +
                                        "VALUES (@category, @fullname, @lname, @fname, @mi, @suffix, @dob, @age, @sex, @brgy, @phealth, @contact, @mednameHypertensive, @day, @month, @bp, @smoker, @exercise, @date, @history, @image)";

                switch (cbCategory.SelectedItem.ToString())
                {
                    case "Diabetic Patient":
                        query = diabeticInsertQuery;
                        using (SqlCommand cmdDiabetic = new SqlCommand(diabeticInsertQuery, connection))
                        {
                            cmdDiabetic.Parameters.AddWithValue("@category", category);
                            cmdDiabetic.Parameters.AddWithValue("@fullname", fullname);
                            cmdDiabetic.Parameters.AddWithValue("@lname", lname);
                            cmdDiabetic.Parameters.AddWithValue("@fname", fname);
                            cmdDiabetic.Parameters.AddWithValue("@mi", mi);
                            cmdDiabetic.Parameters.AddWithValue("@suffix", suffix);
                            cmdDiabetic.Parameters.AddWithValue("@dob", dob);
                            cmdDiabetic.Parameters.AddWithValue("@age", age);
                            cmdDiabetic.Parameters.AddWithValue("@sex", sex);
                            cmdDiabetic.Parameters.AddWithValue("@brgy", brgy);
                            cmdDiabetic.Parameters.AddWithValue("@phealth", phealth);
                            cmdDiabetic.Parameters.AddWithValue("@contact", contact);
                            cmdDiabetic.Parameters.AddWithValue("@mednameDiabetic", mednameDiabetic);
                            cmdDiabetic.Parameters.AddWithValue("@day", day);
                            cmdDiabetic.Parameters.AddWithValue("@month", month);
                            cmdDiabetic.Parameters.AddWithValue("@bp", bp);
                            cmdDiabetic.Parameters.AddWithValue("@smoker", smoker);
                            cmdDiabetic.Parameters.AddWithValue("@exercise", exercise);
                            cmdDiabetic.Parameters.AddWithValue("@date", DateTime.Now);
                            cmdDiabetic.Parameters.AddWithValue("@history", history);
                            cmdDiabetic.Parameters.AddWithValue("@image", imageBytes);


                            int rowsAffectedDiabetic = cmdDiabetic.ExecuteNonQuery();

                            if (rowsAffectedDiabetic > 0)
                            {
                                MessageBox.Show($"Patient information {lname} {fname} {mi} {suffix} added successfully!\n\nPress OK to continue.", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);



                            }
                            else
                            {
                                MessageBox.Show("Failed to add patient information!\n\nPress OK to continue. ", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                        break;

                    case "Hypertensive Patient":
                        query = hypertensiveInsertQuery;
                        using (SqlCommand cmdHypertensive = new SqlCommand(hypertensiveInsertQuery, connection))
                        {
                            cmdHypertensive.Parameters.AddWithValue("@category", category);
                            cmdHypertensive.Parameters.AddWithValue("@fullname", fullname);
                            cmdHypertensive.Parameters.AddWithValue("@lname", lname);
                            cmdHypertensive.Parameters.AddWithValue("@fname", fname);
                            cmdHypertensive.Parameters.AddWithValue("@mi", mi);
                            cmdHypertensive.Parameters.AddWithValue("@suffix", suffix);
                            cmdHypertensive.Parameters.AddWithValue("@dob", dob);
                            cmdHypertensive.Parameters.AddWithValue("@age", age);
                            cmdHypertensive.Parameters.AddWithValue("@sex", sex);
                            cmdHypertensive.Parameters.AddWithValue("@brgy", brgy);
                            cmdHypertensive.Parameters.AddWithValue("@phealth", phealth);
                            cmdHypertensive.Parameters.AddWithValue("@contact", contact);
                            cmdHypertensive.Parameters.AddWithValue("@mednameHypertensive", mednameHypertensive);
                            cmdHypertensive.Parameters.AddWithValue("@day", day);
                            cmdHypertensive.Parameters.AddWithValue("@month", month);
                            cmdHypertensive.Parameters.AddWithValue("@bp", bp);
                            cmdHypertensive.Parameters.AddWithValue("@smoker", smoker);
                            cmdHypertensive.Parameters.AddWithValue("@exercise", exercise);
                            cmdHypertensive.Parameters.AddWithValue("@date", DateTime.Now);
                            cmdHypertensive.Parameters.AddWithValue("@history", history);
                            cmdHypertensive.Parameters.AddWithValue("@image", imageBytes);


                            int rowsAffectedHypertensive = cmdHypertensive.ExecuteNonQuery();

                            if (rowsAffectedHypertensive > 0)
                            {
                                MessageBox.Show($"Patient information {lname} {fname} {mi} {suffix} added successfully!\n\nPress OK to continue.", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            }
                            else
                            {
                                MessageBox.Show("Failed to add patient information!\n\nPress OK to continue. ", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                        break;

                    case "Both Patient":
                        query = bothInsertQueryDiabetic;
                        using (SqlCommand commandDiabetic = new SqlCommand(bothInsertQueryDiabetic, connection))
                        {
                            commandDiabetic.Parameters.AddWithValue("@category", category);
                            commandDiabetic.Parameters.AddWithValue("@fullname", fullname);
                            commandDiabetic.Parameters.AddWithValue("@lname", lname);
                            commandDiabetic.Parameters.AddWithValue("@fname", fname);
                            commandDiabetic.Parameters.AddWithValue("@mi", mi);
                            commandDiabetic.Parameters.AddWithValue("@suffix", suffix);
                            commandDiabetic.Parameters.AddWithValue("@dob", dob);
                            commandDiabetic.Parameters.AddWithValue("@age", age);
                            commandDiabetic.Parameters.AddWithValue("@sex", sex);
                            commandDiabetic.Parameters.AddWithValue("@brgy", brgy);
                            commandDiabetic.Parameters.AddWithValue("@phealth", phealth);
                            commandDiabetic.Parameters.AddWithValue("@contact", contact);
                            commandDiabetic.Parameters.AddWithValue("@mednameDiabetic", mednameDiabetic);
                            commandDiabetic.Parameters.AddWithValue("@mednameHypertensive", mednameHypertensive);
                            commandDiabetic.Parameters.AddWithValue("@day", day);
                            commandDiabetic.Parameters.AddWithValue("@month", month);
                            commandDiabetic.Parameters.AddWithValue("@bp", bp);
                            commandDiabetic.Parameters.AddWithValue("@smoker", smoker);
                            commandDiabetic.Parameters.AddWithValue("@exercise", exercise);
                            commandDiabetic.Parameters.AddWithValue("@date", DateTime.Now);
                            commandDiabetic.Parameters.AddWithValue("@history", history);
                            commandDiabetic.Parameters.AddWithValue("@image", imageBytes);


                            int rowsAffectedDiabetic = commandDiabetic.ExecuteNonQuery();

                            if (rowsAffectedDiabetic > 0)
                            {
                                MessageBox.Show($"Patient information {lname} {fname} {mi} {suffix} added successfully!\n\nPress OK to continue.", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Failed to add patient information!\n\nPress OK to continue. ", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                        query = bothInsertQueryHypertensive;
                        using (SqlCommand commandHypertensive = new SqlCommand(bothInsertQueryHypertensive, connection))
                        {
                            commandHypertensive.Parameters.AddWithValue("@category", category);
                            commandHypertensive.Parameters.AddWithValue("@fullname", fullname);
                            commandHypertensive.Parameters.AddWithValue("@lname", lname);
                            commandHypertensive.Parameters.AddWithValue("@fname", fname);
                            commandHypertensive.Parameters.AddWithValue("@mi", mi);
                            commandHypertensive.Parameters.AddWithValue("@suffix", suffix);
                            commandHypertensive.Parameters.AddWithValue("@dob", dob);
                            commandHypertensive.Parameters.AddWithValue("@age", age);
                            commandHypertensive.Parameters.AddWithValue("@sex", sex);
                            commandHypertensive.Parameters.AddWithValue("@brgy", brgy);
                            commandHypertensive.Parameters.AddWithValue("@phealth", phealth);
                            commandHypertensive.Parameters.AddWithValue("@contact", contact);
                            commandHypertensive.Parameters.AddWithValue("@mednameHypertensive", mednameHypertensive);
                            commandHypertensive.Parameters.AddWithValue("@mednameDiabetic", mednameDiabetic);
                            commandHypertensive.Parameters.AddWithValue("@day", day);
                            commandHypertensive.Parameters.AddWithValue("@month", month);
                            commandHypertensive.Parameters.AddWithValue("@bp", bp);
                            commandHypertensive.Parameters.AddWithValue("@smoker", smoker);
                            commandHypertensive.Parameters.AddWithValue("@exercise", exercise);
                            commandHypertensive.Parameters.AddWithValue("@date", DateTime.Now);
                            commandHypertensive.Parameters.AddWithValue("@history", history);
                            commandHypertensive.Parameters.AddWithValue("@image", imageBytes);


                            int rowsAffectedHypertensive = commandHypertensive.ExecuteNonQuery();

                            if (rowsAffectedHypertensive > 0)
                            {
                                MessageBox.Show($"Patient information {lname} {fname} {mi} {suffix} added successfully!\n\nPress OK to continue.", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                History(fullname, category);
                            }
                            else
                            {
                                MessageBox.Show("Failed to add patient information!\n\nPress OK to continue. ", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                        break;
                }
                // Close the connection after executing the queries
                connection.Close();
            }
        }
        private void History(string fullname, string category)
        {
            DateTime date = DateTime.Now;
            string activity = $"Added patient {fullname} to {category}.";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string insertQuery = "INSERT INTO ActivityLog (username, date, activity)" +
                   "VALUES (@username, @date, @activity)";
                using (SqlCommand command = new SqlCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@username", logUsername);
                    command.Parameters.AddWithValue("@date", date);
                    command.Parameters.AddWithValue("@activity", activity);
                    command.ExecuteNonQuery();
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
        private void ResetFieldColors()
        {
            foreach (Control control in Controls)
            {
                if (control is TextBox || control is ComboBox)
                {
                    control.ForeColor = SystemColors.WindowText;
                }
            }
        }
        private HashSet<string> addedMedicines = new HashSet<string>();
        private void PrescribedMedicines()
        {
            try
            {
                connection.Open();

                string queryDiabetic = "SELECT medname FROM StockInDiabetic";

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

                string queryHypertensive = "SELECT medname FROM StockInHypertensive";

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
        private void PopulateTextBoxes(TextBox textBox1, TextBox textBox2, TextBox textBox3, TextBox textBox4, TextBox textBox5, TextBox textBox6, TextBox textBox7, TextBox textBox8, TextBox textBox9, CheckedListBox checkedListBox)
        {


            // Iterate through checked items and populate corresponding textboxes
            for (int i = 0; i < Math.Min(checkedListBox.CheckedItems.Count, 9); i++)
            {
                string itemName = checkedListBox.CheckedItems[i]?.ToString(); // Use null conditional operator to handle null items
                if (!string.IsNullOrEmpty(itemName))
                {
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
        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCategory.SelectedIndex == 0)
            {
                pnlDMedicine.BringToFront();

            }
            if (cbCategory.SelectedIndex == 1)
            {
                pnlHMedicine.BringToFront();
            }
            if (cbCategory.SelectedIndex == 2)
            {
                pnlHDMedicine.BringToFront();
            }
        }
        private void txtLname_TextChanged(object sender, EventArgs e)
        {
            // Remove any digits from the entered text
            string filteredText = new string(txtLname.Text.Where(c => !char.IsDigit(c)).ToArray());
            if (!string.IsNullOrEmpty(filteredText))
            {
                TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                txtLname.Text = textInfo.ToTitleCase(filteredText);
            }
            else
            {
                txtLname.Text = string.Empty;
            }
            txtLname.SelectionStart = txtLname.Text.Length;
        }
        private void txtFname_TextChanged(object sender, EventArgs e)
        {
            // Remove any digits from the entered text
            string filteredText = new string(txtFname.Text.Where(c => !char.IsDigit(c)).ToArray());
            if (!string.IsNullOrEmpty(filteredText))
            {
                TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                txtFname.Text = textInfo.ToTitleCase(filteredText);
            }
            else
            {
                txtFname.Text = string.Empty;
            }
            txtFname.SelectionStart = txtFname.Text.Length;
        }
        private void txtMI_TextChanged(object sender, EventArgs e)
        {
            string filteredText = new string(txtMI.Text.Where(c => !char.IsDigit(c)).ToArray());

            if (!string.IsNullOrEmpty(filteredText))
            {
                txtMI.Text = filteredText.ToUpper();
                txtMI.SelectionStart = txtMI.Text.Length;
            }
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
        private void dtpDob_ValueChanged(object sender, EventArgs e)
        {
            DateTime start = Convert.ToDateTime(dtpDob.Value.Date);
            DateTime end = DateTime.Today;
            TimeSpan span = end - start;
            var daysTotal = span.TotalDays;
            var yearsTotal = Math.Truncate(daysTotal / 365);

            txtAge.Text = Convert.ToString(yearsTotal);
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
            if (txtDaily9.Text.Any(char.IsLetter))
            {
                // Clear the TextBox if it contains letters
                txtDaily9.Clear();
            }
            else
            {
                // Extract digits from the input text
                string inputText = new string(txtDaily9.Text.Where(char.IsDigit).ToArray());

                if (int.TryParse(inputText, out int medicationFrequency))
                {
                    // Calculate the Monthly Consumption based on the Medication Frequency
                    int daysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
                    int monthlyConsumption = medicationFrequency * daysInMonth;

                    // Display the result in txtMonthly9
                    txtMonthly9.Text = monthlyConsumption.ToString();
                }
                else
                {
                    // Clear txtMonthly9 if the input is not a valid integer
                    txtMonthly9.Text = "";
                }
            }

        }

        private void txtDaily8_TextChanged(object sender, EventArgs e)
        {
            if (txtDaily8.Text.Any(char.IsLetter))
            {
                // Clear the TextBox if it contains letters
                txtDaily8.Clear();
            }
            else
            {
                // Extract digits from the input text
                string inputText = new string(txtDaily8.Text.Where(char.IsDigit).ToArray());

                if (int.TryParse(inputText, out int medicationFrequency))
                {
                    // Calculate the Monthly Consumption based on the Medication Frequency
                    int daysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
                    int monthlyConsumption = medicationFrequency * daysInMonth;

                    // Display the result in txtMonthly9
                    txtMonthly8.Text = monthlyConsumption.ToString();
                }
                else
                {
                    // Clear txtMonthly9 if the input is not a valid integer
                    txtMonthly8.Text = "";
                }
            }
        }

        private void txtDaily7_TextChanged(object sender, EventArgs e)
        {
            if (txtDaily7.Text.Any(char.IsLetter))
            {
                // Clear the TextBox if it contains letters
                txtDaily7.Clear();
            }
            else
            {
                // Extract digits from the input text
                string inputText = new string(txtDaily7.Text.Where(char.IsDigit).ToArray());

                if (int.TryParse(inputText, out int medicationFrequency))
                {
                    // Calculate the Monthly Consumption based on the Medication Frequency
                    int daysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
                    int monthlyConsumption = medicationFrequency * daysInMonth;

                    // Display the result in txtMonthly9
                    txtMonthly7.Text = monthlyConsumption.ToString();
                }
                else
                {
                    // Clear txtMonthly9 if the input is not a valid integer
                    txtMonthly7.Text = "";
                }
            }
        }

        private void txtDaily6_TextChanged(object sender, EventArgs e)
        {
            if (txtDaily6.Text.Any(char.IsLetter))
            {
                // Clear the TextBox if it contains letters
                txtDaily6.Clear();
            }
            else
            {
                // Extract digits from the input text
                string inputText = new string(txtDaily6.Text.Where(char.IsDigit).ToArray());

                if (int.TryParse(inputText, out int medicationFrequency))
                {
                    // Calculate the Monthly Consumption based on the Medication Frequency
                    int daysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
                    int monthlyConsumption = medicationFrequency * daysInMonth;

                    // Display the result in txtMonthly9
                    txtMonthly6.Text = monthlyConsumption.ToString();
                }
                else
                {
                    // Clear txtMonthly9 if the input is not a valid integer
                    txtMonthly6.Text = "";
                }
            }
        }

        private void txtDaily5_TextChanged(object sender, EventArgs e)
        {
            if (txtDaily5.Text.Any(char.IsLetter))
            {
                // Clear the TextBox if it contains letters
                txtDaily5.Clear();
            }
            else
            {
                // Extract digits from the input text
                string inputText = new string(txtDaily5.Text.Where(char.IsDigit).ToArray());

                if (int.TryParse(inputText, out int medicationFrequency))
                {
                    // Calculate the Monthly Consumption based on the Medication Frequency
                    int daysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
                    int monthlyConsumption = medicationFrequency * daysInMonth;

                    // Display the result in txtMonthly9
                    txtMonthly5.Text = monthlyConsumption.ToString();
                }
                else
                {
                    // Clear txtMonthly9 if the input is not a valid integer
                    txtMonthly5.Text = "";
                }
            }
        }

        private void txtDaily4_TextChanged(object sender, EventArgs e)
        {
            if (txtDaily4.Text.Any(char.IsLetter))
            {
                // Clear the TextBox if it contains letters
                txtDaily4.Clear();
            }
            else
            {
                // Extract digits from the input text
                string inputText = new string(txtDaily4.Text.Where(char.IsDigit).ToArray());

                if (int.TryParse(inputText, out int medicationFrequency))
                {
                    // Calculate the Monthly Consumption based on the Medication Frequency
                    int daysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
                    int monthlyConsumption = medicationFrequency * daysInMonth;

                    // Display the result in txtMonthly9
                    txtMonthly4.Text = monthlyConsumption.ToString();
                }
                else
                {
                    // Clear txtMonthly9 if the input is not a valid integer
                    txtMonthly4.Text = "";
                }
            }
        }

        private void txtDaily3_TextChanged(object sender, EventArgs e)
        {
            if (txtDaily3.Text.Any(char.IsLetter))
            {
                // Clear the TextBox if it contains letters
                txtDaily3.Clear();
            }
            else
            {
                // Extract digits from the input text
                string inputText = new string(txtDaily3.Text.Where(char.IsDigit).ToArray());

                if (int.TryParse(inputText, out int medicationFrequency))
                {
                    // Calculate the Monthly Consumption based on the Medication Frequency
                    int daysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
                    int monthlyConsumption = medicationFrequency * daysInMonth;

                    // Display the result in txtMonthly9
                    txtMonthly3.Text = monthlyConsumption.ToString();
                }
                else
                {
                    // Clear txtMonthly9 if the input is not a valid integer
                    txtMonthly3.Text = "";
                }
            }
        }

        private void txtDaily2_TextChanged(object sender, EventArgs e)
        {
            if (txtDaily2.Text.Any(char.IsLetter))
            {
                // Clear the TextBox if it contains letters
                txtDaily2.Clear();
            }
            else
            {
                // Extract digits from the input text
                string inputText = new string(txtDaily2.Text.Where(char.IsDigit).ToArray());

                if (int.TryParse(inputText, out int medicationFrequency))
                {
                    // Calculate the Monthly Consumption based on the Medication Frequency
                    int daysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
                    int monthlyConsumption = medicationFrequency * daysInMonth;

                    // Display the result in txtMonthly9
                    txtMonthly2.Text = monthlyConsumption.ToString();
                }
                else
                {
                    // Clear txtMonthly9 if the input is not a valid integer
                    txtMonthly2.Text = "";
                }
            }
        }

        private void txtDaily1_TextChanged(object sender, EventArgs e)
        {
            if (txtDaily1.Text.Any(char.IsLetter))
            {
                // Clear the TextBox if it contains letters
                txtDaily1.Clear();
            }
            else
            {
                // Extract digits from the input text
                string inputText = new string(txtDaily1.Text.Where(char.IsDigit).ToArray());

                if (int.TryParse(inputText, out int medicationFrequency))
                {
                    // Calculate the Monthly Consumption based on the Medication Frequency
                    int daysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
                    int monthlyConsumption = medicationFrequency * daysInMonth;

                    // Display the result in txtMonthly9
                    txtMonthly1.Text = monthlyConsumption.ToString();
                }
                else
                {
                    // Clear txtMonthly9 if the input is not a valid integer
                    txtMonthly1.Text = "";
                }
            }
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnInsert_(object sender, EventArgs e)
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

                string insertQuery = "INSERT INTO TablePatient (category,fullname, lname, fname, mi, suffix, dob, age, sex, brgy, phealth, contact,medname,day,month, bp, smoker, exercise, date, history,image) " +
                                        "VALUES (@category, @fullname, @lname, @fname, @mi, @suffix, @dob, @age, @sex, @brgy, @phealth, @contact, @mednameDiabetic, @day, @month, @bp, @smoker, @exercise, @date, @history, @image)";
                using (SqlCommand cmdDiabetic = new SqlCommand(insertQuery, connection))
                {
                    cmdDiabetic.Parameters.AddWithValue("@category", category);
                    cmdDiabetic.Parameters.AddWithValue("@fullname", fullname);
                    cmdDiabetic.Parameters.AddWithValue("@lname", lname);
                    cmdDiabetic.Parameters.AddWithValue("@fname", fname);
                    cmdDiabetic.Parameters.AddWithValue("@mi", mi);
                    cmdDiabetic.Parameters.AddWithValue("@suffix", suffix);
                    cmdDiabetic.Parameters.AddWithValue("@dob", dob);
                    cmdDiabetic.Parameters.AddWithValue("@age", age);
                    cmdDiabetic.Parameters.AddWithValue("@sex", sex);
                    cmdDiabetic.Parameters.AddWithValue("@brgy", brgy);
                    cmdDiabetic.Parameters.AddWithValue("@phealth", phealth);
                    cmdDiabetic.Parameters.AddWithValue("@contact", contact);
                    cmdDiabetic.Parameters.AddWithValue("@mednameDiabetic", mednameDiabetic);
                    cmdDiabetic.Parameters.AddWithValue("@day", day);
                    cmdDiabetic.Parameters.AddWithValue("@month", month);
                    cmdDiabetic.Parameters.AddWithValue("@bp", bp);
                    cmdDiabetic.Parameters.AddWithValue("@smoker", smoker);
                    cmdDiabetic.Parameters.AddWithValue("@exercise", exercise);
                    cmdDiabetic.Parameters.AddWithValue("@date", DateTime.Now);
                    cmdDiabetic.Parameters.AddWithValue("@history", history);
                    cmdDiabetic.Parameters.AddWithValue("@image", imageBytes);


                    int rowsAffectedDiabetic = cmdDiabetic.ExecuteNonQuery();

                    if (rowsAffectedDiabetic > 0)
                    {
                        MessageBox.Show($"Patient information {lname} {fname} {mi} {suffix} added successfully!\n\nPress OK to continue.", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);



                    }
                    else
                    {
                        MessageBox.Show("Failed to add patient information!\n\nPress OK to continue. ", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

            }
        }

        private void btnInsert_Click(object sender, EventArgs e)
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

                string insertQuery = "INSERT INTO TablePatient (category,fullname, lname, fname, mi, suffix, dob, age, sex, brgy, phealth, contact,medname,day,month, bp, smoker, exercise, date, history,image) " +
                                        "VALUES (@category, @fullname, @lname, @fname, @mi, @suffix, @dob, @age, @sex, @brgy, @phealth, @contact, @mednameDiabetic, @day, @month, @bp, @smoker, @exercise, @date, @history, @image)";
                using (SqlCommand cmd = new SqlCommand(insertQuery, connection))
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
                    cmd.Parameters.AddWithValue("@mednameDiabetic", mednameDiabetic);
                    cmd.Parameters.AddWithValue("@day", day);
                    cmd.Parameters.AddWithValue("@month", month);
                    cmd.Parameters.AddWithValue("@bp", bp);
                    cmd.Parameters.AddWithValue("@smoker", smoker);
                    cmd.Parameters.AddWithValue("@exercise", exercise);
                    cmd.Parameters.AddWithValue("@date", DateTime.Now);
                    cmd.Parameters.AddWithValue("@history", history);
                    cmd.Parameters.AddWithValue("@image", imageBytes);


                    int rowsAffectedDiabetic = cmd.ExecuteNonQuery();

                    if (rowsAffectedDiabetic > 0)
                    {
                        MessageBox.Show($"Patient information {lname} {fname} {mi} {suffix} added successfully!\n\nPress OK to continue.", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);



                    }
                    else
                    {
                        MessageBox.Show("Failed to add patient information!\n\nPress OK to continue. ", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

            }
        }

        private void checkedListBoxDiabetic_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
