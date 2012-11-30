package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
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

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void DeserializeFromXml(Document doc) throws Exception {
			try {
				if(Serializing.GetXmlNodeValue(doc,"RxNum")!=null) {
					RxNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"RxNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"PatNum")!=null) {
					PatNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"PatNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"RxDate")!=null) {
					RxDate=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.GetXmlNodeValue(doc,"RxDate"));
				}
				if(Serializing.GetXmlNodeValue(doc,"Drug")!=null) {
					Drug=Serializing.GetXmlNodeValue(doc,"Drug");
				}
				if(Serializing.GetXmlNodeValue(doc,"Sig")!=null) {
					Sig=Serializing.GetXmlNodeValue(doc,"Sig");
				}
				if(Serializing.GetXmlNodeValue(doc,"Disp")!=null) {
					Disp=Serializing.GetXmlNodeValue(doc,"Disp");
				}
				if(Serializing.GetXmlNodeValue(doc,"Refills")!=null) {
					Refills=Serializing.GetXmlNodeValue(doc,"Refills");
				}
				if(Serializing.GetXmlNodeValue(doc,"ProvNum")!=null) {
					ProvNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"ProvNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"Notes")!=null) {
					Notes=Serializing.GetXmlNodeValue(doc,"Notes");
				}
				if(Serializing.GetXmlNodeValue(doc,"PharmacyNum")!=null) {
					PharmacyNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"PharmacyNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"IsControlled")!=null) {
					IsControlled=(Serializing.GetXmlNodeValue(doc,"IsControlled")=="0")?false:true;
				}
				if(Serializing.GetXmlNodeValue(doc,"DateTStamp")!=null) {
					DateTStamp=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.GetXmlNodeValue(doc,"DateTStamp"));
				}
				if(Serializing.GetXmlNodeValue(doc,"SendStatus")!=null) {
					SendStatus=RxSendStatus.values()[Integer.valueOf(Serializing.GetXmlNodeValue(doc,"SendStatus"))];
				}
				if(Serializing.GetXmlNodeValue(doc,"RxCui")!=null) {
					RxCui=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"RxCui"));
				}
				if(Serializing.GetXmlNodeValue(doc,"DosageCode")!=null) {
					DosageCode=Serializing.GetXmlNodeValue(doc,"DosageCode");
				}
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
