using System;

namespace OpenDental.Eclaims
{
	///<summary></summary>
	public class X997{
		public static bool Is997(X12object xobj){
			//There is only one transaction set per functional group, but I think there can be multiple functional groups
			//if acking multiple 
			if(xobj.functGroups.Count!=1){
				return false;
			}
			if(xobj.functGroups[0].Header.Get(1)=="997"){
				return true;
			}
			return false;
		}

		/*Example 997
		ISA*00*          *00*          *ZZ*113504607      *ZZ*               *070813*0930*U*00401*705547511*0*P*:~
		GS*FA*113504607**20070813*0930*705547511*X*004010X097A1~
		ST*997*0001~
		AK1*HC*0001~
		AK2*837*0001~
		AK5*A~
		AK9*A*1*1*1~
		SE*6*0001~
		GE*1*705547511~
		IEA*1*705547511~
		*/
		//the only rows that we evaluate are AK2, which has transaction# (batchNumber), and AK5 which has ack code.


	}
}
