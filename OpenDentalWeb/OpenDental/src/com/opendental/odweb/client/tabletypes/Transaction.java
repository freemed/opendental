package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

/** DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD. */
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
		public Transaction deepCopy() {
			Transaction transaction=new Transaction();
			transaction.TransactionNum=this.TransactionNum;
			transaction.DateTimeEntry=this.DateTimeEntry;
			transaction.UserNum=this.UserNum;
			transaction.DepositNum=this.DepositNum;
			transaction.PayNum=this.PayNum;
			return transaction;
		}

		/** Serialize the object into XML. */
		public String serialize() {
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
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"TransactionNum")!=null) {
					TransactionNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"TransactionNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"DateTimeEntry")!=null) {
					DateTimeEntry=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"DateTimeEntry"));
				}
				if(Serializing.getXmlNodeValue(doc,"UserNum")!=null) {
					UserNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"UserNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"DepositNum")!=null) {
					DepositNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"DepositNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"PayNum")!=null) {
					PayNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"PayNum"));
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
