package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

public class ToothGridDef {
		/** Primary key. */
		public int ToothGridDefNum;
		/** This is the internal name that OD uses to identify the column.  Blank if this is a user-defined column.  We will keep a hard-coded list of available NameInternals in the code to pick from. */
		public String NameInternal;
		/** The user may override the internal name for display purposes.  If this is a user-defined column, this is the only name, since there is no NameInternal. */
		public String NameShowing;
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
		public ToothGridDef Copy() {
			ToothGridDef toothgriddef=new ToothGridDef();
			toothgriddef.ToothGridDefNum=this.ToothGridDefNum;
			toothgriddef.NameInternal=this.NameInternal;
			toothgriddef.NameShowing=this.NameShowing;
			toothgriddef.CellType=this.CellType;
			toothgriddef.ItemOrder=this.ItemOrder;
			toothgriddef.ColumnWidth=this.ColumnWidth;
			toothgriddef.CodeNum=this.CodeNum;
			toothgriddef.ProcStatus=this.ProcStatus;
			return toothgriddef;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<ToothGridDef>");
			sb.append("<ToothGridDefNum>").append(ToothGridDefNum).append("</ToothGridDefNum>");
			sb.append("<NameInternal>").append(Serializing.EscapeForXml(NameInternal)).append("</NameInternal>");
			sb.append("<NameShowing>").append(Serializing.EscapeForXml(NameShowing)).append("</NameShowing>");
			sb.append("<CellType>").append(CellType.ordinal()).append("</CellType>");
			sb.append("<ItemOrder>").append(ItemOrder).append("</ItemOrder>");
			sb.append("<ColumnWidth>").append(ColumnWidth).append("</ColumnWidth>");
			sb.append("<CodeNum>").append(CodeNum).append("</CodeNum>");
			sb.append("<ProcStatus>").append(ProcStatus.ordinal()).append("</ProcStatus>");
			sb.append("</ToothGridDef>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				ToothGridDefNum=Integer.valueOf(doc.getElementsByTagName("ToothGridDefNum").item(0).getFirstChild().getNodeValue());
				NameInternal=doc.getElementsByTagName("NameInternal").item(0).getFirstChild().getNodeValue();
				NameShowing=doc.getElementsByTagName("NameShowing").item(0).getFirstChild().getNodeValue();
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
