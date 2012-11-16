package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

public class AccountingAutoPay {
		/** Primary key. */
		public int AccountingAutoPayNum;
		/** FK to definition.DefNum. */
		public int PayType;
		/** FK to account.AccountNum.  AccountNums separated by commas.  No spaces. */
		public String PickList;

		/** Deep copy of object. */
		public AccountingAutoPay Copy() {
			AccountingAutoPay accountingautopay=new AccountingAutoPay();
			accountingautopay.AccountingAutoPayNum=this.AccountingAutoPayNum;
			accountingautopay.PayType=this.PayType;
			accountingautopay.PickList=this.PickList;
			return accountingautopay;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<AccountingAutoPay>");
			sb.append("<AccountingAutoPayNum>").append(AccountingAutoPayNum).append("</AccountingAutoPayNum>");
			sb.append("<PayType>").append(PayType).append("</PayType>");
			sb.append("<PickList>").append(Serializing.EscapeForXml(PickList)).append("</PickList>");
			sb.append("</AccountingAutoPay>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				if(Serializing.GetXmlNodeValue(doc,"AccountingAutoPayNum")!=null) {
					AccountingAutoPayNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"AccountingAutoPayNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"PayType")!=null) {
					PayType=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"PayType"));
				}
				if(Serializing.GetXmlNodeValue(doc,"PickList")!=null) {
					PickList=Serializing.GetXmlNodeValue(doc,"PickList");
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
