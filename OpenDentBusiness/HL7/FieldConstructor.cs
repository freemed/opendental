using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using OpenDental;
using OpenDental.UI;
using OpenDentBusiness.UI;


namespace OpenDentBusiness.HL7 {
	///<summary>This is the engine that will construct our outgoing HL7 message fields.</summary>
	public class FieldConstructor {

		///<summary>apt, guar, proc, prov and pdfDataString can be null and will return an empty string if a field requires that object</summary>
		public static string GenerateDFT(HL7Def def,string fieldName,Patient pat,Provider prov,Procedure proc,Patient guar,Appointment apt,int sequenceNum,EventTypeHL7 eventType,string pdfDescription,string pdfDataString) {
			//big long list of fieldnames that we support
			switch(fieldName){
				case "apt.AptNum":
					if(apt==null) {
						return "";
					}
					else {
						return apt.AptNum.ToString();
					}
				case "dateTime.Now":
					return gDTM(DateTime.Now,14);
				case "eventType":
					return eventType.ToString();
				case "guar.addressCityStateZip":
					if(guar==null) {
						return "";
					}
					else {
						return gConcat(def.ComponentSeparator,guar.Address,guar.Address2,guar.City,guar.State,guar.Zip);
					}
				case "guar.birthdateTime":
					if(guar==null) {
						return "";
					}
					else {
						return gDTM(guar.Birthdate,8);
					}
				case "guar.Gender":
					if(guar==null) {
						return "";
					}
					else {
						return gIS(guar);
					}
				case "guar.HmPhone":
					if(guar==null) {
						return "";
					}
					else {
						return gXTN(guar.HmPhone,10);
					}
				case "guar.nameLFM":
					if(guar==null) {
						return "";
					}
					else {
						return gConcat(def.ComponentSeparator,guar.LName,guar.FName,guar.MiddleI);
					}
				case "guar.PatNum":
					if(guar==null) {
						return "";
					}
					else {
						return guar.PatNum.ToString();
					}
				case "guar.SSN":
					if(guar==null) {
						return "";
					}
					else {
						return guar.SSN;
					}
				case "guar.WkPhone":
					if(guar==null) {
						return "";
					}
					else {
						return gXTN(guar.WkPhone,10);
					}
				case "messageControlId":
					return Guid.NewGuid().ToString("N");
				case "messageType":
					return gConcat(def.ComponentSeparator,"DFT",eventType.ToString());
				case "pat.addressCityStateZip":
					return gConcat(def.ComponentSeparator,pat.Address,pat.Address2,pat.City,pat.State,pat.Zip);
				case "pat.birthdateTime":
					return gDTM(pat.Birthdate,8);
				case "pat.ChartNumber":
					return pat.ChartNumber;
				case "pat.Gender":
					return gIS(pat);
				case "pat.HmPhone":
					return gXTN(pat.HmPhone,10);
				case "pat.nameLFM":
					return gConcat(def.ComponentSeparator,pat.LName,pat.FName,pat.MiddleI);
				case "pat.PatNum":
					return pat.PatNum.ToString();
				case "pat.Position":
					return gPos(pat);
				case "pat.Race":
					return gRace(pat);
				case "pat.SSN":
					return pat.SSN;
				case "pat.WkPhone":
					return gXTN(pat.WkPhone,10);
				case "pdfDescription":
					return pdfDescription;
				case "pdfDataAsBase64":
					if(pdfDataString==null) {
						return "";
					}
					else {
						return pdfDataString;
					}
				case "proc.DiagnosticCode":
					if(proc==null) {
						return "";
					}
					if(proc.DiagnosticCode==null) {
						return "";
					}
					else {
						return proc.DiagnosticCode;
					}
				case "proc.procDateTime":
					if(proc==null) {
						return "";
					}
					else {
						return gDTM(proc.ProcDate,14);
					}
				case "proc.ProcFee":
					if(proc==null) {
						return "";
					}
					else {
						return proc.ProcFee.ToString("F2");
					}
				case "proc.ProcNum":
					if(proc==null) {
						return "";
					}
					else {
						return proc.ProcNum.ToString();
					}
				case "proc.toothSurfRange":
					if(proc==null) {
						return "";
					}
					else {
						return gTreatArea(def.ComponentSeparator,proc);
					}
				case "proccode.ProcCode":
					if(proc==null) {
						return "";
					}
					else {
						return gProcCode(proc);
					}
				case "prov.provIdNameLFM":
					if(prov==null) {
						return "";
					}
					else {
						return gConcat(def.ComponentSeparator,prov.EcwID,prov.LName,prov.FName,prov.MI);
					}
				case "separators^~\\&":
					return gSep(def);
				case "sequenceNum":
					return sequenceNum.ToString();
				default:
					return "";
			}
		}

