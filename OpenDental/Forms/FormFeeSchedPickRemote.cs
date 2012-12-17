using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using OpenDental.UI;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormFeeSchedPickRemote:Form {

		///<summary>Url must include trailing slash.</summary>
		public string Url="";

		public string FileUrlChosen {
			get {
				if(gridFeeSchedFiles.GetSelectedIndex()==-1) {
					return "";
				}
				return Url+gridFeeSchedFiles.Rows[gridFeeSchedFiles.GetSelectedIndex()].Cells[0].Text;
			}
		}

		public FormFeeSchedPickRemote() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormFeeSchedPickRemote_Load(object sender,EventArgs e) {
			Cursor=Cursors.WaitCursor;
			gridFeeSchedFiles.BeginUpdate();
			gridFeeSchedFiles.Columns.Clear();
			ODGridColumn col=new ODGridColumn("",35);
			gridFeeSchedFiles.Columns.Add(col);
			gridFeeSchedFiles.Rows.Clear();
			ODGridRow row;
			HttpWebRequest request=(HttpWebRequest)WebRequest.Create(Url);
			using(HttpWebResponse response=(HttpWebResponse)request.GetResponse()) {
				using(StreamReader reader=new StreamReader(response.GetResponseStream())) {
					string html=reader.ReadToEnd();
					int startIndex=html.IndexOf("<body>")+6;
					int bodyLength=html.Substring(startIndex).IndexOf("</body>");
					string fileListStr=html.Substring(startIndex,bodyLength).Trim();
					string[] files=fileListStr.Split(new string[] { "\n","\r\n" },StringSplitOptions.RemoveEmptyEntries);
					for(int i=0;i<files.Length;i++) {
						row=new ODGridRow();
						row.Cells.Add(files[i]);
						gridFeeSchedFiles.Rows.Add(row);
					}
				}
			}
			gridFeeSchedFiles.EndUpdate();
			Cursor=Cursors.Default;
		}

		private void gridFeeSchedFiles_CellClick(object sender,ODGridClickEventArgs e) {
			butOK.Enabled=(gridFeeSchedFiles.SelectedIndices.Length>0);
		}

		private void gridFeeSchedFiles_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	}
}