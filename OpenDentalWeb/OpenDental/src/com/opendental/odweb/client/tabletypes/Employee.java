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
				if(Serializing.GetXmlNodeValue(doc,"EmployeeNum")!=null) {
					EmployeeNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"EmployeeNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"LName")!=null) {
					LName=Serializing.GetXmlNodeValue(doc,"LName");
				}
				if(Serializing.GetXmlNodeValue(doc,"FName")!=null) {
					FName=Serializing.GetXmlNodeValue(doc,"FName");
				}
				if(Serializing.GetXmlNodeValue(doc,"MiddleI")!=null) {
					MiddleI=Serializing.GetXmlNodeValue(doc,"MiddleI");
				}
				if(Serializing.GetXmlNodeValue(doc,"IsHidden")!=null) {
					IsHidden=(Serializing.GetXmlNodeValue(doc,"IsHidden")=="0")?false:true;
				}
				if(Serializing.GetXmlNodeValue(doc,"ClockStatus")!=null) {
					ClockStatus=Serializing.GetXmlNodeValue(doc,"ClockStatus");
				}
				if(Serializing.GetXmlNodeValue(doc,"PhoneExt")!=null) {
					PhoneExt=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"PhoneExt"));
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
