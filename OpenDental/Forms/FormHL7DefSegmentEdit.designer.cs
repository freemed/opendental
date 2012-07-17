namespace OpenDental{
	partial class FormHL7DefSegmentEdit {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormHL7DefSegmentEdit));
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.comboSegName = new System.Windows.Forms.ComboBox();
			this.textItemOrder = new System.Windows.Forms.TextBox();
			this.labelItemOrder = new System.Windows.Forms.Label();
			this.textNote = new System.Windows.Forms.TextBox();
			this.label12 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.gridMain = new OpenDental.UI.ODGrid();
			this.checkOptional = new System.Windows.Forms.CheckBox();
			this.checkRepeat = new System.Windows.Forms.CheckBox();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(548,494);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,24);
			this.butOK.TabIndex = 3;
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
			this.butCancel.Location = new System.Drawing.Point(634,494);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,24);
			this.butCancel.TabIndex = 2;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.checkRepeat);
			this.groupBox1.Controls.Add(this.checkOptional);
			this.groupBox1.Controls.Add(this.comboSegName);
			this.groupBox1.Controls.Add(this.textItemOrder);
			this.groupBox1.Controls.Add(this.labelItemOrder);
			this.groupBox1.Controls.Add(this.textNote);
			this.groupBox1.Controls.Add(this.label12);
			this.groupBox1.Controls.Add(this.label10);
			this.groupBox1.Location = new System.Drawing.Point(17,2);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(692,132);
			this.groupBox1.TabIndex = 4;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "HL7 Segment Settings";
			// 
			// comboSegName
			// 
			this.comboSegName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboSegName.Location = new System.Drawing.Point(136,21);
			this.comboSegName.MaxDropDownItems = 100;
			this.comboSegName.Name = "comboSegName";
			this.comboSegName.Size = new System.Drawing.Size(138,21);
			this.comboSegName.TabIndex = 1;
			// 
			// textItemOrder
			// 
			this.textItemOrder.Location = new System.Drawing.Point(136,45);
			this.textItemOrder.Name = "textItemOrder";
			this.textItemOrder.Size = new System.Drawing.Size(26,20);
			this.textItemOrder.TabIndex = 2;
			// 
			// labelItemOrder
			// 
			this.labelItemOrder.Location = new System.Drawing.Point(11,45);
			this.labelItemOrder.Name = "labelItemOrder";
			this.labelItemOrder.Size = new System.Drawing.Size(125,18);
			this.labelItemOrder.TabIndex = 0;
			this.labelItemOrder.Text = "Item Order";
			this.labelItemOrder.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textNote
			// 
			this.textNote.Location = new System.Drawing.Point(396,16);
			this.textNote.Multiline = true;
			this.textNote.Name = "textNote";
			this.textNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textNote.Size = new System.Drawing.Size(285,109);
			this.textNote.TabIndex = 5;
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(285,16);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(110,18);
			this.label12.TabIndex = 0;
			this.label12.Text = "Note";
			this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(11,20);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(125,18);
			this.label10.TabIndex = 0;
			this.label10.Text = "Segment Name";
			this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// gridMain
			// 
			this.gridMain.HScrollVisible = false;
			this.gridMain.Location = new System.Drawing.Point(17,140);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.Size = new System.Drawing.Size(692,348);
			this.gridMain.TabIndex = 7;
			this.gridMain.Title = "Fields";
			this.gridMain.TranslationName = null;
			this.gridMain.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellDoubleClick);
			// 
			// checkOptional
			// 
			this.checkOptional.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkOptional.Location = new System.Drawing.Point(14,84);
			this.checkOptional.Name = "checkOptional";
			this.checkOptional.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.checkOptional.Size = new System.Drawing.Size(137,18);
			this.checkOptional.TabIndex = 4;
			this.checkOptional.TabStop = false;
			this.checkOptional.Text = "Is Optional";
			// 
			// checkRepeat
			// 
			this.checkRepeat.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkRepeat.Location = new System.Drawing.Point(14,66);
			this.checkRepeat.Name = "checkRepeat";
			this.checkRepeat.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.checkRepeat.Size = new System.Drawing.Size(137,18);
			this.checkRepeat.TabIndex = 3;
			this.checkRepeat.TabStop = false;
			this.checkRepeat.Text = "Can Repeat";
			// 
			// FormHL7DefSegmentEdit
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			this.ClientSize = new System.Drawing.Size(725,534);
			this.Controls.Add(this.gridMain);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormHL7DefSegmentEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "HL7 Def Segment Edit";
			this.Load += new System.EventHandler(this.FormHL7DefSegmentEdit_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

		}
		#endregion

		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butCancel;
		private System.Windows.Forms.GroupBox groupBox1;
		private UI.ODGrid gridMain;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.TextBox textNote;
		private System.Windows.Forms.TextBox textItemOrder;
		private System.Windows.Forms.Label labelItemOrder;
		private System.Windows.Forms.ComboBox comboSegName;
		private System.Windows.Forms.CheckBox checkRepeat;
		private System.Windows.Forms.CheckBox checkOptional;
	}
}