using System;
using System.Collections;
using System.Data;

namespace OpenDentBusiness{

	///<summary>A task is a single todo item.</summary>
	public class Task {
		///<summary>Primary key.</summary>
		public long TaskNum;
		///<summary>FK to tasklist.TaskListNum.  If 0, then it will show in the trunk of a section.  </summary>
		public long TaskListNum;
		///<summary>Only used if this task is assigned to a dated category.  Children are NOT dated.  Only dated if they should show in the trunk for a date category.  They can also have a parent if they are in the main list as well.</summary>
		public DateTime DateTask;
		///<summary>FK to patient.PatNum or appointment.AptNum. Only used when ObjectType is not 0.</summary>
		public long KeyNum;
		///<summary>The description of this task.  Might be very long.</summary>
		public string Descript;
		///<summary>Enum:TaskStatusEnum New,Viewed,Done.</summary>
		public TaskStatusEnum TaskStatus;
		///<summary>True if it is to show in the repeating section.  There should be no date.  All children and parents should also be set to IsRepeating=true.</summary>
		public bool IsRepeating;
		///<summary>Enum:TaskDateType  None, Day, Week, Month.  If IsRepeating, then setting to None effectively disables the repeating feature.</summary>
		public TaskDateType DateType;
		///<summary>FK to task.TaskNum  If this is derived from a repeating task, then this will hold the TaskNum of that task.  It helps automate the adding and deleting of tasks.  It might be deleted automatically if not are marked complete.</summary>
		public long FromNum;
		///<summary>Enum:TaskObjectType  0=none,1=Patient,2=Appointment.  More will be added later. If a type is selected, then the KeyNum will contain the primary key of the corresponding Patient or Appointment.  Does not really have anything to do with the ObjectType of the parent tasklist, although they tend to match.</summary>
		public TaskObjectType ObjectType;
		///<summary>The date and time that this task was added.  Used to sort the list by the order entered.</summary>
		public DateTime DateTimeEntry;
		///<summary>FK to user.UserNum.  The person who created the task or who made the most recent edit to the task.</summary>
		public long UserNum;
		///<summary>The date and time that this task was marked "done".</summary>
		public DateTime DateTimeFinished;

		///<summary></summary>
		public Task Copy() {
			return (Task)MemberwiseClone();
		}

		public override bool Equals(object obj) {
			if(TaskNum==((Task)obj).TaskNum
				&& TaskListNum==((Task)obj).TaskListNum
				&& DateTask==((Task)obj).DateTask
				&& KeyNum==((Task)obj).KeyNum
				&& Descript==((Task)obj).Descript
				&& TaskStatus==((Task)obj).TaskStatus
				&& IsRepeating==((Task)obj).IsRepeating
				&& DateType==((Task)obj).DateType
				&& FromNum==((Task)obj).FromNum
				&& ObjectType==((Task)obj).ObjectType
				&& DateTimeEntry==((Task)obj).DateTimeEntry
				&& UserNum==((Task)obj).UserNum
				&& DateTimeFinished==((Task)obj).DateTimeFinished)
			{
				return true;
			}
			return false;
			//return base.Equals(obj);
		}

		public override int GetHashCode() {
			return base.GetHashCode();
		}
		

	}
	


}




















