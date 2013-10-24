using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	/// <summary></summary>
	public class FormDiseaseDefEdit : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private System.Windows.Forms.TextBox textName;
		/// <summary>Required designer variable.</summary>
		private System.ComponentModel.Container components = null;
		///<summary></summary>
		public bool IsNew;
		private OpenDental.UI.Button buttonDelete;
		private CheckBox checkIsHidden;
		private Label label1;
		private TextBox textICD9;
		private Label label2;
		private TextBox textSnomed;
		private Label label3;
		private UI.Button butSnomed;
		private UI.Button butIcd9;
		public DiseaseDef DiseaseDefCur;

		///<summary></summary>
		public FormDiseaseDefEdit(DiseaseDef diseaseDefCur)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			Lan.F(this);
			DiseaseDefCur=diseaseDefCur;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDiseaseDefEdit));
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.textName = new System.Windows.Forms.TextBox();
			this.buttonDelete = new OpenDental.UI.Button();
			this.checkIsHidden = new System.Windows.Forms.CheckBox();
			this.label1 = new System.Windows.Forms.Label();
			this.textICD9 = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textSnomed = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.butSnomed = new OpenDental.UI.Button();
			this.butIcd9 = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.Location = new System.Drawing.Point(372, 172);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 25);
			this.butCancel.TabIndex = 4;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(372, 136);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 25);
			this.butOK.TabIndex = 3;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// textName
			// 
			this.textName.Location = new System.Drawing.Point(116, 77);
			this.textName.Name = "textName";
			this.textName.Size = new System.Drawing.Size(308, 20);
			this.textName.TabIndex = 2;
			// 
			// buttonDelete
			// 
			this.buttonDelete.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.buttonDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonDelete.Autosize = true;
			this.buttonDelete.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.buttonDelete.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.buttonDelete.CornerRadius = 4F;
			this.buttonDelete.Image = global::OpenDental.Properties.Resources.deleteX;
			this.buttonDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonDelete.Location = new System.Drawing.Point(30, 172);
			this.buttonDelete.Name = "buttonDelete";
			this.buttonDelete.Size = new System.Drawing.Size(82, 25);
			this.buttonDelete.TabIndex = 5;
			this.buttonDelete.Text = "&Delete";
			this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
			// 
			// checkIsHidden
			// 
			this.checkIsHidden.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkIsHidden.Location = new System.Drawing.Point(26, 111);
			this.checkIsHidden.Name = "checkIsHidden";
			this.checkIsHidden.Size = new System.Drawing.Size(104, 24);
			this.checkIsHidden.TabIndex = 4;
			this.checkIsHidden.Text = "Hidden";
			this.checkIsHidden.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkIsHidden.UseVisualStyleBackColor = true;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(12, 75);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100, 23);
			this.label1.TabIndex = 5;
			this.label1.Text = "Description";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textICD9
			// 
			this.textICD9.Location = new System.Drawing.Point(116, 25);
			this.textICD9.Name = "textICD9";
			this.textICD9.ReadOnly = true;
			this.textICD9.Size = new System.Drawing.Size(273, 20);
			this.textICD9.TabIndex = 0;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(12, 23);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100, 23);
			this.label2.TabIndex = 5;
			this.label2.Text = "ICD-9 Code";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textSnomed
			// 
			this.textSnomed.Location = new System.Drawing.Point(116, 51);
			this.textSnomed.Name = "textSnomed";
			this.textSnomed.ReadOnly = true;
			this.textSnomed.Size = new System.Drawing.Size(273, 20);
			this.textSnomed.TabIndex = 1;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(12, 49);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(100, 23);
			this.label3.TabIndex = 5;
			this.label3.Text = "Snomed Code";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// butSnomed
			// 
			this.butSnomed.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butSnomed.Autosize = true;
			this.butSnomed.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSnomed.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSnomed.CornerRadius = 4F;
			this.butSnomed.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butSnomed.Location = new System.Drawing.Point(395, 49);
			this.butSnomed.Name = "butSnomed";
			this.butSnomed.Size = new System.Drawing.Size(29, 25);
			this.butSnomed.TabIndex = 8;
			this.butSnomed.Text = "...";
			this.butSnomed.Click += new System.EventHandler(this.butSnomed_Click);
			// 
			// butIcd9
			// 
			this.butIcd9.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butIcd9.Autosize = true;
			this.butIcd9.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butIcd9.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butIcd9.CornerRadius = 4F;
			this.butIcd9.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butIcd9.Location = new System.Drawing.Point(395, 22);
			this.butIcd9.Name = "butIcd9";
			this.butIcd9.Size = new System.Drawing.Size(29, 25);
			this.butIcd9.TabIndex = 9;
			this.butIcd9.Text = "...";
			this.butIcd9.Click += new System.EventHandler(this.butIcd9_Click);
			// 
			// FormDiseaseDefEdit
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(460, 208);
			this.Controls.Add(this.butIcd9);
			this.Controls.Add(this.butSnomed);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.checkIsHidden);
			this.Controls.Add(this.buttonDelete);
			this.Controls.Add(this.textSnomed);
			this.Controls.Add(this.textICD9);
			this.Controls.Add(this.textName);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormDiseaseDefEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Disease";
			this.Load += new System.EventHandler(this.FormDiseaseDefEdit_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormDiseaseDefEdit_Load(object sender, System.EventArgs e) {
			textName.Text=DiseaseDefCur.DiseaseName;
			string i9descript=ICD9s.GetCodeAndDescription(DiseaseDefCur.ICD9Code);
			if(i9descript=="") {
				textICD9.Text=DiseaseDefCur.ICD9Code;
			}
			else {
				textICD9.Text=i9descript;
			}
			string sdescript=Snomeds.GetCodeAndDescription(DiseaseDefCur.SnomedCode);
			if(sdescript=="") {
				textSnomed.Text=DiseaseDefCur.SnomedCode;
			}
			else {
				textSnomed.Text=sdescript;
			}
			checkIsHidden.Checked=DiseaseDefCur.IsHidden;
		}

		private void butSnomed_Click(object sender,EventArgs e) {
			FormSnomeds FormS=new FormSnomeds();
			FormS.IsSelectionMode=true;
			FormS.ShowDialog();
			if(FormS.DialogResult!=DialogResult.OK) {
				return;
			}
			if(DiseaseDefs.ContainsSnomed(FormS.SelectedSnomed.SnomedCode,DiseaseDefCur.DiseaseDefNum)) {//DiseaseDefNum could be zero
				MsgBox.Show(this,"Snomed code already exists in the problems list.");
				return;
			}
			DiseaseDefCur.SnomedCode=FormS.SelectedSnomed.SnomedCode;
			string sdescript=Snomeds.GetCodeAndDescription(FormS.SelectedSnomed.SnomedCode);
			if(sdescript=="") {
				textSnomed.Text=FormS.SelectedSnomed.SnomedCode;
			}
			else {
				textSnomed.Text=sdescript;
			}
		}

		private void butIcd9_Click(object sender,EventArgs e) {
			FormIcd9s FormI=new FormIcd9s();
			FormI.IsSelectionMode=true;
			FormI.ShowDialog();
			if(FormI.DialogResult!=DialogResult.OK) {
				return;
			}
			if(DiseaseDefs.ContainsICD9(FormI.SelectedIcd9.ICD9Code,DiseaseDefCur.DiseaseDefNum)) {
				MsgBox.Show(this,"ICD-9 code already exists in the problems list.");
				return;
			}
			DiseaseDefCur.ICD9Code=FormI.SelectedIcd9.ICD9Code;
			string i9descript=ICD9s.GetCodeAndDescription(FormI.SelectedIcd9.ICD9Code);
			if(i9descript=="") {
				textICD9.Text=FormI.SelectedIcd9.ICD9Code;
			}
			else {
				textICD9.Text=i9descript;
			}
		}

		private void buttonDelete_Click(object sender,EventArgs e) {
			if(IsNew){
				DialogResult=DialogResult.Cancel;
				return;
			}
			try{
				DiseaseDefs.Delete(DiseaseDefCur);
				SecurityLogs.MakeLogEntry(Permissions.ProblemEdit,0,DiseaseDefCur.DiseaseName+" deleted.");
				DialogResult=DialogResult.OK;
			}
			catch(ApplicationException ex){
				MessageBox.Show(ex.Message);
			}
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(textName.Text=="") {
				MsgBox.Show(this,"Not allowed to create a Disease Definition without a description.");
				return;
			}
			//Icd9Code and SnomedCode set on load or on return from code picker forms
			DiseaseDefCur.DiseaseName=textName.Text;
			DiseaseDefCur.IsHidden=checkIsHidden.Checked;
			if(IsNew){
				DiseaseDefs.Insert(DiseaseDefCur);
				SecurityLogs.MakeLogEntry(Permissions.ProblemEdit,0,DiseaseDefCur.DiseaseName+" added.");
			}
			else{
				DiseaseDefs.Update(DiseaseDefCur);
				SecurityLogs.MakeLogEntry(Permissions.ProblemEdit,0,DiseaseDefCur.DiseaseName);
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		
	

		


	}
}





















