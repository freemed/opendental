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
using System.Diagnostics;

namespace OpenDentBusiness {
	/// <summary>Provide a standard base class for image (and file) stores.</summary>
	public class ImageStore  {
		///<summary>Remembers the computerpref.AtoZpath.  Set to empty string on startup.  If set to something else, this path will override all other paths.</summary>
		public static string LocalAtoZpath=null;

		///<summary>Only makes a call to the database on startup.  After that, just uses cached data.  Does not validate that the path exists except if the main one is used.</summary>
		public static string GetPreferredImagePath() {
			if(!PrefC.UsingAtoZfolder) {
				return null;
			}
			if(LocalAtoZpath==null) {//on startup
				LocalAtoZpath=ComputerPrefs.LocalComputer.AtoZpath;
			}
			string replicationAtoZ=ReplicationServers.GetAtoZpath();
			if(replicationAtoZ!=""){
				return replicationAtoZ;
			}
			if(LocalAtoZpath!="") {
				return LocalAtoZpath;
			}
			//use this to handle possible multiple paths separated by semicolons.
			return GetValidPathFromString(PrefC.GetString(PrefName.DocPath));
		}

		public static string GetValidPathFromString(string documentPaths) {
			string[] preferredPathsByOrder=documentPaths.Split(new char[] { ';' });
			for(int i=0;i<preferredPathsByOrder.Length;i++) {
				string path=preferredPathsByOrder[i];
				string tryPath=ODFileUtils.CombinePaths(path,"A");
				if(Directory.Exists(tryPath)) {
					return path;
				}
			}
			return null;
		}

		///<summary>Will create folder if needed.  Will validate that folder exists.  It will alter the pat.ImageFolder if needed, but still make sure to pass in a very new Patient because we do not want an invalid patFolder.</summary>
		public static string GetPatientFolder(Patient pat) {
			string retVal="";
			if(pat.ImageFolder=="") {//creates new folder for patient if none present
				string name=pat.LName+pat.FName;
				string folder="";
				for(int i=0;i<name.Length;i++) {
					if(Char.IsLetter(name,i)) {
						folder+=name.Substring(i,1);
					}
				}
				folder+=pat.PatNum.ToString();//ensures unique name
				try {
					Patient PatOld=pat.Copy();
					pat.ImageFolder=folder;
					retVal=ODFileUtils.CombinePaths(GetPreferredImagePath(),
																		pat.ImageFolder.Substring(0,1).ToUpper(),
																		pat.ImageFolder);
					Directory.CreateDirectory(retVal);
					Patients.Update(pat,PatOld);
				}
				catch {
					throw new Exception(Lans.g("ContrDocs","Error.  Could not create folder for patient. "));
				}
			}
			else {//patient folder already created once
				retVal = ODFileUtils.CombinePaths(GetPreferredImagePath(),
																	pat.ImageFolder.Substring(0,1).ToUpper(),
																	pat.ImageFolder);
			}
			if(!Directory.Exists(retVal)) {//this makes it more resiliant and allows copies
				//of the opendentaldata folder to be used in read-only situations.
				try {
					Directory.CreateDirectory(retVal);
				}
				catch {
					throw new Exception(Lans.g("ContrDocs","Error.  Could not create folder for patient: ")+retVal);
				}
			}
			return retVal;
		}

		///<summary>When the Image module is opened, this loads newly added files.</summary>
		public static void AddMissingFilesToDatabase(Patient pat){
			string patFolder=GetPatientFolder(pat);
			DirectoryInfo di = new DirectoryInfo(patFolder);
			FileInfo[] fiList = di.GetFiles();
			List<string> fileList = new List<string>();
			for(int i = 0;i < fiList.Length;i++) {
				fileList.Add(fiList[i].FullName);
			}
			int countAdded = Documents.InsertMissing(pat,fileList);
		//should notify user
			//if(countAdded > 0) {
			//	Debug.WriteLine(countAdded.ToString() + " documents found and added to the first category.");
			//}
			//it will refresh in FillDocList
		}

