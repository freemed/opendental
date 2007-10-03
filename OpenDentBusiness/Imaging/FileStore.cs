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
	public class FileStore : ImageStoreBase {
		public delegate int UpdatePatientDelegate(Patient patCur, Patient patOld);

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

		public override bool OpenFolderSupported {
			get { return true; }
		}

		public override string FolderPath {
			get { return patFolder; }
		}

		public override bool FilePathSupported {
			get { return true; }
		}

		public override string GetFilePath(Document doc) {
			return ODFileUtils.CombinePaths(patFolder, doc.FileName);
		}

		#region Implementation of ImageStoreBase Open/Close functions
		protected override void OnPatientStoreOpened(EventArgs e, object sender) {
			if(Patient.ImageFolder == "") {//creates new folder for patient if none present
				string name = Patient.LName + Patient.FName;
				string folder = "";
				for(int i = 0; i < name.Length; i++) {
					if(Char.IsLetter(name, i)) {
						folder += name.Substring(i, 1);
					}
				}
				folder += Patient.PatNum.ToString();//ensures unique name
				try {
					Patient PatOld = Patient.Copy();
					Patient.ImageFolder = folder;
					patFolder = ODFileUtils.CombinePaths(new string[] {	FileStoreSettings.GetPreferredImagePath,
																		Patient.ImageFolder.Substring(0,1).ToUpper(),
																		Patient.ImageFolder});
					Directory.CreateDirectory(patFolder);
					ImageStore.UpdatePatient(Patient, PatOld);
				}
				catch {
					throw new Exception(Lan.g("ContrDocs", "Error.  Could not create folder for patient. "));
					return;
				}
			}
			else {//patient folder already created once
				patFolder = ODFileUtils.CombinePaths(new string[] {	FileStoreSettings.GetPreferredImagePath,
																	Patient.ImageFolder.Substring(0,1).ToUpper(),
																	Patient.ImageFolder});
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
			int countAdded = Documents.InsertMissing(Patient, fileList);
			if(countAdded > 0) {
				Debug.WriteLine(countAdded.ToString() + " documents found and added to the first category.");
			}
			//it will refresh in FillDocList
		}

		protected override void OnPatientStoreClosed(EventArgs e, object sender) {
			this.storeIdentifier = null;
			this.patFolder = null;
		}
		#endregion

		#region Implementation of ImageStoreBase Open functions
		protected override byte[] GetBytes(Document doc) {
			string path = ODFileUtils.CombinePaths(patFolder, doc.FileName);
			if(!File.Exists(path)) {
				return new byte[] { };
			}

			byte[] buffer;
			using(FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read)) {
				int fileLength = (int)fs.Length;
				buffer = new byte[fileLength];
				fs.Read(buffer, 0, fileLength);
			}
			return buffer;
		}

		protected override Bitmap OpenImage(Document document) {
			string srcFileName = ODFileUtils.CombinePaths(PatFolder, document.FileName);
			if(File.Exists(srcFileName) && HasImageExtension(srcFileName)) {
				return new Bitmap(srcFileName);
			}
			else {
				throw new Exception();
			}
		}
		#endregion

		#region Implementation of ImageStoreBase Save functions
		protected override void SaveDocument(Document doc, Bitmap image) {
			string srcFile = ODFileUtils.CombinePaths(patFolder, doc.FileName);
			image.Save(srcFile);
		}

		protected override void SaveDocument(Document doc, Bitmap image, ImageCodecInfo codec, EncoderParameters encoderParameters) {
			image.Save(ODFileUtils.CombinePaths(patFolder, doc.FileName), codec, encoderParameters);
		}

		protected override void SaveDocument(Document doc, string filename) {
			File.Copy(filename, ODFileUtils.CombinePaths(patFolder, doc.FileName));
		}

		protected override void SaveDocument(Document doc, Bitmap image, ImageFormat format) {
			image.Save(ODFileUtils.CombinePaths(patFolder, doc.FileName), ImageFormat.Bmp);
		}
		#endregion

		#region Implementation of the Delete functions
		protected override void DeleteThumbnailImageInternal(Document doc) {
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

		protected override void DeleteDocument(Document doc) {
			string srcFile = ODFileUtils.CombinePaths(patFolder, doc.FileName);
			if(File.Exists(srcFile)) {
				File.Delete(srcFile);
			}
			else if(Verbose) {
				Debug.WriteLine(Lan.g("ContrDocs", "File could not be found. It may have already been deleted."));
			}
		}
		#endregion
	}
}
