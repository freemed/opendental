package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

//DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD.
public class RefAttach {
		/** Primary key. */
		public int RefAttachNum;
		/** FK to referral.ReferralNum. */
		public int ReferralNum;
		/** FK to patient.PatNum. */
		public int PatNum;
		/** Order to display in patient info. One-based.  Will be automated more in future. */
		public int ItemOrder;
		/** Date of referral. */
		public Date RefDate;
		/** true=from, false=to */
		public boolean IsFrom;
		/** Enum:ReferralToStatus 0=None,1=Declined,2=Scheduled,3=Consulted,4=InTreatment,5=Complete. */
		public ReferralToStatus RefToStatus;
		/** Why the patient was referred out, or less commonly, the circumstances of the referral source. */
		public String Note;
		/** Used to track ehr events.  All outgoing referrals default to true.  The incoming ones get a popup asking if it's a transition of care. */
		public boolean IsTransitionOfCare;
		/** FK to procedurelog.ProcNum */
		public int ProcNum;
		/** . */
		public Date DateProcComplete;

		/** Deep copy of object. */
		public RefAttach deepCopy() {
			RefAttach refattach=new RefAttach();
			refattach.RefAttachNum=this.RefAttachNum;
			refattach.ReferralNum=this.ReferralNum;
			refattach.PatNum=this.PatNum;
			refattach.ItemOrder=this.ItemOrder;
			refattach.RefDate=this.RefDate;
			refattach.IsFrom=this.IsFrom;
			refattach.RefToStatus=this.RefToStatus;
			refattach.Note=this.Note;
			refattach.IsTransitionOfCare=this.IsTransitionOfCare;
			refattach.ProcNum=this.ProcNum;
			refattach.DateProcComplete=this.DateProcComplete;
			return refattach;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<RefAttach>");
			sb.append("<RefAttachNum>").append(RefAttachNum).append("</RefAttachNum>");
			sb.append("<ReferralNum>").append(ReferralNum).append("</ReferralNum>");
			sb.append("<PatNum>").append(PatNum).append("</PatNum>");
			sb.append("<ItemOrder>").append(ItemOrder).append("</ItemOrder>");
			sb.append("<RefDate>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(RefDate)).append("</RefDate>");
			sb.append("<IsFrom>").append((IsFrom)?1:0).append("</IsFrom>");
			sb.append("<RefToStatus>").append(RefToStatus.ordinal()).append("</RefToStatus>");
			sb.append("<Note>").append(Serializing.escapeForXml(Note)).append("</Note>");
			sb.append("<IsTransitionOfCare>").append((IsTransitionOfCare)?1:0).append("</IsTransitionOfCare>");
			sb.append("<ProcNum>").append(ProcNum).append("</ProcNum>");
			sb.append("<DateProcComplete>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateProcComplete)).append("</DateProcComplete>");
			sb.append("</RefAttach>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"RefAttachNum")!=null) {
					RefAttachNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"RefAttachNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"ReferralNum")!=null) {
					ReferralNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ReferralNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"PatNum")!=null) {
					PatNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"PatNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"ItemOrder")!=null) {
					ItemOrder=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ItemOrder"));
				}
				if(Serializing.getXmlNodeValue(doc,"RefDate")!=null) {
					RefDate=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"RefDate"));
				}
				if(Serializing.getXmlNodeValue(doc,"IsFrom")!=null) {
					IsFrom=(Serializing.getXmlNodeValue(doc,"IsFrom")=="0")?false:true;
				}
				if(Serializing.getXmlNodeValue(doc,"RefToStatus")!=null) {
					RefToStatus=ReferralToStatus.values()[Integer.valueOf(Serializing.getXmlNodeValue(doc,"RefToStatus"))];
				}
				if(Serializing.getXmlNodeValue(doc,"Note")!=null) {
					Note=Serializing.getXmlNodeValue(doc,"Note");
				}
				if(Serializing.getXmlNodeValue(doc,"IsTransitionOfCare")!=null) {
					IsTransitionOfCare=(Serializing.getXmlNodeValue(doc,"IsTransitionOfCare")=="0")?false:true;
				}
				if(Serializing.getXmlNodeValue(doc,"ProcNum")!=null) {
					ProcNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ProcNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"DateProcComplete")!=null) {
					DateProcComplete=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"DateProcComplete"));
				}
			}
			catch(Exception e) {
				throw new Exception("Error deserializing RefAttach: "+e.getMessage());
			}
		}

		/** 0=None,1=Declined,2=Scheduled,3=Consulted,4=InTreatment,5=Complete */
		public enum ReferralToStatus {
			/** 0 */
			None,
			/** 1 */
			Declined,
			/** 2 */
			Scheduled,
			/** 3 */
			Consulted,
			/** 4 */
			InTreatment,
			/** 5 */
			Complete
		}


}
