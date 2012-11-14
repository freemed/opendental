package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

public class Supply {
		/** Primary key. */
		public int SupplyNum;
		/** FK to supplier.SupplierNum */
		public int SupplierNum;
		/** The catalog item number that the supplier uses to identify the supply. */
		public String CatalogNumber;
		/** The description can be similar to the catalog, but not required.  Typically includes qty per box/case, etc. */
		public String Descript;
		/** FK to definition.DefNum.  User can define their own categories for supplies. */
		public int Category;
		/** The zero-based order of this supply within it's category. */
		public int ItemOrder;
		/** The level that a fresh order should bring item back up to.  Can include fractions.  If this is 0, then it will be displayed as having this field blank rather than showing 0.  This simply gives a cleaner look. */
		public float LevelDesired;
		/** If hidden, then this supply item won't normally show in the main list. */
		public boolean IsHidden;
		/** The price per unit that the supplier charges for this supply.  If this is 0.00, then no price will be displayed. */
		public double Price;

		/** Deep copy of object. */
		public Supply Copy() {
			Supply supply=new Supply();
			supply.SupplyNum=this.SupplyNum;
			supply.SupplierNum=this.SupplierNum;
			supply.CatalogNumber=this.CatalogNumber;
			supply.Descript=this.Descript;
			supply.Category=this.Category;
			supply.ItemOrder=this.ItemOrder;
			supply.LevelDesired=this.LevelDesired;
			supply.IsHidden=this.IsHidden;
			supply.Price=this.Price;
			return supply;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<Supply>");
			sb.append("<SupplyNum>").append(SupplyNum).append("</SupplyNum>");
			sb.append("<SupplierNum>").append(SupplierNum).append("</SupplierNum>");
			sb.append("<CatalogNumber>").append(Serializing.EscapeForXml(CatalogNumber)).append("</CatalogNumber>");
			sb.append("<Descript>").append(Serializing.EscapeForXml(Descript)).append("</Descript>");
			sb.append("<Category>").append(Category).append("</Category>");
			sb.append("<ItemOrder>").append(ItemOrder).append("</ItemOrder>");
			sb.append("<LevelDesired>").append(LevelDesired).append("</LevelDesired>");
			sb.append("<IsHidden>").append((IsHidden)?1:0).append("</IsHidden>");
			sb.append("<Price>").append(Price).append("</Price>");
			sb.append("</Supply>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				SupplyNum=Integer.valueOf(doc.getElementsByTagName("SupplyNum").item(0).getFirstChild().getNodeValue());
				SupplierNum=Integer.valueOf(doc.getElementsByTagName("SupplierNum").item(0).getFirstChild().getNodeValue());
				CatalogNumber=doc.getElementsByTagName("CatalogNumber").item(0).getFirstChild().getNodeValue();
				Descript=doc.getElementsByTagName("Descript").item(0).getFirstChild().getNodeValue();
				Category=Integer.valueOf(doc.getElementsByTagName("Category").item(0).getFirstChild().getNodeValue());
				ItemOrder=Integer.valueOf(doc.getElementsByTagName("ItemOrder").item(0).getFirstChild().getNodeValue());
				LevelDesired=Float.valueOf(doc.getElementsByTagName("LevelDesired").item(0).getFirstChild().getNodeValue());
				IsHidden=(doc.getElementsByTagName("IsHidden").item(0).getFirstChild().getNodeValue()=="0")?false:true;
				Price=Double.valueOf(doc.getElementsByTagName("Price").item(0).getFirstChild().getNodeValue());
			}
			catch(Exception e) {
				throw e;
			}
		}


}
