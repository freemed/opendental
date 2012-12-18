package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

public class PatFieldDef {
		/** Primary key. */
		public int PatFieldDefNum;
		/** The name of the field that the user will be allowed to fill in the patient info window. */
		public String FieldName;
		/** Enum:PatFieldType Text=0,PickList=1,Date=2,Checkbox=3,Currency=4 */
		public PatFieldType FieldType;
		/** The text that contains pick list values. */
		public String PickList;

		/** Deep copy of object. */
		public PatFieldDef deepCopy() {
			PatFieldDef patfielddef=new PatFieldDef();
			patfielddef.PatFieldDefNum=this.PatFieldDefNum;
			patfielddef.FieldName=this.FieldName;
			patfielddef.FieldType=this.FieldType;
			patfielddef.PickList=this.PickList;
			return patfielddef;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<PatFieldDef>");
			sb.append("<PatFieldDefNum>").append(PatFieldDefNum).append("</PatFieldDefNum>");
			sb.append("<FieldName>").append(Serializing.escapeForXml(FieldName)).append("</FieldName>");
			sb.append("<FieldType>").append(FieldType.ordinal()).append("</FieldType>");
			sb.append("<PickList>").append(Serializing.escapeForXml(PickList)).append("</PickList>");
			sb.append("</PatFieldDef>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"PatFieldDefNum")!=null) {
					PatFieldDefNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"PatFieldDefNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"FieldName")!=null) {
					FieldName=Serializing.getXmlNodeValue(doc,"FieldName");
				}
				if(Serializing.getXmlNodeValue(doc,"FieldType")!=null) {
					FieldType=PatFieldType.values()[Integer.valueOf(Serializing.getXmlNodeValue(doc,"FieldType"))];
				}
				if(Serializing.getXmlNodeValue(doc,"PickList")!=null) {
					PickList=Serializing.getXmlNodeValue(doc,"PickList");
				}
			}
			catch(Exception e) {
				throw e;
			}
		}

		/**  */
		public enum PatFieldType {
			/** 0 */
			Text,
			/** 1 */
			PickList,
			/** 2-Stored in db as entered, already localized.  For example, it could be 2/04/11, 2/4/11, 2/4/2011, or any other variant.  This makes it harder to create queries that filter by date, but easier to display dates as part of results. */
			Date,
			/** 3-If checked, value stored as "1".  If unchecked, row deleted. */
			Checkbox,
			/** 4-This seems to have been added without implementing.  Not sure what will happen if someone tries to use it. */
			Currency
		}


}
