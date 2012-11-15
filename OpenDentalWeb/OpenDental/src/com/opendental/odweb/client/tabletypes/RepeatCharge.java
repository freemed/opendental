package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

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
		public RepeatCharge Copy() {
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
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<RepeatCharge>");
			sb.append("<RepeatChargeNum>").append(RepeatChargeNum).append("</RepeatChargeNum>");
			sb.append("<PatNum>").append(PatNum).append("</PatNum>");
			sb.append("<ProcCode>").append(Serializing.EscapeForXml(ProcCode)).append("</ProcCode>");
			sb.append("<ChargeAmt>").append(ChargeAmt).append("</ChargeAmt>");
			sb.append("<DateStart>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(AptDateTime)).append("</DateStart>");
			sb.append("<DateStop>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(AptDateTime)).append("</DateStop>");
			sb.append("<Note>").append(Serializing.EscapeForXml(Note)).append("</Note>");
			sb.append("</RepeatCharge>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				RepeatChargeNum=Integer.valueOf(doc.getElementsByTagName("RepeatChargeNum").item(0).getFirstChild().getNodeValue());
				PatNum=Integer.valueOf(doc.getElementsByTagName("PatNum").item(0).getFirstChild().getNodeValue());
				ProcCode=doc.getElementsByTagName("ProcCode").item(0).getFirstChild().getNodeValue();
				ChargeAmt=Double.valueOf(doc.getElementsByTagName("ChargeAmt").item(0).getFirstChild().getNodeValue());
				DateStart=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(doc.getElementsByTagName("DateStart").item(0).getFirstChild().getNodeValue());
				DateStop=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(doc.getElementsByTagName("DateStop").item(0).getFirstChild().getNodeValue());
				Note=doc.getElementsByTagName("Note").item(0).getFirstChild().getNodeValue();
			}
			catch(Exception e) {
				throw e;
			}
		}


}
