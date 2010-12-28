using System;

namespace OpenDentBusiness{

	///<summary>These are templates that are used to send simple letters to patients.</summary>
	[Serializable]
	public class Letter:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long LetterNum;
		///<summary>Description of the Letter.</summary>
		public string Description;
		///<summary>Text of the letter</summary>
		[CrudColumn(SpecialType=CrudSpecialColType.TextIsClob)]
		public string BodyText;
	}
	
	
	

}













