package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

//DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD.
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
		public Account deepCopy() {
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
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<Account>");
			sb.append("<AccountNum>").append(AccountNum).append("</AccountNum>");
			sb.append("<Description>").append(Serializing.escapeForXml(Description)).append("</Description>");
			sb.append("<AcctType>").append(AcctType.ordinal()).append("</AcctType>");
			sb.append("<BankNumber>").append(Serializing.escapeForXml(BankNumber)).append("</BankNumber>");
			sb.append("<Inactive>").append((Inactive)?1:0).append("</Inactive>");
			sb.append("<AccountColor>").append(AccountColor).append("</AccountColor>");
			sb.append("</Account>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"AccountNum")!=null) {
					AccountNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"AccountNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"Description")!=null) {
					Description=Serializing.getXmlNodeValue(doc,"Description");
				}
				if(Serializing.getXmlNodeValue(doc,"AcctType")!=null) {
					AcctType=AccountType.values()[Integer.valueOf(Serializing.getXmlNodeValue(doc,"AcctType"))];
				}
				if(Serializing.getXmlNodeValue(doc,"BankNumber")!=null) {
					BankNumber=Serializing.getXmlNodeValue(doc,"BankNumber");
				}
				if(Serializing.getXmlNodeValue(doc,"Inactive")!=null) {
					Inactive=(Serializing.getXmlNodeValue(doc,"Inactive")=="0")?false:true;
				}
				if(Serializing.getXmlNodeValue(doc,"AccountColor")!=null) {
					AccountColor=Integer.valueOf(Serializing.getXmlNodeValue(doc,"AccountColor"));
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
