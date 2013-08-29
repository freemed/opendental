using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace OpenDentBusiness{
	///<summary></summary>
	public class MedicationPats{
		///<summary>Normally, includeDiscontinued is false.  User needs to check a box to include discontinued.</summary>
		public static List<MedicationPat> Refresh(long patNum,bool includeDiscontinued) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<MedicationPat>>(MethodBase.GetCurrentMethod(),patNum,includeDiscontinued);
			}
			string command ="SELECT * FROM medicationpat WHERE PatNum = "+POut.Long(patNum);
			if(includeDiscontinued) {//this only happens when user checks box to show discontinued or for MU.
				//no restriction on DateStop
			}
			else {//exclude discontinued.  This is the default.
				command+=" AND (DateStop < "+POut.Date(new DateTime(1880,1,1))//include all the meds that are not discontinued.
					+" OR DateStop >= CURDATE())";//Show medications that are today or a future stopdate - they are not yet discontinued.
			}
			return Crud.MedicationPatCrud.SelectMany(command);
		}

		///<summary></summary>
		public static MedicationPat GetOne(long medicationPatNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<MedicationPat>(MethodBase.GetCurrentMethod(),medicationPatNum);
			}
			string command ="SELECT * FROM medicationpat WHERE MedicationPatNum = "+POut.Long(medicationPatNum);
			return Crud.MedicationPatCrud.SelectOne(command);
		}

		///<summary></summary>
		public static void Update(MedicationPat Cur){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),Cur);
				return;
			}
			Crud.MedicationPatCrud.Update(Cur);
		}

		///<summary></summary>
		public static long Insert(MedicationPat Cur) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Cur.MedicationPatNum=Meth.GetLong(MethodBase.GetCurrentMethod(),Cur);
				return Cur.MedicationPatNum;
			}
			return Crud.MedicationPatCrud.Insert(Cur);
		}

		///<summary></summary>
		public static void Delete(MedicationPat Cur){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),Cur);
				return;
			}
			string command = "DELETE from medicationpat WHERE medicationpatNum = '"
				+Cur.MedicationPatNum.ToString()+"'";
			Db.NonQ(command);
			DeletedObjects.SetDeleted(DeletedObjectType.MedicationPat,Cur.MedicationPatNum);
		}

		public static List<long> GetChangedSinceMedicationPatNums(DateTime changedSince,List<long> eligibleForUploadPatNumList) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<long>>(MethodBase.GetCurrentMethod(),changedSince,eligibleForUploadPatNumList);
			}
			string strEligibleForUploadPatNums="";
			DataTable table;
			if(eligibleForUploadPatNumList.Count>0) {
				for(int i=0;i<eligibleForUploadPatNumList.Count;i++) {
					if(i>0) {
						strEligibleForUploadPatNums+="OR ";
					}
					strEligibleForUploadPatNums+="PatNum='"+eligibleForUploadPatNumList[i].ToString()+"' ";
				}
				string command="SELECT MedicationPatNum FROM medicationpat WHERE DateTStamp > "+POut.DateT(changedSince)+" AND ("+strEligibleForUploadPatNums+")";
				table=Db.GetTable(command);
			}
			else {
				table=new DataTable();
			}
			List<long> medicationpatnums = new List<long>(table.Rows.Count);
			for(int i=0;i<table.Rows.Count;i++) {
				medicationpatnums.Add(PIn.Long(table.Rows[i]["MedicationPatNum"].ToString()));
			}
			return medicationpatnums;
		}

		///<summary>Used along with GetChangedSinceMedicationPatNums</summary>
		public static List<MedicationPat> GetMultMedicationPats(List<long> medicationPatNums) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<MedicationPat>>(MethodBase.GetCurrentMethod(),medicationPatNums);
			}
			string strMedicationPatNums="";
			DataTable table;
			if(medicationPatNums.Count>0) {
				for(int i=0;i<medicationPatNums.Count;i++) {
					if(i>0) {
						strMedicationPatNums+="OR ";
					}
					strMedicationPatNums+="MedicationPatNum='"+medicationPatNums[i].ToString()+"' ";
				}
				string command="SELECT * FROM medicationpat WHERE "+strMedicationPatNums;
				table=Db.GetTable(command);
			}
			else {
				table=new DataTable();
			}
			MedicationPat[] multMedicationPats=Crud.MedicationPatCrud.TableToList(table).ToArray();
			List<MedicationPat> medicationPatList=new List<MedicationPat>(multMedicationPats);
			return medicationPatList;
		}

		///<summary>Get list of MedicationPats by MedicationNum for a particular patient.</summary>
		public static List<MedicationPat> GetMedicationPatsByMedicationNum(long medicationNum,long patNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<MedicationPat>>(MethodBase.GetCurrentMethod(),medicationNum,patNum);
			}
			string command="SELECT * FROM medicationpat WHERE PatNum="+POut.Long(patNum)+" AND MedicationNum="+POut.Long(medicationNum);
			return Crud.MedicationPatCrud.SelectMany(command);
		}


		///<summary>Changes the value of the DateTStamp column to the current time stamp for all medicationpats of a patient</summary>
		public static void ResetTimeStamps(long patNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),patNum);
				return;
			}
			string command="UPDATE medicationpat SET DateTStamp = CURRENT_TIMESTAMP WHERE PatNum ="+POut.Long(patNum);
			Db.NonQ(command);
		}

		///<summary>Used for NewCrop medication orders only.</summary>
		public static MedicationPat GetMedicationOrderByNewCropGuid(string newCropGuid) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<MedicationPat>(MethodBase.GetCurrentMethod(),newCropGuid);
			}
			string command="SELECT * FROM medicationpat WHERE NewCropGuid='"+POut.String(newCropGuid)+"'";
			List<MedicationPat> medicationOrderNewCrop=Crud.MedicationPatCrud.SelectMany(command);
			if(medicationOrderNewCrop.Count==0) {
				return null;
			}
			return medicationOrderNewCrop[0];
		}

		///<summary>Used to synch medication.RxCui with medicationpat.RxCui.  Updates all medicationpat.RxCui to the given value for those medication pats linked to the given medication num.</summary>
		public static void UpdateRxCuiForMedication(long medicationNum,long rxCui) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),medicationNum,rxCui);
				return;
			}
			string command="UPDATE medicationpat SET RxCui="+POut.Long(rxCui)+" WHERE MedicationNum="+POut.Long(medicationNum);
			Db.NonQ(command);
		}

		public static bool IsMedActive(MedicationPat medicationPat) {
			if(medicationPat.DateStop.Year<1880 || medicationPat.DateStop>DateTime.Today) {
				return true;
			}
			return false;
		}
	
	
	
	}

	





}










