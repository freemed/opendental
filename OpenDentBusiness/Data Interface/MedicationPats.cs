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
		public static void Refresh(int patNum){
			//No need to check RemotingRole; no call to db.
			List<MedicationPat> list=GetList(patNum);
			List=list.ToArray();
		}

		public static List<MedicationPat> GetList(int patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<MedicationPat>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command ="SELECT * from medicationpat WHERE patnum = '"+patNum+"'";
			DataTable table=Db.GetTable(command);
			List<MedicationPat> retVal=new List<MedicationPat>();
			MedicationPat mp;
			for(int i=0;i<table.Rows.Count;i++){
				mp=new MedicationPat();
				mp.MedicationPatNum=PIn.PInt(table.Rows[i][0].ToString());
				mp.PatNum          =PIn.PInt(table.Rows[i][1].ToString());
				mp.MedicationNum   =PIn.PInt(table.Rows[i][2].ToString());
				mp.PatNote         =PIn.PString(table.Rows[i][3].ToString());
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
			string command = "UPDATE medicationpat SET " 
				+ "patnum = '"        +POut.PInt   (Cur.PatNum)+"'"
				+ ",medicationnum = '"+POut.PInt   (Cur.MedicationNum)+"'"
				+ ",patnote = '"      +POut.PString(Cur.PatNote)+"'"
				+" WHERE medicationpatnum = '" +POut.PInt   (Cur.MedicationPatNum)+"'";
			//MessageBox.Show(command);
			Db.NonQ(command);
		}

		///<summary></summary>
		public static int Insert(MedicationPat Cur){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Cur.MedicationPatNum=Meth.GetInt(MethodBase.GetCurrentMethod(),Cur);
				return Cur.MedicationPatNum;
			}
			if(PrefC.RandomKeys){
				Cur.MedicationPatNum=MiscData.GetKey("medicationpat","MedicationPatNum");
			}
			string command="INSERT INTO medicationpat (";
			if(PrefC.RandomKeys){
				command+="MedicationPatNum,";
			}
			command+="patnum,medicationnum,patnote"
				+") VALUES(";
			if(PrefC.RandomKeys){
				command+="'"+POut.PInt(Cur.MedicationPatNum)+"', ";
			}
			command+=
				 "'"+POut.PInt   (Cur.PatNum)+"', "
				+"'"+POut.PInt   (Cur.MedicationNum)+"', "
				+"'"+POut.PString(Cur.PatNote)+"')";
			if(PrefC.RandomKeys){
				Db.NonQ(command);
			}
			else{
 				Cur.MedicationPatNum=Db.NonQ(command,true);
			}
			return Cur.MedicationPatNum;
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










