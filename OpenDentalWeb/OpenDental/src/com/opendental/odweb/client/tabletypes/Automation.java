package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

public class Automation {
		/** Primary key. */
		public int AutomationNum;
		/** . */
		public String Description;
		/** Enum:AutomationTrigger What triggers this automation */
		public AutomationTrigger Autotrigger;
		/** If this has a CompleteProcedure trigger, this is a comma-delimited list of codes that will trigger the action. */
		public String ProcCodes;
		/** Enum:AutomationAction The action taken as a result of the trigger.  To get more than one action, create multiple automation entries. */
		public AutomationAction AutoAction;
		/** FK to sheetdef.SheetDefNum.  If the action is to print a sheet, then this tells which sheet to print.  So it must be a custom sheet.  Also, not that this organization does not allow passing parameters to the sheet such as which procedures were completed, or which appt was broken. */
		public int SheetDefNum;
		/** FK to definition.DefNum. Only used if action is CreateCommlog. */
		public int CommType;
		/** If a commlog action, then this is the text that goes in the commlog.  If this is a ShowStatementNoteBold action, then this is the NoteBold. Might later be expanded to work with email or to use variables. */
		public String MessageContent;

		/** Deep copy of object. */
		public Automation Copy() {
			Automation automation=new Automation();
			automation.AutomationNum=this.AutomationNum;
			automation.Description=this.Description;
			automation.Autotrigger=this.Autotrigger;
			automation.ProcCodes=this.ProcCodes;
			automation.AutoAction=this.AutoAction;
			automation.SheetDefNum=this.SheetDefNum;
			automation.CommType=this.CommType;
			automation.MessageContent=this.MessageContent;
			return automation;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<Automation>");
			sb.append("<AutomationNum>").append(AutomationNum).append("</AutomationNum>");
			sb.append("<Description>").append(Serializing.EscapeForXml(Description)).append("</Description>");
			sb.append("<Autotrigger>").append(Autotrigger.ordinal()).append("</Autotrigger>");
			sb.append("<ProcCodes>").append(Serializing.EscapeForXml(ProcCodes)).append("</ProcCodes>");
			sb.append("<AutoAction>").append(AutoAction.ordinal()).append("</AutoAction>");
			sb.append("<SheetDefNum>").append(SheetDefNum).append("</SheetDefNum>");
			sb.append("<CommType>").append(CommType).append("</CommType>");
			sb.append("<MessageContent>").append(Serializing.EscapeForXml(MessageContent)).append("</MessageContent>");
			sb.append("</Automation>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				AutomationNum=Integer.valueOf(doc.getElementsByTagName("AutomationNum").item(0).getFirstChild().getNodeValue());
				Description=doc.getElementsByTagName("Description").item(0).getFirstChild().getNodeValue();
				Autotrigger=AutomationTrigger.values()[Integer.valueOf(doc.getElementsByTagName("Autotrigger").item(0).getFirstChild().getNodeValue())];
				ProcCodes=doc.getElementsByTagName("ProcCodes").item(0).getFirstChild().getNodeValue();
				AutoAction=AutomationAction.values()[Integer.valueOf(doc.getElementsByTagName("AutoAction").item(0).getFirstChild().getNodeValue())];
				SheetDefNum=Integer.valueOf(doc.getElementsByTagName("SheetDefNum").item(0).getFirstChild().getNodeValue());
				CommType=Integer.valueOf(doc.getElementsByTagName("CommType").item(0).getFirstChild().getNodeValue());
				MessageContent=doc.getElementsByTagName("MessageContent").item(0).getFirstChild().getNodeValue();
			}
			catch(Exception e) {
				throw e;
			}
		}

		/**  */
		public enum AutomationTrigger {
			/**  */
			CompleteProcedure,
			/**  */
			BreakAppointment,
			/**  */
			CreateApptNewPat,
			/** Regardless of module.  Usually only used with conditions. */
			OpenPatient
		}

		/**  */
		public enum AutomationAction {
			/**  */
			PrintPatientLetter,
			/**  */
			CreateCommlog,
			/** If a referral does not exist for this patient, then notify user instead. */
			PrintReferralLetter,
			/**  */
			ShowExamSheet,
			/**  */
			PopUp
		}


}
