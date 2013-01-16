package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

//DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD.
public class ToothGridCol {
		/** Primary key. */
		public int ToothGridColNum;
		/** FK to sheet.SheetFieldNum.  Required. */
		public int SheetFieldNum;
		/** Pulled from the ToothGridDef.  This can be a NameInternal , or it can be a NameShowing if it's a user-defined column. */
		public String NameItem;
		/** Enum:ToothGridCellType  0=HardCoded, 1=Tooth, 2=Surface, 3=FreeText. */
		public ToothGridCellType CellType;
		/** Order of the column to display.  Every entry must have a unique itemorder. */
		public int ItemOrder;
		/** . */
		public int ColumnWidth;
		/** FK to procedurecode.CodeNum.  This allows data entered to flow into main program as actual completed or tp procedures. */
		public int CodeNum;
		/** Enum:ProcStat  If these flow into main program, then this is the status that the new procs will have. */
		public ProcStat ProcStatus;

		/** Deep copy of object. */
		public ToothGridCol deepCopy() {
			ToothGridCol toothgridcol=new ToothGridCol();
			toothgridcol.ToothGridColNum=this.ToothGridColNum;
			toothgridcol.SheetFieldNum=this.SheetFieldNum;
			toothgridcol.NameItem=this.NameItem;
			toothgridcol.CellType=this.CellType;
			toothgridcol.ItemOrder=this.ItemOrder;
			toothgridcol.ColumnWidth=this.ColumnWidth;
			toothgridcol.CodeNum=this.CodeNum;
			toothgridcol.ProcStatus=this.ProcStatus;
			return toothgridcol;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<ToothGridCol>");
			sb.append("<ToothGridColNum>").append(ToothGridColNum).append("</ToothGridColNum>");
			sb.append("<SheetFieldNum>").append(SheetFieldNum).append("</SheetFieldNum>");
			sb.append("<NameItem>").append(Serializing.escapeForXml(NameItem)).append("</NameItem>");
			sb.append("<CellType>").append(CellType.ordinal()).append("</CellType>");
			sb.append("<ItemOrder>").append(ItemOrder).append("</ItemOrder>");
			sb.append("<ColumnWidth>").append(ColumnWidth).append("</ColumnWidth>");
			sb.append("<CodeNum>").append(CodeNum).append("</CodeNum>");
			sb.append("<ProcStatus>").append(ProcStatus.ordinal()).append("</ProcStatus>");
			sb.append("</ToothGridCol>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"ToothGridColNum")!=null) {
					ToothGridColNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ToothGridColNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"SheetFieldNum")!=null) {
					SheetFieldNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"SheetFieldNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"NameItem")!=null) {
					NameItem=Serializing.getXmlNodeValue(doc,"NameItem");
				}
				if(Serializing.getXmlNodeValue(doc,"CellType")!=null) {
					CellType=ToothGridCellType.values()[Integer.valueOf(Serializing.getXmlNodeValue(doc,"CellType"))];
				}
				if(Serializing.getXmlNodeValue(doc,"ItemOrder")!=null) {
					ItemOrder=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ItemOrder"));
				}
				if(Serializing.getXmlNodeValue(doc,"ColumnWidth")!=null) {
					ColumnWidth=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ColumnWidth"));
				}
				if(Serializing.getXmlNodeValue(doc,"CodeNum")!=null) {
					CodeNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"CodeNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"ProcStatus")!=null) {
					ProcStatus=ProcStat.values()[Integer.valueOf(Serializing.getXmlNodeValue(doc,"ProcStatus"))];
				}
			}
			catch(Exception e) {
				throw e;
			}
		}

		/** 0=HardCoded, 1=Tooth, 2=Surface, 3=FreeText. */
		public enum ToothGridCellType {
			/** 0 */
			HardCoded,
			/** 1 */
			Tooth,
			/** 2 */
			Surface,
			/** 3 */
			FreeText
		}

		/** Procedure Status. */
		public enum ProcStat {
			/** 1- Treatment Plan. */
			TP,
			/** 2- Complete. */
			C,
			/** 3- Existing Current Provider. */
			EC,
			/** 4- Existing Other Provider. */
			EO,
			/** 5- Referred Out. */
			R,
			/** 6- Deleted. */
			D,
			/** 7- Condition. */
			Cn
		}


}
