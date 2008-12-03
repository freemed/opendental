using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using OpenDentBusiness;
using CodeBase;
using System.Text.RegularExpressions;

namespace OpenDental {
	public partial class FormRpArizonaPrimaryCareEncounter:Form {
		public FormRpArizonaPrimaryCareEncounter() {
			InitializeComponent();
			Lan.F(this);
		}

		private void butOK_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void butBrowse_Click(object sender,EventArgs e) {
			if(folderEncounter.ShowDialog()==DialogResult.OK){
				this.textEncounterFile.Text=this.folderEncounter.SelectedPath;
			}
		}

		private void butCopy_Click(object sender,EventArgs e) {
			Clipboard.SetText(this.textLog.Text);
		}

		private void butRun_Click(object sender,EventArgs e) {
			//The encounter file is a list of all of the appointments provided for Arizona Primary Care patients within the specified
			//date range. Since each encounter/appointment is reimbursed by the local government at a flat rate, we only need to report
			//a single procedure for each appointment in the encounter file and if there is a question by the government as to the other
			//procedures that were performed during a particular appointment, then the dental office can simply look that information up
			//in Open Dental (but no such calls will likely happen). Thus we always use the same Diagnosis code corresponding to the single
			//ADA code that we emit in this flat file, just to keep things simple and workable.
			this.textLog.Text="";
			string outFile=ODFileUtils.CombinePaths(this.textEncounterFolder.Text,this.textEncounterFile.Text);
			if(File.Exists(outFile)) {
				if(MessageBox.Show("The file at "+outFile+" already exists. Overwrite?","Overwrite File?",
					MessageBoxButtons.YesNo)!=DialogResult.Yes) {
					return;
				}
			}
			string command="";
			//Locate the payment definition number for payments of patients using the Arizona Primary Care program.
			command="SELECT DefNum FROM definition WHERE Category="+POut.PInt((int)DefCat.PaymentTypes)+" AND IsHidden=0 AND LOWER(TRIM(ItemName))='noah'";
			DataTable payDefNumTab=General.GetTable(command);
			if(payDefNumTab.Rows.Count!=1) {
				MessageBox.Show("You must define exactly one payment type with the name 'NOAH' before running this report. "+
					"This payment type must be used on payments made by Arizona Primary Care patients.");
				return;
			}
			int payDefNum=PIn.PInt(payDefNumTab.Rows[0][0].ToString());
			string outputText="";
			string patientsIdNumberStr="SPID#";
			//Get the list of all Arizona Primary Care patients, based on the patients which have an insurance carrier named 'noah'
			command="SELECT DISTINCT p.PatNum FROM patplan pp,insplan i,patient p,carrier c "+
				"WHERE p.PatNum=pp.PatNum AND pp.PlanNum=i.PlanNum AND i.CarrierNum=c.CarrierNum "+
				"AND LOWER(TRIM(c.CarrierName))='noah' AND "+
				"(SELECT MAX(a.AptDateTime) FROM appointment a WHERE a.PatNum=p.PatNum AND a.AptStatus="+((int)ApptStatus.Complete)+") BETWEEN "+
					POut.PDate(dateTimeFrom.Value)+" AND "+POut.PDate(dateTimeTo.Value);
			DataTable primaryCarePatients=General.GetTable(command);
			for(int i=0;i<primaryCarePatients.Rows.Count;i++) {
				string patNum=POut.PInt(PIn.PInt(primaryCarePatients.Rows[i][0].ToString()));
				//Now that we have an Arizona Primary Care patient's patNum, we need to see if there are any appointments
				//that the patient has attented (completed) in the date range specified. If there are, then those appointments
				//will be placed into the flat file.
				command="SELECT a.AptNum FROM appointment a WHERE a.PatNum="+patNum;
				DataTable appointmentList=General.GetTable(command);
				for(int j=0;j<appointmentList.Rows.Count;j++){
					string aptNum=POut.PInt(PIn.PInt(appointmentList.Rows[j][0].ToString()));
					command="SELECT "+
						"TRIM((SELECT f.FieldValue FROM patfield f WHERE f.PatNum=p.PatNum AND "+
							"LOWER(f.FieldName)=LOWER('"+patientsIdNumberStr+"') LIMIT 1)) PCIN, "+//Patient's Care ID Number
						"p.BirthDate,"+//birthdate
						"(CASE p.Gender WHEN 0 THEN 'M' WHEN 1 THEN 'F' ELSE '' END) Gender,"+//Gender
						"CONCAT(CONCAT(p.Address,' '),p.Address2) Address,"+//address
						"p.City,"+//city
						"p.State,"+//state
						"p.ZipCode,"+//zipcode
						"(SELECT CASE pp.Relationship WHEN 0 THEN 1 ELSE 0 END FROM patplan pp,insplan i,carrier c WHERE "+//Relationship to subscriber
							"pp.PatNum=p.PatNum AND pp.PlanNum=i.PlanNum AND i.CarrierNum=c.CarrierNum AND LOWER(TRIM(c.CarrierName))='noah') InsRelat,"+
						"(CASE p.Position WHEN 0 THEN 1 WHEN 1 THEN 2 ELSE 3 END) MaritalStatus,"+//Marital status
						"(CASE WHEN p.EmployerNum=0 THEN (CASE WHEN (ADD_DATE(p.BirthDate,18 YEAR)>CURDATE()) THEN 3 ELSE 2 END) ELSE 1 END) EmploymentStatus,"+
						"(CASE p.StudentStatus WHEN 'f' THEN 1 WHEN 'p' THEN 2 ELSE 3 END) StudentStatus,"+//student status
						"'ADHS PCP' InsurancePlanName,"+//insurance plan name
						"'' ReferringPhysicianName,"+//Name of referring physician
						"'' ReferringPhysicianID,"+//ID # of referring physician
						"722 DiagnosisCode1,"+//Diagnosis Code 1. Always set to V72.2 for simplicity and workability
						"'' DiagnosisCode2,"+//Diagnosis code 2
						"'' DiagnosisCode3,"+//Diagnosis code 3
						"'' DiagnosisCode4,"+//Diagnosis code 4
						"(SELECT a.AptDateTime FROM appointment a WHERE a.AptNum="+aptNum+") DateOfEncounter,"+//Date of encounter
						"(SELECT pc.ProcCode FROM procedurecode pc,procedurelog pl "+
							"WHERE pl.AptNum="+aptNum+" AND pl.CodeNum=pc.CodeNum ORDER BY pl.ProcNum LIMIT 1) Procedure1,"+
						"'' Procedure1Modifier1,"+//Procedure modifier 1
						"'' Procedure1Modifier2,"+//Procedure modifier 2
						"'' Procedure1DiagnosisCode,"+//Diagnosis code
						"(SELECT pl.ProcFee FROM procedurelog pl WHERE pl.AptNum="+aptNum+" ORDER BY pl.ProcNum LIMIT 1) Procedure1Charges,"+
						"'' Procedure2,"+//2nd procedure cpt/hcpcs
						"'' Procedure2Modifier1,"+//2nd procedure modifier 1
						"'' Procedure2Modifier2,"+//2nd procedure modifier 2
						"'' Procedure2DiagnosisCode,"+//Diagnosis code
						"0 Procedure2Charges,"+//charges
						"'' Procedure3,"+//3rd procedure cpt/hcpcs
						"'' Procedure3Modifier1,"+//3rd procedure modifier 1
						"'' Procedure3Modifier2,"+//3rd procedure modifier 2
						"'' Procedure3DiagnosisCode,"+//Diagnosis code
						"0 Procedure3Charges,"+//Charges
						"'' Procedure4,"+//4th procedure cpt/hcpcs
						"'' Procedure4Modifier1,"+//4th procedure modifier 1
						"'' Procedure4Modifier2,"+//4th procedure modifier 2
						"'' Procedure4DiagnosisCode,"+//Diagnosis code
						"0 Procedure4Charges,"+//Charges
						"'' Procedure5,"+//5th procedure cpt/hcpcs
						"'' Procedure5Modifier1,"+//5th procedure modifier 1
						"'' Procedure5Modifier2,"+//5th procedure modifier 2
						"'' Procedure5DiagnosisCode,"+//diagnosis code
						"0 Procedure5Charges,"+//Charges
						"'' Procedure6,"+//6th procedure cpt/hcpcs
						"'' Procedure6Modifier1,"+//6th procedure modifier 1
						"'' Procedure6Modifier2,"+//6th procedure modifier 2
						"'' Procedure6DiagnosisCode,"+//Diagnosis code
						"0 Procedure6Charges,"+//Charges
						"(SELECT SUM(pl.ProcFee) FROM procedurelog pl WHERE pl.AptNum="+aptNum+") TotalCharges,"+//Total charges
						"(SELECT SUM(a.AdjAmt) FROM adjustment a WHERE a.PatNum="+patNum+" AND a.AdjType="+
							payDefNum+" AmountPaid,"+//Amount paid
						"0,"+//Balance due
						"TRIM((SELECT cl.Description FROM appointment ap,clinic cl WHERE ap.AptNum="+aptNum+" AND "+
							"ap.ClinicNum=cl.ClinicNum LIMIT 1)) ClinicDescription,"+
						"(SELECT pr.StateLicense FROM provider pr,appointment ap WHERE ap.AptNum="+aptNum+" AND pr.ProvNum=ap.ProvNum LIMIT 1) PhysicianID"+
						"(SELECT CONCAT(pr.FirstName,' ',pr.MiddleI) FROM provider pr,appointment ap "+
							"WHERE ap.AptNum="+aptNum+" AND pr.ProvNum=ap.ProvNum LIMIT 1) PhysicianFAndMNames"+//Physician's first name and middle initial
						"(SELECT pr.LastName FROM provider pr,appointment ap "+
							"WHERE ap.AptNum="+aptNum+" AND pr.ProvNum=ap.ProvNum LIMIT 1) PhysicianLName"+//Physician's last name
						"FROM patient p WHERE "+
						"p.PatNum="+patNum;
					DataTable primaryCareReportRow=General.GetTable(command);
					string outputRow="";
					string rowErrors="";
					string rowWarnings="";
					//Patient's ID Number
					string pcin=primaryCareReportRow.Rows[0]["PCIN"].ToString();
					if(pcin.Length<9) {
						rowErrors+="ERROR: Incorrectly formatted patient data for patient with patnum "+patNum+
							". Patient ID Number '"+pcin+"' is not at least 9 characters long."+Environment.NewLine;
					}
					outputRow+=pcin.PadLeft(15,'0');
					//Patient's date of birth
					outputRow+=PIn.PDate(primaryCareReportRow.Rows[0]["Birthdate"].ToString()).ToString("MMddyyyy");
					//Patient's gender
					outputRow+=PIn.PString(primaryCareReportRow.Rows[0]["Gender"].ToString());
					//Patient's address
					string householdAddress=POut.PString(PIn.PString(primaryCareReportRow.Rows[0]["Address"].ToString()));
					if(householdAddress.Length>29) {
						string newHouseholdAddress=householdAddress.Substring(0,29);
						rowWarnings+="WARNING: Address for patient with patnum of "+patNum+" was longer than 29 characters and "+
							"was truncated in the report ouput. Address was changed from '"+
							householdAddress+"' to '"+newHouseholdAddress+"'"+Environment.NewLine;
						householdAddress=newHouseholdAddress;
					}
					outputRow+=householdAddress.PadRight(29,' ');
					//Patient's city
					string householdCity=POut.PString(PIn.PString(primaryCareReportRow.Rows[0]["City"].ToString()));
					if(householdCity.Length>15) {
						string newHouseholdCity=householdCity.Substring(0,15);
						rowWarnings+="WARNING: City name for patient with patnum of "+patNum+" was longer than 15 characters and "+
							"was truncated in the report ouput. City name was changed from '"+
							householdCity+"' to '"+newHouseholdCity+"'"+Environment.NewLine;
						householdCity=newHouseholdCity;
					}
					outputRow+=householdCity.PadRight(15,' ');
					//Patient's State
					string householdState=POut.PString(PIn.PString(primaryCareReportRow.Rows[0]["State"].ToString()));
					if(householdState.Length>2) {
						string newHouseholdState=householdState.Substring(0,2);
						rowWarnings+="WARNING: State abbreviation for patient with patnum of "+patNum+" was longer than 2 characters and "+
							"was truncated in the report ouput. State abbreviation was changed from '"+
							householdState+"' to '"+newHouseholdState+"'"+Environment.NewLine;
						householdState=newHouseholdState;
					}
					outputRow+=householdState.PadRight(2,' ');
					//Patient's zip code
					string householdZip=POut.PString(PIn.PString(primaryCareReportRow.Rows[0]["Zip"].ToString()));
					if(householdZip.Length>5) {
						string newHouseholdZip=householdZip.Substring(0,5);
						rowWarnings+="WARNING: The zipcode for patient with patnum of "+patNum+" was longer than 5 characters and "+
							"was truncated in the report ouput. The zipcode was changed from '"+
							householdZip+"' to '"+newHouseholdZip+"'"+Environment.NewLine;
						householdZip=newHouseholdZip;
					}
					outputRow+=householdZip.PadRight(5,' ');
					//Patient's relationship to insured.
					string insuranceRelationship=POut.PString(PIn.PString(primaryCareReportRow.Rows[0]["InsRelat"].ToString()));
					if(insuranceRelationship!="1"){//Not self?
						rowWarnings+="WARNING: The patient insurance relationship is not 'self' for the patient with a patnum of "+patNum;
					}
					outputRow+=insuranceRelationship;
					//Patient's marital status
					outputRow+=POut.PString(PIn.PString(primaryCareReportRow.Rows[0]["MaritalStatus"].ToString()));
					//Patient's employment status
					outputRow+=POut.PString(PIn.PString(primaryCareReportRow.Rows[0]["EmploymentStatus"].ToString()));
					//Patient's student status
					outputRow+=POut.PString(PIn.PString(primaryCareReportRow.Rows[0]["StudentStatus"].ToString()));
					//Insurance plan name
					outputRow+=POut.PString(PIn.PString(primaryCareReportRow.Rows[0]["InsurancePlanName"].ToString())).PadRight(25,' ');
					//Name of referring physician.
					outputRow+=POut.PString(PIn.PString(primaryCareReportRow.Rows[0]["ReferringPhysicianName"].ToString())).PadRight(26,' ');
					//ID# of referring physician
					outputRow+=POut.PString(PIn.PString(primaryCareReportRow.Rows[0]["ReferringPhysicianID"].ToString())).PadLeft(6,' ');
					//Diagnosis code 1
					outputRow+=POut.PString(PIn.PString(primaryCareReportRow.Rows[0]["DiagnosisCode1"].ToString())).PadRight(6,' ');
					//Diagnosis code 2
					outputRow+=POut.PString(PIn.PString(primaryCareReportRow.Rows[0]["DiagnosisCode2"].ToString())).PadRight(6,' ');
					//Diagnosis code 3
					outputRow+=POut.PString(PIn.PString(primaryCareReportRow.Rows[0]["DiagnosisCode3"].ToString())).PadRight(6,' ');
					//Diagnosis code 4
					outputRow+=POut.PString(PIn.PString(primaryCareReportRow.Rows[0]["DiagnosisCode4"].ToString())).PadRight(6,' ');
					//Date of encounter
					outputRow+=PIn.PDate(primaryCareReportRow.Rows[0]["DateOfEncounter"].ToString()).ToString("MMddyyyy");
					//Procedure 1
					outputRow+=POut.PString(PIn.PString(primaryCareReportRow.Rows[0]["Procedure1"].ToString())).PadRight(5,' ');
					//Procedure 1 modifier 1
					outputRow+=POut.PString(PIn.PString(primaryCareReportRow.Rows[0]["Procedure1Modifier1"].ToString())).PadRight(2,' ');
					//Procedure 1 modifier 2
					outputRow+=POut.PString(PIn.PString(primaryCareReportRow.Rows[0]["Procedure1Modifier2"].ToString())).PadRight(2,' ');
					//Procedure 1 diagnosis code
					outputRow+=POut.PString(PIn.PString(primaryCareReportRow.Rows[0]["Procedure1DiagnosisCode"].ToString())).PadRight(4,' ');
					//Procedure 1 charges
					outputRow+=Math.Round(PIn.PDouble(primaryCareReportRow.Rows[0]["Procedure1Charges"].ToString())).ToString().PadLeft(6,'0');
					//Procedure 2
					outputRow+=POut.PString(PIn.PString(primaryCareReportRow.Rows[0]["Procedure2"].ToString())).PadRight(5,' ');
					//Procedure 2 modifier 1
					outputRow+=POut.PString(PIn.PString(primaryCareReportRow.Rows[0]["Procedure2Modifier1"].ToString())).PadRight(2,' ');
					//Procedure 2 modifier 2
					outputRow+=POut.PString(PIn.PString(primaryCareReportRow.Rows[0]["Procedure2Modifier2"].ToString())).PadRight(2,' ');
					//Procedure 2 diagnosis code
					outputRow+=POut.PString(PIn.PString(primaryCareReportRow.Rows[0]["Procedure2DiagnosisCode"].ToString())).PadRight(4,' ');
					//Procedure 2 charges
					outputRow+=Math.Round(PIn.PDouble(primaryCareReportRow.Rows[0]["Procedure2Charges"].ToString())).ToString().PadLeft(6,'0');
					//Procedure 3
					outputRow+=POut.PString(PIn.PString(primaryCareReportRow.Rows[0]["Procedure3"].ToString())).PadRight(5,' ');
					//Procedure 3 modifier 1
					outputRow+=POut.PString(PIn.PString(primaryCareReportRow.Rows[0]["Procedure3Modifier1"].ToString())).PadRight(2,' ');
					//Procedure 3 modifier 2
					outputRow+=POut.PString(PIn.PString(primaryCareReportRow.Rows[0]["Procedure3Modifier2"].ToString())).PadRight(2,' ');
					//Procedure 3 diagnosis code
					outputRow+=POut.PString(PIn.PString(primaryCareReportRow.Rows[0]["Procedure3DiagnosisCode"].ToString())).PadRight(4,' ');
					//Procedure 3 charges
					outputRow+=Math.Round(PIn.PDouble(primaryCareReportRow.Rows[0]["Procedure3Charges"].ToString())).ToString().PadLeft(6,'0');
					//Procedure 4
					outputRow+=POut.PString(PIn.PString(primaryCareReportRow.Rows[0]["Procedure4"].ToString())).PadRight(5,' ');
					//Procedure 4 modifier 1
					outputRow+=POut.PString(PIn.PString(primaryCareReportRow.Rows[0]["Procedure4Modifier1"].ToString())).PadRight(2,' ');
					//Procedure 4 modifier 2
					outputRow+=POut.PString(PIn.PString(primaryCareReportRow.Rows[0]["Procedure4Modifier2"].ToString())).PadRight(2,' ');
					//Procedure 4 diagnosis code
					outputRow+=POut.PString(PIn.PString(primaryCareReportRow.Rows[0]["Procedure4DiagnosisCode"].ToString())).PadRight(4,' ');
					//Procedure 4 charges
					outputRow+=Math.Round(PIn.PDouble(primaryCareReportRow.Rows[0]["Procedure4Charges"].ToString())).ToString().PadLeft(6,'0');
					//Procedure 5
					outputRow+=POut.PString(PIn.PString(primaryCareReportRow.Rows[0]["Procedure5"].ToString())).PadRight(5,' ');
					//Procedure 5 modifier 1
					outputRow+=POut.PString(PIn.PString(primaryCareReportRow.Rows[0]["Procedure5Modifier1"].ToString())).PadRight(2,' ');
					//Procedure 5 modifier 2
					outputRow+=POut.PString(PIn.PString(primaryCareReportRow.Rows[0]["Procedure5Modifier2"].ToString())).PadRight(2,' ');
					//Procedure 5 diagnosis code
					outputRow+=POut.PString(PIn.PString(primaryCareReportRow.Rows[0]["Procedure5DiagnosisCode"].ToString())).PadRight(4,' ');
					//Procedure 5 charges
					outputRow+=Math.Round(PIn.PDouble(primaryCareReportRow.Rows[0]["Procedure5Charges"].ToString())).ToString().PadLeft(6,'0');
					//Procedure 6
					outputRow+=POut.PString(PIn.PString(primaryCareReportRow.Rows[0]["Procedure6"].ToString())).PadRight(5,' ');
					//Procedure 6 modifier 1
					outputRow+=POut.PString(PIn.PString(primaryCareReportRow.Rows[0]["Procedure6Modifier1"].ToString())).PadRight(2,' ');
					//Procedure 6 modifier 2
					outputRow+=POut.PString(PIn.PString(primaryCareReportRow.Rows[0]["Procedure6Modifier2"].ToString())).PadRight(2,' ');
					//Procedure 6 diagnosis code
					outputRow+=POut.PString(PIn.PString(primaryCareReportRow.Rows[0]["Procedure6DiagnosisCode"].ToString())).PadRight(4,' ');
					//Procedure 6 charges
					outputRow+=Math.Round(PIn.PDouble(primaryCareReportRow.Rows[0]["Procedure6Charges"].ToString())).ToString().PadLeft(6,'0');
					//Total charges
					outputRow+=Math.Round(PIn.PDouble(primaryCareReportRow.Rows[0]["TotalCharges"].ToString())).ToString().PadLeft(7,'0');
					//Amount paid
					outputRow+=Math.Round(PIn.PDouble(primaryCareReportRow.Rows[0]["AmountPaid"].ToString())).ToString().PadLeft(7,'0');
					//Balance due
					outputRow+=Math.Round(PIn.PDouble(primaryCareReportRow.Rows[0]["BalanceDue"].ToString())).ToString().PadLeft(7,'0');
					//Facility site number
					string siteId=PIn.PString(primaryCareReportRow.Rows[0]["ClinicDescription"].ToString());
					if(siteId=="null"){
						siteId="";
					}
					if(!Regex.IsMatch(siteId,"^.*_[0-9]{5}$")){
						rowErrors+="ERROR: The clinic description for the clinic associated with the last completed appointment "+
							"for the patient with a patnum of "+patNum+" must be the clinic name, follwed by a '_', followed by the 5-digit Site ID Number "+
							"for the clinic. i.e. ClinicName_12345. The current clinic description is '"+siteId+"'."+Environment.NewLine;
					}else{
						siteId=siteId.Substring(siteId.Length-5);
					}
					outputRow+=siteId;
					//string physicianId=PIn.PString(primaryCareReportRow[0]["PhysicianID"].ToString());
					//if(physicianId.Length>
				}
			}
			File.WriteAllText(outFile,outputText);
			MessageBox.Show("Done.");
		}

	}
}