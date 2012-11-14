package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;

public class ApptView {
		/** Primary key. */
		public int ApptViewNum;
		/** Description of this view.  Gets displayed in Appt module. */
		public String Description;
		/** 0-based order to display in lists. Every view must have a unique itemorder, but it is acceptable to have some missing itemorders in the sequence. */
		public int ItemOrder;
		/** Number of rows per time increment.  Usually 1 or 2.  Programming note: Value updated to ApptDrawing.RowsPerIncr to track current state. */
		public byte RowsPerIncr;
		/** If set to true, then the only operatories that will show will be for providers that have schedules for the day, ops with no provs assigned. */
		public boolean OnlyScheduledProvs;
		/** If OnlyScheduledProvs is set to true, and this time is not 0:00, then only provider schedules with start or stop time before this time will be included. */
		public String OnlySchedBeforeTime;
		/** If OnlyScheduledProvs is set to true, and this time is not 0:00, then only provider schedules with start or stop time after this time will be included. */
		public String OnlySchedAfterTime;
		/** Enum:ApptViewStackBehavior  */
		public ApptViewStackBehavior StackBehavUR;
		/** Enum:ApptViewStackBehavior  */
		public ApptViewStackBehavior StackBehavLR;
		/** FK to clinic.ClinicNum.  0=All clinics.  If OnlyScheduledProvs is set to true, then only provider schedules with matching clinic will be included. */
		public int ClinicNum;

		/** Deep copy of object. */
		public ApptView Copy() {
			ApptView apptview=new ApptView();
			apptview.ApptViewNum=this.ApptViewNum;
			apptview.Description=this.Description;
			apptview.ItemOrder=this.ItemOrder;
			apptview.RowsPerIncr=this.RowsPerIncr;
			apptview.OnlyScheduledProvs=this.OnlyScheduledProvs;
			apptview.OnlySchedBeforeTime=this.OnlySchedBeforeTime;
			apptview.OnlySchedAfterTime=this.OnlySchedAfterTime;
			apptview.StackBehavUR=this.StackBehavUR;
			apptview.StackBehavLR=this.StackBehavLR;
			apptview.ClinicNum=this.ClinicNum;
			return apptview;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<ApptView>");
			sb.append("<ApptViewNum>").append(ApptViewNum).append("</ApptViewNum>");
			sb.append("<Description>").append(Serializing.EscapeForXml(Description)).append("</Description>");
			sb.append("<ItemOrder>").append(ItemOrder).append("</ItemOrder>");
			sb.append("<RowsPerIncr>").append(RowsPerIncr).append("</RowsPerIncr>");
			sb.append("<OnlyScheduledProvs>").append((OnlyScheduledProvs)?1:0).append("</OnlyScheduledProvs>");
			sb.append("<OnlySchedBeforeTime>").append(Serializing.EscapeForXml(OnlySchedBeforeTime)).append("</OnlySchedBeforeTime>");
			sb.append("<OnlySchedAfterTime>").append(Serializing.EscapeForXml(OnlySchedAfterTime)).append("</OnlySchedAfterTime>");
			sb.append("<StackBehavUR>").append(StackBehavUR.ordinal()).append("</StackBehavUR>");
			sb.append("<StackBehavLR>").append(StackBehavLR.ordinal()).append("</StackBehavLR>");
			sb.append("<ClinicNum>").append(ClinicNum).append("</ClinicNum>");
			sb.append("</ApptView>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				ApptViewNum=Integer.valueOf(doc.getElementsByTagName("ApptViewNum").item(0).getFirstChild().getNodeValue());
				Description=doc.getElementsByTagName("Description").item(0).getFirstChild().getNodeValue();
				ItemOrder=Integer.valueOf(doc.getElementsByTagName("ItemOrder").item(0).getFirstChild().getNodeValue());
				RowsPerIncr=Byte.valueOf(doc.getElementsByTagName("RowsPerIncr").item(0).getFirstChild().getNodeValue());
				OnlyScheduledProvs=(doc.getElementsByTagName("OnlyScheduledProvs").item(0).getFirstChild().getNodeValue()=="0")?false:true;
				OnlySchedBeforeTime=doc.getElementsByTagName("OnlySchedBeforeTime").item(0).getFirstChild().getNodeValue();
				OnlySchedAfterTime=doc.getElementsByTagName("OnlySchedAfterTime").item(0).getFirstChild().getNodeValue();
				StackBehavUR=ApptViewStackBehavior.values()[Integer.valueOf(doc.getElementsByTagName("StackBehavUR").item(0).getFirstChild().getNodeValue())];
				StackBehavLR=ApptViewStackBehavior.values()[Integer.valueOf(doc.getElementsByTagName("StackBehavLR").item(0).getFirstChild().getNodeValue())];
				ClinicNum=Integer.valueOf(doc.getElementsByTagName("ClinicNum").item(0).getFirstChild().getNodeValue());
			}
			catch(Exception e) {
				throw e;
			}
		}

		/**  */
		public enum ApptViewStackBehavior {
			/**  */
			Vertical,
			/**  */
			Horizontal
		}


}
