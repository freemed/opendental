namespace OpenDental{
	partial class FormEtrans270Edit {
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
			this.label6 = new System.Windows.Forms.Label();
			this.textNote = new System.Windows.Forms.TextBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.butShowResponse = new OpenDental.UI.Button();
			this.butShowRequest = new OpenDental.UI.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.butImport = new OpenDental.UI.Button();
			this.gridBen = new OpenDental.UI.ODGrid();
			this.gridDates = new OpenDental.UI.ODGrid();
			this.gridMain = new OpenDental.UI.ODGrid();
			this.butDelete = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(6,576);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(100,17);
			this.label6.TabIndex = 15;
			this.label6.Text = "Note";
			this.label6.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// textNote
			// 
			this.textNote.Location = new System.Drawing.Point(9,596);
			this.textNote.Multiline = true;
			this.textNote.Name = "textNote";
			this.textNote.Size = new System.Drawing.Size(355,40);
			this.textNote.TabIndex = 14;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.butShowResponse);
			this.groupBox2.Controls.Add(this.butShowRequest);
			this.groupBox2.Location = new System.Drawing.Point(705,12);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(168,49);
			this.groupBox2.TabIndex = 116;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Show Raw Message of...";
			// 
			// butShowResponse
			// 
			this.butShowResponse.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butShowResponse.Autosize = true;
			this.butShowResponse.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butShowResponse.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butShowResponse.CornerRadius = 4F;
			this.butShowResponse.Location = new System.Drawing.Point(87,19);
			this.butShowResponse.Name = "butShowResponse";
			this.butShowResponse.Size = new System.Drawing.Size(75,24);
			this.butShowResponse.TabIndex = 116;
			this.butShowResponse.Text = "Response";
			this.butShowResponse.Click += new System.EventHandler(this.butShowResponse_Click);
			// 
			// butShowRequest
			// 
			this.butShowRequest.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butShowRequest.Autosize = true;
			this.butShowRequest.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butShowRequest.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butShowRequest.CornerRadius = 4F;
			this.butShowRequest.Location = new System.Drawing.Point(6,19);
			this.butShowRequest.Name = "butShowRequest";
			this.butShowRequest.Size = new System.Drawing.Size(75,24);
			this.butShowRequest.TabIndex = 115;
			this.butShowRequest.Text = "Request";
			this.butShowRequest.Click += new System.EventHandler(this.butShowRequest_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(295,421);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(133,17);
			this.label1.TabIndex = 120;
			this.label1.Text = "Selected Benefits";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// butImport
			// 
			this.butImport.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butImport.Autosize = true;
			this.butImport.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butImport.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butImport.CornerRadius = 4F;
			this.butImport.Image = global::OpenDental.Properties.Resources.down;
			this.butImport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butImport.Location = new System.Drawing.Point(347,394);
			this.butImport.Name = "butImport";
			this.butImport.Size = new System.Drawing.Size(81,24);
			this.butImport.TabIndex = 119;
			this.butImport.Text = "Import";
			this.butImport.Click += new System.EventHandler(this.butImport_Click);
			// 
			// gridBen
			// 
			this.gridBen.HScrollVisible = false;
			this.gridBen.Location = new System.Drawing.Point(432,394);
			this.gridBen.Name = "gridBen";
			this.gridBen.ScrollValue = 0;
			this.gridBen.Size = new System.Drawing.Size(441,242);
			this.gridBen.TabIndex = 118;
			this.gridBen.Title = "Current Benefits";
			this.gridBen.TranslationName = "FormEtrans270Edit";
			this.gridBen.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridBen_CellDoubleClick);
			// 
			// gridDates
			// 
			this.gridDates.HScrollVisible = false;
			this.gridDates.Location = new System.Drawing.Point(9,12);
			this.gridDates.Name = "gridDates";
			this.gridDates.ScrollValue = 0;
			this.gridDates.Size = new System.Drawing.Size(407,119);
			this.gridDates.TabIndex = 117;
			this.gridDates.Title = "Dates";
			this.gridDates.TranslationName = "FormEtrans270Edit";
			// 
			// gridMain
			// 
			this.gridMain.HScrollVisible = false;
			this.gridMain.Location = new System.Drawing.Point(9,137);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.SelectionMode = OpenDental.UI.GridSelectionMode.MultiExtended;
			this.gridMain.Size = new System.Drawing.Size(864,254);
			this.gridMain.TabIndex = 114;
			this.gridMain.Title = "Response Benefit Information";
			this.gridMain.TranslationName = "FormEtrans270Edit";
			// 
			// butDelete
			// 
			this.butDelete.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butDelete.Autosize = true;
			this.butDelete.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDelete.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDelete.CornerRadius = 4F;
			this.butDelete.Image = global::OpenDental.Properties.Resources.deleteX;
			this.butDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDelete.Location = new System.Drawing.Point(9,641);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(81,24);
			this.butDelete.TabIndex = 113;
			this.butDelete.Text = "&Delete";
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(717,641);
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
			this.butCancel.Location = new System.Drawing.Point(798,641);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,24);
			this.butCancel.TabIndex = 2;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// FormEtrans270Edit
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(882,674);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butImport);
			this.Controls.Add(this.gridBen);
			this.Controls.Add(this.gridDates);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.gridMain);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.textNote);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Name = "FormEtrans270Edit";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Electronic Benefit Request";
			this.Load += new System.EventHandler(this.FormEtrans270Edit_Load);
			this.Shown += new System.EventHandler(this.FormEtrans270Edit_Shown);
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butCancel;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox textNote;
		private OpenDental.UI.Button butDelete;
		private OpenDental.UI.ODGrid gridMain;
		private OpenDental.UI.Button butShowRequest;
		private System.Windows.Forms.GroupBox groupBox2;
		private OpenDental.UI.Button butShowResponse;
		private OpenDental.UI.ODGrid gridDates;
		private OpenDental.UI.ODGrid gridBen;
		private OpenDental.UI.Button butImport;
		private System.Windows.Forms.Label label1;
	}
}