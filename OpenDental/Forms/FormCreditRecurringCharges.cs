using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using OpenDental.UI;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormCreditRecurringCharges:Form {
		private DataTable table;
		private PrintDocument pd;
		private int pagesPrinted;
		private int headingPrintH;
		private bool headingPrinted;
		private bool insertPayment;
		private Program prog;
		private DateTime nowDateTime;
		private string xPath;

		///<summary>Only works for XCharge so far.</summary>
		public FormCreditRecurringCharges() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormRecurringCharges_Load(object sender,EventArgs e) {
			nowDateTime=MiscData.GetNowDateTime();
			prog=Programs.GetCur(ProgramName.Xcharge);
			xPath=Programs.GetProgramPath(prog);
			labelCharged.Text=Lan.g(this,"Charged=")+"0";
			labelFailed.Text=Lan.g(this,"Failed=")+"0";
			FillGrid();
			gridMain.SetSelected(true);
			labelSelected.Text=Lan.g(this,"Selected=")+gridMain.SelectedIndices.Length.ToString();
		}

		private void FillGrid() {
			//Currently only working for X-Charge. If more added then move this check out of FillGrid.
			#region XCharge Check
			if(prog==null){
				MsgBox.Show(this,"X-Charge entry is missing from the database.");//should never happen
				return;
			}
			if(!prog.Enabled) {
				if(Security.IsAuthorized(Permissions.Setup)) {
					FormXchargeSetup FormX=new FormXchargeSetup();
					FormX.ShowDialog();
				}
				return;
			}
			if(!File.Exists(xPath)) {
				MsgBox.Show(this,"Path is not valid.");
				if(Security.IsAuthorized(Permissions.Setup)){
					FormXchargeSetup FormX=new FormXchargeSetup();
					FormX.ShowDialog();
				}
				return;
			}
			#endregion
			table=CreditCards.GetRecurringChargeList();
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TableRecurring","PatNum"),110);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableRecurring","Name"),250);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableRecurring","Total Bal"),90,HorizontalAlignment.Right);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableRecurring","ChargeAmt"),100,HorizontalAlignment.Right);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			OpenDental.UI.ODGridRow row;
			for(int i=0;i<table.Rows.Count;i++) {
				row=new OpenDental.UI.ODGridRow();
				Double famBalTotal=PIn.Double(table.Rows[i]["FamBalTotal"].ToString());
				Double chargeAmt=PIn.Double(table.Rows[i]["ChargeAmt"].ToString());
				row.Cells.Add(table.Rows[i]["PatNum"].ToString());
				row.Cells.Add(table.Rows[i]["PatName"].ToString());
				row.Cells.Add(famBalTotal.ToString("c"));
				row.Cells.Add(chargeAmt.ToString("c"));
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
			labelTotal.Text=Lan.g(this,"Total=")+table.Rows.Count.ToString();
			labelSelected.Text=Lan.g(this,"Selected=")+gridMain.SelectedIndices.Length.ToString();
		}

		///<summary>Returns a valid DateTime for the payment's PayDate.  Contains logic if payment should be for the previous or the current month.</summary>
		private DateTime GetPayDate(DateTime latestPayment,DateTime dateStart) {
			//Most common, current day >= dateStart so we use current month and year with the dateStart day.  Will always be a legal DateTime.
			if(nowDateTime.Day>=dateStart.Day) {
				return new DateTime(nowDateTime.Year,nowDateTime.Month,dateStart.Day);
			}
			//If not enough days in current month to match the dateStart see if on the last day in the month.
			//Example: dateStart=08/31/2009 and month is February 28th so we need the PayDate to be today not for last day on the last month, which would happen below.
			int daysInMonth=DateTime.DaysInMonth(nowDateTime.Year,nowDateTime.Month);
			if(daysInMonth<=dateStart.Day && daysInMonth==nowDateTime.Day) {
				return nowDateTime;//Today is last day of the month so return today as the PayDate.
			}
			//PayDate needs to be for the previous month so we need to determine if using the dateStart day would be a legal DateTime.
			DateTime nowMinusOneMonth=nowDateTime.AddMonths(-1);
			daysInMonth=DateTime.DaysInMonth(nowMinusOneMonth.Year,nowMinusOneMonth.Month);
			if(daysInMonth<=dateStart.Day) {
				return new DateTime(nowMinusOneMonth.Year,nowMinusOneMonth.Month,daysInMonth);//Returns the last day of the previous month.
			}
			return new DateTime(nowMinusOneMonth.Year,nowMinusOneMonth.Month,dateStart.Day);//Previous month contains a legal date using dateStart's day.
		}

		///<summary>Tests the selected indicies with newly calculated pay dates.  If there's a date violation, a warning shows and false is returned.</summary>
		private bool PaymentsWithinLockDate() {
			//Check if user has the payment create permission in the first place to save time.
			if(!Security.IsAuthorized(Permissions.PaymentCreate,nowDateTime.Date)) {
				return false;
			}
			List<string> warnings=new List<string>();
			for(int i=0;i<gridMain.SelectedIndices.Length;i++) {
				//Calculate what the new pay date will be.
				DateTime newPayDate=GetPayDate(PIn.Date(table.Rows[gridMain.SelectedIndices[i]]["LatestPayment"].ToString()),
			      PIn.Date(table.Rows[gridMain.SelectedIndices[i]]["DateStart"].ToString()));
				//Test if the user can create a payment with the new pay date.
				if(!Security.IsAuthorized(Permissions.PaymentCreate,newPayDate,true)) {
					if(warnings.Count==0) {
						warnings.Add("Lock date limitation is preventing the recurring charges from running:");
					}
					warnings.Add(newPayDate.ToShortDateString()+" - "
						+table.Rows[i]["PatNum"].ToString()+": "
						+table.Rows[i]["PatName"].ToString()+" - "
						+PIn.Double(table.Rows[i]["FamBalTotal"].ToString()).ToString("c")+" - "
						+PIn.Double(table.Rows[i]["ChargeAmt"].ToString()).ToString("c"));
				}
			}
			if(warnings.Count>0) {
				string msg="";
				for(int i=0;i<warnings.Count;i++) {
					if(i>0) {
						msg+="\r\n";
					}
					msg+=warnings[i];
				}
				//Show the warning message.  This allows the user the ability to unhighlight rows or go change the date limitation.
				MessageBox.Show(msg);
				return false;
			}
			return true;
		}

		private void gridMain_CellClick(object sender,ODGridClickEventArgs e) {
			labelSelected.Text=Lan.g(this,"Selected=")+gridMain.SelectedIndices.Length.ToString();
		}
		
		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			if(!Security.IsAuthorized(Permissions.AccountModule)) {
				return;
			}
			if(gridMain.SelectedIndices.Length==0) {
				MsgBox.Show(this,"Must select at least one recurring charge.");
				return;
			}
			long patNum=PIn.Long(table.Rows[gridMain.SelectedIndices[0]]["PatNum"].ToString());
			GotoModule.GotoAccount(patNum);
		}

		private void butRefresh_Click(object sender,EventArgs e) {
			FillGrid();
			labelCharged.Text=Lan.g(this,"Charged=")+"0";
			labelFailed.Text=Lan.g(this,"Failed=")+"0";
		}

		private void butPrintList_Click(object sender,EventArgs e) {
			pagesPrinted=0;
			pd=new PrintDocument();
			pd.PrintPage += new PrintPageEventHandler(this.pd_PrintPage);
			pd.DefaultPageSettings.Margins=new Margins(25,25,40,40);
			pd.DefaultPageSettings.Landscape=true;
			if(pd.DefaultPageSettings.PrintableArea.Height==0) {
				pd.DefaultPageSettings.PaperSize=new PaperSize("default",850,1100);
			}
			headingPrinted=false;
			try {
				#if DEBUG
					FormRpPrintPreview pView = new FormRpPrintPreview();
					pView.printPreviewControl2.Document=pd;
					pView.ShowDialog();
				#else
					if(PrinterL.SetPrinter(pd,PrintSituation.Default)) {
						pd.Print();
					}
				#endif
			}
			catch {
				MessageBox.Show(Lan.g(this,"Printer not available"));
			}
		}

		private void pd_PrintPage(object sender,System.Drawing.Printing.PrintPageEventArgs e) {
			Rectangle bounds=e.MarginBounds;
				//new Rectangle(50,40,800,1035);//Some printers can handle up to 1042
			Graphics g=e.Graphics;
			string text;
			Font headingFont=new Font("Arial",13,FontStyle.Bold);
			Font subHeadingFont=new Font("Arial",10,FontStyle.Bold);
			int yPos=bounds.Top;
			int center=bounds.X+bounds.Width/2;
			#region printHeading
			if(!headingPrinted) {
				text=Lan.g(this,"Recurring Charges");
				g.DrawString(text,headingFont,Brushes.Black,center-g.MeasureString(text,headingFont).Width/2,yPos);
				yPos+=(int)g.MeasureString(text,headingFont).Height;
				yPos+=20;
				headingPrinted=true;
				headingPrintH=yPos;
			}
			#endregion
			yPos=gridMain.PrintPage(g,pagesPrinted,bounds,headingPrintH);
			pagesPrinted++;
			if(yPos==-1) {
				e.HasMorePages=true;
			}
			else {
				e.HasMorePages=false;
			}
			g.Dispose();
		}

		private void butAll_Click(object sender,EventArgs e) {
			gridMain.SetSelected(true);
			labelSelected.Text=Lan.g(this,"Selected=")+gridMain.SelectedIndices.Length.ToString();
		}

		private void butNone_Click(object sender,EventArgs e) {
			gridMain.SetSelected(false);
			labelSelected.Text=Lan.g(this,"Selected=")+gridMain.SelectedIndices.Length.ToString();
		}

		private void butSend_Click(object sender,EventArgs e) {
			//Assuming the use of XCharge.  If adding another vendor (PayConnect for example)
			//make sure to move XCharge validation in FillGrid() to here.
			if(prog==null) {//Gets filled in FillGrid()
				return;
			}
			if(gridMain.SelectedIndices.Length<1) {
				MsgBox.Show(this,"Must select at least one recurring charge.");
				return;
			}
			if(!PaymentsWithinLockDate()) {
				return;
			}
			string recurringResultFile="Recurring charge results for "+DateTime.Now.ToShortDateString()+" ran at "+DateTime.Now.ToShortTimeString()+"\r\n\r\n";
			int failed=0;
			int success=0;
			string user=ProgramProperties.GetPropVal(prog.ProgramNum,"Username");
			string password=ProgramProperties.GetPropVal(prog.ProgramNum,"Password");
			#region Card Charge Loop
			for(int i=0;i<gridMain.SelectedIndices.Length;i++) {
				if(table.Rows[gridMain.SelectedIndices[i]]["XChargeToken"].ToString()!="" &&
					CreditCards.IsDuplicateXChargeToken(table.Rows[gridMain.SelectedIndices[i]]["XChargeToken"].ToString())) 
				{
					MessageBox.Show(Lan.g(this,"A duplicate token was found, the card cannot be charged for customer: ")+table.Rows[i]["PatName"].ToString());
					continue;
				}
				insertPayment=false;
				ProcessStartInfo info=new ProcessStartInfo(xPath);
				long patNum=PIn.Long(table.Rows[gridMain.SelectedIndices[i]]["PatNum"].ToString());
				string resultfile=Path.Combine(Path.GetDirectoryName(xPath),"XResult.txt");
				File.Delete(resultfile);//delete the old result file.
				info.Arguments="";
				double amt=PIn.Double(table.Rows[gridMain.SelectedIndices[i]]["ChargeAmt"].ToString());
				DateTime exp=PIn.Date(table.Rows[gridMain.SelectedIndices[i]]["CCExpiration"].ToString());
				info.Arguments+="/AMOUNT:"+amt.ToString("F2")+" /LOCKAMOUNT ";
				info.Arguments+="/TRANSACTIONTYPE:PURCHASE /LOCKTRANTYPE ";
				if(table.Rows[gridMain.SelectedIndices[i]]["XChargeToken"].ToString()!="") {
					info.Arguments+="/XCACCOUNTID:"+table.Rows[gridMain.SelectedIndices[i]]["XChargeToken"].ToString()+" ";
				}
				else {
					info.Arguments+="/ACCOUNT:"+table.Rows[gridMain.SelectedIndices[i]]["CCNumberMasked"].ToString()+" ";
					info.Arguments+="/EXP:"+exp.ToString("MMyy")+" ";
					info.Arguments+="\"/ADDRESS:"+table.Rows[gridMain.SelectedIndices[i]]["Address"].ToString()+"\" ";
					info.Arguments+="\"/ZIP:"+table.Rows[gridMain.SelectedIndices[i]]["Zip"].ToString()+"\" ";
				}
				info.Arguments+="/RECEIPT:Pat"+patNum+" ";//aka invoice#
				info.Arguments+="\"/CLERK:"+Security.CurUser.UserName+"\" /LOCKCLERK ";
				info.Arguments+="/RESULTFILE:\""+resultfile+"\" ";
				info.Arguments+="/USERID:"+user+" ";
				info.Arguments+="/PASSWORD:"+password+" ";
				info.Arguments+="/HIDEMAINWINDOW ";
				info.Arguments+="/AUTOPROCESS ";
				info.Arguments+="/SMALLWINDOW ";
				info.Arguments+="/AUTOCLOSE ";
				info.Arguments+="/NORESULTDIALOG ";
				Cursor=Cursors.WaitCursor;
				Process process=new Process();
				process.StartInfo=info;
				process.EnableRaisingEvents=true;
				process.Start();
				while(!process.HasExited) {
					Application.DoEvents();
				}
				Thread.Sleep(200);//Wait 2/10 second to give time for file to be created.
				Cursor=Cursors.Default;
				string line="";
				string resultText="";
				recurringResultFile+="PatNum: "+patNum+" Name: "+table.Rows[i]["PatName"].ToString()+"\r\n";
				using(TextReader reader=new StreamReader(resultfile)) {
					line=reader.ReadLine();
					while(line!=null) {
						if(resultText!="") {
							resultText+="\r\n";
						}
						resultText+=line;
						if(line.StartsWith("RESULT=")) {
							if(line=="RESULT=SUCCESS") {
								success++;
								labelCharged.Text=Lan.g(this,"Charged=")+success;
								insertPayment=true;
							}
							else {
								failed++;
								labelFailed.Text=Lan.g(this,"Failed=")+failed;
							}
						}
						line=reader.ReadLine();
					}
					recurringResultFile+=resultText+"\r\n\r\n";
				}
				if(insertPayment) {
					Patient patCur=Patients.GetPat(patNum);
					long provider=Patients.GetProvNum(patCur);
					Payment paymentCur=new Payment();
					paymentCur.DateEntry=nowDateTime.Date;
					paymentCur.PayDate=GetPayDate(PIn.Date(table.Rows[gridMain.SelectedIndices[i]]["LatestPayment"].ToString()),
						PIn.Date(table.Rows[gridMain.SelectedIndices[i]]["DateStart"].ToString()));
					paymentCur.PatNum=patCur.PatNum;
					paymentCur.ClinicNum=patCur.ClinicNum;
					paymentCur.PayType=PIn.Int(ProgramProperties.GetPropVal(prog.ProgramNum,"PaymentType"));
					paymentCur.PayAmt=amt;
					paymentCur.PayNote=resultText;
					paymentCur.IsRecurringCC=true;
					Payments.Insert(paymentCur);
					DataTable dt=Patients.GetPaymentStartingBalances(patCur.Guarantor,paymentCur.PayNum);
					double highestAmt=0;
					for(int j=0;j<dt.Rows.Count;j++) {//Apply the payment towards the provider that the family owes the most money to.
						double afterIns=PIn.Double(dt.Rows[j]["AfterIns"].ToString());
						if(highestAmt>=afterIns) {
							continue;
						}
						highestAmt=afterIns;
						provider=PIn.Long(dt.Rows[j]["ProvNum"].ToString());
					}
					PaySplit split=new PaySplit();
					split.PatNum=paymentCur.PatNum;
					split.ClinicNum=paymentCur.ClinicNum;
					split.PayNum=paymentCur.PayNum;
					split.ProcDate=paymentCur.PayDate;
					split.DatePay=paymentCur.PayDate;
					split.ProvNum=provider;
					split.SplitAmt=paymentCur.PayAmt;
					split.PayPlanNum=PIn.Long(table.Rows[i]["PayPlanNum"].ToString());
					PaySplits.Insert(split);
					if(PrefC.GetBool(PrefName.AgingCalculatedMonthlyInsteadOfDaily)) {
						Ledgers.ComputeAging(patCur.Guarantor,PrefC.GetDate(PrefName.DateLastAging),false);
					}
					else {
						Ledgers.ComputeAging(patCur.Guarantor,DateTime.Today,false);
						if(PrefC.GetDate(PrefName.DateLastAging) != DateTime.Today) {
							Prefs.UpdateString(PrefName.DateLastAging,POut.Date(DateTime.Today,false));
							//Since this is always called from UI, the above line works fine to keep the prefs cache current.
						}
					}
				}
			}
			#endregion
			try {
				File.WriteAllText(Path.Combine(Path.GetDirectoryName(xPath),"RecurringChargeResult.txt"),recurringResultFile);
			}
			catch { } //Do nothing cause this is just for internal use.
			FillGrid();
			labelCharged.Text=Lan.g(this,"Charged=")+success;
			labelFailed.Text=Lan.g(this,"Failed=")+failed;
			MsgBox.Show(this,"Done charging cards.\r\nIf there are any patients remaining in list, print the list and handle each one manually.");
		}

		private void butCancel_Click(object sender,EventArgs e) {
			Close();
		}

	}
}