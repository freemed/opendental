package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

//DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD.
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
		/**  */
		public String NewCropGuid;

		/** Deep copy of object. */
		public RxPat deepCopy() {
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
			rxpat.NewCropGuid=this.NewCropGuid;
			return rxpat;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<RxPat>");
			sb.append("<RxNum>").append(RxNum).append("</RxNum>");
			sb.append("<PatNum>").append(PatNum).append("</PatNum>");
			sb.append("<RxDate>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(RxDate)).append("</RxDate>");
			sb.append("<Drug>").append(Serializing.escapeForXml(Drug)).append("</Drug>");
			sb.append("<Sig>").append(Serializing.escapeForXml(Sig)).append("</Sig>");
			sb.append("<Disp>").append(Serializing.escapeForXml(Disp)).append("</Disp>");
			sb.append("<Refills>").append(Serializing.escapeForXml(Refills)).append("</Refills>");
			sb.append("<ProvNum>").append(ProvNum).append("</ProvNum>");
			sb.append("<Notes>").append(Serializing.escapeForXml(Notes)).append("</Notes>");
			sb.append("<PharmacyNum>").append(PharmacyNum).append("</PharmacyNum>");
			sb.append("<IsControlled>").append((IsControlled)?1:0).append("</IsControlled>");
			sb.append("<DateTStamp>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateTStamp)).append("</DateTStamp>");
			sb.append("<SendStatus>").append(SendStatus.ordinal()).append("</SendStatus>");
			sb.append("<RxCui>").append(RxCui).append("</RxCui>");
			sb.append("<DosageCode>").append(Serializing.escapeForXml(DosageCode)).append("</DosageCode>");
			sb.append("<NewCropGuid>").append(Serializing.escapeForXml(NewCropGuid)).append("</NewCropGuid>");
			sb.append("</RxPat>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"RxNum")!=null) {
					RxNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"RxNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"PatNum")!=null) {
					PatNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"PatNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"RxDate")!=null) {
					RxDate=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"RxDate"));
				}
				if(Serializing.getXmlNodeValue(doc,"Drug")!=null) {
					Drug=Serializing.getXmlNodeValue(doc,"Drug");
				}
				if(Serializing.getXmlNodeValue(doc,"Sig")!=null) {
					Sig=Serializing.getXmlNodeValue(doc,"Sig");
				}
				if(Serializing.getXmlNodeValue(doc,"Disp")!=null) {
					Disp=Serializing.getXmlNodeValue(doc,"Disp");
				}
				if(Serializing.getXmlNodeValue(doc,"Refills")!=null) {
					Refills=Serializing.getXmlNodeValue(doc,"Refills");
				}
				if(Serializing.getXmlNodeValue(doc,"ProvNum")!=null) {
					ProvNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ProvNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"Notes")!=null) {
					Notes=Serializing.getXmlNodeValue(doc,"Notes");
				}
				if(Serializing.getXmlNodeValue(doc,"PharmacyNum")!=null) {
					PharmacyNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"PharmacyNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"IsControlled")!=null) {
					IsControlled=(Serializing.getXmlNodeValue(doc,"IsControlled")=="0")?false:true;
				}
				if(Serializing.getXmlNodeValue(doc,"DateTStamp")!=null) {
					DateTStamp=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"DateTStamp"));
				}
				if(Serializing.getXmlNodeValue(doc,"SendStatus")!=null) {
					SendStatus=RxSendStatus.values()[Integer.valueOf(Serializing.getXmlNodeValue(doc,"SendStatus"))];
				}
				if(Serializing.getXmlNodeValue(doc,"RxCui")!=null) {
					RxCui=Integer.valueOf(Serializing.getXmlNodeValue(doc,"RxCui"));
				}
				if(Serializing.getXmlNodeValue(doc,"DosageCode")!=null) {
					DosageCode=Serializing.getXmlNodeValue(doc,"DosageCode");
				}
				if(Serializing.getXmlNodeValue(doc,"NewCropGuid")!=null) {
					NewCropGuid=Serializing.getXmlNodeValue(doc,"NewCropGuid");
				}
			}
			catch(Exception e) {
				throw new Exception("Error deserializing RxPat: "+e.getMessage());
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
