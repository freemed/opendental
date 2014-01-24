using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental;
using OpenDental.UI;
using CodeBase;

namespace OpenDental {
	public partial class FormEhrLabNoteEdit:Form {
		public EhrLabNote LabNoteCur;
		public bool IsImport;
		public bool IsViewOnly;

		public FormEhrLabNoteEdit() {
			InitializeComponent();
		}

		private void FormEhrLabOrders_Load(object sender,EventArgs e) {
			if(IsImport || IsViewOnly) {
				foreach(Control c in Controls) {
					c.Enabled=false;
				}
				butCancel.Text="Close";
				butCancel.Enabled=true;
			}
			FillGrid();
		}

		private void FillGrid() {

			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col;
			col=new ODGridColumn("Comments",80);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			string[] comments=LabNoteCur.Comments.Split('^');
			for(int i=0;i<comments.Length;i++) {
				if(LabNoteCur.Comments=="") {
					break;//prevents empty line from being added when the note is actually empty.
				}
				ODGridRow row=new ODGridRow();
				row.Cells.Add(comments[i]);
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}

		private void butAddComment_Click(object sender,EventArgs e) {
			InputBox ipb=new InputBox("Add comment to note.");
			ipb.ShowDialog();
			if(ipb.DialogResult!=DialogResult.OK) {
				return;
			}
			string result=ipb.textResult.Text;
			result=result.Replace("|","");//reserved character
			result=result.Replace("^","");//reserved character
			result=result.Replace("~","");//reserved character
			result=result.Replace("&","");//reserved character
			result=result.Replace("#","");//reserved character
			result=result.Replace("\\","");//reserved character
			if(result!=ipb.textResult.Text) {
				MsgBox.Show(this, "Special characters were removed from comment. The characters |,^,~,\\,&, and # cannot be used in a comment.");
			}
			LabNoteCur.Comments+=(LabNoteCur.Comments==""?"":"^")+result;
			FillGrid();
		}

		private void butSave_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		






	}
}
