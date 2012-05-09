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
		private List<PayPlan> PayPlanList;
		private CreditCard CreditCardOld;
		public CreditCard CreditCardCur;
		///<summary>True if X-Charge is enabled.  Recurring charge section will only show if using X-Charge.</summary>
		private bool IsXCharge;

		public FormCreditCardEdit(Patient pat) {
			InitializeComponent();
			Lan.F(this);
			PatCur=pat;
			IsXCharge=Programs.IsEnabled(ProgramName.Xcharge);
		}

		private void FormCreditCardEdit_Load(object sender,EventArgs e) {
			CreditCardOld=CreditCardCur.Clone();
			FillData();
			if(IsXCharge) {//Get recurring payment plan information if using X-Charge.
				List<PayPlanCharge> chargeList=PayPlanCharges.Refresh(PatCur.PatNum);
				PayPlanList=PayPlans.GetValidPlansNoIns(PatCur.PatNum);
				comboPaymentPlans.Items.Add("None");
				comboPaymentPlans.SelectedIndex=0;
				for(int i=0;i<PayPlanList.Count;i++) {
					comboPaymentPlans.Items.Add(PayPlans.GetTotalPrinc(PayPlanList[i].PayPlanNum,chargeList).ToString("F")
					+"  "+Patients.GetPat(PayPlanList[i].PatNum).GetNameFL());
					if(PayPlanList[i].PayPlanNum==CreditCardCur.PayPlanNum) {
						comboPaymentPlans.SelectedIndex=i+1;
					}
				}
			}
			else {//This will hide the recurring section and change the window size.
				groupRecurringCharges.Visible=false;
				this.ClientSize=new System.Drawing.Size(this.ClientSize.Width,this.ClientSize.Height-215);
			}
		}

		private void FillData() {
			if(!CreditCardCur.IsNew) {
				textCardNumber.Text=CreditCardCur.CCNumberMasked;
				textAddress.Text=CreditCardCur.Address;
				if(CreditCardCur.CCExpiration.Year>1800) {
					textExpDate.Text=CreditCardCur.CCExpiration.ToString("MMyy");
				}
				textZip.Text=CreditCardCur.Zip;
				if(IsXCharge) {//Only fill information if using X-Charge.
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
			if(IsXCharge) {//Only validate recurring setup if using X-Charge.
				if(textDateStart.errorProvider1.GetError(textDateStart)!=""
				|| textDateStop.errorProvider1.GetError(textDateStop)!=""
				|| textChargeAmt.errorProvider1.GetError(textChargeAmt)!=""
					) {
					MsgBox.Show(this,"Please fix data entry errors first.");
					return false;
				}
				if((textChargeAmt.Text=="" && comboPaymentPlans.SelectedIndex>0)
				|| (textChargeAmt.Text=="" && textDateStart.Text.Trim()!="")) {
					MsgBox.Show(this,"You need a charge amount for recurring charges.");
					return false;
				}
				if(textChargeAmt.Text!="" && textDateStart.Text.Trim()=="") {
					MsgBox.Show(this,"You need a start date for recurring charges.");
					return false;
				}
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

		private void butToday_Click(object sender,EventArgs e) {
			textDateStart.Text=DateTime.Today.ToShortDateString();
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
			if(IsXCharge) {//Only update recurring if using X-Charge.
				CreditCardCur.ChargeAmt=PIn.Double(textChargeAmt.Text);
				CreditCardCur.DateStart=PIn.Date(textDateStart.Text);
				CreditCardCur.DateStop=PIn.Date(textDateStop.Text);
				CreditCardCur.Note=textNote.Text;
				if(comboPaymentPlans.SelectedIndex>0) {
					CreditCardCur.PayPlanNum=PayPlanList[comboPaymentPlans.SelectedIndex-1].PayPlanNum;
				}
				else {
					CreditCardCur.PayPlanNum=0;//Allows users to change from a recurring payplan charge to a normal one.
				}
			}
			if(CreditCardCur.IsNew) {
				List<CreditCard> itemOrderCount=CreditCards.Refresh(PatCur.PatNum);
				CreditCardCur.ItemOrder=itemOrderCount.Count;
				CreditCards.Insert(CreditCardCur);
			}
			else {
				#region X-Charge
				//Special logic for had a token and changed number or expiration date
				if(CreditCardCur.XChargeToken!="" && IsXCharge &&
					(CreditCardOld.CCNumberMasked!=CreditCardCur.CCNumberMasked || CreditCardOld.CCExpiration!=CreditCardCur.CCExpiration)) 
				{ 
					Program prog=Programs.GetCur(ProgramName.Xcharge);
					string path=Programs.GetProgramPath(prog);
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
					if(!File.Exists(path)){
						MsgBox.Show(this,"Path is not valid.");
						if(Security.IsAuthorized(Permissions.Setup)){
							FormXchargeSetup FormX=new FormXchargeSetup();
							FormX.ShowDialog();
						}
						return;
					}
					//Either update the exp date or update credit card number by deleting archive so new token can be created next time it's used.
					ProgramProperty prop=(ProgramProperty)ProgramProperties.GetForProgram(prog.ProgramNum)[0];
					ProcessStartInfo info=new ProcessStartInfo(path);
					string resultfile=Path.Combine(Path.GetDirectoryName(path),"XResult.txt");
					File.Delete(resultfile);//delete the old result file.
					if(CreditCardOld.CCNumberMasked!=CreditCardCur.CCNumberMasked) {//They changed card number which we have to delete archived token which will create a new one next time card is charged.
						info.Arguments+="/TRANSACTIONTYPE:ARCHIVEVAULTDELETE ";
						info.Arguments+="/XCACCOUNTID:"+CreditCardCur.XChargeToken+" ";
						info.Arguments+="/RESULTFILE:\""+resultfile+"\" ";
						info.Arguments+="/USERID:"+ProgramProperties.GetPropVal(prog.ProgramNum,"Username")+" ";
						info.Arguments+="/PASSWORD:"+ProgramProperties.GetPropVal(prog.ProgramNum,"Password")+" ";
						info.Arguments+="/AUTOPROCESS ";
						info.Arguments+="/AUTOCLOSE ";
						CreditCardCur.XChargeToken="";//Clear the XChargeToken in our db.
					}
					else {//We can only change exp date for X-Charge via ARCHIVEAULTUPDATE.
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
				#endregion
				CreditCards.Update(CreditCardCur);
			}
			DialogResult=DialogResult.OK;
		}

	}
}