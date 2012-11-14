package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
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

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				ToothGridCellNum=Integer.valueOf(doc.getElementsByTagName("ToothGridCellNum").item(0).getFirstChild().getNodeValue());
				SheetFieldNum=Integer.valueOf(doc.getElementsByTagName("SheetFieldNum").item(0).getFirstChild().getNodeValue());
				ToothGridColNum=Integer.valueOf(doc.getElementsByTagName("ToothGridColNum").item(0).getFirstChild().getNodeValue());
				ValueEntered=doc.getElementsByTagName("ValueEntered").item(0).getFirstChild().getNodeValue();
				ToothNum=doc.getElementsByTagName("ToothNum").item(0).getFirstChild().getNodeValue();
			}
			catch(Exception e) {
				throw e;
			}
		}


}
