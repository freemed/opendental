using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormSheetDefEdit:Form {
		public SheetDef SheetDefCur;
		public bool IsInternal;

		public FormSheetDefEdit(SheetDef sheetDef) {
			InitializeComponent();
			Lan.F(this);
			SheetDefCur=sheetDef;
			Width=SheetDefCur.Width+185;
			Height=SheetDefCur.Height+60;
		}

		private void FormSheetDefEdit_Load(object sender,EventArgs e) {
			if(IsInternal){
				butDelete.Visible=false;
				butOK.Visible=false;
				butCancel.Text=Lan.g(this,"Close");
			}
			else{
				labelInternal.Visible=false;
			}
			panelMain.Width=SheetDefCur.Width;
			panelMain.Height=SheetDefCur.Height;



		}

		private void panelMain_Paint(object sender,PaintEventArgs e) {
			//if(IsUpdating) return;
			//if(Width<1 || Height<1) {
			//	return;
			//}
			Bitmap doubleBuffer=new Bitmap(panelMain.Width,panelMain.Height);
			Graphics g=Graphics.FromImage(doubleBuffer);
			g.FillRectangle(Brushes.White,0,0,doubleBuffer.Width,doubleBuffer.Height);
			DrawFields(g);
			e.Graphics.DrawImage(doubleBuffer,0,0);
			g.Dispose();
		}

		private void DrawFields(Graphics g){
			Pen penBlue=new Pen(Color.Blue);
			for(int i=0;i<SheetDefCur.SheetFieldDefs.Count;i++){
				if(SheetDefCur.SheetFieldDefs[i].FieldType==SheetFieldType.Parameter){
					continue;
				}
				g.DrawRectangle(penBlue,SheetDefCur.SheetFieldDefs[i].Bounds);
				if(SheetDefCur.SheetFieldDefs[i].FieldType==SheetFieldType.StaticText){
					g.DrawString(SheetDefCur.SheetFieldDefs[i].FieldValue,SheetDefCur.SheetFieldDefs[i].Font,Brushes.Black,SheetDefCur.SheetFieldDefs[i].Bounds);
				}
				else{
					g.DrawString(SheetDefCur.SheetFieldDefs[i].FieldName,SheetDefCur.SheetFieldDefs[i].Font,Brushes.Black,SheetDefCur.SheetFieldDefs[i].Bounds);
				}
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