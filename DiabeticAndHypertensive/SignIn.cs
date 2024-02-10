using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Security.Principal;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace DiabeticAndHypertensive
{
  
    public partial class SignIn : Form
    {
        private string connectionString = "Data Source=LAPTOP-03VSR27V\\SQLEXPRESS1;Initial Catalog=had;Integrated Security=True";
        private SqlConnection connection;
        SqlDataAdapter adapter;
        SqlCommand command;
        private AutoCompleteStringCollection SecurityQuestion;
        private string employeeId;
        private string loggedInEmployeeId = "";
        private string enteredEmployeeId;
        private string logUsername;
        public SignIn(string username)
        {
            InitializeComponent();
            connection = new SqlConnection(connectionString);
            logUsername = username;
        }

        private void btnSignIn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Please fill all fields.", "Incomplete Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            connection.Open();
            command = new SqlCommand("SELECT * FROM Account WHERE username= @username AND password= @password", connection);
            command.Parameters.AddWithValue("@username", txtUsername.Text);
            command.Parameters.AddWithValue("@password", txtPassword.Text);

            adapter = new SqlDataAdapter(command);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);

            int rowCount = dataSet.Tables[0].Rows.Count;

            if (rowCount == 1)
            {
                string userType = dataSet.Tables[0].Rows[0]["usertype"].ToString();

                if (userType.Equals("Admin", StringComparison.OrdinalIgnoreCase))
                {
                    Home adminHome = new Home();
                    adminHome.ShowDialog();
                    History();
                    logUsername = txtUsername.Text;
                    this.Close();
                }
                else if (userType.Equals("User", StringComparison.OrdinalIgnoreCase))
                {
                    UHome userHome = new UHome();
                    userHome.ShowDialog();
                    this.Close();

                }
                else
                {
                    MessageBox.Show("Invalid user type", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtUsername.Clear();
                    txtPassword.Clear();
                }
            }
            else
            {
                MessageBox.Show("Invalid credentials", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUsername.Clear();
                txtPassword.Clear();
            }

            connection.Close();
        }
        private void linkLabelForgotPassword_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pnlForgotPassword.BringToFront();
            pnlSignIn.SendToBack();
        }
        private void btnSubmitAnswer_Click(object sender, EventArgs e)
        {
            string enteredEmployeeId = txtemployeeID.Text;
            string SecurityQuestion = cbSecurityQuestion.Text;
            string SecurityAnswer = txtSecurityAnswer.Text;

            if (string.IsNullOrWhiteSpace(enteredEmployeeId) || string.IsNullOrWhiteSpace(SecurityAnswer))
            {
                MessageBox.Show("Please enter valid employee ID and answer.", "Incomplete Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string selectQuery = "SELECT * FROM Account WHERE employeeId = @employeeId AND SecurityQuestion=@SecurityQuestion AND SecurityAnswer=@SecurityAnswer";

                using (SqlCommand command = new SqlCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("@employeeId", enteredEmployeeId); // Use enteredEmployeeId here
                    command.Parameters.AddWithValue("@SecurityQuestion", SecurityQuestion);
                    command.Parameters.AddWithValue("@SecurityAnswer", SecurityAnswer);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        if (dataTable.Rows.Count > 0)
                        {
                            pnlCreateNewPassword.BringToFront();
                            pnlForgotPassword.SendToBack();
                        }
                        else
                        {
                            MessageBox.Show("Invalid answer.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }
        private void btnCancelForgotPassword_Click(object sender, EventArgs e)
        {
            pnlSignIn.BringToFront();
            pnlCreateNewPassword.SendToBack();
        }



        private void btnUpdatePass_Click(object sender, EventArgs e)
        {
            string enteredEmployeeId = txtReEnterEmployeeId.Text;
            string newPassword = txtNewPass.Text;
            string confirmNewPassword = txtConfirmPass.Text;
            if ( string.IsNullOrWhiteSpace(newPassword) || string.IsNullOrWhiteSpace(confirmNewPassword))
            {
                MessageBox.Show("Please enter the new password and confirm it.", "Incomplete Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (newPassword != confirmNewPassword)
            {
                MessageBox.Show("Passwords do not match.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(enteredEmployeeId))
            {
                MessageBox.Show("Employee ID not provided.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                

                // Update the password in the database
                string updateQuery = "UPDATE Account SET password = @newPassword WHERE employeeId = @employeeId";

                using (SqlCommand command = new SqlCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@newPassword", newPassword);
                    command.Parameters.AddWithValue("@employeeId", enteredEmployeeId);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Password updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        pnlCreateNewPassword.SendToBack();
                        pnlSignIn.BringToFront();
                    }
                    else
                    {
                        MessageBox.Show("Failed to update password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        //panel login

        private void History()
        {
            string logUsername = txtUsername.Text;
            DateTime date = DateTime.Now;
            string activity = $"Accessed the system with valid credentials.";
            
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

        private void txtUsername_Click(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            phUsername.Visible = false;
            if (textBox.Text == phUsername.Text)
            {
                textBox.Text = string.Empty;
                textBox.ForeColor = SystemColors.WindowText; // Restore the original text color
            }
        }
        private void txtUsername_Enter(object sender, EventArgs e)
        {
            if (txtUsername.Text == "Username")
            {
                txtUsername.Text = "";
                txtUsername.ForeColor = Color.Black;
            }
        }
        private void txtUsername_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                txtUsername.Text = "Username";
                txtUsername.ForeColor = Color.Gray;
            }
        }
        private void txtPassword_Click(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            phpassword.Visible = false;
            if (textBox.Text == phpassword.Text)
            {
                textBox.Text = string.Empty;
                textBox.ForeColor = SystemColors.WindowText;
            }
        }
        private void txtPassword_Enter(object sender, EventArgs e)
        {
            if (txtPassword.Text == "Password")
            {
                txtPassword.Text = "";
                txtPassword.UseSystemPasswordChar = true; // To hide the password characters
                txtPassword.ForeColor = Color.Black;
            }
        }
        private void txtPassword_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                txtPassword.Text = "Password";
                txtPassword.UseSystemPasswordChar = false; // To show placeholder text
                txtPassword.ForeColor = Color.Gray;
            }
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = false;
            btnShow.Visible = false;
            btnHide.Visible = true;
        }
        private void btnHide_Click(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = true;
            btnHide.Visible = false;
            btnShow.Visible = true;
        }

        private void SignIn_Load(object sender, EventArgs e)
        {

        }

        private void txtemployeeID_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
