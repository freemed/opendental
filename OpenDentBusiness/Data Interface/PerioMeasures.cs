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
				+ "PerioExamNum = '"+POut.Long   (Cur.PerioExamNum)+"'"
				+",SequenceType = '"+POut.Long   ((int)Cur.SequenceType)+"'"
				+",IntTooth = '"    +POut.Long   (Cur.IntTooth)+"'"
				+",ToothValue = '"  +POut.Long   (Cur.ToothValue)+"'"
				+",MBvalue = '"     +POut.Long   (Cur.MBvalue)+"'"
				+",Bvalue = '"      +POut.Long   (Cur.Bvalue)+"'"
				+",DBvalue = '"     +POut.Long   (Cur.DBvalue)+"'"
				+",MLvalue = '"     +POut.Long   (Cur.MLvalue)+"'"
				+",Lvalue = '"      +POut.Long   (Cur.Lvalue)+"'"
				+",DLvalue = '"     +POut.Long   (Cur.DLvalue)+"'"
				+" WHERE PerioMeasureNum = '"+POut.Long(Cur.PerioMeasureNum)+"'";
			Db.NonQ(command);
			//3-10-10 A bug that only lasted for a few weeks has resulted in a number of duplicate entries for each tooth.
			//So we need to clean up duplicates as we go.  Might put in db maint later.
			command="DELETE FROM periomeasure WHERE "
				+ "PerioExamNum = "+POut.Long(Cur.PerioExamNum)
				+" AND SequenceType = "+POut.Long((int)Cur.SequenceType)
				+" AND IntTooth = "+POut.Long(Cur.IntTooth)
				+" AND PerioMeasureNum != "+POut.Long(Cur.PerioMeasureNum);
			Db.NonQ(command);
		}

		///<summary></summary>
		public static long Insert(PerioMeasure Cur) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Cur.PerioMeasureNum=Meth.GetLong(MethodBase.GetCurrentMethod(),Cur);
				return Cur.PerioMeasureNum;
			}
			if(PrefC.RandomKeys){
				Cur.PerioMeasureNum=ReplicationServers.GetKey("periomeasure","PerioMeasureNum");
			}
			string command="INSERT INTO periomeasure (";
			if(PrefC.RandomKeys){
				command+="PerioMeasureNum,";
			}
			command+="PerioExamNum,SequenceType,IntTooth,ToothValue,"
				+"MBvalue,Bvalue,DBvalue,MLvalue,Lvalue,DLvalue"
				+") VALUES(";
			if(PrefC.RandomKeys){
				command+="'"+POut.Long(Cur.PerioMeasureNum)+"', ";
			}
			command+=
				 "'"+POut.Long   (Cur.PerioExamNum)+"', "
				+"'"+POut.Long   ((int)Cur.SequenceType)+"', "
				+"'"+POut.Long   (Cur.IntTooth)+"', "
				+"'"+POut.Long   (Cur.ToothValue)+"', "
				+"'"+POut.Long   (Cur.MBvalue)+"', "
				+"'"+POut.Long   (Cur.Bvalue)+"', "
				+"'"+POut.Long   (Cur.DBvalue)+"', "
				+"'"+POut.Long   (Cur.MLvalue)+"', "
				+"'"+POut.Long   (Cur.Lvalue)+"', "
				+"'"+POut.Long   (Cur.DLvalue)+"')";
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
		public static void SetSkipped(long perioExamNum,List<int> skippedTeeth) {
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
				+"AND SequenceType = '"+POut.Long((int)PerioSequenceType.SkipTooth)+"'";
			Db.NonQ(command);
			//then add the new ones in one at a time.
			PerioMeasure Cur;
			for(int i=0;i<skippedTeeth.Count;i++){
				Cur=new PerioMeasure();
				Cur.PerioExamNum=perioExamNum;
				Cur.SequenceType=PerioSequenceType.SkipTooth;
				Cur.IntTooth=skippedTeeth[i];
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
		public static List<int> GetSkipped(long perioExamNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<int>>(MethodBase.GetCurrentMethod(),perioExamNum);
			}
			string command = "SELECT IntTooth FROM periomeasure WHERE "
				+"SequenceType = '"+POut.Int((int)PerioSequenceType.SkipTooth)+"' "
				+"AND PerioExamNum = '"+perioExamNum.ToString()+"' "
				+"AND ToothValue = '1'";
			DataTable table=Db.GetTable(command);
			List<int> retVal=new List<int>();
			for(int i=0;i<table.Rows.Count;i++){
				retVal.Add(PIn.Int(table.Rows[i][0].ToString()));
			}
			return retVal;
		}

		///<summary>Gets all measurements for the current patient, then organizes them by exam and sequence.</summary>
		public static void Refresh(long patNum,List<PerioExam> listPerioExams) {
			//No need to check RemotingRole; no call to db.
			DataTable table=GetMeasurementTable(patNum,listPerioExams);
			List=new PerioMeasure[listPerioExams.Count,Enum.GetNames(typeof(PerioSequenceType)).Length,33];
			int examIdx=0;
			//PerioMeasure pm;
			List<PerioMeasure> list=FillFromTable(table);
			for(int i=0;i<list.Count;i++) {
				//the next statement can also handle exams with no measurements:
				if(i==0//if this is the first row
					|| list[i].PerioExamNum != list[i-1].PerioExamNum)//or examNum has changed
				{
					examIdx=PerioExams.GetExamIndex(listPerioExams,list[i].PerioExamNum);
				}
				List[examIdx,(int)list[i].SequenceType,list[i].IntTooth]=list[i];
			}
		}

		private static List<PerioMeasure> FillFromTable(DataTable table) {
			//No need to check RemotingRole; no call to db.
			PerioMeasure pm;
			List<PerioMeasure> retVal=new List<PerioMeasure>();
			for(int i=0;i<table.Rows.Count;i++) {
				pm=new PerioMeasure();
				pm.PerioMeasureNum =PIn.Long(table.Rows[i][0].ToString());
				pm.PerioExamNum    =PIn.Long(table.Rows[i][1].ToString());
				pm.SequenceType    =(PerioSequenceType)PIn.Long(table.Rows[i][2].ToString());
				pm.IntTooth        =PIn.Int(table.Rows[i][3].ToString());
				pm.ToothValue      =PIn.Int(table.Rows[i][4].ToString());
				pm.MBvalue         =PIn.Int(table.Rows[i][5].ToString());
				pm.Bvalue          =PIn.Int(table.Rows[i][6].ToString());
				pm.DBvalue         =PIn.Int(table.Rows[i][7].ToString());
				pm.MLvalue         =PIn.Int(table.Rows[i][8].ToString());
				pm.Lvalue          =PIn.Int(table.Rows[i][9].ToString());
				pm.DLvalue         =PIn.Int(table.Rows[i][10].ToString());
				retVal.Add(pm);
			}
			return retVal;
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

		public static List<PerioMeasure> GetAllForExam(long perioExamNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<PerioMeasure>>(MethodBase.GetCurrentMethod(),perioExamNum);
			}
			string command ="SELECT * FROM periomeasure "
				+"WHERE PerioExamNum = "+POut.Long(perioExamNum);
			DataTable table=Db.GetTable(command);
			return FillFromTable(table);
		}
			
		

	
	}
	
	

}















