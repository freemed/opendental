using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SparksToothChart;

namespace TestToothChart {
	public partial class Form1:Form {
		public Form1() {
			InitializeComponent();
			toothChart2D.DrawMode=DrawingMode.Simple2D;
			toothChartOpenGL.DrawMode=DrawingMode.OpenGL;
			toothChartDirectX.DrawMode=DrawingMode.DirectX;
		}

		private void Form1_Load(object sender,EventArgs e) {

		}

		private void butReset_Click(object sender,EventArgs e) {
			toothChart2D.ResetTeeth();
			toothChartOpenGL.ResetTeeth();
			toothChartDirectX.ResetTeeth();
		}

		private void butAllPrimary_Click(object sender,EventArgs e) {
			toothChart2D.ResetTeeth();
			toothChartOpenGL.ResetTeeth();
			toothChartDirectX.ResetTeeth();
			//
			toothChart2D.SetPrimary("1");
			toothChart2D.SetPrimary("2");
			toothChart2D.SetPrimary("3");
			toothChart2D.SetPrimary("4");
			toothChart2D.SetPrimary("5");
			toothChart2D.SetPrimary("6");
			toothChart2D.SetPrimary("7");
			toothChart2D.SetPrimary("8");
			toothChart2D.SetPrimary("9");
			toothChart2D.SetPrimary("10");
			toothChart2D.SetPrimary("11");
			toothChart2D.SetPrimary("12");
			toothChart2D.SetPrimary("13");
			toothChart2D.SetPrimary("14");
			toothChart2D.SetPrimary("15");
			toothChart2D.SetPrimary("16");
			toothChart2D.SetPrimary("17");
			toothChart2D.SetPrimary("18");
			toothChart2D.SetPrimary("19");
			toothChart2D.SetPrimary("20");
			toothChart2D.SetPrimary("21");
			toothChart2D.SetPrimary("22");
			toothChart2D.SetPrimary("23");
			toothChart2D.SetPrimary("24");
			toothChart2D.SetPrimary("25");
			toothChart2D.SetPrimary("26");
			toothChart2D.SetPrimary("27");
			toothChart2D.SetPrimary("28");
			toothChart2D.SetPrimary("29");
			toothChart2D.SetPrimary("30");
			toothChart2D.SetPrimary("31");
			toothChart2D.SetPrimary("32");
			//
			toothChartOpenGL.SetPrimary("1");
			toothChartOpenGL.SetPrimary("2");
			toothChartOpenGL.SetPrimary("3");
			toothChartOpenGL.SetPrimary("4");
			toothChartOpenGL.SetPrimary("5");
			toothChartOpenGL.SetPrimary("6");
			toothChartOpenGL.SetPrimary("7");
			toothChartOpenGL.SetPrimary("8");
			toothChartOpenGL.SetPrimary("9");
			toothChartOpenGL.SetPrimary("10");
			toothChartOpenGL.SetPrimary("11");
			toothChartOpenGL.SetPrimary("12");
			toothChartOpenGL.SetPrimary("13");
			toothChartOpenGL.SetPrimary("14");
			toothChartOpenGL.SetPrimary("15");
			toothChartOpenGL.SetPrimary("16");
			toothChartOpenGL.SetPrimary("17");
			toothChartOpenGL.SetPrimary("18");
			toothChartOpenGL.SetPrimary("19");
			toothChartOpenGL.SetPrimary("20");
			toothChartOpenGL.SetPrimary("21");
			toothChartOpenGL.SetPrimary("22");
			toothChartOpenGL.SetPrimary("23");
			toothChartOpenGL.SetPrimary("24");
			toothChartOpenGL.SetPrimary("25");
			toothChartOpenGL.SetPrimary("26");
			toothChartOpenGL.SetPrimary("27");
			toothChartOpenGL.SetPrimary("28");
			toothChartOpenGL.SetPrimary("29");
			toothChartOpenGL.SetPrimary("30");
			toothChartOpenGL.SetPrimary("31");
			toothChartOpenGL.SetPrimary("32");
			//
			toothChartDirectX.SetPrimary("1");
			toothChartDirectX.SetPrimary("2");
			toothChartDirectX.SetPrimary("3");
			toothChartDirectX.SetPrimary("4");
			toothChartDirectX.SetPrimary("5");
			toothChartDirectX.SetPrimary("6");
			toothChartDirectX.SetPrimary("7");
			toothChartDirectX.SetPrimary("8");
			toothChartDirectX.SetPrimary("9");
			toothChartDirectX.SetPrimary("10");
			toothChartDirectX.SetPrimary("11");
			toothChartDirectX.SetPrimary("12");
			toothChartDirectX.SetPrimary("13");
			toothChartDirectX.SetPrimary("14");
			toothChartDirectX.SetPrimary("15");
			toothChartDirectX.SetPrimary("16");
			toothChartDirectX.SetPrimary("17");
			toothChartDirectX.SetPrimary("18");
			toothChartDirectX.SetPrimary("19");
			toothChartDirectX.SetPrimary("20");
			toothChartDirectX.SetPrimary("21");
			toothChartDirectX.SetPrimary("22");
			toothChartDirectX.SetPrimary("23");
			toothChartDirectX.SetPrimary("24");
			toothChartDirectX.SetPrimary("25");
			toothChartDirectX.SetPrimary("26");
			toothChartDirectX.SetPrimary("27");
			toothChartDirectX.SetPrimary("28");
			toothChartDirectX.SetPrimary("29");
			toothChartDirectX.SetPrimary("30");
			toothChartDirectX.SetPrimary("31");
			toothChartDirectX.SetPrimary("32");
		}

