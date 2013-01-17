package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

//DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD.
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
		public AllergyDef deepCopy() {
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
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<AllergyDef>");
			sb.append("<AllergyDefNum>").append(AllergyDefNum).append("</AllergyDefNum>");
			sb.append("<Description>").append(Serializing.escapeForXml(Description)).append("</Description>");
			sb.append("<IsHidden>").append((IsHidden)?1:0).append("</IsHidden>");
			sb.append("<DateTStamp>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateTStamp)).append("</DateTStamp>");
			sb.append("<Snomed>").append(Snomed.ordinal()).append("</Snomed>");
			sb.append("<MedicationNum>").append(MedicationNum).append("</MedicationNum>");
			sb.append("</AllergyDef>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"AllergyDefNum")!=null) {
					AllergyDefNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"AllergyDefNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"Description")!=null) {
					Description=Serializing.getXmlNodeValue(doc,"Description");
				}
				if(Serializing.getXmlNodeValue(doc,"IsHidden")!=null) {
					IsHidden=(Serializing.getXmlNodeValue(doc,"IsHidden")=="0")?false:true;
				}
				if(Serializing.getXmlNodeValue(doc,"DateTStamp")!=null) {
					DateTStamp=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"DateTStamp"));
				}
				if(Serializing.getXmlNodeValue(doc,"Snomed")!=null) {
					Snomed=SnomedAllergy.valueOf(Serializing.getXmlNodeValue(doc,"Snomed"));
				}
				if(Serializing.getXmlNodeValue(doc,"MedicationNum")!=null) {
					MedicationNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"MedicationNum"));
				}
			}
			catch(Exception e) {
				throw new Exception("Error deserializing AllergyDef: "+e.getMessage());
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
