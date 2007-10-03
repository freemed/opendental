using System;
using System.Collections.Generic;
using System.Text;
using OpenDentBusiness;

namespace OpenDental.Imaging {
	public static class ImageStore {
		public static FileStore.UpdatePatientDelegate UpdatePatient;

		public static string ImageStoreTypeName {
			get { return PrefB.GetString("ImageStore"); }
		}

		public static IImageStore GetImageStore(Patient patient) {
			Type imageStoreType = Type.GetType(ImageStoreTypeName, false);
			if(imageStoreType == null || !typeof(IImageStore).IsAssignableFrom(imageStoreType))
				imageStoreType = typeof(FileStore);

			IImageStore store = (IImageStore)Activator.CreateInstance(imageStoreType);
			store.OpenPatientStore(patient);
			return store;
		}
	}
}
