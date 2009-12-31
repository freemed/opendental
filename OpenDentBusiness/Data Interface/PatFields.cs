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
			DataTable table=Db.GetTable(command);
			PatField[] List=new PatField[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				List[i]=new PatField();
				List[i].PatFieldNum= PIn.Long(table.Rows[i][0].ToString());
				List[i].PatNum     = PIn.Long(table.Rows[i][1].ToString());
				List[i].FieldName  = PIn.String(table.Rows[i][2].ToString());
				List[i].FieldValue = PIn.String(table.Rows[i][3].ToString());
			}
			return List;
		}

		///<summary></summary>
		public static void Update(PatField pf) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),pf);
				return;
			}
			string command="UPDATE patfield SET " 
				+"PatNum = '"            +POut.Long   (pf.PatNum)+"'"
				+",FieldName = '"        +POut.String(pf.FieldName)+"'"
				+",FieldValue = '"       +POut.String(pf.FieldValue)+"'"
				+" WHERE PatFieldNum  ='"+POut.Long   (pf.PatFieldNum)+"'";
			Db.NonQ(command);
		}

		///<summary></summary>
		public static long Insert(PatField pf) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				pf.PatFieldNum=Meth.GetLong(MethodBase.GetCurrentMethod(),pf);
				return pf.PatFieldNum;
			}
			if(PrefC.RandomKeys) {
				pf.PatFieldNum=ReplicationServers.GetKey("patfield","PatFieldNum");
			}
			string command="INSERT INTO patfield (";
			if(PrefC.RandomKeys) {
				command+="PatFieldNum,";
			}
			command+="PatNum,FieldName,FieldValue) VALUES(";
			if(PrefC.RandomKeys) {
				command+="'"+POut.Long(pf.PatFieldNum)+"', ";
			}
			command+=
				 "'"+POut.Long   (pf.PatNum)+"', "
				+"'"+POut.String(pf.FieldName)+"', "
				+"'"+POut.String(pf.FieldValue)+"')";
			if(PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				pf.PatFieldNum=Db.NonQ(command,true);
			}
			return pf.PatFieldNum;
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
		
		
	}

		



		
	

	

	


}










