using System;
using System.Collections;
using OpenDentBusiness.DataAccess;

namespace OpenDentBusiness{
	///<summary>Fee schedule names used to be in the definition table, but now they have their own table.  We are about to have many many more fee schedules as we start automating allowed fees.</summary>
	[DataObject("feesched")]
	public class FeeSched : DataObjectBase{
		[DataField("FeeSchedNum",PrimaryKey=true,AutoNumber=true)]
		private long feeSchedNum;
		private bool feeSchedNumChanged;
		///<summary>Primary key.</summary>
		public long FeeSchedNum{
			get{return feeSchedNum;}
			set{if(feeSchedNum!=value){feeSchedNum=value;MarkDirty();feeSchedNumChanged=true;}}
		}
		public bool FeeSchedNumChanged{
			get{return feeSchedNumChanged;}
		}

		[DataField("Description")]
		private string description;
		private bool descriptionChanged;
		///<summary>The name of the fee schedule.</summary>
		public string Description{
			get{return description;}
			set{if(description!=value){description=value;MarkDirty();descriptionChanged=true;}}
		}
		public bool DescriptionChanged{
			get{return descriptionChanged;}
		}

		[DataField("FeeSchedType")]
		private FeeScheduleType feeSchedType;
		private bool feeSchedTypeChanged;
		///<summary>Enum:FeeScheduleType</summary>
		public FeeScheduleType FeeSchedType{
			get{return feeSchedType;}
			set{if(feeSchedType!=value){feeSchedType=value;MarkDirty();feeSchedTypeChanged=true;}}
		}
		public bool FeeSchedTypeChanged{
			get{return feeSchedTypeChanged;}
		}

		[DataField("ItemOrder")]
		private long itemOrder;
		private bool itemOrderChanged;
		///<summary>Unlike with the old definition table, this ItemOrder is not as critical in the caching of data.  The item order is only for fee schedules of the same type.</summary>
		public long ItemOrder{
			get{return itemOrder;}
			set{if(itemOrder!=value){itemOrder=value;MarkDirty();itemOrderChanged=true;}}
		}
		public bool ItemOrderChanged{
			get{return itemOrderChanged;}
		}

		[DataField("IsHidden")]
		private bool isHidden;
		private bool isHiddenChanged;
		///<summary>True if the fee schedule is hidden.  Can't delete fee schedules or change their type once created.</summary>
		public bool IsHidden{
			get{return isHidden;}
			set{if(isHidden!=value){isHidden=value;MarkDirty();isHiddenChanged=true;}}
		}
		public bool IsHiddenChanged{
			get{return isHiddenChanged;}
		}
		
		public FeeSched Copy(){
			return (FeeSched)Clone();
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






