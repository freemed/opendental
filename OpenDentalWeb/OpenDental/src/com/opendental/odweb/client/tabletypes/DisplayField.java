package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

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
		public DisplayField Copy() {
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
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<DisplayField>");
			sb.append("<DisplayFieldNum>").append(DisplayFieldNum).append("</DisplayFieldNum>");
			sb.append("<InternalName>").append(Serializing.EscapeForXml(InternalName)).append("</InternalName>");
			sb.append("<ItemOrder>").append(ItemOrder).append("</ItemOrder>");
			sb.append("<Description>").append(Serializing.EscapeForXml(Description)).append("</Description>");
			sb.append("<ColumnWidth>").append(ColumnWidth).append("</ColumnWidth>");
			sb.append("<Category>").append(Category.ordinal()).append("</Category>");
			sb.append("<ChartViewNum>").append(ChartViewNum).append("</ChartViewNum>");
			sb.append("</DisplayField>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				if(Serializing.GetXmlNodeValue(doc,"DisplayFieldNum")!=null) {
					DisplayFieldNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"DisplayFieldNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"InternalName")!=null) {
					InternalName=Serializing.GetXmlNodeValue(doc,"InternalName");
				}
				if(Serializing.GetXmlNodeValue(doc,"ItemOrder")!=null) {
					ItemOrder=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"ItemOrder"));
				}
				if(Serializing.GetXmlNodeValue(doc,"Description")!=null) {
					Description=Serializing.GetXmlNodeValue(doc,"Description");
				}
				if(Serializing.GetXmlNodeValue(doc,"ColumnWidth")!=null) {
					ColumnWidth=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"ColumnWidth"));
				}
				if(Serializing.GetXmlNodeValue(doc,"Category")!=null) {
					Category=DisplayFieldCategory.values()[Integer.valueOf(Serializing.GetXmlNodeValue(doc,"Category"))];
				}
				if(Serializing.GetXmlNodeValue(doc,"ChartViewNum")!=null) {
					ChartViewNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"ChartViewNum"));
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
