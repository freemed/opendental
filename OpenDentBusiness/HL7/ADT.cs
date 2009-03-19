using System;
using System.Collections.Generic;
using System.Text;

namespace OpenDentBusiness.HL7 {
	public class ADT {
		public static void ProcessMessage(MessageHL7 message){
			/*string triggerevent=message.Segments[0].GetFieldComponent(8,1);
			switch(triggerevent) {
				case "A01"://Admit/Visit Information

					break;
				case "A04"://New Patient Information
					ProcessNewPatient(message);
					break;
				case "A08"://Update Patient Information

					break;
				case "A28"://Add Patient Information

					break;
				case "A31"://Update Patient Information

					break;
			}*/
		//}

		//private static void ProcessADTMessage(MessageHL7 message) {
			//MSH-Ignore
			//EVN-Ignore
			//PID-------------------------------------
			SegmentHL7 seg=message.GetSegment(SegmentName.PID,true);
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
			else{
				patOld=pat.Copy();
			}
			SegmentPID.ProcessPID(pat,seg);
			//PV1-patient visit---------------------------
			seg=message.GetSegment(SegmentName.PV1,false);
			if(seg!=null) {//this seg is optional
				SegmentPID.ProcessPV1(pat,seg);
			}
			//PD1-additional patient demographics------------
			seg=message.GetSegment(SegmentName.PD1,false);
			if(seg!=null) {//this seg is optional
				ProcessPD1(pat,seg);
			}
			//GT1-Guarantor-------------------------------------
			seg=message.GetSegment(SegmentName.GT1,true);
			SegmentPID.ProcessGT1(pat,seg);
			//IN1-Insurance-------------------------------------
			List<SegmentHL7> segments=message.GetSegments(SegmentName.IN1);
			for(int i=0;i<segments.Count;i++) {
				ProcessIN1(pat,seg);
			}
			if(isNewPat) {
				Patients.Insert(pat,true);
			}
			else {
				Patients.Update(pat,patOld);
			}				
		}

		public static void ProcessPD1(Patient pat,SegmentHL7 seg) {
			int provNum=SegmentPID.ProvProcess(seg.GetField(4));//don't know why both
			if(provNum!=0) {
				pat.PriProv=provNum;
			}
		}

		public static void ProcessIN1(Patient pat,SegmentHL7 seg) {
			//as a general strategy, if certain things are the same, like subscriber and carrier,
			//then we change the existing plan.
			//However, if basics change at all, then we drop the old plan and create a new one
			int ordinal=PIn.PInt(seg.GetFieldFullText(1));
			PatPlan oldPatPlan=PatPlans.GetPatPlan(pat.PatNum,ordinal);
			if(oldPatPlan==null) {
				//create a new plan and a new patplan
			}
			//InsPlan oldPlan=InsPlans.g
			//we'll have to get back to this.  This is lower priority than appointments.
		}

		


	}


}
