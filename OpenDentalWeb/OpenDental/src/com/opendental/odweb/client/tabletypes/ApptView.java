package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

//DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD.
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
		public ApptView deepCopy() {
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
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<ApptView>");
			sb.append("<ApptViewNum>").append(ApptViewNum).append("</ApptViewNum>");
			sb.append("<Description>").append(Serializing.escapeForXml(Description)).append("</Description>");
			sb.append("<ItemOrder>").append(ItemOrder).append("</ItemOrder>");
			sb.append("<RowsPerIncr>").append(RowsPerIncr).append("</RowsPerIncr>");
			sb.append("<OnlyScheduledProvs>").append((OnlyScheduledProvs)?1:0).append("</OnlyScheduledProvs>");
			sb.append("<OnlySchedBeforeTime>").append(Serializing.escapeForXml(OnlySchedBeforeTime)).append("</OnlySchedBeforeTime>");
			sb.append("<OnlySchedAfterTime>").append(Serializing.escapeForXml(OnlySchedAfterTime)).append("</OnlySchedAfterTime>");
			sb.append("<StackBehavUR>").append(StackBehavUR.ordinal()).append("</StackBehavUR>");
			sb.append("<StackBehavLR>").append(StackBehavLR.ordinal()).append("</StackBehavLR>");
			sb.append("<ClinicNum>").append(ClinicNum).append("</ClinicNum>");
			sb.append("</ApptView>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"ApptViewNum")!=null) {
					ApptViewNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ApptViewNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"Description")!=null) {
					Description=Serializing.getXmlNodeValue(doc,"Description");
				}
				if(Serializing.getXmlNodeValue(doc,"ItemOrder")!=null) {
					ItemOrder=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ItemOrder"));
				}
				if(Serializing.getXmlNodeValue(doc,"RowsPerIncr")!=null) {
					RowsPerIncr=Byte.valueOf(Serializing.getXmlNodeValue(doc,"RowsPerIncr"));
				}
				if(Serializing.getXmlNodeValue(doc,"OnlyScheduledProvs")!=null) {
					OnlyScheduledProvs=(Serializing.getXmlNodeValue(doc,"OnlyScheduledProvs")=="0")?false:true;
				}
				if(Serializing.getXmlNodeValue(doc,"OnlySchedBeforeTime")!=null) {
					OnlySchedBeforeTime=Serializing.getXmlNodeValue(doc,"OnlySchedBeforeTime");
				}
				if(Serializing.getXmlNodeValue(doc,"OnlySchedAfterTime")!=null) {
					OnlySchedAfterTime=Serializing.getXmlNodeValue(doc,"OnlySchedAfterTime");
				}
				if(Serializing.getXmlNodeValue(doc,"StackBehavUR")!=null) {
					StackBehavUR=ApptViewStackBehavior.values()[Integer.valueOf(Serializing.getXmlNodeValue(doc,"StackBehavUR"))];
				}
				if(Serializing.getXmlNodeValue(doc,"StackBehavLR")!=null) {
					StackBehavLR=ApptViewStackBehavior.values()[Integer.valueOf(Serializing.getXmlNodeValue(doc,"StackBehavLR"))];
				}
				if(Serializing.getXmlNodeValue(doc,"ClinicNum")!=null) {
					ClinicNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ClinicNum"));
				}
			}
			catch(Exception e) {
				throw new Exception("Error deserializing ApptView: "+e.getMessage());
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
