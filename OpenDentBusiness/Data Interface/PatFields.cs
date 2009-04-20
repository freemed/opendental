using System;
using System.Data;
using System.Diagnostics;
using System.Collections;

namespace OpenDentBusiness {
	///<summary></summary>
	public class PatFields {
		///<summary>Gets a list of all PatFields for a given patient.</summary>
		public static PatField[] Refresh(int patNum) {
			string command="SELECT * FROM patfield WHERE PatNum="+POut.PInt(patNum);
			DataTable table=Db.GetTable(command);
			PatField[] List=new PatField[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				List[i]=new PatField();
				List[i].PatFieldNum= PIn.PInt(table.Rows[i][0].ToString());
				List[i].PatNum     = PIn.PInt(table.Rows[i][1].ToString());
				List[i].FieldName  = PIn.PString(table.Rows[i][2].ToString());
				List[i].FieldValue = PIn.PString(table.Rows[i][3].ToString());
			}
			return List;
		}

		///<summary></summary>
		public static void Update(PatField pf) {
			string command="UPDATE patfield SET " 
				+"PatNum = '"            +POut.PInt   (pf.PatNum)+"'"
				+",FieldName = '"        +POut.PString(pf.FieldName)+"'"
				+",FieldValue = '"       +POut.PString(pf.FieldValue)+"'"
				+" WHERE PatFieldNum  ='"+POut.PInt   (pf.PatFieldNum)+"'";
			Db.NonQ(command);
		}

		///<summary></summary>
		public static void Insert(PatField pf) {
			if(PrefC.RandomKeys) {
				pf.PatFieldNum=MiscData.GetKey("patfield","PatFieldNum");
			}
			string command="INSERT INTO patfield (";
			if(PrefC.RandomKeys) {
				command+="PatFieldNum,";
			}
			command+="PatNum,FieldName,FieldValue) VALUES(";
			if(PrefC.RandomKeys) {
				command+="'"+POut.PInt(pf.PatFieldNum)+"', ";
			}
			command+=
				 "'"+POut.PInt   (pf.PatNum)+"', "
				+"'"+POut.PString(pf.FieldName)+"', "
				+"'"+POut.PString(pf.FieldValue)+"')";
			if(PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				pf.PatFieldNum=Db.NonQ(command,true);
			}
		}

		///<summary></summary>
		public static void Delete(PatField pf) {
			string command="DELETE FROM patfield WHERE PatFieldNum ="+POut.PInt(pf.PatFieldNum);
			Db.NonQ(command);
		}

		///<summary>Frequently returns null.</summary>
		public static PatField GetByName(string name,PatField[] fieldList){
			for(int i=0;i<fieldList.Length;i++){
				if(fieldList[i].FieldName==name){
					return fieldList[i];
				}
			}
			return null;
		}
		
		
	}

		



		
	

	

	


}










