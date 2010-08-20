using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Forms;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Shapes;
using MigraDoc.DocumentObjectModel.Tables;
using OpenDental.UI;
using OpenDentBusiness;

namespace OpenDental {
	///<summary>Used to add functionality to the MigraDoc framework.  Specifically, it helps with absolute positioning.</summary>
	public class MigraDocHelper {
		///<Summary>Creates a container frame in a section.  This allows other objects to be placed absolutely within a given area by adding more textframes within this one.  This frame is full width and is automatically placed after previous elements.</Summary>
		public static TextFrame CreateContainer(Section section){
			TextFrame framebig=section.AddTextFrame();
			framebig.RelativeVertical=RelativeVertical.Line;
			framebig.RelativeHorizontal=RelativeHorizontal.Page;
			framebig.MarginLeft=Unit.Zero;
			framebig.MarginTop=Unit.Zero;
			framebig.Top=TopPosition.Parse("0 in");
			framebig.Left=LeftPosition.Parse("0 in");
			if(CultureInfo.CurrentCulture.Name=="en-US"){
				framebig.Width=Unit.FromInch(8.5);
			}
			//don't know about Canada
			else{
				framebig.Width=Unit.FromInch(8.3);
			}
			//LineFormat lineformat=new LineFormat();
			//lineformat.Width=1;
			//framebig.LineFormat=lineformat;
			return framebig;
		}

		///<summary>Creates a somewhat smaller container at an absolute position on the page</summary>
		public static TextFrame CreateContainer(Section section,float xPos,float yPos,float width,float height) {
			TextFrame framebig=section.AddTextFrame();
			framebig.RelativeVertical=RelativeVertical.Page;
			framebig.RelativeHorizontal=RelativeHorizontal.Page;
			framebig.MarginLeft=Unit.Zero;
			framebig.MarginTop=Unit.Zero;
			framebig.Top=TopPosition.Parse((yPos/100).ToString()+" in");
			framebig.Left=LeftPosition.Parse((xPos/100).ToString()+" in");
			framebig.Width=Unit.FromInch(width/100);
			framebig.Height=Unit.FromInch(height/100);
			return framebig;
		}

		///<summary>In 100ths of an inch</summary>
		public static int GetDocWidth(){
			if(CultureInfo.CurrentCulture.Name=="en-US") {
				return 850;
			}
			//don't know about Canada
			else {
				return 830;
			}
		}

		///<summary></summary>
		public static void DrawString(TextFrame frameContainer,string text,MigraDoc.DocumentObjectModel.Font font,RectangleF rectF){
			DrawString(frameContainer,text,font,rectF,ParagraphAlignment.Left);
		}

		///<summary></summary>
		public static void DrawString(TextFrame frameContainer,string text,MigraDoc.DocumentObjectModel.Font font,RectangleF rectF,
			ParagraphAlignment alignment) 
		{
			TextFrame frame=new TextFrame();
			Paragraph par=frame.AddParagraph();
			par.Format.Font=font.Clone();
			par.Format.Alignment=alignment;
			par.AddText(text);
			frame.RelativeVertical=RelativeVertical.Page;
			frame.RelativeHorizontal=RelativeHorizontal.Page;
			frame.MarginLeft=Unit.FromInch(rectF.Left/100);
			frame.MarginTop=Unit.FromInch(rectF.Top/100);
			frame.Top=TopPosition.Parse("0 in");
			frame.Left=LeftPosition.Parse("0 in");
			frame.Width=Unit.FromInch(rectF.Right/100f);//    frameContainer.Width;
			Unit bottom=Unit.FromInch(rectF.Bottom/100f);
			if(frameContainer.Height<bottom) {
				frameContainer.Height=bottom;
			}
			frame.Height=frameContainer.Height;
			//LineFormat lineformat=new LineFormat();
			//lineformat.Width=1;
			//frame.LineFormat=lineformat;
			frameContainer.Elements.Add(frame);
		}

