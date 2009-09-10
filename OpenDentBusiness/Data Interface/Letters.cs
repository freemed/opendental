using System;
using System.Collections;
using System.Data;
using System.Reflection;
using System.Text.RegularExpressions;

namespace OpenDentBusiness{

	///<summary>Letters are refreshed as local data.</summary>
	public class Letters{
		///<summary>List of</summary>
		private static Letter[] list;

		public static Letter[] List {
			//No need to check RemotingRole; no call to db.
			get {
				if(list==null) {
					RefreshCache();
				}
				return list;
			}
			set {
				list=value;
			}
		}

		public static DataTable RefreshCache() {
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string command=
				"SELECT * from letter ORDER BY Description";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="Letter";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table) {
			//No need to check RemotingRole; no call to db.
			List=new Letter[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				List[i]=new Letter();
				List[i].LetterNum=PIn.PInt(table.Rows[i][0].ToString());
				List[i].Description=PIn.PString(table.Rows[i][1].ToString());
				List[i].BodyText=PIn.PString(table.Rows[i][2].ToString());
			}
		}

		///<summary></summary>
		public static void Update(Letter Cur){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),Cur);
				return;
			}
			string command="UPDATE letter SET "
				+ "Description= '" +POut.PString(Cur.Description)+"' "
				+ ",BodyText= '"   +POut.PString(Cur.BodyText)+"' "
				+"WHERE LetterNum = '"+POut.PInt(Cur.LetterNum)+"'";
			Db.NonQ(command);
		}

		///<summary></summary>
		public static long Insert(Letter Cur){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Cur.LetterNum=Meth.GetInt(MethodBase.GetCurrentMethod(),Cur);
				return Cur.LetterNum;
			}
			if(PrefC.RandomKeys){
				Cur.LetterNum=ReplicationServers.GetKey("letter","LetterNum");
			}
			string command="INSERT INTO letter (";
			if(PrefC.RandomKeys){
				command+="LetterNum,";
			}
			command+="Description,BodyText) VALUES(";
			if(PrefC.RandomKeys){
				command+="'"+POut.PInt(Cur.LetterNum)+"', ";
			}
			command+=
				 "'"+POut.PString(Cur.Description)+"', "
				+"'"+POut.PString(Cur.BodyText)+"')";
			if(PrefC.RandomKeys){
				Db.NonQ(command);
			}
			else{
 				Cur.LetterNum=Db.NonQ(command,true);
			}
			return Cur.LetterNum;
		}

		///<summary></summary>
		public static void Delete(Letter Cur){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),Cur);
				return;
			}
			string command="DELETE from letter WHERE LetterNum = '"+Cur.LetterNum.ToString()+"'";
			Db.NonQ(command);
		}

		
	}

	
	

}













