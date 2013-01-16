package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

//DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD.
public class AppointmentRule {
		/** Primary key. */
		public int AppointmentRuleNum;
		/** The description of the rule which will be displayed to the user. */
		public String RuleDesc;
		/** The procedure code of the start of the range. */
		public String CodeStart;
		/** The procedure code of the end of the range. */
		public String CodeEnd;
		/** Usually true.  But this does allow you to turn off a rule temporarily without losing the settings. */
		public boolean IsEnabled;

		/** Deep copy of object. */
		public AppointmentRule deepCopy() {
			AppointmentRule appointmentrule=new AppointmentRule();
			appointmentrule.AppointmentRuleNum=this.AppointmentRuleNum;
			appointmentrule.RuleDesc=this.RuleDesc;
			appointmentrule.CodeStart=this.CodeStart;
			appointmentrule.CodeEnd=this.CodeEnd;
			appointmentrule.IsEnabled=this.IsEnabled;
			return appointmentrule;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<AppointmentRule>");
			sb.append("<AppointmentRuleNum>").append(AppointmentRuleNum).append("</AppointmentRuleNum>");
			sb.append("<RuleDesc>").append(Serializing.escapeForXml(RuleDesc)).append("</RuleDesc>");
			sb.append("<CodeStart>").append(Serializing.escapeForXml(CodeStart)).append("</CodeStart>");
			sb.append("<CodeEnd>").append(Serializing.escapeForXml(CodeEnd)).append("</CodeEnd>");
			sb.append("<IsEnabled>").append((IsEnabled)?1:0).append("</IsEnabled>");
			sb.append("</AppointmentRule>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"AppointmentRuleNum")!=null) {
					AppointmentRuleNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"AppointmentRuleNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"RuleDesc")!=null) {
					RuleDesc=Serializing.getXmlNodeValue(doc,"RuleDesc");
				}
				if(Serializing.getXmlNodeValue(doc,"CodeStart")!=null) {
					CodeStart=Serializing.getXmlNodeValue(doc,"CodeStart");
				}
				if(Serializing.getXmlNodeValue(doc,"CodeEnd")!=null) {
					CodeEnd=Serializing.getXmlNodeValue(doc,"CodeEnd");
				}
				if(Serializing.getXmlNodeValue(doc,"IsEnabled")!=null) {
					IsEnabled=(Serializing.getXmlNodeValue(doc,"IsEnabled")=="0")?false:true;
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
