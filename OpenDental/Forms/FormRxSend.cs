using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.UI;
using System.IO;

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
			if(gridMain.SelectedIndices.Length<1) {
				MsgBox.Show(this,"Must select at least one Rx.");
				return;
			}
			Pharmacy pharmacy=Pharmacies.GetOne(listRx[gridMain.SelectedIndices[0]].PharmacyNum);
			for(int i=1;i<gridMain.SelectedIndices.Length;i++) {
				if(listRx[gridMain.SelectedIndices[i]].PharmacyNum!=pharmacy.PharmacyNum) {
					MsgBox.Show(this,"All prescriptions must have the same pharmacy.");
					return;
				}
			}
			//Ask Jordan about information like Clinic ID and where we will get/store this stuff.
			//Add special logic for adding multiple perscriptions to one SCRIPT.
			for(int i=0;i<gridMain.SelectedIndices.Length;i++) {
				//Collect all information needed
				RxPat rx=listRx[gridMain.SelectedIndices[i]];
				Patient pat=Patients.GetPat(rx.PatNum);
				Provider prov=Providers.GetProv(rx.ProvNum);
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
				#region SCRIPT
				//Hardcoded values should never change. Ex:Message type, version, release should always be SCRIPT:010:006
				//UNA:+./*'------------------------------------------------------------------------------------------------
				strb.AppendLine("UNA"+f+e+d+r+p+s);
				//UIB+UNOA:Ø++1234567+++77777777:C:PASSWORDQ+77Ø163Ø:P+19971ØØ1:Ø81522’------------------------------------
				strb.Append("UIB"+e);//000
				strb.Append("UNOA"+f+"0"+e);//010 Syntax identifier and version 
				strb.Append(e);//020 not used
				strb.Append("1234567"+e);//030 Transaction reference
				strb.Append(e);//040 not used 
				strb.Append(e);//050 not used
				strb.Append("77777777"+f+"C"+f+"PASSWORDQ"+e);//060 Sender identification
				strb.Append("7701630"+f+"P"+e);//070 Recipient ID
				strb.Append("19971001"+f+"081522"+s);//080 Date of initiation CCYYMMDD:HHMMSS,S
				//UIH+SCRIPT:Ø1Ø:ØØ6:NEWRX+11ØØ72+++19971ØØ1:Ø81522’-------------------------------------------------------
				strb.Append("UIH"+e);//000
				strb.Append("SCRIPT"+f+"010"+f+"006"+f+"NEWRX"+e);//010 Message type:version:release:function.
				strb.Append("110072"+e);//020 Message reference number
				strb.Append(e);//030 conditional Dialogue Reference
				strb.Append(e);//040 not used
				strb.Append("19971001"+f+"081522"+s);//050 Date of initiation
				//PVD+P1+77Ø163Ø:D3+++++MAIN STREET PHARMACY++61522Ø5656:TE’-----------------------------------------------
				strb.Append("PVD"+e);//000
				strb.Append("P1"+e);//010 Provider coded (see external code list in data dictionary)
				strb.Append("7701630"+f+"D3"+e);//020 Reference number and qualifier (ID for facility)
				strb.Append(e);//030 not used
				strb.Append(e);//040 conditional Provider specialty
				strb.Append(e);//050 conditional The name of the prescriber or pharmacist or supervisor
				strb.Append(e);//060 not used 
				strb.Append(e);//070 conditional The clinic or pharmacy name
				strb.Append("MAIN STREET PHARMACY"+e);//080 Address
				strb.Append("6152205656"+f+"TE"+s);//090 Communication number and qualifier
				//PVD+PC+6666666:ØB+++JONES:MARK++++61522198ØØ:TE’---------------------------------------------------------
				strb.Append("PVD"+e);//000 
				strb.Append("PC"+e);//010 Provider coded
				strb.Append("6666666"+f+"0B"+e);//020 Reference number and qualifier (ID for facility)
				strb.Append(e);//030 not used
				strb.Append(e);//040 conditional Provider specialty
				strb.Append("JONES"+f+"MARK"+e);//050 The name of the prescriber or pharmacist or supervisor
				strb.Append(e);//060 not used
				strb.Append(e);//070 conditional The clinic or pharmacy name
				strb.Append(e);//080 conditional Address
				strb.Append("6152205656"+f+"TE"+s);//090 Communication number and qualifier
				//PTT++19541225+SMITH:MARY+F+333445555:SY’-----------------------------------------------------------------
				strb.Append("PTT"+e);//000
				strb.Append(e);//010 conditional Individual relationship
				strb.Append("19541225"+e);//020 Birth date of patient
				strb.Append("SMITH"+f+"MARY"+e);//030 Name
				strb.Append("F"+e);//040 Gender (M,F,U)
				strb.Append("333445555"+f+"SY"+s);//050 Patient ID and/or SSN and qualifier
				//COO+123456:BO+INSURANCE COMPANY NAME++123456789++AA112’--------------------------------------------------
				strb.Append("COO"+e);//000
				strb.Append("123456"+f+"BO"+e);//010 Payer ID Information and qualifier
				strb.Append("INSURANCE COMPANY NAME"+e);//020 Payer name
				strb.Append(e);//030 conditional Service type, coded
				strb.Append("123456789"+e);//040 Cardholder ID
				strb.Append(e);//050 conditional Cardholder name
				strb.Append("AA112"+s);//060 Group ID
				//DRU------------------------------------------------------------------------------------------------------
				//DRU+P:CALAN SR 24ØMG::::24Ø:::::::AA:C42998:AB:C28253+::6Ø:38:AC:C48542+:1 TID -TAKE ONE TABLET TWO TIMES A DAY UNTIL GONE+85:19971ØØ1:1Ø2*ZDS:3Ø:8Ø4+Ø+R:1’
				strb.Append("DRU"+e);//000
				strb.Append("P"+f+"CALAN SR 24ØMG"+f+f+f+f+"24Ø"+f+f+f+f+f+f+f+"AA"+f+"C42998"+f+"AB"+f+"C28253"+e);//010 Item Description Identification
				strb.Append(f+f+"6Ø"+f+"38"+f+"AC"+f+"C48542"+e);//020 Quantity
				strb.Append(f+"1 TID -TAKE ONE TABLET TWO TIMES A DAY UNTIL GONE"+e);//030 Directions
				strb.Append("85"+f+"19971ØØ1"+f+"1Ø2"+p+"ZDS"+f+"3Ø"+f+"8Ø4"+e);//040 Date Note: It is strongly recommended that Days Supply (value “ZDS”) be supported.
				strb.Append("0"+e);//050 Product/Service substitution, coded
				strb.Append("R"+f+"1"+s);//060 Refill and quantity
				//UIT+11ØØ72+6’---------------------------------------------------------------------------------------------
				strb.Append("UIT"+e);//000
				strb.Append("110072"+e);//010 Message reference number
				strb.Append("6"+s);//020 Mandatory field. This is the count of the number of segments in the message including the UIH and UIT
				//UIZ++1’---------------------------------------------------------------------------------------------------
				strb.Append("UIZ"+e);//000
				strb.Append(e);//010 not used
				strb.Append("1"+s);//020 Number of messages per interchange. The count of UIH-UIT occurrences
				#endregion
				string filePath=Path.Combine(Application.StartupPath,"RxScript.txt");
				try {
					File.WriteAllText(filePath,strb.ToString(),Encoding.ASCII);
				}
				catch(Exception ex) {
					MessageBox.Show(ex.Message);
				}
				//The SCRIPT has been created, now send it out.
				//File might contain sensitive info, should we delete the file when done?
			}//End of selected Rx loop
			FillGrid();//Refresh the screen so that sent Rx's go away.
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	}
}