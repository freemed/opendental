using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace OpenDentMobile {
	public partial class Form1:Form {
		public Form1() {
			InitializeComponent();
		}

		private void Form1_KeyDown(object sender,KeyEventArgs e) {
			if((e.KeyCode == System.Windows.Forms.Keys.Up)) {
				// Up
			}
			if((e.KeyCode == System.Windows.Forms.Keys.Down)) {
				// Down
			}
			if((e.KeyCode == System.Windows.Forms.Keys.Left)) {
				// Left
			}
			if((e.KeyCode == System.Windows.Forms.Keys.Right)) {
				// Right
			}
			if((e.KeyCode == System.Windows.Forms.Keys.Enter)) {
				// Enter
			}

		}
	}
}