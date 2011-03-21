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
	public partial class FormRecurringCharges:Form {
		private DataTable table;
		private PrintDocument pd;
		private int pagesPrinted;
		private int headingPrintH;
		private bool headingPrinted;
		private Program prog;

		///<summary>Only works for XCharge so far.</summary>
		public FormRecurringCharges() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormRecurringCharges_Load(object sender,EventArgs e) {
			labelCharged.Text=Lan.g(this,"Charged=")+"0";
			labelFailed.Text=Lan.g(this,"Failed=")+"0";
			FillGrid();
			gridMain.SetSelected(true);
			labelSelected.Text=Lan.g(this,"Selected=")+gridMain.SelectedIndices.Length.ToString();
		}

		private void FillGrid() {
			prog=Programs.GetCur(ProgramName.Xcharge);
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
			if(!File.Exists(prog.Path)) {
				MsgBox.Show(this,"Path is not valid.");
				if(Security.IsAuthorized(Permissions.Setup)){
					FormXchargeSetup FormX=new FormXchargeSetup();
					FormX.ShowDialog();
				}
				return;
			}
			table=CreditCards.GetRecurringChargeList(PIn.Int(ProgramProperties.GetPropVal(prog.ProgramNum,"PaymentType")));
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TableRecurring","PatNum"),130);
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
				row.Cells.Add(famBalTotal.ToString("F"));
				row.Cells.Add(chargeAmt.ToString("F"));
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
			labelTotal.Text=Lan.g(this,"Total=")+table.Rows.Count.ToString();
			labelSelected.Text=Lan.g(this,"Selected=")+gridMain.SelectedIndices.Length.ToString();
			labelCharged.Text=Lan.g(this,"Charged=")+"0";
			labelFailed.Text=Lan.g(this,"Failed=")+"0";
		}
		
		private void gridMain_CellClick(object sender,ODGridClickEventArgs e) {
			labelSelected.Text=Lan.g(this,"Selected=")+gridMain.SelectedIndices.Length.ToString();
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
			if(pd.DefaultPageSettings.PaperSize.Height==0) {
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
			if(prog==null){//Gets filled in FillGrid()
				return;
			}
			int failed=0;
			int success=0;
			string user=ProgramProperties.GetPropVal(prog.ProgramNum,"Username");
			string password=ProgramProperties.GetPropVal(prog.ProgramNum,"Password");
			for(int i=0;i<gridMain.SelectedIndices.Length;i++) {
				ProcessStartInfo info=new ProcessStartInfo(prog.Path);
				string resultfile=Path.Combine(Path.GetDirectoryName(prog.Path),"XResult.txt");
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
				info.Arguments+="/RECEIPT:Pat"+table.Rows[gridMain.SelectedIndices[i]]["PatNum"].ToString()+" ";//aka invoice#
				info.Arguments+="\"/CLERK:"+Security.CurUser.UserName+"\" /LOCKCLERK ";
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
				string resulttext="";
				string line="";
				using(TextReader reader=new StreamReader(resultfile)) {
					line=reader.ReadLine();
					while(line!=null) {
						if(resulttext!="") {
							resulttext+="\r\n";
						}
						resulttext+=line;
						if(line.StartsWith("RESULT=")) {
							if(line!="RESULT=SUCCESS") {
								failed++;
								break;
							}
							success++;
						}
						line=reader.ReadLine();
					}
				}
			}
			//PIn.Int(ProgramProperties.GetPropVal(prog.ProgramNum,"PaymentType"));
			FillGrid();
			labelCharged.Text=Lan.g(this,"Charged=")+success;
			labelFailed.Text=Lan.g(this,"Failed=")+failed;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	}
}