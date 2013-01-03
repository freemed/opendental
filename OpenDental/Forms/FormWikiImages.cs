using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.UI;
using System.IO;

namespace OpenDental {
	public partial class FormWikiImages:Form {
		private string[] ImageNames;
		public string SelectedImageName;

		public FormWikiImages() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormWikiImages_Load(object sender,EventArgs e) {
			FillGrid();
		}

		/// <summary></summary>
		private void FillGrid() {
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g(this,"Image Name"),70);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ImageNames=System.IO.Directory.GetFiles(WikiPages.GetWikiPath());
			for(int i=0;i<ImageNames.Length;i++) {
				ODGridRow row=new ODGridRow();
				row.Cells.Add(ImageNames[i].Replace(WikiPages.GetWikiPath(),"").TrimStart('\\'));
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}

		private void gridMain_CellClick(object sender,ODGridClickEventArgs e) {
			if(ImageNames[e.Row].EndsWith(".jpg") ||
				ImageNames[e.Row].EndsWith(".bmp") ||
				ImageNames[e.Row].EndsWith(".png") ||
				ImageNames[e.Row].EndsWith(".gif"))
			{
				try {
					string s=ImageNames[e.Row];
					picturePreview.Image=new Bitmap(ImageNames[e.Row].Replace("\\\\","\\"));
					picturePreview.Invalidate();
				}
				catch(Exception ex) {
					//do nothing.
				}
			}
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			if(gridMain.GetSelectedIndex()==-1) {
				return;
			}
			SelectedImageName=ImageNames[gridMain.GetSelectedIndex()].Replace(WikiPages.GetWikiPath(),"").TrimStart('\\');
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender,EventArgs e) {
			if(gridMain.GetSelectedIndex()==-1) {
				return;
			}
			SelectedImageName=ImageNames[gridMain.GetSelectedIndex()].Replace(WikiPages.GetWikiPath(),"").TrimStart('\\');
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	}
}