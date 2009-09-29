/*
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Collections.ObjectModel;
using OpenDentBusiness;

namespace OpenDental.Imaging {
	/// <summary>This interface was to be deleted and rolled into ImageStoreBase.  It was stupid to have an interface that was only implemented by one class.</summary>
	public interface IImageStore {
		Patient Patient { get; }

		void OpenPatientStore(Patient patient);
		void ClosePatientStore();

		Bitmap RetrieveImage(Document document);
		Bitmap[] RetrieveImage(Document[] documents);
		Collection<Bitmap> RetrieveImage(IList<Document> documents);
		///<summary>Deletes the image itself as well as the document object.</summary>
		void DeleteImage(IList<Document> documents);
		void DeleteThumbnailImage(Document doc);

		void ImportImage(Document document, string filename);
		/// <summary>This will also save the new document.</summary>
		Document Import(Bitmap image,long docCategory,ImageType imageType);
		Document Import(string path,long docCategory);
		Document Import(Bitmap image,long docCategory);
		Document ImportCapturedImage(Bitmap image,short rotationAngle,long mountItemNum,long docCategory);
		Document ImportForm(string form,long docCategory);
		void ImportPdf(string sPDF);

		string GetHashString(Document doc);
		Bitmap GetMountImage(Mount mount);
		string GetExtension(Document doc);
		bool FilePathSupported { get; }
		bool OpenFolderSupported { get; }
		string FolderPath { get; }
		/// <summary>Gets the full file path of the document</summary>
		string GetFilePath(Document doc);
	}
}
*/