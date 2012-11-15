package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.google.gwt.xml.client.XMLParser;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

public class Appointment {
		/** Primary key. */
		public int AptNum;
		/** FK to patient.PatNum.  The patient that the appointment is for. */
		public int PatNum;
		/** Enum:ApptStatus . */
		public ApptStatus AptStatus;
		/** Time pattern, X for Dr time, / for assist time. Stored in 5 minute increments.  Converted as needed to 10 or 15 minute representations for display. */
		public String Pattern;
		/** FK to definition.DefNum.  This field can also be used to show patient arrived, in chair, etc.  The Category column in the definition table is DefCat.ApptConfirmed. */
		public int Confirmed;
		/** If true, then the program will not attempt to reset the user's time pattern and length when adding or removing procedures. */
		public boolean TimeLocked;
		/** FK to operatory.OperatoryNum. */
		public int Op;
		/** Note. */
		public String Note;
		/** FK to provider.ProvNum. */
		public int ProvNum;
		/** FK to provider.ProvNum.  Optional.  Only used if a hygienist is assigned to this appt. */
		public int ProvHyg;
		/** Appointment Date and time.  If you need just the date or time for an SQL query, you can use DATE(AptDateTime) and TIME(AptDateTime) in your query. */
		public Date AptDateTime;
		/** FK to appointment.AptNum.  A better description of this field would be PlannedAptNum.  Only used to show that this apt is derived from specified planned apt. Otherwise, 0. */
		public int NextAptNum;
		/** FK to definition.DefNum.  The definition.Category in the definition table is DefCat.RecallUnschedStatus.  Only used if this is an Unsched or Planned appt. */
		public int UnschedStatus;
		/** This is the first appoinment this patient has had at this office.  Somewhat automated. */
		public boolean IsNewPatient;
		/** A one line summary of all procedures.  Can be used in various reports, Unscheduled list, and Planned appointment tracker.  Not user editable right now, so it doesn't show on the screen. */
		public String ProcDescript;
		/** FK to employee.EmployeeNum.  You can assign an assistant to the appointment. */
		public int Assistant;
		/** FK to clinic.ClinicNum.  0 if no clinic. */
		public int ClinicNum;
		/** Set true if this is a hygiene appt.  The only purpose of this flag is to cause the hygiene provider's color to show.  This flag is frequently not set even when it is a hygiene appointment because some offices want the dentist color on the appointments. */
		public boolean IsHygiene;
		/** Automatically updated by MySQL every time a row is added or changed. */
		public Date DateTStamp;
		/** The date and time that the patient checked in.  Date is largely ignored since it should be the same as the appt. */
		public Date DateTimeArrived;
		/** The date and time that the patient was seated in the chair in the operatory. */
		public Date DateTimeSeated;
		/** The date and time that the patient got up out of the chair */
		public Date DateTimeDismissed;
		/** FK to insplan.PlanNum for the primary insurance plan at the time the appointment is set complete. May be 0. We can't tell later which subscriber is involved; only the plan. */
		public int InsPlan1;
		/** FK to insplan.PlanNum for the secoondary insurance plan at the time the appointment is set complete. May be 0. We can't tell later which subscriber is involved; only the plan. */
		public int InsPlan2;
		/** Date and time patient asked to arrive, or minval if patient not asked to arrive at a different time than appt. */
		public Date DateTimeAskedToArrive;
		/** Stores XML for the procs colors */
		public String ProcsColored;
		/** If set to anything but 0, then this will override the graphic color for the appointment. */
		public int ColorOverride;

