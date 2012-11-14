package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
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

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				EmployerNum=Integer.valueOf(doc.getElementsByTagName("EmployerNum").item(0).getFirstChild().getNodeValue());
				EmpName=doc.getElementsByTagName("EmpName").item(0).getFirstChild().getNodeValue();
				Address=doc.getElementsByTagName("Address").item(0).getFirstChild().getNodeValue();
				Address2=doc.getElementsByTagName("Address2").item(0).getFirstChild().getNodeValue();
				City=doc.getElementsByTagName("City").item(0).getFirstChild().getNodeValue();
				State=doc.getElementsByTagName("State").item(0).getFirstChild().getNodeValue();
				Zip=doc.getElementsByTagName("Zip").item(0).getFirstChild().getNodeValue();
				Phone=doc.getElementsByTagName("Phone").item(0).getFirstChild().getNodeValue();
			}
			catch(Exception e) {
				throw e;
			}
		}


}
