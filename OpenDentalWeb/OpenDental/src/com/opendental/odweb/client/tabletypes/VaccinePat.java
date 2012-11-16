package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

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
		public VaccinePat Copy() {
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
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<VaccinePat>");
			sb.append("<VaccinePatNum>").append(VaccinePatNum).append("</VaccinePatNum>");
			sb.append("<VaccineDefNum>").append(VaccineDefNum).append("</VaccineDefNum>");
			sb.append("<DateTimeStart>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateTimeStart)).append("</DateTimeStart>");
			sb.append("<DateTimeEnd>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateTimeEnd)).append("</DateTimeEnd>");
			sb.append("<AdministeredAmt>").append(AdministeredAmt).append("</AdministeredAmt>");
			sb.append("<DrugUnitNum>").append(DrugUnitNum).append("</DrugUnitNum>");
			sb.append("<LotNumber>").append(Serializing.EscapeForXml(LotNumber)).append("</LotNumber>");
			sb.append("<PatNum>").append(PatNum).append("</PatNum>");
			sb.append("<NotGiven>").append((NotGiven)?1:0).append("</NotGiven>");
			sb.append("<Note>").append(Serializing.EscapeForXml(Note)).append("</Note>");
			sb.append("</VaccinePat>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				if(Serializing.GetXmlNodeValue(doc,"VaccinePatNum")!=null) {
					VaccinePatNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"VaccinePatNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"VaccineDefNum")!=null) {
					VaccineDefNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"VaccineDefNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"DateTimeStart")!=null) {
					DateTimeStart=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.GetXmlNodeValue(doc,"DateTimeStart"));
				}
				if(Serializing.GetXmlNodeValue(doc,"DateTimeEnd")!=null) {
					DateTimeEnd=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.GetXmlNodeValue(doc,"DateTimeEnd"));
				}
				if(Serializing.GetXmlNodeValue(doc,"AdministeredAmt")!=null) {
					AdministeredAmt=Float.valueOf(Serializing.GetXmlNodeValue(doc,"AdministeredAmt"));
				}
				if(Serializing.GetXmlNodeValue(doc,"DrugUnitNum")!=null) {
					DrugUnitNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"DrugUnitNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"LotNumber")!=null) {
					LotNumber=Serializing.GetXmlNodeValue(doc,"LotNumber");
				}
				if(Serializing.GetXmlNodeValue(doc,"PatNum")!=null) {
					PatNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"PatNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"NotGiven")!=null) {
					NotGiven=(Serializing.GetXmlNodeValue(doc,"NotGiven")=="0")?false:true;
				}
				if(Serializing.GetXmlNodeValue(doc,"Note")!=null) {
					Note=Serializing.GetXmlNodeValue(doc,"Note");
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
