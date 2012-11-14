package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

public class Employee {
		/** Primary key. */
		public int EmployeeNum;
		/** Employee's last name. */
		public String LName;
		/** First name. */
		public String FName;
		/** Middle initial or name. */
		public String MiddleI;
		/** If hidden, the employee will not show on the list. */
		public boolean IsHidden;
		/** This is just text used to quickly display the clockstatus.  eg Working,Break,Lunch,Home, etc. */
		public String ClockStatus;
		/** The phone extension for the employee.  e.g. 101,102,etc.  This field is only visible for user editing if the pref DockPhonePanelShow is true (1). */
		public int PhoneExt;

		/** Deep copy of object. */
		public Employee Copy() {
			Employee employee=new Employee();
			employee.EmployeeNum=this.EmployeeNum;
			employee.LName=this.LName;
			employee.FName=this.FName;
			employee.MiddleI=this.MiddleI;
			employee.IsHidden=this.IsHidden;
			employee.ClockStatus=this.ClockStatus;
			employee.PhoneExt=this.PhoneExt;
			return employee;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<Employee>");
			sb.append("<EmployeeNum>").append(EmployeeNum).append("</EmployeeNum>");
			sb.append("<LName>").append(Serializing.EscapeForXml(LName)).append("</LName>");
			sb.append("<FName>").append(Serializing.EscapeForXml(FName)).append("</FName>");
			sb.append("<MiddleI>").append(Serializing.EscapeForXml(MiddleI)).append("</MiddleI>");
			sb.append("<IsHidden>").append((IsHidden)?1:0).append("</IsHidden>");
			sb.append("<ClockStatus>").append(Serializing.EscapeForXml(ClockStatus)).append("</ClockStatus>");
			sb.append("<PhoneExt>").append(PhoneExt).append("</PhoneExt>");
			sb.append("</Employee>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				EmployeeNum=Integer.valueOf(doc.getElementsByTagName("EmployeeNum").item(0).getFirstChild().getNodeValue());
				LName=doc.getElementsByTagName("LName").item(0).getFirstChild().getNodeValue();
				FName=doc.getElementsByTagName("FName").item(0).getFirstChild().getNodeValue();
				MiddleI=doc.getElementsByTagName("MiddleI").item(0).getFirstChild().getNodeValue();
				IsHidden=(doc.getElementsByTagName("IsHidden").item(0).getFirstChild().getNodeValue()=="0")?false:true;
				ClockStatus=doc.getElementsByTagName("ClockStatus").item(0).getFirstChild().getNodeValue();
				PhoneExt=Integer.valueOf(doc.getElementsByTagName("PhoneExt").item(0).getFirstChild().getNodeValue());
			}
			catch(Exception e) {
				throw e;
			}
		}


}
