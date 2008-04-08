using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using OpenDentBusiness;

namespace OpenDental{
	public class ApptView_client {
		public static void Refresh(){
			DataTable table=Gen.GetTable(MethodNameTable.ApptView_RefreshCache);
			ApptViews.FillCache(table);//now, we have an arrays on both the client and the server.
		}
	}
}
