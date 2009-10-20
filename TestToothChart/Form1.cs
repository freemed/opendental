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
			//this will set all teeth to primary.  The behavior will change from previous versions, so this will also need to translate all permanent teeth neg occlusal.
			toothChart2D.SetToPrimary("5");
			toothChartOpenGL.SetToPrimary("5");
			toothChartDirectX.SetToPrimary("5");
		}
	}
}
