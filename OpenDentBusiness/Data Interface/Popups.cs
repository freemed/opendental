using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Reflection;

namespace OpenDentBusiness{
	///<summary></summary>
	public class Popups {

		///<summary>Gets all Popups that should be displayed for a single patient.</summary>
		public static List<Popup> GetForPatient(long patNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<Popup>>(MethodBase.GetCurrentMethod(),patNum);
			}
			if(patNum==0) {
				return new List<Popup>();
			}
			Patient patCur=Patients.GetPat(patNum);
			string command="SELECT * FROM popup "
				+"WHERE IsDisabled=0 "
				+"AND (PatNum = "+POut.Long(patNum)+" "
				//any family level popup for anyone in the family
				+"OR (PatNum IN (SELECT PatNum FROM patient "
				+"WHERE Guarantor = "+POut.Long(patCur.Guarantor)+") "
				+"AND PopupLevel = "+POut.Int((int)EnumPopupLevel.Family)+") ";
				//any superfamily level popup for anyone in the superfamily
			if(patCur.SuperFamily!=0) {//They are part of a super family
				command+="OR (PatNum IN (SELECT PatNum FROM patient "
					+"WHERE SuperFamily = "+POut.Long(patCur.SuperFamily)+") "
					+"AND PopupLevel = "+POut.Int((int)EnumPopupLevel.SuperFamily)+") ";
			}
			command+=")";
			return Crud.PopupCrud.SelectMany(command);
		}

		///<summary>Gets all Popups for a single family.  If patient is part of a superfamily, it will get all popups for the entire superfamily.</summary>
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









