package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

public class ApptFieldDef {
		/** Primary key. */
		public int ApptFieldDefNum;
		/** The name of the field that the user will be allowed to fill in the appt edit window.  Duplicates are prevented. */
		public String FieldName;
		/** Enum:ApptFieldType Text=0,PickList=1 */
		public ApptFieldType FieldType;
		/** The text that contains pick list values. */
		public String PickList;

		/** Deep copy of object. */
		public ApptFieldDef Copy() {
			ApptFieldDef apptfielddef=new ApptFieldDef();
			apptfielddef.ApptFieldDefNum=this.ApptFieldDefNum;
			apptfielddef.FieldName=this.FieldName;
			apptfielddef.FieldType=this.FieldType;
			apptfielddef.PickList=this.PickList;
			return apptfielddef;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<ApptFieldDef>");
			sb.append("<ApptFieldDefNum>").append(ApptFieldDefNum).append("</ApptFieldDefNum>");
			sb.append("<FieldName>").append(Serializing.EscapeForXml(FieldName)).append("</FieldName>");
			sb.append("<FieldType>").append(FieldType.ordinal()).append("</FieldType>");
			sb.append("<PickList>").append(Serializing.EscapeForXml(PickList)).append("</PickList>");
			sb.append("</ApptFieldDef>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				if(Serializing.GetXmlNodeValue(doc,"ApptFieldDefNum")!=null) {
					ApptFieldDefNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"ApptFieldDefNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"FieldName")!=null) {
					FieldName=Serializing.GetXmlNodeValue(doc,"FieldName");
				}
				if(Serializing.GetXmlNodeValue(doc,"FieldType")!=null) {
					FieldType=ApptFieldType.values()[Integer.valueOf(Serializing.GetXmlNodeValue(doc,"FieldType"))];
				}
				if(Serializing.GetXmlNodeValue(doc,"PickList")!=null) {
					PickList=Serializing.GetXmlNodeValue(doc,"PickList");
				}
			}
			catch(Exception e) {
				throw e;
			}
		}

		/**  */
		public enum ApptFieldType {
			/** 0 */
			Text,
			/** 1 */
			PickList
		}


}
