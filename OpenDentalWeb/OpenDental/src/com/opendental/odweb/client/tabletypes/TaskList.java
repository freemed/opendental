package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

public class TaskList {
		/** Primary key. */
		public int TaskListNum;
		/** The description of this tasklist.  Might be very long, but not usually. */
		public String Descript;
		/** FK to tasklist.TaskListNum  The parent task list to which this task list is assigned.  If zero, then this task list is on the main trunk of one of the sections. */
		public int Parent;
		/** Optional. Set to 0001-01-01 for no date.  If a date is assigned, then this list will also be available from the date section. */
		public Date DateTL;
		/** True if it is to show in the repeating section.  There should be no date.  All children should also be set to IsRepeating=true. */
		public boolean IsRepeating;
		/** Enum:TaskDateType  None, Day, Week, Month.  If IsRepeating, then setting to None effectively disables the repeating feature. */
		public TaskDateType DateType;
		/** FK to tasklist.TaskListNum  If this is derived from a repeating list, then this will hold the TaskListNum of that list.  It helps automate the adding and deleting of lists.  It might be deleted automatically if no tasks are marked complete. */
		public int FromNum;
		/** Enum:TaskObjectType  0=none, 1=Patient, 2=Appointment.  More will be added later. If a type is selected, then this list will be visible in the appropriate places for attaching the correct type of object.  The type is not copied to a task when created.  Tasks in this list do not have to be of the same type.  You can only attach an object to a task, not a tasklist. */
		public TaskObjectType ObjectType;
		/** The date and time that this list was added.  Used to sort the list by the order entered. */
		public Date DateTimeEntry;

		/** Deep copy of object. */
		public TaskList Copy() {
			TaskList tasklist=new TaskList();
			tasklist.TaskListNum=this.TaskListNum;
			tasklist.Descript=this.Descript;
			tasklist.Parent=this.Parent;
			tasklist.DateTL=this.DateTL;
			tasklist.IsRepeating=this.IsRepeating;
			tasklist.DateType=this.DateType;
			tasklist.FromNum=this.FromNum;
			tasklist.ObjectType=this.ObjectType;
			tasklist.DateTimeEntry=this.DateTimeEntry;
			return tasklist;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<TaskList>");
			sb.append("<TaskListNum>").append(TaskListNum).append("</TaskListNum>");
			sb.append("<Descript>").append(Serializing.EscapeForXml(Descript)).append("</Descript>");
			sb.append("<Parent>").append(Parent).append("</Parent>");
			sb.append("<DateTL>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateTL)).append("</DateTL>");
			sb.append("<IsRepeating>").append((IsRepeating)?1:0).append("</IsRepeating>");
			sb.append("<DateType>").append(DateType.ordinal()).append("</DateType>");
			sb.append("<FromNum>").append(FromNum).append("</FromNum>");
			sb.append("<ObjectType>").append(ObjectType.ordinal()).append("</ObjectType>");
			sb.append("<DateTimeEntry>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateTimeEntry)).append("</DateTimeEntry>");
			sb.append("</TaskList>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				TaskListNum=Integer.valueOf(doc.getElementsByTagName("TaskListNum").item(0).getFirstChild().getNodeValue());
				Descript=doc.getElementsByTagName("Descript").item(0).getFirstChild().getNodeValue();
				Parent=Integer.valueOf(doc.getElementsByTagName("Parent").item(0).getFirstChild().getNodeValue());
				DateTL=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(doc.getElementsByTagName("DateTL").item(0).getFirstChild().getNodeValue());
				IsRepeating=(doc.getElementsByTagName("IsRepeating").item(0).getFirstChild().getNodeValue()=="0")?false:true;
				DateType=TaskDateType.values()[Integer.valueOf(doc.getElementsByTagName("DateType").item(0).getFirstChild().getNodeValue())];
				FromNum=Integer.valueOf(doc.getElementsByTagName("FromNum").item(0).getFirstChild().getNodeValue());
				ObjectType=TaskObjectType.values()[Integer.valueOf(doc.getElementsByTagName("ObjectType").item(0).getFirstChild().getNodeValue())];
				DateTimeEntry=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(doc.getElementsByTagName("DateTimeEntry").item(0).getFirstChild().getNodeValue());
			}
			catch(Exception e) {
				throw e;
			}
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
