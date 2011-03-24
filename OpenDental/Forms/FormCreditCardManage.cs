using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormCreditCardManage:Form {
		private Patient PatCur;
		private List<CreditCard> creditCards;

		public FormCreditCardManage(Patient pat) {
			InitializeComponent();
			Lan.F(this);
			PatCur=pat;
		}
		
		private void FormCreditCardManage_Load(object sender,EventArgs e) {
			if(PrefC.GetBool(PrefName.StoreCCnumbers) && Programs.IsEnabled(ProgramName.Xcharge)) {
				labelXChargeWarning.Visible=true;
			}
			RefreshCardList();
			if(creditCards.Count>0) {
				listCreditCards.SelectedIndex=0;
			}
		}

		private void RefreshCardList() {
			listCreditCards.Items.Clear();
			creditCards=CreditCards.Refresh(PatCur.PatNum);
			for(int i=0;i<creditCards.Count;i++) {
				listCreditCards.Items.Add(creditCards[i].CCNumberMasked);
			}
		}

		private void listCreditCards_MouseDoubleClick(object sender,MouseEventArgs e) {
			if(listCreditCards.SelectedIndex==-1) {
				return;
			}
			int prev=creditCards.Count;
			int placement=listCreditCards.SelectedIndex;
			FormCreditCardEdit FormCCE=new FormCreditCardEdit(PatCur);
			FormCCE.CreditCardCur=creditCards[placement];
			FormCCE.ShowDialog();
			RefreshCardList();
			if(creditCards.Count==prev) {
				listCreditCards.SelectedIndex=placement;
			}
			else if(creditCards.Count>0) {
				listCreditCards.SelectedIndex=0;
			}
		}

		private void butAdd_Click(object sender,EventArgs e) {
			if(!PrefC.GetBool(PrefName.StoreCCnumbers)) {
				if(Programs.IsEnabled(ProgramName.Xcharge)) {
					Program prog=Programs.GetCur(ProgramName.Xcharge);
					string user=ProgramProperties.GetPropVal(prog.ProgramNum,"Username");
					string password=ProgramProperties.GetPropVal(prog.ProgramNum,"Password");
					ProcessStartInfo info=new ProcessStartInfo(prog.Path);
					string resultfile=Path.Combine(Path.GetDirectoryName(prog.Path),"XResult.txt");
					File.Delete(resultfile);//delete the old result file.
					info.Arguments="";
					info.Arguments+="/TRANSACTIONTYPE:ArchiveVaultAdd /LOCKTRANTYPE ";
					info.Arguments+="/RESULTFILE:\""+resultfile+"\" ";
					info.Arguments+="/USERID:"+user+" ";
					info.Arguments+="/PASSWORD:"+password+" ";
					info.Arguments+="/VALIDATEARCHIVEVAULTACCOUNT ";
					info.Arguments+="/STAYONTOP ";
					info.Arguments+="/SMARTAUTOPROCESS ";
					info.Arguments+="/AUTOCLOSE ";
					info.Arguments+="/HIDEMAINWINDOW ";
					info.Arguments+="/SMALLWINDOW ";
					info.Arguments+="/NORESULTDIALOG ";
					info.Arguments+="/TOOLBAREXITBUTTON ";
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
					string xChargeToken="";
					string accountMasked="";
					string exp="";;
					bool insertCard=false;
					using(TextReader reader=new StreamReader(resultfile)) {
						line=reader.ReadLine();
						while(line!=null) {
							if(resulttext!="") {
								resulttext+="\r\n";
							}
							resulttext+=line;
							if(line.StartsWith("RESULT=")) {
								if(line!="RESULT=SUCCESS") {
									break;
								}
								insertCard=true;
							}
							if(line.StartsWith("XCACCOUNTID=")) {
								xChargeToken=PIn.String(line.Substring(12));
							}
							if(line.StartsWith("ACCOUNT=")) {
								accountMasked=PIn.String(line.Substring(8));
							}
							if(line.StartsWith("EXPIRATION=")) {
								exp=PIn.String(line.Substring(11));
							}
							line=reader.ReadLine();
						}
						if(insertCard && xChargeToken!="") {//Might not be necessary but we've had successful charges with no tokens returned before.
							CreditCard creditCardCur=new CreditCard();
							List<CreditCard> itemOrderCount=CreditCards.Refresh(PatCur.PatNum);
							creditCardCur.PatNum=PatCur.PatNum;
							creditCardCur.ItemOrder=itemOrderCount.Count;
							creditCardCur.CCNumberMasked=accountMasked;
							creditCardCur.XChargeToken=xChargeToken;
							creditCardCur.CCExpiration=new DateTime(Convert.ToInt32("20"+PIn.String(exp.Substring(2,2))),Convert.ToInt32(PIn.String(exp.Substring(0,2))),1);
							CreditCards.Insert(creditCardCur);
						}
					}
					RefreshCardList();
					return;
				}
				else {
					MsgBox.Show(this,"Not allowed to store credit cards.");
					return;
				}
			}
			bool remember=false;
			int placement=listCreditCards.SelectedIndex;
			if(placement!=-1) {
				remember=true;
			}
			FormCreditCardEdit FormCCE=new FormCreditCardEdit(PatCur);
			FormCCE.CreditCardCur=new CreditCard();
			FormCCE.CreditCardCur.IsNew=true;
			FormCCE.ShowDialog();
			RefreshCardList();
			if(remember) {//in case they canceled and had one selected
				listCreditCards.SelectedIndex=placement;
			}
			if(FormCCE.DialogResult==DialogResult.OK && creditCards.Count>0) {
				listCreditCards.SelectedIndex=0;
			}
		}

		private void butUp_Click(object sender,EventArgs e) {
			int placement=listCreditCards.SelectedIndex;
			if(placement==-1) {
				MsgBox.Show(this,"Please select a card first.");
				return;
			}
			if(placement==0) {
				return;//can't move up any more
			}
			int oldIdx;
			int newIdx;
			CreditCard oldItem;
			CreditCard newItem;
			oldIdx=creditCards[placement].ItemOrder;
			newIdx=oldIdx+1; 
			for(int i=0;i<creditCards.Count;i++) {
				if(creditCards[i].ItemOrder==oldIdx) {
					oldItem=creditCards[i];
					newItem=creditCards[i-1];
					oldItem.ItemOrder=newItem.ItemOrder;
					newItem.ItemOrder-=1;
					CreditCards.Update(oldItem);
					CreditCards.Update(newItem);
				}
			}
			RefreshCardList();
			listCreditCards.SetSelected(placement-1,true);
		}

		private void butDown_Click(object sender,EventArgs e) {
			int placement=listCreditCards.SelectedIndex;
			if(placement==-1) {
				MsgBox.Show(this,"Please select a card first.");
				return;
			}
			if(placement==creditCards.Count-1) {
				return;//can't move down any more
			}
			int oldIdx;
			int newIdx;
			CreditCard oldItem;
			CreditCard newItem;
			oldIdx=creditCards[placement].ItemOrder;
			newIdx=oldIdx-1;
			for(int i=0;i<creditCards.Count;i++) {
				if(creditCards[i].ItemOrder==newIdx) {
					newItem=creditCards[i];
					oldItem=creditCards[i-1];
					newItem.ItemOrder=oldItem.ItemOrder;
					oldItem.ItemOrder-=1;
					CreditCards.Update(oldItem);
					CreditCards.Update(newItem);
				}
			}
			RefreshCardList();
			listCreditCards.SetSelected(placement+1,true);
		}

		private void butClose_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}
	}
}