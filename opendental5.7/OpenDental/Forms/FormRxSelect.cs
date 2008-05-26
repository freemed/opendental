using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDental.UI;
using OpenDentBusiness;

namespace OpenDental{
///<summary></summary>
	public class FormRxSelect : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private System.Windows.Forms.Label label1;
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
			this.label1 = new System.Windows.Forms.Label();
			this.butBlank = new OpenDental.UI.Button();
			this.gridMain = new OpenDental.UI.ODGrid();
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
			this.butCancel.Location = new System.Drawing.Point(848,636);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,26);
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
			this.butOK.Location = new System.Drawing.Point(756,636);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 2;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8,8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(470,16);
			this.label1.TabIndex = 15;
			this.label1.Text = "Please select a Prescription from the list or click Blank to start with a blank p" +
    "rescription.";
			// 
			// butBlank
			// 
			this.butBlank.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butBlank.Autosize = true;
			this.butBlank.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butBlank.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butBlank.CornerRadius = 4F;
			this.butBlank.Location = new System.Drawing.Point(472,5);
			this.butBlank.Name = "butBlank";
			this.butBlank.Size = new System.Drawing.Size(75,26);
			this.butBlank.TabIndex = 0;
			this.butBlank.Text = "&Blank";
			this.butBlank.Click += new System.EventHandler(this.butBlank_Click);
			// 
			// gridMain
			// 
			this.gridMain.HScrollVisible = false;
			this.gridMain.Location = new System.Drawing.Point(12,37);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.Size = new System.Drawing.Size(911,586);
			this.gridMain.TabIndex = 16;
			this.gridMain.Title = "Prescriptions";
			this.gridMain.TranslationName = "TableRxSetup";
			this.gridMain.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellDoubleClick);
			// 
			// FormRxSelect
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(942,674);
			this.Controls.Add(this.gridMain);
			this.Controls.Add(this.butBlank);
			this.Controls.Add(this.label1);
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
		}

		private void FillGrid() {
			RxDefList=RxDefs.Refresh();
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TableRxSetup","Drug"),140);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableRxSetup","Sig"),320);
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
			//Alert
			RxAlert[] alertList=RxAlerts.Refresh(RxDefCur.RxDefNum);
			Disease[] diseases=Diseases.Refresh(PatCur.PatNum);
			ArrayList matchAL=new ArrayList();
			for(int i=0;i<alertList.Length;i++){
				for(int j=0;j<diseases.Length;j++){
					if(alertList[i].DiseaseDefNum==diseases[j].DiseaseDefNum){
						matchAL.Add(DiseaseDefs.GetName(diseases[j].DiseaseDefNum));
					}
				}
			}
			if(matchAL.Count>0){
				string alert=Lan.g(this,"This patient has the following medical conditions or allergies:\r\n");
				for(int i=0;i<matchAL.Count;i++){
					alert+="\r\n"+matchAL[i];
				}
				alert+="\r\n\r\n"+Lan.g(this,"Continue anyway?");
				if(MessageBox.Show(alert,"Alert",MessageBoxButtons.OKCancel,MessageBoxIcon.Exclamation)!=DialogResult.OK){
					return;
				}
			}
			//User OK with alert
			RxPat RxPatCur=new RxPat();
			RxPatCur.RxDate=DateTime.Today;
			RxPatCur.PatNum=PatCur.PatNum;
			RxPatCur.Drug=RxDefCur.Drug;
			RxPatCur.Sig=RxDefCur.Sig;
			RxPatCur.Disp=RxDefCur.Disp;
			RxPatCur.Refills=RxDefCur.Refills;
			//Notes not copied: we don't want these kinds of notes cluttering things
			FormRxEdit FormE=new FormRxEdit(PatCur,RxPatCur);
			FormE.IsNew=true;
			FormE.ShowDialog();
			if(FormE.DialogResult!=DialogResult.OK){
				return;
			}
			DialogResult=DialogResult.OK;
		}

		private void butBlank_Click(object sender, System.EventArgs e) {
			RxPat RxPatCur=new RxPat();
			RxPatCur.RxDate=DateTime.Today;
			RxPatCur.PatNum=PatCur.PatNum;
			FormRxEdit FormE=new FormRxEdit(PatCur,RxPatCur);
			FormE.IsNew=true;
			FormE.ShowDialog();
			if(FormE.DialogResult!=DialogResult.OK){
				return;
			}
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