		/** Deep copy of object. */
		public Appointment Copy() {
			Appointment appointment=new Appointment();
			appointment.AptNum=this.AptNum;
			appointment.PatNum=this.PatNum;
			appointment.AptStatus=this.AptStatus;
			appointment.Pattern=this.Pattern;
			appointment.Confirmed=this.Confirmed;
			appointment.TimeLocked=this.TimeLocked;
			appointment.Op=this.Op;
			appointment.Note=this.Note;
			appointment.ProvNum=this.ProvNum;
			appointment.ProvHyg=this.ProvHyg;
			appointment.AptDateTime=this.AptDateTime;
			appointment.NextAptNum=this.NextAptNum;
			appointment.UnschedStatus=this.UnschedStatus;
			appointment.IsNewPatient=this.IsNewPatient;
			appointment.ProcDescript=this.ProcDescript;
			appointment.Assistant=this.Assistant;
			appointment.ClinicNum=this.ClinicNum;
			appointment.IsHygiene=this.IsHygiene;
			appointment.DateTStamp=this.DateTStamp;
			appointment.DateTimeArrived=this.DateTimeArrived;
			appointment.DateTimeSeated=this.DateTimeSeated;
			appointment.DateTimeDismissed=this.DateTimeDismissed;
			appointment.InsPlan1=this.InsPlan1;
			appointment.InsPlan2=this.InsPlan2;
			appointment.DateTimeAskedToArrive=this.DateTimeAskedToArrive;
			appointment.ProcsColored=this.ProcsColored;
			appointment.ColorOverride=this.ColorOverride;
			return appointment;
		}

