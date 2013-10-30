using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.UI;

namespace OpenDental {
	public partial class FormEhrPatientSmoking:Form {
		public Patient PatCur;
		///<summary>A copy of the original patient object, as it was when this form was first opened.</summary>
		private Patient PatOld;
		private List<EhrMeasureEvent> listEvents;

		public FormEhrPatientSmoking() {
			InitializeComponent();
		}

		private void FormPatientSmoking_Load(object sender,EventArgs e) {
			comboSmokeStatus.Items.Add("None");//First and default index
			//Smoking statuses add in the same order as they appear in the SmokingSnoMed enum (Starting at comboSmokeStatus index 1). Changes to the enum order will change the order added so they will always match
			for(int i=0;i<Enum.GetNames(typeof(SmokingSnoMed)).Length;i++) {
				switch((SmokingSnoMed)i) {
					case SmokingSnoMed._266927001:
						comboSmokeStatus.Items.Add("UnknownIfEver");
						break;
					case SmokingSnoMed._77176002:
						comboSmokeStatus.Items.Add("SmokerUnknownCurrent");
						break;
					case SmokingSnoMed._266919005:
						comboSmokeStatus.Items.Add("NeverSmoked");
						break;
					case SmokingSnoMed._8517006:
						comboSmokeStatus.Items.Add("FormerSmoker");
						break;
					case SmokingSnoMed._428041000124106:
						comboSmokeStatus.Items.Add("CurrentSomeDay");
						break;
					case SmokingSnoMed._449868002:
						comboSmokeStatus.Items.Add("CurrentEveryDay");
						break;
					case SmokingSnoMed._428061000124105:
						comboSmokeStatus.Items.Add("LightSmoker");
						break;
					case SmokingSnoMed._428071000124103:
						comboSmokeStatus.Items.Add("HeavySmoker");
						break;
				}
			}
			PatOld=PatCur.Copy();
			comboSmokeStatus.SelectedIndex=0;//None
			if(PatCur.SmokingSnoMed!="") {
				try {
					comboSmokeStatus.SelectedIndex=(int)Enum.Parse(typeof(SmokingSnoMed),"_"+PatCur.SmokingSnoMed,true)+1;
				}
				catch {
					//stays as None
				}
			}
			FillGrid();
		}

		private void FillGrid() {
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn("DateTime",135);
			gridMain.Columns.Add(col); 
			col=new ODGridColumn("Type",130);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Documentation",150);
			gridMain.Columns.Add(col);
			listEvents=EhrMeasureEvents.RefreshByType(PatCur.PatNum,EhrMeasureEventType.TobaccoCessation,EhrMeasureEventType.TobaccoUseAssessed);
			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<listEvents.Count;i++) {
				row=new ODGridRow();
				row.Cells.Add(listEvents[i].DateTEvent.ToString());
				row.Cells.Add(listEvents[i].EventType.ToString());
				row.Cells.Add(listEvents[i].MoreInfo);
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			if(listEvents[e.Row].EventType!=EhrMeasureEventType.TobaccoCessation) {
				MessageBox.Show("Only Tobacco Cessation entries may be edited.");
				return;
			}
			FormEhrMeasureEventEdit formM=new FormEhrMeasureEventEdit();
			formM.MeasCur=listEvents[e.Row];
			formM.ShowDialog();
			FillGrid();
		}

		private void comboSmokeStatus_SelectionChangeCommitted(object sender,EventArgs e) {
			if(comboSmokeStatus.SelectedIndex==0) {//If None, do not create an event
				return;
			}
			//Automatically make an entry
			for(int i=0;i<listEvents.Count;i++) {
				if(listEvents[i].DateTEvent.Date==DateTime.Today) {
					return;
				}
			}
			//an entry for today does not yet exist
			EhrMeasureEvent meas = new EhrMeasureEvent();
			meas.DateTEvent=DateTime.Now;
			meas.EventType=EhrMeasureEventType.TobaccoUseAssessed;
			meas.PatNum=PatCur.PatNum;
			meas.MoreInfo=comboSmokeStatus.SelectedItem.ToString();
			EhrMeasureEvents.Insert(meas);
			FillGrid();
		}

		private void butAssessed_Click(object sender,EventArgs e) {
			if(comboSmokeStatus.SelectedIndex==0) {//None
				MessageBox.Show("You must select a smoking status.");
				return;
			}
			for(int i=0;i<listEvents.Count;i++) {
				if(listEvents[i].DateTEvent.Date==DateTime.Today) {
					MessageBox.Show("A Tobacco Assessment entry already exists with today's date.");
					return;
				}
			}
			EhrMeasureEvent meas = new EhrMeasureEvent();
			meas.DateTEvent=DateTime.Now;
			meas.EventType=EhrMeasureEventType.TobaccoUseAssessed;
			meas.PatNum=PatCur.PatNum;
			meas.MoreInfo=comboSmokeStatus.SelectedItem.ToString();
			EhrMeasureEvents.Insert(meas);
			FillGrid();
		}

		private void butCessation_Click(object sender,EventArgs e) {
			if(comboSmokeStatus.SelectedIndex==0) {//None
				MessageBox.Show("You must select a smoking status.");
				return;
			}
			EhrMeasureEvent meas = new EhrMeasureEvent();
			meas.DateTEvent=DateTime.Now;
			meas.EventType=EhrMeasureEventType.TobaccoCessation;
			meas.PatNum=PatCur.PatNum;
			EhrMeasureEvents.Insert(meas);
			FormEhrMeasureEventEdit formM=new FormEhrMeasureEventEdit();
			formM.MeasCur=meas;
			formM.ShowDialog();
			if(formM.DialogResult!=DialogResult.OK) {
				EhrMeasureEvents.Delete(meas.EhrMeasureEventNum);
			}
			FillGrid();
		}

		private void butDelete_Click(object sender,EventArgs e) {
			if(gridMain.SelectedIndices.Length < 1) {
				MessageBox.Show("Please select at least one record to delete.");
				return;
			}
			for(int i=0;i<gridMain.SelectedIndices.Length;i++) {
				EhrMeasureEvents.Delete(listEvents[gridMain.SelectedIndices[i]].EhrMeasureEventNum);
			}
			FillGrid();
		}

		private void butOK_Click(object sender,EventArgs e) {
			if(comboSmokeStatus.SelectedIndex==0//None
				|| comboSmokeStatus.SelectedIndex==-1)//should never happen
			{
				PatCur.SmokingSnoMed="";
			}
			else {
				PatCur.SmokingSnoMed=((SmokingSnoMed)comboSmokeStatus.SelectedIndex-1).ToString().Substring(1);
			}
			Patients.Update(PatCur,PatOld);
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

	

		

	

		


		

	

	


	}
}
