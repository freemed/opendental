using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Diagnostics;

namespace OpenDentBusiness.HL7 {
	///<summary>This is the engine that will parse our incoming HL7 messages.</summary>
	public class MessageParser {
		private static bool IsNewPat;
		private static bool IsVerboseLogging;
		private static HL7Msg HL7MsgCur;
		//Open \\SERVERFILES\storage\OPEN DENTAL\Programmers Documents\Standards (X12, ADA, etc)\HL7\Version2.6\V26_CH02_Control_M4_JAN2007.doc
		//At the top of page 33, there are rules for the recipient.
		//Basically, they state that parsing should not fail just because there are extra unexpected items.
		//And parsing should also not fail if expected items are missing.

		public static void Process(MessageHL7 msg,bool isVerboseLogging) {
			HL7MsgCur=new HL7Msg();
			HL7MsgCur.HL7Status=HL7MessageStatus.InFailed;//it will be marked InProcessed once data is inserted.
			HL7MsgCur.MsgText=msg.ToString();
			HL7MsgCur.PatNum=0;
			HL7MsgCur.AptNum=0;
			List<HL7Msg> hl7Existing=HL7Msgs.GetOneExisting(HL7MsgCur);
			if(hl7Existing.Count>0) {//This message is already in the db
				HL7MsgCur.HL7MsgNum=hl7Existing[0].HL7MsgNum;
				HL7Msgs.UpdateDateTStamp(HL7MsgCur);
				msg.ControlId=HL7Msgs.GetControlId(HL7MsgCur);
				return;
			}
			else {
				//Insert as InFailed until processing is complete.  Update once complete, PatNum will have correct value, AptNum will have correct value if SIU message or 0 if ADT, and status changed to InProcessed
				HL7Msgs.Insert(HL7MsgCur);
			}
			IsVerboseLogging=isVerboseLogging;
			IsNewPat=false;
			HL7Def def=HL7Defs.GetOneDeepEnabled();
			if(def==null) {
				HL7MsgCur.Note="Could not process HL7 message.  No HL7 definition is enabled.";
				HL7Msgs.Update(HL7MsgCur);
				throw new Exception("Could not process HL7 message.  No HL7 definition is enabled.");
			}
			HL7DefMessage hl7defmsg=null;
			for(int i=0;i<def.hl7DefMessages.Count;i++) {
				if(def.hl7DefMessages[i].MessageType==msg.MsgType) {//&& def.hl7DefMessages[i].EventType==msg.EventType) { //Ignoring event type for now, we will treat all ADT's and SIU's the same
					hl7defmsg=def.hl7DefMessages[i];
				}
			}
			if(hl7defmsg==null) {//No message definition matches this message's MessageType and EventType
				HL7MsgCur.Note="Could not process HL7 message.  No definition for this type of message in the enabled HL7Def.";
				HL7Msgs.Update(HL7MsgCur);
				throw new Exception("Could not process HL7 message.  No definition for this type of message in the enabled HL7Def.");
			}
			string chartNum=null;
			long patNum=0;
			DateTime birthdate=DateTime.MinValue;
			string patLName=null;
			string patFName=null;
			bool isExistingPID=false;//Needed to add note to hl7msg table if there isn't a PID segment in the message.
			//Get patient in question, incoming messages must have a PID segment so use that to find the pat in question
			for(int s=0;s<hl7defmsg.hl7DefSegments.Count;s++) {
				if(hl7defmsg.hl7DefSegments[s].SegmentName!=SegmentNameHL7.PID) {
					continue;
				}
				int pidOrder=hl7defmsg.hl7DefSegments[s].ItemOrder;
				//we found the PID segment in the def, make sure it exists in the msg
				if(msg.Segments.Count<=pidOrder //If the number of segments in the message is less than the item order of the PID segment
					|| msg.Segments[pidOrder].GetField(0).ToString()!="PID" //Or if the segment we expect to be the PID segment is not the PID segment
				) {
					break;
				}
				isExistingPID=true;
				for(int f=0;f<hl7defmsg.hl7DefSegments[s].hl7DefFields.Count;f++) {//Go through fields of PID segment and get patnum, chartnum, patient name, and/or birthdate to locate patient
					if(hl7defmsg.hl7DefSegments[s].hl7DefFields[f].FieldName=="pat.ChartNumber") {
						int chartNumOrdinal=hl7defmsg.hl7DefSegments[s].hl7DefFields[f].OrdinalPos;
						chartNum=msg.Segments[pidOrder].Fields[chartNumOrdinal].ToString();
					}
					else if(hl7defmsg.hl7DefSegments[s].hl7DefFields[f].FieldName=="pat.PatNum") {
						int patNumOrdinal=hl7defmsg.hl7DefSegments[s].hl7DefFields[f].OrdinalPos;
						patNum=PIn.Long(msg.Segments[pidOrder].Fields[patNumOrdinal].ToString());
					}
					else if(hl7defmsg.hl7DefSegments[s].hl7DefFields[f].FieldName=="pat.birthdateTime") {
						int patBdayOrdinal=hl7defmsg.hl7DefSegments[s].hl7DefFields[f].OrdinalPos;
						birthdate=FieldParser.DateTimeParse(msg.Segments[pidOrder].Fields[patBdayOrdinal].ToString());
					}
					else if(hl7defmsg.hl7DefSegments[s].hl7DefFields[f].FieldName=="pat.nameLFM") {
						int patNameOrdinal=hl7defmsg.hl7DefSegments[s].hl7DefFields[f].OrdinalPos;
						patLName=msg.Segments[pidOrder].GetFieldComponent(patNameOrdinal,0);
						patFName=msg.Segments[pidOrder].GetFieldComponent(patNameOrdinal,1);
					}
				}
			}
			if(!isExistingPID) {
				HL7MsgCur.Note="Could not process the HL7 message due to missing PID segment.";
				HL7Msgs.Update(HL7MsgCur);
				throw new Exception("Could not process HL7 message.  Could not process the HL7 message due to missing PID segment.");
			}
			//We now have patnum, chartnum, patname, and/or birthdate so locate pat
			Patient pat=null;
			Patient patOld=null;
			if(patNum!=0) {
				pat=Patients.GetPat(patNum);
			}
			if(def.InternalType!="eCWStandalone" && pat==null) {
				IsNewPat=true;
			}
			//In eCWstandalone integration, if we couldn't locate patient by patNum or patNum was 0 then pat will still be null so try to locate by chartNum if chartNum is not null
			if(def.InternalType=="eCWStandalone" && chartNum!=null) {
				pat=Patients.GetPatByChartNumber(chartNum);
			}
			//In eCWstandalone integration, if pat is still null we need to try to locate patient by name and birthdate
			if(def.InternalType=="eCWStandalone" && pat==null) {
				long patNumByName=Patients.GetPatNumByNameAndBirthday(patLName,patFName,birthdate);
				//If patNumByName is 0 we couldn't locate by patNum, chartNum or name and birthdate so this message must be for a new patient
				if(patNumByName==0) {
					IsNewPat=true;
				}
				else {
					pat=Patients.GetPat(patNumByName);
					patOld=pat.Copy();
					pat.ChartNumber=chartNum;//from now on, we will be able to find pat by chartNumber
					Patients.Update(pat,patOld);
				}
			}
			if(IsNewPat) {
				pat=new Patient();
				if(chartNum!=null) {
					pat.ChartNumber=chartNum;
				}
				if(patNum!=0) {
					pat.PatNum=patNum;
					pat.Guarantor=patNum;
				}
				pat.PriProv=PrefC.GetLong(PrefName.PracticeDefaultProv);
				pat.BillingType=PrefC.GetLong(PrefName.PracticeDefaultBillType);
			}
			else {
				patOld=pat.Copy();
			}
			//Update hl7msg table with correct PatNum for this message
			HL7MsgCur.PatNum=pat.PatNum;
			HL7Msgs.Update(HL7MsgCur);
			//If this is a message that contains an SCH segment, loop through to find the AptNum.  Pass it to the other segments that will need it.
			long aptNum=0;
			for(int s=0;s<hl7defmsg.hl7DefSegments.Count;s++) {
				if(hl7defmsg.hl7DefSegments[s].SegmentName!=SegmentNameHL7.SCH) {
					continue;
				}
				//we found the SCH segment
				int schOrder=hl7defmsg.hl7DefSegments[s].ItemOrder;
				for(int f=0;f<hl7defmsg.hl7DefSegments[s].hl7DefFields.Count;f++) {//Go through fields of SCH segment and get AptNum
					if(hl7defmsg.hl7DefSegments[s].hl7DefFields[f].FieldName=="apt.AptNum") {
						int aptNumOrdinal=hl7defmsg.hl7DefSegments[s].hl7DefFields[f].OrdinalPos;
						aptNum=PIn.Long(msg.Segments[schOrder].Fields[aptNumOrdinal].ToString());
					}
				}
			}
			//We now have a patient object , either loaded from the db or new, and aptNum so process this message for this patient
			//We need to insert the pat to get a patnum so we can compare to guar patnum to see if relationship to guar is self
			if(IsNewPat) {
				if(isVerboseLogging) {
					EventLog.WriteEntry("OpenDentHL7","Inserted patient: "+pat.FName+" "+pat.LName,EventLogEntryType.Information);
				}
				if(pat.PatNum==0) {
					pat.PatNum=Patients.Insert(pat,false);
				}
				else {
					pat.PatNum=Patients.Insert(pat,true);
				}
				patOld=pat.Copy();
			}
			for(int i=0;i<hl7defmsg.hl7DefSegments.Count;i++) {
				try {
					SegmentHL7 seg=msg.GetSegment(hl7defmsg.hl7DefSegments[i].SegmentName,!hl7defmsg.hl7DefSegments[i].IsOptional);
					if(seg!=null) {//null if segment was not found but is optional
						ProcessSeg(pat,aptNum,hl7defmsg.hl7DefSegments[i],seg,msg);
					}
				}
				catch(ApplicationException ex) {//Required segment was missing, or other error.
					HL7MsgCur.Note="Could not process this HL7 message.  "+ex;
					HL7Msgs.Update(HL7MsgCur);
					throw new Exception("Could not process HL7 message.  "+ex);
				}
			}
			//We have processed the message so now update or insert the patient
			if(pat.FName=="" || pat.LName=="") {
				EventLog.WriteEntry("OpenDentHL7","Patient demographics not processed due to missing first or last name. PatNum:"+pat.PatNum.ToString()
					,EventLogEntryType.Information);
				HL7MsgCur.Note="Patient demographics not processed due to missing first or last name. PatNum:"+pat.PatNum.ToString();
				HL7Msgs.Update(HL7MsgCur);
				return;
			}
			if(IsNewPat) {
				if(pat.Guarantor==0) {
					pat.Guarantor=pat.PatNum;
					Patients.Update(pat,patOld);
				}
				else {
					Patients.Update(pat,patOld);
				}
			}
			else {
				if(isVerboseLogging) {
					EventLog.WriteEntry("OpenDentHL7","Updated patient: "+pat.FName+" "+pat.LName,EventLogEntryType.Information);
				}
				Patients.Update(pat,patOld);
			}
			HL7MsgCur.HL7Status=HL7MessageStatus.InProcessed;
			HL7Msgs.Update(HL7MsgCur);
		}

