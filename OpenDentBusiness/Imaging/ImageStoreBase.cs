using System;
using System.Collections.Generic;
using System.Text;
using OpenDentBusiness;
using System.Drawing;
using System.Drawing.Imaging;
using CodeBase;
using System.IO;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using System.Security.Cryptography;
using OpenDentBusiness.Imaging;
using System.Diagnostics;

namespace OpenDental.Imaging {
	/// <summary>
	/// Provide a standard base class for image (and file) stores.
	/// </summary>
	public abstract class ImageStoreBase : IImageStore {
		#region Properties
		private Patient patient;
		public Patient Patient {
			get { return patient; }
		}

		private bool verbose = false;
		protected bool Verbose {
			get { return verbose; }
		}
		#endregion

		#region Open/Close methods
		public void OpenPatientStore(Patient patient) {
			if(patient == null) {
				ClosePatientStore();
				return;
			}

			this.patient = patient;
			OnPatientStoreOpened(EventArgs.Empty, this);
		}

		public void ClosePatientStore() {
			this.patient = null;
			OnPatientStoreClosed(EventArgs.Empty, this);
		}
		#endregion

		#region Protected Open/Close methods
		protected virtual void OnPatientStoreOpened(EventArgs e, object sender) {
		}

		protected virtual void OnPatientStoreClosed(EventArgs e, object sender) {
		}
		#endregion

		#region Open (and Hash) methods
		public string GetHashString(Document doc) {
			//the key data is the bytes of the file, concatenated with the bytes of the note.
			byte[] textbytes;
			if(doc.Note == null) {
				textbytes = Encoding.UTF8.GetBytes("");
			}
			else {
				textbytes = Encoding.UTF8.GetBytes(doc.Note);
			}

			byte[] filebytes = GetBytes(doc);
			int fileLength = filebytes.Length;

			byte[] buffer = new byte[textbytes.Length + filebytes.Length];
			Array.Copy(filebytes, 0, buffer, 0, fileLength);
			Array.Copy(textbytes, 0, buffer, fileLength, textbytes.Length);
			HashAlgorithm algorithm = MD5.Create();
			byte[] hash = algorithm.ComputeHash(buffer);//always results in length of 16.
			return Encoding.ASCII.GetString(hash);
		}

		public Bitmap RetrieveImage(Document document) {
			if(Patient == null)
				throw new NoActivePatientException();

			if(document == null)
				throw new ArgumentNullException("document");

			try {
				return OpenImage(document);
			}
			catch {
				MessageBox.Show(Lan.g("ContrDocs", "File not found") + document.FileName);
				return null;
			}
		}

		public Collection<Bitmap> RetrieveImage(IList<Document> documents) {
			if(Patient == null)
				throw new NoActivePatientException();

			if(documents == null)
				throw new ArgumentNullException("documents");

			Collection<Bitmap> bitmaps = new Collection<Bitmap>();

			foreach(Document document in documents) {
				if(document == null)
					bitmaps.Add(null);
				else {
					bitmaps.Add(RetrieveImage(document));
				}
			}

			return bitmaps;
		}

