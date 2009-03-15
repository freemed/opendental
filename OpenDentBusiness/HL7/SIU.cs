using System;
using System.Collections.Generic;
using System.Text;

namespace OpenDentBusiness.HL7 {
	public class SIU {
		public static void ProcessMessage(MessageHL7 message){
			SegmentHL7 seg=message.GetSegment(SegmentName.PID);
			int patNum=PIn.PInt(seg.GetFieldFullText(2));
			Patient pat=Patients.GetPat(patNum);
			Patient patOld=null;
			bool isNewPat = pat==null;
			if(isNewPat) {
				pat=new Patient();
				pat.PatNum=patNum;
				pat.Guarantor=patNum;
				pat.PriProv=PrefC.GetInt("PracticeDefaultProv");
				pat.BillingType=PrefC.GetInt("PracticeDefaultBillType");
			}
			else {
				patOld=pat.Copy();
			}
			SegmentPID.ProcessPID(pat,seg);
			//PV1-patient visit---------------------------
			seg=message.GetSegment(SegmentName.PV1);
			SegmentPID.ProcessPV1(pat,seg);
			//SCH- Schedule Activity Information
			seg=message.GetSegment(SegmentName.SCH);
			int aptNum=PIn.PInt(seg.GetFieldFullText(1));
			Appointment apt=Appointments.GetOneApt(aptNum);
			Appointment aptOld=null;
			bool isNewApt = apt==null;
			if(isNewApt) {
				apt=new Appointment();
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
			apt.Pattern=ProcessDuration(seg.GetFieldFullText(9));
			//10 is a duplicate of 9
			//11-Some components are duplicates.  We just want start time
			apt.AptDateTime=DateTimeParse(seg.GetFieldComponent(11,3));
			//AIG is optional, but looks like the only way to get provider----------------
			seg=message.GetSegment(SegmentName.AIG);
			int provNum=SegmentPID.ProvProcess(seg.GetField(3));
			if(provNum!=0) {
				apt.ProvNum=provNum;
			}
			//AIL,AIP seem to be optional, and I'm going to ignore them for now.
			if(isNewApt){
				Appointments.Insert(apt);
			}
			else{
				Appointments.Update(apt,aptOld);
			}
		}

		private static string ProcessDuration(string minuteTxt) {
			int minutes=PIn.PInt(minuteTxt);
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
			int year=PIn.PInt(str.Substring(0,4));
			int month=PIn.PInt(str.Substring(4,2));
			int day=PIn.PInt(str.Substring(6,2));
			int hour=PIn.PInt(str.Substring(8,2));
			int minute=PIn.PInt(str.Substring(10,2));
			//skip seconds
			DateTime retVal=new DateTime(year,month,day,hour,minute,0);
			return retVal;
		}


	}
}
