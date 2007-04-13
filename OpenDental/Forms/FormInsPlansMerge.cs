/*=============================================================================================================
Open Dental GPL license Copyright (C) 2003  Jordan Sparks, DMD.  http://www.open-dent.com,  www.docsparks.com
See header in FormOpenDental.cs for complete text.  Redistributions must retain this text.
===============================================================================================================*/
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDental.UI;
using OpenDentBusiness;

namespace OpenDental{
///<summary></summary>
	public class FormInsPlansMerge:System.Windows.Forms.Form {
		private System.ComponentModel.Container components = null;// Required designer variable.
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		///<summary>After closing this form, if OK, then this will contain the Plan that the others will be merged into.</summary>
		public InsPlan PlanToMergeTo;
		private OpenDental.UI.ODGrid gridMain;
		///<summary>This list must be set before loading the form.  All of the PlanNums will be 0.</summary>
		public InsPlan[] ListAll;
		private Label label1;

		///<summary></summary>
		public FormInsPlansMerge(){
			InitializeComponent();// Required for Windows Form Designer support
			Lan.F(this);
		}

		///<summary></summary>
		protected override void Dispose( bool disposing ){
			if( disposing ){
				if(components != null){
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code

		private void InitializeComponent(){
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormInsPlansMerge));
			this.label1 = new System.Windows.Forms.Label();
			this.gridMain = new OpenDental.UI.ODGrid();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(12,9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(935,27);
			this.label1.TabIndex = 20;
			this.label1.Text = "Please select one plan from the list and click OK.  All the other plans will be m" +
    "ade identical to the plan you select.  You can double click on a plan to view pe" +
    "rcentages, etc.";
			// 
			// gridMain
			// 
			this.gridMain.HScrollVisible = false;
			this.gridMain.Location = new System.Drawing.Point(11,39);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.Size = new System.Drawing.Size(936,591);
			this.gridMain.TabIndex = 19;
			this.gridMain.Title = "Insurance Plans";
			this.gridMain.TranslationName = "TableTemplates";
			this.gridMain.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellDoubleClick);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(776,636);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(78,26);
			this.butOK.TabIndex = 4;
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
			this.butCancel.Location = new System.Drawing.Point(871,636);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(77,26);
			this.butCancel.TabIndex = 5;
			this.butCancel.Text = "Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// FormInsPlansMerge
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(962,669);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.gridMain);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FormInsPlansMerge";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Combine Insurance Plans";
			this.Load += new System.EventHandler(this.FormInsPlansMerge_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormInsPlansMerge_Load(object sender, System.EventArgs e) {
			FillGrid();
		}

		///<summary>Only gets run once.</summary>
		private void FillGrid(){
			Cursor=Cursors.WaitCursor;
			//ListAll: Set externally before loading.
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn("Employer",100);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Carrier",100);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Phone",82);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Address",100);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("City",80);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("ST",25);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Zip",50);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Group#",70);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Group Name",90);
			gridMain.Columns.Add(col);
			//col=new ODGridColumn("noE",35);
			//gridMain.Columns.Add(col);
			//col=new ODGridColumn("ElectID",45);
			//gridMain.Columns.Add(col);
			col=new ODGridColumn("Plans",40);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Plan Note",180);
			gridMain.Columns.Add(col);
			//TrojanID and PlanNote not shown
			gridMain.Rows.Clear();
			ODGridRow row;
			Carrier carrier;
			for(int i=0;i<ListAll.Length;i++) {
				row=new ODGridRow();
				row.Cells.Add(Employers.GetName(ListAll[i].EmployerNum));
				carrier=Carriers.GetCarrier(ListAll[i].CarrierNum);
				row.Cells.Add(carrier.CarrierName);
				row.Cells.Add(carrier.Phone);
				row.Cells.Add(carrier.Address);
				row.Cells.Add(carrier.City);
				row.Cells.Add(carrier.State);
				row.Cells.Add(carrier.Zip);
				row.Cells.Add(ListAll[i].GroupNum);
				row.Cells.Add(ListAll[i].GroupName);
				//if(carrier.NoSendElect)
				//	row.Cells.Add("X");
				//else
				//	row.Cells.Add("");
				//row.Cells.Add(carrier.ElectID);
				row.Cells.Add(ListAll[i].NumberPlans.ToString());
				row.Cells.Add(ListAll[i].PlanNote);
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
			gridMain.SetSelected(0,true);
			Cursor=Cursors.Default;
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e){
			InsPlan PlanCur=ListAll[e.Row].Copy();
			FormInsPlan FormIP=new FormInsPlan(PlanCur,null);
			FormIP.IsForAll=true;
			FormIP.IsReadOnly=true;
			FormIP.ShowDialog();//just for viewing
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			PlanToMergeTo=ListAll[gridMain.GetSelectedIndex()].Copy();
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		

		

		

		

		
	

		

		

		
		

		

		

	}
}


















