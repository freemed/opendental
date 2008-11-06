using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using OpenDentBusiness;

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
			string outputText="";
			string patientsIdNumberStr="SPID#";
			string command="";
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
				command="SELECT a.AptNum FROM appointment a WHERE a.PatNum="+POut.PInt(patNum);
				DataTable appointmentList=General.GetTable(command);
				for(int j=0;j<appointmentList.Rows.Count;j++){
					string aptNum=POut.PInt(PIn.PInt(appointmentList.Rows[j][0].ToString()));
					command="SELECT "+
						"TRIM((SELECT f.FieldValue FROM patfield f WHERE f.PatNum=p.PatNum AND "+
							"LOWER(f.FieldName)=LOWER('"+patientsIdNumberStr+"') LIMIT 1)) PCIN, "+//Patient's Care ID Number
						"p.BirthDate,"+//birthdate
						"CONCAT(CONCAT(p.Address,' '),p.Address2) Address,"+//address
						"p.City,"+//city
						"p.State,"+//state
						"p.ZipCode,"+//zipcode
						"(SELECT pp.Relationship FROM patplan pp,insplan i,carrier c WHERE "+
							"pp.PatNum=p.PatNum AND pp.PlanNum=i.PlanNum AND i.CarrierNum=c.CarrierNum AND LOWER(TRIM(c.CarrierName))='noah'),"+//Relationship to subscriber
						"p.Position,"+//Marital status
						"(CASE WHEN p.EmployerNum=0 THEN (CASE WHEN (ADD_DATE(p.BirthDate,18 YEAR)>CURDATE()) THEN 3 ELSE 2 END) ELSE 1 END),"+//Employment Status
						"(CASE p.StudentStatus WHEN 'f' THEN 1 WHEN 'p' THEN 2 ELSE 3 END),"+//student status
						"'ADHS PCP',"+//insurance plan name
						"'',"+//Name of referring physician
						"'',"+//ID # of referring physician
						"722,"+//Diagnosis Code 1. Always set to V72.2 for simplicity and workability
						"'',"+//Diagnosis code 2
						"'',"+//Diagnosis code 3
						"'',"+//Diagnosis code 4
						"(SELECT AptDateTime FROM appointment a WHERE a.AptNum="+aptNum+"),"+//Date of encounter
						"(SELECT pc.ProcCode FROM procedurecode pc,procedurelog pl "+
							"WHERE pl.AptNum="+aptNum+" AND pl.CodeNum=pc.CodeNum LIMIT 1),"+//Procedure (TODO: Any procedure OK?)
						"'',"+//Procedure modifier 1
						"'',"+//Procedure modifier 2
						"'',"+//Diagnosis code
						"(SELECT pc.ProcFee FROM procedurecode pc,procedurelog pl "+
							"WHERE pl.AptNum="+aptNum+" AND pl.CodeNum=pc.CodeNum LIMIT 1),"+//Charges
						"'',"+//2nd procedure cpt/hcpcs
						"'',"+//2nd procedure modifier 1
						"'',"+//2nd procedure modifier 2
						"'',"+//Diagnosis code
						"'',"+//charges
						"'',"+//3rd procedure cpt/hcpcs
						"'',"+//3rd procedure modifier 1
						"'',"+//3rd procedure modifier 2
						"'',"+//Diagnosis code
						"'',"+//Charges
						"'',"+//4th procedure cpt/hcpcs
						"'',"+//4th procedure modifier 1
						"'',"+//4th procedure modifier 2
						"'',"+//Diagnosis code
						"'',"+//Charges
						"'',"+//5th procedure cpt/hcpcs
						"'',"+//5th procedure modifier 1
						"'',"+//5th procedure modifier 2
						"'',"+//diagnosis code
						"'',"+//Charges
						"'',"+//6th procedure cpt/hcpcs
						"'',"+//6th procedure modifier 1
						"'',"+//6th procedure modifier 2
						"'',"+//Diagnosis code
						"'',"+//Charges
						"(SELECT pc.ProcFee FROM procedurecode pc,procedurelog pl "+
							"WHERE pl.AptNum="+aptNum+" AND pl.CodeNum=pc.CodeNum LIMIT 1),"+//Total charges
						"'',"+//Amount paid
						"FROM patient p WHERE "+
						"p.PatNum="+patNum;
					DataTable primaryCareReportRow=General.GetTable(command);
				}
			}
			File.WriteAllText(outFile,outputText);
			MessageBox.Show("Done.");
		}

	}
}