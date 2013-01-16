package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

//DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD.
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
		public PaySplit deepCopy() {
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
		public String serialize() {
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

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"SplitNum")!=null) {
					SplitNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"SplitNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"SplitAmt")!=null) {
					SplitAmt=Double.valueOf(Serializing.getXmlNodeValue(doc,"SplitAmt"));
				}
				if(Serializing.getXmlNodeValue(doc,"PatNum")!=null) {
					PatNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"PatNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"ProcDate")!=null) {
					ProcDate=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"ProcDate"));
				}
				if(Serializing.getXmlNodeValue(doc,"PayNum")!=null) {
					PayNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"PayNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"IsDiscount")!=null) {
					IsDiscount=(Serializing.getXmlNodeValue(doc,"IsDiscount")=="0")?false:true;
				}
				if(Serializing.getXmlNodeValue(doc,"DiscountType")!=null) {
					DiscountType=Byte.valueOf(Serializing.getXmlNodeValue(doc,"DiscountType"));
				}
				if(Serializing.getXmlNodeValue(doc,"ProvNum")!=null) {
					ProvNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ProvNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"PayPlanNum")!=null) {
					PayPlanNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"PayPlanNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"DatePay")!=null) {
					DatePay=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"DatePay"));
				}
				if(Serializing.getXmlNodeValue(doc,"ProcNum")!=null) {
					ProcNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ProcNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"DateEntry")!=null) {
					DateEntry=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"DateEntry"));
				}
				if(Serializing.getXmlNodeValue(doc,"UnearnedType")!=null) {
					UnearnedType=Integer.valueOf(Serializing.getXmlNodeValue(doc,"UnearnedType"));
				}
				if(Serializing.getXmlNodeValue(doc,"ClinicNum")!=null) {
					ClinicNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ClinicNum"));
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
