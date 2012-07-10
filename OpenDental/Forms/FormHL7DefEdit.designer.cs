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
			this.textInternalTypeVersion = new System.Windows.Forms.TextBox();
			this.label13 = new System.Windows.Forms.Label();
			this.textInternalType = new System.Windows.Forms.TextBox();
			this.label14 = new System.Windows.Forms.Label();
			this.checkEnabled = new System.Windows.Forms.CheckBox();
			this.textEscChar = new System.Windows.Forms.TextBox();
			this.label12 = new System.Windows.Forms.Label();
			this.checkBoxIsInternal = new System.Windows.Forms.CheckBox();
			this.label11 = new System.Windows.Forms.Label();
			this.textNote = new System.Windows.Forms.TextBox();
			this.textSubcompSep = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.textRepSep = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.textCompSep = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.textFieldSep = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.comboModeTx = new System.Windows.Forms.ComboBox();
			this.comboModeTx.SelectedIndexChanged += new System.EventHandler(this.comboModeTx_SelectedIndexChanged);
			this.butBrowseOut = new OpenDental.UI.Button();
			this.butBrowseIn = new OpenDental.UI.Button();
			this.textOutPort = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.textInPort = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.textOutPath = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.textInPath = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.textDescription = new System.Windows.Forms.TextBox();
			this.fb = new System.Windows.Forms.FolderBrowserDialog();
			this.grid1 = new OpenDental.UI.ODGrid();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.textInternalTypeVersion);
			this.groupBox1.Controls.Add(this.label13);
			this.groupBox1.Controls.Add(this.textInternalType);
			this.groupBox1.Controls.Add(this.label14);
			this.groupBox1.Controls.Add(this.checkEnabled);
			this.groupBox1.Controls.Add(this.textEscChar);
			this.groupBox1.Controls.Add(this.label12);
			this.groupBox1.Controls.Add(this.checkBoxIsInternal);
			this.groupBox1.Controls.Add(this.label11);
			this.groupBox1.Controls.Add(this.textNote);
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
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.textInPort);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.textOutPath);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.textInPath);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.textDescription);
			this.groupBox1.Location = new System.Drawing.Point(24,13);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(677,398);
			this.groupBox1.TabIndex = 4;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "HL7 Settings";
			// 
			// textInternalTypeVersion
			// 
			this.textInternalTypeVersion.Location = new System.Drawing.Point(301,76);
			this.textInternalTypeVersion.Name = "textInternalTypeVersion";
			this.textInternalTypeVersion.ReadOnly = true;
			this.textInternalTypeVersion.Size = new System.Drawing.Size(167,20);
			this.textInternalTypeVersion.TabIndex = 5;
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(299,57);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(125,18);
			this.label13.TabIndex = 0;
			this.label13.Text = "Internal Type Version";
			this.label13.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// textInternalType
			// 
			this.textInternalType.Location = new System.Drawing.Point(16,76);
			this.textInternalType.Name = "textInternalType";
			this.textInternalType.ReadOnly = true;
			this.textInternalType.Size = new System.Drawing.Size(270,20);
			this.textInternalType.TabIndex = 4;
			// 
			// label14
			// 
			this.label14.Location = new System.Drawing.Point(14,57);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(100,18);
			this.label14.TabIndex = 0;
			this.label14.Text = "Internal Type";
			this.label14.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// checkEnabled
			// 
			this.checkEnabled.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.checkEnabled.Location = new System.Drawing.Point(567,31);
			this.checkEnabled.Name = "checkEnabled";
			this.checkEnabled.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.checkEnabled.Size = new System.Drawing.Size(72,24);
			this.checkEnabled.TabIndex = 3;
			this.checkEnabled.Text = "Enabled";
			this.checkEnabled.UseVisualStyleBackColor = true;
			// 
			// textEscChar
			// 
			this.textEscChar.Location = new System.Drawing.Point(16,370);
			this.textEscChar.Name = "textEscChar";
			this.textEscChar.Size = new System.Drawing.Size(73,20);
			this.textEscChar.TabIndex = 16;
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(14,351);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(140,18);
			this.label12.TabIndex = 0;
			this.label12.Text = "Escape Character";
			this.label12.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// checkBoxIsInternal
			// 
			this.checkBoxIsInternal.AutoCheck = false;
			this.checkBoxIsInternal.Enabled = false;
			this.checkBoxIsInternal.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.checkBoxIsInternal.Location = new System.Drawing.Point(483,31);
			this.checkBoxIsInternal.Name = "checkBoxIsInternal";
			this.checkBoxIsInternal.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.checkBoxIsInternal.Size = new System.Drawing.Size(72,24);
			this.checkBoxIsInternal.TabIndex = 0;
			this.checkBoxIsInternal.TabStop = false;
			this.checkBoxIsInternal.Text = "Is Internal";
			this.checkBoxIsInternal.UseVisualStyleBackColor = true;
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(177,183);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(100,18);
			this.label11.TabIndex = 0;
			this.label11.Text = "Note";
			this.label11.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// textNote
			// 
			this.textNote.Location = new System.Drawing.Point(179,202);
			this.textNote.Multiline = true;
			this.textNote.Name = "textNote";
			this.textNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textNote.Size = new System.Drawing.Size(492,188);
			this.textNote.TabIndex = 17;
			// 
			// textSubcompSep
			// 
			this.textSubcompSep.Location = new System.Drawing.Point(16,328);
			this.textSubcompSep.Name = "textSubcompSep";
			this.textSubcompSep.Size = new System.Drawing.Size(73,20);
			this.textSubcompSep.TabIndex = 15;
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(14,309);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(140,18);
			this.label9.TabIndex = 0;
			this.label9.Text = "Subcomponent Separator";
			this.label9.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// textRepSep
			// 
			this.textRepSep.Location = new System.Drawing.Point(16,244);
			this.textRepSep.Name = "textRepSep";
			this.textRepSep.Size = new System.Drawing.Size(73,20);
			this.textRepSep.TabIndex = 13;
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(14,225);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(140,18);
			this.label10.TabIndex = 0;
			this.label10.Text = "Repetition Separator";
			this.label10.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// textCompSep
			// 
			this.textCompSep.Location = new System.Drawing.Point(16,286);
			this.textCompSep.Name = "textCompSep";
			this.textCompSep.Size = new System.Drawing.Size(73,20);
			this.textCompSep.TabIndex = 14;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(14,267);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(140,18);
			this.label8.TabIndex = 0;
			this.label8.Text = "Component Separator";
			this.label8.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// textFieldSep
			// 
			this.textFieldSep.Location = new System.Drawing.Point(16,202);
			this.textFieldSep.Name = "textFieldSep";
			this.textFieldSep.Size = new System.Drawing.Size(73,20);
			this.textFieldSep.TabIndex = 12;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(14,183);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(140,18);
			this.label7.TabIndex = 0;
			this.label7.Text = "Field Separator";
			this.label7.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// comboModeTx
			// 
			this.comboModeTx.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboModeTx.Location = new System.Drawing.Point(301,34);
			this.comboModeTx.MaxDropDownItems = 100;
			this.comboModeTx.Name = "comboModeTx";
			this.comboModeTx.Size = new System.Drawing.Size(167,21);
			this.comboModeTx.TabIndex = 2;
			// 
			// butBrowseOut
			// 
			this.butBrowseOut.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butBrowseOut.Autosize = true;
			this.butBrowseOut.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butBrowseOut.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butBrowseOut.CornerRadius = 4F;
			this.butBrowseOut.Location = new System.Drawing.Point(595,157);
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
			this.butBrowseIn.Location = new System.Drawing.Point(595,115);
			this.butBrowseIn.Name = "butBrowseIn";
			this.butBrowseIn.Size = new System.Drawing.Size(76,25);
			this.butBrowseIn.TabIndex = 9;
			this.butBrowseIn.Text = "&Browse";
			this.butBrowseIn.Click += new System.EventHandler(this.butBrowseIn_Click);
			// 
			// textOutPort
			// 
			this.textOutPort.Location = new System.Drawing.Point(16,160);
			this.textOutPort.Name = "textOutPort";
			this.textOutPort.Size = new System.Drawing.Size(138,20);
			this.textOutPort.TabIndex = 7;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(14,141);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(100,18);
			this.label6.TabIndex = 0;
			this.label6.Text = "Outgoing Port";
			this.label6.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// textInPort
			// 
			this.textInPort.Location = new System.Drawing.Point(16,118);
			this.textInPort.Name = "textInPort";
			this.textInPort.Size = new System.Drawing.Size(138,20);
			this.textInPort.TabIndex = 6;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(14,99);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(87,18);
			this.label5.TabIndex = 0;
			this.label5.Text = "Incoming Port";
			this.label5.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// textOutPath
			// 
			this.textOutPath.Location = new System.Drawing.Point(179,160);
			this.textOutPath.Name = "textOutPath";
			this.textOutPath.Size = new System.Drawing.Size(410,20);
			this.textOutPath.TabIndex = 10;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(177,141);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(100,18);
			this.label4.TabIndex = 0;
			this.label4.Text = "Outgoing Folder";
			this.label4.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// textInPath
			// 
			this.textInPath.Location = new System.Drawing.Point(179,118);
			this.textInPath.Name = "textInPath";
			this.textInPath.Size = new System.Drawing.Size(410,20);
			this.textInPath.TabIndex = 8;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(177,99);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(100,18);
			this.label3.TabIndex = 0;
			this.label3.Text = "Incoming Folder";
			this.label3.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(299,15);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100,18);
			this.label2.TabIndex = 0;
			this.label2.Text = "ModeTx";
			this.label2.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(14,15);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100,18);
			this.label1.TabIndex = 0;
			this.label1.Text = "Description";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// textDescription
			// 
			this.textDescription.Location = new System.Drawing.Point(16,34);
			this.textDescription.Name = "textDescription";
			this.textDescription.Size = new System.Drawing.Size(270,20);
			this.textDescription.TabIndex = 1;
			// 
			// grid1
			// 
			this.grid1.HScrollVisible = false;
			this.grid1.Location = new System.Drawing.Point(24,417);
			this.grid1.Name = "grid1";
			this.grid1.ScrollValue = 0;
			this.grid1.Size = new System.Drawing.Size(676,156);
			this.grid1.TabIndex = 18;
			this.grid1.Title = "Messages / Segments";
			this.grid1.TranslationName = null;
			this.grid1.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.grid1_CellDoubleClick);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(538,593);
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
			this.butCancel.Location = new System.Drawing.Point(626,593);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,24);
			this.butCancel.TabIndex = 20;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// FormHL7DefEdit
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			this.ClientSize = new System.Drawing.Size(724,637);
			this.Controls.Add(this.grid1);
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
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox textInPort;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox textOutPath;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textInPath;
		private System.Windows.Forms.Label label3;
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
		private System.Windows.Forms.CheckBox checkBoxIsInternal;
		private System.Windows.Forms.TextBox textEscChar;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.CheckBox checkEnabled;
		private System.Windows.Forms.TextBox textInternalTypeVersion;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.TextBox textInternalType;
		private System.Windows.Forms.Label label14;
		private UI.ODGrid grid1;
	}
}