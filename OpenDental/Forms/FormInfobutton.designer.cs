namespace OpenDental{
	partial class FormInfobutton {
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
			this.groupBoxContext = new System.Windows.Forms.GroupBox();
			this.groupBoxProblem = new System.Windows.Forms.GroupBox();
			this.groupBoxMedication = new System.Windows.Forms.GroupBox();
			this.groupBoxLab = new System.Windows.Forms.GroupBox();
			this.comboRequestType = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.butPreview = new OpenDental.UI.Button();
			this.butSend = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.textMedName = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.groupBoxMedication.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBoxContext
			// 
			this.groupBoxContext.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBoxContext.Location = new System.Drawing.Point(12, 12);
			this.groupBoxContext.Name = "groupBoxContext";
			this.groupBoxContext.Size = new System.Drawing.Size(564, 189);
			this.groupBoxContext.TabIndex = 4;
			this.groupBoxContext.TabStop = false;
			this.groupBoxContext.Text = "Context";
			// 
			// groupBoxProblem
			// 
			this.groupBoxProblem.Location = new System.Drawing.Point(12, 229);
			this.groupBoxProblem.Name = "groupBoxProblem";
			this.groupBoxProblem.Size = new System.Drawing.Size(253, 129);
			this.groupBoxProblem.TabIndex = 5;
			this.groupBoxProblem.TabStop = false;
			this.groupBoxProblem.Text = "Problem";
			// 
			// groupBoxMedication
			// 
			this.groupBoxMedication.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.groupBoxMedication.Controls.Add(this.textMedName);
			this.groupBoxMedication.Controls.Add(this.label2);
			this.groupBoxMedication.Location = new System.Drawing.Point(12, 364);
			this.groupBoxMedication.Name = "groupBoxMedication";
			this.groupBoxMedication.Size = new System.Drawing.Size(253, 132);
			this.groupBoxMedication.TabIndex = 7;
			this.groupBoxMedication.TabStop = false;
			this.groupBoxMedication.Text = "Medication";
			// 
			// groupBoxLab
			// 
			this.groupBoxLab.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBoxLab.Location = new System.Drawing.Point(271, 229);
			this.groupBoxLab.Name = "groupBoxLab";
			this.groupBoxLab.Size = new System.Drawing.Size(305, 267);
			this.groupBoxLab.TabIndex = 7;
			this.groupBoxLab.TabStop = false;
			this.groupBoxLab.Text = "LabResult";
			// 
			// comboRequestType
			// 
			this.comboRequestType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboRequestType.FormattingEnabled = true;
			this.comboRequestType.Location = new System.Drawing.Point(145, 207);
			this.comboRequestType.Name = "comboRequestType";
			this.comboRequestType.Size = new System.Drawing.Size(208, 21);
			this.comboRequestType.TabIndex = 9;
			this.comboRequestType.SelectedIndexChanged += new System.EventHandler(this.comboRequestType_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(12, 210);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(133, 16);
			this.label1.TabIndex = 74;
			this.label1.Text = "Request Type";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// butPreview
			// 
			this.butPreview.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butPreview.Autosize = true;
			this.butPreview.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butPreview.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butPreview.CornerRadius = 4F;
			this.butPreview.Location = new System.Drawing.Point(12, 502);
			this.butPreview.Name = "butPreview";
			this.butPreview.Size = new System.Drawing.Size(75, 24);
			this.butPreview.TabIndex = 8;
			this.butPreview.Text = "&Preview";
			this.butPreview.Click += new System.EventHandler(this.butPreview_Click);
			// 
			// butSend
			// 
			this.butSend.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butSend.Autosize = true;
			this.butSend.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSend.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSend.CornerRadius = 4F;
			this.butSend.Location = new System.Drawing.Point(420, 502);
			this.butSend.Name = "butSend";
			this.butSend.Size = new System.Drawing.Size(75, 24);
			this.butSend.TabIndex = 3;
			this.butSend.Text = "&Send";
			this.butSend.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.Location = new System.Drawing.Point(501, 502);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 24);
			this.butCancel.TabIndex = 2;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// textMedName
			// 
			this.textMedName.Enabled = false;
			this.textMedName.Location = new System.Drawing.Point(110, 19);
			this.textMedName.Name = "textMedName";
			this.textMedName.Size = new System.Drawing.Size(137, 20);
			this.textMedName.TabIndex = 75;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(10, 23);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(94, 16);
			this.label2.TabIndex = 76;
			this.label2.Text = "Name";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// FormInfobutton
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(588, 538);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.comboRequestType);
			this.Controls.Add(this.butPreview);
			this.Controls.Add(this.groupBoxLab);
			this.Controls.Add(this.groupBoxMedication);
			this.Controls.Add(this.groupBoxProblem);
			this.Controls.Add(this.groupBoxContext);
			this.Controls.Add(this.butSend);
			this.Controls.Add(this.butCancel);
			this.Name = "FormInfobutton";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "InfoButton Portal";
			this.Load += new System.EventHandler(this.FormInfobutton_Load);
			this.groupBoxMedication.ResumeLayout(false);
			this.groupBoxMedication.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private OpenDental.UI.Button butSend;
		private OpenDental.UI.Button butCancel;
		private System.Windows.Forms.GroupBox groupBoxContext;
		private System.Windows.Forms.GroupBox groupBoxProblem;
		private System.Windows.Forms.GroupBox groupBoxMedication;
		private System.Windows.Forms.GroupBox groupBoxLab;
		private UI.Button butPreview;
		private System.Windows.Forms.ComboBox comboRequestType;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textMedName;
		private System.Windows.Forms.Label label2;
	}
}