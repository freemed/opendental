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
		private List<EhrMeasureEvent> _ListEvents;
		private List<Intervention> _ListInterventions;
		private List<EhrCode> _ListAssessmentCodes;
		private List<MedicationPat> _ListMedPats;
		private string _TobaccoCodeSelected;

		public FormEhrPatientSmoking() {
			InitializeComponent();
		}

		private void FormPatientSmoking_Load(object sender,EventArgs e) {
			#region ComboSmokeStatus
			comboSmokeStatus.Items.Add("None");//First and default index
			//Smoking statuses add in the same order as they appear in the SmokingSnoMed enum (Starting at comboSmokeStatus index 1). Changes to the enum order will change the order added so they will always match
			for(int i=0;i<Enum.GetNames(typeof(SmokingSnoMed)).Length;i++) {
				//if snomed code exists in the snomed table, use the code - description for the combo box, otherwise use the original abbreviated description
				Snomed smokeCur=Snomeds.GetByCode(((SmokingSnoMed)i).ToString().Substring(1));
				if(smokeCur!=null) {
					comboSmokeStatus.Items.Add(smokeCur.Description);
				}
				else {
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
			}
			PatOld=PatCur.Copy();
			_TobaccoCodeSelected=PatCur.SmokingSnoMed;
			if(_TobaccoCodeSelected=="") {
				comboSmokeStatus.SelectedIndex=0;//None
			}
			else {
				try {
					comboSmokeStatus.SelectedIndex=(int)Enum.Parse(typeof(SmokingSnoMed),"_"+_TobaccoCodeSelected,true)+1;
				}
				catch {
					//if not one of the statuses in the enum, get the Snomed object from the patient's current smoking snomed code
					Snomed smokeCur=Snomeds.GetByCode(_TobaccoCodeSelected);
					if(smokeCur!=null) {//valid snomed code, set the combo box text to this snomed description
						comboSmokeStatus.Text=smokeCur.Description;
					}
				}
			}
			#endregion
			FillGrid();
			#region ComboAssessmentType
			_ListAssessmentCodes=EhrCodes.GetForValueSetOIDs(new List<string> { "2.16.840.1.113883.3.526.3.1278" },true);//'Tobacco Use Screening' value set
			if(_ListAssessmentCodes.Count==0) {//This should only happen if the EHR.dll does not exist or if the codes in the ehrcode list do not exist in the corresponding table
				MsgBox.Show(this,"The codes used for Tobacco Use Screening assessments do not exist in the LOINC table in your database.  You must run the Code System Importer tool in Setup | EHR to import this code set.");
				DialogResult=DialogResult.Cancel;
				return;
			}
			EhrMeasureEvent mostRecentAssessment=new EhrMeasureEvent();
			for(int i=_ListEvents.Count-1;i>-1;i--) {
				if(_ListEvents[i].EventType==EhrMeasureEventType.TobaccoUseAssessed) {
					mostRecentAssessment=_ListEvents[i];//_ListEvents filled ordered by DateTEvent, most recent assessment is last one in the list of type assessed
				}
			}
			for(int i=0;i<_ListAssessmentCodes.Count;i++) {
				comboAssessmentType.Items.Add(_ListAssessmentCodes[i].Description);
				if(i==0) {
					comboAssessmentType.SelectedIndex=i;//default to the first one in the list, 'History of tobacco use Narrative'
				}
				if(mostRecentAssessment.CodeValue==_ListAssessmentCodes[i].CodeValue && mostRecentAssessment.CodeSystem==_ListAssessmentCodes[i].CodeSystem) {
					comboAssessmentType.SelectedIndex=i;//set to most recent assessment
				}
			}
			#endregion
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
			gridMain.Rows.Clear();
			ODGridRow row;
			#region AssessedEvents
			_ListEvents=EhrMeasureEvents.RefreshByType(PatCur.PatNum,EhrMeasureEventType.TobaccoUseAssessed);
			for(int i=0;i<_ListEvents.Count;i++) {
				row=new ODGridRow();
				row.Cells.Add(_ListEvents[i].DateTEvent.ToString());
				row.Cells.Add(_ListEvents[i].EventType.ToString());
				row.Cells.Add(_ListEvents[i].MoreInfo);
				gridMain.Rows.Add(row);
			}
			#endregion
			#region CessationInterventions
			_ListInterventions=Interventions.Refresh(PatCur.PatNum,InterventionCodeSet.TobaccoCessation);
			for(int i=0;i<_ListInterventions.Count;i++) {
				row=new ODGridRow();
				row.Cells.Add(_ListInterventions[i].DateEntry.ToShortDateString());
				row.Cells.Add(InterventionCodeSet.TobaccoCessation.ToString()+" Counseling");
				string descript="";
				switch(_ListInterventions[i].CodeSystem) {
					case "CPT":
						Cpt cptCur=Cpts.GetByCode(_ListInterventions[i].CodeValue);
						if(cptCur!=null) {
							descript=cptCur.Description;
						}
						break;
					case "SNOMEDCT":
						Snomed sCur=Snomeds.GetByCode(_ListInterventions[i].CodeValue);
						if(sCur!=null) {
							descript=sCur.Description;
						}
						break;
				}
				row.Cells.Add(_ListInterventions[i].CodeValue+" - "+descript);
			}
			#endregion
			#region CessationMedications
			_ListMedPats=MedicationPats.Refresh(PatCur.PatNum,true);
			List<EhrCode> listEhrMeds=EhrCodes.GetForValueSetOIDs(new List<string> { "2.16.840.1.113883.3.526.3.1190" },true);//Tobacco Use Cessation Pharmacotherapy Value Set
			//listEhrMeds will contain 41 medications for tobacco cessation if those exist in the rxnorm table
			for(int i=_ListMedPats.Count-1;i>-1;i--) {
				bool found=false;
				for(int j=0;j<listEhrMeds.Count;j++) {
					if(_ListMedPats[i].RxCui.ToString()==listEhrMeds[j].CodeValue) {
						found=true;
						break;
					}
				}
				if(!found) {
					_ListMedPats.RemoveAt(i);
				}
			}
			for(int i=0;i<_ListMedPats.Count;i++) {
				row=new ODGridRow();
				string dateRange="";
				if(_ListMedPats[i].DateStart.Year>1880) {
					dateRange=_ListMedPats[i].DateStart.ToShortDateString();
				}
				if(_ListMedPats[i].DateStop.Year>1880) {
					if(dateRange!="") {
						dateRange+=" - ";
					}
					dateRange+=_ListMedPats[i].DateStop.ToShortDateString();
				}
				if(dateRange=="") {
					dateRange=_ListMedPats[i].DateTStamp.ToString();
				}
				row.Cells.Add(dateRange);
				row.Cells.Add(InterventionCodeSet.TobaccoCessation.ToString()+" Medication");
				string medDescript=RxNorms.GetDescByRxCui(_ListMedPats[i].RxCui.ToString());
				row.Cells.Add(_ListMedPats[i].RxCui.ToString()+" - "+medDescript);
			}
			#endregion
			gridMain.EndUpdate();
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			if(_ListEvents[e.Row].EventType!=EhrMeasureEventType.TobaccoCessation) {
				MessageBox.Show("Only Tobacco Cessation entries may be edited.");
				return;
			}
			FormEhrMeasureEventEdit formM=new FormEhrMeasureEventEdit();
			formM.MeasCur=_ListEvents[e.Row];
			formM.ShowDialog();
			FillGrid();
		}

		private void comboSmokeStatus_SelectionChangeCommitted(object sender,EventArgs e) {
			if(comboSmokeStatus.SelectedIndex==0) {//If None, do not create an event
				return;
			}
			//Automatically make an entry
			for(int i=0;i<_ListEvents.Count;i++) {
				if(_ListEvents[i].DateTEvent.Date==DateTime.Today) {
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
			for(int i=0;i<_ListEvents.Count;i++) {
				if(_ListEvents[i].DateTEvent.Date==DateTime.Today) {
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

		private void butIntervention_Click(object sender,EventArgs e) {
			if(comboSmokeStatus.SelectedIndex==0) {//None
				MessageBox.Show("You must select a smoking status.");
				return;
			}
			FormInterventionEdit FormInt=new FormInterventionEdit();
			FormInt.InterventionCur=new Intervention();
			FormInt.InterventionCur.IsNew=true;
			FormInt.InterventionCur.PatNum=PatCur.PatNum;
			FormInt.InterventionCur.ProvNum=PatCur.PriProv;
			FormInt.InterventionCur.DateEntry=PIn.Date(textDateAssessed.Text);
			FormInt.InterventionCur.CodeSet=InterventionCodeSet.TobaccoCessation;
			FormInt.IsAllTypes=false;
			FormInt.IsSelectionMode=true;
			FormInt.ShowDialog();
			if(FormInt.DialogResult==DialogResult.OK) {
				FillGrid();
			}
			FillGrid();
		}

		private void butSnomed_Click(object sender,EventArgs e) {
			FormSnomeds FormS=new FormSnomeds();
			FormS.IsSelectionMode=true;
			FormS.ShowDialog();
			if(FormS.DialogResult!=DialogResult.OK) {
				return;
			}
			for(int i=0;i<Enum.GetNames(typeof(SmokingSnoMed)).Length;i++) {
				if(FormS.SelectedSnomed.SnomedCode==((SmokingSnoMed)i).ToString().Substring(1)) {
					comboSmokeStatus.SelectedIndex=i+1;
					_TobaccoCodeSelected=((SmokingSnoMed)i).ToString().Substring(1);
					return;
				}
			}
			_TobaccoCodeSelected=FormS.SelectedSnomed.SnomedCode;
			comboSmokeStatus.SelectedIndex=-1;
			comboSmokeStatus.Text=FormS.SelectedSnomed.Description;
		}

		private void butDelete_Click(object sender,EventArgs e) {
			if(gridMain.SelectedIndices.Length < 1) {
				MessageBox.Show("Please select at least one record to delete.");
				return;
			}
			for(int i=0;i<gridMain.SelectedIndices.Length;i++) {
				EhrMeasureEvents.Delete(_ListEvents[gridMain.SelectedIndices[i]].EhrMeasureEventNum);
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
				string smokeStatusSelected="";
				try {
					smokeStatusSelected=((SmokingSnoMed)comboSmokeStatus.SelectedIndex-1).ToString().Substring(1);
				}
				catch {//selected index was greater than 0, but casting to SmokingSnoMed failed so it must not be one of the enum values, get the snomed object from the snomed table
					smokeStatusSelected=comboSmokeStatus.SelectedItem.ToString().Split(new char[] { ' ' },StringSplitOptions.RemoveEmptyEntries)[0];
				}
				PatCur.SmokingSnoMed=smokeStatusSelected;
			}
			Patients.Update(PatCur,PatOld);
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

	

		

	

		


		

	

	


	}
}
