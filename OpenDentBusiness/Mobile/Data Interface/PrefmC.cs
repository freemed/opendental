using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Web;

/*
namespace OpenDentBusiness.Mobile {
	public class PrefmC {
		///<summary>Dennis ToDo: this class has to be revised</summary>
		public Dictionary<string,Prefm> Dict=new Dictionary<string,Prefm>();// cannot have a static variable here because we want something unique for each patient.
		///<summary>Gets a pref of type string.</summary>
		public static string GetString(PrefmName prefmName) {
			try {
				PrefmC prefmC=(PrefmC)HttpContext.Current.Session["prefmC"];
				if(prefmC==null) {
					if(HttpContext.Current.Session["Patient"]!=null) {
						long DentalOfficeID=((Patientm)HttpContext.Current.Session["Patient"]).CustomerNum;
						prefmC=Prefms.LoadPreferences(DentalOfficeID);
					}
				}
				if(!prefmC.Dict.ContainsKey(prefmName.ToString())) {
					throw new Exception(prefmName+" is an invalid pref name.");
				}
				return prefmC.Dict[prefmName.ToString()].ValueString;
			}
			catch(Exception ex) {
				return "";
			}
		}








	}
}*/



	