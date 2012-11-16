package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

public class Account {
		/** Primary key.. */
		public int AccountNum;
		/** . */
		public String Description;
		/** Enum:AccountType Asset, Liability, Equity,Revenue, Expense */
		public AccountType AcctType;
		/** For asset accounts, this would be the bank account number for deposit slips. */
		public String BankNumber;
		/** Set to true to not normally view this account in the list. */
		public boolean Inactive;
		/** . */
		public int AccountColor;

		/** Deep copy of object. */
		public Account Copy() {
			Account account=new Account();
			account.AccountNum=this.AccountNum;
			account.Description=this.Description;
			account.AcctType=this.AcctType;
			account.BankNumber=this.BankNumber;
			account.Inactive=this.Inactive;
			account.AccountColor=this.AccountColor;
			return account;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<Account>");
			sb.append("<AccountNum>").append(AccountNum).append("</AccountNum>");
			sb.append("<Description>").append(Serializing.EscapeForXml(Description)).append("</Description>");
			sb.append("<AcctType>").append(AcctType.ordinal()).append("</AcctType>");
			sb.append("<BankNumber>").append(Serializing.EscapeForXml(BankNumber)).append("</BankNumber>");
			sb.append("<Inactive>").append((Inactive)?1:0).append("</Inactive>");
			sb.append("<AccountColor>").append(AccountColor).append("</AccountColor>");
			sb.append("</Account>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				if(Serializing.GetXmlNodeValue(doc,"AccountNum")!=null) {
					AccountNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"AccountNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"Description")!=null) {
					Description=Serializing.GetXmlNodeValue(doc,"Description");
				}
				if(Serializing.GetXmlNodeValue(doc,"AcctType")!=null) {
					AcctType=AccountType.values()[Integer.valueOf(Serializing.GetXmlNodeValue(doc,"AcctType"))];
				}
				if(Serializing.GetXmlNodeValue(doc,"BankNumber")!=null) {
					BankNumber=Serializing.GetXmlNodeValue(doc,"BankNumber");
				}
				if(Serializing.GetXmlNodeValue(doc,"Inactive")!=null) {
					Inactive=(Serializing.GetXmlNodeValue(doc,"Inactive")=="0")?false:true;
				}
				if(Serializing.GetXmlNodeValue(doc,"AccountColor")!=null) {
					AccountColor=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"AccountColor"));
				}
			}
			catch(Exception e) {
				throw e;
			}
		}

		/** Used in accounting for chart of accounts. */
		public enum AccountType {
			/** 0 */
			Asset,
			/** 1 */
			Liability,
			/** 2 */
			Equity,
			/** 3 */
			Income,
			/** 4 */
			Expense
		}


}
