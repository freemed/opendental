using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using OpenDentBusiness;

namespace OpenDental{
	public class Account_client {
		public static void Refresh(){
			DataTable table=Gen.GetDS(MethodName.Account_RefreshCache).Tables[0];
			Accounts.FillCache(table);//now, we have an arrays on both the client and the server.
		}
	}
}
