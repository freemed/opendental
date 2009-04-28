using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Reflection;

namespace OpenDentBusiness{
	///<summary></summary>
	public class PerioExams{
		///<summary>List of all perio exams for the current patient.</summary>
		public static PerioExam[] List;

		///<summary>Most recent date last.  All exams loaded, even if not displayed.</summary>
		public static void Refresh(int patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),patNum);
				return;
			}
			string command=
				"SELECT * from perioexam"
				+" WHERE PatNum = '"+patNum.ToString()+"'"
				+" ORDER BY perioexam.ExamDate";
			DataTable table=Db.GetTable(command);
			List=new PerioExam[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++){
				List[i]=new PerioExam();
				List[i].PerioExamNum= PIn.PInt   (table.Rows[i][0].ToString());
				List[i].PatNum      = PIn.PInt   (table.Rows[i][1].ToString());
				List[i].ExamDate    = PIn.PDate  (table.Rows[i][2].ToString());
				List[i].ProvNum     = PIn.PInt   (table.Rows[i][3].ToString());
			}
			//PerioMeasures.Refresh(patNum);
		}

		///<summary></summary>
		public static void Update(PerioExam Cur){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),Cur);
				return;
			}
			string command= "UPDATE perioexam SET "
				+ "PatNum = '"     +POut.PInt   (Cur.PatNum)+"'"
				+",ExamDate = "    +POut.PDate  (Cur.ExamDate)
				+",ProvNum = '"    +POut.PInt   (Cur.ProvNum)+"'"
				+" WHERE PerioExamNum = '"+POut.PInt(Cur.PerioExamNum)+"'";
			Db.NonQ(command);
		}

		///<summary></summary>
		public static void Insert(PerioExam Cur){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),Cur);
				return;
			}
			if(PrefC.RandomKeys){
				Cur.PerioExamNum=MiscData.GetKey("perioexam","PerioExamNum");
			}
			string command="INSERT INTO perioexam (";
			if(PrefC.RandomKeys){
				command+="PerioExamNum,";
			}
			command+="PatNum,ExamDate,ProvNum"
				+") VALUES(";
			if(PrefC.RandomKeys){
				command+="'"+POut.PInt(Cur.PerioExamNum)+"', ";
			}
			command+=
				 "'"+POut.PInt   (Cur.PatNum)+"', "
				+POut.PDate  (Cur.ExamDate)+", "
				+"'"+POut.PInt   (Cur.ProvNum)+"')";
			if(PrefC.RandomKeys){
				Db.NonQ(command);
			}
			else{
 				Cur.PerioExamNum=Db.NonQ(command,true);
			}
		}

		///<summary></summary>
		public static void Delete(PerioExam Cur){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),Cur);
				return;
			}
			string command= "DELETE from perioexam WHERE PerioExamNum = '"+Cur.PerioExamNum.ToString()+"'";
			Db.NonQ(command);
			command= "DELETE from periomeasure WHERE PerioExamNum = '"+Cur.PerioExamNum.ToString()+"'";
			Db.NonQ(command);
		}
	
	}
}