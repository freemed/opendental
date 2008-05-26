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
	public class FormLabCaseSelect : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		///<summary></summary>
		private OpenDental.UI.Button butAdd;
		public int PatNum;
		///<summary>This only has a value when DialogResult=OK.</summary>
		public int SelectedLabCaseNum;
		private OpenDental.UI.ODGrid gridMain;
		private Label label1;
		private List<LabCase> labCaseList;
		public bool IsPlanned;

		///<summary></summary>
		public FormLabCaseSelect()
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLabCaseSelect));
			this.butAdd = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.gridMain = new OpenDental.UI.ODGrid();
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// butAdd
			// 
			this.butAdd.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butAdd.Autosize = true;
			this.butAdd.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAdd.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAdd.CornerRadius = 4F;
			this.butAdd.Image = global::OpenDental.Properties.Resources.Add;
			this.butAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAdd.Location = new System.Drawing.Point(12,213);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(81,26);
			this.butAdd.TabIndex = 127;
			this.butAdd.Text = "New";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(407,213);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 8;
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
			this.butCancel.Location = new System.Drawing.Point(498,213);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,26);
			this.butCancel.TabIndex = 9;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// gridMain
			// 
			this.gridMain.HScrollVisible = false;
			this.gridMain.Location = new System.Drawing.Point(12,46);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.Size = new System.Drawing.Size(561,145);
			this.gridMain.TabIndex = 128;
			this.gridMain.Title = "Lab Cases";
			this.gridMain.TranslationName = "TableLabCaseSelect";
			this.gridMain.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellDoubleClick);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(11,9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(562,34);
			this.label1.TabIndex = 130;
			this.label1.Text = "Select a lab case from the list below or create a new one.  This list will not sh" +
    "ow lab cases that are already attached to other appointments.";
			// 
			// FormLabCaseSelect
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(599,257);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.gridMain);
			this.Controls.Add(this.butAdd);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormLabCaseSelect";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Select Lab Case";
			this.Load += new System.EventHandler(this.FormLabCaseSelect_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormLabCaseSelect_Load(object sender, System.EventArgs e) {
			FillGrid();
			if(labCaseList.Count>0){
				gridMain.SetSelected(0,true);
			}
		}

		private void FillGrid(){
			labCaseList=LabCases.GetForPat(PatNum,IsPlanned);
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TableLabCaseSelect","Date Created"),80);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableLabCaseSelect","Lab"),100);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableLabCaseSelect","Phone"),100);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableLabCaseSelect","Instructions"),200);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			DateTime dateCreated;
			Laboratory lab;
			for(int i=0;i<labCaseList.Count;i++){
				row=new ODGridRow();
				dateCreated=labCaseList[i].DateTimeCreated;
				row.Cells.Add(dateCreated.ToString("ddd")+" "+dateCreated.ToShortDateString()+" "+dateCreated.ToShortTimeString());
				lab=Laboratories.GetOne(labCaseList[i].LaboratoryNum);
				row.Cells.Add(lab.Description);
				row.Cells.Add(lab.Phone);
				row.Cells.Add(labCaseList[i].Instructions);
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}

		private void butAdd_Click(object sender,EventArgs e) {
			LabCase lab=new LabCase();
			lab.PatNum=PatNum;
			Patient pat=Patients.GetPat(PatNum);
			lab.ProvNum=Patients.GetProvNum(pat);
			lab.DateTimeCreated=MiscData.GetNowDateTime();
			FormLabCaseEdit FormL=new FormLabCaseEdit();
			FormL.CaseCur=lab;
			FormL.IsNew=true;
			FormL.ShowDialog();
			if(FormL.DialogResult!=DialogResult.OK){
				return;
			}
			SelectedLabCaseNum=FormL.CaseCur.LabCaseNum;
			DialogResult=DialogResult.OK;
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			SelectedLabCaseNum=labCaseList[e.Row].LabCaseNum;
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(gridMain.GetSelectedIndex()==-1){
				MsgBox.Show(this,"Please select an item first.");
				return;
			}
			SelectedLabCaseNum=labCaseList[gridMain.GetSelectedIndex()].LabCaseNum;
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		
	

		

		

		

		


	}
}





















