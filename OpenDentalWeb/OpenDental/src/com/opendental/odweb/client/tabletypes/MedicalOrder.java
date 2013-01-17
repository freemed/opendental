package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

//DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD.
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
		public MedicalOrder deepCopy() {
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
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<MedicalOrder>");
			sb.append("<MedicalOrderNum>").append(MedicalOrderNum).append("</MedicalOrderNum>");
			sb.append("<MedOrderType>").append(MedOrderType.ordinal()).append("</MedOrderType>");
			sb.append("<PatNum>").append(PatNum).append("</PatNum>");
			sb.append("<DateTimeOrder>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateTimeOrder)).append("</DateTimeOrder>");
			sb.append("<Description>").append(Serializing.escapeForXml(Description)).append("</Description>");
			sb.append("<IsDiscontinued>").append((IsDiscontinued)?1:0).append("</IsDiscontinued>");
			sb.append("<ProvNum>").append(ProvNum).append("</ProvNum>");
			sb.append("</MedicalOrder>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"MedicalOrderNum")!=null) {
					MedicalOrderNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"MedicalOrderNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"MedOrderType")!=null) {
					MedOrderType=MedicalOrderType.valueOf(Serializing.getXmlNodeValue(doc,"MedOrderType"));
				}
				if(Serializing.getXmlNodeValue(doc,"PatNum")!=null) {
					PatNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"PatNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"DateTimeOrder")!=null) {
					DateTimeOrder=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"DateTimeOrder"));
				}
				if(Serializing.getXmlNodeValue(doc,"Description")!=null) {
					Description=Serializing.getXmlNodeValue(doc,"Description");
				}
				if(Serializing.getXmlNodeValue(doc,"IsDiscontinued")!=null) {
					IsDiscontinued=(Serializing.getXmlNodeValue(doc,"IsDiscontinued")=="0")?false:true;
				}
				if(Serializing.getXmlNodeValue(doc,"ProvNum")!=null) {
					ProvNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ProvNum"));
				}
			}
			catch(Exception e) {
				throw new Exception("Error deserializing MedicalOrder: "+e.getMessage());
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
