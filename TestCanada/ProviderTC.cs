using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenDentBusiness;

namespace TestCanada {
	public class ProviderTC {
		public static string SetInitialProviders() {
			//Dentist #1----------------------------------
			Provider prov;
			if(ProviderC.List.Length>0) {
				prov=ProviderC.List[0];
				prov.FName="A.";
				prov.LName="Dentist";
				prov.NationalProvID="530123401";
				prov.CanadianOfficeNum="1234";
				prov.ItemOrder=0;
				prov.Abbr="DocA";
				Providers.Update(prov);
			}
			else {
				prov=new Provider();
				prov.FName="A.";
				prov.LName="Dentist";
				prov.NationalProvID="530123401";
				prov.CanadianOfficeNum="1234";
				prov.ItemOrder=0;
				prov.Abbr="DocA";
				Providers.Insert(prov);
			}
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
			//We create a fake test address for the practice, so that forms looks correct when printed, even though no such test address is provided by CDANet.
			Prefs.UpdateString(PrefName.PracticeAddress,"123 Test Ave");
			Prefs.UpdateString(PrefName.PracticeAddress2,"Suite 100");
			Prefs.UpdateString(PrefName.PracticeCity,"East Westchester");
			Prefs.UpdateString(PrefName.PracticeST,"ON");
			Prefs.UpdateString(PrefName.PracticeZip,"M7F2J9");
			Prefs.RefreshCache();
			return "Dentist objects set.\r\n";
		}

	}
}
