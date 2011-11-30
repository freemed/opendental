using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Reflection;

namespace OpenDentBusiness{
	///<summary></summary>
	public class Popups {

		///<summary>Gets all Popups for a single patient.  There will actually only be one or zero for now.</summary>
		public static List<Popup> GetForPatient(long patNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<Popup>>(MethodBase.GetCurrentMethod(),patNum);
			} 
			Patient patCur=Patients.GetPat(patNum);
			string command="SELECT * FROM popup "
				+"WHERE PatNum = "+POut.Long(patNum)+" "
				+"OR (PatNum IN (SELECT Guarantor FROM patient "
						+"WHERE PatNum = "+POut.Long(patNum)+") "
					+"AND IsFamily = "+POut.Int((int)EnumPopupFamily.Family)+") "
				+"OR (PatNum IN (SELECT SuperFamily FROM patient "
						+"WHERE PatNum = "+POut.Long(patNum)+") "
					+"AND IsFamily = "+POut.Int((int)EnumPopupFamily.SuperFamily)+") ";
			return Crud.PopupCrud.SelectMany(command);
		}

		///<summary>Gets all Popups for a single patient.  There will actually only be one or zero for now.</summary>
		public static List<Popup> GetForFamily(Patient pat) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<Popup>>(MethodBase.GetCurrentMethod(),pat);
			}
			string command="SELECT * FROM popup "
					+"WHERE PatNum IN (SELECT PatNum FROM patient "
						+"WHERE Guarantor = "+POut.Long(pat.Guarantor)+") ";
			if(pat.SuperFamily!=0) {//They are part of a super family
				command+="OR PatNum IN (SELECT PatNum FROM patient "
						+"WHERE SuperFamily = "+POut.Long(pat.SuperFamily)+") ";
			}
			return Crud.PopupCrud.SelectMany(command);
		}

		///<summary></summary>
		public static long Insert(Popup popup) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				popup.PopupNum=Meth.GetLong(MethodBase.GetCurrentMethod(),popup);
				return popup.PopupNum;
			}
			return Crud.PopupCrud.Insert(popup);
		}

		///<summary></summary>
		public static void Update(Popup popup) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),popup);
				return;
			}
			Crud.PopupCrud.Update(popup);
		}

		///<summary></summary>
		public static void DeleteObject(Popup popup){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),popup);
				return;
			}
			Crud.PopupCrud.Delete(popup.PopupNum);
		}


	}

	


	


}









