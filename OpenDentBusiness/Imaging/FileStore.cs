using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using CodeBase;
using System.Drawing;
using System.Collections.ObjectModel;
using System.Diagnostics;
using OpenDentBusiness;
using OpenDental.Imaging.Business;
using OpenDentBusiness.Imaging;

namespace OpenDental.Imaging {
	public class FileStore : IImageStore {
		private Patient patient;
		public Patient Patient {
			get { return patient; }
		}

		private string storeIdentifier;
		public object StoreIdentifier {
			get { return storeIdentifier; }
		}

		private string storeFolderName;
		public string StoreFolderName {
			get { return storeFolderName; }
		}

		public bool Exists {
			get {
				return File.Exists(StoreFolderName);
			}
		}

		public void OpenPatientStore(Patient patient) {
			if (patient == null) {
				this.patient = null;
				this.storeIdentifier = null;
				this.storeFolderName = null;
				return;
			}

			this.patient = patient;

			// Get the identifier for the patient store
			// (this is the name of the sub-folder)
			string name = Patient.LName + Patient.FName;

			string folder = "";
			for (int i = 0; i < name.Length; i++) {
				if (Char.IsLetter(name, i)) {
					folder += name.Substring(i, 1);
				}
			}

			folder += Patient.PatNum.ToString();//ensures unique name
			storeIdentifier = folder;

			// Set the name of the store folder
			storeFolderName = ODFileUtils.CombinePaths(
				new string[] {	
					FileStoreSettings.GetPreferredImagePath,
					storeIdentifier.Substring(0,1).ToUpper(),
					storeIdentifier});

			//now find all files in the patient folder that are not in the db and add them
			DirectoryInfo di = new DirectoryInfo(StoreFolderName);
			FileInfo[] fiList = di.GetFiles();
			string[] fileList = new string[fiList.Length];
			for (int i = 0; i < fileList.Length; i++) {
				fileList[i] = fiList[i].Name;
			}

			int countAdded = Documents.InsertMissing(Patient, fileList);
			if (countAdded > 0) {
				Debug.WriteLine(countAdded.ToString() + " documents found and added to the first category.");
			}
		}

		public void ClosePatientStore() {
			OpenPatientStore(null);
		}

		public void CreateStore() {
			if (Exists)
				throw new InvalidOperationException();

			try {
				Directory.CreateDirectory(StoreFolderName);
			}
			catch (IOException e) {
				throw new ImageStoreCreationException(Lan.g("ContrDocs", "Error.  Could not create folder for patient. "), e);
			}
		}

		public void DeleteStore() {
			if (!Exists)
				throw new InvalidOperationException();

			try {
				Directory.CreateDirectory(StoreFolderName);
			}
			catch (IOException e) {
				throw new ImageStoreRemovalException(Lan.g("ContrDocs", "Error.  Could not delete folder for patient. "), e);
			}
		}

		public Bitmap RetrieveImage(Document document) {
			if (Patient == null)
				throw new NoActivePatientException();

			if (document == null)
				throw new ArgumentNullException("document");

			string srcFileName = ODFileUtils.CombinePaths(StoreFolderName, document.FileName);
			if (File.Exists(srcFileName) && HasImageExtension(srcFileName)) {
				return new Bitmap(srcFileName);
			}
			else {
				throw new ImageNotFoundException(Lan.g("ContrDocs", "File not found: ") + srcFileName);
			}
		}

		public Collection<Bitmap> RetrieveImage(IList<Document> documents) {
			if (Patient == null)
				throw new NoActivePatientException();

			if (documents == null)
				throw new ArgumentNullException("documents");

			Collection<Bitmap> bitmaps = new Collection<Bitmap>();

			foreach (Document document in documents) {
				if (document == null)
					bitmaps.Add(null);
				else {
					bitmaps.Add(RetrieveImage(document));
				}
			}

			return bitmaps;
		}

		public Bitmap[] RetrieveImage(Document[] documents) {
			if (documents == null)
				throw new ArgumentNullException("documents");

			Bitmap[] values = new Bitmap[documents.Length];
			Collection<Bitmap> bitmaps = RetrieveImage(new Collection<Document>(documents));
			bitmaps.CopyTo(values, 0);
			return values;
		}

		///<summary>Takes in a mount object and finds all the images pertaining to the mount, then concatonates them together into one large, unscaled image and returns that image. For use in other modules.</summary>
		public Bitmap GetMountImage(Mount mount) {
			MountItem[] mountItems = MountItems.GetItemsForMount(mount.MountNum);
			Document[] documents = Documents.GetDocumentsForMountItems(mountItems);
			Bitmap[] originalImages = RetrieveImage(documents);
			Bitmap mountImage = new Bitmap(mount.Width, mount.Height);
			ImageHelper.RenderMountImage(mountImage, originalImages, mountItems, documents, -1);
			return mountImage;
		}

		public void ImportImage(Document document, string filename) {
			if (Patient == null)
				throw new NoActivePatientException();

			File.Copy(filename, Path.Combine(StoreFolderName, document.FileName));
		}

		///<summary>Returns true if the given filename contains a supported file image extension.</summary>
		public static bool HasImageExtension(string fileName) {
			string ext = Path.GetExtension(fileName).ToLower();
			//The following supported bitmap types were found on a microsoft msdn page:
			return (ext == ".jpg" || ext == ".jpeg" || ext == ".tga" || ext == ".bmp" || ext == ".tif" ||
				ext == ".tiff" || ext == ".gif" || ext == ".emf" || ext == ".exif" || ext == ".ico" || ext == ".png" || ext == ".wmf");
		}
	}
}