		public static void ProcessAck(MessageHL7 msg,bool isVerboseLogging) {
			IsVerboseLogging=isVerboseLogging;
			HL7Def def=HL7Defs.GetOneDeepEnabled();
			if(def==null) {
				throw new Exception("Could not process ACK.  No HL7 definition is enabled.");
			}
			HL7DefMessage hl7defmsg=null;
			for(int i=0;i<def.hl7DefMessages.Count;i++) {
				if(def.hl7DefMessages[i].MessageType==MessageTypeHL7.ACK && def.hl7DefMessages[i].InOrOut==InOutHL7.Incoming) {
					hl7defmsg=def.hl7DefMessages[i];
					break;
				}
			}
			if(hl7defmsg==null) {//No incoming ACK defined, do nothing with it
				throw new Exception("Could not process HL7 ACK message.  No definition for this type of message in the enabled HL7Def.");
			}
			for(int i=0;i<hl7defmsg.hl7DefSegments.Count;i++) {
				try {
					SegmentHL7 seg=msg.GetSegment(hl7defmsg.hl7DefSegments[i].SegmentName,!hl7defmsg.hl7DefSegments[i].IsOptional);
					if(seg!=null) {//null if segment was not found but is optional
						ProcessSeg(null,0,hl7defmsg.hl7DefSegments[i],seg,msg);
					}
				}
				catch(ApplicationException ex) {//Required segment was missing, or other error.
					throw new Exception("Could not process HL7 message.  "+ex);
				}
			}
		}

