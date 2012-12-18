package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
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
		public ClockEvent deepCopy() {
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
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<ClockEvent>");
			sb.append("<ClockEventNum>").append(ClockEventNum).append("</ClockEventNum>");
			sb.append("<EmployeeNum>").append(EmployeeNum).append("</EmployeeNum>");
			sb.append("<TimeEntered1>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(TimeEntered1)).append("</TimeEntered1>");
			sb.append("<TimeDisplayed1>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(TimeDisplayed1)).append("</TimeDisplayed1>");
			sb.append("<ClockStatus>").append(ClockStatus.ordinal()).append("</ClockStatus>");
			sb.append("<Note>").append(Serializing.escapeForXml(Note)).append("</Note>");
			sb.append("<TimeEntered2>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(TimeEntered2)).append("</TimeEntered2>");
			sb.append("<TimeDisplayed2>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(TimeDisplayed2)).append("</TimeDisplayed2>");
			sb.append("<OTimeHours>").append(Serializing.escapeForXml(OTimeHours)).append("</OTimeHours>");
			sb.append("<OTimeAuto>").append(Serializing.escapeForXml(OTimeAuto)).append("</OTimeAuto>");
			sb.append("<Adjust>").append(Serializing.escapeForXml(Adjust)).append("</Adjust>");
			sb.append("<AdjustAuto>").append(Serializing.escapeForXml(AdjustAuto)).append("</AdjustAuto>");
			sb.append("<AdjustIsOverridden>").append((AdjustIsOverridden)?1:0).append("</AdjustIsOverridden>");
			sb.append("<AmountBonus>").append(AmountBonus).append("</AmountBonus>");
			sb.append("<AmountBonusAuto>").append(AmountBonusAuto).append("</AmountBonusAuto>");
			sb.append("</ClockEvent>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"ClockEventNum")!=null) {
					ClockEventNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ClockEventNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"EmployeeNum")!=null) {
					EmployeeNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"EmployeeNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"TimeEntered1")!=null) {
					TimeEntered1=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"TimeEntered1"));
				}
				if(Serializing.getXmlNodeValue(doc,"TimeDisplayed1")!=null) {
					TimeDisplayed1=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"TimeDisplayed1"));
				}
				if(Serializing.getXmlNodeValue(doc,"ClockStatus")!=null) {
					ClockStatus=TimeClockStatus.values()[Integer.valueOf(Serializing.getXmlNodeValue(doc,"ClockStatus"))];
				}
				if(Serializing.getXmlNodeValue(doc,"Note")!=null) {
					Note=Serializing.getXmlNodeValue(doc,"Note");
				}
				if(Serializing.getXmlNodeValue(doc,"TimeEntered2")!=null) {
					TimeEntered2=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"TimeEntered2"));
				}
				if(Serializing.getXmlNodeValue(doc,"TimeDisplayed2")!=null) {
					TimeDisplayed2=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"TimeDisplayed2"));
				}
				if(Serializing.getXmlNodeValue(doc,"OTimeHours")!=null) {
					OTimeHours=Serializing.getXmlNodeValue(doc,"OTimeHours");
				}
				if(Serializing.getXmlNodeValue(doc,"OTimeAuto")!=null) {
					OTimeAuto=Serializing.getXmlNodeValue(doc,"OTimeAuto");
				}
				if(Serializing.getXmlNodeValue(doc,"Adjust")!=null) {
					Adjust=Serializing.getXmlNodeValue(doc,"Adjust");
				}
				if(Serializing.getXmlNodeValue(doc,"AdjustAuto")!=null) {
					AdjustAuto=Serializing.getXmlNodeValue(doc,"AdjustAuto");
				}
				if(Serializing.getXmlNodeValue(doc,"AdjustIsOverridden")!=null) {
					AdjustIsOverridden=(Serializing.getXmlNodeValue(doc,"AdjustIsOverridden")=="0")?false:true;
				}
				if(Serializing.getXmlNodeValue(doc,"AmountBonus")!=null) {
					AmountBonus=Double.valueOf(Serializing.getXmlNodeValue(doc,"AmountBonus"));
				}
				if(Serializing.getXmlNodeValue(doc,"AmountBonusAuto")!=null) {
					AmountBonusAuto=Double.valueOf(Serializing.getXmlNodeValue(doc,"AmountBonusAuto"));
				}
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
