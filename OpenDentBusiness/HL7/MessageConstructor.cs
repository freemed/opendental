using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace OpenDentBusiness.HL7 {
	///<summary>This is the engine that will construct our outgoing HL7 messages.</summary>
	public static class MessageConstructor {

		///<summary>Returns null if there is no DFT defined for the enabled HL7Def.</summary>
		public static MessageHL7 GenerateDFT(List<Procedure> procList,EventTypeHL7 eventType,Patient pat,Patient guar,long aptNum,string pdfDescription,string pdfDataString) {//add event (A04 etc) parameters later if needed
			//In \\SERVERFILES\storage\OPEN DENTAL\Programmers Documents\Standards (X12, ADA, etc)\HL7\Version2.6\V26_CH02_Control_M4_JAN2007.doc
			//On page 28, there is a Message Construction Pseudocode as well as a flowchart which might help.
			Provider prov=Providers.GetProv(Patients.GetProvNum(pat));
			Appointment apt=Appointments.GetOneApt(aptNum);
			MessageHL7 messageHL7=new MessageHL7(MessageTypeHL7.DFT);
			HL7Def hl7Def=HL7Defs.GetOneDeepEnabled();
			//find a DFT message in the def
			HL7DefMessage hl7DefMessage=null;
			for(int i=0;i<hl7Def.hl7DefMessages.Count;i++) {
				if(hl7Def.hl7DefMessages[i].MessageType==MessageTypeHL7.DFT) {
					hl7DefMessage=hl7Def.hl7DefMessages[i];
					continue;
				}
			}
			if(hl7DefMessage==null) {//DFT message type is not defined so do nothing and return
				return null;
			}
			for(int s=0;s<hl7DefMessage.hl7DefSegments.Count;s++) {
				int countRepeat=1;
				if(hl7DefMessage.hl7DefSegments[s].SegmentName==SegmentNameHL7.FT1) {
					countRepeat=procList.Count;
				}
				//for example, countRepeat can be zero in the case where we are only sending a PDF of the TP to eCW, and no procs.
				for(int repeat=0;repeat<countRepeat;repeat++) {//FT1 is optional and can repeat so add as many FT1's as procs in procList
					if(hl7DefMessage.hl7DefSegments[s].SegmentName==SegmentNameHL7.FT1) {
						prov=Providers.GetProv(procList[repeat].ProvNum);
					}
					SegmentHL7 seg=new SegmentHL7(hl7DefMessage.hl7DefSegments[s].SegmentName);
					seg.SetField(0,hl7DefMessage.hl7DefSegments[s].SegmentName.ToString());
					for(int f=0;f<hl7DefMessage.hl7DefSegments[s].hl7DefFields.Count;f++) {
						string fieldName=hl7DefMessage.hl7DefSegments[s].hl7DefFields[f].FieldName;
						if(fieldName=="") {//If fixed text instead of field name just add text to segment
							seg.SetField(hl7DefMessage.hl7DefSegments[s].hl7DefFields[f].OrdinalPos,hl7DefMessage.hl7DefSegments[s].hl7DefFields[f].FixedText);
						}
						else {
							seg.SetField(hl7DefMessage.hl7DefSegments[s].hl7DefFields[f].OrdinalPos,
								FieldConstructor.GenerateDFT(hl7Def,fieldName,pat,prov,procList[repeat],guar,apt,repeat+1,eventType,pdfDescription,pdfDataString));
						}
					}
					messageHL7.Segments.Add(seg);
				}
			}
			return messageHL7;
		}

		///<summary>Returns null if no HL7 def is enabled.</summary>
		public static MessageHL7 GenerateACK(string controlId,EventTypeHL7 eventType,bool isAck) {
			MessageHL7 messageHL7=new MessageHL7(MessageTypeHL7.ACK);
			messageHL7.ControlId=controlId;
			messageHL7.EventType=eventType;
			HL7Def hl7Def=HL7Defs.GetOneDeepEnabled();
			if(hl7Def==null) {
				return null;//no def enabled, return null
			}
			//find an ACK message in the def
			HL7DefMessage hl7DefMessage=null;
			for(int i=0;i<hl7Def.hl7DefMessages.Count;i++) {
				if(hl7Def.hl7DefMessages[i].MessageType==MessageTypeHL7.ACK) {
					hl7DefMessage=hl7Def.hl7DefMessages[i];
					continue;
				}
			}
			if(hl7DefMessage==null) {//ACK message type is not defined so do nothing and return
				return null;
			}
			//go through each segment in the def
			for(int s=0;s<hl7DefMessage.hl7DefSegments.Count;s++) {
				SegmentHL7 seg=new SegmentHL7(hl7DefMessage.hl7DefSegments[s].SegmentName);
				seg.SetField(0,hl7DefMessage.hl7DefSegments[s].SegmentName.ToString());
				for(int f=0;f<hl7DefMessage.hl7DefSegments[s].hl7DefFields.Count;f++) {
					string fieldName=hl7DefMessage.hl7DefSegments[s].hl7DefFields[f].FieldName;
					if(fieldName=="") {//If fixed text instead of field name just add text to segment
						seg.SetField(hl7DefMessage.hl7DefSegments[s].hl7DefFields[f].OrdinalPos,hl7DefMessage.hl7DefSegments[s].hl7DefFields[f].FixedText);
					}
					else {
						seg.SetField(hl7DefMessage.hl7DefSegments[s].hl7DefFields[f].OrdinalPos,FieldConstructor.GenerateACK(hl7Def,fieldName,eventType,controlId,isAck));
					}
				}
				messageHL7.Segments.Add(seg);
			}
			return messageHL7;
		}

	}
}
