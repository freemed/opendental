using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using OpenDentBusiness;
using CodeBase;
using OpenDental.DataAccess;
using OpenDentBusiness.Imaging;
using OpenDental.Imaging;

namespace OpenDentBusiness {
	///<summary>Handles documents and images for the Images module</summary>
	public class Documents {

		///<summary></summary>
		public static Document[] GetAllWithPat(int patNum) {
			string command="SELECT * FROM document WHERE PatNum='"+POut.PInt(patNum)+"'";
			return RefreshAndFill(command);
		}

		///<summary>Gets the document with the specified document number.</summary>
		public static Document GetByNum(int docNum){
			string command="SELECT * FROM document WHERE DocNum='"+docNum+"'";
			DataTable table=General2.GetTable(command);
			if(table.Rows.Count<1){
				return new Document();
			}
			return Fill(table.Rows[0]);
		}

		///<summary></summary>
		public static Document Fill(DataRow document){
			if(document==null){
				return null;
			}
			Document doc=new Document();
			doc.DocNum        =PIn.PInt(document[0].ToString());
			doc.Description   =PIn.PString(document[1].ToString());
			doc.DateCreated   =PIn.PDate(document[2].ToString());
			doc.DocCategory   =PIn.PInt(document[3].ToString());
			doc.PatNum       =PIn.PInt(document[4].ToString());
			doc.FileName      =PIn.PString(document[5].ToString());
			doc.ImgType       =(ImageType)PIn.PInt(document[6].ToString());
			doc.IsFlipped     =PIn.PBool(document[7].ToString());
			doc.DegreesRotated=PIn.PShort(document[8].ToString());
			doc.ToothNumbers  =PIn.PString(document[9].ToString());
			doc.Note          =PIn.PString(document[10].ToString());
			doc.SigIsTopaz    =PIn.PBool(document[11].ToString());
			doc.Signature     =PIn.PString(document[12].ToString());
			doc.CropX			    =PIn.PInt(document[13].ToString());
			doc.CropY         =PIn.PInt(document[14].ToString());
			doc.CropW         =PIn.PInt(document[15].ToString());
			doc.CropH         =PIn.PInt(document[16].ToString());
			doc.WindowingMin  =PIn.PInt(document[17].ToString());
			doc.WindowingMax  =PIn.PInt(document[18].ToString());
			doc.MountItemNum  =PIn.PInt(document[19].ToString());
			return doc;
		}

		public static Document[] Fill(DataTable documents){
			if(documents==null){
				return new Document[0];
			}
			Document[] List=new Document[documents.Rows.Count];
			for(int i=0;i<documents.Rows.Count;i++) {
				List[i]=Fill(documents.Rows[i]);
			}
			return List;
		}

		private static Document[] RefreshAndFill(string command) {
			return Fill(General2.GetTable(command));
		}

		///<summary>Inserts a new document into db, creates a filename based on Cur.DocNum, and then updates the db with this filename.  Also attaches the document to the current patient.</summary>
		[Obsolete("This method now throws an exception!")]
		public static void Insert(Document doc,Patient pat){
			int insertID=0;
				if(RemotingClient.OpenDentBusinessIsLocal) {
					insertID=DocumentB.Insert(doc,pat.LName+pat.FName,pat.PatNum);
				}
				else {
					DtoDocumentInsert dto=new DtoDocumentInsert();
					dto.Doc=doc;
					dto.PatLF=pat.LName+pat.FName;
					dto.PatNum=pat.PatNum;
					insertID=RemotingClient.ProcessCommand(dto);
				}
			doc.DocNum=insertID;
		}

		///<summary></summary>
		[Obsolete("This method now throws an exception!")]
		public static void Update(Document doc){
				if(RemotingClient.OpenDentBusinessIsLocal) {
					DocumentB.Update(doc);
				}
				else {
					DtoDocumentUpdate dto=new DtoDocumentUpdate();
					dto.Doc=doc;
					RemotingClient.ProcessCommand(dto);
				}
		}

		///<summary></summary>
		public static void Delete(Document doc){
			string command= "DELETE from document WHERE DocNum = '"+doc.DocNum.ToString()+"'";
			General2.NonQ(command);	
			command= "DELETE from docattach WHERE DocNum = '"+doc.DocNum.ToString()+"'";
			General2.NonQ(command);	
		}

