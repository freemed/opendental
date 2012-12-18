package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

public class Task {
		/** Primary key. */
		public int TaskNum;
		/** FK to tasklist.TaskListNum.  If 0, then it will show in the trunk of a section.   */
		public int TaskListNum;
		/** Only used if this task is assigned to a dated category.  Children are NOT dated.  Only dated if they should show in the trunk for a date category.  They can also have a parent if they are in the main list as well. */
		public Date DateTask;
		/** FK to patient.PatNum or appointment.AptNum. Only used when ObjectType is not 0. */
		public int KeyNum;
		/** The description of this task.  Might be very long. */
		public String Descript;
		/** Enum:TaskStatusEnum New,Viewed,Done. */
		public TaskStatusEnum TaskStatus;
		/** True if it is to show in the repeating section.  There should be no date.  All children and parents should also be set to IsRepeating=true. */
		public boolean IsRepeating;
		/** Enum:TaskDateType  None, Day, Week, Month.  If IsRepeating, then setting to None effectively disables the repeating feature. */
		public TaskDateType DateType;
		/** FK to task.TaskNum  If this is derived from a repeating task, then this will hold the TaskNum of that task.  It helps automate the adding and deleting of tasks.  It might be deleted automatically if not are marked complete. */
		public int FromNum;
		/** Enum:TaskObjectType  0=none,1=Patient,2=Appointment.  More will be added later. If a type is selected, then the KeyNum will contain the primary key of the corresponding Patient or Appointment.  Does not really have anything to do with the ObjectType of the parent tasklist, although they tend to match. */
		public TaskObjectType ObjectType;
		/** The date and time that this task was added.  Used to sort the list by the order entered. */
		public Date DateTimeEntry;
		/** FK to user.UserNum.  The person who created the task. */
		public int UserNum;
		/** The date and time that this task was marked "done". */
		public Date DateTimeFinished;

		/** Deep copy of object. */
		public Task deepCopy() {
			Task task=new Task();
			task.TaskNum=this.TaskNum;
			task.TaskListNum=this.TaskListNum;
			task.DateTask=this.DateTask;
			task.KeyNum=this.KeyNum;
			task.Descript=this.Descript;
			task.TaskStatus=this.TaskStatus;
			task.IsRepeating=this.IsRepeating;
			task.DateType=this.DateType;
			task.FromNum=this.FromNum;
			task.ObjectType=this.ObjectType;
			task.DateTimeEntry=this.DateTimeEntry;
			task.UserNum=this.UserNum;
			task.DateTimeFinished=this.DateTimeFinished;
			return task;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<Task>");
			sb.append("<TaskNum>").append(TaskNum).append("</TaskNum>");
			sb.append("<TaskListNum>").append(TaskListNum).append("</TaskListNum>");
			sb.append("<DateTask>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateTask)).append("</DateTask>");
			sb.append("<KeyNum>").append(KeyNum).append("</KeyNum>");
			sb.append("<Descript>").append(Serializing.escapeForXml(Descript)).append("</Descript>");
			sb.append("<TaskStatus>").append(TaskStatus.ordinal()).append("</TaskStatus>");
			sb.append("<IsRepeating>").append((IsRepeating)?1:0).append("</IsRepeating>");
			sb.append("<DateType>").append(DateType.ordinal()).append("</DateType>");
			sb.append("<FromNum>").append(FromNum).append("</FromNum>");
			sb.append("<ObjectType>").append(ObjectType.ordinal()).append("</ObjectType>");
			sb.append("<DateTimeEntry>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateTimeEntry)).append("</DateTimeEntry>");
			sb.append("<UserNum>").append(UserNum).append("</UserNum>");
			sb.append("<DateTimeFinished>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateTimeFinished)).append("</DateTimeFinished>");
			sb.append("</Task>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"TaskNum")!=null) {
					TaskNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"TaskNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"TaskListNum")!=null) {
					TaskListNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"TaskListNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"DateTask")!=null) {
					DateTask=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"DateTask"));
				}
				if(Serializing.getXmlNodeValue(doc,"KeyNum")!=null) {
					KeyNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"KeyNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"Descript")!=null) {
					Descript=Serializing.getXmlNodeValue(doc,"Descript");
				}
				if(Serializing.getXmlNodeValue(doc,"TaskStatus")!=null) {
					TaskStatus=TaskStatusEnum.values()[Integer.valueOf(Serializing.getXmlNodeValue(doc,"TaskStatus"))];
				}
				if(Serializing.getXmlNodeValue(doc,"IsRepeating")!=null) {
					IsRepeating=(Serializing.getXmlNodeValue(doc,"IsRepeating")=="0")?false:true;
				}
				if(Serializing.getXmlNodeValue(doc,"DateType")!=null) {
					DateType=TaskDateType.values()[Integer.valueOf(Serializing.getXmlNodeValue(doc,"DateType"))];
				}
				if(Serializing.getXmlNodeValue(doc,"FromNum")!=null) {
					FromNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"FromNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"ObjectType")!=null) {
					ObjectType=TaskObjectType.values()[Integer.valueOf(Serializing.getXmlNodeValue(doc,"ObjectType"))];
				}
				if(Serializing.getXmlNodeValue(doc,"DateTimeEntry")!=null) {
					DateTimeEntry=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"DateTimeEntry"));
				}
				if(Serializing.getXmlNodeValue(doc,"UserNum")!=null) {
					UserNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"UserNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"DateTimeFinished")!=null) {
					DateTimeFinished=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"DateTimeFinished"));
				}
			}
			catch(Exception e) {
				throw e;
			}
		}

		/**  */
		public enum TaskStatusEnum {
			/** 0 */
			New,
			/** 1 */
			Viewed,
			/** 2 */
			Done
		}

		/**  */
		public enum TaskDateType {
			/** 0 */
			None,
			/** 1 */
			Day,
			/** 2 */
			Week,
			/** 3 */
			Month
		}

		/** Used when attaching objects to tasks.  These are the choices. */
		public enum TaskObjectType {
			/** 0 */
			None,
			/** 1 */
			Patient,
			/** 2 */
			Appointment
		}


}
