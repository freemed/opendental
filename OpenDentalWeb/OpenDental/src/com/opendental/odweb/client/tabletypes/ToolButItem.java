package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
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

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void DeserializeFromXml(Document doc) throws Exception {
			try {
				if(Serializing.GetXmlNodeValue(doc,"ToolButItemNum")!=null) {
					ToolButItemNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"ToolButItemNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"ProgramNum")!=null) {
					ProgramNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"ProgramNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"ToolBar")!=null) {
					ToolBar=ToolBarsAvail.values()[Integer.valueOf(Serializing.GetXmlNodeValue(doc,"ToolBar"))];
				}
				if(Serializing.GetXmlNodeValue(doc,"ButtonText")!=null) {
					ButtonText=Serializing.GetXmlNodeValue(doc,"ButtonText");
				}
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