		public static void ProcessSeg(Patient pat,long aptNum,HL7DefSegment segDef,SegmentHL7 seg,MessageHL7 msg) {
			switch(segDef.SegmentName) {
				case SegmentNameHL7.AIG:
					ProcessAIG(pat,aptNum,segDef,seg);
					return;
				case SegmentNameHL7.GT1:
					ProcessGT1(pat,segDef,seg);
					return;
				case SegmentNameHL7.IN1:
					//ProcessIN1();
					return;
				case SegmentNameHL7.MSA:
					ProcessMSA(segDef,seg,msg);
					return;
				case SegmentNameHL7.MSH:
					ProcessMSH(segDef,seg,msg);
					return;
				case SegmentNameHL7.PD1:
					//ProcessPD1();
					return;
				case SegmentNameHL7.PID:
					ProcessPID(pat,segDef,seg);
					return;
				case SegmentNameHL7.PV1:
					ProcessPV1(pat,aptNum,segDef,seg);
					return;
				case SegmentNameHL7.SCH:
					ProcessSCH(pat,segDef,seg);
					return;
				default:
					return;
			}
		}

		public static void ProcessAIG(Patient pat,long aptNum,HL7DefSegment segDef,SegmentHL7 seg) {
			long provNum;
			int provNumOrder=0;
			for(int i=0;i<segDef.hl7DefFields.Count;i++) {
				if(segDef.hl7DefFields[i].FieldName=="prov.provIdNameLFM" || segDef.hl7DefFields[i].FieldName=="prov.provIdName") {
					provNumOrder=segDef.hl7DefFields[i].OrdinalPos;
					break;
				}
			}
			if(provNumOrder==0) {//No provIdNameLFM or provIdName field in this segment definition so do nothing with it
				return;
			}
			provNum=FieldParser.ProvProcess(seg.Fields[provNumOrder]);
			if(provNum==0) {//This segment didn't have a valid provider id in it to locate the provider (must have been blank)
				return;
			}
			Appointment apt=Appointments.GetOneApt(aptNum);//SCH segment was found and aptNum was retrieved, if no SCH segment for this message then 0
			if(apt==null) {//Just in case we can't find an apt with the aptNum from SCH segment
				return;
			}
			Appointment aptOld=apt.Clone();
			Patient patOld=pat.Copy();
			apt.ProvNum=provNum;
			pat.PriProv=provNum;
			Appointments.Update(apt,aptOld);
			Patients.Update(pat,patOld);
			return;
		}

