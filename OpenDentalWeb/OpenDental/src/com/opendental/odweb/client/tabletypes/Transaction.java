package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

public class Transaction {
		/** Primary key. */
		public int TransactionNum;
		/** Not user editable.  Server time. */
		public String DateTimeEntry;
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
			sb.append("<DateTimeEntry>").append(Serializing.EscapeForXml(DateTimeEntry)).append("</DateTimeEntry>");
			sb.append("<UserNum>").append(UserNum).append("</UserNum>");
			sb.append("<DepositNum>").append(DepositNum).append("</DepositNum>");
			sb.append("<PayNum>").append(PayNum).append("</PayNum>");
			sb.append("</Transaction>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				TransactionNum=Integer.valueOf(doc.getElementsByTagName("TransactionNum").item(0).getFirstChild().getNodeValue());
				DateTimeEntry=doc.getElementsByTagName("DateTimeEntry").item(0).getFirstChild().getNodeValue();
				UserNum=Integer.valueOf(doc.getElementsByTagName("UserNum").item(0).getFirstChild().getNodeValue());
				DepositNum=Integer.valueOf(doc.getElementsByTagName("DepositNum").item(0).getFirstChild().getNodeValue());
				PayNum=Integer.valueOf(doc.getElementsByTagName("PayNum").item(0).getFirstChild().getNodeValue());
			}
			catch(Exception e) {
				throw e;
			}
		}


}
