using System;
using System.Collections.Generic;
using System.Text;

namespace OpenDentBusiness.HL7 {
	public class MessageHL7 {
		public List<SegmentHL7> Segments;
		private string originalMsgText;//We'll store this for now, but I don't think we'll use it.
		public MessageType MsgType;

		///<summary>Only use this constructor when generating a message instead of parsing a message.</summary>
		internal MessageHL7(MessageType msgType) {
			Segments=new List<SegmentHL7>();
		}

		public MessageHL7(string msgtext) {
			originalMsgText=msgtext;
			Segments=new List<SegmentHL7>();
			string[] rows=msgtext.Split(new string[] { "\r\n" },StringSplitOptions.RemoveEmptyEntries);
			SegmentHL7 segment;
			for(int i=0;i<rows.Length;i++) {
				segment=new SegmentHL7(rows[i]);//this creates the field objects.
				Segments.Add(segment);
				if(i==0 && segment.Name==SegmentName.MSH) {
					string msgtype=segment.GetFieldComponent(8,0);
					if(msgtype=="ADT") {
						MsgType=MessageType.ADT;
					}
					else if(msgtype=="SIU") {
						MsgType=MessageType.SIU;
					}
					else if(msgtype=="DFT") {
						MsgType=MessageType.DFT;
					}
				}
			}
		}

		///<summary>This will always be generated on the fly, based on the FullText of all the segments combined.  FullText for any other object is cached rather than being generated on the fly.</summary>
		public override string ToString() {
			string retVal="";
			for(int i=0;i<Segments.Count;i++) {
				if(i>0) {
					retVal+="\r\n";
				}
				retVal+=Segments[i].FullText;
			}
			return retVal;
		}

		///<summary>If an optional segment is not present, it will return null.</summary>
		public SegmentHL7 GetSegment(SegmentName segmentName) {
			for(int i=0;i<Segments.Count;i++) {
				if(Segments[i].Name==segmentName) {
					return Segments[i];
				}
			}
			return null;
		}

		///<summary>The list will be ordered by sequence number.</summary>
		public List<SegmentHL7> GetSegments(SegmentName segmentName) {
			List<SegmentHL7> retVal=new List<SegmentHL7>();
			for(int i=0;i<Segments.Count;i++) {
				if(Segments[i].Name!=segmentName) {
					continue;
				}
				if(Segments[i].GetFieldFullText(1) != (retVal.Count+1).ToString()){//wrong sequence number
					continue;
				}
				retVal.Add(Segments[i]);
			}
			return retVal;
		}

		public string GenerateMessage() {
			return "";
		}
	}

	public enum MessageType {
		///<summary>This should never happen.</summary>
		Unknown,
		///<summary>Demographics - A01,A04,A08,A28,A31</summary>
		ADT,
		///<summary>Scheduling - S12,S13,S14,S15,S22</summary>
		SIU,
		///<summary>Detailed Financial Transaction - P03</summary>
		DFT
	}
}
