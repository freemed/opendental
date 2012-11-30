package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
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

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void DeserializeFromXml(Document doc) throws Exception {
			try {
				if(Serializing.GetXmlNodeValue(doc,"TimeCardRuleNum")!=null) {
					TimeCardRuleNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"TimeCardRuleNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"EmployeeNum")!=null) {
					EmployeeNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"EmployeeNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"OverHoursPerDay")!=null) {
					OverHoursPerDay=Serializing.GetXmlNodeValue(doc,"OverHoursPerDay");
				}
				if(Serializing.GetXmlNodeValue(doc,"AfterTimeOfDay")!=null) {
					AfterTimeOfDay=Serializing.GetXmlNodeValue(doc,"AfterTimeOfDay");
				}
				if(Serializing.GetXmlNodeValue(doc,"BeforeTimeOfDay")!=null) {
					BeforeTimeOfDay=Serializing.GetXmlNodeValue(doc,"BeforeTimeOfDay");
				}
				if(Serializing.GetXmlNodeValue(doc,"AmtDiff")!=null) {
					AmtDiff=Double.valueOf(Serializing.GetXmlNodeValue(doc,"AmtDiff"));
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
