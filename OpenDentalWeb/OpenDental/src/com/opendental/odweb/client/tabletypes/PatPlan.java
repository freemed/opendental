package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

public class PatPlan {
		/** Primary key */
		public int PatPlanNum;
		/** FK to  patient.PatNum.  The patient who currently has the insurance.  Not the same as the subscriber. */
		public int PatNum;
		/** Number like 1, 2, 3, etc.  Represents primary ins, secondary ins, tertiary ins, etc. 0 is not used */
		public byte Ordinal;
		/** For informational purposes only. For now, we lose the previous feature which let us set isPending without entering a plan.  Now, you have to enter the plan in order to check this box. */
		public boolean IsPending;
		/** Enum:Relat Remember that this may need to be changed in the Claim also, if already created. */
		public Relat Relationship;
		/** An optional patient ID which will override the insplan.SubscriberID on eclaims.  For Canada, this holds the Dependent Code, C17 and E17, and in that use it doesn't override subscriber id, but instead supplements it. */
		public String PatID;
		/** FK to inssub.InsSubNum.  Gives info about the subscriber. */
		public int InsSubNum;

		/** Deep copy of object. */
		public PatPlan Copy() {
			PatPlan patplan=new PatPlan();
			patplan.PatPlanNum=this.PatPlanNum;
			patplan.PatNum=this.PatNum;
			patplan.Ordinal=this.Ordinal;
			patplan.IsPending=this.IsPending;
			patplan.Relationship=this.Relationship;
			patplan.PatID=this.PatID;
			patplan.InsSubNum=this.InsSubNum;
			return patplan;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<PatPlan>");
			sb.append("<PatPlanNum>").append(PatPlanNum).append("</PatPlanNum>");
			sb.append("<PatNum>").append(PatNum).append("</PatNum>");
			sb.append("<Ordinal>").append(Ordinal).append("</Ordinal>");
			sb.append("<IsPending>").append((IsPending)?1:0).append("</IsPending>");
			sb.append("<Relationship>").append(Relationship.ordinal()).append("</Relationship>");
			sb.append("<PatID>").append(Serializing.EscapeForXml(PatID)).append("</PatID>");
			sb.append("<InsSubNum>").append(InsSubNum).append("</InsSubNum>");
			sb.append("</PatPlan>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				PatPlanNum=Integer.valueOf(doc.getElementsByTagName("PatPlanNum").item(0).getFirstChild().getNodeValue());
				PatNum=Integer.valueOf(doc.getElementsByTagName("PatNum").item(0).getFirstChild().getNodeValue());
				Ordinal=Byte.valueOf(doc.getElementsByTagName("Ordinal").item(0).getFirstChild().getNodeValue());
				IsPending=(doc.getElementsByTagName("IsPending").item(0).getFirstChild().getNodeValue()=="0")?false:true;
				Relationship=Relat.values()[Integer.valueOf(doc.getElementsByTagName("Relationship").item(0).getFirstChild().getNodeValue())];
				PatID=doc.getElementsByTagName("PatID").item(0).getFirstChild().getNodeValue();
				InsSubNum=Integer.valueOf(doc.getElementsByTagName("InsSubNum").item(0).getFirstChild().getNodeValue());
			}
			catch(Exception e) {
				throw e;
			}
		}

		/** Relationship to subscriber for insurance. */
		public enum Relat {
			/** 0 */
			Self,
			/** 1 */
			Spouse,
			/** 2 */
			Child,
			/** 3 */
			Employee,
			/** 4 */
			HandicapDep,
			/** 5 */
			SignifOther,
			/** 6 */
			InjuredPlaintiff,
			/** 7 */
			LifePartner,
			/** 8 */
			Dependent
		}


}
