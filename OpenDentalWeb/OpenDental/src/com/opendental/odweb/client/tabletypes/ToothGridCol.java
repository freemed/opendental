package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

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
		public ToothGridCol Copy() {
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
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<ToothGridCol>");
			sb.append("<ToothGridColNum>").append(ToothGridColNum).append("</ToothGridColNum>");
			sb.append("<SheetFieldNum>").append(SheetFieldNum).append("</SheetFieldNum>");
			sb.append("<NameItem>").append(Serializing.EscapeForXml(NameItem)).append("</NameItem>");
			sb.append("<CellType>").append(CellType.ordinal()).append("</CellType>");
			sb.append("<ItemOrder>").append(ItemOrder).append("</ItemOrder>");
			sb.append("<ColumnWidth>").append(ColumnWidth).append("</ColumnWidth>");
			sb.append("<CodeNum>").append(CodeNum).append("</CodeNum>");
			sb.append("<ProcStatus>").append(ProcStatus.ordinal()).append("</ProcStatus>");
			sb.append("</ToothGridCol>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				ToothGridColNum=Integer.valueOf(doc.getElementsByTagName("ToothGridColNum").item(0).getFirstChild().getNodeValue());
				SheetFieldNum=Integer.valueOf(doc.getElementsByTagName("SheetFieldNum").item(0).getFirstChild().getNodeValue());
				NameItem=doc.getElementsByTagName("NameItem").item(0).getFirstChild().getNodeValue();
				CellType=ToothGridCellType.values()[Integer.valueOf(doc.getElementsByTagName("CellType").item(0).getFirstChild().getNodeValue())];
				ItemOrder=Integer.valueOf(doc.getElementsByTagName("ItemOrder").item(0).getFirstChild().getNodeValue());
				ColumnWidth=Integer.valueOf(doc.getElementsByTagName("ColumnWidth").item(0).getFirstChild().getNodeValue());
				CodeNum=Integer.valueOf(doc.getElementsByTagName("CodeNum").item(0).getFirstChild().getNodeValue());
				ProcStatus=ProcStat.values()[Integer.valueOf(doc.getElementsByTagName("ProcStatus").item(0).getFirstChild().getNodeValue())];
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
