using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
///<summary></summary>
	public class FormRpAdjSheet : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private System.Windows.Forms.MonthCalendar date2;
		private System.Windows.Forms.MonthCalendar date1;
		private System.Windows.Forms.Label labelTO;
		private System.ComponentModel.Container components = null;
		private OpenDental.UI.Button butAll;
		private ListBox listProv;
		private Label label1;
		private ListBox listType;
		private Label label2;
		private FormQuery FormQuery2;

		///<summary></summary>
		public FormRpAdjSheet(){
			InitializeComponent();
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
			OpenDental.UI.Button butTypeAll;
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRpAdjSheet));
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.date2 = new System.Windows.Forms.MonthCalendar();
			this.date1 = new System.Windows.Forms.MonthCalendar();
			this.labelTO = new System.Windows.Forms.Label();
			this.butAll = new OpenDental.UI.Button();
			this.listProv = new System.Windows.Forms.ListBox();
			this.label1 = new System.Windows.Forms.Label();
			this.listType = new System.Windows.Forms.ListBox();
			this.label2 = new System.Windows.Forms.Label();
			butTypeAll = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// butTypeAll
			// 
			butTypeAll.AdjustImageLocation = new System.Drawing.Point(0,0);
			butTypeAll.Autosize = true;
			butTypeAll.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			butTypeAll.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			butTypeAll.CornerRadius = 4F;
			butTypeAll.Location = new System.Drawing.Point(32,366);
			butTypeAll.Name = "butTypeAll";
			butTypeAll.Size = new System.Drawing.Size(75,26);
			butTypeAll.TabIndex = 40;
			butTypeAll.Text = "&All";
			butTypeAll.Click += new System.EventHandler(this.butTypeAll_Click);
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
			this.butCancel.Location = new System.Drawing.Point(627,390);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,26);
			this.butCancel.TabIndex = 4;
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
			this.butOK.Location = new System.Drawing.Point(627,358);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 3;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// date2
			// 
			this.date2.Location = new System.Drawing.Point(288,37);
			this.date2.Name = "date2";
			this.date2.TabIndex = 2;
			// 
			// date1
			// 
			this.date1.Location = new System.Drawing.Point(32,37);
			this.date1.Name = "date1";
			this.date1.TabIndex = 1;
			// 
			// labelTO
			// 
			this.labelTO.Location = new System.Drawing.Point(222,37);
			this.labelTO.Name = "labelTO";
			this.labelTO.Size = new System.Drawing.Size(51,23);
			this.labelTO.TabIndex = 28;
			this.labelTO.Text = "TO";
			this.labelTO.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// butAll
			// 
			this.butAll.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAll.Autosize = true;
			this.butAll.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAll.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAll.CornerRadius = 4F;
			this.butAll.Location = new System.Drawing.Point(522,193);
			this.butAll.Name = "butAll";
			this.butAll.Size = new System.Drawing.Size(75,26);
			this.butAll.TabIndex = 37;
			this.butAll.Text = "&All";
			this.butAll.Click += new System.EventHandler(this.butAll_Click);
			// 
			// listProv
			// 
			this.listProv.Location = new System.Drawing.Point(521,37);
			this.listProv.Name = "listProv";
			this.listProv.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.listProv.Size = new System.Drawing.Size(181,147);
			this.listProv.TabIndex = 36;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(521,18);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(104,16);
			this.label1.TabIndex = 35;
			this.label1.Text = "Providers";
			// 
			// listType
			// 
			this.listType.Location = new System.Drawing.Point(32,263);
			this.listType.Name = "listType";
			this.listType.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.listType.Size = new System.Drawing.Size(120,95);
			this.listType.TabIndex = 39;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(32,244);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(120,16);
			this.label2.TabIndex = 38;
			this.label2.Text = "Adjustment Types";
			// 
			// FormRpAdjSheet
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(736,442);
			this.Controls.Add(butTypeAll);
			this.Controls.Add(this.listType);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.butAll);
			this.Controls.Add(this.listProv);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.date2);
			this.Controls.Add(this.date1);
			this.Controls.Add(this.labelTO);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormRpAdjSheet";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Daily Adjustment Report";
			this.Load += new System.EventHandler(this.FormDailyAdjustment_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormDailyAdjustment_Load(object sender, System.EventArgs e) {
			date1.SelectionStart=DateTime.Today;
			date2.SelectionStart=DateTime.Today;
			for(int i=0;i<Providers.List.Length;i++) {
				listProv.Items.Add(Providers.List[i].Abbr+" - "+Providers.List[i].LName+", "+Providers.List[i].FName);
				listProv.SetSelected(i,true);
			}
			for(int i=0;i<DefB.Short[(int)DefCat.AdjTypes].Length;i++) {
				listType.Items.Add(DefB.Short[(int)DefCat.AdjTypes][i].ItemName);
				listType.SetSelected(i,true);
			}
		}

		private void butAll_Click(object sender,EventArgs e) {
			for(int i=0;i<listProv.Items.Count;i++) {
				listProv.SetSelected(i,true);
			}
		}

		private void butTypeAll_Click(object sender,EventArgs e) {
			for(int i=0;i<listType.Items.Count;i++) {
				listType.SetSelected(i,true);
			}
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(listProv.SelectedIndices.Count==0) {
				MsgBox.Show(this,"At least one provider must be selected.");
				return;
			}
			if(listType.SelectedIndices.Count==0) {
				MsgBox.Show(this,"At least one type must be selected.");
				return;
			}
			string whereProv="(";
			for(int i=0;i<listProv.SelectedIndices.Count;i++) {
				if(i>0) {
					whereProv+="OR ";
				}
				whereProv+="adjustment.ProvNum = '"
					+POut.PInt(Providers.List[listProv.SelectedIndices[i]].ProvNum)+"' ";
			}
			whereProv+=")";
			string whereType="(";
			for(int i=0;i<listType.SelectedIndices.Count;i++) {
				if(i>0) {
					whereType+="OR ";
				}
				whereType+="adjustment.AdjType = '"
					+POut.PInt(DefB.Short[(int)DefCat.AdjTypes][listType.SelectedIndices[i]].DefNum)+"' ";
			}
			whereType+=")";
			Queries.CurReport=new ReportOld();
			Queries.CurReport.Query="SELECT adjustment.AdjDate,"
				+"CONCAT(CONCAT(CONCAT(CONCAT(patient.LName,', '),patient.FName),' '),patient.MiddleI),"
				+"definition.ItemName,adjustment.AdjNote,adjustment.AdjAmt FROM "
				+"adjustment,patient,definition WHERE adjustment.AdjType=definition.DefNum "
			  +"AND patient.PatNum=adjustment.PatNum "
				+"AND "+whereProv+" "
				+"AND "+whereType+" "
				+"AND adjustment.AdjDate >= "+POut.PDate(date1.SelectionStart)+" "
				+"AND adjustment.AdjDate <= "+POut.PDate(date2.SelectionStart);
			Queries.CurReport.Query += " ORDER BY adjustment.AdjDate";
			FormQuery2=new FormQuery();
			FormQuery2.IsReport=true;
			FormQuery2.SubmitReportQuery();			
			Queries.CurReport.Title="Daily Adjustments";
			Queries.CurReport.SubTitle=new string[2];
			Queries.CurReport.SubTitle[0]=((Pref)PrefB.HList["PracticeTitle"]).ValueString;
			Queries.CurReport.SubTitle[1]=date1.SelectionStart.ToString("d")+" - "+date2.SelectionStart.ToString("d");	
			Queries.CurReport.ColPos=new int[6];
			Queries.CurReport.ColCaption=new string[5];
			Queries.CurReport.ColAlign=new HorizontalAlignment[5];
			Queries.CurReport.ColPos[0]=20;
			Queries.CurReport.ColPos[1]=120;
			Queries.CurReport.ColPos[2]=270;
			Queries.CurReport.ColPos[3]=495;
			Queries.CurReport.ColPos[4]=645;
			Queries.CurReport.ColPos[5]=720;
			Queries.CurReport.ColCaption[0]="Date";
			Queries.CurReport.ColCaption[1]="Patient Name";			
			Queries.CurReport.ColCaption[2]="Adjustment Type";
			Queries.CurReport.ColCaption[3]="Adjustment Note";
			Queries.CurReport.ColCaption[4]="Amount";
			Queries.CurReport.ColAlign[4]=HorizontalAlignment.Right;
			Queries.CurReport.Summary=new string[0];
			FormQuery2.ShowDialog();
			DialogResult=DialogResult.OK;
		}
		

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		
	}
}
