package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

public class RxDef {
		/** Primary key. */
		public int RxDefNum;
		/** The name of the drug. */
		public String Drug;
		/** Directions. */
		public String Sig;
		/** Amount to dispense. */
		public String Disp;
		/** Number of refills. */
		public String Refills;
		/** Notes about this drug. Will not be copied to the rxpat. */
		public String Notes;
		/** Is a controlled substance.  This will affect the way it prints. */
		public boolean IsControlled;
		/** RxNorm Code identifier.  This is used to enhance the RxAlert functionality.  Usually, RxAlerts are triggered when an Rx is entered from an RxDef.  But if an alert needs to be triggered when entering a medication through the ehr CPOE, then this RxCui matching the RxCui of the medication is the trigger.  This is clearly not practical because there are so many RxCuis that an exact match would be extremely rare.  So the only reason this field is here is to pass ehr certification. */
		public int RxCui;

		/** Deep copy of object. */
		public RxDef deepCopy() {
			RxDef rxdef=new RxDef();
			rxdef.RxDefNum=this.RxDefNum;
			rxdef.Drug=this.Drug;
			rxdef.Sig=this.Sig;
			rxdef.Disp=this.Disp;
			rxdef.Refills=this.Refills;
			rxdef.Notes=this.Notes;
			rxdef.IsControlled=this.IsControlled;
			rxdef.RxCui=this.RxCui;
			return rxdef;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<RxDef>");
			sb.append("<RxDefNum>").append(RxDefNum).append("</RxDefNum>");
			sb.append("<Drug>").append(Serializing.escapeForXml(Drug)).append("</Drug>");
			sb.append("<Sig>").append(Serializing.escapeForXml(Sig)).append("</Sig>");
			sb.append("<Disp>").append(Serializing.escapeForXml(Disp)).append("</Disp>");
			sb.append("<Refills>").append(Serializing.escapeForXml(Refills)).append("</Refills>");
			sb.append("<Notes>").append(Serializing.escapeForXml(Notes)).append("</Notes>");
			sb.append("<IsControlled>").append((IsControlled)?1:0).append("</IsControlled>");
			sb.append("<RxCui>").append(RxCui).append("</RxCui>");
			sb.append("</RxDef>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"RxDefNum")!=null) {
					RxDefNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"RxDefNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"Drug")!=null) {
					Drug=Serializing.getXmlNodeValue(doc,"Drug");
				}
				if(Serializing.getXmlNodeValue(doc,"Sig")!=null) {
					Sig=Serializing.getXmlNodeValue(doc,"Sig");
				}
				if(Serializing.getXmlNodeValue(doc,"Disp")!=null) {
					Disp=Serializing.getXmlNodeValue(doc,"Disp");
				}
				if(Serializing.getXmlNodeValue(doc,"Refills")!=null) {
					Refills=Serializing.getXmlNodeValue(doc,"Refills");
				}
				if(Serializing.getXmlNodeValue(doc,"Notes")!=null) {
					Notes=Serializing.getXmlNodeValue(doc,"Notes");
				}
				if(Serializing.getXmlNodeValue(doc,"IsControlled")!=null) {
					IsControlled=(Serializing.getXmlNodeValue(doc,"IsControlled")=="0")?false:true;
				}
				if(Serializing.getXmlNodeValue(doc,"RxCui")!=null) {
					RxCui=Integer.valueOf(Serializing.getXmlNodeValue(doc,"RxCui"));
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
