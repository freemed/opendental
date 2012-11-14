package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

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
		public SupplyOrderItem Copy() {
			SupplyOrderItem supplyorderitem=new SupplyOrderItem();
			supplyorderitem.SupplyOrderItemNum=this.SupplyOrderItemNum;
			supplyorderitem.SupplyOrderNum=this.SupplyOrderNum;
			supplyorderitem.SupplyNum=this.SupplyNum;
			supplyorderitem.Qty=this.Qty;
			supplyorderitem.Price=this.Price;
			return supplyorderitem;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
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

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				SupplyOrderItemNum=Integer.valueOf(doc.getElementsByTagName("SupplyOrderItemNum").item(0).getFirstChild().getNodeValue());
				SupplyOrderNum=Integer.valueOf(doc.getElementsByTagName("SupplyOrderNum").item(0).getFirstChild().getNodeValue());
				SupplyNum=Integer.valueOf(doc.getElementsByTagName("SupplyNum").item(0).getFirstChild().getNodeValue());
				Qty=Integer.valueOf(doc.getElementsByTagName("Qty").item(0).getFirstChild().getNodeValue());
				Price=Double.valueOf(doc.getElementsByTagName("Price").item(0).getFirstChild().getNodeValue());
			}
			catch(Exception e) {
				throw e;
			}
		}


}
