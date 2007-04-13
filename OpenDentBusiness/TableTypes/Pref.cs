using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace OpenDentBusiness {
	///<summary>Stores small bits of data for a wide variety of purposes.  Any data that's too small to warrant its own table will usually end up here.</summary>
	public class Pref {
		///<summary>Primary key.</summary>
		public string PrefName;//
		///<summary>The stored value.</summary>
		public string ValueString;
	}

	public class DtoPrefRefresh:DtoQueryBase {
	}

	/*public class DtoDefInsert:DtoCommandBase {
		public Def DefCur;
	}

	public class DtoDefUpdate:DtoCommandBase {
		public Def DefCur;
	}*/

	



}
