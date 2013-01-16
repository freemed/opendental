package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

//DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD.
public class JournalEntry {
		/** Primary key. */
		public int JournalEntryNum;
		/** FK to transaction.TransactionNum */
		public int TransactionNum;
		/** FK to account.AccountNum */
		public int AccountNum;
		/** Always the same for all journal entries within one transaction. */
		public Date DateDisplayed;
		/** Negative numbers never allowed. */
		public double DebitAmt;
		/** Negative numbers never allowed. */
		public double CreditAmt;
		/** . */
		public String Memo;
		/** A human-readable description of the splits.  Used only for display purposes. */
		public String Splits;
		/** Any user-defined string.  Usually a check number, but can also be D for deposit, Adj, etc. */
		public String CheckNumber;
		/** FK to reconcile.ReconcileNum. 0 if not attached to a reconcile. Not allowed to alter amounts if attached. */
		public int ReconcileNum;

		/** Deep copy of object. */
		public JournalEntry deepCopy() {
			JournalEntry journalentry=new JournalEntry();
			journalentry.JournalEntryNum=this.JournalEntryNum;
			journalentry.TransactionNum=this.TransactionNum;
			journalentry.AccountNum=this.AccountNum;
			journalentry.DateDisplayed=this.DateDisplayed;
			journalentry.DebitAmt=this.DebitAmt;
			journalentry.CreditAmt=this.CreditAmt;
			journalentry.Memo=this.Memo;
			journalentry.Splits=this.Splits;
			journalentry.CheckNumber=this.CheckNumber;
			journalentry.ReconcileNum=this.ReconcileNum;
			return journalentry;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<JournalEntry>");
			sb.append("<JournalEntryNum>").append(JournalEntryNum).append("</JournalEntryNum>");
			sb.append("<TransactionNum>").append(TransactionNum).append("</TransactionNum>");
			sb.append("<AccountNum>").append(AccountNum).append("</AccountNum>");
			sb.append("<DateDisplayed>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateDisplayed)).append("</DateDisplayed>");
			sb.append("<DebitAmt>").append(DebitAmt).append("</DebitAmt>");
			sb.append("<CreditAmt>").append(CreditAmt).append("</CreditAmt>");
			sb.append("<Memo>").append(Serializing.escapeForXml(Memo)).append("</Memo>");
			sb.append("<Splits>").append(Serializing.escapeForXml(Splits)).append("</Splits>");
			sb.append("<CheckNumber>").append(Serializing.escapeForXml(CheckNumber)).append("</CheckNumber>");
			sb.append("<ReconcileNum>").append(ReconcileNum).append("</ReconcileNum>");
			sb.append("</JournalEntry>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"JournalEntryNum")!=null) {
					JournalEntryNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"JournalEntryNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"TransactionNum")!=null) {
					TransactionNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"TransactionNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"AccountNum")!=null) {
					AccountNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"AccountNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"DateDisplayed")!=null) {
					DateDisplayed=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"DateDisplayed"));
				}
				if(Serializing.getXmlNodeValue(doc,"DebitAmt")!=null) {
					DebitAmt=Double.valueOf(Serializing.getXmlNodeValue(doc,"DebitAmt"));
				}
				if(Serializing.getXmlNodeValue(doc,"CreditAmt")!=null) {
					CreditAmt=Double.valueOf(Serializing.getXmlNodeValue(doc,"CreditAmt"));
				}
				if(Serializing.getXmlNodeValue(doc,"Memo")!=null) {
					Memo=Serializing.getXmlNodeValue(doc,"Memo");
				}
				if(Serializing.getXmlNodeValue(doc,"Splits")!=null) {
					Splits=Serializing.getXmlNodeValue(doc,"Splits");
				}
				if(Serializing.getXmlNodeValue(doc,"CheckNumber")!=null) {
					CheckNumber=Serializing.getXmlNodeValue(doc,"CheckNumber");
				}
				if(Serializing.getXmlNodeValue(doc,"ReconcileNum")!=null) {
					ReconcileNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ReconcileNum"));
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
