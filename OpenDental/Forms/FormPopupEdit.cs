using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	/// <summary></summary>
	public class FormPopupEdit : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private TextBox textDescription;
		private Label label1;
		private CheckBox checkIsDisabled;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private OpenDental.UI.Button butDelete;
		public Popup PopupCur;
		private ComboBox comboPopupLevel;
		private Label label2;
		private Label label3;
		private TextBox textPatient;
		private Patient Pat;

		///<summary></summary>
		public FormPopupEdit()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			Lan.F(this);
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPopupEdit));
			this.textDescription = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.checkIsDisabled = new System.Windows.Forms.CheckBox();
			this.comboPopupLevel = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.textPatient = new System.Windows.Forms.TextBox();
			this.butDelete = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// textDescription
			// 
			this.textDescription.Location = new System.Drawing.Point(157,93);
			this.textDescription.Multiline = true;
			this.textDescription.Name = "textDescription";
			this.textDescription.Size = new System.Drawing.Size(271,91);
			this.textDescription.TabIndex = 2;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(53,93);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(101,19);
			this.label1.TabIndex = 3;
			this.label1.Text = "Popup Message";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// checkIsDisabled
			// 
			this.checkIsDisabled.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkIsDisabled.Location = new System.Drawing.Point(14,70);
			this.checkIsDisabled.Name = "checkIsDisabled";
			this.checkIsDisabled.Size = new System.Drawing.Size(158,18);
			this.checkIsDisabled.TabIndex = 4;
			this.checkIsDisabled.Text = "Permanently Disabled";
			this.checkIsDisabled.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkIsDisabled.UseVisualStyleBackColor = true;
			// 
			// comboPopupLevel
			// 
			this.comboPopupLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboPopupLevel.FormattingEnabled = true;
			this.comboPopupLevel.Location = new System.Drawing.Point(157,43);
			this.comboPopupLevel.Name = "comboPopupLevel";
			this.comboPopupLevel.Size = new System.Drawing.Size(159,21);
			this.comboPopupLevel.TabIndex = 6;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(33,43);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(121,18);
			this.label2.TabIndex = 7;
			this.label2.Text = "Level";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(11,19);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(142,15);
			this.label3.TabIndex = 8;
			this.label3.Text = "Patient";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textPatient
			// 
			this.textPatient.Enabled = false;
			this.textPatient.Location = new System.Drawing.Point(157,17);
			this.textPatient.Name = "textPatient";
			this.textPatient.Size = new System.Drawing.Size(271,20);
			this.textPatient.TabIndex = 9;
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
			this.butDelete.Location = new System.Drawing.Point(28,205);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(81,26);
			this.butDelete.TabIndex = 5;
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
			this.butOK.Location = new System.Drawing.Point(302,205);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(77,26);
			this.butOK.TabIndex = 1;
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
			this.butCancel.Location = new System.Drawing.Point(385,205);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,26);
			this.butCancel.TabIndex = 0;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// FormPopupEdit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(492,248);
			this.Controls.Add(this.textPatient);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.comboPopupLevel);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.checkIsDisabled);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textDescription);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormPopupEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Popup";
			this.Load += new System.EventHandler(this.FormPopupEdit_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormPopupEdit_Load(object sender,EventArgs e) {
			Pat=Patients.GetPat(PopupCur.PatNum);
			textPatient.Text=Pat.GetNameLF();
			comboPopupLevel.Items.Add(Lan.g("enumEnumPopupFamily",Enum.GetNames(typeof(EnumPopupLevel))[0]));//Patient
			comboPopupLevel.Items.Add(Lan.g("enumEnumPopupFamily",Enum.GetNames(typeof(EnumPopupLevel))[1]));//Family
			if(Pat.SuperFamily!=0) {
				comboPopupLevel.Items.Add(Lan.g("enumEnumPopupFamily",Enum.GetNames(typeof(EnumPopupLevel))[2]));//SuperFamily
			}
			comboPopupLevel.SelectedIndex=(int)PopupCur.PopupLevel;
			checkIsDisabled.Checked=PopupCur.IsDisabled;
			textDescription.Text=PopupCur.Description;
		}

		private void butDelete_Click(object sender,EventArgs e) {
			if(PopupCur.IsNew){
				DialogResult=DialogResult.Cancel;
				return;
			}
			//don't ask user to make it go faster.
			Popups.DeleteObject(PopupCur);
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(textDescription.Text==""){
				MsgBox.Show(this,"Please enter text first");
				return;
			}
			//PatNum cannot be set
			PopupCur.PopupLevel=(EnumPopupLevel)comboPopupLevel.SelectedIndex;
			PopupCur.IsDisabled=checkIsDisabled.Checked;
			PopupCur.Description=textDescription.Text;
			if(PopupCur.IsNew) {
				Popups.Insert(PopupCur);
			}
			else {
				Popups.Update(PopupCur);
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		

		

		


	}
}





















