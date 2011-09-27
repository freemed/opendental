using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace OpenDentBusiness.UI {
	public class ApptSingleDrawing {
		public static float ApptSingleHeight;
		public static float ApptSingleWidth;
		private static Point location;

		///<summary>Set default fontSize to 8 unless printing.</summary>
		public static void DrawEntireAppt(Graphics g,DataRow dataRoww,string patternShowing,float totalWidth,float totalHeight,bool isSelected,bool thisIsPinBoard,long selectedAptNum,List<ApptViewItem> apptRows,ApptView apptViewCur,DataTable tableApptFields,DataTable tablePatFields,int fontSize,bool isPrinting) {
			Pen penB=new Pen(Color.Black);
			Pen penW=new Pen(Color.White);
			Pen penGr=new Pen(Color.SlateGray);
			Pen penDG=new Pen(Color.DarkSlateGray);
			Pen penO;//provider outline color
			Color backColor;
			Color provColor;
			if(dataRoww["ProvNum"].ToString()!="0" && dataRoww["IsHygiene"].ToString()=="0") {//dentist
				provColor=Providers.GetColor(PIn.Long(dataRoww["ProvNum"].ToString()));
				penO=new Pen(Providers.GetOutlineColor(PIn.Long(dataRoww["ProvNum"].ToString())));
			}
			else if(dataRoww["ProvHyg"].ToString()!="0" && dataRoww["IsHygiene"].ToString()=="1") {//hygienist
				provColor=Providers.GetColor(PIn.Long(dataRoww["ProvHyg"].ToString()));
				penO=new Pen(Providers.GetOutlineColor(PIn.Long(dataRoww["ProvHyg"].ToString())));
			}
			else {//unknown
				provColor=Color.White;
				penO=new Pen(Color.Black);
			}
			if(PIn.Long(dataRoww["AptStatus"].ToString())==(int)ApptStatus.Complete) {
				backColor=DefC.Long[(int)DefCat.AppointmentColors][3].ItemColor;
			}
			else if(PIn.Long(dataRoww["AptStatus"].ToString())==(int)ApptStatus.PtNote) {
				backColor=DefC.Long[(int)DefCat.AppointmentColors][7].ItemColor;
			}
			else if(PIn.Long(dataRoww["AptStatus"].ToString()) == (int)ApptStatus.PtNoteCompleted) {
				backColor = DefC.Long[(int)DefCat.AppointmentColors][10].ItemColor;
			}
			else {
				backColor=provColor;
				//We might want to do something interesting here.
			}
			SolidBrush backBrush=new SolidBrush(backColor);
			g.FillRectangle(backBrush,7,0,totalWidth-7,(int)totalHeight);
			g.FillRectangle(Brushes.White,0,0,7,(int)totalHeight);
			g.DrawLine(penB,7,0,7,(int)totalHeight);
			Pen penTimediv=Pens.Silver;
			//g.TextRenderingHint=TextRenderingHint.SingleBitPerPixelGridFit;//to make printing clearer
			for(int i=0;i<patternShowing.Length;i++) {//Info.MyApt.Pattern.Length;i++){
				if(patternShowing.Substring(i,1)=="X") {
					g.FillRectangle(new SolidBrush(provColor),1,i*ApptDrawing.LineH+1,6,ApptDrawing.LineH);
				}
				else {
					//leave empty
				}
				if(Math.IEEERemainder((double)i,(double)ApptDrawing.RowsPerIncr)==0) {//0/1
					g.DrawLine(penTimediv,1,i*ApptDrawing.LineH,6,i*ApptDrawing.LineH);
				}
			}
			#region Highlighting border
			if(isSelected	|| (!thisIsPinBoard && dataRoww["AptNum"].ToString()==selectedAptNum.ToString())) {
				//Left
				g.DrawLine(penO,8,1,8,totalHeight-2);
				g.DrawLine(penO,9,1,9,totalHeight-3);
				//Right
				g.DrawLine(penO,totalWidth-2,1,totalWidth-2,totalHeight-2);
				g.DrawLine(penO,totalWidth-3,2,totalWidth-3,totalHeight-3);
				//Top
				g.DrawLine(penO,8,1,totalWidth-2,1);
				g.DrawLine(penO,8,2,totalWidth-3,2);
				//bottom
				g.DrawLine(penO,9,totalHeight-2,totalWidth-2,totalHeight-2);
				g.DrawLine(penO,10,totalHeight-3,totalWidth-3,totalHeight-3);
			}
			#endregion
			#region Main rows
			Point drawLoc=new Point(9,0);
			int elementI=0;
			while(drawLoc.Y<totalHeight && elementI<apptRows.Count) {
				if(apptRows[elementI].ElementAlignment!=ApptViewAlignment.Main) {
					elementI++;
					continue;
				}
				drawLoc=DrawElement(g,elementI,drawLoc,ApptViewStackBehavior.Vertical,ApptViewAlignment.Main,backBrush,dataRoww,apptRows,tableApptFields,tablePatFields,totalWidth,fontSize);//set the drawLoc to a new point, based on space used by element
				elementI++;
			}
			#endregion
			#region UR
			drawLoc=new Point((int)totalWidth-1,0);//in the UR area, we refer to the upper right corner of each element.
			elementI=0;
			while(drawLoc.Y<totalHeight && elementI<apptRows.Count) {
				if(apptRows[elementI].ElementAlignment!=ApptViewAlignment.UR) {
					elementI++;
					continue;
				}
				drawLoc=DrawElement(g,elementI,drawLoc,apptViewCur.StackBehavUR,ApptViewAlignment.UR,backBrush,dataRoww,apptRows,tableApptFields,tablePatFields,totalWidth,fontSize);
				elementI++;
			}
			#endregion
			#region LR
			drawLoc=new Point((int)totalWidth-1,(int)totalHeight-1);//in the LR area, we refer to the lower right corner of each element.
			elementI=apptRows.Count-1;//For lower right, draw the list backwards.
			while(drawLoc.Y>0 && elementI>=0) {
				if(apptRows[elementI].ElementAlignment!=ApptViewAlignment.LR) {
					elementI--;
					continue;
				}
				drawLoc=DrawElement(g,elementI,drawLoc,apptViewCur.StackBehavLR,ApptViewAlignment.LR,backBrush,dataRoww,apptRows,tableApptFields,tablePatFields,totalWidth,fontSize);
				elementI--;
			}
			#endregion
			//Main outline
			if(isPrinting) {
				g.DrawRectangle(new Pen(Color.Black),0,0,totalWidth,totalHeight);
			}
			else {
				g.DrawRectangle(new Pen(Color.Black),0,0,totalWidth-1,totalHeight-1);
			}
			//broken X
			if(dataRoww["AptStatus"].ToString()==((int)ApptStatus.Broken).ToString()) {
				g.DrawLine(new Pen(Color.Black),8,1,totalWidth-1,totalHeight-1);
				g.DrawLine(new Pen(Color.Black),8,totalHeight-1,totalWidth-1,1);
			}
		}

		///<summary></summary>
		public static Point DrawElement(Graphics g,int elementI,Point drawLoc,ApptViewStackBehavior stackBehavior,ApptViewAlignment align,Brush backBrush,DataRow dataRoww,List<ApptViewItem> apptRows,DataTable tableApptFields,DataTable tablePatFields,float totalWidth,int fontSize) {
			Font baseFont=new Font("Arial",fontSize);
			string text="";
			bool isNote=false;
			#region FillText
			if(PIn.Long(dataRoww["AptStatus"].ToString()) == (int)ApptStatus.PtNote
				|| PIn.Long(dataRoww["AptStatus"].ToString()) == (int)ApptStatus.PtNoteCompleted) {
				isNote=true;
			}
			bool isGraphic=false;
			if(apptRows[elementI].ElementDesc=="ConfirmedColor") {
				isGraphic=true;
			}
			if(apptRows[elementI].ApptFieldDefNum>0) {
				string fieldName=ApptFieldDefs.GetFieldName(apptRows[elementI].ApptFieldDefNum);
				for(int i=0;i<tableApptFields.Rows.Count;i++) {
					if(tableApptFields.Rows[i]["AptNum"].ToString()!=dataRoww["AptNum"].ToString()) {
						continue;
					}
					if(tableApptFields.Rows[i]["FieldName"].ToString()!=fieldName) {
						continue;
					}
					text=tableApptFields.Rows[i]["FieldValue"].ToString();
				}
			}
			else if(apptRows[elementI].PatFieldDefNum>0) {
				string fieldName=PatFieldDefs.GetFieldName(apptRows[elementI].PatFieldDefNum);
				for(int i=0;i<tablePatFields.Rows.Count;i++) {
					if(tablePatFields.Rows[i]["PatNum"].ToString()!=dataRoww["PatNum"].ToString()) {
						continue;
					}
					if(tablePatFields.Rows[i]["FieldName"].ToString()!=fieldName) {
						continue;
					}
					text=tablePatFields.Rows[i]["FieldValue"].ToString();
				}
			}
			else switch(apptRows[elementI].ElementDesc) {
					case "Address":
						if(isNote) {
							text="";
						}
						else {
							text=dataRoww["address"].ToString();
						}
						break;
					case "AddrNote":
						if(isNote) {
							text="";
						}
						else {
							text=dataRoww["addrNote"].ToString();
						}
						break;
					case "Age":
						if(isNote) {
							text="";
						}
						else {
							text=dataRoww["age"].ToString();
						}
						break;
					case "ASAP":
						if(isNote) {
							text="";
						}
						else {
							if(dataRoww["AptStatus"].ToString()==((int)ApptStatus.ASAP).ToString()) {
								text=Lans.g("enumApptStatus","ASAP");
							}
						}
						break;
					case "ASAP[A]":
						if(dataRoww["AptStatus"].ToString()==((int)ApptStatus.ASAP).ToString()) {
							text="A";
						}
						break;
					case "AssistantAbbr":
						if(isNote) {
							text="";
						}
						else {
							text=dataRoww["assistantAbbr"].ToString();
						}
						break;
					case "ChartNumAndName":
						text=dataRoww["chartNumAndName"].ToString();
						break;
					case "ChartNumber":
						text=dataRoww["chartNumber"].ToString();
						break;
					case "CreditType":
						text=dataRoww["CreditType"].ToString();
						break;
					case "Guardians":
						if(isNote) {
							text="";
						}
						else {
							text=dataRoww["guardians"].ToString();
						}
						break;
					case "HasIns[I]":
						text=dataRoww["hasIns[I]"].ToString();
						break;
					case "HmPhone":
						if(isNote) {
							text="";
						}
						else {
							text=dataRoww["hmPhone"].ToString();
						}
						break;
					case "InsToSend[!]":
						text=dataRoww["insToSend[!]"].ToString();
						break;
					case "Lab":
						if(isNote) {
							text="";
						}
						else {
							text=dataRoww["lab"].ToString();
						}
						break;
					case "MedOrPremed[+]":
						if(isNote) {
							text="";
						}
						else {
							text=dataRoww["medOrPremed[+]"].ToString();
						}
						break;
					case "MedUrgNote":
						if(isNote) {
							text="";
						}
						else {
							text=dataRoww["MedUrgNote"].ToString();
						}
						break;
					case "Note":
						text=dataRoww["Note"].ToString();
						break;
					case "PatientName":
						text=dataRoww["patientName"].ToString();
						break;
					case "PatientNameF":
						text=dataRoww["patientNameF"].ToString();
						break;
					case "PatNum":
						text=dataRoww["patNum"].ToString();
						break;
					case "PatNumAndName":
						text=dataRoww["patNumAndName"].ToString();
						break;
					case "PremedFlag":
						if(isNote) {
							text="";
						}
						else {
							text=dataRoww["preMedFlag"].ToString();
						}
						break;
					case "Procs":
						if(isNote) {
							text="";
						}
						else {
							text=dataRoww["procs"].ToString();
						}
						break;
					case "ProcsColored":
						string value=dataRoww["procsColored"].ToString();
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
							if(rgbInt=="") {
								rgbInt=apptRows[elementI].ElementColorXml.ToString();
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
							if(tempPt.X+procRect.Width>totalWidth) {
								tempPt.X=drawLoc.X;
								tempPt.Y+=lastH;
								count++;
							}
							SolidBrush sb=new SolidBrush(c);
							g.DrawString(proc,baseFont,sb,tempPt);
							sb.Dispose();
							tempPt.X+=(int)procRect.Width+3;//+3 is room for spaces
							if((int)procRect.Height>lastH) {
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
							text=dataRoww["production"].ToString();
						}
						break;
					case "Provider":
						if(isNote) {
							text="";
						}
						else {
							text=dataRoww["provider"].ToString();
						}
						break;
					case "TimeAskedToArrive":
						if(isNote) {
							text="";
						}
						else {
							text=dataRoww["timeAskedToArrive"].ToString();//could be blank
						}
						break;
					case "WirelessPhone":
						if(isNote) {
							text="";
						}
						else {
							text=dataRoww["wirelessPhone"].ToString();
						}
						break;
					case "WkPhone":
						if(isNote) {
							text="";
						}
						else {
							text=dataRoww["wkPhone"].ToString();
						}
						break;

				}
			#endregion
			if(text=="" && !isGraphic) {
				return drawLoc;//next element will draw at the same position as this one would have.
			}
			SolidBrush brush=new SolidBrush(apptRows[elementI].ElementColor);
			SolidBrush brushWhite=new SolidBrush(Color.White);
			SolidBrush noteTitlebrush = new SolidBrush(DefC.Long[(int)DefCat.AppointmentColors][8].ItemColor);
			StringFormat format=new StringFormat();
			format.Alignment=StringAlignment.Near;
			int charactersFitted;//not used, but required as 'out' param for measureString.
			int linesFilled;
			SizeF noteSize;
			RectangleF rect;
			RectangleF rectBack;
			#region Main
			if(align==ApptViewAlignment.Main) {//always stacks vertical
				if(isGraphic) {
					Bitmap bitmap=new Bitmap(12,12);
					noteSize=new SizeF(bitmap.Width,bitmap.Height);
					rect=new RectangleF(drawLoc,noteSize);
					using(Graphics gfx=Graphics.FromImage(bitmap)) {
						gfx.SmoothingMode=SmoothingMode.HighQuality;
						Color confirmColor=DefC.GetColor(DefCat.ApptConfirmed,PIn.Long(dataRoww["Confirmed"].ToString()));
						SolidBrush confirmBrush=new SolidBrush(confirmColor);
						gfx.FillEllipse(confirmBrush,0,0,11,11);
						gfx.DrawEllipse(Pens.Black,0,0,11,11);
						confirmBrush.Dispose();
					}
					g.DrawImage(bitmap,drawLoc.X,drawLoc.Y);
					return new Point(drawLoc.X,drawLoc.Y+(int)noteSize.Height);
				}
				else {
					noteSize=g.MeasureString(text,baseFont,(int)totalWidth-9);
					//Problem: "limited-tooth bothering him ", the trailing space causes measuring error, resulting in m getting partially chopped off.
					//Tried TextRenderer, but it caused premature wrapping
					//Size noteSizeInt=TextRenderer.MeasureText(text,baseFont,new Size(totalWidth-9,1000));
					//noteSize=new SizeF(noteSizeInt.totalWidth,noteSizeInt.totalHeight);
					noteSize.Width=(float)Math.Ceiling((double)noteSize.Width);//round up to nearest int solves specific problem discussed above.
					g.MeasureString(text,baseFont,noteSize,format,out charactersFitted,out linesFilled);
					rect=new RectangleF(drawLoc,noteSize);
					g.DrawString(text,baseFont,brush,rect,format);
					return new Point(drawLoc.X,drawLoc.Y+linesFilled*ApptDrawing.LineH);
				}
			}
			#endregion
			#region UR
			else if(align==ApptViewAlignment.UR) {
				if(stackBehavior==ApptViewStackBehavior.Vertical) {
					float w=totalWidth-9;
					if(isGraphic) {
						Bitmap bitmap=new Bitmap(12,12);
						noteSize=new SizeF(bitmap.Width,bitmap.Height);
						Point drawLocThis=new Point(drawLoc.X-(int)noteSize.Width,drawLoc.Y+1);//upper left corner of this element
						rect=new RectangleF(drawLoc,noteSize);
						using(Graphics gfx=Graphics.FromImage(bitmap)) {
							gfx.SmoothingMode=SmoothingMode.HighQuality;
							Color confirmColor=DefC.GetColor(DefCat.ApptConfirmed,PIn.Long(dataRoww["Confirmed"].ToString()));
							SolidBrush confirmBrush=new SolidBrush(confirmColor);
							gfx.FillEllipse(confirmBrush,0,0,11,11);
							gfx.DrawEllipse(Pens.Black,0,0,11,11);
							confirmBrush.Dispose();
						}
						g.DrawImage(bitmap,drawLocThis.X,drawLocThis.Y);
						return new Point(drawLoc.X,drawLoc.Y+(int)noteSize.Height);
					}
					else {
						noteSize=g.MeasureString(text,baseFont,(int)w);
						noteSize=new SizeF(noteSize.Width,ApptDrawing.LineH+1);//only allowed to be one line high.
						if(noteSize.Width<5) {
							noteSize=new SizeF(5,noteSize.Height);
						}
						//g.MeasureString(text,baseFont,noteSize,format,out charactersFitted,out linesFilled);
						Point drawLocThis=new Point(drawLoc.X-(int)noteSize.Width,drawLoc.Y);//upper left corner of this element
						rect=new RectangleF(drawLocThis,noteSize);
						rectBack=new RectangleF(drawLocThis.X,drawLocThis.Y+1,noteSize.Width,ApptDrawing.LineH);
						if(apptRows[elementI].ElementDesc=="MedOrPremed[+]"
							|| apptRows[elementI].ElementDesc=="HasIns[I]"
							|| apptRows[elementI].ElementDesc=="InsToSend[!]") {
							g.FillRectangle(brush,rectBack);
							g.DrawString(text,baseFont,Brushes.Black,rect,format);
						}
						else {
							g.FillRectangle(brushWhite,rectBack);
							g.DrawString(text,baseFont,brush,rect,format);
						}
						g.DrawRectangle(Pens.Black,rectBack.X,rectBack.Y,rectBack.Width,rectBack.Height);
						return new Point(drawLoc.X,drawLoc.Y+ApptDrawing.LineH);//move down a certain number of lines for next element.
					}
				}
				else {//horizontal
					int w=drawLoc.X-9;//drawLoc is upper right of each element.  The first element draws at (totalWidth-1,0).
					if(isGraphic) {
						Bitmap bitmap=new Bitmap(12,12);
						noteSize=new SizeF(bitmap.Width,bitmap.Height);
						Point drawLocThis=new Point(drawLoc.X-(int)noteSize.Width,drawLoc.Y+1);//upper left corner of this element
						rect=new RectangleF(drawLoc,noteSize);
						using(Graphics gfx=Graphics.FromImage(bitmap)) {
							gfx.SmoothingMode=SmoothingMode.HighQuality;
							Color confirmColor=DefC.GetColor(DefCat.ApptConfirmed,PIn.Long(dataRoww["Confirmed"].ToString()));
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
						noteSize=new SizeF(noteSize.Width,ApptDrawing.LineH+1);//only allowed to be one line high.  Needs an extra pixel.
						if(noteSize.Width<5) {
							noteSize=new SizeF(5,noteSize.Height);
						}
						Point drawLocThis=new Point(drawLoc.X-(int)noteSize.Width,drawLoc.Y);//upper left corner of this element
						rect=new RectangleF(drawLocThis,noteSize);
						rectBack=new RectangleF(drawLocThis.X,drawLocThis.Y+1,noteSize.Width,ApptDrawing.LineH);
						if(apptRows[elementI].ElementDesc=="MedOrPremed[+]"
							|| apptRows[elementI].ElementDesc=="HasIns[I]"
							|| apptRows[elementI].ElementDesc=="InsToSend[!]") {
							g.FillRectangle(brush,rectBack);
							g.DrawString(text,baseFont,Brushes.Black,rect,format);
						}
						else {
							g.FillRectangle(brushWhite,rectBack);
							g.DrawString(text,baseFont,brush,rect,format);
						}
						g.DrawRectangle(Pens.Black,rectBack.X,rectBack.Y,rectBack.Width,rectBack.Height);
						return new Point(drawLoc.X-(int)noteSize.Width-1,drawLoc.Y);//Move to left.  Might also have to subtract a little from x to space out elements.
					}
				}
			}
			#endregion
			#region LR
			else {//LR
				if(stackBehavior==ApptViewStackBehavior.Vertical) {
					float w=totalWidth-9;
					if(isGraphic) {
						Bitmap bitmap=new Bitmap(12,12);
						noteSize=new SizeF(bitmap.Width,bitmap.Height);
						Point drawLocThis=new Point(drawLoc.X-(int)noteSize.Width,drawLoc.Y+1-ApptDrawing.LineH);//upper left corner of this element
						rect=new RectangleF(drawLoc,noteSize);
						using(Graphics gfx=Graphics.FromImage(bitmap)) {
							gfx.SmoothingMode=SmoothingMode.HighQuality;
							Color confirmColor=DefC.GetColor(DefCat.ApptConfirmed,PIn.Long(dataRoww["Confirmed"].ToString()));
							SolidBrush confirmBrush=new SolidBrush(confirmColor);
							gfx.FillEllipse(confirmBrush,0,0,11,11);
							gfx.DrawEllipse(Pens.Black,0,0,11,11);
							confirmBrush.Dispose();
						}
						g.DrawImage(bitmap,drawLocThis.X,drawLocThis.Y);
						return new Point(drawLoc.X,drawLoc.Y-(int)noteSize.Height);
					}
					else {
						noteSize=g.MeasureString(text,baseFont,(int)w);
						noteSize=new SizeF(noteSize.Width,ApptDrawing.LineH+1);//only allowed to be one line high.  Needs an extra pixel.
						if(noteSize.Width<5) {
							noteSize=new SizeF(5,noteSize.Height);
						}
						//g.MeasureString(text,baseFont,noteSize,format,out charactersFitted,out linesFilled);
						Point drawLocThis=new Point(drawLoc.X-(int)noteSize.Width,drawLoc.Y-ApptDrawing.LineH);//upper left corner of this element
						rect=new RectangleF(drawLocThis,noteSize);
						rectBack=new RectangleF(drawLocThis.X,drawLocThis.Y+1,noteSize.Width,ApptDrawing.LineH);
						if(apptRows[elementI].ElementDesc=="MedOrPremed[+]"
							|| apptRows[elementI].ElementDesc=="HasIns[I]"
							|| apptRows[elementI].ElementDesc=="InsToSend[!]") {
							g.FillRectangle(brush,rectBack);
							g.DrawString(text,baseFont,Brushes.Black,rect,format);
						}
						else {
							g.FillRectangle(brushWhite,rectBack);
							g.DrawString(text,baseFont,brush,rect,format);
						}
						g.DrawRectangle(Pens.Black,rectBack.X,rectBack.Y,rectBack.Width,rectBack.Height);
						return new Point(drawLoc.X,drawLoc.Y-ApptDrawing.LineH);//move up a certain number of lines for next element.
					}
				}
				else {//horizontal
					int w=drawLoc.X-9;//drawLoc is upper right of each element.  The first element draws at (totalWidth-1,0).
					if(isGraphic) {
						Bitmap bitmap=new Bitmap(12,12);
						noteSize=new SizeF(bitmap.Width,bitmap.Height);
						Point drawLocThis=new Point(drawLoc.X-(int)noteSize.Width+1,drawLoc.Y+1-ApptDrawing.LineH);//upper left corner of this element
						rect=new RectangleF(drawLoc,noteSize);
						using(Graphics gfx=Graphics.FromImage(bitmap)) {
							gfx.SmoothingMode=SmoothingMode.HighQuality;
							Color confirmColor=DefC.GetColor(DefCat.ApptConfirmed,PIn.Long(dataRoww["Confirmed"].ToString()));
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
						noteSize=new SizeF(noteSize.Width,ApptDrawing.LineH+1);//only allowed to be one line high.  Needs an extra pixel.
						if(noteSize.Width<5) {
							noteSize=new SizeF(5,noteSize.Height);
						}
						Point drawLocThis=new Point(drawLoc.X-(int)noteSize.Width,drawLoc.Y-ApptDrawing.LineH);//upper left corner of this element
						rect=new RectangleF(drawLocThis,noteSize);
						rectBack=new RectangleF(drawLocThis.X,drawLocThis.Y+1,noteSize.Width,ApptDrawing.LineH);
						if(apptRows[elementI].ElementDesc=="MedOrPremed[+]"
							|| apptRows[elementI].ElementDesc=="HasIns[I]"
							|| apptRows[elementI].ElementDesc=="InsToSend[!]") {
							g.FillRectangle(brush,rectBack);
							g.DrawString(text,baseFont,Brushes.Black,rect,format);
						}
						else {
							g.FillRectangle(brushWhite,rectBack);
							g.DrawString(text,baseFont,brush,rect,format);
						}
						g.DrawRectangle(Pens.Black,rectBack.X,rectBack.Y,rectBack.Width,rectBack.Height);
						return new Point(drawLoc.X-(int)noteSize.Width-1,drawLoc.Y);//Move to left.  Subtract a little from x to space out elements.
					}
				}
			}
			#endregion
		}

		///<summary>This is only called when viewing appointments on the Appt module.  For Planned apt and pinboard, use SetSize instead so that the location won't change.  Pass 0 for startHour unless printing.  Pass visible ops for colsPerPage unless printing.  Pass 0 for pageColumn unless printing.</summary>
		public static Point SetLocation(DataRow dataRoww,int beginHour,int colsPerPage,int pageColumn) {
			if(ApptDrawing.IsWeeklyView) {
				ApptSingleWidth=(int)ApptDrawing.ColAptWidth;
				location=new Point(ConvertToX(dataRoww,colsPerPage,pageColumn),ConvertToY(dataRoww,beginHour));
			}
			else {
				location=new Point(ConvertToX(dataRoww,colsPerPage,pageColumn)+2,ConvertToY(dataRoww,beginHour));
				ApptSingleWidth=ApptDrawing.ColWidth-5;
			}
			return location;
		}

		///<summary>Used for Planned apt and pinboard instead of SetLocation so that the location won't be altered.</summary>
		public static Size SetSize(DataRow dataRoww) {
			ApptSingleWidth=ApptDrawing.ColWidth-5;
			if(ApptDrawing.IsWeeklyView) {
				ApptSingleWidth=(int)ApptDrawing.ColAptWidth;
			}
			//height is based on original 5 minute pattern. Might result in half-rows
			ApptSingleHeight=dataRoww["Pattern"].ToString().Length*ApptDrawing.LineH*ApptDrawing.RowsPerIncr;
			if(PrefC.GetLong(PrefName.AppointmentTimeIncrement)==10) {
				ApptSingleHeight=ApptSingleHeight/2;
			}
			if(PrefC.GetLong(PrefName.AppointmentTimeIncrement)==15) {
				ApptSingleHeight=ApptSingleHeight/3;
			}
			return new Size((int)ApptSingleWidth,(int)ApptSingleHeight);
		}

		///<summary>Called from SetLocation to establish X position of control.</summary>
		public static int ConvertToX(DataRow dataRoww,int colsPerPage,int pageColumn) {
			if(ApptDrawing.IsWeeklyView) {
				//the next few lines are because we start on Monday instead of Sunday
				int dayofweek=(int)PIn.DateT(dataRoww["AptDateTime"].ToString()).DayOfWeek-1;
				if(dayofweek==-1) {
					dayofweek=6;
				}
				return (int)(ApptDrawing.TimeWidth
					+ApptDrawing.ColDayWidth*(dayofweek)+1
					+(ApptDrawing.ColAptWidth*(ApptDrawing.GetIndexOp(PIn.Long(dataRoww["Op"].ToString()))-(colsPerPage*pageColumn))));
			}
			else {
				return (int)(ApptDrawing.TimeWidth+ApptDrawing.ProvWidth*ApptDrawing.ProvCount
					+ApptDrawing.ColWidth*(ApptDrawing.GetIndexOp(PIn.Long(dataRoww["Op"].ToString()))-(colsPerPage*pageColumn))+1);
				//Info.MyApt.Op))+1;
			}
		}

		///<summary>Called from SetLocation to establish Y position of control.  Also called from ContrAppt.RefreshDay when determining ProvBar markings. Does not round to the nearest row.</summary>
		public static int ConvertToY(DataRow dataRoww,int beginHour) {
			DateTime aptDateTime=PIn.DateT(dataRoww["AptDateTime"].ToString());
			int retVal=(int)(((double)(aptDateTime.Hour-beginHour)*(double)60
				/(double)PrefC.GetLong(PrefName.AppointmentTimeIncrement)
				+(double)aptDateTime.Minute
				/(double)PrefC.GetLong(PrefName.AppointmentTimeIncrement)
				)*(double)ApptDrawing.LineH*ApptDrawing.RowsPerIncr);
			return retVal;
		}

		///<summary>This converts the dbPattern in 5 minute interval into the pattern that will be viewed based on RowsPerIncrement and AppointmentTimeIncrement.  So it will always depend on the current view.Therefore, it should only be used for visual display purposes rather than within the FormAptEdit. If height of appointment allows a half row, then this includes an increment for that half row.</summary>
		public static string GetPatternShowing(string dbPattern) {
			StringBuilder strBTime=new StringBuilder();
			for(int i=0;i<dbPattern.Length;i++) {
				for(int j=0;j<ApptDrawing.RowsPerIncr;j++) {
					strBTime.Append(dbPattern.Substring(i,1));
				}
				if(PrefC.GetLong(PrefName.AppointmentTimeIncrement)==10) {
					i++;//skip
				}
				if(PrefC.GetLong(PrefName.AppointmentTimeIncrement)==15) {
					i++;
					i++;//skip two
				}
			}
			return strBTime.ToString();
		}

		///<summary>Tests if the appt is in the allotted time frame and is in a visible operatory.  Returns false in order to skip drawing for apptointment printing.</summary>
		public static bool ApptWithinTimeFrame(DataRow dataRoww,DateTime beginTime,DateTime endTime,int colsPerPage,int pageColumn) {
			//Test if appts op is currently visible.
			bool visible=false;
			for(int i=0;i<colsPerPage;i++) {
				if(i==ApptDrawing.VisOps.Count) {
					return false;
				}
				int k=colsPerPage*pageColumn+i;
				if(k>=ApptDrawing.VisOps.Count) {
					return false;
				}
				int testOp=ApptDrawing.GetIndexOp(PIn.Long(dataRoww["Op"].ToString()));
				if(k==ApptDrawing.GetIndexOp(PIn.Long(dataRoww["Op"].ToString()))) {
					visible=true;
					break;
				}
			}
			if(!visible) {//Op not visible so don't test time frame.
				return false;
			}
			//Test if any portion of appt is within time frame.
			TimeSpan aptTimeBegin=PIn.DateT(dataRoww["AptDateTime"].ToString()).TimeOfDay;
			TimeSpan aptTimeEnd=aptTimeBegin.Add(new TimeSpan(0,dataRoww["Pattern"].ToString().Length*5,0));
			if(aptTimeBegin>=endTime.TimeOfDay || aptTimeEnd<=beginTime.TimeOfDay) {
				return false;
			}
			return true;
		}

	}
}
