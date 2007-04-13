using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	///<summary></summary>
	public class PerioExams{
		///<summary>List of all perio exams for the current patient.</summary>
		public static PerioExam[] List;

		///<summary>Most recent date last.  All exams loaded, even if not displayed.</summary>
		public static void Refresh(int patNum){
			string command=
				"SELECT * from perioexam"
				+" WHERE PatNum = '"+patNum.ToString()+"'"
				+" ORDER BY perioexam.ExamDate";
			DataTable table=General.GetTable(command);
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
			string command= "UPDATE perioexam SET "
				+ "PatNum = '"     +POut.PInt   (Cur.PatNum)+"'"
				+",ExamDate = "   +POut.PDate  (Cur.ExamDate)
				+",ProvNum = '"    +POut.PInt   (Cur.ProvNum)+"'"
				+" WHERE PerioExamNum = '"+POut.PInt(Cur.PerioExamNum)+"'";
			General.NonQ(command);
		}

		///<summary></summary>
		public static void Insert(PerioExam Cur){
			if(PrefB.RandomKeys){
				Cur.PerioExamNum=MiscData.GetKey("perioexam","PerioExamNum");
			}
			string command="INSERT INTO perioexam (";
			if(PrefB.RandomKeys){
				command+="PerioExamNum,";
			}
			command+="PatNum,ExamDate,ProvNum"
				+") VALUES(";
			if(PrefB.RandomKeys){
				command+="'"+POut.PInt(Cur.PerioExamNum)+"', ";
			}
			command+=
				 "'"+POut.PInt   (Cur.PatNum)+"', "
				+POut.PDate  (Cur.ExamDate)+", "
				+"'"+POut.PInt   (Cur.ProvNum)+"')";
			if(PrefB.RandomKeys){
				General.NonQ(command);
			}
			else{
 				Cur.PerioExamNum=General.NonQ(command,true);
			}
		}

		///<summary></summary>
		public static void Delete(PerioExam Cur){
			string command= "DELETE from perioexam WHERE PerioExamNum = '"+Cur.PerioExamNum.ToString()+"'";
			General.NonQ(command);
			command= "DELETE from periomeasure WHERE PerioExamNum = '"+Cur.PerioExamNum.ToString()+"'";
			General.NonQ(command);
		}

		///<summary>Used by PerioMeasures when refreshing to organize array.</summary>
		public static int GetExamIndex(int perioExamNum){
			for(int i=0;i<List.Length;i++){
				if(List[i].PerioExamNum==perioExamNum){
					return i;
				}
			}
			MessageBox.Show("Error. PerioExamNum not in list: "+perioExamNum.ToString());
			return 0;
		}

	
	}
	
	

}















