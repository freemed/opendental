using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.Forms;
using System.Xml;
using System.Threading;
using System.Net;
using System.IO;
using Ionic.Zip;

namespace OpenDental {
	public partial class FormCDSSetup:Form {
		public FormCDSSetup() {
			InitializeComponent();
			Lan.F(this);
		}

		private void butClose_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}


		

	}
}