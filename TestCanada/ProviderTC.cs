using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenDentBusiness;

namespace TestCanada {
	public class ProviderTC {
		public static string SetInitialProviders() {
			//Hide any existing providers.
			for(int i=0;i<ProviderC.List.Length;i++) {
				ProviderC.List[i].IsHidden=true;
			}
			//Dr. A1---------------------------------
			Provider prov;
			prov=new Provider();
			prov.FName="A.";
			prov.LName="Dentist";
			prov.NationalProvID="530123401";
			prov.CanadianOfficeNum="1234";
			prov.ItemOrder=0;
			prov.Abbr="DocA";
			Providers.Insert(prov);
			//Dr. B1---------------------------------
			prov=new Provider();
			prov.FName="B.";
			prov.LName="Dentist";
			prov.NationalProvID="035678900";
			prov.CanadianOfficeNum="1234";
			prov.ItemOrder=1;
			prov.Abbr="DocB";
			prov.FeeSched=53;
			Providers.Insert(prov);
			//Dr. A2---------------------------------
			prov=new Provider();
			prov.FName="A.";
			prov.LName="Dentist";
			prov.NationalProvID="600567801";
			prov.CanadianOfficeNum="1234";
			prov.ItemOrder=0;
			prov.Abbr="DocA2";
			Providers.Insert(prov);
			//Dr. B1---------------------------------
			prov=new Provider();
			prov.FName="B.";
			prov.LName="Dentist";
			prov.NationalProvID="035123400";
			prov.CanadianOfficeNum="1234";
			prov.ItemOrder=1;
			prov.Abbr="DocB2";
			prov.FeeSched=53;
			Providers.Insert(prov);
			Providers.RefreshCache();
			//The billing provider for both is Dr. A.
			Prefs.UpdateLong(PrefName.InsBillingProv,0);//since Dr. A is also the default practice provider.
			//We create a fake test address for the practice, so that forms looks correct when printed, even though no such test address is provided by CDANet.
			Prefs.UpdateString(PrefName.PracticeAddress,"123 Test Ave");
			Prefs.UpdateString(PrefName.PracticeAddress2,"Suite 100");
			Prefs.UpdateString(PrefName.PracticeCity,"East Westchester");
			Prefs.UpdateString(PrefName.PracticeST,"ON");
			Prefs.UpdateString(PrefName.PracticeZip,"M7F2J9");
			Prefs.UpdateString(PrefName.PracticePhone,"123-456-7890");
			Prefs.RefreshCache();
			return "Dentist objects set.\r\n";
		}

	}
}
