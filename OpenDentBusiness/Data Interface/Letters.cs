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
			string command=
				"SELECT * from letter ORDER BY Description";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="Letter";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table) {
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
			string command="UPDATE letter SET "
				+ "Description= '" +POut.PString(Cur.Description)+"' "
				+ ",BodyText= '"   +POut.PString(Cur.BodyText)+"' "
				+"WHERE LetterNum = '"+POut.PInt(Cur.LetterNum)+"'";
			Db.NonQ(command);
		}

		///<summary></summary>
		public static void Insert(Letter Cur){
			if(PrefC.RandomKeys){
				Cur.LetterNum=MiscData.GetKey("letter","LetterNum");
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
		}

		///<summary></summary>
		public static void Delete(Letter Cur){
			string command="DELETE from letter WHERE LetterNum = '"+Cur.LetterNum.ToString()+"'";
			Db.NonQ(command);
		}

		
	}

	
	

}