		public static string GenerateACK(HL7Def def,string fieldName,EventTypeHL7 eventType,string controlId,bool isAck) {
			//big long list of fieldnames that we support
			switch(fieldName) {
				case "ackCode":
					return gAck(isAck);
				case "dateTime.Now":
					return gDTM(DateTime.Now,14);
				case "messageControlId":
					return controlId;
				case "messageType":
					return gConcat(def.ComponentSeparator,"ACK",eventType.ToString());
				case "separators^~\\&":
					return gSep(def);
				default:
					return "";
			}
		}

		private static string gAck(bool isAck) {
			if(isAck) {
				return "AA";//Acknowledgment accept
			}
			else {
				return "AE";//Acknowledgment error
			}
			//Ack reject is a third possible response that we don't currently support
		}

		//Send in component separator for this def and the values in the order they should be in the message.
		private static string gConcat(string componentSep,params string[] vals) {
			string retVal="";
			if(vals.Length==1) {
				return retVal=vals[0];//this allows us to pass in all components for the field as one long string: comp1^comp2^comp3
			}
			for(int i=0;i<vals.Length;i++) {
				if(i>0) {
					retVal+=componentSep;
				}
				retVal+=vals[i];
			}
			return retVal;
		}

		private static string gSep(HL7Def def) {
			return def.ComponentSeparator+def.RepetitionSeparator+def.EscapeCharacter+def.SubcomponentSeparator;
		}

		private static string gDTM(DateTime dt,int precisionDigits) {
			switch(precisionDigits) {
				case 8:
					return dt.ToString("yyyyMMdd");
				case 14:
					return dt.ToString("yyyyMMddHHmmss");
				default:
					return "";
			}
		}

		private static string gIS(Patient pat) {
			if(pat.Gender==PatientGender.Female) {
				return "F";
			}
			if(pat.Gender==PatientGender.Male) {
				return "M";
			}
			return "U";
		}

		private static string gPos(Patient pat) {
			switch(pat.Position) {
				case PatientPosition.Single:
					return "Single";
				case PatientPosition.Married:
					return "Married";
				case PatientPosition.Divorced:
					return "Divorced";
				case PatientPosition.Widowed:
					return "Widowed";
				case PatientPosition.Child:
					return "Single";
				default:
					return "Single";
			}
		}

		private static string gProcCode(Procedure proc) {
			string retVal="";
			ProcedureCode procCode=ProcedureCodes.GetProcCode(proc.CodeNum);
			if(procCode.ProcCode.Length>5 && procCode.ProcCode.StartsWith("D")) {
				retVal=procCode.ProcCode.Substring(0,5);//Remove suffix from all D codes.
			}
			else {
				retVal=procCode.ProcCode;
			}
			return retVal;
		}

		private static string gRace(Patient pat) {
			switch(PatientRaces.GetPatientRaceOldFromPatientRaces(pat.PatNum)) { //Uses the deprecated PatientRaceOld enum converted from PatientRaces.GetPatientRaceOldFromPatientRaces()
				case PatientRaceOld.AmericanIndian:
					return "American Indian Or Alaska Native";
				case PatientRaceOld.Asian:
					return "Asian";
				case PatientRaceOld.HawaiiOrPacIsland:
					return "Native Hawaiian or Other Pacific";
				case PatientRaceOld.AfricanAmerican:
					return "Black or African American";
				case PatientRaceOld.White:
					return "White";
				case PatientRaceOld.HispanicLatino:
					return "Hispanic";
				case PatientRaceOld.Other:
					return "Other Race";
				default:
					return "Other Race";
			}
		}

		private static string gTreatArea(string componentSep,Procedure proc) {
			string retVal="";
			ProcedureCode procCode=ProcedureCodes.GetProcCode(proc.CodeNum);
			if(procCode.TreatArea==TreatmentArea.ToothRange) {
				retVal=proc.ToothRange;
			}
			else if(procCode.TreatArea==TreatmentArea.Surf) {//probably not necessary
				retVal=gConcat(componentSep,Tooth.ToInternat(proc.ToothNum),Tooth.SurfTidyForClaims(proc.Surf,proc.ToothNum));
			}
			else {
				retVal=gConcat(componentSep,Tooth.ToInternat(proc.ToothNum),proc.Surf);
			}
			return retVal;
		}

		///<summary>XTN is a phone number.</summary>
		private static string gXTN(string phone,int numDigits) {
			string retVal="";
			for(int i=0;i<phone.Length;i++) {
				if(Char.IsNumber(phone,i)) {
					if(retVal=="" && phone.Substring(i,1)=="1") {
						continue;//skip leading 1.
					}
					retVal+=phone.Substring(i,1);
				}
				if(retVal.Length==numDigits) {
					return retVal;
				}
			}
			//never made it to 10
			return "";
		}
	}
}