		private void butMixed_Click(object sender,EventArgs e) {
			toothChart2D.ResetTeeth();
			toothChartOpenGL.ResetTeeth();
			toothChartDirectX.ResetTeeth();
			//
			toothChart2D.SetPrimary("1");
			toothChart2D.SetPrimary("2");
			toothChart2D.SetPrimary("4");
			toothChart2D.SetPrimary("5");
			toothChart2D.SetPrimary("6");
			toothChart2D.SetPrimary("11");
			toothChart2D.SetPrimary("12");
			toothChart2D.SetPrimary("13");
			toothChart2D.SetPrimary("15");
			toothChart2D.SetPrimary("16");
			toothChart2D.SetPrimary("17");
			toothChart2D.SetPrimary("18");
			toothChart2D.SetPrimary("20");
			toothChart2D.SetPrimary("21");
			toothChart2D.SetPrimary("22");
			toothChart2D.SetPrimary("27");
			toothChart2D.SetPrimary("28");
			toothChart2D.SetPrimary("29");
			toothChart2D.SetPrimary("31");
			toothChart2D.SetPrimary("32");
			//
			toothChartOpenGL.SetPrimary("1");
			toothChartOpenGL.SetPrimary("2");
			toothChartOpenGL.SetPrimary("4");
			toothChartOpenGL.SetPrimary("5");
			toothChartOpenGL.SetPrimary("6");
			toothChartOpenGL.SetPrimary("11");
			toothChartOpenGL.SetPrimary("12");
			toothChartOpenGL.SetPrimary("13");
			toothChartOpenGL.SetPrimary("15");
			toothChartOpenGL.SetPrimary("16");
			toothChartOpenGL.SetPrimary("17");
			toothChartOpenGL.SetPrimary("18");
			toothChartOpenGL.SetPrimary("20");
			toothChartOpenGL.SetPrimary("21");
			toothChartOpenGL.SetPrimary("22");
			toothChartOpenGL.SetPrimary("27");
			toothChartOpenGL.SetPrimary("28");
			toothChartOpenGL.SetPrimary("29");
			toothChartOpenGL.SetPrimary("31");
			toothChartOpenGL.SetPrimary("32");
			//
			toothChartDirectX.SetPrimary("1");
			toothChartDirectX.SetPrimary("2");
			toothChartDirectX.SetPrimary("4");
			toothChartDirectX.SetPrimary("5");
			toothChartDirectX.SetPrimary("6");
			toothChartDirectX.SetPrimary("11");
			toothChartDirectX.SetPrimary("12");
			toothChartDirectX.SetPrimary("13");
			toothChartDirectX.SetPrimary("15");
			toothChartDirectX.SetPrimary("16");
			toothChartDirectX.SetPrimary("17");
			toothChartDirectX.SetPrimary("18");
			toothChartDirectX.SetPrimary("20");
			toothChartDirectX.SetPrimary("21");
			toothChartDirectX.SetPrimary("22");
			toothChartDirectX.SetPrimary("27");
			toothChartDirectX.SetPrimary("28");
			toothChartDirectX.SetPrimary("29");
			toothChartDirectX.SetPrimary("31");
			toothChartDirectX.SetPrimary("32");
		}

