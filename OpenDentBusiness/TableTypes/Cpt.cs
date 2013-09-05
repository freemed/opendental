using System;
using System.Collections;
using System.Drawing;

namespace OpenDentBusiness {
	///<summary>Other tables generally use the CptCode as their foreign key.</summary>
	[Serializable]
	public class Cpt:TableBase {
		///<summary>Primary key. .</summary>
		[CrudColumn(IsPriKey=true)]
		public long CptNum;
		///<summary>Cvx code. Not allowed to edit this column once saved in the database.</summary>
		public string CptCode;
		///<summary>Short Description provided by Cvx documentation.</summary>
		public string Description;


		///<summary></summary>
		public Cpt Copy() {
			return (Cpt)this.MemberwiseClone();
		}

	}
}