		///<summary></summary>
		public static void DrawString(TextFrame frameContainer,string text,MigraDoc.DocumentObjectModel.Font font,float xPos,float yPos) {
			TextFrame frame=new TextFrame();
			Paragraph par=frame.AddParagraph();
			par.Format.Font=font.Clone();
			par.AddText(text);
			frame.RelativeVertical=RelativeVertical.Page;
			frame.RelativeHorizontal=RelativeHorizontal.Page;
			frame.MarginLeft=Unit.FromInch(xPos/100);
			frame.MarginTop=Unit.FromInch(yPos/100);
			frame.Top=TopPosition.Parse("0 in");
			frame.Left=LeftPosition.Parse("0 in");
			frame.Width=frameContainer.Width;
			FontStyle fontstyle=FontStyle.Regular;
			if(font.Bold){
				fontstyle=FontStyle.Bold;
			}
			System.Drawing.Font fontSystem=new System.Drawing.Font(font.Name,(float)font.Size.Point,fontstyle);
			float fontH=fontSystem.Height;
			Unit bottom=Unit.FromInch((yPos+fontH)/100);
			if(frameContainer.Height<bottom) {
				frameContainer.Height=bottom;
			}
			frame.Height=frameContainer.Height;
			//LineFormat lineformat=new LineFormat();
			//lineformat.Width=1;
			//frame.LineFormat=lineformat;
			frameContainer.Elements.Add(frame);
		}

		///<summary></summary>
		public static void DrawBitmap(TextFrame frameContainer,System.Drawing.Bitmap bitmap,float xPos,float yPos) {
			string imageFileName=Path.GetTempFileName();
			bitmap.SetResolution(100,100);//prevents framework from scaling it.
			bitmap.Save(imageFileName);
			TextFrame frame=new TextFrame();
			frame.AddImage(imageFileName);
			frame.RelativeVertical=RelativeVertical.Page;
			frame.RelativeHorizontal=RelativeHorizontal.Page;
			frame.MarginLeft=Unit.FromInch(xPos/100);
			frame.MarginTop=Unit.FromInch(yPos/100);
			frame.Top=TopPosition.Parse("0 in");
			frame.Left=LeftPosition.Parse("0 in");
			frame.Width=frameContainer.Width;
			Unit bottom=Unit.FromInch((yPos+(double)bitmap.Height)/100);
			if(frameContainer.Height<bottom) {
				frameContainer.Height=bottom;
			}
			frame.Height=frameContainer.Height;
			//LineFormat lineformat=new LineFormat();
			//lineformat.Width=1;
			//frame.LineFormat=lineformat;
			frameContainer.Elements.Add(frame);
		}

		public static MigraDoc.DocumentObjectModel.Font CreateFont(float fsize) {
			MigraDoc.DocumentObjectModel.Font font=new MigraDoc.DocumentObjectModel.Font();
			font.Name="Arial";
			font.Size=Unit.FromPoint(fsize);
			return font;
		}

		public static MigraDoc.DocumentObjectModel.Font CreateFont(float fsize,bool isBold){
			MigraDoc.DocumentObjectModel.Font font=new MigraDoc.DocumentObjectModel.Font();
			//if(fontFamily==FontFamily.GenericSansSerif){
			font.Name="Arial";
			//}
			//if(fontFamily==FontFamily.GenericSerif){
			//	font.Name="Times";
			//}
			font.Size=Unit.FromPoint(fsize);
			font.Bold=isBold;
			return font;
		}

		public static MigraDoc.DocumentObjectModel.Font CreateFont(float fsize,bool isBold,System.Drawing.Color color) {
			MigraDoc.DocumentObjectModel.Color colorx=ConvertColor(color);
			MigraDoc.DocumentObjectModel.Font font=new MigraDoc.DocumentObjectModel.Font();
			//if(fontFamily.==FontFamily.GenericSansSerif) {
			font.Name="Arial";
			//}
			//if(fontFamily==FontFamily.GenericSerif) {
			//	font.Name="Times";
			//}
			font.Size=Unit.FromPoint(fsize);
			font.Bold=isBold;
			font.Color=colorx;
			return font;
		}

