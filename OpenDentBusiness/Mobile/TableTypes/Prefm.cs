using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenDentBusiness.Mobile {
	/// <summary>This table is called preference in the mobile database.  This is to simply to avoid having to rewrite DataConnection.TestConnection().  The primary key of this table has an m in it to remind us that the preferences are totally different than in the main program.</summary>
	public class Prefm {
		///<summary>Primary key.</summary>
		public string PrefmName;//
		///<summary>The stored value.</summary>
		public string ValueString;
	}

	///<summary>Because this enum is stored in the database as strings rather than as numbers, we can do the order alphabetically and we can change it whenever we want.</summary>
	public enum PrefmName {

	}
}
