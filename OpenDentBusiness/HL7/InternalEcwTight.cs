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
			//-----------------------------------------------------------------------
			//eCW incoming patient information (ADT).
			HL7DefMessage msg=new HL7DefMessage();
			def.AddMessage(msg,MessageTypeHL7.ADT,EventTypeHL7.A04,InOutHL7.Incoming,1,"");
			//PID segment
			HL7DefSegment seg=new HL7DefSegment();
			msg.AddSegment(seg,1,false,false,SegmentNameHL7.PID,"");
			//PID.2
			HL7DefField field=new HL7DefField();
			seg.AddField(field,2,"",DataTypeHL7.ST,"patient.PatNum");//TODO: PatNum in tight integration, ChartNum in stand alone mode.
			//PID.4
			field=new HL7DefField();
			seg.AddField(field,4,"",DataTypeHL7.ST,"patient.ChartNum");
			//PID.22
			field=new HL7DefField();
			seg.AddField(field,22,"",DataTypeHL7.None,"");
			//GT1 segment
			seg=new HL7DefSegment();
			msg.AddSegment(seg,4,false,false,SegmentNameHL7.GT1,"");
			//GT1.2
			field=new HL7DefField();
			seg.AddField(field,2,"",DataTypeHL7.ST,"patient.PatNum");
			//GT1.4
			field=new HL7DefField();
			seg.AddField(field,4,"",DataTypeHL7.ST,"patient.ChartNum");
			//GT1.22
			field=new HL7DefField();
			seg.AddField(field,22,"",DataTypeHL7.None,"");
			//-----------------------------------------------------------------------
			//eCW incoming appointment information (SIU).
			msg=new HL7DefMessage();
			def.AddMessage(msg,MessageTypeHL7.SIU,EventTypeHL7.S12,InOutHL7.Incoming,2,"");
			//PID segment
			seg=new HL7DefSegment();
			msg.AddSegment(seg,1,false,false,SegmentNameHL7.PID,"");
			//PID.2
			field=new HL7DefField();
			seg.AddField(field,2,"",DataTypeHL7.CX,"patient.PatNum");
			//PID.4
			field=new HL7DefField();
			seg.AddField(field,4,"",DataTypeHL7.ST,"patient.ChartNum");
//TODO: other PID fields.
			//PV1 segment. TODO: Use when AIG segment is not present.
			//SCH segment
			seg=new HL7DefSegment();
			msg.AddSegment(seg,3,false,false,SegmentNameHL7.SCH,"");
			//SCH.2
			field=new HL7DefField();
			seg.AddField(field,2,"",DataTypeHL7.CX,"appointment.AptNum");
			//SCH.7
			field=new HL7DefField();
			seg.AddField(field,7,"",DataTypeHL7.ST,"appointment.Note");
			//SCH.11
			field=new HL7DefField();
			seg.AddField(field,11,"",DataTypeHL7.None,"appointment.AptDateTime");
//TODO: components for 11.3 (appointment.AptDateTime) and 11.4 (appointment stop time)
			//AIG segment
			seg=new HL7DefSegment();
			msg.AddSegment(seg,4,false,true,SegmentNameHL7.AIG,"");
			seg.hl7DefFields=new List<HL7DefField>();
			//AIG 3.1. TODO
			//AIG 3.2. TODO
			//AIG 3.3. TODO
			//AIG 3.4. TODO

			return def;
		}


	}
}
