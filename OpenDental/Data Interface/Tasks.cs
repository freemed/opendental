using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	///<summary></summary>
	public class Tasks {
		///<summary></summary>
		public static ArrayList LastOpenList;
		///<summary></summary>
		public static int LastOpenGroup;
		///<summary></summary>
		public static DateTime LastOpenDate;

		///<summary>Gets all tasks for a given taskList.  But the 5 trunks don't have parents: For main trunk use date.Min and TaskListNum=0.  For Repeating trunk use date.Min isRepeating and TaskListNum=0.  For the 3 dated trunks, use a date and a dateType.  Date and TaskListNum are mutually exclusive.  Also used to get all repeating tasks for one dateType without any heirarchy: supply listNum=-1.</summary>
		public static Task[] Refresh(int listNum,DateTime date,TaskDateType dateType,bool isRepeating) {
			DateTime dateFrom=DateTime.MinValue;
			DateTime dateTo=DateTime.MaxValue;
			string where="";
			if(date.Year>1880) {
				//date supplied always indicates one of 3 dated trunks.
				//the value of listNum is completely ignored
				if(dateType==TaskDateType.Day) {
					dateFrom=date;
					dateTo=date;
				}
				else if(dateType==TaskDateType.Week) {
					dateFrom=date.AddDays(-(int)date.DayOfWeek);
					dateTo=dateFrom.AddDays(6);
				}
				else if(dateType==TaskDateType.Month) {
					dateFrom=new DateTime(date.Year,date.Month,1);
					dateTo=dateFrom.AddMonths(1).AddDays(-1);
				}
				where="DateTask >= "+POut.PDate(dateFrom)
					+" AND DateTask <= "+POut.PDate(dateTo)+" "
					+"AND DateType="+POut.PInt((int)dateType)+" ";
			}
			else {//no date supplied.
				if(listNum==0) {//main trunk or repeating trunk
					where="TaskListNum="+POut.PInt(listNum)
						+" AND DateTask < '1880-01-01'"
						+" AND IsRepeating="+POut.PBool(isRepeating)+" ";
				}
				else if(listNum==-1 && isRepeating) {//all repeating items with no heirarchy
					where="IsRepeating=1 "
						+"AND DateType="+POut.PInt((int)dateType)+" ";
				}
				else {//any child
					where="TaskListNum="+POut.PInt(listNum)+" ";
					//+" AND IsRepeating="+POut.PBool(isRepeating)+" ";
				}
			}
			string command=
				"SELECT * FROM task "
				+"WHERE "+where
				+"ORDER BY DateTimeEntry";
			DataTable table=General.GetTable(command);
			Task[] List=new Task[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				List[i]=new Task();
				List[i].TaskNum        = PIn.PInt(table.Rows[i][0].ToString());
				List[i].TaskListNum    = PIn.PInt(table.Rows[i][1].ToString());
				List[i].DateTask       = PIn.PDate(table.Rows[i][2].ToString());
				List[i].KeyNum         = PIn.PInt(table.Rows[i][3].ToString());
				List[i].Descript       = PIn.PString(table.Rows[i][4].ToString());
				List[i].TaskStatus     = PIn.PBool(table.Rows[i][5].ToString());
				List[i].IsRepeating    = PIn.PBool(table.Rows[i][6].ToString());
				List[i].DateType       = (TaskDateType)PIn.PInt(table.Rows[i][7].ToString());
				List[i].FromNum        = PIn.PInt(table.Rows[i][8].ToString());
				List[i].ObjectType     = (TaskObjectType)PIn.PInt(table.Rows[i][9].ToString());
				List[i].DateTimeEntry  = PIn.PDateT(table.Rows[i][10].ToString());
			}
			return List;
		}
	

		///<summary></summary>
		private static void Update(Task task){
			string command= "UPDATE task SET " 
				+"TaskListNum = '"    +POut.PInt   (task.TaskListNum)+"'"
				+",DateTask = "      +POut.PDate  (task.DateTask)
				+",KeyNum = '"        +POut.PInt   (task.KeyNum)+"'"
				+",Descript = '"      +POut.PString(task.Descript)+"'"
				+",TaskStatus = '"    +POut.PBool  (task.TaskStatus)+"'"
				+",IsRepeating = '"   +POut.PBool  (task.IsRepeating)+"'"
				+",DateType = '"      +POut.PInt   ((int)task.DateType)+"'"
				+",FromNum = '"       +POut.PInt   (task.FromNum)+"'"
				+",ObjectType = '"    +POut.PInt   ((int)task.ObjectType)+"'"
				+",DateTimeEntry = " +POut.PDateT (task.DateTimeEntry)
				+" WHERE TaskNum = '"+POut.PInt(task.TaskNum)+"'";
 			General.NonQ(command);
		}

		///<summary></summary>
		private static void Insert(Task task){
			if(PrefB.RandomKeys){
				task.TaskNum=MiscData.GetKey("task","TaskNum");
			}
			string command= "INSERT INTO task (";
			if(PrefB.RandomKeys){
				command+="TaskNum,";
			}
			command+="TaskListNum,DateTask,KeyNum,Descript,TaskStatus,"
				+"IsRepeating,DateType,FromNum,ObjectType,DateTimeEntry) VALUES(";
			if(PrefB.RandomKeys){
				command+="'"+POut.PInt(task.TaskNum)+"', ";
			}
			command+=
				 "'"+POut.PInt   (task.TaskListNum)+"', "
				+POut.PDate  (task.DateTask)+", "
				+"'"+POut.PInt   (task.KeyNum)+"', "
				+"'"+POut.PString(task.Descript)+"', "
				+"'"+POut.PBool  (task.TaskStatus)+"', "
				+"'"+POut.PBool  (task.IsRepeating)+"', "
				+"'"+POut.PInt   ((int)task.DateType)+"', "
				+"'"+POut.PInt   (task.FromNum)+"', "
				+"'"+POut.PInt   ((int)task.ObjectType)+"', "
				+POut.PDateT (task.DateTimeEntry)+")";
				//+"NOW())";//DateTimeEntry set to current server time
 			if(PrefB.RandomKeys){
				General.NonQ(command);
			}
			else{
 				task.TaskNum=General.NonQ(command,true);
			}
		}

		///<summary></summary>
		public static void InsertOrUpdate(Task task, bool isNew){
			if(task.IsRepeating && task.DateTask.Year>1880){
				throw new Exception(Lan.g("Tasks","Task cannot be tagged repeating and also have a date."));
			}
			if(task.IsRepeating && task.TaskStatus){//and complete
				throw new Exception(Lan.g("Tasks","Task cannot be tagged repeating and also be marked complete."));
			}
			if(task.IsRepeating && task.TaskListNum!=0 && task.DateType!=TaskDateType.None){//In repeating, children not allowed to repeat.
				throw new Exception(Lan.g("Tasks","In repeating tasks, only the main parents can have a task status."));
			}
			if(isNew){
				Insert(task);
			}
			else{
				Update(task);
			}
		}

		///<summary>Deleting a task never causes a problem, so no dependencies are checked.</summary>
		public static void Delete(Task task){
			string command= "DELETE from task WHERE TaskNum = '"
				+POut.PInt(task.TaskNum)+"'";
 			General.NonQ(command);
		}


	
	

	
	}

	

	


}




















