namespace OpenDental{
	partial class FormHL7DefEdit {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormHL7DefEdit));
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label15 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.textNote = new System.Windows.Forms.TextBox();
			this.labelOutPortEx = new System.Windows.Forms.Label();
			this.labelInPortEx = new System.Windows.Forms.Label();
			this.textInternalTypeVersion = new System.Windows.Forms.TextBox();
			this.label13 = new System.Windows.Forms.Label();
			this.textInternalType = new System.Windows.Forms.TextBox();
			this.label14 = new System.Windows.Forms.Label();
			this.checkEnabled = new System.Windows.Forms.CheckBox();
			this.textEscChar = new System.Windows.Forms.TextBox();
			this.label12 = new System.Windows.Forms.Label();
			this.checkInternal = new System.Windows.Forms.CheckBox();
			this.label11 = new System.Windows.Forms.Label();
			this.textSubcompSep = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.textRepSep = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.textCompSep = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.textFieldSep = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.comboModeTx = new System.Windows.Forms.ComboBox();
			this.butBrowseOut = new OpenDental.UI.Button();
			this.butBrowseIn = new OpenDental.UI.Button();
			this.textOutPort = new System.Windows.Forms.TextBox();
			this.labelOutPort = new System.Windows.Forms.Label();
			this.textInPort = new System.Windows.Forms.TextBox();
			this.labelInPort = new System.Windows.Forms.Label();
			this.textOutPath = new System.Windows.Forms.TextBox();
			this.labelOutPath = new System.Windows.Forms.Label();
			this.textInPath = new System.Windows.Forms.TextBox();
			this.labelInPath = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.textDescription = new System.Windows.Forms.TextBox();
			this.gridMain = new OpenDental.UI.ODGrid();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label15);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.textNote);
			this.groupBox1.Controls.Add(this.labelOutPortEx);
			this.groupBox1.Controls.Add(this.labelInPortEx);
			this.groupBox1.Controls.Add(this.textInternalTypeVersion);
			this.groupBox1.Controls.Add(this.label13);
			this.groupBox1.Controls.Add(this.textInternalType);
			this.groupBox1.Controls.Add(this.label14);
			this.groupBox1.Controls.Add(this.checkEnabled);
			this.groupBox1.Controls.Add(this.textEscChar);
			this.groupBox1.Controls.Add(this.label12);
			this.groupBox1.Controls.Add(this.checkInternal);
			this.groupBox1.Controls.Add(this.label11);
			this.groupBox1.Controls.Add(this.textSubcompSep);
			this.groupBox1.Controls.Add(this.label9);
			this.groupBox1.Controls.Add(this.textRepSep);
			this.groupBox1.Controls.Add(this.label10);
			this.groupBox1.Controls.Add(this.textCompSep);
			this.groupBox1.Controls.Add(this.label8);
			this.groupBox1.Controls.Add(this.textFieldSep);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Controls.Add(this.comboModeTx);
			this.groupBox1.Controls.Add(this.butBrowseOut);
			this.groupBox1.Controls.Add(this.butBrowseIn);
			this.groupBox1.Controls.Add(this.textOutPort);
			this.groupBox1.Controls.Add(this.labelOutPort);
			this.groupBox1.Controls.Add(this.textInPort);
			this.groupBox1.Controls.Add(this.labelInPort);
			this.groupBox1.Controls.Add(this.textOutPath);
			this.groupBox1.Controls.Add(this.labelOutPath);
			this.groupBox1.Controls.Add(this.textInPath);
			this.groupBox1.Controls.Add(this.labelInPath);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.textDescription);
			this.groupBox1.Location = new System.Drawing.Point(17,2);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(890,280);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "HL7 Settings";
			// 
			// label15
			// 
			this.label15.Location = new System.Drawing.Point(187,252);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(105,18);
			this.label15.TabIndex = 24;
			this.label15.Text = "Default: \\";
			this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(187,232);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(105,18);
			this.label6.TabIndex = 23;
			this.label6.Text = "Default: &";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.label6.UseMnemonic = false;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(187,212);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(105,18);
			this.label5.TabIndex = 22;
			this.label5.Text = "Default: ^";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(187,192);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(105,18);
			this.label4.TabIndex = 21;
			this.label4.Text = "Default: ~";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(187,172);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(105,18);
			this.label3.TabIndex = 20;
			this.label3.Text = "Default: |";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// textNote
			// 
			this.textNote.Location = new System.Drawing.Point(439,76);
			this.textNote.Multiline = true;
			this.textNote.Name = "textNote";
			this.textNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textNote.Size = new System.Drawing.Size(356,196);
			this.textNote.TabIndex = 17;
			// 
			// labelOutPortEx
			// 
			this.labelOutPortEx.Location = new System.Drawing.Point(298,154);
			this.labelOutPortEx.Name = "labelOutPortEx";
			this.labelOutPortEx.Size = new System.Drawing.Size(145,18);
			this.labelOutPortEx.TabIndex = 19;
			this.labelOutPortEx.Text = "Ex: 192.168.0.23:5846";
			this.labelOutPortEx.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// labelInPortEx
			// 
			this.labelInPortEx.Location = new System.Drawing.Point(298,133);
			this.labelInPortEx.Name = "labelInPortEx";
			this.labelInPortEx.Size = new System.Drawing.Size(145,18);
			this.labelInPortEx.TabIndex = 18;
			this.labelInPortEx.Text = "Ex: 5845";
			this.labelInPortEx.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// textInternalTypeVersion
			// 
			this.textInternalTypeVersion.Location = new System.Drawing.Point(156,112);
			this.textInternalTypeVersion.Name = "textInternalTypeVersion";
			this.textInternalTypeVersion.ReadOnly = true;
			this.textInternalTypeVersion.Size = new System.Drawing.Size(100,20);
			this.textInternalTypeVersion.TabIndex = 5;
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(10,112);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(145,18);
			this.label13.TabIndex = 0;
			this.label13.Text = "Internal Type Version";
			this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textInternalType
			// 
			this.textInternalType.Location = new System.Drawing.Point(156,92);
			this.textInternalType.Name = "textInternalType";
			this.textInternalType.ReadOnly = true;
			this.textInternalType.Size = new System.Drawing.Size(138,20);
			this.textInternalType.TabIndex = 4;
			// 
			// label14
			// 
			this.label14.Location = new System.Drawing.Point(10,92);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(145,18);
			this.label14.TabIndex = 0;
			this.label14.Text = "Internal Type";
			this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkEnabled
			// 
			this.checkEnabled.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkEnabled.Location = new System.Drawing.Point(24,30);
			this.checkEnabled.Name = "checkEnabled";
			this.checkEnabled.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.checkEnabled.Size = new System.Drawing.Size(145,18);
			this.checkEnabled.TabIndex = 1;
			this.checkEnabled.Text = "Enabled";
			// 
			// textEscChar
			// 
			this.textEscChar.Location = new System.Drawing.Point(156,252);
			this.textEscChar.Name = "textEscChar";
			this.textEscChar.Size = new System.Drawing.Size(27,20);
			this.textEscChar.TabIndex = 16;
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(10,252);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(145,18);
			this.label12.TabIndex = 0;
			this.label12.Text = "Escape Character";
			this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkInternal
			// 
			this.checkInternal.Enabled = false;
			this.checkInternal.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkInternal.Location = new System.Drawing.Point(24,12);
			this.checkInternal.Name = "checkInternal";
			this.checkInternal.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.checkInternal.Size = new System.Drawing.Size(145,18);
			this.checkInternal.TabIndex = 0;
			this.checkInternal.TabStop = false;
			this.checkInternal.Text = "Internal";
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(311,76);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(127,18);
			this.label11.TabIndex = 0;
			this.label11.Text = "Note";
			this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textSubcompSep
			// 
			this.textSubcompSep.Location = new System.Drawing.Point(156,232);
			this.textSubcompSep.Name = "textSubcompSep";
			this.textSubcompSep.Size = new System.Drawing.Size(27,20);
			this.textSubcompSep.TabIndex = 15;
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(10,232);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(145,18);
			this.label9.TabIndex = 0;
			this.label9.Text = "Subcomponent Separator";
			this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textRepSep
			// 
			this.textRepSep.Location = new System.Drawing.Point(156,192);
			this.textRepSep.Name = "textRepSep";
			this.textRepSep.Size = new System.Drawing.Size(27,20);
			this.textRepSep.TabIndex = 13;
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(10,192);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(145,18);
			this.label10.TabIndex = 0;
			this.label10.Text = "Repetition Separator";
			this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textCompSep
			// 
			this.textCompSep.Location = new System.Drawing.Point(156,212);
			this.textCompSep.Name = "textCompSep";
			this.textCompSep.Size = new System.Drawing.Size(27,20);
			this.textCompSep.TabIndex = 14;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(10,212);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(145,18);
			this.label8.TabIndex = 0;
			this.label8.Text = "Component Separator";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textFieldSep
			// 
			this.textFieldSep.Location = new System.Drawing.Point(156,172);
			this.textFieldSep.Name = "textFieldSep";
			this.textFieldSep.Size = new System.Drawing.Size(27,20);
			this.textFieldSep.TabIndex = 12;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(10,172);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(145,18);
			this.label7.TabIndex = 0;
			this.label7.Text = "Field Separator";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboModeTx
			// 
			this.comboModeTx.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboModeTx.Location = new System.Drawing.Point(156,71);
			this.comboModeTx.MaxDropDownItems = 100;
			this.comboModeTx.Name = "comboModeTx";
			this.comboModeTx.Size = new System.Drawing.Size(138,21);
			this.comboModeTx.TabIndex = 3;
			this.comboModeTx.SelectedIndexChanged += new System.EventHandler(this.comboModeTx_SelectedIndexChanged);
			// 
			// butBrowseOut
			// 
			this.butBrowseOut.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butBrowseOut.Autosize = true;
			this.butBrowseOut.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butBrowseOut.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butBrowseOut.CornerRadius = 4F;
			this.butBrowseOut.Location = new System.Drawing.Point(801,47);
			this.butBrowseOut.Name = "butBrowseOut";
			this.butBrowseOut.Size = new System.Drawing.Size(76,25);
			this.butBrowseOut.TabIndex = 11;
			this.butBrowseOut.Text = "&Browse";
			this.butBrowseOut.Click += new System.EventHandler(this.butBrowseOut_Click);
			// 
			// butBrowseIn
			// 
			this.butBrowseIn.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butBrowseIn.Autosize = true;
			this.butBrowseIn.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butBrowseIn.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butBrowseIn.CornerRadius = 4F;
			this.butBrowseIn.Location = new System.Drawing.Point(801,14);
			this.butBrowseIn.Name = "butBrowseIn";
			this.butBrowseIn.Size = new System.Drawing.Size(76,25);
			this.butBrowseIn.TabIndex = 9;
			this.butBrowseIn.Text = "&Browse";
			this.butBrowseIn.Click += new System.EventHandler(this.butBrowseIn_Click);
			// 
			// textOutPort
			// 
			this.textOutPort.Location = new System.Drawing.Point(156,152);
			this.textOutPort.Name = "textOutPort";
			this.textOutPort.Size = new System.Drawing.Size(138,20);
			this.textOutPort.TabIndex = 7;
			// 
			// labelOutPort
			// 
			this.labelOutPort.Location = new System.Drawing.Point(10,152);
			this.labelOutPort.Name = "labelOutPort";
			this.labelOutPort.Size = new System.Drawing.Size(145,18);
			this.labelOutPort.TabIndex = 0;
			this.labelOutPort.Text = "Outgoing IP:Port";
			this.labelOutPort.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textInPort
			// 
			this.textInPort.Location = new System.Drawing.Point(156,132);
			this.textInPort.Name = "textInPort";
			this.textInPort.Size = new System.Drawing.Size(70,20);
			this.textInPort.TabIndex = 6;
			// 
			// labelInPort
			// 
			this.labelInPort.Location = new System.Drawing.Point(10,132);
			this.labelInPort.Name = "labelInPort";
			this.labelInPort.Size = new System.Drawing.Size(145,18);
			this.labelInPort.TabIndex = 0;
			this.labelInPort.Text = "Incoming Port";
			this.labelInPort.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textOutPath
			// 
			this.textOutPath.Location = new System.Drawing.Point(439,50);
			this.textOutPath.Name = "textOutPath";
			this.textOutPath.Size = new System.Drawing.Size(356,20);
			this.textOutPath.TabIndex = 10;
			// 
			// labelOutPath
			// 
			this.labelOutPath.Location = new System.Drawing.Point(311,50);
			this.labelOutPath.Name = "labelOutPath";
			this.labelOutPath.Size = new System.Drawing.Size(127,18);
			this.labelOutPath.TabIndex = 0;
			this.labelOutPath.Text = "Outgoing Folder";
			this.labelOutPath.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textInPath
			// 
			this.textInPath.Location = new System.Drawing.Point(439,17);
			this.textInPath.Name = "textInPath";
			this.textInPath.Size = new System.Drawing.Size(356,20);
			this.textInPath.TabIndex = 8;
			// 
			// labelInPath
			// 
			this.labelInPath.Location = new System.Drawing.Point(311,17);
			this.labelInPath.Name = "labelInPath";
			this.labelInPath.Size = new System.Drawing.Size(127,18);
			this.labelInPath.TabIndex = 0;
			this.labelInPath.Text = "Incoming Folder";
			this.labelInPath.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(10,71);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(145,18);
			this.label2.TabIndex = 0;
			this.label2.Text = "ModeTx";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(10,50);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(145,18);
			this.label1.TabIndex = 0;
			this.label1.Text = "Description";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textDescription
			// 
			this.textDescription.Location = new System.Drawing.Point(156,50);
			this.textDescription.Name = "textDescription";
			this.textDescription.Size = new System.Drawing.Size(138,20);
			this.textDescription.TabIndex = 2;
			// 
			// gridMain
			// 
			this.gridMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridMain.HScrollVisible = false;
			this.gridMain.Location = new System.Drawing.Point(17,288);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.Size = new System.Drawing.Size(890,307);
			this.gridMain.TabIndex = 18;
			this.gridMain.Title = "Messages / Segments";
			this.gridMain.TranslationName = null;
			this.gridMain.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellDoubleClick);
			this.gridMain.CellClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellClick);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(746,601);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,24);
			this.butOK.TabIndex = 19;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.Location = new System.Drawing.Point(832,601);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,24);
			this.butCancel.TabIndex = 20;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// FormHL7DefEdit
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			this.ClientSize = new System.Drawing.Size(923,641);
			this.Controls.Add(this.gridMain);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormHL7DefEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "HL7 Def Edit";
			this.Load += new System.EventHandler(this.FormHL7DefEdit_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

		}
		#endregion

		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butCancel;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox textDescription;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textOutPort;
		private System.Windows.Forms.Label labelOutPort;
		private System.Windows.Forms.TextBox textInPort;
		private System.Windows.Forms.Label labelInPort;
		private System.Windows.Forms.TextBox textOutPath;
		private System.Windows.Forms.Label labelOutPath;
		private System.Windows.Forms.TextBox textInPath;
		private System.Windows.Forms.Label labelInPath;
		private UI.Button butBrowseIn;
		private UI.Button butBrowseOut;
		private System.Windows.Forms.ComboBox comboModeTx;
		private System.Windows.Forms.TextBox textSubcompSep;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.TextBox textRepSep;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.TextBox textCompSep;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox textFieldSep;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox textNote;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.CheckBox checkInternal;
		private System.Windows.Forms.TextBox textEscChar;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.CheckBox checkEnabled;
		private System.Windows.Forms.TextBox textInternalTypeVersion;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.TextBox textInternalType;
		private System.Windows.Forms.Label label14;
		private UI.ODGrid gridMain;
		private System.Windows.Forms.Label labelOutPortEx;
		private System.Windows.Forms.Label labelInPortEx;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
	}
}