		///<summary>If relationship is self, this method does nothing.  A new pat will later change guarantor to be same as patnum. </summary>
		public static void ProcessGT1(Patient pat,HL7DefSegment segDef,SegmentHL7 seg) {
			//Find the position of the guarNum, guarChartNum, guarName, and guarBirthdate in this HL7 segment based on the definition of a GT1
			int guarPatNumOrdinal=-1;
			int guarChartNumOrdinal=-1;
			int guarNameOrdinal=-1;
			int guarBirthdateOrdinal=-1;
			for(int i=0;i<segDef.hl7DefFields.Count;i++) {
				if(segDef.hl7DefFields[i].FieldName=="guar.PatNum") {
					guarPatNumOrdinal=segDef.hl7DefFields[i].OrdinalPos;
				}
				if(segDef.hl7DefFields[i].FieldName=="guar.ChartNumber") {
					guarChartNumOrdinal=segDef.hl7DefFields[i].OrdinalPos;
				}
				if(segDef.hl7DefFields[i].FieldName=="guar.nameLFM") {
					guarNameOrdinal=segDef.hl7DefFields[i].OrdinalPos;
				}
				if(segDef.hl7DefFields[i].FieldName=="guar.birthdateTime") {
					guarBirthdateOrdinal=segDef.hl7DefFields[i].OrdinalPos;
				}
			}
			//If neither guar.PatNum nor guar.ChartNumber are included in this GT1 definition log a message in the event log and return
			if(guarPatNumOrdinal==-1 && guarChartNumOrdinal==-1) {
				HL7MsgCur.Note="Guarantor not processed.  guar.PatNum or guar.ChartNumber must be included in the GT1 definition.  PatNum of patient:"+pat.PatNum.ToString();
				HL7Msgs.Update(HL7MsgCur);
				EventLog.WriteEntry("OpenDentHL7","Guarantor not processed.  guar.PatNum or guar.ChartNumber must be included in the GT1 definition.  PatNum of patient:"+pat.PatNum.ToString()
					,EventLogEntryType.Information);
				return;
			}
			//If guar.nameLFM is not included in this GT1 definition log a message in the event log and return
			if(guarNameOrdinal==-1) {
				HL7MsgCur.Note="Guarantor not processed due to guar.nameLFM not included in the GT1 definition.  Patnum of patient:"+pat.PatNum.ToString();
				HL7Msgs.Update(HL7MsgCur);
				EventLog.WriteEntry("OpenDentHL7","Guarantor not processed due to guar.nameLFM not included in the GT1 definition.  Patnum of patient:"+pat.PatNum.ToString()
					,EventLogEntryType.Information);
				return;
			}
			//If the first or last name are not included in this GT1 segment, log a message in the event log and return
			if(seg.GetFieldComponent(guarNameOrdinal,0)=="" || seg.GetFieldComponent(guarNameOrdinal,1)=="") {
				HL7MsgCur.Note="Guarantor not processed due to missing first or last name.  PatNum of patient:"+pat.PatNum.ToString();
				HL7Msgs.Update(HL7MsgCur);
				EventLog.WriteEntry("OpenDentHL7","Guarantor not processed due to missing first or last name.  PatNum of patient:"+pat.PatNum.ToString()
					,EventLogEntryType.Information);
				return;
			}
			//Only process GT1 if either guar.PatNum or guar.ChartNumber is included and both guar.LName and guar.FName are included
			long guarPatNum=0;
			string guarChartNum="";
			if(guarPatNumOrdinal!=-1) {
				guarPatNum=PIn.Long(seg.GetFieldFullText(guarPatNumOrdinal));
			}
			if(guarChartNumOrdinal!=-1) {
				guarChartNum=seg.GetFieldComponent(guarChartNumOrdinal);
			}
			if(guarPatNum==0 && guarChartNum=="") {//because we have an example where they sent us this (position 2 (guarPatNumOrder or guarChartNumOrder for eCW) is empty): GT1|1||^^||^^^^||||||||
				HL7MsgCur.Note="Guarantor not processed due to missing both guar.PatNum and guar.ChartNumber.  One of those numbers must be included.  PatNum of patient:"+pat.PatNum.ToString();
				HL7Msgs.Update(HL7MsgCur);
				EventLog.WriteEntry("OpenDentHL7","Guarantor not processed due to missing both guar.PatNum and guar.ChartNumber.  One of those numbers must be included.  PatNum of patient:"+pat.PatNum.ToString()
					,EventLogEntryType.Information);
				return;
			}
			if(guarPatNum==pat.PatNum || guarChartNum==pat.ChartNumber) {//if relationship is self
				return;
			}
			//Guar must be someone else
			Patient guar=null;
			Patient guarOld=null;
			//Find guarantor by guar.PatNum if defined and in this segment
			if(guarPatNum!=0) {
				guar=Patients.GetPat(guarPatNum);
			}
			else {//guarPatNum was 0 so try to get guar by guar.ChartNumber or name and birthdate
				//try to find guarantor using chartNumber
				guar=Patients.GetPatByChartNumber(guarChartNum);
				if(guar==null) {
					//try to find the guarantor by using name and birthdate
					string guarLName=seg.GetFieldComponent(guarNameOrdinal,0);
					string guarFName=seg.GetFieldComponent(guarNameOrdinal,1);
					DateTime guarBirthdate=FieldParser.DateTimeParse(seg.GetFieldFullText(guarBirthdateOrdinal));
					long guarNumByName=Patients.GetPatNumByNameAndBirthday(guarLName,guarFName,guarBirthdate);
					if(guarNumByName==0) {//guarantor does not exist in OD
						//so guar will still be null, triggering creation of new guarantor further down.
					}
					else {
						guar=Patients.GetPat(guarNumByName);
						guarOld=guar.Copy();
						guar.ChartNumber=guarChartNum;//from now on, we will be able to find guar by chartNumber
						Patients.Update(guar,guarOld);
					}
				}
			}
			//At this point we have a guarantor located in OD or guar=null so guar is new patient
			bool isNewGuar=guar==null;
			if(isNewGuar) {//then we need to add guarantor to db
				guar=new Patient();
				if(guarPatNum!=0) {
					guar.PatNum=guarPatNum;
				}
				else {
					guar.ChartNumber=guarChartNum;
				}
				guar.PriProv=PrefC.GetLong(PrefName.PracticeDefaultProv);
				guar.BillingType=PrefC.GetLong(PrefName.PracticeDefaultBillType);
			}
			else {
				guarOld=guar.Copy();
			}
			//Now that we have our guarantor, process the GT1 segment
			for(int i=0;i<segDef.hl7DefFields.Count;i++) {
				int itemOrder=segDef.hl7DefFields[i].OrdinalPos;
				switch(segDef.hl7DefFields[i].FieldName) {
					case "guar.addressCityStateZip":
						guar.Address=seg.GetFieldComponent(itemOrder,0);
						guar.Address2=seg.GetFieldComponent(itemOrder,1);
						guar.City=seg.GetFieldComponent(itemOrder,2);
						guar.State=seg.GetFieldComponent(itemOrder,3);
						guar.Zip=seg.GetFieldComponent(itemOrder,4);
						continue;
					case "guar.birthdateTime":
						guar.Birthdate=FieldParser.DateTimeParse(seg.GetFieldComponent(itemOrder));
						continue;
					case "guar.ChartNumber":
						guar.ChartNumber=seg.GetFieldComponent(itemOrder);
						continue;
					case "guar.Gender":
						guar.Gender=FieldParser.GenderParse(seg.GetFieldComponent(itemOrder));
						continue;
					case "guar.HmPhone":
						guar.HmPhone=FieldParser.PhoneParse(seg.GetFieldComponent(itemOrder));
						continue;
					case "guar.nameLFM":
						guar.LName=seg.GetFieldComponent(itemOrder,0);
						guar.FName=seg.GetFieldComponent(itemOrder,1);
						guar.MiddleI=seg.GetFieldComponent(itemOrder,2);
						continue;
					//case "guar.PatNum": Maybe do nothing??
					case "guar.SSN":
						guar.SSN=seg.GetFieldComponent(itemOrder);
						continue;
					case "guar.WkPhone":
						guar.WkPhone=FieldParser.PhoneParse(seg.GetFieldComponent(itemOrder));
						continue;
					default:
						continue;
				}
			}
			if(isNewGuar) {
				guarOld=guar.Copy();
				if(guar.PatNum==0) {
					guar.PatNum=Patients.Insert(guar,false);
				}
				else {
					guar.PatNum=Patients.Insert(guar,true);
				}
				guar.Guarantor=guar.PatNum;
				Patients.Update(guar,guarOld);
			}
			else {
				Patients.Update(guar,guarOld);
			}
			pat.Guarantor=guar.PatNum;
			return;
		}

