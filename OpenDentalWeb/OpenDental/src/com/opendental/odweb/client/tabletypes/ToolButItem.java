package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

public class ToolButItem {
		/** Primary key. */
		public int ToolButItemNum;
		/** FK to program.ProgramNum. */
		public int ProgramNum;
		/** Enum:ToolBarsAvail The toolbar to show the button on. */
		public ToolBarsAvail ToolBar;
		/** The text to show on the toolbar button. */
		public String ButtonText;

		/** Deep copy of object. */
		public ToolButItem Copy() {
			ToolButItem toolbutitem=new ToolButItem();
			toolbutitem.ToolButItemNum=this.ToolButItemNum;
			toolbutitem.ProgramNum=this.ProgramNum;
			toolbutitem.ToolBar=this.ToolBar;
			toolbutitem.ButtonText=this.ButtonText;
			return toolbutitem;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<ToolButItem>");
			sb.append("<ToolButItemNum>").append(ToolButItemNum).append("</ToolButItemNum>");
			sb.append("<ProgramNum>").append(ProgramNum).append("</ProgramNum>");
			sb.append("<ToolBar>").append(ToolBar.ordinal()).append("</ToolBar>");
			sb.append("<ButtonText>").append(Serializing.EscapeForXml(ButtonText)).append("</ButtonText>");
			sb.append("</ToolButItem>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				ToolButItemNum=Integer.valueOf(doc.getElementsByTagName("ToolButItemNum").item(0).getFirstChild().getNodeValue());
				ProgramNum=Integer.valueOf(doc.getElementsByTagName("ProgramNum").item(0).getFirstChild().getNodeValue());
				ToolBar=ToolBarsAvail.values()[Integer.valueOf(doc.getElementsByTagName("ToolBar").item(0).getFirstChild().getNodeValue())];
				ButtonText=doc.getElementsByTagName("ButtonText").item(0).getFirstChild().getNodeValue();
			}
			catch(Exception e) {
				throw e;
			}
		}

		/**  */
		public enum ToolBarsAvail {
			/** 0 */
			AccountModule,
			/** 1 */
			ApptModule,
			/** 2 */
			ChartModule,
			/** 3 */
			ImagesModule,
			/** 4 */
			FamilyModule,
			/** 5 */
			TreatmentPlanModule,
			/** 6 */
			ClaimsSend,
			/** 7 Shows in the toolbar at the top that is common to all modules. */
			AllModules
		}


}
