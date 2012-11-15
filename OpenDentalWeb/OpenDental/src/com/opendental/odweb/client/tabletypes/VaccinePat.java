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
				VaccinePatNum=Integer.valueOf(doc.getElementsByTagName("VaccinePatNum").item(0).getFirstChild().getNodeValue());
				VaccineDefNum=Integer.valueOf(doc.getElementsByTagName("VaccineDefNum").item(0).getFirstChild().getNodeValue());
				DateTimeStart=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(doc.getElementsByTagName("DateTimeStart").item(0).getFirstChild().getNodeValue());
				DateTimeEnd=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(doc.getElementsByTagName("DateTimeEnd").item(0).getFirstChild().getNodeValue());
				AdministeredAmt=Float.valueOf(doc.getElementsByTagName("AdministeredAmt").item(0).getFirstChild().getNodeValue());
				DrugUnitNum=Integer.valueOf(doc.getElementsByTagName("DrugUnitNum").item(0).getFirstChild().getNodeValue());
				LotNumber=doc.getElementsByTagName("LotNumber").item(0).getFirstChild().getNodeValue();
				PatNum=Integer.valueOf(doc.getElementsByTagName("PatNum").item(0).getFirstChild().getNodeValue());
				NotGiven=(doc.getElementsByTagName("NotGiven").item(0).getFirstChild().getNodeValue()=="0")?false:true;
				Note=doc.getElementsByTagName("Note").item(0).getFirstChild().getNodeValue();
			}
			catch(Exception e) {
				throw e;
			}
		}


}
