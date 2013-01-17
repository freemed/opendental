package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

//DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD.
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
		public SupplyOrder deepCopy() {
			SupplyOrder supplyorder=new SupplyOrder();
			supplyorder.SupplyOrderNum=this.SupplyOrderNum;
			supplyorder.SupplierNum=this.SupplierNum;
			supplyorder.DatePlaced=this.DatePlaced;
			supplyorder.Note=this.Note;
			supplyorder.AmountTotal=this.AmountTotal;
			return supplyorder;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<SupplyOrder>");
			sb.append("<SupplyOrderNum>").append(SupplyOrderNum).append("</SupplyOrderNum>");
			sb.append("<SupplierNum>").append(SupplierNum).append("</SupplierNum>");
			sb.append("<DatePlaced>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DatePlaced)).append("</DatePlaced>");
			sb.append("<Note>").append(Serializing.escapeForXml(Note)).append("</Note>");
			sb.append("<AmountTotal>").append(AmountTotal).append("</AmountTotal>");
			sb.append("</SupplyOrder>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"SupplyOrderNum")!=null) {
					SupplyOrderNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"SupplyOrderNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"SupplierNum")!=null) {
					SupplierNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"SupplierNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"DatePlaced")!=null) {
					DatePlaced=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"DatePlaced"));
				}
				if(Serializing.getXmlNodeValue(doc,"Note")!=null) {
					Note=Serializing.getXmlNodeValue(doc,"Note");
				}
				if(Serializing.getXmlNodeValue(doc,"AmountTotal")!=null) {
					AmountTotal=Double.valueOf(Serializing.getXmlNodeValue(doc,"AmountTotal"));
				}
			}
			catch(Exception e) {
				throw new Exception("Error deserializing SupplyOrder: "+e.getMessage());
			}
		}


}
