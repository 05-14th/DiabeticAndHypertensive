namespace DiabeticAndHypertensive
{
    partial class RPatientHypertensive
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
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.btnfiltermonthly = new System.Windows.Forms.Button();
            this.cbMonthly = new System.Windows.Forms.ComboBox();
            this.btnfilterbrgy = new System.Windows.Forms.Button();
            this.cbBarangay = new System.Windows.Forms.ComboBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportViewer1.Location = new System.Drawing.Point(0, 36);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(709, 414);
            this.reportViewer1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.comboBox2);
            this.panel1.Controls.Add(this.btnfiltermonthly);
            this.panel1.Controls.Add(this.cbMonthly);
            this.panel1.Controls.Add(this.btnfilterbrgy);
            this.panel1.Controls.Add(this.cbBarangay);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.panel1.Size = new System.Drawing.Size(709, 36);
            this.panel1.TabIndex = 6;
            // 
            // button2
            // 
            this.button2.AutoSize = true;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(887, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(79, 33);
            this.button2.TabIndex = 12;
            this.button2.Text = "Load";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            // 
            // comboBox2
            // 
            this.comboBox2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
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
            this.comboBox2.Location = new System.Drawing.Point(684, 3);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(193, 31);
            this.comboBox2.TabIndex = 11;
            this.comboBox2.Visible = false;
            // 
            // btnfiltermonthly
            // 
            this.btnfiltermonthly.AutoSize = true;
            this.btnfiltermonthly.FlatAppearance.BorderSize = 0;
            this.btnfiltermonthly.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnfiltermonthly.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnfiltermonthly.Location = new System.Drawing.Point(547, 2);
            this.btnfiltermonthly.Name = "btnfiltermonthly";
            this.btnfiltermonthly.Size = new System.Drawing.Size(79, 33);
            this.btnfiltermonthly.TabIndex = 10;
            this.btnfiltermonthly.Text = "Load";
            this.btnfiltermonthly.UseVisualStyleBackColor = true;
            this.btnfiltermonthly.Visible = false;
            this.btnfiltermonthly.Click += new System.EventHandler(this.btnfiltermonthly_Click);
            // 
            // cbMonthly
            // 
            this.cbMonthly.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbMonthly.FormattingEnabled = true;
            this.cbMonthly.Items.AddRange(new object[] {
            "Select Month",
            "January",
            "February",
            "March",
            "April",
            "May",
            "June",
            "July",
            "August",
            "September",
            "October",
            "November",
            "December"});
            this.cbMonthly.Location = new System.Drawing.Point(344, 3);
            this.cbMonthly.Name = "cbMonthly";
            this.cbMonthly.Size = new System.Drawing.Size(193, 31);
            this.cbMonthly.TabIndex = 9;
            this.cbMonthly.Visible = false;
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
            // RPatientHypertensive
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(709, 450);
            this.Controls.Add(this.reportViewer1);
            this.Controls.Add(this.panel1);
            this.Name = "RPatientHypertensive";
            this.Text = "RPatientHypertensive";
            this.Load += new System.EventHandler(this.RPatientHypertensive_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnfilterbrgy;
        private System.Windows.Forms.ComboBox cbBarangay;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Button btnfiltermonthly;
        private System.Windows.Forms.ComboBox cbMonthly;
    }
}