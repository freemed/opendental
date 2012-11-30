package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

public class Transaction {
		/** Primary key. */
		public int TransactionNum;
		/** Not user editable.  Server time. */
		public Date DateTimeEntry;
		/** FK to user.UserNum. */
		public int UserNum;
		/** FK to deposit.DepositNum.  Will eventually be replaced by a source document table, and deposits will just be one of many types. */
		public int DepositNum;
		/** FK to payment.PayNum.  Like DepositNum, it will eventually be replaced by a source document table, and payments will just be one of many types. */
		public int PayNum;

		/** Deep copy of object. */
		public Transaction Copy() {
			Transaction transaction=new Transaction();
			transaction.TransactionNum=this.TransactionNum;
			transaction.DateTimeEntry=this.DateTimeEntry;
			transaction.UserNum=this.UserNum;
			transaction.DepositNum=this.DepositNum;
			transaction.PayNum=this.PayNum;
			return transaction;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<Transaction>");
			sb.append("<TransactionNum>").append(TransactionNum).append("</TransactionNum>");
			sb.append("<DateTimeEntry>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateTimeEntry)).append("</DateTimeEntry>");
			sb.append("<UserNum>").append(UserNum).append("</UserNum>");
			sb.append("<DepositNum>").append(DepositNum).append("</DepositNum>");
			sb.append("<PayNum>").append(PayNum).append("</PayNum>");
			sb.append("</Transaction>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void DeserializeFromXml(Document doc) throws Exception {
			try {
				if(Serializing.GetXmlNodeValue(doc,"TransactionNum")!=null) {
					TransactionNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"TransactionNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"DateTimeEntry")!=null) {
					DateTimeEntry=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.GetXmlNodeValue(doc,"DateTimeEntry"));
				}
				if(Serializing.GetXmlNodeValue(doc,"UserNum")!=null) {
					UserNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"UserNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"DepositNum")!=null) {
					DepositNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"DepositNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"PayNum")!=null) {
					PayNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"PayNum"));
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
