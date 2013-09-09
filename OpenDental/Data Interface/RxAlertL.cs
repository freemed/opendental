using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public class RxAlertL {
		///<summary>Returns false if user does not wish to continue after seeing alert.</summary>
		public static bool DisplayAlerts(long patNum,long rxDefNum){
			List<RxAlert> alertList=RxAlerts.Refresh(rxDefNum);
			List<Allergy> allergies=Allergies.Refresh(patNum);
			List<string> allergiesMatches=new List<string>();
			List<string> customMessages=new List<string>();
			for(int i=0;i<alertList.Count;i++){
				for(int j=0;j<allergies.Count;j++) {
					if(alertList[i].AllergyDefNum==allergies[j].AllergyDefNum && allergies[j].StatusIsActive) {
						if(alertList[i].NotificationMsg=="") {
							allergiesMatches.Add(AllergyDefs.GetOne(alertList[i].AllergyDefNum).Description);
						}
						else {
							customMessages.Add(alertList[i].NotificationMsg);
						}
					}
				}
			}
			//these matches do not include ones that have custom messages.
			if(allergiesMatches.Count>0) {
				string alert="";
				for(int i=0;i<allergiesMatches.Count;i++) {
					if(i==0) {
						alert=Lan.g("RxAlertL","This patient has the following allergies: ");
					}
					alert+=allergiesMatches[i];
					if((i+1)==allergiesMatches.Count) {
						alert+=".\r\n";
					}
					else {
						alert+=", ";
					}
				}
				alert+="\r\n"+Lan.g("RxAlertL","Continue anyway?");
				if(MessageBox.Show(alert,"Alert",MessageBoxButtons.OKCancel,MessageBoxIcon.Exclamation)!=DialogResult.OK) {
					return false;
				}
			}
			for(int i=0;i<customMessages.Count;i++){
				if(MessageBox.Show(customMessages[i]+"\r\n"+Lan.g("RxAlertL","Continue anyway?"),"Alert",MessageBoxButtons.OKCancel,MessageBoxIcon.Exclamation)!=DialogResult.OK){
					return false;
				}
			}
			return true;
		}






	}
}
