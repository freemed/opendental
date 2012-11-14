package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
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
		public PatFieldDef Copy() {
			PatFieldDef patfielddef=new PatFieldDef();
			patfielddef.PatFieldDefNum=this.PatFieldDefNum;
			patfielddef.FieldName=this.FieldName;
			patfielddef.FieldType=this.FieldType;
			patfielddef.PickList=this.PickList;
			return patfielddef;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<PatFieldDef>");
			sb.append("<PatFieldDefNum>").append(PatFieldDefNum).append("</PatFieldDefNum>");
			sb.append("<FieldName>").append(Serializing.EscapeForXml(FieldName)).append("</FieldName>");
			sb.append("<FieldType>").append(FieldType.ordinal()).append("</FieldType>");
			sb.append("<PickList>").append(Serializing.EscapeForXml(PickList)).append("</PickList>");
			sb.append("</PatFieldDef>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				PatFieldDefNum=Integer.valueOf(doc.getElementsByTagName("PatFieldDefNum").item(0).getFirstChild().getNodeValue());
				FieldName=doc.getElementsByTagName("FieldName").item(0).getFirstChild().getNodeValue();
				FieldType=PatFieldType.values()[Integer.valueOf(doc.getElementsByTagName("FieldType").item(0).getFirstChild().getNodeValue())];
				PickList=doc.getElementsByTagName("PickList").item(0).getFirstChild().getNodeValue();
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
