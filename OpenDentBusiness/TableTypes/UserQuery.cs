using System;

namespace OpenDentBusiness{

	///<summary>A list of query favorites that users can run.</summary>
	[Serializable]
	public class UserQuery:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long QueryNum;
		///<summary>Description.</summary>
		public string Description;
		///<summary>The name of the file to export to.</summary>
		public string FileName;
		///<summary>The text of the query.</summary>
		[CrudColumn(SpecialType=CrudSpecialColType.TextIsClob)]
		public string QueryText;
	}

	
	

	
}













