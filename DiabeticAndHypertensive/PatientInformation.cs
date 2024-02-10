using System;
using System.Windows.Forms;

namespace DiabeticAndHypertensive
{
    public partial class PatientInformation : Form
    {
        public PatientInformation()
        {
            InitializeComponent();
        }

        private void PatientInformation_Load(object sender, EventArgs e)
        {
            OpenPanelContent(new PatientInformationLists());
        }
        public Panel GetPanelContent()
        {
            return pnlContent;
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
    }
}