using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Windows.Forms;
using OpenDental.UI;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormNewCropBilling:Form {

		public FormNewCropBilling() {
			InitializeComponent();
		}

		private void FormBillingList_Load(object sender,EventArgs e) {
		}

		private void FormBillingList_Resize(object sender,EventArgs e) {
			RefreshGridColumns();
		}

		private void butBrowse_Click(object sender,EventArgs e) {
			if(openFileDialog1.ShowDialog()==DialogResult.OK) {
				textBillingXmlPath.Text=openFileDialog1.FileName;
			}
		}

		private void butLoad_Click(object sender,EventArgs e) {
			if(!File.Exists(textBillingXmlPath.Text)) {
				MessageBox.Show("File does not exist or could not be accessed. Make sure the file is not open in another program and try again.");
				return;
			}
			FillGrid();
		}

		private void RefreshGridColumns() {
			gridBillingList.BeginUpdate();
			gridBillingList.Columns.Clear();
			int gridWidth=this.Width-50;
			int patNumWidth=54;//fixed width
			int npiWidth=70;//fixed width
			int yearMonthAddedWidth=104;//fixed width
			int isNewWidth=46;//fixed width
			int variableWidth=gridWidth-patNumWidth-npiWidth-yearMonthAddedWidth-isNewWidth;
			int practiceTitleWidth=variableWidth/2;//variable width
			int firstLastNameWidth=variableWidth-practiceTitleWidth;//variable width
			gridBillingList.Columns.Add(new ODGridColumn("PatNum",patNumWidth,HorizontalAlignment.Center));//0
			gridBillingList.Columns.Add(new ODGridColumn("NPI",npiWidth,HorizontalAlignment.Center));//1
			gridBillingList.Columns.Add(new ODGridColumn("YearMonthAdded",yearMonthAddedWidth,HorizontalAlignment.Center));//2
			gridBillingList.Columns.Add(new ODGridColumn("IsNew",isNewWidth,HorizontalAlignment.Center));//3
			gridBillingList.Columns.Add(new ODGridColumn("PracticeTitle",practiceTitleWidth,HorizontalAlignment.Left));//4
			gridBillingList.Columns.Add(new ODGridColumn("FirstLastName",firstLastNameWidth,HorizontalAlignment.Left));//5			
			gridBillingList.EndUpdate();
		}

		private void FillGrid() {
			try {
				string xmlData=File.ReadAllText(textBillingXmlPath.Text);
				xmlData=xmlData.Replace("&nbsp;","");
				XmlDocument xml=new XmlDocument();
				xml.LoadXml(xmlData);
				XmlNode divNode=xml.FirstChild;
				XmlNode tableNode=divNode.FirstChild;
				RefreshGridColumns();
				gridBillingList.BeginUpdate();
				gridBillingList.Rows.Clear();
				for(int i=1;i<tableNode.ChildNodes.Count;i++) { //Skip the first row, because it contains the column names.
					ODGridRow gr=new ODGridRow();
					XmlNode trNode=tableNode.ChildNodes[i];
					//PatNum
					string shortName=trNode.ChildNodes[1].InnerText;
					int accountIdStartIndex=shortName.IndexOf("-")+1;
					int accountIdLength=shortName.Substring(accountIdStartIndex).LastIndexOf("-");
					string accountId=shortName.Substring(accountIdStartIndex,accountIdLength);
					int patNumLength=accountId.IndexOf("-");
					string patNumStr=PIn.String(accountId.Substring(0,patNumLength));
					if(patNumStr=="6566") {//Account 6566 corresponds to our software key in the training database. These accounts are test accounts.
						continue;//Do not show OD test accounts.
					}
					long patNum=PIn.Long(patNumStr);
					gr.Cells.Add(new ODGridCell(patNumStr));
					//NPI
					string npi=PIn.String(trNode.ChildNodes[8].InnerText);
					gr.Cells.Add(new ODGridCell(npi));
					//YearMonthAdded
					gr.Cells.Add(new ODGridCell(trNode.ChildNodes[9].InnerText));
					//IsNew
					RepeatCharge RepeatCur=RepeatCharges.GetForNewCrop(patNum);
					if(RepeatCur==null || //No such repeating charge exists yet for this account. Therefore, new provider.
						!RepeatChargeGetNpiList(RepeatCur).Contains(npi)) {//The NPI is not part of the repeating charge yet. Therefore, new provider.
						gr.Cells.Add(new ODGridCell("X"));
					}
					else {//Existing provider.
						gr.Cells.Add(new ODGridCell(""));
					}
					//PracticeTitle
					gr.Cells.Add(new ODGridCell(trNode.ChildNodes[0].InnerText));
					//FirstLastName
					gr.Cells.Add(new ODGridCell(trNode.ChildNodes[2].InnerText));
					gridBillingList.Rows.Add(gr);
				}
				gridBillingList.EndUpdate();
			}
			catch(Exception ex) {
				MessageBox.Show("There is something wrong with the input file. Try again. If issue persists, then contact a programmer: "+ex.Message);
			}
		}

		///<summary>The NPIs for the providers using electronic prescriptions are expected in the beginning of the repeating charge note.
		///The note is expected to begin with "NPIs=" followed by a comma separated list of NPI numbers. The note may contain other information, as long
		///as the other information is after the NPI list.</summary>
		private List <string> RepeatChargeGetNpiList(RepeatCharge repeatCharge) {
			string npis=repeatCharge.Note;
			if(!npis.Trim().ToUpper().StartsWith("NPIS=")) {
				return new List<string>();
			}
			npis=npis.Trim().ToUpper().Substring(5).Trim();//everything after the "NPIS=". The Trim() allows for white space between the "NPIs=" and the first NPI number.
			List<string> npiList=new List<string>();
			for(int i=0;i<npis.Length;) {
				int j=i;
				while(j<npis.Length && npis[j]>='0' && npis[j]<='9') {//While the current location is a digit in the note, advance.
					j++;
				}
				if(j==i) {//No digits found.
					break;//No more NPIs.
				}
				string npi=npis.Substring(i,j-i);//index j is not a digit.
				npiList.Add(npi);
				i=j;
				while(i<npis.Length && npis[i]==' ') {//Skip any spaces following the NPI number. This is how we allow spaces before the comma separator.
					i++;
				}
				if(i<npis.Length && npis[i]==',') {//Skip the NPI separator if it exists.
					i++;
				}
				while(i<npis.Length && npis[i]==' ') {//Skip any spaces following the NPI comma separator. This is how we allow spaces after the comma and before the next NPI.
					i++;
				}
			}
			return npiList;
		}

		private void butProcess_Click(object sender,EventArgs e) {
			if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"This will add a new repeating charge for each provider in the list above"
				+" who is new (does not already have a repeating charge), based on PatNum and NPI.  Continue?")) {
				return;
			}
			const int chargePerProv=15;//15$/month
			Cursor=Cursors.WaitCursor;
			int numChargesAdded=0;
			int numSkipped=0;
			for(int i=0;i<gridBillingList.Rows.Count;i++) {
				long patNum=PIn.Long(gridBillingList.Rows[i].Cells[0].Text);
				string npi=PIn.String(gridBillingList.Rows[i].Cells[1].Text);
				RepeatCharge RepeatCur=RepeatCharges.GetForNewCrop(patNum);
				if(RepeatCur==null) {//No such repeating charge exists yet.
					string yearMonth=gridBillingList.Rows[i].Cells[2].Text;
					int year=PIn.Int(yearMonth.Substring(0,4));
					int month=PIn.Int(yearMonth.Substring(4));
					//Match the day of the month for the NewCrop repeating charge to their existing monthly support charge (even if the monthly support is disabled).
					int day=15;//Day 15 will be used if they do not have any existing repeating charges.
					RepeatCharge[] chargesForPat=RepeatCharges.Refresh(patNum);
					bool hasMaintCharge=false;
					for(int j=0;j<chargesForPat.Length;j++) {
						if(chargesForPat[j].ProcCode=="001") {//Monthly maintenance repeating charge
							hasMaintCharge=true;
							day=chargesForPat[j].DateStart.Day;
							break;
						}
					}
					//The customer is not on monthly support, so use any other existing repeating charge day (example EHR Monthly and Mobile).
					if(!hasMaintCharge && chargesForPat.Length>0) {
						day=chargesForPat[0].DateStart.Day;
					}
					DateTime dateBilling=new DateTime(year,month,day);
					if(dateBilling<DateTime.Today.AddDays(-90)) {//The customer was added into NewCrop over 90 days ago. Not really new. Skip and warn.
						numSkipped++;
					}
					else {
						//We consider the provider a new provider and create a new repeating charge.
						RepeatCur=new RepeatCharge();
						RepeatCur.IsNew=true;
						RepeatCur.PatNum=patNum;
						RepeatCur.ProcCode="NewCrop";
						RepeatCur.ChargeAmt=chargePerProv;
						RepeatCur.DateStart=dateBilling;
						RepeatCur.Note="NPIs="+npi;
						RepeatCharges.Insert(RepeatCur);
						numChargesAdded++;
					}
				}
				else { //The repeating charge for NewCrop billing already exists.
					List<string> npiList=RepeatChargeGetNpiList(RepeatCur);
					if(!npiList.Contains(npi)) {//The NPI is not already part of the repeating charge.
						string note="NPIs="+npi;
						if(npiList.Count>0) {//There are some other NPIs already in the list.
							note+=",";//Separate the new NPI from the old ones with a comma.
						}
						if(RepeatCur.Note.ToUpper().StartsWith("NPIS=")) {//The existing note already contains an "NPIS=" prefix.
							note+=RepeatCur.Note.Substring(5);//Append the existing note without the prefix because the prefix was already added above.
						}
						else {//The existing note does not yet have the "NPIS=" prefix.
							note+=RepeatCur.Note;//Simply append the existing note as-is, because the prefix was already added above.
						}
						RepeatCur.Note=note;
						RepeatCur.ChargeAmt=chargePerProv*(npiList.Count+1);//The list length has increased by 1, because of the new NPI.
						RepeatCharges.Update(RepeatCur);
						numChargesAdded++;
					}
				}
			}
			FillGrid();
			Cursor=Cursors.Default;
			string msg="Done. Number of provider charges added: "+numChargesAdded;
			if(numSkipped>0) {
				msg+=Environment.NewLine+"Number skipped due to old DateBilling (over 90 days ago): "+numSkipped;
			}
			MessageBox.Show(msg);
		}

		private void butClose_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	}
}