		///<summary>This is used by FormImageViewer to get a list of paths based on supplied list of DocNums. The reason is that later we will allow sharing of documents, so the paths may not be in the current patient folder.</summary>
		public static ArrayList GetPaths(ArrayList docNums){
			if(docNums.Count==0){
				return new ArrayList();
			}
			string command="SELECT document.DocNum,document.FileName,patient.ImageFolder "
				+"FROM document "
				+"LEFT JOIN patient ON patient.PatNum=document.PatNum "
				+"WHERE document.DocNum = '"+docNums[0].ToString()+"'";
			for(int i=1;i<docNums.Count;i++){
				command+=" OR document.DocNum = '"+docNums[i].ToString()+"'";
			}
			//remember, they will not be in the correct order.
			DataTable table=General2.GetTable(command);
			Hashtable hList=new Hashtable();//key=docNum, value=path
			//one row for each document, but in the wrong order
			for(int i=0;i<table.Rows.Count;i++){
				//We do not need to check if A to Z folders are being used here, because
				//thumbnails are not visible from the chart module when A to Z are disabled,
				//making it impossible to launch the form image viewer (the only place this
				//function is called from.
				hList.Add(PIn.PInt(table.Rows[i][0].ToString()),
					ODFileUtils.CombinePaths(new string[] {	FileStoreSettings.GetPreferredImagePath,
																									PIn.PString(table.Rows[i][2].ToString()).Substring(0,1).ToUpper(),
																									PIn.PString(table.Rows[i][2].ToString()),
																									PIn.PString(table.Rows[i][1].ToString()),}));
			}
			ArrayList retVal=new ArrayList();
			for(int i=0;i<docNums.Count;i++){
				retVal.Add((string)hList[(int)docNums[i]]);
			}
			return retVal;
		}

		/// <summary>Makes one call to the database to retrieve the document of the patient for the given patNum, then uses that document and the patFolder to load and process the patient picture so it appears the same way it did in the image module.  It first creates a 100x100 thumbnail if needed, then it uses the thumbnail so no scaling needed. Returns false if there is no patient picture, true otherwise. Sets the value of patientPict equal to a new instance of the patient's processed picture, but will be set to null on error. Assumes WithPat will always be same as patnum.</summary>
		[Obsolete("This method now throws an exception!")]
		public static bool GetPatPict(int patNum, string patFolder, out Bitmap patientPict) {
			patientPict=null;
			//first establish which category pat pics are in
			int defNumPicts=0;
			for(int i=0;i<DefB.Short[(int)DefCat.ImageCats].Length;i++){
				if(DefB.Short[(int)DefCat.ImageCats][i].ItemValue=="P" || DefB.Short[(int)DefCat.ImageCats][i].ItemValue=="XP"){
					defNumPicts=DefB.Short[(int)DefCat.ImageCats][i].DefNum;
					break;
				}
			}
			if(defNumPicts==0){//no category set for picts
				return false;
			}
			//then find 
			string command="SELECT document.* FROM document,docattach "
				+"WHERE document.DocNum=docattach.DocNum "
				+"AND docattach.PatNum="+POut.PInt(patNum)
				+" AND document.DocCategory="+POut.PInt(defNumPicts)
				+" ORDER BY DateCreated DESC ";
			//gets the most recent
			if(DataSettings.DbType==DatabaseType.Oracle){
				command="SELECT * FROM ("+command+") WHERE ROWNUM<=1";
			}else{//Assume MySQL
				command+="LIMIT 1";
			}
			Document[] pictureDocs=RefreshAndFill(command);
			if(pictureDocs==null || pictureDocs.Length<1){//no pictures
				return false;
			}
			string shortFileName=pictureDocs[0].FileName;
			if(shortFileName.Length<1){
				return false;
			}
			string fullName=ODFileUtils.CombinePaths(patFolder,shortFileName);
			if(!File.Exists(fullName)) {
				return false;
			}
			//create Thumbnails folder
			string thumbPath=ODFileUtils.CombinePaths(patFolder,"Thumbnails");
			if(!Directory.Exists(thumbPath)) {
				try {
					Directory.CreateDirectory(thumbPath);
				}
				catch {
					throw new ImageStoreCreationException(Lan.g("Documents","Error.  Could not create thumbnails folder. "));
				}
			}
			string thumbFileName=ODFileUtils.CombinePaths(new string[] { patFolder,"Thumbnails",shortFileName });
			if(!ImageHelper.HasImageExtension(thumbFileName)){
				return false;
			}
			if(File.Exists(thumbFileName)) {//use existing thumbnail
				patientPict=(Bitmap)Bitmap.FromFile(thumbFileName);
				return true;
			}
			//add thumbnail
			Bitmap thumbBitmap;
			//Gets the cropped/flipped/rotated image with any color filtering applied.
			Bitmap sourceImage=new Bitmap(fullName);
			Bitmap fullImage=ImageHelper.ApplyDocumentSettingsToImage(pictureDocs[0],sourceImage,ApplySettings.ALL);
			sourceImage.Dispose();
			thumbBitmap=ImageHelper.GetThumbnail(fullImage,100);
			fullImage.Dispose();
			try {
				thumbBitmap.Save(thumbFileName);
			}
			catch {
				//Oh well, we can regenerate it next time if we have to!
			}
			patientPict=thumbBitmap;
			return true;
		}

