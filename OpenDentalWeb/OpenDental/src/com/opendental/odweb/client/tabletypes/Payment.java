package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

public class Payment {
		/** Primary key. */
		public int PayNum;
		/** FK to definition.DefNum.  This will be 0 if this is an income transfer to another provider. */
		public int PayType;
		/** The date that the payment displays on the patient account. */
		public String PayDate;
		/** Amount of the payment.  Must equal the sum of the splits. */
		public double PayAmt;
		/** Check number is optional. */
		public String CheckNum;
		/** Bank-branch for checks. */
		public String BankBranch;
		/** Any admin note.  Not for patient to see. */
		public String PayNote;
		/** Set to true to indicate that a payment is split.  Just makes a few functions easier.  Might be eliminated. */
		public boolean IsSplit;
		/** FK to patient.PatNum.  The patient where the payment entry will show.  But only the splits affect account balances.  This has a value even if the 'payment' is actually an income transfer to another provider. */
		public int PatNum;
		/** FK to clinic.ClinicNum.  Can be 0. Copied from patient.ClinicNum when creating payment, but user can override.  Not used in provider income transfers.  Cannot be used in financial reporting when grouping by clinic, because payments may be split between clinics. */
		public int ClinicNum;
		/** The date that this payment was entered.  Not user editable. */
		public String DateEntry;
		/** FK to deposit.DepositNum.  0 if not attached to any deposits.  Cash does not usually get attached to a deposit; only checks. */
		public int DepositNum;
		/** Text of printed receipt if the payment was done electronically. Allows reprinting if needed. Only used for PayConnect at the moment, but plans to use for XCharge as well. */
		public String Receipt;
		/** True if this was an automatically added recurring CC charge rather then one entered by the user.  This was set to true for all historical entries before version 11.1, but will be accurate after that. */
		public boolean IsRecurringCC;

		/** Deep copy of object. */
		public Payment Copy() {
			Payment payment=new Payment();
			payment.PayNum=this.PayNum;
			payment.PayType=this.PayType;
			payment.PayDate=this.PayDate;
			payment.PayAmt=this.PayAmt;
			payment.CheckNum=this.CheckNum;
			payment.BankBranch=this.BankBranch;
			payment.PayNote=this.PayNote;
			payment.IsSplit=this.IsSplit;
			payment.PatNum=this.PatNum;
			payment.ClinicNum=this.ClinicNum;
			payment.DateEntry=this.DateEntry;
			payment.DepositNum=this.DepositNum;
			payment.Receipt=this.Receipt;
			payment.IsRecurringCC=this.IsRecurringCC;
			return payment;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<Payment>");
			sb.append("<PayNum>").append(PayNum).append("</PayNum>");
			sb.append("<PayType>").append(PayType).append("</PayType>");
			sb.append("<PayDate>").append(Serializing.EscapeForXml(PayDate)).append("</PayDate>");
			sb.append("<PayAmt>").append(PayAmt).append("</PayAmt>");
			sb.append("<CheckNum>").append(Serializing.EscapeForXml(CheckNum)).append("</CheckNum>");
			sb.append("<BankBranch>").append(Serializing.EscapeForXml(BankBranch)).append("</BankBranch>");
			sb.append("<PayNote>").append(Serializing.EscapeForXml(PayNote)).append("</PayNote>");
			sb.append("<IsSplit>").append((IsSplit)?1:0).append("</IsSplit>");
			sb.append("<PatNum>").append(PatNum).append("</PatNum>");
			sb.append("<ClinicNum>").append(ClinicNum).append("</ClinicNum>");
			sb.append("<DateEntry>").append(Serializing.EscapeForXml(DateEntry)).append("</DateEntry>");
			sb.append("<DepositNum>").append(DepositNum).append("</DepositNum>");
			sb.append("<Receipt>").append(Serializing.EscapeForXml(Receipt)).append("</Receipt>");
			sb.append("<IsRecurringCC>").append((IsRecurringCC)?1:0).append("</IsRecurringCC>");
			sb.append("</Payment>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				PayNum=Integer.valueOf(doc.getElementsByTagName("PayNum").item(0).getFirstChild().getNodeValue());
				PayType=Integer.valueOf(doc.getElementsByTagName("PayType").item(0).getFirstChild().getNodeValue());
				PayDate=doc.getElementsByTagName("PayDate").item(0).getFirstChild().getNodeValue();
				PayAmt=Double.valueOf(doc.getElementsByTagName("PayAmt").item(0).getFirstChild().getNodeValue());
				CheckNum=doc.getElementsByTagName("CheckNum").item(0).getFirstChild().getNodeValue();
				BankBranch=doc.getElementsByTagName("BankBranch").item(0).getFirstChild().getNodeValue();
				PayNote=doc.getElementsByTagName("PayNote").item(0).getFirstChild().getNodeValue();
				IsSplit=(doc.getElementsByTagName("IsSplit").item(0).getFirstChild().getNodeValue()=="0")?false:true;
				PatNum=Integer.valueOf(doc.getElementsByTagName("PatNum").item(0).getFirstChild().getNodeValue());
				ClinicNum=Integer.valueOf(doc.getElementsByTagName("ClinicNum").item(0).getFirstChild().getNodeValue());
				DateEntry=doc.getElementsByTagName("DateEntry").item(0).getFirstChild().getNodeValue();
				DepositNum=Integer.valueOf(doc.getElementsByTagName("DepositNum").item(0).getFirstChild().getNodeValue());
				Receipt=doc.getElementsByTagName("Receipt").item(0).getFirstChild().getNodeValue();
				IsRecurringCC=(doc.getElementsByTagName("IsRecurringCC").item(0).getFirstChild().getNodeValue()=="0")?false:true;
			}
			catch(Exception e) {
				throw e;
			}
		}


}
