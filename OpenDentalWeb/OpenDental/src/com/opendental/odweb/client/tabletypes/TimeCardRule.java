package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

public class TimeCardRule {
		/** A rule for automation of timecard overtime.  Can apply to one employee or all. */
		public int TimeCardRuleNum;
		/** FK to employee.EmployeeNum. If zero, then this rule applies to all employees. */
		public int EmployeeNum;
		/** Typical example is 8:00.  In California, any work after the first 8 hours is overtime. */
		public String OverHoursPerDay;
		/** Typical example is 16:00 to indicate that all time worked after 4pm for specific employees is overtime. */
		public String AfterTimeOfDay;
		/** Typical example is 6:00 to indicate that all time worked before 6am for specific employees is overtime. */
		public String BeforeTimeOfDay;
		/** Differential paid to employees working before or after the specified times. */
		public double AmtDiff;

		/** Deep copy of object. */
		public TimeCardRule Copy() {
			TimeCardRule timecardrule=new TimeCardRule();
			timecardrule.TimeCardRuleNum=this.TimeCardRuleNum;
			timecardrule.EmployeeNum=this.EmployeeNum;
			timecardrule.OverHoursPerDay=this.OverHoursPerDay;
			timecardrule.AfterTimeOfDay=this.AfterTimeOfDay;
			timecardrule.BeforeTimeOfDay=this.BeforeTimeOfDay;
			timecardrule.AmtDiff=this.AmtDiff;
			return timecardrule;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<TimeCardRule>");
			sb.append("<TimeCardRuleNum>").append(TimeCardRuleNum).append("</TimeCardRuleNum>");
			sb.append("<EmployeeNum>").append(EmployeeNum).append("</EmployeeNum>");
			sb.append("<OverHoursPerDay>").append(Serializing.EscapeForXml(OverHoursPerDay)).append("</OverHoursPerDay>");
			sb.append("<AfterTimeOfDay>").append(Serializing.EscapeForXml(AfterTimeOfDay)).append("</AfterTimeOfDay>");
			sb.append("<BeforeTimeOfDay>").append(Serializing.EscapeForXml(BeforeTimeOfDay)).append("</BeforeTimeOfDay>");
			sb.append("<AmtDiff>").append(AmtDiff).append("</AmtDiff>");
			sb.append("</TimeCardRule>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				TimeCardRuleNum=Integer.valueOf(doc.getElementsByTagName("TimeCardRuleNum").item(0).getFirstChild().getNodeValue());
				EmployeeNum=Integer.valueOf(doc.getElementsByTagName("EmployeeNum").item(0).getFirstChild().getNodeValue());
				OverHoursPerDay=doc.getElementsByTagName("OverHoursPerDay").item(0).getFirstChild().getNodeValue();
				AfterTimeOfDay=doc.getElementsByTagName("AfterTimeOfDay").item(0).getFirstChild().getNodeValue();
				BeforeTimeOfDay=doc.getElementsByTagName("BeforeTimeOfDay").item(0).getFirstChild().getNodeValue();
				AmtDiff=Double.valueOf(doc.getElementsByTagName("AmtDiff").item(0).getFirstChild().getNodeValue());
			}
			catch(Exception e) {
				throw e;
			}
		}


}
