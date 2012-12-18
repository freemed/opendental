using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace OpenDentBusiness.HL7 {
	///<summary>Parses a single incoming HL7 field.</summary>
	public class FieldParser {
		//HL7 has very specific data types.  Each data type that we use will have a corresponding parser method here.
		//Data types are listed in 2.15.

		///<summary>yyyyMMddHHmmss.  Can have more precision than seconds and won't break.  If less than 8 digits, returns MinVal.</summary>
		public static DateTime DateTimeParse(string str) {
			int year=0;
			int month=0;
			int day=0;
			int hour=0;
			int minute=0;
			if(str.Length<8) {
				return DateTime.MinValue;
			}
			year=PIn.Int(str.Substring(0,4));
			month=PIn.Int(str.Substring(4,2));
			day=PIn.Int(str.Substring(6,2));
			if(str.Length>=10) {
				hour=PIn.Int(str.Substring(8,2));
			}
			if(str.Length>=12) {
				minute=PIn.Int(str.Substring(10,2));
			}
			//skip seconds and any trailing numbers
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

		///<summary>Supply in format UPIN^LastName^FirstName^MI (PV1) or UPIN^LastName, FirstName MI (AIG).  If UPIN(abbr) does not exist, provider gets created.  If name has changed, provider gets updated.  ProvNum is returned.  If blank, then returns 0.  If field is NULL, returns 0. For PV1, the provider.LName field will hold "LastName, FirstName MI". They can manually change later.</summary>
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
				prov.FeeSched=FeeSchedC.ListShort[0].FeeSchedNum;
			}
			if(field.Components.Count==4) {//PV1 segment in format UPIN^LastName^FirstName^MI
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
			}
			else if(field.Components.Count==2) {//AIG segment in format UPIN^LastName, FirstName MI
				string[] components=field.GetComponentVal(1).Split(' ');
				if(components.Length>0) {
					components[0]=components[0].TrimEnd(',');
					if(prov.LName!=components[0]) {
						provChanged=true;
						prov.LName=components[0];
					}
				}
				if(components.Length>1 && prov.FName!=components[1]) {
					provChanged=true;
					prov.FName=components[1];
				}
				if(components.Length>2 && prov.MI!=components[2]) {
					provChanged=true;
					prov.MI=components[2];
				}
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

		/// <summary>Will return 0 if string cannot be parsed to a number.  Will return 0 if the fee schedule passed in does not exactly match the description of a regular fee schedule.</summary>
		public static long FeeScheduleParse(string str) {
			if(str=="") {
				return 0;
			}
			FeeSched feeSched=FeeScheds.GetByExactName(str,FeeScheduleType.Normal);
			if(feeSched==null) {
				return 0;
			}
			return feeSched.FeeSchedNum;
		}
	}
}
