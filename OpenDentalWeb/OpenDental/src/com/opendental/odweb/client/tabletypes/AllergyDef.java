package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

public class AllergyDef {
		/** Primary key. */
		public int AllergyDefNum;
		/** Name of the drug.  User can change this.  If an RxCui is present, the RxNorm string can be pulled from the in-memory table for UI display in addition to the Description. */
		public String Description;
		/** Because user can't delete. */
		public boolean IsHidden;
		/** The last date and time this row was altered.  Not user editable. */
		public Date DateTStamp;
		/** Enum:SnomedAllergy SNOMED Allergy Type Code. */
		public SnomedAllergy Snomed;
		/** FK to Medication.MedicationNum. Optional. */
		public int MedicationNum;

		/** Deep copy of object. */
		public AllergyDef Copy() {
			AllergyDef allergydef=new AllergyDef();
			allergydef.AllergyDefNum=this.AllergyDefNum;
			allergydef.Description=this.Description;
			allergydef.IsHidden=this.IsHidden;
			allergydef.DateTStamp=this.DateTStamp;
			allergydef.Snomed=this.Snomed;
			allergydef.MedicationNum=this.MedicationNum;
			return allergydef;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<AllergyDef>");
			sb.append("<AllergyDefNum>").append(AllergyDefNum).append("</AllergyDefNum>");
			sb.append("<Description>").append(Serializing.EscapeForXml(Description)).append("</Description>");
			sb.append("<IsHidden>").append((IsHidden)?1:0).append("</IsHidden>");
			sb.append("<DateTStamp>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateTStamp)).append("</DateTStamp>");
			sb.append("<Snomed>").append(Snomed.ordinal()).append("</Snomed>");
			sb.append("<MedicationNum>").append(MedicationNum).append("</MedicationNum>");
			sb.append("</AllergyDef>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				AllergyDefNum=Integer.valueOf(doc.getElementsByTagName("AllergyDefNum").item(0).getFirstChild().getNodeValue());
				Description=doc.getElementsByTagName("Description").item(0).getFirstChild().getNodeValue();
				IsHidden=(doc.getElementsByTagName("IsHidden").item(0).getFirstChild().getNodeValue()=="0")?false:true;
				DateTStamp=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(doc.getElementsByTagName("DateTStamp").item(0).getFirstChild().getNodeValue());
				Snomed=SnomedAllergy.values()[Integer.valueOf(doc.getElementsByTagName("Snomed").item(0).getFirstChild().getNodeValue())];
				MedicationNum=Integer.valueOf(doc.getElementsByTagName("MedicationNum").item(0).getFirstChild().getNodeValue());
			}
			catch(Exception e) {
				throw e;
			}
		}

		/**  */
		public enum SnomedAllergy {
			/** 0-No SNOMED allergy type code has been assigned. */
			None,
			/** 1-Allergy to substance (disorder), code number 418038007. */
			AllergyToSubstance,
			/** 2-Drug allergy (disorder), code number 416098002. */
			DrugAllergy,
			/** 3-Drug intolerance (disorder), code number 59037007. */
			DrugIntolerance,
			/** 4-Food allergy (disorder), code number 414285001. */
			FoodAllergy,
			/** 5-Food intolerance (disorder), code number 235719002. */
			FoodIntolerance,
			/** 6-Propensity to adverse reactions (disorder), code number 420134006. */
			AdverseReactions,
			/** 7-Propensity to adverse reactions to drug (disorder), code number 419511003 */
			AdverseReactionsToDrug,
			/** 8-Propensity to adverse reactions to food (disorder), code number 418471000. */
			AdverseReactionsToFood,
			/** 9-Propensity to adverse reactions to substance (disorder), code number 419199007. */
			AdverseReactionsToSubstance
		}


}
