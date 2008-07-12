using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormSheetFill:Form {
		public Sheet SheetCur;

		public FormSheetFill() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormSheetFill_Load(object sender,EventArgs e) {
			panelMain.Width=SheetCur.Width;
			panelMain.Height=SheetCur.Height;
			TextBox textbox;
			for(int i=0;i<SheetCur.SheetFields.Count;i++){
				textbox=new TextBox();
				textbox.BorderStyle=BorderStyle.None;
				if(SheetCur.SheetFields[i].FieldType==SheetFieldType.OutputText
					|| SheetCur.SheetFields[i].FieldType==SheetFieldType.StaticText)
				{
					//textbox.BackColor=Color.White;
				}
				else if(SheetCur.SheetFields[i].FieldType==SheetFieldType.InputField){
					textbox.BackColor=Color.FromArgb(245,245,200);
				}
				textbox.Location=new Point(SheetCur.SheetFields[i].XPos,SheetCur.SheetFields[i].YPos);
				textbox.Width=SheetCur.SheetFields[i].Width;
				textbox.Height=SheetCur.SheetFields[i].Height;
				textbox.Text=SheetCur.SheetFields[i].FieldValue;
				textbox.Font=SheetCur.SheetFields[i].Font;
				if(SheetCur.SheetFields[i].Height<textbox.Font.Height+10){
					textbox.Multiline=false;
				}
				else{
					textbox.Multiline=true;
				}
				//textbox.ScrollBars=RichTextBoxScrollBars.None;
				textbox.Tag=SheetCur.SheetFields[i];
				panelMain.Controls.Add(textbox);
			}
		}

		private void butOK_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		
	}
}