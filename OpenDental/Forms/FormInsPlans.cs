/*=============================================================================================================
Open Dental GPL license Copyright (C) 2003  Jordan Sparks, DMD.  http://www.open-dent.com,  www.docsparks.com
See header in FormOpenDental.cs for complete text.  Redistributions must retain this text.
===============================================================================================================*/
using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDental.UI;
using OpenDentBusiness;

namespace OpenDental{
///<summary></summary>
	public class FormInsPlans:System.Windows.Forms.Form {
		private System.ComponentModel.Container components = null;// Required designer variable.
		//private InsTemplates InsTemplates;
		private OpenDental.UI.Button butBlank;
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.RadioButton radioOrderCarrier;
		private System.Windows.Forms.RadioButton radioOrderEmp;
		//<summary>Set to true if we are only using the list to select a template to link to rather than creating a new plan. If this is true, then IsSelectMode will be ignored.</summary>
		//public bool IsLinkMode;
		///<summary>Set to true when selecting a plan for a patient and we want SelectedPlan to be filled upon closing.</summary>
		public bool IsSelectMode;
		///<summary>After closing this form, if IsSelectMode, then this will contain the selected Plan.</summary>
		public InsPlan SelectedPlan;
		private Label label1;
		private TextBox textEmployer;
		private TextBox textCarrier;
		private Label label2;
		private OpenDental.UI.ODGrid gridMain;
		//private InsPlan[] ListAll;
		///<summary>Supply a string here to start off the search with filtered employers.</summary>
		public string empText;
		private TextBox textGroupNum;
		private Label label3;
		private TextBox textGroupName;
		private Label label4;
		private OpenDental.UI.Button butMerge;
		///<summary>Supply a string here to start off the search with filtered carriers.</summary>
		public string carrierText;
		private TextBox textTrojanID;
		private Label labelTrojanID;
		private DataTable table;
		private bool trojan;

