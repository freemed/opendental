using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace OpenDentBusiness {
	public class TelephoneNumbers {
		///<summary>Used in the tool that loops through the database fixing telephone numbers.  Also used in the patient import from XML tool, and PT Dental bridge.</summary>
		public static string ReFormat(string phoneNum) {
			if(CultureInfo.CurrentCulture.Name!="en-US" && CultureInfo.CurrentCulture.Name.Length>=4 && CultureInfo.CurrentCulture.Name.Substring(3)!="CA") {
				return phoneNum;
			}
			Regex regex;
			regex=new Regex(@"^\d{10}");//eg. 5033635432
			if(regex.IsMatch(phoneNum)) {
				return "("+phoneNum.Substring(0,3)+")"+phoneNum.Substring(3,3)+"-"+phoneNum.Substring(6);
			}
			regex=new Regex(@"^\d{3}-\d{3}-\d{4}");//eg. 503-363-5432
			if(regex.IsMatch(phoneNum)) {
				return "("+phoneNum.Substring(0,3)+")"+phoneNum.Substring(4);
			}
			regex=new Regex(@"^\d-\d{3}-\d{3}-\d{4}");//eg. 1-503-363-5432 to 1(503)363-5432
			if(regex.IsMatch(phoneNum)) {
				return phoneNum.Substring(0,1)+"("+phoneNum.Substring(2,3)+")"+phoneNum.Substring(6);
			}
			regex=new Regex(@"^\d{3} \d{3}-\d{4}");//eg 503 363-5432
			if(regex.IsMatch(phoneNum)) {
				return "("+phoneNum.Substring(0,3)+")"+phoneNum.Substring(4);
			}
			//Keyush Shah 04/21/05 Added more formats:
			regex=new Regex(@"^\d{3} \d{3} \d{4}");//eg 916 363 5432
			if(regex.IsMatch(phoneNum)) {
				return "("+phoneNum.Substring(0,3)+")"+phoneNum.Substring(4,3)+"-"+phoneNum.Substring(8,4);
			}
			regex=new Regex(@"^\(\d{3}\) \d{3} \d{4}");//eg (916) 363 5432
			if(regex.IsMatch(phoneNum)) {
				return "("+phoneNum.Substring(1,3)+")"+phoneNum.Substring(6,3)+"-"+phoneNum.Substring(10,4);
			}
			regex=new Regex(@"^\(\d{3}\) \d{3}-\d{4}");//eg (916) 363-5432
			if(regex.IsMatch(phoneNum)) {
				return "("+phoneNum.Substring(1,3)+")"+phoneNum.Substring(6,3)+"-"+phoneNum.Substring(10,4);
			}
			regex=new Regex(@"^\d{7}$");//eg 3635432
			if(regex.IsMatch(phoneNum)) {
				return (phoneNum.Substring(0,3)+"-"+phoneNum.Substring(3));
			}
			return phoneNum;
		}

		///<summary>reformats initial entry with each keystroke</summary>
		public static string AutoFormat(string phoneNum) {
			if(CultureInfo.CurrentCulture.Name!="en-US" && 
				CultureInfo.CurrentCulture.Name.Length>=4 && 
				CultureInfo.CurrentCulture.Name.Substring(3)!="CA") {
				return phoneNum;
			}
			if(Regex.IsMatch(phoneNum,@"^[2-9]$")) {
				return "("+phoneNum;
			}
			if(Regex.IsMatch(phoneNum,@"^1\d$")) {
				return "1("+phoneNum.Substring(1);
			}
			if(Regex.IsMatch(phoneNum,@"^\(\d\d\d\d$")) {
				return (phoneNum.Substring(0,4)+")"+phoneNum.Substring(4));
			}
			if(Regex.IsMatch(phoneNum,@"^1\(\d\d\d\d$")) {
				return (phoneNum.Substring(0,5)+")"+phoneNum.Substring(5));
			}
			if(Regex.IsMatch(phoneNum,@"^\(\d\d\d\)\d\d\d\d$")) {
				return (phoneNum.Substring(0,8)+"-"+phoneNum.Substring(8));
			}
			if(Regex.IsMatch(phoneNum,@"^1\(\d\d\d\)\d\d\d\d$")) {
				return (phoneNum.Substring(0,9)+"-"+phoneNum.Substring(9));
			}
			return phoneNum;
		}

		///<Summary>Also truncates if more than two non-numbers in a row.  This is to avoid the notes that can follow phone numbers.</Summary>
		public static string FormatNumbersOnly(string phoneStr) {
			string retVal="";
			int nonnumcount=0;
			for(int i=0;i<phoneStr.Length;i++) {
				if(nonnumcount==2) {
					return retVal;
				}
				if(Char.IsNumber(phoneStr,i)) {
					retVal+=phoneStr.Substring(i,1);
					nonnumcount=0;
				}
				else {
					nonnumcount++;
				}
			}
			return retVal;
		}

		///<summary></summary>
		public static string FormatNumbersExactTen(string phoneNum) {
			string retVal="";
			for(int i=0;i<phoneNum.Length;i++) {
				if(Char.IsNumber(phoneNum,i)) {
					if(retVal=="" && phoneNum.Substring(i,1)=="1") {
						continue;//skip leading 1.
					}
					retVal+=phoneNum.Substring(i,1);
				}
				if(retVal.Length==10) {
					return retVal;
				}
			}
			//never made it to 10
			return "";
		}

	}
}
