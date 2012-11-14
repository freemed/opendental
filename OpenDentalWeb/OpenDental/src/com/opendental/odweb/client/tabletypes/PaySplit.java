package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

public class PaySplit {
		/** Primary key. */
		public int SplitNum;
		/** Amount of split. */
		public double SplitAmt;
		/** FK to patient.PatNum. */
		public int PatNum;
		/** Procedure date.  Typically only used if tied to a procedure.  In older versions (before 7.0), this was the date that showed on the account.  Frequently the same as the date of the payment, but not necessarily.  Not when the payment was made.  This is what the aging will be based on in a future version. */
		public String ProcDate;
		/** FK to payment.PayNum.  Every paysplit must be linked to a payment. */
		public int PayNum;
		/** No longer used. */
		public boolean IsDiscount;
		/** No longer used */
		public byte DiscountType;
		/** FK to provider.ProvNum. */
		public int ProvNum;
		/** FK to payplan.PayPlanNum.  0 if not attached to a payplan. */
		public int PayPlanNum;
		/** Date always in perfect synch with Payment date. */
		public String DatePay;
		/** FK to procedurelog.ProcNum.  0 if not attached to a procedure. */
		public int ProcNum;
		/** Date this paysplit was created.  User not allowed to edit. */
		public String DateEntry;
		/** FK to definition.DefNum.  Usually 0 unless this is a special unearned split. */
		public int UnearnedType;
		/** FK to clinic.ClinicNum.  Can be 0.  Need not match the ClinicNum of the Payment, because a payment can be split between clinics. */
		public int ClinicNum;

		/** Deep copy of object. */
		public PaySplit Copy() {
			PaySplit paysplit=new PaySplit();
			paysplit.SplitNum=this.SplitNum;
			paysplit.SplitAmt=this.SplitAmt;
			paysplit.PatNum=this.PatNum;
			paysplit.ProcDate=this.ProcDate;
			paysplit.PayNum=this.PayNum;
			paysplit.IsDiscount=this.IsDiscount;
			paysplit.DiscountType=this.DiscountType;
			paysplit.ProvNum=this.ProvNum;
			paysplit.PayPlanNum=this.PayPlanNum;
			paysplit.DatePay=this.DatePay;
			paysplit.ProcNum=this.ProcNum;
			paysplit.DateEntry=this.DateEntry;
			paysplit.UnearnedType=this.UnearnedType;
			paysplit.ClinicNum=this.ClinicNum;
			return paysplit;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<PaySplit>");
			sb.append("<SplitNum>").append(SplitNum).append("</SplitNum>");
			sb.append("<SplitAmt>").append(SplitAmt).append("</SplitAmt>");
			sb.append("<PatNum>").append(PatNum).append("</PatNum>");
			sb.append("<ProcDate>").append(Serializing.EscapeForXml(ProcDate)).append("</ProcDate>");
			sb.append("<PayNum>").append(PayNum).append("</PayNum>");
			sb.append("<IsDiscount>").append((IsDiscount)?1:0).append("</IsDiscount>");
			sb.append("<DiscountType>").append(DiscountType).append("</DiscountType>");
			sb.append("<ProvNum>").append(ProvNum).append("</ProvNum>");
			sb.append("<PayPlanNum>").append(PayPlanNum).append("</PayPlanNum>");
			sb.append("<DatePay>").append(Serializing.EscapeForXml(DatePay)).append("</DatePay>");
			sb.append("<ProcNum>").append(ProcNum).append("</ProcNum>");
			sb.append("<DateEntry>").append(Serializing.EscapeForXml(DateEntry)).append("</DateEntry>");
			sb.append("<UnearnedType>").append(UnearnedType).append("</UnearnedType>");
			sb.append("<ClinicNum>").append(ClinicNum).append("</ClinicNum>");
			sb.append("</PaySplit>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				SplitNum=Integer.valueOf(doc.getElementsByTagName("SplitNum").item(0).getFirstChild().getNodeValue());
				SplitAmt=Double.valueOf(doc.getElementsByTagName("SplitAmt").item(0).getFirstChild().getNodeValue());
				PatNum=Integer.valueOf(doc.getElementsByTagName("PatNum").item(0).getFirstChild().getNodeValue());
				ProcDate=doc.getElementsByTagName("ProcDate").item(0).getFirstChild().getNodeValue();
				PayNum=Integer.valueOf(doc.getElementsByTagName("PayNum").item(0).getFirstChild().getNodeValue());
				IsDiscount=(doc.getElementsByTagName("IsDiscount").item(0).getFirstChild().getNodeValue()=="0")?false:true;
				DiscountType=Byte.valueOf(doc.getElementsByTagName("DiscountType").item(0).getFirstChild().getNodeValue());
				ProvNum=Integer.valueOf(doc.getElementsByTagName("ProvNum").item(0).getFirstChild().getNodeValue());
				PayPlanNum=Integer.valueOf(doc.getElementsByTagName("PayPlanNum").item(0).getFirstChild().getNodeValue());
				DatePay=doc.getElementsByTagName("DatePay").item(0).getFirstChild().getNodeValue();
				ProcNum=Integer.valueOf(doc.getElementsByTagName("ProcNum").item(0).getFirstChild().getNodeValue());
				DateEntry=doc.getElementsByTagName("DateEntry").item(0).getFirstChild().getNodeValue();
				UnearnedType=Integer.valueOf(doc.getElementsByTagName("UnearnedType").item(0).getFirstChild().getNodeValue());
				ClinicNum=Integer.valueOf(doc.getElementsByTagName("ClinicNum").item(0).getFirstChild().getNodeValue());
			}
			catch(Exception e) {
				throw e;
			}
		}


}