		public static string GetHashString(Document doc,string patFolder) {
			//the key data is the bytes of the file, concatenated with the bytes of the note.
			byte[] textbytes;
			if(doc.Note == null) {
				textbytes = Encoding.UTF8.GetBytes("");
			}
			else {
				textbytes = Encoding.UTF8.GetBytes(doc.Note);
			}
			byte[] filebytes = GetBytes(doc,patFolder);
			int fileLength = filebytes.Length;
			byte[] buffer = new byte[textbytes.Length + filebytes.Length];
			Array.Copy(filebytes, 0, buffer, 0, fileLength);
			Array.Copy(textbytes, 0, buffer, fileLength, textbytes.Length);
			HashAlgorithm algorithm = MD5.Create();
			byte[] hash = algorithm.ComputeHash(buffer);//always results in length of 16.
			return Encoding.ASCII.GetString(hash);
		}

		public static Collection<Bitmap> OpenImages(IList<Document> documents,string patFolder) {
			//string patFolder=GetPatientFolder(pat);
			Collection<Bitmap> bitmaps = new Collection<Bitmap>();
			foreach(Document document in documents) {
				if(document == null) {
					bitmaps.Add(null);
				}
				else {
					bitmaps.Add(OpenImage(document,patFolder));
				}
			}
			return bitmaps;
		}

		public static Bitmap[] OpenImages(Document[] documents,string patFolder) {
			Bitmap[] values = new Bitmap[documents.Length];
			Collection<Bitmap> bitmaps = OpenImages(new Collection<Document>(documents),patFolder);
			bitmaps.CopyTo(values, 0);
			return values;
		}

		//public static Bitmap OpenImage(Document doc,Patient pat) {
		//	string patFolder=GetPatientFolder(pat);
		//	return OpenImage(doc,patFolder);
		//}

		public static Bitmap OpenImage(Document doc,string patFolder) {
			if(!PrefC.UsingAtoZfolder) {
				return PIn.Bitmap(doc.RawBase64);
			}
			else {
				string srcFileName = ODFileUtils.CombinePaths(patFolder,doc.FileName);
				if(HasImageExtension(srcFileName)) {
					//if(File.Exists(srcFileName) && HasImageExtension(srcFileName)) {
					if(File.Exists(srcFileName)) {
						return new Bitmap(srcFileName);
					}
					else {
						//throw new Exception();
						return null;
					}
				}
				else {
					return null;
				}
			}
		}

		///<summary>Takes in a mount object and finds all the images pertaining to the mount, then concatonates them together into one large, unscaled image and returns that image. For use in other modules.</summary>
		public static Bitmap GetMountImage(Mount mount,string patFolder) {
			//string patFolder=GetPatientFolder(pat);
			List <MountItem> mountItems = MountItems.GetItemsForMount(mount.MountNum);
			Document[] documents = Documents.GetDocumentsForMountItems(mountItems);
			Bitmap[] originalImages = OpenImages(documents,patFolder);
			Bitmap mountImage = new Bitmap(mount.Width, mount.Height);
			ImageHelper.RenderMountImage(mountImage, originalImages, mountItems, documents, -1);
			return mountImage;
		}
		
		public static byte[] GetBytes(Document doc,string patFolder) {
			/*if(ImageStoreIsDatabase) {not supported
				byte[] buffer;
				using(IDbConnection connection = DataSettings.GetConnection())
				using(IDbCommand command = connection.CreateCommand()) {
					command.CommandText =	@"SELECT Data FROM files WHERE DocNum = ?DocNum";
					IDataParameter docNumParameter = command.CreateParameter();
					docNumParameter.ParameterName = "?DocNum";
					docNumParameter.Value = doc.DocNum;
					command.Parameters.Add(docNumParameter);
					connection.Open();
					buffer = (byte[])command.ExecuteScalar();
					connection.Close();
				}
				return buffer;
			}
			else {*/
			string path = ODFileUtils.CombinePaths(patFolder,doc.FileName);
			if(!File.Exists(path)) {
				return new byte[] { };
			}
			byte[] buffer;
			using(FileStream fs = new FileStream(path,FileMode.Open,FileAccess.Read,FileShare.Read)) {
				int fileLength = (int)fs.Length;
				buffer = new byte[fileLength];
				fs.Read(buffer,0,fileLength);
			}
			return buffer;
		}

