using System;
using System.Collections;
using System.Drawing;

namespace OpenDentBusiness {
	///<summary></summary>
	[Serializable]
	public class AllergyDef:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long AllergyDefNum;
		///<summary>Name of the drug.  User can change this.  If an RxCui is present, the RxNorm string can be pulled from the in-memory table for UI display in addition to the Description.</summary>
		public string Description;
		///<summary></summary>
		public bool IsHidden;
		///<summary>The last date and time this row was altered.  Not user editable.</summary>
		[CrudColumn(SpecialType=CrudSpecialColType.TimeStamp)]
		public DateTime DateTStamp;
		///<summary>SNOMED Allergy Type Code</summary>
		public SnomedAllergy Snomed;
		///<summary>RxNorm Code identifier.  FK to an in-memory dictionary of RxCui/RxNorm mappings.</summary>
		public long RxCui;

		///<summary></summary>
		public AllergyDef Copy() {
			return (AllergyDef)this.MemberwiseClone();
		}

		public enum SnomedAllergy{
			///<summary>0-</summary>
			_416098002,
			///<summary>1- </summary>
			_420134006,
			///<summary>2- </summary>
			_418038007,
			///<summary>3- </summary>
			_419511003,
			///<summary>4- </summary>
			_418471000,
			///<summary>5- </summary>
			_419199007,
			///<summary>6- </summary>
			_414285001,
			///<summary>7- </summary>
			_59037007,
			///<summary>8- </summary>
			_235719002
		}
	}
}