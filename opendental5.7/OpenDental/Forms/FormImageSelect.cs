using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using OpenDental.UI;
using OpenDentBusiness;
using OpenDentBusiness.Imaging;
using CodeBase;

namespace OpenDental{
	/// <summary></summary>
	public class FormImageSelect : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private OpenDental.UI.ODGrid gridMain;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		public int PatNum;
		private Document[] Docs;
		///<summary>If DialogResult==OK, then this will contain the new ClaimAttach with the filename that the file was saved under.  File will be in the EmailAttachments folder.  But ClaimNum will not be set.</summary>
		public ClaimAttach ClaimAttachNew;

		///<summary></summary>
		public FormImageSelect()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			Lan.F(this);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormImageSelect));
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.gridMain = new OpenDental.UI.ODGrid();
			this.SuspendLayout();
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(505,472);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 1;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.Location = new System.Drawing.Point(505,513);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,26);
			this.butCancel.TabIndex = 0;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// gridMain
			// 
			this.gridMain.HScrollVisible = false;
			this.gridMain.Location = new System.Drawing.Point(12,12);
			this.gridMain.Name = "gridMain";
			this.gridMain.ScrollValue = 0;
			this.gridMain.Size = new System.Drawing.Size(451,527);
			this.gridMain.TabIndex = 2;
			this.gridMain.Title = "Images";
			this.gridMain.TranslationName = "FormImageSelect";
			this.gridMain.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridMain_CellDoubleClick);
			// 
			// FormImageSelect
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(632,564);
			this.Controls.Add(this.gridMain);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormImageSelect";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Select Image";
			this.Load += new System.EventHandler(this.FormImageSelect_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormImageSelect_Load(object sender,EventArgs e) {
			FillGrid();
		}

		private void FillGrid(){
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g(this,"Date"),100);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Category"),120);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Description"),300);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			Docs=Documents.GetAllWithPat(PatNum);
			for(int i=0;i<Docs.Length;i++){
				row=new ODGridRow();
				row.Cells.Add(Docs[i].DateCreated.ToShortDateString());
				row.Cells.Add(DefC.GetName(DefCat.ImageCats,Docs[i].DocCategory));
			  row.Cells.Add(Docs[i].Description);
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			SaveAttachment();
		}

		private void SaveAttachment(){
			Patient PatCur=Patients.GetPat(PatNum);
			if(PatCur.ImageFolder=="") {
				MsgBox.Show(this,"Invalid patient image folder.");
				return;
			}
			if(!PrefC.UsingAtoZfolder) {
				MsgBox.Show(this,"Error. Not using AtoZ images folder.");
				return;
			}
			string patfolder=ODFileUtils.CombinePaths(
				FormPath.GetPreferredImagePath(),PatCur.ImageFolder.Substring(0,1).ToUpper(),PatCur.ImageFolder);
			if(!Directory.Exists(patfolder)) {
				MsgBox.Show(this,"Patient folder not found in AtoZ folder.");
				return;
			}
			Document doc=Docs[gridMain.GetSelectedIndex()];
			if(!ImageHelper.HasImageExtension(doc.FileName)){
				MsgBox.Show(this,"Invalid file.  Only images may be attached, no other file format.");
				return;
			}
			string oldPath=ODFileUtils.CombinePaths(patfolder,doc.FileName);
			Random rnd=new Random();
			string newName=DateTime.Now.ToString("yyyyMMdd")+"_"+DateTime.Now.TimeOfDay.Ticks.ToString()+rnd.Next(1000).ToString()
				+Path.GetExtension(oldPath);
			string attachPath=FormEmailMessageEdit.GetAttachPath();
			string newPath=ODFileUtils.CombinePaths(attachPath,newName);
			try {
				if(ImageHelper.HasImageExtension(oldPath)){
					Bitmap bitmapold=(Bitmap)Bitmap.FromFile(oldPath);
					Bitmap bitmapnew=ImageHelper.ApplyDocumentSettingsToImage(doc,bitmapold,ApplySettings.ALL);
					bitmapnew.Save(newPath);
				}
				else{
					File.Copy(oldPath,newPath);
				}
				ClaimAttachNew=new ClaimAttach();
				ClaimAttachNew.DisplayedFileName=Docs[gridMain.GetSelectedIndex()].FileName;
				ClaimAttachNew.ActualFileName=newName;
				DialogResult=DialogResult.OK;
			}
			catch(Exception ex) {
				MessageBox.Show(ex.Message);
			}
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(gridMain.GetSelectedIndex()==-1){
				MsgBox.Show(this,"Please select an image first.");
				return;
			}
			SaveAttachment();
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		


	}
}





















