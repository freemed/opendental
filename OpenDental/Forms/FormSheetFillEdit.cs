using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
using CodeBase;

namespace OpenDental {
	public partial class FormSheetFillEdit:Form {
		public Sheet SheetCur;
		//public List<SheetField> SheetFieldList;

		public FormSheetFillEdit(Sheet sheet){
			InitializeComponent();
			Lan.F(this);
			SheetCur=sheet;
			Width=sheet.Width+185;
			Height=sheet.Height+60;
		}

		private void FormSheetFillEdit_Load(object sender,EventArgs e) {
			panelMain.Width=SheetCur.Width;
			panelMain.Height=SheetCur.Height;
			textDateTime.Text=SheetCur.DateTimeSheet.ToShortDateString()+" "+SheetCur.DateTimeSheet.ToShortTimeString();
			textNote.Text=SheetCur.InternalNote;
			RichTextBox textbox;//has to be richtextbox due to MS bug that doesn't show cursor.
			FontStyle style;
			//first, draw images
			foreach(SheetField field in SheetCur.SheetFields){
				if(field.FieldType!=SheetFieldType.Image){
					continue;
				}
				string filePathAndName=ODFileUtils.CombinePaths(SheetUtil.GetImagePath(),field.FieldName);
				if(!File.Exists(filePathAndName)){
					continue;
				}
				Image img=Image.FromFile(filePathAndName);
				PictureBox pictBox=new PictureBox();
				pictBox.Location=new Point(field.XPos,field.YPos);
				pictBox.Width=field.Width;
				pictBox.Height=field.Height;
				pictBox.Image=img;
				pictBox.SizeMode=PictureBoxSizeMode.StretchImage;
				pictBox.Tag=field;
				panelMain.Controls.Add(pictBox);
				pictBox.BringToFront();
			}
			//then, draw textboxes
			foreach(SheetField field in SheetCur.SheetFields){
				if(field.FieldType!=SheetFieldType.InputField
					&& field.FieldType!=SheetFieldType.OutputText
					&& field.FieldType!=SheetFieldType.StaticText)
				{
					continue;
				}
				textbox=new RichTextBox();
				textbox.BorderStyle=BorderStyle.None;
				//textbox.Multiline=true;//due to MS malfunction at 9pt which cuts off the bottom of the text.
				if(field.FieldType==SheetFieldType.OutputText
					|| field.FieldType==SheetFieldType.StaticText)
				{
					//textbox.BackColor=Color.White;
					//textbox.BackColor=Color.FromArgb(245,245,200);
				}
				else if(field.FieldType==SheetFieldType.InputField){
					textbox.BackColor=Color.FromArgb(245,245,200);
				}
				textbox.Location=new Point(field.XPos,field.YPos);
				textbox.Width=field.Width;
				textbox.Text=field.FieldValue;
				style=FontStyle.Regular;
				if(field.FontIsBold){
					style=FontStyle.Bold;
				}
				textbox.Font=new Font(field.FontName,field.FontSize,style);
				if(field.Height<textbox.Font.Height+2){
					textbox.Multiline=false;
					//textbox.AcceptsReturn=false;
				}
				else{
					textbox.Multiline=true;
					//textbox.AcceptsReturn=true;
				}
				textbox.Height=field.Height;
				//textbox.ScrollBars=RichTextBoxScrollBars.None;
				textbox.Tag=field;
				panelMain.Controls.Add(textbox);
				textbox.BringToFront();
			}
		}

