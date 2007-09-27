using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using OpenDental.ReportingOld2;
using OpenDentBusiness;

namespace OpenDental
{
	/// <summary>
	/// Summary description for FormRpApptWithPhones.
	/// </summary>
	public class FormRpRouting : System.Windows.Forms.Form
	{
		private OpenDental.UI.Button butAll;
		private System.Windows.Forms.ListBox listProv;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private OpenDental.ValidDate textDate;
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butDisplayed;
		private OpenDental.UI.Button butToday;
		///<summary>This list of appointments gets filled.  Each appointment will result in one page when printing.</summary>
		private Appointment[] Appts;
		private int pagesPrinted;
		private PrintDocument pd;
		private OpenDental.UI.PrintPreview printPreview;
		///<summary>The date that the user selected.</summary>
		private DateTime date;
		///<summary>If set externally beforehand, then the user will not see any choices, and only a routing slip for the one appt will print.</summary>
		public int ApptNum;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		///<summary></summary>
		public FormRpRouting()
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRpRouting));
			this.butAll = new OpenDental.UI.Button();
			this.listProv = new System.Windows.Forms.ListBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.textDate = new OpenDental.ValidDate();
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.butToday = new OpenDental.UI.Button();
			this.butDisplayed = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// butAll
			// 
			this.butAll.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAll.Autosize = true;
			this.butAll.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAll.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAll.CornerRadius = 4F;
			this.butAll.Location = new System.Drawing.Point(28,243);
			this.butAll.Name = "butAll";
			this.butAll.Size = new System.Drawing.Size(75,26);
			this.butAll.TabIndex = 34;
			this.butAll.Text = "&All";
			this.butAll.Click += new System.EventHandler(this.butAll_Click);
			// 
			// listProv
			// 
			this.listProv.Location = new System.Drawing.Point(27,41);
			this.listProv.Name = "listProv";
			this.listProv.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.listProv.Size = new System.Drawing.Size(120,186);
			this.listProv.TabIndex = 33;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(27,18);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(104,16);
			this.label1.TabIndex = 32;
			this.label1.Text = "Providers";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(183,43);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(82,18);
			this.label2.TabIndex = 37;
			this.label2.Text = "Date";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textDate
			// 
			this.textDate.Location = new System.Drawing.Point(269,41);
			this.textDate.Name = "textDate";
			this.textDate.Size = new System.Drawing.Size(100,20);
			this.textDate.TabIndex = 43;
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
			this.butCancel.Location = new System.Drawing.Point(447,244);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,26);
			this.butCancel.TabIndex = 44;
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
			this.butOK.Location = new System.Drawing.Point(447,204);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 43;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butToday
			// 
			this.butToday.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butToday.Autosize = true;
			this.butToday.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butToday.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butToday.CornerRadius = 4F;
			this.butToday.Location = new System.Drawing.Point(427,40);
			this.butToday.Name = "butToday";
			this.butToday.Size = new System.Drawing.Size(96,23);
			this.butToday.TabIndex = 46;
			this.butToday.Text = "Today";
			this.butToday.Click += new System.EventHandler(this.butToday_Click);
			// 
			// butDisplayed
			// 
			this.butDisplayed.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butDisplayed.Autosize = true;
			this.butDisplayed.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDisplayed.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDisplayed.CornerRadius = 4F;
			this.butDisplayed.Location = new System.Drawing.Point(427,67);
			this.butDisplayed.Name = "butDisplayed";
			this.butDisplayed.Size = new System.Drawing.Size(96,23);
			this.butDisplayed.TabIndex = 45;
			this.butDisplayed.Text = "Displayed";
			this.butDisplayed.Click += new System.EventHandler(this.butDisplayed_Click);
			// 
			// FormRpRouting
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(562,292);
			this.Controls.Add(this.butToday);
			this.Controls.Add(this.butDisplayed);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.textDate);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butAll);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.listProv);
			this.Controls.Add(this.label1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FormRpRouting";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Routing Slips";
			this.Load += new System.EventHandler(this.FormRpRouting_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormRpRouting_Load(object sender, System.EventArgs e){
			if(ApptNum!=0){
				Appts=new Appointment[] {Appointments.GetOneApt(ApptNum)};
				if(Appts.Length==0 || Appts[0]==null) {
					MsgBox.Show(this,"Appointment not found");
					return;
				}
				pagesPrinted=0;
				pd=new PrintDocument();
				pd.PrintPage+=new PrintPageEventHandler(this.pd_PrintPage);
				pd.OriginAtMargins=true;
				pd.DefaultPageSettings.Margins=new Margins(0,0,0,0);
				printPreview=new OpenDental.UI.PrintPreview(PrintSituation.Default,pd,Appts.Length);
				printPreview.ShowDialog();
				DialogResult=DialogResult.OK;
			}
			for(int i=0;i<Providers.List.Length;i++){
				listProv.Items.Add(Providers.List[i].Abbr+" - "+Providers.List[i].LName+", "
					+Providers.List[i].FName);
				listProv.SetSelected(i,true);
			}
			textDate.Text=DateTime.Today.ToShortDateString();
		}

		private void butToday_Click(object sender, System.EventArgs e) {
			textDate.Text=DateTime.Today.ToShortDateString();
		}

		private void butDisplayed_Click(object sender, System.EventArgs e) {
			textDate.Text=Appointments.DateSelected.ToShortDateString();
		}

		private void butAll_Click(object sender, System.EventArgs e){
			for(int i=0;i<listProv.Items.Count;i++){
				listProv.SetSelected(i,true);
			}
		}

		private void butOK_Click(object sender, System.EventArgs e){
			//validate user input
			if(textDate.errorProvider1.GetError(textDate) != "")	{
				MsgBox.Show(this,"Please fix data entry errors first.");
				return;
			}
			if(textDate.Text.Length==0){
				MessageBox.Show(Lan.g(this,"Date is required."));
				return;
			}
			date=PIn.PDate(textDate.Text);
			if(listProv.SelectedIndices.Count==0){
				MessageBox.Show(Lan.g(this,"You must select at least one provider."));
				return;
			}
			int[] provNums=new int[listProv.SelectedIndices.Count];
			for(int i=0;i<provNums.Length;i++){
				provNums[i]=Providers.List[listProv.SelectedIndices[i]].ProvNum;
			}
			Appts=Appointments.GetRouting(date,provNums);
			if(Appts.Length==0){
				MsgBox.Show(this,"There are no appointments scheduled for that date.");
				return;
			}
			pagesPrinted=0;
			pd=new PrintDocument();
			pd.PrintPage+=new PrintPageEventHandler(this.pd_PrintPage);
			pd.OriginAtMargins=true;
			pd.DefaultPageSettings.Margins=new Margins(0,0,0,0);
			printPreview=new OpenDental.UI.PrintPreview(PrintSituation.Default,pd,Appts.Length);
			printPreview.ShowDialog();
			if(printPreview.DialogResult!=DialogResult.OK){
				return;
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, EventArgs e){
			DialogResult=DialogResult.Cancel;
		}

		///<summary>raised for each page to be printed.  One page per appointment.</summary>
		private void pd_PrintPage(object sender,PrintPageEventArgs ev) {
			if(ApptNum!=0) {//just for one appointment
				date=Appointments.DateSelected;
			}
			Graphics g=ev.Graphics;
			float y=50;
			float x=0;
			string str;
			float sizeW;//used when measuring text for placement
			Font fontTitle=new Font(FontFamily.GenericSansSerif,11,FontStyle.Bold);
			Font fontHeading=new Font(FontFamily.GenericSansSerif,9,FontStyle.Bold);
			Font font=new Font(FontFamily.GenericSansSerif,8);
			SolidBrush brush=new SolidBrush(Color.Black);
			//Title----------------------------------------------------------------------------------------------------------
			str=Lan.g(this,"Routing Slip");
			sizeW=g.MeasureString(str,fontTitle).Width;
			x=425-sizeW/2;
			g.DrawString(str,fontTitle,brush,x,y);
			y+=35;
			x=75;
			//Today's appointment, including procedures-----------------------------------------------------------------------
			Family fam=Patients.GetFamily(Appts[pagesPrinted].PatNum);
			Patient pat=fam.GetPatient(Appts[pagesPrinted].PatNum);
			str=pat.GetNameFL();
			g.DrawString(str,fontHeading,brush,x,y);
			y+=18;
			str=Appts[pagesPrinted].AptDateTime.ToShortTimeString()+"  "+Appts[pagesPrinted].AptDateTime.ToShortDateString();
			g.DrawString(str,fontHeading,brush,x,y);
			y+=18;
			str=(Appts[pagesPrinted].Pattern.Length*5).ToString()+" "+Lan.g(this,"minutes");
			g.DrawString(str,font,brush,x,y);
			y+=15;
			str=Providers.GetAbbr(Appts[pagesPrinted].ProvNum);
			g.DrawString(str,font,brush,x,y);
			y+=15;
			if(Appts[pagesPrinted].ProvHyg!=0) {
				str=Providers.GetAbbr(Appts[pagesPrinted].ProvHyg);
				g.DrawString(str,font,brush,x,y);
				y+=15;
			}
			str=Lan.g(this,"Procedures:");
			g.DrawString(str,font,brush,x,y);
			y+=15;
			Procedure[] procsAll=Procedures.Refresh(pat.PatNum);
			Procedure[] procsApt=Procedures.GetProcsOneApt(Appts[pagesPrinted].AptNum,procsAll);
			for(int i=0;i<procsApt.Length;i++) {
				str="   "+Procedures.GetDescription(procsApt[i]);
				g.DrawString(str,font,brush,x,y);
				y+=15;
			}
			str=Lan.g(this,"Note:")+" "+Appts[pagesPrinted].Note;
			g.DrawString(str,font,brush,x,y);
			y+=25;
			//Patient/Family Info---------------------------------------------------------------------------------------------
			g.DrawLine(Pens.Black,75,y,775,y);
			str=Lan.g(this,"Patient Info");
			g.DrawString(str,fontHeading,brush,x,y);
			y+=18;
			str=Lan.g(this,"PatNum:")+" "+pat.PatNum.ToString();
			g.DrawString(str,font,brush,x,y);
			y+=15;
			str=Lan.g(this,"Age:")+" ";
			if(pat.Age>0){
				str+=pat.Age.ToString();
			}
			g.DrawString(str,font,brush,x,y);
			y+=15;
			str=Lan.g(this,"Date of First Visit:")+" ";
			if(pat.DateFirstVisit.Year<1880){
				str+="?";
			}
			else if(pat.DateFirstVisit==Appts[pagesPrinted].AptDateTime.Date){
				str+=Lan.g(this,"New Patient");
			}
			else{
				str+=pat.DateFirstVisit.ToShortDateString();
			}
			g.DrawString(str,font,brush,x,y);
			y+=15;
			str=Lan.g(this,"Billing Type:")+" "+DefB.GetName(DefCat.BillingTypes,pat.BillingType);
			g.DrawString(str,font,brush,x,y);
			y+=15;
			Recall[] recallList=Recalls.GetList(new int[] {pat.PatNum});
			str=Lan.g(this,"Recall Due Date:")+" ";
			if(recallList.Length>0){
				str+=recallList[0].DateDue.ToShortDateString();
			}
			g.DrawString(str,font,brush,x,y);
			y+=15;
			str=Lan.g(this,"Medical notes:")+" "+pat.MedUrgNote;
			g.DrawString(str,font,brush,x,y);
			y+=25;
			//Other Family Members
			str=Lan.g(this,"Other Family Members");
			g.DrawString(str,fontHeading,brush,x,y);
			y+=18;
			for(int i=0;i<fam.List.Length;i++) {
				if(fam.List[i].PatNum==pat.PatNum) {
					continue;
				}
				str=fam.List[i].GetNameFL();
				if(fam.List[i].Age>0){
					str+=",   "+fam.List[i].Age.ToString();
				}
				g.DrawString(str,font,brush,x,y);
				y+=15;
			}
			y+=10;
			//Insurance Info--------------------------------------------------------------------------------------------------
			g.DrawLine(Pens.Black,75,y,775,y);
			str=Lan.g(this,"Insurance");
			g.DrawString(str,fontHeading,brush,x,y);
			y+=18;
			PatPlan[] patPlanList=PatPlans.Refresh(pat.PatNum);
			InsPlan[] plans=InsPlans.Refresh(fam);
			ClaimProc[] claimProcList=ClaimProcs.Refresh(pat.PatNum);
			Benefit[] benefits=Benefits.Refresh(patPlanList);
			InsPlan plan;
			Carrier carrier;
			string subscriber;
			double max;
			double deduct;
			if(patPlanList.Length==0){
				str=Lan.g(this,"none");
				g.DrawString(str,font,brush,x,y);
				y+=15;
			}
			for(int i=0;i<patPlanList.Length;i++){
				plan=InsPlans.GetPlan(patPlanList[i].PlanNum,plans);
				carrier=Carriers.GetCarrier(plan.CarrierNum);
				str=carrier.CarrierName;
				g.DrawString(str,fontHeading,brush,x,y);
				y+=18;
				subscriber=fam.GetNameInFamFL(plan.Subscriber);
				if(subscriber=="") {//subscriber from another family
					subscriber=Patients.GetLim(plan.Subscriber).GetNameLF();
				}
				str=Lan.g(this,"Subscriber:")+" "+subscriber;
				g.DrawString(str,font,brush,x,y);
				y+=15;
				bool isFamMax=Benefits.GetIsFamMax(benefits,plan.PlanNum);
				str="";
				if(isFamMax){
					str+=Lan.g(this,"Family ");
				}
				str+=Lan.g(this,"Annual Max:")+" ";
				max=Benefits.GetAnnualMax(benefits,plan.PlanNum,patPlanList[i].PatPlanNum);
				if(max!=-1){
					str+=max.ToString("n0")+" ";
				}
				str+="   ";
				bool isFamDed=Benefits.GetIsFamDed(benefits,plan.PlanNum);
				if(isFamDed) {
					str+=Lan.g(this,"Family ");
				}
				str+=Lan.g(this,"Deductible:")+" ";
				deduct=Benefits.GetDeductible(benefits,plan.PlanNum,patPlanList[i].PatPlanNum);
				if(deduct!=-1) {
					str+=deduct.ToString("n0");
				}
				g.DrawString(str,font,brush,x,y);
				y+=15;
				str="";
				for(int j=0;j<benefits.Length;j++){
					if(benefits[j].PlanNum != plan.PlanNum){
						continue;
					}
					if(benefits[j].BenefitType != InsBenefitType.Percentage) {
						continue;
					}
					if(str!=""){
						str+=",  ";
					}
					str+=CovCats.GetDesc(benefits[j].CovCatNum)+" "+benefits[j].Percent.ToString()+"%";
				}
				if(str!=""){
					g.DrawString(str,font,brush,x,y);
					y+=15;
				}
				double pend=0;
				double used=0;
				if(isFamMax || isFamDed){
					ClaimProc[] claimProcsFam=ClaimProcs.RefreshFam(plan.PlanNum);
					used=InsPlans.GetInsUsed(claimProcsFam,date,plan.PlanNum,patPlanList[i].PatPlanNum,-1,plans,benefits);
					pend=InsPlans.GetPending(claimProcsFam,date,plan,patPlanList[i].PatPlanNum,-1,benefits);
				}
				else{
					used=InsPlans.GetInsUsed(claimProcList,date,plan.PlanNum,patPlanList[i].PatPlanNum,-1,plans,benefits);
					pend=InsPlans.GetPending(claimProcList,date,plan,patPlanList[i].PatPlanNum,-1,benefits);
				}
				str=Lan.g(this,"Ins Used:")+" "+used.ToString("n");
				g.DrawString(str,font,brush,x,y);
				y+=15;
				str=Lan.g(this,"Ins Pending:")+" "+pend.ToString("n");
				g.DrawString(str,font,brush,x,y);
				y+=15;
			}
			y+=10;
			//Account Info---------------------------------------------------------------------------------------------------
			g.DrawLine(Pens.Black,75,y,775,y);
			str=Lan.g(this,"Account Info");
			g.DrawString(str,fontHeading,brush,x,y);
			y+=18;
			str=Lan.g(this,"Guarantor:")+" "+fam.List[0].GetNameFL();
			g.DrawString(str,font,brush,x,y);
			y+=15;
			str=Lan.g(this,"Balance:")+(fam.List[0].BalTotal-fam.List[0].InsEst).ToString("c");
			if(fam.List[0].InsEst>.01){
				str+="  ("+fam.List[0].BalTotal.ToString("c")+" - "
					+fam.List[0].InsEst.ToString("c")+" "+Lan.g(this,"InsEst")+")";
			}
			g.DrawString(str,font,brush,x,y);
			y+=15;
			str=Lan.g(this,"Aging:")
				+"  0-30:"+fam.List[0].Bal_0_30.ToString("c")
				+"  31-60:"+fam.List[0].Bal_31_60.ToString("c")
				+"  61-90:"+fam.List[0].Bal_61_90.ToString("c")
				+"  90+:"+fam.List[0].BalOver90.ToString("c");
			g.DrawString(str,font,brush,x,y);
			y+=15;
			str=Lan.g(this,"Fam Urgent Fin Note:")
				+fam.List[0].FamFinUrgNote;
			g.DrawString(str,font,brush,x,y);
			y+=15;
			y+=10;
			//Treatment Plan--------------------------------------------------------------------------------------------------
			g.DrawLine(Pens.Black,75,y,775,y);
			str=Lan.g(this,"Treatment Plan");
			g.DrawString(str,fontHeading,brush,x,y);
			y+=18;
			for(int i=0;i<procsAll.Length;i++){
				if(procsAll[i].ProcStatus!=ProcStat.TP){
					continue;
				}
				str=Procedures.GetDescription(procsAll[i]);
				g.DrawString(str,font,brush,x,y);
				y+=15;
			}
			pagesPrinted++;
			if(pagesPrinted==Appts.Length) {
				ev.HasMorePages=false;
				pagesPrinted=0;
			}
			else {
				ev.HasMorePages=true;
			}
		}



		

		
	}
}
