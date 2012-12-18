package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

public class ZipCode {
		/** Primary key. */
		public int ZipCodeNum;
		/** The actual zipcode. */
		public String ZipCodeDigits;
		/** . */
		public String City;
		/** . */
		public String State;
		/** If true, then it will show in the dropdown list in the patient edit window. */
		public boolean IsFrequent;

		/** Deep copy of object. */
		public ZipCode deepCopy() {
			ZipCode zipcode=new ZipCode();
			zipcode.ZipCodeNum=this.ZipCodeNum;
			zipcode.ZipCodeDigits=this.ZipCodeDigits;
			zipcode.City=this.City;
			zipcode.State=this.State;
			zipcode.IsFrequent=this.IsFrequent;
			return zipcode;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<ZipCode>");
			sb.append("<ZipCodeNum>").append(ZipCodeNum).append("</ZipCodeNum>");
			sb.append("<ZipCodeDigits>").append(Serializing.escapeForXml(ZipCodeDigits)).append("</ZipCodeDigits>");
			sb.append("<City>").append(Serializing.escapeForXml(City)).append("</City>");
			sb.append("<State>").append(Serializing.escapeForXml(State)).append("</State>");
			sb.append("<IsFrequent>").append((IsFrequent)?1:0).append("</IsFrequent>");
			sb.append("</ZipCode>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"ZipCodeNum")!=null) {
					ZipCodeNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ZipCodeNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"ZipCodeDigits")!=null) {
					ZipCodeDigits=Serializing.getXmlNodeValue(doc,"ZipCodeDigits");
				}
				if(Serializing.getXmlNodeValue(doc,"City")!=null) {
					City=Serializing.getXmlNodeValue(doc,"City");
				}
				if(Serializing.getXmlNodeValue(doc,"State")!=null) {
					State=Serializing.getXmlNodeValue(doc,"State");
				}
				if(Serializing.getXmlNodeValue(doc,"IsFrequent")!=null) {
					IsFrequent=(Serializing.getXmlNodeValue(doc,"IsFrequent")=="0")?false:true;
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
