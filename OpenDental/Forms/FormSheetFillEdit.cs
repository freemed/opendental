using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
using CodeBase;

namespace OpenDental {
	public partial class FormSheetFillEdit:Form {
		public Sheet SheetCur;
		private bool mouseIsDown;
		///<summary>A list of points for a line currently being drawn.  Once the mouse is raised, this list gets cleared.</summary>
		private List<Point> PointList;
		private PictureBox pictDraw;
		private Image imgDraw;
		private bool drawingsAltered;
		public bool RxIsControlled;

		public FormSheetFillEdit(Sheet sheet){
			InitializeComponent();
			Lan.F(this);
			SheetCur=sheet;
			if(sheet.IsLandscape){
				Width=sheet.Height+190;
				Height=sheet.Width+65;
			}
			else{
				Width=sheet.Width+190;
				Height=sheet.Height+65;
			}
			if(Width>SystemInformation.WorkingArea.Width){
				Width=SystemInformation.WorkingArea.Width;
			}
			if(Height>SystemInformation.WorkingArea.Height){
				Height=SystemInformation.WorkingArea.Height;
			}
			PointList=new List<Point>();
		}

		private void FormSheetFillEdit_Load(object sender,EventArgs e) {
			if(SheetCur.IsLandscape){
				panelMain.Width=SheetCur.Height;
				panelMain.Height=SheetCur.Width;
			}
			else{
				panelMain.Width=SheetCur.Width;
				panelMain.Height=SheetCur.Height;
			}
			textDateTime.Text=SheetCur.DateTimeSheet.ToShortDateString()+" "+SheetCur.DateTimeSheet.ToShortTimeString();
			textNote.Text=SheetCur.InternalNote;
			LayoutFields();
		}

		///<summary>Runs as the final step of loading the form, and also immediately after fields are moved down due to growth.</summary>
		private void LayoutFields(){
			panelMain.Controls.Clear();
			RichTextBox textbox;//has to be richtextbox due to MS bug that doesn't show cursor.
			FontStyle style;
			SheetCheckBox checkbox;
			//first, draw images---------------------------------------------------------------------------------------
			//might change this to only happen once when first loading form:
			if(pictDraw!=null){
				if(panelMain.Controls.Contains(pictDraw)){
					Controls.Remove(pictDraw);
				}
				pictDraw=null;
			}
			imgDraw=null;
			foreach(SheetField field in SheetCur.SheetFields){
				if(field.FieldType!=SheetFieldType.Image){
					continue;
				}
				string filePathAndName=ODFileUtils.CombinePaths(SheetUtil.GetImagePath(),field.FieldName);
				if(!File.Exists(filePathAndName)){
					continue;
				}
				Image img=Image.FromFile(filePathAndName);
				//we will now change the resolution to 100x100 to make this screen faster and to simplify calculations.
				//I tested it, and this does not decrease the screen quality.
				Image img2=new Bitmap(img,field.Width,field.Height);
				PictureBox pictBox=new PictureBox();
				pictBox.Location=new Point(field.XPos,field.YPos);
				pictBox.Width=field.Width;
				pictBox.Height=field.Height;
				pictBox.Image=img2;
				pictBox.SizeMode=PictureBoxSizeMode.StretchImage;
				pictBox.Tag=field;
				panelMain.Controls.Add(pictBox);
				pictBox.BringToFront();
				if(pictDraw==null || pictBox.Height>pictDraw.Height){//use the tallest pictBox
					pictDraw=pictBox;
					imgDraw=(Image)img2.Clone();//we will preserve this original image and draw on the real image.
				}
			}
			if(pictDraw==null){
				pictDraw=new PictureBox();				
				if(SheetCur.IsLandscape){
					imgDraw=new Bitmap(SheetCur.Height,SheetCur.Width);
					pictDraw.Width=SheetCur.Height;
					pictDraw.Height=SheetCur.Width;
				}
				else{
					imgDraw=new Bitmap(SheetCur.Width,SheetCur.Height);
					pictDraw.Width=SheetCur.Width;
					pictDraw.Height=SheetCur.Height;
				}
				pictDraw.Location=new Point(0,0);
				pictDraw.Image=(Image)imgDraw.Clone();
				pictDraw.SizeMode=PictureBoxSizeMode.StretchImage;
				panelMain.Controls.Add(pictDraw);
				panelMain.SendToBack();
			}
			//Set mouse events for the pictDraw
			pictDraw.MouseDown+=new MouseEventHandler(pictDraw_MouseDown);
			pictDraw.MouseMove+=new MouseEventHandler(pictDraw_MouseMove);
			pictDraw.MouseUp+=new MouseEventHandler(pictDraw_MouseUp);
			//draw drawings, rectangles, and lines-----------------------------------------------------------------------
			RefreshPanel();
			//draw textboxes----------------------------------------------------------------------------------------------
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
				textbox.ScrollBars=RichTextBoxScrollBars.None;
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
				textbox.TextChanged+=new EventHandler(text_TextChanged);
				panelMain.Controls.Add(textbox);
				textbox.BringToFront();
			}
			//draw checkboxes----------------------------------------------------------------------------------------------
			foreach(SheetField field in SheetCur.SheetFields){
				if(field.FieldType!=SheetFieldType.CheckBox){
					continue;
				}
				checkbox=new SheetCheckBox();
				if(field.FieldValue=="X"){
					checkbox.IsChecked=true;
				}
				checkbox.Location=new Point(field.XPos,field.YPos);
				checkbox.Width=field.Width;
				checkbox.Height=field.Height;
				checkbox.Tag=field;
				checkbox.Click+=new EventHandler(text_TextChanged);
				panelMain.Controls.Add(checkbox);
				checkbox.BringToFront();
			}
			//draw signature boxes----------------------------------------------------------------------------------------------
			foreach(SheetField field in SheetCur.SheetFields){
				if(field.FieldType!=SheetFieldType.SigBox){
					continue;
				}
				OpenDental.UI.SignatureBoxWrapper sigBox=new OpenDental.UI.SignatureBoxWrapper();
				sigBox.Location=new Point(field.XPos,field.YPos);
				sigBox.Width=field.Width;
				sigBox.Height=field.Height;
				if(field.FieldValue.Length>0){//a signature is present
					bool sigIsTopaz=false;
					if(field.FieldValue[0]=='1'){
						sigIsTopaz=true;
					}
					string signature="";
					if(field.FieldValue.Length>1){
						signature=field.FieldValue.Substring(1);
					}
					string keyData=Sheets.GetSignatureKey(SheetCur);
					sigBox.FillSignature(sigIsTopaz,keyData,signature);
				}
				sigBox.Tag=field;
				panelMain.Controls.Add(sigBox);
				sigBox.BringToFront();
			}
		}

