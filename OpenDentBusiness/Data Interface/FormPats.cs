using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text.RegularExpressions;

namespace OpenDentBusiness{
	///<summary></summary>
	public class FormPats{

		///<summary></summary>
		public static long Insert(FormPat Cur) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Cur.FormPatNum=Meth.GetLong(MethodBase.GetCurrentMethod(),Cur);
				return Cur.FormPatNum;
			}
			if(PrefC.RandomKeys) {
				Cur.FormPatNum=ReplicationServers.GetKey("formpat","FormPatNum");
			}
			string command="INSERT INTO formpat (";
			if(PrefC.RandomKeys) {
				command+="FormPatNum,";
			}
			command+="PatNum,FormDateTime) VALUES(";
			if(PrefC.RandomKeys) {
				command+="'"+POut.Long(Cur.FormPatNum)+"', ";
			}
			command+=
				 "'"+POut.Long  (Cur.PatNum)+"', "
				+POut.DateT(Cur.FormDateTime)+")";
			if(PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				Cur.FormPatNum=Db.NonQ(command,true);
			}
			return Cur.FormPatNum;
		}

		public static FormPat GetOne(long formPatNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<FormPat>(MethodBase.GetCurrentMethod(),formPatNum);
			}
			string command= "SELECT * FROM formpat WHERE FormPatNum="+POut.Long(formPatNum);
			DataTable table=Db.GetTable(command);
			if(table.Rows.Count==0){
				return null;//should never happen.
			}
			FormPat form=new FormPat();
			form.FormPatNum=formPatNum;
			form.PatNum      =PIn.Long  (table.Rows[0][1].ToString());
			form.FormDateTime=PIn.DateT(table.Rows[0][2].ToString());
			form.QuestionList=new List<Question>();
			command="SELECT * FROM question WHERE FormPatNum="+POut.Long(formPatNum);
			table=Db.GetTable(command);
			Question quest;
			for(int i=0;i<table.Rows.Count;i++){
				quest=new Question();
				quest.QuestionNum=PIn.Long   (table.Rows[i][0].ToString());
				quest.PatNum     =PIn.Long   (table.Rows[i][1].ToString());
				quest.ItemOrder  =PIn.Int   (table.Rows[i][2].ToString());
				quest.Description=PIn.String(table.Rows[i][3].ToString());
				quest.Answer     =PIn.String(table.Rows[i][4].ToString());
				quest.FormPatNum =PIn.Long   (table.Rows[i][5].ToString());
				form.QuestionList.Add(quest);
			}
			return form;
		}

		

		///<summary></summary>
		public static void Delete(long formPatNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),formPatNum);
				return;
			}
			string command="DELETE FROM formpat WHERE FormPatNum="+POut.Long(formPatNum);
			Db.NonQ(command);
			command="DELETE FROM question WHERE FormPatNum="+POut.Long(formPatNum);
			Db.NonQ(command);
		}


	}

	
	

}













