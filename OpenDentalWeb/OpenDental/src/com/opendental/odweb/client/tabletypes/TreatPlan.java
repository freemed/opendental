package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

//DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD.
public class TreatPlan {
		/** Primary key. */
		public int TreatPlanNum;
		/** FK to patient.PatNum. */
		public int PatNum;
		/** The date of the treatment plan */
		public Date DateTP;
		/** The heading that shows at the top of the treatment plan.  Usually 'Proposed Treatment Plan' */
		public String Heading;
		/** A note specific to this treatment plan that shows at the bottom. */
		public String Note;
		/** The encrypted and bound signature in base64 format.  The signature is bound to the byte sequence of the original image. */
		public String Signature;
		/** True if the signature is in Topaz format rather than OD format. */
		public boolean SigIsTopaz;
		/** FK to patient.PatNum. Can be 0.  The patient responsible for approving the treatment.  Public health field not visible to everyone else. */
		public int ResponsParty;

		/** Deep copy of object. */
		public TreatPlan deepCopy() {
			TreatPlan treatplan=new TreatPlan();
			treatplan.TreatPlanNum=this.TreatPlanNum;
			treatplan.PatNum=this.PatNum;
			treatplan.DateTP=this.DateTP;
			treatplan.Heading=this.Heading;
			treatplan.Note=this.Note;
			treatplan.Signature=this.Signature;
			treatplan.SigIsTopaz=this.SigIsTopaz;
			treatplan.ResponsParty=this.ResponsParty;
			return treatplan;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<TreatPlan>");
			sb.append("<TreatPlanNum>").append(TreatPlanNum).append("</TreatPlanNum>");
			sb.append("<PatNum>").append(PatNum).append("</PatNum>");
			sb.append("<DateTP>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateTP)).append("</DateTP>");
			sb.append("<Heading>").append(Serializing.escapeForXml(Heading)).append("</Heading>");
			sb.append("<Note>").append(Serializing.escapeForXml(Note)).append("</Note>");
			sb.append("<Signature>").append(Serializing.escapeForXml(Signature)).append("</Signature>");
			sb.append("<SigIsTopaz>").append((SigIsTopaz)?1:0).append("</SigIsTopaz>");
			sb.append("<ResponsParty>").append(ResponsParty).append("</ResponsParty>");
			sb.append("</TreatPlan>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"TreatPlanNum")!=null) {
					TreatPlanNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"TreatPlanNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"PatNum")!=null) {
					PatNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"PatNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"DateTP")!=null) {
					DateTP=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"DateTP"));
				}
				if(Serializing.getXmlNodeValue(doc,"Heading")!=null) {
					Heading=Serializing.getXmlNodeValue(doc,"Heading");
				}
				if(Serializing.getXmlNodeValue(doc,"Note")!=null) {
					Note=Serializing.getXmlNodeValue(doc,"Note");
				}
				if(Serializing.getXmlNodeValue(doc,"Signature")!=null) {
					Signature=Serializing.getXmlNodeValue(doc,"Signature");
				}
				if(Serializing.getXmlNodeValue(doc,"SigIsTopaz")!=null) {
					SigIsTopaz=(Serializing.getXmlNodeValue(doc,"SigIsTopaz")=="0")?false:true;
				}
				if(Serializing.getXmlNodeValue(doc,"ResponsParty")!=null) {
					ResponsParty=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ResponsParty"));
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
