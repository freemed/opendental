using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace OpenDentBusiness.HL7 {
	///<summary>Parses a single incoming HL7 field.</summary>
	public class FieldParser {
		//HL7 has very specific data types.  Each data type that we use will have a corresponding parser method here.
		//Data types are listed in 2.15.

		///<summary>yyyyMMdd.  If not in that format, it returns minVal.</summary>
		public static DateTime DateParse(string str) {
			if(str.Length != 8) {
				return DateTime.MinValue;
			}
			int year=PIn.Int(str.Substring(0,4));
			int month=PIn.Int(str.Substring(4,2));
			int day=PIn.Int(str.Substring(6));
			DateTime retVal=new DateTime(year,month,day);
			return retVal;
		}

		///<summary>yyyyMMddHHmmss.  If not in that format, it returns minVal.</summary>
		public static DateTime DateTimeParse(string str) {
			if(str.Length != 14) {
				return DateTime.MinValue;
			}
			int year=PIn.Int(str.Substring(0,4));
			int month=PIn.Int(str.Substring(4,2));
			int day=PIn.Int(str.Substring(6,2));
			int hour=PIn.Int(str.Substring(8,2));
			int minute=PIn.Int(str.Substring(10,2));
			//skip seconds
			DateTime retVal=new DateTime(year,month,day,hour,minute,0);
			return retVal;
		}

		///<summary>M,F,U</summary>
		public static PatientGender GenderParse(string str) {
			if(str.ToLower()=="m" || str.ToLower()=="male") {
				return PatientGender.Male;
			}
			else if(str.ToLower()=="f" || str.ToLower()=="female") {
				return PatientGender.Female;
			}
			else {
				return PatientGender.Unknown;
			}
		}

		public static PatientPosition MaritalStatusParse(string str) {
			switch(str) {
				case "Single":
					return PatientPosition.Single;
				case "Married":
					return PatientPosition.Married;
				case "Divorced":
					return PatientPosition.Divorced;
				case "Widowed":
					return PatientPosition.Widowed;
				case "Legally Separated":
					return PatientPosition.Married;
				case "Unknown":
					return PatientPosition.Single;
				case "Partner":
					return PatientPosition.Single;
				default:
					return PatientPosition.Single;
			}
		}

		/// <summary>If it's exactly 10 digits, it will be formatted like this: (###)###-####.  Otherwise, no change.</summary>
		public static string PhoneParse(string str) {
			if(str.Length != 10) {
				return str;//no change
			}
			return "("+str.Substring(0,3)+")"+str.Substring(3,3)+"-"+str.Substring(6);
		}

		public static string ProcessPattern(DateTime startTime,DateTime stopTime) {
			int minutes=(int)((stopTime-startTime).TotalMinutes);
			if(minutes<=0) {
				return "//";//we don't want it to be zero minutes
			}
			int increments5=minutes/5;
			StringBuilder pattern=new StringBuilder();
			for(int i=0;i<increments5;i++) {
				pattern.Append("X");//make it all provider time, I guess.
			}
			return pattern.ToString();
		}

		///<summary>Supply in format UPIN^LastName^FirstName^MI (AIG) or UPIN^LastName, FirstName MI (PV1).  If UPIN(abbr) does not exist, provider gets created.  If name has changed, provider gets updated.
		///ProvNum is returned.  If blank, then returns 0.  If field is NULL, returns 0. For PV1, the provider.LName field will hold "LastName, FirstName MI". They can manually change later.</summary>
		public static long ProvProcess(FieldHL7 field) {
			if(field==null) {
				return 0;
			}
			string eID=field.GetComponentVal(0);
			eID=eID.Trim();
			if(eID=="") {
				return 0;
			}
			Provider prov=Providers.GetProvByEcwID(eID);
			bool isNewProv=false;
			bool provChanged=false;
			if(prov==null) {
				isNewProv=true;
				prov=new Provider();
				prov.Abbr=eID;//They can manually change this later.
				prov.EcwID=eID;
			}
			if(prov.LName!=field.GetComponentVal(1)) {
				provChanged=true;
				prov.LName=field.GetComponentVal(1);
			}
			if(prov.FName!=field.GetComponentVal(2)) {
				provChanged=true;
				prov.FName=field.GetComponentVal(2);
			}
			if(prov.MI!=field.GetComponentVal(3)) {
				provChanged=true;
				prov.MI=field.GetComponentVal(3);
			}
			if(isNewProv) {
				Providers.Insert(prov);
				Providers.RefreshCache();
			}
			else if(provChanged) {
				Providers.Update(prov);
				Providers.RefreshCache();
			}
			return prov.ProvNum;
		}

		public static PatientRace RaceParse(string str) {
			switch(str) {
				case "American Indian Or Alaska Native":
					return PatientRace.AmericanIndian;
				case "Asian":
					return PatientRace.Asian;
				case "Native Hawaiian or Other Pacific":
					return PatientRace.HawaiiOrPacIsland;
				case "Black or African American":
					return PatientRace.AfricanAmerican;
				case "White":
					return PatientRace.White;
				case "Hispanic":
					return PatientRace.HispanicLatino;
				case "Other Race":
					return PatientRace.Other;
				default:
					return PatientRace.Other;
			}
		}

		public static double SecondsToMinutes(string secs) {
			double retVal;
			try {
				retVal=double.Parse(secs);
			}
			catch {//couldn't parse the value to a double so just return 0
				return 0;
			}
			return retVal/60;
		}
	}
}
