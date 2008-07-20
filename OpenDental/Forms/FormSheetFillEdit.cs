using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormSheetFillEdit:Form {
		public Sheet SheetCur;
		//public List<SheetField> SheetFieldList;

		public FormSheetFillEdit(Sheet sheet){//,List<SheetField> sheetFieldList) {
			InitializeComponent();
			Lan.F(this);
			SheetCur=sheet;
			//SheetFieldList=sheetFieldList;
			Width=sheet.Width+185;
			Height=sheet.Height+60;
		}

		private void FormSheetFillEdit_Load(object sender,EventArgs e) {
			panelMain.Width=SheetCur.Width;
			panelMain.Height=SheetCur.Height;
			textDateTime.Text=SheetCur.DateTimeSheet.ToShortDateString()+" "+SheetCur.DateTimeSheet.ToShortTimeString();
			textNote.Text=SheetCur.InternalNote;
			TextBox textbox;
			FontStyle style;
			for(int i=0;i<SheetCur.SheetFields.Count;i++){
				textbox=new TextBox();
				textbox.BorderStyle=BorderStyle.None;
				textbox.Multiline=true;//due to MS malfunction at 9pt which cuts off the bottom of the text.
				if(SheetCur.SheetFields[i].FieldType==SheetFieldType.OutputText
					|| SheetCur.SheetFields[i].FieldType==SheetFieldType.StaticText)
				{
					//textbox.BackColor=Color.White;
					//textbox.BackColor=Color.FromArgb(245,245,200);
				}
				else if(SheetCur.SheetFields[i].FieldType==SheetFieldType.InputField){
					textbox.BackColor=Color.FromArgb(245,245,200);
				}
				textbox.Location=new Point(SheetCur.SheetFields[i].XPos,SheetCur.SheetFields[i].YPos);
				textbox.Width=SheetCur.SheetFields[i].Width;
				textbox.Text=SheetCur.SheetFields[i].FieldValue;
				style=FontStyle.Regular;
				if(SheetCur.SheetFields[i].FontIsBold){
					style=FontStyle.Bold;
				}
				textbox.Font=new Font(SheetCur.SheetFields[i].FontName,SheetCur.SheetFields[i].FontSize,style);
				if(SheetCur.SheetFields[i].Height<textbox.Font.Height+2){
					//textbox.Multiline=false;
					textbox.AcceptsReturn=false;
				}
				else{
					//textbox.Multiline=true;
					textbox.AcceptsReturn=true;
				}
				textbox.Height=SheetCur.SheetFields[i].Height;
				//textbox.ScrollBars=RichTextBoxScrollBars.None;
				textbox.Tag=SheetCur.SheetFields[i];
				panelMain.Controls.Add(textbox);
			}
		}

		private void butPrint_Click(object sender,EventArgs e) {

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

		private void butOK_Click(object sender,EventArgs e) {
			if(textDateTime.errorProvider1.GetError(textDateTime)!=""){
				MsgBox.Show(this,"Please fix data entry errors first.");
				return;
			}
			SheetCur.DateTimeSheet=PIn.PDateT(textDateTime.Text);
			SheetCur.InternalNote=textNote.Text;
			bool isNew=SheetCur.IsNew;
			Sheets.WriteObject(SheetCur);
			SaveText(this,SheetCur.SheetNum);
			DialogResult=DialogResult.OK;
		}

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
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

	

		
	}
}