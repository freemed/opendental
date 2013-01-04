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
		private List<string> ImageNamesList;
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
			string[] imageNamesArray=System.IO.Directory.GetFiles(WikiPages.GetWikiPath());
			ImageNamesList=new List<string>();
			for(int i=0;i<imageNamesArray.Length;i++) {//only add image files to the list.
				if(textSearch.Text!="" && !imageNamesArray[i].Replace(WikiPages.GetWikiPath(),"").TrimStart('\\').Contains(textSearch.Text)) {
					continue;
				}
				if(imageNamesArray[i].EndsWith(".jpg") ||
					imageNamesArray[i].EndsWith(".bmp") ||
					imageNamesArray[i].EndsWith(".png") ||
					imageNamesArray[i].EndsWith(".gif")) 
				{
					ImageNamesList.Add(imageNamesArray[i]);
				}
			}
			for(int i=0;i<ImageNamesList.Count;i++) {
				ODGridRow row=new ODGridRow();
				row.Cells.Add(ImageNamesList[i].Replace(WikiPages.GetWikiPath(),"").TrimStart('\\'));
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
			labelImageSize.Text=Lan.g(this,"Image Size")+":";
			picturePreview.Image=null;
			picturePreview.Invalidate();
		}

		private void gridMain_CellClick(object sender,ODGridClickEventArgs e) {
			paintPreviewPicture();
		}

		private void paintPreviewPicture() {
			if(gridMain.GetSelectedIndex()==-1) {
				return;
			}
			string s=ImageNamesList[gridMain.GetSelectedIndex()];
			Image tmpImg=new Bitmap(ImageNamesList[gridMain.GetSelectedIndex()].Replace("\\\\","\\"));
			float imgScale=1;//will be between 0 and 1
			if(tmpImg.PhysicalDimension.Height>picturePreview.Height || tmpImg.PhysicalDimension.Width>picturePreview.Width) {//image is too large
				//Image is larger than PictureBox, resize to fit.
				if(tmpImg.PhysicalDimension.Width/picturePreview.Width>tmpImg.PhysicalDimension.Height/picturePreview.Height) {//resize image based on width
					imgScale=picturePreview.Width/tmpImg.PhysicalDimension.Width;
				}
				else {//resize image based on height
					imgScale=picturePreview.Height/tmpImg.PhysicalDimension.Height;
				}
						
			}
			picturePreview.Image=new Bitmap(tmpImg,(int)(tmpImg.PhysicalDimension.Width*imgScale),(int)(tmpImg.PhysicalDimension.Height*imgScale));
			labelImageSize.Text=Lan.g(this,"Image Size")+": "+(int)tmpImg.PhysicalDimension.Width+" x "+(int)tmpImg.PhysicalDimension.Height;
			picturePreview.Invalidate();
		}

		private void FormWikiImages_ResizeEnd(object sender,EventArgs e) {
			paintPreviewPicture();
		}

		private void butImport_Click(object sender,EventArgs e) {
			OpenFileDialog openFD=new OpenFileDialog();
			openFD.Multiselect=true;
			if(openFD.ShowDialog()!=DialogResult.OK) {
				return;
			}
			Invalidate();
			foreach(string fileName in openFD.FileNames) {
				//check file types?
				string destinationPath=WikiPages.GetWikiPath()+"\\"+fileName.Split('\\')[fileName.Split('\\').Length-1];
				if(File.Exists(destinationPath)){
					switch(MessageBox.Show(Lan.g(this,"Overwrite Existing File")+": "+destinationPath,"",MessageBoxButtons.YesNoCancel)){
						case DialogResult.No://rename
							InputBox ip = new InputBox(Lan.g(this,"New file name."));
							ip.textResult.Text=fileName.Split('\\')[fileName.Split('\\').Length-1];
							ip.ShowDialog();
							if(ip.DialogResult!=DialogResult.OK) {
								continue;//cancel, next file.
							}
							while(File.Exists(WikiPages.GetWikiPath()+"\\"+ip.textResult.Text) && ip.DialogResult==DialogResult.OK){
								MsgBox.Show(this,"File name already exists.");
								ip.ShowDialog();
							}
							destinationPath=WikiPages.GetWikiPath()+"\\"+ip.textResult.Text;
							break;//proceed to save file.
						case DialogResult.OK://overwrite
							try {
								File.Delete(destinationPath);
							}
							catch(Exception ex){
								MessageBox.Show(Lan.g(this,"Cannot copy file")+":" +fileName+"\r\n"+ex.Message);
								continue;
							}
							break;//file deleted, proceed to save.
						default://anything that is not OK and not NO
							continue;//skip this file.
					}
				}
				File.Copy(fileName,destinationPath);
			}
			FillGrid();
		}

		private void textSearch_TextChanged(object sender,EventArgs e) {
			FillGrid();
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			if(gridMain.GetSelectedIndex()==-1) {
				return;
			}
			SelectedImageName=ImageNamesList[gridMain.GetSelectedIndex()].Replace(WikiPages.GetWikiPath(),"").TrimStart('\\');
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender,EventArgs e) {
			if(gridMain.GetSelectedIndex()==-1) {
				return;
			}
			SelectedImageName=ImageNamesList[gridMain.GetSelectedIndex()].Replace(WikiPages.GetWikiPath(),"").TrimStart('\\');
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	}
}