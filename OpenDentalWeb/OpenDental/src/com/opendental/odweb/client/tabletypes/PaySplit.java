package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

public class PaySplit {
		/** Primary key. */
		public int SplitNum;
		/** Amount of split. */
		public double SplitAmt;
		/** FK to patient.PatNum. */
		public int PatNum;
		/** Procedure date.  Typically only used if tied to a procedure.  In older versions (before 7.0), this was the date that showed on the account.  Frequently the same as the date of the payment, but not necessarily.  Not when the payment was made.  This is what the aging will be based on in a future version. */
		public Date ProcDate;
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
		public Date DatePay;
		/** FK to procedurelog.ProcNum.  0 if not attached to a procedure. */
		public int ProcNum;
		/** Date this paysplit was created.  User not allowed to edit. */
		public Date DateEntry;
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
			sb.append("<ProcDate>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(ProcDate)).append("</ProcDate>");
			sb.append("<PayNum>").append(PayNum).append("</PayNum>");
			sb.append("<IsDiscount>").append((IsDiscount)?1:0).append("</IsDiscount>");
			sb.append("<DiscountType>").append(DiscountType).append("</DiscountType>");
			sb.append("<ProvNum>").append(ProvNum).append("</ProvNum>");
			sb.append("<PayPlanNum>").append(PayPlanNum).append("</PayPlanNum>");
			sb.append("<DatePay>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DatePay)).append("</DatePay>");
			sb.append("<ProcNum>").append(ProcNum).append("</ProcNum>");
			sb.append("<DateEntry>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateEntry)).append("</DateEntry>");
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
				if(Serializing.GetXmlNodeValue(doc,"SplitNum")!=null) {
					SplitNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"SplitNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"SplitAmt")!=null) {
					SplitAmt=Double.valueOf(Serializing.GetXmlNodeValue(doc,"SplitAmt"));
				}
				if(Serializing.GetXmlNodeValue(doc,"PatNum")!=null) {
					PatNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"PatNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"ProcDate")!=null) {
					ProcDate=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.GetXmlNodeValue(doc,"ProcDate"));
				}
				if(Serializing.GetXmlNodeValue(doc,"PayNum")!=null) {
					PayNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"PayNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"IsDiscount")!=null) {
					IsDiscount=(Serializing.GetXmlNodeValue(doc,"IsDiscount")=="0")?false:true;
				}
				if(Serializing.GetXmlNodeValue(doc,"DiscountType")!=null) {
					DiscountType=Byte.valueOf(Serializing.GetXmlNodeValue(doc,"DiscountType"));
				}
				if(Serializing.GetXmlNodeValue(doc,"ProvNum")!=null) {
					ProvNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"ProvNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"PayPlanNum")!=null) {
					PayPlanNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"PayPlanNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"DatePay")!=null) {
					DatePay=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.GetXmlNodeValue(doc,"DatePay"));
				}
				if(Serializing.GetXmlNodeValue(doc,"ProcNum")!=null) {
					ProcNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"ProcNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"DateEntry")!=null) {
					DateEntry=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.GetXmlNodeValue(doc,"DateEntry"));
				}
				if(Serializing.GetXmlNodeValue(doc,"UnearnedType")!=null) {
					UnearnedType=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"UnearnedType"));
				}
				if(Serializing.GetXmlNodeValue(doc,"ClinicNum")!=null) {
					ClinicNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"ClinicNum"));
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
