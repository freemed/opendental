package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

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
		public String RefDate;
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
		public String DateProcComplete;

		/** Deep copy of object. */
		public RefAttach Copy() {
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
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<RefAttach>");
			sb.append("<RefAttachNum>").append(RefAttachNum).append("</RefAttachNum>");
			sb.append("<ReferralNum>").append(ReferralNum).append("</ReferralNum>");
			sb.append("<PatNum>").append(PatNum).append("</PatNum>");
			sb.append("<ItemOrder>").append(ItemOrder).append("</ItemOrder>");
			sb.append("<RefDate>").append(Serializing.EscapeForXml(RefDate)).append("</RefDate>");
			sb.append("<IsFrom>").append((IsFrom)?1:0).append("</IsFrom>");
			sb.append("<RefToStatus>").append(RefToStatus.ordinal()).append("</RefToStatus>");
			sb.append("<Note>").append(Serializing.EscapeForXml(Note)).append("</Note>");
			sb.append("<IsTransitionOfCare>").append((IsTransitionOfCare)?1:0).append("</IsTransitionOfCare>");
			sb.append("<ProcNum>").append(ProcNum).append("</ProcNum>");
			sb.append("<DateProcComplete>").append(Serializing.EscapeForXml(DateProcComplete)).append("</DateProcComplete>");
			sb.append("</RefAttach>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				RefAttachNum=Integer.valueOf(doc.getElementsByTagName("RefAttachNum").item(0).getFirstChild().getNodeValue());
				ReferralNum=Integer.valueOf(doc.getElementsByTagName("ReferralNum").item(0).getFirstChild().getNodeValue());
				PatNum=Integer.valueOf(doc.getElementsByTagName("PatNum").item(0).getFirstChild().getNodeValue());
				ItemOrder=Integer.valueOf(doc.getElementsByTagName("ItemOrder").item(0).getFirstChild().getNodeValue());
				RefDate=doc.getElementsByTagName("RefDate").item(0).getFirstChild().getNodeValue();
				IsFrom=(doc.getElementsByTagName("IsFrom").item(0).getFirstChild().getNodeValue()=="0")?false:true;
				RefToStatus=ReferralToStatus.values()[Integer.valueOf(doc.getElementsByTagName("RefToStatus").item(0).getFirstChild().getNodeValue())];
				Note=doc.getElementsByTagName("Note").item(0).getFirstChild().getNodeValue();
				IsTransitionOfCare=(doc.getElementsByTagName("IsTransitionOfCare").item(0).getFirstChild().getNodeValue()=="0")?false:true;
				ProcNum=Integer.valueOf(doc.getElementsByTagName("ProcNum").item(0).getFirstChild().getNodeValue());
				DateProcComplete=doc.getElementsByTagName("DateProcComplete").item(0).getFirstChild().getNodeValue();
			}
			catch(Exception e) {
				throw e;
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
