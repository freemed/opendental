using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.IO;
using System.Text;
using OpenDentBusiness;
using CodeBase;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;

namespace OpenDental {
	public class SheetPrinting {
		///<summary>If there is only one sheet, then this will stay 0.</Summary>
		private static int sheetsPrinted;
		///<summary>If not a batch, then there will just be one sheet in the list.</summary>
		private static List<Sheet> SheetList;

		///<summary>Surround with try/catch.</summary>
		public static void PrintBatch(List<Sheet> sheetBatch){
			//currently no validation for parameters in a batch because of the way it was created.
			//could validate field names here later.
			SheetList=sheetBatch;
			sheetsPrinted=0;
			PrintDocument pd=new PrintDocument();
			pd.OriginAtMargins=true;
			pd.PrintPage+=new PrintPageEventHandler(pd_PrintPage);
			if(sheetBatch[0].Width>0 && sheetBatch[0].Height>0){
				pd.DefaultPageSettings.PaperSize=new PaperSize("Default",sheetBatch[0].Width,sheetBatch[0].Height);
			}
			PrintSituation sit=PrintSituation.Default;
			pd.DefaultPageSettings.Landscape=sheetBatch[0].IsLandscape;
			switch(sheetBatch[0].SheetType){
				case SheetTypeEnum.LabelPatient:
				case SheetTypeEnum.LabelCarrier:
				case SheetTypeEnum.LabelReferral:
					sit=PrintSituation.LabelSingle;
					break;
				case SheetTypeEnum.ReferralSlip:
					sit=PrintSituation.Default;
					break;
			}
			//later: add a check here for print preview.
			#if DEBUG
				pd.DefaultPageSettings.Margins=new Margins(20,20,0,0);
				UI.PrintPreview printPreview=new UI.PrintPreview(sit,pd,SheetList.Count);
				printPreview.ShowDialog();
			#else
				try {
					if(!Printers.SetPrinter(pd,sit)) {
						return;
					}
					pd.DefaultPageSettings.Margins=new Margins(0,0,0,0);
					pd.Print();
				}
				catch(Exception ex){
					throw ex;
					//MessageBox.Show(Lan.g("Sheet","Printer not available"));
				}
			#endif
		}

		public static void PrintRx(Sheet sheet,bool isControlled){
			Print(sheet,1,isControlled);
		}

		///<Summary>Surround with try/catch.</Summary>
		public static void Print(Sheet sheet){
			Print(sheet,1,false);
		}

		///<Summary>Surround with try/catch.</Summary>
		public static void Print(Sheet sheet,int copies){
			Print(sheet,copies,false);
		}

		///<Summary></Summary>
		public static void Print(Sheet sheet,int copies,bool isRxControlled){
			//parameter null check moved to SheetFiller.
			//could validate field names here later.
			SheetList=new List<Sheet>();
			for(int i=0;i<copies;i++){
				SheetList.Add(sheet.Copy());
			}
			sheetsPrinted=0;
			PrintDocument pd=new PrintDocument();
			pd.OriginAtMargins=true;
			pd.PrintPage+=new PrintPageEventHandler(pd_PrintPage);
			if(sheet.Width>0 && sheet.Height>0){
				pd.DefaultPageSettings.PaperSize=new PaperSize("Default",sheet.Width,sheet.Height);
			}
			PrintSituation sit=PrintSituation.Default;
			pd.DefaultPageSettings.Landscape=sheet.IsLandscape;
			switch(sheet.SheetType){
				case SheetTypeEnum.LabelPatient:
				case SheetTypeEnum.LabelCarrier:
				case SheetTypeEnum.LabelReferral:
					sit=PrintSituation.LabelSingle;
					break;
				case SheetTypeEnum.ReferralSlip:
					sit=PrintSituation.Default;
					break;
				case SheetTypeEnum.Rx:
					if(isRxControlled){
						sit=PrintSituation.RxControlled;
					}
					else{
						sit=PrintSituation.Rx;
					}
					break;
			}
			//later: add a check here for print preview.
			#if DEBUG
				pd.DefaultPageSettings.Margins=new Margins(20,20,0,0);
				UI.PrintPreview printPreview=new UI.PrintPreview(sit,pd,SheetList.Count);
				printPreview.ShowDialog();
			#else
				try {
					if(!Printers.SetPrinter(pd,sit)) {
						return;
					}
					pd.DefaultPageSettings.Margins=new Margins(0,0,0,0);
					pd.Print();
				}
				catch(Exception ex){
					throw ex;
					//MessageBox.Show(Lan.g("Sheet","Printer not available"));
				}
			#endif
		}

