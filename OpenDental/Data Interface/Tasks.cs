using System;
using System.Collections;
using System.Collections.Generic;
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

		/*
		///<summary>Gets all tasks for the trunk of the user tab.</summary>
		public static List<Task> RefreshUserTrunk(int userNum) {
			string command="SELECT task.* FROM tasksubscription "
				+"LEFT JOIN task ON task.TaskNum=tasksubscription.TaskNum "
				+"WHERE tasksubscription.UserNum="+POut.PInt(userNum)
				+" AND tasksubscription.TaskNum!=0 "
				+"ORDER BY DateTimeEntry";
			return RefreshAndFill(command);
		}*/

		///<summary>Gets all tasks for the main trunk.</summary>
		public static List<Task> RefreshMainTrunk() {
			string command="SELECT * FROM task "
				+"WHERE TaskListNum=0 "
				+"AND DateTask < '1880-01-01' "
				+"AND IsRepeating=0 "
				+"ORDER BY DateTimeEntry";
			return RefreshAndFill(command);
		}

		///<summary>Gets all tasks for the repeating trunk.</summary>
		public static List<Task> RefreshRepeatingTrunk() {
			string command="SELECT * FROM task "
				+"WHERE TaskListNum=0 "
				+"AND DateTask < '1880-01-01' "
				+"AND IsRepeating=1 "
				+"ORDER BY DateTimeEntry";
			return RefreshAndFill(command);
		}

		///<summary>0 is not allowed, because that would be a trunk.</summary>
		public static List<Task> RefreshChildren(int listNum) {
			string command=
				"SELECT * FROM task "
				+"WHERE TaskListNum="+POut.PInt(listNum)
				+" ORDER BY DateTimeEntry";
			return RefreshAndFill(command);
		}

		///<summary>All repeating items for one date type with no heirarchy.</summary>
		public static List<Task> RefreshRepeating(TaskDateType dateType) {
			string command=
				"SELECT * FROM task "
				+"WHERE IsRepeating=1 "
				+"AND DateType="+POut.PInt((int)dateType)+" "
				+"ORDER BY DateTimeEntry";
			return RefreshAndFill(command);
		}

		///<summary>Gets all tasks for one of the 3 dated trunks.</summary>
		public static List<Task> RefreshDatedTrunk(DateTime date,TaskDateType dateType) {
			DateTime dateFrom=DateTime.MinValue;
			DateTime dateTo=DateTime.MaxValue;
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
			string command=
				"SELECT * FROM task "
				+"WHERE DateTask >= "+POut.PDate(dateFrom)
				+" AND DateTask <= "+POut.PDate(dateTo)
				+" AND DateType="+POut.PInt((int)dateType)
				+" ORDER BY DateTimeEntry";
			return RefreshAndFill(command);
		}

		///<summary>Only used once when first synching all the tasks for taskAncestors.  Then, never used again.</summary>
		public static List<Task> RefreshAll(){
			string command="SELECT * FROM task WHERE TaskListNum != 0";
			return RefreshAndFill(command);
		}

		private static List<Task> RefreshAndFill(string command){
			DataTable table=General.GetTable(command);
			List<Task> retVal=new List<Task>();
			Task task;
			for(int i=0;i<table.Rows.Count;i++) {
				task=new Task();
				task.TaskNum        = PIn.PInt(table.Rows[i][0].ToString());
				task.TaskListNum    = PIn.PInt(table.Rows[i][1].ToString());
				task.DateTask       = PIn.PDate(table.Rows[i][2].ToString());
				task.KeyNum         = PIn.PInt(table.Rows[i][3].ToString());
				task.Descript       = PIn.PString(table.Rows[i][4].ToString());
				task.TaskStatus     = PIn.PBool(table.Rows[i][5].ToString());
				task.IsRepeating    = PIn.PBool(table.Rows[i][6].ToString());
				task.DateType       = (TaskDateType)PIn.PInt(table.Rows[i][7].ToString());
				task.FromNum        = PIn.PInt(table.Rows[i][8].ToString());
				task.ObjectType     = (TaskObjectType)PIn.PInt(table.Rows[i][9].ToString());
				task.DateTimeEntry  = PIn.PDateT(table.Rows[i][10].ToString());
				retVal.Add(task);
			}
			return retVal;
		}

		///<summary>Must supply the supposedly unaltered oldTask.  The update will fail if oldTask does not exactly match the database state.  Keeps users from overwriting each other's changes.</summary>
		public static void Update(Task task,Task oldTask){
			if(task.IsRepeating && task.DateTask.Year>1880) {
				throw new Exception(Lan.g("Tasks","Task cannot be tagged repeating and also have a date."));
			}
			if(task.IsRepeating && task.TaskStatus) {//and complete
				throw new Exception(Lan.g("Tasks","Task cannot be tagged repeating and also be marked complete."));
			}
			if(task.IsRepeating && task.TaskListNum!=0 && task.DateType!=TaskDateType.None) {//In repeating, children not allowed to repeat.
				throw new Exception(Lan.g("Tasks","In repeating tasks, only the main parents can have a task status."));
			}
			if(WasTaskAltered(oldTask)){
				throw new Exception(Lan.g("Tasks","Not allowed to save changes because the task has been altered by someone else."));
			}
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
			//need to optimize this later to skip unless TaskListNumChanged
			TaskAncestors.Synch(task);
		}

		///<summary></summary>
		public static void Insert(Task task){
			if(task.IsRepeating && task.DateTask.Year>1880) {
				throw new Exception(Lan.g("Tasks","Task cannot be tagged repeating and also have a date."));
			}
			if(task.IsRepeating && task.TaskStatus) {//and complete
				throw new Exception(Lan.g("Tasks","Task cannot be tagged repeating and also be marked complete."));
			}
			if(task.IsRepeating && task.TaskListNum!=0 && task.DateType!=TaskDateType.None) {//In repeating, children not allowed to repeat.
				throw new Exception(Lan.g("Tasks","In repeating tasks, only the main parents can have a task status."));
			}
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
			TaskAncestors.Synch(task);
		}

		///<summary></summary>
		public static bool WasTaskAltered(Task task){
			string command="SELECT * FROM task WHERE TaskNum="+POut.PInt(task.TaskNum);
			Task oldtask=RefreshAndFill(command)[0];
			if(oldtask.DateTask!=task.DateTask
					|| oldtask.DateType!=task.DateType
					|| oldtask.Descript!=task.Descript
					|| oldtask.FromNum!=task.FromNum
					|| oldtask.IsRepeating!=task.IsRepeating
					|| oldtask.KeyNum!=task.KeyNum
					|| oldtask.ObjectType!=task.ObjectType
					|| oldtask.TaskListNum!=task.TaskListNum
					|| oldtask.TaskStatus!=task.TaskStatus)
			{
				return true;
			}
			return false;
		}

		///<summary>Deleting a task never causes a problem, so no dependencies are checked.</summary>
		public static void Delete(Task task){
			string command= "DELETE from task WHERE TaskNum = "+POut.PInt(task.TaskNum);
 			General.NonQ(command);
			command="DELETE from taskancestor WHERE TaskNum = "+POut.PInt(task.TaskNum);
			General.NonQ(command);
		}

		public static int UserTasksCount(int userNum){
			string command="SELECT COUNT(*) FROM taskancestor,task,tasklist,tasksubscription "
				+"WHERE taskancestor.TaskListNum=tasklist.TaskListNum "
				+"AND task.TaskNum=taskancestor.TaskNum "
				+"AND tasksubscription.TaskListNum=tasklist.TaskListNum "
				+"AND tasksubscription.UserNum="+POut.PInt(userNum)
				+" AND task.TaskStatus=0";
			return PIn.PInt(General.GetCount(command));
		}
	
	

	
	}

	

	


}




















