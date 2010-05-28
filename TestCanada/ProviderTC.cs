using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenDentBusiness;

namespace TestCanada {
	public class ProviderTC {
		public static string SetInitialProviders() {
			//Dentist #1----------------------------------
			Provider prov=ProviderC.List[0];
			prov.FName="A.";
			prov.LName="Dentist";
			prov.NationalProvID="530123401";
			prov.CanadianOfficeNum="1234";
			prov.ItemOrder=0;
			prov.Abbr="DocA";
			Providers.Update(prov);
			if(ProviderC.List.Length>1) {
				prov=ProviderC.List[1];
				prov.FName="B.";
				prov.LName="Dentist";
				prov.NationalProvID="035678900";
				prov.CanadianOfficeNum="1234";
				prov.ItemOrder=1;
				prov.Abbr="DocB";
				prov.FeeSched=53;
				Providers.Update(prov);
			}
			else {
				prov=new Provider();
				prov.FName="B.";
				prov.LName="Dentist";
				prov.NationalProvID="035678900";
				prov.CanadianOfficeNum="1234";
				prov.ItemOrder=1;
				prov.Abbr="DocB";
				prov.FeeSched=53;
				Providers.Insert(prov);
			}
			Providers.RefreshCache();
			//The billing provider for both is Dr. A.
			Prefs.UpdateLong(PrefName.InsBillingProv,0);//since Dr. A is also the default practice provider.
			Prefs.RefreshCache();
			return "Dentist objects set.";
		}

	}
}
