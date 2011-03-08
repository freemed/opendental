using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenDentBusiness;
using System.Windows.Forms;



namespace OpenDental.Bridges {
	///<summary>Provides bridging functionality to Patterson Imaging.</summary>
	class Patterson {

		///<summary>Default Constructor</summary>
		public Patterson() { 
		}

		/// <summary>Launches Patterson Imaging, logs user in, and opens current patient.</summary>
		public static void SendData(Program ProgramCur,Patient pat) {
			if(pat==null) {
				return;
			}
			Provider prov=Providers.GetProv(pat.PriProv);
			try {
				VBbridges.Patterson.Launch(
					Tidy(pat.FName,40),
					Tidy(pat.MiddleI,1),
					Tidy(pat.LName,40),
					Tidy(pat.Preferred,40),
					Tidy(pat.Address,40),
					Tidy(pat.City,30),
					Tidy(pat.State,2),
					Tidy(pat.Zip,10),
					Tidy(pat.SSN.ToString(),9),//This only works with ssn in america with no punctuation
					Tidy((pat.Gender==PatientGender.Male?"M":(pat.Gender==PatientGender.Female?"F":" ")),1),//uses "M" for male "F" for female and " " for unkown
					Tidy(pat.Birthdate.ToShortDateString(),11),
					LTidy(pat.PatNum.ToString(),5),//Limit is 5 characters, but that would only be exceeded if they are using random primary keys or they have a lot of data, neither case is common.
					LTidy(prov.ProvNum.ToString(),3),//Limit is 3 characters, but that would only be exceeded if they are using random primary keys or they have a lot of data, neither case is common.
					Tidy(prov.FName,40),
					Tidy(prov.LName,40),
					ProgramCur.Path,
					ProgramProperties.GetPropVal(ProgramCur.ProgramNum,"System path to Patterson Imaging ini")
					);
			}
			catch {
				MessageBox.Show("Error launching Patterson Imaging.");
			}
		}

		/// <summary>Appended with whitespace and returns FIRST maxL characters. Accepts null to return string full of spaces.</summary>
		private static string Tidy(string str,int maxL) {
			if(str==null){
				str="";
			}
			str=str.Trim().PadRight(maxL,' ');
			return str.Substring(0,maxL);
		}

		/// <summary>PREpended with whitespace and returns LAST maxL characters. Accepts null to return string full of spaces.</summary>
		private static string LTidy(string str,int maxL) {
			if(str==null) {
				str="";
			}
			str=str.Trim().PadLeft(maxL,' ');
			return str.Substring(str.Length-maxL,maxL);
		}


	}
}
