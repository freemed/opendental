using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Globalization;

namespace OpenDentBusiness{
	///<summary></summary>
	public class TaskLists {

		///<summary>Gets all task lists for the trunk of the user tab.</summary>
		public static List<TaskList> RefreshUserTrunk(long userNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<TaskList>>(MethodBase.GetCurrentMethod(),userNum);
			}
			string command="SELECT tasklist.*,"
				+"(SELECT COUNT(*) FROM taskancestor,task WHERE taskancestor.TaskListNum=tasklist.TaskListNum "
				+"AND task.TaskNum=taskancestor.TaskNum AND task.TaskStatus=0),"
				+"t2.Descript,t3.Descript FROM tasksubscription "
				+"LEFT JOIN tasklist ON tasklist.TaskListNum=tasksubscription.TaskListNum "
				+"LEFT JOIN tasklist t2 ON t2.TaskListNum=tasklist.Parent "
				+"LEFT JOIN tasklist t3 ON t3.TaskListNum=t2.Parent "
				//+"LEFT JOIN taskancestor ON taskancestor.TaskList=tasklist.TaskList "
				//+"LEFT JOIN task ON task.TaskNum=taskancestor.TaskNum AND task.TaskStatus=0 "
				+"WHERE tasksubscription.UserNum="+POut.Long(userNum)
				+" AND tasksubscription.TaskListNum!=0 "
				+"GROUP BY tasklist.TaskListNum "
				+"ORDER BY DateTimeEntry";
			return RefreshAndFill(Db.GetTable(command));
		}

