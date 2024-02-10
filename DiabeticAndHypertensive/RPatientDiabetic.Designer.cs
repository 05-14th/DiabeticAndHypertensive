namespace DiabeticAndHypertensive
{
    partial class RPatientDiabetic
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnfilterbrgy = new System.Windows.Forms.Button();
            this.cbBarangay = new System.Windows.Forms.ComboBox();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnfilterbrgy);
            this.panel1.Controls.Add(this.cbBarangay);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.panel1.Size = new System.Drawing.Size(800, 36);
            this.panel1.TabIndex = 8;
            // 
            // btnfilterbrgy
            // 
            this.btnfilterbrgy.AutoSize = true;
            this.btnfilterbrgy.FlatAppearance.BorderSize = 0;
            this.btnfilterbrgy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnfilterbrgy.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnfilterbrgy.Location = new System.Drawing.Point(203, 2);
            this.btnfilterbrgy.Name = "btnfilterbrgy";
            this.btnfilterbrgy.Size = new System.Drawing.Size(79, 33);
            this.btnfilterbrgy.TabIndex = 8;
            this.btnfilterbrgy.Text = "Load";
            this.btnfilterbrgy.UseVisualStyleBackColor = true;
            this.btnfilterbrgy.Click += new System.EventHandler(this.btnfilterbrgy_Click);
            // 
            // cbBarangay
            // 
            this.cbBarangay.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbBarangay.FormattingEnabled = true;
            this.cbBarangay.Items.AddRange(new object[] {
            "Select Barangay",
            "Araw at Bituin",
            "Bagong Sikat",
            "Banaag ng Pag-asa",
            "Binacas",
            "Cabra",
            "Likas ng Silangan",
            "Maguinhawa",
            "Maligaya",
            "Maliig",
            "Ninikat ng Pag-asa",
            "Paraiso",
            "Sorville",
            "Tagbac",
            "Tangal",
            "Tilik",
            "Tumibo",
            "Vigo"});
            this.cbBarangay.Location = new System.Drawing.Point(0, 3);
            this.cbBarangay.Name = "cbBarangay";
            this.cbBarangay.Size = new System.Drawing.Size(193, 31);
            this.cbBarangay.TabIndex = 7;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportViewer1.Location = new System.Drawing.Point(0, 36);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(800, 414);
            this.reportViewer1.TabIndex = 9;
            // 
            // RPatientDiabetic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.reportViewer1);
            this.Controls.Add(this.panel1);
            this.Name = "RPatientDiabetic";
            this.Text = "RPatientDiabetic";
            this.Load += new System.EventHandler(this.RPatientDiabetic_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnfilterbrgy;
        private System.Windows.Forms.ComboBox cbBarangay;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
    }
}