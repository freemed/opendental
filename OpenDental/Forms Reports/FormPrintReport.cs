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
		///Is set to a non-null value only during printing to a physical printer.
		private Graphics printerGraph=null;
		private Rectangle printerMargins;
		private int pageHeight=900;
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
			printPanel.Clear();
			Invoke(printGenerator,new object[] { this });//Call the custom printing code.
		}

		private int CurPage(){
			return (vScroll.Value-vScroll.Minimum+1)/pageHeight;
		}

		private void CalculatePageOffset(){
			printPanel.Origin=new Point(0,-vScroll.Value);
			labPageNum.Text="Page: "+(CurPage()+1)+"\\"+totalPages;
		}

		private void CalculateVScrollMax(){
			if(totalPages>1){
				vScroll.Maximum=pageHeight*(totalPages-1)-1+vScroll.Minimum;
			}else{
				vScroll.Maximum=vScroll.Minimum;
			}
		}

		private void MoveScrollBar(int amount){
			int val=vScroll.Value+amount;
			if(val<vScroll.Minimum){
				val=vScroll.Minimum;
			}else if(val>vScroll.Maximum){
				val=vScroll.Maximum;
			}
			vScroll.Value=val;
		}

		public int ScrollAmount{
			get{ return vScroll.SmallChange; }
			set{ vScroll.SmallChange=value; }
		}

		public Graphics Graph{
			get{ return (printerGraph!=null)?printerGraph:printPanel.backBuffer; }
		}

		public int PageHeight {
			get { return pageHeight; }
			set { pageHeight=value; CalculateVScrollMax(); }
		}

		///<summary>Must be set by the external printing algorithm in order to get page numbers working properly.</summary>
		public int TotalPages{
			get{ return totalPages; }
			set { totalPages=value; CalculatePageOffset(); labPageNum.Visible=(totalPages>0); CalculateVScrollMax(); }
		}

		public int GraphWidth{
			get{ return (printerGraph!=null)?printerMargins.Width:printPanel.Width; }
			set{ printPanel.Width=value; }
		}

		public int GraphHeight{
			get{ return (printerGraph!=null)?printerMargins.Height:printPanel.Height; }
			set { printPanel.Height=value; }
		}

		private void Display(){
			CalculatePageOffset();
			PrintCustom();
			printPanel.Invalidate(true);
		}

		private void butNextPage_Click(object sender,EventArgs e) {
			MoveScrollBar((CurPage()+1)*pageHeight-vScroll.Value);
			Display();
		}

		private void butPreviousPage_Click(object sender,EventArgs e) {
			if(vScroll.Value%pageHeight==0){
				MoveScrollBar(-pageHeight);
			}else{
				MoveScrollBar(CurPage()*pageHeight-vScroll.Value);
			}
			Display();
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

		private void vScroll_Scroll(object sender,ScrollEventArgs e) {
			Display();
		}

	}
}