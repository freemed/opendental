using System;
using System.Collections.Generic;
using System.Text;
using OpenDentBusiness;

namespace OpenDental.Imaging {
	public static class ImageStore {
		public static IImageStore GetImageStore(Patient patient) {
			// For now, always use the file store
			IImageStore store = new FileStore();
			store.OpenPatientStore(patient);
			return store;
		}
	}
}
