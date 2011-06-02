namespace OpenDental{
	partial class FormAllergyDefEdit {
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
			this.butDelete = new OpenDental.UI.Button();
			this.labelDescription = new System.Windows.Forms.Label();
			this.textDescription = new System.Windows.Forms.TextBox();
			this.checkHidden = new System.Windows.Forms.CheckBox();
			this.butCancel = new OpenDental.UI.Button();
			this.textRxCui = new ODR.ValidNumber();
			this.label2 = new System.Windows.Forms.Label();
			this.comboSnomedAllergyType = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
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
			this.butOK.Location = new System.Drawing.Point(270,169);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,24);
			this.butOK.TabIndex = 3;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
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
			this.butDelete.Location = new System.Drawing.Point(15,169);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(75,24);
			this.butDelete.TabIndex = 2;
			this.butDelete.Text = "&Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// labelDescription
			// 
			this.labelDescription.Location = new System.Drawing.Point(50,31);
			this.labelDescription.Name = "labelDescription";
			this.labelDescription.Size = new System.Drawing.Size(85,20);
			this.labelDescription.TabIndex = 6;
			this.labelDescription.Text = "Description";
			this.labelDescription.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textDescription
			// 
			this.textDescription.Location = new System.Drawing.Point(141,31);
			this.textDescription.Name = "textDescription";
			this.textDescription.Size = new System.Drawing.Size(276,20);
			this.textDescription.TabIndex = 7;
			// 
			// checkHidden
			// 
			this.checkHidden.Location = new System.Drawing.Point(52,111);
			this.checkHidden.Name = "checkHidden";
			this.checkHidden.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.checkHidden.Size = new System.Drawing.Size(104,24);
			this.checkHidden.TabIndex = 8;
			this.checkHidden.Text = "Is Hidden";
			this.checkHidden.UseVisualStyleBackColor = true;
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.Location = new System.Drawing.Point(351,169);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,24);
			this.butCancel.TabIndex = 9;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// textRxCui
			// 
			this.textRxCui.Location = new System.Drawing.Point(141,87);
			this.textRxCui.MaxVal = 255;
			this.textRxCui.MinVal = 0;
			this.textRxCui.Name = "textRxCui";
			this.textRxCui.Size = new System.Drawing.Size(100,20);
			this.textRxCui.TabIndex = 21;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(35,86);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100,20);
			this.label2.TabIndex = 20;
			this.label2.Text = "RxNorm CUI";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboSnomedAllergyType
			// 
			this.comboSnomedAllergyType.FormattingEnabled = true;
			this.comboSnomedAllergyType.Location = new System.Drawing.Point(141,59);
			this.comboSnomedAllergyType.Name = "comboSnomedAllergyType";
			this.comboSnomedAllergyType.Size = new System.Drawing.Size(272,21);
			this.comboSnomedAllergyType.TabIndex = 19;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(5,58);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(130,20);
			this.label3.TabIndex = 18;
			this.label3.Text = "SNOMED Allergy Type";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// FormAllergyDefEdit
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(438,205);
			this.Controls.Add(this.textRxCui);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.comboSnomedAllergyType);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.checkHidden);
			this.Controls.Add(this.textDescription);
			this.Controls.Add(this.labelDescription);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butDelete);
			this.Name = "FormAllergyDefEdit";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Allergy Def Edit";
			this.Load += new System.EventHandler(this.FormAllergyEdit_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butDelete;
		private System.Windows.Forms.Label labelDescription;
		private System.Windows.Forms.TextBox textDescription;
		private System.Windows.Forms.CheckBox checkHidden;
		private UI.Button butCancel;
		private ODR.ValidNumber textRxCui;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox comboSnomedAllergyType;
		private System.Windows.Forms.Label label3;
	}
}