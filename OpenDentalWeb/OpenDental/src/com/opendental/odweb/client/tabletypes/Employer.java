package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

public class Employer {
		/** Primary key. */
		public int EmployerNum;
		/** Name of the employer. */
		public String EmpName;
		/** . */
		public String Address;
		/** Second line of address. */
		public String Address2;
		/** . */
		public String City;
		/** 2 char in the US. */
		public String State;
		/** . */
		public String Zip;
		/** Includes any punctuation. */
		public String Phone;

		/** Deep copy of object. */
		public Employer deepCopy() {
			Employer employer=new Employer();
			employer.EmployerNum=this.EmployerNum;
			employer.EmpName=this.EmpName;
			employer.Address=this.Address;
			employer.Address2=this.Address2;
			employer.City=this.City;
			employer.State=this.State;
			employer.Zip=this.Zip;
			employer.Phone=this.Phone;
			return employer;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<Employer>");
			sb.append("<EmployerNum>").append(EmployerNum).append("</EmployerNum>");
			sb.append("<EmpName>").append(Serializing.escapeForXml(EmpName)).append("</EmpName>");
			sb.append("<Address>").append(Serializing.escapeForXml(Address)).append("</Address>");
			sb.append("<Address2>").append(Serializing.escapeForXml(Address2)).append("</Address2>");
			sb.append("<City>").append(Serializing.escapeForXml(City)).append("</City>");
			sb.append("<State>").append(Serializing.escapeForXml(State)).append("</State>");
			sb.append("<Zip>").append(Serializing.escapeForXml(Zip)).append("</Zip>");
			sb.append("<Phone>").append(Serializing.escapeForXml(Phone)).append("</Phone>");
			sb.append("</Employer>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"EmployerNum")!=null) {
					EmployerNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"EmployerNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"EmpName")!=null) {
					EmpName=Serializing.getXmlNodeValue(doc,"EmpName");
				}
				if(Serializing.getXmlNodeValue(doc,"Address")!=null) {
					Address=Serializing.getXmlNodeValue(doc,"Address");
				}
				if(Serializing.getXmlNodeValue(doc,"Address2")!=null) {
					Address2=Serializing.getXmlNodeValue(doc,"Address2");
				}
				if(Serializing.getXmlNodeValue(doc,"City")!=null) {
					City=Serializing.getXmlNodeValue(doc,"City");
				}
				if(Serializing.getXmlNodeValue(doc,"State")!=null) {
					State=Serializing.getXmlNodeValue(doc,"State");
				}
				if(Serializing.getXmlNodeValue(doc,"Zip")!=null) {
					Zip=Serializing.getXmlNodeValue(doc,"Zip");
				}
				if(Serializing.getXmlNodeValue(doc,"Phone")!=null) {
					Phone=Serializing.getXmlNodeValue(doc,"Phone");
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
