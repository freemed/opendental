using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness.HL7 {
	///<summary></summary>
	public class InternalEcwTight {

		public static HL7Def GetHL7Def() {
			HL7Def def=HL7Defs.GetInternalFromDb("eCWtight");
			if(def==null) {//wasn't in the database
				def=new HL7Def();
				def.IsNew=true;
				def.Description="eClinicalWorks";
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
				def.InternalType="eCWtight";
				def.InternalTypeVersion=Assembly.GetAssembly(typeof(Db)).GetName().Version.ToString();
				def.IsEnabled=false;
				def.Note="";
			}
			//in either case, now get all child objects, which can't be in the database.
			//----------------------------------------------------------------------------------------------------------------------------------
			//eCW incoming patient information (ADT).
			HL7DefMessage msg=new HL7DefMessage();
			def.AddMessage(msg,MessageTypeHL7.ADT,EventTypeHL7.A04,InOutHL7.Incoming,0,"");
			//PID segment------------------------------------------------------------------
			HL7DefSegment seg=new HL7DefSegment();
			msg.AddSegment(seg,2,false,false,SegmentNameHL7.PID,"");
			//PID.2, Patient ID
			HL7DefField field=new HL7DefField();
			seg.AddField(field,2,"",DataTypeHL7.CX,"pat.PatNum");//TODO: PatNum in tight integration, ChartNum in stand alone mode.
			//PID.4, Alternate Patient ID
			field=new HL7DefField();
			seg.AddField(field,4,"",DataTypeHL7.CX,"pat.ChartNumber");//TODO: PID.4 is not saved with using standalone integration
			//PID.5, Patient Name
			field=new HL7DefField();
			seg.AddField(field,5,"",DataTypeHL7.XPN,"pat.nameLFM");
			//PID.7, Date/Time of Birth
			field=new HL7DefField();
			seg.AddField(field,7,"",DataTypeHL7.DTM,"pat.birthdateTime");
			//PID.8, Administrative Sex
			field=new HL7DefField();
			seg.AddField(field,8,"",DataTypeHL7.IS,"pat.Gender");
			//PID.10, Race
			field=new HL7DefField();
			seg.AddField(field,10,"",DataTypeHL7.CWE,"pat.Race");
			//PID.11, Patient Address
			field=new HL7DefField();
			seg.AddField(field,11,"",DataTypeHL7.XAD,"pat.addressCityStateZip");
			//PID.13, Phone Number - Home
			field=new HL7DefField();
			seg.AddField(field,13,"",DataTypeHL7.XTN,"pat.HmPhone");
			//PID.14, Phone Number - Business
			field=new HL7DefField();
			seg.AddField(field,14,"",DataTypeHL7.XTN,"pat.WkPhone");
			//PID.16, Marital Status
			field=new HL7DefField();
			seg.AddField(field,16,"",DataTypeHL7.CWE,"pat.Position");
			//PID.19, SSN - Patient
			field=new HL7DefField();
			seg.AddField(field,19,"",DataTypeHL7.ST,"pat.SSN");
			//PID.22, Fee Schedule
			field=new HL7DefField();
			seg.AddField(field,22,"",DataTypeHL7.ST,"pat.FeeSched");
			//GT1 segment------------------------------------------------------------------
			seg=new HL7DefSegment();
			msg.AddSegment(seg,5,false,false,SegmentNameHL7.GT1,"");
			//GT1.2, Guarantor Number
			field=new HL7DefField();
			seg.AddField(field,2,"",DataTypeHL7.CX,"guar.PatNum");
			//GT1.3, Guarantor Name
			field=new HL7DefField();
			seg.AddField(field,3,"",DataTypeHL7.XPN,"guar.nameLFM");
			//GT1.5, Guarantor Address
			field=new HL7DefField();
			seg.AddField(field,5,"",DataTypeHL7.XAD,"guar.addressCityStateZip");
			//GT1.6, Guarantor Phone Number - Home
			field=new HL7DefField();
			seg.AddField(field,6,"",DataTypeHL7.XTN,"guar.HmPhone");
			//GT1.7, Guarantor Phone Number - Business
			field=new HL7DefField();
			seg.AddField(field,7,"",DataTypeHL7.XTN,"guar.WkPhone");
			//GT1.8, Guarantor Date/Time of Birth
			field=new HL7DefField();
			seg.AddField(field,8,"",DataTypeHL7.DTM,"guar.birthdateTime");
			//GT1.9, Guarantor Administrative Sex
			field=new HL7DefField();
			seg.AddField(field,9,"",DataTypeHL7.IS,"guar.Gender");
			//GT1.12, Guarantor SSN
			field=new HL7DefField();
			seg.AddField(field,12,"",DataTypeHL7.ST,"guar.SSN");
			//------------------------------------------------------------------------------------------------------------------------------------
			//eCW incoming appointment information (SIU - Schedule information unsolicited).
			msg=new HL7DefMessage();
			def.AddMessage(msg,MessageTypeHL7.SIU,EventTypeHL7.S12,InOutHL7.Incoming,0,"");
			//PID segment------------------------------------------------------------------
			seg=new HL7DefSegment();
			msg.AddSegment(seg,2,false,false,SegmentNameHL7.PID,"");
			//PID.2
			field=new HL7DefField();
			seg.AddField(field,2,"",DataTypeHL7.CX,"pat.PatNum");
			//PID.4, Alternate Patient ID
			field=new HL7DefField();
			seg.AddField(field,4,"",DataTypeHL7.CX,"pat.ChartNumber");
			//PID.5, Patient Name
			field=new HL7DefField();
			seg.AddField(field,5,"",DataTypeHL7.XPN,"pat.nameLFM");
			//PID.7, Date/Time of Birth
			field=new HL7DefField();
			seg.AddField(field,7,"",DataTypeHL7.DTM,"pat.birthdateTime");
			//PID.8, Administrative Sex
			field=new HL7DefField();
			seg.AddField(field,8,"",DataTypeHL7.IS,"pat.Gender");
			//PID.10, Race
			field=new HL7DefField();
			seg.AddField(field,10,"",DataTypeHL7.CWE,"pat.Race");
			//PID.11, Patient Address
			field=new HL7DefField();
			seg.AddField(field,11,"",DataTypeHL7.XAD,"pat.addressCityStateZip");
			//PID.13, Phone Number - Home
			field=new HL7DefField();
			seg.AddField(field,13,"",DataTypeHL7.XTN,"pat.HmPhone");
			//PID.14, Phone Number - Business
			field=new HL7DefField();
			seg.AddField(field,14,"",DataTypeHL7.XTN,"pat.WkPhone");
			//PID.16, Marital Status
			field=new HL7DefField();
			seg.AddField(field,16,"",DataTypeHL7.CWE,"pat.Position");
			//PID.19, SSN - Patient
			field=new HL7DefField();
			seg.AddField(field,19,"",DataTypeHL7.ST,"pat.SSN");
			//PID.22, Fee Schedule
			field=new HL7DefField();
			seg.AddField(field,22,"",DataTypeHL7.ST,"pat.FeeSched");
			//SCH segment------------------------------------------------------------------
			seg=new HL7DefSegment();
			msg.AddSegment(seg,1,false,false,SegmentNameHL7.SCH,"");
			//SCH.2, Filler Appointment ID
			field=new HL7DefField();
			seg.AddField(field,2,"",DataTypeHL7.EI,"apt.AptNum");
			//SCH.7, Appointment Reason
			field=new HL7DefField();
			seg.AddField(field,7,"",DataTypeHL7.CWE,"apt.Note");
			//SCH.11, Appointment Timing Quantity
			field=new HL7DefField();
			seg.AddField(field,11,"",DataTypeHL7.TQ,"apt.lengthStartEnd");
			field=new HL7DefField();
			//AIG segment------------------------------------------------------------------
			seg=new HL7DefSegment();
			msg.AddSegment(seg,4,false,true,SegmentNameHL7.AIG,"");
			//AIG.3, Resource ID^Resource Name
			field=new HL7DefField();
			seg.AddField(field,3,"",DataTypeHL7.CWE,"prov.ProvNum");
			//PV1 segment.-----------------------------------------------------------------
			seg=new HL7DefSegment();
			msg.AddSegment(seg,3,false,true,SegmentNameHL7.PV1,"");
			//PV1.7, Attending/Primary Care Doctor
			field=new HL7DefField();
			seg.AddField(field,7,"",DataTypeHL7.XCN,"prov.provNumNameLFM");
			//------------------------------------------------------------------------------------------------------------------------------------
			//Detail financial transaction (DFT)
			msg=new HL7DefMessage();
			def.AddMessage(msg,MessageTypeHL7.DFT,EventTypeHL7.P03,InOutHL7.Outgoing,1,"");
			//MSH (Message Header) segment-------------------------------------------------
			seg=new HL7DefSegment();
			msg.AddSegment(seg,0,false,false,SegmentNameHL7.MSH,"");
//todo:Add Message header fields?
			//EVN (Event Type) segment-----------------------------------------------------
			seg=new HL7DefSegment();
			msg.AddSegment(seg,1,false,false,SegmentNameHL7.EVN,"");
//todo:Add Event type fields?
			//PID (Patient Identification) segment-----------------------------------------
			seg=new HL7DefSegment();
			msg.AddSegment(seg,2,false,false,SegmentNameHL7.PID,"");
			//PID.2, Patient ID (Account number.  eCW requires this to be the same # as came in on PID.4.)
			field=new HL7DefField();
			seg.AddField(field,2,"",DataTypeHL7.CX,"pat.ChartNumber");
			//PID.3, Patient MRN number
			field=new HL7DefField();
			seg.AddField(field,3,"",DataTypeHL7.CX,"pat.PatNum");
			//PID.5, Patient Name (Last^First^MI)
			field=new HL7DefField();
			seg.AddField(field,5,"",DataTypeHL7.XPN,"pat.nameLFM");
			//PID.7, Birthdate
			field=new HL7DefField();
			seg.AddField(field,7,"",DataTypeHL7.DTM,"pat.birthdateTime");
			//PID.8, Gender
			field=new HL7DefField();
			seg.AddField(field,8,"",DataTypeHL7.IS,"pat.Gender");
			//PID.10, Race
			field=new HL7DefField();
			seg.AddField(field,10,"",DataTypeHL7.CWE,"pat.Race");
			//PID.11, Address
			field=new HL7DefField();
			seg.AddField(field,11,"",DataTypeHL7.XAD,"pat.addressCityStateZip");
			//PID.13, Home Phone
			field=new HL7DefField();
			seg.AddField(field,13,"",DataTypeHL7.XTN,"pat.HmPhone");
			//PID.14, Work Phone
			field=new HL7DefField();
			seg.AddField(field,14,"",DataTypeHL7.XTN,"pat.WkPhone");
			//PID.16, Marital Status
			field=new HL7DefField();
			seg.AddField(field,16,"",DataTypeHL7.CWE,"pat.Position");
			//PID.19, SSN
			field=new HL7DefField();
			seg.AddField(field,19,"",DataTypeHL7.ST,"pat.SSN");
			//PV1 (Patient Visit) segment--------------------------------------------------
			seg=new HL7DefSegment();
			msg.AddSegment(seg,3,false,false,SegmentNameHL7.PV1,"");
			//PV1.7, Attending/Primary Care Doctor
			field=new HL7DefField();
			seg.AddField(field,7,"",DataTypeHL7.XCN,"prov.provNumNameLFM");
			//PV1.19, Visit Number
			field=new HL7DefField();
			seg.AddField(field,19,"",DataTypeHL7.CX,"apt.AptNum");
			//FT1 (Financial Transaction Information) segment------------------------------
			seg=new HL7DefSegment();
			msg.AddSegment(seg,4,false,false,SegmentNameHL7.FT1,"");


			return def;
		}


	}
}
