using CodeBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using OpenDental.UI;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormRxSend:Form {
		private List<RxPat> listRx;

		public FormRxSend() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormRxSend_Load(object sender,EventArgs e) {
			FillGrid();
			gridMain.SetSelected(true);
		}

		private void FillGrid() {
			if(PharmacyC.Listt.Count<1) {
				MsgBox.Show(this,"Need to set up at least one pharmacy.");
				return;
			}
			listRx=RxPats.GetQueue();
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TableQueue","Patient"),150);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableQueue","Provider"),150);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableQueue","Rx"),150);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableQueue","Pharmacy"),150);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<listRx.Count;i++) {
				Patient patCur=Patients.GetLim(listRx[i].PatNum);
				row=new ODGridRow();
				row.Cells.Add(Patients.GetNameLF(patCur.LName,patCur.FName,patCur.Preferred,patCur.MiddleI));
				row.Cells.Add(Providers.GetAbbr(listRx[i].ProvNum));
				row.Cells.Add(listRx[i].Drug);
				row.Cells.Add(Pharmacies.GetDescription(listRx[i].PharmacyNum));
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}
		
		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			Patient patCur=Patients.GetLim(listRx[e.Row].PatNum);
			FormRxEdit FormRE=new FormRxEdit(patCur,listRx[e.Row]);
			FormRE.ShowDialog();
			FillGrid();
		}

		///<summary>Converts any string to an acceptable format for SCRIPT. Converts to all caps and strips off all invalid characters.</summary>
		private static string Sout(string intputStr){
			string retStr=intputStr.ToUpper();
			retStr=Regex.Replace(retStr,//replaces characters in this input string
				//Allowed: A-Z, a-z, 0-9, and any printable character.  But we will allow a smaller set of printable characters.
				//Allowed: #!$%& *_-  We can allow more characters later as needed.
				//Do not allow: :+/\ because we are using those for delimiters in test environment
				"[^\\w#!\\$%& \\*_-]",//[](any single char)^(that is not)\w(A-Z or 0-9) or one of the above chars.
				"");
			retStr=retStr.Trim();//removes leading and trailing spaces.
			return retStr;
		}

		private void butSend_Click(object sender,EventArgs ea) {
			if(gridMain.SelectedIndices.Length!=1) {
				MsgBox.Show(this,"Exactly one Rx must be selected.");
				return;
			}
			Pharmacy pharmacy=Pharmacies.GetOne(listRx[gridMain.SelectedIndices[0]].PharmacyNum);
			//for(int i=1;i<gridMain.SelectedIndices.Length;i++) {
			//	if(listRx[gridMain.SelectedIndices[i]].PharmacyNum!=pharmacy.PharmacyNum) {
			//		MsgBox.Show(this,"All prescriptions must have the same pharmacy.");
			//		return;
			//	}
			//}
			//Use pharmacy object that was set above because all perscriptions sent MUST have the same pharmacy.
			StringBuilder strb=new StringBuilder();
			//These characters will be replaced in a production by unprintable characters, but hardcoded for debugging.
			char f=':';//separates fields within a composite element
			char e='+';//(separates composite elements) SureScripts may require an unprintable character here.
			char d='.';//decimal notation
			char r='/';//release indicator
			char p='*';//repetition separator
			char s='\'';//segment separator
			#if DEBUG
				if(true){
					//Set false if you want to use unprintable characters to simulate running in release mode. 
				}
				else{
					//f=''; we don't know the values for these characters yet.
					//e='';
					//d='';
					//r='';
					//p='';
					//s='';
				}
			#else
				//f=''; we don't know the values for these characters yet.
				//e='';
				//d='';
				//r='';
				//p='';
				//s='';
			#endif
			DateTime msgTimeSent=DateTime.Now;
			//for(int i=0;i<gridMain.SelectedIndices.Length;i++) {//Loop through and send all rx's
			//Hardcoded values should never change. Ex:Message type, version, release should always be SCRIPT:010:006
			//Hardcoded values allowed to change until released version.
			//UNA:+./*'------------------------------------------------------------------------------------------------
			strb.Append("UNA"+f+e+d+r+p+s);
			//UIB+UNOA:0++1234567+++77777777:C:PASSWORDQ+7701630:P+19971001:081522'------------------------------------
			strb.Append("UIB"+e);//000
			strb.Append("UNOA"+f+"0"+e);//010 Syntax identifier and version 
			strb.Append(e);//020 not used
			strb.Append("1234567"+e);//030 Transaction reference (Clinic system trace number.)
			strb.Append(e);//040 not used 
			strb.Append(e);//050 not used
			strb.Append("77777777"+f+"C"+f+"PASSWORDQ"+e);//060 Sender identification (This is the Clinic ID of the sender; C means it is a Clinic.)
			strb.Append("7701630"+f+"P"+e);//070 Recipient ID (NCPDP Provider ID Number of pharmacy; P means it is a pharmacy.)
			strb.Append(msgTimeSent.ToString("yyyyMMdd")+f+msgTimeSent.ToString("HHmmss")+s);//080 Date of initiation CCYYMMDD:HHMMSS,S 
			//UIH+SCRIPT:010:006:NEWRX+110072+++19971001:081522'-------------------------------------------------------
			strb.Append("UIH"+e);//000
			strb.Append("SCRIPT"+f+"010"+f+"006"+f+"NEWRX"+e);//010 Message type:version:release:function.
			//Clinic's reference number for message. Usually this is the folio number for the patient. However, this is the ID by which the clinic will be able to refer to this prescription.
			strb.Append("110072"+e);//020 Message reference number (Must match number in UIT segment below, must be unique. Recommend using rx num) 
			strb.Append(e);//030 conditional Dialogue Reference
			strb.Append(e);//040 not used
			strb.Append(msgTimeSent.ToString("yyyyMMdd")+f+msgTimeSent.ToString("HHmmss")+s);//050 Date of initiation
			RxPat rx=listRx[gridMain.SelectedIndices[0]];
			Patient pat=Patients.GetPat(rx.PatNum);
			Provider prov=Providers.GetProv(rx.ProvNum);
			PatPlan patPlan=PatPlans.GetPatPlan(pat.PatNum,1);
			Family fam=Patients.GetFamily(pat.PatNum);
			List<InsSub> subList=InsSubs.RefreshForFam(fam);
			List<InsPlan> planList=InsPlans.RefreshForSubList(subList);
			InsPlan ins=InsPlans.GetPlan(patPlan.PlanNum,planList);
			InsSub sub=InsSubs.GetOne(patPlan.InsSubNum);
			Carrier car=Carriers.GetCarrier(ins.CarrierNum);
			//PVD+P1+7701630:D3+++++MAIN STREET PHARMACY++6152205656:TE'-----------------------------------------------
			strb.Append("PVD"+e);//000
			strb.Append("P1"+e);//010 Provider coded (see external code list pg.231)
			strb.Append("7701630"+f+"D3"+e);//020 Reference number and qualifier (Pharmacy ID)
			strb.Append(e);//030 not used
			strb.Append(e);//040 conditional Provider specialty
			strb.Append(e);//050 conditional The name of the prescriber or pharmacist or supervisor
			strb.Append(e);//060 not used 
			strb.Append(e);//070 conditional The clinic or pharmacy name
			strb.Append(Sout(pharmacy.Address)+e);//080 Address
			strb.Append(Regex.Replace(Sout(pharmacy.Phone),@"[-()]",string.Empty)+f+"TE"+s);//090 Communication number and qualifier
			//PVD+PC+6666666:0B+++JONES:MARK++++6152219800:TE'---------------------------------------------------------
			strb.Append("PVD"+e);//000 
			strb.Append("PC"+e);//010 Provider coded
			strb.Append("6666666"+f+"0B"+e);//020 Reference number and qualifier (0B: Provider State License Number)
			strb.Append(e);//030 not used
			strb.Append(e);//040 conditional Provider specialty
			strb.Append(Sout(prov.LName)+f+Sout(prov.FName)+e);//050 The name of the prescriber or pharmacist or supervisor
			strb.Append(e);//060 not used
			strb.Append(e);//070 conditional The clinic or pharmacy name
			strb.Append(e);//080 conditional Address
			strb.Append(Regex.Replace(Sout(PrefC.GetString(PrefName.PracticePhone)),@"[-()]",string.Empty)+f+"TE"+s);//090 Communication number and qualifier
			//PTT++19541225+SMITH:MARY+F+333445555:SY'-----------------------------------------------------------------
			strb.Append("PTT"+e);//000
			strb.Append(e);//010 conditional Individual relationship
			strb.Append(pat.Birthdate.ToString("yyyyMMdd")+e);//020 Birth date of patient YYYYMMDD
			strb.Append(Sout(pat.LName)+f+Sout(pat.FName)+e);//030 Name
			strb.Append(pat.Gender.ToString().Substring(0,1)+e);//040 Gender (M,F,U)
			strb.Append(Sout(pat.SSN)+f+"SY"+s);//050 Patient ID and/or SSN and qualifier
			//COO+123456:BO+INSURANCE COMPANY NAME++123456789++AA112'--------------------------------------------------
			strb.Append("COO"+e);//000
			strb.Append("123456"+f+"BO"+e);//010 Payer ID Information and qualifier (Primary Payer's identification number? BO is for BIN Location Number.)
			strb.Append(Sout(car.CarrierName)+e);//020 Payer name
			strb.Append(e);//030 conditional Service type, coded
			strb.Append(Sout(sub.SubscriberID)+e);//040 Cardholder ID
			strb.Append(e);//050 conditional Cardholder name
			strb.Append(Sout(ins.GroupNum)+s);//060 Group ID
			//DRU------------------------------------------------------------------------------------------------------
			//DRU+P:CALAN SR 240MG::::240:::::::AA:C42998:AB:C28253+::60:38:AC:C48542+:1 TID -TAKE ONE TABLET TWO TIMES A DAY UNTIL GONE+85:19971001:102*ZDS:30:804+0+R:1'
			strb.Append("DRU"+e);//000
			//P means prescribed. Drug prescribed is Calan Sr 240mg. 
			//240 is the strength; AA is the Source for NCI Pharmaceutical Dosage Form. C42998 is the code for “Tablet dosing form”.
			//AB is the Source for NCI Units of Presentation. C28253 is the code for “Milligram”. So this means the prescription is for 240mg tablets.
			strb.Append("P"+f+rx.Drug+f+f+f+f+"240"+f+f+f+f+f+f+f+"AA"+f+"C42998"+f+"AB"+f+"C28253"+e);//010 Item Description Identification
			//This means dispense 60 tablets. 38 is the code value for Original Qty. AC is the Source for NCI Potency Units. C48542 is the code for “Tablet dosing unit”.
			strb.Append(f+f+"60"+f+"38"+f+"AC"+f+"C48542"+e);//020 Quantity
			strb.Append(f+Sout(rx.Sig)+e);//030 Directions
			//ZDS is the qualifier for Days Supply. 30 is the number of days supply. 804 is the qualifier for Quantity of Days.
			strb.Append("85"+f+"19971001"+f+"102"+p+"ZDS"+f+"30"+f+"804"+e);//040 Date Note: It is strongly recommended that Days Supply (value “ZDS”) be supported. YYYYMMDD
			strb.Append("0"+e);//050 Product/Service substitution, coded
			strb.Append("R"+f+Sout(rx.Refills)+s);//060 Refill and quantity
			//UIT+110072+6'---------------------------------------------------------------------------------------------
			strb.Append("UIT"+e);//000
			strb.Append("110072"+e);//010 Message reference number
			strb.Append("6"+s);//020 Mandatory field. This is the count of the number of segments in the message including the UIH and UIT
			//UIZ++1'---------------------------------------------------------------------------------------------------
			strb.Append("UIZ"+e);//000
			strb.Append(e);//010 not used
			strb.Append("1"+s);//020 Number of messages per interchange. The count of UIH-UIT occurrences
			//Uncomment if you want to see the message text:
			//MsgBoxCopyPaste msgbox=new MsgBoxCopyPaste(strb.ToString());
			//msgbox.ShowDialog();
			SmtpClient client=new SmtpClient(PrefC.GetString(PrefName.EmailSMTPserver),PrefC.GetInt(PrefName.EmailPort));
			client.Credentials=new NetworkCredential(PrefC.GetString(PrefName.EmailUsername),PrefC.GetString(PrefName.EmailPassword));
			client.DeliveryMethod=SmtpDeliveryMethod.Network;
			client.EnableSsl=PrefC.GetBool(PrefName.EmailUseSSL);
			client.Timeout=180000;//Timeout of 3 minutes (in milliseconds).
			MailMessage message=new MailMessage();
			message.From=new MailAddress(PrefC.GetString(PrefName.EmailSenderAddress));
			message.To.Add(PrefC.GetString(PrefName.EHREmailToAddress));
			message.Subject="SCRIPT for NEWRX";
			message.Body=strb.ToString();
			message.IsBodyHtml=false;
			client.Send(message);
			//}//End of selected Rx loop
			//Remove the Rx from the grid.
			//rx.IsElectQueue=false;
			//RxPats.Update(rx);
			FillGrid();//Refresh the screen so that sent Rx's go away.
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	}
}