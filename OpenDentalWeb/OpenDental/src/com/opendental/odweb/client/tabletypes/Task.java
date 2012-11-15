package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
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
		public Task Copy() {
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
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<Task>");
			sb.append("<TaskNum>").append(TaskNum).append("</TaskNum>");
			sb.append("<TaskListNum>").append(TaskListNum).append("</TaskListNum>");
			sb.append("<DateTask>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateTask)).append("</DateTask>");
			sb.append("<KeyNum>").append(KeyNum).append("</KeyNum>");
			sb.append("<Descript>").append(Serializing.EscapeForXml(Descript)).append("</Descript>");
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

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				TaskNum=Integer.valueOf(doc.getElementsByTagName("TaskNum").item(0).getFirstChild().getNodeValue());
				TaskListNum=Integer.valueOf(doc.getElementsByTagName("TaskListNum").item(0).getFirstChild().getNodeValue());
				DateTask=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(doc.getElementsByTagName("DateTask").item(0).getFirstChild().getNodeValue());
				KeyNum=Integer.valueOf(doc.getElementsByTagName("KeyNum").item(0).getFirstChild().getNodeValue());
				Descript=doc.getElementsByTagName("Descript").item(0).getFirstChild().getNodeValue();
				TaskStatus=TaskStatusEnum.values()[Integer.valueOf(doc.getElementsByTagName("TaskStatus").item(0).getFirstChild().getNodeValue())];
				IsRepeating=(doc.getElementsByTagName("IsRepeating").item(0).getFirstChild().getNodeValue()=="0")?false:true;
				DateType=TaskDateType.values()[Integer.valueOf(doc.getElementsByTagName("DateType").item(0).getFirstChild().getNodeValue())];
				FromNum=Integer.valueOf(doc.getElementsByTagName("FromNum").item(0).getFirstChild().getNodeValue());
				ObjectType=TaskObjectType.values()[Integer.valueOf(doc.getElementsByTagName("ObjectType").item(0).getFirstChild().getNodeValue())];
				DateTimeEntry=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(doc.getElementsByTagName("DateTimeEntry").item(0).getFirstChild().getNodeValue());
				UserNum=Integer.valueOf(doc.getElementsByTagName("UserNum").item(0).getFirstChild().getNodeValue());
				DateTimeFinished=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(doc.getElementsByTagName("DateTimeFinished").item(0).getFirstChild().getNodeValue());
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
