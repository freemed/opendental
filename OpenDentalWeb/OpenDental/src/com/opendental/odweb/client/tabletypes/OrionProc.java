package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

/** DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD. */
public class OrionProc {
		/** Primary key. */
		public int OrionProcNum;
		/** FK to procedurelog.ProcNum */
		public int ProcNum;
		/** Enum:OrionDPC NotSpecified=0,None=1,_1A=2,_1B=3,_1C=4,_2=5,_3=6,_4=7,_5=8. */
		public OrionDPC DPC;
		/** Enum:OrionDPC None=0,1A=1,1B=2,1C=3,2=4,3=5,4=6,5=7 */
		public OrionDPC DPCpost;
		/** System adds days to the diagnosis date based upon the DPC entered for that procedure. If DPC = none the system will return “No Schedule by Date”.  */
		public Date DateScheduleBy;
		/**  Default to current date.  Provider shall have to ability to edit with a previous date, but not a future date. */
		public Date DateStopClock;
		/** Enum:OrionStatus None=0,TP=1,C=2,E=4,R=8,RO=16,CS=32,CR=64,CA-Tx=128,CA-ERPD=256,CA-P/D=512,S=1024,ST=2048,W=4096,A=8192 */
		public OrionStatus Status2;
		/** . */
		public boolean IsOnCall;
		/** Indicates in the clinical note that effective communication was used for this encounter. */
		public boolean IsEffectiveComm;
		/** . */
		public boolean IsRepair;

		/** Deep copy of object. */
		public OrionProc deepCopy() {
			OrionProc orionproc=new OrionProc();
			orionproc.OrionProcNum=this.OrionProcNum;
			orionproc.ProcNum=this.ProcNum;
			orionproc.DPC=this.DPC;
			orionproc.DPCpost=this.DPCpost;
			orionproc.DateScheduleBy=this.DateScheduleBy;
			orionproc.DateStopClock=this.DateStopClock;
			orionproc.Status2=this.Status2;
			orionproc.IsOnCall=this.IsOnCall;
			orionproc.IsEffectiveComm=this.IsEffectiveComm;
			orionproc.IsRepair=this.IsRepair;
			return orionproc;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<OrionProc>");
			sb.append("<OrionProcNum>").append(OrionProcNum).append("</OrionProcNum>");
			sb.append("<ProcNum>").append(ProcNum).append("</ProcNum>");
			sb.append("<DPC>").append(DPC.ordinal()).append("</DPC>");
			sb.append("<DPCpost>").append(DPCpost.ordinal()).append("</DPCpost>");
			sb.append("<DateScheduleBy>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateScheduleBy)).append("</DateScheduleBy>");
			sb.append("<DateStopClock>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateStopClock)).append("</DateStopClock>");
			sb.append("<Status2>").append(Status2.ordinal()).append("</Status2>");
			sb.append("<IsOnCall>").append((IsOnCall)?1:0).append("</IsOnCall>");
			sb.append("<IsEffectiveComm>").append((IsEffectiveComm)?1:0).append("</IsEffectiveComm>");
			sb.append("<IsRepair>").append((IsRepair)?1:0).append("</IsRepair>");
			sb.append("</OrionProc>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"OrionProcNum")!=null) {
					OrionProcNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"OrionProcNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"ProcNum")!=null) {
					ProcNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ProcNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"DPC")!=null) {
					DPC=OrionDPC.values()[Integer.valueOf(Serializing.getXmlNodeValue(doc,"DPC"))];
				}
				if(Serializing.getXmlNodeValue(doc,"DPCpost")!=null) {
					DPCpost=OrionDPC.values()[Integer.valueOf(Serializing.getXmlNodeValue(doc,"DPCpost"))];
				}
				if(Serializing.getXmlNodeValue(doc,"DateScheduleBy")!=null) {
					DateScheduleBy=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"DateScheduleBy"));
				}
				if(Serializing.getXmlNodeValue(doc,"DateStopClock")!=null) {
					DateStopClock=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"DateStopClock"));
				}
				if(Serializing.getXmlNodeValue(doc,"Status2")!=null) {
					Status2=OrionStatus.values()[Integer.valueOf(Serializing.getXmlNodeValue(doc,"Status2"))];
				}
				if(Serializing.getXmlNodeValue(doc,"IsOnCall")!=null) {
					IsOnCall=(Serializing.getXmlNodeValue(doc,"IsOnCall")=="0")?false:true;
				}
				if(Serializing.getXmlNodeValue(doc,"IsEffectiveComm")!=null) {
					IsEffectiveComm=(Serializing.getXmlNodeValue(doc,"IsEffectiveComm")=="0")?false:true;
				}
				if(Serializing.getXmlNodeValue(doc,"IsRepair")!=null) {
					IsRepair=(Serializing.getXmlNodeValue(doc,"IsRepair")=="0")?false:true;
				}
			}
			catch(Exception e) {
				throw e;
			}
		}

		/**  */
		public enum OrionDPC {
			/** 0- Not Specified */
			NotSpecified,
			/** 1- None */
			None,
			/** 2- Treatment to be scheduled within 1 calendar day */
			_1A,
			/** 3- Treatment to be scheduled within 30 calendar days */
			_1B,
			/** 4- Treatment to be scheduled within 60 calendar days */
			_1C,
			/** 5– Treatment to be scheduled within 120 calendar days */
			_2,
			/** 6– Treatment to be scheduled within 1 year */
			_3,
			/** 7– No further treatment is needed, no appointment needed */
			_4,
			/** 8– No appointment needed  */
			_5
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


}