		public static Document Import(string path,long docCategory,Patient pat) {
			string patFolder=GetPatientFolder(pat);
			Document doc = new Document();
			//Document.Insert will use this extension when naming:
			doc.FileName = Path.GetExtension(path);
			doc.DateCreated = File.GetLastWriteTime(path);
			doc.PatNum = pat.PatNum;
			doc.ImgType = (HasImageExtension(path) || Path.GetExtension(path) == "") ? ImageType.Photo : ImageType.Document;
			doc.DocCategory = docCategory;
			Documents.Insert(doc,pat);//this assigns a filename and saves to db
			doc=Documents.GetByNum(doc.DocNum);
			try {
				// If the file has no extension, try to open it as a image. If it is an image,
				// save it as a JPEG file.
				if(Path.GetExtension(path) == string.Empty && IsImageFile(path)) {
					Bitmap testImage = new Bitmap(path);
					doc.FileName += ".jpg";
					Documents.Update(doc);
					SaveDocument(doc, testImage, ImageFormat.Jpeg,patFolder);
				}
				else {
					// Just copy the file.
					SaveDocument(doc, path,patFolder);
				}
			}
			catch {
				Documents.Delete(doc);
				throw;
			}
			return doc;
		}

		public static Document Import(Bitmap image,long docCategory,Patient pat) {
			string patFolder="";
			if(PrefC.UsingAtoZfolder) {
				patFolder=GetPatientFolder(pat);
			}
			Document doc=new Document();
			doc.FileName=".jpg";
			doc.DateCreated=DateTime.Today;
			doc.DocCategory=docCategory;
			doc.PatNum=pat.PatNum;
			doc.ImgType=ImageType.Photo;
			if(!PrefC.UsingAtoZfolder) {
				doc.RawBase64=POut.Bitmap(image,ImageFormat.Jpeg);
				doc.Thumbnail="";
				//no thumbnail yet
			}
			Documents.Insert(doc,pat);//this assigns a filename and saves to db
			doc=Documents.GetByNum(doc.DocNum);
			if(PrefC.UsingAtoZfolder) {
				try {
					SaveDocument(doc,image,patFolder);
				}
				catch {
					Documents.Delete(doc);
					throw;
				}
			}
			return doc;
		}

		/// <summary></summary>
		public static Document Import(Bitmap image,long docCategory,ImageType imageType,Patient pat) {
			string patFolder=GetPatientFolder(pat);
			Document doc = new Document();
			doc.ImgType = imageType;
			doc.FileName = ".jpg";
			doc.DateCreated = DateTime.Today;
			doc.PatNum = pat.PatNum;
			doc.DocCategory = docCategory;
			Documents.Insert(doc,pat);//creates filename and saves to db
			doc=Documents.GetByNum(doc.DocNum);
			long qualityL = 0;
			if(imageType == ImageType.Radiograph) {
				qualityL=100;//Convert.ToInt64(((Pref)PrefC.HList["ScannerCompressionRadiographs"]).ValueString);
			}
			else if(imageType == ImageType.Photo) {
				qualityL=100;//Convert.ToInt64(((Pref)PrefC.HList["ScannerCompressionPhotos"]).ValueString);
			}
			else {//Assume document
				//Possible values 0-100?
				qualityL=PrefC.GetLong(PrefName.ScannerCompression);
			}
			ImageCodecInfo myImageCodecInfo;
			ImageCodecInfo[] encoders;
			encoders = ImageCodecInfo.GetImageEncoders();
			myImageCodecInfo = null;
			for(int j = 0; j < encoders.Length; j++) {
				if(encoders[j].MimeType == "image/jpeg") {
					myImageCodecInfo = encoders[j];
				}
			}
			EncoderParameters myEncoderParameters = new EncoderParameters(1);
			EncoderParameter myEncoderParameter = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, qualityL);
			myEncoderParameters.Param[0] = myEncoderParameter;
			//AutoCrop()?
			try {
				SaveDocument(doc, image, myImageCodecInfo, myEncoderParameters,patFolder);
			}
			catch {
				Documents.Delete(doc);
				throw;
			}
			return doc;
		}

		public static Document ImportForm(string form,long docCategory,Patient pat) {
			string patFolder=GetPatientFolder(pat);
			string fileName = ODFileUtils.CombinePaths(GetPreferredImagePath(),"Forms",form);
			if(!File.Exists(fileName)) {
				throw new Exception(Lans.g("ContrDocs", "Could not find file: ") + fileName);
			}
			Document doc = new Document();
			doc.FileName = Path.GetExtension(fileName);
			doc.DateCreated = DateTime.Today;
			doc.DocCategory = docCategory;
			doc.PatNum = pat.PatNum;
			doc.ImgType = ImageType.Document;
			Documents.Insert(doc,pat);//this assigns a filename and saves to db
			doc=Documents.GetByNum(doc.DocNum);
			try {
				SaveDocument(doc,fileName,patFolder);
			}
			catch {
				Documents.Delete(doc);
				throw;
			}
			return doc;
		}

