package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

//DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD.
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
		public PatPlan deepCopy() {
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
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<PatPlan>");
			sb.append("<PatPlanNum>").append(PatPlanNum).append("</PatPlanNum>");
			sb.append("<PatNum>").append(PatNum).append("</PatNum>");
			sb.append("<Ordinal>").append(Ordinal).append("</Ordinal>");
			sb.append("<IsPending>").append((IsPending)?1:0).append("</IsPending>");
			sb.append("<Relationship>").append(Relationship.ordinal()).append("</Relationship>");
			sb.append("<PatID>").append(Serializing.escapeForXml(PatID)).append("</PatID>");
			sb.append("<InsSubNum>").append(InsSubNum).append("</InsSubNum>");
			sb.append("</PatPlan>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"PatPlanNum")!=null) {
					PatPlanNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"PatPlanNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"PatNum")!=null) {
					PatNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"PatNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"Ordinal")!=null) {
					Ordinal=Byte.valueOf(Serializing.getXmlNodeValue(doc,"Ordinal"));
				}
				if(Serializing.getXmlNodeValue(doc,"IsPending")!=null) {
					IsPending=(Serializing.getXmlNodeValue(doc,"IsPending")=="0")?false:true;
				}
				if(Serializing.getXmlNodeValue(doc,"Relationship")!=null) {
					Relationship=Relat.values()[Integer.valueOf(Serializing.getXmlNodeValue(doc,"Relationship"))];
				}
				if(Serializing.getXmlNodeValue(doc,"PatID")!=null) {
					PatID=Serializing.getXmlNodeValue(doc,"PatID");
				}
				if(Serializing.getXmlNodeValue(doc,"InsSubNum")!=null) {
					InsSubNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"InsSubNum"));
				}
			}
			catch(Exception e) {
				throw new Exception("Error deserializing PatPlan: "+e.getMessage());
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
