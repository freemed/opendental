using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace TestToothChart {
	public partial class FormPerioTest:Form {
		public FormPerioTest() {
			InitializeComponent();
			toothChart.DrawMode=DrawingMode.DirectX;
		}

		private void FormPerioTest_Load(object sender,EventArgs e) {
			toothChart.ColorBackground=Color.White;
			toothChart.ColorText=Color.Black;
			toothChart.PerioMode=true;
			toothChart.SetMissing("1");
			toothChart.SetMissing("2");
			toothChart.SetMissing("13");
			toothChart.SetMissing("14");
			toothChart.SetMissing("17");
			toothChart.SetMissing("25");
			toothChart.SetMissing("26");
			toothChart.SetImplant("14",Color.Gray);
			toothChart.MoveTooth("4",0,0,0,0,-5,0);
			toothChart.MoveTooth("16",0,20,0,-3,0,0);
			toothChart.MoveTooth("24",15,2,0,0,0,0);
			
		}

		private void butPrint_Click(object sender,EventArgs e) {
			PrintDocument pd2=new PrintDocument();
			pd2.PrintPage+=new PrintPageEventHandler(this.pd2_PrintPage);
			pd2.OriginAtMargins=true;
			pd2.DefaultPageSettings.Margins=new Margins(0,0,0,0);
			pd2.Print();
		}

		private void pd2_PrintPage(object sender,PrintPageEventArgs ev) {//raised for each page to be printed.
			Graphics g=ev.Graphics;
			Bitmap bitmap=toothChart.GetBitmap();
			g.DrawImage(bitmap,75,75,bitmap.Width,bitmap.Height);
		}
	}
}
