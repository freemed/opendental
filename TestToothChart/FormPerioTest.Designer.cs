namespace TestToothChart {
	partial class FormPerioTest {
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
			SparksToothChart.ToothChartData toothChartData3 = new SparksToothChart.ToothChartData();
			this.toothChart = new SparksToothChart.ToothChartWrapper();
			this.SuspendLayout();
			// 
			// toothChart
			// 
			this.toothChart.AutoFinish = false;
			this.toothChart.ColorBackground = System.Drawing.Color.FromArgb(((int)(((byte)(150)))),((int)(((byte)(145)))),((int)(((byte)(152)))));
			this.toothChart.Cursor = System.Windows.Forms.Cursors.Default;
			this.toothChart.CursorTool = SparksToothChart.CursorTool.Pointer;
			this.toothChart.DrawMode = OpenDentBusiness.DrawingMode.Simple2D;
			this.toothChart.Location = new System.Drawing.Point(71,65);
			this.toothChart.Name = "toothChart";
			this.toothChart.PreferredPixelFormatNumber = 0;
			this.toothChart.Size = new System.Drawing.Size(700,667);
			this.toothChart.TabIndex = 198;
			toothChartData3.SizeControl = new System.Drawing.Size(700,667);
			this.toothChart.TcData = toothChartData3;
			this.toothChart.UseHardware = false;
			// 
			// FormPerioTest
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F,13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(846,769);
			this.Controls.Add(this.toothChart);
			this.Name = "FormPerioTest";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "FormPerioTest";
			this.Load += new System.EventHandler(this.FormPerioTest_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private SparksToothChart.ToothChartWrapper toothChart;
	}
}