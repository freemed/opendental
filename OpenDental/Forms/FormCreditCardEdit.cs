using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormCreditCardEdit:Form {
		private Patient PatCur;
		private CreditCard creditCardOld;
		public CreditCard CreditCardCur;

		public FormCreditCardEdit(Patient pat) {
			InitializeComponent();
			Lan.F(this);
			PatCur=pat;
		}

		private void FormPayConnect_Load(object sender,EventArgs e) {
			creditCardOld=CreditCardCur.Clone();
			FillData();
		}

		private void FillData() {
			if(!CreditCardCur.IsNew) {
				textCardNumber.Text=CreditCardCur.CCNumberMasked;
				textAddress.Text=CreditCardCur.Address;
				textExpDate.Text=CreditCardCur.CCExpiration.ToString("MMyy");
				textZip.Text=CreditCardCur.Zip;
				if(CreditCardCur.ChargeAmt>0) {
					textChargeAmt.Text=CreditCardCur.ChargeAmt.ToString("F");
				}
				if(CreditCardCur.DateStart.Year>1880) {
					textDateStart.Text=CreditCardCur.DateStart.ToShortDateString();
				}
				if(CreditCardCur.DateStop.Year>1880) {
					textDateStop.Text=CreditCardCur.DateStop.ToShortDateString();
				}
				textNote.Text=CreditCardCur.Note;
			}
		}

		private bool VerifyData() {
			if(textCardNumber.Text.Trim().Length<5) {
				MsgBox.Show(this,"Invalid Card Number.");
				return false;
			}
			try {
				if(Regex.IsMatch(textExpDate.Text,@"^\d\d[/\- ]\d\d$")) {//08/07 or 08-07 or 08 07
					CreditCardCur.CCExpiration=new DateTime(Convert.ToInt32("20"+textExpDate.Text.Substring(3,2)),Convert.ToInt32(textExpDate.Text.Substring(0,2)),1);
				}
				else if(Regex.IsMatch(textExpDate.Text,@"^\d{4}$")) {//0807
					CreditCardCur.CCExpiration=new DateTime(Convert.ToInt32("20"+textExpDate.Text.Substring(2,2)),Convert.ToInt32(textExpDate.Text.Substring(0,2)),1);
				}
				else {
					MsgBox.Show(this,"Expiration format invalid.");
					return false;
				}
			}
			catch {
				MsgBox.Show(this,"Expiration format invalid.");
				return false;
			}
			if(  textDateStart.errorProvider1.GetError(textDateStart)!=""
				|| textDateStop.errorProvider1.GetError(textDateStop)!=""
				)
			{
				MsgBox.Show(this,"Please fix data entry errors first.");
				return false;
			}
			if(textChargeAmt.Text!="" && textDateStart.Text.Trim()=="") {
				MsgBox.Show(this,"You need a start date for recurring charges.");
				return false;
			}
			return true;
		}

		private void butClear_Click(object sender,EventArgs e) {
			//Only clear text boxes for recurring charges group.
			textChargeAmt.Text="";
			textDateStart.Text="";
			textDateStop.Text="";
			textNote.Text="";
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void butDelete_Click(object sender,EventArgs e) {
			if(CreditCardCur.IsNew) {
				DialogResult=DialogResult.Cancel;
			}
			CreditCards.Delete(CreditCardCur.CreditCardNum);
			List<CreditCard> creditCards=CreditCards.Refresh(PatCur.PatNum);
			for(int i=0;i<creditCards.Count;i++) {
				creditCards[i].ItemOrder=creditCards.Count-(i+1);
				CreditCards.Update(creditCards[i]);//Resets ItemOrder.
			}
			DialogResult=DialogResult.OK;
		}
		
		private void butOK_Click(object sender,EventArgs e) {
			if(!VerifyData()) {
				return;
			}
			CreditCardCur.Address=textAddress.Text;
			CreditCardCur.CCNumberMasked=textCardNumber.Text;
			CreditCardCur.PatNum=PatCur.PatNum;
			CreditCardCur.Zip=textZip.Text;
			CreditCardCur.ChargeAmt=PIn.Double(textChargeAmt.Text);
			CreditCardCur.DateStart=PIn.Date(textDateStart.Text);
			CreditCardCur.DateStop=PIn.Date(textDateStop.Text);
			CreditCardCur.Note=textNote.Text;
			if(CreditCardCur.IsNew) {
				List<CreditCard> itemOrderCount=CreditCards.Refresh(PatCur.PatNum);
				CreditCardCur.ItemOrder=itemOrderCount.Count;
				CreditCards.Insert(CreditCardCur);
			}
			else {
				//Special logic for had a token and changed number or expiration date
				if(CreditCardCur.XChargeToken!="" && (creditCardOld.CCNumberMasked!=CreditCardCur.CCNumberMasked || creditCardOld.CCExpiration!=CreditCardCur.CCExpiration)) { 
					Program prog=Programs.GetCur(ProgramName.Xcharge);
					if(prog==null){
						MsgBox.Show(this,"X-Charge entry is missing from the database.");//should never happen
						return;
					}
					if(!prog.Enabled){
						if(Security.IsAuthorized(Permissions.Setup)){
							FormXchargeSetup FormX=new FormXchargeSetup();
							FormX.ShowDialog();
						}
						return;
					}
					if(!File.Exists(prog.Path)){
						MsgBox.Show(this,"Path is not valid.");
						if(Security.IsAuthorized(Permissions.Setup)){
							FormXchargeSetup FormX=new FormXchargeSetup();
							FormX.ShowDialog();
						}
						return;
					}
					//Either update the exp date or update credit card number by deleting archive so new token can be created next time it's used.
					ProgramProperty prop=(ProgramProperty)ProgramProperties.GetForProgram(prog.ProgramNum)[0];
					ProcessStartInfo info=new ProcessStartInfo(prog.Path);
					string resultfile=Path.Combine(Path.GetDirectoryName(prog.Path),"XResult.txt");
					File.Delete(resultfile);//delete the old result file.
					if(creditCardOld.CCExpiration!=CreditCardCur.CCExpiration) {//We can only change exp date for X-Charge via ARCHIVEAULTUPDATE.
						info.Arguments+="/TRANSACTIONTYPE:ARCHIVEVAULTUPDATE ";
						info.Arguments+="/XCACCOUNTID:"+CreditCardCur.XChargeToken+" ";
						if(CreditCardCur.CCExpiration!=null && CreditCardCur.CCExpiration.Year>2005) {
							info.Arguments+="/EXP:"+CreditCardCur.CCExpiration.ToString("MMyy")+" ";
						}
						info.Arguments+="/RESULTFILE:\""+resultfile+"\" ";
						info.Arguments+="/USERID:"+ProgramProperties.GetPropVal(prog.ProgramNum,"Username")+" ";
						info.Arguments+="/PASSWORD:"+ProgramProperties.GetPropVal(prog.ProgramNum,"Password")+" ";
						info.Arguments+="/AUTOPROCESS ";
						info.Arguments+="/AUTOCLOSE ";
					}
					else {//They changed card number which we have to delete archived token which will create a new one next time card is charged.
						info.Arguments+="/TRANSACTIONTYPE:ARCHIVEVAULTDELETE ";
						info.Arguments+="/XCACCOUNTID:"+CreditCardCur.XChargeToken+" ";
						info.Arguments+="/RESULTFILE:\""+resultfile+"\" ";
						info.Arguments+="/USERID:"+ProgramProperties.GetPropVal(prog.ProgramNum,"Username")+" ";
						info.Arguments+="/PASSWORD:"+ProgramProperties.GetPropVal(prog.ProgramNum,"Password")+" ";
						info.Arguments+="/AUTOPROCESS ";
						info.Arguments+="/AUTOCLOSE ";
						CreditCardCur.XChargeToken="";//Clear the XChargeToken in our db.
					}
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
									CreditCardCur=CreditCards.GetOne(CreditCardCur.CreditCardNum);
									FillData();
									return;
								}
							}
							line=reader.ReadLine();
						}
					}
				}//End of special token logic
				CreditCards.Update(CreditCardCur);
			}
			DialogResult=DialogResult.OK;
		}

	}
}