using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormMedicationEdit : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textGenericName;
		private System.Windows.Forms.TextBox textMedName;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		///<summary>Required designer variable.</summary>
		private System.ComponentModel.Container components = null;
		///<summary></summary>
		public bool IsNew;
		private OpenDental.ODtextBox textNotes;
		private OpenDental.UI.Button butDelete;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ComboBox comboPatients;
		///<summary></summary>
		public bool GenericOnly;
		private System.Windows.Forms.Label labelBrands;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label labelPatients;
		private System.Windows.Forms.ComboBox comboBrands;//does not allow changing to non generic.
		///<summary></summary>
		private string[] PatNames;
		///<summary></summary>
		private string[] Brands;
		public Medication MedicationCur;

		///<summary></summary>
		public FormMedicationEdit()
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMedicationEdit));
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.textGenericName = new System.Windows.Forms.TextBox();
			this.textMedName = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.textNotes = new OpenDental.ODtextBox();
			this.butDelete = new OpenDental.UI.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.comboPatients = new System.Windows.Forms.ComboBox();
			this.labelPatients = new System.Windows.Forms.Label();
			this.labelBrands = new System.Windows.Forms.Label();
			this.comboBrands = new System.Windows.Forms.ComboBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.groupBox1.SuspendLayout();
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
			this.butCancel.Location = new System.Drawing.Point(535,401);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,25);
			this.butCancel.TabIndex = 3;
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
			this.butOK.Location = new System.Drawing.Point(535,363);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,25);
			this.butOK.TabIndex = 2;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(20,45);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(127,17);
			this.label1.TabIndex = 2;
			this.label1.Text = "Generic name";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textGenericName
			// 
			this.textGenericName.Location = new System.Drawing.Point(148,42);
			this.textGenericName.Name = "textGenericName";
			this.textGenericName.ReadOnly = true;
			this.textGenericName.Size = new System.Drawing.Size(248,20);
			this.textGenericName.TabIndex = 3;
			// 
			// textMedName
			// 
			this.textMedName.Location = new System.Drawing.Point(148,17);
			this.textMedName.Name = "textMedName";
			this.textMedName.Size = new System.Drawing.Size(248,20);
			this.textMedName.TabIndex = 0;
			this.textMedName.TextChanged += new System.EventHandler(this.textMedName_TextChanged);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(20,20);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(127,17);
			this.label2.TabIndex = 6;
			this.label2.Text = "Drug name";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(21,66);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(127,17);
			this.label3.TabIndex = 8;
			this.label3.Text = "Notes";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textNotes
			// 
			this.textNotes.AcceptsReturn = true;
			this.textNotes.Location = new System.Drawing.Point(148,67);
			this.textNotes.Multiline = true;
			this.textNotes.Name = "textNotes";
			this.textNotes.QuickPasteType = OpenDentBusiness.QuickPasteType.MedicationEdit;
			this.textNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textNotes.Size = new System.Drawing.Size(459,194);
			this.textNotes.TabIndex = 9;
			// 
			// butDelete
			// 
			this.butDelete.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butDelete.Autosize = true;
			this.butDelete.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDelete.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDelete.CornerRadius = 4F;
			this.butDelete.Image = global::OpenDental.Properties.Resources.deleteX;
			this.butDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDelete.Location = new System.Drawing.Point(43,398);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(84,26);
			this.butDelete.TabIndex = 32;
			this.butDelete.Text = "&Delete";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(399,15);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(208,47);
			this.label4.TabIndex = 33;
			this.label4.Text = "Make sure you spell it right, because you can\'t ever change it";
			// 
			// comboPatients
			// 
			this.comboPatients.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboPatients.Location = new System.Drawing.Point(105,23);
			this.comboPatients.MaxDropDownItems = 30;
			this.comboPatients.Name = "comboPatients";
			this.comboPatients.Size = new System.Drawing.Size(299,21);
			this.comboPatients.TabIndex = 34;
			// 
			// labelPatients
			// 
			this.labelPatients.Location = new System.Drawing.Point(24,25);
			this.labelPatients.Name = "labelPatients";
			this.labelPatients.Size = new System.Drawing.Size(78,17);
			this.labelPatients.TabIndex = 35;
			this.labelPatients.Text = "Patients";
			this.labelPatients.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// labelBrands
			// 
			this.labelBrands.Location = new System.Drawing.Point(27,52);
			this.labelBrands.Name = "labelBrands";
			this.labelBrands.Size = new System.Drawing.Size(75,17);
			this.labelBrands.TabIndex = 37;
			this.labelBrands.Text = "Brands";
			this.labelBrands.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// comboBrands
			// 
			this.comboBrands.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBrands.Location = new System.Drawing.Point(105,50);
			this.comboBrands.MaxDropDownItems = 30;
			this.comboBrands.Name = "comboBrands";
			this.comboBrands.Size = new System.Drawing.Size(299,21);
			this.comboBrands.TabIndex = 36;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.labelPatients);
			this.groupBox1.Controls.Add(this.comboPatients);
			this.groupBox1.Controls.Add(this.labelBrands);
			this.groupBox1.Controls.Add(this.comboBrands);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(43,279);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(423,86);
			this.groupBox1.TabIndex = 38;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Dependencies";
			// 
			// FormMedicationEdit
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(653,444);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.textNotes);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.textMedName);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textGenericName);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormMedicationEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Medication";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.FormMedicationEdit_Closing);
			this.Load += new System.EventHandler(this.MedicationEdit_Load);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void MedicationEdit_Load(object sender, System.EventArgs e) {
			//Medications.Refresh() should have already been run.
			FillForm();
		}

		private void FillForm(){
			textMedName.Text=MedicationCur.MedName;
			if(!IsNew){
				textMedName.ReadOnly=true;
			}
			if(MedicationCur.MedicationNum==MedicationCur.GenericNum){
				textGenericName.Text=MedicationCur.MedName;
				textNotes.Text=MedicationCur.Notes;
				textNotes.ReadOnly=false;
				Brands=Medications.GetBrands(MedicationCur.MedicationNum);
				comboBrands.Items.Clear();
				comboBrands.Items.AddRange(Brands);
				if(Brands.Length>0){
					comboBrands.SelectedIndex=0;
				}
			}
			else{
				textGenericName.Text=Medications.GetMedication(MedicationCur.GenericNum).MedName;
				textNotes.Text=Medications.GetMedication(MedicationCur.GenericNum).Notes;
				textNotes.ReadOnly=true;
				Brands=new string[0];
				comboBrands.Visible=false;
				labelBrands.Visible=false;
			}
			PatNames=Medications.GetPats(MedicationCur.MedicationNum);
			comboPatients.Items.Clear();
			comboPatients.Items.AddRange(PatNames);
			if(PatNames.Length>0){
				comboPatients.SelectedIndex=0;
			}
		}

		private void textMedName_TextChanged(object sender, System.EventArgs e) {
			//this causes immediate display update with each keypress
			if(MedicationCur.MedicationNum==MedicationCur.GenericNum){
				textGenericName.Text=textMedName.Text;
			}
		}

		private void butDelete_Click(object sender, System.EventArgs e) {
			if(PatNames.Length>0){
				MessageBox.Show(Lan.g(this,"You can not delete a medication that is in use by patients."));
				return;
			}
			if(Brands.Length>0){
				MessageBox.Show(Lan.g(this,"You can not delete a medication that has brand names attached."));
				return;
			}
			if(Brands.Length>0){
				if(MessageBox.Show(Lan.g(this,"Delete this medication?"),"",MessageBoxButtons.OKCancel)
					!=DialogResult.OK){
					return;
				}
			}
			Medications.Delete(MedicationCur);
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			//generic num already handled
			MedicationCur.MedName=textMedName.Text;
			if(MedicationCur.MedicationNum==MedicationCur.GenericNum){
				MedicationCur.Notes=textNotes.Text;
			}
			else{
				MedicationCur.Notes="";
			}
			Medications.Update(MedicationCur);
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void FormMedicationEdit_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			if(DialogResult==DialogResult.OK)
				return;
			if(IsNew){
				Medications.Delete(MedicationCur);
			}
		}

		

		

		

		

		


	}
}





















