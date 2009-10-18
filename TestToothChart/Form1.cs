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
			toothChartDirectX.DrawMode=DrawingMode.DirectX;
			toothChartOpenGL.DrawMode=DrawingMode.OpenGL;
			toothChart2D.DrawMode=DrawingMode.Simple2D;
		}

		private void Form1_Load(object sender,EventArgs e) {

		}
	}
}
