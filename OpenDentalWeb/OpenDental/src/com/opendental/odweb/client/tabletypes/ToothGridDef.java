package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
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

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void DeserializeFromXml(Document doc) throws Exception {
			try {
				if(Serializing.GetXmlNodeValue(doc,"ToothGridDefNum")!=null) {
					ToothGridDefNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"ToothGridDefNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"NameInternal")!=null) {
					NameInternal=Serializing.GetXmlNodeValue(doc,"NameInternal");
				}
				if(Serializing.GetXmlNodeValue(doc,"NameShowing")!=null) {
					NameShowing=Serializing.GetXmlNodeValue(doc,"NameShowing");
				}
				if(Serializing.GetXmlNodeValue(doc,"CellType")!=null) {
					CellType=ToothGridCellType.values()[Integer.valueOf(Serializing.GetXmlNodeValue(doc,"CellType"))];
				}
				if(Serializing.GetXmlNodeValue(doc,"ItemOrder")!=null) {
					ItemOrder=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"ItemOrder"));
				}
				if(Serializing.GetXmlNodeValue(doc,"ColumnWidth")!=null) {
					ColumnWidth=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"ColumnWidth"));
				}
				if(Serializing.GetXmlNodeValue(doc,"CodeNum")!=null) {
					CodeNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"CodeNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"ProcStatus")!=null) {
					ProcStatus=ProcStat.values()[Integer.valueOf(Serializing.GetXmlNodeValue(doc,"ProcStatus"))];
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
