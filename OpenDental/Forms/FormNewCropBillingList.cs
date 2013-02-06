using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OpenDental.UI;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormNewCropBillingList:Form {

		private string html;

		public FormNewCropBillingList(string pHtml) {
			InitializeComponent();
			html=pHtml;
		}

		private void FormBillingList_Load(object sender,EventArgs e) {
			try {
				html=html.Replace("&nbsp;"," ");
				int tableRootStartIndex=html.IndexOf("<table")+7;//Should always exist.
				int tableDataStartIndex=html.IndexOf("<table",tableRootStartIndex)+7;//Should always exist.
				int tableDataLength=html.Substring(tableDataStartIndex).IndexOf("</table");//Should always exist. Length of the data between the <table> and </table> tags.
				string htmlTableData=html.Substring(tableDataStartIndex,tableDataLength);//Excludes the <table> and </table> tags.
				//Now take the HTML table data and divide it into HTML table rows using <tr> and </tr> tags.
				List<string> tableRows=new List<string>();
				for(int i=0;i<htmlTableData.Length;) {
					int rowStartIndex=htmlTableData.IndexOf("<tr",i);
					if(rowStartIndex<0) {//If we passed the last row, then break out of the loop.
						break;
					}
					rowStartIndex+=4;//To get inside the <tr> tag
					int rowLength=htmlTableData.Substring(rowStartIndex).IndexOf("</tr");//Length of the row between the <tr> and </tr> tags.
					tableRows.Add(htmlTableData.Substring(rowStartIndex,rowLength).Trim());//Excludes the <tr> and </tr> tags.
					i+=rowLength+9;//Advance 4 for length of <tr>, 5 for length of </tr>. 4+5=9.
				}
				RefreshGridColumns();
				//Available Columns from NewCrop are:
				//0: Blank (for the "Select" link)
				//1: Parent (always "OpenDental")
				//2: AccountName (Customer Business Name)
				//3: ShortName ("OpenDental-" followed by OD assigned account number followed by "-1")
				//4: LocationName (Practice or clinic name for real providers, "Default Location/WorkgroupOpenDental-" followed by OD assigned account number followed by "-1" for test providers)
				//5: FirstLastName (First and last name of provider for real providers, "Doctor D. Test MD" for test providers)
				//6: DeaNumber (DEA number for real providers, "Dea2" for test providers)
				//7: NPI (NPI for real providers, blank for test providers)
				//8: ExternalID (OD ProvNum for real providers, blank for test providers)
				//9: SPIRoot (A 10 digit number for real providers, blank for test providers)
				//10: SPILocation ("002" for real providers, blank for test providers),
				//11: Address (Address of real provider for both test accounts and real accounts)
				//12: AddressLine1 (Address line 2 of real provider, blank for test accounts)
				//13: CityStateZip (City state and zip of real provider for both test accounts and real accounts, with a comma between state and zip)
				//14: PrimaryPhone (10 digit phone number without dashes for real providers, "555-555-1212" for test providers)
				//15: Fax (10 digit phone number without dashes for real providers, blank for test providers)
				//16: StateLicenseNumber (State license number for real providers, "State2" for test providers)
				//17: InternalValidationStatus (Obviously this is a NewCrop internal field. Some acceptable values are 0 and 32)
				//18: DateAdded (The date that the real or test provider was created within NewCrop)
				//19: IsActive (0 if inactive, 1 if active)
				//20: Pager (usually blank)
				//21: CellPhone (usually blank)
				//22: Email (usually blank)
				//23: DoctorType (usually "D". I think D stands for Dental, and they probably use "M" for medical)
				gridBillingList.BeginUpdate();
				gridBillingList.Rows.Clear();
				for(int i=1;i<tableRows.Count;i++) { //Skip the first row, because it contains the column names.
					string tr=tableRows[i];
					//Split the row into cells.
					List<string> trCells=new List<string>();
					for(int j=0;j<tr.Length;) {
						int cellStartIndex=tr.IndexOf("<td",j);
						if(cellStartIndex<0) { //End of row found.
							break;
						}
						cellStartIndex+=4;//To get inside of the <td> tag.
						int cellLength=tr.Substring(cellStartIndex).IndexOf("</td");//Length of the cell between the <td> and </td> tags.
						string cellData=tr.Substring(cellStartIndex,cellLength);//Excludes the <td> and </td> tags.
						trCells.Add(cellData);
						j+=cellLength+9;//Advance 4 for length of <td>, 5 for length of </td>. 4+5=9.
					}
					if(trCells[8].Trim()=="") {//ProvNum is blank.
						//ProvNum will be a natural number if the provider clicked over from OD to NewCrop and accepted the fees.
						//Therefore if ProvNum is blank, then the provider is an account created by NewCrop staff (all examples so far are test accounts, which we do not want to bill).
						continue;//Do not show NewCrop test providers.
					}
					string shortName=trCells[3];
					int accountIdStartIndex=shortName.IndexOf("-")+1;
					int accountIdLength=shortName.Substring(accountIdStartIndex).LastIndexOf("-");
					string accountId=shortName.Substring(accountIdStartIndex,accountIdLength);
					int patNumLength=accountId.IndexOf("-");
					string patNum=accountId.Substring(0,patNumLength);
					if(patNum=="6566") {//Account 6566 corresponds to our software key in the training database. These accounts are test accounts.
						continue;//Do not show OD test accounts.
					}
					ODGridRow gr=new ODGridRow();
					//PatNum
					gr.Cells.Add(new ODGridCell(patNum));
					//Phone
					gr.Cells.Add(new ODGridCell(trCells[14]));
					//PracticeTitle
					gr.Cells.Add(new ODGridCell(trCells[2]));
					//ClinicOrTitle
					gr.Cells.Add(new ODGridCell(trCells[4]));
					//FirstLastName
					gr.Cells.Add(new ODGridCell(trCells[5]));
					//Address
					gr.Cells.Add(new ODGridCell(trCells[11]));
					//Address2
					gr.Cells.Add(new ODGridCell(trCells[12]));
					//CityStateZip
					gr.Cells.Add(new ODGridCell(trCells[13]));
					//DateAdded
					string datet=trCells[18];
					string date=datet.Substring(0,datet.IndexOf(" "));
					gr.Cells.Add(new ODGridCell(date));
					//NPI
					gr.Cells.Add(new ODGridCell(trCells[7]));
					gridBillingList.Rows.Add(gr);
				}
				gridBillingList.EndUpdate();
			}
			catch(Exception ex) {
				MessageBox.Show("There is something wrong with the HTML code. Try again. If issue persists, then contact a programmer: "+ex.Message);
			}
		}

		private void RefreshGridColumns() {
			gridBillingList.BeginUpdate();
			gridBillingList.Columns.Clear();
			int gridWidth=this.Width-50;
			int patNumWidth=54;//fixed width
			int phoneWidth=70;//fixed width
			int address2Width=70;//fixed width
			int dateAddedWidth=68;//fixed width
			int npiWidth=70;//fixed width
			int variableWidth=gridWidth-patNumWidth-phoneWidth-address2Width-dateAddedWidth-npiWidth;
			int practiceTitleWidth=variableWidth/5;
			int clinicOrTitleWidth=practiceTitleWidth;
			int firstLastNameWidth=practiceTitleWidth;
			int addressWidth=practiceTitleWidth;
			int cityStateZipWidth=variableWidth-practiceTitleWidth-clinicOrTitleWidth-firstLastNameWidth-addressWidth;
			gridBillingList.Columns.Add(new ODGridColumn("PatNum",patNumWidth,HorizontalAlignment.Center));//0
			gridBillingList.Columns.Add(new ODGridColumn("Phone",phoneWidth,HorizontalAlignment.Center));//1
			gridBillingList.Columns.Add(new ODGridColumn("PracticeTitle",practiceTitleWidth,HorizontalAlignment.Left));//2
			gridBillingList.Columns.Add(new ODGridColumn("ClinicOrTitle",clinicOrTitleWidth,HorizontalAlignment.Left));//3
			gridBillingList.Columns.Add(new ODGridColumn("FirstLastName",firstLastNameWidth,HorizontalAlignment.Left));//4
			gridBillingList.Columns.Add(new ODGridColumn("Address",addressWidth,HorizontalAlignment.Left));//5
			gridBillingList.Columns.Add(new ODGridColumn("Address2",address2Width,HorizontalAlignment.Left));//6
			gridBillingList.Columns.Add(new ODGridColumn("CityStateZip",cityStateZipWidth,HorizontalAlignment.Left));//7
			gridBillingList.Columns.Add(new ODGridColumn("DateAdded",dateAddedWidth,HorizontalAlignment.Center));//8
			gridBillingList.Columns.Add(new ODGridColumn("NPI",npiWidth,HorizontalAlignment.Center));//9
			gridBillingList.EndUpdate();
		}

		private void FormBillingList_Resize(object sender,EventArgs e) {
			RefreshGridColumns();
		}

		private void butProcess_Click(object sender,EventArgs e) {
			int numChargesAdded=0;
			for(int i=0;i<gridBillingList.Rows.Count;i++) {
				DateTime dateAdded=PIn.Date(gridBillingList.Rows[i].Cells[8].Text);
				//TODO: warn if older than 3 months.
				//if(PIn.Date(textDateStart.Text)<DateTime.Today.AddMonths(-1)) {
				//  MsgBox.Show(this,"Start date cannot be more than a month in the past.  But you can still enter previous charges manually in the account.");
				//  return;
				//}
				long patNum=PIn.Long(gridBillingList.Rows[i].Cells[0].Text);
				string npi=PIn.String(gridBillingList.Rows[i].Cells[9].Text);
				RepeatCharge RepeatCur=RepeatCharges.GetForNewCrop(patNum,npi);
				if(RepeatCur==null) {//No such repeating charge exists yet.
					RepeatCur=new RepeatCharge();
					RepeatCur.IsNew=true;
					RepeatCur.PatNum=patNum;
					RepeatCur.ProcCode="NewCrop";
					RepeatCur.ChargeAmt=15;
					RepeatCur.DateStart=dateAdded;
					RepeatCur.Note="NPI="+npi;
					RepeatCharges.Insert(RepeatCur);
					numChargesAdded++;
				}
			}
			MessageBox.Show("Done. Number of repeating charges added: "+numChargesAdded);
		}

		private void butClose_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	}
}