		///<summary>Gets all task lists for the main trunk.</summary>
		public static List<TaskList> RefreshMainTrunk() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<TaskList>>(MethodBase.GetCurrentMethod());
			}
			string command="SELECT tasklist.*,"
				+"(SELECT COUNT(*) FROM taskancestor,task WHERE taskancestor.TaskListNum=tasklist.TaskListNum "
				+"AND task.TaskNum=taskancestor.TaskNum AND task.TaskStatus=0) "
				+"FROM tasklist "
				+"WHERE Parent=0 "
				+"AND DateTL < '1880-01-01' "
				+"AND IsRepeating=0 "
				+"ORDER BY DateTimeEntry";
			return RefreshAndFill(Db.GetTable(command));
		}

		///<summary>Gets all task lists for the repeating trunk.</summary>
		public static List<TaskList> RefreshRepeatingTrunk() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<TaskList>>(MethodBase.GetCurrentMethod());
			}
			string command="SELECT tasklist.*,"
				+"(SELECT COUNT(*) FROM taskancestor,task WHERE taskancestor.TaskListNum=tasklist.TaskListNum "
				+"AND task.TaskNum=taskancestor.TaskNum AND task.TaskStatus=0) "
				+"FROM tasklist "
				+"WHERE Parent=0 "
				+"AND DateTL < '1880-01-01' "
				+"AND IsRepeating=1 "
				+"ORDER BY DateTimeEntry";
			return RefreshAndFill(Db.GetTable(command));
		}

		///<summary>0 is not allowed, because that would be a trunk.</summary>
		public static List<TaskList> RefreshChildren(long parent) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<TaskList>>(MethodBase.GetCurrentMethod(),parent);
			}
			string command=
				"SELECT tasklist.*,"
				+"(SELECT COUNT(*) FROM taskancestor,task WHERE taskancestor.TaskListNum=tasklist.TaskListNum "
				+"AND task.TaskNum=taskancestor.TaskNum AND task.TaskStatus=0) "
				+"FROM tasklist "
				+"WHERE Parent="+POut.Long(parent)
				+" ORDER BY DateTimeEntry";
			return RefreshAndFill(Db.GetTable(command));
		}

		///<summary>All repeating items for one date type with no heirarchy.</summary>
		public static List<TaskList> RefreshRepeating(TaskDateType dateType){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<TaskList>>(MethodBase.GetCurrentMethod(),dateType);
			}
			string command=
				"SELECT tasklist.*,"
				+"(SELECT COUNT(*) FROM taskancestor,task WHERE taskancestor.TaskListNum=tasklist.TaskListNum "
				+"AND task.TaskNum=taskancestor.TaskNum AND task.TaskStatus=0) "
				+"FROM tasklist "
				+"WHERE IsRepeating=1 "
				+"AND DateType="+POut.Long((int)dateType)+" "
				+"ORDER BY DateTimeEntry";
			return RefreshAndFill(Db.GetTable(command));
		}

		///<summary>Gets all task lists for one of the 3 dated trunks.</summary>
		public static List<TaskList> RefreshDatedTrunk(DateTime date,TaskDateType dateType){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<TaskList>>(MethodBase.GetCurrentMethod(),date,dateType);
			}
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
				"SELECT tasklist.*,"
				+"(SELECT COUNT(*) FROM taskancestor,task WHERE taskancestor.TaskListNum=tasklist.TaskListNum "
				+"AND task.TaskNum=taskancestor.TaskNum AND task.TaskStatus=0) "
				+"FROM tasklist "
				+"WHERE DateTL >= "+POut.Date(dateFrom)
				+" AND DateTL <= "+POut.Date(dateTo)
				+" AND DateType="+POut.Long((int)dateType)
				+" ORDER BY DateTimeEntry";
			return RefreshAndFill(Db.GetTable(command));
		}

		///<summary></summary>
		public static TaskList GetOne(long taskListNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<TaskList>(MethodBase.GetCurrentMethod(),taskListNum);
			}
			if(taskListNum==0){
				return null;
			}
			string command="SELECT * FROM tasklist WHERE TaskListNum="+POut.Long(taskListNum);
			List<TaskList> list=RefreshAndFill(Db.GetTable(command));
			if(list.Count>0){
				return list[0];
			}
			return null;
		}

		/*
		///<Summary>Gets all task lists in the general tab with no heirarchy.  This allows us to loop through the list to grab useful heirarchy info.  Only used when viewing user tab.  Not guaranteed to get all tasklists, because we exclude those with a DateType.</Summary>
		public static List<TaskList> GetAllGeneral(){
//THIS WON'T WORK BECAUSE THERE ARE TOO MANY REPEATING TASKLISTS.
			string command="SELECT * FROM tasklist WHERE DateType=0 ";
		}*/

		private static List<TaskList> RefreshAndFill(DataTable table){
			//No need to check RemotingRole; no call to db.
			List<TaskList> retVal=new List<TaskList>();
			TaskList tasklist;
			string desc;
			for(int i=0;i<table.Rows.Count;i++) {
				tasklist=new TaskList();
				tasklist.TaskListNum    = PIn.Long(table.Rows[i][0].ToString());
				tasklist.Descript       = PIn.String(table.Rows[i][1].ToString());
				tasklist.Parent         = PIn.Long(table.Rows[i][2].ToString());
				tasklist.DateTL         = PIn.Date(table.Rows[i][3].ToString());
				tasklist.IsRepeating    = PIn.Bool(table.Rows[i][4].ToString());
				tasklist.DateType       = (TaskDateType)PIn.Long(table.Rows[i][5].ToString());
				tasklist.FromNum        = PIn.Long(table.Rows[i][6].ToString());
				tasklist.ObjectType     = (TaskObjectType)PIn.Long(table.Rows[i][7].ToString());
				tasklist.DateTimeEntry  = PIn.DateT(table.Rows[i][8].ToString());
				tasklist.ParentDesc="";
				tasklist.NewTaskCount=0;
				if(table.Columns.Count>9){
					tasklist.NewTaskCount=PIn.Int(table.Rows[i][9].ToString());
				}
				if(table.Columns.Count>10){
					desc=PIn.String(table.Rows[i][10].ToString());
					if(desc!=""){
						tasklist.ParentDesc=desc+"/";
					}
					desc=PIn.String(table.Rows[i][11].ToString());
					if(desc!="") {
						tasklist.ParentDesc=desc+"/"+tasklist.ParentDesc;
					}
				}
				retVal.Add(tasklist);
			}
			return retVal;
		}

		/// <summary>Gets all task lists with the give object type.  Used in TaskListSelect when assigning an object to a task list.</summary>
		public static TaskList[] GetForObjectType(TaskObjectType oType) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<TaskList[]>(MethodBase.GetCurrentMethod(),oType);
			}
			string command=
				"SELECT * FROM tasklist "
				+"WHERE ObjectType="+POut.Long((int)oType)
				+" ORDER BY Descript";
			DataTable table=Db.GetTable(command);
			TaskList[] List=new TaskList[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				List[i]=new TaskList();
				List[i].TaskListNum    = PIn.Long(table.Rows[i][0].ToString());
				List[i].Descript       = PIn.String(table.Rows[i][1].ToString());
				List[i].Parent         = PIn.Long(table.Rows[i][2].ToString());
				List[i].DateTL         = PIn.Date(table.Rows[i][3].ToString());
				List[i].IsRepeating    = PIn.Bool(table.Rows[i][4].ToString());
				List[i].DateType       = (TaskDateType)PIn.Long(table.Rows[i][5].ToString());
				List[i].FromNum        = PIn.Long(table.Rows[i][6].ToString());
				List[i].ObjectType     = (TaskObjectType)PIn.Long(table.Rows[i][7].ToString());
				List[i].DateTimeEntry  = PIn.DateT(table.Rows[i][8].ToString());
			}
			return List;
		}

		///<summary></summary>
		private static void Update(TaskList tlist){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),tlist);
				return;
			}
			string command= "UPDATE tasklist SET " 
				+"Descript = '"       +POut.String(tlist.Descript)+"'"
				+",Parent = '"        +POut.Long   (tlist.Parent)+"'"
				+",DateTL = "        +POut.Date  (tlist.DateTL)
				+",IsRepeating = '"   +POut.Bool  (tlist.IsRepeating)+"'"
				+",DateType = '"      +POut.Long   ((int)tlist.DateType)+"'"
				+",FromNum = '"       +POut.Long   (tlist.FromNum)+"'"
				+",ObjectType = '"    +POut.Long   ((int)tlist.ObjectType)+"'"
				+",DateTimeEntry = " +POut.DateT (tlist.DateTimeEntry)
				+" WHERE TaskListNum = '" +POut.Long (tlist.TaskListNum)+"'";
 			Db.NonQ(command);
		}

		///<summary></summary>
		private static long Insert(TaskList tlist) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				tlist.TaskListNum=Meth.GetLong(MethodBase.GetCurrentMethod(),tlist);
				return tlist.TaskListNum;
			}
			if(PrefC.RandomKeys){
				tlist.TaskListNum=ReplicationServers.GetKey("tasklist","TaskListNum");
			}
			string command= "INSERT INTO tasklist (";
			if(PrefC.RandomKeys){
				command+="TaskListNum,";
			}
			command+="Descript,Parent,DateTL,IsRepeating,DateType,"
				+"FromNum,ObjectType,DateTimeEntry) VALUES(";
			if(PrefC.RandomKeys){
				command+="'"+POut.Long(tlist.TaskListNum)+"', ";
			}
			command+=
				 "'"+POut.String(tlist.Descript)+"', "
				+"'"+POut.Long   (tlist.Parent)+"', "
				+POut.Date  (tlist.DateTL)+", "
				+"'"+POut.Bool  (tlist.IsRepeating)+"', "
				+"'"+POut.Long   ((int)tlist.DateType)+"', "
				+"'"+POut.Long   (tlist.FromNum)+"', "
				+"'"+POut.Long   ((int)tlist.ObjectType)+"', "
				+"NOW() )";//DateTimeEntry set to current server time
 			if(PrefC.RandomKeys){
				Db.NonQ(command);
			}
			else{
 				tlist.TaskListNum=Db.NonQ(command,true);
			}
			return tlist.TaskListNum;
		}

		///<summary></summary>
		public static void InsertOrUpdate(TaskList tlist, bool isNew){
			//No need to check RemotingRole; no call to db.
			//check for duplicate trunk?
			if(tlist.IsRepeating && tlist.DateTL.Year>1880){
				throw new Exception(Lans.g("TaskLists","TaskList cannot be tagged repeating and also have a date."));
			}
			if(tlist.Parent==0 && tlist.DateTL.Year>1880 && tlist.DateType==TaskDateType.None){//it would not show anywhere, so it would be 'lost'
				throw new Exception(Lans.g("TaskLists","A TaskList with a date must also have a type selected."));
			}
			if(tlist.IsRepeating && tlist.Parent!=0 && tlist.DateType!=TaskDateType.None){//In repeating, children not allowed to repeat.
				throw new Exception(Lans.g("TaskLists","In repeating tasklists, only the main parents can have a task status."));
			}
			if(isNew){
				Insert(tlist);
			}
			else{
				Update(tlist);
			}
		}

		///<summary>Throws exception if any child tasklists or tasks.</summary>
		public static void Delete(TaskList tlist){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),tlist);
				return;
			}
			string command="SELECT COUNT(*) FROM tasklist WHERE Parent="+POut.Long(tlist.TaskListNum);
			DataTable table=Db.GetTable(command);
			if(table.Rows[0][0].ToString()!="0"){
				throw new Exception(Lans.g("TaskLists","Not allowed to delete task list because it still has child lists attached."));
			}
			command="SELECT COUNT(*) FROM task WHERE TaskListNum="+POut.Long(tlist.TaskListNum);
			table=Db.GetTable(command);
			if(table.Rows[0][0].ToString()!="0"){
				throw new Exception(Lans.g("TaskLists","Not allowed to delete task list because it still has child tasks attached."));
			}
			command="SELECT COUNT(*) FROM userod WHERE TaskListInBox="+POut.Long(tlist.TaskListNum);
			table=Db.GetTable(command);
			if(table.Rows[0][0].ToString()!="0"){
				throw new Exception(Lans.g("TaskLists","Not allowed to delete task list because it is attached to a user inbox."));
			}
			command= "DELETE from tasklist WHERE TaskListNum = '"
				+POut.Long(tlist.TaskListNum)+"'";
 			Db.NonQ(command);
		}

		
	
	

		

	
	}

	

	


}













