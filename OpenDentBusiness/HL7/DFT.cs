using System;
using System.Collections.Generic;
using System.Text;

namespace OpenDentBusiness.HL7 {
	public class DFT {
		private MessageHL7 msg;

		///<summary>The constructor has all the info necessary to create the Message object.</summary>
		public DFT(Appointment apt) {
			msg=new MessageHL7(MessageType.DFT);
			SegmentHL7 segment;
			segment=new SegmentHL7(SegmentName.MSH);

			msg.Segments.Add(segment);
			segment=new SegmentHL7(SegmentName.EVN);

			msg.Segments.Add(segment);
			segment=new SegmentHL7(SegmentName.PID);
			msg.Segments.Add(segment);
			segment=new SegmentHL7(SegmentName.PV1);
			msg.Segments.Add(segment);
			segment=new SegmentHL7(SegmentName.FT1);
			msg.Segments.Add(segment);
			segment=new SegmentHL7(SegmentName.DG1);
			msg.Segments.Add(segment);
		}

		public string GenerateMessage() {
			return msg.GenerateMessage();
		}
	}
}
