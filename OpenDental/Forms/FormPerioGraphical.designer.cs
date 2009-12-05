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
			SparksToothChart.ToothChartData toothChartData1=new SparksToothChart.ToothChartData();
			this.groupBox1=new System.Windows.Forms.GroupBox();
			this.butSetup=new OpenDental.UI.Button();
			this.panelColorMGJ=new System.Windows.Forms.Panel();
			this.panelColorCAL=new System.Windows.Forms.Panel();
			this.panelColorGM=new System.Windows.Forms.Panel();
			this.butColorProbingRed=new System.Windows.Forms.Button();
			this.label8=new System.Windows.Forms.Label();
			this.butColorProbing=new System.Windows.Forms.Button();
			this.label6=new System.Windows.Forms.Label();
			this.label3=new System.Windows.Forms.Label();
			this.label4=new System.Windows.Forms.Label();
			this.label2=new System.Windows.Forms.Label();
			this.label1=new System.Windows.Forms.Label();
			this.butColorPus=new System.Windows.Forms.Button();
			this.butColorBleed=new System.Windows.Forms.Button();
			this.toothChart=new SparksToothChart.ToothChartWrapper();
			this.butPrint=new OpenDental.UI.Button();
			this.butSave=new OpenDental.UI.Button();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.butSetup);
			this.groupBox1.Controls.Add(this.panelColorMGJ);
			this.groupBox1.Controls.Add(this.panelColorCAL);
			this.groupBox1.Controls.Add(this.panelColorGM);
			this.groupBox1.Controls.Add(this.butColorProbingRed);
			this.groupBox1.Controls.Add(this.label8);
			this.groupBox1.Controls.Add(this.butColorProbing);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.butColorPus);
			this.groupBox1.Controls.Add(this.butColorBleed);
			this.groupBox1.Location=new System.Drawing.Point(65,695);
			this.groupBox1.Name="groupBox1";
			this.groupBox1.Size=new System.Drawing.Size(556,106);
			this.groupBox1.TabIndex=217;
			this.groupBox1.TabStop=false;
			// 
			// butSetup
			// 
			this.butSetup.AdjustImageLocation=new System.Drawing.Point(0,0);
			this.butSetup.Autosize=true;
			this.butSetup.BtnShape=OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSetup.BtnStyle=OpenDental.UI.enumType.XPStyle.Silver;
			this.butSetup.CornerRadius=4F;
			this.butSetup.Location=new System.Drawing.Point(446,63);
			this.butSetup.Name="butSetup";
			this.butSetup.Size=new System.Drawing.Size(88,24);
			this.butSetup.TabIndex=221;
			this.butSetup.Text="Setup Colors";
			this.butSetup.Click+=new System.EventHandler(this.butSetup_Click);
			// 
			// panelColorMGJ
			// 
			this.panelColorMGJ.BackColor=System.Drawing.Color.DarkOrange;
			this.panelColorMGJ.Enabled=false;
			this.panelColorMGJ.Location=new System.Drawing.Point(283,79);
			this.panelColorMGJ.Name="panelColorMGJ";
			this.panelColorMGJ.Size=new System.Drawing.Size(50,2);
			this.panelColorMGJ.TabIndex=220;
			// 
			// panelColorCAL
			// 
			this.panelColorCAL.BackColor=System.Drawing.Color.FromArgb(((int)(((byte)(0)))),((int)(((byte)(0)))),((int)(((byte)(192)))));
			this.panelColorCAL.Enabled=false;
			this.panelColorCAL.Location=new System.Drawing.Point(283,53);
			this.panelColorCAL.Name="panelColorCAL";
			this.panelColorCAL.Size=new System.Drawing.Size(50,2);
			this.panelColorCAL.TabIndex=219;
			// 
			// panelColorGM
			// 
			this.panelColorGM.BackColor=System.Drawing.Color.Purple;
			this.panelColorGM.Enabled=false;
			this.panelColorGM.Location=new System.Drawing.Point(283,28);
			this.panelColorGM.Name="panelColorGM";
			this.panelColorGM.Size=new System.Drawing.Size(50,2);
			this.panelColorGM.TabIndex=218;
			// 
			// butColorProbingRed
			// 
			this.butColorProbingRed.BackColor=System.Drawing.Color.Red;
			this.butColorProbingRed.Enabled=false;
			this.butColorProbingRed.FlatStyle=System.Windows.Forms.FlatStyle.Popup;
			this.butColorProbingRed.Location=new System.Drawing.Point(112,70);
			this.butColorProbingRed.Name="butColorProbingRed";
			this.butColorProbingRed.Size=new System.Drawing.Size(4,20);
			this.butColorProbingRed.TabIndex=66;
			this.butColorProbingRed.UseVisualStyleBackColor=false;
			// 
			// label8
			// 
			this.label8.Location=new System.Drawing.Point(11,70);
			this.label8.Name="label8";
			this.label8.Size=new System.Drawing.Size(89,18);
			this.label8.TabIndex=65;
			this.label8.Text="Probing";
			this.label8.TextAlign=System.Drawing.ContentAlignment.MiddleRight;
			// 
			// butColorProbing
			// 
			this.butColorProbing.BackColor=System.Drawing.Color.Green;
			this.butColorProbing.Enabled=false;
			this.butColorProbing.FlatStyle=System.Windows.Forms.FlatStyle.Popup;
			this.butColorProbing.Location=new System.Drawing.Point(102,70);
			this.butColorProbing.Name="butColorProbing";
			this.butColorProbing.Size=new System.Drawing.Size(4,20);
			this.butColorProbing.TabIndex=64;
			this.butColorProbing.UseVisualStyleBackColor=false;
			// 
			// label6
			// 
			this.label6.Location=new System.Drawing.Point(137,69);
			this.label6.Name="label6";
			this.label6.Size=new System.Drawing.Size(144,18);
			this.label6.TabIndex=61;
			this.label6.Text="Mucogingival Junction";
			this.label6.TextAlign=System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label3
			// 
			this.label3.Location=new System.Drawing.Point(137,44);
			this.label3.Name="label3";
			this.label3.Size=new System.Drawing.Size(144,18);
			this.label3.TabIndex=58;
			this.label3.Text="Clinical Attachment Level";
			this.label3.TextAlign=System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label4
			// 
			this.label4.Location=new System.Drawing.Point(137,19);
			this.label4.Name="label4";
			this.label4.Size=new System.Drawing.Size(144,18);
			this.label4.TabIndex=57;
			this.label4.Text="Gingival Margin";
			this.label4.TextAlign=System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label2
			// 
			this.label2.Location=new System.Drawing.Point(11,44);
			this.label2.Name="label2";
			this.label2.Size=new System.Drawing.Size(89,18);
			this.label2.TabIndex=54;
			this.label2.Text="Suppuration";
			this.label2.TextAlign=System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label1
			// 
			this.label1.Location=new System.Drawing.Point(11,19);
			this.label1.Name="label1";
			this.label1.Size=new System.Drawing.Size(89,18);
			this.label1.TabIndex=53;
			this.label1.Text="Bleeding";
			this.label1.TextAlign=System.Drawing.ContentAlignment.MiddleRight;
			// 
			// butColorPus
			// 
			this.butColorPus.BackColor=System.Drawing.Color.Gold;
			this.butColorPus.Enabled=false;
			this.butColorPus.FlatStyle=System.Windows.Forms.FlatStyle.Popup;
			this.butColorPus.Location=new System.Drawing.Point(102,51);
			this.butColorPus.Name="butColorPus";
			this.butColorPus.Size=new System.Drawing.Size(6,6);
			this.butColorPus.TabIndex=52;
			this.butColorPus.UseVisualStyleBackColor=false;
			// 
			// butColorBleed
			// 
			this.butColorBleed.BackColor=System.Drawing.Color.Red;
			this.butColorBleed.Enabled=false;
			this.butColorBleed.FlatStyle=System.Windows.Forms.FlatStyle.Popup;
			this.butColorBleed.Location=new System.Drawing.Point(102,26);
			this.butColorBleed.Name="butColorBleed";
			this.butColorBleed.Size=new System.Drawing.Size(6,6);
			this.butColorBleed.TabIndex=51;
			this.butColorBleed.UseVisualStyleBackColor=false;
			// 
			// toothChart
			// 
			this.toothChart.AutoFinish=false;
			this.toothChart.ColorBackground=System.Drawing.Color.FromArgb(((int)(((byte)(150)))),((int)(((byte)(145)))),((int)(((byte)(152)))));
			this.toothChart.Cursor=System.Windows.Forms.Cursors.Default;
			this.toothChart.CursorTool=SparksToothChart.CursorTool.Pointer;
			this.toothChart.DrawMode=OpenDentBusiness.DrawingMode.Simple2D;
			this.toothChart.Location=new System.Drawing.Point(66,12);
			this.toothChart.Name="toothChart";
			this.toothChart.PerioMode=false;
			this.toothChart.PreferredPixelFormatNumber=0;
			this.toothChart.Size=new System.Drawing.Size(700,667);
			this.toothChart.TabIndex=198;
			toothChartData1.SizeControl=new System.Drawing.Size(700,667);
			this.toothChart.TcData=toothChartData1;
			this.toothChart.UseHardware=false;
			// 
			// butPrint
			// 
			this.butPrint.AdjustImageLocation=new System.Drawing.Point(0,0);
			this.butPrint.Autosize=true;
			this.butPrint.BtnShape=OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPrint.BtnStyle=OpenDental.UI.enumType.XPStyle.Silver;
			this.butPrint.CornerRadius=4F;
			this.butPrint.Location=new System.Drawing.Point(691,758);
			this.butPrint.Name="butPrint";
			this.butPrint.Size=new System.Drawing.Size(75,24);
			this.butPrint.TabIndex=220;
			this.butPrint.Text="Print";
			this.butPrint.Click+=new System.EventHandler(this.butPrint_Click);
			// 
			// butSave
			// 
			this.butSave.AdjustImageLocation=new System.Drawing.Point(0,0);
			this.butSave.Autosize=true;
			this.butSave.BtnShape=OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSave.BtnStyle=OpenDental.UI.enumType.XPStyle.Silver;
			this.butSave.CornerRadius=4F;
			this.butSave.Location=new System.Drawing.Point(674,717);
			this.butSave.Name="butSave";
			this.butSave.Size=new System.Drawing.Size(92,24);
			this.butSave.TabIndex=219;
			this.butSave.Text="Save to Images";
			this.butSave.Click+=new System.EventHandler(this.butSave_Click);
			// 
			// FormPerioGraphical
			// 
			this.AutoScaleDimensions=new System.Drawing.SizeF(6F,13F);
			this.AutoScaleMode=System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize=new System.Drawing.Size(846,817);
			this.Controls.Add(this.butPrint);
			this.Controls.Add(this.butSave);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.toothChart);
			this.Name="FormPerioGraphical";
			this.StartPosition=System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text="Graphical Perio Chart";
			this.Load+=new System.EventHandler(this.FormPerioGraphic_Load);
			this.FormClosed+=new System.Windows.Forms.FormClosedEventHandler(this.FormPerioGraphical_FormClosed);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private SparksToothChart.ToothChartWrapper toothChart;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button butColorPus;
		private System.Windows.Forms.Button butColorBleed;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Button butColorProbing;
		private System.Windows.Forms.Button butColorProbingRed;
		private System.Windows.Forms.Panel panelColorMGJ;
		private System.Windows.Forms.Panel panelColorCAL;
		private System.Windows.Forms.Panel panelColorGM;
		private OpenDental.UI.Button butSave;
		private OpenDental.UI.Button butSetup;
		private OpenDental.UI.Button butPrint;
	}
}