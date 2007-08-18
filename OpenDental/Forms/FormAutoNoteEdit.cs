using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	
	public partial class FormAutoNoteEdit:Form {
		public bool IsNew;
		public AutoNote AutoNoteCur;

		public FormAutoNoteEdit() {
			InitializeComponent();
			Lan.F(this);
		}		

		private void FormAutoNoteEdit_Load(object sender,EventArgs e) {
			//todo: fill controls on form

		}

		private void listBoxControlsToIncl_SelectedIndexChanged(object sender, EventArgs e) {				

		}

		private void listBoxControls_SelectedIndexChanged(object sender, EventArgs e) {
			
		}

		private void butApplyControlChanges_Click(object sender, EventArgs e) {
			
		}

		private void butCreateControl_Click(object sender,EventArgs e) {
			//should launch FormAutoNoteControlEdit
		}

		private void butEditControl_Click(object sender,EventArgs e) {
			//should launch FormAutoNoteControlEdit
		}

		private void butOK_Click(object sender,EventArgs e) {
			//Save changes to database here
		}

		private void butCancel_Click(object sender,EventArgs e) {

		}


	}
}