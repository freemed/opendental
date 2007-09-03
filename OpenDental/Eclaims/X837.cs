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
			string curTransNumStr="";
			for(int i=0;i<Segments.Count;i++) {
				if(Segments[i].SegmentID=="ST"){
					curTransNumStr=Segments[i].Get(2);
				}
				if(Segments[i].SegmentID=="CLM"){
					if(Segments[i].Get(1).TrimStart(new char[] {'0'})==claimNum.ToString()){//if for specified claim
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
