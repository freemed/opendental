using System;
using System.Collections.Generic;

namespace OpenDentBusiness
{
	///<summary>One EB segment from a 271.  Helps to organize a 271 for presentation to the user.</summary>
	public class EB271{
		public X12Segment Segment;
		///<summary>Can be null if the segment can't be translated to an appropriate benefit.</summary>
		public Benefit Benefitt;

		public EB271(X12Segment segment)  {
			Segment=segment;
		}

		
		


	}
}
