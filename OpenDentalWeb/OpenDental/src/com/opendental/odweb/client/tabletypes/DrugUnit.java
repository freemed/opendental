package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

/** DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD. */
public class DrugUnit {
		/** Primary key. */
		public int DrugUnitNum;
		/** Example ml, capitalization not critical. Usually entered as lowercase except for L. */
		public String UnitIdentifier;
		/** Example milliliter. */
		public String UnitText;

		/** Deep copy of object. */
		public DrugUnit deepCopy() {
			DrugUnit drugunit=new DrugUnit();
			drugunit.DrugUnitNum=this.DrugUnitNum;
			drugunit.UnitIdentifier=this.UnitIdentifier;
			drugunit.UnitText=this.UnitText;
			return drugunit;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<DrugUnit>");
			sb.append("<DrugUnitNum>").append(DrugUnitNum).append("</DrugUnitNum>");
			sb.append("<UnitIdentifier>").append(Serializing.escapeForXml(UnitIdentifier)).append("</UnitIdentifier>");
			sb.append("<UnitText>").append(Serializing.escapeForXml(UnitText)).append("</UnitText>");
			sb.append("</DrugUnit>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"DrugUnitNum")!=null) {
					DrugUnitNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"DrugUnitNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"UnitIdentifier")!=null) {
					UnitIdentifier=Serializing.getXmlNodeValue(doc,"UnitIdentifier");
				}
				if(Serializing.getXmlNodeValue(doc,"UnitText")!=null) {
					UnitText=Serializing.getXmlNodeValue(doc,"UnitText");
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