		///<summary></summary>
		public FormInsPlans(){
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormInsPlans));
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.radioOrderCarrier = new System.Windows.Forms.RadioButton();
			this.radioOrderEmp = new System.Windows.Forms.RadioButton();
			this.label1 = new System.Windows.Forms.Label();
			this.textEmployer = new System.Windows.Forms.TextBox();
			this.textCarrier = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textGroupNum = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.textGroupName = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.textTrojanID = new System.Windows.Forms.TextBox();
			this.labelTrojanID = new System.Windows.Forms.Label();
			this.butMerge = new OpenDental.UI.Button();
			this.gridMain = new OpenDental.UI.ODGrid();
			this.butOK = new OpenDental.UI.Button();
			this.butBlank = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.groupBox2.Controls.Add(this.radioOrderCarrier);
			this.groupBox2.Controls.Add(this.radioOrderEmp);
			this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox2.Location = new System.Drawing.Point(740,3);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(207,33);
			this.groupBox2.TabIndex = 2;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Order By";
			// 
			// radioOrderCarrier
			// 
			this.radioOrderCarrier.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioOrderCarrier.Location = new System.Drawing.Point(98,13);
			this.radioOrderCarrier.Name = "radioOrderCarrier";
			this.radioOrderCarrier.Size = new System.Drawing.Size(84,16);
			this.radioOrderCarrier.TabIndex = 1;
			this.radioOrderCarrier.Text = "Carrier";
			this.radioOrderCarrier.Click += new System.EventHandler(this.radioOrderCarrier_Click);
			// 
			// radioOrderEmp
			// 
			this.radioOrderEmp.Checked = true;
			this.radioOrderEmp.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioOrderEmp.Location = new System.Drawing.Point(9,13);
			this.radioOrderEmp.Name = "radioOrderEmp";
			this.radioOrderEmp.Size = new System.Drawing.Size(83,16);
			this.radioOrderEmp.TabIndex = 0;
			this.radioOrderEmp.TabStop = true;
			this.radioOrderEmp.Text = "Employer";
			this.radioOrderEmp.Click += new System.EventHandler(this.radioOrderEmp_Click);
			// 
			// label1
			// 
			this.label1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label1.Location = new System.Drawing.Point(7,7);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100,17);
			this.label1.TabIndex = 15;
			this.label1.Text = "Employer";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textEmployer
			// 
			this.textEmployer.Location = new System.Drawing.Point(113,4);
			this.textEmployer.Name = "textEmployer";
			this.textEmployer.Size = new System.Drawing.Size(140,20);
			this.textEmployer.TabIndex = 1;
			this.textEmployer.TextChanged += new System.EventHandler(this.textEmployer_TextChanged);
			// 
			// textCarrier
			// 
			this.textCarrier.Location = new System.Drawing.Point(113,25);
			this.textCarrier.Name = "textCarrier";
			this.textCarrier.Size = new System.Drawing.Size(140,20);
			this.textCarrier.TabIndex = 0;
			this.textCarrier.TextChanged += new System.EventHandler(this.textCarrier_TextChanged);
			// 
			// label2
			// 
			this.label2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label2.Location = new System.Drawing.Point(7,28);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100,17);
			this.label2.TabIndex = 17;
			this.label2.Text = "Carrier";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textGroupNum
			// 
			this.textGroupNum.Location = new System.Drawing.Point(372,25);
			this.textGroupNum.Name = "textGroupNum";
			this.textGroupNum.Size = new System.Drawing.Size(140,20);
			this.textGroupNum.TabIndex = 20;
			this.textGroupNum.TextChanged += new System.EventHandler(this.textGroupNum_TextChanged);
			// 
			// label3
			// 
			this.label3.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label3.Location = new System.Drawing.Point(266,28);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(100,17);
			this.label3.TabIndex = 23;
			this.label3.Text = "Group Num";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textGroupName
			// 
			this.textGroupName.Location = new System.Drawing.Point(372,4);
			this.textGroupName.Name = "textGroupName";
			this.textGroupName.Size = new System.Drawing.Size(140,20);
			this.textGroupName.TabIndex = 21;
			this.textGroupName.TextChanged += new System.EventHandler(this.textGroupName_TextChanged);
			// 
			// label4
			// 
			this.label4.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label4.Location = new System.Drawing.Point(266,7);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(100,17);
			this.label4.TabIndex = 22;
			this.label4.Text = "Group Name";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textTrojanID
			// 
			this.textTrojanID.Location = new System.Drawing.Point(616,4);
			this.textTrojanID.Name = "textTrojanID";
			this.textTrojanID.Size = new System.Drawing.Size(95,20);
			this.textTrojanID.TabIndex = 25;
			this.textTrojanID.TextChanged += new System.EventHandler(this.textTrojanID_TextChanged);
			// 
			// labelTrojanID
			// 
			this.labelTrojanID.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.labelTrojanID.Location = new System.Drawing.Point(536,7);
			this.labelTrojanID.Name = "labelTrojanID";
			this.labelTrojanID.Size = new System.Drawing.Size(74,17);
			this.labelTrojanID.TabIndex = 26;
			this.labelTrojanID.Text = "Trojan ID";
			this.labelTrojanID.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// butMerge
			// 
			this.butMerge.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butMerge.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butMerge.Autosize = true;
			this.butMerge.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butMerge.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butMerge.CornerRadius = 4F;
			this.butMerge.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butMerge.Location = new System.Drawing.Point(11,636);
			this.butMerge.Name = "butMerge";
			this.butMerge.Size = new System.Drawing.Size(74,26);
			this.butMerge.TabIndex = 24;
			this.butMerge.Text = "Combine";
			this.butMerge.Click += new System.EventHandler(this.butMerge_Click);
			// 
			// gridMain
			// 
			this.gridMain.HScrollVisible = true;
			this.gridMain.Location = new System.Drawing.Point(11,51);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.SelectionMode = OpenDental.UI.GridSelectionMode.MultiExtended;
			this.gridMain.Size = new System.Drawing.Size(936,579);
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
			// butBlank
			// 
			this.butBlank.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butBlank.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butBlank.Autosize = true;
			this.butBlank.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butBlank.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butBlank.CornerRadius = 4F;
			this.butBlank.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butBlank.Location = new System.Drawing.Point(427,636);
			this.butBlank.Name = "butBlank";
			this.butBlank.Size = new System.Drawing.Size(87,26);
			this.butBlank.TabIndex = 3;
			this.butBlank.Text = "Blank Plan";
			this.butBlank.Visible = false;
			this.butBlank.Click += new System.EventHandler(this.butBlank_Click);
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
			this.butCancel.Location = new System.Drawing.Point(871,636);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(77,26);
			this.butCancel.TabIndex = 5;
			this.butCancel.Text = "Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// FormInsPlans
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(962,669);
			this.Controls.Add(this.textTrojanID);
			this.Controls.Add(this.labelTrojanID);
			this.Controls.Add(this.butMerge);
			this.Controls.Add(this.textGroupNum);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.textGroupName);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.gridMain);
			this.Controls.Add(this.textCarrier);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textEmployer);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butBlank);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FormInsPlans";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Insurance Plans";
			this.Load += new System.EventHandler(this.FormInsTemplates_Load);
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormInsTemplates_Load(object sender, System.EventArgs e) {
			Program prog=Programs.GetCur("Trojan");
			if(prog!=null && prog.Enabled) {
				trojan=true;
			}
			else{
				labelTrojanID.Visible=false;
				textTrojanID.Visible=false;
			}
			textEmployer.Text=empText;
			textCarrier.Text=carrierText;
			FillGrid();
		}

		private void FillGrid(){
			Cursor=Cursors.WaitCursor;
			table=InsPlans.GetBigList(radioOrderEmp.Checked,textEmployer.Text,textCarrier.Text,
				textGroupName.Text,textGroupNum.Text,textTrojanID.Text);
			if(IsSelectMode){
				butBlank.Visible=true;
			}
			int selectedRow;//preserves the selected row.
			if(gridMain.SelectedIndices.Length==1){
				selectedRow=gridMain.SelectedIndices[0];
			}
			else{
				selectedRow=-1;
			}
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn("Employer",140);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Carrier",140);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Phone",82);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Address",120);
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
			col=new ODGridColumn("noE",35);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("ElectID",45);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Plans",40);
			gridMain.Columns.Add(col);
			if(trojan){
				col=new ODGridColumn("TrojanID",60);
				gridMain.Columns.Add(col);
			}
			//PlanNote not shown
			gridMain.Rows.Clear();
			ODGridRow row;
			//Carrier carrier;
			for(int i=0;i<table.Rows.Count;i++) {
				row=new ODGridRow();
				row.Cells.Add(table.Rows[i]["EmpName"].ToString());
				row.Cells.Add(table.Rows[i]["CarrierName"].ToString());
				row.Cells.Add(table.Rows[i]["Phone"].ToString());
				row.Cells.Add(table.Rows[i]["Address"].ToString());
				row.Cells.Add(table.Rows[i]["City"].ToString());
				row.Cells.Add(table.Rows[i]["State"].ToString());
				row.Cells.Add(table.Rows[i]["Zip"].ToString());
				row.Cells.Add(table.Rows[i]["GroupNum"].ToString());
				row.Cells.Add(table.Rows[i]["GroupName"].ToString());
				row.Cells.Add(table.Rows[i]["noSendElect"].ToString());
				row.Cells.Add(table.Rows[i]["ElectID"].ToString());
				row.Cells.Add(table.Rows[i]["plans"].ToString());
				if(trojan){
					row.Cells.Add(table.Rows[i]["TrojanID"].ToString());
				}
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
			gridMain.SetSelected(selectedRow,true);
			Cursor=Cursors.Default;
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e){
			InsPlan plan=InsPlans.GetPlan(PIn.PInt(table.Rows[e.Row]["PlanNum"].ToString()),null);
			if(IsSelectMode) {
				SelectedPlan=plan.Copy();
				DialogResult=DialogResult.OK;
				return;
			}
			FormInsPlan FormIP=new FormInsPlan(plan,null);
			FormIP.IsForAll=true;
			FormIP.ShowDialog();
			if(FormIP.DialogResult!=DialogResult.OK)
				return;
			FillGrid();
		}

		private void radioOrderEmp_Click(object sender, System.EventArgs e) {
			FillGrid();
		}

		private void radioOrderCarrier_Click(object sender, System.EventArgs e) {
			FillGrid();
		}

		private void textEmployer_TextChanged(object sender,EventArgs e) {
			FillGrid();
		}

		private void textCarrier_TextChanged(object sender,EventArgs e) {
			FillGrid();
		}

		private void textGroupName_TextChanged(object sender,EventArgs e) {
			FillGrid();
		}

		private void textGroupNum_TextChanged(object sender,EventArgs e) {
			FillGrid();
		}

		private void textTrojanID_TextChanged(object sender,EventArgs e) {
			FillGrid();
		}

		private void butMerge_Click(object sender,EventArgs e) {
			if(gridMain.SelectedIndices.Length<2) {
				MessageBox.Show(Lan.g(this,"Please select at least two items first."));
				return;
			}
			InsPlan[] listSelected=new InsPlan[gridMain.SelectedIndices.Length];
			for(int i=0;i<listSelected.Length;i++){
				listSelected[i]=InsPlans.GetPlan(PIn.PInt(table.Rows[gridMain.SelectedIndices[i]]["PlanNum"].ToString()),null);
			}
			FormInsPlansMerge FormI=new FormInsPlansMerge();
			FormI.ListAll=listSelected;
			FormI.ShowDialog();
			if(FormI.DialogResult!=DialogResult.OK){
				return;
			}
			//Do the merge.
			InsPlan planToMergeTo=FormI.PlanToMergeTo.Copy();
			List<Benefit> benList=Benefits.RefreshForAll(planToMergeTo);
			Cursor=Cursors.WaitCursor;
			List<int> planNums;
			for(int i=0;i<listSelected.Length;i++){//loop through each selected plan group
				//skip the planToMergeTo, because it's already correct
				if(planToMergeTo.PlanNum==listSelected[i].PlanNum){
					continue;
				}
				planNums=new List<int>(InsPlans.GetPlanNumsOfSamePlans(Employers.GetName(listSelected[i].EmployerNum),
					listSelected[i].GroupName,listSelected[i].GroupNum,listSelected[i].DivisionNo,
					Carriers.GetName(listSelected[i].CarrierNum),
					listSelected[i].IsMedical,listSelected[i].PlanNum,false));//remember that planNum=0
				//First plan info
				InsPlans.UpdateForLike(listSelected[i],planToMergeTo);
				//for(int j=0;j<planNums.Count;j++) {
					//InsPlans.ComputeEstimatesForPlan(planNums[j]);
					//Eliminated in 5.0 for speed.
				//}
				//then benefits
				Benefits.UpdateListForIdentical(new List<Benefit>(),benList,planNums);//there will always be changes
				//Then PlanNote.  This is much simpler than the usual synch, because user has seen all versions of note.
				InsPlans.UpdateNoteForPlans(planNums,planToMergeTo.PlanNote);
			}
			FillGrid();
			//highlight the merged plan
			for(int i=0;i<table.Rows.Count;i++){
				for(int j=0;j<listSelected.Length;j++){
					if(table.Rows[i]["PlanNum"].ToString()==listSelected[j].PlanNum.ToString()){
						gridMain.SetSelected(i,true);
					}
				}
			}
			Cursor=Cursors.Default;
		}

		private void butBlank_Click(object sender, System.EventArgs e) {
			SelectedPlan=new InsPlan();
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(IsSelectMode){
				if(gridMain.SelectedIndices.Length==0){
					MessageBox.Show(Lan.g(this,"Please select an item first."));
					return;
				}
				if(gridMain.SelectedIndices.Length>1) {
					MessageBox.Show(Lan.g(this,"Please select only one item first."));
					return;
				}
				SelectedPlan=InsPlans.GetPlan(PIn.PInt(table.Rows[gridMain.SelectedIndices[0]]["PlanNum"].ToString()),null).Copy();
				DialogResult=DialogResult.OK;
			}
			else{//just editing the list from the main menu
				DialogResult=DialogResult.OK;
			}
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		

		

		

		

		

		
	

		

		

		
		

		

		

	}
}


















