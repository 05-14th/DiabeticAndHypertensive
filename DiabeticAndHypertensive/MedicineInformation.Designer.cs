namespace DiabeticAndHypertensive
{
    partial class MedicineInformation
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.roundedPanel1 = new RoundedPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pnlSlide = new System.Windows.Forms.Panel();
            this.btnExpiredMedicine = new System.Windows.Forms.Button();
            this.btnStockIn = new System.Windows.Forms.Button();
            this.btnInventory = new System.Windows.Forms.Button();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.btnMedicineInformation = new System.Windows.Forms.Button();
            this.roundedPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // roundedPanel1
            // 
            this.roundedPanel1.BackColor = System.Drawing.Color.ForestGreen;
            this.roundedPanel1.Controls.Add(this.label1);
            this.roundedPanel1.CornerRadius = 5;
            this.roundedPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.roundedPanel1.ForeColor = System.Drawing.Color.Black;
            this.roundedPanel1.Location = new System.Drawing.Point(0, 0);
            this.roundedPanel1.Name = "roundedPanel1";
            this.roundedPanel1.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.roundedPanel1.Size = new System.Drawing.Size(900, 34);
            this.roundedPanel1.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(896, 34);
            this.label1.TabIndex = 0;
            this.label1.Text = "Medicine Information";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Controls.Add(this.pnlSlide);
            this.panel2.Controls.Add(this.btnExpiredMedicine);
            this.panel2.Controls.Add(this.btnMedicineInformation);
            this.panel2.Controls.Add(this.btnStockIn);
            this.panel2.Controls.Add(this.btnInventory);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel2.ForeColor = System.Drawing.Color.Black;
            this.panel2.Location = new System.Drawing.Point(0, 34);
            this.panel2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.panel2.Size = new System.Drawing.Size(900, 32);
            this.panel2.TabIndex = 13;
            // 
            // pnlSlide
            // 
            this.pnlSlide.BackColor = System.Drawing.Color.Aquamarine;
            this.pnlSlide.Location = new System.Drawing.Point(4, 26);
            this.pnlSlide.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnlSlide.Name = "pnlSlide";
            this.pnlSlide.Size = new System.Drawing.Size(153, 4);
            this.pnlSlide.TabIndex = 0;
            // 
            // btnExpiredMedicine
            // 
            this.btnExpiredMedicine.BackColor = System.Drawing.Color.LimeGreen;
            this.btnExpiredMedicine.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnExpiredMedicine.FlatAppearance.BorderSize = 0;
            this.btnExpiredMedicine.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExpiredMedicine.Location = new System.Drawing.Point(463, 2);
            this.btnExpiredMedicine.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnExpiredMedicine.Name = "btnExpiredMedicine";
            this.btnExpiredMedicine.Size = new System.Drawing.Size(153, 28);
            this.btnExpiredMedicine.TabIndex = 4;
            this.btnExpiredMedicine.Text = "Expired Medicine";
            this.btnExpiredMedicine.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnExpiredMedicine.UseVisualStyleBackColor = false;
            this.btnExpiredMedicine.Click += new System.EventHandler(this.btnExpiredMedicine_Click);
            // 
            // btnStockIn
            // 
            this.btnStockIn.BackColor = System.Drawing.Color.LimeGreen;
            this.btnStockIn.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnStockIn.FlatAppearance.BorderSize = 0;
            this.btnStockIn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStockIn.Location = new System.Drawing.Point(157, 2);
            this.btnStockIn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnStockIn.Name = "btnStockIn";
            this.btnStockIn.Size = new System.Drawing.Size(153, 28);
            this.btnStockIn.TabIndex = 1;
            this.btnStockIn.Text = "Medicine Stock-In";
            this.btnStockIn.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnStockIn.UseVisualStyleBackColor = false;
            this.btnStockIn.Click += new System.EventHandler(this.btnStockIn_Click);
            // 
            // btnInventory
            // 
            this.btnInventory.BackColor = System.Drawing.Color.LimeGreen;
            this.btnInventory.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnInventory.FlatAppearance.BorderSize = 0;
            this.btnInventory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInventory.Location = new System.Drawing.Point(4, 2);
            this.btnInventory.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnInventory.Name = "btnInventory";
            this.btnInventory.Size = new System.Drawing.Size(153, 28);
            this.btnInventory.TabIndex = 0;
            this.btnInventory.Text = "Medicine Inventory";
            this.btnInventory.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnInventory.UseVisualStyleBackColor = false;
            this.btnInventory.Click += new System.EventHandler(this.btnInventory_Click);
            // 
            // pnlContent
            // 
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(0, 66);
            this.pnlContent.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(900, 574);
            this.pnlContent.TabIndex = 14;
            // 
            // btnMedicineInformation
            // 
            this.btnMedicineInformation.BackColor = System.Drawing.Color.LimeGreen;
            this.btnMedicineInformation.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnMedicineInformation.FlatAppearance.BorderSize = 0;
            this.btnMedicineInformation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMedicineInformation.Location = new System.Drawing.Point(310, 2);
            this.btnMedicineInformation.Margin = new System.Windows.Forms.Padding(2);
            this.btnMedicineInformation.Name = "btnMedicineInformation";
            this.btnMedicineInformation.Size = new System.Drawing.Size(153, 28);
            this.btnMedicineInformation.TabIndex = 3;
            this.btnMedicineInformation.Text = "Medicine Information";
            this.btnMedicineInformation.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnMedicineInformation.UseVisualStyleBackColor = false;
            // 
            // MedicineInformation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(900, 640);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.roundedPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "MedicineInformation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MedicineInformation";
            this.Load += new System.EventHandler(this.MedicineInformation_Load);
            this.roundedPanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private RoundedPanel roundedPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel pnlSlide;
        private System.Windows.Forms.Button btnExpiredMedicine;
        private System.Windows.Forms.Button btnStockIn;
        private System.Windows.Forms.Button btnInventory;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.Button btnMedicineInformation;
    }
}