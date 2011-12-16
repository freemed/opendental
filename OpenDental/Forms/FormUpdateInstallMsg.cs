using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormUpdateInstallMsg:Form {
		public string VersionAvailable;

		public FormUpdateInstallMsg() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormUpdateInstallMsg_Load(object sender,EventArgs e) {
			Version thisVersion=new Version(Application.ProductVersion);
			Version versAvail=new Version(VersionAvailable);
			/*
			if(thisVersion < new Version(7,0) && versAvail < new Version(7,0)) {
				listBox1.Items.Add("http://www.opendental.com/manual/version7_0.html");
			}
			if(thisVersion < new Version(7,1) && thisVersion < new Version(7,1)) {
				listBox1.Items.Add("http://www.opendental.com/manual/version7_1.html");
			}
			if(thisVersion < new Version(7,2) && thisVersion < new Version(7,2)) {
				listBox1.Items.Add("http://www.opendental.com/manual/version7_2.html");
			}
			if(thisVersion < new Version(7,3) && thisVersion < new Version(7,3)) {
				listBox1.Items.Add("http://www.opendental.com/manual/version7_3.html");
			}
			if(thisVersion < new Version(7,4) && thisVersion < new Version(7,4)) {
				listBox1.Items.Add("http://www.opendental.com/manual/version7_4.html");
			}
			if(thisVersion < new Version(7,5) && thisVersion < new Version(7,5)) {
				listBox1.Items.Add("http://www.opendental.com/manual/version7_5.html");
			}
			if(thisVersion < new Version(7,6) && thisVersion < new Version(7,6)) {
				listBox1.Items.Add("http://www.opendental.com/manual/version7_6.html");
			}
			if(thisVersion < new Version(7,7) && thisVersion < new Version(7,7)) {
				listBox1.Items.Add("http://www.opendental.com/manual/version7_7.html");
			}
			if(thisVersion < new Version(7,8) && thisVersion < new Version(7,8)) {
				listBox1.Items.Add("http://www.opendental.com/manual/version7_8.html");
			}
			if(thisVersion < new Version(7,9) && thisVersion < new Version(7,9)) {
				listBox1.Items.Add("http://www.opendental.com/manual/version7_9.html");
			}
			if(thisVersion < new Version(11,0) && thisVersion < new Version(11,0)) {
				listBox1.Items.Add("http://www.opendental.com/manual/version11_0.html");
			}
			if(thisVersion < new Version(11,1) && thisVersion < new Version(11,1)) {
				listBox1.Items.Add("http://www.opendental.com/manual/version11_1.html");
			}
			if(thisVersion < new Version(12,0) && thisVersion < new Version(12,0)) {
				listBox1.Items.Add("http://www.opendental.com/manual/version12_0.html");
			}
			 */
		}

		private void butOK_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		
	}
}