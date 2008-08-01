using System;
using System.Collections;
using OpenDental.DataAccess;

namespace OpenDentBusiness{
	///<summary>When some objects are deleted, we sometimes need a way to track them for synching purposes.  Other objects already have fields for IsHidden or PatStatus which track deletions just fine.  Those types of objects will not use this table.</summary>
	[DataObject("deletedobject")]
	public class DeletedObject : DataObjectBase{
		[DataField("DeletedObjectNum",PrimaryKey=true,AutoNumber=true)]
		private int deletedObjectNum;
		private bool deletedObjectNumChanged;
		///<summary>Primary key.</summary>
		public int DeletedObjectNum{
			get{return deletedObjectNum;}
			set{if(deletedObjectNum!=value){deletedObjectNum=value;MarkDirty();deletedObjectNumChanged=true;}}
		}
		public bool DeletedObjectNumChanged{
			get{return deletedObjectNumChanged;}
		}

		[DataField("ObjectNum")]
		private int objectNum;
		private bool objectNumChanged;
		///<summary>Foreign key to a number of different tables, depending on which type it is.</summary>
		public int ObjectNum{
			get{return objectNum;}
			set{if(objectNum!=value){objectNum=value;MarkDirty();objectNumChanged=true;}}
		}
		public bool ObjectNumChanged{
			get{return objectNumChanged;}
		}

		[DataField("ObjectType")]
		private DeletedObjectType objectType;
		private bool objectTypeChanged;
		///<summary>Enum:DeletedObjectType </summary>
		public DeletedObjectType ObjectType{
			get{return objectType;}
			set{if(objectType!=value){objectType=value;MarkDirty();objectTypeChanged=true;}}
		}
		public bool ObjectTypeChanged{
			get{return objectTypeChanged;}
		}

		//DateTStamp
		
		public DeletedObject Copy(){
			return (DeletedObject)Clone();
		}	
	}
}




	