		///<summary>Returns the documents which correspond to the given mountitems.</summary>
		public static Document[] GetDocumentsForMountItems(MountItem[] mountItems) {
			if(mountItems==null || mountItems.Length<1){
				return new Document[0];
			}
			Document[] documents=new Document[mountItems.Length];
			for(int i=0;i<mountItems.Length;i++){
				string command="SELECT * FROM document WHERE MountItemNum='"+POut.PInt(mountItems[i].MountItemNum)+"'";
				DataTable table=General2.GetTable(command);
				if(table.Rows.Count<1){
					documents[i]=null;
				}else{
					documents[i]=Fill(table)[0];
				}
			}
			return documents;
		}

		///<summary>Any filenames mentioned in the fileList which are not attached to the given patient are properly attached to that patient. Returns the total number of documents that were newly attached to the patient.</summary>
		public static int InsertMissing(Patient patient,string[] fileList){
			int countAdded=0;
			string command="SELECT FileName FROM document WHERE PatNum='"+patient.PatNum+"' ORDER BY FileName";
			DataTable table=General2.GetTable(command);
			for(int j=0;j<fileList.Length;j++){
				if(!IsAcceptableFileName(fileList[j])){
					continue;
				}
				bool inList=false;
				for(int i=0;i<table.Rows.Count && !inList;i++){
					inList=(table.Rows[i]["FileName"].ToString()==fileList[j]);
				}
				if(!inList){
					Document doc=new Document();
					doc.DateCreated=DateTime.Today;
					doc.Description=fileList[j];
					doc.DocCategory=DefB.Short[(int)DefCat.ImageCats][0].DefNum;//First category.
					doc.FileName=fileList[j];
					doc.PatNum=patient.PatNum;
					Insert(doc,patient);
					countAdded++;
				}
			}
			return countAdded;
		}

		///<Summary>Parameters: 1:PatNum</Summary>
		public static DataSet RefreshForPatient(string[] parameters) {
			DataSet retVal=new DataSet();
			retVal.Tables.Add(GetTreeListTableForPatient(parameters[0]));
			return retVal;
		}

