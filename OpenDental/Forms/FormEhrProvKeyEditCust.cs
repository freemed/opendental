using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
#if DEBUG
using EHR;
#endif

namespace OpenDental {
	public partial class FormEhrProvKeyEditCust:Form {
		public EhrProvKey KeyCur;

		public FormEhrProvKeyEditCust() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormEhrProvKeyEditCust_Load(object sender,EventArgs e) {
			textLName.Text=KeyCur.LName;
			textFName.Text=KeyCur.FName;
			textEhrKey.Text=KeyCur.ProvKey;
		}

		private void FillProcedure() {
			/*
			if(PaySplitCur.ProcNum==0) {
				textProcDate2.Text="";
				textProcProv.Text="";
				textProcTooth.Text="";
				textProcDescription.Text="";
				ProcFee=0;
				textProcFee.Text="";
				ProcWriteoff=0;
				textProcWriteoff.Text="";
				ProcInsPaid=0;
				textProcInsPaid.Text="";
				ProcInsEst=0;
				textProcInsEst.Text="";
				ProcAdj=0;
				textProcAdj.Text="";
				ProcPrevPaid=0;
				textProcPrevPaid.Text="";
				ProcPaidHere=0;
				textProcPaidHere.Text="";
				labelProcRemain.Text="";
				//butAttach.Enabled=true;
				//butDetach.Enabled=false;
				//ComputeProcTotals();
				return;
			}
			Procedure ProcCur=Procedures.GetOneProc(PaySplitCur.ProcNum,false);
			List<ClaimProc> ClaimProcList=ClaimProcs.Refresh(ProcCur.PatNum);
			Adjustment[] AdjustmentList=Adjustments.Refresh(ProcCur.PatNum);
			PaySplit[] PaySplitList=PaySplits.Refresh(ProcCur.PatNum);
			//textProcDate.Text=ProcCur.ProcDate.ToShortDateString();
			textProcDate2.Text=ProcCur.ProcDate.ToShortDateString();
			textProcProv.Text=Providers.GetAbbr(ProcCur.ProvNum);
			textProcTooth.Text=Tooth.ToInternat(ProcCur.ToothNum);
			textProcDescription.Text=ProcedureCodes.GetProcCode(ProcCur.CodeNum).Descript;
			ProcFee=ProcCur.ProcFee;
			ProcWriteoff=-ClaimProcs.ProcWriteoff(ClaimProcList,ProcCur.ProcNum);
			ProcInsPaid=-ClaimProcs.ProcInsPay(ClaimProcList,ProcCur.ProcNum);
			ProcInsEst=-ClaimProcs.ProcEstNotReceived(ClaimProcList,ProcCur.ProcNum);
			ProcAdj=Adjustments.GetTotForProc(ProcCur.ProcNum,AdjustmentList);
			//next line will still work even if IsNew
			ProcPrevPaid=-PaySplits.GetTotForProc(ProcCur.ProcNum,PaySplitList,PaySplitCur.SplitNum);
			textProcFee.Text=ProcFee.ToString("F");
			if(ProcWriteoff==0) {
				textProcWriteoff.Text="";
			}
			else {
				textProcWriteoff.Text=ProcWriteoff.ToString("F");
			}
			if(ProcInsPaid==0) {
				textProcInsPaid.Text="";
			}
			else {
				textProcInsPaid.Text=ProcInsPaid.ToString("F");
			}
			if(ProcInsEst==0) {
				textProcInsEst.Text="";
			}
			else {
				textProcInsEst.Text=ProcInsEst.ToString("F");
			}
			if(ProcAdj==0) {
				textProcAdj.Text="";
			}
			else {
				textProcAdj.Text=ProcAdj.ToString("F");
			}
			if(ProcPrevPaid==0) {
				textProcPrevPaid.Text="";
			}
			else {
				textProcPrevPaid.Text=ProcPrevPaid.ToString("F");
			}
			ComputeProcTotals();
			//butAttach.Enabled=false;
			//butDetach.Enabled=true;*/
		}

		private void butGenerate_Click(object sender,EventArgs e) {
			if(textLName.Text=="" || textFName.Text=="") {
				MessageBox.Show("Please enter firstname and lastname.");
				return;
			}
			string progPath=@"E:\My Documents\Shared Projects Subversion\EhrProvKeyGenerator\EhrProvKeyGenerator\bin\Debug\EhrProvKeyGenerator.exe";
			ProcessStartInfo startInfo=new ProcessStartInfo(progPath);
			startInfo.Arguments="\""+textLName.Text.Replace("\"","")+"\" \""+textFName.Text.Replace("\"","")+"\"";
			startInfo.UseShellExecute=false;
			startInfo.RedirectStandardOutput=true;
			Process process=Process.Start(startInfo);
			string result=process.StandardOutput.ReadToEnd();
			result=result.Trim();//remove \r\n from the end
			//process.WaitForExit();
			textEhrKey.Text=result;
		}

		private void butAttach_Click(object sender,EventArgs e) {
			FormProcSelect FormPS=new FormProcSelect(KeyCur.PatNum);
			FormPS.IsForProvKeys=true;
			FormPS.ShowDialog();
			if(FormPS.DialogResult!=DialogResult.OK) {
				return;
			}
			//PaySplitCur.ProcNum=FormPS.SelectedProcNum;
			FillProcedure();
		}

		private void butDetach_Click(object sender,EventArgs e) {
			//PaySplitCur.ProcNum=0;
			FillProcedure();
		}

		private void butDelete_Click(object sender,EventArgs e) {
			if(KeyCur.IsNew) {
				DialogResult=DialogResult.Cancel;
				return;
			}
			if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"Delete?")) {
				return;
			}
			EhrProvKeys.Delete(KeyCur.EhrProvKeyNum);
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender,EventArgs e) {
			//if(textEhrKey.Text=="") {
			//	MessageBox.Show("Key must not be blank");
			//	return;
			//}
			if(textEhrKey.Text!="") {
				bool provKeyIsValid=false;
				#if DEBUG
					provKeyIsValid=((FormEHR)FormOpenDental.FormEHR).ProvKeyIsValid(textLName.Text,textFName.Text,textEhrKey.Text);
				#else
					Type type=FormOpenDental.AssemblyEHR.GetType("EHR.FormEHR");//namespace.class
					object[] args=new object[] { textLName.Text,textFName.Text,textEhrKey.Text };
					provKeyIsValid=(bool)type.InvokeMember("ProvKeyIsValid",System.Reflection.BindingFlags.InvokeMethod,null,FormOpenDental.FormEHR,args);
				#endif
				if(!provKeyIsValid) {
					MessageBox.Show("Invalid provider key");
					return;
				}
			}
			KeyCur.LName=textLName.Text;
			KeyCur.FName=textFName.Text;
			KeyCur.ProvKey=textEhrKey.Text;
			if(KeyCur.IsNew) {
				EhrProvKeys.Insert(KeyCur);
			}
			else {
				EhrProvKeys.Update(KeyCur);
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		

		
	}
}