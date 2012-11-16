package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

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
		public AppointmentRule Copy() {
			AppointmentRule appointmentrule=new AppointmentRule();
			appointmentrule.AppointmentRuleNum=this.AppointmentRuleNum;
			appointmentrule.RuleDesc=this.RuleDesc;
			appointmentrule.CodeStart=this.CodeStart;
			appointmentrule.CodeEnd=this.CodeEnd;
			appointmentrule.IsEnabled=this.IsEnabled;
			return appointmentrule;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<AppointmentRule>");
			sb.append("<AppointmentRuleNum>").append(AppointmentRuleNum).append("</AppointmentRuleNum>");
			sb.append("<RuleDesc>").append(Serializing.EscapeForXml(RuleDesc)).append("</RuleDesc>");
			sb.append("<CodeStart>").append(Serializing.EscapeForXml(CodeStart)).append("</CodeStart>");
			sb.append("<CodeEnd>").append(Serializing.EscapeForXml(CodeEnd)).append("</CodeEnd>");
			sb.append("<IsEnabled>").append((IsEnabled)?1:0).append("</IsEnabled>");
			sb.append("</AppointmentRule>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				if(Serializing.GetXmlNodeValue(doc,"AppointmentRuleNum")!=null) {
					AppointmentRuleNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"AppointmentRuleNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"RuleDesc")!=null) {
					RuleDesc=Serializing.GetXmlNodeValue(doc,"RuleDesc");
				}
				if(Serializing.GetXmlNodeValue(doc,"CodeStart")!=null) {
					CodeStart=Serializing.GetXmlNodeValue(doc,"CodeStart");
				}
				if(Serializing.GetXmlNodeValue(doc,"CodeEnd")!=null) {
					CodeEnd=Serializing.GetXmlNodeValue(doc,"CodeEnd");
				}
				if(Serializing.GetXmlNodeValue(doc,"IsEnabled")!=null) {
					IsEnabled=(Serializing.GetXmlNodeValue(doc,"IsEnabled")=="0")?false:true;
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
