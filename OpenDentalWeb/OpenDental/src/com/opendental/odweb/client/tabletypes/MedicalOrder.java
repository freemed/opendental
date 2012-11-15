package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

public class MedicalOrder {
		/** Primary key. */
		public int MedicalOrderNum;
		/** Enum:MedicalOrderType Laboratory=0,Radiology=1. */
		public MedicalOrderType MedOrderType;
		/** FK to patient.PatNum */
		public int PatNum;
		/** Date and time of order. */
		public Date DateTimeOrder;
		/** User will be required to type entire order out from scratch. */
		public String Description;
		/** EHR requires Active/Discontinued status. 0=Active, 1=Discontinued. */
		public boolean IsDiscontinued;
		/** FK to provider.ProvNum. */
		public int ProvNum;

		/** Deep copy of object. */
		public MedicalOrder Copy() {
			MedicalOrder medicalorder=new MedicalOrder();
			medicalorder.MedicalOrderNum=this.MedicalOrderNum;
			medicalorder.MedOrderType=this.MedOrderType;
			medicalorder.PatNum=this.PatNum;
			medicalorder.DateTimeOrder=this.DateTimeOrder;
			medicalorder.Description=this.Description;
			medicalorder.IsDiscontinued=this.IsDiscontinued;
			medicalorder.ProvNum=this.ProvNum;
			return medicalorder;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<MedicalOrder>");
			sb.append("<MedicalOrderNum>").append(MedicalOrderNum).append("</MedicalOrderNum>");
			sb.append("<MedOrderType>").append(MedOrderType.ordinal()).append("</MedOrderType>");
			sb.append("<PatNum>").append(PatNum).append("</PatNum>");
			sb.append("<DateTimeOrder>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateTimeOrder)).append("</DateTimeOrder>");
			sb.append("<Description>").append(Serializing.EscapeForXml(Description)).append("</Description>");
			sb.append("<IsDiscontinued>").append((IsDiscontinued)?1:0).append("</IsDiscontinued>");
			sb.append("<ProvNum>").append(ProvNum).append("</ProvNum>");
			sb.append("</MedicalOrder>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				MedicalOrderNum=Integer.valueOf(doc.getElementsByTagName("MedicalOrderNum").item(0).getFirstChild().getNodeValue());
				MedOrderType=MedicalOrderType.values()[Integer.valueOf(doc.getElementsByTagName("MedOrderType").item(0).getFirstChild().getNodeValue())];
				PatNum=Integer.valueOf(doc.getElementsByTagName("PatNum").item(0).getFirstChild().getNodeValue());
				DateTimeOrder=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(doc.getElementsByTagName("DateTimeOrder").item(0).getFirstChild().getNodeValue());
				Description=doc.getElementsByTagName("Description").item(0).getFirstChild().getNodeValue();
				IsDiscontinued=(doc.getElementsByTagName("IsDiscontinued").item(0).getFirstChild().getNodeValue()=="0")?false:true;
				ProvNum=Integer.valueOf(doc.getElementsByTagName("ProvNum").item(0).getFirstChild().getNodeValue());
			}
			catch(Exception e) {
				throw e;
			}
		}

		/**  */
		public enum MedicalOrderType {
			/** 0- Laboratory */
			Laboratory,
			/** 1- Radiology */
			Radiology
		}


}
