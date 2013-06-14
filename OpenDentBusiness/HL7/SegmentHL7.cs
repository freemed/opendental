using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace OpenDentBusiness.HL7 {
	///<summary>A 'row' in the message.  Composed of fields</summary>
	public class SegmentHL7 {
		public List<FieldHL7> Fields;
		///<summary>The name</summary>
		public SegmentNameHL7 Name;
		///<summary>The original full text of the segment.</summary>
		private string fullText;

		///<summary>Only use this constructor when generating a message instead of parsing a message.</summary>
		internal SegmentHL7(SegmentNameHL7 name) {
			fullText="";
			Name=name;
			Fields=new List<FieldHL7>();
			//remember that the "field quantity" is one more than the last index, because 0-based.
			//All fields are initially added with just one component
			//This can all probably be removed now since we add fields dynamically as needed:
			if(name==SegmentNameHL7.MSA) {
				AddFields(3);
			}
			if(name==SegmentNameHL7.MSH) {
				AddFields(12);
			}
			if(name==SegmentNameHL7.EVN) {
				AddFields(4);
			}
			if(name==SegmentNameHL7.PID) {
				AddFields(23);
			}
			if(name==SegmentNameHL7.PV1) {
				AddFields(51);
			}
			if(name==SegmentNameHL7.FT1) {
				AddFields(27);
			}
			if(name==SegmentNameHL7.DG1) {
				AddFields(5);
			}
			if(name==SegmentNameHL7.ZX1) {
				AddFields(6);
			}
			
		}

		private void AddFields(int quantity) {
			FieldHL7 field;
			for(int i=0;i<quantity;i++) {
				field=new FieldHL7();
				Fields.Add(field);
			}
		}

		///<summary>Use this constructor when we have a message to parse.</summary>
		public SegmentHL7(string rowtext) {
			FullText=rowtext;
		}

		public override string ToString() {
			return fullText;
		}

		///<summary>Setting the FullText resets all the child fields.</summary>
		public string FullText {
			get {
				return fullText;
			}
			set {
				fullText=value;
				Fields=new List<FieldHL7>();
				string[] fields=fullText.Split(new string[] { "|" },StringSplitOptions.None);
				FieldHL7 field;
				for(int i=0;i<fields.Length;i++) {
					field=new FieldHL7(fields[i]);
					Fields.Add(field);
				}
				try {
					Name=(SegmentNameHL7)Enum.Parse(typeof(SegmentNameHL7),Fields[0].FullText);
				}
				catch {
					Name=SegmentNameHL7.Unknown;
				}
			}
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

		///<summary>Pass in one val to set the whole field.  Pass in multiple vals to set multiple components.  It also sets the fullText of the segment.</summary>
		public void SetField(int fieldIndex,params string[] vals) {
			if(fieldIndex>Fields.Count-1) {
				AddFields(fieldIndex-(Fields.Count-1));
			}
			Fields[fieldIndex].SetVals(vals);
			//kind of repetitive to recalc the whole segment again here, but it goes very fast.
			fullText="";
			for(int i=0;i<Fields.Count;i++) {
				if(i>0) {
					fullText+="|";
				}
				fullText+=Fields[i].FullText;
			}
		}

		///<summary>yyyyMMdd[HHmm[ss]].  If not in that format, it returns minVal.</summary>
		public DateTime GetDateTime(int fieldIndex) {
			if(fieldIndex > Fields.Count-1) {
				return DateTime.MinValue;
			}
			string str=Fields[fieldIndex].FullText.Trim();//trailing space was causing problems.
			try {
				if(str.Length==8) {
					return DateTime.ParseExact(str,"yyyyMMdd",DateTimeFormatInfo.InvariantInfo);
				}
				if(str.Length==12) {
					return DateTime.ParseExact(str,"yyyyMMddHHmm",DateTimeFormatInfo.InvariantInfo);
				}
				if(str.Length==14) {
					return DateTime.ParseExact(str,"yyyyMMddHHmmss",DateTimeFormatInfo.InvariantInfo);
				}
			}
			catch {
				return DateTime.MinValue;
			}
			return DateTime.MinValue;
		}

	}

	
}
