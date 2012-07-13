namespace OpenDental{
	partial class FormHL7DefMessageEdit {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormHL7DefMessageEdit));
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.radioButtonOut = new System.Windows.Forms.RadioButton();
			this.radioButtonIn = new System.Windows.Forms.RadioButton();
			this.labelItemOrderDesc = new System.Windows.Forms.Label();
			this.comboEventType = new System.Windows.Forms.ComboBox();
			this.comboMsgType = new System.Windows.Forms.ComboBox();
			this.textItemOrder = new System.Windows.Forms.TextBox();
			this.labelItemOrder = new System.Windows.Forms.Label();
			this.textNote = new System.Windows.Forms.TextBox();
			this.label12 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.gridMain = new OpenDental.UI.ODGrid();
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
			this.groupBox1.Controls.Add(this.radioButtonOut);
			this.groupBox1.Controls.Add(this.radioButtonIn);
			this.groupBox1.Controls.Add(this.labelItemOrderDesc);
			this.groupBox1.Controls.Add(this.comboEventType);
			this.groupBox1.Controls.Add(this.comboMsgType);
			this.groupBox1.Controls.Add(this.textItemOrder);
			this.groupBox1.Controls.Add(this.labelItemOrder);
			this.groupBox1.Controls.Add(this.textNote);
			this.groupBox1.Controls.Add(this.label12);
			this.groupBox1.Controls.Add(this.label10);
			this.groupBox1.Controls.Add(this.label8);
			this.groupBox1.Location = new System.Drawing.Point(17,2);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(692,132);
			this.groupBox1.TabIndex = 4;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "HL7 Message Settings";
			// 
			// radioButtonOut
			// 
			this.radioButtonOut.Location = new System.Drawing.Point(50,85);
			this.radioButtonOut.Name = "radioButtonOut";
			this.radioButtonOut.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.radioButtonOut.Size = new System.Drawing.Size(100,18);
			this.radioButtonOut.TabIndex = 4;
			this.radioButtonOut.TabStop = true;
			this.radioButtonOut.Text = "Outgoing";
			this.radioButtonOut.UseVisualStyleBackColor = true;
			this.radioButtonOut.Click += new System.EventHandler(this.radioButtonOut_Selected);
			// 
			// radioButtonIn
			// 
			this.radioButtonIn.Location = new System.Drawing.Point(50,65);
			this.radioButtonIn.Name = "radioButtonIn";
			this.radioButtonIn.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.radioButtonIn.Size = new System.Drawing.Size(100,18);
			this.radioButtonIn.TabIndex = 3;
			this.radioButtonIn.TabStop = true;
			this.radioButtonIn.Text = "Incoming";
			this.radioButtonIn.UseVisualStyleBackColor = true;
			this.radioButtonIn.Click += new System.EventHandler(this.radioButtonIn_Selected);
			// 
			// labelItemOrderDesc
			// 
			this.labelItemOrderDesc.Location = new System.Drawing.Point(179,105);
			this.labelItemOrderDesc.Name = "labelItemOrderDesc";
			this.labelItemOrderDesc.Size = new System.Drawing.Size(125,18);
			this.labelItemOrderDesc.TabIndex = 0;
			this.labelItemOrderDesc.Text = "(for outgoing)";
			this.labelItemOrderDesc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// comboEventType
			// 
			this.comboEventType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboEventType.Location = new System.Drawing.Point(136,42);
			this.comboEventType.MaxDropDownItems = 100;
			this.comboEventType.Name = "comboEventType";
			this.comboEventType.Size = new System.Drawing.Size(138,21);
			this.comboEventType.TabIndex = 2;
			// 
			// comboMsgType
			// 
			this.comboMsgType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboMsgType.Location = new System.Drawing.Point(136,21);
			this.comboMsgType.MaxDropDownItems = 100;
			this.comboMsgType.Name = "comboMsgType";
			this.comboMsgType.Size = new System.Drawing.Size(138,21);
			this.comboMsgType.TabIndex = 1;
			// 
			// textItemOrder
			// 
			this.textItemOrder.Location = new System.Drawing.Point(136,105);
			this.textItemOrder.Name = "textItemOrder";
			this.textItemOrder.Size = new System.Drawing.Size(40,20);
			this.textItemOrder.TabIndex = 5;
			// 
			// labelItemOrder
			// 
			this.labelItemOrder.Location = new System.Drawing.Point(10,105);
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
			this.textNote.TabIndex = 6;
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
			this.label10.Location = new System.Drawing.Point(10,20);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(125,18);
			this.label10.TabIndex = 0;
			this.label10.Text = "Message Type";
			this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(10,42);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(125,18);
			this.label8.TabIndex = 0;
			this.label8.Text = "Event Type";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// gridMain
			// 
			this.gridMain.HScrollVisible = false;
			this.gridMain.Location = new System.Drawing.Point(17,140);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.Size = new System.Drawing.Size(692,348);
			this.gridMain.TabIndex = 7;
			this.gridMain.Title = "Segments";
			this.gridMain.TranslationName = null;
			this.gridMain.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellDoubleClick);
			// 
			// FormHL7DefMessageEdit
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
			this.Name = "FormHL7DefMessageEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "HL7 Def Message Edit";
			this.Load += new System.EventHandler(this.FormHL7DefMessageEdit_Load);
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
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox textNote;
		private System.Windows.Forms.TextBox textItemOrder;
		private System.Windows.Forms.Label labelItemOrder;
		private System.Windows.Forms.ComboBox comboMsgType;
		private System.Windows.Forms.ComboBox comboEventType;
		private System.Windows.Forms.Label labelItemOrderDesc;
		private System.Windows.Forms.RadioButton radioButtonOut;
		private System.Windows.Forms.RadioButton radioButtonIn;
	}
}