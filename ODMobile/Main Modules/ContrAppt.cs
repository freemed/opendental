using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace OpenDentMobile {
	public partial class ContrAppt:UserControl {
		private int PatCurNum;

		public ContrAppt() {
			InitializeComponent();
		}

		///<summary></summary>
		public void ModuleSelected(int patNum){
			RefreshModulePatient(patNum);
			RefreshPeriod();
		}
		///<summary></summary>
		private void RefreshModulePatient(int patNum){
			PatCurNum=patNum;//might be zero
		}

		///<summary>Important.  Gets all new day info from db and redraws screen</summary>
		public void RefreshPeriod(){
			//DS=AppointmentL.RefreshPeriod(startDate,endDate);
		}





	}
}
