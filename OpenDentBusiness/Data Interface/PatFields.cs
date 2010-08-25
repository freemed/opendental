using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Reflection;

namespace OpenDentBusiness {
	///<summary></summary>
	public class PatFields {
		///<summary>Gets a list of all PatFields for a given patient.</summary>
		public static PatField[] Refresh(long patNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<PatField[]>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM patfield WHERE PatNum="+POut.Long(patNum);
			return Crud.PatFieldCrud.SelectMany(command).ToArray();
		}

		///<summary></summary>
		public static void Update(PatField patField) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),patField);
				return;
			}
			Crud.PatFieldCrud.Update(patField);
		}

		///<summary></summary>
		public static long Insert(PatField patField) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				patField.PatFieldNum=Meth.GetLong(MethodBase.GetCurrentMethod(),patField);
				return patField.PatFieldNum;
			}
			return Crud.PatFieldCrud.Insert(patField);
		}

		///<summary></summary>
		public static void Delete(PatField pf) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),pf);
				return;
			}
			string command="DELETE FROM patfield WHERE PatFieldNum ="+POut.Long(pf.PatFieldNum);
			Db.NonQ(command);
		}

		///<summary>Frequently returns null.</summary>
		public static PatField GetByName(string name,PatField[] fieldList){
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<fieldList.Length;i++){
				if(fieldList[i].FieldName==name){
					return fieldList[i];
				}
			}
			return null;
		}

		public static int GetDuplicatePatFieldCount() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetInt(MethodBase.GetCurrentMethod());
			}
			string command=@"SELECT COUNT(*) countDup
				FROM patfield
					WHERE PatNum IN(
					SELECT PatNum
					FROM patfield
					GROUP BY PatNum,FieldName
					HAVING COUNT(*)>1)";
			DataTable table=Db.GetTable(command);
			int retVal=0;
			for(int i=0;i<table.Rows.Count;i++) {
				retVal+=PIn.Int(table.Rows[i][0].ToString());
			}
			return retVal;
		}

		public static void ClearDuplicatePatField() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod());
				return;
			}
			//Create a temp table with duplicated patfields
			string command="DROP TABLE IF EXISTS temppatfieldstodelete";
			Db.NonQ(command);
			command=@"CREATE TABLE temppatfieldstodelete (
				SELECT PatFieldNum
				FROM patfield
				WHERE PatNum IN(
				SELECT PatNum
				FROM patfield
				GROUP BY PatNum,FieldName
				HAVING COUNT(*)>1))";
			Db.NonQ(command);
			command=@"DELETE FROM patfield 
				WHERE PatFieldNum IN(
				SELECT PatFieldNum 
				FROM temppatfieldstodelete)";
			Db.NonQ(command);
			command="DROP TABLE IF EXISTS temppatfieldstodelete";
			Db.NonQ(command);
		}
	}

		



		
	

	

	


}










