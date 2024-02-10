namespace DiabeticAndHypertensive
{
    partial class MIExpiredMedicine
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
            this.LoadDateAdded = new System.Windows.Forms.Button();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.phSearch = new System.Windows.Forms.Label();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgvExMed = new System.Windows.Forms.DataGridView();
            this.roundedPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExMed)).BeginInit();
            this.SuspendLayout();
            // 
            // roundedPanel1
            // 
            this.roundedPanel1.BackColor = System.Drawing.Color.White;
            this.roundedPanel1.Controls.Add(this.label1);
            this.roundedPanel1.CornerRadius = 5;
            this.roundedPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.roundedPanel1.Location = new System.Drawing.Point(0, 0);
            this.roundedPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.roundedPanel1.Name = "roundedPanel1";
            this.roundedPanel1.Size = new System.Drawing.Size(1303, 36);
            this.roundedPanel1.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1303, 36);
            this.label1.TabIndex = 0;
            this.label1.Text = "Expired Medicine";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.LoadDateAdded);
            this.panel2.Controls.Add(this.dtpToDate);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Controls.Add(this.phSearch);
            this.panel2.Controls.Add(this.dtpFromDate);
            this.panel2.Controls.Add(this.txtSearch);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 36);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1303, 35);
            this.panel2.TabIndex = 16;
            // 
            // LoadDateAdded
            // 
            this.LoadDateAdded.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.LoadDateAdded.AutoSize = true;
            this.LoadDateAdded.BackColor = System.Drawing.Color.Silver;
            this.LoadDateAdded.FlatAppearance.BorderSize = 0;
            this.LoadDateAdded.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Green;
            this.LoadDateAdded.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LoadDateAdded.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.LoadDateAdded.Location = new System.Drawing.Point(1192, 1);
            this.LoadDateAdded.Name = "LoadDateAdded";
            this.LoadDateAdded.Size = new System.Drawing.Size(79, 33);
            this.LoadDateAdded.TabIndex = 1;
            this.LoadDateAdded.Text = "Load";
            this.LoadDateAdded.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.LoadDateAdded.UseVisualStyleBackColor = false;
            // 
            // dtpToDate
            // 
            this.dtpToDate.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dtpToDate.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpToDate.Location = new System.Drawing.Point(863, 2);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(311, 30);
            this.dtpToDate.TabIndex = 14;
            this.dtpToDate.Value = new System.DateTime(2024, 1, 1, 0, 0, 0, 0);
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label4.Location = new System.Drawing.Point(821, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 23);
            this.label4.TabIndex = 15;
            this.label4.Text = "To :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DiabeticAndHypertensive.Properties.Resources.search;
            this.pictureBox1.Location = new System.Drawing.Point(368, 9);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(16, 16);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            // 
            // phSearch
            // 
            this.phSearch.AutoSize = true;
            this.phSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.phSearch.Location = new System.Drawing.Point(8, 6);
            this.phSearch.Name = "phSearch";
            this.phSearch.Size = new System.Drawing.Size(61, 23);
            this.phSearch.TabIndex = 11;
            this.phSearch.Text = "Search";
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dtpFromDate.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpFromDate.Location = new System.Drawing.Point(504, 2);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(311, 30);
            this.dtpFromDate.TabIndex = 4;
            this.dtpFromDate.Value = new System.DateTime(2024, 1, 1, 0, 0, 0, 0);
            // 
            // txtSearch
            // 
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSearch.Location = new System.Drawing.Point(6, 6);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(356, 23);
            this.txtSearch.TabIndex = 9;
            this.txtSearch.Click += new System.EventHandler(this.txtSearch_Click);
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            this.txtSearch.Enter += new System.EventHandler(this.txtSearch_Enter);
            this.txtSearch.Leave += new System.EventHandler(this.txtSearch_Leave);
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label6.Location = new System.Drawing.Point(440, 6);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 23);
            this.label6.TabIndex = 5;
            this.label6.Text = "From :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dgvExMed);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(0, 71);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1303, 402);
            this.panel1.TabIndex = 17;
            // 
            // dgvExMed
            // 
            this.dgvExMed.AllowUserToAddRows = false;
            this.dgvExMed.AllowUserToDeleteRows = false;
            this.dgvExMed.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvExMed.BackgroundColor = System.Drawing.Color.White;
            this.dgvExMed.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvExMed.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvExMed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvExMed.Location = new System.Drawing.Point(0, 0);
            this.dgvExMed.Name = "dgvExMed";
            this.dgvExMed.ReadOnly = true;
            this.dgvExMed.RowHeadersVisible = false;
            this.dgvExMed.RowHeadersWidth = 51;
            this.dgvExMed.RowTemplate.Height = 24;
            this.dgvExMed.Size = new System.Drawing.Size(1303, 402);
            this.dgvExMed.TabIndex = 13;
            this.dgvExMed.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvExMed_CellFormatting);
            // 
            // MIExpiredMedicine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1303, 473);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.roundedPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MIExpiredMedicine";
            this.Text = "MIExpiredMedicine";
            this.Load += new System.EventHandler(this.MIExpiredMedicine_Load);
            this.roundedPanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvExMed)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private RoundedPanel roundedPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button LoadDateAdded;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label phSearch;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgvExMed;
    }
}