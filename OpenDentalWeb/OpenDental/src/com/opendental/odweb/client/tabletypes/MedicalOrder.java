package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
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

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void DeserializeFromXml(Document doc) throws Exception {
			try {
				if(Serializing.GetXmlNodeValue(doc,"MedicalOrderNum")!=null) {
					MedicalOrderNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"MedicalOrderNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"MedOrderType")!=null) {
					MedOrderType=MedicalOrderType.values()[Integer.valueOf(Serializing.GetXmlNodeValue(doc,"MedOrderType"))];
				}
				if(Serializing.GetXmlNodeValue(doc,"PatNum")!=null) {
					PatNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"PatNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"DateTimeOrder")!=null) {
					DateTimeOrder=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.GetXmlNodeValue(doc,"DateTimeOrder"));
				}
				if(Serializing.GetXmlNodeValue(doc,"Description")!=null) {
					Description=Serializing.GetXmlNodeValue(doc,"Description");
				}
				if(Serializing.GetXmlNodeValue(doc,"IsDiscontinued")!=null) {
					IsDiscontinued=(Serializing.GetXmlNodeValue(doc,"IsDiscontinued")=="0")?false:true;
				}
				if(Serializing.GetXmlNodeValue(doc,"ProvNum")!=null) {
					ProvNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"ProvNum"));
				}
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