		//public static void ProcessIN1() {
		//	return;
		//}

		public static void ProcessMSA(HL7DefSegment segDef,SegmentHL7 seg,MessageHL7 msg) {
			int ackCodeOrder=0;
			int msgControlIdOrder=0;
			//find position of AckCode in segDef for MSA seg
			for(int i=0;i<segDef.hl7DefFields.Count;i++) {
				if(segDef.hl7DefFields[i].FieldName=="ackCode") {
					ackCodeOrder=segDef.hl7DefFields[i].OrdinalPos;
				}
				if(segDef.hl7DefFields[i].FieldName=="messageControlId") {
					msgControlIdOrder=segDef.hl7DefFields[i].OrdinalPos;
				}
			}
			if(ackCodeOrder==0) {//no ackCode defined for this def of MSA, do nothing with it?
				return;
			}
			if(msgControlIdOrder==0) {//no messageControlId defined for this def of MSA, do nothing with it?
				return;
			}
			//set msg.AckCode to value in position located in def of ackcode in seg
			msg.AckCode=seg.Fields[ackCodeOrder].ToString();
			msg.ControlId=seg.Fields[msgControlIdOrder].ToString();
		}

		public static void ProcessMSH(HL7DefSegment segDef,SegmentHL7 seg,MessageHL7 msg) {
			int msgControlIdOrder=0;
			//find position of messageControlId in segDef for MSH seg
			for(int i=0;i<segDef.hl7DefFields.Count;i++) {
				if(segDef.hl7DefFields[i].FieldName=="messageControlId") {
					msgControlIdOrder=segDef.hl7DefFields[i].OrdinalPos;
					break;
				}
			}
			if(msgControlIdOrder==0) {
				return;
			}
			msg.ControlId=seg.Fields[msgControlIdOrder].ToString();
		}

