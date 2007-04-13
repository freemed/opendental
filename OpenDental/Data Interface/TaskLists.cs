using System;
using System.Collections;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	///<summary></summary>
	public class TaskLists {

		///<summary>Gets all tasklists for a given parent.  But the 5 trunks don't have parents: For main trunk use date.Min and Parent=0.  For Repeating trunk use date.Min isRepeating and Parent=0.  For the 3 dated trunks, use a date and a dateType.  Date and parent are mutually exclusive.  Also used to get all repeating lists for one dateType without any heirarchy: supply parent=-1.</summary>
		public static TaskList[] Refresh(int parent,DateTime date,TaskDateType dateType,bool isRepeating) {
			DateTime dateFrom=DateTime.MinValue;
			DateTime dateTo=DateTime.MaxValue;
			string where="";
			if(date.Year>1880) {
				//date supplied always indicates one of 3 dated trunks.
				//the value of parent is completely ignored
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
				where="DateTL >= "+POut.PDate(dateFrom)
					+" AND DateTL <= "+POut.PDate(dateTo)+" "
					+"AND DateType="+POut.PInt((int)dateType)+" ";
			}
			else {//no date supplied.
				if(parent==0) {//main trunk or repeating trunk
					where="Parent="+POut.PInt(parent)
						+" AND DateTL < '1880-01-01'"
						+" AND IsRepeating="+POut.PBool(isRepeating)+" ";
				}
				else if(parent==-1 && isRepeating) {//all repeating items with no heirarchy
					where="IsRepeating=1 "
						+"AND DateType="+POut.PInt((int)dateType)+" ";
				}
				else {//any child
					where="Parent="+POut.PInt(parent)+" ";
					//+" AND IsRepeating="+POut.PBool(isRepeating)+" ";
				}
			}
			string command=
				"SELECT * FROM tasklist "
				+"WHERE "+where
				+"ORDER BY DateTimeEntry";
			DataTable table=General.GetTable(command);
			TaskList[] List=new TaskList[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				List[i]=new TaskList();
				List[i].TaskListNum    = PIn.PInt(table.Rows[i][0].ToString());
				List[i].Descript       = PIn.PString(table.Rows[i][1].ToString());
				List[i].Parent         = PIn.PInt(table.Rows[i][2].ToString());
				List[i].DateTL         = PIn.PDate(table.Rows[i][3].ToString());
				List[i].IsRepeating    = PIn.PBool(table.Rows[i][4].ToString());
				List[i].DateType       = (TaskDateType)PIn.PInt(table.Rows[i][5].ToString());
				List[i].FromNum        = PIn.PInt(table.Rows[i][6].ToString());
				List[i].ObjectType     = (TaskObjectType)PIn.PInt(table.Rows[i][7].ToString());
				List[i].DateTimeEntry  = PIn.PDateT(table.Rows[i][8].ToString());
			}
			return List;
		}

		/// <summary>Gets all task lists with the give object type.  Used in TaskListSelect when assigning an object to a task list.</summary>
		public static TaskList[] GetForObjectType(TaskObjectType oType) {
			string command=
				"SELECT * FROM tasklist "
				+"WHERE ObjectType="+POut.PInt((int)oType)
				+" ORDER BY Descript";
			DataTable table=General.GetTable(command);
			TaskList[] List=new TaskList[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				List[i]=new TaskList();
				List[i].TaskListNum    = PIn.PInt(table.Rows[i][0].ToString());
				List[i].Descript       = PIn.PString(table.Rows[i][1].ToString());
				List[i].Parent         = PIn.PInt(table.Rows[i][2].ToString());
				List[i].DateTL         = PIn.PDate(table.Rows[i][3].ToString());
				List[i].IsRepeating    = PIn.PBool(table.Rows[i][4].ToString());
				List[i].DateType       = (TaskDateType)PIn.PInt(table.Rows[i][5].ToString());
				List[i].FromNum        = PIn.PInt(table.Rows[i][6].ToString());
				List[i].ObjectType     = (TaskObjectType)PIn.PInt(table.Rows[i][7].ToString());
				List[i].DateTimeEntry  = PIn.PDateT(table.Rows[i][8].ToString());
			}
			return List;
		}

		///<summary></summary>
		private static void Update(TaskList tlist){
			string command= "UPDATE tasklist SET " 
				+"Descript = '"       +POut.PString(tlist.Descript)+"'"
				+",Parent = '"        +POut.PInt   (tlist.Parent)+"'"
				+",DateTL = "        +POut.PDate  (tlist.DateTL)
				+",IsRepeating = '"   +POut.PBool  (tlist.IsRepeating)+"'"
				+",DateType = '"      +POut.PInt   ((int)tlist.DateType)+"'"
				+",FromNum = '"       +POut.PInt   (tlist.FromNum)+"'"
				+",ObjectType = '"    +POut.PInt   ((int)tlist.ObjectType)+"'"
				+",DateTimeEntry = " +POut.PDateT (tlist.DateTimeEntry)
				+" WHERE TaskListNum = '" +POut.PInt (tlist.TaskListNum)+"'";
 			General.NonQ(command);
		}

		///<summary></summary>
		private static void Insert(TaskList tlist){
			if(PrefB.RandomKeys){
				tlist.TaskListNum=MiscData.GetKey("tasklist","TaskListNum");
			}
			string command= "INSERT INTO tasklist (";
			if(PrefB.RandomKeys){
				command+="TaskListNum,";
			}
			command+="Descript,Parent,DateTL,IsRepeating,DateType,"
				+"FromNum,ObjectType,DateTimeEntry) VALUES(";
			if(PrefB.RandomKeys){
				command+="'"+POut.PInt(tlist.TaskListNum)+"', ";
			}
			command+=
				 "'"+POut.PString(tlist.Descript)+"', "
				+"'"+POut.PInt   (tlist.Parent)+"', "
				+POut.PDate  (tlist.DateTL)+", "
				+"'"+POut.PBool  (tlist.IsRepeating)+"', "
				+"'"+POut.PInt   ((int)tlist.DateType)+"', "
				+"'"+POut.PInt   (tlist.FromNum)+"', "
				+"'"+POut.PInt   ((int)tlist.ObjectType)+"', ";
			if(FormChooseDatabase.DBtype==DatabaseType.Oracle) {
				command+=POut.PDateT(MiscData.GetNowDateTime());
			}
			else {//Assume MySQL
				command+="NOW()";
			}
			command+=")";//DateTimeEntry set to current server time
 			if(PrefB.RandomKeys){
				General.NonQ(command);
			}
			else{
 				tlist.TaskListNum=General.NonQ(command,true);
			}
		}

		///<summary></summary>
		public static void InsertOrUpdate(TaskList tlist, bool isNew){
			//check for duplicate trunk?
			if(tlist.IsRepeating && tlist.DateTL.Year>1880){
				throw new Exception(Lan.g("TaskLists","TaskList cannot be tagged repeating and also have a date."));
			}
			if(tlist.Parent==0 && tlist.DateTL.Year>1880 && tlist.DateType==TaskDateType.None){//it would not show anywhere, so it would be 'lost'
				throw new Exception(Lan.g("TaskLists","A TaskList with a date must also have a type selected."));
			}
			if(tlist.IsRepeating && tlist.Parent!=0 && tlist.DateType!=TaskDateType.None){//In repeating, children not allowed to repeat.
				throw new Exception(Lan.g("TaskLists","In repeating tasklists, only the main parents can have a task status."));
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
			string command="SELECT COUNT(*) FROM tasklist WHERE Parent="+POut.PInt(tlist.TaskListNum);
			DataTable table=General.GetTable(command);
			if(table.Rows[0][0].ToString()!="0"){
				throw new Exception(Lan.g("TaskLists","Not allowed to delete task list because it still has child lists attached."));
			}
			command="SELECT COUNT(*) FROM task WHERE TaskListNum="+POut.PInt(tlist.TaskListNum);
			table=General.GetTable(command);
			if(table.Rows[0][0].ToString()!="0"){
				throw new Exception(Lan.g("TaskLists","Not allowed to delete task list because it still has child tasks attached."));
			}
			command= "DELETE from tasklist WHERE TaskListNum = '"
				+POut.PInt(tlist.TaskListNum)+"'";
 			General.NonQ(command);
		}


	
	

		

	
	}

	

	


}