		public static Document ImportCapturedImage(Bitmap image,short rotationAngle,long mountItemNum,long docCategory,Patient pat) {
			string patFolder=GetPatientFolder(pat);
			string fileExtention = ".bmp";//The file extention to save the greyscale image as.
			Document doc = new Document();
			doc.MountItemNum = mountItemNum;
			doc.DegreesRotated = rotationAngle;
			doc.ImgType = ImageType.Radiograph;
			doc.FileName = fileExtention;
			doc.DateCreated = DateTime.Today;
			doc.PatNum = pat.PatNum;
			doc.DocCategory = docCategory;
			doc.WindowingMin = PrefC.GetInt(PrefName.ImageWindowingMin);
			doc.WindowingMax = PrefC.GetInt(PrefName.ImageWindowingMax);
			Documents.Insert(doc,pat);//creates filename and saves to db
			doc=Documents.GetByNum(doc.DocNum);
			try {
				SaveDocument(doc, image, ImageFormat.Bmp,patFolder);
			}
			catch {
				Documents.Delete(doc);
				throw;
			}
			return doc;
		}

		public static void ImportImage(Document document,string filename,string patFolder) {
			//string patFolder=GetPatientFolder(pat);
			// No try -- catch here, because the document already existed -- we cannot delete it.
			SaveDocument(document, filename,patFolder);
		}

		/*public static void ImportPdf(string sPDF,Patient pat) {
			string patFolder=GetPatientFolder(pat);
			Document DocCur = new Document();
			DocCur.FileName = ".pdf";
			DocCur.DateCreated = DateTime.Today;
			//Find the category, hopefully 'Patient Information'
			//otherwise, just default to first one
			Def[] defs=DefC.GetList(DefCat.ImageCats);
			long iCategory = defs[0].DefNum; ;
			for(int i = 0;i < defs.Length;i++) {
				if(defs[i].ItemName == "Patient Information") {
					iCategory = defs[i].DefNum;
				}
			}
			DocCur.DocCategory = iCategory;
			DocCur.ImgType = ImageType.Document;
			DocCur.Description = "New Patient Form";
			DocCur.PatNum = pat.PatNum;
			Documents.Insert(DocCur,pat);//this assigns a filename and saves to db
			DocCur=Documents.GetByNum(DocCur.DocNum);
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
				SaveDocument(DocCur, tempFileName,patFolder);
			}
			catch {
				Documents.Delete(DocCur);
				throw;
			}
			finally {
				if(File.Exists(tempFileName))
					File.Delete(tempFileName);
			}
		}*/

		///<summary>patFolder must be fully qualified and valid.</summary>
		public static void SaveDocument(Document doc,Bitmap image,string patFolder) {
			//if(ImageStoreIsDatabase) {
			//	SaveDocument(doc,image,image.RawFormat);
			//}
			//else {
			string srcFile = ODFileUtils.CombinePaths(patFolder,doc.FileName);
			image.Save(srcFile);
		}

		///<summary>patFolder must be fully qualified and valid.</summary>
		public static void SaveDocument(Document doc,Bitmap image,ImageFormat format,string patFolder) {
			//if(ImageStoreIsDatabase) {
			//	using(MemoryStream stream = new MemoryStream()) {
			//		image.Save(stream,format);
			//		SaveDocumentToDatabase(doc,stream);
			//	}
			//}
			//else {
			image.Save(ODFileUtils.CombinePaths(patFolder,doc.FileName),ImageFormat.Bmp);
		}

		///<summary>patFolder must be fully qualified and valid.</summary>
		public static void SaveDocument(Document doc,Bitmap image,ImageCodecInfo codec,EncoderParameters encoderParameters,string patFolder) {
			//if(ImageStoreIsDatabase) {
			//	using(MemoryStream stream = new MemoryStream()) {
			//		image.Save(stream,codec,encoderParameters);
			//		SaveDocumentToDatabase(doc,stream);
			//	}
			//}
			//else {
			image.Save(ODFileUtils.CombinePaths(patFolder,doc.FileName),codec,encoderParameters);
		}

