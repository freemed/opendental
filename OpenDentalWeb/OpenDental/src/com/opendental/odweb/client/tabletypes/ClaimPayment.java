package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

public class ClaimPayment {
		/** Primary key. */
		public int ClaimPaymentNum;
		/** Date the check was entered into this system, not the date on the check. */
		public String CheckDate;
		/** The amount of the check. */
		public double CheckAmt;
		/** The check number. */
		public String CheckNum;
		/** Bank and branch. */
		public String BankBranch;
		/** Note for this check if needed. */
		public String Note;
		/** FK to clinic.ClinicNum.  0 if no clinic. */
		public int ClinicNum;
		/** FK to deposit.DepositNum.  0 if not attached to any deposits. */
		public int DepositNum;
		/** Descriptive name of the carrier just for reporting purposes.  We use this because the CarrierNums could conceivably be different for the different claimprocs attached. */
		public String CarrierName;
		/** Date that the carrier issued the check. Date on the check. */
		public String DateIssued;
		/** . */
		public boolean IsPartial;

		/** Deep copy of object. */
		public ClaimPayment Copy() {
			ClaimPayment claimpayment=new ClaimPayment();
			claimpayment.ClaimPaymentNum=this.ClaimPaymentNum;
			claimpayment.CheckDate=this.CheckDate;
			claimpayment.CheckAmt=this.CheckAmt;
			claimpayment.CheckNum=this.CheckNum;
			claimpayment.BankBranch=this.BankBranch;
			claimpayment.Note=this.Note;
			claimpayment.ClinicNum=this.ClinicNum;
			claimpayment.DepositNum=this.DepositNum;
			claimpayment.CarrierName=this.CarrierName;
			claimpayment.DateIssued=this.DateIssued;
			claimpayment.IsPartial=this.IsPartial;
			return claimpayment;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<ClaimPayment>");
			sb.append("<ClaimPaymentNum>").append(ClaimPaymentNum).append("</ClaimPaymentNum>");
			sb.append("<CheckDate>").append(Serializing.EscapeForXml(CheckDate)).append("</CheckDate>");
			sb.append("<CheckAmt>").append(CheckAmt).append("</CheckAmt>");
			sb.append("<CheckNum>").append(Serializing.EscapeForXml(CheckNum)).append("</CheckNum>");
			sb.append("<BankBranch>").append(Serializing.EscapeForXml(BankBranch)).append("</BankBranch>");
			sb.append("<Note>").append(Serializing.EscapeForXml(Note)).append("</Note>");
			sb.append("<ClinicNum>").append(ClinicNum).append("</ClinicNum>");
			sb.append("<DepositNum>").append(DepositNum).append("</DepositNum>");
			sb.append("<CarrierName>").append(Serializing.EscapeForXml(CarrierName)).append("</CarrierName>");
			sb.append("<DateIssued>").append(Serializing.EscapeForXml(DateIssued)).append("</DateIssued>");
			sb.append("<IsPartial>").append((IsPartial)?1:0).append("</IsPartial>");
			sb.append("</ClaimPayment>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				ClaimPaymentNum=Integer.valueOf(doc.getElementsByTagName("ClaimPaymentNum").item(0).getFirstChild().getNodeValue());
				CheckDate=doc.getElementsByTagName("CheckDate").item(0).getFirstChild().getNodeValue();
				CheckAmt=Double.valueOf(doc.getElementsByTagName("CheckAmt").item(0).getFirstChild().getNodeValue());
				CheckNum=doc.getElementsByTagName("CheckNum").item(0).getFirstChild().getNodeValue();
				BankBranch=doc.getElementsByTagName("BankBranch").item(0).getFirstChild().getNodeValue();
				Note=doc.getElementsByTagName("Note").item(0).getFirstChild().getNodeValue();
				ClinicNum=Integer.valueOf(doc.getElementsByTagName("ClinicNum").item(0).getFirstChild().getNodeValue());
				DepositNum=Integer.valueOf(doc.getElementsByTagName("DepositNum").item(0).getFirstChild().getNodeValue());
				CarrierName=doc.getElementsByTagName("CarrierName").item(0).getFirstChild().getNodeValue();
				DateIssued=doc.getElementsByTagName("DateIssued").item(0).getFirstChild().getNodeValue();
				IsPartial=(doc.getElementsByTagName("IsPartial").item(0).getFirstChild().getNodeValue()=="0")?false:true;
			}
			catch(Exception e) {
				throw e;
			}
		}


}
