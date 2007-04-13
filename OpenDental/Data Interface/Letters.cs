using System;
using System.Collections;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{

	///<summary>Letters are refreshed as local data.</summary>
	public class Letters{
		///<summary>List of</summary>
		public static Letter[] List;

		///<summary></summary>
		public static void Refresh(){
			//HList=new Hashtable();
			string command = 
				"SELECT * from letter ORDER BY Description";
			DataTable table=General.GetTable(command);
			//Employer temp=new Employer();
			List=new Letter[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				List[i]=new Letter();
				List[i].LetterNum  =PIn.PInt   (table.Rows[i][0].ToString());
				List[i].Description=PIn.PString(table.Rows[i][1].ToString());
				List[i].BodyText   =PIn.PString(table.Rows[i][2].ToString());
				//HList.Add(List[i].LetterNum,List[i]);
			}
		}

		///<summary></summary>
		public static void Update(Letter Cur){
			string command="UPDATE letter SET "
				+ "Description= '" +POut.PString(Cur.Description)+"' "
				+ ",BodyText= '"   +POut.PString(Cur.BodyText)+"' "
				+"WHERE LetterNum = '"+POut.PInt(Cur.LetterNum)+"'";
			General.NonQ(command);
		}

		///<summary></summary>
		public static void Insert(Letter Cur){
			if(PrefB.RandomKeys){
				Cur.LetterNum=MiscData.GetKey("letter","LetterNum");
			}
			string command="INSERT INTO letter (";
			if(PrefB.RandomKeys){
				command+="LetterNum,";
			}
			command+="Description,BodyText) VALUES(";
			if(PrefB.RandomKeys){
				command+="'"+POut.PInt(Cur.LetterNum)+"', ";
			}
			command+=
				 "'"+POut.PString(Cur.Description)+"', "
				+"'"+POut.PString(Cur.BodyText)+"')";
			if(PrefB.RandomKeys){
				General.NonQ(command);
			}
			else{
 				Cur.LetterNum=General.NonQ(command,true);
			}
		}

		///<summary></summary>
		public static void Delete(Letter Cur){
			string command="DELETE from letter WHERE LetterNum = '"+Cur.LetterNum.ToString()+"'";
			General.NonQ(command);
		}

		
	}

	
	

}













