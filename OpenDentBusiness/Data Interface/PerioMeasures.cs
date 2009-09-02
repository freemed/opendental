using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Reflection;

namespace OpenDentBusiness{
	///<summary></summary>
	public class PerioMeasures{
		///<summary>List of all perio measures for the current patient. Dim 1 is exams. Dim 2 is Sequences. Dim 3 is Measurements, always 33 per sequence(0 is not used).  This public static variable is only used by the UI.  It's here because it would be complicated to put it in ContrPerio.</summary>
		public static PerioMeasure[,,] List;

		///<summary></summary>
		public static void Update(PerioMeasure Cur){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),Cur);
				return;
			}
			string command = "UPDATE periomeasure SET "
				+ "PerioExamNum = '"+POut.PInt   (Cur.PerioExamNum)+"'"
				+",SequenceType = '"+POut.PInt   ((int)Cur.SequenceType)+"'"
				+",IntTooth = '"    +POut.PInt   (Cur.IntTooth)+"'"
				+",ToothValue = '"  +POut.PInt   (Cur.ToothValue)+"'"
				+",MBvalue = '"     +POut.PInt   (Cur.MBvalue)+"'"
				+",Bvalue = '"      +POut.PInt   (Cur.Bvalue)+"'"
				+",DBvalue = '"     +POut.PInt   (Cur.DBvalue)+"'"
				+",MLvalue = '"     +POut.PInt   (Cur.MLvalue)+"'"
				+",Lvalue = '"      +POut.PInt   (Cur.Lvalue)+"'"
				+",DLvalue = '"     +POut.PInt   (Cur.DLvalue)+"'"
				+" WHERE PerioMeasureNum = '"+POut.PInt(Cur.PerioMeasureNum)+"'";
			Db.NonQ(command);
		}

		///<summary></summary>
		public static long Insert(PerioMeasure Cur) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Cur.PerioMeasureNum=Meth.GetInt(MethodBase.GetCurrentMethod(),Cur);
				return Cur.PerioMeasureNum;
			}
			if(PrefC.RandomKeys){
				Cur.PerioMeasureNum=MiscData.GetKey("periomeasure","PerioMeasureNum");
			}
			string command="INSERT INTO periomeasure (";
			if(PrefC.RandomKeys){
				command+="PerioMeasureNum,";
			}
			command+="PerioExamNum,SequenceType,IntTooth,ToothValue,"
				+"MBvalue,Bvalue,DBvalue,MLvalue,Lvalue,DLvalue"
				+") VALUES(";
			if(PrefC.RandomKeys){
				command+="'"+POut.PInt(Cur.PerioMeasureNum)+"', ";
			}
			command+=
				 "'"+POut.PInt   (Cur.PerioExamNum)+"', "
				+"'"+POut.PInt   ((int)Cur.SequenceType)+"', "
				+"'"+POut.PInt   (Cur.IntTooth)+"', "
				+"'"+POut.PInt   (Cur.ToothValue)+"', "
				+"'"+POut.PInt   (Cur.MBvalue)+"', "
				+"'"+POut.PInt   (Cur.Bvalue)+"', "
				+"'"+POut.PInt   (Cur.DBvalue)+"', "
				+"'"+POut.PInt   (Cur.MLvalue)+"', "
				+"'"+POut.PInt   (Cur.Lvalue)+"', "
				+"'"+POut.PInt   (Cur.DLvalue)+"')";
			if(PrefC.RandomKeys){
				Db.NonQ(command);
			}
			else{
 				Cur.PerioMeasureNum=Db.NonQ(command,true);
			}
			return Cur.PerioMeasureNum;
		}

		///<summary></summary>
		public static void Delete(PerioMeasure Cur){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),Cur);
				return;
			}
			string command= "DELETE from periomeasure WHERE PerioMeasureNum = '"
				+Cur.PerioMeasureNum.ToString()+"'";
			Db.NonQ(command);
		}

		///<summary>For the current exam, clears existing skipped teeth and resets them to the specified skipped teeth. The ArrayList valid values are 1-32 int.</summary>
		public static void SetSkipped(long perioExamNum,ArrayList skippedTeeth) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),perioExamNum,skippedTeeth);
				return;
			}
			//for(int i=0;i<skippedTeeth.Count;i++){
			//MessageBox.Show(skippedTeeth[i].ToString());
			//}
			//first, delete all skipped teeth for this exam
			string command = "DELETE from periomeasure WHERE "
				+"PerioExamNum = '"+perioExamNum.ToString()+"' "
				+"AND SequenceType = '"+POut.PInt((int)PerioSequenceType.SkipTooth)+"'";
			Db.NonQ(command);
			//then add the new ones in one at a time.
			PerioMeasure Cur;
			for(int i=0;i<skippedTeeth.Count;i++){
				Cur=new PerioMeasure();
				Cur.PerioExamNum=perioExamNum;
				Cur.SequenceType=PerioSequenceType.SkipTooth;
				Cur.IntTooth=(int)skippedTeeth[i];
				Cur.ToothValue=1;
				Cur.MBvalue=-1;
				Cur.Bvalue=-1;
				Cur.DBvalue=-1;
				Cur.MLvalue=-1;
				Cur.Lvalue=-1;
				Cur.DLvalue=-1;
				Insert(Cur);
			}
		}

		///<summary>Used in FormPerio.Add_Click. For the specified exam, gets a list of all skipped teeth. The ArrayList valid values are 1-32 int.</summary>
		public static ArrayList GetSkipped(long perioExamNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<ArrayList>(MethodBase.GetCurrentMethod(),perioExamNum);
			}
			string command = "SELECT IntTooth FROM periomeasure WHERE "
				+"SequenceType = '"+POut.PInt((int)PerioSequenceType.SkipTooth)+"' "
				+"AND PerioExamNum = '"+perioExamNum.ToString()+"' "
				+"AND ToothValue = '1'";
			DataTable table=Db.GetTable(command);
			ArrayList retList=new ArrayList();
			for(int i=0;i<table.Rows.Count;i++){
				retList.Add(PIn.PInt(table.Rows[i][0].ToString()));
			}
			return retList;
		}

		///<summary>Gets all measurements for the current patient, then organizes them by exam and sequence.</summary>
		public static void Refresh(long patNum,List<PerioExam> listPerioExams) {
			//No need to check RemotingRole; no call to db.
			DataTable table=GetMeasurementTable(patNum,listPerioExams);
			List=new PerioMeasure[listPerioExams.Count,Enum.GetNames(typeof(PerioSequenceType)).Length,33];
			int curExamI=0;
			PerioMeasure Cur;
			for(int i=0;i<table.Rows.Count;i++) {
				Cur=new PerioMeasure();
				Cur.PerioMeasureNum =PIn.PInt(table.Rows[i][0].ToString());
				Cur.PerioExamNum    =PIn.PInt(table.Rows[i][1].ToString());
				Cur.SequenceType    =(PerioSequenceType)PIn.PInt(table.Rows[i][2].ToString());
				Cur.IntTooth        =PIn.PInt32(table.Rows[i][3].ToString());
				Cur.ToothValue      =PIn.PInt32(table.Rows[i][4].ToString());
				Cur.MBvalue         =PIn.PInt32(table.Rows[i][5].ToString());
				Cur.Bvalue          =PIn.PInt32(table.Rows[i][6].ToString());
				Cur.DBvalue         =PIn.PInt32(table.Rows[i][7].ToString());
				Cur.MLvalue         =PIn.PInt32(table.Rows[i][8].ToString());
				Cur.Lvalue          =PIn.PInt32(table.Rows[i][9].ToString());
				Cur.DLvalue         =PIn.PInt32(table.Rows[i][10].ToString());
				//perioexam.ExamDate                           11
				//the next statement can also handle exams with no measurements:
				if(i==0//if this is the first row
					|| table.Rows[i][1].ToString() != table.Rows[i-1][1].ToString())//or examNum has changed
				{
					curExamI=PerioExams.GetExamIndex(listPerioExams,PIn.PInt32(table.Rows[i][1].ToString()));
				}
				List[curExamI,(int)Cur.SequenceType,Cur.IntTooth]=Cur;
			}
		}

		public static DataTable GetMeasurementTable(long patNum,List<PerioExam> listPerioExams) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetTable(MethodBase.GetCurrentMethod(),patNum,listPerioExams);
			}
			string command =
				"SELECT periomeasure.*,perioexam.ExamDate"
				+" FROM periomeasure,perioexam"
				+" WHERE periomeasure.PerioExamNum = perioexam.PerioExamNum"
				+" AND perioexam.PatNum = '"+patNum.ToString()+"'"
				+" ORDER BY perioexam.ExamDate";
			DataTable table=Db.GetTable(command);
			return table;
		}
			
		

	
	}
	
	

}















