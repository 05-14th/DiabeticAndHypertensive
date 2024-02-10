namespace DiabeticAndHypertensive
{
    partial class ChartPI
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.chartBarangay = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.ChartP = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartBarangay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChartP)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.chartBarangay, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 28.57143F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 71.42857F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1262, 610);
            this.tableLayoutPanel2.TabIndex = 6;
            // 
            // chartBarangay
            // 
            chartArea1.Name = "ChartArea1";
            this.chartBarangay.ChartAreas.Add(chartArea1);
            this.chartBarangay.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.chartBarangay.Legends.Add(legend1);
            this.chartBarangay.Location = new System.Drawing.Point(3, 177);
            this.chartBarangay.Name = "chartBarangay";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartBarangay.Series.Add(series1);
            this.chartBarangay.Size = new System.Drawing.Size(1256, 430);
            this.chartBarangay.TabIndex = 1;
            this.chartBarangay.Text = "chartPatient";
            // 
            // ChartP
            // 
            chartArea2.Name = "ChartArea1";
            this.ChartP.ChartAreas.Add(chartArea2);
            this.ChartP.Dock = System.Windows.Forms.DockStyle.Fill;
            legend2.Name = "Legend1";
            this.ChartP.Legends.Add(legend2);
            this.ChartP.Location = new System.Drawing.Point(200, 2);
            this.ChartP.Name = "ChartP";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.ChartP.Series.Add(series2);
            this.ChartP.Size = new System.Drawing.Size(856, 164);
            this.ChartP.TabIndex = 0;
            this.ChartP.Text = "chartPatient";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ChartP);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(200, 2, 200, 2);
            this.panel1.Size = new System.Drawing.Size(1256, 168);
            this.panel1.TabIndex = 2;
            // 
            // ChartPI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1262, 610);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Name = "ChartPI";
            this.Text = "PIchart";
            this.Load += new System.EventHandler(this.PIchart_Load);
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartBarangay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChartP)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.DataVisualization.Charting.Chart ChartP;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartBarangay;
        private System.Windows.Forms.Panel panel1;
    }
}