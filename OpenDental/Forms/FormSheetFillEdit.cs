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
		//public Sheet SheetCur;
		public SheetData SheetDataCur;
		public List<SheetFieldData> SheetFields;

		public FormSheetFillEdit(SheetData sheetData,List<SheetFieldData> sheetFields) {//Sheet sheet,
			InitializeComponent();
			Lan.F(this);
			SheetDataCur=sheetData;
			SheetFields=sheetFields;
			Width=sheetData.Width+185;
			Height=sheetData.Height+60;
		}

		private void FormSheetFillEdit_Load(object sender,EventArgs e) {
			panelMain.Width=SheetDataCur.Width;
			panelMain.Height=SheetDataCur.Height;
			textDateTime.Text=SheetDataCur.DateTimeSheet.ToShortDateString()+" "+SheetDataCur.DateTimeSheet.ToShortTimeString();
			textNote.Text=SheetDataCur.InternalNote;
			TextBox textbox;
			FontStyle style;
			for(int i=0;i<SheetFields.Count;i++){
				textbox=new TextBox();
				textbox.BorderStyle=BorderStyle.None;
				textbox.Multiline=true;//due to MS malfunction at 9pt which cuts off the bottom of the text.
				if(SheetFields[i].FieldType==SheetFieldType.OutputText
					|| SheetFields[i].FieldType==SheetFieldType.StaticText)
				{
					//textbox.BackColor=Color.White;
					//textbox.BackColor=Color.FromArgb(245,245,200);
				}
				else if(SheetFields[i].FieldType==SheetFieldType.InputField){
					textbox.BackColor=Color.FromArgb(245,245,200);
				}
				textbox.Location=new Point(SheetFields[i].XPos,SheetFields[i].YPos);
				textbox.Width=SheetFields[i].Width;
				textbox.Text=SheetFields[i].FieldValue;
				style=FontStyle.Regular;
				if(SheetFields[i].FontIsBold){
					style=FontStyle.Bold;
				}
				textbox.Font=new Font(SheetFields[i].FontName,SheetFields[i].FontSize,style);
				if(SheetFields[i].Height<textbox.Font.Height+2){
					//textbox.Multiline=false;
					textbox.AcceptsReturn=false;
				}
				else{
					//textbox.Multiline=true;
					textbox.AcceptsReturn=true;
				}
				textbox.Height=SheetFields[i].Height;
				//textbox.ScrollBars=RichTextBoxScrollBars.None;
				textbox.Tag=SheetFields[i];
				panelMain.Controls.Add(textbox);
			}
		}

		private void butDelete_Click(object sender,EventArgs e) {
			if(SheetDataCur.IsNew){
				DialogResult=DialogResult.Cancel;
			}
			if(!MsgBox.Show(this,true,"Delete?")){
				return;
			}
			SheetDatas.DeleteObject(SheetDataCur.SheetDataNum);
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender,EventArgs e) {
			if(textDateTime.errorProvider1.GetError(textDateTime)!=""){
				MsgBox.Show(this,"Please fix data entry errors first.");
				return;
			}
			SheetDataCur.DateTimeSheet=PIn.PDateT(textDateTime.Text);
			SheetDataCur.InternalNote=textNote.Text;
			bool isNew=SheetDataCur.IsNew;
			SheetDatas.WriteObject(SheetDataCur);
			SaveText(this,SheetDataCur.SheetDataNum);
			DialogResult=DialogResult.OK;
		}

		///<summary>Recursively saves all SheetFieldData objects for which there is a textbox.</summary>
		private void SaveText(Control control,int sheetDataNum){
			if(control.Tag!=null){
				SheetFieldData field=(SheetFieldData)control.Tag;
				field.FieldValue=control.Text;
				field.SheetDataNum=sheetDataNum;//whether or not isnew
				SheetFieldDatas.WriteObject(field);
			}
			for(int i=0;i<control.Controls.Count;i++){
				SaveText(control.Controls[i],sheetDataNum);
			}
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	

		
	}
}