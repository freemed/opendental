using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using OpenDentBusiness;
using CodeBase;
using System.Globalization;
using System.Xml.XPath;
using System.IO;
using OpenDental.UI;

namespace OpenDental {
	public partial class FormEhrTriggers:Form {

		public FormEhrTriggers() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormEhrTriggers_Load(object sender,EventArgs e) {
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	}
}