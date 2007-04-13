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
	public class FormMedications : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private System.Windows.Forms.ListBox listMeds;
		/// <summary>Required designer variable.</summary>
		private System.ComponentModel.Container components = null;
		///<summary></summary>
		public bool SelectMode;
		private System.Windows.Forms.Label label1;
		private OpenDental.UI.Button butAddGeneric;
		private OpenDental.UI.Button butAddBrand;
		///<summary>the number returned if using select mode.</summary>
		public int MedicationNum;

		///<summary></summary>
		public FormMedications()
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMedications));
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.listMeds = new System.Windows.Forms.ListBox();
			this.label1 = new System.Windows.Forms.Label();
			this.butAddGeneric = new OpenDental.UI.Button();
			this.butAddBrand = new OpenDental.UI.Button();
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
			this.butCancel.Location = new System.Drawing.Point(858,635);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,26);
			this.butCancel.TabIndex = 0;
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
			this.butOK.Location = new System.Drawing.Point(858,594);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 1;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// listMeds
			// 
			this.listMeds.Location = new System.Drawing.Point(8,25);
			this.listMeds.MultiColumn = true;
			this.listMeds.Name = "listMeds";
			this.listMeds.Size = new System.Drawing.Size(791,628);
			this.listMeds.TabIndex = 2;
			this.listMeds.DoubleClick += new System.EventHandler(this.listMeds_DoubleClick);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(9,5);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(506,17);
			this.label1.TabIndex = 4;
			this.label1.Text = "(medications marked with a * are missing notes)";
			// 
			// butAddGeneric
			// 
			this.butAddGeneric.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAddGeneric.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butAddGeneric.Autosize = true;
			this.butAddGeneric.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAddGeneric.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAddGeneric.CornerRadius = 4F;
			this.butAddGeneric.Image = global::OpenDental.Properties.Resources.Add;
			this.butAddGeneric.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAddGeneric.Location = new System.Drawing.Point(820,477);
			this.butAddGeneric.Name = "butAddGeneric";
			this.butAddGeneric.Size = new System.Drawing.Size(113,26);
			this.butAddGeneric.TabIndex = 33;
			this.butAddGeneric.Text = "Add Generic";
			this.butAddGeneric.Click += new System.EventHandler(this.butAddGeneric_Click);
			// 
			// butAddBrand
			// 
			this.butAddBrand.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAddBrand.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butAddBrand.Autosize = true;
			this.butAddBrand.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAddBrand.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAddBrand.CornerRadius = 4F;
			this.butAddBrand.Image = global::OpenDental.Properties.Resources.Add;
			this.butAddBrand.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAddBrand.Location = new System.Drawing.Point(820,517);
			this.butAddBrand.Name = "butAddBrand";
			this.butAddBrand.Size = new System.Drawing.Size(113,26);
			this.butAddBrand.TabIndex = 34;
			this.butAddBrand.Text = "Add Brand";
			this.butAddBrand.Click += new System.EventHandler(this.butAddBrand_Click);
			// 
			// FormMedications
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(941,671);
			this.Controls.Add(this.butAddBrand);
			this.Controls.Add(this.butAddGeneric);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.listMeds);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormMedications";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Medications";
			this.Load += new System.EventHandler(this.FormMedications_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormMedications_Load(object sender, System.EventArgs e) {
			//not refreshed in localdata
			FillMedList();
			//if(SelectGenericMode){
			//	this.Text=Lan.g(this,"Select Generic Medication");
				//butAdd.Visible=false;//visible, but it ONLY lets you add a generic
			//}
			if(SelectMode){
				this.Text=Lan.g(this,"Select Medication");
			}
		}

		private void FillMedList(){
			listMeds.Items.Clear();
			//if(SelectGenericMode){
			//	Medications.RefreshGeneric();
			//}
			//else{//SelectMode and standard
			Medications.Refresh();
			//}
			string s;
			for(int i=0;i<Medications.List.Length;i++){
				s=Medications.List[i].MedName;
				if(Medications.List[i].MedicationNum==Medications.List[i].GenericNum
					&& Medications.List[i].Notes=="")
				{
					s+=" *";
				}
				listMeds.Items.Add(s);
			}
		}

		private void butAddGeneric_Click(object sender, System.EventArgs e) {
			Medication MedicationCur=new Medication();
			Medications.Insert(MedicationCur);//so that we will have the primary key
			MedicationCur.GenericNum=MedicationCur.MedicationNum;
			FormMedicationEdit FormME=new FormMedicationEdit();
			FormME.MedicationCur=MedicationCur;
			FormME.IsNew=true;
			FormME.ShowDialog();
			FillMedList();
		}

		private void butAddBrand_Click(object sender, System.EventArgs e) {
			if(listMeds.SelectedIndex==-1){
				MessageBox.Show(Lan.g(this,"You must first highlight the generic medication from the list.  If it is not already on the list, then you must add it first."));
				return;
			}
			Medication selected=Medications.List[listMeds.SelectedIndex];
			if(selected.MedicationNum!=selected.GenericNum){
				MessageBox.Show(Lan.g(this,"The selected medication is not generic."));
				return;
			}
			Medication MedicationCur=new Medication();
			Medications.Insert(MedicationCur);//so that we will have the primary key
			MedicationCur.GenericNum=selected.MedicationNum;
			FormMedicationEdit FormME=new FormMedicationEdit();
			FormME.MedicationCur=MedicationCur;
			FormME.IsNew=true;
			FormME.ShowDialog();
			FillMedList();
		}

		private void listMeds_DoubleClick(object sender, System.EventArgs e) {
			if(SelectMode){
				MedicationNum=Medications.List[listMeds.SelectedIndex].MedicationNum;
				DialogResult=DialogResult.OK;
			}
			else{//normal mode from main menu
				//edit
				FormMedicationEdit FormME=new FormMedicationEdit();
				FormME.MedicationCur=Medications.List[listMeds.SelectedIndex];
				FormME.ShowDialog();
				FillMedList();
			}
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(SelectMode){
				if(listMeds.SelectedIndex==-1){
					MessageBox.Show(Lan.g(this,"Please select an item first."));
					return;
				}
				MedicationNum=Medications.List[listMeds.SelectedIndex].MedicationNum;
			}
			else{//normal mode from main menu
				//just close
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		

		

		


	}
}





















