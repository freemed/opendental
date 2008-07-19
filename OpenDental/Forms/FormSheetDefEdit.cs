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
		private bool MouseIsDown;
		private bool CtrlIsDown;
		private Point MouseOriginalPos;
		private List<Point> OriginalControlPositions;

		public FormSheetDefEdit(SheetDef sheetDef) {
			InitializeComponent();
			Lan.F(this);
			SheetDefCur=sheetDef;
			/*if(SheetDefCur.IsLandscape){
				Width=SheetDefCur.Height+185;
				Height=SheetDefCur.Width+60;
			}
			else{
				Width=SheetDefCur.Width+185;
				Height=SheetDefCur.Height+60;
			}*/
		}

		private void FormSheetDefEdit_Load(object sender,EventArgs e) {
			if(IsInternal){
				butDelete.Visible=false;
				butOK.Visible=false;
				butCancel.Text=Lan.g(this,"Close");
				groupAddNew.Visible=false;
			}
			else{
				labelInternal.Visible=false;
			}
			textDescription.Text=SheetDefCur.Description;
			if(SheetDefCur.IsLandscape){
				panelMain.Width=SheetDefCur.Height;
				panelMain.Height=SheetDefCur.Width;
			}
			else{
				panelMain.Width=SheetDefCur.Width;
				panelMain.Height=SheetDefCur.Height;
			}
			FillFieldList();
			panelMain.Invalidate();
			panelMain.Focus();
			//textDescription.Focus();
		}

		private void FillFieldList(){
			listFields.Items.Clear();
			for(int i=0;i<SheetDefCur.SheetFieldDefs.Count;i++){
				if(SheetDefCur.SheetFieldDefs[i].FieldType==SheetFieldType.StaticText){
					listFields.Items.Add(SheetDefCur.SheetFieldDefs[i].FieldValue);
				}
				else{
					listFields.Items.Add(SheetDefCur.SheetFieldDefs[i].FieldName);
				}
			}
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
			Pen penRed=new Pen(Color.Red);
			SolidBrush brushBlue=new SolidBrush(Color.Blue);
			SolidBrush brushRed=new SolidBrush(Color.Red);
			SolidBrush brush=null;
			Font font;
			FontStyle fontstyle;
			for(int i=0;i<SheetDefCur.SheetFieldDefs.Count;i++){
				if(SheetDefCur.SheetFieldDefs[i].FieldType==SheetFieldType.Parameter){
					continue;
				}
				fontstyle=FontStyle.Regular;
				if(SheetDefCur.SheetFieldDefs[i].FontIsBold){
					fontstyle=FontStyle.Bold;
				}
				font=new Font(SheetDefCur.SheetFieldDefs[i].FontName,SheetDefCur.SheetFieldDefs[i].FontSize,fontstyle);
				if(listFields.SelectedIndices.Contains(i)){
					g.DrawRectangle(penRed,SheetDefCur.SheetFieldDefs[i].Bounds);
					brush=brushRed;
				}
				else{
					g.DrawRectangle(penBlue,SheetDefCur.SheetFieldDefs[i].Bounds);
					brush=brushBlue;
				}
				if(SheetDefCur.SheetFieldDefs[i].FieldType==SheetFieldType.StaticText){
					g.DrawString(SheetDefCur.SheetFieldDefs[i].FieldValue,font,
						brush,SheetDefCur.SheetFieldDefs[i].Bounds);
				}
				else{
					g.DrawString(SheetDefCur.SheetFieldDefs[i].FieldName,font,
						brush,SheetDefCur.SheetFieldDefs[i].Bounds);
				}
			}
		}

		private void butEdit_Click(object sender,EventArgs e) {
			FormSheetDef FormS=new FormSheetDef();
			FormS.SheetDefCur=SheetDefCur;
			FormS.ShowDialog();
			if(FormS.DialogResult!=DialogResult.OK){
				return;
			}
			textDescription.Text=SheetDefCur.Description;
			//resize
			if(SheetDefCur.IsLandscape){
				panelMain.Width=SheetDefCur.Height;
				panelMain.Height=SheetDefCur.Width;
			}
			else{
				panelMain.Width=SheetDefCur.Width;
				panelMain.Height=SheetDefCur.Height;
			}
			FillFieldList();
			panelMain.Invalidate();
		}

		private void butAddOutputText_Click(object sender,EventArgs e) {
			Font font=new Font(SheetDefCur.FontName,SheetDefCur.FontSize);
			FormSheetFieldOutput FormS=new FormSheetFieldOutput();
			FormS.SheetDefCur=SheetDefCur;
			FormS.SheetFieldDefCur=SheetFieldDef.NewOutput("",SheetDefCur.FontSize,SheetDefCur.FontName,false,0,0,100,font.Height);
			if(this.IsInternal){
				FormS.IsReadOnly=true;
			}
			FormS.ShowDialog();
			if(FormS.DialogResult!=DialogResult.OK){
				return;
			}
			SheetDefCur.SheetFieldDefs.Add(FormS.SheetFieldDefCur);
			FillFieldList();
			panelMain.Invalidate();
		}

		private void butAddStaticText_Click(object sender,EventArgs e) {
			Font font=new Font(SheetDefCur.FontName,SheetDefCur.FontSize);
			FormSheetFieldStatic FormS=new FormSheetFieldStatic();
			FormS.SheetDefCur=SheetDefCur;
			FormS.SheetFieldDefCur=SheetFieldDef.NewStaticText("",SheetDefCur.FontSize,SheetDefCur.FontName,false,0,0,100,font.Height);
			if(this.IsInternal){
				FormS.IsReadOnly=true;
			}
			FormS.ShowDialog();
			if(FormS.DialogResult!=DialogResult.OK){
				return;
			}
			SheetDefCur.SheetFieldDefs.Add(FormS.SheetFieldDefCur);
			FillFieldList();
			panelMain.Invalidate();
		}

		private void butAddInputField_Click(object sender,EventArgs e) {
			if(SheetFieldsAvailable.GetListInput(SheetDefCur.SheetType).Count==0){
				MsgBox.Show(this,"There are no input fields available for this type of sheet.");
				return;
			}
			Font font=new Font(SheetDefCur.FontName,SheetDefCur.FontSize);
			FormSheetFieldInput FormS=new FormSheetFieldInput();
			FormS.SheetDefCur=SheetDefCur;
			FormS.SheetFieldDefCur=SheetFieldDef.NewInput("",SheetDefCur.FontSize,SheetDefCur.FontName,false,0,0,100,font.Height);
			if(this.IsInternal){
				FormS.IsReadOnly=true;
			}
			FormS.ShowDialog();
			if(FormS.DialogResult!=DialogResult.OK){
				return;
			}
			SheetDefCur.SheetFieldDefs.Add(FormS.SheetFieldDefCur);
			FillFieldList();
			panelMain.Invalidate();
		}

		private void listFields_Click(object sender,EventArgs e) {
			//if(listFields.SelectedIndices.Count==0){
			//	return;
			//}
			panelMain.Invalidate();
		}

		private void listFields_MouseDoubleClick(object sender,MouseEventArgs e) {
			int idx=listFields.IndexFromPoint(e.Location);
			if(idx==-1){
				return;
			}
			listFields.SelectedIndices.Clear();
			listFields.SetSelected(idx,true);
			panelMain.Invalidate();
			SheetFieldDef field=SheetDefCur.SheetFieldDefs[idx];
			LaunchEditWindow(field);
		}

		///<summary>Only for editing fields that already exist.</summary>
		private void LaunchEditWindow(SheetFieldDef field){
			//not every field will have been saved to the database, so we can't depend on SheetFieldDefNum.
			int idx=SheetDefCur.SheetFieldDefs.IndexOf(field);
			switch(field.FieldType){
				case SheetFieldType.InputField:
					FormSheetFieldInput FormS=new FormSheetFieldInput();
					FormS.SheetDefCur=SheetDefCur;
					FormS.SheetFieldDefCur=field;
					if(this.IsInternal){
						FormS.IsReadOnly=true;
					}
					FormS.ShowDialog();
					if(FormS.DialogResult!=DialogResult.OK){
						return;
					}
					if(FormS.SheetFieldDefCur==null){
						SheetDefCur.SheetFieldDefs.RemoveAt(idx);
					}
					break;
				case SheetFieldType.OutputText:
					FormSheetFieldOutput FormSO=new FormSheetFieldOutput();
					FormSO.SheetDefCur=SheetDefCur;
					FormSO.SheetFieldDefCur=field;
					if(this.IsInternal){
						FormSO.IsReadOnly=true;
					}
					FormSO.ShowDialog();
					if(FormSO.DialogResult!=DialogResult.OK){
						return;
					}
					if(FormSO.SheetFieldDefCur==null){
						SheetDefCur.SheetFieldDefs.RemoveAt(idx);
					}
					break;
				case SheetFieldType.StaticText:
					FormSheetFieldStatic FormSS=new FormSheetFieldStatic();
					FormSS.SheetDefCur=SheetDefCur;
					FormSS.SheetFieldDefCur=field;
					if(this.IsInternal){
						FormSS.IsReadOnly=true;
					}
					FormSS.ShowDialog();
					if(FormSS.DialogResult!=DialogResult.OK){
						return;
					}
					if(FormSS.SheetFieldDefCur==null){
						SheetDefCur.SheetFieldDefs.RemoveAt(idx);
					}
					break;
					return;
			}
			FillFieldList();
			listFields.SelectedIndex=idx;//reselect the item.
			panelMain.Invalidate();
		}

		private void panelMain_MouseDown(object sender,MouseEventArgs e) {
			panelMain.Select();
			MouseIsDown=true;
			MouseOriginalPos=e.Location;
			SheetFieldDef field=HitTest(e.X,e.Y);
			if(field==null){
				if(CtrlIsDown){
					return;
				}
				else{
					listFields.SelectedIndices.Clear();
					panelMain.Invalidate();
					return;
				}
			}
			int idx=SheetDefCur.SheetFieldDefs.IndexOf(field);
			if(CtrlIsDown){
				if(listFields.SelectedIndices.Contains(idx)){
					listFields.SetSelected(idx,false);	
				}
				else{
					listFields.SetSelected(idx,true);	
				}
			}
			else{
				if(listFields.SelectedIndices.Contains(idx)){
					//clicking on the group, probably to start a drag.
				}
				else{
					listFields.SelectedIndices.Clear();
					listFields.SetSelected(idx,true);
				}
			}
			OriginalControlPositions=new List<Point>();
			Point point;
			for(int i=0;i<listFields.SelectedIndices.Count;i++){
				point=new Point(SheetDefCur.SheetFieldDefs[listFields.SelectedIndices[i]].XPos,
					SheetDefCur.SheetFieldDefs[listFields.SelectedIndices[i]].YPos);
				OriginalControlPositions.Add(point);
			}
			panelMain.Invalidate();
		}

		private void panelMain_MouseMove(object sender,MouseEventArgs e) {
			if(!MouseIsDown){
				return;
			}
			for(int i=0;i<listFields.SelectedIndices.Count;i++){
				SheetDefCur.SheetFieldDefs[listFields.SelectedIndices[i]].XPos=
					OriginalControlPositions[i].X+e.X-MouseOriginalPos.X;
				SheetDefCur.SheetFieldDefs[listFields.SelectedIndices[i]].YPos=
					OriginalControlPositions[i].Y+e.Y-MouseOriginalPos.Y;
			}
			panelMain.Invalidate();
		}

		private void panelMain_MouseUp(object sender,MouseEventArgs e) {
			MouseIsDown=false;
			OriginalControlPositions=null;
		}

		private void panelMain_MouseDoubleClick(object sender,MouseEventArgs e) {
			SheetFieldDef field=HitTest(e.X,e.Y);
			if(field==null){
				return;
			}
			LaunchEditWindow(field);
		}

		private SheetFieldDef HitTest(int x,int y){
			for(int i=0;i<SheetDefCur.SheetFieldDefs.Count;i++){
				if(SheetDefCur.SheetFieldDefs[i].Bounds.Contains(x,y)){
					return SheetDefCur.SheetFieldDefs[i];
				}
			}
			return null;
		}

		private void FormSheetDefEdit_KeyDown(object sender,KeyEventArgs e) {
			e.Handled=true;
			if(e.Control){
				CtrlIsDown=true;
			}
			for(int i=0;i<listFields.SelectedIndices.Count;i++){
				switch(e.KeyCode){
					case Keys.Up:
						if(e.Shift)
							SheetDefCur.SheetFieldDefs[listFields.SelectedIndices[i]].YPos-=7;
						else
							SheetDefCur.SheetFieldDefs[listFields.SelectedIndices[i]].YPos--;
						break;
					case Keys.Down:
						if(e.Shift)
							SheetDefCur.SheetFieldDefs[listFields.SelectedIndices[i]].YPos+=7;
						else
							SheetDefCur.SheetFieldDefs[listFields.SelectedIndices[i]].YPos++;
						break;
					case Keys.Left:
						if(e.Shift)
							SheetDefCur.SheetFieldDefs[listFields.SelectedIndices[i]].XPos-=7;
						else
							SheetDefCur.SheetFieldDefs[listFields.SelectedIndices[i]].XPos--;
						break;
					case Keys.Right:
						if(e.Shift)
							SheetDefCur.SheetFieldDefs[listFields.SelectedIndices[i]].XPos+=7;
						else
							SheetDefCur.SheetFieldDefs[listFields.SelectedIndices[i]].XPos++;
						break;
					default:
						break;
				}
			}
			panelMain.Invalidate();
		}

		private void FormSheetDefEdit_KeyUp(object sender,KeyEventArgs e) {
			if((e.KeyCode & Keys.ControlKey) == Keys.ControlKey){
				CtrlIsDown=false;
			}
		}

		private void butDelete_Click(object sender,EventArgs e) {
			if(SheetDefCur.IsNew){
				DialogResult=DialogResult.Cancel;
				return;
			}
			if(!MsgBox.Show(this,true,"Delete entire sheet?")){
				return;
			}
			try{
				SheetDefs.DeleteObject(SheetDefCur.SheetDefNum);
				DialogResult=DialogResult.OK;
			}
			catch(Exception ex){
				MessageBox.Show(ex.Message);
			}
		}

		private void butOK_Click(object sender,EventArgs e) {
			SheetDefs.WriteObject(SheetDefCur);
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		

		

		

		

		

		

		

		

		

		

		
	}
}