using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using CodeBase;
using System.Drawing;
using System.Collections.ObjectModel;
using System.Diagnostics;
using OpenDentBusiness;
using OpenDentBusiness.Imaging;
using System.Security.Cryptography;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace OpenDental.Imaging {
	public class FileStore : IImageStore {
		public delegate int UpdatePatientDelegate(Patient patCur, Patient patOld);
		private UpdatePatientDelegate updatePatient;
		private bool verbose = false;

		private Patient patient;
		public Patient Patient {
			get { return patient; }
		}

		private string storeIdentifier;
		public object StoreIdentifier {
			get { return storeIdentifier; }
		}

		private string patFolder;
		public string PatFolder {
			get { return patFolder; }
		}

		public bool Exists {
			get {
				return File.Exists(PatFolder);
			}
		}

		public void SetUpdatePatientDelegate(UpdatePatientDelegate updatePatient) {
			this.updatePatient = updatePatient;
		}

		public void OpenPatientStore(Patient patient) {
			if (patient == null) {
				this.patient = null;
				this.storeIdentifier = null;
				this.patFolder = null;
				return;
			}

			this.patient = patient;

			if(patient.ImageFolder == "") {//creates new folder for patient if none present
				string name = patient.LName + patient.FName;
				string folder = "";
				for(int i = 0; i < name.Length; i++) {
					if(Char.IsLetter(name, i)) {
						folder += name.Substring(i, 1);
					}
				}
				folder += patient.PatNum.ToString();//ensures unique name
				try {
					Patient PatOld = patient.Copy();
					patient.ImageFolder = folder;
					patFolder = ODFileUtils.CombinePaths(new string[] {	FileStoreSettings.GetPreferredImagePath,
																		patient.ImageFolder.Substring(0,1).ToUpper(),
																		patient.ImageFolder});
					Directory.CreateDirectory(patFolder);
					updatePatient(patient, PatOld);
				}
				catch {
					throw new Exception(Lan.g("ContrDocs", "Error.  Could not create folder for patient. "));
					return;
				}
			}
			else {//patient folder already created once
				patFolder = ODFileUtils.CombinePaths(new string[] {	FileStoreSettings.GetPreferredImagePath,
																	patient.ImageFolder.Substring(0,1).ToUpper(),
																	patient.ImageFolder});
			}
			if(!Directory.Exists(patFolder)) {//this makes it more resiliant and allows copies
				//of the opendentaldata folder to be used in read-only situations.
				try {
					Directory.CreateDirectory(patFolder);
				}
				catch {
					throw new Exception(Lan.g("ContrDocs", "Error.  Could not create folder for patient. "));
				}
			}
			//now find all files in the patient folder that are not in the db and add them
			DirectoryInfo di = new DirectoryInfo(patFolder);
			FileInfo[] fiList = di.GetFiles();
			string[] fileList = new string[fiList.Length];
			for(int i = 0; i < fileList.Length; i++) {
				fileList[i] = fiList[i].Name;
			}
			int countAdded = Documents.InsertMissing(patient, fileList);
			if(countAdded > 0) {
				Debug.WriteLine(countAdded.ToString() + " documents found and added to the first category.");
			}
			//it will refresh in FillDocList
		}

		public void ClosePatientStore() {
			OpenPatientStore(null);
		}

		public Bitmap RetrieveImage(Document document) {
			if (Patient == null)
				throw new NoActivePatientException();

			if (document == null)
				throw new ArgumentNullException("document");

			string srcFileName = ODFileUtils.CombinePaths(PatFolder, document.FileName);
			if(File.Exists(srcFileName)){
				if(HasImageExtension(srcFileName)){
					return new Bitmap(srcFileName);
				}
			}else{
				MessageBox.Show(Lan.g("ContrDocs","File not found")+document.FileName);
			}
			return null;
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

		public string GetHashString(Document doc) {
			//the key data is the bytes of the file, concatenated with the bytes of the note.
			byte[] textbytes;
			if(doc.Note == null) {
				textbytes = Encoding.UTF8.GetBytes("");
			}
			else {
				textbytes = Encoding.UTF8.GetBytes(doc.Note);
			}
			string path = ODFileUtils.CombinePaths(patFolder, doc.FileName);
			if(!File.Exists(path)) {
				return "";
			}
			FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
			int fileLength = (int)fs.Length;
			byte[] buffer = new byte[fileLength + textbytes.Length];
			fs.Read(buffer, 0, fileLength);
			fs.Close();
			Array.Copy(textbytes, 0, buffer, fileLength, textbytes.Length);
			HashAlgorithm algorithm = MD5.Create();
			byte[] hash = algorithm.ComputeHash(buffer);//always results in length of 16.
			return Encoding.ASCII.GetString(hash);
		}

		public Document Import(string path, int docCategory) {
			Document doc = new Document();
			//Document.Insert will use this extension when naming:
			doc.FileName = Path.GetExtension(path);
			doc.DateCreated = DateTime.Today;
			doc.PatNum = Patient.PatNum;
			doc.ImgType = (HasImageExtension(path)||Path.GetExtension(path)=="") ? ImageType.Photo : ImageType.Document;
			doc.DocCategory = docCategory;
			Documents.Insert(doc, Patient);//this assigns a filename and saves to db
			try {
				string destinationFile=ODFileUtils.CombinePaths(patFolder,doc.FileName);
				if(Path.GetExtension(path)=="") {
					try {//Find out if the file is actually an image.
						Bitmap testImage=new Bitmap(path);
						testImage.Save(destinationFile+".jpg",ImageFormat.Jpeg);
						testImage.Dispose();
						doc.FileName+=".jpg";
						Documents.Update(doc);
					} catch {
						File.Copy(path,destinationFile);
					}
				} else {
					File.Copy(path,destinationFile);
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
			string srcFile = ODFileUtils.CombinePaths(patFolder, doc.FileName);
			try {
				image.Save(srcFile);
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
			doc.DocCategory=docCategory;
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
			encoders=ImageCodecInfo.GetImageEncoders();
			myImageCodecInfo=null;
			for(int j=0;j<encoders.Length;j++) {
				if(encoders[j].MimeType=="image/jpeg")
					myImageCodecInfo=encoders[j];
			}
			System.Drawing.Imaging.Encoder myEncoder=System.Drawing.Imaging.Encoder.Quality;
			EncoderParameters myEncoderParameters=new EncoderParameters(1);
			EncoderParameter myEncoderParameter=new EncoderParameter(myEncoder,qualityL);
			myEncoderParameters.Param[0]=myEncoderParameter;
			//AutoCrop()?
			try {
				image.Save(ODFileUtils.CombinePaths(patFolder, doc.FileName), myImageCodecInfo, myEncoderParameters);
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
				File.Copy(fileName,ODFileUtils.CombinePaths(patFolder,doc.FileName));
			}catch{
				Documents.Delete(doc);
				throw new Exception(Lan.g("ContrDocs","Unable to copy file. May be in use: ")+fileName);
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
				image.Save(ODFileUtils.CombinePaths(patFolder, doc.FileName), ImageFormat.Bmp);
			}
			catch(Exception ex) {
				Documents.Delete(doc);
				//Raise an exception in the capture thread.
				throw new Exception(Lan.g("ContrDocs", "Unable to save captured XRay image as document") + ": " + ex.Message);
			}
			return doc;
		}

		public void DeleteImage(IList<Document> documents) {
			for(int i = 0; i < documents.Count; i++) {
				if(documents[i] == null) {
					continue;
				}
				try {
					string srcFile = ODFileUtils.CombinePaths(patFolder, documents[i].FileName);
					if(File.Exists(srcFile)) {
						File.Delete(srcFile);
					}
					else if(verbose) {
						Debug.WriteLine(Lan.g("ContrDocs", "File could not be found. It may have already been deleted."));
					}
				}
				catch {
					if(verbose) {
						Debug.WriteLine(Lan.g("ContrDocs", "Could not delete file. It may be in use elsewhere, or may have already been deleted."));
					}
				}
				Documents.Delete(documents[i]);
			}
		}

		public void DeleteThumbnailImage(Document doc) {
			string thumbnailFile = ODFileUtils.CombinePaths(new string[] { patFolder, "Thumbnails", doc.FileName });
			if(File.Exists(thumbnailFile)) {
				try {
					File.Delete(thumbnailFile);
				}
				catch {
					//Two users *might* edit the same image at the same time, so the image might already be deleted.
				}
			}
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

			File.Copy(filename, Path.Combine(PatFolder, document.FileName));
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

			try {
				// Convert the Base64 UUEncoded input into binary output.
				byte[] binaryData = Convert.FromBase64String(sPDF);

				// Write out the decoded data.
				FileStream outFile = new FileStream(patFolder + DocCur.FileName, FileMode.Create, FileAccess.Write);
				outFile.Write(binaryData, 0, binaryData.Length);
				outFile.Close();
				//Above is the code to save the file to a particular directory from NewPatientForm.com
			}
			catch {
				Documents.Delete(DocCur);
				throw;
			}
		}

		public bool OpenFolderSupported {
			get { return true; }
		}

		public string FolderPath {
			get { return patFolder; }
		}

		public string GetExtension(Document doc) {
			string srcFileName = ODFileUtils.CombinePaths(patFolder, doc.FileName);
			return Path.GetExtension(srcFileName).ToLower();
		}

		public bool FilePathSupported {
			get { return true; }
		}

		public string GetFilePath(Document doc) {
			return ODFileUtils.CombinePaths(patFolder, doc.FileName);
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
