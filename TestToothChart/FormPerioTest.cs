using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
		}
	}
}
