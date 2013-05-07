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
		private X835 x835;

		public FormEtrans835Edit() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormEtrans835Edit_Load(object sender,EventArgs e) {
			MessageText=EtransMessageTexts.GetMessageText(EtransCur.EtransMessageTextNum);
			x835=new X835(MessageText);
			FillAll();
		}

		private void FormEtrans835Edit_Resize(object sender,EventArgs e) {
			//This funciton is called before FormEtrans835Edit_Load() when using ShowDialog(). Therefore, x835 is null the first time FormEtrans835Edit_Resize() is called.
			if(x835==null) {
				return;
			}
			FillClaimDetails();//Because the grid columns change size depending on the form size.
		}

		///<summary>Reads the X12 835 text in the MessageText variable and displays the information in this form.</summary>
		private void FillAll() {
			//*835 has 3 parts: Table 1 (header), Table 2 (claim level details, one CLP segment for each claim), and Table 3 (PLB: provider/check level details).
			FillHeader();//Table 1
			FillClaimDetails();//Table 2
			FillProviderAdjustmentDetails();//Table 3
			//The following concepts should each be addressed as development progresses.
			//*837 CLM01 -> 835 CLP01 (even for split claims)
			//*Reassociation (pg. 19): 835 TRN = Reassociation Key Segment. See TRN02.
			//*SVC02-(CAS03+CAS06+CAS09+CAS12+CAS15+CAS18)=SVC03
			//When the service payment information loop is not present, then: CLP03-(CAS03+CAS06+CAS09+CAS12+CAS15+CAS18)=CLP04
			//*Otherwise, CAS must also be considered from the service adjustment segment.
			//*Reassociation (pg. 20): Use the trace # in TRN02 and the company ID number in TRN03 to uniquely identify the claim payment/data.
			//*Institutional (pg. 23): CAS reason code 78 requires special handling.
			//*Advance payments (pg. 23): in PLB segment with adjustment reason code PI. Can be yearly or monthly.
			//*Bundled procs (pg. 27): have the original proc listed in SV06. Use Line Item Control Number to identify the original proc line.
			//*Line Item Control Number (pgs. 28 & 36): REF*6B or LX01 from 837 -> 2110REF in 835. We are not using REF*6B in 837, so we will get LX01 back in 835.
			//*Predetermination (pg. 28): Identified by claim status code 25 in CLP02. Claim adjustment reason code is 101.
			//*Claim reversals (pg. 30): Identified by code 22 in CLP02. The original claim adjustment codes can be found in CAS01 to negate the original claim.
			//Use CLP07 to identify the original claim, or if different, get the original ref num from REF02 of 2040REF*F8.
			//*Interest and Prompt Payment Discounts (pg. 31): Located in AMT segments with qualifiers I (interest) and D8 (discount). Found at claim and provider/check level.
			//Not part of AR, but part of deposit. Handle this situation by using claimprocs with 2 new status, one for interest and one for discount? Would allow reports, deposits, and claim checks to work as is.
			//*Capitation and related payments or adjustments (pg. 34 & 52): Not many of our customers use capitation, so this will probably be our last concern.
			//*Claim splits (pg. 36): MIA or MOA segments will exist to indicate the claim was split.
			//*Service Line Splits (pg. 42): LQ segment with LQ01=HE and LQ02=N123 indicate the procedure was split.
			//*PPOs (pg. 47): 2100CAS or 2110CAS will contain the value CO (Contractual Obligation) in CAS01. The PPO name is reported in REF02 of the Other Claim Related Information segment REF*CE.
		}

		///<summary>Reads the X12 835 text in the MessageText variable and displays the information from Table 1 (Header).</summary>
		private void FillHeader() {
			//Payer information
			textPayerName.Text=x835.GetPayerName();
			textPayerID.Text=x835.GetPayerID();
			textPayerAddress1.Text=x835.GetPayerAddress1();
			textPayerCity.Text=x835.GetPayerCityName();
			textPayerState.Text=x835.GetPayerState();
			textPayerZip.Text=x835.GetPayerZip();
			textPayerContactInfo.Text=x835.GetPayerContactInfo();
			//Payee information
			textPayeeName.Text=x835.GetPayeeName();
			textPayeeIdType.Text=x835.GetPayeeIdType();
			textPayeeID.Text=x835.GetPayeeId();
			//Payment information
			textTransHandlingDesc.Text=x835.GetTransactionHandlingCodeDescription();
			textPaymentMethod.Text=x835.GetPaymentMethodDescription();
			textPaymentAmount.Text=x835.GetPaymentAmount();
			textCreditOrDebit.Text=x835.GetCreditDebit();
			textAcctNumEndingIn.Text=x835.GetAccountNumReceivingShort();
			DateTime dateEffective=x835.GetDateEffective();
			if(dateEffective.Year>1880) {
				textDateEffective.Text=dateEffective.ToShortDateString();
			}
			textCheckNumOrRefNum.Text=x835.GetTransactionReferenceNumber();			
		}

		///<summary>Reads the X12 835 text in the MessageText variable and displays the information from Table 2 (Detail).</summary>
		private void FillClaimDetails() {
		}

		///<summary>Reads the X12 835 text in the MessageText variable and displays the information from Table 3 (Summary).</summary>
		private void FillProviderAdjustmentDetails() {
			string provAdjNPI=x835.GetProviderLevelAdjustmentNPI();
			DateTime dateFiscalPeriod=x835.GetProviderLevelAdjustmentFiscalPeriodDate();
			if(provAdjNPI!="") {
				gridProviderAdjustments.Title="Provider Adjustments for NPI: "+provAdjNPI+" Fiscal Period: "+dateFiscalPeriod.ToShortDateString();
			}
			else {
				gridProviderAdjustments.Title="Provider Adjustments (none)";
			}
			gridProviderAdjustments.BeginUpdate();
			gridProviderAdjustments.Columns.Clear();
			gridProviderAdjustments.Columns.Add(new ODGridColumn("ReasonCode",80));
			gridProviderAdjustments.Columns.Add(new ODGridColumn("RefIdent",100));
			gridProviderAdjustments.Columns.Add(new ODGridColumn("Description",220));
			gridProviderAdjustments.Columns.Add(new ODGridColumn("Amount",100));
			gridProviderAdjustments.EndUpdate();
			gridProviderAdjustments.BeginUpdate();
			gridProviderAdjustments.Rows.Clear();
			List<string[]> providerAdjustments=x835.GetProviderLevelAdjustments();
			for(int i=0;i<providerAdjustments.Count;i++) {
				ODGridRow row=new ODGridRow();
				row.Cells.Add(new ODGridCell(providerAdjustments[i][0]));//ReasonCode
				row.Cells.Add(new ODGridCell(providerAdjustments[i][1]));//RefIdent
				row.Cells.Add(new ODGridCell(providerAdjustments[i][2]));//Description
				row.Cells.Add(new ODGridCell(providerAdjustments[i][3]));//Amount
				gridProviderAdjustments.Rows.Add(row);
			}
			gridProviderAdjustments.EndUpdate();
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