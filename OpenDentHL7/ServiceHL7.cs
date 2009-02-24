using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;

namespace OpenDentHL7 {
	public partial class ServiceHL7:ServiceBase {
		public ServiceHL7() {
			InitializeComponent();
		}

		protected override void OnStart(string[] args) {

		}

		protected override void OnStop() {

		}
	}
}
