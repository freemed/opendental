using System;
using System.Data;
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
	public class FormFeesForIns : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private OpenDental.UI.ODGrid gridMain;
		private TextBox textCarrier;
		private Label label2;
		private Label label1;
		private ComboBox comboFeeSchedWithout;
		private Label label3;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private List<FeeSched> FeeSchedsForType;
		private ComboBox comboFeeSchedWith;
		private Label label4;
		private ComboBox comboFeeSchedNew;
		private Label label5;
		private TextBox textCarrierNot;
		private Label label6;
		private ListBox listType;
		private OpenDental.UI.Button butSelectAll;
		private DataTable table;

		///<summary></summary>
		public FormFeesForIns()
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormFeesForIns));
			this.textCarrier = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.comboFeeSchedWithout = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.comboFeeSchedWith = new System.Windows.Forms.ComboBox();
			this.label4 = new System.Windows.Forms.Label();
			this.comboFeeSchedNew = new System.Windows.Forms.ComboBox();
			this.label5 = new System.Windows.Forms.Label();
			this.textCarrierNot = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.listType = new System.Windows.Forms.ListBox();
			this.butSelectAll = new OpenDental.UI.Button();
			this.gridMain = new OpenDental.UI.ODGrid();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// textCarrier
			// 
			this.textCarrier.Location = new System.Drawing.Point(235,26);
			this.textCarrier.Name = "textCarrier";
			this.textCarrier.Size = new System.Drawing.Size(180,20);
			this.textCarrier.TabIndex = 0;
			this.textCarrier.TextChanged += new System.EventHandler(this.textCarrier_TextChanged);
			// 
			// label2
			// 
			this.label2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label2.Location = new System.Drawing.Point(136,29);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(93,17);
			this.label2.TabIndex = 19;
			this.label2.Text = "Carrier Like";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label1
			// 
			this.label1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label1.Location = new System.Drawing.Point(416,51);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(128,17);
			this.label1.TabIndex = 20;
			this.label1.Text = "Without Fee Schedule";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboFeeSchedWithout
			// 
			this.comboFeeSchedWithout.FormattingEnabled = true;
			this.comboFeeSchedWithout.Location = new System.Drawing.Point(550,47);
			this.comboFeeSchedWithout.MaxDropDownItems = 40;
			this.comboFeeSchedWithout.Name = "comboFeeSchedWithout";
			this.comboFeeSchedWithout.Size = new System.Drawing.Size(228,21);
			this.comboFeeSchedWithout.TabIndex = 1;
			this.comboFeeSchedWithout.SelectionChangeCommitted += new System.EventHandler(this.comboFeeSchedWithout_SelectionChangeCommitted);
			// 
			// label3
			// 
			this.label3.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label3.Location = new System.Drawing.Point(13,4);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(432,15);
			this.label3.TabIndex = 22;
			this.label3.Text = "You are searching for Insurance Plans that might have the wrong fee schedule atta" +
    "ched.";
			// 
			// comboFeeSchedWith
			// 
			this.comboFeeSchedWith.FormattingEnabled = true;
			this.comboFeeSchedWith.Location = new System.Drawing.Point(550,25);
			this.comboFeeSchedWith.MaxDropDownItems = 40;
			this.comboFeeSchedWith.Name = "comboFeeSchedWith";
			this.comboFeeSchedWith.Size = new System.Drawing.Size(228,21);
			this.comboFeeSchedWith.TabIndex = 23;
			this.comboFeeSchedWith.SelectionChangeCommitted += new System.EventHandler(this.comboFeeSchedWith_SelectionChangeCommitted);
			// 
			// label4
			// 
			this.label4.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label4.Location = new System.Drawing.Point(416,29);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(128,17);
			this.label4.TabIndex = 24;
			this.label4.Text = "With Fee Schedule";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboFeeSchedNew
			// 
			this.comboFeeSchedNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.comboFeeSchedNew.FormattingEnabled = true;
			this.comboFeeSchedNew.Location = new System.Drawing.Point(370,633);
			this.comboFeeSchedNew.MaxDropDownItems = 40;
			this.comboFeeSchedNew.Name = "comboFeeSchedNew";
			this.comboFeeSchedNew.Size = new System.Drawing.Size(228,21);
			this.comboFeeSchedNew.TabIndex = 25;
			// 
			// label5
			// 
			this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label5.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label5.Location = new System.Drawing.Point(215,637);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(149,17);
			this.label5.TabIndex = 26;
			this.label5.Text = "New Fee Schedule";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textCarrierNot
			// 
			this.textCarrierNot.Location = new System.Drawing.Point(235,47);
			this.textCarrierNot.Name = "textCarrierNot";
			this.textCarrierNot.Size = new System.Drawing.Size(180,20);
			this.textCarrierNot.TabIndex = 27;
			this.textCarrierNot.TextChanged += new System.EventHandler(this.textCarrierNot_TextChanged);
			// 
			// label6
			// 
			this.label6.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label6.Location = new System.Drawing.Point(136,50);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(93,17);
			this.label6.TabIndex = 28;
			this.label6.Text = "Carrier Not Like";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// listType
			// 
			this.listType.FormattingEnabled = true;
			this.listType.Location = new System.Drawing.Point(13,25);
			this.listType.Name = "listType";
			this.listType.Size = new System.Drawing.Size(120,43);
			this.listType.TabIndex = 29;
			this.listType.Click += new System.EventHandler(this.listType_Click);
			// 
			// butSelectAll
			// 
			this.butSelectAll.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butSelectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butSelectAll.Autosize = true;
			this.butSelectAll.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSelectAll.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSelectAll.CornerRadius = 4F;
			this.butSelectAll.Location = new System.Drawing.Point(12,630);
			this.butSelectAll.Name = "butSelectAll";
			this.butSelectAll.Size = new System.Drawing.Size(75,24);
			this.butSelectAll.TabIndex = 30;
			this.butSelectAll.Text = "Select All";
			this.butSelectAll.Click += new System.EventHandler(this.butSelectAll_Click);
			// 
			// gridMain
			// 
			this.gridMain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.gridMain.HScrollVisible = false;
			this.gridMain.Location = new System.Drawing.Point(13,72);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.SelectionMode = OpenDental.UI.GridSelectionMode.MultiExtended;
			this.gridMain.Size = new System.Drawing.Size(732,552);
			this.gridMain.TabIndex = 2;
			this.gridMain.Title = "Ins Plans that might need to be changed";
			this.gridMain.TranslationName = null;
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(614,631);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,24);
			this.butOK.TabIndex = 2;
			this.butOK.Text = "Change";
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
			this.butCancel.Location = new System.Drawing.Point(737,631);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,24);
			this.butCancel.TabIndex = 3;
			this.butCancel.Text = "&Close";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// FormFeesForIns
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(824,668);
			this.Controls.Add(this.butSelectAll);
			this.Controls.Add(this.listType);
			this.Controls.Add(this.textCarrierNot);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.comboFeeSchedNew);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.comboFeeSchedWith);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.comboFeeSchedWithout);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textCarrier);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.gridMain);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormFeesForIns";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Check Insurance Plan Fees";
			this.Load += new System.EventHandler(this.FormFeesForIns_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormFeesForIns_Load(object sender,EventArgs e) {
			for(int i=0;i<Enum.GetNames(typeof(FeeScheduleType)).Length;i++){
				listType.Items.Add(((FeeScheduleType)i).ToString());
			}
			listType.SelectedIndex=0;
			ResetSelections();
			FillGrid();
		}

		private void ResetSelections(){
			comboFeeSchedWithout.Items.Clear();
			comboFeeSchedWith.Items.Clear();
			comboFeeSchedNew.Items.Clear();
			comboFeeSchedWithout.Items.Add(Lan.g(this,"none"));
			comboFeeSchedWith.Items.Add(Lan.g(this,"none"));
			comboFeeSchedNew.Items.Add(Lan.g(this,"none"));
			comboFeeSchedWithout.SelectedIndex=0;
			comboFeeSchedWith.SelectedIndex=0;
			comboFeeSchedNew.SelectedIndex=0;
			FeeSchedsForType=FeeScheds.GetListForType((FeeScheduleType)(listType.SelectedIndex),false);
			for(int i=0;i<FeeSchedsForType.Count;i++){
				comboFeeSchedWithout.Items.Add(FeeSchedsForType[i].Description);
				comboFeeSchedWith.Items.Add(FeeSchedsForType[i].Description);
				comboFeeSchedNew.Items.Add(FeeSchedsForType[i].Description);
			}
		}

		private void listType_Click(object sender,EventArgs e) {
			ResetSelections();
			FillGrid();
		}
		
		private void textCarrier_TextChanged(object sender,EventArgs e) {
			FillGrid();
		}

		private void textCarrierNot_TextChanged(object sender,EventArgs e) {
			FillGrid();
		}

		private void comboFeeSchedWithout_SelectionChangeCommitted(object sender,EventArgs e) {
			FillGrid();
		}

		private void comboFeeSchedWith_SelectionChangeCommitted(object sender,EventArgs e) {
			FillGrid();
		}

		private void FillGrid(){
			int feeSchedWithout=0;
			int feeSchedWith=0;
			if(comboFeeSchedWithout.SelectedIndex!=0){
				feeSchedWithout=FeeSchedsForType[comboFeeSchedWithout.SelectedIndex-1].FeeSchedNum;
			}
			if(comboFeeSchedWith.SelectedIndex!=0) {
				feeSchedWith=FeeSchedsForType[comboFeeSchedWith.SelectedIndex-1].FeeSchedNum;
			}
			table=InsPlans.GetListFeeCheck(textCarrier.Text,textCarrierNot.Text,feeSchedWithout,feeSchedWith,
				(FeeScheduleType)(listType.SelectedIndex));
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn("Employer",170);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Carrier",170);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Group#",80);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Group Name",100);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Plan Type",65);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Fee Schedule",90);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Plans",40);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			string planType;
			for(int i=0;i<table.Rows.Count;i++) {
				row=new ODGridRow();
				row.Cells.Add(table.Rows[i]["EmpName"].ToString());
				row.Cells.Add(table.Rows[i]["CarrierName"].ToString());
				row.Cells.Add(table.Rows[i]["GroupNum"].ToString());
				row.Cells.Add(table.Rows[i]["GroupName"].ToString());
				planType=table.Rows[i]["PlanType"].ToString();
				if(planType=="p"){
					row.Cells.Add("PPO");
				}
				else if(planType=="f"){
					row.Cells.Add("FlatCopay");
				}
				else if(planType=="c"){
					row.Cells.Add("Capitation");
				}
				else{
					row.Cells.Add("Cat%");
				}
				row.Cells.Add(table.Rows[i]["FeeSchedName"].ToString());
				row.Cells.Add(table.Rows[i]["Plans"].ToString());
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
			gridMain.ScrollValue=0;
		}

		private void butSelectAll_Click(object sender,EventArgs e) {
			gridMain.SetSelected(true);
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(gridMain.Rows.Count==0){
				MsgBox.Show(this,"No rows to fix.");
				return;
			}
			//if(comboFeeSchedNew.SelectedIndex==0) {
			//	MsgBox.Show(this,"No rows to fix.");
			//	return;
			//}
			if(gridMain.SelectedIndices.Length==0){
				gridMain.SetSelected(true);
			}
			if(!MsgBox.Show(this,true,"Change the fee schedule for all selected plans to the new fee schedule?")){
				return;
			}
			Cursor=Cursors.WaitCursor;
			int employerNum;
			string carrierName;
			string groupNum;
			string groupName;
			int newFeeSchedNum=0;
			if(comboFeeSchedNew.SelectedIndex!=0){
				newFeeSchedNum=FeeSchedsForType[comboFeeSchedNew.SelectedIndex-1].FeeSchedNum;
			}
			int oldFeeSchedNum;
			int rowsChanged=0;
			for(int i=0;i<gridMain.SelectedIndices.Length;i++){
				oldFeeSchedNum=PIn.PInt(table.Rows[gridMain.SelectedIndices[i]]["feeSched"].ToString());
				if(oldFeeSchedNum==newFeeSchedNum){
					continue;
				}
				employerNum=PIn.PInt(table.Rows[gridMain.SelectedIndices[i]]["EmployerNum"].ToString());
				carrierName=table.Rows[gridMain.SelectedIndices[i]]["CarrierName"].ToString();
				groupNum=table.Rows[gridMain.SelectedIndices[i]]["GroupNum"].ToString();
				groupName=table.Rows[gridMain.SelectedIndices[i]]["GroupName"].ToString();
				rowsChanged+=InsPlans.SetFeeSched(employerNum,carrierName,groupNum,groupName,newFeeSchedNum,
					(FeeScheduleType)(listType.SelectedIndex));
			}
			FillGrid();
			Cursor=Cursors.Default;
			MessageBox.Show(Lan.g(this,"Plans changed: ")+rowsChanged.ToString());
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		

		

		

		

		

		


	}
}





