		private void panelColorBackgroundGray_Click(object sender,EventArgs e) {
			toothChartOpenGL.ColorBackground=panelColorBackgroundGray.BackColor;
			toothChartDirectX.ColorBackground=panelColorBackgroundGray.BackColor;
		}

		private void panelColorBackgroundLtGray_Click(object sender,EventArgs e) {
			toothChartOpenGL.ColorBackground=panelColorBackgroundLtGray.BackColor;
			toothChartDirectX.ColorBackground=panelColorBackgroundLtGray.BackColor;
		}

		private void panelColorBackgroundBlack_Click(object sender,EventArgs e) {
			toothChartOpenGL.ColorBackground=panelColorBackgroundBlack.BackColor;
			toothChartDirectX.ColorBackground=panelColorBackgroundBlack.BackColor;
		}

		private void panelColorBackgroundWhite_Click(object sender,EventArgs e) {
			toothChartOpenGL.ColorBackground=panelColorBackgroundWhite.BackColor;
			toothChartDirectX.ColorBackground=panelColorBackgroundWhite.BackColor;
		}

		private void panelColorBackgroundBlue_Click(object sender,EventArgs e) {
			toothChartOpenGL.ColorBackground=panelColorBackgroundBlue.BackColor;
			toothChartDirectX.ColorBackground=panelColorBackgroundBlue.BackColor;
		}

		private void panelColorTextGray_Click(object sender,EventArgs e) {
			toothChart2D.ColorText=panelColorTextGray.BackColor;
			toothChartOpenGL.ColorText=panelColorTextGray.BackColor;
			toothChartDirectX.ColorText=panelColorTextGray.BackColor;
		}

		private void panelColorTextBlack_Click(object sender,EventArgs e) {
			toothChart2D.ColorText=panelColorTextBlack.BackColor;
			toothChartOpenGL.ColorText=panelColorTextBlack.BackColor;
			toothChartDirectX.ColorText=panelColorTextBlack.BackColor;
		}

		private void panelColorTextWhite_Click(object sender,EventArgs e) {
			toothChart2D.ColorText=panelColorTextWhite.BackColor;
			toothChartOpenGL.ColorText=panelColorTextWhite.BackColor;
			toothChartDirectX.ColorText=panelColorTextWhite.BackColor;
		}

		private void panelColorTextHighlightGray_Click(object sender,EventArgs e) {
			toothChart2D.ColorTextHighlight=panelColorTextHighlightGray.BackColor;
			toothChartOpenGL.ColorTextHighlight=panelColorTextHighlightGray.BackColor;
			toothChartDirectX.ColorTextHighlight=panelColorTextHighlightGray.BackColor;
		}

		private void panelColorTextHighlightBlack_Click(object sender,EventArgs e) {
			toothChart2D.ColorTextHighlight=panelColorTextHighlightBlack.BackColor;
			toothChartOpenGL.ColorTextHighlight=panelColorTextHighlightBlack.BackColor;
			toothChartDirectX.ColorTextHighlight=panelColorTextHighlightBlack.BackColor;
		}

		private void panelColorTextHighlightWhite_Click(object sender,EventArgs e) {
			toothChart2D.ColorTextHighlight=panelColorTextHighlightWhite.BackColor;
			toothChartOpenGL.ColorTextHighlight=panelColorTextHighlightWhite.BackColor;
			toothChartDirectX.ColorTextHighlight=panelColorTextHighlightWhite.BackColor;
		}

		private void panelColorTextHighlightRed_Click(object sender,EventArgs e) {
			toothChart2D.ColorTextHighlight=panelColorTextHighlightRed.BackColor;
			toothChartOpenGL.ColorTextHighlight=panelColorTextHighlightRed.BackColor;
			toothChartDirectX.ColorTextHighlight=panelColorTextHighlightRed.BackColor;
		}

		private void panelColorBackHighlightGray_Click(object sender,EventArgs e) {
			toothChart2D.ColorBackHighlight=panelColorBackHighlightGray.BackColor;
			toothChartOpenGL.ColorBackHighlight=panelColorBackHighlightGray.BackColor;
			toothChartDirectX.ColorBackHighlight=panelColorBackHighlightGray.BackColor;
		}