		private void butPrint_Click(object sender,EventArgs e) {
			if(!TryToSaveData()){
				return;
			}
			//whether this is a new sheet, or one pulled from the database,
			//it will have the extra parameter we are looking for.
			//A new sheet will also have a PatNum parameter which we will ignore.
			FormSheetOutputFormat FormS=new FormSheetOutputFormat();
			if(SheetCur.SheetType==SheetTypeEnum.ReferralSlip){
				FormS.PaperCopies=2;
			}
			else{
				FormS.PaperCopies=1;//although not used yet.
			}
			Patient pat=Patients.GetPat(SheetCur.PatNum);
			if(pat.Email!=""){
				FormS.EmailPatAddress=pat.Email;
				FormS.EmailPat=true;
				FormS.PaperCopies--;
			}
			Referral referral=null;
			if(SheetCur.SheetType==SheetTypeEnum.ReferralSlip){
				FormS.Email2Visible=true;
				int referralNum=PIn.PInt(SheetParameter.GetParamByName(SheetCur.Parameters,"ReferralNum").ParamValue.ToString());
				referral=Referrals.GetReferral(referralNum);
				if(referral.EMail!=""){
					FormS.Email2Address=referral.EMail;
					FormS.Email2=true;
					FormS.PaperCopies--;
				}
			}
			FormS.ShowDialog();
			if(FormS.DialogResult!=DialogResult.OK){
				return;
			}
			if(FormS.PaperCopies>0){
				SheetPrinting.Print(SheetCur,FormS.PaperCopies);
			}
			EmailMessage message;
			Random rnd=new Random();
			string attachPath=FormEmailMessageEdit.GetAttachPath();
			string fileName;
			string filePathAndName;
			if(FormS.EmailPat){
				fileName=DateTime.Now.ToString("yyyyMMdd")+"_"+DateTime.Now.TimeOfDay.Ticks.ToString()+rnd.Next(1000).ToString()+".pdf";
				filePathAndName=ODFileUtils.CombinePaths(attachPath,fileName);
				SheetPrinting.CreatePdf(SheetCur,filePathAndName);
				//Process.Start(filePathAndName);
				message=new EmailMessage();
				message.PatNum=SheetCur.PatNum;
				message.ToAddress=FormS.EmailPatAddress;
				message.FromAddress=PrefC.GetString("EmailSenderAddress");
				message.Subject=SheetCur.SheetType.ToString();//this could be improved
				EmailAttach attach=new EmailAttach();
				attach.DisplayedFileName=SheetCur.SheetType.ToString()+".pdf";
				attach.ActualFileName=fileName;
				message.Attachments.Add(attach);
				FormEmailMessageEdit FormE=new FormEmailMessageEdit(message);
				FormE.IsNew=true;
				FormE.ShowDialog();
			}
			if(SheetCur.SheetType==SheetTypeEnum.ReferralSlip && FormS.Email2){
				//email referral
				fileName=DateTime.Now.ToString("yyyyMMdd")+"_"+DateTime.Now.TimeOfDay.Ticks.ToString()+rnd.Next(1000).ToString()+".pdf";
				filePathAndName=ODFileUtils.CombinePaths(attachPath,fileName);
				SheetPrinting.CreatePdf(SheetCur,filePathAndName);
				//Process.Start(filePathAndName);
				message=new EmailMessage();
				message.PatNum=SheetCur.PatNum;
				message.ToAddress=FormS.Email2Address;
				message.FromAddress=PrefC.GetString("EmailSenderAddress");
				message.Subject=SheetCur.SheetType.ToString()+" to "+Referrals.GetNameFL(referral.ReferralNum);//this could be improved
				EmailAttach attach=new EmailAttach();
				attach.DisplayedFileName=SheetCur.SheetType.ToString()+".pdf";
				attach.ActualFileName=fileName;
				message.Attachments.Add(attach);
				FormEmailMessageEdit FormE=new FormEmailMessageEdit(message);
				FormE.IsNew=true;
				FormE.ShowDialog();
			}
			DialogResult=DialogResult.OK;
		}

		private void butDelete_Click(object sender,EventArgs e) {
			if(SheetCur.IsNew){
				DialogResult=DialogResult.Cancel;
			}
			if(!MsgBox.Show(this,true,"Delete?")){
				return;
			}
			Sheets.DeleteObject(SheetCur.SheetNum);
			DialogResult=DialogResult.OK;
		}

		private bool TryToSaveData(){
			if(textDateTime.errorProvider1.GetError(textDateTime)!=""){
				MsgBox.Show(this,"Please fix data entry errors first.");
				return false;
			}
			SheetCur.DateTimeSheet=PIn.PDateT(textDateTime.Text);
			SheetCur.InternalNote=textNote.Text;
			bool isNew=SheetCur.IsNew;
			Sheets.WriteObject(SheetCur);
			//SaveText();//this,SheetCur.SheetNum);
			foreach(Control control in panelMain.Controls){
				if(control.Tag==null){
					continue;
				}
				SheetField field=(SheetField)control.Tag;
				if(control.GetType()==typeof(RichTextBox)){
					field.FieldValue=control.Text;
				}
				field.SheetNum=SheetCur.SheetNum;//whether or not isnew
				SheetFields.WriteObject(field);
			}
			if(isNew){
				Sheets.SaveParameters(SheetCur);
			}
			return true;
		}

		/*
		///<summary>Recursively saves all SheetField objects for which there is a textbox.</summary>
		private void SaveText(Control control,int sheetNum){
			if(control.Tag!=null){
				SheetField field=(SheetField)control.Tag;
				field.FieldValue=control.Text;
				field.SheetNum=sheetNum;//whether or not isnew
				SheetFields.WriteObject(field);
			}
			for(int i=0;i<control.Controls.Count;i++){
				SaveText(control.Controls[i],sheetNum);
			}
		}*/

		private void butOK_Click(object sender,EventArgs e) {
			if(!TryToSaveData()){
				return;
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

	

		
	}
}