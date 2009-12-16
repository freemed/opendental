namespace OpenDental{
	partial class FormSheetFillEdit {
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
			this.textNote = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.panelMain = new System.Windows.Forms.Panel();
			this.checkErase = new System.Windows.Forms.CheckBox();
			this.butPDF = new OpenDental.UI.Button();
			this.butDelete = new OpenDental.UI.Button();
			this.textDateTime = new OpenDental.ValidDate();
			this.butPrint = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.textDescription = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// textNote
			// 
			this.textNote.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.textNote.Location = new System.Drawing.Point(580,107);
			this.textNote.Multiline = true;
			this.textNote.Name = "textNote";
			this.textNote.Size = new System.Drawing.Size(146,90);
			this.textNote.TabIndex = 6;
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label1.Location = new System.Drawing.Point(579,88);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(147,16);
			this.label1.TabIndex = 7;
			this.label1.Text = "Internal Note";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label3.Location = new System.Drawing.Point(579,6);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(84,16);
			this.label3.TabIndex = 76;
			this.label3.Text = "Date time";
			this.label3.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel1.AutoScroll = true;
			this.panel1.Controls.Add(this.panelMain);
			this.panel1.Location = new System.Drawing.Point(3,3);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(564,536);
			this.panel1.TabIndex = 78;
			// 
			// panelMain
			// 
			this.panelMain.BackColor = System.Drawing.SystemColors.Window;
			this.panelMain.Location = new System.Drawing.Point(0,0);
			this.panelMain.Name = "panelMain";
			this.panelMain.Size = new System.Drawing.Size(549,513);
			this.panelMain.TabIndex = 0;
			// 
			// checkErase
			// 
			this.checkErase.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.checkErase.Location = new System.Drawing.Point(645,272);
			this.checkErase.Name = "checkErase";
			this.checkErase.Size = new System.Drawing.Size(89,20);
			this.checkErase.TabIndex = 81;
			this.checkErase.Text = "Eraser Tool";
			this.checkErase.UseVisualStyleBackColor = true;
			this.checkErase.Click += new System.EventHandler(this.checkErase_Click);
			// 
			// butPDF
			// 
			this.butPDF.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butPDF.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butPDF.Autosize = true;
			this.butPDF.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPDF.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPDF.CornerRadius = 4F;
			this.butPDF.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butPDF.Location = new System.Drawing.Point(645,376);
			this.butPDF.Name = "butPDF";
			this.butPDF.Size = new System.Drawing.Size(81,24);
			this.butPDF.TabIndex = 83;
			this.butPDF.Text = "Create PDF";
			this.butPDF.Click += new System.EventHandler(this.butPDF_Click);
			// 
			// butDelete
			// 
			this.butDelete.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butDelete.Autosize = true;
			this.butDelete.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDelete.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDelete.CornerRadius = 4F;
			this.butDelete.Image = global::OpenDental.Properties.Resources.deleteX;
			this.butDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDelete.Location = new System.Drawing.Point(645,429);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(81,24);
			this.butDelete.TabIndex = 79;
			this.butDelete.Text = "Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// textDateTime
			// 
			this.textDateTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.textDateTime.Location = new System.Drawing.Point(580,24);
			this.textDateTime.Name = "textDateTime";
			this.textDateTime.Size = new System.Drawing.Size(146,20);
			this.textDateTime.TabIndex = 77;
			// 
			// butPrint
			// 
			this.butPrint.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butPrint.Autosize = true;
			this.butPrint.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPrint.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPrint.CornerRadius = 4F;
			this.butPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butPrint.Location = new System.Drawing.Point(645,325);
			this.butPrint.Name = "butPrint";
			this.butPrint.Size = new System.Drawing.Size(81,24);
			this.butPrint.TabIndex = 80;
			this.butPrint.Text = "Print/Email";
			this.butPrint.Click += new System.EventHandler(this.butPrint_Click);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(645,485);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(81,24);
			this.butOK.TabIndex = 3;
			this.butOK.Text = "OK";
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
			this.butCancel.Location = new System.Drawing.Point(645,515);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(81,24);
			this.butCancel.TabIndex = 2;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// textDescription
			// 
			this.textDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.textDescription.Location = new System.Drawing.Point(580,66);
			this.textDescription.Name = "textDescription";
			this.textDescription.Size = new System.Drawing.Size(146,20);
			this.textDescription.TabIndex = 84;
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label2.Location = new System.Drawing.Point(579,47);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(147,16);
			this.label2.TabIndex = 85;
			this.label2.Text = "Description";
			this.label2.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// FormSheetFillEdit
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(737,551);
			this.Controls.Add(this.textDescription);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.butPDF);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.checkErase);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.textDateTime);
			this.Controls.Add(this.butPrint);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.textNote);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Name = "FormSheetFillEdit";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Fill Sheet";
			this.Load += new System.EventHandler(this.FormSheetFillEdit_Load);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butCancel;
		private System.Windows.Forms.TextBox textNote;
		private System.Windows.Forms.Label label1;
		private ValidDate textDateTime;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panelMain;
		private OpenDental.UI.Button butDelete;
		private OpenDental.UI.Button butPrint;
		private System.Windows.Forms.CheckBox checkErase;
		private OpenDental.UI.Button butPDF;
		private System.Windows.Forms.TextBox textDescription;
		private System.Windows.Forms.Label label2;
	}
}