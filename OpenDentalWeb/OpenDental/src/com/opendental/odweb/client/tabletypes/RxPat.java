package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

public class RxPat {
		/** Primary key. */
		public int RxNum;
		/** FK to patient.PatNum. */
		public int PatNum;
		/** Date of Rx. */
		public Date RxDate;
		/** Drug name. */
		public String Drug;
		/** Directions. */
		public String Sig;
		/** Amount to dispense. */
		public String Disp;
		/** Number of refills. */
		public String Refills;
		/** FK to provider.ProvNum. */
		public int ProvNum;
		/** Notes specific to this Rx.  Will not show on the printout.  For staff use only. */
		public String Notes;
		/** FK to pharmacy.PharmacyNum. */
		public int PharmacyNum;
		/** Is a controlled substance.  This will affect the way it prints. */
		public boolean IsControlled;
		/** The last date and time this row was altered.  Not user editable. */
		public Date DateTStamp;
		/** Enum:RxSendStatus  */
		public RxSendStatus SendStatus;
		/** RxNorm Code identifier. */
		public int RxCui;
		/** NCI Pharmaceutical Dosage Form code.  Only used with ehr. */
		public String DosageCode;

		/** Deep copy of object. */
		public RxPat Copy() {
			RxPat rxpat=new RxPat();
			rxpat.RxNum=this.RxNum;
			rxpat.PatNum=this.PatNum;
			rxpat.RxDate=this.RxDate;
			rxpat.Drug=this.Drug;
			rxpat.Sig=this.Sig;
			rxpat.Disp=this.Disp;
			rxpat.Refills=this.Refills;
			rxpat.ProvNum=this.ProvNum;
			rxpat.Notes=this.Notes;
			rxpat.PharmacyNum=this.PharmacyNum;
			rxpat.IsControlled=this.IsControlled;
			rxpat.DateTStamp=this.DateTStamp;
			rxpat.SendStatus=this.SendStatus;
			rxpat.RxCui=this.RxCui;
			rxpat.DosageCode=this.DosageCode;
			return rxpat;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<RxPat>");
			sb.append("<RxNum>").append(RxNum).append("</RxNum>");
			sb.append("<PatNum>").append(PatNum).append("</PatNum>");
			sb.append("<RxDate>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(RxDate)).append("</RxDate>");
			sb.append("<Drug>").append(Serializing.EscapeForXml(Drug)).append("</Drug>");
			sb.append("<Sig>").append(Serializing.EscapeForXml(Sig)).append("</Sig>");
			sb.append("<Disp>").append(Serializing.EscapeForXml(Disp)).append("</Disp>");
			sb.append("<Refills>").append(Serializing.EscapeForXml(Refills)).append("</Refills>");
			sb.append("<ProvNum>").append(ProvNum).append("</ProvNum>");
			sb.append("<Notes>").append(Serializing.EscapeForXml(Notes)).append("</Notes>");
			sb.append("<PharmacyNum>").append(PharmacyNum).append("</PharmacyNum>");
			sb.append("<IsControlled>").append((IsControlled)?1:0).append("</IsControlled>");
			sb.append("<DateTStamp>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateTStamp)).append("</DateTStamp>");
			sb.append("<SendStatus>").append(SendStatus.ordinal()).append("</SendStatus>");
			sb.append("<RxCui>").append(RxCui).append("</RxCui>");
			sb.append("<DosageCode>").append(Serializing.EscapeForXml(DosageCode)).append("</DosageCode>");
			sb.append("</RxPat>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				RxNum=Integer.valueOf(doc.getElementsByTagName("RxNum").item(0).getFirstChild().getNodeValue());
				PatNum=Integer.valueOf(doc.getElementsByTagName("PatNum").item(0).getFirstChild().getNodeValue());
				RxDate=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(doc.getElementsByTagName("RxDate").item(0).getFirstChild().getNodeValue());
				Drug=doc.getElementsByTagName("Drug").item(0).getFirstChild().getNodeValue();
				Sig=doc.getElementsByTagName("Sig").item(0).getFirstChild().getNodeValue();
				Disp=doc.getElementsByTagName("Disp").item(0).getFirstChild().getNodeValue();
				Refills=doc.getElementsByTagName("Refills").item(0).getFirstChild().getNodeValue();
				ProvNum=Integer.valueOf(doc.getElementsByTagName("ProvNum").item(0).getFirstChild().getNodeValue());
				Notes=doc.getElementsByTagName("Notes").item(0).getFirstChild().getNodeValue();
				PharmacyNum=Integer.valueOf(doc.getElementsByTagName("PharmacyNum").item(0).getFirstChild().getNodeValue());
				IsControlled=(doc.getElementsByTagName("IsControlled").item(0).getFirstChild().getNodeValue()=="0")?false:true;
				DateTStamp=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(doc.getElementsByTagName("DateTStamp").item(0).getFirstChild().getNodeValue());
				SendStatus=RxSendStatus.values()[Integer.valueOf(doc.getElementsByTagName("SendStatus").item(0).getFirstChild().getNodeValue())];
				RxCui=Integer.valueOf(doc.getElementsByTagName("RxCui").item(0).getFirstChild().getNodeValue());
				DosageCode=doc.getElementsByTagName("DosageCode").item(0).getFirstChild().getNodeValue();
			}
			catch(Exception e) {
				throw e;
			}
		}

		/**  */
		public enum RxSendStatus {
			/** 0 */
			Unsent,
			/** 1 */
			InElectQueue,
			/** 2 */
			SentElect,
			/** 3 */
			Printed
		}


}
