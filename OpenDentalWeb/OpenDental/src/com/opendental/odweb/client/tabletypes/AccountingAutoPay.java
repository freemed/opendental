package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

//DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD.
public class AccountingAutoPay {
		/** Primary key. */
		public int AccountingAutoPayNum;
		/** FK to definition.DefNum. */
		public int PayType;
		/** FK to account.AccountNum.  AccountNums separated by commas.  No spaces. */
		public String PickList;

		/** Deep copy of object. */
		public AccountingAutoPay deepCopy() {
			AccountingAutoPay accountingautopay=new AccountingAutoPay();
			accountingautopay.AccountingAutoPayNum=this.AccountingAutoPayNum;
			accountingautopay.PayType=this.PayType;
			accountingautopay.PickList=this.PickList;
			return accountingautopay;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<AccountingAutoPay>");
			sb.append("<AccountingAutoPayNum>").append(AccountingAutoPayNum).append("</AccountingAutoPayNum>");
			sb.append("<PayType>").append(PayType).append("</PayType>");
			sb.append("<PickList>").append(Serializing.escapeForXml(PickList)).append("</PickList>");
			sb.append("</AccountingAutoPay>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"AccountingAutoPayNum")!=null) {
					AccountingAutoPayNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"AccountingAutoPayNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"PayType")!=null) {
					PayType=Integer.valueOf(Serializing.getXmlNodeValue(doc,"PayType"));
				}
				if(Serializing.getXmlNodeValue(doc,"PickList")!=null) {
					PickList=Serializing.getXmlNodeValue(doc,"PickList");
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
