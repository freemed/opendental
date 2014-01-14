using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental;
using OpenDental.UI;
using CodeBase;

namespace OpenDental {
	public partial class FormEhrLabOrders:Form {
		public List<EhrLab> ListEhrLabs;
		public Patient PatCur;

		public FormEhrLabOrders() {
			InitializeComponent();
		}

		private void FormEhrLabOrders_Load(object sender,EventArgs e) {
			FillGrid();
		}

		private void FillGrid() {
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col;
			col=new ODGridColumn("Date Time",80);//Formatted yyyyMMdd
			col.SortingStrategy=GridSortingStrategy.DateParse;
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Placer Order Number",180);//Should be PK but might not be. Instead use Placer Order Num.
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Filler Order Number",180);//Should be PK but might not be. Instead use Placer Order Num.
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Results In",80);//Or date of latest result? or both?
			gridMain.Columns.Add(col);
			ListEhrLabs = EhrLabs.GetAllForPat(PatCur.PatNum);
			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<ListEhrLabs.Count;i++) {
				row=new ODGridRow();
				string dateSt=ListEhrLabs[i].ObservationDateTimeStart.Substring(0,8);//stored in DB as yyyyMMddhhmmss-zzzz
				DateTime dateT=PIn.Date(dateSt.Substring(4,2)+"/"+dateSt.Substring(6,2)+"/"+dateSt.Substring(0,4));
				row.Cells.Add(dateT.ToShortDateString());//date only
				row.Cells.Add(ListEhrLabs[i].PlacerOrderNum);
				row.Cells.Add(ListEhrLabs[i].FillerOrderNum);
				row.Cells.Add(ListEhrLabs[i].ListEhrLabResults.Count.ToString());
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}

