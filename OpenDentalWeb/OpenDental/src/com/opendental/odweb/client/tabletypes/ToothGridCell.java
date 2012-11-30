package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

public class ToothGridCell {
		/** Primary key. */
		public int ToothGridCellNum;
		/** FK to sheet.SheetFieldNum.  Required. */
		public int SheetFieldNum;
		/** FK to toothgridcol.ToothGridColNum.  This tells which column it belongs in.  Can't use the column name here because multiple columns could have the same name. */
		public int ToothGridColNum;
		/** Cannot be empty.  For a tooth-level cell, the only allowed value is X.  If the cell is unchecked, then it won't even have a row in this table.  For a surface level column, only valid surfaces can be entered:MOIDBFLV  Enforced.  FreeText columns can have any text up to 255 char. */
		public String ValueEntered;
		/** Corresponds exactly to procedurelog.ToothNum.  May be blank, otherwise 1-32, 51-82, A-T, or AS-TS, 1 or 2 char.  Gets internationalized as being displayed. */
		public String ToothNum;

		/** Deep copy of object. */
		public ToothGridCell Copy() {
			ToothGridCell toothgridcell=new ToothGridCell();
			toothgridcell.ToothGridCellNum=this.ToothGridCellNum;
			toothgridcell.SheetFieldNum=this.SheetFieldNum;
			toothgridcell.ToothGridColNum=this.ToothGridColNum;
			toothgridcell.ValueEntered=this.ValueEntered;
			toothgridcell.ToothNum=this.ToothNum;
			return toothgridcell;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<ToothGridCell>");
			sb.append("<ToothGridCellNum>").append(ToothGridCellNum).append("</ToothGridCellNum>");
			sb.append("<SheetFieldNum>").append(SheetFieldNum).append("</SheetFieldNum>");
			sb.append("<ToothGridColNum>").append(ToothGridColNum).append("</ToothGridColNum>");
			sb.append("<ValueEntered>").append(Serializing.EscapeForXml(ValueEntered)).append("</ValueEntered>");
			sb.append("<ToothNum>").append(Serializing.EscapeForXml(ToothNum)).append("</ToothNum>");
			sb.append("</ToothGridCell>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void DeserializeFromXml(Document doc) throws Exception {
			try {
				if(Serializing.GetXmlNodeValue(doc,"ToothGridCellNum")!=null) {
					ToothGridCellNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"ToothGridCellNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"SheetFieldNum")!=null) {
					SheetFieldNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"SheetFieldNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"ToothGridColNum")!=null) {
					ToothGridColNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"ToothGridColNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"ValueEntered")!=null) {
					ValueEntered=Serializing.GetXmlNodeValue(doc,"ValueEntered");
				}
				if(Serializing.GetXmlNodeValue(doc,"ToothNum")!=null) {
					ToothNum=Serializing.GetXmlNodeValue(doc,"ToothNum");
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
