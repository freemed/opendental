package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
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

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				ZipCodeNum=Integer.valueOf(doc.getElementsByTagName("ZipCodeNum").item(0).getFirstChild().getNodeValue());
				ZipCodeDigits=doc.getElementsByTagName("ZipCodeDigits").item(0).getFirstChild().getNodeValue();
				City=doc.getElementsByTagName("City").item(0).getFirstChild().getNodeValue();
				State=doc.getElementsByTagName("State").item(0).getFirstChild().getNodeValue();
				IsFrequent=(doc.getElementsByTagName("IsFrequent").item(0).getFirstChild().getNodeValue()=="0")?false:true;
			}
			catch(Exception e) {
				throw e;
			}
		}


}
