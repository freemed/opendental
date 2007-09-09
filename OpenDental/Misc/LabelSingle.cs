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
		private Referral ReferralCur;

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
		public void PrintPatXRay(Patient pat) {
			Pat = pat.Copy();
			pd = new PrintDocument();
			pd.PrintPage += new PrintPageEventHandler(pd_PrintPageXRay);
			pd.DefaultPageSettings.Margins = new Margins(0, 0, 0, 0);
			pd.OriginAtMargins = true;
			pd.DefaultPageSettings.Landscape = false;
			if (!Printers.SetPrinter(pd, PrintSituation.LabelSingle)) {
				return;
			}
			try {
				pd.Print();
			}
			catch {
				MessageBox.Show(Lan.g("Label", "Printer not available"));
			}
		}

		public void PrintPatLF(Patient pat) {
			Pat = pat.Copy();
			pd = new PrintDocument();
			pd.PrintPage += new PrintPageEventHandler(pd_PrintPagePatLF);
			pd.DefaultPageSettings.Margins = new Margins(0, 0, 0, 0);
			pd.OriginAtMargins = true;
			pd.DefaultPageSettings.Landscape = false;
			if (!Printers.SetPrinter(pd, PrintSituation.LabelSingle)) {
				return;
			}
			try {
				pd.Print();
			}
			catch {
				MessageBox.Show(Lan.g("Label", "Printer not available"));
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

		///<summary></summary>
		public void PrintReferral(Referral referralCur) {
			ReferralCur=referralCur.Copy();
			pd=new PrintDocument();
			pd.PrintPage+=new PrintPageEventHandler(pd_PrintPageReferral);
			pd.DefaultPageSettings.Margins=new Margins(0,0,0,0);
			pd.OriginAtMargins=true;
			pd.DefaultPageSettings.Landscape=false;
			if(!Printers.SetPrinter(pd,PrintSituation.LabelSingle)) {
				return;
			}
			try {
				pd.Print();
			}
			catch {
				MessageBox.Show(Lan.g("Label","Printer not available"));
			}
		}

		private void pd_PrintPageXRay(object sender, System.Drawing.Printing.PrintPageEventArgs e) {
			float xPos = 25;
			float yPos = 50;
			Graphics g = e.Graphics;
			g.TranslateTransform(100, 0);
			g.RotateTransform(90);
			Font mainFont = new Font(FontFamily.GenericSansSerif, 14);
			Font smallFont = new Font(FontFamily.GenericSansSerif, 9);
			float lineH = e.Graphics.MeasureString("any", mainFont).Height;
			float smlineH = e.Graphics.MeasureString("any", smallFont).Height;
			g.DrawString(Pat.GetNameFL() + " - " + DateTime.Now.ToShortDateString(), mainFont, Brushes.Black, xPos, yPos);
			yPos += lineH;
			g.DrawString(Lan.g(this,"BDay:") + Pat.Birthdate.ToShortDateString() + Lan.g(this," - Dr. ") + Providers.GetLName(Pat.PriProv) + " - " + Providers.GetAbbr(Pat.PriProv), smallFont, Brushes.Black, xPos, yPos);
			yPos += smlineH;
			//e.HasMorePages=false;
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
		private void pd_PrintPagePatLF(object sender, System.Drawing.Printing.PrintPageEventArgs e) {
			float xPos = 25;
			float yPos = 10;//22;
			Graphics g = e.Graphics;
			g.TranslateTransform(100, 0);
			g.RotateTransform(90);
			Font mainFont = new Font(FontFamily.GenericSansSerif, 12);
			float lineH = e.Graphics.MeasureString("any", mainFont).Height;
			g.DrawString(Pat.GetNameLF(), mainFont, Brushes.Black, xPos, yPos);
			yPos += lineH;
			g.DrawString(Pat.Address, mainFont, Brushes.Black, xPos, yPos);
			yPos += lineH;
			if (Pat.Address2 != "") {
				g.DrawString(Pat.Address2, mainFont, Brushes.Black, xPos, yPos);
				yPos += lineH;
			}
			g.DrawString(Pat.City + ", " + Pat.State + "  " + Pat.Zip
				, mainFont, Brushes.Black, xPos, yPos);
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

		private void pd_PrintPageReferral(object sender,System.Drawing.Printing.PrintPageEventArgs e) {
			float xPos=25;
			float yPos=10;
			Graphics g=e.Graphics;
			g.TranslateTransform(100,0);
			g.RotateTransform(90);
			Font mainFont=new Font(FontFamily.GenericSansSerif,12);
			float lineH=e.Graphics.MeasureString("any",mainFont).Height;
			g.DrawString(Referrals.GetNameFL(ReferralCur.ReferralNum),mainFont,Brushes.Black,xPos,yPos);
			yPos+=lineH;
			g.DrawString(ReferralCur.Address,mainFont,Brushes.Black,xPos,yPos);
			yPos+=lineH;
			if(ReferralCur.Address2!="") {
				g.DrawString(ReferralCur.Address2,mainFont,Brushes.Black,xPos,yPos);
				yPos+=lineH;
			}
			g.DrawString(ReferralCur.City+", "+ReferralCur.ST+"  "+ReferralCur.Zip
				,mainFont,Brushes.Black,xPos,yPos);
			//e.HasMorePages=false;
		}

	}

}