		public static MigraDoc.DocumentObjectModel.Color ConvertColor(System.Drawing.Color color){
			byte a=color.A;
			byte r=color.R;
			byte g=color.G;
			byte b=color.B;
			return new MigraDoc.DocumentObjectModel.Color(a,r,g,b);
		}

		public static void FillRectangle(TextFrame frameContainer,System.Drawing.Color color,float xPos,float yPos,float width,float height) {
			MigraDoc.DocumentObjectModel.Color colorx=ConvertColor(color);
			TextFrame frameRect=new TextFrame();
			frameRect.FillFormat.Visible=true;
			frameRect.FillFormat.Color=colorx;
			frameRect.Height=Unit.FromInch(height/100);
			frameRect.Width=Unit.FromInch(width/100);
			TextFrame frame=new TextFrame();
			frame.Elements.Add(frameRect);
			frame.RelativeVertical=RelativeVertical.Page;
			frame.RelativeHorizontal=RelativeHorizontal.Page;
			frame.MarginLeft=Unit.FromInch(xPos/100);
			frame.MarginTop=Unit.FromInch(yPos/100);
			frame.Top=TopPosition.Parse("0 in");
			frame.Left=LeftPosition.Parse("0 in");
			frame.Width=frameContainer.Width;
			Unit bottom=Unit.FromInch((yPos+height)/100);
			if(frameContainer.Height<bottom) {
				frameContainer.Height=bottom;
			}
			frame.Height=frameContainer.Height;
			//LineFormat lineformat=new LineFormat();
			//lineformat.Width=1;
			//frame.LineFormat=lineformat;
			frameContainer.Elements.Add(frame);
		}

		public static void DrawRectangle(TextFrame frameContainer,System.Drawing.Color color,float xPos,float yPos,float width,float height) {
			MigraDoc.DocumentObjectModel.Color colorx=ConvertColor(color);
			TextFrame frameRect=new TextFrame();
			frameRect.LineFormat.Color=colorx;
			frameRect.Height=Unit.FromInch(height/100);
			frameRect.Width=Unit.FromInch(width/100);
			TextFrame frame=new TextFrame();
			frame.Elements.Add(frameRect);
			frame.RelativeVertical=RelativeVertical.Page;
			frame.RelativeHorizontal=RelativeHorizontal.Page;
			frame.MarginLeft=Unit.FromInch(xPos/100);
			frame.MarginTop=Unit.FromInch(yPos/100);
			frame.Top=TopPosition.Parse("0 in");
			frame.Left=LeftPosition.Parse("0 in");
			frame.Width=frameContainer.Width;
			Unit bottom=Unit.FromInch((yPos+height)/100);
			if(frameContainer.Height<bottom) {
				frameContainer.Height=bottom;
			}
			frame.Height=frameContainer.Height;
			frameContainer.Elements.Add(frame);
		}

