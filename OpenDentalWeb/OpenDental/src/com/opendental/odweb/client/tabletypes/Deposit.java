package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

public class Deposit {
		/** Primary key. */
		public int DepositNum;
		/** The date of the deposit. */
		public Date DateDeposit;
		/** User editable.  Usually includes name on the account and account number.  Possibly the bank name as well. */
		public String BankAccountInfo;
		/** Total amount of the deposit. User not allowed to directly edit. */
		public double Amount;
		/** Short description to help identify the deposit. */
		public String Memo;

		/** Deep copy of object. */
		public Deposit Copy() {
			Deposit deposit=new Deposit();
			deposit.DepositNum=this.DepositNum;
			deposit.DateDeposit=this.DateDeposit;
			deposit.BankAccountInfo=this.BankAccountInfo;
			deposit.Amount=this.Amount;
			deposit.Memo=this.Memo;
			return deposit;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<Deposit>");
			sb.append("<DepositNum>").append(DepositNum).append("</DepositNum>");
			sb.append("<DateDeposit>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateDeposit)).append("</DateDeposit>");
			sb.append("<BankAccountInfo>").append(Serializing.EscapeForXml(BankAccountInfo)).append("</BankAccountInfo>");
			sb.append("<Amount>").append(Amount).append("</Amount>");
			sb.append("<Memo>").append(Serializing.EscapeForXml(Memo)).append("</Memo>");
			sb.append("</Deposit>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void DeserializeFromXml(Document doc) throws Exception {
			try {
				if(Serializing.GetXmlNodeValue(doc,"DepositNum")!=null) {
					DepositNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"DepositNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"DateDeposit")!=null) {
					DateDeposit=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.GetXmlNodeValue(doc,"DateDeposit"));
				}
				if(Serializing.GetXmlNodeValue(doc,"BankAccountInfo")!=null) {
					BankAccountInfo=Serializing.GetXmlNodeValue(doc,"BankAccountInfo");
				}
				if(Serializing.GetXmlNodeValue(doc,"Amount")!=null) {
					Amount=Double.valueOf(Serializing.GetXmlNodeValue(doc,"Amount"));
				}
				if(Serializing.GetXmlNodeValue(doc,"Memo")!=null) {
					Memo=Serializing.GetXmlNodeValue(doc,"Memo");
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
