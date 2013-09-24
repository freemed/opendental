using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental;
using OpenDental.UI;
using CodeBase;
using EHR;

namespace OpenDental {
	public partial class FormEHR:Form {
		public long PatNum;
		private Patient PatCur;
		public PatientNote PatNotCur;
		public Family PatFamCur;
		/// <summary>If ResultOnClosing=RxEdit, then this specifies which Rx to open.</summary>
		public long LaunchRxNum;
		///<summary>After this form closes, this can trigger other things to happen.  DialogResult is not checked.  After the other actions are taken, FormEHR opens back up again for seamless user experience.</summary>
		public EhrFormResult ResultOnClosing;
		private List<EhrMu> listMu;
		/// <summary>If ResultOnClosing=MedicationPatEdit, then this specifies which MedicationPat to open.</summary>
		public long LaunchMedicationPatNum;
		///<summary>If set to true, this will cause medical orders window to come up.</summary>
		public bool OnShowLaunchOrders;
		///<summary>This is set every time the form is shown.  It is used to determine if there is an Ehr key for the patient's primary provider.  If not, then The main grid will show empty.</summary>
		private Provider ProvPat;

		public FormEHR() {
			InitializeComponent();
		}

		private void FormEHR_Load(object sender,EventArgs e) {
			//Can't really use this, because it's only loaded once at the very beginning of OD starting up.
		}

		private void FormEHR_Shown(object sender,EventArgs e) {
			ResultOnClosing=EhrFormResult.None;
			PatCur=Patients.GetPat(PatNum);
			ProvPat=Providers.GetProv(PatCur.PriProv);
			labelProvPat.Text=ProvPat.GetLongDesc();
			if(ProvPat.EhrKey=="") {
				labelProvPat.Text+=" (no ehr provider key entered)";
			}
			if(Security.CurUser.ProvNum==0) {
				labelProvUser.Text="none";
			}
			else {
				Provider provUser=Providers.GetProv(Security.CurUser.ProvNum);
				labelProvUser.Text=Providers.GetLongDesc(provUser.ProvNum);
				if(provUser.EhrKey=="") {
					labelProvUser.Text+=" (no ehr provider key entered)";
				}
			}
			FillGridMu();
			if(OnShowLaunchOrders) {
				LaunchOrdersWindow();
				OnShowLaunchOrders=false;
			}
			if(ProvPat.EhrKey=="") {
				MessageBox.Show("No ehr provider key entered for this patient's primary provider.");
			}
		}

		private void FillGridMu() {
			gridMu.BeginUpdate();
			gridMu.Columns.Clear();
			ODGridColumn col=new ODGridColumn("MeasureType",137);
			gridMu.Columns.Add(col);
			col=new ODGridColumn("Met",35,HorizontalAlignment.Center);
			gridMu.Columns.Add(col);
			col=new ODGridColumn("Details",170);
			gridMu.Columns.Add(col);
			col=new ODGridColumn("click to take action",168);
			gridMu.Columns.Add(col);
			col=new ODGridColumn("related actions",142);
			gridMu.Columns.Add(col);
			if(ProvPat.EhrKey=="") {
				listMu=new List<EhrMu>();
			}
			else {
				listMu=EhrMeasures.GetMu(PatCur);
			}
			gridMu.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<listMu.Count;i++) {
				row=new ODGridRow();
				row.Cells.Add(listMu[i].MeasureType.ToString());
				if(listMu[i].Met==MuMet.True) {
					row.Cells.Add("X");
					row.ColorBackG=Color.FromArgb(178,255,178);
				}
				else if(listMu[i].Met==MuMet.NA) {
					row.Cells.Add("N/A");
					row.ColorBackG=Color.FromArgb(178,255,178);
				}
				else {
					row.Cells.Add("");
				}
				row.Cells.Add(listMu[i].Details);
				row.Cells.Add(listMu[i].Action);
				row.Cells.Add(listMu[i].Action2);
				gridMu.Rows.Add(row);
			}
			gridMu.EndUpdate();
		}

