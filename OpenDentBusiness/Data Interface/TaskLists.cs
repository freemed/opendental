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
				+"AND task.TaskNum=taskancestor.TaskNum ";
			if(PrefC.GetBool(PrefName.TasksNewTrackedByUser)) {
				command+="AND EXISTS(SELECT * FROM taskunread WHERE taskunread.TaskNum=task.TaskNum "
					+"AND taskunread.UserNum="+POut.Long(userNum)+") "
					+"AND task.TaskStatus !=2 ";//not done
			}
			else {
				command+="AND task.TaskStatus=0 ";
			}
			command+="),"
				+"t2.Descript,t3.Descript FROM tasksubscription "
				+"LEFT JOIN tasklist ON tasklist.TaskListNum=tasksubscription.TaskListNum "
				+"LEFT JOIN tasklist t2 ON t2.TaskListNum=tasklist.Parent "
				+"LEFT JOIN tasklist t3 ON t3.TaskListNum=t2.Parent "
				//+"LEFT JOIN taskancestor ON taskancestor.TaskList=tasklist.TaskList "
				//+"LEFT JOIN task ON task.TaskNum=taskancestor.TaskNum AND task.TaskStatus=0 "
				+"WHERE tasksubscription.UserNum="+POut.Long(userNum)
				+" AND tasksubscription.TaskListNum!=0 "
				+"ORDER BY DateTimeEntry";
			return TableToList(Db.GetTable(command));
		}

		///<summary>Gets all task lists for the main trunk.  Pass in the current user.</summary>
		public static List<TaskList> RefreshMainTrunk(long userNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<TaskList>>(MethodBase.GetCurrentMethod());
			}
			string command="SELECT tasklist.*,"
				+"(SELECT COUNT(*) FROM taskancestor,task WHERE taskancestor.TaskListNum=tasklist.TaskListNum "
				+"AND task.TaskNum=taskancestor.TaskNum ";
			if(PrefC.GetBool(PrefName.TasksNewTrackedByUser)) {
				command+="AND EXISTS(SELECT * FROM taskunread WHERE taskunread.TaskNum=task.TaskNum ";
				//if a list is someone's inbox, 
				command+="AND (CASE WHEN EXISTS(SELECT * FROM userod WHERE userod.TaskListInBox=tasklist.TaskListNum) ";
				//then restrict by that user
				command+="THEN (taskunread.UserNum=(SELECT UserNum FROM userod WHERE userod.TaskListInBox=tasklist.TaskListNum)) ";
				//otherwise, restrict by current user
				command+="ELSE taskunread.UserNum="+POut.Long(userNum)+" END)) "
					+"AND task.TaskStatus !=2 ";//not done
			}
			else {
				command+="AND task.TaskStatus=0";
			}
			command+=") "
				+"FROM tasklist "
				+"WHERE Parent=0 "
				+"AND DateTL < '1880-01-01' "
				+"AND IsRepeating=0 "
				+"ORDER BY Descript";//DateTimeEntry";
			return TableToList(Db.GetTable(command));
		}

		///<summary>Gets all task lists for the repeating trunk.</summary>
		public static List<TaskList> RefreshRepeatingTrunk() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<TaskList>>(MethodBase.GetCurrentMethod());
			}
			string command="SELECT tasklist.*,"
				+"(SELECT COUNT(*) FROM taskancestor,task WHERE taskancestor.TaskListNum=tasklist.TaskListNum "
				+"AND task.TaskNum=taskancestor.TaskNum AND task.TaskStatus=0) "
				//I don't think the repeating trunk would ever track by user, so no special treatment here.
				//Acutual behavior in both cases needs to be tested.
				+"FROM tasklist "
				+"WHERE Parent=0 "
				+"AND DateTL < '1880-01-01' "
				+"AND IsRepeating=1 "
				+"ORDER BY DateTimeEntry";
			return TableToList(Db.GetTable(command));
		}

		///<summary>0 is not allowed, because that would be a trunk.  Pass in the current user.  Also, if this is in someone's inbox, then pass in the userNum whose inbox it is in.  If not in an inbox, pass in 0.</summary>
		public static List<TaskList> RefreshChildren(long parent,long userNum,long userNumInbox) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<TaskList>>(MethodBase.GetCurrentMethod(),parent,userNum,userNumInbox);
			}
			string command=
				"SELECT tasklist.*,"
				+"(SELECT COUNT(*) FROM taskancestor,task WHERE taskancestor.TaskListNum=tasklist.TaskListNum "
				+"AND task.TaskNum=taskancestor.TaskNum ";
			if(PrefC.GetBool(PrefName.TasksNewTrackedByUser)) {
				command+="AND EXISTS(SELECT * FROM taskunread WHERE taskunread.TaskNum=task.TaskNum ";
				//if a list is someone's inbox,
				if(userNumInbox>0) {
					//then restrict by that user
					command+="AND taskunread.UserNum="+POut.Long(userNumInbox)+") ";
				}
				else{
					//otherwise, restrict by current user
					command+="AND taskunread.UserNum="+POut.Long(userNum)+") ";
				}
			}
			else {
				command+="AND task.TaskStatus=0";
			}
			command+=") "
				+"FROM tasklist "
				+"WHERE Parent="+POut.Long(parent)
				+" ORDER BY DateTimeEntry";
			return TableToList(Db.GetTable(command));
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
				//See the note in RefreshRepeatingTrunk.  Behavior needs to be tested.
				+"FROM tasklist "
				+"WHERE IsRepeating=1 "
				+"AND DateType="+POut.Long((int)dateType)+" "
				+"ORDER BY DateTimeEntry";
			return TableToList(Db.GetTable(command));
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
				+"AND task.TaskNum=taskancestor.TaskNum ";
			//if(PrefC.GetBool(PrefName.TasksNewTrackedByUser)) {
			//	command+="AND EXISTS(SELECT * FROM taskunread WHERE taskunread.TaskNum=task.TaskNum)";
			//}
			//else {
				command+="AND task.TaskStatus=0";
			//}
			command+=") "
				+"FROM tasklist "
				+"WHERE DateTL >= "+POut.Date(dateFrom)
				+" AND DateTL <= "+POut.Date(dateTo)
				+" AND DateType="+POut.Long((int)dateType)
				+" ORDER BY DateTimeEntry";
			return TableToList(Db.GetTable(command));
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
			return Crud.TaskListCrud.SelectOne(command);
		}

		/*
		///<Summary>Gets all task lists in the general tab with no heirarchy.  This allows us to loop through the list to grab useful heirarchy info.  Only used when viewing user tab.  Not guaranteed to get all tasklists, because we exclude those with a DateType.</Summary>
		public static List<TaskList> GetAllGeneral(){
//THIS WON'T WORK BECAUSE THERE ARE TOO MANY REPEATING TASKLISTS.
			string command="SELECT * FROM tasklist WHERE DateType=0 ";
		}*/

		private static List<TaskList> TableToList(DataTable table){
			//No need to check RemotingRole; no call to db.
			List<TaskList> retVal=Crud.TaskListCrud.TableToList(table);
			TaskList tasklist;
			string desc;
			for(int i=0;i<retVal.Count;i++) {
				if(table.Columns.Count>9){
					retVal[i].NewTaskCount=PIn.Int(table.Rows[i][9].ToString());
				}
				if(table.Columns.Count>10){
					desc=PIn.String(table.Rows[i][10].ToString());
					if(desc!=""){
						retVal[i].ParentDesc=desc+"/";
					}
					desc=PIn.String(table.Rows[i][11].ToString());
					if(desc!="") {
						retVal[i].ParentDesc=desc+"/"+retVal[i].ParentDesc;
					}
				}
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
			return Crud.TaskListCrud.SelectMany(command).ToArray();
		}

		///<summary></summary>
		public static void Update(TaskList tlist){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),tlist);
				return;
			}
			if(tlist.IsRepeating && tlist.DateTL.Year>1880) {
				throw new Exception(Lans.g("TaskLists","TaskList cannot be tagged repeating and also have a date."));
			}
			if(tlist.Parent==0 && tlist.DateTL.Year>1880 && tlist.DateType==TaskDateType.None) {//it would not show anywhere, so it would be 'lost'
				throw new Exception(Lans.g("TaskLists","A TaskList with a date must also have a type selected."));
			}
			if(tlist.IsRepeating && tlist.Parent!=0 && tlist.DateType!=TaskDateType.None) {//In repeating, children not allowed to repeat.
				throw new Exception(Lans.g("TaskLists","In repeating tasklists, only the main parents can have a task status."));
			}
			Crud.TaskListCrud.Update(tlist);
		}

		///<summary></summary>
		public static long Insert(TaskList tlist) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				tlist.TaskListNum=Meth.GetLong(MethodBase.GetCurrentMethod(),tlist);
				return tlist.TaskListNum;
			}
			if(tlist.IsRepeating && tlist.DateTL.Year>1880) {
				throw new Exception(Lans.g("TaskLists","TaskList cannot be tagged repeating and also have a date."));
			}
			if(tlist.Parent==0 && tlist.DateTL.Year>1880 && tlist.DateType==TaskDateType.None) {//it would not show anywhere, so it would be 'lost'
				throw new Exception(Lans.g("TaskLists","A TaskList with a date must also have a type selected."));
			}
			if(tlist.IsRepeating && tlist.Parent!=0 && tlist.DateType!=TaskDateType.None) {//In repeating, children not allowed to repeat.
				throw new Exception(Lans.g("TaskLists","In repeating tasklists, only the main parents can have a task status."));
			}
			return Crud.TaskListCrud.Insert(tlist);
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

		///<summary>Will return 0 if not anyone's inbox.</summary>
		public static long GetMailboxUserNum(long taskListNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetLong(MethodBase.GetCurrentMethod(),taskListNum);
			}
			string command="SELECT UserNum FROM userod WHERE TaskListInBox="+POut.Long(taskListNum);
			return PIn.Long(Db.GetScalar(command));
		}

		///<summary>Checks all ancestors of a task.  Will return 0 if no ancestor is anyone's inbox.</summary>
		public static long GetMailboxUserNumByAncestor(long taskNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetLong(MethodBase.GetCurrentMethod(),taskNum);
			}
			string command="SELECT UserNum FROM taskancestor,userod "
				+"WHERE taskancestor.TaskListNum=userod.TaskListInBox "
				+"AND taskancestor.TaskNum="+POut.Long(taskNum);
			return PIn.Long(Db.GetScalar(command));
		}

		

	
	}

	

	


}