		/** Serialize the object into XML. */
		public String SerializeToXml() {
			StringBuilder sb=new StringBuilder();
			sb.append("<Appointment>");
			sb.append("<AptNum>").append(AptNum).append("</AptNum>");
			sb.append("<PatNum>").append(PatNum).append("</PatNum>");
			sb.append("<AptStatus>").append(AptStatus.ordinal()).append("</AptStatus>");
			sb.append("<Pattern>").append(Serializing.EscapeForXml(Pattern)).append("</Pattern>");
			sb.append("<Confirmed>").append(Confirmed).append("</Confirmed>");
			sb.append("<TimeLocked>").append((TimeLocked)?1:0).append("</TimeLocked>");
			sb.append("<Op>").append(Op).append("</Op>");
			sb.append("<Note>").append(Serializing.EscapeForXml(Note)).append("</Note>");
			sb.append("<ProvNum>").append(ProvNum).append("</ProvNum>");
			sb.append("<ProvHyg>").append(ProvHyg).append("</ProvHyg>");
			sb.append("<AptDateTime>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(AptDateTime)).append("</AptDateTime>");
			sb.append("<NextAptNum>").append(NextAptNum).append("</NextAptNum>");
			sb.append("<UnschedStatus>").append(UnschedStatus).append("</UnschedStatus>");
			sb.append("<IsNewPatient>").append((IsNewPatient)?1:0).append("</IsNewPatient>");
			sb.append("<ProcDescript>").append(Serializing.EscapeForXml(ProcDescript)).append("</ProcDescript>");
			sb.append("<Assistant>").append(Assistant).append("</Assistant>");
			sb.append("<ClinicNum>").append(ClinicNum).append("</ClinicNum>");
			sb.append("<IsHygiene>").append((IsHygiene)?1:0).append("</IsHygiene>");
			sb.append("<DateTStamp>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateTStamp)).append("</DateTStamp>");
			sb.append("<DateTimeArrived>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateTimeArrived)).append("</DateTimeArrived>");
			sb.append("<DateTimeSeated>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateTimeSeated)).append("</DateTimeSeated>");
			sb.append("<DateTimeDismissed>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateTimeDismissed)).append("</DateTimeDismissed>");
			sb.append("<InsPlan1>").append(InsPlan1).append("</InsPlan1>");
			sb.append("<InsPlan2>").append(InsPlan2).append("</InsPlan2>");
			sb.append("<DateTimeAskedToArrive>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(DateTimeAskedToArrive)).append("</DateTimeAskedToArrive>");
			sb.append("<ProcsColored>").append(Serializing.EscapeForXml(ProcsColored)).append("</ProcsColored>");
			sb.append("<ColorOverride>").append(ColorOverride).append("</ColorOverride>");
			sb.append("</Appointment>");
			return sb.toString();
		}

		/** Sets the variables for this object based on the values from the XML.
		 * @param xml The XML passed in must be valid and contain a node for every variable on this object.
		 * @throws Exception Deserialize is encased in a try catch and will pass any thrown exception on. */
		public void DeserializeFromXml(String xml) throws Exception {
			try {
				Document doc=XMLParser.parse(xml);
				AptNum=Integer.valueOf(doc.getElementsByTagName("AptNum").item(0).getFirstChild().getNodeValue());
				PatNum=Integer.valueOf(doc.getElementsByTagName("PatNum").item(0).getFirstChild().getNodeValue());
				AptStatus=ApptStatus.values()[Integer.valueOf(doc.getElementsByTagName("AptStatus").item(0).getFirstChild().getNodeValue())];
				Pattern=doc.getElementsByTagName("Pattern").item(0).getFirstChild().getNodeValue();
				Confirmed=Integer.valueOf(doc.getElementsByTagName("Confirmed").item(0).getFirstChild().getNodeValue());
				TimeLocked=(doc.getElementsByTagName("TimeLocked").item(0).getFirstChild().getNodeValue()=="0")?false:true;
				Op=Integer.valueOf(doc.getElementsByTagName("Op").item(0).getFirstChild().getNodeValue());
				Note=doc.getElementsByTagName("Note").item(0).getFirstChild().getNodeValue();
				ProvNum=Integer.valueOf(doc.getElementsByTagName("ProvNum").item(0).getFirstChild().getNodeValue());
				ProvHyg=Integer.valueOf(doc.getElementsByTagName("ProvHyg").item(0).getFirstChild().getNodeValue());
				AptDateTime=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(doc.getElementsByTagName("AptDateTime").item(0).getFirstChild().getNodeValue());
				NextAptNum=Integer.valueOf(doc.getElementsByTagName("NextAptNum").item(0).getFirstChild().getNodeValue());
				UnschedStatus=Integer.valueOf(doc.getElementsByTagName("UnschedStatus").item(0).getFirstChild().getNodeValue());
				IsNewPatient=(doc.getElementsByTagName("IsNewPatient").item(0).getFirstChild().getNodeValue()=="0")?false:true;
				ProcDescript=doc.getElementsByTagName("ProcDescript").item(0).getFirstChild().getNodeValue();
				Assistant=Integer.valueOf(doc.getElementsByTagName("Assistant").item(0).getFirstChild().getNodeValue());
				ClinicNum=Integer.valueOf(doc.getElementsByTagName("ClinicNum").item(0).getFirstChild().getNodeValue());
				IsHygiene=(doc.getElementsByTagName("IsHygiene").item(0).getFirstChild().getNodeValue()=="0")?false:true;
				DateTStamp=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(doc.getElementsByTagName("DateTStamp").item(0).getFirstChild().getNodeValue());
				DateTimeArrived=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(doc.getElementsByTagName("DateTimeArrived").item(0).getFirstChild().getNodeValue());
				DateTimeSeated=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(doc.getElementsByTagName("DateTimeSeated").item(0).getFirstChild().getNodeValue());
				DateTimeDismissed=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(doc.getElementsByTagName("DateTimeDismissed").item(0).getFirstChild().getNodeValue());
				InsPlan1=Integer.valueOf(doc.getElementsByTagName("InsPlan1").item(0).getFirstChild().getNodeValue());
				InsPlan2=Integer.valueOf(doc.getElementsByTagName("InsPlan2").item(0).getFirstChild().getNodeValue());
				DateTimeAskedToArrive=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(doc.getElementsByTagName("DateTimeAskedToArrive").item(0).getFirstChild().getNodeValue());
				ProcsColored=doc.getElementsByTagName("ProcsColored").item(0).getFirstChild().getNodeValue();
				ColorOverride=Integer.valueOf(doc.getElementsByTagName("ColorOverride").item(0).getFirstChild().getNodeValue());
			}
			catch(Exception e) {
				throw e;
			}
		}

		/** Appointment status. */
		public enum ApptStatus {
			/** 0- No appointment should ever have this status. */
			None,
			/** 1- Shows as a regularly scheduled appointment. */
			Scheduled,
			/** 2- Shows greyed out. */
			Complete,
			/** 3- Only shows on unscheduled list. */
			UnschedList,
			/** 4- Functions almost the same as Scheduled, but also causes the appointment to show on the ASAP list. */
			ASAP,
			/** 5- Shows with a big X on it. */
			Broken,
			/** 6- Planned appointment.  Only shows in Chart module. User not allowed to change this status, and it does not display as one of the options. */
			Planned,
			/** 7- Patient "post-it" note on the schedule. Shows light yellow. Shows on day scheduled just like appt, as well as in prog notes, etc. */
			PtNote,
			/** 8- Patient "post-it" note completed */
			PtNoteCompleted
		}


}