		private void gridMu_CellClick(object sender,ODGridClickEventArgs e) {
			FormMedical FormMed;
			if(e.Col==3) {
				switch(listMu[e.Row].MeasureType) {
					case EhrMeasureType.ProblemList:
						FormMed=new FormMedical(PatNotCur,PatCur);
						FormMed.ShowDialog();
						FillGridMu();
						//ResultOnClosing=EhrFormResult.Medical;
						//Close();
						break;
					case EhrMeasureType.MedicationList:
						FormMed=new FormMedical(PatNotCur,PatCur);
						FormMed.ShowDialog();
						FillGridMu();
						//ResultOnClosing=EhrFormResult.Medical;
						//Close();
						break;
					case EhrMeasureType.AllergyList:
						FormMed=new FormMedical(PatNotCur,PatCur);
						FormMed.ShowDialog();
						FillGridMu();
						//ResultOnClosing=EhrFormResult.Medical;
						//Close();
						break;
					case EhrMeasureType.Demographics:
						FormPatientEdit FormPatEdit=new FormPatientEdit(PatCur, PatFamCur);
						FormPatEdit.ShowDialog();
						FillGridMu();
						//ResultOnClosing=EhrFormResult.PatientEdit;
						//Close();
						break;
					case EhrMeasureType.Education:
						FormEduResourcesPat FormEDUPat = new FormEduResourcesPat();
						FormEDUPat.patCur=PatCur;
						FormEDUPat.ShowDialog();
						FillGridMu();
						break;
					case EhrMeasureType.TimelyAccess:
						FormPatientPortal FormPatPort=new FormPatientPortal();
						FormPatPort.ShowDialog();
						FillGridMu();
						//ResultOnClosing=EhrFormResult.Online;
						//Close();
						break;
					case EhrMeasureType.ProvOrderEntry:
					case EhrMeasureType.CPOE_MedOrdersOnly:
					case EhrMeasureType.CPOE_PreviouslyOrdered:
						LaunchOrdersWindow();
						break;
					case EhrMeasureType.Rx:
						//no action available
						break;
					case EhrMeasureType.VitalSigns:
					case EhrMeasureType.VitalSignsBMIOnly:
					case EhrMeasureType.VitalSignsBPOnly:
						FormVitalsigns formVital=new FormVitalsigns();
						formVital.PatNum=PatNum;
						formVital.ShowDialog();
						FillGridMu();
						break;
					//This code will not be used until we certify for EHR 2014. For 2011 certification, vital sign form will look exactly the same as it did at certification time.
					//case EhrMeasureType.VitalSignsBMIOnly:
					//	formVital=new FormVitalsigns();
					//	formVital.PatNum=PatNum;
					//	formVital.IsBMIOnly=true;
					//	formVital.ShowDialog();
					//	FillGridMu();
					//	break;
					//case EhrMeasureType.VitalSignsBPOnly:
					//	formVital=new FormVitalsigns();
					//	formVital.PatNum=PatNum;
					//	formVital.IsBPOnly=true;
					//	formVital.ShowDialog();
					//	FillGridMu();
					//	break;
					case EhrMeasureType.Smoking:
						FormPatientSmoking formPS=new FormPatientSmoking();
						formPS.PatCur=PatCur;
						formPS.ShowDialog();
						PatCur=Patients.GetPat(PatNum);
						FillGridMu();
						break;
					case EhrMeasureType.Lab:
						FormLabPanels formLP=new FormLabPanels();
						formLP.PatCur=PatCur;
						formLP.ShowDialog();
						FillGridMu();
						break;
					case EhrMeasureType.ElectronicCopy:
						FormElectronicCopy formE=new FormElectronicCopy();
						formE.PatCur=PatCur;
						formE.ShowDialog();
						FillGridMu();
						break;
					case EhrMeasureType.ClinicalSummaries:
						FormClinicalSummary FormCS=new FormClinicalSummary();
						FormCS.PatCur=PatCur;
						FormCS.ShowDialog();
						FillGridMu();
						break;
					case EhrMeasureType.Reminders:
						FormReminders FormRem = new FormReminders();
						FormRem.PatCur=PatCur;
						FormRem.ShowDialog();
						FillGridMu();
						break;
					case EhrMeasureType.MedReconcile:
						FormMedicationReconcile FormMedRec=new FormMedicationReconcile();
						FormMedRec.ShowDialog();
						FillGridMu();
						//ResultOnClosing=EhrFormResult.MedReconcile;
						//Close();
						break;
					case EhrMeasureType.SummaryOfCare:
						FormSummaryOfCare FormSoC=new FormSummaryOfCare();
						FormSoC.PatCur=PatCur;
						FormSoC.ShowDialog();
						FillGridMu();
						break;
				}
			}
			if(e.Col==4) {
				switch(listMu[e.Row].MeasureType) {
					case EhrMeasureType.MedReconcile:
						FormReferralsPatient FormRefMed=new FormReferralsPatient();
						FormRefMed.ShowDialog();
						FillGridMu();
						//ResultOnClosing=EhrFormResult.Referrals;
						//Close();
						break;
					case EhrMeasureType.SummaryOfCare:
						FormReferralsPatient FormRefSum=new FormReferralsPatient();
						FormRefSum.ShowDialog();
						FillGridMu();
						//ResultOnClosing=EhrFormResult.Referrals;
						//Close();
						break;
					case EhrMeasureType.Lab:
						FormLabPanelImport formLPI=new FormLabPanelImport();
						formLPI.ShowDialog();
						FillGridMu();
						break;
				}
			}
		}

