using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
using CodeBase;

namespace OpenDental {
	public partial class FormSheetFieldImage:Form {
		///<summary>This is the object we are editing.</summary>
		public SheetFieldDef SheetFieldDefCur;
		///<summary>We need access to a few other fields of the sheetDef.</summary>
		public SheetDef SheetDefCur;
		public bool IsReadOnly;

		public FormSheetFieldImage() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormSheetFieldImage_Load(object sender,EventArgs e) {
			if(IsReadOnly){
				butOK.Enabled=false;
			}
			FillCombo();
			comboFieldName.Text=SheetFieldDefCur.FieldName;
			FillImage();
			textXPos.Text=SheetFieldDefCur.XPos.ToString();
			textYPos.Text=SheetFieldDefCur.YPos.ToString();
			textWidth.Text=SheetFieldDefCur.Width.ToString();
			textHeight.Text=SheetFieldDefCur.Height.ToString();
		}

		private void FillCombo(){
			if(PrefC.UsingAtoZfolder) {
				comboFieldName.Items.Clear();
				string[] files=Directory.GetFiles(SheetUtil.GetImagePath());
				for(int i=0;i<files.Length;i++){
					comboFieldName.Items.Add(Path.GetFileName(files[i]));
				}
				//comboFieldName.Items.Add("Patient Info.gif");
			}
		}

		private void butImport_Click(object sender,EventArgs e) {
			OpenFileDialog dlg=new OpenFileDialog();
			dlg.Multiselect=false;
			if(dlg.ShowDialog()!=DialogResult.OK){
				return;
			}
			if(!File.Exists(dlg.FileName)){
				MsgBox.Show(this,"File does not exist.");
				return;
			}
			string newName=ODFileUtils.CombinePaths(SheetUtil.GetImagePath(),Path.GetFileName(dlg.FileName));
			if(File.Exists(newName)){
				MsgBox.Show(this,"A file of that name already exists in SheetImages.  Please rename the file before importing.");
				return;
			}
			File.Copy(dlg.FileName,newName);
			FillCombo();
			for(int i=0;i<comboFieldName.Items.Count;i++){
				if(comboFieldName.Items[i].ToString()==Path.GetFileName(newName)){
					comboFieldName.SelectedIndex=i;
					comboFieldName.Text=Path.GetFileName(newName);
					FillImage();
					ShrinkToFit();
				}
			}
		}

		private void comboFieldName_TextUpdate(object sender,EventArgs e) {
			FillImage();
			ShrinkToFit();
		}

		private void comboFieldName_SelectionChangeCommitted(object sender,EventArgs e) {
			comboFieldName.Text=comboFieldName.SelectedItem.ToString();
			FillImage();
			ShrinkToFit();
		}

		private void FillImage(){
			textFullPath.Text=ODFileUtils.CombinePaths(SheetUtil.GetImagePath(),comboFieldName.Text);
			if(File.Exists(textFullPath.Text)){
				pictureBox.Image=Image.FromFile(textFullPath.Text);
				textWidth2.Text=pictureBox.Image.Width.ToString();
				textHeight2.Text=pictureBox.Image.Height.ToString();
			}
			else if(comboFieldName.Text=="Patient Info.gif") {
				pictureBox.Image=Properties.Resources.Patient_Info;
				textWidth2.Text=pictureBox.Image.Width.ToString();
				textHeight2.Text=pictureBox.Image.Height.ToString();
			}
			else{
				pictureBox.Image=null;
				textWidth2.Text="";
				textHeight2.Text="";
			}
		}

		private void butShrink_Click(object sender,EventArgs e) {
			ShrinkToFit();
		}

		private void ShrinkToFit(){
			if(pictureBox.Image==null){
				return;
			}
			if(pictureBox.Image.Width>SheetDefCur.Width || pictureBox.Image.Height>SheetDefCur.Height){//image would be too big
				float imgWtoH=((float)pictureBox.Image.Width)/((float)pictureBox.Image.Height);
				float sheetWtoH=((float)SheetDefCur.Width)/((float)SheetDefCur.Height);
				float newRatio;
				int newW;
				int newH;
				if(imgWtoH < sheetWtoH){//image is tall and skinny
					newRatio=((float)SheetDefCur.Height)/((float)pictureBox.Image.Height);//restrict by height
					newW=(int)(((float)pictureBox.Image.Width)*newRatio);
					newH=(int)(((float)pictureBox.Image.Height)*newRatio);
					textWidth.Text=newW.ToString();
					textHeight.Text=newH.ToString();
				}
				else{//image is short and fat
					newRatio=((float)SheetDefCur.Width)/((float)pictureBox.Image.Width);//restrict by width
					newW=(int)(((float)pictureBox.Image.Width)*newRatio);
					newH=(int)(((float)pictureBox.Image.Height)*newRatio);
					textWidth.Text=newW.ToString();
					textHeight.Text=newH.ToString();
				}
			}
			else{
				textWidth.Text=pictureBox.Image.Width.ToString();
				textHeight.Text=pictureBox.Image.Height.ToString();
			}
		}

		private void textWidth_KeyUp(object sender,KeyEventArgs e) {
			if(!checkRatio.Checked){
				return;
			}
			if(pictureBox.Image==null){
				return;
			}
			float w;
			try{
				w=PIn.Float(textWidth.Text);
			}
			catch{
				return;
			}
			float imgWtoH=((float)pictureBox.Image.Width)/((float)pictureBox.Image.Height);
			int newH=(int)(w/imgWtoH);
			textHeight.Text=newH.ToString();
		}

		private void textHeight_KeyUp(object sender,KeyEventArgs e) {
			if(!checkRatio.Checked){
				return;
			}
			if(pictureBox.Image==null){
				return;
			}
			float h;
			try{
				h=PIn.Float(textHeight.Text);
			}
			catch{
				return;
			}
			float imgWtoH=((float)pictureBox.Image.Width)/((float)pictureBox.Image.Height);
			int newW=(int)(h*imgWtoH);
			textWidth.Text=newW.ToString();
		}

		private void butDelete_Click(object sender,EventArgs e) {
			SheetFieldDefCur=null;
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender,EventArgs e) {
			if(textXPos.errorProvider1.GetError(textXPos)!=""
				|| textYPos.errorProvider1.GetError(textYPos)!=""
				|| textWidth.errorProvider1.GetError(textWidth)!=""
				|| textHeight.errorProvider1.GetError(textHeight)!="")
			{
				MsgBox.Show(this,"Please fix data entry errors first.");
				return;
			}
			if(comboFieldName.Text==""){
				MsgBox.Show(this,"Please enter a file name first.");
				return;
			}
			if(!File.Exists(textFullPath.Text)
				&& comboFieldName.Text!="Patient Info.gif") 
			{
				MsgBox.Show(this,"Image file does not exist.");
				return;
			}
			SheetFieldDefCur.FieldName=comboFieldName.Text;
			SheetFieldDefCur.XPos=PIn.Int(textXPos.Text);
			SheetFieldDefCur.YPos=PIn.Int(textYPos.Text);
			SheetFieldDefCur.Width=PIn.Int(textWidth.Text);
			SheetFieldDefCur.Height=PIn.Int(textHeight.Text);
			//don't save to database here.
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		
		

		

	

		

		

		

		

		
	}
}