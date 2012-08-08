using System;
using System.Collections;
using System.Drawing;

namespace OpenDentBusiness{
	///<summary>Multiple fields per segment.</summary>
	[Serializable()]
	public class HL7DefField:TableBase{
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long HL7DefFieldNum;
		///<summary>FK to HL7DefSegment.HL7DefSegmentNum</summary>
		public long HL7DefSegmentNum;
		///<summary>Position within the segment.</summary>
		public int OrdinalPos;
		///<summary>HL7 table Id, if applicable. Example: 0234. Example: 1234/2345</summary>
		public string TableId;
		///<summary>The DataTypeHL7 enum will be unlinked from the db by storing as string in db. As it's loaded into OD, it will become an enum.</summary>
		[CrudColumn(SpecialType=CrudSpecialColType.EnumAsString)]
		public DataTypeHL7 DataType;
		///<summary>User will get to pick from a list of fields that we will maintain. For example, guarantor.LName, provider.Abbr, or patient.addressFull</summary>
		public string FieldName;

		///<summary></summary>
		public HL7DefField Clone() {
			return (HL7DefField)this.MemberwiseClone();
		}

	}

	///<summary>Data types are listed in HL7 docs section 2.15.  The items in this enumeration can be freely rearranged without damaging the database.  But can't change spelling or remove existing item.</summary>
	public enum DataTypeHL7 {
		///<summary>Extended composite ID with check digit.</summary>
		CX,
		///<summary>Date</summary>
		DT,
		///<summary>String, alphanumeric</summary>
		ST	
	}

	
}
