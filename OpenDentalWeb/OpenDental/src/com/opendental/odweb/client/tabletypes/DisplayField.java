package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

//DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD.
public class DisplayField {
		/** Primary key. */
		public int DisplayFieldNum;
		/** This is the internal name that OD uses to identify the field within this category.  This will be the default description if the user doesn't specify an alternate. */
		public String InternalName;
		/** Order to display in the grid or list. Every entry must have a unique itemorder. */
		public int ItemOrder;
		/** Optional alternate description to display for field.  Can be in another language.  For the ortho category, this is the 'key', since InternalName is blank. */
		public String Description;
		/** For grid columns, this lets user override the column width.  Especially useful for foreign languages. */
		public int ColumnWidth;
		/** Enum:DisplayFieldCategory.  If category is 0, then this is attached to a ChartView. */
		public DisplayFieldCategory Category;
		/** FK to chartview.ChartViewNum. 0 if attached to a category. */
		public int ChartViewNum;

		/** Deep copy of object. */
		public DisplayField deepCopy() {
			DisplayField displayfield=new DisplayField();
			displayfield.DisplayFieldNum=this.DisplayFieldNum;
			displayfield.InternalName=this.InternalName;
			displayfield.ItemOrder=this.ItemOrder;
			displayfield.Description=this.Description;
			displayfield.ColumnWidth=this.ColumnWidth;
			displayfield.Category=this.Category;
			displayfield.ChartViewNum=this.ChartViewNum;
			return displayfield;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<DisplayField>");
			sb.append("<DisplayFieldNum>").append(DisplayFieldNum).append("</DisplayFieldNum>");
			sb.append("<InternalName>").append(Serializing.escapeForXml(InternalName)).append("</InternalName>");
			sb.append("<ItemOrder>").append(ItemOrder).append("</ItemOrder>");
			sb.append("<Description>").append(Serializing.escapeForXml(Description)).append("</Description>");
			sb.append("<ColumnWidth>").append(ColumnWidth).append("</ColumnWidth>");
			sb.append("<Category>").append(Category.ordinal()).append("</Category>");
			sb.append("<ChartViewNum>").append(ChartViewNum).append("</ChartViewNum>");
			sb.append("</DisplayField>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"DisplayFieldNum")!=null) {
					DisplayFieldNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"DisplayFieldNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"InternalName")!=null) {
					InternalName=Serializing.getXmlNodeValue(doc,"InternalName");
				}
				if(Serializing.getXmlNodeValue(doc,"ItemOrder")!=null) {
					ItemOrder=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ItemOrder"));
				}
				if(Serializing.getXmlNodeValue(doc,"Description")!=null) {
					Description=Serializing.getXmlNodeValue(doc,"Description");
				}
				if(Serializing.getXmlNodeValue(doc,"ColumnWidth")!=null) {
					ColumnWidth=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ColumnWidth"));
				}
				if(Serializing.getXmlNodeValue(doc,"Category")!=null) {
					Category=DisplayFieldCategory.values()[Integer.valueOf(Serializing.getXmlNodeValue(doc,"Category"))];
				}
				if(Serializing.getXmlNodeValue(doc,"ChartViewNum")!=null) {
					ChartViewNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ChartViewNum"));
				}
			}
			catch(Exception e) {
				throw e;
			}
		}

		/**  */
		public enum DisplayFieldCategory {
			/** 0- This indicates progress notes. */
			None,
			/** 1 */
			PatientSelect,
			/** 2- Family module. */
			PatientInformation,
			/** 3 */
			AccountModule,
			/** 4 */
			RecallList,
			/** 5 */
			ChartPatientInformation,
			/** 6 */
			ProcedureGroupNote,
			/** 7 */
			TreatmentPlanModule,
			/** 8 */
			OrthoChart
		}


}