		///<summary>Triggered when any field value changes.  This immediately invalidates signatures.  It also causes fields to grow as needed.</summary>
		private void text_TextChanged(object sender,EventArgs e) {
			foreach(Control control in panelMain.Controls){
				if(control.GetType()!=typeof(OpenDental.UI.SignatureBoxWrapper)){
					continue;
				}
				if(control.Tag==null){
					continue;
				}
				SheetField field=(SheetField)control.Tag;
				OpenDental.UI.SignatureBoxWrapper sigBox=(OpenDental.UI.SignatureBoxWrapper)control;
				sigBox.SetInvalid();
			}
			if(sender.GetType() != typeof(RichTextBox)){
				//since CheckBoxes also trigger this event for sig invalid.
				return;
			}
			//everything below here is for growth calc.
			RichTextBox textBox=(RichTextBox)sender;
			//remember where we were
			int cursorPos=textBox.SelectionStart;
			//int boxX=textBox.Location.X;
			//int boxY=textBox.Location.Y;
			//string originalFieldValue=((SheetField)((RichTextBox)control).Tag).FieldValue;
			SheetField fld=(SheetField)textBox.Tag;
			if(fld.GrowthBehavior==GrowthBehaviorEnum.None){
				return;
			}
			fld.FieldValue=textBox.Text;
			Graphics g=this.CreateGraphics();
			FontStyle fontstyle=FontStyle.Regular;
			if(fld.FontIsBold){
				fontstyle=FontStyle.Bold;
			}
			Font font=new Font(fld.FontName,fld.FontSize,fontstyle);
			int calcH=GraphicsHelper.MeasureStringH(g,fld.FieldValue,font,fld.Width);
				//(int)(g.MeasureString(fld.FieldValue,font,fld.Width).Height * 1.133f);//Seems to need 2 pixels per line of text to prevent hidden text due to scroll.
			calcH+=font.Height+2;//add one line just in case.
			g.Dispose();
			if(calcH<=fld.Height){//no growth needed
				return;
			}
			//the field height needs to change, so:
			int amountOfGrowth=calcH-fld.Height;
			fld.Height=calcH;
			//FillFieldsFromControls();//We already changed the value of this field manually, and the other field values don't matter.
			if(fld.GrowthBehavior==GrowthBehaviorEnum.DownGlobal) {
				SheetUtil.MoveAllDownBelowThis(SheetCur,fld,amountOfGrowth);
			}
			else if(fld.GrowthBehavior==GrowthBehaviorEnum.DownLocal) {
				SheetUtil.MoveAllDownWhichIntersect(SheetCur,fld);
			}
			LayoutFields();
			//find the original textbox, and put the cursor back where it belongs
			foreach(Control control in panelMain.Controls){
				if(control.GetType() == typeof(RichTextBox)){
					if((SheetField)(control.Tag)==fld){
						((RichTextBox)control).Select(cursorPos,0);
						((RichTextBox)control).Focus();
						//((RichTextBox)control).SelectionStart=cursorPos;
					}
				}
			}
		}

