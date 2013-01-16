package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

//DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD.
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
		public ApptFieldDef deepCopy() {
			ApptFieldDef apptfielddef=new ApptFieldDef();
			apptfielddef.ApptFieldDefNum=this.ApptFieldDefNum;
			apptfielddef.FieldName=this.FieldName;
			apptfielddef.FieldType=this.FieldType;
			apptfielddef.PickList=this.PickList;
			return apptfielddef;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<ApptFieldDef>");
			sb.append("<ApptFieldDefNum>").append(ApptFieldDefNum).append("</ApptFieldDefNum>");
			sb.append("<FieldName>").append(Serializing.escapeForXml(FieldName)).append("</FieldName>");
			sb.append("<FieldType>").append(FieldType.ordinal()).append("</FieldType>");
			sb.append("<PickList>").append(Serializing.escapeForXml(PickList)).append("</PickList>");
			sb.append("</ApptFieldDef>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"ApptFieldDefNum")!=null) {
					ApptFieldDefNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ApptFieldDefNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"FieldName")!=null) {
					FieldName=Serializing.getXmlNodeValue(doc,"FieldName");
				}
				if(Serializing.getXmlNodeValue(doc,"FieldType")!=null) {
					FieldType=ApptFieldType.values()[Integer.valueOf(Serializing.getXmlNodeValue(doc,"FieldType"))];
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
		public enum ApptFieldType {
			/** 0 */
			Text,
			/** 1 */
			PickList
		}


}
