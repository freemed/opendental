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

		///<summary>Only makes a call to the database on startup.  After that, just uses cached data.  Does not validate that the path exists except if the main one is used.  ONLY used from Client layer; no S classes.</summary>
		public static string GetPreferredAtoZpath() {
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
		public static string GetPatientFolder(Patient pat,string AtoZpath) {
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
					retVal=ODFileUtils.CombinePaths(AtoZpath,
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
				retVal = ODFileUtils.CombinePaths(AtoZpath,
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

		///<summary>Will create folder if needed.  Will validate that folder exists.</summary>
		public static string GetEobFolder() {
			string retVal=ODFileUtils.CombinePaths(GetPreferredAtoZpath(),"EOBs");
			if(!Directory.Exists(retVal)) {
				Directory.CreateDirectory(retVal);
			}
			return retVal;
		}

		///<summary>When the Image module is opened, this loads newly added files.</summary>
		public static void AddMissingFilesToDatabase(Patient pat){
			string patFolder=GetPatientFolder(pat,GetPreferredAtoZpath());
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

		public static Bitmap OpenImage(Document doc,string patFolder) {
			if(PrefC.UsingAtoZfolder) {
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
			else {
				if(HasImageExtension(doc.FileName)) {
					return PIn.Bitmap(doc.RawBase64);
				}
				else {
					return null;
				}
			}
		}

		public static Bitmap[] OpenImagesEob(EobAttach eob) {
			Bitmap[] values = new Bitmap[1];
			if(PrefC.UsingAtoZfolder) {
				string eobFolder=GetEobFolder();
				string srcFileName = ODFileUtils.CombinePaths(eobFolder,eob.FileName);
				if(HasImageExtension(srcFileName)) {
					if(File.Exists(srcFileName)) {
						values[0]=new Bitmap(srcFileName);
					}
					else {
						//throw new Exception();
						values[0]= null;
					}
				}
				else {
					values[0]= null;
				}
			}
			else {
				if(HasImageExtension(eob.FileName)) {
					values[0]= PIn.Bitmap(eob.RawBase64);
				}
				else {
					values[0]= null;
				}
			}
			return values;
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

		/// <summary></summary>
		public static Document Import(string pathImportFrom,long docCategory,Patient pat) {
			string patFolder="";
			if(PrefC.UsingAtoZfolder) {
				patFolder=GetPatientFolder(pat,GetPreferredAtoZpath());
			}
			Document doc = new Document();
			//Document.Insert will use this extension when naming:
			if(Path.GetExtension(pathImportFrom)=="") {//If the file has no extension
				doc.FileName=".jpg";
			}
			else{
				doc.FileName = Path.GetExtension(pathImportFrom);
			}
			doc.DateCreated = File.GetLastWriteTime(pathImportFrom);
			doc.PatNum = pat.PatNum;
			if(HasImageExtension(doc.FileName)) {
				doc.ImgType=ImageType.Photo;
			}
			else {
				doc.ImgType=ImageType.Document;
			}
			doc.DocCategory = docCategory;
			Documents.Insert(doc,pat);//this assigns a filename and saves to db
			doc=Documents.GetByNum(doc.DocNum);
			try {
				SaveDocument(doc,pathImportFrom,patFolder);
				if(PrefC.UsingAtoZfolder) {
					Documents.Update(doc);
				}
			}
			catch {
				Documents.Delete(doc);
				throw;
			}
			return doc;
		}

		/*
		public static Document Import(Bitmap image,long docCategory,Patient pat) {
			string patFolder="";
			if(PrefC.UsingAtoZfolder) {
				patFolder=GetPatientFolder(pat,GetPreferredAtoZpath());
			}
			Document doc=new Document();
			doc.FileName=".jpg";
			doc.DateCreated=DateTime.Today;
			doc.DocCategory=docCategory;
			doc.PatNum=pat.PatNum;
			doc.ImgType=ImageType.Photo;
			//doc.RawBase64 handled further down.
			//doc.Thumbnail="";//no thumbnail yet
			Documents.Insert(doc,pat);//this assigns a filename and saves to db
			doc=Documents.GetByNum(doc.DocNum);
			try {
				SaveDocument(doc,image,ImageFormat.Jpeg,patFolder);
				if(PrefC.UsingAtoZfolder) {
					Documents.Update(doc);
				}
			}
			catch {
				Documents.Delete(doc);
				throw;
			}
			return doc;
		}*/

		/// <summary>Saves to either AtoZ folder or to db.  Saves image as a jpg.  Compression will differ depending on imageType.</summary>
		public static Document Import(Bitmap image,long docCategory,ImageType imageType,Patient pat) {
			string patFolder="";
			if(PrefC.UsingAtoZfolder) {
				patFolder=GetPatientFolder(pat,GetPreferredAtoZpath());
			}
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
				qualityL=100;
			}
			else if(imageType == ImageType.Photo) {
				qualityL=100;
			}
			else {//Assume document
				//Possible values 0-100?
				qualityL=(long)ComputerPrefs.LocalComputer.ScanDocQuality;
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
				SaveDocument(doc,image,myImageCodecInfo,myEncoderParameters,patFolder);
				if(!PrefC.UsingAtoZfolder) {
					Documents.Update(doc);//because SaveDocument stuck the image in doc.RawBase64.
					//no thumbnail yet
				}
			}
			catch {
				Documents.Delete(doc);
				throw;
			}
			return doc;
		}

		/// <summary>Obviously no support for db storage</summary>
		public static Document ImportForm(string form,long docCategory,Patient pat) {
			string patFolder=GetPatientFolder(pat,GetPreferredAtoZpath());
			string pathSourceFile = ODFileUtils.CombinePaths(GetPreferredAtoZpath(),"Forms",form);
			if(!File.Exists(pathSourceFile)) {
				throw new Exception(Lans.g("ContrDocs", "Could not find file: ") + pathSourceFile);
			}
			Document doc = new Document();
			doc.FileName = Path.GetExtension(pathSourceFile);
			doc.DateCreated = DateTime.Today;
			doc.DocCategory = docCategory;
			doc.PatNum = pat.PatNum;
			doc.ImgType = ImageType.Document;
			Documents.Insert(doc,pat);//this assigns a filename and saves to db
			doc=Documents.GetByNum(doc.DocNum);
			try {
				SaveDocument(doc,pathSourceFile,patFolder);
			}
			catch {
				Documents.Delete(doc);
				throw;
			}
			return doc;
		}

		/// <summary>Always saves as bmp.  So the 'paste to mount' logic needs to be changed to prevent conversion to bmp.</summary>
		public static Document ImportImageToMount(Bitmap image,short rotationAngle,long mountItemNum,long docCategory,Patient pat) {
			string patFolder=GetPatientFolder(pat,GetPreferredAtoZpath());
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
				SaveDocument(doc,image,ImageFormat.Bmp,patFolder);
			}
			catch {
				Documents.Delete(doc);
				throw;
			}
			return doc;
		}

		/// <summary>Saves to either AtoZ folder or to db.  Saves image as a jpg.  Compression will be according to user setting.</summary>
		public static EobAttach ImportEobAttach(Bitmap image,long claimPaymentNum) {
			string eobFolder="";
			if(PrefC.UsingAtoZfolder) {
				eobFolder=GetEobFolder();
			}
			EobAttach eob=new EobAttach();
			eob.FileName=".jpg";
			eob.DateTCreated = DateTime.Now;
			eob.ClaimPaymentNum=claimPaymentNum;
			EobAttaches.Insert(eob);//creates filename and saves to db
			eob=EobAttaches.GetOne(eob.EobAttachNum);
			long qualityL=(long)ComputerPrefs.LocalComputer.ScanDocQuality;
			ImageCodecInfo myImageCodecInfo;
			ImageCodecInfo[] encoders;
			encoders = ImageCodecInfo.GetImageEncoders();
			myImageCodecInfo = null;
			for(int j = 0;j < encoders.Length;j++) {
				if(encoders[j].MimeType == "image/jpeg") {
					myImageCodecInfo = encoders[j];
				}
			}
			EncoderParameters myEncoderParameters = new EncoderParameters(1);
			EncoderParameter myEncoderParameter = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality,qualityL);
			myEncoderParameters.Param[0] = myEncoderParameter;
			try {
				SaveEobAttach(eob,image,myImageCodecInfo,myEncoderParameters,eobFolder);
				if(!PrefC.UsingAtoZfolder) {
					EobAttaches.Update(eob);//because SaveEobAttach stuck the image in eob.RawBase64.
					//no thumbnail
				}
			}
			catch {
				EobAttaches.Delete(eob.EobAttachNum);
				throw;
			}
			return eob;
		}

		/// <summary></summary>
		public static EobAttach ImportEobAttach(string pathImportFrom,long claimPaymentNum) {
			string eobFolder="";
			if(PrefC.UsingAtoZfolder) {
				eobFolder=GetEobFolder();
			}
			EobAttach eob=new EobAttach();
			if(Path.GetExtension(pathImportFrom)=="") {//If the file has no extension
				eob.FileName=".jpg";
			}
			else{
				eob.FileName=Path.GetExtension(pathImportFrom);
			}
			eob.DateTCreated=File.GetLastWriteTime(pathImportFrom);
			eob.ClaimPaymentNum=claimPaymentNum;
			EobAttaches.Insert(eob);//creates filename and saves to db
			eob=EobAttaches.GetOne(eob.EobAttachNum);
			try {
				SaveEobAttach(eob,pathImportFrom,eobFolder);
				if(PrefC.UsingAtoZfolder) {
					EobAttaches.Update(eob);
				}
			}
			catch {
				EobAttaches.Delete(eob.EobAttachNum);
				throw;
			}
			return eob;
		}

		///<summary> Save a Document to another location on the disk (outside of Open Dental). </summary>
		public static void Export(string saveToPath,Document doc,Patient pat) {
			if(PrefC.UsingAtoZfolder) {
				string patFolder=GetPatientFolder(pat,GetPreferredAtoZpath());
				File.Copy(ODFileUtils.CombinePaths(patFolder,doc.FileName),saveToPath);
			}
			else {//image is in database
				byte[] rawData=Convert.FromBase64String(doc.RawBase64);
				Image image=null;
				using(MemoryStream stream=new MemoryStream()) {
					stream.Read(rawData,0,rawData.Length);
					image=Image.FromStream(stream);
				}
				image.Save(saveToPath);
			}
		}

		///<summary> Save an Eob to another location on the disk (outside of Open Dental). </summary>
		public static void ExportEobAttach(string saveToPath,EobAttach eob) {
			if(PrefC.UsingAtoZfolder) {
				string eobFolder=GetEobFolder();
				File.Copy(ODFileUtils.CombinePaths(eobFolder,eob.FileName),saveToPath);
			}
			else {//image is in database
				byte[] rawData=Convert.FromBase64String(eob.RawBase64);
				Image image=null;
				using(MemoryStream stream=new MemoryStream()) {
					stream.Read(rawData,0,rawData.Length);
					image=Image.FromStream(stream);
				}
				image.Save(saveToPath);
			}
		}

		///<summary>If using AtoZ folder, then patFolder must be fully qualified and valid.  If not usingAtoZ folder, this fills the doc.RawBase64 which must then be updated to db.  The image format can be bmp, jpg, etc, but this overload does not allow specifying jpg compression quality.</summary>
		public static void SaveDocument(Document doc,Bitmap image,ImageFormat format,string patFolder) {
			if(PrefC.UsingAtoZfolder) {
				string pathFileOut = ODFileUtils.CombinePaths(patFolder,doc.FileName);
				image.Save(pathFileOut);
			}
			else {//saving to db
				using(MemoryStream stream=new MemoryStream()) {
					image.Save(stream,format);
					byte[] rawData=stream.ToArray();
					doc.RawBase64=Convert.ToBase64String(rawData);
				}
			}
		}

		///<summary>If usingAtoZfoler, then patFolder must be fully qualified and valid.  If not usingAtoZ folder, this fills the doc.RawBase64 which must then be updated to db.</summary>
		public static void SaveDocument(Document doc,Bitmap image,ImageCodecInfo codec,EncoderParameters encoderParameters,string patFolder) {
			if(!PrefC.UsingAtoZfolder) {//if saving to db
				using(MemoryStream stream=new MemoryStream()) {
					image.Save(stream,codec,encoderParameters);
					byte[] rawData=stream.ToArray();
					doc.RawBase64=Convert.ToBase64String(rawData);
				}
			}
			else {//if saving to AtoZ folder
				image.Save(ODFileUtils.CombinePaths(patFolder,doc.FileName),codec,encoderParameters);
			}
		}

		///<summary>If usingAtoZfoler, then patFolder must be fully qualified and valid.  If not usingAtoZ folder, this fills the eob.RawBase64 which must then be updated to db.</summary>
		public static void SaveDocument(Document doc,string pathSourceFile,string patFolder) {
			if(PrefC.UsingAtoZfolder) {
				File.Copy(pathSourceFile,ODFileUtils.CombinePaths(patFolder,doc.FileName));
			}
			else {//saving to db
				byte[] rawData=File.ReadAllBytes(pathSourceFile);
				doc.RawBase64=Convert.ToBase64String(rawData);
			}
		}

		///<summary>If usingAtoZfoler, then patFolder must be fully qualified and valid.  If not usingAtoZ folder, this fills the eob.RawBase64 which must then be updated to db.</summary>
		public static void SaveEobAttach(EobAttach eob,Bitmap image,ImageCodecInfo codec,EncoderParameters encoderParameters,string eobFolder) {
			if(PrefC.UsingAtoZfolder) {
				image.Save(ODFileUtils.CombinePaths(eobFolder,eob.FileName),codec,encoderParameters);
			}
			else {//saving to db
				using(MemoryStream stream=new MemoryStream()) {
					image.Save(stream,codec,encoderParameters);
					byte[] rawData=stream.ToArray();
					eob.RawBase64=Convert.ToBase64String(rawData);
				}
			}
		}

		///<summary>If usingAtoZfoler, then eobFolder must be fully qualified and valid.  If not usingAtoZ folder, this fills the eob.RawBase64 which must then be updated to db.</summary>
		public static void SaveEobAttach(EobAttach eob,string pathSourceFile,string eobFolder) {
			if(PrefC.UsingAtoZfolder) {
				File.Copy(pathSourceFile,ODFileUtils.CombinePaths(eobFolder,eob.FileName));
			}
			else {//saving to db
				byte[] rawData=File.ReadAllBytes(pathSourceFile);
				eob.RawBase64=Convert.ToBase64String(rawData);
			}
		}

		///<summary>For each of the documents in the list, deletes row from db and image from AtoZ folder if needed.</summary>
		public static void DeleteDocuments(IList<Document> documents,string patFolder) {
			for(int i=0;i<documents.Count;i++){
				if(documents[i]==null){
					continue;
				}
				if(PrefC.UsingAtoZfolder) {
					try{
						string filePath = ODFileUtils.CombinePaths(patFolder,documents[i].FileName);
						if(File.Exists(filePath)) {
							File.Delete(filePath);
						}
					}
					catch {
						//if(verbose) {
						//	Debug.WriteLine(Lans.g("ContrDocs", "Could not delete file. It may be in use elsewhere, or may have already been deleted."));
						//}
					}
				}
				//Row from db.  This deletes the "image file" also if it's stored in db.
				Documents.Delete(documents[i]);
			}
		}

		///<summary>Also handles deletion of db object.</summary>
		public static void DeleteEobAttach(EobAttach eob) {
			if(PrefC.UsingAtoZfolder) {
				string eobFolder=GetEobFolder();
				string filePath=ODFileUtils.CombinePaths(eobFolder,eob.FileName);
				if(File.Exists(filePath)) {
					try {
						File.Delete(filePath);
					}
					catch { }//file seems to be frequently locked.
				}
			}
			//db
			EobAttaches.Delete(eob.EobAttachNum);
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

		public static string GetExtension(Document doc) {
			return Path.GetExtension(doc.FileName).ToLower();
		}

		public static string GetFilePath(Document doc,string patFolder) {
			//string patFolder=GetPatientFolder(pat);
			return ODFileUtils.CombinePaths(patFolder,doc.FileName);
		}
		
		/*
		public static bool IsImageFile(string filename) {
			try {
				Bitmap bitmap = new Bitmap(filename);
				bitmap.Dispose();
				bitmap=null;
				return true;
			}
			catch {
				return false;
			}
		}*/

		///<summary>Returns true if the given filename contains a supported file image extension.</summary>
		public static bool HasImageExtension(string fileName) {
			string ext = Path.GetExtension(fileName).ToLower();
			//The following supported bitmap types were found on a microsoft msdn page:
			return (ext == ".jpg" || ext == ".jpeg" || ext == ".tga" || ext == ".bmp" || ext == ".tif" ||
				ext == ".tiff" || ext == ".gif" || ext == ".emf" || ext == ".exif" || ext == ".ico" || ext == ".png" || ext == ".wmf");
		}

		
	
	}
}
