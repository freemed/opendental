using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
using System.IO;
using System.Text.RegularExpressions;
using CodeBase;

namespace OpenDental {
	public partial class FormRpArizonaPrimaryCareElegibility:Form {

		public FormRpArizonaPrimaryCareElegibility() {
			InitializeComponent();
			Lan.F(this);
		}

		private void butFinished_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void butBrowse_Click(object sender,EventArgs e) {
			if(folderElegibilityPath.ShowDialog()==DialogResult.OK){
				textElegibilityFolder.Text=folderElegibilityPath.SelectedPath;
			}
		}

		private void butCopy_Click(object sender,EventArgs e) {
			Clipboard.SetText(this.textLog.Text);
		}

		private void butRun_Click(object sender,EventArgs e) {
			this.textLog.Text="";
			string outFile=ODFileUtils.CombinePaths(textElegibilityFolder.Text,textElegibilityFile.Text);
			if(File.Exists(outFile)) {
				if(MessageBox.Show("The file at "+outFile+" already exists. Overwrite?","Overwrite File?",
					MessageBoxButtons.YesNo)!=DialogResult.Yes) {
					return;
				}
			}
			string outputText="";
			string patientsIdNumberStr="SPID#";
			string householdGrossIncomeStr="Income";
			string householdPercentOfPovertyStr="Percent of Poverty";
			string statusStr="Patient Status";
			string command="";
			//Locate the payment definition number for copayments of patients using the Arizona Primary Care program.
			command="SELECT DefNum FROM definition WHERE Category="+POut.PInt((int)DefCat.PaymentTypes)+" AND IsHidden=0 AND LOWER(TRIM(ItemName))='noah'";
			DataTable copayDefNumTab=General.GetTable(command);
			if(copayDefNumTab.Rows.Count!=1){
				MessageBox.Show("You must define exactly one payment type with the name 'NOAH' before running this report. "+
					"This payment type must be used on copayments made by Arizona Primary Care patients.");
				return;
			}
			int copayDefNum=PIn.PInt(copayDefNumTab.Rows[0][0].ToString());
			//Get the list of all Arizona Primary Care patients, based on the patient's available in the
			//patfieldef table.
			//command="SELECT DISTINCT PatNum FROM patfield WHERE LOWER(FieldName) IN ("+
			//	"LOWER('"+patientsIdNumberStr+"'),LOWER('"+householdGrossIncomeStr+"'),LOWER('"+householdPercentOfPovertyStr+"'),LOWER('"+statusStr+"'))";
			command="SELECT DISTINCT p.PatNum FROM patplan pp,insplan i,patient p,carrier c "+
				"WHERE p.PatNum=pp.PatNum AND pp.PlanNum=i.PlanNum AND i.CarrierNum=c.CarrierNum AND LOWER(TRIM(c.CarrierName))='noah'";
			DataTable primaryCarePatients=General.GetTable(command);
			for(int i=0;i<primaryCarePatients.Rows.Count;i++) {
				string patNum=POut.PInt(PIn.PInt(primaryCarePatients.Rows[i][0].ToString()));
				command="SELECT "+
					"(SELECT f.FieldValue FROM patfield f WHERE f.PatNum=p.PatNum AND "+
						"LOWER(f.FieldName)=LOWER('"+patientsIdNumberStr+"') LIMIT 1) PCIN, "+//Patient's Care ID Number
					""+//TODO: Site ID Number
					"p.BirthDate,"+
					"CASE p.Position WHEN "+((int)PatientPosition.Single)+" THEN 1 "+
						"WHEN "+((int)PatientPosition.Married)+" THEN 2 ELSE 3 END MaritalStatus,"+//Marital status
					"CASE p.Race WHEN "+((int)PatientRace.Asian)+" THEN 'A' WHEN "+((int)PatientRace.HispanicLatino)+" THEN 'H' "+
						"WHEN "+((int)PatientRace.HawaiiOrPacIsland)+" THEN 'P' WHEN "+((int)PatientRace.AfricanAmerican)+" THEN 'B' "+
						"WHEN "+((int)PatientRace.AmericanIndian)+" THEN 'I' WHEN "+((int)PatientRace.White)+" THEN 'W' ELSE 'O' END PatRace,"+
					"CONCAT(CONCAT(TRIM(p.Address),' '),TRIM(p.Address2)) HouseholdAddress,"+//Patient address
					"p.City HouseholdCity,"+//Household residence city
					"p.State HouseholdState,"+//Household residence state
					"p.Zip HouseholdZip,"+//Household residence zip code
					"(SELECT f.FieldValue FROM patfield f WHERE f.PatNum=p.PatNum AND "+
						"LOWER(f.FieldName)=LOWER('"+householdGrossIncomeStr+"') LIMIT 1) HGI, "+//Household gross income
					"(SELECT f.FieldValue FROM patfield f WHERE f.PatNum=p.PatNum AND "+
						"LOWER(f.FieldName)=LOWER('"+householdPercentOfPovertyStr+"') LIMIT 1) HPP, "+//Household % of poverty
					"(SELECT a.AdjAmt FROM adjustment a WHERE a.PatNum="+patNum+" AND a.AdjType="+
						copayDefNum+" ORDER BY AdjDate DESC LIMIT 1) HSFS,"+//Household sliding fee scale
					"(SELECT i.DateTerm FROM insplan i,patplan pp WHERE pp.PatNum="+patNum+" AND pp.PlanNum=i.PlanNum LIMIT 1)"+//Date of elegibility status
					"(SELECT f.FieldValue FROM patfield f WHERE f.PatNum=p.PatNum AND "+
						"LOWER(f.FieldName)=LOWER('"+statusStr+"') LIMIT 1) CareStatus "+//Status
					"FROM patient p WHERE "+
					"p.PatNum="+patNum;
				DataTable primaryCareReportRow=General.GetTable(command);
				if(primaryCareReportRow.Rows.Count!=1) {
					//Either the results are ambiguous or for some reason, the patient number listed in the patfield table
					//does not actually exist. In either of these cases, it makes the most sense to just skip this patient
					//and continue with the rest of the reporting.
					continue;
				}
				string outputRow="";
				string rowErrors="";
				string rowWarnings="";
				string pcin=PIn.PString(primaryCareReportRow.Rows[0]["PCIN"].ToString());
				if(pcin.Length!=9) {
					rowErrors+="ERROR: Incorrectly formatted patient data for patient with patnum "+patNum+
						". Patient ID Number '"+pcin+"' is not 9 characters long."+Environment.NewLine;
				}
				outputRow+=pcin.PadLeft(15,'0');//Patient's ID Number
				outputRow+="";//TODO: Site ID Number
				outputRow+=PIn.PDate(primaryCareReportRow.Rows[0]["Birthdate"].ToString()).ToString("MMddyyyy");//Patient's Date of Birth
				outputRow+=POut.PInt(PIn.PInt(primaryCareReportRow.Rows[0]["MaritalStatus"].ToString()));
				outputRow+=primaryCareReportRow.Rows[0]["PatRace"].ToString();
				//Household residence address
				string householdAddress=POut.PString(PIn.PString(primaryCareReportRow.Rows[0]["HouseholdAddress"].ToString()));
				if(householdAddress.Length>29) {
					string newHouseholdAddress=householdAddress.Substring(0,29);
					rowWarnings+="WARNING: Address for patient with patnum of "+patNum+" was longer than 29 characters and "+
						"was truncated in the report ouput. Address was changed from '"+
						householdAddress+"' to '"+newHouseholdAddress+"'"+Environment.NewLine;
					householdAddress=newHouseholdAddress;
				}
				outputRow+=householdAddress.PadRight(29,' ');
				//Household residence city
				string householdCity=POut.PString(PIn.PString(primaryCareReportRow.Rows[0]["HouseholdCity"].ToString()));
				if(householdCity.Length>15) {
					string newHouseholdCity=householdCity.Substring(0,15);
					rowWarnings+="WARNING: City name for patient with patnum of "+patNum+" was longer than 15 characters and "+
						"was truncated in the report ouput. City name was changed from '"+
						householdCity+"' to '"+newHouseholdCity+"'"+Environment.NewLine;
					householdCity=newHouseholdCity;
				}
				outputRow+=householdCity.PadRight(15,' ');
				//Household residence state
				string householdState=POut.PString(PIn.PString(primaryCareReportRow.Rows[0]["HouseholdState"].ToString()));
				if(householdState.Length>2) {
					string newHouseholdState=householdState.Substring(0,2);
					rowWarnings+="WARNING: State abbreviation for patient with patnum of "+patNum+" was longer than 2 characters and "+
						"was truncated in the report ouput. State abbreviation was changed from '"+
						householdState+"' to '"+newHouseholdState+"'"+Environment.NewLine;
					householdState=newHouseholdState;
				}
				outputRow+=householdState.PadRight(2,' ');
				//Household residence zip code
				string householdZip=POut.PString(PIn.PString(primaryCareReportRow.Rows[0]["HouseholdZip"].ToString()));
				if(householdZip.Length>5) {
					string newHouseholdZip=householdZip.Substring(0,5);
					rowWarnings+="WARNING: The zipcode for patient with patnum of "+patNum+" was longer than 5 characters and "+
						"was truncated in the report ouput. The zipcode was changed from '"+
						householdZip+"' to '"+newHouseholdZip+"'"+Environment.NewLine;
					householdZip=newHouseholdZip;
				}
				outputRow+=householdZip.PadRight(5,' ');
				//Household gross income
				string householdGrossIncome=POut.PString(PIn.PString(primaryCareReportRow.Rows[0]["HGI"].ToString()));
				if(householdGrossIncome==""||householdGrossIncome=="null") {
					householdGrossIncome="0";
				}
				//Remove any character that is not a digit or a decimal.
				string newHouseholdGrossIncome=Math.Round(Convert.ToDouble(Regex.Replace(householdGrossIncome,"[^0-9\\.]","")),0).ToString();
				if(householdGrossIncome!=newHouseholdGrossIncome) {
					rowWarnings+="WARNING: The household gross income for patient with patnum "+patNum+" contained invalid characters "+
						"and was changed in the output report from '"+householdGrossIncome+"' to '"+newHouseholdGrossIncome+"'."+Environment.NewLine;
				}
				householdGrossIncome=newHouseholdGrossIncome.PadLeft(7,'0');
				if(householdGrossIncome.Length>7) {
					rowErrors+="ERROR: Abnormally large household gross income of '"+householdGrossIncome+
						"' for patient with patnum of "+patNum+"."+Environment.NewLine;
				}
				outputRow+=householdGrossIncome;
				//Household percent of poverty
				string householdPercentPoverty=POut.PString(PIn.PString(primaryCareReportRow.Rows[0]["HPP"].ToString()));
				if(householdPercentPoverty==""||householdPercentPoverty=="null") {
					householdPercentPoverty="0";
				}
				string newHouseholdPercentPoverty=Regex.Replace(householdPercentPoverty,"[^0-9]","");//Remove anything that is not a digit.
				if(newHouseholdPercentPoverty!=householdPercentPoverty) {
					rowWarnings+="WARNING: Household percent poverty for the patient with a patnum of "+patNum+
						" had to be modified in the output report from '"+householdPercentPoverty+"' to '"+newHouseholdPercentPoverty+
						"' based on output requirements."+Environment.NewLine;
				}
				householdPercentPoverty=newHouseholdPercentPoverty.PadLeft(3,'0');
				if(householdPercentPoverty.Length>3||Convert.ToInt16(householdPercentPoverty)>200) {
					rowErrors+="ERROR: Household percent poverty must be between 0 and 200 percent, but is set to '"+
						householdPercentPoverty+"' for the patient with the patnum of "+patNum+Environment.NewLine;
				}
				outputRow+=householdPercentPoverty;
				string householdSlidingFeeScale=POut.PString(PIn.PString(primaryCareReportRow.Rows[0]["HSFS"].ToString()));
				if(householdSlidingFeeScale.Length==0){
					householdSlidingFeeScale="0";
				}
				string newHouseholdSlidingFeeScale=Regex.Replace(householdSlidingFeeScale,"[^0-9]","");
				if(newHouseholdSlidingFeeScale!=householdSlidingFeeScale){
					rowWarnings+="WARNING: The household sliding fee scale (latest NOAH copay amount) for the patient with a patnum of "+patNum+
						" contains invalid characters and was changed from '"+householdSlidingFeeScale+"' to '"+newHouseholdSlidingFeeScale+"'.";
					householdSlidingFeeScale=newHouseholdSlidingFeeScale;
				}
				if(householdSlidingFeeScale.Length>3 || Convert.ToInt16(householdSlidingFeeScale)>100){
					rowWarnings+="The household sliding fee scale (latest NOAH copay amount) for the patient with a patnum of "+patNum+
						" is '"+householdSlidingFeeScale+"', but will be reported as 100.";
					householdSlidingFeeScale="100";
				}
				outputRow+=householdSlidingFeeScale.PadLeft(3,'0');
				string dateOfEligibilityStatusStr=primaryCareReportRow.Rows[0]["HSFS"].ToString();
				DateTime dateOfEligibilityStatus=DateTime.MinValue;
				if(dateOfEligibilityStatusStr!="" && dateOfEligibilityStatusStr!="null"){
					dateOfEligibilityStatus=PIn.PDate(dateOfEligibilityStatusStr);
				}
				outputRow+=dateOfEligibilityStatus.ToString("MMddyyyy");
				//Primary care status
				string primaryCareStatus=POut.PString(PIn.PString(primaryCareReportRow.Rows[0]["CareStatus"].ToString())).ToUpper();
				if(primaryCareStatus!="A"&&primaryCareStatus!="B"&&primaryCareStatus!="C"&&primaryCareStatus!="D") {
					rowErrors+="The primary care status of the patient with a patnum of "+patNum+" is set to '"+primaryCareStatus+
						"', but must be set to A, B, C or D. "+Environment.NewLine;
				}
				outputRow+=primaryCareStatus;
				textLog.Text+=rowErrors+rowWarnings;
				if(rowErrors.Length>0) {
					continue;
				}
				outputText+=outputRow+Environment.NewLine;//Only add the row to the output file if it is properly formatted.
			}
			File.WriteAllText(outFile,outputText);
			MessageBox.Show("Done.");
		}

	}
}