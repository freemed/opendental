using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental;
using OpenDental.UI;

namespace OpenDental {
	///<summary>Only used for editing smoking documentation.</summary>
	public partial class FormEhrMeasureEventEdit:Form {
		public EhrMeasureEvent MeasCur;

		public FormEhrMeasureEventEdit() {
			InitializeComponent();
		}

		private void FormEhrMeasureEventEdit_Load(object sender,EventArgs e) {
			textDateTime.Text=MeasCur.DateTEvent.ToString();
			textType.Text=MeasCur.EventType.ToString();
			textMoreInfo.Text=MeasCur.MoreInfo;
		}

		private void butOK_Click(object sender,EventArgs e) {
			//inserts never happens here.  Only updates.
			MeasCur.MoreInfo=textMoreInfo.Text;
			EhrMeasureEvents.Update(MeasCur);
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}




	


	}
}