		private void panelColorBackHighlightBlack_Click(object sender,EventArgs e) {
			toothChart2D.ColorBackHighlight=panelColorBackHighlightBlack.BackColor;
			toothChartOpenGL.ColorBackHighlight=panelColorBackHighlightBlack.BackColor;
			toothChartDirectX.ColorBackHighlight=panelColorBackHighlightBlack.BackColor;
		}

		private void panelColorBackHighlightWhite_Click(object sender,EventArgs e) {
			toothChart2D.ColorBackHighlight=panelColorBackHighlightWhite.BackColor;
			toothChartOpenGL.ColorBackHighlight=panelColorBackHighlightWhite.BackColor;
			toothChartDirectX.ColorBackHighlight=panelColorBackHighlightWhite.BackColor;
		}

		private void panelColorBackHighlightBlue_Click(object sender,EventArgs e) {
			toothChart2D.ColorBackHighlight=panelColorBackHighlightBlue.BackColor;
			toothChartOpenGL.ColorBackHighlight=panelColorBackHighlightBlue.BackColor;
			toothChartDirectX.ColorBackHighlight=panelColorBackHighlightBlue.BackColor;
		}

		private void butSizeNormal_Click(object sender,EventArgs e) {
			toothChart2D.Size=new Size(410,307);
			toothChartOpenGL.Size=new Size(410,307);
			toothChartDirectX.Size=new Size(410,307);
		}

		private void butSizeTall_Click(object sender,EventArgs e) {
			toothChart2D.Size=new Size(250,307);
			toothChartOpenGL.Size=new Size(250,307);
			toothChartDirectX.Size=new Size(250,307);
		}

		private void butSizeWide_Click(object sender,EventArgs e) {
			toothChart2D.Size=new Size(410,190);
			toothChartOpenGL.Size=new Size(410,190);
			toothChartDirectX.Size=new Size(410,190);
		}

		private void butMissing_Click(object sender,EventArgs e) {
			toothChart2D.ResetTeeth();
			toothChartOpenGL.ResetTeeth();
			toothChartDirectX.ResetTeeth();
			//toothChart2D.  pointless
			toothChartOpenGL.SetMissing("1");
			toothChartOpenGL.SetMissing("3");
			toothChartOpenGL.SetMissing("5");
			toothChartOpenGL.SetMissing("7");
			toothChartOpenGL.SetMissing("9");
			toothChartOpenGL.SetMissing("11");
			toothChartOpenGL.SetMissing("13");
			toothChartOpenGL.SetMissing("15");
			toothChartOpenGL.SetMissing("17");
			toothChartOpenGL.SetMissing("19");
			toothChartOpenGL.SetMissing("21");
			toothChartOpenGL.SetMissing("23");
			toothChartOpenGL.SetMissing("25");
			toothChartOpenGL.SetMissing("27");
			toothChartOpenGL.SetMissing("29");
			toothChartOpenGL.SetMissing("31");
			//
			toothChartDirectX.SetMissing("1");
			toothChartDirectX.SetMissing("3");
			toothChartDirectX.SetMissing("5");
			toothChartDirectX.SetMissing("7");
			toothChartDirectX.SetMissing("9");
			toothChartDirectX.SetMissing("11");
			toothChartDirectX.SetMissing("13");
			toothChartDirectX.SetMissing("15");
			toothChartDirectX.SetMissing("17");
			toothChartDirectX.SetMissing("19");
			toothChartDirectX.SetMissing("21");
			toothChartDirectX.SetMissing("23");
			toothChartDirectX.SetMissing("25");
			toothChartDirectX.SetMissing("27");
			toothChartDirectX.SetMissing("29");
			toothChartDirectX.SetMissing("31");
		}

