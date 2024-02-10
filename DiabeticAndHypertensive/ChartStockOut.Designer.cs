namespace DiabeticAndHypertensive
{
    partial class ChartStockOut
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dgvSODiabetic = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.dgvSOHypertensive = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSODiabetic)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSOHypertensive)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(0, 0);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(800, 264);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 264);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 186);
            this.tableLayoutPanel1.TabIndex = 8;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dgvSODiabetic);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(150, 20, 50, 20);
            this.panel2.Size = new System.Drawing.Size(394, 180);
            this.panel2.TabIndex = 6;
            // 
            // dgvSODiabetic
            // 
            this.dgvSODiabetic.AllowUserToAddRows = false;
            this.dgvSODiabetic.AllowUserToDeleteRows = false;
            this.dgvSODiabetic.AllowUserToResizeColumns = false;
            this.dgvSODiabetic.AllowUserToResizeRows = false;
            this.dgvSODiabetic.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSODiabetic.BackgroundColor = System.Drawing.Color.White;
            this.dgvSODiabetic.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvSODiabetic.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvSODiabetic.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvSODiabetic.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSODiabetic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSODiabetic.Enabled = false;
            this.dgvSODiabetic.Location = new System.Drawing.Point(150, 53);
            this.dgvSODiabetic.MultiSelect = false;
            this.dgvSODiabetic.Name = "dgvSODiabetic";
            this.dgvSODiabetic.ReadOnly = true;
            this.dgvSODiabetic.RowHeadersVisible = false;
            this.dgvSODiabetic.RowHeadersWidth = 51;
            this.dgvSODiabetic.RowTemplate.Height = 24;
            this.dgvSODiabetic.ShowCellErrors = false;
            this.dgvSODiabetic.ShowCellToolTips = false;
            this.dgvSODiabetic.ShowEditingIcon = false;
            this.dgvSODiabetic.ShowRowErrors = false;
            this.dgvSODiabetic.Size = new System.Drawing.Size(194, 107);
            this.dgvSODiabetic.TabIndex = 3;
            this.dgvSODiabetic.TabStop = false;
            this.dgvSODiabetic.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvSODiabetic_CellFormatting);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.LightSeaGreen;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(150, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(194, 33);
            this.label2.TabIndex = 0;
            this.label2.Text = "Diabetic Medicine Stock-Out";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.dgvSOHypertensive);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(403, 3);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(50, 20, 150, 20);
            this.panel3.Size = new System.Drawing.Size(394, 180);
            this.panel3.TabIndex = 7;
            // 
            // dgvSOHypertensive
            // 
            this.dgvSOHypertensive.AllowUserToAddRows = false;
            this.dgvSOHypertensive.AllowUserToDeleteRows = false;
            this.dgvSOHypertensive.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSOHypertensive.BackgroundColor = System.Drawing.Color.White;
            this.dgvSOHypertensive.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvSOHypertensive.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvSOHypertensive.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvSOHypertensive.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSOHypertensive.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSOHypertensive.Location = new System.Drawing.Point(50, 53);
            this.dgvSOHypertensive.Name = "dgvSOHypertensive";
            this.dgvSOHypertensive.ReadOnly = true;
            this.dgvSOHypertensive.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvSOHypertensive.RowHeadersVisible = false;
            this.dgvSOHypertensive.RowHeadersWidth = 51;
            this.dgvSOHypertensive.RowTemplate.Height = 24;
            this.dgvSOHypertensive.Size = new System.Drawing.Size(194, 107);
            this.dgvSOHypertensive.TabIndex = 4;
            this.dgvSOHypertensive.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvSOHypertensive_CellFormatting);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.ForestGreen;
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(50, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(194, 33);
            this.label3.TabIndex = 0;
            this.label3.Text = "Hypertesive Medicine Stock-Out";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chart1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 264);
            this.panel1.TabIndex = 9;
            // 
            // ChartStockOut
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.panel1);
            this.Name = "ChartStockOut";
            this.Text = "ChartStockOut";
            this.Load += new System.EventHandler(this.ChartStockOut_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSODiabetic)).EndInit();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSOHypertensive)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dgvSODiabetic;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridView dgvSOHypertensive;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel1;
    }
}