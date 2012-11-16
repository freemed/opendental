package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

public class SupplyOrder {
		/** Primary key. */
		public int SupplyOrderNum;
		/** FK to supplier.SupplierNum. */
		public int SupplierNum;
		/** A date greater than 2200 (eg 2500), is considered a max date.  A max date is used for an order that was started but has not yet been placed.  This puts it at the end of the list where it belongs, but it will display as blank.  Only one unplaced order is allowed per supplier. */
		public Date DatePlaced;
		/** . */
		public String Note;
		/** The sum of all the amounts of each item on the order.  If any of the item prices are zero, then it won't auto calculate this total.  This will allow the user to manually put in the total without having it get deleted. */
		public double AmountTotal;

		/** Deep copy of object. */
		public SupplyOrder Copy() {
			SupplyOrder supplyorder=new SupplyOrder();
			supplyorder.SupplyOrderNum=this.SupplyOrderNum;
			supplyorder.SupplierNum=this.SupplierNum;
			supplyorder.DatePlaced=this.DatePlaced;
			supplyorder.Note=this.Note;
			supplyorder.AmountTotal=this.AmountTotal;
			return supplyorder;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<SupplyOrder>");
			sb.append("<SupplyOrderNum>").append(SupplyOrderNum).append("</SupplyOrderNum>");
			sb.append("<SupplierNum>").append(SupplierNum).append("</SupplierNum>");
			sb.append("<DatePlaced>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DatePlaced)).append("</DatePlaced>");
			sb.append("<Note>").append(Serializing.EscapeForXml(Note)).append("</Note>");
			sb.append("<AmountTotal>").append(AmountTotal).append("</AmountTotal>");
			sb.append("</SupplyOrder>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				if(Serializing.GetXmlNodeValue(doc,"SupplyOrderNum")!=null) {
					SupplyOrderNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"SupplyOrderNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"SupplierNum")!=null) {
					SupplierNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"SupplierNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"DatePlaced")!=null) {
					DatePlaced=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.GetXmlNodeValue(doc,"DatePlaced"));
				}
				if(Serializing.GetXmlNodeValue(doc,"Note")!=null) {
					Note=Serializing.GetXmlNodeValue(doc,"Note");
				}
				if(Serializing.GetXmlNodeValue(doc,"AmountTotal")!=null) {
					AmountTotal=Double.valueOf(Serializing.GetXmlNodeValue(doc,"AmountTotal"));
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
