package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

//DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD.
public class ClaimPayment {
		/** Primary key. */
		public int ClaimPaymentNum;
		/** Date the check was entered into this system, not the date on the check. */
		public Date CheckDate;
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
		public Date DateIssued;
		/** . */
		public boolean IsPartial;

		/** Deep copy of object. */
		public ClaimPayment deepCopy() {
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
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<ClaimPayment>");
			sb.append("<ClaimPaymentNum>").append(ClaimPaymentNum).append("</ClaimPaymentNum>");
			sb.append("<CheckDate>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(CheckDate)).append("</CheckDate>");
			sb.append("<CheckAmt>").append(CheckAmt).append("</CheckAmt>");
			sb.append("<CheckNum>").append(Serializing.escapeForXml(CheckNum)).append("</CheckNum>");
			sb.append("<BankBranch>").append(Serializing.escapeForXml(BankBranch)).append("</BankBranch>");
			sb.append("<Note>").append(Serializing.escapeForXml(Note)).append("</Note>");
			sb.append("<ClinicNum>").append(ClinicNum).append("</ClinicNum>");
			sb.append("<DepositNum>").append(DepositNum).append("</DepositNum>");
			sb.append("<CarrierName>").append(Serializing.escapeForXml(CarrierName)).append("</CarrierName>");
			sb.append("<DateIssued>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateIssued)).append("</DateIssued>");
			sb.append("<IsPartial>").append((IsPartial)?1:0).append("</IsPartial>");
			sb.append("</ClaimPayment>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"ClaimPaymentNum")!=null) {
					ClaimPaymentNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ClaimPaymentNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"CheckDate")!=null) {
					CheckDate=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"CheckDate"));
				}
				if(Serializing.getXmlNodeValue(doc,"CheckAmt")!=null) {
					CheckAmt=Double.valueOf(Serializing.getXmlNodeValue(doc,"CheckAmt"));
				}
				if(Serializing.getXmlNodeValue(doc,"CheckNum")!=null) {
					CheckNum=Serializing.getXmlNodeValue(doc,"CheckNum");
				}
				if(Serializing.getXmlNodeValue(doc,"BankBranch")!=null) {
					BankBranch=Serializing.getXmlNodeValue(doc,"BankBranch");
				}
				if(Serializing.getXmlNodeValue(doc,"Note")!=null) {
					Note=Serializing.getXmlNodeValue(doc,"Note");
				}
				if(Serializing.getXmlNodeValue(doc,"ClinicNum")!=null) {
					ClinicNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ClinicNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"DepositNum")!=null) {
					DepositNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"DepositNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"CarrierName")!=null) {
					CarrierName=Serializing.getXmlNodeValue(doc,"CarrierName");
				}
				if(Serializing.getXmlNodeValue(doc,"DateIssued")!=null) {
					DateIssued=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"DateIssued"));
				}
				if(Serializing.getXmlNodeValue(doc,"IsPartial")!=null) {
					IsPartial=(Serializing.getXmlNodeValue(doc,"IsPartial")=="0")?false:true;
				}
			}
			catch(Exception e) {
				throw new Exception("Error deserializing ClaimPayment: "+e.getMessage());
			}
		}


}
