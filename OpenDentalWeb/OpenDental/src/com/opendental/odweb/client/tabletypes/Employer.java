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
		public Employer Copy() {
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
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<Employer>");
			sb.append("<EmployerNum>").append(EmployerNum).append("</EmployerNum>");
			sb.append("<EmpName>").append(Serializing.EscapeForXml(EmpName)).append("</EmpName>");
			sb.append("<Address>").append(Serializing.EscapeForXml(Address)).append("</Address>");
			sb.append("<Address2>").append(Serializing.EscapeForXml(Address2)).append("</Address2>");
			sb.append("<City>").append(Serializing.EscapeForXml(City)).append("</City>");
			sb.append("<State>").append(Serializing.EscapeForXml(State)).append("</State>");
			sb.append("<Zip>").append(Serializing.EscapeForXml(Zip)).append("</Zip>");
			sb.append("<Phone>").append(Serializing.EscapeForXml(Phone)).append("</Phone>");
			sb.append("</Employer>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void DeserializeFromXml(Document doc) throws Exception {
			try {
				if(Serializing.GetXmlNodeValue(doc,"EmployerNum")!=null) {
					EmployerNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"EmployerNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"EmpName")!=null) {
					EmpName=Serializing.GetXmlNodeValue(doc,"EmpName");
				}
				if(Serializing.GetXmlNodeValue(doc,"Address")!=null) {
					Address=Serializing.GetXmlNodeValue(doc,"Address");
				}
				if(Serializing.GetXmlNodeValue(doc,"Address2")!=null) {
					Address2=Serializing.GetXmlNodeValue(doc,"Address2");
				}
				if(Serializing.GetXmlNodeValue(doc,"City")!=null) {
					City=Serializing.GetXmlNodeValue(doc,"City");
				}
				if(Serializing.GetXmlNodeValue(doc,"State")!=null) {
					State=Serializing.GetXmlNodeValue(doc,"State");
				}
				if(Serializing.GetXmlNodeValue(doc,"Zip")!=null) {
					Zip=Serializing.GetXmlNodeValue(doc,"Zip");
				}
				if(Serializing.GetXmlNodeValue(doc,"Phone")!=null) {
					Phone=Serializing.GetXmlNodeValue(doc,"Phone");
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
