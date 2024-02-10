namespace DiabeticAndHypertensive
{
    partial class MIInventory
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
            this.panel9 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.phSearch = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.dgvMI = new System.Windows.Forms.DataGridView();
            this.roundedPanel1 = new RoundedPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMI)).BeginInit();
            this.roundedPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.pictureBox1);
            this.panel9.Controls.Add(this.phSearch);
            this.panel9.Controls.Add(this.txtSearch);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel9.Location = new System.Drawing.Point(0, 36);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(1370, 34);
            this.panel9.TabIndex = 10;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DiabeticAndHypertensive.Properties.Resources.search;
            this.pictureBox1.Location = new System.Drawing.Point(324, 9);
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
            this.phSearch.Location = new System.Drawing.Point(13, 6);
            this.phSearch.Name = "phSearch";
            this.phSearch.Size = new System.Drawing.Size(49, 19);
            this.phSearch.TabIndex = 11;
            this.phSearch.Text = "Search";
            // 
            // txtSearch
            // 
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSearch.Location = new System.Drawing.Point(10, 6);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(312, 18);
            this.txtSearch.TabIndex = 9;
            this.txtSearch.Click += new System.EventHandler(this.txtSearch_Click);
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            this.txtSearch.Enter += new System.EventHandler(this.txtSearch_Enter);
            this.txtSearch.Leave += new System.EventHandler(this.txtSearch_Leave);
            // 
            // dgvMI
            // 
            this.dgvMI.AllowUserToAddRows = false;
            this.dgvMI.AllowUserToDeleteRows = false;
            this.dgvMI.AllowUserToResizeColumns = false;
            this.dgvMI.AllowUserToResizeRows = false;
            this.dgvMI.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMI.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvMI.BackgroundColor = System.Drawing.Color.White;
            this.dgvMI.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvMI.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvMI.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMI.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMI.GridColor = System.Drawing.Color.White;
            this.dgvMI.Location = new System.Drawing.Point(0, 70);
            this.dgvMI.MultiSelect = false;
            this.dgvMI.Name = "dgvMI";
            this.dgvMI.ReadOnly = true;
            this.dgvMI.RowHeadersVisible = false;
            this.dgvMI.RowHeadersWidth = 51;
            this.dgvMI.RowTemplate.Height = 24;
            this.dgvMI.ShowCellErrors = false;
            this.dgvMI.ShowCellToolTips = false;
            this.dgvMI.ShowEditingIcon = false;
            this.dgvMI.ShowRowErrors = false;
            this.dgvMI.Size = new System.Drawing.Size(1370, 666);
            this.dgvMI.TabIndex = 11;
            this.dgvMI.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMI_CellContentClick);
            this.dgvMI.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvMI_CellFormatting);
            // 
            // roundedPanel1
            // 
            this.roundedPanel1.Controls.Add(this.label1);
            this.roundedPanel1.CornerRadius = 5;
            this.roundedPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.roundedPanel1.Location = new System.Drawing.Point(0, 0);
            this.roundedPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.roundedPanel1.Name = "roundedPanel1";
            this.roundedPanel1.Size = new System.Drawing.Size(1370, 36);
            this.roundedPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1370, 36);
            this.label1.TabIndex = 0;
            this.label1.Text = "Medicine Inventory";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MIInventory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1370, 736);
            this.Controls.Add(this.dgvMI);
            this.Controls.Add(this.panel9);
            this.Controls.Add(this.roundedPanel1);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(1364, 736);
            this.Name = "MIInventory";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MIInventory";
            this.Load += new System.EventHandler(this.MIInventory_Load);
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMI)).EndInit();
            this.roundedPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private RoundedPanel roundedPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label phSearch;
        private System.Windows.Forms.TextBox txtSearch;
        public System.Windows.Forms.DataGridView dgvMI;
    }
}