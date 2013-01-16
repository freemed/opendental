package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

//DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD.
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
		public Automation deepCopy() {
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
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<Automation>");
			sb.append("<AutomationNum>").append(AutomationNum).append("</AutomationNum>");
			sb.append("<Description>").append(Serializing.escapeForXml(Description)).append("</Description>");
			sb.append("<Autotrigger>").append(Autotrigger.ordinal()).append("</Autotrigger>");
			sb.append("<ProcCodes>").append(Serializing.escapeForXml(ProcCodes)).append("</ProcCodes>");
			sb.append("<AutoAction>").append(AutoAction.ordinal()).append("</AutoAction>");
			sb.append("<SheetDefNum>").append(SheetDefNum).append("</SheetDefNum>");
			sb.append("<CommType>").append(CommType).append("</CommType>");
			sb.append("<MessageContent>").append(Serializing.escapeForXml(MessageContent)).append("</MessageContent>");
			sb.append("</Automation>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"AutomationNum")!=null) {
					AutomationNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"AutomationNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"Description")!=null) {
					Description=Serializing.getXmlNodeValue(doc,"Description");
				}
				if(Serializing.getXmlNodeValue(doc,"Autotrigger")!=null) {
					Autotrigger=AutomationTrigger.values()[Integer.valueOf(Serializing.getXmlNodeValue(doc,"Autotrigger"))];
				}
				if(Serializing.getXmlNodeValue(doc,"ProcCodes")!=null) {
					ProcCodes=Serializing.getXmlNodeValue(doc,"ProcCodes");
				}
				if(Serializing.getXmlNodeValue(doc,"AutoAction")!=null) {
					AutoAction=AutomationAction.values()[Integer.valueOf(Serializing.getXmlNodeValue(doc,"AutoAction"))];
				}
				if(Serializing.getXmlNodeValue(doc,"SheetDefNum")!=null) {
					SheetDefNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"SheetDefNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"CommType")!=null) {
					CommType=Integer.valueOf(Serializing.getXmlNodeValue(doc,"CommType"));
				}
				if(Serializing.getXmlNodeValue(doc,"MessageContent")!=null) {
					MessageContent=Serializing.getXmlNodeValue(doc,"MessageContent");
				}
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
