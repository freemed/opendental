package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

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
		public TreatPlan Copy() {
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
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<TreatPlan>");
			sb.append("<TreatPlanNum>").append(TreatPlanNum).append("</TreatPlanNum>");
			sb.append("<PatNum>").append(PatNum).append("</PatNum>");
			sb.append("<DateTP>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateTP)).append("</DateTP>");
			sb.append("<Heading>").append(Serializing.EscapeForXml(Heading)).append("</Heading>");
			sb.append("<Note>").append(Serializing.EscapeForXml(Note)).append("</Note>");
			sb.append("<Signature>").append(Serializing.EscapeForXml(Signature)).append("</Signature>");
			sb.append("<SigIsTopaz>").append((SigIsTopaz)?1:0).append("</SigIsTopaz>");
			sb.append("<ResponsParty>").append(ResponsParty).append("</ResponsParty>");
			sb.append("</TreatPlan>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void DeserializeFromXml(Document doc) throws Exception {
			try {
				if(Serializing.GetXmlNodeValue(doc,"TreatPlanNum")!=null) {
					TreatPlanNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"TreatPlanNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"PatNum")!=null) {
					PatNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"PatNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"DateTP")!=null) {
					DateTP=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.GetXmlNodeValue(doc,"DateTP"));
				}
				if(Serializing.GetXmlNodeValue(doc,"Heading")!=null) {
					Heading=Serializing.GetXmlNodeValue(doc,"Heading");
				}
				if(Serializing.GetXmlNodeValue(doc,"Note")!=null) {
					Note=Serializing.GetXmlNodeValue(doc,"Note");
				}
				if(Serializing.GetXmlNodeValue(doc,"Signature")!=null) {
					Signature=Serializing.GetXmlNodeValue(doc,"Signature");
				}
				if(Serializing.GetXmlNodeValue(doc,"SigIsTopaz")!=null) {
					SigIsTopaz=(Serializing.GetXmlNodeValue(doc,"SigIsTopaz")=="0")?false:true;
				}
				if(Serializing.GetXmlNodeValue(doc,"ResponsParty")!=null) {
					ResponsParty=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"ResponsParty"));
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
