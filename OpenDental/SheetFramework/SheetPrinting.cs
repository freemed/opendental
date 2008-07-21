using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Text;
using OpenDentBusiness;
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
					//pd.DefaultPageSettings.Landscape=true;
					sit=PrintSituation.LabelSingle;
					break;
				case SheetTypeEnum.ReferralSlip:
					//pd.DefaultPageSettings.Landscape=false;
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

		///<Summary>Surround with try/catch.</Summary>
		public static void Print(Sheet sheet){
			Print(sheet,1);
		}

		///<Summary></Summary>
		public static void Print(Sheet sheet,int copies){
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
					//pd.DefaultPageSettings.Landscape=true;
					sit=PrintSituation.LabelSingle;
					break;
				case SheetTypeEnum.ReferralSlip:
					//pd.DefaultPageSettings.Landscape=false;
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

		private static void pd_PrintPage(object sender,System.Drawing.Printing.PrintPageEventArgs e) {
			Graphics g=e.Graphics;
			Sheet sheet=SheetList[sheetsPrinted];
			SheetUtil.CalculateHeights(sheet,g);//this is here because of easy access to g.
			Font font;
			FontStyle fontstyle;
			foreach(SheetField field in sheet.SheetFields){
				if(field.FieldType==SheetFieldType.Parameter){
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
			PdfPage page=document.AddPage();
			XGraphics g=XGraphics.FromPdfPage(page);
			XTextFormatter tf = new XTextFormatter(g);//needed for text wrap
			if(sheet.IsLandscape){
				page.Orientation=PageOrientation.Landscape;
			}
			page.Width=sheet.Width;
			page.Height=sheet.Height;
			//pd.DefaultPageSettings.Landscape=
			//already done?:SheetUtil.CalculateHeights(sheet,g);//this is here because of easy access to g.
			XFont font;
			XFontStyle fontstyle;
			//XStringFormat stringformat=new XStringFormat();
			foreach(SheetField field in sheet.SheetFields){
				if(field.FieldType==SheetFieldType.Parameter){
					continue;
				}
				fontstyle=XFontStyle.Regular;
				if(field.FontIsBold){
					fontstyle=XFontStyle.Bold;
				}
				font=new XFont(field.FontName,field.FontSize,fontstyle);
				tf.DrawString(field.FieldValue,font,XBrushes.Black,field.BoundsF);
			}
			document.Save(fullFileName);
		}



	}
}
