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
    public partial class MedicineInformation : Form
    {
        public MedicineInformation()
        {
            InitializeComponent();
        }

        private void MedicineInformation_Load(object sender, EventArgs e)
        {
            OpenPanelContent(new MIInventory());
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

        private void btnInventory_Click(object sender, EventArgs e)
        {
            movepanel(btnInventory);
            OpenPanelContent(new MIInventory());
        }

        private void btnStockIn_Click(object sender, EventArgs e)
        {
            movepanel(btnStockIn);
            OpenPanelContent(new MIStockIn());
        }

        private void btnExpiredMedicine_Click(object sender, EventArgs e)
        {
            movepanel(btnExpiredMedicine);
            OpenPanelContent(new MIExpiredMedicine());
        }
    }
}
