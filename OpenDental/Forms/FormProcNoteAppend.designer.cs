namespace OpenDental{
	partial class FormProcNoteAppend {
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
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.signatureBoxWrapper = new OpenDental.UI.SignatureBoxWrapper();
			this.label15 = new System.Windows.Forms.Label();
			this.buttonUseAutoNote = new OpenDental.UI.Button();
			this.textUser = new System.Windows.Forms.TextBox();
			this.textNotes = new OpenDental.ODtextBox();
			this.label16 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.textAppended = new OpenDental.ODtextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(440, 447);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 24);
			this.butOK.TabIndex = 3;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.Location = new System.Drawing.Point(521, 447);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 24);
			this.butCancel.TabIndex = 2;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// signatureBoxWrapper
			// 
			this.signatureBoxWrapper.BackColor = System.Drawing.SystemColors.ControlDark;
			this.signatureBoxWrapper.LabelText = null;
			this.signatureBoxWrapper.Location = new System.Drawing.Point(112, 340);
			this.signatureBoxWrapper.Name = "signatureBoxWrapper";
			this.signatureBoxWrapper.Size = new System.Drawing.Size(364, 81);
			this.signatureBoxWrapper.TabIndex = 107;
			// 
			// label15
			// 
			this.label15.Location = new System.Drawing.Point(109, 319);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(136, 18);
			this.label15.TabIndex = 108;
			this.label15.Text = "Signature / Initials";
			this.label15.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// buttonUseAutoNote
			// 
			this.buttonUseAutoNote.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.buttonUseAutoNote.Autosize = true;
			this.buttonUseAutoNote.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.buttonUseAutoNote.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.buttonUseAutoNote.CornerRadius = 4F;
			this.buttonUseAutoNote.Location = new System.Drawing.Point(230, 17);
			this.buttonUseAutoNote.Name = "buttonUseAutoNote";
			this.buttonUseAutoNote.Size = new System.Drawing.Size(80, 22);
			this.buttonUseAutoNote.TabIndex = 113;
			this.buttonUseAutoNote.Text = "Auto Note";
			this.buttonUseAutoNote.Click += new System.EventHandler(this.buttonUseAutoNote_Click);
			// 
			// textUser
			// 
			this.textUser.Location = new System.Drawing.Point(112, 18);
			this.textUser.Name = "textUser";
			this.textUser.ReadOnly = true;
			this.textUser.Size = new System.Drawing.Size(116, 20);
			this.textUser.TabIndex = 112;
			// 
			// textNotes
			// 
			this.textNotes.AcceptsReturn = true;
			this.textNotes.AcceptsTab = true;
			this.textNotes.Location = new System.Drawing.Point(112, 38);
			this.textNotes.Multiline = true;
			this.textNotes.Name = "textNotes";
			this.textNotes.QuickPasteType = OpenDentBusiness.QuickPasteType.Procedure;
			this.textNotes.ReadOnly = true;
			this.textNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textNotes.Size = new System.Drawing.Size(450, 164);
			this.textNotes.TabIndex = 110;
			// 
			// label16
			// 
			this.label16.Location = new System.Drawing.Point(37, 19);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(73, 16);
			this.label16.TabIndex = 111;
			this.label16.Text = "User";
			this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(24, 42);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(86, 16);
			this.label7.TabIndex = 109;
			this.label7.Text = "Original Note";
			this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textAppended
			// 
			this.textAppended.AcceptsReturn = true;
			this.textAppended.AcceptsTab = true;
			this.textAppended.Location = new System.Drawing.Point(112, 208);
			this.textAppended.Multiline = true;
			this.textAppended.Name = "textAppended";
			this.textAppended.QuickPasteType = OpenDentBusiness.QuickPasteType.Procedure;
			this.textAppended.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textAppended.Size = new System.Drawing.Size(450, 108);
			this.textAppended.TabIndex = 115;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(7, 212);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(103, 16);
			this.label1.TabIndex = 114;
			this.label1.Text = "Appended Note";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// FormProcNoteAppend
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(608, 483);
			this.Controls.Add(this.textAppended);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.buttonUseAutoNote);
			this.Controls.Add(this.textUser);
			this.Controls.Add(this.textNotes);
			this.Controls.Add(this.label16);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label15);
			this.Controls.Add(this.signatureBoxWrapper);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Name = "FormProcNoteAppend";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Procedure Note Append";
			this.Load += new System.EventHandler(this.FormProcNoteAppend_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butCancel;
		private UI.SignatureBoxWrapper signatureBoxWrapper;
		private System.Windows.Forms.Label label15;
		private UI.Button buttonUseAutoNote;
		private System.Windows.Forms.TextBox textUser;
		private ODtextBox textNotes;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.Label label7;
		private ODtextBox textAppended;
		private System.Windows.Forms.Label label1;
	}
}