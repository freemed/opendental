package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

public class DrugUnit {
		/** Primary key. */
		public int DrugUnitNum;
		/** Example ml, capitalization not critical. Usually entered as lowercase except for L. */
		public String UnitIdentifier;
		/** Example milliliter. */
		public String UnitText;

		/** Deep copy of object. */
		public DrugUnit Copy() {
			DrugUnit drugunit=new DrugUnit();
			drugunit.DrugUnitNum=this.DrugUnitNum;
			drugunit.UnitIdentifier=this.UnitIdentifier;
			drugunit.UnitText=this.UnitText;
			return drugunit;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<DrugUnit>");
			sb.append("<DrugUnitNum>").append(DrugUnitNum).append("</DrugUnitNum>");
			sb.append("<UnitIdentifier>").append(Serializing.EscapeForXml(UnitIdentifier)).append("</UnitIdentifier>");
			sb.append("<UnitText>").append(Serializing.EscapeForXml(UnitText)).append("</UnitText>");
			sb.append("</DrugUnit>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void DeserializeFromXml(Document doc) throws Exception {
			try {
				if(Serializing.GetXmlNodeValue(doc,"DrugUnitNum")!=null) {
					DrugUnitNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"DrugUnitNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"UnitIdentifier")!=null) {
					UnitIdentifier=Serializing.GetXmlNodeValue(doc,"UnitIdentifier");
				}
				if(Serializing.GetXmlNodeValue(doc,"UnitText")!=null) {
					UnitText=Serializing.GetXmlNodeValue(doc,"UnitText");
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
