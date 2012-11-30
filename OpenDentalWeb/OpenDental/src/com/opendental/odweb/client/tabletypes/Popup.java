package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

public class Popup {
		/** Primary key. */
		public int PopupNum;
		/** FK to patient.PatNum.  If PopupLevel is Family/SuperFamily then this must be a guarantor/super family head. */
		public int PatNum;
		/** The text of the popup. */
		public String Description;
		/** If true, then the popup won't ever automatically show. */
		public boolean IsDisabled;
		/** Enum:EnumPopupFamily 0=Patient, 1=Family, 2=Superfamily. If Family, then this Popup will apply to the entire family and PatNum will the Guarantor PatNum.  If Superfamily, then this popup will apply to the entire superfamily and PatNum will be the head of the superfamily. This column will need to be synched for all family actions where the guarantor changes.   */
		public EnumPopupLevel PopupLevel;

		/** Deep copy of object. */
		public Popup Copy() {
			Popup popup=new Popup();
			popup.PopupNum=this.PopupNum;
			popup.PatNum=this.PatNum;
			popup.Description=this.Description;
			popup.IsDisabled=this.IsDisabled;
			popup.PopupLevel=this.PopupLevel;
			return popup;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<Popup>");
			sb.append("<PopupNum>").append(PopupNum).append("</PopupNum>");
			sb.append("<PatNum>").append(PatNum).append("</PatNum>");
			sb.append("<Description>").append(Serializing.EscapeForXml(Description)).append("</Description>");
			sb.append("<IsDisabled>").append((IsDisabled)?1:0).append("</IsDisabled>");
			sb.append("<PopupLevel>").append(PopupLevel.ordinal()).append("</PopupLevel>");
			sb.append("</Popup>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void DeserializeFromXml(Document doc) throws Exception {
			try {
				if(Serializing.GetXmlNodeValue(doc,"PopupNum")!=null) {
					PopupNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"PopupNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"PatNum")!=null) {
					PatNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"PatNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"Description")!=null) {
					Description=Serializing.GetXmlNodeValue(doc,"Description");
				}
				if(Serializing.GetXmlNodeValue(doc,"IsDisabled")!=null) {
					IsDisabled=(Serializing.GetXmlNodeValue(doc,"IsDisabled")=="0")?false:true;
				}
				if(Serializing.GetXmlNodeValue(doc,"PopupLevel")!=null) {
					PopupLevel=EnumPopupLevel.values()[Integer.valueOf(Serializing.GetXmlNodeValue(doc,"PopupLevel"))];
				}
			}
			catch(Exception e) {
				throw e;
			}
		}

		/**  */
		public enum EnumPopupLevel {
			/** 0=Patient */
			Patient,
			/** 1=Family */
			Family,
			/** 3=SuperFamily */
			SuperFamily
		}


}
