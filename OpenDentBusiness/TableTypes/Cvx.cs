using System;
using System.Collections;
using System.Drawing;

namespace OpenDentBusiness {
	///<summary>Vaccines administered.  Other tables generally use the CvxCode as their foreign key.</summary>
	[Serializable]
	public class Cvx:TableBase {
		///<summary>Primary key. .</summary>
		[CrudColumn(IsPriKey=true)]
		public long CvxNum;
		///<summary>Cvx code. Not allowed to edit this column once saved in the database.</summary>
		public string CvxCode;
		///<summary>Short Description provided by Cvx documentation.</summary>
		public string Description;
		///<summary>1 if the code is an active code, 0 if the code is inactive.</summary>
		public string IsActive;//might not need this column.


		///<summary></summary>
		public Cvx Copy() {
			return (Cvx)this.MemberwiseClone();
		}

	}
}