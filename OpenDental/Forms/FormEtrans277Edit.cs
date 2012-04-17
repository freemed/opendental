using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.UI;
using CodeBase;

namespace OpenDental {
	public partial class FormEtrans277Edit:Form {

		public Etrans EtransCur;
		private string MessageText;
		private X277 x277;
		//private bool headingPrinted;
		//private int pagesPrinted;
		//private int headingPrintH=0;
		//private long SubNum;

		public FormEtrans277Edit() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormEtrans277Edit_Load(object sender,EventArgs e) {
			MessageText=EtransMessageTexts.GetMessageText(EtransCur.EtransMessageTextNum);
			x277=new X277(MessageText);
			FillHeader();
			FillGrid();
		}

		private void FillHeader() {
			//Set the title of the window to include he reponding entity type and name (i.e. payor delta, clearinghouse emdeon, etc...)
			Text+=x277.GetInformationSourceType()+" "+x277.GetInformationSourceName();
			//Fill the textboxes in the upper portion of the window.
			textReceiptDate.Text=x277.GetInformationSourceReceiptDate().ToShortDateString();
			textProcessDate.Text=x277.GetInformationSourceProcessDate().ToShortDateString();
			textQuantityAccepted.Text=x277.GetQuantityAccepted().ToString();
			textQuantityRejected.Text=x277.GetQuantityRejected().ToString();
			textAmountAccepted.Text=x277.GetAmountAccepted().ToString("F");
			textAmountRejected.Text=x277.GetAmountRejected().ToString("F");
		}

		private void FillGrid() {
			List<string> claimTrackingNumbers=x277.GetClaimTrackingNumbers();
			//bool showInstBillType=false;
			bool showServiceDateRange=false;
			for(int i=0;i<claimTrackingNumbers.Count;i++) {
				string[] claimInfo=x277.GetClaimInfo(claimTrackingNumbers[i]);
				//if(claimInfo[5]!="") { //institutional type of bill
				//  showInstBillType=true;
				//}
				if(claimInfo[7]!="") {//service date end
					showServiceDateRange=true;
				}
			}
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col;
			if(showServiceDateRange) {
				col=new ODGridColumn(Lan.g(this,"ServDateFrom"),86,HorizontalAlignment.Center);
				gridMain.Columns.Add(col);
				col=new ODGridColumn(Lan.g(this,"ServDateTo"),80,HorizontalAlignment.Center);
				gridMain.Columns.Add(col);
			}
			else {
				col=new ODGridColumn(Lan.g(this,"ServiceDate"),80,HorizontalAlignment.Center);
				gridMain.Columns.Add(col);
			}
			col=new ODGridColumn(Lan.g(this,"Status"),54,HorizontalAlignment.Center);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"LName"),100);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"FName"),100);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"ClaimIdentifier"),140);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"PayorControlNum"),0);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			for(int i=0;i<claimTrackingNumbers.Count;i++) {
				string[] claimInfo=x277.GetClaimInfo(claimTrackingNumbers[i]);
			  ODGridRow row=new ODGridRow();
				row.Cells.Add(new ODGridCell(claimInfo[6]));//service date start
				if(showServiceDateRange) {					
					row.Cells.Add(new ODGridCell(claimInfo[7]));//service date end
				}
				string claimStatus="";
				if(claimInfo[3]=="A") {
					claimStatus="Accepted";
				}
				else if(claimInfo[3]=="R") {
					claimStatus="Rejected";
				}
				row.Cells.Add(new ODGridCell(claimStatus));//status
				row.Cells.Add(new ODGridCell(claimInfo[0]));//lname
				row.Cells.Add(new ODGridCell(claimInfo[1]));//fname
				row.Cells.Add(new ODGridCell(claimTrackingNumbers[i]));//claim identifier
				row.Cells.Add(new ODGridCell(claimInfo[4]));//payor control number
			  gridMain.Rows.Add(row);
			}			
			gridMain.EndUpdate();
		}

		private void butRawMessage_Click(object sender,EventArgs e) {
			MsgBoxCopyPaste msgbox=new MsgBoxCopyPaste(MessageText);
			msgbox.ShowDialog();
		}

		private void butPrint_Click(object sender,EventArgs e) {
			////only visible in Message mode.
			//pagesPrinted=0;
			//PrintDocument pd=new PrintDocument();
			//pd.PrintPage += new PrintPageEventHandler(this.pd_PrintPage);
			//pd.DefaultPageSettings.Margins=new Margins(25,25,40,80);
			////pd.OriginAtMargins=true;
			//if(pd.DefaultPageSettings.PrintableArea.Height==0) {
			//  pd.DefaultPageSettings.PaperSize=new PaperSize("default",850,1100);
			//}
			//headingPrinted=false;
			//try {
			//  #if DEBUG
			//    FormRpPrintPreview pView = new FormRpPrintPreview();
			//    pView.printPreviewControl2.Document=pd;
			//    pView.ShowDialog();
			//  #else
			//    if(PrinterL.SetPrinter(pd,PrintSituation.Default)) {
			//      pd.Print();
			//    }
			//  #endif
			//}
			//catch {
			//  MessageBox.Show(Lan.g(this,"Printer not available"));
			//}
		}

		private void pd_PrintPage(object sender,System.Drawing.Printing.PrintPageEventArgs e) {
			//Rectangle bounds=e.MarginBounds;
			////new Rectangle(50,40,800,1035);//Some printers can handle up to 1042
			//Graphics g=e.Graphics;
			//string text;
			//Font headingFont=new Font("Arial",12,FontStyle.Bold);
			//Font subHeadingFont=new Font("Arial",10,FontStyle.Bold);
			//int yPos=bounds.Top;
			//int center=bounds.X+bounds.Width/2;
			//#region printHeading
			//if(!headingPrinted) {
			//  text=Lan.g(this,"Electronic Benefits Response");
			//  g.DrawString(text,headingFont,Brushes.Black,center-g.MeasureString(text,headingFont).Width/2,yPos);
			//  yPos+=(int)g.MeasureString(text,headingFont).Height;
			//  InsSub sub=InsSubs.GetSub(this.SubNum,new List<InsSub>());
			//  InsPlan plan=InsPlans.GetPlan(this.PlanNum,new List<InsPlan>());
			//  Patient subsc=Patients.GetPat(sub.Subscriber);
			//  text=Lan.g(this,"Subscriber: ")+subsc.GetNameFL();
			//  g.DrawString(text,subHeadingFont,Brushes.Black,center-g.MeasureString(text,subHeadingFont).Width/2,yPos);
			//  yPos+=(int)g.MeasureString(text,subHeadingFont).Height;
			//  Carrier carrier=Carriers.GetCarrier(plan.CarrierNum);
			//  if(carrier.CarrierNum!=0) {//not corrupted
			//    text=carrier.CarrierName;
			//    g.DrawString(text,subHeadingFont,Brushes.Black,center-g.MeasureString(text,subHeadingFont).Width/2,yPos);
			//  }
			//  yPos+=20;
			//  headingPrinted=true;
			//  headingPrintH=yPos;
			//}
			//#endregion
			//yPos=gridMain.PrintPage(g,pagesPrinted,bounds,headingPrintH);
			//pagesPrinted++;
			//if(yPos==-1) {
			//  e.HasMorePages=true;
			//}
			//else {
			//  e.HasMorePages=false;
			//}
			//g.Dispose();
		}

		private void butOK_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.OK;
		}
		
	}
}