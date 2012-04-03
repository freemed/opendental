using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
using CodeBase;

namespace OpenDental {
	public partial class FormSheetDefEdit:Form {
		public SheetDef SheetDefCur;
		public bool IsInternal;
		private bool MouseIsDown;
		private bool CtrlIsDown;
		private Point MouseOriginalPos;
		private Point MouseCurrentPos;
		private List<Point> OriginalControlPositions;
		///<summary>When you first mouse down, if you clicked on a valid control, this will be false.  For drag selection, this must be true.</summary>
		private bool ClickedOnBlankSpace;
		private bool AltIsDown;
		private List<SheetFieldDef> ListSheetFieldDefsCopyPaste;
		private int PasteOffset=0;
		///<summary>After each 10 pastes to the upper left origin, this increments 10 to shift the next 10 down.</summary>
		private int PasteOffsetY=0;
		private bool IsTabMode;
		private List<SheetFieldDef> ListSheetFieldDefsTabOrder;
		public static Font tabOrderFont = new Font("Times New Roman",12f,FontStyle.Regular,GraphicsUnit.Pixel);
		private Bitmap BmBackground;
		private Graphics GraphicsBackground;

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
			if(sheetDef.IsLandscape){
				Width=sheetDef.Height+190;
				Height=sheetDef.Width+65;
			}
			else{
				Width=sheetDef.Width+190;
				Height=sheetDef.Height+65;
			}
			if(Width<600){
				Width=600;
			}
			if(Height<600){
				Height=600;
			}
			if(Width>SystemInformation.WorkingArea.Width){
				Width=SystemInformation.WorkingArea.Width;
			}
			if(Height>SystemInformation.WorkingArea.Height){
				Height=SystemInformation.WorkingArea.Height;
			}
		}

