using System;
using System.Collections.Generic;
using System.Text;
using OpenDentBusiness;

namespace OpenDental.Imaging {
	public static class ImageStore {
		public static FileStore.UpdatePatientDelegate UpdatePatient;

		public static IImageStore GetImageStore(Patient patient) {
			string imageStoreTypeName = PrefB.GetString("ImageStore");
			Type imageStoreType = Type.GetType(imageStoreTypeName, false);
			if(imageStoreType == null || !imageStoreType.IsSubclassOf(typeof(IImageStore)))
				imageStoreType = typeof(FileStore);

			IImageStore store = (IImageStore)Activator.CreateInstance(imageStoreType);
			store.OpenPatientStore(patient);
			return store;
		}
	}
}
