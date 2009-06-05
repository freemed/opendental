using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.UI;
using CodeBase;

namespace OpenDental {
	public partial class FormEtrans270Edit:Form {
		public Etrans EtransCur;
		private Etrans EtransAck271;
		private string MessageText;
		private string MessageTextAck;
		//public bool IsNew;//this makes no sense.  A 270 will never be new.  Always created, saved, and sent ahead of time.
		///<summary>True if the 270 and response have just been created and are being viewed for the first time.</summary>
		public bool IsInitialResponse;
		private List<EB271> listEB;
		private List<DTP271> listDTP;
		private X271 x271;

		public FormEtrans270Edit() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormEtrans270Edit_Load(object sender,EventArgs e) {
			MessageText=EtransMessageTexts.GetMessageText(EtransCur.EtransMessageTextNum);
			MessageTextAck="";
			//textMessageText.Text=MessageText;
			textNote.Text=EtransCur.Note;
			EtransAck271=Etranss.GetEtrans(EtransCur.AckEtransNum);
			x271=null;
			if(EtransAck271!=null) {
				MessageTextAck=EtransMessageTexts.GetMessageText(EtransAck271.EtransMessageTextNum);//.Replace("~","~\r\n");
				if(EtransAck271.Etype==EtransType.BenefitResponse271) {
					x271=new X271(MessageTextAck);
				}
			}
			FillGridDates();
			FillGrid();
			
		}

		private void FormEtrans270Edit_Shown(object sender,EventArgs e) {
			if(EtransAck271!=null && EtransAck271.Etype==EtransType.Acknowledge_997) {
				if(IsInitialResponse) {
					MessageBox.Show(EtransCur.Note);
				}
			}
		}

		private void FillGridDates() {
			listDTP=new List<DTP271>();
			if(x271 != null) {
				listDTP=x271.GetListDtpSubscriber();
			}
			gridDates.BeginUpdate();
			gridDates.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g(this,"Date"),150);
			gridDates.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Qualifier"),230);
			gridDates.Columns.Add(col);
			gridDates.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<listDTP.Count;i++) {
				row=new ODGridRow();
				row.Cells.Add(listDTP[i].GetDateStr());
				row.Cells.Add(listDTP[i].GetQualifier());
				gridDates.Rows.Add(row);
			}
			gridDates.EndUpdate();
		}

		private void FillGrid(){
			//process the 271 to create a list of benefits--------------------------------------------------------
			listEB=new List<EB271>();
			if(x271 != null) {
				listEB=x271.GetListEB();
			}
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g(this,"Response"),420);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Import As Benefit"),420);
			gridMain.Columns.Add(col); 
			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<listEB.Count;i++) {
				row=new ODGridRow();
				row.Cells.Add(listEB[i].GetDescription());
				row.Cells.Add(listEB[i].Benefitt.ToString());
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}

		private void butShowRequest_Click(object sender,EventArgs e) {
			MsgBoxCopyPaste msgbox=new MsgBoxCopyPaste(MessageText);
			msgbox.ShowDialog();
		}

		private void butShowResponse_Click(object sender,EventArgs e) {
			MsgBoxCopyPaste msgbox=new MsgBoxCopyPaste(MessageTextAck);
			msgbox.ShowDialog();
		}

		private void butDelete_Click(object sender,EventArgs e) {
			//This button is not visible if IsNew
			if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"Delete entire request and response?")) {
				return;
			}
			if(EtransAck271!=null) {
				EtransMessageTexts.Delete(EtransAck271.EtransMessageTextNum);
				Etranss.Delete(EtransAck271.EtransNum);
			}
			EtransMessageTexts.Delete(EtransCur.EtransMessageTextNum);
			Etranss.Delete(EtransCur.EtransNum);
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender,EventArgs e) {
			EtransCur.Note=textNote.Text;
			Etranss.Update(EtransCur);
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			//if(IsNew) {
			//	EtransMessageTexts.Delete(EtransCur.EtransMessageTextNum);
			//	Etranss.Delete(EtransCur.EtransNum);
			//}
			DialogResult=DialogResult.Cancel;
		}

		

		

	

		
	}
}