using System;
using System.Collections;

namespace OpenDentBusiness{

	///<summary>A tasklist is like a folder system, where it can have child tasklists as well as tasks.</summary>
	[Serializable]
	public class TaskList:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long TaskListNum;
		///<summary>The description of this tasklist.  Might be very long, but not usually.</summary>
		public string Descript;
		///<summary>FK to tasklist.TaskListNum  The parent task list to which this task list is assigned.  If zero, then this task list is on the main trunk of one of the sections.</summary>
		public long Parent;
		///<summary>Optional. Set to 0001-01-01 for no date.  If a date is assigned, then this list will also be available from the date section.</summary>
		public DateTime DateTL;
		///<summary>True if it is to show in the repeating section.  There should be no date.  All children should also be set to IsRepeating=true.</summary>
		public bool IsRepeating;
		///<summary>Enum:TaskDateType  None, Day, Week, Month.  If IsRepeating, then setting to None effectively disables the repeating feature.</summary>
		public TaskDateType DateType;
		///<summary>FK to tasklist.TaskListNum  If this is derived from a repeating list, then this will hold the TaskListNum of that list.  It helps automate the adding and deleting of lists.  It might be deleted automatically if no tasks are marked complete.</summary>
		public long FromNum;
		///<summary>Enum:TaskObjectType  0=none, 1=Patient, 2=Appointment.  More will be added later. If a type is selected, then this list will be visible in the appropriate places for attaching the correct type of object.  The type is not copied to a task when created.  Tasks in this list do not have to be of the same type.  You can only attach an object to a task, not a tasklist.</summary>
		public TaskObjectType ObjectType;
		///<summary>The date and time that this list was added.  Used to sort the list by the order entered.</summary>
		[CrudColumn(SpecialType=CrudSpecialColType.DateTEntryEditable)]
		public DateTime DateTimeEntry;
		///<Summary>Not a database column.  A string description of the parents of this list.  Might look like this: MegaParent/Parent/  This string may then be tacked on before the Descript to indicate the heirarchy.  It will extend a max of 3 levels.  Only useful in the User tab.</Summary>
		[CrudColumn(IsNotDbColumn=true)]
		public string ParentDesc;
		///<summary>Not a database column.  The number of new tasks found within a tasklist.  Used in the user tab to turn the tasklist orange, indicating that tasks are present.</summary>
		[CrudColumn(IsNotDbColumn=true)]
		public int NewTaskCount;

		///<summary></summary>
		public TaskList Copy(){
			return (TaskList)MemberwiseClone();
		}


	
	}

	

	


}













