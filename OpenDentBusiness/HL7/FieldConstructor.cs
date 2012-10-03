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

		public static string GenerateDFT(HL7Def def,string fieldName,Patient pat,Provider prov,Procedure proc,Patient guar,Appointment apt,int sequenceNum,string eventType,string pdfDescription,string pdfDataString) {
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
					return eventType;
				case "guar.addressCityStateZip":
					return gConcat(def.ComponentSeparator,guar.Address,guar.Address2,guar.City,guar.State,guar.Zip);
				case "guar.birthdateTime":
					return gDTM(guar.Birthdate,8);
				case "guar.Gender":
					return gIS(guar);
				case "guar.HmPhone":
					return gXTN(guar.HmPhone,10);
				case "guar.nameLFM":
					return gConcat(def.ComponentSeparator,guar.LName,guar.FName,guar.MiddleI);
				case "guar.PatNum":
					return guar.PatNum.ToString();
				case "guar.SSN":
					return guar.SSN;
				case "guar.WkPhone":
					return gXTN(guar.WkPhone,10);
				case "messageType":
					return gConcat(def.ComponentSeparator,"DFT",eventType);
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
					return pdfDataString;
				case "proc.DiagnosticCode":
					if(proc.DiagnosticCode==null) {
						return "";
					}
					else {
						return proc.DiagnosticCode;
					}
				case "proc.procDateTime":
					return gDTM(proc.ProcDate,14);
				case "proc.ProcFee":
					return proc.ProcFee.ToString("F2");
				case "proc.toothSurfRange":
					return gTreatArea(def.ComponentSeparator,proc);
				case "proccode.ProcCode":
					return gProcCode(proc);
				case "prov.provIdNameLFM":
					return gConcat(def.ComponentSeparator,prov.EcwID,prov.LName,prov.FName,prov.MI);
				case "separators^~\\&":
					return gSep(def);
				case "sequenceNum":
					return sequenceNum.ToString();
				default:
					return "";
			}
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
			switch(pat.Race) {
				case PatientRace.AmericanIndian:
					return "American Indian Or Alaska Native";
				case PatientRace.Asian:
					return "Asian";
				case PatientRace.HawaiiOrPacIsland:
					return "Native Hawaiian or Other Pacific";
				case PatientRace.AfricanAmerican:
					return "Black or African American";
				case PatientRace.White:
					return "White";
				case PatientRace.HispanicLatino:
					return "Hispanic";
				case PatientRace.Other:
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
