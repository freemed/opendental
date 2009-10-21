namespace TestToothChart {
	partial class Form1 {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if(disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.toothChart2D = new SparksToothChart.ToothChartWrapper();
			this.toothChartOpenGL = new SparksToothChart.ToothChartWrapper();
			this.toothChartDirectX = new SparksToothChart.ToothChartWrapper();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.butReset = new System.Windows.Forms.Button();
			this.butAllPrimary = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// toothChart2D
			// 
			this.toothChart2D.AutoFinish = false;
			this.toothChart2D.Cursor = System.Windows.Forms.Cursors.Default;
			this.toothChart2D.CursorTool = SparksToothChart.CursorTool.Pointer;
			this.toothChart2D.DrawMode = SparksToothChart.DrawingMode.Simple2D;
			this.toothChart2D.Location = new System.Drawing.Point(8,28);
			this.toothChart2D.Name = "toothChart2D";
			this.toothChart2D.PreferredPixelFormatNumber = 0;
			this.toothChart2D.Size = new System.Drawing.Size(410,307);
			this.toothChart2D.TabIndex = 195;
			this.toothChart2D.UseHardware = false;
			// 
			// toothChartOpenGL
			// 
			this.toothChartOpenGL.AutoFinish = false;
			this.toothChartOpenGL.Cursor = System.Windows.Forms.Cursors.Default;
			this.toothChartOpenGL.CursorTool = SparksToothChart.CursorTool.Pointer;
			this.toothChartOpenGL.DrawMode = SparksToothChart.DrawingMode.Simple2D;
			this.toothChartOpenGL.Location = new System.Drawing.Point(424,28);
			this.toothChartOpenGL.Name = "toothChartOpenGL";
			this.toothChartOpenGL.PreferredPixelFormatNumber = 0;
			this.toothChartOpenGL.Size = new System.Drawing.Size(410,307);
			this.toothChartOpenGL.TabIndex = 196;
			this.toothChartOpenGL.UseHardware = false;
			// 
			// toothChartDirectX
			// 
			this.toothChartDirectX.AutoFinish = false;
			this.toothChartDirectX.Cursor = System.Windows.Forms.Cursors.Default;
			this.toothChartDirectX.CursorTool = SparksToothChart.CursorTool.Pointer;
			this.toothChartDirectX.DrawMode = SparksToothChart.DrawingMode.Simple2D;
			this.toothChartDirectX.Location = new System.Drawing.Point(840,28);
			this.toothChartDirectX.Name = "toothChartDirectX";
			this.toothChartDirectX.PreferredPixelFormatNumber = 0;
			this.toothChartDirectX.Size = new System.Drawing.Size(410,307);
			this.toothChartDirectX.TabIndex = 197;
			this.toothChartDirectX.UseHardware = false;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(167,5);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100,17);
			this.label1.TabIndex = 198;
			this.label1.Text = "2D";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(580,5);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100,17);
			this.label2.TabIndex = 199;
			this.label2.Text = "OpenGL";
			this.label2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(996,5);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(100,17);
			this.label3.TabIndex = 200;
			this.label3.Text = "DirectX";
			this.label3.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// butReset
			// 
			this.butReset.Location = new System.Drawing.Point(8,361);
			this.butReset.Name = "butReset";
			this.butReset.Size = new System.Drawing.Size(75,23);
			this.butReset.TabIndex = 201;
			this.butReset.Text = "Reset";
			this.butReset.UseVisualStyleBackColor = true;
			this.butReset.Click += new System.EventHandler(this.butReset_Click);
			// 
			// butAllPrimary
			// 
			this.butAllPrimary.Location = new System.Drawing.Point(8,390);
			this.butAllPrimary.Name = "butAllPrimary";
			this.butAllPrimary.Size = new System.Drawing.Size(75,23);
			this.butAllPrimary.TabIndex = 202;
			this.butAllPrimary.Text = "All Primary";
			this.butAllPrimary.UseVisualStyleBackColor = true;
			this.butAllPrimary.Click += new System.EventHandler(this.butAllPrimary_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F,13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1258,738);
			this.Controls.Add(this.butAllPrimary);
			this.Controls.Add(this.butReset);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.toothChartDirectX);
			this.Controls.Add(this.toothChartOpenGL);
			this.Controls.Add(this.toothChart2D);
			this.Name = "Form1";
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private SparksToothChart.ToothChartWrapper toothChart2D;
		private SparksToothChart.ToothChartWrapper toothChartOpenGL;
		private SparksToothChart.ToothChartWrapper toothChartDirectX;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button butReset;
		private System.Windows.Forms.Button butAllPrimary;
	}
}

