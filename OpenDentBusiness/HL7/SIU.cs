using System;
using System.Collections.Generic;
using System.Text;

namespace OpenDentBusiness.HL7 {
	public class SIU {
		public static void ProcessMessage(MessageHL7 message){
			SegmentHL7 seg=message.GetSegment(SegmentName.PID,true);
			long patNum=PIn.Long(seg.GetFieldFullText(2));
			Patient pat=Patients.GetPat(patNum);
			Patient patOld=null;
			bool isNewPat = pat==null;
			if(isNewPat) {
				pat=new Patient();
				pat.PatNum=patNum;
				pat.Guarantor=patNum;
				pat.PriProv=PrefC.GetLong(PrefName.PracticeDefaultProv);
				pat.BillingType=PrefC.GetLong(PrefName.PracticeDefaultBillType);
			}
			else {
				patOld=pat.Copy();
			}
			SegmentPID.ProcessPID(pat,seg);
			//PV1-patient visit---------------------------
			seg=message.GetSegment(SegmentName.PV1,false);
			if(seg!=null) {
				SegmentPID.ProcessPV1(pat,seg);
			}
			//SCH- Schedule Activity Information
			seg=message.GetSegment(SegmentName.SCH,true);
			long aptNum=PIn.Long(seg.GetFieldFullText(1));
			Appointment apt=Appointments.GetOneApt(aptNum);
			Appointment aptOld=null;
			bool isNewApt = apt==null;
			if(isNewApt) {
				apt=new Appointment();
				apt.AptNum=aptNum;
				apt.PatNum=pat.PatNum;
				apt.AptStatus=ApptStatus.Scheduled;
			}
			else{
				aptOld=apt.Copy();
			}
			if(apt.PatNum != pat.PatNum) {
				return;//we can't process this message because wrong patnum.  No good way to notify user yet.
			}
			apt.Note=seg.GetFieldFullText(7);
			//apt.Pattern=ProcessDuration(seg.GetFieldFullText(9));
			//9 and 10 are not actually available, in spite of the documentation.
			//11-We need start time and stop time
			apt.AptDateTime=DateTimeParse(seg.GetFieldComponent(11,3));
			DateTime stopTime=DateTimeParse(seg.GetFieldComponent(11,4));
			apt.Pattern=ProcessPattern(apt.AptDateTime,stopTime);
			apt.ProvNum=pat.PriProv;//just in case there's not AIG segment.
			//AIG is optional, but looks like the only way to get provider for the appt-----------
			seg=message.GetSegment(SegmentName.AIG,false);
			if(seg!=null) {
				long provNum=SegmentPID.ProvProcess(seg.GetField(3));
				if(provNum!=0) {
					apt.ProvNum=provNum;
				}
			}
			//AIL,AIP seem to be optional, and I'm going to ignore them for now.
			if(isNewPat) {
				Patients.Insert(pat,true);
			}
			else {
				Patients.Update(pat,patOld);
			}	
			if(isNewApt){
				Appointments.Insert(apt,true);
			}
			else{
				Appointments.Update(apt,aptOld);
			}
		}

		private static string ProcessPattern(DateTime startTime,DateTime stopTime) {
			int minutes=(int)((stopTime-startTime).TotalMinutes);
			if(minutes==0){
				return "//";//we don't want it to be zero minutes
			}
			int increments5=minutes/5;
			StringBuilder pattern=new StringBuilder();
			for(int i=0;i<increments5;i++) {
				pattern.Append("X");//make it all provider time, I guess.
			}
			return pattern.ToString();
		}

		///<summary>yyyyMMddHHmmss.  If not in that format, it returns minVal.</summary>
		public static DateTime DateTimeParse(string str) {
			if(str.Length != 14) {
				return DateTime.MinValue;
			}
			int year=PIn.Int(str.Substring(0,4));
			int month=PIn.Int(str.Substring(4,2));
			int day=PIn.Int(str.Substring(6,2));
			int hour=PIn.Int(str.Substring(8,2));
			int minute=PIn.Int(str.Substring(10,2));
			//skip seconds
			DateTime retVal=new DateTime(year,month,day,hour,minute,0);
			return retVal;
		}


	}
}
