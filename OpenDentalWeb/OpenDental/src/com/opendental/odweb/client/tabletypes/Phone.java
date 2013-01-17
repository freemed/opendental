package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

//DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD.
public class Phone {
		/** Primary key. */
		public int PhoneNum;
		/**  */
		public int Extension;
		/**  */
		public String EmployeeName;
		/** This enum is stored in the db as a string, so it needs special handling.  In phoneTrackingServer initialize, this value is pulled from employee.ClockStatus as Home, Lunch, Break, or Working(which gets converted to Available).  After that, the phone server uses those 4 in addition to WrapUp, Off, Training, TeamAssist, OfflineAssist, Backup, and None(which is displayed as an empty string).  The main program sets Unavailable sometimes, and pulls from employee.ClockStatus sometimes. */
		public ClockStatusEnum ClockStatus;
		/** Either blank or 'In use' */
		public String Description;
		/**  */
		public int ColorBar;
		/**  */
		public int ColorText;
		/** FK to employee.EmployeeNum. */
		public int EmployeeNum;
		/** The phone number or name of customer. */
		public String CustomerNumber;
		/** Blank or 'in' or 'out'. */
		public String InOrOut;
		/** FK to patient.PatNum.  The customer. */
		public int PatNum;
		/** The date/time that the phonecall started.  Used to calculate how long user has been on phone. */
		public Date DateTimeStart;
		/** The base64 representation of a bitmap. */
		public String WebCamImage;
		/** Full path to the most recent screenshot. */
		public String ScreenshotPath;
		/** The base64 thumbnail of the most recent screenshot. */
		public String ScreenshotImage;
		/** Always set to the phone number of the caller. */
		public String CustomerNumberRaw;
		/** A copy of DateTimeStart made when a call has ended.  Gets set to 0001-01-01 after the 30 second wrap up thread has run. */
		public Date LastCallTimeStart;

		/** Deep copy of object. */
		public Phone deepCopy() {
			Phone phone=new Phone();
			phone.PhoneNum=this.PhoneNum;
			phone.Extension=this.Extension;
			phone.EmployeeName=this.EmployeeName;
			phone.ClockStatus=this.ClockStatus;
			phone.Description=this.Description;
			phone.ColorBar=this.ColorBar;
			phone.ColorText=this.ColorText;
			phone.EmployeeNum=this.EmployeeNum;
			phone.CustomerNumber=this.CustomerNumber;
			phone.InOrOut=this.InOrOut;
			phone.PatNum=this.PatNum;
			phone.DateTimeStart=this.DateTimeStart;
			phone.WebCamImage=this.WebCamImage;
			phone.ScreenshotPath=this.ScreenshotPath;
			phone.ScreenshotImage=this.ScreenshotImage;
			phone.CustomerNumberRaw=this.CustomerNumberRaw;
			phone.LastCallTimeStart=this.LastCallTimeStart;
			return phone;
		}

