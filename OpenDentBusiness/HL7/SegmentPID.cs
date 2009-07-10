using System;
using System.Collections.Generic;
using System.Text;

namespace OpenDentBusiness.HL7 {
	/// <summary>(and GT1 and PV1)</summary>
	public class SegmentPID {
		///<summary>PatNum will not be altered here.  The pat passed in must either have PatNum=0, or must have a PatNum matching the segment.</summary>
		public static void ProcessPID(Patient pat,SegmentHL7 seg) {
			int patNum=PIn.PInt(seg.GetFieldFullText(2));
			//if(pat.PatNum==0) {
			//	pat.PatNum=patNum;
			//}
			//else 
			if(pat.PatNum!=0 && pat.PatNum != patNum) {
				throw new ApplicationException("Invalid patNum");
			}
			pat.LName=seg.GetFieldComponent(5,0);
			pat.FName=seg.GetFieldComponent(5,1);
			pat.MiddleI=seg.GetFieldComponent(5,2);
			pat.Birthdate=DateParse(seg.GetFieldFullText(7));
			pat.Gender=GenderParse(seg.GetFieldFullText(8));
			pat.Race=RaceParse(seg.GetFieldFullText(10));
			pat.Address=seg.GetFieldComponent(11,0);
			pat.Address2=seg.GetFieldComponent(11,1);
			pat.City=seg.GetFieldComponent(11,2);
			pat.State=seg.GetFieldComponent(11,3);
			pat.Zip=seg.GetFieldComponent(11,4);
			pat.HmPhone=PhoneParse(seg.GetFieldFullText(13));
			pat.WkPhone=PhoneParse(seg.GetFieldFullText(14));
			pat.Position=MaritalStatusParse(seg.GetFieldFullText(16));
			//pat.ChartNumber=seg.GetFieldFullText(18);//this is wrong.  Would also break standalone mode
			pat.SSN=seg.GetFieldFullText(19);
		}

		///<summary>If relationship is self, this loop does nothing.  A new pat will later change guarantor to be same as patnum. </summary>
		public static void ProcessGT1(Patient pat,SegmentHL7 seg,bool isStandalone) {
			int guarNum=PIn.PInt(seg.GetFieldFullText(2));
			if(seg.GetFieldFullText(11)=="1") {//if relationship is self
				return;
			}
			Patient guar=null;
			Patient guarOld=null;
			//So guarantor is someone else
			if(isStandalone) {
				//try to find guarantor by using chartNumber
				guar=Patients.GetPatByChartNumber(guarNum.ToString());
				if(guar==null) {
					//try to find the guarantor by using name and birthdate
					string lName=seg.GetFieldComponent(3,0);
					string fName=seg.GetFieldComponent(3,1);
					DateTime birthdate=SegmentPID.DateParse(seg.GetFieldFullText(8));
					int guarNumByName=Patients.GetPatNumByNameAndBirthday(lName,fName,birthdate);
					if(guarNumByName==0) {//guarantor does not exist in OD
						//so guar will still be null, triggering creation of new guarantor further down.
					}
					else {
						guar=Patients.GetPat(guarNumByName);
						guar.ChartNumber=guarNum.ToString();//from now on, we will be able to find guar by chartNumber
					}
				}
			}
			else {
				guar=Patients.GetPat(guarNum);
			}
			//we can't necessarily set pat.Guarantor yet, because in Standalone mode, we might not know it yet.
			bool isNewGuar= guar==null;
			if(isNewGuar) {//then we need to add guarantor to db
				guar=new Patient();
				if(isStandalone) {
					guar.ChartNumber=guarNum.ToString();
				}
				else {
					guar.PatNum=guarNum;
				}
				guar.PriProv=PrefC.GetInt("PracticeDefaultProv");
				guar.BillingType=PrefC.GetInt("PracticeDefaultBillType");
			}
			else {
				guarOld=guar.Copy();
			}
			//guar.Guarantor=guarNum;
			guar.LName=seg.GetFieldComponent(3,0);
			guar.FName=seg.GetFieldComponent(3,1);
			guar.MiddleI=seg.GetFieldComponent(3,2);
			guar.Address=seg.GetFieldComponent(5,0);
			guar.Address2=seg.GetFieldComponent(5,1);
			guar.City=seg.GetFieldComponent(5,2);
			guar.State=seg.GetFieldComponent(5,3);
			guar.Zip=seg.GetFieldComponent(5,4);
			guar.HmPhone=PhoneParse(seg.GetFieldFullText(6));
			guar.WkPhone=PhoneParse(seg.GetFieldFullText(7));
			guar.Birthdate=DateParse(seg.GetFieldFullText(8));
			guar.Gender=GenderParse(seg.GetFieldFullText(9));
			//11. Guarantor relationship to patient.  We can't really do anything with this value
			guar.SSN=seg.GetFieldFullText(12);
			if(isNewGuar) {
				Patients.Insert(guar,true);
				guarOld=guar.Copy();
				guar.Guarantor=guar.PatNum;
				Patients.Update(guar,guarOld);
			}
			else {
				Patients.Update(guar,guarOld);
			}
			pat.Guarantor=guar.PatNum;
		}

		public static void ProcessPV1(Patient pat,SegmentHL7 seg) {
			int provNum=ProvProcess(seg.GetField(7));
			if(provNum!=0) {
				pat.PriProv=provNum;
			}
		}

		///<summary>yyyyMMdd.  If not in that format, it returns minVal.</summary>
		public static DateTime DateParse(string str) {
			if(str.Length != 8) {
				return DateTime.MinValue;
			}
			int year=PIn.PInt(str.Substring(0,4));
			int month=PIn.PInt(str.Substring(4,2));
			int day=PIn.PInt(str.Substring(6));
			DateTime retVal=new DateTime(year,month,day);
			return retVal;
		}

		/// <summary>If it's exactly 10 digits, it will be formatted like this: (###)###-####.  Otherwise, no change.</summary>
		public static string PhoneParse(string str) {
			if(str.Length != 10) {
				return str;//no change
			}
			return "("+str.Substring(0,3)+")"+str.Substring(3,3)+"-"+str.Substring(6);
		}

		///<summary>M,F,U</summary>
		public static PatientGender GenderParse(string str) {
			if(str=="M") {
				return PatientGender.Male;
			}
			else if(str=="F") {
				return PatientGender.Female;
			}
			else {
				return PatientGender.Unknown;
			}
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

		///<summary>Supply in format UPIN^LastName^FirstName^MI.  If UPIN(abbr) does not exist, provider gets created.  If name has changed, provider gets updated.  ProvNum is returned.  If blank, then returns 0.  If field is NULL, returns 0.</summary>
		public static int ProvProcess(FieldHL7 field) {
			if(field==null) {
				return 0;
			}
			string provAbbr=field.GetComponentVal(0);
			provAbbr=provAbbr.TrimStart(' ');
			provAbbr=provAbbr.TrimEnd(' ');
			if(provAbbr=="") {
				return 0;
			}
			Provider prov=Providers.GetProvByAbbr(provAbbr);
			bool isNewProv=false;
			bool provChanged=false;
			if(prov==null) {
				isNewProv=true;
				prov=new Provider();
				prov.Abbr=provAbbr;
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









	}
}
