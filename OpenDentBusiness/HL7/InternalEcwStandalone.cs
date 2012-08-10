using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness.HL7 {
	///<summary></summary>
	public class InternalEcwStandalone {

		public static HL7Def GetHL7Def() {
			HL7Def def=HL7Defs.GetInternalFromDb("eCWStandalone");
			if(def==null) {//wasn't in the database
				def=new HL7Def();
				def.IsNew=true;
				def.Description="eCW Standalone";
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
				def.InternalType="eCWStandalone";
				def.InternalTypeVersion=Assembly.GetAssembly(typeof(Db)).GetName().Version.ToString();
				def.IsEnabled=false;
				def.Note="";
			}
			//in either case, now get all child objects, which can't be in the database.
			//----------------------------------------------------------------------------------------------------------------------------------
			//eCW incoming patient information (ADT).
			HL7DefMessage msg=new HL7DefMessage();
			def.AddMessage(msg,MessageTypeHL7.ADT,EventTypeHL7.A04,InOutHL7.Incoming,0);
			//PID segment------------------------------------------------------------------
			HL7DefSegment seg=new HL7DefSegment();
			msg.AddSegment(seg,2,SegmentNameHL7.PID);
			//PID.2, Patient ID
			seg.AddField(2,DataTypeHL7.CX,"pat.ChartNumber");
			//PID.4, Alternate Patient ID, PID.4 is not saved with using standalone integration
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
			return def;
		}

	}
}