		//public static void ProcessPD1() {
		//	return;
		//}

		public static void ProcessPID(Patient pat,HL7DefSegment segDef,SegmentHL7 seg) {
			for(int i=0;i<segDef.hl7DefFields.Count;i++) {
				int itemOrder=segDef.hl7DefFields[i].OrdinalPos;
				switch(segDef.hl7DefFields[i].FieldName) {
					case "pat.addressCityStateZip":
						pat.Address=seg.GetFieldComponent(itemOrder,0);
						pat.Address2=seg.GetFieldComponent(itemOrder,1);
						pat.City=seg.GetFieldComponent(itemOrder,2);
						pat.State=seg.GetFieldComponent(itemOrder,3);
						pat.Zip=seg.GetFieldComponent(itemOrder,4);
						continue;
					case "pat.birthdateTime":
						pat.Birthdate=FieldParser.DateTimeParse(seg.GetFieldComponent(itemOrder));
						continue;
					case "pat.ChartNumber":
						pat.ChartNumber=seg.GetFieldComponent(itemOrder);
						continue;
					case "pat.Gender":
						pat.Gender=FieldParser.GenderParse(seg.GetFieldComponent(itemOrder));
						continue;
					case "pat.HmPhone":
						pat.HmPhone=FieldParser.PhoneParse(seg.GetFieldComponent(itemOrder));
						continue;
					case "pat.nameLFM":
						pat.LName=seg.GetFieldComponent(itemOrder,0);
						pat.FName=seg.GetFieldComponent(itemOrder,1);
						pat.MiddleI=seg.GetFieldComponent(itemOrder,2);
						continue;
					case "pat.PatNum":
						if(pat.PatNum!=0 && pat.PatNum!=PIn.Long(seg.GetFieldComponent(itemOrder))) {
							throw new Exception("Invalid PatNum");
						}
						continue;
					case "pat.Position":
						pat.Position=FieldParser.MaritalStatusParse(seg.GetFieldComponent(itemOrder));
						continue;
					case "pat.Race":
						PatientRaceOld patientRaceOld=FieldParser.RaceParse(seg.GetFieldComponent(itemOrder));
						//Converts deprecated PatientRaceOld enum to list of PatRaces, and adds them to the PatientRaces table for the patient.
						PatientRaces.Reconcile(pat.PatNum,PatientRaces.GetPatRacesFromPatientRaceOld(patientRaceOld));
						continue;
					case "pat.SSN":
						pat.SSN=seg.GetFieldComponent(itemOrder);
						continue;
					case "pat.WkPhone":
						pat.WkPhone=FieldParser.PhoneParse(seg.GetFieldComponent(itemOrder));
						continue;
					case "pat.FeeSched":
						if(Programs.IsEnabled(ProgramName.eClinicalWorks) && ProgramProperties.GetPropVal(ProgramName.eClinicalWorks,"FeeSchedulesSetManually")=="1") {
							//if using eCW and FeeSchedulesSetManually
							continue;//do not process fee sched field, manually set by user
						}
						else {
							pat.FeeSched=FieldParser.FeeScheduleParse(seg.GetFieldComponent(itemOrder));
						}
						continue;
					default:
						continue;
				}
			}
			return;
		}

