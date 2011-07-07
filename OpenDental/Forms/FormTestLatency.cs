using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
using System.Diagnostics;

namespace OpenDental {
	public partial class FormTestLatency:Form {
		public FormTestLatency() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormTestLatency_Load(object sender,EventArgs e) {
			textTests.Text="50";
		}

		private void butRun_Click(object sender,EventArgs e) {
			long loopCount;
			try {
				loopCount=long.Parse(textTests.Text);
			}
			catch {
				MsgBox.Show(this,Lan.g(this,"Unrecognized number, running 50 tests by default."));
				textTests.Text="50";
				loopCount=50;
			}
			if(loopCount>1000) {
				MsgBox.Show(this,Lan.g(this,"Maximum tests allowed is 1000."));
				textTests.Text="1000";
				loopCount=1000;
			}
			Stopwatch watch = new Stopwatch();
			long min=long.MaxValue;
			long max=-1;
			long total = 0;
			for(int i=0;i<loopCount;i++) {
				watch.Reset();
				watch.Start();
				Prefs.RefreshCache();
				watch.Stop();
				total+=watch.ElapsedMilliseconds;
				if(min>watch.ElapsedMilliseconds) {
					min=watch.ElapsedMilliseconds;
				}
				if(max<watch.ElapsedMilliseconds) {
					max=watch.ElapsedMilliseconds;
				}
			}
			textMin.Text=min.ToString();
			textAverage.Text=((float)total/(float)loopCount).ToString();
			textMax.Text=max.ToString();
		}

		private void butClose_Click(object sender,EventArgs e) {
			Close();
		}


	}
}