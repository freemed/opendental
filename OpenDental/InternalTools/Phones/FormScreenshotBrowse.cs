using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace OpenDental {
	public partial class FormScreenshotBrowse:Form {
		public string ScreenshotPath;
		private string[] files;

		public FormScreenshotBrowse() {
			InitializeComponent();
		}

		private void FormScreenshotBrowse_Load(object sender,EventArgs e) {
			Cursor=Cursors.WaitCursor;
			pictureBoxMain.Image=Image.FromFile(ScreenshotPath);
			string folder=Path.GetDirectoryName(ScreenshotPath);
			files=Directory.GetFileSystemEntries(folder,"*-*-*");//to exclude thumbs.db
			for(int i=0;i<files.Length;i++){
				string filename=Path.GetFileNameWithoutExtension(files[i]);//2011-08-20-07112332
				if(filename.Length==19){
					filename=filename.Substring(0,10)+" "+filename.Substring(11,2)+":"+filename.Substring(13,2)+":"+filename.Substring(15,2);//2011-08-20 07:11:32
				}
				listFiles.Items.Add(filename);
				if(ScreenshotPath==files[i]){
					listFiles.SetSelected(i,true);
				}
			}
			Cursor=Cursors.Default;
		}

		private void listFiles_MouseClick(object sender,MouseEventArgs e) {
			int idx=listFiles.IndexFromPoint(e.Location);
			if(idx==-1){
				return;
			}
			Cursor=Cursors.WaitCursor;
			pictureBoxMain.Image=Image.FromFile(files[idx]);
			Cursor=Cursors.Default;
		}



	}
}
