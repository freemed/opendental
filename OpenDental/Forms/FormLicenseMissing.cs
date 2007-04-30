using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDental.UI;
using System.Collections;
using OpenDentBusiness;
using CodeBase;

namespace OpenDental {
	public partial class FormLicenseMissing:Form {
		public ArrayList Comments;

		public FormLicenseMissing() {
			InitializeComponent();
			//Create a list of comments detailing ADA code shortcomings (if possible).
			//ArrayList comments=GetComplianceComments();
			//if(comments.Count==0) {//No bad comments?
			//	comments.Add(Lan.g(this,"Compliance test passed"));//Tell the user of success.
			//}
			//comments);
		}

		private void FormLicenseMissing_Load(object sender,EventArgs e) {
			RefreshGrid();
		}

		private void RefreshGrid() {
			//Display those comments in the comment grid.
			codeGrid.BeginUpdate();
			codeGrid.Columns.Clear();
			ODGridColumn col=new ODGridColumn("Comments",80);
			codeGrid.Columns.Add(col);
			codeGrid.Rows.Clear();
			for(int i=0;i<Comments.Count;i++) {
				ODGridRow row=new ODGridRow();
				row.Cells.Add(Comments[i].ToString());
				codeGrid.Rows.Add(row);
			}
			codeGrid.EndUpdate();
			label2.Text="Approximate count: "+(Comments.Count/2).ToString();
		}

		private void okbutton_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.OK;
		}

		private void printbutton_Click(object sender,EventArgs e) {
			//not visible yet
		}

		

	}
}