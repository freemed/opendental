package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

/** DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD. */
public class VaccinePat {
		/** Primary key. */
		public int VaccinePatNum;
		/** FK to vaccinedef.VaccineDefNum. */
		public int VaccineDefNum;
		/** The datetime that the vaccine was administered. */
		public Date DateTimeStart;
		/** Typically set to the same as DateTimeStart.  User can change. */
		public Date DateTimeEnd;
		/** Size of the dose of the vaccine.  0 indicates unknown and gets converted to 999 on HL7 output. */
		public float AdministeredAmt;
		/** FK to drugunit.DrugUnitNum. Unit of measurement of the AdministeredAmt.  0 represents null. */
		public int DrugUnitNum;
		/** Optional */
		public String LotNumber;
		/** FK to patient.PatNum. */
		public int PatNum;
		/** Set to true if no vaccine given.  Documentation required in the Note. */
		public boolean NotGiven;
		/** Documentation sometimes required. */
		public String Note;

		/** Deep copy of object. */
		public VaccinePat deepCopy() {
			VaccinePat vaccinepat=new VaccinePat();
			vaccinepat.VaccinePatNum=this.VaccinePatNum;
			vaccinepat.VaccineDefNum=this.VaccineDefNum;
			vaccinepat.DateTimeStart=this.DateTimeStart;
			vaccinepat.DateTimeEnd=this.DateTimeEnd;
			vaccinepat.AdministeredAmt=this.AdministeredAmt;
			vaccinepat.DrugUnitNum=this.DrugUnitNum;
			vaccinepat.LotNumber=this.LotNumber;
			vaccinepat.PatNum=this.PatNum;
			vaccinepat.NotGiven=this.NotGiven;
			vaccinepat.Note=this.Note;
			return vaccinepat;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<VaccinePat>");
			sb.append("<VaccinePatNum>").append(VaccinePatNum).append("</VaccinePatNum>");
			sb.append("<VaccineDefNum>").append(VaccineDefNum).append("</VaccineDefNum>");
			sb.append("<DateTimeStart>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateTimeStart)).append("</DateTimeStart>");
			sb.append("<DateTimeEnd>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateTimeEnd)).append("</DateTimeEnd>");
			sb.append("<AdministeredAmt>").append(AdministeredAmt).append("</AdministeredAmt>");
			sb.append("<DrugUnitNum>").append(DrugUnitNum).append("</DrugUnitNum>");
			sb.append("<LotNumber>").append(Serializing.escapeForXml(LotNumber)).append("</LotNumber>");
			sb.append("<PatNum>").append(PatNum).append("</PatNum>");
			sb.append("<NotGiven>").append((NotGiven)?1:0).append("</NotGiven>");
			sb.append("<Note>").append(Serializing.escapeForXml(Note)).append("</Note>");
			sb.append("</VaccinePat>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"VaccinePatNum")!=null) {
					VaccinePatNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"VaccinePatNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"VaccineDefNum")!=null) {
					VaccineDefNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"VaccineDefNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"DateTimeStart")!=null) {
					DateTimeStart=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"DateTimeStart"));
				}
				if(Serializing.getXmlNodeValue(doc,"DateTimeEnd")!=null) {
					DateTimeEnd=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"DateTimeEnd"));
				}
				if(Serializing.getXmlNodeValue(doc,"AdministeredAmt")!=null) {
					AdministeredAmt=Float.valueOf(Serializing.getXmlNodeValue(doc,"AdministeredAmt"));
				}
				if(Serializing.getXmlNodeValue(doc,"DrugUnitNum")!=null) {
					DrugUnitNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"DrugUnitNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"LotNumber")!=null) {
					LotNumber=Serializing.getXmlNodeValue(doc,"LotNumber");
				}
				if(Serializing.getXmlNodeValue(doc,"PatNum")!=null) {
					PatNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"PatNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"NotGiven")!=null) {
					NotGiven=(Serializing.getXmlNodeValue(doc,"NotGiven")=="0")?false:true;
				}
				if(Serializing.getXmlNodeValue(doc,"Note")!=null) {
					Note=Serializing.getXmlNodeValue(doc,"Note");
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
