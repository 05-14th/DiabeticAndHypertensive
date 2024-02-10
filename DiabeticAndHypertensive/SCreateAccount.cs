using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace DiabeticAndHypertensive
{
    public partial class SCreateAccount : Form
    {
        private SqlConnection connection = new SqlConnection("Data Source=LAPTOP-03VSR27V\\SQLEXPRESS1;Initial Catalog=had;Integrated Security=True"); private List<string> securityQuestions = new List<string>
        {
            "What is your mother's maiden name?",
            "In what city were you born?",
            "What is the name of your favorite pet?",
            "What is your favorite movie?",
            "What is the last name of your favorite teacher?"
        };
        public SCreateAccount()
        {
            InitializeComponent();
            cbSecurityQuestion.DataSource = securityQuestions;
        }

        private void SCreateAccount_Load(object sender, EventArgs e)
        {

        }

        private void btnAddAccount_Click(object sender, EventArgs e)
        {
            string employeeId = txtID.Text;
            string name = txtName.Text;
            string username = txtUname.Text;
            string password = txtPass.Text;
            string usertype = cbRole.Text;
            string securityQuestion = cbSecurityQuestion.Text;
            string securityAnswer = txtSecurityAnswer.Text;

            // Check if the user uploaded an image
            byte[] imageBytes = (pictureBoxUser.Image != null) ? ConvertImageToByteArray(pictureBoxUser.Image) : GetDefaultImageBytes();

            if (string.IsNullOrWhiteSpace(employeeId) || string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(usertype) || string.IsNullOrWhiteSpace(securityQuestion) || string.IsNullOrWhiteSpace(securityAnswer))
            {
                MessageBox.Show("Please fill in all fields.", "Incomplete Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Create an Account object
            Account newAccount = new Account
            {
                EmployeeId = employeeId,
                Name = name,
                Username = username,
                Password = password,
                UserType = usertype,
                SecurityQuestion = securityQuestion,
                SecurityAnswer = securityAnswer,
                Image = imageBytes
            };

            // Call a method to add the account to the database
            AddAccountToDatabase(newAccount);
        }

        private void AddAccountToDatabase(Account account)
        {
            try
            {
                connection.Open();

                string insertQuery = "INSERT INTO Account (employeeId, name, username, password, usertype, SecurityQuestion, SecurityAnswer, image) " +
                    "VALUES (@EmployeeId, @Name, @Username, @Password, @Usertype, @SecurityQuestion, @SecurityAnswer, @Image)";

                

                using (SqlCommand command = new SqlCommand(insertQuery, connection))
                {
                    // Adding parameters to the query
                    command.Parameters.AddWithValue("@EmployeeId", account.EmployeeId);
                    command.Parameters.AddWithValue("@Name", account.Name);
                    command.Parameters.AddWithValue("@Username", account.Username);
                    command.Parameters.AddWithValue("@Password", account.Password);
                    command.Parameters.AddWithValue("@Usertype", account.UserType);
                    command.Parameters.AddWithValue("@SecurityQuestion", account.SecurityQuestion);
                    command.Parameters.AddWithValue("@SecurityAnswer", account.SecurityAnswer);
                    command.Parameters.AddWithValue("@Image", account.Image);
                    // Executing the query
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Account added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Failed to add account.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connection.Close();
            }
        }

        private byte[] GetDefaultImageBytes()
        {
            // Provide a default image in case the user did not upload one
            Image defaultImage = Properties.Resources.DefaultImage; // Assuming you have a default image in your resources
            return ConvertImageToByteArray(defaultImage);
        }
        private byte[] ConvertImageToByteArray(Image image)
        {
            if (image == null)
            {
                // Return default image bytes or handle accordingly
                return GetDefaultImageBytes();
            }

            using (MemoryStream memoryStream = new MemoryStream())
            {
                image.Save(memoryStream, ImageFormat.Png); // Change the format as needed
                return memoryStream.ToArray();
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
                pictureBoxUser.Image = Image.FromFile(imagePath);

                // Optionally, store the image data in a variable for later use
                byte[] imageData = ConvertImageToByteArray(pictureBoxUser.Image);
            }
        }
    }
}