		public Bitmap[] RetrieveImage(Document[] documents) {
			if(documents == null)
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
		#endregion

		#region Abstract Open methods
		protected abstract Bitmap OpenImage(Document doc);
		protected abstract byte[] GetBytes(Document doc);
		#endregion

		#region Import methods
		public Document Import(string path, int docCategory) {
			Document doc = new Document();
			//Document.Insert will use this extension when naming:
			doc.FileName = Path.GetExtension(path);
			doc.DateCreated = DateTime.Today;
			doc.PatNum = Patient.PatNum;
			doc.ImgType = (HasImageExtension(path) || Path.GetExtension(path) == "") ? ImageType.Photo : ImageType.Document;
			doc.DocCategory = docCategory;
			Documents.Insert(doc, Patient);//this assigns a filename and saves to db
			try {
				// If the file has no extension, try to open it as a image. If it is an image,
				// save it as a JPEG file.
				if(Path.GetExtension(path) == string.Empty && IsImageFile(path)) {
					Bitmap testImage = new Bitmap(path);
					doc.FileName += ".jpg";
					Documents.Update(doc);
					SaveDocument(doc, testImage, ImageFormat.Jpeg);
				}
				else {
					// Just copy the file.
					SaveDocument(doc, path);
				}
			}
			catch {
				Documents.Delete(doc);
				throw;
			}
			return doc;
		}

		public Document Import(Bitmap image, int docCategory) {
			Document doc = new Document();
			doc.FileName = ".jpg";
			doc.DateCreated = DateTime.Today;
			doc.DocCategory = docCategory;
			doc.PatNum = Patient.PatNum;
			doc.ImgType = ImageType.Photo;
			Documents.Insert(doc, Patient);//this assigns a filename and saves to db
			try {
				SaveDocument(doc, image);
			}
			catch {
				Documents.Delete(doc);
				throw;
			}
			return doc;
		}

		public Document Import(Bitmap image, int docCategory, ImageType imageType) {
			Document doc = new Document();
			doc.ImgType = imageType;
			doc.FileName = ".jpg";
			doc.DateCreated = DateTime.Today;
			doc.PatNum = Patient.PatNum;
			doc.DocCategory = docCategory;
			Documents.Insert(doc, Patient);//creates filename and saves to db

			long qualityL = 0;
			if(imageType == ImageType.Radiograph) {
				qualityL = Convert.ToInt64(((Pref)PrefB.HList["ScannerCompressionRadiographs"]).ValueString);
			}
			else if(imageType == ImageType.Photo) {
				qualityL = Convert.ToInt64(((Pref)PrefB.HList["ScannerCompressionPhotos"]).ValueString);
			}
			else {//Assume document
				//Possible values 0-100?
				qualityL = (long)Convert.ToInt32(((Pref)PrefB.HList["ScannerCompression"]).ValueString);
			}

			ImageCodecInfo myImageCodecInfo;
			ImageCodecInfo[] encoders;
			encoders = ImageCodecInfo.GetImageEncoders();
			myImageCodecInfo = null;
			for(int j = 0; j < encoders.Length; j++) {
				if(encoders[j].MimeType == "image/jpeg")
					myImageCodecInfo = encoders[j];
			}
			System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;
			EncoderParameters myEncoderParameters = new EncoderParameters(1);
			EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, qualityL);
			myEncoderParameters.Param[0] = myEncoderParameter;
			//AutoCrop()?
			try {
				SaveDocument(doc, image, myImageCodecInfo, myEncoderParameters);
			}
			catch {
				Documents.Delete(doc);
				throw;
			}
			return doc;
		}

		public Document ImportForm(string form, int docCategory) {
			string fileName = ODFileUtils.CombinePaths(new string[] {FileStoreSettings.GetPreferredImagePath,
				"Forms",form});
			if(!File.Exists(fileName)) {
				throw new Exception(Lan.g("ContrDocs", "Could not find file: ") + fileName);
			}
			Document doc = new Document();
			doc.FileName = Path.GetExtension(fileName);
			doc.DateCreated = DateTime.Today;
			doc.DocCategory = docCategory;
			doc.PatNum = Patient.PatNum;
			doc.ImgType = ImageType.Document;
			Documents.Insert(doc, Patient);//this assigns a filename and saves to db
			try {
				SaveDocument(doc, fileName);
			}
			catch {
				Documents.Delete(doc);
				throw;
			}
			return doc;
		}

		public Document ImportCapturedImage(Bitmap image, short rotationAngle, int mountItemNum, int docCategory) {
			string fileExtention = ".bmp";//The file extention to save the greyscale image as.
			Document doc = new Document();
			doc.MountItemNum = mountItemNum;
			doc.DegreesRotated = rotationAngle;
			doc.ImgType = ImageType.Radiograph;
			doc.FileName = fileExtention;
			doc.DateCreated = DateTime.Today;
			doc.PatNum = Patient.PatNum;
			doc.DocCategory = docCategory;
			doc.WindowingMin = PrefB.GetInt("ImageWindowingMin");
			doc.WindowingMax = PrefB.GetInt("ImageWindowingMax");
			Documents.Insert(doc, Patient);//creates filename and saves to db
			try {
				SaveDocument(doc, image, ImageFormat.Bmp);
			}
			catch {
				Documents.Delete(doc);
				throw;
			}
			return doc;
		}

		public void ImportImage(Document document, string filename) {
			if(Patient == null)
				throw new NoActivePatientException();

			// No try -- catch here, because the document already existed -- we cannot delete it.
			SaveDocument(document, filename);
		}


