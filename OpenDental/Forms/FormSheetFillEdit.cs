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

		public FormSheetFillEdit(Sheet sheet){
			InitializeComponent();
			Lan.F(this);
			SheetCur=sheet;
			if(sheet.IsLandscape){
				Width=sheet.Height+185;
				Height=sheet.Width+60;
			}
			else{
				Width=sheet.Width+185;
				Height=sheet.Height+60;
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
			RichTextBox textbox;//has to be richtextbox due to MS bug that doesn't show cursor.
			FontStyle style;
			//first, draw images---------------------------------------------------------------------------------------
			pictDraw=null;
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
				Point point;
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
			else{
				FormS.Email2Visible=false;
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
			SheetField field;
			//RichTextBoxes-----------------------------------------------
			foreach(Control control in panelMain.Controls){
				if(control.Tag==null){
					continue;
				}
				field=(SheetField)control.Tag;
				if(control.GetType()==typeof(RichTextBox)){
					field.FieldValue=control.Text;
				}
				field.SheetNum=SheetCur.SheetNum;//whether or not isnew
				SheetFields.WriteObject(field);
			}
			//Rectangles and lines
			foreach(SheetField fld in SheetCur.SheetFields){
				if(!fld.IsNew){
					continue;//so it only saves them when the sheet is first created because user can't edit.
				}
				if(fld.FieldType==SheetFieldType.Rectangle || fld.FieldType==SheetFieldType.Line){
					fld.SheetNum=SheetCur.SheetNum;
					SheetFields.WriteObject(fld);
				}
			}
			//Drawings----------------------------------------------------
			if(drawingsAltered){
				List<SheetField> drawingList=new List<SheetField>();
				foreach(SheetField sf in SheetCur.SheetFields){
					if(sf.FieldType==SheetFieldType.Drawing){
						sf.SheetNum=SheetCur.SheetNum;
						sf.IsNew=true;
						drawingList.Add(sf);
					}
				}
				SheetFields.SetDrawings(drawingList,SheetCur.SheetNum);
			}
			if(isNew){
				Sheets.SaveParameters(SheetCur);
			}
			return true;
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