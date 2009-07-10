using System;
using System.Collections.Generic;
using System.Text;

namespace OpenDentBusiness.HL7 {
	public class ADT {
		public static void ProcessMessage(MessageHL7 message,bool isStandalone){
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
			//MSH-Ignore
			//EVN-Ignore
			//PID-------------------------------------
			SegmentHL7 seg=message.GetSegment(SegmentName.PID,true);
			int patNum=PIn.PInt(seg.GetFieldFullText(2));
			Patient pat=null;
			if(isStandalone) {
				pat=Patients.GetPatByChartNumber(patNum.ToString());
				if(pat==null) {
					//try to find the patient in question by using name and birthdate
					string lName=seg.GetFieldComponent(5,0);
					string fName=seg.GetFieldComponent(5,1);
					DateTime birthdate=SegmentPID.DateParse(seg.GetFieldFullText(7));
					int patNumByName=Patients.GetPatNumByNameAndBirthday(lName,fName,birthdate);
					if(patNumByName==0) {//patient does not exist in OD
						//so pat will still be null, triggering creation of new patient further down.
					}
					else {
						pat=Patients.GetPat(patNumByName);
						pat.ChartNumber=patNum.ToString();//from now on, we will be able to find pat by chartNumber
					}
				}
			}
			else {
				pat=Patients.GetPat(patNum);
			}
			Patient patOld=null;
			bool isNewPat = pat==null;
			if(isNewPat) {
				pat=new Patient();
				if(isStandalone) {
					pat.ChartNumber=patNum.ToString();
				}
				else {
					pat.PatNum=patNum;
				}
				//this line does not work if isStandalone, so moved to end
				//pat.Guarantor=patNum;
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
			SegmentPID.ProcessGT1(pat,seg,isStandalone);
			//IN1-Insurance-------------------------------------
			List<SegmentHL7> segments=message.GetSegments(SegmentName.IN1);
			for(int i=0;i<segments.Count;i++) {
				ProcessIN1(pat,seg);
			}
			if(isNewPat) {
				Patients.Insert(pat,true);
				if(pat.Guarantor==0) {
					patOld=pat.Copy();
					pat.Guarantor=pat.PatNum;
					Patients.Update(pat,patOld);
				}
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
