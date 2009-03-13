using System;
using System.Collections.Generic;
using System.Text;

namespace OpenDentBusiness.HL7 {
	public class SIU {
		public static void ProcessMessage(MessageHL7 message){
			SegmentHL7 seg=message.GetSegment(SegmentName.SCH);





		}


	}
}
