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
			if(MeasCur.EventType==EhrMeasureEventType.TobaccoUseAssessed) {
				Loinc lCur=Loincs.GetByCode(MeasCur.CodeValueEvent);//TobaccoUseAssessed events can be one of three types, all LOINC codes
				if(lCur!=null) {
					textType.Text=lCur.NameLongCommon;//Example: History of tobacco use Narrative
				}
				Snomed sCur=Snomeds.GetByCode(MeasCur.CodeValueResult);//TobaccoUseAssessed results can be any SNOMEDCT code, we recommend one of 8 codes, but the CQM measure allows 54 codes and we let the user select any SNOMEDCT they want
				if(sCur!=null) {
					textResult.Text=sCur.Description;//Examples: Non-smoker (finding) or Smoker (finding)
				}
			}
			if(textType.Text==""){//if not set by LOINC name above, then either not a TobaccoUseAssessed event or the code was not in the LOINC table, fill with EventType
				textType.Text=MeasCur.EventType.ToString();
			}
			textMoreInfo.Text=MeasCur.MoreInfo;
		}

		private void butDelete_Click(object sender,EventArgs e) {
			if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"Delete?")) {
				return;
			}
			EhrMeasureEvents.Delete(MeasCur.EhrMeasureEventNum);
			DialogResult=DialogResult.Cancel;
		}

		private void butOK_Click(object sender,EventArgs e) {
			//inserts never happen here.  Only updates.
			MeasCur.MoreInfo=textMoreInfo.Text;
			EhrMeasureEvents.Update(MeasCur);
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}




	


	}
}
