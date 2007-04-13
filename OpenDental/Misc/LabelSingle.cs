using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	///<summary></summary>
	public class LabelSingle{
		private PrintDocument pd;
		private Patient Pat;
		private Carrier CarrierCur;

		///<summary></summary>
		public LabelSingle(){
			
		}

		///<summary></summary>
		public void PrintPat(Patient pat){
			Pat=pat.Copy();
			pd=new PrintDocument();
			pd.PrintPage+=new PrintPageEventHandler(pd_PrintPagePat);
			pd.DefaultPageSettings.Margins=new Margins(0,0,0,0);
			pd.OriginAtMargins=true;
			pd.DefaultPageSettings.Landscape=false;
			if(!Printers.SetPrinter(pd,PrintSituation.LabelSingle)){
				return;
			}
			try{
				pd.Print();
			}
			catch{
				MessageBox.Show(Lan.g("Label","Printer not available"));
			}
		}

		///<summary>Have to supply printer name because this can be called multiple times in a loop. Returns false if fails.</summary>
		public bool PrintIns(Carrier carrierCur,string printerName){
			CarrierCur=carrierCur;
			pd=new PrintDocument();
			pd.PrintPage+=new PrintPageEventHandler(pd_PrintPageIns);
			pd.DefaultPageSettings.Margins=new Margins(0,0,0,0);
			pd.OriginAtMargins=true;
			pd.DefaultPageSettings.Landscape=false;
			pd.PrinterSettings.PrinterName=printerName;
			try{
				pd.Print();
			}
			catch{
				MessageBox.Show(Lan.g("Label","Printer not available"));
				return false;
			}
			return true;
		}

		private void pd_PrintPagePat(object sender, System.Drawing.Printing.PrintPageEventArgs e) {
			float xPos=25;
			float yPos=10;//22;
			Graphics g=e.Graphics;
			g.TranslateTransform(100,0);
			g.RotateTransform(90);
			Font mainFont=new Font(FontFamily.GenericSansSerif,12);
			float lineH=e.Graphics.MeasureString("any",mainFont).Height;
			g.DrawString(Pat.GetNameFL(),mainFont,Brushes.Black,xPos,yPos);
			yPos+=lineH;
			g.DrawString(Pat.Address,mainFont,Brushes.Black,xPos,yPos);
			yPos+=lineH;
			if(Pat.Address2!=""){
				g.DrawString(Pat.Address2,mainFont,Brushes.Black,xPos,yPos);
				yPos+=lineH;
			}
			g.DrawString(Pat.City+", "+Pat.State+"  "+Pat.Zip
				,mainFont,Brushes.Black,xPos,yPos);
			//e.HasMorePages=false;
		}

		private void pd_PrintPageIns(object sender, System.Drawing.Printing.PrintPageEventArgs e) {
			float xPos=25;
			float yPos=10;
			Graphics g=e.Graphics;
			g.TranslateTransform(100,0);
			g.RotateTransform(90);
			Font mainFont=new Font(FontFamily.GenericSansSerif,12);
			float lineH=e.Graphics.MeasureString("any",mainFont).Height;
			g.DrawString(CarrierCur.CarrierName,mainFont,Brushes.Black,xPos,yPos);
			yPos+=lineH;
			g.DrawString(CarrierCur.Address,mainFont,Brushes.Black,xPos,yPos);
			yPos+=lineH;
			if(CarrierCur.Address2!=""){
				g.DrawString(CarrierCur.Address2,mainFont,Brushes.Black,xPos,yPos);
				yPos+=lineH;
			}
			g.DrawString(CarrierCur.City+", "+CarrierCur.State+"  "+CarrierCur.Zip
				,mainFont,Brushes.Black,xPos,yPos);
			//e.HasMorePages=false;
		}

	}

}
