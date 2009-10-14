using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace OpenDentBusiness{
	///<summary></summary>
	public class LetterMergeFields {
		///<summary>List of all lettermergeFields.</summary>
		private static LetterMergeField[] list;

		public static DataTable RefreshCache() {
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string command=
				"SELECT * FROM lettermergefield "
				+"ORDER BY FieldName";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="LetterMergeField";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table) {
			//No need to check RemotingRole; no call to db.
			list=new LetterMergeField[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				list[i]=new LetterMergeField();
				list[i].FieldNum=PIn.PLong(table.Rows[i][0].ToString());
				list[i].LetterMergeNum=PIn.PLong(table.Rows[i][1].ToString());
				list[i].FieldName=PIn.PString(table.Rows[i][2].ToString());
			}
		}

		///<summary>Inserts this lettermergefield into database.</summary>
		public static long Insert(LetterMergeField lmf) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				lmf.FieldNum=Meth.GetLong(MethodBase.GetCurrentMethod(),lmf);
				return lmf.FieldNum;
			}
			if(PrefC.RandomKeys){
				lmf.FieldNum=ReplicationServers.GetKey("lettermergefield","FieldNum");
			}
			string command= "INSERT INTO lettermergefield (";
			if(PrefC.RandomKeys){
				command+="FieldNum,";
			}
			command+="LetterMergeNum,FieldName"
				+") VALUES(";
			if(PrefC.RandomKeys){
				command+="'"+POut.PLong(lmf.FieldNum)+"', ";
			}
			command+=
				 "'"+POut.PLong   (lmf.LetterMergeNum)+"', "
				+"'"+POut.PString(lmf.FieldName)+"')";
 			if(PrefC.RandomKeys){
				Db.NonQ(command);
			}
			else{
 				lmf.FieldNum=Db.NonQ(command,true);
			}
			return lmf.FieldNum;
		}

		/*
		///<summary></summary>
		public void Update(){
			string command="UPDATE lettermergefield SET "
				+"LetterMergeNum = '"+POut.PInt   (LetterMergeNum)+"' "
				+",FieldName = '"    +POut.PString(FieldName)+"' "
				+"WHERE FieldNum = '"+POut.PInt(FieldNum)+"'";
			DataConnection dcon=new DataConnection();
 			Db.NonQ(command);
		}*/

		/*
		///<summary></summary>
		public void Delete(){
			string command="DELETE FROM lettermergefield "
				+"WHERE FieldNum = "+POut.PInt(FieldNum);
			DataConnection dcon=new DataConnection();
			Db.NonQ(command);
		}*/

		///<summary>Called from LetterMerge.Refresh() to get all field names for a given letter.  The result is a collection of strings representing field names.</summary>
		public static List<string> GetForLetter(long letterMergeNum) {
			//No need to check RemotingRole; no call to db.
			if(list==null) {
				RefreshCache();
			}
			List<string> retVal=new List<string>();
			for(int i=0;i<list.Length;i++) {
				if(list[i].LetterMergeNum==letterMergeNum) {
					retVal.Add(list[i].FieldName);
				}
			}
			return retVal;
		}

		///<summary>Deletes all lettermergefields for the given letter.  This is then followed by adding them all back, which is simpler than just updating.</summary>
		public static void DeleteForLetter(long letterMergeNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),letterMergeNum);
				return;
			}
			string command="DELETE FROM lettermergefield "
				+"WHERE LetterMergeNum = "+POut.PLong(letterMergeNum);
			Db.NonQ(command);
		}

		
		


	}

	



}









