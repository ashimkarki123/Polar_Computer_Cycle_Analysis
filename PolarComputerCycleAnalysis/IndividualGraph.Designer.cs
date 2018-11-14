namespace PolarComputerCycleAnalysis
{
    partial class IndividualGraph
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
            this.label4 = new System.Windows.Forms.Label();
            this.zedGraphControl4 = new ZedGraph.ZedGraphControl();
            this.label3 = new System.Windows.Forms.Label();
            this.zedGraphControl3 = new ZedGraph.ZedGraphControl();
            this.label2 = new System.Windows.Forms.Label();
            this.zedGraphControl2 = new ZedGraph.ZedGraphControl();
            this.label1 = new System.Windows.Forms.Label();
            this.zedGraphControl1 = new ZedGraph.ZedGraphControl();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(-116, 385);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Power";
            // 
            // zedGraphControl4
            // 
            this.zedGraphControl4.IsShowPointValues = false;
            this.zedGraphControl4.Location = new System.Drawing.Point(0, 382);
            this.zedGraphControl4.Name = "zedGraphControl4";
            this.zedGraphControl4.PointValueFormat = "G";
            this.zedGraphControl4.Size = new System.Drawing.Size(780, 121);
            this.zedGraphControl4.TabIndex = 14;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(-116, 233);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Cadence";
            // 
            // zedGraphControl3
            // 
            this.zedGraphControl3.IsShowPointValues = false;
            this.zedGraphControl3.Location = new System.Drawing.Point(0, 255);
            this.zedGraphControl3.Name = "zedGraphControl3";
            this.zedGraphControl3.PointValueFormat = "G";
            this.zedGraphControl3.Size = new System.Drawing.Size(788, 121);
            this.zedGraphControl3.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(-116, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Heart Rate";
            // 
            // zedGraphControl2
            // 
            this.zedGraphControl2.IsShowPointValues = false;
            this.zedGraphControl2.Location = new System.Drawing.Point(0, 128);
            this.zedGraphControl2.Name = "zedGraphControl2";
            this.zedGraphControl2.PointValueFormat = "G";
            this.zedGraphControl2.Size = new System.Drawing.Size(788, 121);
            this.zedGraphControl2.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(-116, -74);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Speed";
            // 
            // zedGraphControl1
            // 
            this.zedGraphControl1.IsShowPointValues = false;
            this.zedGraphControl1.Location = new System.Drawing.Point(0, 1);
            this.zedGraphControl1.Name = "zedGraphControl1";
            this.zedGraphControl1.PointValueFormat = "G";
            this.zedGraphControl1.Size = new System.Drawing.Size(788, 121);
            this.zedGraphControl1.TabIndex = 8;
            // 
            // IndividualGraph
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(795, 504);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.zedGraphControl4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.zedGraphControl3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.zedGraphControl2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.zedGraphControl1);
            this.Name = "IndividualGraph";
            this.Text = "IndividualGraph";
            this.Load += new System.EventHandler(this.IndividualGraph_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private ZedGraph.ZedGraphControl zedGraphControl4;
        private System.Windows.Forms.Label label3;
        private ZedGraph.ZedGraphControl zedGraphControl3;
        private System.Windows.Forms.Label label2;
        private ZedGraph.ZedGraphControl zedGraphControl2;
        private System.Windows.Forms.Label label1;
        private ZedGraph.ZedGraphControl zedGraphControl1;
    }
}