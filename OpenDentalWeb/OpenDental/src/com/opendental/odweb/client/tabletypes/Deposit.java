package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
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

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				DepositNum=Integer.valueOf(doc.getElementsByTagName("DepositNum").item(0).getFirstChild().getNodeValue());
				DateDeposit=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(doc.getElementsByTagName("DateDeposit").item(0).getFirstChild().getNodeValue());
				BankAccountInfo=doc.getElementsByTagName("BankAccountInfo").item(0).getFirstChild().getNodeValue();
				Amount=Double.valueOf(doc.getElementsByTagName("Amount").item(0).getFirstChild().getNodeValue());
				Memo=doc.getElementsByTagName("Memo").item(0).getFirstChild().getNodeValue();
			}
			catch(Exception e) {
				throw e;
			}
		}


}