		///<summary>patFolder must be fully qualified and valid.</summary>
		public static void SaveDocument(Document doc,string filename,string patFolder) {
			//if(ImageStoreIsDatabase) {
			//	using(FileStream stream = new FileStream(filename,FileMode.Open,FileAccess.Read)) {
			//		SaveDocumentToDatabase(doc,stream);
			//	}
			//}
			//else {
			File.Copy(filename,ODFileUtils.CombinePaths(patFolder,doc.FileName));
		}

		/*
		private static void SaveDocumentToDatabase(Document doc,Stream stream) {
			// Clone the contents to a byte array
			int length = (int)stream.Length;
			byte[] buffer = new byte[length];
			stream.Read(buffer,0,length);
			using(IDbConnection connection = DataSettings.GetConnection())
			using(IDbCommand command = connection.CreateCommand()) {
				command.CommandText =@"INSERT INTO files (DocNum, Data, Thumbnail) VALUES (?DocNum, ?Data, NULL)";
				IDataParameter docNumParameter = command.CreateParameter();
				docNumParameter.ParameterName = "?DocNum";
				docNumParameter.Value = doc.DocNum;
				command.Parameters.Add(docNumParameter);
				IDataParameter dataParameter = command.CreateParameter();
				dataParameter.ParameterName = "?Data";
				dataParameter.Value = buffer;
				command.Parameters.Add(dataParameter);
				connection.Open();
				command.ExecuteNonQuery();
				connection.Close();
			}
		}*/

		//public static void DeleteThumbnailImage(Document doc) {
		//	DeleteThumbnailImageInternal(doc);
		//}

		public static void DeleteImage(IList<Document> documents,string patFolder) {
			//string patFolder=GetPatientFolder(pat);
			for(int i = 0; i < documents.Count; i++) {
				if(documents[i] == null) {
					continue;
				}
				try {
					DeleteDocument(documents[i],patFolder);
				}
				catch {
					//if(verbose) {
					//	Debug.WriteLine(Lans.g("ContrDocs", "Could not delete file. It may be in use elsewhere, or may have already been deleted."));
					//}
				}
				Documents.Delete(documents[i]);
			}
		}

		///<summary></summary>
		public static void DeleteThumbnailImage(Document doc,string patFolder) {
			/*if(ImageStoreIsDatabase) {
				using(IDbConnection connection = DataSettings.GetConnection())
				using(IDbCommand command = connection.CreateCommand()) {
					command.CommandText =
					@"UPDATE files SET Thumbnail = NULL WHERE DocNum = ?DocNum";

					IDataParameter docNumParameter = command.CreateParameter();
					docNumParameter.ParameterName = "?DocNum";
					docNumParameter.Value = doc.DocNum;
					command.Parameters.Add(docNumParameter);

					connection.Open();
					command.ExecuteNonQuery();
					connection.Close();
				}
			}
			else {*/
			//string patFolder=GetPatientFolder(pat);
			string thumbnailFile = ODFileUtils.CombinePaths(patFolder,"Thumbnails",doc.FileName);
			if(File.Exists(thumbnailFile)) {
				try {
					File.Delete(thumbnailFile);
				}
				catch {
					//Two users *might* edit the same image at the same time, so the image might already be deleted.
				}
			}
		}

		///<summary>patFolder must be fully qualified and valid.</summary>
		public static void DeleteDocument(Document doc,string patFolder) {
			/*if(ImageStoreIsDatabase) {
				unsupported, but leaving code here for later.
				using(IDbConnection connection = DataSettings.GetConnection())
				using(IDbCommand command = connection.CreateCommand()) {
					command.CommandText =
					@"DELETE FROM files WHERE DocNum = ?DocNum";
					IDataParameter docNumParameter = command.CreateParameter();
					docNumParameter.ParameterName = "?DocNum";
					docNumParameter.Value = doc.DocNum;
					command.Parameters.Add(docNumParameter);

					connection.Open();
					command.ExecuteNonQuery();
					connection.Close();
				}
			}
			else {*/
			string srcFile = ODFileUtils.CombinePaths(patFolder,doc.FileName);
			if(File.Exists(srcFile)) {
				File.Delete(srcFile);
			}
		}

		public static string GetExtension(Document doc) {
			return Path.GetExtension(doc.FileName).ToLower();
		}

		public static string GetFilePath(Document doc,string patFolder) {
			//string patFolder=GetPatientFolder(pat);
			return ODFileUtils.CombinePaths(patFolder,doc.FileName);
		}
		
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

		
	
	}
}
