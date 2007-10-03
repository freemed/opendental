using System;
using System.Collections.Generic;
using System.Text;
using OpenDentBusiness;

namespace OpenDental.Imaging {
	public static class ImageStore {
		public static FileStore.UpdatePatientDelegate UpdatePatient;

		public static IImageStore GetImageStore(Patient patient) {
			// For now, always use the file store
			FileStore store = new FileStore();
			store.SetUpdatePatientDelegate(UpdatePatient);
			store.OpenPatientStore(patient);
			return store;
		}
	}
}