		private void butHidden_Click(object sender,EventArgs e) {
			toothChart2D.ResetTeeth();
			toothChartOpenGL.ResetTeeth();
			toothChartDirectX.ResetTeeth();
			//
			toothChart2D.SetHidden("4");
			toothChart2D.SetHidden("5");
			toothChart2D.SetHidden("12");
			toothChart2D.SetHidden("13");
			toothChart2D.SetHidden("20");
			toothChart2D.SetHidden("21");
			toothChart2D.SetHidden("28");
			toothChart2D.SetHidden("29");
			//
			toothChartOpenGL.SetHidden("4");
			toothChartOpenGL.SetHidden("5");
			toothChartOpenGL.SetHidden("12");
			toothChartOpenGL.SetHidden("13");
			toothChartOpenGL.SetHidden("20");
			toothChartOpenGL.SetHidden("21");
			toothChartOpenGL.SetHidden("28");
			toothChartOpenGL.SetHidden("29");
			//
			toothChartDirectX.SetHidden("4");
			toothChartDirectX.SetHidden("5");
			toothChartDirectX.SetHidden("12");
			toothChartDirectX.SetHidden("13");
			toothChartDirectX.SetHidden("20");
			toothChartDirectX.SetHidden("21");
			toothChartDirectX.SetHidden("28");
			toothChartDirectX.SetHidden("29");
		}

		private void butMissingHiddenComplex_Click(object sender,EventArgs e) {
			toothChart2D.ResetTeeth();
			toothChartOpenGL.ResetTeeth();
			toothChartDirectX.ResetTeeth();
			//
			toothChart2D.SetPrimary("4");
			toothChart2D.SetPrimary("5");
			toothChart2D.SetPrimary("6");
			toothChart2D.SetPrimary("7");
			toothChart2D.SetPrimary("8");
			toothChart2D.SetPrimary("9");
			toothChart2D.SetPrimary("10");
			toothChart2D.SetPrimary("11");
			toothChart2D.SetPrimary("12");
			toothChart2D.SetPrimary("13");
			toothChart2D.SetPrimary("14");
			toothChart2D.SetPrimary("15");
			toothChart2D.SetPrimary("16");
			toothChart2D.SetMissing("4");
			toothChart2D.SetMissing("B");
			toothChart2D.SetMissing("6");
			toothChart2D.SetMissing("D");
			toothChart2D.SetHidden("G");
			toothChart2D.SetHidden("11");
			toothChart2D.SetHidden("I");
			toothChart2D.SetHidden("13");
			toothChart2D.SetHidden("14");
			//
			toothChartOpenGL.SetPrimary("4");
			toothChartOpenGL.SetPrimary("5");
			toothChartOpenGL.SetPrimary("6");
			toothChartOpenGL.SetPrimary("7");
			toothChartOpenGL.SetPrimary("8");
			toothChartOpenGL.SetPrimary("9");
			toothChartOpenGL.SetPrimary("10");
			toothChartOpenGL.SetPrimary("11");
			toothChartOpenGL.SetPrimary("12");
			toothChartOpenGL.SetPrimary("13");
			toothChartOpenGL.SetPrimary("14");
			toothChartOpenGL.SetPrimary("15");
			toothChartOpenGL.SetPrimary("16");
			toothChartOpenGL.SetMissing("4");
			toothChartOpenGL.SetMissing("B");
			toothChartOpenGL.SetMissing("6");
			toothChartOpenGL.SetMissing("D");
			toothChartOpenGL.SetHidden("G");
			toothChartOpenGL.SetHidden("11");
			toothChartOpenGL.SetHidden("I");
			toothChartOpenGL.SetHidden("13");
			toothChartOpenGL.SetHidden("14");
			//mirror image on the bottom:
			toothChartOpenGL.SetPrimary("29");
			toothChartOpenGL.SetPrimary("28");
			toothChartOpenGL.SetPrimary("27");
			toothChartOpenGL.SetPrimary("26");
			toothChartOpenGL.SetPrimary("25");
			toothChartOpenGL.SetPrimary("24");
			toothChartOpenGL.SetPrimary("23");
			toothChartOpenGL.SetPrimary("22");
			toothChartOpenGL.SetPrimary("21");
			toothChartOpenGL.SetPrimary("20");
			toothChartOpenGL.SetPrimary("19");
			toothChartOpenGL.SetPrimary("18");
			toothChartOpenGL.SetPrimary("17");
			toothChartOpenGL.SetMissing("29");
			toothChartOpenGL.SetMissing("S");
			toothChartOpenGL.SetMissing("27");
			toothChartOpenGL.SetMissing("Q");
			toothChartOpenGL.SetHidden("N");
			toothChartOpenGL.SetHidden("22");
			toothChartOpenGL.SetHidden("L");
			toothChartOpenGL.SetHidden("20");
			toothChartOpenGL.SetHidden("19");
			//
			toothChartDirectX.SetPrimary("4");
			toothChartDirectX.SetPrimary("5");
			toothChartDirectX.SetPrimary("6");
			toothChartDirectX.SetPrimary("7");
			toothChartDirectX.SetPrimary("8");
			toothChartDirectX.SetPrimary("9");
			toothChartDirectX.SetPrimary("10");
			toothChartDirectX.SetPrimary("11");
			toothChartDirectX.SetPrimary("12");
			toothChartDirectX.SetPrimary("13");
			toothChartDirectX.SetPrimary("14");
			toothChartDirectX.SetPrimary("15");
			toothChartDirectX.SetPrimary("16");
			toothChartDirectX.SetMissing("4");
			toothChartDirectX.SetMissing("B");
			toothChartDirectX.SetMissing("6");
			toothChartDirectX.SetMissing("D");
			toothChartDirectX.SetHidden("G");
			toothChartDirectX.SetHidden("11");
			toothChartDirectX.SetHidden("I");
			toothChartDirectX.SetHidden("13");
			toothChartDirectX.SetHidden("14");
		}

