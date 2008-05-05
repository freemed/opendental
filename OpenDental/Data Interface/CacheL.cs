using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using OpenDentBusiness;

namespace OpenDental{
	public class CacheL {
		public static void Refresh(InvalidTypes itypes){
			DataSet ds=Gen.GetDS(MethodNameDS.Cache_Refresh,itypes);
			if((itypes & InvalidTypes.Defs)==InvalidTypes.Defs){
				Defs.FillCache(ds.Tables["Def"]);
			}
			if((itypes & InvalidTypes.Operatories)==InvalidTypes.Operatories){
				Operatories.FillCache(ds.Tables["Operatory"]);
			}
			if((itypes & InvalidTypes.Prefs)==InvalidTypes.Prefs){
				Prefs.FillCache(ds.Tables["Pref"]);
			}
			if((itypes & InvalidTypes.Providers)==InvalidTypes.Providers){
				Providers.FillCache(ds.Tables["Provider"]);
			}
			if((itypes & InvalidTypes.Security)==InvalidTypes.Security){
				Userods.FillCache(ds.Tables["Userod"]);
			}
			if((itypes & InvalidTypes.Views)==InvalidTypes.Views){
				ApptViews.FillCache(ds.Tables["ApptView"]);
				ApptViewItems.FillCache(ds.Tables["ApptViewItem"]);
			}



		}

	}
}
