using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows;

namespace OpenDental {
	public partial class FormPrintReport:Form {

		///<summary>If pageNumberFont is set, then the page number is displayed using the page number information.</summary>
		private Font pageNumberFont=null;
		private float pageNumberFontSize=0;
		private PointF pageNumberLocation;
		private int totalPages=0;
		private int curPage=0;
		///Is set to a non-null value only during printing to a physical printer.
		private Graphics printerGraph=null;
		private Rectangle printerMargins;
		public int pageHeight=900;
		private int curPrintPage=0;
		public delegate void PrintCallback(FormPrintReport fpr);
		public PrintCallback printGenerator=null;

		public FormPrintReport() {
			InitializeComponent();
		}

		private void FormPrintReport_Load(object sender,EventArgs e) {
			PrintCustom();
		}

		public void UsePageNumbers(Font font,float fontSize,PointF location){
			pageNumberFont=font;
			pageNumberFontSize=fontSize;
			pageNumberLocation=location;
		}

		private void PrintCustom(){
			if(printGenerator==null){
				return;
			}
			Invoke(printGenerator,new object[] { this });//Call the custom printing code.
		}

		private void CalculatePageOffset(){
			printPanel.printOrigin=new Point(0,-((curPage*pageHeight)/totalPages));
			labPageNum.Text="Page: "+(curPage+1)+"\\"+totalPages;
		}

		public Graphics Graph{
			get{ return (printerGraph!=null)?printerGraph:printPanel.backBuffer;	}
		}

		///<summary>Must be set by the external printing algorithm in order to get page numbers working properly.</summary>
		public int TotalPages{
			get{ return totalPages; }
			set{ totalPages=value; CalculatePageOffset(); labPageNum.Visible=(totalPages>0); }
		}

		public int GraphWidth{
			get{ return (printerGraph!=null)?printerMargins.Width:printPanel.Width; }
			set{ printPanel.Width=value; }
		}

		public int GraphHeight{
			get{ return (printerGraph!=null)?printerMargins.Height:printPanel.Height; }
			set { printPanel.Height=value; }
		}

		private void butNextPage_Click(object sender,EventArgs e) {
			if(totalPages<1 || curPage>=totalPages-1){
				return;
			}
			curPage++;
			CalculatePageOffset();
			PrintCustom();
		}

		private void butPreviousPage_Click(object sender,EventArgs e) {
			if(totalPages<1 || curPage<=0){
				return;
			}
			curPage--;
			CalculatePageOffset();
			PrintCustom();
		}

		private void butPrint_Click(object sender,EventArgs e) {
			curPrintPage=0;
			pd1.OriginAtMargins=true;
			pd1.Print();
			printerGraph=null;
		}

		private void pd1_PrintPage(object sender,System.Drawing.Printing.PrintPageEventArgs e) {
			printerGraph=e.Graphics;
			printerMargins=e.MarginBounds;
			PrintCustom();
			curPrintPage++;
			e.HasMorePages=(printGenerator!=null)&&(curPrintPage<totalPages-1);
		}

	}
}