		private void FormSheetDefEdit_Load(object sender,EventArgs e) {
			if(IsInternal){
				butDelete.Visible=false;
				butOK.Visible=false;
				butCancel.Text=Lan.g(this,"Close");
				groupAddNew.Visible=false;
				butAlignLeft.Visible=false;
				butAlignTop.Visible=false;
				linkLabelTips.Visible=false;
				butCopy.Visible=false;
				butPaste.Visible=false;
				butTabOrder.Visible=false;
			}
			else{
				labelInternal.Visible=false;
				//butAlignLeft.Visible=true;
				//butAlignTop.Visible=true;
				//butCopy.Visible=true;
				//butPaste.Visible=true;
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
			RefreshDoubleBuffer();
			panelMain.Refresh();
			panelMain.Focus();
			//textDescription.Focus();
		}

		private void FillFieldList(){
			listFields.Items.Clear();
			string txt;
			SheetDefCur.SheetFieldDefs.Sort(CompareTabOrder);
			for(int i=0;i<SheetDefCur.SheetFieldDefs.Count;i++){
				if(SheetDefCur.SheetFieldDefs[i].FieldType==SheetFieldType.StaticText){
					listFields.Items.Add(SheetDefCur.SheetFieldDefs[i].FieldValue);
				}
				else if(SheetDefCur.SheetFieldDefs[i].FieldType==SheetFieldType.Image){
					listFields.Items.Add(Lan.g(this,"Image:")+SheetDefCur.SheetFieldDefs[i].FieldName);
				}
				else if(SheetDefCur.SheetFieldDefs[i].FieldType==SheetFieldType.Line){
					listFields.Items.Add(Lan.g(this,"Line:")
						+SheetDefCur.SheetFieldDefs[i].XPos.ToString()+","
						+SheetDefCur.SheetFieldDefs[i].YPos.ToString()+","
						+"W:"+SheetDefCur.SheetFieldDefs[i].Width.ToString()+","
						+"H:"+SheetDefCur.SheetFieldDefs[i].Height.ToString());
				}
				else if(SheetDefCur.SheetFieldDefs[i].FieldType==SheetFieldType.Rectangle){
					listFields.Items.Add(Lan.g(this,"Rect:")
						+SheetDefCur.SheetFieldDefs[i].XPos.ToString()+","
						+SheetDefCur.SheetFieldDefs[i].YPos.ToString()+","
						+"W:"+SheetDefCur.SheetFieldDefs[i].Width.ToString()+","
						+"H:"+SheetDefCur.SheetFieldDefs[i].Height.ToString());
				}
				else if(SheetDefCur.SheetFieldDefs[i].FieldType==SheetFieldType.SigBox){
					listFields.Items.Add(Lan.g(this,"Signature Box"));
				}
				else if(SheetDefCur.SheetFieldDefs[i].FieldType==SheetFieldType.CheckBox){
					txt=//Lan.g(this,"Check:")+
						SheetDefCur.SheetFieldDefs[i].TabOrder.ToString()+": "+
						SheetDefCur.SheetFieldDefs[i].FieldName;
					if(SheetDefCur.SheetFieldDefs[i].RadioButtonValue!="") {
						txt+=" - "+SheetDefCur.SheetFieldDefs[i].RadioButtonValue;
					}
					listFields.Items.Add(txt);
				}
				else if(SheetDefCur.SheetFieldDefs[i].FieldType==SheetFieldType.InputField){
					listFields.Items.Add(SheetDefCur.SheetFieldDefs[i].TabOrder.ToString()+": "+SheetDefCur.SheetFieldDefs[i].FieldName);
				}
				else {
					listFields.Items.Add(SheetDefCur.SheetFieldDefs[i].FieldName);
				}
			}
		}

		///<summary>This is a comparator function used by List&lt;T&gt;.Sort() 
		///When compairing SheetFieldDef.TabOrder it returns a negative number if def1&lt;def2, 0 if def1==def2, and a positive number if def1&gt;def2.
		///Does not handle null values, but there should never be any instances of null being passed in. 
		///Must always return 0 when compairing item to itself.
		///This function should probably be moved to SheetFieldDefs.</summary>
		private static int CompareTabOrder(SheetFieldDef def1,SheetFieldDef def2) {
			if(def1.FieldType==def2.FieldType) {
				//do nothing
			}
			else if(def1.FieldType==SheetFieldType.Image) {//Always move images to the top of the list. This is because of the way the sheet is drawn.
				return -1;
			}
			else if(def1.FieldType==SheetFieldType.OutputText) {//Move Output text to the top of the list under images.
				return -1;
			}
			if(def1.TabOrder-def2.TabOrder==0) {
				int comp=(def1.FieldName+def1.RadioButtonValue).CompareTo(def2.FieldName+def2.RadioButtonValue);//RadioButtionValuecan be filled or ""
				if(comp!=0) {
					return comp;
				}
				comp=def1.YPos-def2.YPos;//arbitrarily order by YPos if both controls have the same tab orer and name. This will only happen if both fields are either identical or if they are both misc fields.
				if(comp!=0) {
					return comp;
				}
				return def1.XPos-def2.XPos;//If tabOrder, Name, and YPos are equal then compare based on X coordinate. 
			}
			return def1.TabOrder-def2.TabOrder;
		}

		private void panelMain_Paint(object sender,PaintEventArgs e) {
			Bitmap doubleBuffer=new Bitmap(panelMain.Width,panelMain.Height);
			Graphics g=Graphics.FromImage(doubleBuffer);
			g.DrawImage(BmBackground,0,0);
			DrawFields(g,false);
			e.Graphics.DrawImage(doubleBuffer,0,0);
			g.Dispose();
			doubleBuffer.Dispose();
			doubleBuffer=null;
		}

		///<summary>Whenever a user might have edited or moved a background image, this gets called.</summary>
		private void RefreshDoubleBuffer() {
			GraphicsBackground.FillRectangle(Brushes.White,0,0,BmBackground.Width,BmBackground.Height);
			DrawFields(GraphicsBackground,true);
		}

		///<summary>If drawImages is true then only image fields will be drawn. Otherwise, all fields but images will be drawn.</summary>
		private void DrawFields(Graphics g,bool onlyDrawImages){
			g.SmoothingMode=SmoothingMode.HighQuality;
			g.CompositingQuality=CompositingQuality.HighQuality;//This has to be here or the line thicknesses are wrong.
			//g.InterpolationMode=InterpolationMode.High;//This doesn't seem to help
			Pen penBlue=new Pen(Color.Blue);
			Pen penRed=new Pen(Color.Red);
			Pen penBlueThick=new Pen(Color.Blue,1.6f);
			Pen penRedThick=new Pen(Color.Red,1.6f);
			Pen penBlack=new Pen(Color.Black);
			Pen penSelection=new Pen(Color.Black);
			Pen pen;
			Brush brush;
			SolidBrush brushBlue=new SolidBrush(Color.Blue);
			SolidBrush brushRed=new SolidBrush(Color.Red);
			Font font;
			FontStyle fontstyle;
			for(int i=0;i<SheetDefCur.SheetFieldDefs.Count;i++){
				if(SheetDefCur.SheetFieldDefs[i].FieldType==SheetFieldType.Parameter){
					continue;
				}
				if(SheetDefCur.SheetFieldDefs[i].FieldType==SheetFieldType.Image) {
					if(onlyDrawImages) {
						string filePathAndName=ODFileUtils.CombinePaths(SheetUtil.GetImagePath(),SheetDefCur.SheetFieldDefs[i].FieldName);
						Image img=null;
						if(SheetDefCur.SheetFieldDefs[i].FieldName=="Patient Info.gif") {
							img=Properties.Resources.Patient_Info;
						}
						else if(File.Exists(filePathAndName)) {
							img=Image.FromFile(filePathAndName);
						}
						else {
							continue;
						}
						g.DrawImage(img,SheetDefCur.SheetFieldDefs[i].XPos,SheetDefCur.SheetFieldDefs[i].YPos,
							SheetDefCur.SheetFieldDefs[i].Width,SheetDefCur.SheetFieldDefs[i].Height);
					}
					continue;
				}
				if(onlyDrawImages) {
					continue;//Only draw the images for the background.
				}
				if(SheetDefCur.SheetFieldDefs[i].FieldType==SheetFieldType.Line){
					if(listFields.SelectedIndices.Contains(i)){
						pen=penRed;
					}
					else{
						pen=penBlack;
					}
					g.DrawLine(pen,SheetDefCur.SheetFieldDefs[i].XPos,SheetDefCur.SheetFieldDefs[i].YPos,
						SheetDefCur.SheetFieldDefs[i].XPos+SheetDefCur.SheetFieldDefs[i].Width,
						SheetDefCur.SheetFieldDefs[i].YPos+SheetDefCur.SheetFieldDefs[i].Height);
					continue;
				}
				if(SheetDefCur.SheetFieldDefs[i].FieldType==SheetFieldType.Rectangle){
					if(listFields.SelectedIndices.Contains(i)){
						pen=penRed;
					}
					else{
						pen=penBlack;
					}
					g.DrawRectangle(pen,SheetDefCur.SheetFieldDefs[i].XPos,SheetDefCur.SheetFieldDefs[i].YPos,
						SheetDefCur.SheetFieldDefs[i].Width,SheetDefCur.SheetFieldDefs[i].Height);
					continue;
				}
				if(SheetDefCur.SheetFieldDefs[i].FieldType==SheetFieldType.CheckBox){
					if(listFields.SelectedIndices.Contains(i)){
						pen=penRedThick;
					}
					else{
						pen=penBlueThick;
					}
					//g.DrawRectangle(pen,SheetDefCur.SheetFieldDefs[i].XPos,SheetDefCur.SheetFieldDefs[i].YPos,
					//	SheetDefCur.SheetFieldDefs[i].Width,SheetDefCur.SheetFieldDefs[i].Height);
					g.DrawLine(pen,SheetDefCur.SheetFieldDefs[i].XPos,
						SheetDefCur.SheetFieldDefs[i].YPos,
						SheetDefCur.SheetFieldDefs[i].XPos+SheetDefCur.SheetFieldDefs[i].Width-1,
						SheetDefCur.SheetFieldDefs[i].YPos+SheetDefCur.SheetFieldDefs[i].Height-1);
					g.DrawLine(pen,SheetDefCur.SheetFieldDefs[i].XPos+SheetDefCur.SheetFieldDefs[i].Width-1,
						SheetDefCur.SheetFieldDefs[i].YPos,
						SheetDefCur.SheetFieldDefs[i].XPos,
						SheetDefCur.SheetFieldDefs[i].YPos+SheetDefCur.SheetFieldDefs[i].Height-1);
					if(IsTabMode) {
						Rectangle tabRect = new Rectangle(
							SheetDefCur.SheetFieldDefs[i].XPos-1,//X
							SheetDefCur.SheetFieldDefs[i].YPos-1,//Y
							(int)g.MeasureString(SheetDefCur.SheetFieldDefs[i].TabOrder.ToString(),tabOrderFont).Width+1,//Width
							12);//height
						if(ListSheetFieldDefsTabOrder.Contains(SheetDefCur.SheetFieldDefs[i])) {//blue border, white box, blue letters
							g.FillRectangle(Brushes.White,tabRect);
							g.DrawRectangle(Pens.Blue,tabRect);
							g.DrawString(SheetDefCur.SheetFieldDefs[i].TabOrder.ToString(),tabOrderFont,Brushes.Blue,tabRect.X,tabRect.Y-1);
							//GraphicsHelper.DrawString(g,g,SheetDefCur.SheetFieldDefs[i].TabOrder.ToString(),SheetDefCur.GetFont(),Brushes.Blue,tabRect);
						}
						else {//Blue border, blue box, white letters
							g.FillRectangle(brushBlue,tabRect);
							g.DrawString(SheetDefCur.SheetFieldDefs[i].TabOrder.ToString(),tabOrderFont,Brushes.White,tabRect.X,tabRect.Y-1);
							//GraphicsHelper.DrawString(g,g,SheetDefCur.SheetFieldDefs[i].TabOrder.ToString(),SheetDefCur.GetFont(),Brushes.White,tabRect);
						}
					}
					continue;
				}
				if(SheetDefCur.SheetFieldDefs[i].FieldType==SheetFieldType.SigBox){
					//font=new Font(Font,
					if(listFields.SelectedIndices.Contains(i)){
						pen=penRed;
						brush=brushRed;
					}
					else{
						pen=penBlue;
						brush=brushBlue;
					}
					g.DrawRectangle(pen,SheetDefCur.SheetFieldDefs[i].XPos,SheetDefCur.SheetFieldDefs[i].YPos,
						SheetDefCur.SheetFieldDefs[i].Width,SheetDefCur.SheetFieldDefs[i].Height);
					g.DrawString("(signature box)",Font,brush,SheetDefCur.SheetFieldDefs[i].XPos,SheetDefCur.SheetFieldDefs[i].YPos);
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
				string str;
				if(SheetDefCur.SheetFieldDefs[i].FieldType==SheetFieldType.StaticText){
					str=SheetDefCur.SheetFieldDefs[i].FieldValue;
					//g.DrawString(SheetDefCur.SheetFieldDefs[i].FieldValue,font,
					//	brush,SheetDefCur.SheetFieldDefs[i].Bounds);
				}
				else{
					str=SheetDefCur.SheetFieldDefs[i].FieldName;
					//g.DrawString(SheetDefCur.SheetFieldDefs[i].FieldName,font,
					//	brush,SheetDefCur.SheetFieldDefs[i].Bounds);
				}
				if(ClickedOnBlankSpace) {
					g.DrawRectangle(penSelection,
						//The math functions are used below to account for users clicking and dragging up, down, left, or right.
						Math.Min(MouseOriginalPos.X,MouseCurrentPos.X),//X
						Math.Min(MouseOriginalPos.Y,MouseCurrentPos.Y),//Y
						Math.Abs(MouseCurrentPos.X-MouseOriginalPos.X),//Width
						Math.Abs(MouseCurrentPos.Y-MouseOriginalPos.Y));//Height
				}
				g.DrawString(str,font,brush,SheetDefCur.SheetFieldDefs[i].Bounds);
				//GraphicsHelper.DrawString(g,g,str,font,brush,SheetDefCur.SheetFieldDefs[i].Bounds);
				if(IsTabMode && SheetDefCur.SheetFieldDefs[i].FieldType==SheetFieldType.InputField) {
					Rectangle tabRect = new Rectangle(
						SheetDefCur.SheetFieldDefs[i].XPos-1,//X
						SheetDefCur.SheetFieldDefs[i].YPos-1,//Y
						(int)g.MeasureString(SheetDefCur.SheetFieldDefs[i].TabOrder.ToString(),tabOrderFont).Width+1,//Width
						12);//height
					if(ListSheetFieldDefsTabOrder.Contains(SheetDefCur.SheetFieldDefs[i])) {//blue border, white box, blue letters
						g.FillRectangle(Brushes.White,tabRect);
						g.DrawRectangle(Pens.Blue,tabRect);
						g.DrawString(SheetDefCur.SheetFieldDefs[i].TabOrder.ToString(),tabOrderFont,Brushes.Blue,tabRect.X,tabRect.Y-1);
						//GraphicsHelper.DrawString(g,g,SheetDefCur.SheetFieldDefs[i].TabOrder.ToString(),SheetDefCur.GetFont(),Brushes.Blue,tabRect);
					}
					else {//Blue border, blue box, white letters
						g.FillRectangle(brushBlue,tabRect);
						g.DrawString(SheetDefCur.SheetFieldDefs[i].TabOrder.ToString(),tabOrderFont,Brushes.White,tabRect.X,tabRect.Y-1);
						//GraphicsHelper.DrawString(g,g,SheetDefCur.SheetFieldDefs[i].TabOrder.ToString(),SheetDefCur.GetFont(),Brushes.White,tabRect);
					}
				}
			}
		}

		private void butEdit_Click(object sender,EventArgs e) {
			FormSheetDef FormS=new FormSheetDef();
			FormS.SheetDefCur=SheetDefCur;
			if(this.IsInternal){
				FormS.IsReadOnly=true;
			}
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
			RefreshDoubleBuffer();
			panelMain.Refresh();
		}

		private void butAddOutputText_Click(object sender,EventArgs e) {
			if(SheetFieldsAvailable.GetList(SheetDefCur.SheetType,OutInCheck.Out).Count==0) {
				MsgBox.Show(this,"There are no output fields available for this type of sheet.");
				return;
			}
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
			panelMain.Refresh();
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
			panelMain.Refresh();
		}

		private void butAddInputField_Click(object sender,EventArgs e) {
			if(SheetFieldsAvailable.GetList(SheetDefCur.SheetType,OutInCheck.In).Count==0){
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
			panelMain.Refresh();
		}

		private void butAddImage_Click(object sender,EventArgs e) {
			if(!PrefC.UsingAtoZfolder) {
				MsgBox.Show(this,"Not allowed because not using AtoZ folder");
				return;
			}
			//Font font=new Font(SheetDefCur.FontName,SheetDefCur.FontSize);
			FormSheetFieldImage FormS=new FormSheetFieldImage();
			FormS.SheetDefCur=SheetDefCur;
			FormS.SheetFieldDefCur=SheetFieldDef.NewImage("",0,0,100,100);
			if(this.IsInternal){
				FormS.IsReadOnly=true;
			}
			FormS.ShowDialog();
			if(FormS.DialogResult!=DialogResult.OK){
				return;
			}
			SheetDefCur.SheetFieldDefs.Insert(0,FormS.SheetFieldDefCur);
			FillFieldList();
			RefreshDoubleBuffer();
			panelMain.Refresh();
		}

		private void butAddLine_Click(object sender,EventArgs e) {
			FormSheetFieldLine FormS=new FormSheetFieldLine();
			FormS.SheetDefCur=SheetDefCur;
			FormS.SheetFieldDefCur=SheetFieldDef.NewLine(0,0,0,0);
			if(this.IsInternal){
				FormS.IsReadOnly=true;
			}
			FormS.ShowDialog();
			if(FormS.DialogResult!=DialogResult.OK){
				return;
			}
			SheetDefCur.SheetFieldDefs.Add(FormS.SheetFieldDefCur);
			FillFieldList();
			panelMain.Refresh();
		}

		private void butAddRect_Click(object sender,EventArgs e) {
			FormSheetFieldRect FormS=new FormSheetFieldRect();
			FormS.SheetDefCur=SheetDefCur;
			FormS.SheetFieldDefCur=SheetFieldDef.NewRect(0,0,0,0);
			if(this.IsInternal){
				FormS.IsReadOnly=true;
			}
			FormS.ShowDialog();
			if(FormS.DialogResult!=DialogResult.OK){
				return;
			}
			SheetDefCur.SheetFieldDefs.Add(FormS.SheetFieldDefCur);
			FillFieldList();
			panelMain.Refresh();
		}

		private void butAddCheckBox_Click(object sender,EventArgs e) {
			if(SheetFieldsAvailable.GetList(SheetDefCur.SheetType,OutInCheck.Check).Count==0){
				MsgBox.Show(this,"There are no checkbox fields available for this type of sheet.");
				return;
			}
			FormSheetFieldCheckBox FormS=new FormSheetFieldCheckBox();
			FormS.IsNew=true;
			FormS.SheetDefCur=SheetDefCur;
			FormS.SheetFieldDefCur=SheetFieldDef.NewCheckBox("",0,0,11,11);
			if(this.IsInternal){
				FormS.IsReadOnly=true;
			}
			FormS.ShowDialog();
			if(FormS.DialogResult!=DialogResult.OK){
				return;
			}
			SheetDefCur.SheetFieldDefs.Add(FormS.SheetFieldDefCur);
			FillFieldList();
			panelMain.Refresh();
		}

		private void butAddSigBox_Click(object sender,EventArgs e) {
			FormSheetFieldSigBox FormS=new FormSheetFieldSigBox();
			FormS.SheetDefCur=SheetDefCur;
			FormS.SheetFieldDefCur=SheetFieldDef.NewSigBox(0,0,364,81);
			if(this.IsInternal){
				FormS.IsReadOnly=true;
			}
			FormS.ShowDialog();
			if(FormS.DialogResult!=DialogResult.OK){
				return;
			}
			SheetDefCur.SheetFieldDefs.Add(FormS.SheetFieldDefCur);
			FillFieldList();
			panelMain.Refresh();
		}

		private void butAddPatImage_Click(object sender,EventArgs e) {
			if(!PrefC.UsingAtoZfolder) {
				MsgBox.Show(this,"Not allowed because not using AtoZ folder");
				return;
			}
			//Font font=new Font(SheetDefCur.FontName,SheetDefCur.FontSize);
			FormSheetFieldImage FormS=new FormSheetFieldImage();
			FormS.SheetDefCur=SheetDefCur;
			FormS.SheetFieldDefCur=SheetFieldDef.NewImage("",0,0,100,100);
			if(this.IsInternal) {
				FormS.IsReadOnly=true;
			}
			FormS.ShowDialog();
			if(FormS.DialogResult!=DialogResult.OK) {
				return;
			}
			SheetDefCur.SheetFieldDefs.Insert(0,FormS.SheetFieldDefCur);
			FillFieldList();
			panelMain.Refresh();
		}

		private void listFields_Click(object sender,EventArgs e) {
			//if(listFields.SelectedIndices.Count==0){
			//	return;
			//}
			panelMain.Refresh();
		}

		private void listFields_MouseDoubleClick(object sender,MouseEventArgs e) {
			int idx=listFields.IndexFromPoint(e.Location);
			if(idx==-1){
				return;
			}
			listFields.SelectedIndices.Clear();
			listFields.SetSelected(idx,true);
			panelMain.Refresh();
			SheetFieldDef field=SheetDefCur.SheetFieldDefs[idx];
			SheetFieldDef fieldold=field.Copy();
			LaunchEditWindow(field);
			if(field.TabOrder!=fieldold.TabOrder) {//otherwise a different control will be selected.
				listFields.SelectedIndices.Clear();
			}
		}

		///<summary>Only for editing fields that already exist.</summary>
		private void LaunchEditWindow(SheetFieldDef field){
			bool refreshBuffer=false;
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
				case SheetFieldType.Image:
					FormSheetFieldImage FormSI=new FormSheetFieldImage();
					FormSI.SheetDefCur=SheetDefCur;
					FormSI.SheetFieldDefCur=field;
					if(this.IsInternal){
						FormSI.IsReadOnly=true;
					}
					FormSI.ShowDialog();
					if(FormSI.DialogResult!=DialogResult.OK){
						return;
					}
					if(FormSI.SheetFieldDefCur==null) {
						SheetDefCur.SheetFieldDefs.RemoveAt(idx);
					}
					refreshBuffer=true;
					break;
				case SheetFieldType.Line:
					FormSheetFieldLine FormSL=new FormSheetFieldLine();
					FormSL.SheetDefCur=SheetDefCur;
					FormSL.SheetFieldDefCur=field;
					if(this.IsInternal){
						FormSL.IsReadOnly=true;
					}
					FormSL.ShowDialog();
					if(FormSL.DialogResult!=DialogResult.OK){
						return;
					}
					if(FormSL.SheetFieldDefCur==null){
						SheetDefCur.SheetFieldDefs.RemoveAt(idx);
					}
					break;
				case SheetFieldType.Rectangle:
					FormSheetFieldRect FormSR=new FormSheetFieldRect();
					FormSR.SheetDefCur=SheetDefCur;
					FormSR.SheetFieldDefCur=field;
					if(this.IsInternal){
						FormSR.IsReadOnly=true;
					}
					FormSR.ShowDialog();
					if(FormSR.DialogResult!=DialogResult.OK){
						return;
					}
					if(FormSR.SheetFieldDefCur==null){
						SheetDefCur.SheetFieldDefs.RemoveAt(idx);
					}
					break;
				case SheetFieldType.CheckBox:
					FormSheetFieldCheckBox FormSB=new FormSheetFieldCheckBox();
					FormSB.SheetDefCur=SheetDefCur;
					FormSB.SheetFieldDefCur=field;
					if(this.IsInternal){
						FormSB.IsReadOnly=true;
					}
					FormSB.ShowDialog();
					if(FormSB.DialogResult!=DialogResult.OK){
						return;
					}
					if(FormSB.SheetFieldDefCur==null){
						SheetDefCur.SheetFieldDefs.RemoveAt(idx);
					}
					break;
				case SheetFieldType.SigBox:
					FormSheetFieldSigBox FormSBx=new FormSheetFieldSigBox();
					FormSBx.SheetDefCur=SheetDefCur;
					FormSBx.SheetFieldDefCur=field;
					if(this.IsInternal){
						FormSBx.IsReadOnly=true;
					}
					FormSBx.ShowDialog();
					if(FormSBx.DialogResult!=DialogResult.OK){
						return;
					}
					if(FormSBx.SheetFieldDefCur==null){
						SheetDefCur.SheetFieldDefs.RemoveAt(idx);
					}
					break;
			}
			if(IsTabMode) {
				if(ListSheetFieldDefsTabOrder.Contains(field)) {
					ListSheetFieldDefsTabOrder.RemoveAt(ListSheetFieldDefsTabOrder.IndexOf(field));
				}
				if(field.TabOrder>0 && field.TabOrder<=(ListSheetFieldDefsTabOrder.Count+1)) {
					ListSheetFieldDefsTabOrder.Insert(field.TabOrder-1,field);
				}
				RenumberTabOrderHelper();
				return;
			}
			FillFieldList();
			if(refreshBuffer) {//Only when image was edited.
				RefreshDoubleBuffer();
			}
			if(listFields.Items.Count-1>=idx){
				listFields.SelectedIndex=idx;//reselect the item.
			}
			panelMain.Refresh();
		}

		private void panelMain_MouseDown(object sender,MouseEventArgs e) {
			panel1.Select();
			if(AltIsDown) {
				PasteControlsFromMemory(e.Location);
				return;
			}
			MouseIsDown=true;
			ClickedOnBlankSpace=false;
			MouseOriginalPos=e.Location;
			MouseCurrentPos=e.Location;
			SheetFieldDef field=HitTest(e.X,e.Y);
			if(IsTabMode) {
				MouseIsDown=false;
				CtrlIsDown=false;
				AltIsDown=false;
				if(field==null
					//Some of the fields below are redundant and should never be returned from HitTest but are here to explicity exclude them.
					|| field.FieldType==SheetFieldType.Drawing
					|| field.FieldType==SheetFieldType.Image
					|| field.FieldType==SheetFieldType.Line
					|| field.FieldType==SheetFieldType.OutputText
					|| field.FieldType==SheetFieldType.Parameter
					|| field.FieldType==SheetFieldType.PatImage
					|| field.FieldType==SheetFieldType.Rectangle
					|| field.FieldType==SheetFieldType.StaticText
					) {
					return;
				}
				if(ListSheetFieldDefsTabOrder.Contains(field)) {
					field.TabOrder=0;
					ListSheetFieldDefsTabOrder.RemoveAt(ListSheetFieldDefsTabOrder.IndexOf(field));
				}
				else {
					ListSheetFieldDefsTabOrder.Add(field);
				}
				RenumberTabOrderHelper();
				return;
			}
			if(field==null){
				if(CtrlIsDown){
					return;//so that you can add more to the previous selection
				}
				ClickedOnBlankSpace=true;
				listFields.SelectedIndices.Clear();//clear the existing selection
				panelMain.Refresh();
				return;
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
			else{//Ctrl not down
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
			panelMain.Refresh();
		}

		private void panelMain_MouseMove(object sender,MouseEventArgs e) {
			if(!MouseIsDown){
				return;
			}
			if(IsInternal) {
				return;
			}
			if(IsTabMode) {
				return;
			}
			if(ClickedOnBlankSpace) {
				MouseCurrentPos=e.Location;
				panelMain.Refresh();
				return;
			}
			for(int i=0;i<listFields.SelectedIndices.Count;i++){
				SheetDefCur.SheetFieldDefs[listFields.SelectedIndices[i]].XPos=OriginalControlPositions[i].X+e.X-MouseOriginalPos.X;
				SheetDefCur.SheetFieldDefs[listFields.SelectedIndices[i]].YPos=OriginalControlPositions[i].Y+e.Y-MouseOriginalPos.Y;
			}
			panelMain.Refresh();
		}

		private void panelMain_MouseUp(object sender,MouseEventArgs e) {
			MouseIsDown=false;
			OriginalControlPositions=null;
			if(ClickedOnBlankSpace) {//if initial mouse down was not on a control.  ie, if we are dragging to select.
				Rectangle selectionBounds=new Rectangle(
					Math.Min(MouseOriginalPos.X,MouseCurrentPos.X),//X
					Math.Min(MouseOriginalPos.Y,MouseCurrentPos.Y),//Y
					Math.Abs(MouseCurrentPos.X-MouseOriginalPos.X),//Width
					Math.Abs(MouseCurrentPos.Y-MouseOriginalPos.Y));//Height
				for(int i=0;i<SheetDefCur.SheetFieldDefs.Count;i++) {
					SheetFieldDef tempDef = SheetDefCur.SheetFieldDefs[i];//to speed this process up instead of referencing the array every time.
					if(tempDef.FieldType==SheetFieldType.Line || tempDef.FieldType==SheetFieldType.Image) {
						continue;//lines and images are currently not selectable by drag and drop. will require lots of calculations, completely possible, but complex.
					}
					//If the selection is contained within the "hollow" portion of the rectangle, it shouldn't be selected.
					if(tempDef.FieldType==SheetFieldType.Rectangle) {
						Rectangle tempDefBounds=new Rectangle(tempDef.Bounds.X+4,tempDef.Bounds.Y+4,tempDef.Bounds.Width-8,tempDef.Bounds.Height-8);
						if(tempDefBounds.Contains(selectionBounds)) {
							continue;
						}
					}
					if(tempDef.BoundsF.IntersectsWith(selectionBounds)){
						listFields.SetSelected(i,true);//Add to selected indicies
					}
				}
			}
			ClickedOnBlankSpace=false;
			panelMain.Refresh();
		}

		private void panelMain_MouseDoubleClick(object sender,MouseEventArgs e) {
			if(AltIsDown) {
				return;
			}
			SheetFieldDef field=HitTest(e.X,e.Y);
			if(field==null){
				return;
			}
			SheetFieldDef fieldold=field.Copy();
			LaunchEditWindow(field);
			//if(field.TabOrder!=fieldold.TabOrder) {
			//  listFields.SelectedIndices.Clear();
			//}
			//if(isTabMode) {
			//  if(ListSheetFieldDefsTabOrder.Contains(field)){
			//    ListSheetFieldDefsTabOrder.RemoveAt(ListSheetFieldDefsTabOrder.IndexOf(field));
			//  }
			//  if(field.TabOrder>0 && field.TabOrder<ListSheetFieldDefsTabOrder.Count+1) {
			//    ListSheetFieldDefsTabOrder.Insert(field.TabOrder-1,field);
			//  }
			//  RenumberTabOrderHelper();
			//}
		}

		private void panelMain_Resize(object sender,EventArgs e) {
			if(BmBackground!=null && panelMain.Size==BmBackground.Size) {
				return;
			}
			if(GraphicsBackground!=null) {
				GraphicsBackground.Dispose();
			}
			if(BmBackground!=null) {
				BmBackground.Dispose();
			}
			BmBackground=new Bitmap(panelMain.Width,panelMain.Height);
			GraphicsBackground=Graphics.FromImage(BmBackground);
			panelMain.Refresh();
		}

		///<summary>Used To renumber TabOrder on controls</summary>
		private void RenumberTabOrderHelper() {
			for(int i=0;i<ListSheetFieldDefsTabOrder.Count;i++) {
				ListSheetFieldDefsTabOrder[i].TabOrder=i+1;//Start number tab order at 1
			}
			FillFieldList();
			panelMain.Refresh();
		}

		///<summary>Images will be ignored in the hit test since they frequently fill the entire background.  Lines will be ignored too, since a diagonal line could fill a large area.</summary>
		private SheetFieldDef HitTest(int x,int y){
			for(int i=0;i<SheetDefCur.SheetFieldDefs.Count;i++){
				if(SheetDefCur.SheetFieldDefs[i].FieldType==SheetFieldType.Image){
					continue;
				}
				if(SheetDefCur.SheetFieldDefs[i].FieldType==SheetFieldType.Line){
					continue;
				}
				Rectangle fieldDefBounds=SheetDefCur.SheetFieldDefs[i].Bounds;
				if(fieldDefBounds.Contains(x,y)){
					//Center of the rectangle will not be considered a hit.
					if(SheetDefCur.SheetFieldDefs[i].FieldType==SheetFieldType.Rectangle
						&& new Rectangle(fieldDefBounds.X+4,fieldDefBounds.Y+4,fieldDefBounds.Width-8,fieldDefBounds.Height-8).Contains(x,y)) 
					{
						continue;
					}
					return SheetDefCur.SheetFieldDefs[i];
				}
			}
			return null;
		}

		private void FormSheetDefEdit_KeyDown(object sender,KeyEventArgs e) {
			bool refreshBuffer=false;
			e.Handled=true;
			if(e.Control){
				CtrlIsDown=true;
			}
			if(CtrlIsDown && e.KeyCode==Keys.C) {//CTRL-C
				CopyControlsToMemory();
			}
			else if(CtrlIsDown && e.KeyCode==Keys.V) {//CTRL-V
				PasteControlsFromMemory(new Point(0,0));
			}
			else if(e.Alt) {
				Cursor=Cursors.Cross;//change cursor to rubber stamp cursor
				AltIsDown=true;
			}
			else if(e.KeyCode==Keys.Delete || e.KeyCode==Keys.Back) {
				if(listFields.SelectedIndices.Count==0) {
					return;
				}
				if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"Delete selected fields?")) {
					return;
				}
				for(int i=listFields.SelectedIndices.Count-1;i>=0;i--) {//iterate backwards through list
					if(SheetDefCur.SheetFieldDefs[listFields.SelectedIndices[i]].FieldType==SheetFieldType.Image) {
						refreshBuffer=true;
					}
					SheetDefCur.SheetFieldDefs.RemoveAt(listFields.SelectedIndices[i]);
				}
				FillFieldList();
			}
			for(int i=0;i<listFields.SelectedIndices.Count;i++){
				if(SheetDefCur.SheetFieldDefs[listFields.SelectedIndices[i]].FieldType==SheetFieldType.Image) {
					refreshBuffer=true;
				}
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
			if(refreshBuffer) {//Only when an image was selected.
				RefreshDoubleBuffer();
			}
			panelMain.Refresh();
		}

		private void FormSheetDefEdit_KeyUp(object sender,KeyEventArgs e) {
			if((e.KeyCode & Keys.ControlKey) == Keys.ControlKey){
				CtrlIsDown=false;
			}
			if(!e.Alt) {
				Cursor=Cursors.Default;
				AltIsDown=false;
			}
		}

		private void CopyControlsToMemory() {
			if(IsTabMode) {
				return;
			}
			if(listFields.SelectedIndices.Count==0) {
				return;
			}
			//List<SheetFieldDef> listDuplicates=new List<SheetFieldDef>();
			string strPrompt=Lan.g(this,"The following selected fields can cause conflicts if they are copied:\r\n");
			bool conflictingfield=false;
			for(int i=0;i<listFields.SelectedIndices.Count;i++) {
				SheetFieldDef fielddef=SheetDefCur.SheetFieldDefs[listFields.SelectedIndices[i]];
				switch(fielddef.FieldType) {
					case SheetFieldType.Drawing:
					case SheetFieldType.Image:
					case SheetFieldType.Line:
					case SheetFieldType.PatImage:
					//case SheetFieldType.Parameter://would not be seen on the sheet.
					case SheetFieldType.Rectangle:
					case SheetFieldType.SigBox:
					case SheetFieldType.StaticText:
						break;//it will always be ok to copy the types of fields above.
					case SheetFieldType.CheckBox:
						if(fielddef.FieldName!="misc") {//custom fields should be okay to copy
							strPrompt+=fielddef.FieldName+"."+fielddef.RadioButtonValue+"\r\n";
							conflictingfield=true;
						}
						break;
					case SheetFieldType.InputField:
					case SheetFieldType.OutputText:
						if(fielddef.FieldName!="misc") {//custom fields should be okay to copy
							strPrompt+=fielddef.FieldName+"\r\n";
							conflictingfield=true;
						}
						break;
				}
			}
			strPrompt+=Lan.g(this,"Would you like to continue anyways?");
			if(conflictingfield && MessageBox.Show(strPrompt,Lan.g(this,"Warning"),MessageBoxButtons.OKCancel)!=DialogResult.OK) {
				panel1.Select();
				CtrlIsDown=false;
				return;
			}
			ListSheetFieldDefsCopyPaste=new List<SheetFieldDef>();//empty the remembered field list
			for(int i=0;i<listFields.SelectedIndices.Count;i++) {
				ListSheetFieldDefsCopyPaste.Add(SheetDefCur.SheetFieldDefs[listFields.SelectedIndices[i]].Copy());//fill clipboard with copies of the controls. 
				//It would probably be safe to fill the clipboard with the originals. but it is safer to fill it with copies.
			}
			PasteOffset=0;
			PasteOffsetY=0;//reset PasteOffset for pasting a new set of fields.
		}

		private void PasteControlsFromMemory(Point origin) {
			if(IsTabMode) {
				return;
			}
			if(ListSheetFieldDefsCopyPaste==null || ListSheetFieldDefsCopyPaste.Count==0) {
				return;
			}
			if(origin.X==0 && origin.Y==0) {//allows for cascading pastes in the upper right hand corner.
				Rectangle r=panelMain.Bounds;//Gives relative position of panel (scroll position)
				int h=panel1.Height;//Current resized height/width of parent panel
				int w=panel1.Width;
				int maxH=0;
				int maxW=0;
				for(int i=0;i<ListSheetFieldDefsCopyPaste.Count;i++) {//calculate height/width of control to be pasted
					maxH=Math.Max(maxH,ListSheetFieldDefsCopyPaste[i].Height);
					maxW=Math.Max(maxW,ListSheetFieldDefsCopyPaste[i].Width);
				}
				origin=new Point((-1)*r.X+w/2-maxW/2-10,(-1)*r.Y+h/2-maxH/2-10);//Center: scroll position * (-1) + 1/2 size of window - 1/2 the size of the field - 10 for scroll bar
				origin.X+=PasteOffset;
				origin.Y+=PasteOffset+PasteOffsetY;
			}
			listFields.ClearSelected();
			int minX=int.MaxValue;
			int minY=int.MaxValue;
			for(int i=0;i<ListSheetFieldDefsCopyPaste.Count;i++) {//calculate offset
				minX=Math.Min(minX,ListSheetFieldDefsCopyPaste[i].XPos);
				minY=Math.Min(minY,ListSheetFieldDefsCopyPaste[i].YPos);
			} 
			for(int i=0;i<ListSheetFieldDefsCopyPaste.Count;i++) {//create new controls
				SheetFieldDef fielddef=ListSheetFieldDefsCopyPaste[i].Copy();
				fielddef.XPos=fielddef.XPos-minX+origin.X;
				fielddef.YPos=fielddef.YPos-minY+origin.Y;
				SheetDefCur.SheetFieldDefs.Add(fielddef);
			}
			if(!AltIsDown) {
				PasteOffsetY+=((PasteOffset+10)/100)*10;//this will shift the pastes down 10 pixels every 10 pastes.
				PasteOffset=(PasteOffset+10)%100;//cascades and allows for 90 consecutive pastes without overlap

			}
			FillFieldList();
			for(int i=0;i<ListSheetFieldDefsCopyPaste.Count;i++) {//reselect newly added controls
				listFields.SetSelected((listFields.Items.Count-1)-i,true);//Add to selected indicies, which will be the newest clipboard.count controls on the bottom of the list.
			}
			panelMain.Refresh();
		}

		private void butDelete_Click(object sender,EventArgs e) {
			if(IsTabMode) {
				return;
			}
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

		private bool VerifyDesign(){
			//Keep a temporary list of every medical check box so it saves time checking for duplicates.
			List<SheetFieldDef> medChkBoxList=new List<SheetFieldDef>();
			//Verify radio button groups.
			for(int i=0;i<SheetDefCur.SheetFieldDefs.Count;i++){
				SheetFieldDef field=SheetDefCur.SheetFieldDefs[i];
				if(field.FieldType==SheetFieldType.CheckBox && field.IsRequired 
					&& (field.RadioButtonGroup!="" //for misc radio groups
					|| field.RadioButtonValue!=""))//for built-in radio groups
				{
					//All radio buttons within a group must either all be marked required or all be marked not required. 
					//Not the most efficient check, but there won't usually be more than a few hundred items so the user will not ever notice. We can speed up later if needed.
					for(int j=0;j<SheetDefCur.SheetFieldDefs.Count;j++){
						SheetFieldDef field2=SheetDefCur.SheetFieldDefs[j];
						if(field2.FieldType==SheetFieldType.CheckBox && !field2.IsRequired &&
							field2.RadioButtonGroup.ToLower()==field.RadioButtonGroup.ToLower() //for misc groups
							&& field2.FieldName.ToLower()==field.FieldName.ToLower()) //for misc groups
						{
							MessageBox.Show(Lan.g(this,"Radio buttons in radio button group")+" '"+(field.RadioButtonGroup==""?field.FieldName:field.RadioButtonGroup)+"' "
								+Lan.g(this,"must all be marked required or all be marked not required."));
							return false;
						}
					}
				}
				if(field.FieldType==SheetFieldType.CheckBox
					&& (field.FieldName.StartsWith("allergy:"))
						|| field.FieldName.StartsWith("medication:")
						|| field.FieldName.StartsWith("problem:"))
				{
					//Check for duplicate medical check boxes.
					for(int j=0;j<medChkBoxList.Count;j++) {
						if(medChkBoxList[j].FieldName==field.FieldName 
						&& medChkBoxList[j].RadioButtonValue==field.RadioButtonValue) 
						{
							MessageBox.Show(Lan.g(this,"Duplicate check box found")+": '"+field.FieldName+" "+field.RadioButtonValue+"'. "
								+Lan.g(this,"Only one of each type is allowed."));
							return false;
						}
					}
					//Not a duplicate so add it to the med chk box list.
					medChkBoxList.Add(field);
				}
			}
			return true;
		}

		private void linkLabelTips_LinkClicked(object sender,LinkLabelLinkClickedEventArgs e) {
			if(IsTabMode) {
				return;
			}
			string tips="";
			tips+="The following shortcuts and hotkeys are supported:\r\n";
			tips+="\r\n";
			tips+="CTRL + C : Copy selected field(s).\r\n";
			tips+="\r\n";
			tips+="CTRL + V : Paste.\r\n";
			tips+="\r\n";
			tips+="ALT + Click : 'Rubber stamp' paste to the cursor position.\r\n";
			tips+="\r\n";
			tips+="Click + Drag : Click on a blank space and then drag to group select.\r\n";
			tips+="\r\n";
			tips+="CTRL + Click + Drag : Add a group of fields to the selection.\r\n";
			tips+="\r\n";
			tips+="Delete or Backspace : Delete selected field(s).\r\n";
			MessageBox.Show(Lan.g(this,tips));
		}

		/// <summary>When clicked it will set all selected elements' Y coordinates to the smallest Y coordinate in the group, unless two controls have the same X coordinate.</summary>
		private void butAlignTop_Click(object sender,EventArgs e) {
			if(listFields.SelectedIndices.Count<2) {
				return;
			}
			float minY=SheetDefCur.SheetFieldDefs[listFields.SelectedIndices[0]].BoundsF.Top;
			for(int i=0;i<listFields.SelectedIndices.Count;i++) {
				if(SheetDefCur.SheetFieldDefs[listFields.SelectedIndices[i]].BoundsF.Top<minY) {//current element is higher up than the current 'highest' element.
					minY=SheetDefCur.SheetFieldDefs[listFields.SelectedIndices[i]].BoundsF.Top;
				}
				for(int j=0;j<listFields.SelectedIndices.Count;j++) {
					if(i==j) {//Don't compare element to itself.
						continue;
					}
					if(SheetDefCur.SheetFieldDefs[listFields.SelectedIndices[i]].Bounds.X//compair the int bounds not the boundsF for practical use
						==SheetDefCur.SheetFieldDefs[listFields.SelectedIndices[j]].Bounds.X) //compair the int bounds not the boundsF for practical use
					{
						MsgBox.Show(this,"Cannot align controls. Two or more selected controls will overlap.");
						return;
					}
				}
			}
			for(int i=0;i<listFields.SelectedIndices.Count;i++) {//Actually move the controls now
				SheetDefCur.SheetFieldDefs[listFields.SelectedIndices[i]].YPos=(int)minY;
			}
			panelMain.Refresh();
		}

		private void butAlignLeft_Click(object sender,EventArgs e) {
			if(listFields.SelectedIndices.Count<2) {
				return;
			}
			float minX=SheetDefCur.SheetFieldDefs[listFields.SelectedIndices[0]].BoundsF.Left;
			for(int i=0;i<listFields.SelectedIndices.Count;i++) {
				if(SheetDefCur.SheetFieldDefs[listFields.SelectedIndices[i]].BoundsF.Left<minX) {//current element is higher up than the current 'highest' element.
					minX=SheetDefCur.SheetFieldDefs[listFields.SelectedIndices[i]].BoundsF.Left;
				}
				for(int j=0;j<listFields.SelectedIndices.Count;j++) {
					if(i==j) {//Don't compare element to itself.
						continue;
					}
					if(SheetDefCur.SheetFieldDefs[listFields.SelectedIndices[i]].Bounds.Y//compare the int bounds not the boundsF for practical use
						==SheetDefCur.SheetFieldDefs[listFields.SelectedIndices[j]].Bounds.Y) //compare the int bounds not the boundsF for practical use
					{
						MsgBox.Show(this,"Cannot align controls. Two or more selected controls will overlap.");
						return;
					}
				}
			}
			for(int i=0;i<listFields.SelectedIndices.Count;i++) {//Actually move the controls now
				SheetDefCur.SheetFieldDefs[listFields.SelectedIndices[i]].XPos=(int)minX;
			}
			panelMain.Refresh();
		}

		private void butPaste_Click(object sender,EventArgs e) {
			PasteControlsFromMemory(new Point(0,0));
		}

		private void butCopy_Click(object sender,EventArgs e) {
			CopyControlsToMemory();
		}

		private void butTabOrder_Click(object sender,EventArgs e) {
			IsTabMode=!IsTabMode;
			if(IsTabMode) {
				butOK.Enabled=false;
				butCancel.Enabled=false;
				butDelete.Enabled=false;
				groupAddNew.Enabled=false;
				butCopy.Enabled=false;
				butPaste.Enabled=false;
				butAlignLeft.Enabled=false;
				butAlignTop.Enabled=false;
				butEdit.Enabled=false;
				ListSheetFieldDefsTabOrder=new List<SheetFieldDef>();//clear or create the list of tab orders.
			}
			else {
				butOK.Enabled=true;
				butCancel.Enabled=true;
				butDelete.Enabled=true;
				groupAddNew.Enabled=true;
				butCopy.Enabled=true;
				butPaste.Enabled=true;
				butAlignLeft.Enabled=true;
				butAlignTop.Enabled=true;
				butEdit.Enabled=true;
			}
			panelMain.Refresh();
		}

		private void butOK_Click(object sender,EventArgs e) {
			if(!VerifyDesign()){
				return;
			}
			SheetDefs.InsertOrUpdate(SheetDefCur);
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	


		

		

		

		

	

		

		

		

		

		

		

		

		

		

		

		

		
	}
}