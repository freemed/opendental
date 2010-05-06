using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace OpenDentBusiness{
	public class X12Generator {
		///<summary>If clearhouse.SenderTIN is blank, then 810624427 will be used to indicate Open Dental.</summary>
		public static string GetISA06(Clearinghouse clearhouse) {
			if(clearhouse.SenderTIN=="") {
				return Sout("810624427",15,15);//TIN of OD.
			}
			else {
				return Sout(clearhouse.SenderTIN,15,15);//already validated to be length at least 2.
			}
		}

		/// <summary>Sometimes SenderTIN, sometimes OD's TIN.</summary>
		public static string GetGS02(Clearinghouse clearhouse) {
			if(clearhouse.SenderTIN=="") {
				return Sout("810624427",15,2);
			}
			else {
				return Sout(clearhouse.SenderTIN,15,2);//already validated to be length at least 2.
			}
		}

		///<summary>Returns the Provider Taxonomy code for the given specialty.</summary>
		public static string GetTaxonomy(Provider provider){
			//must return a string with length of at least one char.
			if(provider.TaxonomyCodeOverride!=""){
				return provider.TaxonomyCodeOverride;
			}
			string spec=" ";
			switch(provider.Specialty) {
				case DentalSpecialty.General: spec="1223G0001X"; break;
				case DentalSpecialty.Hygienist: spec="124Q00000X"; break;//?
				case DentalSpecialty.PublicHealth: spec="1223D0001X"; break;
				case DentalSpecialty.Endodontics: spec="1223E0200X"; break;
				case DentalSpecialty.Pathology: spec="1223P0106X"; break;
				case DentalSpecialty.Radiology: spec="1223D0008X"; break;
				case DentalSpecialty.Surgery: spec="1223S0112X"; break;
				case DentalSpecialty.Ortho: spec="1223X0400X"; break;
				case DentalSpecialty.Pediatric: spec="1223P0221X"; break;
				case DentalSpecialty.Perio: spec="1223P0300X"; break;
				case DentalSpecialty.Prosth: spec="1223P0700X"; break;
				case DentalSpecialty.Denturist: spec=" "; break;
				case DentalSpecialty.Assistant: spec=" "; break;
				case DentalSpecialty.LabTech: spec=" "; break;
			}
			return spec;
		}

		///<summary>Converts any string to an acceptable format for X12. Converts to all caps and strips off all invalid characters. Optionally shortens the string to the specified length and/or makes sure the string is long enough by padding with spaces.</summary>
		public static string Sout(string inputStr,int maxL,int minL) {
			string retStr=inputStr.ToUpper();
			//Debug.Write(retStr+",");
			retStr=Regex.Replace(retStr,//replaces characters in this input string
				//Allowed: !"&'()+,-./;?=(space)#   # is actually part of extended character set
				"[^\\w!\"&'\\(\\)\\+,-\\./;\\?= #]",//[](any single char)^(that is not)\w(A-Z or 0-9) or one of the above chars.
				"");
			retStr=Regex.Replace(retStr,"[_]","");//replaces _
			if(maxL!=-1) {
				if(retStr.Length>maxL) {
					retStr=retStr.Substring(0,maxL);
				}
			}
			if(minL!=-1) {
				if(retStr.Length<minL) {
					retStr=retStr.PadRight(minL,' ');
				}
			}
			//Debug.WriteLine(retStr);
			return retStr;
		}
	}
}
