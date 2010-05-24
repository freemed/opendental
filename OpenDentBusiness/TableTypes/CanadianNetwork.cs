using System;
using System.Collections;
using System.Data;
using System.Drawing;

namespace OpenDentBusiness{

	///<summary></summary>
	[Serializable()]
	public class CanadianNetwork:TableBase{
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long CanadianNetworkNum;
		///<summary>This will also be the folder name</summary>
		public string Abbrev;
		///<summary></summary>
		public string Descript;

		///<summary></summary>
		public CanadianNetwork Copy() {
			return (CanadianNetwork)this.MemberwiseClone();
		}
	}

	
}




