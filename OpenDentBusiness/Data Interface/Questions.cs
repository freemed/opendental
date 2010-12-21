using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Reflection;

namespace OpenDentBusiness {
	///<summary></summary>
	public class Questions {
		///<summary>Gets a list of all Questions for a given patient.  Sorted by ItemOrder.</summary>
		public static Question[] Refresh(long patNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<Question[]>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM question WHERE PatNum="+POut.Long(patNum)
				+" ORDER BY ItemOrder";
			DataTable table=Db.GetTable(command);
			Question[] List=new Question[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				List[i]=new Question();
				List[i].QuestionNum= PIn.Long(table.Rows[i][0].ToString());
				List[i].PatNum     = PIn.Long(table.Rows[i][1].ToString());
				List[i].ItemOrder  = PIn.Int(table.Rows[i][2].ToString());
				List[i].Description= PIn.String(table.Rows[i][3].ToString());
				List[i].Answer     = PIn.String(table.Rows[i][4].ToString());
				List[i].FormPatNum = PIn.Long   (table.Rows[i][5].ToString());
			}
			return List;
		}	

		///<summary></summary>
		public static void Update(Question quest) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),quest);
				return;
			}
			Crud.QuestionCrud.Update(quest);
		}

		///<summary></summary>
		public static long Insert(Question quest) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				quest.QuestionNum=Meth.GetLong(MethodBase.GetCurrentMethod(),quest);
				return quest.QuestionNum;
			}
			return Crud.QuestionCrud.Insert(quest);
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










