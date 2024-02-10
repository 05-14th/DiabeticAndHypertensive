namespace DiabeticAndHypertensive
{
    partial class Settings
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.pnlSlide = new System.Windows.Forms.Panel();
            this.btnRecycleBin = new System.Windows.Forms.Button();
            this.btnLogHistory = new System.Windows.Forms.Button();
            this.btnAccount = new System.Windows.Forms.Button();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.roundedPanel1 = new RoundedPanel();
            this.label11 = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.roundedPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Controls.Add(this.pnlSlide);
            this.panel2.Controls.Add(this.btnRecycleBin);
            this.panel2.Controls.Add(this.btnLogHistory);
            this.panel2.Controls.Add(this.btnAccount);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel2.ForeColor = System.Drawing.Color.Black;
            this.panel2.Location = new System.Drawing.Point(0, 39);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(5, 3, 0, 3);
            this.panel2.Size = new System.Drawing.Size(800, 45);
            this.panel2.TabIndex = 5;
            // 
            // pnlSlide
            // 
            this.pnlSlide.BackColor = System.Drawing.Color.Aquamarine;
            this.pnlSlide.Location = new System.Drawing.Point(5, 40);
            this.pnlSlide.Name = "pnlSlide";
            this.pnlSlide.Size = new System.Drawing.Size(204, 5);
            this.pnlSlide.TabIndex = 0;
            // 
            // btnRecycleBin
            // 
            this.btnRecycleBin.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnRecycleBin.FlatAppearance.BorderSize = 0;
            this.btnRecycleBin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRecycleBin.Location = new System.Drawing.Point(413, 3);
            this.btnRecycleBin.Name = "btnRecycleBin";
            this.btnRecycleBin.Size = new System.Drawing.Size(204, 39);
            this.btnRecycleBin.TabIndex = 3;
            this.btnRecycleBin.Text = "Recycle Bin";
            this.btnRecycleBin.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnRecycleBin.UseVisualStyleBackColor = true;
            // 
            // btnLogHistory
            // 
            this.btnLogHistory.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnLogHistory.FlatAppearance.BorderSize = 0;
            this.btnLogHistory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogHistory.Location = new System.Drawing.Point(209, 3);
            this.btnLogHistory.Name = "btnLogHistory";
            this.btnLogHistory.Size = new System.Drawing.Size(204, 39);
            this.btnLogHistory.TabIndex = 1;
            this.btnLogHistory.Text = "Log History";
            this.btnLogHistory.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnLogHistory.UseVisualStyleBackColor = true;
            this.btnLogHistory.Click += new System.EventHandler(this.btnLogHistory_Click);
            // 
            // btnAccount
            // 
            this.btnAccount.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnAccount.FlatAppearance.BorderSize = 0;
            this.btnAccount.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAccount.Location = new System.Drawing.Point(5, 3);
            this.btnAccount.Name = "btnAccount";
            this.btnAccount.Size = new System.Drawing.Size(204, 39);
            this.btnAccount.TabIndex = 0;
            this.btnAccount.Text = "Create Account";
            this.btnAccount.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnAccount.UseVisualStyleBackColor = true;
            this.btnAccount.Click += new System.EventHandler(this.btnAccount_Click);
            // 
            // pnlContent
            // 
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlContent.Location = new System.Drawing.Point(0, 84);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(800, 366);
            this.pnlContent.TabIndex = 6;
            // 
            // roundedPanel1
            // 
            this.roundedPanel1.BackColor = System.Drawing.Color.ForestGreen;
            this.roundedPanel1.Controls.Add(this.label11);
            this.roundedPanel1.CornerRadius = 5;
            this.roundedPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.roundedPanel1.Location = new System.Drawing.Point(0, 0);
            this.roundedPanel1.Name = "roundedPanel1";
            this.roundedPanel1.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.roundedPanel1.Size = new System.Drawing.Size(800, 39);
            this.roundedPanel1.TabIndex = 29;
            // 
            // label11
            // 
            this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label11.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(5, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(795, 39);
            this.label11.TabIndex = 4;
            this.label11.Text = "Settings";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.roundedPanel1);
            this.Name = "Settings";
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.Settings_Load);
            this.panel2.ResumeLayout(false);
            this.roundedPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel pnlSlide;
        private System.Windows.Forms.Button btnRecycleBin;
        private System.Windows.Forms.Button btnLogHistory;
        private System.Windows.Forms.Button btnAccount;
        private System.Windows.Forms.Panel pnlContent;
        private RoundedPanel roundedPanel1;
        private System.Windows.Forms.Label label11;
    }
}