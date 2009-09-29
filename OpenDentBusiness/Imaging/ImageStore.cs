using System;
using System.Collections.Generic;
using System.Text;
using OpenDentBusiness;

namespace OpenDental.Imaging {
	public static class ImageStore {
		public static FileStore.UpdatePatientDelegate UpdatePatient;

		

		public static ImageStoreBase GetImageStore(Patient patient) {
			Type imageStoreType = Type.GetType(ImageStoreTypeName, false);
			if(imageStoreType == null || !typeof(ImageStoreBase).IsAssignableFrom(imageStoreType)) {
				imageStoreType = typeof(FileStore);
			}
			ImageStoreBase store = (ImageStoreBase)Activator.CreateInstance(imageStoreType);
			store.OpenPatientStore(patient);
			return store;
		}
	}
}
