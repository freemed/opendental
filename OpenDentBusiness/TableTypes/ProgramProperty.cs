using System;

namespace OpenDentBusiness{

	///<summary>Some program links (bridges), have properties that need to be set.  The property names are always hard coded.  User can change the value.  The property is usually retrieved based on its name.</summary>
	[Serializable]
	public class ProgramProperty:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long ProgramPropertyNum;
		///<summary>FK to program.ProgramNum</summary>
		public long ProgramNum;
		///<summary>The description or prompt for this property.  Blank for workstation overrides of program path.</summary>
		public string PropertyDesc;
		///<summary>The value.  </summary>
		public string PropertyValue;
		///<summary>The human-readable name of the computer on the network (not the IP address).  Only used when overriding program path.  Blank for typical Program Properties.</summary>
		public string ComputerName;
	}

	



	

	


}










