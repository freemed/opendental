using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using OpenDentBusiness;

namespace OpenDental {
	public class GroupPermission_client {
		public static void Refresh(){
			DataTable table=Gen.GetTable(MethodNameTable.GroupPermission_RefreshCache);
			GroupPermissions.FillCache(table);//now, we have an arrays on both the client and the server.
		}
	}
}
