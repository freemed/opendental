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
			SparksToothChart.ToothChartData toothChartData1 = new SparksToothChart.ToothChartData();
			this.butPrint = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label6 = new System.Windows.Forms.Label();
			this.button4 = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.butColorGM = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.butColorPus = new System.Windows.Forms.Button();
			this.butColorBleed = new System.Windows.Forms.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.toothChart = new SparksToothChart.ToothChartWrapper();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// butPrint
			// 
			this.butPrint.Location = new System.Drawing.Point(691,722);
			this.butPrint.Name = "butPrint";
			this.butPrint.Size = new System.Drawing.Size(75,23);
			this.butPrint.TabIndex = 216;
			this.butPrint.Text = "Print";
			this.butPrint.UseVisualStyleBackColor = true;
			this.butPrint.Click += new System.EventHandler(this.butPrint_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.button4);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.button1);
			this.groupBox1.Controls.Add(this.butColorGM);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.butColorPus);
			this.groupBox1.Controls.Add(this.butColorBleed);
			this.groupBox1.Location = new System.Drawing.Point(66,722);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(367,131);
			this.groupBox1.TabIndex = 217;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Setup Colors (not yet editable here)";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(150,90);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(136,18);
			this.label6.TabIndex = 61;
			this.label6.Text = "Mucogingival Junction";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// button4
			// 
			this.button4.BackColor = System.Drawing.Color.SandyBrown;
			this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.button4.Location = new System.Drawing.Point(288,88);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(22,22);
			this.button4.TabIndex = 59;
			this.button4.UseVisualStyleBackColor = false;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(150,65);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(136,18);
			this.label3.TabIndex = 58;
			this.label3.Text = "Clinical Attachment Level";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(150,40);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(136,18);
			this.label4.TabIndex = 57;
			this.label4.Text = "Gingival Margin";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// button1
			// 
			this.button1.BackColor = System.Drawing.Color.LightSkyBlue;
			this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.button1.Location = new System.Drawing.Point(288,63);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(22,22);
			this.button1.TabIndex = 56;
			this.button1.UseVisualStyleBackColor = false;
			// 
			// butColorGM
			// 
			this.butColorGM.BackColor = System.Drawing.Color.Pink;
			this.butColorGM.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.butColorGM.ForeColor = System.Drawing.SystemColors.ControlText;
			this.butColorGM.Location = new System.Drawing.Point(288,38);
			this.butColorGM.Name = "butColorGM";
			this.butColorGM.Size = new System.Drawing.Size(22,22);
			this.butColorGM.TabIndex = 55;
			this.butColorGM.UseVisualStyleBackColor = false;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(22,65);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(78,18);
			this.label2.TabIndex = 54;
			this.label2.Text = "Suppuration";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(22,40);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(78,18);
			this.label1.TabIndex = 53;
			this.label1.Text = "Bleeding";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// butColorPus
			// 
			this.butColorPus.BackColor = System.Drawing.Color.Gold;
			this.butColorPus.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.butColorPus.Location = new System.Drawing.Point(102,63);
			this.butColorPus.Name = "butColorPus";
			this.butColorPus.Size = new System.Drawing.Size(22,22);
			this.butColorPus.TabIndex = 52;
			this.butColorPus.UseVisualStyleBackColor = false;
			// 
			// butColorBleed
			// 
			this.butColorBleed.BackColor = System.Drawing.Color.Red;
			this.butColorBleed.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.butColorBleed.Location = new System.Drawing.Point(102,38);
			this.butColorBleed.Name = "butColorBleed";
			this.butColorBleed.Size = new System.Drawing.Size(22,22);
			this.butColorBleed.TabIndex = 51;
			this.butColorBleed.UseVisualStyleBackColor = false;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(454,727);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(215,126);
			this.label5.TabIndex = 218;
			this.label5.Text = "Tests currently included:\r\nMissing teeth (13,14,18,25,26)\r\nImplant (14)\r\nMobiliti" +
    "es\r\nFurcations (2,5,30)\r\nBleeding and Suppuration\r\nGM, CAL, MGJ\r\nProbing";
			// 
			// toothChart
			// 
			this.toothChart.AutoFinish = false;
			this.toothChart.ColorBackground = System.Drawing.Color.FromArgb(((int)(((byte)(150)))),((int)(((byte)(145)))),((int)(((byte)(152)))));
			this.toothChart.Cursor = System.Windows.Forms.Cursors.Default;
			this.toothChart.CursorTool = SparksToothChart.CursorTool.Pointer;
			this.toothChart.DrawMode = OpenDentBusiness.DrawingMode.Simple2D;
			this.toothChart.Location = new System.Drawing.Point(66,12);
			this.toothChart.Name = "toothChart";
			this.toothChart.PerioMode = false;
			this.toothChart.PreferredPixelFormatNumber = 0;
			this.toothChart.Size = new System.Drawing.Size(700,667);
			this.toothChart.TabIndex = 198;
			toothChartData1.SizeControl = new System.Drawing.Size(700,667);
			this.toothChart.TcData = toothChartData1;
			this.toothChart.UseHardware = false;
			// 
			// FormPerioTest
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F,13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(846,881);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.butPrint);
			this.Controls.Add(this.toothChart);
			this.Name = "FormPerioTest";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "FormPerioTest";
			this.Load += new System.EventHandler(this.FormPerioTest_Load);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private SparksToothChart.ToothChartWrapper toothChart;
		private System.Windows.Forms.Button butPrint;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button butColorPus;
		private System.Windows.Forms.Button butColorBleed;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button butColorGM;
		private System.Windows.Forms.Label label5;
	}
}