using System;
using System.Collections.Generic;
using System.Text;

namespace OpenDentBusiness {
	public class DefC {
		///<summary>Stores all defs in a 2D array except the hidden ones.  The first dimension is the category, in int format.  The second dimension is the index of the definition in this category.  This is dependent on how it was refreshed, and not on what is in the database.  If you need to reference a specific def, then the DefNum is more effective.</summary>
		public static Def[][] Short;
		///<summary>Stores all defs in a 2D array.</summary>
		public static Def[][] Long;
	}
}