		public static void ProcessPV1(Patient pat,long aptNum,HL7DefSegment segDef,SegmentHL7 seg) {
			long provNum;
			int provNumOrder=0;
			for(int i=0;i<segDef.hl7DefFields.Count;i++) {
				if(segDef.hl7DefFields[i].FieldName=="prov.provIdName" || segDef.hl7DefFields[i].FieldName=="prov.provIdNameLFM") {
					provNumOrder=segDef.hl7DefFields[i].OrdinalPos;
					break;
				}
			}
			if(provNumOrder==0) {//No provIdName or provIdNameLFM field in this segment definition so do nothing with it
				return;
			}
			provNum=FieldParser.ProvProcess(seg.Fields[provNumOrder]);
			if(provNum==0) {//This segment didn't have a valid provider id in it to locate the provider (must have been blank) so do nothing
				return;
			}
			Appointment apt=Appointments.GetOneApt(aptNum);//SCH segment was found and aptNum was retrieved, if no SCH segment for this message then 0
			if(apt==null) {//Just in case we can't find an apt with the aptNum from SCH segment
				return;
			}
			Appointment aptOld=apt.Clone();
			Patient patOld=pat.Copy();
			apt.ProvNum=provNum;
			pat.PriProv=provNum;
			Appointments.Update(apt,aptOld);
			Patients.Update(pat,patOld);
			return;
		}

