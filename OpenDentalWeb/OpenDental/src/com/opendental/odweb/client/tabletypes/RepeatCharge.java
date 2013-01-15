package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

/** DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD. */
public class RepeatCharge {
		/** Primary key */
		public int RepeatChargeNum;
		/** FK to patient.PatNum. */
		public int PatNum;
		/** FK to procedurecode.ProcCode.  The code that will be added to the account as a completed procedure. */
		public String ProcCode;
		/** The amount that will be charged.  The amount from the procedurecode will not be used.  This way, a repeating charge cannot be accidentally altered. */
		public double ChargeAmt;
		/** The date of the first charge.  Charges will always be added on the same day of the month as the start date.  If more than one month goes by, then multiple charges will be added. */
		public Date DateStart;
		/** The last date on which a charge is allowed.  So if you want 12 charges, and the start date is 8/1/05, then the stop date should be 7/1/05, not 8/1/05.  Can be blank (0001-01-01) to represent a perpetual repeating charge. */
		public Date DateStop;
		/** Any note for internal use. */
		public String Note;

		/** Deep copy of object. */
		public RepeatCharge deepCopy() {
			RepeatCharge repeatcharge=new RepeatCharge();
			repeatcharge.RepeatChargeNum=this.RepeatChargeNum;
			repeatcharge.PatNum=this.PatNum;
			repeatcharge.ProcCode=this.ProcCode;
			repeatcharge.ChargeAmt=this.ChargeAmt;
			repeatcharge.DateStart=this.DateStart;
			repeatcharge.DateStop=this.DateStop;
			repeatcharge.Note=this.Note;
			return repeatcharge;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<RepeatCharge>");
			sb.append("<RepeatChargeNum>").append(RepeatChargeNum).append("</RepeatChargeNum>");
			sb.append("<PatNum>").append(PatNum).append("</PatNum>");
			sb.append("<ProcCode>").append(Serializing.escapeForXml(ProcCode)).append("</ProcCode>");
			sb.append("<ChargeAmt>").append(ChargeAmt).append("</ChargeAmt>");
			sb.append("<DateStart>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateStart)).append("</DateStart>");
			sb.append("<DateStop>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateStop)).append("</DateStop>");
			sb.append("<Note>").append(Serializing.escapeForXml(Note)).append("</Note>");
			sb.append("</RepeatCharge>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"RepeatChargeNum")!=null) {
					RepeatChargeNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"RepeatChargeNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"PatNum")!=null) {
					PatNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"PatNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"ProcCode")!=null) {
					ProcCode=Serializing.getXmlNodeValue(doc,"ProcCode");
				}
				if(Serializing.getXmlNodeValue(doc,"ChargeAmt")!=null) {
					ChargeAmt=Double.valueOf(Serializing.getXmlNodeValue(doc,"ChargeAmt"));
				}
				if(Serializing.getXmlNodeValue(doc,"DateStart")!=null) {
					DateStart=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"DateStart"));
				}
				if(Serializing.getXmlNodeValue(doc,"DateStop")!=null) {
					DateStop=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"DateStop"));
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
