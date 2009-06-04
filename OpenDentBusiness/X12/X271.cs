using System;
using System.Collections.Generic;

namespace OpenDentBusiness
{
	///<summary></summary>
	public class X271:X12object{

		public X271(string messageText):base(messageText){
		
		}

		///<summary>In realtime mode, X12 limits the request to one patient.  We will always use the subscriber.  So all EB segments are for the subscriber.</summary>
		public List<EB271> GetListEB() {
			List<EB271> retVal=new List<EB271>();
			EB271 eb;
			for(int i=0;i<Segments.Count;i++) {
				if(Segments[i].SegmentID != "EB") {
					continue;
				}
				eb=new EB271(Segments[i]);
				retVal.Add(eb);
			}
			return retVal;
		}

		///<summary>Only the DTP segments that come before the EB segments.  X12 loop 2100C.</summary>
		public List<DTP271> GetListDtpSubscriber() {
			List<DTP271> retVal=new List<DTP271>();
			DTP271 dtp;
			for(int i=0;i<Segments.Count;i++) {
				if(Segments[i].SegmentID!="DTP") {
					continue;
				}
				if(Segments[i].SegmentID=="EB") {
					break;
				}
				dtp=new DTP271(Segments[i]);
				retVal.Add(dtp);
			}
			return retVal;
		}
		
		


	}
}
