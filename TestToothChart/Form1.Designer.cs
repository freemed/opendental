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
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.butReset = new System.Windows.Forms.Button();
			this.butAllPrimary = new System.Windows.Forms.Button();
			this.butMixed = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.panelColorBackgroundLtGray = new System.Windows.Forms.Panel();
			this.panelColorBackgroundBlue = new System.Windows.Forms.Panel();
			this.panelColorBackgroundWhite = new System.Windows.Forms.Panel();
			this.panelColorBackgroundBlack = new System.Windows.Forms.Panel();
			this.panelColorBackgroundGray = new System.Windows.Forms.Panel();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.panelColorTextWhite = new System.Windows.Forms.Panel();
			this.panelColorTextBlack = new System.Windows.Forms.Panel();
			this.panelColorTextGray = new System.Windows.Forms.Panel();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.panelColorTextHighlightRed = new System.Windows.Forms.Panel();
			this.panelColorTextHighlightWhite = new System.Windows.Forms.Panel();
			this.panelColorTextHighlightBlack = new System.Windows.Forms.Panel();
			this.panelColorTextHighlightGray = new System.Windows.Forms.Panel();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.panelColorBackHighlightBlue = new System.Windows.Forms.Panel();
			this.panelColorBackHighlightWhite = new System.Windows.Forms.Panel();
			this.panelColorBackHighlightBlack = new System.Windows.Forms.Panel();
			this.panelColorBackHighlightGray = new System.Windows.Forms.Panel();
			this.groupBox5 = new System.Windows.Forms.GroupBox();
			this.butSizeWide = new System.Windows.Forms.Button();
			this.butSizeTall = new System.Windows.Forms.Button();
			this.butSizeNormal = new System.Windows.Forms.Button();
			this.toothChartDirectX = new SparksToothChart.ToothChartWrapper();
			this.toothChartOpenGL = new SparksToothChart.ToothChartWrapper();
			this.toothChart2D = new SparksToothChart.ToothChartWrapper();
			this.butMissing = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.groupBox5.SuspendLayout();
			this.SuspendLayout();
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
			this.butAllPrimary.Location = new System.Drawing.Point(8,412);
			this.butAllPrimary.Name = "butAllPrimary";
			this.butAllPrimary.Size = new System.Drawing.Size(75,23);
			this.butAllPrimary.TabIndex = 202;
			this.butAllPrimary.Text = "All Primary";
			this.butAllPrimary.UseVisualStyleBackColor = true;
			this.butAllPrimary.Click += new System.EventHandler(this.butAllPrimary_Click);
			// 
			// butMixed
			// 
			this.butMixed.Location = new System.Drawing.Point(8,437);
			this.butMixed.Name = "butMixed";
			this.butMixed.Size = new System.Drawing.Size(75,23);
			this.butMixed.TabIndex = 203;
			this.butMixed.Text = "Mixed";
			this.butMixed.UseVisualStyleBackColor = true;
			this.butMixed.Click += new System.EventHandler(this.butMixed_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.panelColorBackgroundLtGray);
			this.groupBox1.Controls.Add(this.panelColorBackgroundBlue);
			this.groupBox1.Controls.Add(this.panelColorBackgroundWhite);
			this.groupBox1.Controls.Add(this.panelColorBackgroundBlack);
			this.groupBox1.Controls.Add(this.panelColorBackgroundGray);
			this.groupBox1.Location = new System.Drawing.Point(170,357);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(174,47);
			this.groupBox1.TabIndex = 204;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "ColorBackground";
			// 
			// panelColorBackgroundLtGray
			// 
			this.panelColorBackgroundLtGray.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))),((int)(((byte)(224)))),((int)(((byte)(224)))));
			this.panelColorBackgroundLtGray.Location = new System.Drawing.Point(38,18);
			this.panelColorBackgroundLtGray.Name = "panelColorBackgroundLtGray";
			this.panelColorBackgroundLtGray.Size = new System.Drawing.Size(23,21);
			this.panelColorBackgroundLtGray.TabIndex = 209;
			this.panelColorBackgroundLtGray.Click += new System.EventHandler(this.panelColorBackgroundLtGray_Click);
			// 
			// panelColorBackgroundBlue
			// 
			this.panelColorBackgroundBlue.BackColor = System.Drawing.Color.CornflowerBlue;
			this.panelColorBackgroundBlue.Location = new System.Drawing.Point(125,18);
			this.panelColorBackgroundBlue.Name = "panelColorBackgroundBlue";
			this.panelColorBackgroundBlue.Size = new System.Drawing.Size(23,21);
			this.panelColorBackgroundBlue.TabIndex = 208;
			this.panelColorBackgroundBlue.Click += new System.EventHandler(this.panelColorBackgroundBlue_Click);
			// 
			// panelColorBackgroundWhite
			// 
			this.panelColorBackgroundWhite.BackColor = System.Drawing.Color.White;
			this.panelColorBackgroundWhite.Location = new System.Drawing.Point(96,18);
			this.panelColorBackgroundWhite.Name = "panelColorBackgroundWhite";
			this.panelColorBackgroundWhite.Size = new System.Drawing.Size(23,21);
			this.panelColorBackgroundWhite.TabIndex = 207;
			this.panelColorBackgroundWhite.Click += new System.EventHandler(this.panelColorBackgroundWhite_Click);
			// 
			// panelColorBackgroundBlack
			// 
			this.panelColorBackgroundBlack.BackColor = System.Drawing.Color.Black;
			this.panelColorBackgroundBlack.Location = new System.Drawing.Point(67,18);
			this.panelColorBackgroundBlack.Name = "panelColorBackgroundBlack";
			this.panelColorBackgroundBlack.Size = new System.Drawing.Size(23,21);
			this.panelColorBackgroundBlack.TabIndex = 206;
			this.panelColorBackgroundBlack.Click += new System.EventHandler(this.panelColorBackgroundBlack_Click);
			// 
			// panelColorBackgroundGray
			// 
			this.panelColorBackgroundGray.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))),((int)(((byte)(145)))),((int)(((byte)(152)))));
			this.panelColorBackgroundGray.Location = new System.Drawing.Point(9,18);
			this.panelColorBackgroundGray.Name = "panelColorBackgroundGray";
			this.panelColorBackgroundGray.Size = new System.Drawing.Size(23,21);
			this.panelColorBackgroundGray.TabIndex = 205;
			this.panelColorBackgroundGray.Click += new System.EventHandler(this.panelColorBackgroundGray_Click);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.panelColorTextWhite);
			this.groupBox2.Controls.Add(this.panelColorTextBlack);
			this.groupBox2.Controls.Add(this.panelColorTextGray);
			this.groupBox2.Location = new System.Drawing.Point(170,410);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(129,47);
			this.groupBox2.TabIndex = 209;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "ColorText";
			// 
			// panelColorTextWhite
			// 
			this.panelColorTextWhite.BackColor = System.Drawing.Color.White;
			this.panelColorTextWhite.Location = new System.Drawing.Point(67,18);
			this.panelColorTextWhite.Name = "panelColorTextWhite";
			this.panelColorTextWhite.Size = new System.Drawing.Size(23,21);
			this.panelColorTextWhite.TabIndex = 207;
			this.panelColorTextWhite.Click += new System.EventHandler(this.panelColorTextWhite_Click);
			// 
			// panelColorTextBlack
			// 
			this.panelColorTextBlack.BackColor = System.Drawing.Color.Black;
			this.panelColorTextBlack.Location = new System.Drawing.Point(38,18);
			this.panelColorTextBlack.Name = "panelColorTextBlack";
			this.panelColorTextBlack.Size = new System.Drawing.Size(23,21);
			this.panelColorTextBlack.TabIndex = 206;
			this.panelColorTextBlack.Click += new System.EventHandler(this.panelColorTextBlack_Click);
			// 
			// panelColorTextGray
			// 
			this.panelColorTextGray.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))),((int)(((byte)(145)))),((int)(((byte)(152)))));
			this.panelColorTextGray.Location = new System.Drawing.Point(9,18);
			this.panelColorTextGray.Name = "panelColorTextGray";
			this.panelColorTextGray.Size = new System.Drawing.Size(23,21);
			this.panelColorTextGray.TabIndex = 205;
			this.panelColorTextGray.Click += new System.EventHandler(this.panelColorTextGray_Click);
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.panelColorTextHighlightRed);
			this.groupBox3.Controls.Add(this.panelColorTextHighlightWhite);
			this.groupBox3.Controls.Add(this.panelColorTextHighlightBlack);
			this.groupBox3.Controls.Add(this.panelColorTextHighlightGray);
			this.groupBox3.Location = new System.Drawing.Point(170,463);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(129,47);
			this.groupBox3.TabIndex = 210;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "ColorTextHighlight";
			// 
			// panelColorTextHighlightRed
			// 
			this.panelColorTextHighlightRed.BackColor = System.Drawing.Color.Red;
			this.panelColorTextHighlightRed.Location = new System.Drawing.Point(96,18);
			this.panelColorTextHighlightRed.Name = "panelColorTextHighlightRed";
			this.panelColorTextHighlightRed.Size = new System.Drawing.Size(23,21);
			this.panelColorTextHighlightRed.TabIndex = 208;
			this.panelColorTextHighlightRed.Click += new System.EventHandler(this.panelColorTextHighlightRed_Click);
			// 
			// panelColorTextHighlightWhite
			// 
			this.panelColorTextHighlightWhite.BackColor = System.Drawing.Color.White;
			this.panelColorTextHighlightWhite.Location = new System.Drawing.Point(67,18);
			this.panelColorTextHighlightWhite.Name = "panelColorTextHighlightWhite";
			this.panelColorTextHighlightWhite.Size = new System.Drawing.Size(23,21);
			this.panelColorTextHighlightWhite.TabIndex = 207;
			this.panelColorTextHighlightWhite.Click += new System.EventHandler(this.panelColorTextHighlightWhite_Click);
			// 
			// panelColorTextHighlightBlack
			// 
			this.panelColorTextHighlightBlack.BackColor = System.Drawing.Color.Black;
			this.panelColorTextHighlightBlack.Location = new System.Drawing.Point(38,18);
			this.panelColorTextHighlightBlack.Name = "panelColorTextHighlightBlack";
			this.panelColorTextHighlightBlack.Size = new System.Drawing.Size(23,21);
			this.panelColorTextHighlightBlack.TabIndex = 206;
			this.panelColorTextHighlightBlack.Click += new System.EventHandler(this.panelColorTextHighlightBlack_Click);
			// 
			// panelColorTextHighlightGray
			// 
			this.panelColorTextHighlightGray.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))),((int)(((byte)(145)))),((int)(((byte)(152)))));
			this.panelColorTextHighlightGray.Location = new System.Drawing.Point(9,18);
			this.panelColorTextHighlightGray.Name = "panelColorTextHighlightGray";
			this.panelColorTextHighlightGray.Size = new System.Drawing.Size(23,21);
			this.panelColorTextHighlightGray.TabIndex = 205;
			this.panelColorTextHighlightGray.Click += new System.EventHandler(this.panelColorTextHighlightGray_Click);
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.panelColorBackHighlightBlue);
			this.groupBox4.Controls.Add(this.panelColorBackHighlightWhite);
			this.groupBox4.Controls.Add(this.panelColorBackHighlightBlack);
			this.groupBox4.Controls.Add(this.panelColorBackHighlightGray);
			this.groupBox4.Location = new System.Drawing.Point(170,516);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(129,47);
			this.groupBox4.TabIndex = 211;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "ColorBackHighlight";
			// 
			// panelColorBackHighlightBlue
			// 
			this.panelColorBackHighlightBlue.BackColor = System.Drawing.Color.CornflowerBlue;
			this.panelColorBackHighlightBlue.Location = new System.Drawing.Point(96,18);
			this.panelColorBackHighlightBlue.Name = "panelColorBackHighlightBlue";
			this.panelColorBackHighlightBlue.Size = new System.Drawing.Size(23,21);
			this.panelColorBackHighlightBlue.TabIndex = 208;
			this.panelColorBackHighlightBlue.Click += new System.EventHandler(this.panelColorBackHighlightBlue_Click);
			// 
			// panelColorBackHighlightWhite
			// 
			this.panelColorBackHighlightWhite.BackColor = System.Drawing.Color.White;
			this.panelColorBackHighlightWhite.Location = new System.Drawing.Point(67,18);
			this.panelColorBackHighlightWhite.Name = "panelColorBackHighlightWhite";
			this.panelColorBackHighlightWhite.Size = new System.Drawing.Size(23,21);
			this.panelColorBackHighlightWhite.TabIndex = 207;
			this.panelColorBackHighlightWhite.Click += new System.EventHandler(this.panelColorBackHighlightWhite_Click);
			// 
			// panelColorBackHighlightBlack
			// 
			this.panelColorBackHighlightBlack.BackColor = System.Drawing.Color.Black;
			this.panelColorBackHighlightBlack.Location = new System.Drawing.Point(38,18);
			this.panelColorBackHighlightBlack.Name = "panelColorBackHighlightBlack";
			this.panelColorBackHighlightBlack.Size = new System.Drawing.Size(23,21);
			this.panelColorBackHighlightBlack.TabIndex = 206;
			this.panelColorBackHighlightBlack.Click += new System.EventHandler(this.panelColorBackHighlightBlack_Click);
			// 
			// panelColorBackHighlightGray
			// 
			this.panelColorBackHighlightGray.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))),((int)(((byte)(145)))),((int)(((byte)(152)))));
			this.panelColorBackHighlightGray.Location = new System.Drawing.Point(9,18);
			this.panelColorBackHighlightGray.Name = "panelColorBackHighlightGray";
			this.panelColorBackHighlightGray.Size = new System.Drawing.Size(23,21);
			this.panelColorBackHighlightGray.TabIndex = 205;
			this.panelColorBackHighlightGray.Click += new System.EventHandler(this.panelColorBackHighlightGray_Click);
			// 
			// groupBox5
			// 
			this.groupBox5.Controls.Add(this.butSizeWide);
			this.groupBox5.Controls.Add(this.butSizeTall);
			this.groupBox5.Controls.Add(this.butSizeNormal);
			this.groupBox5.Location = new System.Drawing.Point(399,361);
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.Size = new System.Drawing.Size(113,99);
			this.groupBox5.TabIndex = 212;
			this.groupBox5.TabStop = false;
			this.groupBox5.Text = "Resize and Scale";
			// 
			// butSizeWide
			// 
			this.butSizeWide.Location = new System.Drawing.Point(9,68);
			this.butSizeWide.Name = "butSizeWide";
			this.butSizeWide.Size = new System.Drawing.Size(75,23);
			this.butSizeWide.TabIndex = 206;
			this.butSizeWide.Text = "Wide";
			this.butSizeWide.UseVisualStyleBackColor = true;
			this.butSizeWide.Click += new System.EventHandler(this.butSizeWide_Click);
			// 
			// butSizeTall
			// 
			this.butSizeTall.Location = new System.Drawing.Point(9,43);
			this.butSizeTall.Name = "butSizeTall";
			this.butSizeTall.Size = new System.Drawing.Size(75,23);
			this.butSizeTall.TabIndex = 205;
			this.butSizeTall.Text = "Tall";
			this.butSizeTall.UseVisualStyleBackColor = true;
			this.butSizeTall.Click += new System.EventHandler(this.butSizeTall_Click);
			// 
			// butSizeNormal
			// 
			this.butSizeNormal.Location = new System.Drawing.Point(9,18);
			this.butSizeNormal.Name = "butSizeNormal";
			this.butSizeNormal.Size = new System.Drawing.Size(75,23);
			this.butSizeNormal.TabIndex = 204;
			this.butSizeNormal.Text = "Normal";
			this.butSizeNormal.UseVisualStyleBackColor = true;
			this.butSizeNormal.Click += new System.EventHandler(this.butSizeNormal_Click);
			// 
			// toothChartDirectX
			// 
			this.toothChartDirectX.AutoFinish = false;
			this.toothChartDirectX.ColorBackground = System.Drawing.Color.FromArgb(((int)(((byte)(150)))),((int)(((byte)(145)))),((int)(((byte)(152)))));
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
			// toothChartOpenGL
			// 
			this.toothChartOpenGL.AutoFinish = false;
			this.toothChartOpenGL.ColorBackground = System.Drawing.Color.FromArgb(((int)(((byte)(150)))),((int)(((byte)(145)))),((int)(((byte)(152)))));
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
			// toothChart2D
			// 
			this.toothChart2D.AutoFinish = false;
			this.toothChart2D.ColorBackground = System.Drawing.Color.FromArgb(((int)(((byte)(150)))),((int)(((byte)(145)))),((int)(((byte)(152)))));
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
			// butMissing
			// 
			this.butMissing.Location = new System.Drawing.Point(551,361);
			this.butMissing.Name = "butMissing";
			this.butMissing.Size = new System.Drawing.Size(75,23);
			this.butMissing.TabIndex = 207;
			this.butMissing.Text = "Missing";
			this.butMissing.UseVisualStyleBackColor = true;
			this.butMissing.Click += new System.EventHandler(this.butMissing_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F,13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1258,738);
			this.Controls.Add(this.butMissing);
			this.Controls.Add(this.groupBox5);
			this.Controls.Add(this.groupBox4);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.butMixed);
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
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.groupBox4.ResumeLayout(false);
			this.groupBox5.ResumeLayout(false);
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
		private System.Windows.Forms.Button butMixed;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Panel panelColorBackgroundGray;
		private System.Windows.Forms.Panel panelColorBackgroundBlue;
		private System.Windows.Forms.Panel panelColorBackgroundWhite;
		private System.Windows.Forms.Panel panelColorBackgroundBlack;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Panel panelColorTextWhite;
		private System.Windows.Forms.Panel panelColorTextBlack;
		private System.Windows.Forms.Panel panelColorTextGray;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Panel panelColorTextHighlightRed;
		private System.Windows.Forms.Panel panelColorTextHighlightWhite;
		private System.Windows.Forms.Panel panelColorTextHighlightBlack;
		private System.Windows.Forms.Panel panelColorTextHighlightGray;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.Panel panelColorBackHighlightBlue;
		private System.Windows.Forms.Panel panelColorBackHighlightWhite;
		private System.Windows.Forms.Panel panelColorBackHighlightBlack;
		private System.Windows.Forms.Panel panelColorBackHighlightGray;
		private System.Windows.Forms.Panel panelColorBackgroundLtGray;
		private System.Windows.Forms.GroupBox groupBox5;
		private System.Windows.Forms.Button butSizeWide;
		private System.Windows.Forms.Button butSizeTall;
		private System.Windows.Forms.Button butSizeNormal;
		private System.Windows.Forms.Button butMissing;
	}
}

