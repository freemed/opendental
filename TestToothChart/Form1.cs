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
			toothChart2D.SetToPrimary("1");
			toothChart2D.SetToPrimary("2");
			toothChart2D.SetToPrimary("3");
			toothChart2D.SetToPrimary("4");
			toothChart2D.SetToPrimary("5");
			toothChart2D.SetToPrimary("6");
			toothChart2D.SetToPrimary("7");
			toothChart2D.SetToPrimary("8");
			toothChart2D.SetToPrimary("9");
			toothChart2D.SetToPrimary("10");
			toothChart2D.SetToPrimary("11");
			toothChart2D.SetToPrimary("12");
			toothChart2D.SetToPrimary("13");
			toothChart2D.SetToPrimary("14");
			toothChart2D.SetToPrimary("15");
			toothChart2D.SetToPrimary("16");
			toothChart2D.SetToPrimary("17");
			toothChart2D.SetToPrimary("18");
			toothChart2D.SetToPrimary("19");
			toothChart2D.SetToPrimary("20");
			toothChart2D.SetToPrimary("21");
			toothChart2D.SetToPrimary("22");
			toothChart2D.SetToPrimary("23");
			toothChart2D.SetToPrimary("24");
			toothChart2D.SetToPrimary("25");
			toothChart2D.SetToPrimary("26");
			toothChart2D.SetToPrimary("27");
			toothChart2D.SetToPrimary("28");
			toothChart2D.SetToPrimary("29");
			toothChart2D.SetToPrimary("30");
			toothChart2D.SetToPrimary("31");
			toothChart2D.SetToPrimary("32");
			//
			toothChartOpenGL.SetToPrimary("1");
			toothChartOpenGL.SetToPrimary("2");
			toothChartOpenGL.SetToPrimary("3");
			toothChartOpenGL.SetToPrimary("4");
			toothChartOpenGL.SetToPrimary("5");
			toothChartOpenGL.SetToPrimary("6");
			toothChartOpenGL.SetToPrimary("7");
			toothChartOpenGL.SetToPrimary("8");
			toothChartOpenGL.SetToPrimary("9");
			toothChartOpenGL.SetToPrimary("10");
			toothChartOpenGL.SetToPrimary("11");
			toothChartOpenGL.SetToPrimary("12");
			toothChartOpenGL.SetToPrimary("13");
			toothChartOpenGL.SetToPrimary("14");
			toothChartOpenGL.SetToPrimary("15");
			toothChartOpenGL.SetToPrimary("16");
			toothChartOpenGL.SetToPrimary("17");
			toothChartOpenGL.SetToPrimary("18");
			toothChartOpenGL.SetToPrimary("19");
			toothChartOpenGL.SetToPrimary("20");
			toothChartOpenGL.SetToPrimary("21");
			toothChartOpenGL.SetToPrimary("22");
			toothChartOpenGL.SetToPrimary("23");
			toothChartOpenGL.SetToPrimary("24");
			toothChartOpenGL.SetToPrimary("25");
			toothChartOpenGL.SetToPrimary("26");
			toothChartOpenGL.SetToPrimary("27");
			toothChartOpenGL.SetToPrimary("28");
			toothChartOpenGL.SetToPrimary("29");
			toothChartOpenGL.SetToPrimary("30");
			toothChartOpenGL.SetToPrimary("31");
			toothChartOpenGL.SetToPrimary("32");
			//
			toothChartDirectX.SetToPrimary("1");
			toothChartDirectX.SetToPrimary("2");
			toothChartDirectX.SetToPrimary("3");
			toothChartDirectX.SetToPrimary("4");
			toothChartDirectX.SetToPrimary("5");
			toothChartDirectX.SetToPrimary("6");
			toothChartDirectX.SetToPrimary("7");
			toothChartDirectX.SetToPrimary("8");
			toothChartDirectX.SetToPrimary("9");
			toothChartDirectX.SetToPrimary("10");
			toothChartDirectX.SetToPrimary("11");
			toothChartDirectX.SetToPrimary("12");
			toothChartDirectX.SetToPrimary("13");
			toothChartDirectX.SetToPrimary("14");
			toothChartDirectX.SetToPrimary("15");
			toothChartDirectX.SetToPrimary("16");
			toothChartDirectX.SetToPrimary("17");
			toothChartDirectX.SetToPrimary("18");
			toothChartDirectX.SetToPrimary("19");
			toothChartDirectX.SetToPrimary("20");
			toothChartDirectX.SetToPrimary("21");
			toothChartDirectX.SetToPrimary("22");
			toothChartDirectX.SetToPrimary("23");
			toothChartDirectX.SetToPrimary("24");
			toothChartDirectX.SetToPrimary("25");
			toothChartDirectX.SetToPrimary("26");
			toothChartDirectX.SetToPrimary("27");
			toothChartDirectX.SetToPrimary("28");
			toothChartDirectX.SetToPrimary("29");
			toothChartDirectX.SetToPrimary("30");
			toothChartDirectX.SetToPrimary("31");
			toothChartDirectX.SetToPrimary("32");
		}

		private void butMixed_Click(object sender,EventArgs e) {
			toothChart2D.ResetTeeth();
			toothChartOpenGL.ResetTeeth();
			toothChartDirectX.ResetTeeth();
			//
			toothChart2D.SetToPrimary("1");
			toothChart2D.SetToPrimary("2");
			toothChart2D.SetToPrimary("4");
			toothChart2D.SetToPrimary("5");
			toothChart2D.SetToPrimary("6");
			toothChart2D.SetToPrimary("11");
			toothChart2D.SetToPrimary("12");
			toothChart2D.SetToPrimary("13");
			toothChart2D.SetToPrimary("15");
			toothChart2D.SetToPrimary("16");
			toothChart2D.SetToPrimary("17");
			toothChart2D.SetToPrimary("18");
			toothChart2D.SetToPrimary("20");
			toothChart2D.SetToPrimary("21");
			toothChart2D.SetToPrimary("22");
			toothChart2D.SetToPrimary("27");
			toothChart2D.SetToPrimary("28");
			toothChart2D.SetToPrimary("29");
			toothChart2D.SetToPrimary("31");
			toothChart2D.SetToPrimary("32");
			//
			toothChartOpenGL.SetToPrimary("1");
			toothChartOpenGL.SetToPrimary("2");
			toothChartOpenGL.SetToPrimary("4");
			toothChartOpenGL.SetToPrimary("5");
			toothChartOpenGL.SetToPrimary("6");
			toothChartOpenGL.SetToPrimary("11");
			toothChartOpenGL.SetToPrimary("12");
			toothChartOpenGL.SetToPrimary("13");
			toothChartOpenGL.SetToPrimary("15");
			toothChartOpenGL.SetToPrimary("16");
			toothChartOpenGL.SetToPrimary("17");
			toothChartOpenGL.SetToPrimary("18");
			toothChartOpenGL.SetToPrimary("20");
			toothChartOpenGL.SetToPrimary("21");
			toothChartOpenGL.SetToPrimary("22");
			toothChartOpenGL.SetToPrimary("27");
			toothChartOpenGL.SetToPrimary("28");
			toothChartOpenGL.SetToPrimary("29");
			toothChartOpenGL.SetToPrimary("31");
			toothChartOpenGL.SetToPrimary("32");
			//
			toothChartDirectX.SetToPrimary("1");
			toothChartDirectX.SetToPrimary("2");
			toothChartDirectX.SetToPrimary("4");
			toothChartDirectX.SetToPrimary("5");
			toothChartDirectX.SetToPrimary("6");
			toothChartDirectX.SetToPrimary("11");
			toothChartDirectX.SetToPrimary("12");
			toothChartDirectX.SetToPrimary("13");
			toothChartDirectX.SetToPrimary("15");
			toothChartDirectX.SetToPrimary("16");
			toothChartDirectX.SetToPrimary("17");
			toothChartDirectX.SetToPrimary("18");
			toothChartDirectX.SetToPrimary("20");
			toothChartDirectX.SetToPrimary("21");
			toothChartDirectX.SetToPrimary("22");
			toothChartDirectX.SetToPrimary("27");
			toothChartDirectX.SetToPrimary("28");
			toothChartDirectX.SetToPrimary("29");
			toothChartDirectX.SetToPrimary("31");
			toothChartDirectX.SetToPrimary("32");
		}

		private void panelColorBackgroundGray_Click(object sender,EventArgs e) {
			toothChartOpenGL.ColorBackground=panelColorBackgroundGray.BackColor;
			toothChartDirectX.ColorBackground=panelColorBackgroundGray.BackColor;
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
			//toothChart2D.ColorText=panelColorTextGray.BackColor;
			//toothChartOpenGL.ColorText=panelColorTextGray.BackColor;
			//toothChartDirectX.ColorText=panelColorTextGray.BackColor;
		}

		private void panelColorTextHighlightBlack_Click(object sender,EventArgs e) {

		}

		private void panelColorTextHighlightWhite_Click(object sender,EventArgs e) {

		}

		private void panelColorTextHighlightRed_Click(object sender,EventArgs e) {

		}

		private void panelColorBackHighlightGray_Click(object sender,EventArgs e) {

		}

		private void panelColorBackHighlightBlack_Click(object sender,EventArgs e) {

		}

		private void panelColorBackHighlightWhite_Click(object sender,EventArgs e) {

		}

		private void panelColorBackHighlightBlue_Click(object sender,EventArgs e) {

		}



	}
}
