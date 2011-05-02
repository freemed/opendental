using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.UI;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormMedications : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		/// <summary>Required designer variable.</summary>
		private System.ComponentModel.Container components = null;
		///<summary></summary>
		public bool IsSelectionMode;
		private OpenDental.UI.Button butAddGeneric;
		private OpenDental.UI.Button butAddBrand;
		private System.Windows.Forms.Button butLexiComp;
		private Label label2;
		private OpenDental.UI.ODGrid gridMain;
		private TextBox textSearch;
		private Label label1;
		///<summary>the number returned if using select mode.</summary>
		public long SelectedMedicationNum;
		private List<Medication> medList;

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
			this.butAddGeneric = new OpenDental.UI.Button();
			this.butAddBrand = new OpenDental.UI.Button();
			this.butLexiComp = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.gridMain = new OpenDental.UI.ODGrid();
			this.textSearch = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
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
			this.butCancel.Text = "Cancel";
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
			this.butOK.Text = "OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butAddGeneric
			// 
			this.butAddGeneric.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAddGeneric.Autosize = true;
			this.butAddGeneric.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAddGeneric.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAddGeneric.CornerRadius = 4F;
			this.butAddGeneric.Image = global::OpenDental.Properties.Resources.Add;
			this.butAddGeneric.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAddGeneric.Location = new System.Drawing.Point(9,41);
			this.butAddGeneric.Name = "butAddGeneric";
			this.butAddGeneric.Size = new System.Drawing.Size(113,26);
			this.butAddGeneric.TabIndex = 33;
			this.butAddGeneric.Text = "Add Generic";
			this.butAddGeneric.Click += new System.EventHandler(this.butAddGeneric_Click);
			// 
			// butAddBrand
			// 
			this.butAddBrand.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAddBrand.Autosize = true;
			this.butAddBrand.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAddBrand.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAddBrand.CornerRadius = 4F;
			this.butAddBrand.Image = global::OpenDental.Properties.Resources.Add;
			this.butAddBrand.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAddBrand.Location = new System.Drawing.Point(128,41);
			this.butAddBrand.Name = "butAddBrand";
			this.butAddBrand.Size = new System.Drawing.Size(113,26);
			this.butAddBrand.TabIndex = 34;
			this.butAddBrand.Text = "Add Brand";
			this.butAddBrand.Click += new System.EventHandler(this.butAddBrand_Click);
			// 
			// butLexiComp
			// 
			this.butLexiComp.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("butLexiComp.BackgroundImage")));
			this.butLexiComp.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
			this.butLexiComp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.butLexiComp.Location = new System.Drawing.Point(8,3);
			this.butLexiComp.Name = "butLexiComp";
			this.butLexiComp.Size = new System.Drawing.Size(175,34);
			this.butLexiComp.TabIndex = 35;
			this.butLexiComp.UseVisualStyleBackColor = true;
			this.butLexiComp.Click += new System.EventHandler(this.butLexiComp_Click);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(185,6);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(348,31);
			this.label2.TabIndex = 36;
			this.label2.Text = "Sign up with LexiComp now.  Get a 15% discount by using coupon code LCODODS at ch" +
    "eckout.  There is not yet any integration.";
			// 
			// gridMain
			// 
			this.gridMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridMain.HScrollVisible = false;
			this.gridMain.Location = new System.Drawing.Point(8,72);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.Size = new System.Drawing.Size(836,590);
			this.gridMain.TabIndex = 37;
			this.gridMain.Title = "Medications";
			this.gridMain.TranslationName = "FormMedications";
			this.gridMain.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellDoubleClick);
			// 
			// textSearch
			// 
			this.textSearch.Location = new System.Drawing.Point(443,46);
			this.textSearch.Name = "textSearch";
			this.textSearch.Size = new System.Drawing.Size(195,20);
			this.textSearch.TabIndex = 0;
			this.textSearch.TextChanged += new System.EventHandler(this.textSearch_TextChanged);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(315,49);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(127,17);
			this.label1.TabIndex = 39;
			this.label1.Text = "Search";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// FormMedications
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(941,671);
			this.Controls.Add(this.textSearch);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.gridMain);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.butLexiComp);
			this.Controls.Add(this.butAddBrand);
			this.Controls.Add(this.butAddGeneric);
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
			this.PerformLayout();

		}
		#endregion

		private void FormMedications_Load(object sender, System.EventArgs e) {
			//not refreshed in localdata
			FillGrid();
			//if(SelectGenericMode){
			//	this.Text=Lan.g(this,"Select Generic Medication");
				//butAdd.Visible=false;//visible, but it ONLY lets you add a generic
			//}
			if(IsSelectionMode){
				this.Text=Lan.g(this,"Select Medication");
			}
		}

		private void FillGrid(){
			medList=Medications.GetList(textSearch.Text);
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g(this,"Drug Name"),120);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Generic Name"),120);
			gridMain.Columns.Add(col);
			 col=new ODGridColumn(Lan.g(this,"Notes for Generic"),250);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<medList.Count;i++) {
				row=new ODGridRow();
				if(medList[i].MedicationNum==medList[i].GenericNum) {//isGeneric
					row.Cells.Add(medList[i].MedName);
					row.Cells.Add("");
				}
				else{
					row.Cells.Add(medList[i].MedName);
					row.Cells.Add(Medications.GetGenericName(medList[i].GenericNum));
				}
				row.Cells.Add(medList[i].Notes);
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}

		private void butLexiComp_Click(object sender,EventArgs e) {
			System.Diagnostics.Process.Start("http://www.lexi.com/ods/");
		}

		private void butAddGeneric_Click(object sender, System.EventArgs e) {
			Medication MedicationCur=new Medication();
			Medications.Insert(MedicationCur);//so that we will have the primary key
			MedicationCur.GenericNum=MedicationCur.MedicationNum;
			FormMedicationEdit FormME=new FormMedicationEdit();
			FormME.MedicationCur=MedicationCur;
			FormME.IsNew=true;
			FormME.ShowDialog();
			FillGrid();
		}

		private void butAddBrand_Click(object sender, System.EventArgs e) {
			if(gridMain.GetSelectedIndex()==-1){
				MessageBox.Show(Lan.g(this,"You must first highlight the generic medication from the list.  If it is not already on the list, then you must add it first."));
				return;
			}
			Medication selected=medList[gridMain.GetSelectedIndex()];
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
			FillGrid();
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			if(IsSelectionMode){
				SelectedMedicationNum=medList[e.Row].MedicationNum;
				DialogResult=DialogResult.OK;
			}
			else{//normal mode from main menu
				//edit
				FormMedicationEdit FormME=new FormMedicationEdit();
				FormME.MedicationCur=medList[e.Row];
				FormME.ShowDialog();
				FillGrid();
			}
		}

		private void textSearch_TextChanged(object sender,EventArgs e) {
			FillGrid();
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(IsSelectionMode){
				if(gridMain.GetSelectedIndex()==-1){
					MessageBox.Show(Lan.g(this,"Please select an item first."));
					return;
				}
				SelectedMedicationNum=medList[gridMain.GetSelectedIndex()].MedicationNum;
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





