		///<summary>This can happen when clicking in the grid, or when the form is Shown.  The latter would happen after user unknowingly exited ehr in order to use FormMedPat.  Popping back to the Orders window makes the experience seamless.  This can be recursive if the user edits a series of medicationpats.</summary>
		private void LaunchOrdersWindow() {
			FormMedicalOrders FormOrd = new FormMedicalOrders();
			FormOrd.PatCur=PatCur;
			FormOrd.ShowDialog();
			//if(FormOrd.DialogResult!=DialogResult.OK) {//There is no ok button
			//	return;
			//}
			/*not currently used, but might be if we let users generate Rx from med order.
			if(FormOrd.LaunchRx) {
				if(FormOrd.LaunchRxNum==0) {
					ResultOnClosing=EhrFormResult.RxSelect;
				}
				else {
					ResultOnClosing=EhrFormResult.RxEdit;
					LaunchRxNum=FormOrd.LaunchRxNum;
				}
				Close();
			}
			else*/
			if(FormOrd.LaunchMedicationPat) {
				//if(FormOrd.LaunchMedicationPatNum==0) {
				//	ResultOnClosing=EhrFormResult.MedicationPatNew;//This cannot happen unless a provider is logged in with a valid ehr key
				//}
				//else {
				FormMedPat formMP=new FormMedPat();
				formMP.MedicationPatCur=MedicationPats.GetOne(FormOrd.LaunchMedicationPatNum);
				formMP.ShowDialog();
					//ResultOnClosing=EhrFormResult.MedicationPatEdit;
					//LaunchMedicationPatNum=FormOrd.LaunchMedicationPatNum;
				//}
				//Close();
			//}
			//else {
				FillGridMu();
			}
		}

		private void butMeasures_Click(object sender,EventArgs e) {
			FormEhrMeasures formMeasures=new FormEhrMeasures();
			formMeasures.ShowDialog();
			FillGridMu();
		}

		private void butHash_Click(object sender,EventArgs e) {
			FormHash formH=new FormHash();
			formH.ShowDialog();
		}

		private void butEncryption_Click(object sender,EventArgs e) {
			FormEncryption FormE=new FormEncryption();
			FormE.ShowDialog();
		}

		private void butQuality_Click(object sender,EventArgs e) {
			FormQualityMeasures formQ=new FormQualityMeasures();
			formQ.ShowDialog();
			FillGridMu();
		}

		private void but2014CQM_Click(object sender,EventArgs e) {
			FormQualityMeasures2014 formQ=new FormQualityMeasures2014();
			formQ.ShowDialog();
			FillGridMu();
		}

		private void butVaccines_Click(object sender,EventArgs e) {
			FormVaccines FormVac = new FormVaccines();
			FormVac.PatCur=PatCur;
			FormVac.ShowDialog();
		}

		private void butPatList_Click(object sender,EventArgs e) {
			FormPatList FormPL=new FormPatList();
			FormPL.ElementList=new List<EhrPatListElement>();
			FormPL.ShowDialog();
		}

		private void butPatList14_Click(object sender,EventArgs e) {
			MessageBox.Show("This form was moved to the OpenDental project and should be launched from Reports ?");
			//OpenDental.FormPatList2014 FormPL14=new OpenDental.FormPatList2014();
			//FormPL14.ShowDialog();
		}

		private void butLabPanelLOINC_Click(object sender,EventArgs e) {
			//FormLOINCPicker FormLP = new FormLOINCPicker();
			//FormLP.ShowDialog();
		}

		private void butAmendments_Click(object sender,EventArgs e) {
			FormEhrAmendments FormAmd=new FormEhrAmendments();
			FormAmd.PatCur=PatCur;
			FormAmd.ShowDialog();
		}

		private void butEhrNotPerformed_Click(object sender,EventArgs e) {
			
		}

		private void butEncounters_Click(object sender,EventArgs e) {

		}

		public bool ProvKeyIsValid(string lName,string fName,bool hasReportAccess,string provKey) {
			return ProvKey.ProvKeyIsValid(lName,fName,hasReportAccess,provKey);
		}

		public bool QuarterlyKeyIsValid(string year,string quarter,string practiceTitle,string qkey) {
			return QuarterlyKey.QuarterlyKeyIsValid(year,quarter,practiceTitle,qkey);
		}

		private void butClose_Click(object sender,EventArgs e) {
			Close();
		}


		


	}
}