		///<summary>Returns AptNum of the incoming appointment.</summary>
		public static long ProcessSCH(Patient pat,HL7DefSegment segDef,SegmentHL7 seg) {
			string aptNote="";
			double aptLength=0;
			long aptNum=0;
			DateTime aptStart=DateTime.MinValue;
			DateTime aptStop=DateTime.MinValue;
			for(int i=0;i<segDef.hl7DefFields.Count;i++) {
				int ordinalPos=segDef.hl7DefFields[i].OrdinalPos;
				switch(segDef.hl7DefFields[i].FieldName) {
					case "apt.AptNum":
						aptNum=PIn.Long(seg.GetFieldComponent(ordinalPos));
						continue;
					case "apt.lengthStartEnd":
						aptLength=FieldParser.SecondsToMinutes(seg.GetFieldComponent(ordinalPos,2));
						aptStart=FieldParser.DateTimeParse(seg.GetFieldComponent(ordinalPos,3));
						aptStop=FieldParser.DateTimeParse(seg.GetFieldComponent(ordinalPos,4));
						continue;
					case "apt.Note":
						aptNote=seg.GetFieldComponent(ordinalPos);
						continue;
					default:
						continue;
				}
			}
			Appointment apt=Appointments.GetOneApt(aptNum);
			Appointment aptOld=null;
			bool isNewApt=apt==null;
			if(isNewApt) {
				apt=new Appointment();
				apt.AptNum=aptNum;
				apt.PatNum=pat.PatNum;
				apt.AptStatus=ApptStatus.Scheduled;
			}
			else {
				aptOld=apt.Clone();
			}
			if(apt.PatNum!=pat.PatNum) {//we can't process this message because wrong patnum.
				throw new Exception("Appointment does not match patient: "+pat.FName+" "+pat.LName+", apt.PatNum: "+apt.PatNum.ToString()+", pat.PatNum: "+pat.PatNum.ToString());
			}
			apt.Note=aptNote;
			string pattern;
			//If aptStop is MinValue we know that stop time was not sent or was not in the correct format so try to use the duration field.
			if(aptStop==DateTime.MinValue && aptLength!=0) {//Stop time is optional.  If not included we will use the duration field to calculate pattern.
				pattern=FieldParser.ProcessPattern(aptStart,aptStart.AddMinutes(aptLength));
			}
			else {//We received a good stop time or stop time is MinValue but we don't have a good aptLength so ProcessPattern will return the apt length or the default 5 minutes
				pattern=FieldParser.ProcessPattern(aptStart,aptStop);
			}
			apt.AptDateTime=aptStart;
			apt.Pattern=pattern;
			apt.ProvNum=pat.PriProv;//Set apt.ProvNum to the patient's primary provider.  This may change after processing the AIG or PV1 segments, but set here in case those segs are missing.
			if(pat.FName=="" || pat.LName=="") {
				throw new Exception("Appointment not processed due to missing patient first or last name. PatNum:"+pat.PatNum.ToString());
			}
			if(isNewApt) {
				if(IsVerboseLogging) {
					EventLog.WriteEntry("OpenDentHL7","Inserted appointment for: "+pat.FName+" "+pat.LName,EventLogEntryType.Information);
				}
				Appointments.InsertIncludeAptNum(apt,true);
			}
			else {
				if(IsVerboseLogging) {
					EventLog.WriteEntry("OpenDentHL7","Updated appointment for: "+pat.FName+" "+pat.LName,EventLogEntryType.Information);
				}
				Appointments.Update(apt,aptOld);
			}
			HL7MsgCur.AptNum=apt.AptNum;
			HL7Msgs.Update(HL7MsgCur);
			return aptNum;
		}
	}
}
