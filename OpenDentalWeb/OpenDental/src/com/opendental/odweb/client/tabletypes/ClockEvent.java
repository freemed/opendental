package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

public class ClockEvent {
		/** Primary key. */
		public int ClockEventNum;
		/** FK to employee.EmployeeNum */
		public int EmployeeNum;
		/** The actual time that this entry was entered.  Cannot be 01-01-0001. */
		public Date TimeEntered1;
		/** The time to display and to use in all calculations.  Cannot be 01-01-0001. */
		public Date TimeDisplayed1;
		/** Enum:TimeClockStatus  Home, Lunch, or Break.  The status really only applies to the clock out.  Except the Break status applies to both out and in. */
		public TimeClockStatus ClockStatus;
		/** . */
		public String Note;
		/** The user can never edit this, but the program has to be able to edit this when user clocks out.  Can be 01-01-0001 if not clocked out yet. */
		public Date TimeEntered2;
		/** User can edit. Can be 01-01-0001 if not clocked out yet. */
		public Date TimeDisplayed2;
		/** This is a manual override for OTimeAuto.  Typically -1 hour (-01:00:00) to indicate no override.  When used as override, allowed values are zero or positive.  This is an alternative to using a TimeAdjust row. */
		public String OTimeHours;
		/** Automatically calculated OT.  Will be zero if none. */
		public String OTimeAuto;
		/** This is a manual override of AdjustAuto.  Ignored unless AdjustIsOverridden set to true.  When used as override, it's typically negative, although zero and positive are also allowed. */
		public String Adjust;
		/** Automatically calculated Adjust.  Will be zero if none. */
		public String AdjustAuto;
		/** True if AdjustAuto is overridden by Adjust. */
		public boolean AdjustIsOverridden;
		/** Override for AmountBonusAuto. -1 indicates no override. */
		public double AmountBonus;
		/** Automatically created bonus (due to differential and OT hours worked). -1 will indicate no bonus calculated. */
		public double AmountBonusAuto;

		/** Deep copy of object. */
		public ClockEvent Copy() {
			ClockEvent clockevent=new ClockEvent();
			clockevent.ClockEventNum=this.ClockEventNum;
			clockevent.EmployeeNum=this.EmployeeNum;
			clockevent.TimeEntered1=this.TimeEntered1;
			clockevent.TimeDisplayed1=this.TimeDisplayed1;
			clockevent.ClockStatus=this.ClockStatus;
			clockevent.Note=this.Note;
			clockevent.TimeEntered2=this.TimeEntered2;
			clockevent.TimeDisplayed2=this.TimeDisplayed2;
			clockevent.OTimeHours=this.OTimeHours;
			clockevent.OTimeAuto=this.OTimeAuto;
			clockevent.Adjust=this.Adjust;
			clockevent.AdjustAuto=this.AdjustAuto;
			clockevent.AdjustIsOverridden=this.AdjustIsOverridden;
			clockevent.AmountBonus=this.AmountBonus;
			clockevent.AmountBonusAuto=this.AmountBonusAuto;
			return clockevent;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<ClockEvent>");
			sb.append("<ClockEventNum>").append(ClockEventNum).append("</ClockEventNum>");
			sb.append("<EmployeeNum>").append(EmployeeNum).append("</EmployeeNum>");
			sb.append("<TimeEntered1>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(AptDateTime)).append("</TimeEntered1>");
			sb.append("<TimeDisplayed1>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(AptDateTime)).append("</TimeDisplayed1>");
			sb.append("<ClockStatus>").append(ClockStatus.ordinal()).append("</ClockStatus>");
			sb.append("<Note>").append(Serializing.EscapeForXml(Note)).append("</Note>");
			sb.append("<TimeEntered2>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(AptDateTime)).append("</TimeEntered2>");
			sb.append("<TimeDisplayed2>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(AptDateTime)).append("</TimeDisplayed2>");
			sb.append("<OTimeHours>").append(Serializing.EscapeForXml(OTimeHours)).append("</OTimeHours>");
			sb.append("<OTimeAuto>").append(Serializing.EscapeForXml(OTimeAuto)).append("</OTimeAuto>");
			sb.append("<Adjust>").append(Serializing.EscapeForXml(Adjust)).append("</Adjust>");
			sb.append("<AdjustAuto>").append(Serializing.EscapeForXml(AdjustAuto)).append("</AdjustAuto>");
			sb.append("<AdjustIsOverridden>").append((AdjustIsOverridden)?1:0).append("</AdjustIsOverridden>");
			sb.append("<AmountBonus>").append(AmountBonus).append("</AmountBonus>");
			sb.append("<AmountBonusAuto>").append(AmountBonusAuto).append("</AmountBonusAuto>");
			sb.append("</ClockEvent>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				ClockEventNum=Integer.valueOf(doc.getElementsByTagName("ClockEventNum").item(0).getFirstChild().getNodeValue());
				EmployeeNum=Integer.valueOf(doc.getElementsByTagName("EmployeeNum").item(0).getFirstChild().getNodeValue());
				TimeEntered1=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(doc.getElementsByTagName("TimeEntered1").item(0).getFirstChild().getNodeValue());
				TimeDisplayed1=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(doc.getElementsByTagName("TimeDisplayed1").item(0).getFirstChild().getNodeValue());
				ClockStatus=TimeClockStatus.values()[Integer.valueOf(doc.getElementsByTagName("ClockStatus").item(0).getFirstChild().getNodeValue())];
				Note=doc.getElementsByTagName("Note").item(0).getFirstChild().getNodeValue();
				TimeEntered2=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(doc.getElementsByTagName("TimeEntered2").item(0).getFirstChild().getNodeValue());
				TimeDisplayed2=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(doc.getElementsByTagName("TimeDisplayed2").item(0).getFirstChild().getNodeValue());
				OTimeHours=doc.getElementsByTagName("OTimeHours").item(0).getFirstChild().getNodeValue();
				OTimeAuto=doc.getElementsByTagName("OTimeAuto").item(0).getFirstChild().getNodeValue();
				Adjust=doc.getElementsByTagName("Adjust").item(0).getFirstChild().getNodeValue();
				AdjustAuto=doc.getElementsByTagName("AdjustAuto").item(0).getFirstChild().getNodeValue();
				AdjustIsOverridden=(doc.getElementsByTagName("AdjustIsOverridden").item(0).getFirstChild().getNodeValue()=="0")?false:true;
				AmountBonus=Double.valueOf(doc.getElementsByTagName("AmountBonus").item(0).getFirstChild().getNodeValue());
				AmountBonusAuto=Double.valueOf(doc.getElementsByTagName("AmountBonusAuto").item(0).getFirstChild().getNodeValue());
			}
			catch(Exception e) {
				throw e;
			}
		}

		/**  */
		public enum TimeClockStatus {
			/** 0 */
			Home,
			/** 1 */
			Lunch,
			/** 2 */
			Break
		}


}