		/*private void panelDraw_Paint(object sender,PaintEventArgs e) {
			//e.Graphics.DrawLine(Pens.Black,0,0,300,300);
			for(int i=1;i<PointList.Count;i++){
				e.Graphics.DrawLine(Pens.Black,PointList[i-1].X,PointList[i-1].Y,PointList[i].X,PointList[i].Y);
			}
		}*/

		private void pictDraw_MouseDown(object sender,MouseEventArgs e) {
			mouseIsDown=true;
			if(checkErase.Checked){
				return;
			}
			PointList.Add(new Point(e.X,e.Y));
			drawingsAltered=true;
		}

		private void pictDraw_MouseEnter(object sender,EventArgs e) {

		}

		private void pictDraw_MouseLeave(object sender,EventArgs e) {

		}

		private void pictDraw_MouseMove(object sender,MouseEventArgs e) {
			if(!mouseIsDown){
				return;
			}
			if(checkErase.Checked){
				//look for any lines that intersect the "eraser".
				//since the line segments are so short, it's sufficient to check end points.
				//Point point;
				string[] xy;
				string[] pointStr;
				float x;
				float y;
				float dist;//the distance between the point being tested and the center of the eraser circle.
				float radius=8f;//by trial and error to achieve best feel.
				PointF eraserPt=new PointF(e.X+8.49f,e.Y+8.49f);
				foreach(SheetField field in SheetCur.SheetFields){
					if(field.FieldType!=SheetFieldType.Drawing){
						continue;
					}
					pointStr=field.FieldValue.Split(';');
					for(int p=0;p<pointStr.Length;p++){
						xy=pointStr[p].Split(',');
						if(xy.Length==2){
							x=PIn.PFloat(xy[0]);
							y=PIn.PFloat(xy[1]);
							dist=(float)Math.Sqrt(Math.Pow(Math.Abs(x-eraserPt.X),2)+Math.Pow(Math.Abs(y-eraserPt.Y),2));
							if(dist<=radius){//testing circle intersection here
								SheetCur.SheetFields.Remove(field);
								drawingsAltered=true;
								RefreshPanel();
								return;;
							}
						}
					}
				}	
				return;
			}
			PointList.Add(new Point(e.X,e.Y));
			//RefreshPanel();
			//just add the last line segment instead of redrawing the whole thing.
			Graphics g=Graphics.FromImage(pictDraw.Image);
			g.SmoothingMode=SmoothingMode.HighQuality;
			Pen pen=new Pen(Brushes.Black,2f);
			int i=PointList.Count-1;
			g.DrawLine(pen,PointList[i-1].X,PointList[i-1].Y,PointList[i].X,PointList[i].Y);
			pictDraw.Invalidate();
			g.Dispose();
		}

		private void pictDraw_MouseUp(object sender,MouseEventArgs e) {
			mouseIsDown=false;
			if(checkErase.Checked){
				return;
			}
			SheetField field=new SheetField();
			field.FieldType=SheetFieldType.Drawing;
			field.FieldName="";
			field.FieldValue="";
			for(int i=0;i<PointList.Count;i++){
				if(i>0){
					field.FieldValue+=";";
				}
				field.FieldValue+=(PointList[i].X+pictDraw.Left)+","+(PointList[i].Y+pictDraw.Top);
			}
			field.FontName="";
			SheetCur.SheetFields.Add(field);
			PointList.Clear();
			RefreshPanel();
		}

