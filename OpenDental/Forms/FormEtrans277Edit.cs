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
			for(int i=0;i<x277.FunctGroups[0].Transactions[0].Segments.Count;i++) {
				X12Segment seg=x277.FunctGroups[0].Transactions[0].Segments[i];
				if(seg.SegmentID=="NM1") {
					if(seg.Get(1)=="AY") {
						Text+="Clearinghouse "+seg.Get(3);
						break;
					}
					else if(seg.Get(1)=="PR") {
						Text+="Payer "+seg.Get(3);
						break;
					}
				}
			}
			//Fill the textboxes in the upper portion of the window.

		}

		private void FillGrid() {
			//gridMain.BeginUpdate();
			//gridMain.Columns.Clear();
			//ODGridColumn col=new ODGridColumn(Lan.g(this,"Response"),420);
			//gridMain.Columns.Add(col);
			//gridMain.Rows.Clear();
			//ODGridRow row;
			//for(int i=0;i<listEB.Count;i++) {
			//  row=new ODGridRow();
			//  gridMain.Rows.Add(row);
			//}
			//gridMain.EndUpdate();
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			//if(e.Col==0) {//raw benefit
			//  //FormEtrans270EBraw FormE=new FormEtrans270EBraw();
			//  //FormE.EB271val=listEB[e.Row];
			//  //FormE.ShowDialog();
			//  ////user can't make changes, so no need to refresh grid.
			//}
			//else {//generated benefit
			//  if(listEB[e.Row].Benefitt==null) {//create new benefit
			//    listEB[e.Row].Benefitt=new Benefit();
			//    FormBenefitEdit FormB=new FormBenefitEdit(0,PlanNum);
			//    FormB.IsNew=true;
			//    FormB.BenCur=listEB[e.Row].Benefitt;
			//    FormB.ShowDialog();
			//    if(FormB.BenCur==null) {//user deleted or cancelled
			//      listEB[e.Row].Benefitt=null;
			//    }
			//  }
			//  else {//edit existing benefit
			//    FormBenefitEdit FormB=new FormBenefitEdit(0,PlanNum);
			//    FormB.BenCur=listEB[e.Row].Benefitt;
			//    FormB.ShowDialog();
			//    if(FormB.BenCur==null) {//user deleted
			//      listEB[e.Row].Benefitt=null;
			//    }
			//  }
			//  FillGrid();
			//}
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

		private void butDelete_Click(object sender,EventArgs e) {
			////This button is not visible if IsNew
			//if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"Delete entire request and response?")) {
			//  return;
			//}
			//if(EtransAck271!=null) {
			//  EtransMessageTexts.Delete(EtransAck271.EtransMessageTextNum);
			//  Etranss.Delete(EtransAck271.EtransNum);
			//}
			//EtransMessageTexts.Delete(EtransCur.EtransMessageTextNum);
			//Etranss.Delete(EtransCur.EtransNum);
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

	

		
	}
}