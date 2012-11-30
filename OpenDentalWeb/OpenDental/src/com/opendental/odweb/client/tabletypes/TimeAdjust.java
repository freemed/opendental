package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

public class TimeAdjust {
		/** Primary key. */
		public int TimeAdjustNum;
		/** FK to employee.EmployeeNum */
		public int EmployeeNum;
		/** The date and time that this entry will show on timecard. */
		public Date TimeEntry;
		/** The number of regular hours to adjust timecard by.  Can be + or -. */
		public String RegHours;
		/** Overtime hours. Usually +.  Automatically combined with a - adj to RegHours.  Another option is clockevent.OTimeHours. */
		public String OTimeHours;
		/** . */
		public String Note;
		/** Set to true if this adjustment was automatically made by the system.  When the calc weekly ot tool is run, these types of adjustments are fair game for deletion.  Other adjustments are preserved. */
		public boolean IsAuto;

		/** Deep copy of object. */
		public TimeAdjust Copy() {
			TimeAdjust timeadjust=new TimeAdjust();
			timeadjust.TimeAdjustNum=this.TimeAdjustNum;
			timeadjust.EmployeeNum=this.EmployeeNum;
			timeadjust.TimeEntry=this.TimeEntry;
			timeadjust.RegHours=this.RegHours;
			timeadjust.OTimeHours=this.OTimeHours;
			timeadjust.Note=this.Note;
			timeadjust.IsAuto=this.IsAuto;
			return timeadjust;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<TimeAdjust>");
			sb.append("<TimeAdjustNum>").append(TimeAdjustNum).append("</TimeAdjustNum>");
			sb.append("<EmployeeNum>").append(EmployeeNum).append("</EmployeeNum>");
			sb.append("<TimeEntry>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(TimeEntry)).append("</TimeEntry>");
			sb.append("<RegHours>").append(Serializing.EscapeForXml(RegHours)).append("</RegHours>");
			sb.append("<OTimeHours>").append(Serializing.EscapeForXml(OTimeHours)).append("</OTimeHours>");
			sb.append("<Note>").append(Serializing.EscapeForXml(Note)).append("</Note>");
			sb.append("<IsAuto>").append((IsAuto)?1:0).append("</IsAuto>");
			sb.append("</TimeAdjust>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void DeserializeFromXml(Document doc) throws Exception {
			try {
				if(Serializing.GetXmlNodeValue(doc,"TimeAdjustNum")!=null) {
					TimeAdjustNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"TimeAdjustNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"EmployeeNum")!=null) {
					EmployeeNum=Integer.valueOf(Serializing.GetXmlNodeValue(doc,"EmployeeNum"));
				}
				if(Serializing.GetXmlNodeValue(doc,"TimeEntry")!=null) {
					TimeEntry=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.GetXmlNodeValue(doc,"TimeEntry"));
				}
				if(Serializing.GetXmlNodeValue(doc,"RegHours")!=null) {
					RegHours=Serializing.GetXmlNodeValue(doc,"RegHours");
				}
				if(Serializing.GetXmlNodeValue(doc,"OTimeHours")!=null) {
					OTimeHours=Serializing.GetXmlNodeValue(doc,"OTimeHours");
				}
				if(Serializing.GetXmlNodeValue(doc,"Note")!=null) {
					Note=Serializing.GetXmlNodeValue(doc,"Note");
				}
				if(Serializing.GetXmlNodeValue(doc,"IsAuto")!=null) {
					IsAuto=(Serializing.GetXmlNodeValue(doc,"IsAuto")=="0")?false:true;
				}
			}
			catch(Exception e) {
				throw e;
			}
		}


}
