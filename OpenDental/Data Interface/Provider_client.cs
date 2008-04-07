using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using OpenDentBusiness;

namespace OpenDental {
	public class Provider_client {
		///<summary>Refreshes List with all providers.</summary>
		public static void RefreshOnClient(){
			DataTable table=Gen.GetTable(MethodNameTable.Providers_RefreshCache);
			Providers.FillCache(table);//now, we have an arrays on both the client and the server.
		}
	}
}
