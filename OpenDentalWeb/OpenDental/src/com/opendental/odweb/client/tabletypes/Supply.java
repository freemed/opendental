package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

//DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD.
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
		public Supply deepCopy() {
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
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<Supply>");
			sb.append("<SupplyNum>").append(SupplyNum).append("</SupplyNum>");
			sb.append("<SupplierNum>").append(SupplierNum).append("</SupplierNum>");
			sb.append("<CatalogNumber>").append(Serializing.escapeForXml(CatalogNumber)).append("</CatalogNumber>");
			sb.append("<Descript>").append(Serializing.escapeForXml(Descript)).append("</Descript>");
			sb.append("<Category>").append(Category).append("</Category>");
			sb.append("<ItemOrder>").append(ItemOrder).append("</ItemOrder>");
			sb.append("<LevelDesired>").append(LevelDesired).append("</LevelDesired>");
			sb.append("<IsHidden>").append((IsHidden)?1:0).append("</IsHidden>");
			sb.append("<Price>").append(Price).append("</Price>");
			sb.append("</Supply>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"SupplyNum")!=null) {
					SupplyNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"SupplyNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"SupplierNum")!=null) {
					SupplierNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"SupplierNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"CatalogNumber")!=null) {
					CatalogNumber=Serializing.getXmlNodeValue(doc,"CatalogNumber");
				}
				if(Serializing.getXmlNodeValue(doc,"Descript")!=null) {
					Descript=Serializing.getXmlNodeValue(doc,"Descript");
				}
				if(Serializing.getXmlNodeValue(doc,"Category")!=null) {
					Category=Integer.valueOf(Serializing.getXmlNodeValue(doc,"Category"));
				}
				if(Serializing.getXmlNodeValue(doc,"ItemOrder")!=null) {
					ItemOrder=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ItemOrder"));
				}
				if(Serializing.getXmlNodeValue(doc,"LevelDesired")!=null) {
					LevelDesired=Float.valueOf(Serializing.getXmlNodeValue(doc,"LevelDesired"));
				}
				if(Serializing.getXmlNodeValue(doc,"IsHidden")!=null) {
					IsHidden=(Serializing.getXmlNodeValue(doc,"IsHidden")=="0")?false:true;
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
