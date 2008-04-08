using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
//using System.Security;
//using System.Security.Cryptography;
//using System.Text;
using OpenDentBusiness;

namespace OpenDental{
	///<summary></summary>
	public class Userod_client {
		public static void Refresh(){
			DataTable table=Gen.GetTable(MethodNameTable.Userod_RefreshCache);
			Userods.FillCache(table);//now, we have an arrays on both the client and the server.
		}

		

		
		

	}
 
	

	
}













