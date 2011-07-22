using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenDentBusiness.UI {
	public class ApptSingleDrawing {
		/*
		///<summary></summary>
		public static void DrawEntireAppt(Graphics g,DataRow DataRoww,int Width,int Height,string patternShowing,int lineH,int rowsPerIncr,bool isSelected,bool thisIsPinBoard,long aptNum,List<ApptViewItem> apptViewItem,ApptView apptViewCur) {
			Pen penB=new Pen(Color.Black);
			Pen penW=new Pen(Color.White);
			Pen penGr=new Pen(Color.SlateGray);
			Pen penDG=new Pen(Color.DarkSlateGray);
			Pen penO;//provider outline color
			Color backColor;
			Color provColor;
			if(DataRoww["ProvNum"].ToString()!="0" && DataRoww["IsHygiene"].ToString()=="0") {//dentist
				provColor=Providers.GetColor(PIn.Long(DataRoww["ProvNum"].ToString()));
				penO=new Pen(Providers.GetOutlineColor(PIn.Long(DataRoww["ProvNum"].ToString())));
			}
			else if(DataRoww["ProvHyg"].ToString()!="0" && DataRoww["IsHygiene"].ToString()=="1") {//hygienist
				provColor=Providers.GetColor(PIn.Long(DataRoww["ProvHyg"].ToString()));
				penO=new Pen(Providers.GetOutlineColor(PIn.Long(DataRoww["ProvHyg"].ToString())));
			}
			else {//unknown
				provColor=Color.White;
				penO=new Pen(Color.Black);
			}
			if(PIn.Long(DataRoww["AptStatus"].ToString())==(int)ApptStatus.Complete) {
				backColor=DefC.Long[(int)DefCat.AppointmentColors][3].ItemColor;
			}
			else if(PIn.Long(DataRoww["AptStatus"].ToString())==(int)ApptStatus.PtNote) {
				backColor=DefC.Long[(int)DefCat.AppointmentColors][7].ItemColor;
			}
			else if(PIn.Long(DataRoww["AptStatus"].ToString()) == (int)ApptStatus.PtNoteCompleted) {
				backColor = DefC.Long[(int)DefCat.AppointmentColors][10].ItemColor;
			}
			else {
				backColor=provColor;
				//We might want to do something interesting here.
			}
			SolidBrush backBrush=new SolidBrush(backColor);
			g.FillRectangle(backBrush,7,0,Width-7,Height);
			g.FillRectangle(Brushes.White,0,0,7,Height);
			g.DrawLine(penB,7,0,7,Height);
			Pen penTimediv=Pens.Silver;
			//g.TextRenderingHint=TextRenderingHint.SingleBitPerPixelGridFit;//to make printing clearer
			for(int i=0;i<patternShowing.Length;i++) {//Info.MyApt.Pattern.Length;i++){
				if(patternShowing.Substring(i,1)=="X") {
					g.FillRectangle(new SolidBrush(provColor),1,i*lineH+1,6,lineH);
				}
				else {
					//leave empty
				}
				if(Math.IEEERemainder((double)i,(double)rowsPerIncr)==0) {//0/1
					g.DrawLine(penTimediv,1,i*lineH,6,i*lineH);
				}
			}
			//Highlighting border
			if(isSelected	|| (!thisIsPinBoard && DataRoww["AptNum"].ToString()==aptNum.ToString())) {
				//Left
				g.DrawLine(penO,8,1,8,Height-2);
				g.DrawLine(penO,9,1,9,Height-3);
				//Right
				g.DrawLine(penO,Width-2,1,Width-2,Height-2);
				g.DrawLine(penO,Width-3,2,Width-3,Height-3);
				//Top
				g.DrawLine(penO,8,1,Width-2,1);
				g.DrawLine(penO,8,2,Width-3,2);
				//bottom
				g.DrawLine(penO,9,Height-2,Width-2,Height-2);
				g.DrawLine(penO,10,Height-3,Width-3,Height-3);
			}
			//Draw all the main rows
			Point drawLoc=new Point(9,0);
			int elementI=0;
			while(drawLoc.Y<Height && elementI<apptViewItem.Count) {
				if(apptViewItem[elementI].ElementAlignment!=ApptViewAlignment.Main) {
					elementI++;
					continue;
				}
				drawLoc=DrawElement(g,elementI,drawLoc,ApptViewStackBehavior.Vertical,ApptViewAlignment.Main,backBrush);//set the drawLoc to a new point, based on space used by element
				elementI++;
			}
			//UR
			drawLoc=new Point(Width-1,0);//in the UR area, we refer to the upper right corner of each element.
			elementI=0;
			while(drawLoc.Y<Height && elementI<apptViewItem.Count) {
				if(apptViewItem[elementI].ElementAlignment!=ApptViewAlignment.UR) {
					elementI++;
					continue;
				}
				drawLoc=DrawElement(g,elementI,drawLoc,apptViewCur.StackBehavUR,ApptViewAlignment.UR,backBrush);
				elementI++;
			}
			//LR
			drawLoc=new Point(Width-1,Height-1);//in the LR area, we refer to the lower right corner of each element.
			elementI=apptViewItem.Count-1;//For lower right, draw the list backwards.
			while(drawLoc.Y>0 && elementI>=0) {
				if(apptViewItem[elementI].ElementAlignment!=ApptViewAlignment.LR) {
					elementI--;
					continue;
				}
				drawLoc=DrawElement(g,elementI,drawLoc,apptViewCur.StackBehavLR,ApptViewAlignment.LR,backBrush);
				elementI--;
			}
			//Main outline
			g.DrawRectangle(new Pen(Color.Black),0,0,Width-1,Height-1);
			//broken X
			if(DataRoww["AptStatus"].ToString()==((int)ApptStatus.Broken).ToString()) {
				g.DrawLine(new Pen(Color.Black),8,1,Width-1,Height-1);
				g.DrawLine(new Pen(Color.Black),8,Height-1,Width-1,1);
			}
		}
		
		///<summary></summary>
		public static Point DrawElement(Graphics g,int elementI,Point drawLoc,ApptViewStackBehavior stackBehavior,ApptViewAlignment align,Brush backBrush,DataRow DataRoww) {
			string text="";
			bool isNote=false;
			if(PIn.Long(DataRoww["AptStatus"].ToString()) == (int)ApptStatus.PtNote
				|| PIn.Long(DataRoww["AptStatus"].ToString()) == (int)ApptStatus.PtNoteCompleted) 
			{
				isNote=true;
			}
			bool isGraphic=false;
			if(ApptViewItemL.ApptRows[elementI].ElementDesc=="ConfirmedColor") {
				isGraphic=true;
			}
			if(ApptViewItemL.ApptRows[elementI].ApptFieldDefNum>0) {
				string fieldName=ApptFieldDefs.GetFieldName(ApptViewItemL.ApptRows[elementI].ApptFieldDefNum);
				for(int i=0;i<TableApptFields.Rows.Count;i++) {
					if(TableApptFields.Rows[i]["AptNum"].ToString()!=DataRoww["AptNum"].ToString()) {
						continue;
					}
					if(TableApptFields.Rows[i]["FieldName"].ToString()!=fieldName) {
						continue;
					}
					text=TableApptFields.Rows[i]["FieldValue"].ToString();
				}
			}
			else if(ApptViewItemL.ApptRows[elementI].PatFieldDefNum>0) {
				string fieldName=PatFieldDefs.GetFieldName(ApptViewItemL.ApptRows[elementI].PatFieldDefNum);
				for(int i=0;i<TablePatFields.Rows.Count;i++) {
					if(TablePatFields.Rows[i]["PatNum"].ToString()!=DataRoww["PatNum"].ToString()) {
						continue;
					}
					if(TablePatFields.Rows[i]["FieldName"].ToString()!=fieldName) {
						continue;
					}
					text=TablePatFields.Rows[i]["FieldValue"].ToString();
				}
			}
			else switch(ApptViewItemL.ApptRows[elementI].ElementDesc){
				case "Address":
					if(isNote) {
						text="";
					}
					else {
						text=DataRoww["address"].ToString();
					}
					break;
				case "AddrNote":
					if(isNote) {
						text="";
					}
					else {
						text=DataRoww["addrNote"].ToString();
					}
					break;
				case "Age":
					if(isNote) {
						text="";
					}
					else {
						text=DataRoww["age"].ToString();
					}
					break;
				case "ASAP":
					if(isNote) {
						text="";
					}
					else {
						if(DataRoww["AptStatus"].ToString()==((int)ApptStatus.ASAP).ToString()){
							text=Lan.g("enumApptStatus","ASAP");
						}
					}
					break;
				case "ASAP[A]":
					if(DataRoww["AptStatus"].ToString()==((int)ApptStatus.ASAP).ToString()) {
						text="A";
					}
					break;
				case "AssistantAbbr":
					if(isNote) {
						text="";
					}
					else {
						text=DataRoww["assistantAbbr"].ToString();
					}
					break;
				case "ChartNumAndName":
					text=DataRoww["chartNumAndName"].ToString();
					break;
				case "ChartNumber":
					text=DataRoww["chartNumber"].ToString();
					break;
				case "CreditType":
					text=DataRoww["CreditType"].ToString();
					break;
				case "Guardians":
					if(isNote) {
						text="";
					}
					else {
						text=DataRoww["guardians"].ToString();
					}
					break;
				case "HasIns[I]":
					text=DataRoww["hasIns[I]"].ToString();
					break;
				case "HmPhone":
					if(isNote) {
						text="";
					}
					else {
						text=DataRoww["hmPhone"].ToString();
					}
					break;
				case "InsToSend[!]":
					text=DataRoww["insToSend[!]"].ToString();
					break;
				case "Lab":
					if(isNote) {
						text="";
					}
					else {
						text=DataRoww["lab"].ToString();
					}
					break;
				case "MedOrPremed[+]":
					if(isNote) {
						text="";
					}
					else {
						text=DataRoww["medOrPremed[+]"].ToString();
					}
					break;
				case "MedUrgNote":
					if(isNote) {
						text="";
					}
					else {
						text=DataRoww["MedUrgNote"].ToString();
					}
					break;
				case "Note":
					text=DataRoww["Note"].ToString();
					break;
				case "PatientName":
					text=DataRoww["patientName"].ToString();
					break;
				case "PatientNameF":
					text=DataRoww["patientNameF"].ToString();
					break;
				case "PatNum":
					text=DataRoww["patNum"].ToString();
					break;
				case "PatNumAndName":
					text=DataRoww["patNumAndName"].ToString();
					break;
				case "PremedFlag":
					if(isNote) {
						text="";
					}
					else {
						text=DataRoww["preMedFlag"].ToString();
					}
					break;
				case "Procs":
					if(isNote) {
						text="";
					}
					else {
						text=DataRoww["procs"].ToString();
					}
					break;
				case "ProcsColored":
					string value=DataRoww["procsColored"].ToString();
					string[] lines=value.Split(new string[] { "</span>" },StringSplitOptions.RemoveEmptyEntries);
					Point tempPt=new Point();
					tempPt=drawLoc;
					int lastH=0;
					int count=1;
					for(int i=0;i<lines.Length;i++) {
						Match m=Regex.Match(lines[i],"^<span color=\"(-?[0-9]*)\">(.*)$");
						string rgbInt=m.Result("$1");
						string proc=m.Result("$2");
						if(lines[i]!=lines[lines.Length-1]) {
							proc+=",";
						}
						if(rgbInt==""){
							rgbInt=ApptViewItemL.ApptRows[elementI].ElementColorXml.ToString();
						}
						Color c=Color.FromArgb(Convert.ToInt32(rgbInt));
						StringFormat procFormat=new StringFormat();
						RectangleF procRect=new RectangleF(0,0,1000,1000);
						CharacterRange[] ranges= { new CharacterRange(0,proc.Length) };
						Region[] regions=new Region[1];
						procFormat.SetMeasurableCharacterRanges(ranges);
						regions=g.MeasureCharacterRanges(proc,baseFont,procRect,procFormat);
						if(regions.Length==0) {
							procRect=new RectangleF(0,0,0,0);
						}
						else {
							procRect=regions[0].GetBounds(g);
						}
						if(tempPt.X+procRect.Width>Shadow.Width) {
							tempPt.X=drawLoc.X;
							tempPt.Y+=lastH;
							count++;
						}
						SolidBrush sb=new SolidBrush(c);
						g.DrawString(proc,baseFont,sb,tempPt);
						sb.Dispose();
						tempPt.X+=(int)procRect.Width+3;//+3 is room for spaces
						if((int)procRect.Height>lastH){
							lastH=(int)procRect.Height;
						}
					}
					drawLoc.Y+=lastH*count;
					return drawLoc;
				case "Production":
					if(isNote) {
						text="";
					}
					else {
						text=DataRoww["production"].ToString();
					}
					break;
				case "Provider":
					if(isNote) {
						text="";
					}
					else {
						text=DataRoww["provider"].ToString();
					}
					break;
				case "TimeAskedToArrive":
					if(isNote){
						text="";
					}
					else {
						text=DataRoww["timeAskedToArrive"].ToString();//could be blank
					}
					break;
				case "WirelessPhone":
					if(isNote) {
						text="";
					}
					else {
						text=DataRoww["wirelessPhone"].ToString();
					}
					break;
				case "WkPhone":
					if(isNote) {
						text="";
					}
					else {
						text=DataRoww["wkPhone"].ToString();
					}
					break;
				
			}
			if(text=="" && !isGraphic) {
				return drawLoc;//next element will draw at the same position as this one would have.
			}
			SolidBrush brush=new SolidBrush(ApptViewItemL.ApptRows[elementI].ElementColor);
			SolidBrush brushWhite=new SolidBrush(Color.White);
			SolidBrush noteTitlebrush = new SolidBrush(DefC.Long[(int)DefCat.AppointmentColors][8].ItemColor);
			StringFormat format=new StringFormat();
			format.Alignment=StringAlignment.Near;
			int charactersFitted;//not used, but required as 'out' param for measureString.
			int linesFilled;
			SizeF noteSize;
			RectangleF rect;
			RectangleF rectBack;
			if(align==ApptViewAlignment.Main) {//always stacks vertical
				if(isGraphic) {
					Bitmap bitmap=new Bitmap(12,12);
					noteSize=new SizeF(bitmap.Width,bitmap.Height);
					rect=new RectangleF(drawLoc,noteSize);
					using(Graphics gfx=Graphics.FromImage(bitmap)){
						gfx.SmoothingMode=SmoothingMode.HighQuality;
						Color confirmColor=DefC.GetColor(DefCat.ApptConfirmed,PIn.Long(DataRoww["Confirmed"].ToString()));
						SolidBrush confirmBrush=new SolidBrush(confirmColor);
						gfx.FillEllipse(confirmBrush,0,0,11,11);
						gfx.DrawEllipse(Pens.Black,0,0,11,11);
						confirmBrush.Dispose();
					}
					g.DrawImage(bitmap,drawLoc.X,drawLoc.Y);
					return new Point(drawLoc.X,drawLoc.Y+(int)noteSize.Height);
				}
				else {
					noteSize=g.MeasureString(text,baseFont,Width-9);
					//Problem: "limited-tooth bothering him ", the trailing space causes measuring error, resulting in m getting partially chopped off.
					//Tried TextRenderer, but it caused premature wrapping
					//Size noteSizeInt=TextRenderer.MeasureText(text,baseFont,new Size(Width-9,1000));
					//noteSize=new SizeF(noteSizeInt.Width,noteSizeInt.Height);
					noteSize.Width=(float)Math.Ceiling((double)noteSize.Width);//round up to nearest int solves specific problem discussed above.
					g.MeasureString(text,baseFont,noteSize,format,out charactersFitted,out linesFilled);
					rect=new RectangleF(drawLoc,noteSize);
					g.DrawString(text,baseFont,brush,rect,format);
					return new Point(drawLoc.X,drawLoc.Y+linesFilled*ContrApptSheet.Lh);
				}
			}
			else if(align==ApptViewAlignment.UR) {
				if(stackBehavior==ApptViewStackBehavior.Vertical) {
					int w=Width-9;
					if(isGraphic) {
						Bitmap bitmap=new Bitmap(12,12);
						noteSize=new SizeF(bitmap.Width,bitmap.Height);
						Point drawLocThis=new Point(drawLoc.X-(int)noteSize.Width,drawLoc.Y+1);//upper left corner of this element
						rect=new RectangleF(drawLoc,noteSize);
						using(Graphics gfx=Graphics.FromImage(bitmap)) {
							gfx.SmoothingMode=SmoothingMode.HighQuality;
							Color confirmColor=DefC.GetColor(DefCat.ApptConfirmed,PIn.Long(DataRoww["Confirmed"].ToString()));
							SolidBrush confirmBrush=new SolidBrush(confirmColor);
							gfx.FillEllipse(confirmBrush,0,0,11,11);
							gfx.DrawEllipse(Pens.Black,0,0,11,11);
							confirmBrush.Dispose();
						}
						g.DrawImage(bitmap,drawLocThis.X,drawLocThis.Y);
						return new Point(drawLoc.X,drawLoc.Y+(int)noteSize.Height);
					}
					else {
						noteSize=g.MeasureString(text,baseFont,w);
						noteSize=new SizeF(noteSize.Width,ContrApptSheet.Lh+1);//only allowed to be one line high.
						if(noteSize.Width<5) {
							noteSize=new SizeF(5,noteSize.Height);
						}
						//g.MeasureString(text,baseFont,noteSize,format,out charactersFitted,out linesFilled);
						Point drawLocThis=new Point(drawLoc.X-(int)noteSize.Width,drawLoc.Y);//upper left corner of this element
						rect=new RectangleF(drawLocThis,noteSize);
						rectBack=new RectangleF(drawLocThis.X,drawLocThis.Y+1,noteSize.Width,ContrApptSheet.Lh);
						if(ApptViewItemL.ApptRows[elementI].ElementDesc=="MedOrPremed[+]"
							|| ApptViewItemL.ApptRows[elementI].ElementDesc=="HasIns[I]"
							|| ApptViewItemL.ApptRows[elementI].ElementDesc=="InsToSend[!]")
						{
							g.FillRectangle(brush,rectBack);
							g.DrawString(text,baseFont,Brushes.Black,rect,format);
						}
						else{
							g.FillRectangle(brushWhite,rectBack);
							g.DrawString(text,baseFont,brush,rect,format);
						}
						g.DrawRectangle(Pens.Black,rectBack.X,rectBack.Y,rectBack.Width,rectBack.Height);
						return new Point(drawLoc.X,drawLoc.Y+ContrApptSheet.Lh);//move down a certain number of lines for next element.
					}
				}
				else {//horizontal
					int w=drawLoc.X-9;//drawLoc is upper right of each element.  The first element draws at (Width-1,0).
					if(isGraphic) {
						Bitmap bitmap=new Bitmap(12,12);
						noteSize=new SizeF(bitmap.Width,bitmap.Height);
						Point drawLocThis=new Point(drawLoc.X-(int)noteSize.Width,drawLoc.Y+1);//upper left corner of this element
						rect=new RectangleF(drawLoc,noteSize);
						using(Graphics gfx=Graphics.FromImage(bitmap)) {
							gfx.SmoothingMode=SmoothingMode.HighQuality;
							Color confirmColor=DefC.GetColor(DefCat.ApptConfirmed,PIn.Long(DataRoww["Confirmed"].ToString()));
							SolidBrush confirmBrush=new SolidBrush(confirmColor);
							gfx.FillEllipse(confirmBrush,0,0,11,11);
							gfx.DrawEllipse(Pens.Black,0,0,11,11);
							confirmBrush.Dispose();
						}
						g.DrawImage(bitmap,drawLocThis.X,drawLocThis.Y);
						return new Point(drawLoc.X-(int)noteSize.Width-2,drawLoc.Y);
					}
					else {
						noteSize=g.MeasureString(text,baseFont,w);
						noteSize=new SizeF(noteSize.Width,ContrApptSheet.Lh+1);//only allowed to be one line high.  Needs an extra pixel.
						if(noteSize.Width<5) {
							noteSize=new SizeF(5,noteSize.Height);
						}
						Point drawLocThis=new Point(drawLoc.X-(int)noteSize.Width,drawLoc.Y);//upper left corner of this element
						rect=new RectangleF(drawLocThis,noteSize);
						rectBack=new RectangleF(drawLocThis.X,drawLocThis.Y+1,noteSize.Width,ContrApptSheet.Lh);
						if(ApptViewItemL.ApptRows[elementI].ElementDesc=="MedOrPremed[+]"
							|| ApptViewItemL.ApptRows[elementI].ElementDesc=="HasIns[I]"
							|| ApptViewItemL.ApptRows[elementI].ElementDesc=="InsToSend[!]")
						{
							g.FillRectangle(brush,rectBack);
							g.DrawString(text,baseFont,Brushes.Black,rect,format);
						}
						else{
							g.FillRectangle(brushWhite,rectBack);
							g.DrawString(text,baseFont,brush,rect,format);
						}
						g.DrawRectangle(Pens.Black,rectBack.X,rectBack.Y,rectBack.Width,rectBack.Height);
						return new Point(drawLoc.X-(int)noteSize.Width-1,drawLoc.Y);//Move to left.  Might also have to subtract a little from x to space out elements.
					}
				}
			}
			else {//LR
				if(stackBehavior==ApptViewStackBehavior.Vertical) {
					int w=Width-9;
					if(isGraphic) {
						Bitmap bitmap=new Bitmap(12,12);
						noteSize=new SizeF(bitmap.Width,bitmap.Height);
						Point drawLocThis=new Point(drawLoc.X-(int)noteSize.Width,drawLoc.Y+1-ContrApptSheet.Lh);//upper left corner of this element
						rect=new RectangleF(drawLoc,noteSize);
						using(Graphics gfx=Graphics.FromImage(bitmap)) {
							gfx.SmoothingMode=SmoothingMode.HighQuality;
							Color confirmColor=DefC.GetColor(DefCat.ApptConfirmed,PIn.Long(DataRoww["Confirmed"].ToString()));
							SolidBrush confirmBrush=new SolidBrush(confirmColor);
							gfx.FillEllipse(confirmBrush,0,0,11,11);
							gfx.DrawEllipse(Pens.Black,0,0,11,11);
							confirmBrush.Dispose();
						}
						g.DrawImage(bitmap,drawLocThis.X,drawLocThis.Y);
						return new Point(drawLoc.X,drawLoc.Y-(int)noteSize.Height);
					}
					else {
						noteSize=g.MeasureString(text,baseFont,w);
						noteSize=new SizeF(noteSize.Width,ContrApptSheet.Lh+1);//only allowed to be one line high.  Needs an extra pixel.
						if(noteSize.Width<5) {
							noteSize=new SizeF(5,noteSize.Height);
						}
						//g.MeasureString(text,baseFont,noteSize,format,out charactersFitted,out linesFilled);
						Point drawLocThis=new Point(drawLoc.X-(int)noteSize.Width,drawLoc.Y-ContrApptSheet.Lh);//upper left corner of this element
						rect=new RectangleF(drawLocThis,noteSize);
						rectBack=new RectangleF(drawLocThis.X,drawLocThis.Y+1,noteSize.Width,ContrApptSheet.Lh);
						if(ApptViewItemL.ApptRows[elementI].ElementDesc=="MedOrPremed[+]"
							|| ApptViewItemL.ApptRows[elementI].ElementDesc=="HasIns[I]"
							|| ApptViewItemL.ApptRows[elementI].ElementDesc=="InsToSend[!]")
						{
							g.FillRectangle(brush,rectBack);
							g.DrawString(text,baseFont,Brushes.Black,rect,format);
						}
						else{
							g.FillRectangle(brushWhite,rectBack);
							g.DrawString(text,baseFont,brush,rect,format);
						}
						g.DrawRectangle(Pens.Black,rectBack.X,rectBack.Y,rectBack.Width,rectBack.Height);
						return new Point(drawLoc.X,drawLoc.Y-ContrApptSheet.Lh);//move up a certain number of lines for next element.
					}
				}
				else {//horizontal
					int w=drawLoc.X-9;//drawLoc is upper right of each element.  The first element draws at (Width-1,0).
					if(isGraphic) {
						Bitmap bitmap=new Bitmap(12,12);
						noteSize=new SizeF(bitmap.Width,bitmap.Height);
						Point drawLocThis=new Point(drawLoc.X-(int)noteSize.Width+1,drawLoc.Y+1-ContrApptSheet.Lh);//upper left corner of this element
						rect=new RectangleF(drawLoc,noteSize);
						using(Graphics gfx=Graphics.FromImage(bitmap)) {
							gfx.SmoothingMode=SmoothingMode.HighQuality;
							Color confirmColor=DefC.GetColor(DefCat.ApptConfirmed,PIn.Long(DataRoww["Confirmed"].ToString()));
							SolidBrush confirmBrush=new SolidBrush(confirmColor);
							gfx.FillEllipse(confirmBrush,0,0,11,11);
							gfx.DrawEllipse(Pens.Black,0,0,11,11);
							confirmBrush.Dispose();
						}
						g.DrawImage(bitmap,drawLocThis.X,drawLocThis.Y);
						return new Point(drawLoc.X-(int)noteSize.Width-1,drawLoc.Y);
					}
					else {
						noteSize=g.MeasureString(text,baseFont,w);
						noteSize=new SizeF(noteSize.Width,ContrApptSheet.Lh+1);//only allowed to be one line high.  Needs an extra pixel.
						if(noteSize.Width<5) {
							noteSize=new SizeF(5,noteSize.Height);
						}
						Point drawLocThis=new Point(drawLoc.X-(int)noteSize.Width,drawLoc.Y-ContrApptSheet.Lh);//upper left corner of this element
						rect=new RectangleF(drawLocThis,noteSize);
						rectBack=new RectangleF(drawLocThis.X,drawLocThis.Y+1,noteSize.Width,ContrApptSheet.Lh);
						if(ApptViewItemL.ApptRows[elementI].ElementDesc=="MedOrPremed[+]"
							|| ApptViewItemL.ApptRows[elementI].ElementDesc=="HasIns[I]"
							|| ApptViewItemL.ApptRows[elementI].ElementDesc=="InsToSend[!]")
						{
							g.FillRectangle(brush,rectBack);
							g.DrawString(text,baseFont,Brushes.Black,rect,format);
						}
						else{
							g.FillRectangle(brushWhite,rectBack);
							g.DrawString(text,baseFont,brush,rect,format);
						}
						g.DrawRectangle(Pens.Black,rectBack.X,rectBack.Y,rectBack.Width,rectBack.Height);
						return new Point(drawLoc.X-(int)noteSize.Width-1,drawLoc.Y);//Move to left.  Subtract a little from x to space out elements.
					}
				}
			}
		}
		*/
	}
}
