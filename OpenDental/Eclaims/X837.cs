using System;
using System.Collections.Generic;
using OpenDentBusiness;

namespace OpenDental.Eclaims
{
	///<summary></summary>
	public class X837:X12object{

		public X837(string messageText):base(messageText){
		
		}
		
		///<summary>Loops through the 837 to find the transaction number for the specified claim. Will return 0 if can't find.</summary>
		public int GetTransNum(int claimNum){
			X12Segment seg;
			string curTransNumStr="";
			for(int i=0;i<functGroups[0].Transactions[0].Segments.Count;i++) {
				seg=functGroups[0].Transactions[0].Segments[i];
				if(seg.SegmentID=="ST"){
					curTransNumStr=seg.Get(2);
				}
				if(seg.SegmentID=="CLM"){
					if(seg.Get(1).TrimStart(new char[] {'0'})==claimNum.ToString()){//if for specified claim
						try {
							return PIn.PInt(curTransNumStr);
						}
						catch {
							return 0;
						}
					}
				}
			}
			return 0;
		}
		


	}
}