		///<summary>Only supports horizontal and vertical lines.  Assumes single width, no dashes.</summary>
		public static void DrawLine(TextFrame frameContainer,System.Drawing.Color color,float x1,float y1,float x2,float y2) {
			MigraDoc.DocumentObjectModel.Color colorx=ConvertColor(color);
			TextFrame frameRect=new TextFrame();
			TextFrame frame=new TextFrame();//nearly as big as frameContainer.  Its margins will position the line.
			frameRect.LineFormat.Color=colorx;
			if(x1==x2){//vertical
				frameRect.Width=Unit.FromPoint(.01);
				frame.MarginLeft=Unit.FromInch(x1/100);
				if(y2>y1){//draw down
					frameRect.Height=Unit.FromInch((y2-y1)/100);
					frame.MarginTop=Unit.FromInch(y1/100);
				}
				else{//draw up
					frameRect.Height=Unit.FromInch((y1-y2)/100);
					frame.MarginTop=Unit.FromInch(y2/100);
				}
			}
			else if(y1==y2){//horizontal
				frameRect.Height=Unit.FromPoint(.01);
				frame.MarginTop=Unit.FromInch(y1/100);
				if(x2>x1) {//right
					frameRect.Width=Unit.FromInch((x2-x1)/100);
					frame.MarginLeft=Unit.FromInch(x1/100);
				}
				else {//draw left
					frameRect.Width=Unit.FromInch((x1-x2)/100);
					frame.MarginLeft=Unit.FromInch(x2/100);
				}
			}
			else{
				return;//diagonal lines not supported.
			}
			frame.Elements.Add(frameRect);
			frame.RelativeVertical=RelativeVertical.Page;
			frame.RelativeHorizontal=RelativeHorizontal.Page;
			frame.Top=TopPosition.Parse("0 in");
			frame.Left=LeftPosition.Parse("0 in");
			frame.Width=frameContainer.Width;
			Unit bottom=Unit.Zero;
			if(y1>y2){
				bottom=Unit.FromInch(y1/100);
			}
			else{
				bottom=Unit.FromInch(y2/100);
			}
			if(frameContainer.Height<bottom) {
				frameContainer.Height=bottom;
			}
			frame.Height=frameContainer.Height;
			frameContainer.Elements.Add(frame);
		}

		///<summary>Draws a small simple table at a fixed location within a frame container.  Not intended for tables with variable number of rows.</summary>
		public static Table DrawTable(TextFrame frameContainer,float xPos,float yPos,float height) {
			Table table=new Table();
			TextFrame frame=new TextFrame();
			frame.Elements.Add(table);
			frame.RelativeVertical=RelativeVertical.Page;
			frame.RelativeHorizontal=RelativeHorizontal.Page;
			frame.MarginLeft=Unit.FromInch(xPos/100);
			frame.MarginTop=Unit.FromInch(yPos/100);
			frame.Top=TopPosition.Parse("0 in");
			frame.Left=LeftPosition.Parse("0 in");
			frame.Width=frameContainer.Width;
			Unit bottom=Unit.FromInch((yPos+height)/100);
			if(frameContainer.Height<bottom) {
				frameContainer.Height=bottom;
			}
			frame.Height=frameContainer.Height;
			frameContainer.Elements.Add(frame);
			return table;
		}

		///<summary>Vertical spacer.</summary>
		public static void InsertSpacer(Section section,int space){
			TextFrame frame=section.AddTextFrame();
			frame.Height=Unit.FromInch((double)space/100);
		}

