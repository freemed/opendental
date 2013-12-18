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
		private List<string> ListFeeSchedFilesOntario;

		///<summary>Returns the full URL of the chosen file.</summary>
		public string FileChosenUrl {
			get {				
				return Url+FileChosenName;
			}
		}

		///<summary>Returns just the file name of the chosen file.</summary>
		public string FileChosenName {
			get {
				if(gridFeeSchedFiles.GetSelectedIndex()==-1) {
					return "";
				}
				return gridFeeSchedFiles.Rows[gridFeeSchedFiles.GetSelectedIndex()].Cells[0].Text;
			}
		}

		///<summary>Returns true if the file chosen must be downloaded from the web service. Currently, Ontario fees must be downloaded from the web service.
		///Otherwise, if false is returned, the file can be downloaded by anyone directly from our website.</summary>
		public bool IsFileChosenProtected {
			get {
				return ListFeeSchedFilesOntario.Contains(FileChosenName);
			}
		}

		public FormFeeSchedPickRemote() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormFeeSchedPickRemote_Load(object sender,EventArgs e) {			
			Cursor=Cursors.WaitCursor;
			FillListFeeSchedFilesAll();
			Cursor=Cursors.Default;
		}

		private void FillListFeeSchedFilesAll() {
			FillListFeeSchedFilesOntario();
			List<string> ListFeeSchedFilesAll=new List<string>();
			ListFeeSchedFilesAll.AddRange(ListFeeSchedFilesOntario);
			HttpWebRequest request=(HttpWebRequest)WebRequest.Create(Url);
			using(HttpWebResponse response=(HttpWebResponse)request.GetResponse()) {
				using(StreamReader reader=new StreamReader(response.GetResponseStream())) {
					string html=reader.ReadToEnd();
					int startIndex=html.IndexOf("<body>")+6;
					int bodyLength=html.Substring(startIndex).IndexOf("</body>");
					string fileListStr=html.Substring(startIndex,bodyLength).Trim();
					string[] files=fileListStr.Split(new string[] { "\n","\r\n" },StringSplitOptions.RemoveEmptyEntries);
					for(int i=0;i<files.Length;i++) {
						if(files[i].ToLower().StartsWith("procedurecodes")) {
							continue;//Skip any files which contain procedure codes, because we only want to display fee files.
						}
						ListFeeSchedFilesAll.Add(files[i]);
					}
				}
			}
			ListFeeSchedFilesAll.Sort();
			gridFeeSchedFiles.BeginUpdate();
			gridFeeSchedFiles.Columns.Clear();
			ODGridColumn col=new ODGridColumn("",35);
			gridFeeSchedFiles.Columns.Add(col);
			gridFeeSchedFiles.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<ListFeeSchedFilesAll.Count;i++) {
				row=new ODGridRow();
				row.Cells.Add(ListFeeSchedFilesAll[i]);
				gridFeeSchedFiles.Rows.Add(row);
			}
			gridFeeSchedFiles.EndUpdate();
		}

		private void FillListFeeSchedFilesOntario() {
			ListFeeSchedFilesOntario=new List<string>();
			ListFeeSchedFilesOntario.Add("ON_ODA_2013_AN.txt");//Ontario Dental Association 2013 fee schedule for Anaesthesiologists.
			ListFeeSchedFilesOntario.Add("ON_ODA_2013_EN.txt");//Ontario Dental Association 2013 fee schedule for Endodontists.
			ListFeeSchedFilesOntario.Add("ON_ODA_2013_GP.txt");//Ontario Dental Association 2013 fee schedule for General Practitioners.
			ListFeeSchedFilesOntario.Add("ON_ODA_2013_OS.txt");//Ontario Dental Association 2013 fee schedule for Oral & Maxillofacial Surgeons.
			ListFeeSchedFilesOntario.Add("ON_ODA_2013_PA.txt");//Ontario Dental Association 2013 fee schedule for Paediatric Dentists.
			ListFeeSchedFilesOntario.Add("ON_ODA_2013_PE.txt");//Ontario Dental Association 2013 fee schedule for Periodontists.
			ListFeeSchedFilesOntario.Add("ON_ODA_2013_PR.txt");//Ontario Dental Association 2013 fee schedule for Prosthodontists.
			ListFeeSchedFilesOntario.Add("ON_ODA_2014_AN.txt");//Ontario Dental Association 2014 fee schedule for Anaesthesiologists.
			ListFeeSchedFilesOntario.Add("ON_ODA_2014_EN.txt");//Ontario Dental Association 2014 fee schedule for Endodontists.
			ListFeeSchedFilesOntario.Add("ON_ODA_2014_GP.txt");//Ontario Dental Association 2014 fee schedule for General Practitioners.
			ListFeeSchedFilesOntario.Add("ON_ODA_2014_OS.txt");//Ontario Dental Association 2014 fee schedule for Oral & Maxillofacial Surgeons.
			ListFeeSchedFilesOntario.Add("ON_ODA_2014_PA.txt");//Ontario Dental Association 2014 fee schedule for Paediatric Dentists.
			ListFeeSchedFilesOntario.Add("ON_ODA_2014_PE.txt");//Ontario Dental Association 2014 fee schedule for Periodontists.
			ListFeeSchedFilesOntario.Add("ON_ODA_2014_PR.txt");//Ontario Dental Association 2014 fee schedule for Prosthodontists.
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