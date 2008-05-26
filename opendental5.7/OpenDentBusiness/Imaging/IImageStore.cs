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
		Document Import(Bitmap image, int docCategory, ImageType imageType);
		Document Import(string path, int docCategory);
		Document Import(Bitmap image, int docCategory);
		Document ImportCapturedImage(Bitmap image, short rotationAngle, int mountItemNum, int docCategory);
		Document ImportForm(string form, int docCategory);
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
