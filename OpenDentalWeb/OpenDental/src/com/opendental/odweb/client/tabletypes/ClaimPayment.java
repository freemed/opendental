package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

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
			sb.append("<CheckDate>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(CheckDate)).append("</CheckDate>");
			sb.append("<CheckAmt>").append(CheckAmt).append("</CheckAmt>");
			sb.append("<CheckNum>").append(Serializing.EscapeForXml(CheckNum)).append("</CheckNum>");
			sb.append("<BankBranch>").append(Serializing.EscapeForXml(BankBranch)).append("</BankBranch>");
			sb.append("<Note>").append(Serializing.EscapeForXml(Note)).append("</Note>");
			sb.append("<ClinicNum>").append(ClinicNum).append("</ClinicNum>");
			sb.append("<DepositNum>").append(DepositNum).append("</DepositNum>");
			sb.append("<CarrierName>").append(Serializing.EscapeForXml(CarrierName)).append("</CarrierName>");
			sb.append("<DateIssued>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateIssued)).append("</DateIssued>");
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
				if(Serializing.GetXmlNodeValue(doc,"ClaimPaymentNum")!=null) {
					ClaimPaymentNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"ClaimPaymentNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"CheckDate")!=null) {
					CheckDate=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.GetXmlNodeValue(doc,"CheckDate"));
				}
				if(Serializing.GetXmlNodeValue(doc,"CheckAmt")!=null) {
					CheckAmt=Double.valueOf(Serializing.GetXmlNodeValue(doc,"CheckAmt"));
				}
				if(Serializing.GetXmlNodeValue(doc,"CheckNum")!=null) {
					CheckNum=Serializing.GetXmlNodeValue(doc,"CheckNum");
				}
				if(Serializing.GetXmlNodeValue(doc,"BankBranch")!=null) {
					BankBranch=Serializing.GetXmlNodeValue(doc,"BankBranch");
				}
				if(Serializing.GetXmlNodeValue(doc,"Note")!=null) {
					Note=Serializing.GetXmlNodeValue(doc,"Note");
				}
				if(Serializing.GetXmlNodeValue(doc,"ClinicNum")!=null) {
					ClinicNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"ClinicNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"DepositNum")!=null) {
					DepositNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"DepositNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"CarrierName")!=null) {
					CarrierName=Serializing.GetXmlNodeValue(doc,"CarrierName");
				}
				if(Serializing.GetXmlNodeValue(doc,"DateIssued")!=null) {
					DateIssued=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.GetXmlNodeValue(doc,"DateIssued"));
				}
				if(Serializing.GetXmlNodeValue(doc,"IsPartial")!=null) {
					IsPartial=(Serializing.GetXmlNodeValue(doc,"IsPartial")=="0")?false:true;
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
