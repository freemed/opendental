using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Collections.ObjectModel;
using OpenDentBusiness;

namespace OpenDental.Imaging {
	/// <summary>
	/// Provides a common interface for all methods of storing images in Open Dental.
	/// </summary>
	public interface IImageStore {
		Patient Patient { get; }
		bool Exists { get; }
		object StoreIdentifier { get; }

		void OpenPatientStore(Patient patient);
		void ClosePatientStore();
		void CreateStore();
		void DeleteStore();
		Bitmap RetrieveImage(Document document);
		Collection<Bitmap> RetrieveImage(IList<Document> documents);
		void ImportImage(Document document, string filename);
	}
}
