﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness.HL7 {
	///<summary></summary>
	public class InternalEcwTight {

		public static HL7Def GetHL7Def() {
			HL7Def def=HL7Defs.GetInternalFromDb("eCWTight");
			if(def==null) {//wasn't in the database
				def=new HL7Def();
				def.IsNew=true;
				def.Description="eCW Tight";
				def.ModeTx=ModeTxHL7.File;
				def.IncomingFolder="";
				def.OutgoingFolder="";
				def.IncomingPort="";
				def.OutgoingIpPort="";
				def.FieldSeparator="|";
				def.ComponentSeparator="^";
				def.SubcomponentSeparator="&";
				def.RepetitionSeparator="~";
				def.EscapeCharacter=@"\";
				def.IsInternal=true;
				def.InternalType="eCWTight";
				def.InternalTypeVersion=Assembly.GetAssembly(typeof(Db)).GetName().Version.ToString();
				def.IsEnabled=false;
				def.Note="";
			}
			//in either case, now get all child objects, which can't be in the database.
			//======================================================================================================================
			//eCW incoming patient information (ADT).
			HL7DefMessage msg=new HL7DefMessage();
			def.AddMessage(msg,MessageTypeHL7.ADT,EventTypeHL7.A04,InOutHL7.Incoming,0);
			//PID segment------------------------------------------------------------------
			HL7DefSegment seg=new HL7DefSegment();
			msg.AddSegment(seg,2,SegmentNameHL7.PID);
			//PID.2, Patient ID
			seg.AddField(2,DataTypeHL7.CX,"pat.PatNum");
			//PID.4, Alternate Patient ID
			seg.AddField(4,DataTypeHL7.CX,"pat.ChartNumber");
			//PID.5, Patient Name
			seg.AddField(5,DataTypeHL7.XPN,"pat.nameLFM");
			//PID.7, Date/Time of Birth
			seg.AddField(7,DataTypeHL7.DTM,"pat.birthdateTime");
			//PID.8, Administrative Sex
			seg.AddField(8,DataTypeHL7.IS,"pat.Gender");
			//PID.10, Race
			seg.AddField(10,DataTypeHL7.CWE,"pat.Race");
			//PID.11, Patient Address
			seg.AddField(11,DataTypeHL7.XAD,"pat.addressCityStateZip");
			//PID.13, Phone Number - Home
			seg.AddField(13,DataTypeHL7.XTN,"pat.HmPhone");
			//PID.14, Phone Number - Business
			seg.AddField(14,DataTypeHL7.XTN,"pat.WkPhone");
			//PID.16, Marital Status
			seg.AddField(16,DataTypeHL7.CWE,"pat.Position");
			//PID.19, SSN - Patient
			seg.AddField(19,DataTypeHL7.ST,"pat.SSN");
			//PID.22, Fee Schedule
			seg.AddField(22,DataTypeHL7.ST,"pat.FeeSched");
			//GT1 segment------------------------------------------------------------------
			seg=new HL7DefSegment();
			msg.AddSegment(seg,5,SegmentNameHL7.GT1);
			//GT1.2, Guarantor Number
			seg.AddField(2,DataTypeHL7.CX,"guar.PatNum");
			//GT1.3, Guarantor Name
			seg.AddField(3,DataTypeHL7.XPN,"guar.nameLFM");
			//GT1.5, Guarantor Address
			seg.AddField(5,DataTypeHL7.XAD,"guar.addressCityStateZip");
			//GT1.6, Guarantor Phone Number - Home
			seg.AddField(6,DataTypeHL7.XTN,"guar.HmPhone");
			//GT1.7, Guarantor Phone Number - Business
			seg.AddField(7,DataTypeHL7.XTN,"guar.WkPhone");
			//GT1.8, Guarantor Date/Time of Birth
			seg.AddField(8,DataTypeHL7.DTM,"guar.birthdateTime");
			//GT1.9, Guarantor Administrative Sex
			seg.AddField(9,DataTypeHL7.IS,"guar.Gender");
			//GT1.12, Guarantor SSN
			seg.AddField(12,DataTypeHL7.ST,"guar.SSN");
			//======================================================================================================================
			//eCW incoming appointment information (SIU - Schedule information unsolicited).
			msg=new HL7DefMessage();
			def.AddMessage(msg,MessageTypeHL7.SIU,EventTypeHL7.S12,InOutHL7.Incoming,1);
			//PID segment------------------------------------------------------------------
			seg=new HL7DefSegment();
			msg.AddSegment(seg,2,SegmentNameHL7.PID);
			//PID.2
			seg.AddField(2,DataTypeHL7.CX,"pat.PatNum");
			//PID.4, Alternate Patient ID
			seg.AddField(4,DataTypeHL7.CX,"pat.ChartNumber");
			//PID.5, Patient Name
			seg.AddField(5,DataTypeHL7.XPN,"pat.nameLFM");
			//PID.7, Date/Time of Birth
			seg.AddField(7,DataTypeHL7.DTM,"pat.birthdateTime");
			//PID.8, Administrative Sex
			seg.AddField(8,DataTypeHL7.IS,"pat.Gender");
			//PID.10, Race
			seg.AddField(10,DataTypeHL7.CWE,"pat.Race");
			//PID.11, Patient Address
			seg.AddField(11,DataTypeHL7.XAD,"pat.addressCityStateZip");
			//PID.13, Phone Number - Home
			seg.AddField(13,DataTypeHL7.XTN,"pat.HmPhone");
			//PID.14, Phone Number - Business
			seg.AddField(14,DataTypeHL7.XTN,"pat.WkPhone");
			//PID.16, Marital Status
			seg.AddField(16,DataTypeHL7.CWE,"pat.Position");
			//PID.19, SSN - Patient
			seg.AddField(19,DataTypeHL7.ST,"pat.SSN");
			//PID.22, Fee Schedule
			seg.AddField(22,DataTypeHL7.ST,"pat.FeeSched");
			//SCH segment------------------------------------------------------------------
			seg=new HL7DefSegment();
			msg.AddSegment(seg,1,SegmentNameHL7.SCH);
			//SCH.2, Filler Appointment ID
			seg.AddField(2,DataTypeHL7.EI,"apt.AptNum");
			//SCH.7, Appointment Reason
			seg.AddField(7,DataTypeHL7.CWE,"apt.Note");
			//SCH.11, Appointment Timing Quantity
			seg.AddField(11,DataTypeHL7.TQ,"apt.lengthStartEnd");
			//AIG segment------------------------------------------------------------------
			seg=new HL7DefSegment();
			msg.AddSegment(seg,4,false,true,SegmentNameHL7.AIG);
			//AIG.3, Resource ID^Resource Name (Lname, Fname all as a string)
			seg.AddField(3,DataTypeHL7.CWE,"prov.provIdName");
			//PV1 segment.-----------------------------------------------------------------
			seg=new HL7DefSegment();
			msg.AddSegment(seg,3,false,true,SegmentNameHL7.PV1);
			//PV1.7, Attending/Primary Care Doctor, UPIN^LastName^FirstName^MI
			seg.AddField(7,DataTypeHL7.XCN,"prov.provIdNameLFM");
			//=======================================================================================================================
			//Detail financial transaction (DFT)
			msg=new HL7DefMessage();
			def.AddMessage(msg,MessageTypeHL7.DFT,EventTypeHL7.P03,InOutHL7.Outgoing,2);
			//MSH (Message Header) segment-------------------------------------------------
			seg=new HL7DefSegment();
			msg.AddSegment(seg,0,SegmentNameHL7.MSH);
			//MSH.1, Encoding Characters (DataType.ST)
			seg.AddField(1,DataTypeHL7.ST,"separators^~\\&");
			//MSH.2, Sending Application
			seg.AddFieldFixed(2,DataTypeHL7.HD,"OD");
			//MSH.4, Receiving Application
			seg.AddFieldFixed(4,DataTypeHL7.HD,"ECW");
			//MSH.6, Message Date and Time (YYYYMMDDHHMMSS)
			seg.AddField(6,DataTypeHL7.DTM,"dateTime.Now");
			//MSH.8, Message Type^Event Type, example DFT^P03
			seg.AddField(8,DataTypeHL7.MSG,"messageType");
			//MSH.10, Processing ID (P-production, T-test)
			seg.AddFieldFixed(10,DataTypeHL7.PT,"P");
			//MSH.11, Version ID
			seg.AddFieldFixed(11,DataTypeHL7.VID,"2.3");
			//EVN (Event Type) segment-----------------------------------------------------
			seg=new HL7DefSegment();
			msg.AddSegment(seg,1,SegmentNameHL7.EVN);
			//EVN.1, Event Type, example P03
			seg.AddField(1,"0003",DataTypeHL7.ID,"eventType","");
			//EVN.2, Recorded Date/Time
			seg.AddField(2,DataTypeHL7.DTM,"dateTime.Now");
			//PID (Patient Identification) segment-----------------------------------------
			seg=new HL7DefSegment();
			msg.AddSegment(seg,2,SegmentNameHL7.PID);
			//PID.1, Sequence Number (1 for DFT's)
			seg.AddFieldFixed(1,DataTypeHL7.ST,"1");
			//PID.2, Patient ID (Account number.  eCW requires this to be the same # as came in on PID.4.)
			seg.AddField(2,DataTypeHL7.CX,"pat.ChartNumber");
			//PID.3, Patient MRN number
			seg.AddField(3,DataTypeHL7.CX,"pat.PatNum");
			//PID.5, Patient Name (Last^First^MI)
			seg.AddField(5,DataTypeHL7.XPN,"pat.nameLFM");
			//PID.7, Birthdate
			seg.AddField(7,DataTypeHL7.DTM,"pat.birthdateTime");
			//PID.8, Gender
			seg.AddField(8,DataTypeHL7.IS,"pat.Gender");
			//PID.10, Race
			seg.AddField(10,DataTypeHL7.CWE,"pat.Race");
			//PID.11, Address
			seg.AddField(11,DataTypeHL7.XAD,"pat.addressCityStateZip");
			//PID.13, Home Phone
			seg.AddField(13,DataTypeHL7.XTN,"pat.HmPhone");
			//PID.14, Work Phone
			seg.AddField(14,DataTypeHL7.XTN,"pat.WkPhone");
			//PID.16, Marital Status
			seg.AddField(16,DataTypeHL7.CWE,"pat.Position");
			//PID.19, SSN
			seg.AddField(19,DataTypeHL7.ST,"pat.SSN");
			//PV1 (Patient Visit) segment--------------------------------------------------
			seg=new HL7DefSegment();
			msg.AddSegment(seg,3,SegmentNameHL7.PV1);
			//PV1.7, Attending/Primary Care Doctor
			seg.AddField(7,DataTypeHL7.XCN,"prov.provIdNameLFM");
			//PV1.19, Visit Number
			seg.AddField(19,DataTypeHL7.CX,"apt.AptNum");
			//FT1 (Financial Transaction Information) segment------------------------------
			seg=new HL7DefSegment();
			msg.AddSegment(seg,4,true,true,SegmentNameHL7.FT1);
			//FT1.1, Sequence Number (starts with 1)
			seg.AddField(1,DataTypeHL7.SI,"sequenceNum");
			//FT1.4, Transaction Date (YYYYMMDDHHMMSS)
			seg.AddField(4,DataTypeHL7.DTM,"proc.procDateTime");
			//FT1.5, Transaction Posting Date (YYYYMMDDHHMMSS)
			seg.AddField(5,DataTypeHL7.DTM,"proc.procDateTime");
			//FT1.6, Transaction Type
//todo: needs table # ?
			seg.AddFieldFixed(6,DataTypeHL7.IS,"CG");
			//FT1.10, Transaction Quantity
			seg.AddFieldFixed(10,DataTypeHL7.NM,"1.0");
			//FT1.19, Diagnosis Code
			seg.AddField(19,DataTypeHL7.CWE,"proc.DiagnosticCode");
			//FT1.20, Performed by Code (provider)
			seg.AddField(20,DataTypeHL7.XCN,"prov.provNumNameLFM");
			//FT1.21, Ordering Provider
			seg.AddField(21,DataTypeHL7.XCN,"prov.provNumNameLFM");
			//FT1.22, Unit Cost (procedure fee)
			seg.AddField(22,DataTypeHL7.CP,"proc.ProcFee");
			//FT1.25, Procedure Code
			seg.AddField(25,DataTypeHL7.CNE,"proccode.ProcCode");
			//FT1.26, Modifiers (treatment area)
			seg.AddField(26,DataTypeHL7.CNE,"proc.toothSurfRange");
			//DG1 (Diagnosis) segment is optional, skip for now
			//ZX1 (PDF Data) segment-------------------------------------------------------
			seg=new HL7DefSegment();
			msg.AddSegment(seg,5,SegmentNameHL7.ZX1);
			//ZX1.1
			seg.AddFieldFixed(1,DataTypeHL7.ST,"6");
			//ZX1.2
			seg.AddFieldFixed(2,DataTypeHL7.ST,"PDF");
			//ZX1.3
			seg.AddFieldFixed(3,DataTypeHL7.ST,"PATHOLOGY^Pathology Report^L");
			//ZX1.4
			seg.AddField(4,DataTypeHL7.ST,"pdfDescription");
			//ZX1.5
			seg.AddField(5,DataTypeHL7.ST,"pdfDataAsBase64");
			return def;
		}

	}
}

