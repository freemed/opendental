using System;
using System.Collections;

namespace OpenDentBusiness{

	///<summary>A tasklist is like a folder system, where it can have child tasklists as well as tasks.</summary>
	public class TaskList{
		///<summary>Primary key.</summary>
		public int TaskListNum;
		///<summary>The description of this tasklist.  Might be very long, but not usually.</summary>
		public string Descript;
		///<summary>FK tasklist.TaskListNum  The parent task list to which this task list is assigned.  If zero, then this task list is on the main trunk of one of the sections.</summary>
		public int Parent;
		///<summary>Optional. Set to 0001-01-01 for no date.  If a date is assigned, then this list will also be available from the date section.</summary>
		public DateTime DateTL;
		///<summary>True if it is to show in the repeating section.  There should be no date.  All children should also be set to IsRepeating=true.</summary>
		public bool IsRepeating;
		///<summary>Enum:TaskDateType  None, Day, Week, Month.  If IsRepeating, then setting to None effectively disables the repeating feature.</summary>
		public TaskDateType DateType;
		///<summary>FK to tasklist.TaskListNum  If this is derived from a repeating list, then this will hold the TaskListNum of that list.  It helps automate the adding and deleting of lists.  It might be deleted automatically if no tasks are marked complete.</summary>
		public int FromNum;
		///<summary>Enum:TaskObjectType  0=none, 1=Patient, 2=Appointment.  More will be added later. If a type is selected, then this list will be visible in the appropriate places for attaching the correct type of object.  The type is not copied to a task when created.  Tasks in this list do not have to be of the same type.  You can only attach an object to a task, not a tasklist.</summary>
		public TaskObjectType ObjectType;
		///<summary>The date and time that this list was added.  Used to sort the list by the order entered.</summary>
		public DateTime DateTimeEntry;

		///<summary></summary>
		public TaskList Copy(){
			TaskList t=new TaskList();
			t.TaskListNum=TaskListNum;
			t.Descript=Descript;
			t.Parent=Parent;
			t.DateTL=DateTL;
			t.IsRepeating=IsRepeating;
			t.DateType=DateType;
			t.FromNum=FromNum;
			t.ObjectType=ObjectType;
			t.DateTimeEntry=DateTimeEntry;
			return t;
		}


	
	}

	

	


}













