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
				def.IncomingFolder=@"C:\HL7\In";
				def.OutgoingFolder=@"C:\HL7\Out";
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
			def.hl7DefMessages=new List<HL7DefMessage> ();
			//-----------------------------------------------------------------------
			//eCW incoming patient information (ADT).
			HL7DefMessage msg=new HL7DefMessage();
			msg.IsNew=true;
			msg.MessageType=MessageTypeHL7.ADT;
			msg.EventType=EventTypeHL7.A04;
			msg.InOrOut=InOutHL7.Incoming;
			msg.ItemOrder=1;
			msg.Note="ADT";
			def.hl7DefMessages.Add(msg);
			msg.hl7DefSegments=new List<HL7DefSegment>();
			//PID segment
			HL7DefSegment seg=new HL7DefSegment();
			seg.IsNew=true;
			seg.ItemOrder=1;
			seg.CanRepeat=false;
			seg.IsOptional=false;
			seg.SegmentName=SegmentNameHL7.PID;
			seg.Note="";
			msg.hl7DefSegments.Add(seg);
			seg.hl7DefFields=new List<HL7DefField>();
			//PID.2
			seg.hl7DefFields.Add(new HL7DefField(2,DataTypeHL7.ST,"patient.PatNum"));//TODO: PatNum in tight integration, ChartNum in stand alone mode.
			//PID.4
			field=new HL7DefField();
			field.IsNew=true;
			field.OrdinalPos=4;
			field.TableId="";//TODO
			field.DataType=DataTypeHL7.ST;
			field.FieldName="patient.ChartNum";//TODO: ChartNum in tight integration, not saved in stand alone.
			seg.hl7DefFields.Add(field);
			//PID.22
			field=new HL7DefField();
			field.IsNew=true;
			field.OrdinalPos=22;
			field.TableId="";//TODO
			field.DataType=DataTypeHL7.None;//TODO
			field.FieldName="";//TODO:
			seg.hl7DefFields.Add(field);
			//GT1 segment
			seg=new HL7DefSegment();
			seg.IsNew=true;
			seg.ItemOrder=4;
			seg.CanRepeat=false;
			seg.IsOptional=false;
			seg.SegmentName=SegmentNameHL7.GT1;
			seg.Note="";
			msg.hl7DefSegments.Add(seg);
			seg.hl7DefFields=new List<HL7DefField>();
			//GT1.2
			field=new HL7DefField();
			field.IsNew=true;
			field.OrdinalPos=2;
			field.TableId="";//TODO
			field.DataType=DataTypeHL7.ST;
			field.FieldName="patient.PatNum";//TODO: PatNum in tight integration, ChartNum in stand alone mode.
			seg.hl7DefFields.Add(field);
			//GT1.4
			field=new HL7DefField();
			field.IsNew=true;
			field.OrdinalPos=4;
			field.TableId="";//TODO
			field.DataType=DataTypeHL7.ST;
			field.FieldName="patient.ChartNum";//TODO: ChartNum in tight integration, not saved in stand alone.
			seg.hl7DefFields.Add(field);
			//GT1.22
			field=new HL7DefField();
			field.IsNew=true;
			field.OrdinalPos=22;
			field.TableId="";//TODO
			field.DataType=DataTypeHL7.None;//TODO
			field.FieldName="";//TODO:
			seg.hl7DefFields.Add(field);
			//-----------------------------------------------------------------------
			//eCW incoming appointment information (SIU).
			msg=new HL7DefMessage();
			msg.IsNew=true;
			msg.MessageType=MessageTypeHL7.SIU;
			msg.EventType=EventTypeHL7.S12;
			msg.InOrOut=InOutHL7.Incoming;
			msg.ItemOrder=2;
			msg.Note="SIU";
			def.hl7DefMessages.Add(msg);
			msg.hl7DefSegments=new List<HL7DefSegment>();
			//PID segment
			seg=new HL7DefSegment();
			seg.IsNew=true;
			seg.ItemOrder=1;
			seg.CanRepeat=false;
			seg.IsOptional=false;
			seg.SegmentName=SegmentNameHL7.PID;
			seg.Note="";
			msg.hl7DefSegments.Add(seg);
			seg.hl7DefFields=new List<HL7DefField>();
			//PID.2
			field=new HL7DefField();
			field.IsNew=true;
			field.OrdinalPos=2;
			field.TableId="";//TODO
			field.DataType=DataTypeHL7.CX;//TODO
			field.FieldName="patient.PatNum";
			seg.hl7DefFields.Add(field);
			//PID.4
			field=new HL7DefField();
			field.IsNew=true;
			field.OrdinalPos=4;
			field.TableId="";//TODO
			field.DataType=DataTypeHL7.ST;
			field.FieldName="patient.ChartNum";//TODO: saved to chart number when in tight integration, but what about stand alone?
			seg.hl7DefFields.Add(field);
			//TODO: other PID fields.
			//PV1 segment. TODO: Use when AIG segment is not present.
			//SCH segment
			seg=new HL7DefSegment();
			seg.IsNew=true;
			seg.ItemOrder=3;
			seg.CanRepeat=false;
			seg.IsOptional=false;
			seg.SegmentName=SegmentNameHL7.SCH;
			seg.Note="";
			msg.hl7DefSegments.Add(seg);
			seg.hl7DefFields=new List<HL7DefField>();
			//SCH.2
			field=new HL7DefField();
			field.IsNew=true;
			field.OrdinalPos=2;
			field.TableId="";//TODO
			field.DataType=DataTypeHL7.CX;
			field.FieldName="appointment.AptNum";
			seg.hl7DefFields.Add(field);
			//SCH.7
			field=new HL7DefField();
			field.IsNew=true;
			field.OrdinalPos=7;
			field.TableId="";//TODO
			field.DataType=DataTypeHL7.ST;
			field.FieldName="appointment.Note";
			seg.hl7DefFields.Add(field);
			//SCH.11
			field=new HL7DefField();
			field.IsNew=true;
			field.OrdinalPos=11;
			field.TableId="";//TODO
			field.DataType=DataTypeHL7.None;
			field.FieldName="appointment.AptDateTime";
			seg.hl7DefFields.Add(field);
			//TODO: components for 11.3 (appointment.AptDateTime) and 11.4 (appointment stop time)
			//AIG segment
			seg=new HL7DefSegment();
			seg.IsNew=true;
			seg.ItemOrder=4;
			seg.CanRepeat=false;
			seg.IsOptional=true;
			seg.SegmentName=SegmentNameHL7.AIG;
			seg.Note="";
			msg.hl7DefSegments.Add(seg);
			seg.hl7DefFields=new List<HL7DefField>();
			//AIG 3.1. TODO
			//AIG 3.2. TODO
			//AIG 3.3. TODO
			//AIG 3.4. TODO








			return def;
		}


	}
}
