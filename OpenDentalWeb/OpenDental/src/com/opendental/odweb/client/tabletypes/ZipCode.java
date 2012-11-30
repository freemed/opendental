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
		public ZipCode Copy() {
			ZipCode zipcode=new ZipCode();
			zipcode.ZipCodeNum=this.ZipCodeNum;
			zipcode.ZipCodeDigits=this.ZipCodeDigits;
			zipcode.City=this.City;
			zipcode.State=this.State;
			zipcode.IsFrequent=this.IsFrequent;
			return zipcode;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<ZipCode>");
			sb.append("<ZipCodeNum>").append(ZipCodeNum).append("</ZipCodeNum>");
			sb.append("<ZipCodeDigits>").append(Serializing.EscapeForXml(ZipCodeDigits)).append("</ZipCodeDigits>");
			sb.append("<City>").append(Serializing.EscapeForXml(City)).append("</City>");
			sb.append("<State>").append(Serializing.EscapeForXml(State)).append("</State>");
			sb.append("<IsFrequent>").append((IsFrequent)?1:0).append("</IsFrequent>");
			sb.append("</ZipCode>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void DeserializeFromXml(Document doc) throws Exception {
			try {
				if(Serializing.GetXmlNodeValue(doc,"ZipCodeNum")!=null) {
					ZipCodeNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"ZipCodeNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"ZipCodeDigits")!=null) {
					ZipCodeDigits=Serializing.GetXmlNodeValue(doc,"ZipCodeDigits");
				}
				if(Serializing.GetXmlNodeValue(doc,"City")!=null) {
					City=Serializing.GetXmlNodeValue(doc,"City");
				}
				if(Serializing.GetXmlNodeValue(doc,"State")!=null) {
					State=Serializing.GetXmlNodeValue(doc,"State");
				}
				if(Serializing.GetXmlNodeValue(doc,"IsFrequent")!=null) {
					IsFrequent=(Serializing.GetXmlNodeValue(doc,"IsFrequent")=="0")?false:true;
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
