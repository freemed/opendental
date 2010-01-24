namespace OpenDental {
	partial class FormPerioGraphical {
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
			SparksToothChart.ToothChartData toothChartData1 = new SparksToothChart.ToothChartData();
			this.toothChart = new SparksToothChart.ToothChartWrapper();
			this.butPrint = new OpenDental.UI.Button();
			this.butSave = new OpenDental.UI.Button();
			this.butSetup = new OpenDental.UI.Button();
			this.butClose = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// toothChart
			// 
			this.toothChart.AutoFinish = false;
			this.toothChart.ColorBackground = System.Drawing.Color.FromArgb(((int)(((byte)(150)))),((int)(((byte)(145)))),((int)(((byte)(152)))));
			this.toothChart.Cursor = System.Windows.Forms.Cursors.Default;
			this.toothChart.CursorTool = SparksToothChart.CursorTool.Pointer;
			this.toothChart.DeviceFormat = null;
			this.toothChart.DrawMode = OpenDentBusiness.DrawingMode.Simple2D;
			this.toothChart.Location = new System.Drawing.Point(66,12);
			this.toothChart.Name = "toothChart";
			this.toothChart.PerioMode = false;
			this.toothChart.PreferredPixelFormatNumber = 0;
			this.toothChart.Size = new System.Drawing.Size(700,751);
			this.toothChart.TabIndex = 198;
			toothChartData1.SizeControl = new System.Drawing.Size(700,751);
			this.toothChart.TcData = toothChartData1;
			this.toothChart.UseHardware = false;
			// 
			// butPrint
			// 
			this.butPrint.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butPrint.Autosize = true;
			this.butPrint.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPrint.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPrint.CornerRadius = 4F;
			this.butPrint.Image = global::OpenDental.Properties.Resources.butPrint;
			this.butPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butPrint.Location = new System.Drawing.Point(564,787);
			this.butPrint.Name = "butPrint";
			this.butPrint.Size = new System.Drawing.Size(81,24);
			this.butPrint.TabIndex = 220;
			this.butPrint.Text = "Print";
			this.butPrint.Click += new System.EventHandler(this.butPrint_Click);
			// 
			// butSave
			// 
			this.butSave.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butSave.Autosize = true;
			this.butSave.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSave.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSave.CornerRadius = 4F;
			this.butSave.Location = new System.Drawing.Point(466,787);
			this.butSave.Name = "butSave";
			this.butSave.Size = new System.Drawing.Size(92,24);
			this.butSave.TabIndex = 219;
			this.butSave.Text = "Save to Images";
			this.butSave.Click += new System.EventHandler(this.butSave_Click);
			// 
			// butSetup
			// 
			this.butSetup.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butSetup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butSetup.Autosize = true;
			this.butSetup.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSetup.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSetup.CornerRadius = 4F;
			this.butSetup.Location = new System.Drawing.Point(66,787);
			this.butSetup.Name = "butSetup";
			this.butSetup.Size = new System.Drawing.Size(88,24);
			this.butSetup.TabIndex = 221;
			this.butSetup.Text = "Setup Colors";
			this.butSetup.Click += new System.EventHandler(this.butSetup_Click);
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.CornerRadius = 4F;
			this.butClose.Location = new System.Drawing.Point(691,787);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75,24);
			this.butClose.TabIndex = 222;
			this.butClose.Text = "Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// FormPerioGraphical
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F,13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(846,851);
			this.Controls.Add(this.butClose);
			this.Controls.Add(this.butSetup);
			this.Controls.Add(this.butPrint);
			this.Controls.Add(this.butSave);
			this.Controls.Add(this.toothChart);
			this.Name = "FormPerioGraphical";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Graphical Perio Chart";
			this.Load += new System.EventHandler(this.FormPerioGraphic_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private SparksToothChart.ToothChartWrapper toothChart;
		private OpenDental.UI.Button butSave;
		private OpenDental.UI.Button butSetup;
		private OpenDental.UI.Button butPrint;
		private OpenDental.UI.Button butClose;
	}
}