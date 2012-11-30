package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

public class PhoneEmpDefault {
		/** Primary key. */
		public int EmployeeNum;
		/**  */
		public boolean NoGraph;
		/**  */
		public boolean NoColor;
		/** Enum:AsteriskRingGroups 0=all, 1=none, 2=backup */
		public AsteriskRingGroups RingGroups;
		/** Just makes management easier.  Not used by the program. */
		public String EmpName;
		/** The phone extension for the employee.  e.g. 101,102,etc.  Used to be in the employee table.  This can be changed daily by staff who float from workstation to workstation.  Can be 0 in order to keep two rows from sharing the same extension. */
		public int PhoneExt;
		/** Enum:PhoneEmpStatusOverride  */
		public PhoneEmpStatusOverride StatusOverride;
		/** Used to be stored as phoneoverride.Explanation. */
		public String Notes;
		/** This is used by the cameras.  Only necessary when the ip address doesn't match the 192.168.0.2xx pattern that we normally use.  For example, if Jordan sets this value to JORDANS, then the camera on JORDANS(.186) will send its images to the phone table where extension=104.  The second consequence is that .204 will not send any camera images.  This is used heavily by remote users working from home.  If a staff floats to another .2xx workstation, then this does not need to be set since it will match their changed extension with their current workstation ip address because if follows the normal pattern.  If there are multiple ip addresses, and the camera picks up the wrong one, setting this field can fix it. */
		public String ComputerName;
		/** Can only be used by management when handling personnel issues. */
		public boolean IsPrivateScreen;
		/** Used to launch a task window instead of a commlog window when user clicks on name/phone number on the bottom left. */
		public boolean IsTriageOperator;

		/** Deep copy of object. */
		public PhoneEmpDefault Copy() {
			PhoneEmpDefault phoneempdefault=new PhoneEmpDefault();
			phoneempdefault.EmployeeNum=this.EmployeeNum;
			phoneempdefault.NoGraph=this.NoGraph;
			phoneempdefault.NoColor=this.NoColor;
			phoneempdefault.RingGroups=this.RingGroups;
			phoneempdefault.EmpName=this.EmpName;
			phoneempdefault.PhoneExt=this.PhoneExt;
			phoneempdefault.StatusOverride=this.StatusOverride;
			phoneempdefault.Notes=this.Notes;
			phoneempdefault.ComputerName=this.ComputerName;
			phoneempdefault.IsPrivateScreen=this.IsPrivateScreen;
			phoneempdefault.IsTriageOperator=this.IsTriageOperator;
			return phoneempdefault;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<PhoneEmpDefault>");
			sb.append("<EmployeeNum>").append(EmployeeNum).append("</EmployeeNum>");
			sb.append("<NoGraph>").append((NoGraph)?1:0).append("</NoGraph>");
			sb.append("<NoColor>").append((NoColor)?1:0).append("</NoColor>");
			sb.append("<RingGroups>").append(RingGroups.ordinal()).append("</RingGroups>");
			sb.append("<EmpName>").append(Serializing.EscapeForXml(EmpName)).append("</EmpName>");
			sb.append("<PhoneExt>").append(PhoneExt).append("</PhoneExt>");
			sb.append("<StatusOverride>").append(StatusOverride.ordinal()).append("</StatusOverride>");
			sb.append("<Notes>").append(Serializing.EscapeForXml(Notes)).append("</Notes>");
			sb.append("<ComputerName>").append(Serializing.EscapeForXml(ComputerName)).append("</ComputerName>");
			sb.append("<IsPrivateScreen>").append((IsPrivateScreen)?1:0).append("</IsPrivateScreen>");
			sb.append("<IsTriageOperator>").append((IsTriageOperator)?1:0).append("</IsTriageOperator>");
			sb.append("</PhoneEmpDefault>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void DeserializeFromXml(Document doc) throws Exception {
			try {
				if(Serializing.GetXmlNodeValue(doc,"EmployeeNum")!=null) {
					EmployeeNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"EmployeeNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"NoGraph")!=null) {
					NoGraph=(Serializing.GetXmlNodeValue(doc,"NoGraph")=="0")?false:true;
				}
				if(Serializing.GetXmlNodeValue(doc,"NoColor")!=null) {
					NoColor=(Serializing.GetXmlNodeValue(doc,"NoColor")=="0")?false:true;
				}
				if(Serializing.GetXmlNodeValue(doc,"RingGroups")!=null) {
					RingGroups=AsteriskRingGroups.values()[Integer.valueOf(Serializing.GetXmlNodeValue(doc,"RingGroups"))];
				}
				if(Serializing.GetXmlNodeValue(doc,"EmpName")!=null) {
					EmpName=Serializing.GetXmlNodeValue(doc,"EmpName");
				}
				if(Serializing.GetXmlNodeValue(doc,"PhoneExt")!=null) {
					PhoneExt=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"PhoneExt"));
				}
				if(Serializing.GetXmlNodeValue(doc,"StatusOverride")!=null) {
					StatusOverride=PhoneEmpStatusOverride.values()[Integer.valueOf(Serializing.GetXmlNodeValue(doc,"StatusOverride"))];
				}
				if(Serializing.GetXmlNodeValue(doc,"Notes")!=null) {
					Notes=Serializing.GetXmlNodeValue(doc,"Notes");
				}
				if(Serializing.GetXmlNodeValue(doc,"ComputerName")!=null) {
					ComputerName=Serializing.GetXmlNodeValue(doc,"ComputerName");
				}
				if(Serializing.GetXmlNodeValue(doc,"IsPrivateScreen")!=null) {
					IsPrivateScreen=(Serializing.GetXmlNodeValue(doc,"IsPrivateScreen")=="0")?false:true;
				}
				if(Serializing.GetXmlNodeValue(doc,"IsTriageOperator")!=null) {
					IsTriageOperator=(Serializing.GetXmlNodeValue(doc,"IsTriageOperator")=="0")?false:true;
				}
			}
			catch(Exception e) {
				throw e;
			}
		}

		/**  */
		public enum AsteriskRingGroups {
			/** 0 - All really means both regular and backup. Most techs.  Default. This setting is used for employees with no entries in this table */
			All,
			/** 1 - For example, Jordan and developers. */
			None,
			/** 2 - For example, Nathan. */
			Backup
		}

		/**  */
		public enum PhoneEmpStatusOverride {
			/** 0 - None. */
			None,
			/** 1  */
			Unavailable,
			/** 2 */
			OfflineAssist
		}


}
