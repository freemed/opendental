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
			this.SuspendLayout();
			// 
			// toothChart2D
			// 
			this.toothChart2D.AutoFinish = false;
			this.toothChart2D.ColorBackground = System.Drawing.Color.Empty;
			this.toothChart2D.Cursor = System.Windows.Forms.Cursors.Default;
			this.toothChart2D.CursorTool = SparksToothChart.CursorTool.Pointer;
			this.toothChart2D.Location = new System.Drawing.Point(3,3);
			this.toothChart2D.Name = "toothChart2D";
			this.toothChart2D.PreferredPixelFormatNumber = 0;
			this.toothChart2D.Size = new System.Drawing.Size(410,307);
			this.toothChart2D.TabIndex = 195;
			this.toothChart2D.UseHardware = false;
			// 
			// toothChartOpenGL
			// 
			this.toothChartOpenGL.AutoFinish = false;
			this.toothChartOpenGL.ColorBackground = System.Drawing.Color.Empty;
			this.toothChartOpenGL.Cursor = System.Windows.Forms.Cursors.Default;
			this.toothChartOpenGL.CursorTool = SparksToothChart.CursorTool.Pointer;
			this.toothChartOpenGL.Location = new System.Drawing.Point(419,3);
			this.toothChartOpenGL.Name = "toothChartOpenGL";
			this.toothChartOpenGL.PreferredPixelFormatNumber = 0;
			this.toothChartOpenGL.Size = new System.Drawing.Size(410,307);
			this.toothChartOpenGL.TabIndex = 196;
			this.toothChartOpenGL.UseHardware = false;
			// 
			// toothChartDirectX
			// 
			this.toothChartDirectX.AutoFinish = false;
			this.toothChartDirectX.ColorBackground = System.Drawing.Color.Empty;
			this.toothChartDirectX.Cursor = System.Windows.Forms.Cursors.Default;
			this.toothChartDirectX.CursorTool = SparksToothChart.CursorTool.Pointer;
			this.toothChartDirectX.Location = new System.Drawing.Point(835,3);
			this.toothChartDirectX.Name = "toothChartDirectX";
			this.toothChartDirectX.PreferredPixelFormatNumber = 0;
			this.toothChartDirectX.Size = new System.Drawing.Size(410,307);
			this.toothChartDirectX.TabIndex = 197;
			this.toothChartDirectX.UseHardware = false;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F,13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1288,738);
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
	}
}

