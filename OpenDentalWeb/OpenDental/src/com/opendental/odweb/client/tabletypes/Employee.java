package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

//DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD.
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
		public Employee deepCopy() {
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
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<Employee>");
			sb.append("<EmployeeNum>").append(EmployeeNum).append("</EmployeeNum>");
			sb.append("<LName>").append(Serializing.escapeForXml(LName)).append("</LName>");
			sb.append("<FName>").append(Serializing.escapeForXml(FName)).append("</FName>");
			sb.append("<MiddleI>").append(Serializing.escapeForXml(MiddleI)).append("</MiddleI>");
			sb.append("<IsHidden>").append((IsHidden)?1:0).append("</IsHidden>");
			sb.append("<ClockStatus>").append(Serializing.escapeForXml(ClockStatus)).append("</ClockStatus>");
			sb.append("<PhoneExt>").append(PhoneExt).append("</PhoneExt>");
			sb.append("</Employee>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"EmployeeNum")!=null) {
					EmployeeNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"EmployeeNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"LName")!=null) {
					LName=Serializing.getXmlNodeValue(doc,"LName");
				}
				if(Serializing.getXmlNodeValue(doc,"FName")!=null) {
					FName=Serializing.getXmlNodeValue(doc,"FName");
				}
				if(Serializing.getXmlNodeValue(doc,"MiddleI")!=null) {
					MiddleI=Serializing.getXmlNodeValue(doc,"MiddleI");
				}
				if(Serializing.getXmlNodeValue(doc,"IsHidden")!=null) {
					IsHidden=(Serializing.getXmlNodeValue(doc,"IsHidden")=="0")?false:true;
				}
				if(Serializing.getXmlNodeValue(doc,"ClockStatus")!=null) {
					ClockStatus=Serializing.getXmlNodeValue(doc,"ClockStatus");
				}
				if(Serializing.getXmlNodeValue(doc,"PhoneExt")!=null) {
					PhoneExt=Integer.valueOf(Serializing.getXmlNodeValue(doc,"PhoneExt"));
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
