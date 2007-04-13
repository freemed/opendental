/*=============================================================================================================
Open Dental GPL license Copyright (C) 2003  Jordan Sparks, DMD.  http://www.open-dent.com,  www.docsparks.com
See header in FormOpenDental.cs for complete text.  Redistributions must retain this text.
===============================================================================================================*/
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.IO;
using System.Net;
using System.Resources;
using System.Runtime.InteropServices;
using System.Text;
using OpenDentBusiness;
using CodeBase;

namespace OpenDental{
///<summary></summary>
	public class FormDocInfo : System.Windows.Forms.Form{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private OpenDental.UI.Button butOK;
		private System.Windows.Forms.ListBox listCategory;
		private System.Windows.Forms.TextBox textDescript;
		private OpenDental.UI.Button butCancel;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private OpenDental.ValidDate textDate;
		private System.ComponentModel.Container components = null;//required by designer
		private System.Windows.Forms.TextBox textFileName;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.ListBox listType;
		//<summary></summary>
		//public bool IsNew;
		private System.Windows.Forms.TextBox textSize;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox textToothNumbers;
		private System.Windows.Forms.Label label7;
		private Patient PatCur;
		private Document DocCur;
		private string initialSelection;
		
		///<summary></summary>
		public FormDocInfo(Patient patCur,Document docCur,string pInitialSelection){
			InitializeComponent();
			PatCur=patCur;
			DocCur=docCur;
			initialSelection=pInitialSelection;
			Lan.F(this);
		}

