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
		private static List<SheetDef> SheetDefList;

		///<summary>Surround with try/catch.</summary>
		public static void PrintBatch(List<SheetDef> sheetDefBatch){
			//currently no validation for parameters in a batch because of the way it was created.
			//could validate field names here later.
			SheetDefList=sheetDefBatch;
			//heightsCalculated=false;
			sheetsPrinted=0;
			PrintDocument pd=new PrintDocument();
			pd.OriginAtMargins=true;
			pd.PrintPage+=new PrintPageEventHandler(pd_PrintPage);
			if(sheetDefBatch[0].Width>0 && sheetDefBatch[0].Height>0){
				pd.DefaultPageSettings.PaperSize=new PaperSize("Default",sheetDefBatch[0].Width,sheetDefBatch[0].Height);
			}
			PrintSituation sit=PrintSituation.Default;
			switch(sheetDefBatch[0].SheetType){
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
				UI.PrintPreview printPreview=new UI.PrintPreview(sit,pd,SheetDefList.Count);
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
		public static void Print(SheetDef sheetDef){
			foreach(SheetParameter param in sheetDef.Parameters){
				if(param.IsRequired && param.ParamValue==null){
					throw new ApplicationException(Lan.g("Sheet","Parameter not specified for sheet: ")+param.ParamName);
				}
			}
			//could validate field names here later.
			SheetDefList=new List<SheetDef>();
			SheetDefList.Add(sheetDef);
			//heightsCalculated=false;
			sheetsPrinted=0;
			PrintDocument pd=new PrintDocument();
			pd.OriginAtMargins=true;
			pd.PrintPage+=new PrintPageEventHandler(pd_PrintPage);
			if(sheetDef.Width>0 && sheetDef.Height>0){
				pd.DefaultPageSettings.PaperSize=new PaperSize("Default",sheetDef.Width,sheetDef.Height);
			}
			PrintSituation sit=PrintSituation.Default;
			switch(sheetDef.SheetType){
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
				UI.PrintPreview printPreview=new UI.PrintPreview(sit,pd,SheetDefList.Count);
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
			SheetDef sheetDef=SheetDefList[sheetsPrinted];
			SheetUtil.CalculateHeights(sheetDef,g);//this is here because of easy access to g.
			if(sheetDef.SheetType==SheetTypeEnum.LabelCarrier
				|| sheetDef.SheetType==SheetTypeEnum.LabelPatient
				|| sheetDef.SheetType==SheetTypeEnum.LabelReferral)
			{
				g.TranslateTransform(100,0);
				g.RotateTransform(90);
			}
			foreach(SheetFieldDef fieldDef in sheetDef.SheetFieldDefs){
				g.DrawString(fieldDef.FieldValue,fieldDef.Font,Brushes.Black,fieldDef.BoundsF);
			}
			g.Dispose();
			//no logic yet for multiple pages on one sheet.
			sheetsPrinted++;
			//heightsCalculated=false;
			if(sheetsPrinted<SheetDefList.Count){
				e.HasMorePages=true;
			}
			else{
				e.HasMorePages=false;
				sheetsPrinted=0;
			}	
		}



	}
}
