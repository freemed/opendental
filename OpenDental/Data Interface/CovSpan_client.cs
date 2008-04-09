using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using OpenDentBusiness;

namespace OpenDental{
	public class CovSpan_client {
		public static void Refresh(){
			DataTable table=Gen.GetTable(MethodNameTable.CovSpan_RefreshCache);
			CovSpans.FillCache(table);//now, we have an arrays on both the client and the server.
		}
	}
}
