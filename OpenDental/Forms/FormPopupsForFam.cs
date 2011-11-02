using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.UI;

namespace OpenDental {
	public partial class FormPopupsForFam:Form {
		public Patient PatCur;
		public List<PopupEvent> PopupEventList;
		private List<Popup> PopupList;

		public FormPopupsForFam() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormPopupsForFam_Load(object sender,EventArgs e) {
			PopupEventList=new List<PopupEvent>();
			FillGrid();
		}

		private void FillGrid() {
			PopupList=Popups.GetForFamily(PatCur);
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TablePopupsForFamily","Family Member"),120);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TablePopupsForFamily","Popup Message"),120);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<PopupList.Count;i++) {
				row=new ODGridRow();
				if(PopupList[i].IsFamily==EnumPopupFamily.Family) {
					row.Cells.Add("Entire Family");
				}
				else {
					row.Cells.Add(Patients.GetPat(PopupList[i].PatNum).GetNameFL());
				}
				row.Cells.Add(PopupList[i].Description);
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}

		private void butAdd_Click(object sender,EventArgs e) {
			FormPopupEdit FormPE=new FormPopupEdit();
			Popup popup=new Popup();
			popup.PatNum=PatCur.PatNum;
			popup.IsNew=true;
			FormPE.PopupCur=popup;
			FormPE.ShowDialog();
			if(FormPE.MinutesDisabled>0) {
				PopupEvent popupEvent=new PopupEvent();
				popupEvent.PatNum=PatCur.PatNum;
				popupEvent.PopupNum=popup.PopupNum;
				popupEvent.DisableUntil=DateTime.Now+TimeSpan.FromMinutes(FormPE.MinutesDisabled);
				PopupEventList.Add(popupEvent);
			}
			FillGrid();
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			FormPopupEdit FormPE=new FormPopupEdit();
			FormPE.PopupCur=PopupList[gridMain.GetSelectedIndex()];
			FormPE.ShowDialog();
			if(FormPE.MinutesDisabled>0) {
				PopupEvent popupEvent=new PopupEvent();
				popupEvent.PatNum=PatCur.PatNum;
				popupEvent.PopupNum=FormPE.PopupCur.PopupNum;
				popupEvent.DisableUntil=DateTime.Now+TimeSpan.FromMinutes(FormPE.MinutesDisabled);
				for(int i=0;i<PopupEventList.Count;i++) {
					if(PopupEventList[i].PopupNum==FormPE.PopupCur.PopupNum) {
						PopupEventList[i]=popupEvent;
						break;
					}
				}
			}
			FillGrid();
		}

		private void butClose_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.OK;
		}
	}
}