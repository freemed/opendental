package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

//DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD.
public class Etrans {
		/** Primary key. */
		public int EtransNum;
		/** The date and time of the transaction. */
		public Date DateTimeTrans;
		/** FK to clearinghouse.ClearinghouseNum .  Can be 0 if no clearinghouse was involved. */
		public int ClearingHouseNum;
		/** Enum:EtransType */
		public EtransType Etype;
		/** FK to claim.ClaimNum if a claim. Otherwise 0.  Warning.  Original claim might have been deleted.  But if Canadian claim was successfully sent, then deletion will be blocked. */
		public int ClaimNum;
		/** For Canada.  Unique for every transaction sent.  Increments by one until 999999, then resets to 1. */
		public int OfficeSequenceNumber;
		/** For Canada.  Separate counter for each carrier.  Increments by one until 99999, then resets to 1. */
		public int CarrierTransCounter;
		/** For Canada.  If this claim includes secondary, then this is the counter for the secondary carrier. */
		public int CarrierTransCounter2;
		/** FK to carrier.CarrierNum. */
		public int CarrierNum;
		/** FK to carrier.CarrierNum Only used if secondary insurance info is provided on a claim.  Necessary for Canada. */
		public int CarrierNum2;
		/** FK to patient.PatNum This is useful in case the original claim has been deleted.  Now, we can still tell who the patient was. */
		public int PatNum;
		/** Maxes out at 999, then loops back to 1.  This is not a good key, but is a restriction of (canadian?).  So dates must also be used to isolate the correct BatchNumber key.  Specific to one clearinghouse.  Only used with e-claims.  Claim will have BatchNumber, and 997 will have matching BatchNumber. (In X12 lingo, it's a functional group number) */
		public int BatchNumber;
		/** A=Accepted, R=Rejected, blank if not able to parse.  More options will be added later.  The incoming 997 or 999 sets this flag automatically.  To find the 997 or 999, look for a matching BatchNumber with a similar date, since both the claims and the 997 or 999 will both have the same batch number.  The 997 or 999 does not have this flag set on itself. */
		public String AckCode;
		/** For sent e-claims, within each batch (functional group), each carrier gets it's own transaction set.  Since 997s and 999s acknowledge transaction sets rather than batches, we need to keep track of which transaction set each claim is part of as well as which batch it's part of.  This field can't be set as part of 997 or 999, because one 997 or 999 refers to multiple trans sets. */
		public int TransSetNum;
		/** Typical uses include indicating that the report was printed, the claim was resent, reason for rejection, etc.  For a 270, this contains the automatically generated short summary of the response.  The response could include the reason for failure, or it could be a short summary of the 271. */
		public String Note;
		/** FK to etransmessagetext.EtransMessageTextNum.  Can be 0 if there is no message text.  Multiple Etrans objects can refer to the same message text, very common in a batch. */
		public int EtransMessageTextNum;
		/** FK to etrans.EtransNum.  Only has a non-zero value if there exists an ack etrans, like a 997, 999, 277ack, 271, 835, or ackError.  There can be only one ack for any given etrans, but one ack can apply to multiple etran's that were sent as one batch. */
		public int AckEtransNum;
		/** FK to insplan.PlanNum.  Used if EtransType.BenefitInquiry270 and BenefitResponse271 and Eligibility_CA. */
		public int PlanNum;
		/** FK to inssub.InsSubNum.  Used if EtransType.BenefitInquiry270 and BenefitResponse271 and Eligibility_CA. */
		public int InsSubNum;

