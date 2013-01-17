package com.opendental.odweb.client.tabletypes;

import com.google.gwt.xml.client.Document;
import com.opendental.odweb.client.remoting.Serializing;
import com.google.gwt.i18n.client.DateTimeFormat;
import java.util.Date;

//DO NOT MAKE CHANGES TO THIS FILE.  THEY WILL GET OVERWRITTEN BY THE CRUD.
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
		public Appointment deepCopy() {
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
		public String serialize() {
			StringBuilder sb=new StringBuilder();
			sb.append("<Appointment>");
			sb.append("<AptNum>").append(AptNum).append("</AptNum>");
			sb.append("<PatNum>").append(PatNum).append("</PatNum>");
			sb.append("<AptStatus>").append(AptStatus.ordinal()).append("</AptStatus>");
			sb.append("<Pattern>").append(Serializing.escapeForXml(Pattern)).append("</Pattern>");
			sb.append("<Confirmed>").append(Confirmed).append("</Confirmed>");
			sb.append("<TimeLocked>").append((TimeLocked)?1:0).append("</TimeLocked>");
			sb.append("<Op>").append(Op).append("</Op>");
			sb.append("<Note>").append(Serializing.escapeForXml(Note)).append("</Note>");
			sb.append("<ProvNum>").append(ProvNum).append("</ProvNum>");
			sb.append("<ProvHyg>").append(ProvHyg).append("</ProvHyg>");
			sb.append("<AptDateTime>").append(DateTimeFormat.getFormat("yyyyMMddHHmmss").format(AptDateTime)).append("</AptDateTime>");
			sb.append("<NextAptNum>").append(NextAptNum).append("</NextAptNum>");
			sb.append("<UnschedStatus>").append(UnschedStatus).append("</UnschedStatus>");
			sb.append("<IsNewPatient>").append((IsNewPatient)?1:0).append("</IsNewPatient>");
			sb.append("<ProcDescript>").append(Serializing.escapeForXml(ProcDescript)).append("</ProcDescript>");
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
			sb.append("<ProcsColored>").append(Serializing.escapeForXml(ProcsColored)).append("</ProcsColored>");
			sb.append("<ColorOverride>").append(ColorOverride).append("</ColorOverride>");
			sb.append("</Appointment>");
			return sb.toString();
		}

		/** Sets all the variables on this object based on the values in the XML document.  Variables that are not in the XML document will be null or their default values.
		 * @param doc A parsed XML document.  Must be valid XML.  Does not need to contain a node for every variable on this object.
		 * @throws Exception DeserializeFromXml is entirely encased in a try catch and will throw exceptions if anything goes wrong. */
		public void deserialize(Document doc) throws Exception {
			try {
				if(Serializing.getXmlNodeValue(doc,"AptNum")!=null) {
					AptNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"AptNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"PatNum")!=null) {
					PatNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"PatNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"AptStatus")!=null) {
					AptStatus=ApptStatus.valueOf(Serializing.getXmlNodeValue(doc,"AptStatus"));
				}
				if(Serializing.getXmlNodeValue(doc,"Pattern")!=null) {
					Pattern=Serializing.getXmlNodeValue(doc,"Pattern");
				}
				if(Serializing.getXmlNodeValue(doc,"Confirmed")!=null) {
					Confirmed=Integer.valueOf(Serializing.getXmlNodeValue(doc,"Confirmed"));
				}
				if(Serializing.getXmlNodeValue(doc,"TimeLocked")!=null) {
					TimeLocked=(Serializing.getXmlNodeValue(doc,"TimeLocked")=="0")?false:true;
				}
				if(Serializing.getXmlNodeValue(doc,"Op")!=null) {
					Op=Integer.valueOf(Serializing.getXmlNodeValue(doc,"Op"));
				}
				if(Serializing.getXmlNodeValue(doc,"Note")!=null) {
					Note=Serializing.getXmlNodeValue(doc,"Note");
				}
				if(Serializing.getXmlNodeValue(doc,"ProvNum")!=null) {
					ProvNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ProvNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"ProvHyg")!=null) {
					ProvHyg=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ProvHyg"));
				}
				if(Serializing.getXmlNodeValue(doc,"AptDateTime")!=null) {
					AptDateTime=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"AptDateTime"));
				}
				if(Serializing.getXmlNodeValue(doc,"NextAptNum")!=null) {
					NextAptNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"NextAptNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"UnschedStatus")!=null) {
					UnschedStatus=Integer.valueOf(Serializing.getXmlNodeValue(doc,"UnschedStatus"));
				}
				if(Serializing.getXmlNodeValue(doc,"IsNewPatient")!=null) {
					IsNewPatient=(Serializing.getXmlNodeValue(doc,"IsNewPatient")=="0")?false:true;
				}
				if(Serializing.getXmlNodeValue(doc,"ProcDescript")!=null) {
					ProcDescript=Serializing.getXmlNodeValue(doc,"ProcDescript");
				}
				if(Serializing.getXmlNodeValue(doc,"Assistant")!=null) {
					Assistant=Integer.valueOf(Serializing.getXmlNodeValue(doc,"Assistant"));
				}
				if(Serializing.getXmlNodeValue(doc,"ClinicNum")!=null) {
					ClinicNum=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ClinicNum"));
				}
				if(Serializing.getXmlNodeValue(doc,"IsHygiene")!=null) {
					IsHygiene=(Serializing.getXmlNodeValue(doc,"IsHygiene")=="0")?false:true;
				}
				if(Serializing.getXmlNodeValue(doc,"DateTStamp")!=null) {
					DateTStamp=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"DateTStamp"));
				}
				if(Serializing.getXmlNodeValue(doc,"DateTimeArrived")!=null) {
					DateTimeArrived=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"DateTimeArrived"));
				}
				if(Serializing.getXmlNodeValue(doc,"DateTimeSeated")!=null) {
					DateTimeSeated=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"DateTimeSeated"));
				}
				if(Serializing.getXmlNodeValue(doc,"DateTimeDismissed")!=null) {
					DateTimeDismissed=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"DateTimeDismissed"));
				}
				if(Serializing.getXmlNodeValue(doc,"InsPlan1")!=null) {
					InsPlan1=Integer.valueOf(Serializing.getXmlNodeValue(doc,"InsPlan1"));
				}
				if(Serializing.getXmlNodeValue(doc,"InsPlan2")!=null) {
					InsPlan2=Integer.valueOf(Serializing.getXmlNodeValue(doc,"InsPlan2"));
				}
				if(Serializing.getXmlNodeValue(doc,"DateTimeAskedToArrive")!=null) {
					DateTimeAskedToArrive=DateTimeFormat.getFormat("yyyyMMddHHmmss").parseStrict(Serializing.getXmlNodeValue(doc,"DateTimeAskedToArrive"));
				}
				if(Serializing.getXmlNodeValue(doc,"ProcsColored")!=null) {
					ProcsColored=Serializing.getXmlNodeValue(doc,"ProcsColored");
				}
				if(Serializing.getXmlNodeValue(doc,"ColorOverride")!=null) {
					ColorOverride=Integer.valueOf(Serializing.getXmlNodeValue(doc,"ColorOverride"));
				}
			}
			catch(Exception e) {
				throw new Exception("Error deserializing Appointment: "+e.getMessage());
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
