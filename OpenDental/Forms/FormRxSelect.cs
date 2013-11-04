using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDental.UI;
using OpenDentBusiness;

namespace OpenDental{
///<summary></summary>
	public class FormRxSelect : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private System.Windows.Forms.Label labelInstructions;
		private OpenDental.UI.Button butBlank;
		private System.ComponentModel.Container components = null;// Required designer variable.
		private Patient PatCur;
		private OpenDental.UI.ODGrid gridMain;
		private RxDef[] RxDefList;

		///<summary></summary>
		public FormRxSelect(Patient patCur){
			InitializeComponent();// Required for Windows Form Designer support
			PatCur=patCur;
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
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRxSelect));
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.labelInstructions = new System.Windows.Forms.Label();
			this.butBlank = new OpenDental.UI.Button();
			this.gridMain = new OpenDental.UI.ODGrid();
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
			this.butCancel.Location = new System.Drawing.Point(848, 636);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75, 26);
			this.butCancel.TabIndex = 3;
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
			this.butOK.Location = new System.Drawing.Point(756, 636);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75, 26);
			this.butOK.TabIndex = 2;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// labelInstructions
			// 
			this.labelInstructions.Location = new System.Drawing.Point(8, 8);
			this.labelInstructions.Name = "labelInstructions";
			this.labelInstructions.Size = new System.Drawing.Size(470, 16);
			this.labelInstructions.TabIndex = 15;
			this.labelInstructions.Text = "Please select a Prescription from the list or click Blank to start with a blank p" +
    "rescription.";
			// 
			// butBlank
			// 
			this.butBlank.AdjustImageLocation = new System.Drawing.Point(0, 0);
			this.butBlank.Autosize = true;
			this.butBlank.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butBlank.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butBlank.CornerRadius = 4F;
			this.butBlank.Location = new System.Drawing.Point(472, 5);
			this.butBlank.Name = "butBlank";
			this.butBlank.Size = new System.Drawing.Size(75, 26);
			this.butBlank.TabIndex = 0;
			this.butBlank.Text = "&Blank";
			this.butBlank.Click += new System.EventHandler(this.butBlank_Click);
			// 
			// gridMain
			// 
			this.gridMain.HScrollVisible = false;
			this.gridMain.Location = new System.Drawing.Point(12, 37);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.Size = new System.Drawing.Size(911, 586);
			this.gridMain.TabIndex = 16;
			this.gridMain.Title = "Prescriptions";
			this.gridMain.TranslationName = "TableRxSetup";
			this.gridMain.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellDoubleClick);
			// 
			// FormRxSelect
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(942, 674);
			this.Controls.Add(this.gridMain);
			this.Controls.Add(this.butBlank);
			this.Controls.Add(this.labelInstructions);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormRxSelect";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Select Prescription";
			this.Load += new System.EventHandler(this.FormRxSelect_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormRxSelect_Load(object sender, System.EventArgs e) {
			FillGrid();
			if(PrefC.GetBool(PrefName.ShowFeatureEhr)) {
				//We cannot allow blank prescription when using EHR, because each prescription created in this window must have an RxCui.
				//If we allowed blank, we would not know where to pull the RxCui from.
				butBlank.Visible=false;
				labelInstructions.Text="Please select a Prescription from the list.";
			}
		}

		private void FillGrid() {
			RxDefList=RxDefs.Refresh();
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TableRxSetup","Drug"),140);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableRxSetup","Controlled"),70,HorizontalAlignment.Center);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableRxSetup","Sig"),250);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableRxSetup","Disp"),70);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableRxSetup","Refills"),70);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableRxSetup","Notes"),300);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<RxDefList.Length;i++) {
				row=new ODGridRow();
				row.Cells.Add(RxDefList[i].Drug);
				if(RxDefList[i].IsControlled){
					row.Cells.Add("X");
				}
				else{
					row.Cells.Add("");
				}
				row.Cells.Add(RxDefList[i].Sig);
				row.Cells.Add(RxDefList[i].Disp);
				row.Cells.Add(RxDefList[i].Refills);
				row.Cells.Add(RxDefList[i].Notes);
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}

		private void gridMain_CellDoubleClick(object sender,OpenDental.UI.ODGridClickEventArgs e) {
			RxSelected();
		}

		private void RxSelected(){
			if(gridMain.GetSelectedIndex()==-1) {
				//this should never happen
				return;
			}
			RxDef RxDefCur=RxDefList[gridMain.GetSelectedIndex()];
			if(PrefC.GetBool(PrefName.ShowFeatureEhr) && RxDefCur.RxCui==0) {
				string strMsgText=Lan.g(this,"The selected prescription is missing an RxNorm")+".\r\n"
					+Lan.g(this,"Prescriptions without RxNorms cannot be exported in EHR documents")+".\r\n"
					+Lan.g(this,"Edit RxNorm in Rx Template?");
				if(MsgBox.Show(this,true,strMsgText)) {
					FormRxDefEdit form=new FormRxDefEdit(RxDefCur);
					form.ShowDialog();
					RxDefCur=RxDefs.GetOne(RxDefCur.RxDefNum);//FormRxDefEdit does not modify the RxDefCur object, so we must get the updated RxCui from the db.
				}
			}
			//Alert
			if(!RxAlertL.DisplayAlerts(PatCur.PatNum,RxDefCur.RxDefNum)){
				return;
			}
			//User OK with alert
			RxPat RxPatCur=new RxPat();
			RxPatCur.RxDate=DateTime.Today;
			RxPatCur.PatNum=PatCur.PatNum;
			RxPatCur.Drug=RxDefCur.Drug;
			RxPatCur.IsControlled=RxDefCur.IsControlled;
			RxPatCur.Sig=RxDefCur.Sig;
			RxPatCur.Disp=RxDefCur.Disp;
			RxPatCur.Refills=RxDefCur.Refills;
			if(PrefC.GetBool(PrefName.RxSendNewToQueue)) {
				RxPatCur.SendStatus=RxSendStatus.InElectQueue;
			}
			else {
				RxPatCur.SendStatus=RxSendStatus.Unsent;
			}
			//Notes not copied: we don't want these kinds of notes cluttering things
			FormRxEdit FormE=new FormRxEdit(PatCur,RxPatCur);
			FormE.IsNew=true;
			FormE.ShowDialog();
			if(FormE.DialogResult!=DialogResult.OK){
				return;
			}
			bool isProvOrder=false;
			if(Security.CurUser.ProvNum!=0) {//The user who is currently logged in is a provider.
				isProvOrder=true;
			}
			MedicationPats.InsertOrUpdateMedOrderForRx(RxPatCur,RxDefCur.RxCui,isProvOrder);//RxDefCur.RxCui can be 0.
			DialogResult=DialogResult.OK;
		}

		private void butBlank_Click(object sender, System.EventArgs e) {
			RxPat RxPatCur=new RxPat();
			RxPatCur.RxDate=DateTime.Today;
			RxPatCur.PatNum=PatCur.PatNum;
			if(PrefC.GetBool(PrefName.RxSendNewToQueue)) {
				RxPatCur.SendStatus=RxSendStatus.InElectQueue;
			}
			else {
				RxPatCur.SendStatus=RxSendStatus.Unsent;
			}
			FormRxEdit FormE=new FormRxEdit(PatCur,RxPatCur);
			FormE.IsNew=true;
			FormE.ShowDialog();
			if(FormE.DialogResult!=DialogResult.OK){
				return;
			}
			//We do not need to make a medical order here, because butBlank is not visible in EHR mode.
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(gridMain.GetSelectedIndex()==-1){
				MsgBox.Show(this,"Please select Rx first or click Blank");
				return;
			}
			RxSelected();
		}

		

	}
}