		/** Deep copy of object. */
		public Etrans deepCopy() {
			Etrans etrans=new Etrans();
			etrans.EtransNum=this.EtransNum;
			etrans.DateTimeTrans=this.DateTimeTrans;
			etrans.ClearingHouseNum=this.ClearingHouseNum;
			etrans.Etype=this.Etype;
			etrans.ClaimNum=this.ClaimNum;
			etrans.OfficeSequenceNumber=this.OfficeSequenceNumber;
			etrans.CarrierTransCounter=this.CarrierTransCounter;
			etrans.CarrierTransCounter2=this.CarrierTransCounter2;
			etrans.CarrierNum=this.CarrierNum;
			etrans.CarrierNum2=this.CarrierNum2;
			etrans.PatNum=this.PatNum;
			etrans.BatchNumber=this.BatchNumber;
			etrans.AckCode=this.AckCode;
			etrans.TransSetNum=this.TransSetNum;
			etrans.Note=this.Note;
			etrans.EtransMessageTextNum=this.EtransMessageTextNum;
			etrans.AckEtransNum=this.AckEtransNum;
			etrans.PlanNum=this.PlanNum;
			etrans.InsSubNum=this.InsSubNum;
			return etrans;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<Etrans>");
			sb.append("<EtransNum>").append(EtransNum).append("</EtransNum>");
			sb.append("<DateTimeTrans>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateTimeTrans)).append("</DateTimeTrans>");
			sb.append("<ClearingHouseNum>").append(ClearingHouseNum).append("</ClearingHouseNum>");
			sb.append("<Etype>").append(Etype.ordinal()).append("</Etype>");
			sb.append("<ClaimNum>").append(ClaimNum).append("</ClaimNum>");
			sb.append("<OfficeSequenceNumber>").append(OfficeSequenceNumber).append("</OfficeSequenceNumber>");
			sb.append("<CarrierTransCounter>").append(CarrierTransCounter).append("</CarrierTransCounter>");
			sb.append("<CarrierTransCounter2>").append(CarrierTransCounter2).append("</CarrierTransCounter2>");
			sb.append("<CarrierNum>").append(CarrierNum).append("</CarrierNum>");
			sb.append("<CarrierNum2>").append(CarrierNum2).append("</CarrierNum2>");
			sb.append("<PatNum>").append(PatNum).append("</PatNum>");
			sb.append("<BatchNumber>").append(BatchNumber).append("</BatchNumber>");
			sb.append("<AckCode>").append(Serializing.escapeForXml(AckCode)).append("</AckCode>");
			sb.append("<TransSetNum>").append(TransSetNum).append("</TransSetNum>");
			sb.append("<Note>").append(Serializing.escapeForXml(Note)).append("</Note>");
			sb.append("<EtransMessageTextNum>").append(EtransMessageTextNum).append("</EtransMessageTextNum>");
			sb.append("<AckEtransNum>").append(AckEtransNum).append("</AckEtransNum>");
			sb.append("<PlanNum>").append(PlanNum).append("</PlanNum>");
			sb.append("<InsSubNum>").append(InsSubNum).append("</InsSubNum>");
			sb.append("</Etrans>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"EtransNum")!=null) {
					EtransNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"EtransNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"DateTimeTrans")!=null) {
					DateTimeTrans=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"DateTimeTrans"));
				}
				if(Serializing.getXmlNodeValue(doc,"ClearingHouseNum")!=null) {
					ClearingHouseNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ClearingHouseNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"Etype")!=null) {
					Etype=EtransType.valueOf(Serializing.getXmlNodeValue(doc,"Etype"));
				}
				if(Serializing.getXmlNodeValue(doc,"ClaimNum")!=null) {
					ClaimNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ClaimNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"OfficeSequenceNumber")!=null) {
					OfficeSequenceNumber=Integer.valueOf(Serializing.getXmlNodeValue(doc,"OfficeSequenceNumber"));
				}
				if(Serializing.getXmlNodeValue(doc,"CarrierTransCounter")!=null) {
					CarrierTransCounter=Integer.valueOf(Serializing.getXmlNodeValue(doc,"CarrierTransCounter"));
				}
				if(Serializing.getXmlNodeValue(doc,"CarrierTransCounter2")!=null) {
					CarrierTransCounter2=Integer.valueOf(Serializing.getXmlNodeValue(doc,"CarrierTransCounter2"));
				}
				if(Serializing.getXmlNodeValue(doc,"CarrierNum")!=null) {
					CarrierNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"CarrierNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"CarrierNum2")!=null) {
					CarrierNum2=Integer.valueOf(Serializing.getXmlNodeValue(doc,"CarrierNum2"));
				}
				if(Serializing.getXmlNodeValue(doc,"PatNum")!=null) {
					PatNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"PatNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"BatchNumber")!=null) {
					BatchNumber=Integer.valueOf(Serializing.getXmlNodeValue(doc,"BatchNumber"));
				}
				if(Serializing.getXmlNodeValue(doc,"AckCode")!=null) {
					AckCode=Serializing.getXmlNodeValue(doc,"AckCode");
				}
				if(Serializing.getXmlNodeValue(doc,"TransSetNum")!=null) {
					TransSetNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"TransSetNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"Note")!=null) {
					Note=Serializing.getXmlNodeValue(doc,"Note");
				}
				if(Serializing.getXmlNodeValue(doc,"EtransMessageTextNum")!=null) {
					EtransMessageTextNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"EtransMessageTextNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"AckEtransNum")!=null) {
					AckEtransNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"AckEtransNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"PlanNum")!=null) {
					PlanNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"PlanNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"InsSubNum")!=null) {
					InsSubNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"InsSubNum"));
				}
			}
			catch(Exception e) {
				throw new Exception("Error deserializing Etrans: "+e.getMessage());
			}
		}

		/** The _CA of some types should get stripped off when displaying to users. */
		public enum EtransType {
			/** 0 X12-837.  Should we differenitate between different kinds of 837s and 4010 vs 5010? */
			ClaimSent,
			/** 1 claim */
			ClaimPrinted,
			/** 2 Canada. Type 01 */
			Claim_CA,
			/** 3 Renaissance */
			Claim_Ren,
			/** 4 Canada. Type 11 */
			ClaimAck_CA,
			/** 5 Canada. Type 21 */
			ClaimEOB_CA,
			/** 6 Canada. Type 08 */
			Eligibility_CA,
			/** 7 Canada. Type 18. V02 type 10. */
			EligResponse_CA,
			/** 8 Canada. Type 02 */
			ClaimReversal_CA,
			/** 9 Canada. Type 03 */
			Predeterm_CA,
			/** 10 Canada. Type 04 */
			RequestOutstand_CA,
			/** 11 Canada. Type 05 */
			RequestSumm_CA,
			/** 12 Canada. Type 06 */
			RequestPay_CA,
			/** 13 Canada. Type 07 */
			ClaimCOB_CA,
			/** 14 Canada. Type 12 */
			ReverseResponse_CA,
			/** 15 Canada. Type 13 */
			PredetermAck_CA,
			/** 16 Canada. Type 23 */
			PredetermEOB_CA,
			/** 17 Canada. Type 14 */
			OutstandingAck_CA,
			/** 18 Canada. Type 24 */
			EmailResponse_CA,
			/** 19 Canada. Type 16 */
			PaymentResponse_CA,
			/** 20 Canada. Type 15 */
			SummaryResponse_CA,
			/** 21 Ack from clearinghouse. X12-997. */
			Acknowledge_997,
			/** 22 X12-277. Unsolicited claim status notification. */
			StatusNotify_277,
			/** 23 Text report from clearinghouse in human readable format. */
			TextReport,
			/** 24 X12-270. */
			BenefitInquiry270,
			/** 25 X12-271 */
			BenefitResponse271,
			/** 26 When a Canadian message is sent, and an error comes back instead of a message.  This stores information about the error.  The etrans with this type is attached it to the original etrans as an ack. */
			AckError,
			/** 27 X12-835. Not used yet. */
			ERA_835,
			/** 28 Ack from clearinghouse. X12-999. */
			Acknowledge_999
		}


}