		private static void pd_PrintPage(object sender,System.Drawing.Printing.PrintPageEventArgs e) {
			Graphics g=e.Graphics;
			g.SmoothingMode=SmoothingMode.HighQuality;
			Sheet sheet=SheetList[sheetsPrinted];
			SheetUtil.CalculateHeights(sheet,g);//this is here because of easy access to g.
			Font font;
			FontStyle fontstyle;
			//first, draw images------------------------------------------------------------------------------------
			foreach(SheetField field in sheet.SheetFields){
				if(field.FieldType!=SheetFieldType.Image){
					continue;
				}
				string filePathAndName=ODFileUtils.CombinePaths(SheetUtil.GetImagePath(),field.FieldName);
				if(!File.Exists(filePathAndName)){
					continue;
				}
				Image img=Image.FromFile(filePathAndName);
				g.DrawImage(img,field.XPos,field.YPos,field.Width,field.Height);
			}
			//then, drawings--------------------------------------------------------------------------------------------
			Pen pen=new Pen(Brushes.Black,2f);
			string[] pointStr;
			List<Point> points;
			Point point;
			string[] xy;
			foreach(SheetField field in sheet.SheetFields){
				if(field.FieldType!=SheetFieldType.Drawing){
					continue;
				}
				pointStr=field.FieldValue.Split(';');
				points=new List<Point>();
				for(int p=0;p<pointStr.Length;p++){
					xy=pointStr[p].Split(',');
					if(xy.Length==2){
						point=new Point(PIn.PInt(xy[0]),PIn.PInt(xy[1]));
						points.Add(point);
					}
				}
				for(int i=1;i<points.Count;i++){
					g.DrawLine(pen,points[i-1].X,points[i-1].Y,points[i].X,points[i].Y);
				}
			}
			//then, rectangles and lines----------------------------------------------------------------------------------
			Pen pen2=new Pen(Brushes.Black,1f);
			foreach(SheetField field in sheet.SheetFields){
				if(field.FieldType==SheetFieldType.Rectangle){
					g.DrawRectangle(pen2,field.XPos,field.YPos,field.Width,field.Height);
				}
				if(field.FieldType==SheetFieldType.Line){
					g.DrawLine(pen2,field.XPos,field.YPos,
						field.XPos+field.Width,
						field.YPos+field.Height);
				}
			}
			//then, draw text-----------------------------------------------------------------------------------------------
			foreach(SheetField field in sheet.SheetFields){
				if(field.FieldType!=SheetFieldType.InputField
					&& field.FieldType!=SheetFieldType.OutputText
					&& field.FieldType!=SheetFieldType.StaticText)
				{
					continue;
				}
				fontstyle=FontStyle.Regular;
				if(field.FontIsBold){
					fontstyle=FontStyle.Bold;
				}
				font=new Font(field.FontName,field.FontSize,fontstyle);
				g.DrawString(field.FieldValue,font,Brushes.Black,field.BoundsF);
			}
			g.Dispose();
			//no logic yet for multiple pages on one sheet.
			sheetsPrinted++;
			//heightsCalculated=false;
			if(sheetsPrinted<SheetList.Count){
				e.HasMorePages=true;
			}
			else{
				e.HasMorePages=false;
				sheetsPrinted=0;
			}	
		}

