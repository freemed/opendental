using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Reflection;

namespace OpenDentBusiness{
	///<summary></summary>
	public class PerioExams{
		///<summary>This is public static because it would be hard to pass it into ContrPerio.  Only used by UI.</summary>
		public static List<PerioExam> ListExams;

		///<summary>Most recent date last.  All exams loaded, even if not displayed.</summary>
		public static void Refresh(long patNum) {
			//No need to check RemotingRole; no call to db.
			DataTable table=GetExamsTable(patNum);
			ListExams=new List<PerioExam>();
			PerioExam exam;
			for(int i=0;i<table.Rows.Count;i++){
				exam=new PerioExam();
				exam.PerioExamNum= PIn.Long   (table.Rows[i][0].ToString());
				exam.PatNum      = PIn.Long(table.Rows[i][1].ToString());
				exam.ExamDate    = PIn.Date(table.Rows[i][2].ToString());
				exam.ProvNum     = PIn.Long(table.Rows[i][3].ToString());
				ListExams.Add(exam);
			}
			//return list;
			//PerioMeasures.Refresh(patNum);
		}

		public static DataTable GetExamsTable(long patNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetTable(MethodBase.GetCurrentMethod(),patNum);
			}
			string command=
				"SELECT * from perioexam"
				+" WHERE PatNum = '"+patNum.ToString()+"'"
				+" ORDER BY perioexam.ExamDate";
			DataTable table=Db.GetTable(command);
			return table;
		}

		///<summary></summary>
		public static void Update(PerioExam Cur){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),Cur);
				return;
			}
			string command= "UPDATE perioexam SET "
				+ "PatNum = '"     +POut.Long   (Cur.PatNum)+"'"
				+",ExamDate = "    +POut.Date  (Cur.ExamDate)
				+",ProvNum = '"    +POut.Long   (Cur.ProvNum)+"'"
				+" WHERE PerioExamNum = '"+POut.Long(Cur.PerioExamNum)+"'";
			Db.NonQ(command);
		}

		///<summary></summary>
		public static long Insert(PerioExam Cur) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Cur.PerioExamNum=Meth.GetLong(MethodBase.GetCurrentMethod(),Cur);
				return Cur.PerioExamNum;
			}
			if(PrefC.RandomKeys){
				Cur.PerioExamNum=ReplicationServers.GetKey("perioexam","PerioExamNum");
			}
			string command="INSERT INTO perioexam (";
			if(PrefC.RandomKeys){
				command+="PerioExamNum,";
			}
			command+="PatNum,ExamDate,ProvNum"
				+") VALUES(";
			if(PrefC.RandomKeys){
				command+="'"+POut.Long(Cur.PerioExamNum)+"', ";
			}
			command+=
				 "'"+POut.Long   (Cur.PatNum)+"', "
				+POut.Date  (Cur.ExamDate)+", "
				+"'"+POut.Long   (Cur.ProvNum)+"')";
			if(PrefC.RandomKeys){
				Db.NonQ(command);
			}
			else{
 				Cur.PerioExamNum=Db.NonQ(command,true);
			}
			return Cur.PerioExamNum;
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

		///<summary>Used by PerioMeasures when refreshing to organize array.</summary>
		public static int GetExamIndex(List<PerioExam> list,long perioExamNum) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<list.Count;i++) {
				if(list[i].PerioExamNum==perioExamNum) {
					return i;
				}
			}
			//MessageBox.Show("Error. PerioExamNum not in list: "+perioExamNum.ToString());
			return 0;
		}
	
	}
}