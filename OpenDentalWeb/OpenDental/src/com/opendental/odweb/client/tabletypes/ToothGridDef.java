package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

//DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD.
public class ToothGridDef {
		/** Primary key. */
		public int ToothGridDefNum;
		/** FK to sheetfielddef.SheetFieldDefNum */
		public int SheetFieldDefNum;
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
		public ToothGridDef deepCopy() {
			ToothGridDef toothgriddef=new ToothGridDef();
			toothgriddef.ToothGridDefNum=this.ToothGridDefNum;
			toothgriddef.SheetFieldDefNum=this.SheetFieldDefNum;
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
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<ToothGridDef>");
			sb.append("<ToothGridDefNum>").append(ToothGridDefNum).append("</ToothGridDefNum>");
			sb.append("<SheetFieldDefNum>").append(SheetFieldDefNum).append("</SheetFieldDefNum>");
			sb.append("<NameInternal>").append(Serializing.escapeForXml(NameInternal)).append("</NameInternal>");
			sb.append("<NameShowing>").append(Serializing.escapeForXml(NameShowing)).append("</NameShowing>");
			sb.append("<CellType>").append(CellType.ordinal()).append("</CellType>");
			sb.append("<ItemOrder>").append(ItemOrder).append("</ItemOrder>");
			sb.append("<ColumnWidth>").append(ColumnWidth).append("</ColumnWidth>");
			sb.append("<CodeNum>").append(CodeNum).append("</CodeNum>");
			sb.append("<ProcStatus>").append(ProcStatus.ordinal()).append("</ProcStatus>");
			sb.append("</ToothGridDef>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"ToothGridDefNum")!=null) {
					ToothGridDefNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ToothGridDefNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"SheetFieldDefNum")!=null) {
					SheetFieldDefNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"SheetFieldDefNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"NameInternal")!=null) {
					NameInternal=Serializing.getXmlNodeValue(doc,"NameInternal");
				}
				if(Serializing.getXmlNodeValue(doc,"NameShowing")!=null) {
					NameShowing=Serializing.getXmlNodeValue(doc,"NameShowing");
				}
				if(Serializing.getXmlNodeValue(doc,"CellType")!=null) {
					CellType=ToothGridCellType.valueOf(Serializing.getXmlNodeValue(doc,"CellType"));
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
					ProcStatus=ProcStat.valueOf(Serializing.getXmlNodeValue(doc,"ProcStatus"));
				}
			}
			catch(Exception e) {
				throw new Exception("Error deserializing ToothGridDef: "+e.getMessage());
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
