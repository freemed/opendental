namespace OpenDental{
	partial class FormPhoneOverrideEdit {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPhoneOverrideEdit));
			this.comboEmp = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.textExtension = new System.Windows.Forms.TextBox();
			this.checkIsAvailable = new System.Windows.Forms.CheckBox();
			this.label3 = new System.Windows.Forms.Label();
			this.textExplanation = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.butDelete = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// comboEmp
			// 
			this.comboEmp.FormattingEnabled = true;
			this.comboEmp.Location = new System.Drawing.Point(131,42);
			this.comboEmp.MaxDropDownItems = 50;
			this.comboEmp.Name = "comboEmp";
			this.comboEmp.Size = new System.Drawing.Size(121,21);
			this.comboEmp.TabIndex = 5;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(30,44);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100,18);
			this.label1.TabIndex = 6;
			this.label1.Text = "Employee";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(30,14);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100,18);
			this.label2.TabIndex = 7;
			this.label2.Text = "Extension";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textExtension
			// 
			this.textExtension.Location = new System.Drawing.Point(131,14);
			this.textExtension.Name = "textExtension";
			this.textExtension.Size = new System.Drawing.Size(46,20);
			this.textExtension.TabIndex = 8;
			// 
			// checkIsAvailable
			// 
			this.checkIsAvailable.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkIsAvailable.Location = new System.Drawing.Point(41,72);
			this.checkIsAvailable.Name = "checkIsAvailable";
			this.checkIsAvailable.Size = new System.Drawing.Size(104,20);
			this.checkIsAvailable.TabIndex = 9;
			this.checkIsAvailable.Text = "Available";
			this.checkIsAvailable.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkIsAvailable.UseVisualStyleBackColor = true;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(149,74);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(370,89);
			this.label3.TabIndex = 10;
			this.label3.Text = resources.GetString("label3.Text");
			// 
			// textExplanation
			// 
			this.textExplanation.AcceptsReturn = true;
			this.textExplanation.Location = new System.Drawing.Point(131,148);
			this.textExplanation.Multiline = true;
			this.textExplanation.Name = "textExplanation";
			this.textExplanation.Size = new System.Drawing.Size(382,78);
			this.textExplanation.TabIndex = 12;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(30,148);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(100,18);
			this.label4.TabIndex = 11;
			this.label4.Text = "Explanation";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
			this.butDelete.Location = new System.Drawing.Point(27,319);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(78,24);
			this.butDelete.TabIndex = 4;
			this.butDelete.Text = "Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(482,278);
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
			this.butCancel.Location = new System.Drawing.Point(482,319);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,24);
			this.butCancel.TabIndex = 2;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// FormPhoneOverrideEdit
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(582,370);
			this.Controls.Add(this.textExplanation);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.checkIsAvailable);
			this.Controls.Add(this.textExtension);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.comboEmp);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Name = "FormPhoneOverrideEdit";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Phone Override Edit";
			this.Load += new System.EventHandler(this.FormPhoneOverrideEdit_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butDelete;
		private System.Windows.Forms.ComboBox comboEmp;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textExtension;
		private System.Windows.Forms.CheckBox checkIsAvailable;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textExplanation;
		private System.Windows.Forms.Label label4;
	}
}