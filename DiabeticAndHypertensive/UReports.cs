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
    public partial class UReports : Form
    {
        public UReports()
        {
            InitializeComponent();
        }

        private void UReports_Load(object sender, EventArgs e)
        {
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

        private void cbChooseReports_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedReport = cbChooseReports.SelectedItem.ToString();

            if (selectedReport == "Diabetic Medicine Inventory List")
            {

            }
            if (selectedReport == "Hypertensive Medicine Inventory List")
            {

            }
            if (selectedReport == "Diabetic Medicine Inventory List")
            {

            }
            if (selectedReport == "Hypertensive Medicine Inventory List")
            {
                OpenPanelContent(new RInventoryHypertensive());
            }
        }
    }
}