		///<summary></summary>
		protected override void Dispose( bool disposing ){
			if( disposing ){
				if(components != null){
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDocInfo));
			this.listCategory = new System.Windows.Forms.ListBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.textDescript = new System.Windows.Forms.TextBox();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.textFileName = new System.Windows.Forms.TextBox();
			this.textDate = new OpenDental.ValidDate();
			this.label5 = new System.Windows.Forms.Label();
			this.listType = new System.Windows.Forms.ListBox();
			this.textSize = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.textToothNumbers = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// listCategory
			// 
			this.listCategory.Location = new System.Drawing.Point(12,30);
			this.listCategory.Name = "listCategory";
			this.listCategory.Size = new System.Drawing.Size(104,342);
			this.listCategory.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(12,14);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100,16);
			this.label1.TabIndex = 1;
			this.label1.Text = "Category";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(122,224);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(127,18);
			this.label2.TabIndex = 2;
			this.label2.Text = "Optional Description";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textDescript
			// 
			this.textDescript.Location = new System.Drawing.Point(252,221);
			this.textDescript.MaxLength = 255;
			this.textDescript.Multiline = true;
			this.textDescript.Name = "textDescript";
			this.textDescript.Size = new System.Drawing.Size(364,77);
			this.textDescript.TabIndex = 2;
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(668,368);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,25);
			this.butOK.TabIndex = 3;
			this.butOK.Text = "OK";
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
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.Location = new System.Drawing.Point(766,368);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,25);
			this.butCancel.TabIndex = 4;
			this.butCancel.Text = "Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(149,95);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(100,18);
			this.label3.TabIndex = 6;
			this.label3.Text = "Date";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(149,33);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(100,18);
			this.label4.TabIndex = 8;
			this.label4.Text = "File Name";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textFileName
			// 
			this.textFileName.Location = new System.Drawing.Point(252,30);
			this.textFileName.Name = "textFileName";
			this.textFileName.ReadOnly = true;
			this.textFileName.Size = new System.Drawing.Size(586,20);
			this.textFileName.TabIndex = 9;
			// 
			// textDate
			// 
			this.textDate.Location = new System.Drawing.Point(252,92);
			this.textDate.Name = "textDate";
			this.textDate.Size = new System.Drawing.Size(100,20);
			this.textDate.TabIndex = 1;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(149,123);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(100,18);
			this.label5.TabIndex = 11;
			this.label5.Text = "Type";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// listType
			// 
			this.listType.Location = new System.Drawing.Point(252,123);
			this.listType.Name = "listType";
			this.listType.Size = new System.Drawing.Size(104,56);
			this.listType.TabIndex = 10;
			// 
			// textSize
			// 
			this.textSize.Location = new System.Drawing.Point(252,61);
			this.textSize.Name = "textSize";
			this.textSize.ReadOnly = true;
			this.textSize.Size = new System.Drawing.Size(134,20);
			this.textSize.TabIndex = 13;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(149,64);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(100,18);
			this.label6.TabIndex = 12;
			this.label6.Text = "File Size";
			this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textToothNumbers
			// 
			this.textToothNumbers.Location = new System.Drawing.Point(252,190);
			this.textToothNumbers.Name = "textToothNumbers";
			this.textToothNumbers.Size = new System.Drawing.Size(240,20);
			this.textToothNumbers.TabIndex = 15;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(149,193);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(100,18);
			this.label7.TabIndex = 14;
			this.label7.Text = "Tooth Numbers";
			this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// FormDocInfo
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(868,419);
			this.Controls.Add(this.textToothNumbers);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.textSize);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.listType);
			this.Controls.Add(this.textDate);
			this.Controls.Add(this.textDescript);
			this.Controls.Add(this.textFileName);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.listCategory);
			this.Controls.Add(this.label4);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormDocInfo";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Item Info";
			this.Load += new System.EventHandler(this.FormDocInfo_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		///<summary></summary>
		public void FormDocInfo_Load(object sender, System.EventArgs e){
			//if (Docs.Cur.FileName.Equals(null))
			listCategory.Items.Clear();
			for(int i=0;i<DefB.Short[(int)DefCat.ImageCats].Length;i++){
				string folderName=DefB.Short[(int)DefCat.ImageCats][i].ItemName;
				listCategory.Items.Add(folderName);
				if(i==0 || DefB.Short[(int)DefCat.ImageCats][i].DefNum==DocCur.DocCategory || folderName==initialSelection){
					listCategory.SelectedIndex=i;
				}
			}
			listType.Items.Clear();
			listType.Items.AddRange(Enum.GetNames(typeof(ImageType)));
			listType.SelectedIndex=(int)DocCur.ImgType;
			textToothNumbers.Text=Tooth.FormatRangeForDisplay(DocCur.ToothNumbers);
			textDate.Text=DocCur.DateCreated.ToString("d");
			textDescript.Text=DocCur.Description;
			//When the A to Z folders are disabled, the image module is disabled and this
			//code will never get called. Thus, by the time this point in the code is reached,
			//there will be a valid image path.
			textFileName.Text=ODFileUtils.CombinePaths(new string[] {	FormPath.GetPreferredImagePath(),
																																PatCur.ImageFolder.Substring(0,1).ToUpper(),
																																PatCur.ImageFolder,
																																DocCur.FileName});
			if(File.Exists(textFileName.Text)){
				FileInfo fileInfo=new FileInfo(textFileName.Text);
				textSize.Text=fileInfo.Length.ToString("n0");
			}
			textToothNumbers.Text=Tooth.FormatRangeForDisplay(DocCur.ToothNumbers);
			//textNote.Text=DocCur.Note;
		}

		private void butOK_Click(object sender, System.EventArgs e){
			if(textDate.errorProvider1.GetError(textDate)!=""){
				MessageBox.Show(Lan.g(this,"Please fix data entry errors first."));
				return;
			}
			try{
				DocCur.ToothNumbers=Tooth.FormatRangeForDb(textToothNumbers.Text);
			}
			catch(ApplicationException ex){
				MessageBox.Show(ex.Message);
				return;
			}
			DocCur.DocCategory=DefB.Short[(int)DefCat.ImageCats][listCategory.SelectedIndex].DefNum;
			DocCur.ImgType=(ImageType)listType.SelectedIndex;
			DocCur.Description=textDescript.Text;
			DocCur.DateCreated=DateTime.Parse(textDate.Text);
			try{//incomplete
				DocCur.ToothNumbers=Tooth.FormatRangeForDb(textToothNumbers.Text);
			}
			catch(ApplicationException ex){
				MessageBox.Show(ex.Message);
				return;
			}
			//DocCur.Note=textNote.Text;
      //Docs.Cur.LastAltered=DateTime.Today;
			//if(IsNew){
			//	DocCur.Insert(PatCur);
			//}
			//else{
			Documents.Update(DocCur);
			//}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}
		
	}
}