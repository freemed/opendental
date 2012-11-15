package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
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
			sb.append("<DateTP>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(AptDateTime)).append("</DateTP>");
			sb.append("<Heading>").append(Serializing.EscapeForXml(Heading)).append("</Heading>");
			sb.append("<Note>").append(Serializing.EscapeForXml(Note)).append("</Note>");
			sb.append("<Signature>").append(Serializing.EscapeForXml(Signature)).append("</Signature>");
			sb.append("<SigIsTopaz>").append((SigIsTopaz)?1:0).append("</SigIsTopaz>");
			sb.append("<ResponsParty>").append(ResponsParty).append("</ResponsParty>");
			sb.append("</TreatPlan>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				TreatPlanNum=Integer.valueOf(doc.getElementsByTagName("TreatPlanNum").item(0).getFirstChild().getNodeValue());
				PatNum=Integer.valueOf(doc.getElementsByTagName("PatNum").item(0).getFirstChild().getNodeValue());
				DateTP=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(doc.getElementsByTagName("DateTP").item(0).getFirstChild().getNodeValue());
				Heading=doc.getElementsByTagName("Heading").item(0).getFirstChild().getNodeValue();
				Note=doc.getElementsByTagName("Note").item(0).getFirstChild().getNodeValue();
				Signature=doc.getElementsByTagName("Signature").item(0).getFirstChild().getNodeValue();
				SigIsTopaz=(doc.getElementsByTagName("SigIsTopaz").item(0).getFirstChild().getNodeValue()=="0")?false:true;
				ResponsParty=Integer.valueOf(doc.getElementsByTagName("ResponsParty").item(0).getFirstChild().getNodeValue());
			}
			catch(Exception e) {
				throw e;
			}
		}


}
