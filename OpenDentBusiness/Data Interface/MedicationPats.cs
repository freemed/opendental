using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace OpenDentBusiness{

	///<summary></summary>
	public class MedicationPats{
		///<summary>For current pat.  Only used by UI, not business layer.</summary>
		public static MedicationPat[] List;

		///<summary></summary>
		public static void Refresh(long patNum) {
			//No need to check RemotingRole; no call to db.
			List<MedicationPat> list=GetList(patNum);
			List=list.ToArray();
		}

		public static List<MedicationPat> GetList(long patNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<MedicationPat>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command ="SELECT * from medicationpat WHERE patnum = '"+patNum+"'";
			DataTable table=Db.GetTable(command);
			List<MedicationPat> retVal=new List<MedicationPat>();
			MedicationPat mp;
			for(int i=0;i<table.Rows.Count;i++){
				mp=new MedicationPat();
				mp.MedicationPatNum=PIn.Long(table.Rows[i][0].ToString());
				mp.PatNum          =PIn.Long(table.Rows[i][1].ToString());
				mp.MedicationNum   =PIn.Long(table.Rows[i][2].ToString());
				mp.PatNote         =PIn.String(table.Rows[i][3].ToString());
				retVal.Add(mp);
			}
			return retVal;
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
		}
		
	}

	





}










