using System;
using System.Collections;
using OpenDentBusiness.DataAccess;

namespace OpenDentBusiness{
	///<summary>Fee schedule names used to be in the definition table, but now they have their own table.  We are about to have many many more fee schedules as we start automating allowed fees.</summary>
	[Serializable()]
	public class FeeSched : TableBase{
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long FeeSchedNum;
		///<summary>The name of the fee schedule.</summary>
		public string Description;
		///<summary>Enum:FeeScheduleType</summary>
		public FeeScheduleType FeeSchedType;
		///<summary>Unlike with the old definition table, this ItemOrder is not as critical in the caching of data.  The item order is only for fee schedules of the same type.</summary>
		public int ItemOrder;
		///<summary>True if the fee schedule is hidden.  Can't delete fee schedules or change their type once created.</summary>
		public bool IsHidden;
		
		public FeeSched Copy(){
			return (FeeSched)this.MemberwiseClone();
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






