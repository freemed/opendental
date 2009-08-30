using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Reflection;

namespace OpenDentBusiness {
	///<summary></summary>
	public class Questions {
		///<summary>Gets a list of all Questions for a given patient.  Sorted by ItemOrder.</summary>
		public static Question[] Refresh(int patNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<Question[]>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM question WHERE PatNum="+POut.PInt(patNum)
				+" ORDER BY ItemOrder";
			DataTable table=Db.GetTable(command);
			Question[] List=new Question[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				List[i]=new Question();
				List[i].QuestionNum= PIn.PInt(table.Rows[i][0].ToString());
				List[i].PatNum     = PIn.PInt(table.Rows[i][1].ToString());
				List[i].ItemOrder  = PIn.PInt32(table.Rows[i][2].ToString());
				List[i].Description= PIn.PString(table.Rows[i][3].ToString());
				List[i].Answer     = PIn.PString(table.Rows[i][4].ToString());
				List[i].FormPatNum = PIn.PInt   (table.Rows[i][5].ToString());
			}
			return List;
		}	

		///<summary></summary>
		public static void Update(Question quest) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),quest);
				return;
			}
			string command="UPDATE question SET " 
				+"PatNum = '"      +POut.PInt   (quest.PatNum)+"'"
				+",ItemOrder = '"  +POut.PInt   (quest.ItemOrder)+"'"
				+",Description = '"+POut.PString(quest.Description)+"'"
				+",Answer = '"     +POut.PString(quest.Answer)+"'"
				+",FormPatNum = '" +POut.PInt   (quest.FormPatNum)+"'"
				+" WHERE QuestionNum  ='"+POut.PInt   (quest.QuestionNum)+"'";
			Db.NonQ(command);
		}

		///<summary></summary>
		public static long Insert(Question quest) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				quest.QuestionNum=Meth.GetInt(MethodBase.GetCurrentMethod(),quest);
				return quest.QuestionNum;
			}
			if(PrefC.RandomKeys) {
				quest.QuestionNum=MiscData.GetKey("question","QuestionNum");
			}
			string command="INSERT INTO question (";
			if(PrefC.RandomKeys) {
				command+="QuestionNum,";
			}
			command+="PatNum,ItemOrder,Description,Answer,FormPatNum) VALUES(";
			if(PrefC.RandomKeys) {
				command+="'"+POut.PInt(quest.QuestionNum)+"', ";
			}
			command+=
				 "'"+POut.PInt   (quest.PatNum)+"', "
				+"'"+POut.PInt   (quest.ItemOrder)+"', "
				+"'"+POut.PString(quest.Description)+"', "
				+"'"+POut.PString(quest.Answer)+"', "
				+"'"+POut.PInt   (quest.FormPatNum)+"')";
			if(PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				quest.QuestionNum=Db.NonQ(command,true);
			}
			return quest.QuestionNum;
		}

		//<summary>I can't see how this could ever be used.</summary>
		/*public void Delete() {
			string command="DELETE FROM question WHERE QuestionNum ="+POut.PInt(QuestionNum);
			DataConnection dcon=new DataConnection();
			Db.NonQ(command);
		}*/

	
	
		/*
		///<summary>Checks the database to see if the specified patient has previously answered a questionnaire.</summary>
		public static bool PatHasQuest(int patNum){
			string command="SELECT COUNT(*) FROM question WHERE PatNum="+POut.PInt(patNum);
			if(Db.GetCount(command)=="0"){
				return false;			
			}
			return true;
		}

		///<summary>Deletes all questions for this patient.</summary>
		public static void DeleteAllForPat(int patNum) {
			string command="DELETE FROM question WHERE PatNum ="+POut.PInt(patNum);
			Db.NonQ(command);
		}
	*/
		
		
	}

		



		
	

	

	


}










