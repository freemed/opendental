using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.UI;
using CodeBase;
using System.Reflection;
#if EHRTEST
using EHR;
#endif

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
		///<summary>This will be null if EHR didn't load up.  EHRTEST conditional compilation constant is used because the EHR project is only part of the solution here at HQ.  We need to use late binding in a few places so that it will still compile for people who download our sourcecode.  But late binding prevents us from stepping through for debugging, so the EHRTEST lets us switch to early binding.</summary>
		public static object ObjFormEhrMeasures;
		///<summary>This will be null if EHR didn't load up.</summary>
		public static Assembly AssemblyEHR;

		public FormEHR() {
			InitializeComponent();
			if(PrefC.GetBoolSilent(PrefName.ShowFeatureEhr,false)) {
				constructObjFormEhrMeasuresHelper();
			}
		}

		///<summary>Constructs the ObjFormEhrMeasures fro use with late binding.</summary>
		private static void constructObjFormEhrMeasuresHelper() {
			string dllPathEHR=ODFileUtils.CombinePaths(Application.StartupPath,"EHR.dll");
			ObjFormEhrMeasures=null;
			AssemblyEHR=null;
			if(File.Exists(dllPathEHR)) {//EHR.dll is available, so load it up
				AssemblyEHR=Assembly.LoadFile(dllPathEHR);
				Type type=AssemblyEHR.GetType("EHR.FormEhrMeasures");//namespace.class
				ObjFormEhrMeasures=Activator.CreateInstance(type);
				return;
			}
			#if EHRTEST
				ObjFormEhrMeasures=new FormEhrMeasures();
			#endif
		}

		///<summary>Loads a resource file from the EHR assembly and returns the file text as a string.
		///Returns "" is the EHR assembly did not load. strResourceName can be either "CCD" or "CCR".
		///This function performs a late binding to the EHR.dll, because resellers do not have EHR.dll necessarily.</summary>
		public static string GetEhrResource(string strResourceName) {
			if(AssemblyEHR==null) {
				constructObjFormEhrMeasuresHelper();
				if(AssemblyEHR==null) {
					return "";
				}
			}
			Stream stream=AssemblyEHR.GetManifestResourceStream("EHR.Properties.Resources.resources");
			System.Resources.ResourceReader resourceReader=new System.Resources.ResourceReader(stream);
			string strResourceType="";
			byte[] arrayResourceBytes=null;
			resourceReader.GetResourceData(strResourceName,out strResourceType,out arrayResourceBytes);
			resourceReader.Dispose();
			stream.Dispose();
			MemoryStream ms=new MemoryStream(arrayResourceBytes);
			BinaryReader br=new BinaryReader(ms);
			string retVal=br.ReadString();//Removes the leading binary characters from the string.
			ms.Dispose();
			br.Dispose();
			return retVal;
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
				//LaunchOrdersWindow();
				OnShowLaunchOrders=false;
			}
			if(ProvPat.EhrKey=="") {
				MessageBox.Show("No ehr provider key entered for this patient's primary provider.");
			}
		}

		private void FillGridMu() {
			gridMu.BeginUpdate();
			gridMu.Columns.Clear();
			ODGridColumn col=new ODGridColumn("MeasureType",145);
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
				if(PrefC.GetBool(PrefName.MeaningfulUseTwo)) {
					listMu=EhrMeasures.GetMu2(PatCur);
				}
				else {
					listMu=EhrMeasures.GetMu(PatCur);
				}
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
						FormEhrEduResourcesPat FormEDUPat = new FormEhrEduResourcesPat();
						FormEDUPat.patCur=PatCur;
						FormEDUPat.ShowDialog();
						FillGridMu();
						break;
					case EhrMeasureType.TimelyAccess:
						FormPatientPortal FormPatPort=new FormPatientPortal();
						FormPatPort.PatCur=PatCur;
						FormPatPort.ShowDialog();
						FillGridMu();
						//ResultOnClosing=EhrFormResult.Online;
						//Close();
						break;
					case EhrMeasureType.ProvOrderEntry:
					case EhrMeasureType.CPOE_MedOrdersOnly:
					case EhrMeasureType.CPOE_PreviouslyOrdered:
						//LaunchOrdersWindow();
						break;
					case EhrMeasureType.Rx:
						//no action available
						break;
					case EhrMeasureType.VitalSigns:
					case EhrMeasureType.VitalSignsBMIOnly:
					case EhrMeasureType.VitalSignsBPOnly:
					case EhrMeasureType.VitalSigns2014:
						FormVitalsigns FormVital=new FormVitalsigns();
						FormVital.PatNum=PatNum;
						FormVital.ShowDialog();
						FillGridMu();
						break;
					case EhrMeasureType.Smoking:
						FormEhrPatientSmoking FormPS=new FormEhrPatientSmoking();
						FormPS.PatCur=PatCur;
						FormPS.ShowDialog();
						PatCur=Patients.GetPat(PatNum);
						FillGridMu();
						break;
					case EhrMeasureType.Lab:
						FormEhrLabOrders FormLP=new FormEhrLabOrders();
						FormLP.PatCur=PatCur;
						FormLP.ShowDialog();
						FillGridMu();
						break;
					case EhrMeasureType.ElectronicCopy:
						if(listMu[e.Row].Action=="Provide elect copy to Pt") {
							FormEhrElectronicCopy FormE=new FormEhrElectronicCopy();
							FormE.PatCur=PatCur;
							FormE.ShowDialog();
							FillGridMu();
						}
						break;
					case EhrMeasureType.ClinicalSummaries:
						FormEhrClinicalSummary FormCS=new FormEhrClinicalSummary();
						FormCS.PatCur=PatCur;
						FormCS.ShowDialog();
						FillGridMu();
						break;
					case EhrMeasureType.Reminders:
						FormEhrReminders FormRem = new FormEhrReminders();
						FormRem.PatCur=PatCur;
						FormRem.ShowDialog();
						FillGridMu();
						break;
					case EhrMeasureType.MedReconcile:
						FormEhrSummaryOfCare FormMedRec=new FormEhrSummaryOfCare();
						FormMedRec.PatCur=PatCur;
						FormMedRec.ShowDialog();
						FillGridMu();
						//ResultOnClosing=EhrFormResult.MedReconcile;
						//Close();
						break;
					case EhrMeasureType.SummaryOfCare:
						FormEhrSummaryOfCare FormSoC=new FormEhrSummaryOfCare();
						FormSoC.PatCur=PatCur;
						FormSoC.ShowDialog();
						FillGridMu();
						break;
					case EhrMeasureType.SummaryOfCareElectronic:
						FormEhrSummaryOfCare FormSoCE=new FormEhrSummaryOfCare();
						FormSoCE.PatCur=PatCur;
						FormSoCE.ShowDialog();
						FillGridMu();
						break;
					case EhrMeasureType.SecureMessaging:
						//Patient Sent
						break;
					case EhrMeasureType.FamilyHistory:
						FormMed=new FormMedical(PatNotCur,PatCur);
						FormMed.ShowDialog();
						FillGridMu();
						break;
					case EhrMeasureType.ElectronicNote:
						//Sign a Note
						break;
					case EhrMeasureType.CPOE_LabOrdersOnly:
						//Click on Lab Edit
						break;
					case EhrMeasureType.CPOE_RadiologyOrdersOnly:
						//Click on Lab Edit
						break;
					case EhrMeasureType.LabImages:
						FormEhrLabOrders FormLO=new FormEhrLabOrders();
						FormLO.PatCur=PatCur;
						FormLO.ShowDialog();
						FillGridMu();
						break;
				}
			}
			if(e.Col==4) {
				switch(listMu[e.Row].MeasureType) {
					case EhrMeasureType.MedReconcile:
						FormReferralsPatient FormRefMed=new FormReferralsPatient();
						FormRefMed.PatNum=PatCur.PatNum;
						FormRefMed.ShowDialog();
						FillGridMu();
						//ResultOnClosing=EhrFormResult.Referrals;
						//Close();
						break;
					case EhrMeasureType.SummaryOfCare:
						FormReferralsPatient FormRefSum=new FormReferralsPatient();
						FormRefSum.PatNum=PatCur.PatNum;
						FormRefSum.ShowDialog();
						FillGridMu();
						//ResultOnClosing=EhrFormResult.Referrals;
						//Close();
						break;
					case EhrMeasureType.Lab:
						//Redundant now that everything is done from one window
						break;
				}
			}
		}

		///<summary>This can happen when clicking in the grid, or when the form is Shown.  The latter would happen after user unknowingly exited ehr in order to use FormMedPat.  Popping back to the Orders window makes the experience seamless.  This can be recursive if the user edits a series of medicationpats.</summary>
		private void LaunchOrdersWindow() {
			FormEhrMedicalOrders FormOrd = new FormEhrMedicalOrders();
			FormOrd._patCur=PatCur;
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
				FormMedPat FormMP=new FormMedPat();
				FormMP.MedicationPatCur=MedicationPats.GetOne(FormOrd.LaunchMedicationPatNum);
				FormMP.ShowDialog();
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
			#if EHRTEST
				ObjFormEhrMeasures=new EHR.FormEhrMeasures();
				((EHR.FormEhrMeasures)ObjFormEhrMeasures).ShowDialog();
			#else
				if(ObjFormEhrMeasures==null) {
					return;
				}
				Type type=AssemblyEHR.GetType("EHR.FormEhrMeasures");//namespace.class
				type.InvokeMember("ShowDialog",System.Reflection.BindingFlags.InvokeMethod,null,ObjFormEhrMeasures,null);
			#endif
			FillGridMu();
		}

		private void butHash_Click(object sender,EventArgs e) {
			FormEhrHash FormH=new FormEhrHash();
			FormH.ShowDialog();
		}

		private void butEncryption_Click(object sender,EventArgs e) {
			FormEhrEncryption FormE=new FormEhrEncryption();
			FormE.ShowDialog();
		}

		private void butQuality_Click(object sender,EventArgs e) {
			FormEhrQualityMeasures FormQ=new FormEhrQualityMeasures();
			FormQ.ShowDialog();
			FillGridMu();
		}

		private void but2014CQM_Click(object sender,EventArgs e) {
			FormEhrQualityMeasures2014 FormQ=new FormEhrQualityMeasures2014();
	FormQ.ShowDialog();
			FillGridMu();
		}

		private void butVaccines_Click(object sender,EventArgs e) {
			FormEhrVaccines FormVac = new FormEhrVaccines();
			FormVac.PatCur=PatCur;
			FormVac.ShowDialog();
		}

		private void butPatList_Click(object sender,EventArgs e) {
			FormEhrPatList FormPL=new FormEhrPatList();
			FormPL.ElementList=new List<EhrPatListElement>();
			FormPL.ShowDialog();
		}

		private void butPatList14_Click(object sender,EventArgs e) {
			MessageBox.Show("This form was moved to the OpenDental project and should be launched from Reports ?");
			//OpenDental.FormPatList2014 FormPL14=new OpenDental.FormPatList2014();
			//FormPL14.ShowDialog();
		}

		private void butLabPanelLOINC_Click(object sender,EventArgs e) {
			//TODO: Add this to the Grid instead of the button
			FormEhrLabOrders FormEHRLO=new FormEhrLabOrders();
			FormEHRLO.PatCur=PatCur;
			FormEHRLO.ShowDialog();
		}

		private void butAmendments_Click(object sender,EventArgs e) {
			FormEhrAmendments FormAmd=new FormEhrAmendments();
			FormAmd.PatCur=PatCur;
			FormAmd.ShowDialog();
		}

		private void butEhrNotPerformed_Click(object sender,EventArgs e) {
			FormEhrNotPerformed FormNP=new FormEhrNotPerformed();
			FormNP.PatCur=PatCur;
			FormNP.ShowDialog();
		}

		private void butEncounters_Click(object sender,EventArgs e) {
			FormEncounters FormEnc=new FormEncounters();
			FormEnc.PatCur=PatCur;
			FormEnc.ShowDialog();
		}

		private void butInterventions_Click(object sender,EventArgs e) {
			FormInterventions FormInt=new FormInterventions();
			FormInt.PatCur=PatCur;
			FormInt.ShowDialog();
		}

		private void butCarePlans_Click(object sender,EventArgs e) {
			FormEhrCarePlans FormCP=new FormEhrCarePlans(PatCur);
			FormCP.ShowDialog();
		}

		private void but2011Labs_Click(object sender,EventArgs e) {
			LaunchOrdersWindow();
		}

		public static bool ProvKeyIsValid(string lName,string fName,bool hasReportAccess,string provKey) {
			try{
				#if EHRTEST //This pattern allows the code to compile without having the EHR code available.
					return FormEhrMeasures.ProvKeyIsValid(lName,fName,hasReportAccess,provKey);
				#else
					constructObjFormEhrMeasuresHelper();
					Type type=AssemblyEHR.GetType("EHR.FormEhrMeasures");//namespace.class
					object[] args=new object[] { lName,fName,hasReportAccess,provKey };
					return (bool)type.InvokeMember("ProvKeyIsValid",System.Reflection.BindingFlags.InvokeMethod,null,ObjFormEhrMeasures,args);
				#endif
			}
			catch{
				return false;
			}
		}

		public static bool QuarterlyKeyIsValid(string year,string quarter,string practiceTitle,string qkey) {
			try{
				#if EHRTEST //This pattern allows the code to compile without having the EHR code available.
					return FormEhrMeasures.QuarterlyKeyIsValid(year,quarter,practiceTitle,qkey);
				#else
					constructObjFormEhrMeasuresHelper();
					Type type=AssemblyEHR.GetType("EHR.FormEhrMeasures");//namespace.class
					object[] args=new object[] { year,quarter,practiceTitle,qkey };
					return (bool)type.InvokeMember("QuarterlyKeyIsValid",System.Reflection.BindingFlags.InvokeMethod,null,ObjFormEhrMeasures,args);
				#endif
			}
			catch{
				return false;
			}
		}

		private void butClose_Click(object sender,EventArgs e) {
			Close();
		}

		


		


	}
}
