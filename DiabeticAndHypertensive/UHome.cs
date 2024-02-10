using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace DiabeticAndHypertensive
{
    public partial class UHome : Form
    {
        private string connectionString = "Data Source=LAPTOP-03VSR27V\\SQLEXPRESS1;Initial Catalog=had;Integrated Security=True";
        ToolTip toolTip;
        public UHome()
        {
            InitializeComponent();
            toolTip = new ToolTip();
            toolTip.SetToolTip(btnH, "Home");
            toolTip.SetToolTip(btnPI, "Patient Information");
            toolTip.SetToolTip(btnMI, "Medicine Information");
            toolTip.SetToolTip(btnMD, "Medicine Distribution");
            toolTip.SetToolTip(btnR, "Reports");
            toolTip.SetToolTip(btnS, "Settings");
            // Subscribe to events
            btnH.MouseHover += btnH_MouseHover;
            btnH.MouseLeave += btnH_MouseLeave;
            btnPI.MouseHover += btnPI_MouseHover;
            btnPI.MouseLeave += btnPI_MouseLeave;
            btnMI.MouseHover += btnMI_MouseHover;
            btnMI.MouseLeave += btnMI_MouseLeave;
            btnMD.MouseHover += btnMD_MouseHover;
            btnMD.MouseLeave += btnMD_MouseLeave;
            btnR.MouseHover += btnR_MouseHover;
            btnR.MouseLeave += btnR_MouseLeave;
            btnS.MouseHover += btnS_MouseHover;
            btnS.MouseLeave += btnS_MouseLeave;
            panel3.Resize += ExistingPanel_Resize;
            SetSoftEdge(panel3.ClientRectangle, 7);
        }
        private void ExistingPanel_Resize(object sender, EventArgs e)
        {
            // Update the soft edge effect when the panel is resized
            SetSoftEdge(panel3.ClientRectangle, 20);
        }

        private void SetSoftEdge(RectangleF rect, float radius)
        {
            // Create a GraphicsPath to define a rounded rectangle based on the current size
            GraphicsPath roundedRect = new GraphicsPath();
            roundedRect.AddArc(rect.X, rect.Y, radius * 2, radius * 2, 180, 90);
            roundedRect.AddArc(rect.Width - radius * 2, rect.Y, radius * 2, radius * 2, 270, 90);
            roundedRect.AddArc(rect.Width - radius * 2, rect.Height - radius * 2, radius * 2, radius * 2, 0, 90);
            roundedRect.AddArc(rect.X, rect.Height - radius * 2, radius * 2, radius * 2, 90, 90);
            roundedRect.CloseFigure();

            // Create a region based on the rounded rectangle
            panel3.Region = new Region(roundedRect);
        }

        private void UHome_Load(object sender, EventArgs e)
        {
            OpenPanelContent(new Dashboard());
            Welcome();
            userImage();
        }
        private void userImage()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    string queryImages = "SELECT image FROM Account";

                    using (SqlCommand cmd = new SqlCommand(queryImages, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                // Assuming you have a PictureBox named pictureBoxUser in your form
                                if (reader.Read())
                                {
                                    byte[] imageData = (byte[])reader["image"];
                                    DisplayImage(imageData);
                                }
                            }
                            else
                            {
                                // Handle the case where no images are found
                                MessageBox.Show("No images found for users.");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions here
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        private void DisplayImage(byte[] imageData)
        {
            try
            {
                using (MemoryStream ms = new MemoryStream(imageData))
                {
                    pbUser.Image = Image.FromStream(ms);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error displaying image: " + ex.Message);
            }
        }
        private void Welcome()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    string queryName = "SELECT name FROM Account";

                    using (SqlCommand cmd = new SqlCommand(queryName, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                if (reader.Read())
                                {
                                    string name = reader["name"].ToString();

                                    lblName.Text = name;

                                }
                            }
                            else
                            {

                                MessageBox.Show("No names found in TableAccount.");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions here
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        private void movepanel(Control panel)
        {
            panelSlide.Width = panel.Width;
            panelSlide.Left = panel.Right - panelSlide.Width;
        }
        public Form activeForm = null;
        public void OpenPanelContent(Form panelContent)
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

        private void btnH_Click(object sender, EventArgs e)
        {
            movepanel(btnPI);
            OpenPanelContent(new PatientInformation());
        }

        private void btnPI_Click(object sender, EventArgs e)
        {
            movepanel(btnPI);
            OpenPanelContent(new PatientInformation());
        }

        private void btnMI_Click(object sender, EventArgs e)
        {
            movepanel(btnMI);
            OpenPanelContent(new MedicineInformation());
        }

        private void btnMD_Click(object sender, EventArgs e)
        {
            movepanel(btnMD);
            OpenPanelContent(new MedicineDistribution());
        }

        private void btnR_Click(object sender, EventArgs e)
        {
            movepanel(btnR);
            OpenPanelContent(new Reports());
        }

        private void btnS_Click(object sender, EventArgs e)
        {
            movepanel(btnS);
            OpenPanelContent(new USettings());
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Exit Application", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnMax_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            btnMax.Visible = false;
            btnMaxed.Location = btnMax.Location;
            btnMaxed.Visible = true;
        }

        private void btnMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnMaxed_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            btnMaxed.Visible = false;
            btnMax.Visible = true;
        }

        private void btnH_MouseHover(object sender, EventArgs e)
        {
            toolTip.Show("Home", btnH, btnH.Width, btnH.Height);
        }

        private void btnH_MouseLeave(object sender, EventArgs e)
        {
            toolTip.Hide(btnH);
        }

        private void btnPI_MouseHover(object sender, EventArgs e)
        {
            toolTip.Show("Patient Information", btnPI, btnPI.Width, btnPI.Height);
        }

        private void btnPI_MouseLeave(object sender, EventArgs e)
        {
            toolTip.Hide(btnPI);
        }

        private void btnMI_MouseHover(object sender, EventArgs e)
        {
            toolTip.Show("Medicine Information", btnMI, btnMI.Width, btnMI.Height);
        }

        private void btnMI_MouseLeave(object sender, EventArgs e)
        {
            toolTip.Hide(btnMI);
        }

        private void btnMD_MouseHover(object sender, EventArgs e)
        {
            toolTip.Show("Medicine Distribution", btnMD, btnMD.Width, btnMD.Height);
        }

        private void btnMD_MouseLeave(object sender, EventArgs e)
        {
            toolTip.Hide(btnMD);
        }

        private void btnR_MouseHover(object sender, EventArgs e)
        {
            toolTip.Show("Reports", btnR, btnR.Width, btnR.Height);
        }

        private void btnR_MouseLeave(object sender, EventArgs e)
        {
            toolTip.Hide(btnR);
        }

        private void btnS_MouseHover(object sender, EventArgs e)
        {
            toolTip.Show("Settings", btnS, btnS.Width, btnS.Height);
        }

        private void btnS_MouseLeave(object sender, EventArgs e)
        {
            toolTip.Hide(btnS);
        }
    }
}