		public static void CreatePdf(Sheet sheet,string fullFileName){
			PdfDocument document=new PdfDocument();
			//document.PageLayout=new PdfPageLayout()
			PdfPage page=document.AddPage();
			XGraphics g=XGraphics.FromPdfPage(page);
			g.SmoothingMode=XSmoothingMode.HighQuality;
			//g.PageUnit=XGraphicsUnit. //wish they had pixel
			XTextFormatter tf = new XTextFormatter(g);//needed for text wrap
			//tf.Alignment=XParagraphAlignment.Left;
			if(sheet.IsLandscape){
				page.Orientation=PageOrientation.Landscape;
			}
			page.Width=p(sheet.Width);//XUnit.FromInch((double)sheet.Width/100);  //new XUnit((double)sheet.Width/100,XGraphicsUnit.Inch);
			page.Height=p(sheet.Height);//new XUnit((double)sheet.Height/100,XGraphicsUnit.Inch);
			//pd.DefaultPageSettings.Landscape=
			//already done?:SheetUtil.CalculateHeights(sheet,g);//this is here because of easy access to g.
			XFont font;
			XFontStyle fontstyle;
			//first, draw images--------------------------------------------------------------------------------------
			foreach(SheetField field in sheet.SheetFields){
				if(field.FieldType!=SheetFieldType.Image){
					continue;
				}
				string filePathAndName=ODFileUtils.CombinePaths(SheetUtil.GetImagePath(),field.FieldName);
				if(!File.Exists(filePathAndName)){
					continue;
				}
				XImage img=XImage.FromFile(filePathAndName);
				g.DrawImage(img,p(field.XPos),p(field.YPos),p(field.Width),p(field.Height));
			}
			//then, drawings--------------------------------------------------------------------------------------------
			XPen pen=new XPen(XColors.Black,p(2));
			string[] pointStr;
			List<Point> points;
			Point point;
			string[] xy;
			foreach(SheetField field in sheet.SheetFields){
				if(field.FieldType!=SheetFieldType.Drawing){
					continue;
				}
				pointStr=field.FieldValue.Split(';');
				points=new List<Point>();
				for(int j=0;j<pointStr.Length;j++){
					xy=pointStr[j].Split(',');
					if(xy.Length==2){
						point=new Point(PIn.PInt(xy[0]),PIn.PInt(xy[1]));
						points.Add(point);
					}
				}
				for(int i=1;i<points.Count;i++){
					g.DrawLine(pen,p(points[i-1].X),p(points[i-1].Y),p(points[i].X),p(points[i].Y));
				}
			}
			//then, rectangles and lines----------------------------------------------------------------------------------
			XPen pen2=new XPen(XColors.Black,p(1));
			foreach(SheetField field in sheet.SheetFields){
				if(field.FieldType==SheetFieldType.Rectangle){
					g.DrawRectangle(pen2,p(field.XPos),p(field.YPos),p(field.Width),p(field.Height));
				}
				if(field.FieldType==SheetFieldType.Line){
					g.DrawLine(pen2,p(field.XPos),p(field.YPos),
						p(field.XPos+field.Width),
						p(field.YPos+field.Height));
				}
			}
			//then, draw text--------------------------------------------------------------------------------------------
			foreach(SheetField field in sheet.SheetFields){
				if(field.FieldType!=SheetFieldType.InputField
					&& field.FieldType!=SheetFieldType.OutputText
					&& field.FieldType!=SheetFieldType.StaticText)
				{
					continue;
				}
				fontstyle=XFontStyle.Regular;
				if(field.FontIsBold){
					fontstyle=XFontStyle.Bold;
				}
				//Font font2=new Font(field.FontName,8,GraphicsUnit.
				font=new XFont(field.FontName,field.FontSize,fontstyle);
				XRect xrect=new XRect(p(field.XPos),p(field.YPos),p(field.Width),p(field.Height));
				//XStringFormat format=new XStringFormat();
				tf.DrawString(field.FieldValue,font,XBrushes.Black,xrect,XStringFormats.TopLeft);
			}
			document.Save(fullFileName);
		}

		/*//<summary>Converts pixels used by us to points used by PdfSharp.</summary>
		private double P(double pixels){
			return (double)pixels/100;
		}*/

		///<summary>Converts pixels used by us to points used by PdfSharp.</summary>
		private static double p(int pixels){
			XUnit xunit=XUnit.FromInch((double)pixels/100);//100 ppi
			return xunit.Point;
				//XUnit.FromInch((double)pixels/100);
		}

	}
}
