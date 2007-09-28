using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace ConsoleApplication1 {
	public class SmartCardWatcher {
		public SmartCardWatcher() {
			manager = SmartCardManager.Load();
			manager.SmartCardChanged += new SmartCardStateChangedEventHandler(OnSmartCardChanged);
			// Register all known Smart Cards
			smartCardServices = new Collection<SmartCardService>();
			smartCardServices.Add(new BelgianIdentityCard(manager));
		}

		private ISmartCardManager manager;
		private Collection<SmartCardService> smartCardServices;

		void OnSmartCardChanged(object sender, SmartCardStateChangedEventArgs e) {
			if(e.State == SmartCardState.Inserted) {
				foreach(SmartCardService service in smartCardServices) {
					if(service.IsSupported(e.Atr)){
						Console.WriteLine("Supported card found!");
						service.Test(e.Reader);
					}
				}
			}
		}

		public event EventHandler PatientCardInserted;
	}
}
