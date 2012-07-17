using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace OpenDentBusiness {
	///<summary>multiple segments per message</summary>
	[Serializable]
	public class HL7DefSegment:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long HL7DefSegmentNum;
		///<summary>FK to HL7DefMessage.HL7DefMessageNum</summary>
		public long HL7DefMessageNum;
		///<summary>.</summary>
		public int ItemOrder;
		///<summary>For example, a DFT can have multiple FT1 segments.</summary>
		public bool CanRepeat;
		///<summary>An incoming message may or may not contain this segment. Not used for outgoing.</summary>
		public bool IsOptional;
		///<summary>Stored in db as string, but used in OD as enum SegmentNameHL7. Example: PID.</summary>
		[CrudColumn(SpecialType=CrudSpecialColType.EnumAsString)]
		public SegmentNameHL7 SegmentName;
		///<summary>.</summary>
		[CrudColumn(SpecialType=CrudSpecialColType.TextIsClob)]
		public string Note;

		///<Summary>List of segments associated with this hierarchical definition.  Use items in this list to get to items lower in the hierarchy.</Summary>
		[CrudColumn(IsNotDbColumn=true)]
		[XmlIgnore]
		public List<HL7DefField> hl7DefFields;

		///<summary></summary>
		public HL7DefSegment Clone() {
			return (HL7DefSegment)this.MemberwiseClone();
		}

		public void AddField(HL7DefField field,int ordinalPos,string tableId,DataTypeHL7 dataType,string fieldName) {
			if(hl7DefFields==null){
				hl7DefFields=new List<HL7DefField>();
			}
			field.OrdinalPos=ordinalPos;
			field.DataType=dataType;
			field.TableId=tableId;
			field.FieldName=fieldName;
			this.hl7DefFields.Add(field);
		}

	}

	/// <summary>Keep this list alphabetized.  Reordering will not mess up the database.</summary>
	public enum SegmentNameHL7 {
		///<summary>Db Resource Appointment Information</summary>
		AIG,
		///<summary>Location Resource Appointment Information</summary>
		AIL,
		///<summary>Personnel Resource Appointment Information</summary>
		AIP,
		///<summary>Diagnosis Information</summary>
		DG1,
		///<summary>Event Type</summary>
		EVN,
		///<summary>Financial Transaction Information</summary>
		FT1,
		///<summary>Guarantor Information</summary>
		GT1,
		///<summary>Insurance Information</summary>
		IN1,
		///<summary>Message Header</summary>
		MSH,
		///<summary>Observations Request</summary>
		OBR,
		///<summary>Observation Related to OBR</summary>
		OBX,
		///<summary>Common Order.  Used in outgoing vaccinations VXUs as well as incoming lab result ORUs.</summary>
		ORC,
		///<summary>Patient Identification</summary>
		PID,
		///<summary>Patient additional demographics</summary>
		PD1,
		///<summary>Patient Visit</summary>
		PV1,
		///<summary>Pharmacy Administration Segment</summary>
		RXA,
		///<summary>Scheduling Activity Information</summary>
		SCH,
		///<summary>This can happen for unsupported segments.</summary>
		Unknown,
		///<summary>We use for PDF Data</summary>
		ZX1,
	}
}
