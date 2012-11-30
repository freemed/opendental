package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
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

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void DeserializeFromXml(Document doc) throws Exception {
			try {
				if(Serializing.GetXmlNodeValue(doc,"DeletedObjectNum")!=null) {
					DeletedObjectNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"DeletedObjectNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"ObjectNum")!=null) {
					ObjectNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"ObjectNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"ObjectType")!=null) {
					ObjectType=DeletedObjectType.values()[Integer.valueOf(Serializing.GetXmlNodeValue(doc,"ObjectType"))];
				}
				if(Serializing.GetXmlNodeValue(doc,"DateTStamp")!=null) {
					DateTStamp=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.GetXmlNodeValue(doc,"DateTStamp"));
				}
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
