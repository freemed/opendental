using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	///<summary></summary>
	public class LetterMergeFields {
		///<summary>List of all lettermergeFields.</summary>
		private static LetterMergeField[] Listt;

		///<summary></summary>
		public static void Refresh() {
			string command=
				"SELECT * FROM lettermergefield "
				+"ORDER BY FieldName";
			DataTable table=General.GetTable(command);
			Listt=new LetterMergeField[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				Listt[i]=new LetterMergeField();
				Listt[i].FieldNum      = PIn.PInt(table.Rows[i][0].ToString());
				Listt[i].LetterMergeNum= PIn.PInt(table.Rows[i][1].ToString());
				Listt[i].FieldName     = PIn.PString(table.Rows[i][2].ToString());
			}
		}

		///<summary>Inserts this lettermergefield into database.</summary>
		public static void Insert(LetterMergeField lmf){
			if(PrefC.RandomKeys){
				lmf.FieldNum=MiscData.GetKey("lettermergefield","FieldNum");
			}
			string command= "INSERT INTO lettermergefield (";
			if(PrefC.RandomKeys){
				command+="FieldNum,";
			}
			command+="LetterMergeNum,FieldName"
				+") VALUES(";
			if(PrefC.RandomKeys){
				command+="'"+POut.PInt(lmf.FieldNum)+"', ";
			}
			command+=
				 "'"+POut.PInt   (lmf.LetterMergeNum)+"', "
				+"'"+POut.PString(lmf.FieldName)+"')";
			//MessageBox.Show(string command);
 			if(PrefC.RandomKeys){
				General.NonQ(command);
			}
			else{
 				lmf.FieldNum=General.NonQ(command,true);
			}
		}

		/*
		///<summary></summary>
		public void Update(){
			string command="UPDATE lettermergefield SET "
				+"LetterMergeNum = '"+POut.PInt   (LetterMergeNum)+"' "
				+",FieldName = '"    +POut.PString(FieldName)+"' "
				+"WHERE FieldNum = '"+POut.PInt(FieldNum)+"'";
			DataConnection dcon=new DataConnection();
 			General.NonQ(command);
		}*/

		/*
		///<summary></summary>
		public void Delete(){
			string command="DELETE FROM lettermergefield "
				+"WHERE FieldNum = "+POut.PInt(FieldNum);
			DataConnection dcon=new DataConnection();
			General.NonQ(command);
		}*/

		///<summary>Called from LetterMerge.Refresh() to get all field names for a given letter.  The result is a collection of strings representing field names.</summary>
		public static List<string> GetForLetter(int letterMergeNum){
			List<string> retVal=new List<string>();
			for(int i=0;i<Listt.Length;i++){
				if(Listt[i].LetterMergeNum==letterMergeNum){
					retVal.Add(Listt[i].FieldName);
				}
			}
			return retVal;
		}

		///<summary>Deletes all lettermergefields for the given letter.  This is then followed by adding them all back, which is simpler than just updating.</summary>
		public static void DeleteForLetter(int letterMergeNum){
			string command="DELETE FROM lettermergefield "
				+"WHERE LetterMergeNum = "+POut.PInt(letterMergeNum);
			General.NonQ(command);
		}

		
		


	}

	



}









