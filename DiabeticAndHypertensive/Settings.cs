using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiabeticAndHypertensive
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void Settings_Load(object sender, EventArgs e)
        {

        }
        private void movepanel(Control panel)
        {
            pnlSlide.Width = panel.Width;
            pnlSlide.Left = panel.Right - pnlSlide.Width;
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
        private void btnAccount_Click(object sender, EventArgs e)
        {
            movepanel(btnAccount);
            OpenPanelContent(new SCreateAccount());
        }

        private void btnLogHistory_Click(object sender, EventArgs e)
        {
            movepanel(btnLogHistory);
            OpenPanelContent(new LogHistory());
        }
    }
}
