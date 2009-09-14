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
			string command="SELECT * FROM patfield WHERE PatNum="+POut.PLong(patNum);
			DataTable table=Db.GetTable(command);
			PatField[] List=new PatField[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				List[i]=new PatField();
				List[i].PatFieldNum= PIn.PLong(table.Rows[i][0].ToString());
				List[i].PatNum     = PIn.PLong(table.Rows[i][1].ToString());
				List[i].FieldName  = PIn.PString(table.Rows[i][2].ToString());
				List[i].FieldValue = PIn.PString(table.Rows[i][3].ToString());
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
				+"PatNum = '"            +POut.PLong   (pf.PatNum)+"'"
				+",FieldName = '"        +POut.PString(pf.FieldName)+"'"
				+",FieldValue = '"       +POut.PString(pf.FieldValue)+"'"
				+" WHERE PatFieldNum  ='"+POut.PLong   (pf.PatFieldNum)+"'";
			Db.NonQ(command);
		}

		///<summary></summary>
		public static long Insert(PatField pf) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				pf.PatFieldNum=Meth.GetInt(MethodBase.GetCurrentMethod(),pf);
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
				command+="'"+POut.PLong(pf.PatFieldNum)+"', ";
			}
			command+=
				 "'"+POut.PLong   (pf.PatNum)+"', "
				+"'"+POut.PString(pf.FieldName)+"', "
				+"'"+POut.PString(pf.FieldValue)+"')";
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
			string command="DELETE FROM patfield WHERE PatFieldNum ="+POut.PLong(pf.PatFieldNum);
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