		public void ImportPdf(string sPDF) {
			Document DocCur = new Document();
			DocCur.FileName = ".pdf";
			DocCur.DateCreated = DateTime.Today;

			//Find the category, hopefully 'Patient Information'
			//otherwise, just default to first one
			int iCategory = iCategory = DefB.Short[(int)DefCat.ImageCats][0].DefNum; ;
			for(int i = 0; i < DefB.Short[(int)DefCat.ImageCats].Length; i++) {
				if(DefB.Short[(int)DefCat.ImageCats][i].ItemName == "Patient Information") {
					iCategory = DefB.Short[(int)DefCat.ImageCats][i].DefNum;
				}
			}

			DocCur.DocCategory = iCategory;
			DocCur.ImgType = ImageType.Document;
			DocCur.Description = "New Patient Form";
			DocCur.PatNum = Patient.PatNum;
			Documents.Insert(DocCur, Patient);//this assigns a filename and saves to db

			// Save the PDF to a temporary file, then import that file.
			string tempFileName = Path.GetTempFileName();
			try {
				// Convert the Base64 UUEncoded input into binary output.
				byte[] binaryData = Convert.FromBase64String(sPDF);

				// Write out the decoded data.
				FileStream outFile = new FileStream(tempFileName, FileMode.Create, FileAccess.Write);
				outFile.Write(binaryData, 0, binaryData.Length);
				outFile.Close();
				//Above is the code to save the file to a particular directory from NewPatientForm.com

				// Import this file
				SaveDocument(DocCur, tempFileName);
			}
			catch {
				Documents.Delete(DocCur);
				throw;
			}
			finally {
				if(File.Exists(tempFileName))
					File.Delete(tempFileName);
			}
		}
		#endregion

		#region Abstract import methods
		protected abstract void SaveDocument(Document doc, Bitmap image);
		protected abstract void SaveDocument(Document doc, Bitmap image, ImageFormat format);
		protected abstract void SaveDocument(Document doc, Bitmap image, ImageCodecInfo codec, EncoderParameters encoderParameters);
		protected abstract void SaveDocument(Document doc, string filename);
		#endregion

		#region Delete methods
		public void DeleteThumbnailImage(Document doc) {
			DeleteThumbnailImageInternal(doc);
		}

		public void DeleteImage(IList<Document> documents) {
			for(int i = 0; i < documents.Count; i++) {
				if(documents[i] == null) {
					continue;
				}
				try {
					DeleteDocument(documents[i]);
				}
				catch {
					if(verbose) {
						Debug.WriteLine(Lan.g("ContrDocs", "Could not delete file. It may be in use elsewhere, or may have already been deleted."));
					}
				}
				Documents.Delete(documents[i]);
			}
		}
		#endregion

		#region Abstract delete methods
		protected abstract void DeleteThumbnailImageInternal(Document doc);
		protected abstract void DeleteDocument(Document doc);
		#endregion

		#region Misc methods
		public string GetExtension(Document doc) {
			return Path.GetExtension(doc.FileName).ToLower();
		}

		public virtual bool OpenFolderSupported {
			get { return false; }
		}

		public virtual string FolderPath {
			get { throw new NotSupportedException(); }
		}

		public virtual bool FolderPathSupported {
			get { return false; }
		}

		public virtual bool FilePathSupported {
			get { return false; }
		}

		public virtual string GetFilePath(Document doc) {
			throw new NotSupportedException();
		}
		#endregion
		#region Static methods
		public static bool IsImageFile(string filename) {
			try {
				Bitmap bitmap = new Bitmap(filename);
				bitmap.Dispose();
				return true;
			}
			catch {
				return false;
			}
		}

		///<summary>Returns true if the given filename contains a supported file image extension.</summary>
		public static bool HasImageExtension(string fileName) {
			string ext = Path.GetExtension(fileName).ToLower();
			//The following supported bitmap types were found on a microsoft msdn page:
			return (ext == ".jpg" || ext == ".jpeg" || ext == ".tga" || ext == ".bmp" || ext == ".tif" ||
				ext == ".tiff" || ext == ".gif" || ext == ".emf" || ext == ".exif" || ext == ".ico" || ext == ".png" || ext == ".wmf");
		}
		#endregion
	}
}
