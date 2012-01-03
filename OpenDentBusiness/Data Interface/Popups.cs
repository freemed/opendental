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

		/// <summary>Copies all family level popups when a family member leaves a family. Copies from other family members to patient, and from patient to guarantor.</summary>
		public static void CopyForMovingFamilyMember(Patient pat) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),pat);
				return;
			}
			//Get a list of all popups for the family
			string command="SELECT * FROM popup "
				+"WHERE PopupLevel = "+POut.Int((int)EnumPopupLevel.Family)+" "
				+"AND PatNum IN (SELECT PatNum FROM patient WHERE Guarantor = "+POut.Long(pat.Guarantor)+")";
			List<Popup> FamilyPopups=Crud.PopupCrud.SelectMany(command);
			Popup popup;
			for(int i=0;i<FamilyPopups.Count;i++) {
				popup=FamilyPopups[i].Copy();
				if(popup.PatNum==pat.PatNum) {//if popup is on the patient who's leaving, copy to guarantor of old family.
					popup.PatNum=pat.Guarantor;
				}
				else {//if popup is on some other family member, then copy to this patient.
					popup.PatNum=pat.PatNum;
				}
				Popups.Insert(popup);//changes the PK
			}
		}
		
		/// <summary>When a patient leaves a superfamily, this copies the superfamily level popups to be in both places. Takes pat leaving, and new superfamily. If newSuperFamily is 0, superfamily popups will not be copied from the old superfamily.</summary>
		public static void CopyForMovingSuperFamily(Patient pat,long newSuperFamily) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),pat);
				return;
			}
			//Get a list of all popups for the super family
			string command="SELECT * FROM popup "
				+"WHERE PopupLevel = "+POut.Int((int)EnumPopupLevel.SuperFamily)+" "
				+"AND PatNum IN (SELECT PatNum FROM patient WHERE SuperFamily = "+POut.Long(pat.SuperFamily)+")";
			List<Popup> SuperFamilyPopups=Crud.PopupCrud.SelectMany(command);
			Popup popup;
			for(int i=0;i<SuperFamilyPopups.Count;i++) {
				popup=SuperFamilyPopups[i].Copy();
				if(popup.PatNum==pat.PatNum) {//if popup is on the patient who's leaving, copy to superfamily head of old superfamily.
					popup.PatNum=pat.SuperFamily;
					if(newSuperFamily==0) {//If they are not going to a superfamily, delete the popup
						Popups.DeleteObject(SuperFamilyPopups[i]);
					}
				}
				else {//if popup is on some other super family member, then copy to this patient.
					if(newSuperFamily!=0) {//Only if they are moving to a superfamily.
						popup.PatNum=pat.PatNum;
					}
				}
				Popups.Insert(popup);//changes the PK
			}
		}

		/// <summary>Moves all family and superfamily level popups for a patient being deleted so that those popups stay in the family/superfamily.</summary>
		public static void MoveForDeletePat(Patient pat) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),pat);
				return;
			}
			string command="UPDATE popup ";
			if(pat.PatNum==pat.Guarantor) {//When deleting the guarantor, move all superfamily popups to the superfamily head
				command+="SET PatNum = "+POut.Long(pat.SuperFamily)+" "
					+"WHERE PopupLevel = "+POut.Int((int)EnumPopupLevel.SuperFamily)+" "
					+"AND PatNum = "+POut.Long(pat.PatNum);
			}
			else{//Move all family/superfamily popups to the guarantor
				command+="SET PatNum = "+POut.Long(pat.Guarantor)+" "
					+"WHERE (PopupLevel = "+POut.Int((int)EnumPopupLevel.Family)+" "
					+"OR PopupLevel = "+POut.Int((int)EnumPopupLevel.SuperFamily)+") "
					+"AND PatNum = "+POut.Long(pat.PatNum);
			}
			Db.NonQ(command);
		}

		/// <summary>Deletes all superfamily level popups for a superfamily being disbanded.</summary>
		public static void RemoveForDisbandingSuperFamily(Patient pat) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),pat);
				return;
			}
			//Get a list of family and superfamily popups for this individual
			string command="DELETE FROM popup "
				+"WHERE PopupLevel = "+POut.Int((int)EnumPopupLevel.SuperFamily)+" "
				+"AND PatNum IN (SELECT PatNum FROM Patient WHERE SuperFamily="+POut.Long(pat.SuperFamily)+")";
			Db.NonQ(command);
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