		private void butImport_Click(object sender,EventArgs e) {
			MsgBoxCopyPaste MBCP = new MsgBoxCopyPaste("Paste HL7 Lab Message Text Here.");
#if DEBUG
			#region Populate MBCP with a large sample lab that should attach to the current patient
			MBCP.textMain.Text=@"MSH|^~\&#|NIST Test Lab APP^2.16.840.1.113883.3.72.5.20^ISO|NIST Lab Facility^2.16.840.1.113883.3.72.5.21^ISO||NIST EHR Facility^2.16.840.1.113883.3.72.5.23^ISO|20110531140551-0500||ORU^R01^ORU_R01|NIST-LRI-GU-001.01|T|2.5.1|||AL|NE|||||LRI_Common_Component^Profile Component^2.16.840.1.113883.9.16^ISO~LRI_GU_Component^Profile Component^2.16.840.1.113883.9.12^ISO~LRI_RU_Component^Profile Component^2.16.840.1.113883.9.14^ISO
PID|1||1^^^NIST MPI&2.16.840.1.113883.3.72.5.30.2&ISO^MR||Jones^William^A^JR^^^L||19610615|M||2106-3^White^HL70005^CAUC^Caucasian^L
ORC|RE|2^NIST EHR^2.16.840.1.113883.3.72.5.24^ISO|R-783274^NIST Lab Filler^2.16.840.1.113883.3.72.5.25^ISO|GORD874211^NIST EHR^2.16.840.1.113883.3.72.5.24^ISO||||||||57422^Radon^Nicholas^M^JR^DR^^^NIST-AA-1&2.16.840.1.113883.3.72.5.30.1&ISO^L^^^NPI
OBR|1|2^NIST EHR^2.16.840.1.113883.3.72.5.24^ISO|R-783274^NIST Lab Filler^2.16.840.1.113883.3.72.5.25^ISO|30341-2^Erythrocyte sedimentation rate^LN^815115^Erythrocyte sedimentation rate^99USI^^^Erythrocyte sedimentation rate|||20110331140551-0800||||L||7520000^fever of unknown origin^SCT^22546000^fever, origin unknown^99USI^^^Fever of unknown origin|||57422^Radon^Nicholas^M^JR^DR^^^NIST-AA-1&2.16.840.1.113883.3.72.5.30.1&ISO^L^^^NPI||||||20110331160428-0800|||C|||10092^Hamlin^Pafford^M^Sr.^Dr.^^^NIST-AA-1&2.16.840.1.113883.3.72.5.30.1&ISO^L^^^NPI|||||||||||||||||||||CC^Carbon Copy^HL70507^C^Send Copy^L^^^Copied Requested
NTE|1||Patient is extremely anxious about needles used for drawing blood.^We're no strangers to love^You know the rules ... and so do I^A full commitment's what I'm ... thinkin' of^You wouldn't get this from any other guy^I just wanna tell you how I'm feeling^Gotta make you ... understand^^Never gonna give you up^Never gonna let you down^Never gonna run around and desert you^Never gonna make you cry^Never gonna say goodbye^Never gonna tell a lie and hurt you^^We've known each other ... for so long^Your heart's been aching, but ... you're too shy to say it^Inside we both know what's been ... goin' on^We know the game and we're ... gonna play it^And if you ask me how I'm feeling^Don't tell me you're to ... blind to see^^Never gonna give you up^Never gonna let you down^Never gonna run around and desert you^Never gonna make you cry^Never gonna say goodbye^Never gonna tell a lie and hurt you^^Never gonna give you up^Never gonna let you down^Never gonna run around and desert you^Never gonna make you cry^Never gonna say goodbye^Never gonna tell a lie and hurt you^^Oooooooooh ... give you up^Oooooooooh ... give you up^^Never gonna give never gonna give ^Give you up^Never gonna give never gonna give^Give you up^^We've known each other ... for so long^Your heart's been aching, but ... you're too shy to say it^Inside we both know what's been ... goin' on^We know the game and we're ... gonna play it^I just wanna tell you how I'm feeling^Gotta make you ... understand^^Never gonna give you up^Never gonna let you down^Never gonna run around and desert you^Never gonna make you cry^Never gonna say goodbye^Never gonna tell a lie and hurt you^^Never gonna give you up^Never gonna let you down^Never gonna run around and desert you^Never gonna make you cry^Never gonna say goodbye^Never gonna tell a lie and hurt you^^Never gonna give you up^Never gonna let you down^Never gonna run around and desert you^Never gonna make you cry^Never gonna say goodbye^Never gonna tell a lie and hurt you
TQ1|1||||||20110331150028-0800|20110331152028-0800
OBX|1|NM|30341-2^Erythrocyte sedimentation rate^LN^815117^ESR^99USI^^^Erythrocyte sedimentation rate||20|mm/h^millimeter per hour^UCUM|0 to 17|H|||C|||20110331140551-0800|||||20110331150551-0800||||Century Hospital^^^^^NIST-AA-1&2.16.840.1.113883.3.72.5.30.1&ISO^XX^^^987|2070 Test Park^^Los Angeles^CA^90067^USA^B^^06037|2343242^Knowsalot^Phil^J.^III^Dr.^^^NIST-AA-1&2.16.840.1.113883.3.72.5.30.1&ISO^L^^^DN
NTE|1||Specimen re-analyzed per request of ordering provider.^Comment from Ryan:This comment/note should actually be attached to the first lab result.
SPM|1|||119297000^BLD^SCT^BldSpc^Blood^99USA^^^Blood Specimen|||||||||||||20110331140551-0800|||||||COOL^Cool^HL70493^CL^Cool^99USA^^^Cool";
			#endregion
#endif
			MBCP.textMain.SelectAll();
			MBCP.ShowDialog();
			EhrLab ehrLab;
			try {
				ehrLab=EhrLabs.ProcessHl7Message(MBCP.textMain.Text,PatCur);//Not a typical use of the msg box copy paste
				if(ehrLab.PatNum!=PatCur.PatNum) {
					if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"Lab patient does not match current patient, would you like to import the lab and attach it to current patient?")) {
						return;
					}
					ehrLab.PatNum=PatCur.PatNum;//TODO:this might need enhancing.
				}
				//ehrLab.Patnum already matches current patient.
			}
			catch (Exception Ex){
				MessageBox.Show(this,"Unable to import lab.\r\n"+Ex.Message);
				return;
			}
			ehrLab=EhrLabs.SaveToDB(ehrLab);
			for(int i=0;i<ehrLab.ListEhrLabResults.Count;i++) {
				if(Security.IsAuthorized(Permissions.EhrShowCDS,true)) {
					FormCDSIntervention FormCDSI=new FormCDSIntervention();
					FormCDSI.ListCDSI=EhrTriggers.TriggerMatch(ehrLab.ListEhrLabResults[i],PatCur);
					FormCDSI.ShowIfRequired(false);
				}
			}
			FillGrid();
		}

		private void butAdd_Click(object sender,EventArgs e) {
			FormEhrLabOrderEdit2014 FormLOE=new FormEhrLabOrderEdit2014();
			FormLOE.EhrLabCur=new EhrLab();
			FormLOE.EhrLabCur.PatNum=PatCur.PatNum;
			FormLOE.ShowDialog();
			//Save from the form??
			if(FormLOE.DialogResult!=DialogResult.OK) {
				return;
			}
			EhrLabs.SaveToDB(FormLOE.EhrLabCur);
			for(int i=0;i<FormLOE.EhrLabCur.ListEhrLabResults.Count;i++) {
				if(Security.IsAuthorized(Permissions.EhrShowCDS,true)) {
					FormCDSIntervention FormCDSI=new FormCDSIntervention();
					FormCDSI.ListCDSI=EhrTriggers.TriggerMatch(FormLOE.EhrLabCur.ListEhrLabResults[i],PatCur);
					FormCDSI.ShowIfRequired(false);
				}
			}
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			FormEhrLabOrderEdit2014 FormLOE=new FormEhrLabOrderEdit2014();
			FormLOE.EhrLabCur=ListEhrLabs[e.Row];
			FormLOE.ShowDialog();
			if(FormLOE.DialogResult!=DialogResult.OK) {
				return;
			}
			EhrLabs.SaveToDB(FormLOE.EhrLabCur);
			for(int i=0;i<FormLOE.EhrLabCur.ListEhrLabResults.Count;i++) {
				if(Security.IsAuthorized(Permissions.EhrShowCDS,true)) {
					FormCDSIntervention FormCDSI=new FormCDSIntervention();
					FormCDSI.ListCDSI=EhrTriggers.TriggerMatch(FormLOE.EhrLabCur.ListEhrLabResults[i],PatCur);
					FormCDSI.ShowIfRequired(false);
				}
			}
			//TODO:maybe add more code here for when we come back from form... In case we delete a lab from the form.
		}

		private void butClose_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		






	}
}
