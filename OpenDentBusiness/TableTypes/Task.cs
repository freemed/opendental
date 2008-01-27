using System;
using System.Collections;
using System.Data;

namespace OpenDentBusiness{

	///<summary>A task is a single todo item.</summary>
	public class Task {
		///<summary>Primary key.</summary>
		public int TaskNum;
		///<summary>FK to tasklist.TaskListNum.  If 0, then it will show in the trunk of a section.  </summary>
		public int TaskListNum;
		///<summary>Only used if this task is assigned to a dated category.  Children are NOT dated.  Only dated if they should show in the trunk for a date category.  They can also have a parent if they are in the main list as well.</summary>
		public DateTime DateTask;
		///<summary>FK to patient.PatNum or appointment.AptNum. Only used when ObjectType is not 0.</summary>
		public int KeyNum;
		///<summary>The description of this task.  Might be very long.</summary>
		public string Descript;
		///<summary>True if the task has been completed. This could later be turned into an enumeration if more statuses are needed.</summary>
		public bool TaskStatus;
		///<summary>True if it is to show in the repeating section.  There should be no date.  All children and parents should also be set to IsRepeating=true.</summary>
		public bool IsRepeating;
		///<summary>Enum:TaskDateType  None, Day, Week, Month.  If IsRepeating, then setting to None effectively disables the repeating feature.</summary>
		public TaskDateType DateType;
		///<summary>FK to task.TaskNum  If this is derived from a repeating task, then this will hold the TaskNum of that task.  It helps automate the adding and deleting of tasks.  It might be deleted automatically if not are marked complete.</summary>
		public int FromNum;
		///<summary>Enum:TaskObjectType  0=none,1=Patient,2=Appointment.  More will be added later. If a type is selected, then the KeyNum will contain the primary key of the corresponding Patient or Appointment.  Does not really have anything to do with the ObjectType of the parent tasklist, although they tend to match.</summary>
		public TaskObjectType ObjectType;
		///<summary>The date and time that this task was added.  Used to sort the list by the order entered.</summary>
		public DateTime DateTimeEntry;

		///<summary></summary>
		public Task Copy() {
			return (Task)MemberwiseClone();
		}
		///<summary>Gets one Task item from database.</summary>
		public static Task GetOne(int TaskNum) {
			string command=
				"SELECT * FROM task"
				+" WHERE TaskNum = "+POut.PInt(TaskNum);
			Task[] taskList=RefreshAndFill(command);
			if(taskList.Length==0) {
				return null;
			}
			return taskList[0];
		}
		private static Task[] RefreshAndFill(string command) {
			DataTable table=General.GetTable(command);
			Task[] List=new Task[table.Rows.Count];
			for(int i=0;i<List.Length;i++) {
				List[i]=new Task();
				List[i].TaskNum     = PIn.PInt(table.Rows[i][0].ToString());
				List[i].TaskListNum         = PIn.PInt(table.Rows[i][1].ToString());
				List[i].DateTask   = PIn.PDate(table.Rows[i][2].ToString());
				List[i].KeyNum       = PIn.PInt(table.Rows[i][3].ToString());
				List[i].Descript           = PIn.PString(table.Rows[i][4].ToString());
				List[i].TaskStatus          = PIn.PBool(table.Rows[i][5].ToString());
				List[i].IsRepeating = PIn.PBool(table.Rows[i][6].ToString());
				List[i].DateType= (TaskDateType)PIn.PInt(table.Rows[i][7].ToString());
				List[i].FromNum        = PIn.PInt(table.Rows[i][8].ToString());
				List[i].ObjectType        = (TaskObjectType)PIn.PInt(table.Rows[i][9].ToString());
				List[i].DateTimeEntry        = PIn.PDate(table.Rows[i][10].ToString());
			}
			return List;

		}


	}
	


}




















