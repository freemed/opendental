using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	///<summary></summary>
	public class FormPats{

		///<summary></summary>
		public static void Insert(FormPat Cur) {
			if(PrefB.RandomKeys) {
				Cur.FormPatNum=MiscData.GetKey("formpat","FormPatNum");
			}
			string command="INSERT INTO formpat (";
			if(PrefB.RandomKeys) {
				command+="FormPatNum,";
			}
			command+="PatNum,FormDateTime) VALUES(";
			if(PrefB.RandomKeys) {
				command+="'"+POut.PInt(Cur.FormPatNum)+"', ";
			}
			command+=
				 "'"+POut.PInt  (Cur.PatNum)+"', "
				+POut.PDateT(Cur.FormDateTime)+")";
			if(PrefB.RandomKeys) {
				General.NonQ(command);
			}
			else {
				Cur.FormPatNum=General.NonQ(command,true);
			}
		}

		public static FormPat GetOne(int formPatNum){
			string command= "SELECT * FROM formpat WHERE FormPatNum="+POut.PInt(formPatNum);
			DataTable table=General.GetTable(command);
			if(table.Rows.Count==0){
				return null;//should never happen.
			}
			FormPat form=new FormPat();
			form.FormPatNum=formPatNum;
			form.PatNum      =PIn.PInt  (table.Rows[0][1].ToString());
			form.FormDateTime=PIn.PDateT(table.Rows[0][2].ToString());
			form.QuestionList=new List<Question>();
			command="SELECT * FROM question WHERE FormPatNum="+POut.PInt(formPatNum);
			table=General.GetTable(command);
			Question quest;
			for(int i=0;i<table.Rows.Count;i++){
				quest=new Question();
				quest.QuestionNum=PIn.PInt   (table.Rows[i][0].ToString());
				quest.PatNum     =PIn.PInt   (table.Rows[i][1].ToString());
				quest.ItemOrder  =PIn.PInt   (table.Rows[i][2].ToString());
				quest.Description=PIn.PString(table.Rows[i][3].ToString());
				quest.Answer     =PIn.PString(table.Rows[i][4].ToString());
				quest.FormPatNum =PIn.PInt   (table.Rows[i][5].ToString());
				form.QuestionList.Add(quest);
			}
			return form;
		}

		

		///<summary></summary>
		public static void Delete(int formPatNum){
			string command="DELETE FROM formpat WHERE FormPatNum="+POut.PInt(formPatNum);
			General.NonQ(command);
			command="DELETE FROM question WHERE FormPatNum="+POut.PInt(formPatNum);
			General.NonQ(command);
		}


	}

	
	

}