		private void RefreshPanel(){
			Image img=(Image)imgDraw.Clone();
			Graphics g=Graphics.FromImage(img);
			g.SmoothingMode=SmoothingMode.HighQuality;
			//g.CompositingQuality=CompositingQuality.Default;
			Pen pen=new Pen(Brushes.Black,2f);
			Pen pen2=new Pen(Brushes.Black,1f);
			string[] pointStr;
			List<Point> points;
			Point point;
			string[] xy;
			for(int f=0;f<SheetCur.SheetFields.Count;f++){
				if(SheetCur.SheetFields[f].FieldType==SheetFieldType.Drawing){
					pointStr=SheetCur.SheetFields[f].FieldValue.Split(';');
					points=new List<Point>();
					for(int p=0;p<pointStr.Length;p++){
						xy=pointStr[p].Split(',');
						if(xy.Length==2){
							point=new Point(PIn.PInt(xy[0]),PIn.PInt(xy[1]));
							points.Add(point);
						}
					}
					for(int i=1;i<points.Count;i++){
						g.DrawLine(pen,points[i-1].X-pictDraw.Left,
							points[i-1].Y-pictDraw.Top,
							points[i].X-pictDraw.Left,
							points[i].Y-pictDraw.Top);
					}
				}
				if(SheetCur.SheetFields[f].FieldType==SheetFieldType.Line){
					g.DrawLine(pen2,SheetCur.SheetFields[f].XPos-pictDraw.Left,
						SheetCur.SheetFields[f].YPos-pictDraw.Top,
						SheetCur.SheetFields[f].XPos+SheetCur.SheetFields[f].Width-pictDraw.Left,
						SheetCur.SheetFields[f].YPos+SheetCur.SheetFields[f].Height-pictDraw.Top);
				}
				if(SheetCur.SheetFields[f].FieldType==SheetFieldType.Rectangle){
					g.DrawRectangle(pen2,SheetCur.SheetFields[f].XPos-pictDraw.Left,
						SheetCur.SheetFields[f].YPos-pictDraw.Top,
						SheetCur.SheetFields[f].Width,
						SheetCur.SheetFields[f].Height);
				}
			}
			pictDraw.Image=img;
			g.Dispose();
		}

		private void checkErase_Click(object sender,EventArgs e) {
			if(checkErase.Checked){
				pictDraw.Cursor=new Cursor(GetType(),"EraseCircle.cur");
			}
			else{
				pictDraw.Cursor=Cursors.Default;
			}
		}

		private void panelColor_DoubleClick(object sender,EventArgs e) {

		}