		/** Serialize the object into XML. */
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<Phone>");
			sb.append("<PhoneNum>").append(PhoneNum).append("</PhoneNum>");
			sb.append("<Extension>").append(Extension).append("</Extension>");
			sb.append("<EmployeeName>").append(Serializing.escapeForXml(EmployeeName)).append("</EmployeeName>");
			sb.append("<ClockStatus>").append(ClockStatus.ordinal()).append("</ClockStatus>");
			sb.append("<Description>").append(Serializing.escapeForXml(Description)).append("</Description>");
			sb.append("<ColorBar>").append(ColorBar).append("</ColorBar>");
			sb.append("<ColorText>").append(ColorText).append("</ColorText>");
			sb.append("<EmployeeNum>").append(EmployeeNum).append("</EmployeeNum>");
			sb.append("<CustomerNumber>").append(Serializing.escapeForXml(CustomerNumber)).append("</CustomerNumber>");
			sb.append("<InOrOut>").append(Serializing.escapeForXml(InOrOut)).append("</InOrOut>");
			sb.append("<PatNum>").append(PatNum).append("</PatNum>");
			sb.append("<DateTimeStart>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateTimeStart)).append("</DateTimeStart>");
			sb.append("<WebCamImage>").append(Serializing.escapeForXml(WebCamImage)).append("</WebCamImage>");
			sb.append("<ScreenshotPath>").append(Serializing.escapeForXml(ScreenshotPath)).append("</ScreenshotPath>");
			sb.append("<ScreenshotImage>").append(Serializing.escapeForXml(ScreenshotImage)).append("</ScreenshotImage>");
			sb.append("<CustomerNumberRaw>").append(Serializing.escapeForXml(CustomerNumberRaw)).append("</CustomerNumberRaw>");
			sb.append("<LastCallTimeStart>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(LastCallTimeStart)).append("</LastCallTimeStart>");
			sb.append("</Phone>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"PhoneNum")!=null) {
					PhoneNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"PhoneNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"Extension")!=null) {
					Extension=Integer.valueOf(Serializing.getXmlNodeValue(doc,"Extension"));
				}
				if(Serializing.getXmlNodeValue(doc,"EmployeeName")!=null) {
					EmployeeName=Serializing.getXmlNodeValue(doc,"EmployeeName");
				}
				if(Serializing.getXmlNodeValue(doc,"ClockStatus")!=null) {
					ClockStatus=ClockStatusEnum.values()[Integer.valueOf(Serializing.getXmlNodeValue(doc,"ClockStatus"))];
				}
				if(Serializing.getXmlNodeValue(doc,"Description")!=null) {
					Description=Serializing.getXmlNodeValue(doc,"Description");
				}
				if(Serializing.getXmlNodeValue(doc,"ColorBar")!=null) {
					ColorBar=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ColorBar"));
				}
				if(Serializing.getXmlNodeValue(doc,"ColorText")!=null) {
					ColorText=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ColorText"));
				}
				if(Serializing.getXmlNodeValue(doc,"EmployeeNum")!=null) {
					EmployeeNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"EmployeeNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"CustomerNumber")!=null) {
					CustomerNumber=Serializing.getXmlNodeValue(doc,"CustomerNumber");
				}
				if(Serializing.getXmlNodeValue(doc,"InOrOut")!=null) {
					InOrOut=Serializing.getXmlNodeValue(doc,"InOrOut");
				}
				if(Serializing.getXmlNodeValue(doc,"PatNum")!=null) {
					PatNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"PatNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"DateTimeStart")!=null) {
					DateTimeStart=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"DateTimeStart"));
				}
				if(Serializing.getXmlNodeValue(doc,"WebCamImage")!=null) {
					WebCamImage=Serializing.getXmlNodeValue(doc,"WebCamImage");
				}
				if(Serializing.getXmlNodeValue(doc,"ScreenshotPath")!=null) {
					ScreenshotPath=Serializing.getXmlNodeValue(doc,"ScreenshotPath");
				}
				if(Serializing.getXmlNodeValue(doc,"ScreenshotImage")!=null) {
					ScreenshotImage=Serializing.getXmlNodeValue(doc,"ScreenshotImage");
				}
				if(Serializing.getXmlNodeValue(doc,"CustomerNumberRaw")!=null) {
					CustomerNumberRaw=Serializing.getXmlNodeValue(doc,"CustomerNumberRaw");
				}
				if(Serializing.getXmlNodeValue(doc,"LastCallTimeStart")!=null) {
					LastCallTimeStart=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"LastCallTimeStart"));
				}
			}
			catch(Exception e) {
				throw new Exception("Error deserializing Phone: "+e.getMessage());
			}
		}

		/**  */
		public enum ClockStatusEnum {
			/** This shows in the UI as blank. */
			None,
			/**  */
			Home,
			/**  */
			Lunch,
			/**  */
			Break,
			/**  */
			Available,
			/**  */
			WrapUp,
			/**  */
			Off,
			/**  */
			Training,
			/**  */
			TeamAssist,
			/**  */
			OfflineAssist,
			/**  */
			Backup,
			/**  */
			Unavailable,
			/**  */
			NeedsHelp
		}


}
