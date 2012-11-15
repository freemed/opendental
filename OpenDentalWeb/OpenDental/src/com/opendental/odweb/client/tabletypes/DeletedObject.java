package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

public class DeletedObject {
		/** Primary key. */
		public int DeletedObjectNum;
		/** Foreign key to a number of different tables, depending on which type it is. */
		public int ObjectNum;
		/** Enum:DeletedObjectType  */
		public DeletedObjectType ObjectType;
		/** Updated any time the row is altered in any way. */
		public Date DateTStamp;

		/** Deep copy of object. */
		public DeletedObject Copy() {
			DeletedObject deletedobject=new DeletedObject();
			deletedobject.DeletedObjectNum=this.DeletedObjectNum;
			deletedobject.ObjectNum=this.ObjectNum;
			deletedobject.ObjectType=this.ObjectType;
			deletedobject.DateTStamp=this.DateTStamp;
			return deletedobject;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<DeletedObject>");
			sb.append("<DeletedObjectNum>").append(DeletedObjectNum).append("</DeletedObjectNum>");
			sb.append("<ObjectNum>").append(ObjectNum).append("</ObjectNum>");
			sb.append("<ObjectType>").append(ObjectType.ordinal()).append("</ObjectType>");
			sb.append("<DateTStamp>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateTStamp)).append("</DateTStamp>");
			sb.append("</DeletedObject>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				DeletedObjectNum=Integer.valueOf(doc.getElementsByTagName("DeletedObjectNum").item(0).getFirstChild().getNodeValue());
				ObjectNum=Integer.valueOf(doc.getElementsByTagName("ObjectNum").item(0).getFirstChild().getNodeValue());
				ObjectType=DeletedObjectType.values()[Integer.valueOf(doc.getElementsByTagName("ObjectType").item(0).getFirstChild().getNodeValue())];
				DateTStamp=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(doc.getElementsByTagName("DateTStamp").item(0).getFirstChild().getNodeValue());
			}
			catch(Exception e) {
				throw e;
			}
		}

		/**  */
		public enum DeletedObjectType {
			/** 0 */
			Appointment,
			/** 1 - A schedule object.  Only provider schedules are tracked for deletion. */
			ScheduleProv,
			/** 2 - When a recall row is deleted, this records the PatNum for which it was deleted. */
			RecallPatNum,
			/**  */
			RxPat,
			/**  */
			LabPanel,
			/**  */
			LabResult,
			/**  */
			DrugUnit,
			/**  */
			Medication,
			/**  */
			MedicationPat,
			/**  */
			Allergy,
			/**  */
			AllergyDef,
			/**  */
			Disease,
			/**  */
			DiseaseDef,
			/**  */
			ICD9,
			/**  */
			Provider,
			/**  */
			Pharmacy,
			/**  */
			Statement,
			/**  */
			Document,
			/**  */
			Recall
		}


}