		private void butPrint_Click(object sender,EventArgs e) {
			if(!TryToSaveData()){
				return;
			}
			SheetCur=Sheets.GetSheet(SheetCur.SheetNum);
			//whether this is a new sheet, or one pulled from the database,
			//it will have the extra parameter we are looking for.
			//A new sheet will also have a PatNum parameter which we will ignore.
			FormSheetOutputFormat FormS=new FormSheetOutputFormat();
			if(SheetCur.SheetType==SheetTypeEnum.ReferralSlip
				|| SheetCur.SheetType==SheetTypeEnum.ReferralLetter)
			{
				FormS.PaperCopies=2;
			}
			else{
				FormS.PaperCopies=1;
			}
			Patient pat=Patients.GetPat(SheetCur.PatNum);
			if(pat.Email!=""){
				FormS.EmailPatAddress=pat.Email;
				FormS.EmailPat=true;
				FormS.PaperCopies--;
			}
			Referral referral=null;
			if(SheetCur.SheetType==SheetTypeEnum.ReferralSlip
				|| SheetCur.SheetType==SheetTypeEnum.ReferralLetter)
			{
				FormS.Email2Visible=true;
				SheetParameter parameter=SheetParameter.GetParamByName(SheetCur.Parameters,"ReferralNum");
				if(parameter==null){//it can be null sometimes because of old bug in db.
					FormS.Email2Visible=false;//prevents trying to attach email to nonexistent referral.
				}
				else{
					int referralNum=PIn.PInt(parameter.ParamValue.ToString());
					referral=Referrals.GetReferral(referralNum);
					if(referral.EMail!=""){
						FormS.Email2Address=referral.EMail;
						FormS.Email2=true;
						FormS.PaperCopies--;
					}
				}
			}
			else{
				FormS.Email2Visible=false;
			}
			FormS.ShowDialog();
			if(FormS.DialogResult!=DialogResult.OK){
				return;
			}
			if(FormS.PaperCopies>0){
				SheetPrinting.Print(SheetCur,FormS.PaperCopies,RxIsControlled);
			}
			EmailMessage message;
			Random rnd=new Random();
			string attachPath=FormEmailMessageEdit.GetAttachPath();
			string fileName;
			string filePathAndName;
			//Graphics g=this.CreateGraphics();
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
			if((SheetCur.SheetType==SheetTypeEnum.ReferralSlip
				|| SheetCur.SheetType==SheetTypeEnum.ReferralLetter)
				&& FormS.Email2)
			{
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
			//g.Dispose();
			DialogResult=DialogResult.OK;
		}

		private void butPDF_Click(object sender,EventArgs e) {
			if(!TryToSaveData()){
				return;
			}
			SheetCur=Sheets.GetSheet(SheetCur.SheetNum);
			string filePathAndName=Path.ChangeExtension(Path.GetTempFileName(),".pdf");
			//Graphics g=this.CreateGraphics();
			SheetPrinting.CreatePdf(SheetCur,filePathAndName);
			//g.Dispose();
			Process.Start(filePathAndName);
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
			FillFieldsFromControls();//But SheetNums will still be 0 for a new sheet.
			bool isNew=SheetCur.IsNew;
			Sheets.WriteObject(SheetCur);
			List<SheetField> drawingList=new List<SheetField>();
			foreach(SheetField fld in SheetCur.SheetFields){
				if(fld.FieldType==SheetFieldType.SigBox){
					continue;//done in a separate step
				}
				if(fld.FieldType==SheetFieldType.Image
					|| fld.FieldType==SheetFieldType.Rectangle
					|| fld.FieldType==SheetFieldType.Line)
				{
					if(!fld.IsNew){
						continue;//it only saves them when the sheet is first created because user can't edit anyway.
					}
				}
				fld.SheetNum=SheetCur.SheetNum;//whether or not isnew
				if(fld.FieldType==SheetFieldType.Drawing){
					fld.IsNew=true;
					drawingList.Add(fld);
				}
				else{
					SheetFields.WriteObject(fld);
				}
			}
			if(drawingsAltered){
				//drawings get saved as a group rather than with the other fields.
				SheetFields.SetDrawings(drawingList,SheetCur.SheetNum);
			}
			//SigBoxes must come after ALL other types in order for the keyData to be in the right order.
			SheetField field;
			foreach(Control control in panelMain.Controls){
				if(control.GetType()!=typeof(OpenDental.UI.SignatureBoxWrapper)){
					continue;
				}
				if(control.Tag==null){
					continue;
				}
				field=(SheetField)control.Tag;
				OpenDental.UI.SignatureBoxWrapper sigBox=(OpenDental.UI.SignatureBoxWrapper)control;
				if(sigBox.GetSigChanged()){
					//refresh the fields so they are in the correct order
					SheetFields.GetFieldsAndParameters(SheetCur);
					bool sigIsTopaz=sigBox.GetSigIsTopaz();
					string keyData=Sheets.GetSignatureKey(SheetCur);
					string signature=sigBox.GetSignature(keyData);
					field.FieldValue="";
					if(signature!=""){
						if(sigIsTopaz){
							field.FieldValue+="1";
						}
						else{
							field.FieldValue+="0";
						}
						field.FieldValue+=signature;
					}
				}
				field.SheetNum=SheetCur.SheetNum;//whether or not isnew
				SheetFields.WriteObject(field);
			}
			if(isNew) {
				Sheets.SaveParameters(SheetCur);
			}
			return true;
		}

		///<summary>This is always done before the save process.  But it's also done before bumping down fields due to growth behavior.</summary>
		private void FillFieldsFromControls(){			
			//SheetField field;
			//Images------------------------------------------------------
				//Images can't be changed in this UI
			//RichTextBoxes-----------------------------------------------
			foreach(Control control in panelMain.Controls){
				if(control.GetType()!=typeof(RichTextBox)){
					continue;
				}
				if(control.Tag==null){
					continue;
				}
				//field=(SheetField)control.Tag;
				((SheetField)control.Tag).FieldValue=control.Text;
			}
			//CheckBoxes-----------------------------------------------
			foreach(Control control in panelMain.Controls){
				if(control.GetType()!=typeof(SheetCheckBox)){
					continue;
				}
				if(control.Tag==null){
					continue;
				}
				//field=(SheetField)control.Tag;
				if(((SheetCheckBox)control).IsChecked){
					((SheetField)control.Tag).FieldValue="X";
				}
				else{
					((SheetField)control.Tag).FieldValue="";
				}
			}
			//Rectangles and lines-----------------------------------------
				//Rectangles and lines can't be changed in this UI
			//Drawings----------------------------------------------------
				//Drawings data is already saved to fields
			//SigBoxes---------------------------------------------------
				//SigBoxes won't be strictly checked for validity
				//or data saved to the field until it's time to actually save to the database.
		}

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