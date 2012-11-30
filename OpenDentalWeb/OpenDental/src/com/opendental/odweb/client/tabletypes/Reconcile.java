package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

public class Reconcile {
		/** Primary key. */
		public int ReconcileNum;
		/** FK to account.AccountNum */
		public int AccountNum;
		/** User enters starting balance here. */
		public double StartingBal;
		/** User enters ending balance here. */
		public double EndingBal;
		/** The date that the reconcile was performed. */
		public Date DateReconcile;
		/** If StartingBal + sum of entries selected = EndingBal, then user can lock.  Unlock requires special permission, which nobody will have by default. */
		public boolean IsLocked;

		/** Deep copy of object. */
		public Reconcile Copy() {
			Reconcile reconcile=new Reconcile();
			reconcile.ReconcileNum=this.ReconcileNum;
			reconcile.AccountNum=this.AccountNum;
			reconcile.StartingBal=this.StartingBal;
			reconcile.EndingBal=this.EndingBal;
			reconcile.DateReconcile=this.DateReconcile;
			reconcile.IsLocked=this.IsLocked;
			return reconcile;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<Reconcile>");
			sb.append("<ReconcileNum>").append(ReconcileNum).append("</ReconcileNum>");
			sb.append("<AccountNum>").append(AccountNum).append("</AccountNum>");
			sb.append("<StartingBal>").append(StartingBal).append("</StartingBal>");
			sb.append("<EndingBal>").append(EndingBal).append("</EndingBal>");
			sb.append("<DateReconcile>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateReconcile)).append("</DateReconcile>");
			sb.append("<IsLocked>").append((IsLocked)?1:0).append("</IsLocked>");
			sb.append("</Reconcile>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void DeserializeFromXml(Document doc) throws Exception {
			try {
				if(Serializing.GetXmlNodeValue(doc,"ReconcileNum")!=null) {
					ReconcileNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"ReconcileNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"AccountNum")!=null) {
					AccountNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"AccountNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"StartingBal")!=null) {
					StartingBal=Double.valueOf(Serializing.GetXmlNodeValue(doc,"StartingBal"));
				}
				if(Serializing.GetXmlNodeValue(doc,"EndingBal")!=null) {
					EndingBal=Double.valueOf(Serializing.GetXmlNodeValue(doc,"EndingBal"));
				}
				if(Serializing.GetXmlNodeValue(doc,"DateReconcile")!=null) {
					DateReconcile=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.GetXmlNodeValue(doc,"DateReconcile"));
				}
				if(Serializing.GetXmlNodeValue(doc,"IsLocked")!=null) {
					IsLocked=(Serializing.GetXmlNodeValue(doc,"IsLocked")=="0")?false:true;
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