		private void butFillings_Click(object sender,EventArgs e) {
			toothChart2D.ResetTeeth();
			toothChartOpenGL.ResetTeeth();
			toothChartDirectX.ResetTeeth();
			//
			toothChart2D.SetSurfaceColors("3","MOD",Color.DarkRed);
			toothChart2D.SetSurfaceColors("4","V",Color.Green);
			toothChart2D.SetSurfaceColors("5","B",Color.Green);
			toothChart2D.SetSurfaceColors("6","FIL",Color.DarkRed);
			toothChart2D.SetPrimary("11");
			toothChart2D.SetMissing("11");
			toothChart2D.SetSurfaceColors("H","MOD",Color.DarkRed);//some invalid surfaces
			toothChart2D.SetPrimary("12");
			toothChart2D.SetSurfaceColors("I","MOD",Color.DarkRed);
			toothChart2D.SetSurfaceColors("J","O",Color.DarkRed);//should not show
			toothChart2D.SetHidden("14");
			toothChart2D.SetSurfaceColors("14","MOD",Color.DarkRed);//should not show
			//
			toothChartOpenGL.SetSurfaceColors("3","MOD",Color.DarkRed);
			toothChartOpenGL.SetSurfaceColors("4","V",Color.Green);
			toothChartOpenGL.SetSurfaceColors("5","B",Color.Green);
			toothChartOpenGL.SetSurfaceColors("6","FIL",Color.DarkRed);
			toothChartOpenGL.SetPrimary("11");
			toothChartOpenGL.SetMissing("11");
			toothChartOpenGL.SetSurfaceColors("H","MOD",Color.DarkRed);//some invalid surfaces
			toothChartOpenGL.SetPrimary("12");
			toothChartOpenGL.SetSurfaceColors("I","MOD",Color.DarkRed);
			toothChartOpenGL.SetSurfaceColors("J","O",Color.DarkRed);//should not show
			toothChartOpenGL.SetHidden("14");
			toothChartOpenGL.SetSurfaceColors("14","MOD",Color.DarkRed);//should not show
			//
			toothChartDirectX.SetSurfaceColors("3","MOD",Color.DarkRed);
			toothChartDirectX.SetSurfaceColors("4","V",Color.Green);
			toothChartDirectX.SetSurfaceColors("5","B",Color.Green);
			toothChartDirectX.SetSurfaceColors("6","FIL",Color.DarkRed);
			toothChartDirectX.SetPrimary("11");
			toothChartDirectX.SetMissing("11");
			toothChartDirectX.SetSurfaceColors("H","MOD",Color.DarkRed);//some invalid surfaces
			toothChartDirectX.SetPrimary("12");
			toothChartDirectX.SetSurfaceColors("I","MOD",Color.DarkRed);
			toothChartDirectX.SetSurfaceColors("J","O",Color.DarkRed);//should not show
			toothChartDirectX.SetHidden("14");
			toothChartDirectX.SetSurfaceColors("14","MOD",Color.DarkRed);//should not show
		}

		private void butMouse_Click(object sender,EventArgs e) {
			//toothChartOpenGL.
		}

		



	}
}
