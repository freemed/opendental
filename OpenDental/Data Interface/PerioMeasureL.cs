using System;
using System.Collections;
using System.Data;
using System.Drawing;
using OpenDentBusiness;

namespace OpenDental{
	///<summary></summary>
	public class PerioMeasureL{

		///<summary>Gets all measurements for the current patient, then organizes them by exam and sequence.</summary>
		public static void Refresh(int patNum){
			string command =
				"SELECT periomeasure.*,perioexam.ExamDate"
				+" FROM periomeasure,perioexam"
				+" WHERE periomeasure.PerioExamNum = perioexam.PerioExamNum"
				+" AND perioexam.PatNum = '"+patNum.ToString()+"'"
				+" ORDER BY perioexam.ExamDate";
			DataTable table=General.GetTable(command);
			PerioMeasures.List=new PerioMeasure[PerioExams.List.Length,Enum.GetNames(typeof(PerioSequenceType)).Length,33];
			int curExamI=0;
			PerioMeasure Cur;
			for(int i=0;i<table.Rows.Count;i++){
				Cur=new PerioMeasure();
				Cur.PerioMeasureNum =PIn.PInt   (table.Rows[i][0].ToString());
				Cur.PerioExamNum    =PIn.PInt   (table.Rows[i][1].ToString());
				Cur.SequenceType    =(PerioSequenceType)PIn.PInt   (table.Rows[i][2].ToString());
				Cur.IntTooth        =PIn.PInt   (table.Rows[i][3].ToString());
				Cur.ToothValue      =PIn.PInt   (table.Rows[i][4].ToString());
				Cur.MBvalue         =PIn.PInt   (table.Rows[i][5].ToString());
				Cur.Bvalue          =PIn.PInt   (table.Rows[i][6].ToString());
				Cur.DBvalue         =PIn.PInt   (table.Rows[i][7].ToString());
				Cur.MLvalue         =PIn.PInt   (table.Rows[i][8].ToString());
				Cur.Lvalue          =PIn.PInt   (table.Rows[i][9].ToString());
				Cur.DLvalue         =PIn.PInt   (table.Rows[i][10].ToString());
				//perioexam.ExamDate                           11
				//the next statement can also handle exams with no measurements:
				if(i==0//if this is the first row
					|| table.Rows[i][1].ToString() != table.Rows[i-1][1].ToString())//or examNum has changed
				{
					curExamI=PerioExamL.GetExamIndex(PIn.PInt(table.Rows[i][1].ToString()));
				}
				PerioMeasures.List[curExamI,(int)Cur.SequenceType,Cur.IntTooth]=Cur;
			}
		}
	
	}
}