package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

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

		/** Deep copy of object. */
		public Phone Copy() {
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
			return phone;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<Phone>");
			sb.append("<PhoneNum>").append(PhoneNum).append("</PhoneNum>");
			sb.append("<Extension>").append(Extension).append("</Extension>");
			sb.append("<EmployeeName>").append(Serializing.EscapeForXml(EmployeeName)).append("</EmployeeName>");
			sb.append("<ClockStatus>").append(ClockStatus.ordinal()).append("</ClockStatus>");
			sb.append("<Description>").append(Serializing.EscapeForXml(Description)).append("</Description>");
			sb.append("<ColorBar>").append(ColorBar).append("</ColorBar>");
			sb.append("<ColorText>").append(ColorText).append("</ColorText>");
			sb.append("<EmployeeNum>").append(EmployeeNum).append("</EmployeeNum>");
			sb.append("<CustomerNumber>").append(Serializing.EscapeForXml(CustomerNumber)).append("</CustomerNumber>");
			sb.append("<InOrOut>").append(Serializing.EscapeForXml(InOrOut)).append("</InOrOut>");
			sb.append("<PatNum>").append(PatNum).append("</PatNum>");
			sb.append("<DateTimeStart>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateTimeStart)).append("</DateTimeStart>");
			sb.append("<WebCamImage>").append(Serializing.EscapeForXml(WebCamImage)).append("</WebCamImage>");
			sb.append("<ScreenshotPath>").append(Serializing.EscapeForXml(ScreenshotPath)).append("</ScreenshotPath>");
			sb.append("<ScreenshotImage>").append(Serializing.EscapeForXml(ScreenshotImage)).append("</ScreenshotImage>");
			sb.append("<CustomerNumberRaw>").append(Serializing.EscapeForXml(CustomerNumberRaw)).append("</CustomerNumberRaw>");
			sb.append("</Phone>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				PhoneNum=Integer.valueOf(doc.getElementsByTagName("PhoneNum").item(0).getFirstChild().getNodeValue());
				Extension=Integer.valueOf(doc.getElementsByTagName("Extension").item(0).getFirstChild().getNodeValue());
				EmployeeName=doc.getElementsByTagName("EmployeeName").item(0).getFirstChild().getNodeValue();
				ClockStatus=ClockStatusEnum.values()[Integer.valueOf(doc.getElementsByTagName("ClockStatus").item(0).getFirstChild().getNodeValue())];
				Description=doc.getElementsByTagName("Description").item(0).getFirstChild().getNodeValue();
				ColorBar=Integer.valueOf(doc.getElementsByTagName("ColorBar").item(0).getFirstChild().getNodeValue());
				ColorText=Integer.valueOf(doc.getElementsByTagName("ColorText").item(0).getFirstChild().getNodeValue());
				EmployeeNum=Integer.valueOf(doc.getElementsByTagName("EmployeeNum").item(0).getFirstChild().getNodeValue());
				CustomerNumber=doc.getElementsByTagName("CustomerNumber").item(0).getFirstChild().getNodeValue();
				InOrOut=doc.getElementsByTagName("InOrOut").item(0).getFirstChild().getNodeValue();
				PatNum=Integer.valueOf(doc.getElementsByTagName("PatNum").item(0).getFirstChild().getNodeValue());
				DateTimeStart=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(doc.getElementsByTagName("DateTimeStart").item(0).getFirstChild().getNodeValue());
				WebCamImage=doc.getElementsByTagName("WebCamImage").item(0).getFirstChild().getNodeValue();
				ScreenshotPath=doc.getElementsByTagName("ScreenshotPath").item(0).getFirstChild().getNodeValue();
				ScreenshotImage=doc.getElementsByTagName("ScreenshotImage").item(0).getFirstChild().getNodeValue();
				CustomerNumberRaw=doc.getElementsByTagName("CustomerNumberRaw").item(0).getFirstChild().getNodeValue();
			}
			catch(Exception e) {
				throw e;
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
