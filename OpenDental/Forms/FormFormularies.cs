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
	public partial class FormFormularies:Form {
		private List<Formulary> ListFormularies;
		public long SelectedFormularyNum;
		public bool IsSelectionMode;

		public FormFormularies() {
			InitializeComponent();
		}

		private void FormFormularies_Load(object sender,EventArgs e) {
			butAdd.Visible=false;
			if(!IsSelectionMode) {
				labelSelect.Visible=false;
			}
			FillList();
		}

		private void FillList() {
			Cursor=Cursors.WaitCursor;
			ListFormularies=Formularies.GetAllFormularies();
			listMain.Items.Clear();
			for(int i=0;i<ListFormularies.Count;i++) {
				listMain.Items.Add(ListFormularies[i].Description);
			}
			Cursor=Cursors.Default;
		}

		private void listMain_DoubleClick(object sender,EventArgs e) {
			if(listMain.SelectedIndex==-1) {
				return;
			}
			if(IsSelectionMode) {
				SelectedFormularyNum=ListFormularies[listMain.SelectedIndex].FormularyNum;
				DialogResult=DialogResult.OK;
				return;
			}
			FormFormularyEdit FormF=new FormFormularyEdit();
			FormF.FormularyCur=ListFormularies[listMain.SelectedIndex];
			FormF.ShowDialog();
			if(FormF.DialogResult!=DialogResult.OK) {
				return;
			}
			FillList();
		}

		private void butAdd_Click(object sender,EventArgs e) {
			FormFormularyEdit FormF=new FormFormularyEdit();
			FormF.FormularyCur=new Formulary();
			FormF.IsNew=true;
			FormF.ShowDialog();
			if(FormF.DialogResult!=DialogResult.OK) {
				return;
			}
			FillList();
		}

		private void butClose_Click(object sender,EventArgs e) {
			this.Close();
		}






	}
}