		private static DataTable GetTreeListTableForPatient(string patNum){
			DataConnection dcon=new DataConnection();
			DataTable table=new DataTable("DocumentList");
			DataRow row;
			DataTable raw;
			string command;
			//Rows are first added to the resultSet list so they can be sorted at the end as a larger group, then
			//they are placed in the datatable to be returned.
			List<Object> resultSet=new List<Object>();
			//columns that start with lowercase are altered for display rather than being raw data.
			table.Columns.Add("DocNum");
			table.Columns.Add("MountNum");
			table.Columns.Add("DocCategory");
			table.Columns.Add("DateCreated");
			table.Columns.Add("docFolder");//The folder order to which the Document category corresponds.
			table.Columns.Add("description");
			table.Columns.Add("ImgType");
			//Move all documents which are invisible to the first document category.
			command="SELECT DocNum FROM document WHERE PatNum='"+patNum+"' AND "
				+"DocCategory<0";
			raw=dcon.GetTable(command);
			if(raw.Rows.Count>0){//Are there any invisible documents?
				command="UPDATE document SET DocCategory='"+DefB.Short[(int)DefCat.ImageCats][0].DefNum
					+"' WHERE PatNum='"+patNum+"' AND (";
				for(int i=0;i<raw.Rows.Count;i++){
					command+="DocNum='"+PIn.PInt(raw.Rows[i]["DocNum"].ToString())+"' ";
					if(i<raw.Rows.Count-1){
						command+="OR ";
					}
				}
				command+=")";
				dcon.NonQ(command);
			}
			//Load all documents into the result table.
			command="SELECT DocNum,DocCategory,DateCreated,Description,ImgType,MountItemNum FROM document WHERE PatNum='"+patNum+"'";
			raw=dcon.GetTable(command);
			for(int i=0;i<raw.Rows.Count;i++){
				//Make sure hidden documents are never added (there is a small possibility that one is added after all are made visible).
				if(DefB.GetOrder(DefCat.ImageCats,PIn.PInt(raw.Rows[i]["DocCategory"].ToString()))<0){ 
					continue;
				}
				//Do not add individual documents which are part of a mount object.
				if(PIn.PInt(raw.Rows[i]["MountItemNum"].ToString())!=0) {
					continue;
				}
				row=table.NewRow();
				row["DocNum"]=PIn.PInt(raw.Rows[i]["DocNum"].ToString());
				row["MountNum"]=0;
				row["DocCategory"]=PIn.PInt(raw.Rows[i]["DocCategory"].ToString());
				row["DateCreated"]=PIn.PDate(raw.Rows[i]["DateCreated"].ToString());
				row["docFolder"]=DefB.GetOrder(DefCat.ImageCats,PIn.PInt(raw.Rows[i]["DocCategory"].ToString()));
				row["description"]=PIn.PDate(raw.Rows[i]["DateCreated"].ToString()).ToString("d")+": "
					+PIn.PString(raw.Rows[i]["Description"].ToString());
				row["ImgType"]=PIn.PInt(raw.Rows[i]["ImgType"].ToString());
				resultSet.Add(row);
			}
			//Move all mounts which are invisible to the first document category.
			command="SELECT MountNum FROM mount WHERE PatNum='"+patNum+"' AND "
				+"DocCategory<0";
			raw=dcon.GetTable(command);
			if(raw.Rows.Count>0) {//Are there any invisible mounts?
				command="UPDATE mount SET DocCategory='"+DefB.Short[(int)DefCat.ImageCats][0].DefNum
					+"' WHERE PatNum='"+patNum+"' AND (";
				for(int i=0;i<raw.Rows.Count;i++) {
					command+="MountNum='"+PIn.PInt(raw.Rows[i]["MountNum"].ToString())+"' ";
					if(i<raw.Rows.Count-1) {
						command+="OR ";
					}
				}
				command+=")";
				dcon.NonQ(command);
			}
			//Load all mounts into the result table.
			command="SELECT MountNum,DocCategory,DateCreated,Description,ImgType FROM mount WHERE PatNum='"+patNum+"'";
			raw=dcon.GetTable(command);
			for(int i=0;i<raw.Rows.Count;i++){
				//Make sure hidden mounts are never added (there is a small possibility that one is added after all are made visible).
				if(DefB.GetOrder(DefCat.ImageCats,PIn.PInt(raw.Rows[i]["DocCategory"].ToString()))<0) {
					continue;
				}
				row=table.NewRow();
				row["DocNum"]=0;
				row["MountNum"]=PIn.PInt(raw.Rows[i]["MountNum"].ToString());
				row["DocCategory"]=PIn.PInt(raw.Rows[i]["DocCategory"].ToString());
				row["DateCreated"]=PIn.PDate(raw.Rows[i]["DateCreated"].ToString());
				row["docFolder"]=DefB.GetOrder(DefCat.ImageCats,PIn.PInt(raw.Rows[i]["DocCategory"].ToString()));
				row["description"]=PIn.PDate(raw.Rows[i]["DateCreated"].ToString()).ToString("d")+": "
					+PIn.PString(raw.Rows[i]["Description"].ToString());
				row["ImgType"]=PIn.PInt(raw.Rows[i]["ImgType"].ToString());
				resultSet.Add(row);
			}
			//We must soft the results after they are returned from the database, because the database software (i.e. MySQL)
			//cannot return sorted results from two or more result sets like we have here.
			resultSet.Sort(delegate(Object o1,Object o2) {
				DataRow r1=(DataRow)o1;
				DataRow r2=(DataRow)o2;
				int docFolder1=Convert.ToInt32(r1["docFolder"].ToString());
				int docFolder2=Convert.ToInt32(r2["docFolder"].ToString());
				if(docFolder1<docFolder2){
					return -1;
				}else if(docFolder1>docFolder2){
					return 1;
				}
				return PIn.PDate(r1["DateCreated"].ToString()).CompareTo(PIn.PDate(r2["DateCreated"].ToString()));
			});
			//Finally, move the results from the list into a data table.
			for(int i=0;i<resultSet.Count;i++){
				table.Rows.Add((DataRow)resultSet[i]);
			}
			return table;
		}

		///<summary>Returns false if the file is a specific short file name that is not accepted or contains one of the unsupported file exentions.</summary>
		public static bool IsAcceptableFileName(string file) {
			string[] specificBadFileNames=new string[] {
				"thumbs.db"
			};
			for(int i=0;i<specificBadFileNames.Length;i++) {
				if(file.Length>=specificBadFileNames[i].Length && 
					file.Substring(file.Length-specificBadFileNames[i].Length,
					specificBadFileNames[i].Length).ToLower()==specificBadFileNames[i]) {
					return false;
				}
			}
			return true;
		}

	}	
  
}