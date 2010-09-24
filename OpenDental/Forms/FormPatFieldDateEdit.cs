using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDental.UI;
using OpenDentBusiness;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormPatFieldDateEdit:System.Windows.Forms.Form {
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		///<summary></summary>
		public bool IsNew;
		private PatField Field;
		private Label labelName;
		private ODR.ValidDate textFieldDate;

		///<summary></summary>
		public FormPatFieldDateEdit(PatField field)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			Lan.F(this);
			Field=field;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPatFieldDateEdit));
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.labelName = new System.Windows.Forms.Label();
			this.textFieldDate = new ODR.ValidDate();
			this.SuspendLayout();
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.Location = new System.Drawing.Point(177,93);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,25);
			this.butCancel.TabIndex = 2;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(83,93);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,25);
			this.butOK.TabIndex = 1;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// labelName
			// 
			this.labelName.Location = new System.Drawing.Point(19,17);
			this.labelName.Name = "labelName";
			this.labelName.Size = new System.Drawing.Size(335,20);
			this.labelName.TabIndex = 3;
			this.labelName.Text = "Field Name";
			this.labelName.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// textFieldDate
			// 
			this.textFieldDate.Location = new System.Drawing.Point(22,40);
			this.textFieldDate.Name = "textFieldDate";
			this.textFieldDate.Size = new System.Drawing.Size(125,20);
			this.textFieldDate.TabIndex = 0;
			// 
			// FormPatFieldDateEdit
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(272,140);
			this.Controls.Add(this.textFieldDate);
			this.Controls.Add(this.labelName);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormPatFieldDateEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Patient Field Date";
			this.Load += new System.EventHandler(this.FormPatFieldDateEdit_Load);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormPatFieldDefEdit_FormClosing);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormPatFieldDateEdit_Load(object sender, System.EventArgs e) {
			labelName.Text=Field.FieldName;
			if(!IsNew) {
				textFieldDate.Text=Field.FieldValue.ToString();
			}
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(textFieldDate.errorProvider1.GetError(textFieldDate)!="") {
				MsgBox.Show(this,"Invalid date");
				return;
			}
			if(Field.FieldValue==""){//if blank, then delete
				if(IsNew) {
					DialogResult=DialogResult.Cancel;
					return;
				}
				PatFields.Delete(Field);
				DialogResult=DialogResult.OK;
				return;
			}
			Field.FieldValue=textFieldDate.Text;
			if(IsNew){
				PatFields.Insert(Field);
			}
			else{
				PatFields.Update(Field);
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void FormPatFieldDefEdit_FormClosing(object sender,FormClosingEventArgs e) {
			if(DialogResult==DialogResult.OK){
				return;
			}
			if(IsNew) {
				PatFields.Delete(Field);
			}
		}

	

		


	}
}





















