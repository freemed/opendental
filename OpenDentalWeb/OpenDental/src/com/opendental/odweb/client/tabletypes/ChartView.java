package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;

/** DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD. */
public class ChartView {
		/** Primary key. */
		public int ChartViewNum;
		/** Description of this view.  Gets displayed at top of Progress Notes grid. */
		public String Description;
		/** 0-based order to display in lists. */
		public int ItemOrder;
		/** Enum:ChartViewProcStat None=0,TP=1,Complete=2,Existing Cur Prov=4,Existing Other Prov=8,Referred=16,Deleted=32,Condition=64,All=127. */
		public ChartViewProcStat ProcStatuses;
		/** Enum:ChartViewObjs None=0,Appointments=1,Comm Log=2,Comm Log Family=4,Tasks=8,Email=16,LabCases=32,Rx=64,Sheets=128,All=255. */
		public ChartViewObjs ObjectTypes;
		/** Set true to show procedure notes. */
		public boolean ShowProcNotes;
		/** Set true to enable audit mode. */
		public boolean IsAudit;
		/** Set true to only show information regarding the selected teeth. */
		public boolean SelectedTeethOnly;
		/** Enum:OrionStatus Which orion statuses to show. Will be zero if not orion. */
		public OrionStatus OrionStatusFlags;
		/** Enum:ChartViewDates  */
		public ChartViewDates DatesShowing;

		/** Deep copy of object. */
		public ChartView deepCopy() {
			ChartView chartview=new ChartView();
			chartview.ChartViewNum=this.ChartViewNum;
			chartview.Description=this.Description;
			chartview.ItemOrder=this.ItemOrder;
			chartview.ProcStatuses=this.ProcStatuses;
			chartview.ObjectTypes=this.ObjectTypes;
			chartview.ShowProcNotes=this.ShowProcNotes;
			chartview.IsAudit=this.IsAudit;
			chartview.SelectedTeethOnly=this.SelectedTeethOnly;
			chartview.OrionStatusFlags=this.OrionStatusFlags;
			chartview.DatesShowing=this.DatesShowing;
			return chartview;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<ChartView>");
			sb.append("<ChartViewNum>").append(ChartViewNum).append("</ChartViewNum>");
			sb.append("<Description>").append(Serializing.escapeForXml(Description)).append("</Description>");
			sb.append("<ItemOrder>").append(ItemOrder).append("</ItemOrder>");
			sb.append("<ProcStatuses>").append(ProcStatuses.ordinal()).append("</ProcStatuses>");
			sb.append("<ObjectTypes>").append(ObjectTypes.ordinal()).append("</ObjectTypes>");
			sb.append("<ShowProcNotes>").append((ShowProcNotes)?1:0).append("</ShowProcNotes>");
			sb.append("<IsAudit>").append((IsAudit)?1:0).append("</IsAudit>");
			sb.append("<SelectedTeethOnly>").append((SelectedTeethOnly)?1:0).append("</SelectedTeethOnly>");
			sb.append("<OrionStatusFlags>").append(OrionStatusFlags.ordinal()).append("</OrionStatusFlags>");
			sb.append("<DatesShowing>").append(DatesShowing.ordinal()).append("</DatesShowing>");
			sb.append("</ChartView>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"ChartViewNum")!=null) {
					ChartViewNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ChartViewNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"Description")!=null) {
					Description=Serializing.getXmlNodeValue(doc,"Description");
				}
				if(Serializing.getXmlNodeValue(doc,"ItemOrder")!=null) {
					ItemOrder=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ItemOrder"));
				}
				if(Serializing.getXmlNodeValue(doc,"ProcStatuses")!=null) {
					ProcStatuses=ChartViewProcStat.values()[Integer.valueOf(Serializing.getXmlNodeValue(doc,"ProcStatuses"))];
				}
				if(Serializing.getXmlNodeValue(doc,"ObjectTypes")!=null) {
					ObjectTypes=ChartViewObjs.values()[Integer.valueOf(Serializing.getXmlNodeValue(doc,"ObjectTypes"))];
				}
				if(Serializing.getXmlNodeValue(doc,"ShowProcNotes")!=null) {
					ShowProcNotes=(Serializing.getXmlNodeValue(doc,"ShowProcNotes")=="0")?false:true;
				}
				if(Serializing.getXmlNodeValue(doc,"IsAudit")!=null) {
					IsAudit=(Serializing.getXmlNodeValue(doc,"IsAudit")=="0")?false:true;
				}
				if(Serializing.getXmlNodeValue(doc,"SelectedTeethOnly")!=null) {
					SelectedTeethOnly=(Serializing.getXmlNodeValue(doc,"SelectedTeethOnly")=="0")?false:true;
				}
				if(Serializing.getXmlNodeValue(doc,"OrionStatusFlags")!=null) {
					OrionStatusFlags=OrionStatus.values()[Integer.valueOf(Serializing.getXmlNodeValue(doc,"OrionStatusFlags"))];
				}
				if(Serializing.getXmlNodeValue(doc,"DatesShowing")!=null) {
					DatesShowing=ChartViewDates.values()[Integer.valueOf(Serializing.getXmlNodeValue(doc,"DatesShowing"))];
				}
			}
			catch(Exception e) {
				throw e;
			}
		}

		/**  */
		public enum ChartViewProcStat {
			/** 0- None. */
			None,
			/** 1- Treatment Plan. */
			TP,
			/** 2- Complete. */
			C,
			/** 4- Existing Current Provider. */
			EC,
			/** 8- Existing Other Provider. */
			EO,
			/** 16- Referred Out. */
			R,
			/** 32- Deleted. */
			D,
			/** 64- Condition. */
			Cn,
			/** 127- All. */
			All
		}

		/**  */
		public enum ChartViewObjs {
			/** 0- None */
			None,
			/** 1- Appointments */
			Appointments,
			/** 2- Comm Log */
			CommLog,
			/** 4- Comm Log Family */
			CommLogFamily,
			/** 8- Tasks */
			Tasks,
			/** 16- Email */
			Email,
			/** 32- Lab Cases */
			LabCases,
			/** 64- Rx */
			Rx,
			/** 128- Sheets */
			Sheets,
			/** 255- All */
			All
		}

		/**  */
		public enum OrionStatus {
			/** 0- None.  While a normal orion proc would never have this status2, it is still needed for flags in ChartViews.  And it's also possible that a status2 slipped through the cracks and was not assigned, leaving it with this value. */
			None,
			/** 1– Treatment planned */
			TP,
			/** 2– Completed */
			C,
			/** 4– Existing prior to incarceration */
			E,
			/** 8– Refused treatment */
			R,
			/** 16– Referred out to specialist */
			RO,
			/** 32– Completed by specialist */
			CS,
			/** 64– Completed by registry */
			CR,
			/** 128- Cancelled, tx plan changed */
			CA_Tx,
			/** 256- Cancelled, eligible parole */
			CA_EPRD,
			/** 512- Cancelled, parole/discharge */
			CA_PD,
			/** 1024– Suspended, unacceptable plaque */
			S,
			/** 2048- Stop clock, multi visit */
			ST,
			/** 4096– Watch */
			W,
			/** 8192– Alternative */
			A
		}

		/**  */
		public enum ChartViewDates {
			/** 0- All */
			All,
			/** 1- Today */
			Today,
			/** 2- Yesterday */
			Yesterday,
			/** 3- This Year */
			ThisYear,
			/** 4- Last Year */
			LastYear
		}


}
