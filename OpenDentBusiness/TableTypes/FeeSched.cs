using System;
using System.Collections;

namespace OpenDentBusiness{
	
	///<summary>Fee schedule names used to be in the definition table, but now they have their own table.  We are about to have many many more fee schedules as we start automating allowed fees.</summary>
	public class FeeSched{
		///<summary>Primary key.</summary>
		public int FeeSchedNum;
		///<summary>The name of the fee schedule.</summary>
		public string Description;
		///<summary>Enum:FeeScheduleType</summary>
		public FeeScheduleType FeeSchedType;
		///<summary>Unlike with the old definition table, this ItemOrder is not as critical in the caching of data.  The item order is only for fee schedules of the same type.</summary>
		public int ItemOrder;
		///<summary>Primary key.</summary>
		public bool IsHidden;
		
		///<summary></summary>
		public FeeSched Copy(){
			return (FeeSched)MemberwiseClone();
		}

	}

	public enum FeeScheduleType{
		///<summary>0</summary>
		Normal,
		///<summary>1</summary>
		CoPay,
		///<summary>2</summary>
		Allowed
	}

}













