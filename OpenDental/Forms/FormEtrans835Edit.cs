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
	public partial class FormEtrans835Edit:Form {

		public Etrans EtransCur;
		private string MessageText;
		//private X277 x277;

		public FormEtrans835Edit() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormEtrans277Edit_Load(object sender,EventArgs e) {
			MessageText=EtransMessageTexts.GetMessageText(EtransCur.EtransMessageTextNum);
			//x277=new X277(MessageText);
			FillHeader();
			FillGrid();
		}

		private void FillHeader() {
			////Set the title of the window to include he reponding entity type and name (i.e. payor delta, clearinghouse emdeon, etc...)
			//Text+=x277.GetInformationSourceType()+" "+x277.GetInformationSourceName();
			////Fill the textboxes in the upper portion of the window.
			//textReceiptDate.Text=x277.GetInformationSourceReceiptDate().ToShortDateString();
			//textProcessDate.Text=x277.GetInformationSourceProcessDate().ToShortDateString();
			//textQuantityAccepted.Text=x277.GetQuantityAccepted().ToString();
			//textQuantityRejected.Text=x277.GetQuantityRejected().ToString();
			//textAmountAccepted.Text=x277.GetAmountAccepted().ToString("F");
			//textAmountRejected.Text=x277.GetAmountRejected().ToString("F");
		}

		private void FillGrid() {
			//List<string> claimTrackingNumbers=x277.GetClaimTrackingNumbers();
			////bool showInstBillType=false;
			//bool showServiceDateRange=false;
			//for(int i=0;i<claimTrackingNumbers.Count;i++) {
			//  string[] claimInfo=x277.GetClaimInfo(claimTrackingNumbers[i]);
			//  //if(claimInfo[5]!="") { //institutional type of bill
			//  //  showInstBillType=true;
			//  //}
			//  if(claimInfo[7]!="") {//service date end
			//    showServiceDateRange=true;
			//  }
			//}
			//gridMain.BeginUpdate();
			//gridMain.Columns.Clear();
			//ODGridColumn col;
			//if(showServiceDateRange) {
			//  col=new ODGridColumn(Lan.g(this,"ServDateFrom"),86,HorizontalAlignment.Center);
			//  gridMain.Columns.Add(col);
			//  col=new ODGridColumn(Lan.g(this,"ServDateTo"),80,HorizontalAlignment.Center);
			//  gridMain.Columns.Add(col);
			//}
			//else {
			//  col=new ODGridColumn(Lan.g(this,"ServiceDate"),80,HorizontalAlignment.Center);
			//  gridMain.Columns.Add(col);
			//}
			//col=new ODGridColumn(Lan.g(this,"Status"),54,HorizontalAlignment.Center);
			//gridMain.Columns.Add(col);
			//col=new ODGridColumn(Lan.g(this,"LName"),showServiceDateRange?110:153);
			//gridMain.Columns.Add(col);
			//col=new ODGridColumn(Lan.g(this,"FName"),showServiceDateRange?110:153);
			//gridMain.Columns.Add(col);
			//col=new ODGridColumn(Lan.g(this,"ClaimIdentifier"),140);
			//gridMain.Columns.Add(col);
			//col=new ODGridColumn(Lan.g(this,"PayorControlNum"),0);
			//gridMain.Columns.Add(col);
			//gridMain.Rows.Clear();
			//for(int i=0;i<claimTrackingNumbers.Count;i++) {
			//  string[] claimInfo=x277.GetClaimInfo(claimTrackingNumbers[i]);
			//  ODGridRow row=new ODGridRow();
			//  row.Cells.Add(new ODGridCell(claimInfo[6]));//service date start
			//  if(showServiceDateRange) {					
			//    row.Cells.Add(new ODGridCell(claimInfo[7]));//service date end
			//  }
			//  string claimStatus="";
			//  if(claimInfo[3]=="A") {
			//    claimStatus="Accepted";
			//  }
			//  else if(claimInfo[3]=="R") {
			//    claimStatus="Rejected";
			//  }
			//  row.Cells.Add(new ODGridCell(claimStatus));//status
			//  row.Cells.Add(new ODGridCell(claimInfo[0]));//lname
			//  row.Cells.Add(new ODGridCell(claimInfo[1]));//fname
			//  row.Cells.Add(new ODGridCell(claimTrackingNumbers[i]));//claim identifier
			//  row.Cells.Add(new ODGridCell(claimInfo[4]));//payor control number
			//  gridMain.Rows.Add(row);
			//}			
			//gridMain.EndUpdate();
		}

		private void butRawMessage_Click(object sender,EventArgs e) {
			MsgBoxCopyPaste msgbox=new MsgBoxCopyPaste(MessageText);
			msgbox.ShowDialog();
		}

		private void butOK_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.OK;
		}
		
	}
}