		public static void DrawGrid(Section section,ODGrid grid){
			//first, calculate width of dummy column that will push the grid over just enough to center it on the page.
			double pageW=0;
			if(CultureInfo.CurrentCulture.Name=="en-US") {
				pageW=850;
			}
			//don't know about Canada
			else {
				pageW=830;
			}
			//in 100ths/inch
			double widthAllColumns=(double)grid.WidthAllColumns/.96;
			double lmargin=section.Document.DefaultPageSetup.LeftMargin.Inch*100;
			double dummyColW=(pageW-widthAllColumns)/2-lmargin;
			Table table=new Table();
			Column col;
			col=table.AddColumn(Unit.FromInch(dummyColW/100));
			for(int i=0;i<grid.Columns.Count;i++){
				col=table.AddColumn(Unit.FromInch((double)grid.Columns[i].ColWidth/96));
				col.LeftPadding=Unit.FromInch(.01);
				col.RightPadding=Unit.FromInch(.01);
			}
			Row row;
			row=table.AddRow();
			row.HeadingFormat=true;
			row.TopPadding=Unit.FromInch(0);
			row.BottomPadding=Unit.FromInch(-.01);
			Cell cell;
			Paragraph par;
			//dummy column:
			cell=row.Cells[0];
			//cell.Shading.Color=Colors.LightGray;
			//format dummy cell?
			MigraDoc.DocumentObjectModel.Font fontHead=new MigraDoc.DocumentObjectModel.Font("Arial",Unit.FromPoint(8.5));
			fontHead.Bold=true;
			for(int i=0;i<grid.Columns.Count;i++){
				cell = row.Cells[i+1];
				par=cell.AddParagraph();
				par.AddFormattedText(grid.Columns[i].Heading,fontHead);
				par.Format.Alignment=ParagraphAlignment.Center;
				cell.Format.Alignment=ParagraphAlignment.Center;
				cell.Borders.Width=Unit.FromPoint(1);
				cell.Borders.Color=Colors.Black;
				cell.Shading.Color=Colors.LightGray;
			}
			MigraDoc.DocumentObjectModel.Font fontBody=null;//=new MigraDoc.DocumentObjectModel.Font("Arial",Unit.FromPoint(8.5));
			bool isBold;
			System.Drawing.Color color;
			int edgeRows=1;
			for(int i=0;i<grid.Rows.Count;i++,edgeRows++){
				row=table.AddRow();
				row.TopPadding=Unit.FromInch(.01);
				row.BottomPadding=Unit.FromInch(0);
				for(int j=0;j<grid.Columns.Count;j++){
					cell = row.Cells[j+1];
					par=cell.AddParagraph();
					if(grid.Rows[i].Cells[j].Bold==YN.Unknown){
						isBold=grid.Rows[i].Bold;
					}
					else if(grid.Rows[i].Cells[j].Bold==YN.Yes){
						isBold=true;
					}
					else{// if(grid.Rows[i].Cells[j].Bold==YN.No){
						isBold=false;
					}
					if(grid.Rows[i].Cells[j].ColorText==System.Drawing.Color.Empty){
						color=grid.Rows[i].ColorText;
					}
					else{
						color=grid.Rows[i].Cells[j].ColorText;
					}
					fontBody=CreateFont(8.5f,isBold,color);
					par.AddFormattedText(grid.Rows[i].Cells[j].Text,fontBody);
					if(grid.Columns[j].TextAlign==HorizontalAlignment.Center){
						cell.Format.Alignment=ParagraphAlignment.Center;
					}
					if(grid.Columns[j].TextAlign==HorizontalAlignment.Left) {
						cell.Format.Alignment=ParagraphAlignment.Left;
					}
					if(grid.Columns[j].TextAlign==HorizontalAlignment.Right) {
						cell.Format.Alignment=ParagraphAlignment.Right;
					}
					cell.Borders.Color=new MigraDoc.DocumentObjectModel.Color(180,180,180);
					if(grid.Rows[i].ColorLborder!=System.Drawing.Color.Empty){
						cell.Borders.Bottom.Color=ConvertColor(grid.Rows[i].ColorLborder);
					}
				}
				if(grid.Rows[i].Note!=null && grid.Rows[i].Note!="" && grid.NoteSpanStop>0 && grid.NoteSpanStart<grid.Columns.Count){
					row=table.AddRow();
					row.TopPadding=Unit.FromInch(.01);
					row.BottomPadding=Unit.FromInch(0);
					cell=row.Cells[grid.NoteSpanStart+1];
					par=cell.AddParagraph();
				  par.AddFormattedText(grid.Rows[i].Note,fontBody);
				  cell.Format.Alignment=ParagraphAlignment.Left;
					cell.Borders.Color=new MigraDoc.DocumentObjectModel.Color(180,180,180);
					cell.MergeRight=grid.Columns.Count-1-grid.NoteSpanStart;
					edgeRows++;
				}
			}
			table.SetEdge(1,0,grid.Columns.Count,edgeRows,Edge.Box,MigraDoc.DocumentObjectModel.BorderStyle.Single,1,Colors.Black);
			section.Add(table);
		}
	}
}
