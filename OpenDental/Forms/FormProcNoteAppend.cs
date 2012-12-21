using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
using CodeBase;

namespace OpenDental {
	public partial class FormProcNoteAppend:Form {
		public Procedure ProcCur;

		public FormProcNoteAppend() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormProcNoteAppend_Load(object sender,EventArgs e) {
			textUser.Text=Security.CurUser.UserName;
			textNotes.Text=ProcCur.Note;
			//there is no signature to display when this form is opened.
			//signatureBoxWrapper.FillSignature(false,"","");
			signatureBoxWrapper.BringToFront();
			//signatureBoxWrapper.ClearSignature();
		}

		private void buttonUseAutoNote_Click(object sender,EventArgs e) {
			FormAutoNoteCompose FormA=new FormAutoNoteCompose();
			FormA.ShowDialog();
			if(FormA.DialogResult==DialogResult.OK) {
				textAppended.AppendText(FormA.CompletedNote);
			}
		}

		private string GetSignatureKey() {
			//ProcCur.Note was already assembled as it will appear in proc edit window.  We want to key on that.
			//Procs and proc groups are keyed differently
			string keyData;
			if(ProcedureCodes.GetStringProcCode(ProcCur.CodeNum)==ProcedureCodes.GroupProcCode) {
				keyData=ProcCur.ProcDate.ToShortDateString();
				keyData+=ProcCur.DateEntryC.ToShortDateString();
				keyData+=ProcCur.UserNum.ToString();//Security.CurUser.UserName;
				keyData+=ProcCur.Note;
				List<ProcGroupItem> groupItemList=ProcGroupItems.GetForGroup(ProcCur.ProcNum);//Orders the list to ensure same key in all cases.
				for(int i=0;i<groupItemList.Count;i++) {
					keyData+=groupItemList[i].ProcGroupItemNum.ToString();
				}
			}
			else {//regular proc
				keyData=ProcCur.Note+ProcCur.UserNum.ToString();
			}
			//MsgBoxCopyPaste msgb=new MsgBoxCopyPaste(keyData);
			//msgb.ShowDialog();
			return keyData;
		}

		private void SaveSignature() {
			//This is not a good pattern to copy, because it's simpler than usual.  Try FormCommItem.
			string keyData=GetSignatureKey();
			ProcCur.Signature=signatureBoxWrapper.GetSignature(keyData);
			ProcCur.SigIsTopaz=signatureBoxWrapper.GetSigIsTopaz();
		}

		private void butOK_Click(object sender,EventArgs e) {
			Procedure procOld=ProcCur.Copy();
			ProcCur.UserNum=Security.CurUser.UserNum;
			ProcCur.Note=textNotes.Text+"\r\n"
				+DateTime.Now.ToShortDateString()+" "+DateTime.Now.ToShortTimeString()+" "+Security.CurUser.UserName+":  "
				+textAppended.Text;
			try {
				SaveSignature();
			}
			catch(Exception ex) {
				MessageBox.Show(Lan.g(this,"Error saving signature.")+"\r\n"+ex.Message);
				//and continue with the rest of this method
			}
			Procedures.Update(ProcCur,procOld);
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		
	}
}