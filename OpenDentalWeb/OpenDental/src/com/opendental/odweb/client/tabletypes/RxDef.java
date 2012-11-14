package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
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
		public RxDef Copy() {
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
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<RxDef>");
			sb.append("<RxDefNum>").append(RxDefNum).append("</RxDefNum>");
			sb.append("<Drug>").append(Serializing.EscapeForXml(Drug)).append("</Drug>");
			sb.append("<Sig>").append(Serializing.EscapeForXml(Sig)).append("</Sig>");
			sb.append("<Disp>").append(Serializing.EscapeForXml(Disp)).append("</Disp>");
			sb.append("<Refills>").append(Serializing.EscapeForXml(Refills)).append("</Refills>");
			sb.append("<Notes>").append(Serializing.EscapeForXml(Notes)).append("</Notes>");
			sb.append("<IsControlled>").append((IsControlled)?1:0).append("</IsControlled>");
			sb.append("<RxCui>").append(RxCui).append("</RxCui>");
			sb.append("</RxDef>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				RxDefNum=Integer.valueOf(doc.getElementsByTagName("RxDefNum").item(0).getFirstChild().getNodeValue());
				Drug=doc.getElementsByTagName("Drug").item(0).getFirstChild().getNodeValue();
				Sig=doc.getElementsByTagName("Sig").item(0).getFirstChild().getNodeValue();
				Disp=doc.getElementsByTagName("Disp").item(0).getFirstChild().getNodeValue();
				Refills=doc.getElementsByTagName("Refills").item(0).getFirstChild().getNodeValue();
				Notes=doc.getElementsByTagName("Notes").item(0).getFirstChild().getNodeValue();
				IsControlled=(doc.getElementsByTagName("IsControlled").item(0).getFirstChild().getNodeValue()=="0")?false:true;
				RxCui=Integer.valueOf(doc.getElementsByTagName("RxCui").item(0).getFirstChild().getNodeValue());
			}
			catch(Exception e) {
				throw e;
			}
		}


}
