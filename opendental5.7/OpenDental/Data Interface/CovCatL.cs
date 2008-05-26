using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using OpenDentBusiness;

namespace OpenDental{
	public class CovCatL {
		public static void Refresh(){
			DataSet ds=Gen.GetDS(MethodNameDS.CovCats_RefreshCache);
			CovCats.FillCache(ds);
		}
	}
}
