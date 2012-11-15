package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

public class Allergy {
		/** Primary key. */
		public int AllergyNum;
		/** FK to allergydef.AllergyDefNum */
		public int AllergyDefNum;
		/** FK to patient.PatNum */
		public int PatNum;
		/** Adverse reaction description. */
		public String Reaction;
		/** True if still an active allergy.  False helps hide it from the list of active allergies. */
		public boolean StatusIsActive;
		/** To be used for synch with web server for CertTimelyAccess. */
		public Date DateTStamp;
		/** The historical date that the patient had the adverse reaction to this agent. */
		public Date DateAdverseReaction;

		/** Deep copy of object. */
		public Allergy Copy() {
			Allergy allergy=new Allergy();
			allergy.AllergyNum=this.AllergyNum;
			allergy.AllergyDefNum=this.AllergyDefNum;
			allergy.PatNum=this.PatNum;
			allergy.Reaction=this.Reaction;
			allergy.StatusIsActive=this.StatusIsActive;
			allergy.DateTStamp=this.DateTStamp;
			allergy.DateAdverseReaction=this.DateAdverseReaction;
			return allergy;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<Allergy>");
			sb.append("<AllergyNum>").append(AllergyNum).append("</AllergyNum>");
			sb.append("<AllergyDefNum>").append(AllergyDefNum).append("</AllergyDefNum>");
			sb.append("<PatNum>").append(PatNum).append("</PatNum>");
			sb.append("<Reaction>").append(Serializing.EscapeForXml(Reaction)).append("</Reaction>");
			sb.append("<StatusIsActive>").append((StatusIsActive)?1:0).append("</StatusIsActive>");
			sb.append("<DateTStamp>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(AptDateTime)).append("</DateTStamp>");
			sb.append("<DateAdverseReaction>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(AptDateTime)).append("</DateAdverseReaction>");
			sb.append("</Allergy>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				AllergyNum=Integer.valueOf(doc.getElementsByTagName("AllergyNum").item(0).getFirstChild().getNodeValue());
				AllergyDefNum=Integer.valueOf(doc.getElementsByTagName("AllergyDefNum").item(0).getFirstChild().getNodeValue());
				PatNum=Integer.valueOf(doc.getElementsByTagName("PatNum").item(0).getFirstChild().getNodeValue());
				Reaction=doc.getElementsByTagName("Reaction").item(0).getFirstChild().getNodeValue();
				StatusIsActive=(doc.getElementsByTagName("StatusIsActive").item(0).getFirstChild().getNodeValue()=="0")?false:true;
				DateTStamp=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(doc.getElementsByTagName("DateTStamp").item(0).getFirstChild().getNodeValue());
				DateAdverseReaction=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(doc.getElementsByTagName("DateAdverseReaction").item(0).getFirstChild().getNodeValue());
			}
			catch(Exception e) {
				throw e;
			}
		}


}
