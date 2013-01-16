package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

//DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD.
public class SupplyOrderItem {
		/** Primary key. */
		public int SupplyOrderItemNum;
		/** FK to supplyorder.supplyOrderNum. */
		public int SupplyOrderNum;
		/** FK to supply.SupplyNum. */
		public int SupplyNum;
		/** How many were ordered. */
		public int Qty;
		/** Price per unit on this order. */
		public double Price;

		/** Deep copy of object. */
		public SupplyOrderItem deepCopy() {
			SupplyOrderItem supplyorderitem=new SupplyOrderItem();
			supplyorderitem.SupplyOrderItemNum=this.SupplyOrderItemNum;
			supplyorderitem.SupplyOrderNum=this.SupplyOrderNum;
			supplyorderitem.SupplyNum=this.SupplyNum;
			supplyorderitem.Qty=this.Qty;
			supplyorderitem.Price=this.Price;
			return supplyorderitem;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<SupplyOrderItem>");
			sb.append("<SupplyOrderItemNum>").append(SupplyOrderItemNum).append("</SupplyOrderItemNum>");
			sb.append("<SupplyOrderNum>").append(SupplyOrderNum).append("</SupplyOrderNum>");
			sb.append("<SupplyNum>").append(SupplyNum).append("</SupplyNum>");
			sb.append("<Qty>").append(Qty).append("</Qty>");
			sb.append("<Price>").append(Price).append("</Price>");
			sb.append("</SupplyOrderItem>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"SupplyOrderItemNum")!=null) {
					SupplyOrderItemNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"SupplyOrderItemNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"SupplyOrderNum")!=null) {
					SupplyOrderNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"SupplyOrderNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"SupplyNum")!=null) {
					SupplyNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"SupplyNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"Qty")!=null) {
					Qty=Integer.valueOf(Serializing.getXmlNodeValue(doc,"Qty"));
				}
				if(Serializing.getXmlNodeValue(doc,"Price")!=null) {
					Price=Double.valueOf(Serializing.getXmlNodeValue(doc,"Price"));
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
