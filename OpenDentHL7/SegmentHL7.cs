using System;
using System.Collections.Generic;
using System.Text;

namespace OpenDentHL7 {
	///<summary>A 'row' in the message.  Composed of fields</summary>
	public class SegmentHL7 {
		public List<FieldHL7> Fields;
		///<summary>The name</summary>
		public SegmentName Name;
		///<summary>The original full text of the </summary>
		public string FullText;

		public SegmentHL7(string rowtext) {
			FullText=rowtext;
			Fields=new List<FieldHL7>();
			string[] fields=rowtext.Split(new string[] { "|" },StringSplitOptions.None);
			FieldHL7 field;
			for(int i=0;i<fields.Length;i++) {
				field=new FieldHL7(fields[i]);
				Fields.Add(field);
			}
			switch(Fields[0].FullText) {
				default:
					Name=SegmentName.Unknown;
					break;
				case "MSH":
					Name=SegmentName.MSH;
					break;
				case "EVN":
					Name=SegmentName.EVN;
					break;
				case "PID":
					Name=SegmentName.PID;
					break;
				case "PV1":
					Name=SegmentName.PV1;
					break;
				case "PD1":
					Name=SegmentName.PD1;
					break;
				case "GT1":
					Name=SegmentName.GT1;
					break;
				case "IN1":
					Name=SegmentName.IN1;
					break;
			}
		}

		public override string ToString() {
			return FullText;
		}

		///<summary></summary>
		public string GetFieldFullText(int indexPos) {
			if(indexPos > Fields.Count-1) {
				return "";
			}
			return Fields[indexPos].FullText;
		}

		///<summary>Really just a handy shortcut.  Identical to getting segment 0 or to GetFieldFullText.</summary>
		public string GetFieldComponent(int fieldIndex) {
			return GetFieldFullText(fieldIndex);
		}

		public string GetFieldComponent(int fieldIndex,int segIndex) {
			if(fieldIndex > Fields.Count-1) {
				return "";
			}
			return Fields[fieldIndex].GetComponentVal(segIndex);
		}

		///<summary></summary>
		public FieldHL7 GetField(int indexPos) {
			if(indexPos > Fields.Count-1) {
				return null;
			}
			return Fields[indexPos];
		}
	}

	public enum SegmentName {
		///<summary>This can happen for unsupported segments.</summary>
		Unknown,
		///<summary>Message Header</summary>
		MSH,
		///<summary>Event Type</summary>
		EVN,
		///<summary>Patient Identification</summary>
		PID,
		///<summary>Patient Visit</summary>
		PV1,
		///<summary>Patient additional demographics</summary>
		PD1,
		///<summary>Guarantor Information</summary>
		GT1,
		///<summary>Insurance Information</summary>
		IN1,
		///<summary>Scheduling Activity Information</summary>
		SCH,
		///<summary>General Resource Appointment Information</summary>
		AIG,
		///<summary>Location Resource Appointment Information</summary>
		AIL,
		///<summary>Personnel Resource Appointment Information</summary>
		AIP
	}
}
