using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Text;
using OpenDentBusiness;

namespace OpenDental {
	public class SheetPrinting {
		//<Summary>For the current sheet in the batch.  Reset for each new sheet in a batch.</Summary>
		//private static bool heightsCalculated;
		///<summary>If there is only one sheet, then this will stay 0.</Summary>
		private static int sheetsPrinted;
		///<summary>If not a batch, then there will just be one sheet in the list.</summary>
		private static List<Sheet> SheetList;

		///<summary>Surround with try/catch.</summary>
		public static void PrintBatch(List<Sheet> sheetBatch){
			//currently no validation for parameters in a batch because of the way it was created.
			//could validate field names here later.
			SheetList=sheetBatch;
			//heightsCalculated=false;
			sheetsPrinted=0;
			PrintDocument pd=new PrintDocument();
			pd.OriginAtMargins=true;
			pd.PrintPage+=new PrintPageEventHandler(pd_PrintPage);
			if(sheetBatch[0].Width>0 && sheetBatch[0].Height>0){
				pd.DefaultPageSettings.PaperSize=new PaperSize("Default",sheetBatch[0].Width,sheetBatch[0].Height);
			}
			PrintSituation sit=PrintSituation.Default;
			switch(sheetBatch[0].SheetType){
				case SheetTypeEnum.LabelPatient:
				case SheetTypeEnum.LabelCarrier:
				case SheetTypeEnum.LabelReferral:
					pd.DefaultPageSettings.Landscape=false;
					sit=PrintSituation.LabelSingle;
					break;
				case SheetTypeEnum.ReferralSlip:
					pd.DefaultPageSettings.Landscape=false;
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
			foreach(SheetParameter param in sheet.Parameters){
				if(param.IsRequired && param.ParamValue==null){
					throw new ApplicationException(Lan.g("Sheet","Parameter not specified for sheet: ")+param.ParamName);
				}
			}
			//could validate field names here later.
			SheetList=new List<Sheet>();
			SheetList.Add(sheet);
			//heightsCalculated=false;
			sheetsPrinted=0;
			PrintDocument pd=new PrintDocument();
			pd.OriginAtMargins=true;
			pd.PrintPage+=new PrintPageEventHandler(pd_PrintPage);
			if(sheet.Width>0 && sheet.Height>0){
				pd.DefaultPageSettings.PaperSize=new PaperSize("Default",sheet.Width,sheet.Height);
			}
			PrintSituation sit=PrintSituation.Default;
			switch(sheet.SheetType){
				case SheetTypeEnum.LabelPatient:
				case SheetTypeEnum.LabelCarrier:
				case SheetTypeEnum.LabelReferral:
					pd.DefaultPageSettings.Landscape=false;//prevents a bug.
					sit=PrintSituation.LabelSingle;
					break;
				case SheetTypeEnum.ReferralSlip:
					pd.DefaultPageSettings.Landscape=false;
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
			if(sheet.SheetType==SheetTypeEnum.LabelCarrier
				|| sheet.SheetType==SheetTypeEnum.LabelPatient
				|| sheet.SheetType==SheetTypeEnum.LabelReferral)
			{
				g.TranslateTransform(100,0);
				g.RotateTransform(90);
			}
			foreach(SheetField field in sheet.SheetFields){
				g.DrawString(field.FieldValue,field.Font,Brushes.Black,field.BoundsF);
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



	}
}
