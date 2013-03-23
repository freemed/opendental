package com.opendental.opendentbusiness.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.opendentbusiness.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

//DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD.
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
		public Allergy deepCopy() {
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
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<Allergy>");
			sb.append("<AllergyNum>").append(AllergyNum).append("</AllergyNum>");
			sb.append("<AllergyDefNum>").append(AllergyDefNum).append("</AllergyDefNum>");
			sb.append("<PatNum>").append(PatNum).append("</PatNum>");
			sb.append("<Reaction>").append(Serializing.escapeForXml(Reaction)).append("</Reaction>");
			sb.append("<StatusIsActive>").append((StatusIsActive)?1:0).append("</StatusIsActive>");
			sb.append("<DateTStamp>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateTStamp)).append("</DateTStamp>");
			sb.append("<DateAdverseReaction>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateAdverseReaction)).append("</DateAdverseReaction>");
			sb.append("</Allergy>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"AllergyNum")!=null) {
					AllergyNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"AllergyNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"AllergyDefNum")!=null) {
					AllergyDefNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"AllergyDefNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"PatNum")!=null) {
					PatNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"PatNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"Reaction")!=null) {
					Reaction=Serializing.getXmlNodeValue(doc,"Reaction");
				}
				if(Serializing.getXmlNodeValue(doc,"StatusIsActive")!=null) {
					StatusIsActive=(Serializing.getXmlNodeValue(doc,"StatusIsActive")=="0")?false:true;
				}
				if(Serializing.getXmlNodeValue(doc,"DateTStamp")!=null) {
					DateTStamp=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"DateTStamp"));
				}
				if(Serializing.getXmlNodeValue(doc,"DateAdverseReaction")!=null) {
					DateAdverseReaction=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"DateAdverseReaction"));
				}
			}
			catch(Exception e) {
				throw new Exception("Error deserializing Allergy: "+e.getMessage());
			}
		}


}
