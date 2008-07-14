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
		public SheetData SheetDataCur;

		public FormSheetFillEdit(Sheet sheet,SheetData sheetData) {
			InitializeComponent();
			Lan.F(this);
			SheetCur=SheetUtil.CalculateHeights(sheet,this.CreateGraphics());
			SheetDataCur=sheetData;
			Width=sheet.Width+185;
			Height=sheet.Height+60;
		}

		private void FormSheetFillEdit_Load(object sender,EventArgs e) {
			panelMain.Width=SheetCur.Width;
			panelMain.Height=SheetCur.Height;
			textDateTime.Text=SheetDataCur.DateTimeSheet.ToShortDateString()+" "+SheetDataCur.DateTimeSheet.ToShortTimeString();
			textNote.Text=SheetDataCur.InternalNote;
			TextBox textbox;
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
				textbox.Font=SheetCur.SheetFields[i].Font;
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

		private void butOK_Click(object sender,EventArgs e) {
			if(textDateTime.errorProvider1.GetError(textDateTime)!=""){
				MsgBox.Show(this,"Please fix data entry errors first.");
				return;
			}
			SheetDataCur.DateTimeSheet=PIn.PDateT(textDateTime.Text);
			SheetDataCur.InternalNote=textNote.Text;
			SheetDatas.WriteObject(SheetDataCur);
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		
	}
}