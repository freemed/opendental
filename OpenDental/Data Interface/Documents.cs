using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;
using OpenDentBusiness;
using CodeBase;

namespace OpenDental{
	///<summary>Handles documents and images for the Images module</summary>
	public class Documents {

		private static string BuildRefreshCommand(List<DocAttach> docAttachList){
			string command="SELECT * FROM document WHERE DocNum = '"+docAttachList[0].DocNum.ToString()+"'";
			for(int i=1;i<docAttachList.Count;i++) {
				command+=" OR DocNum = '"+POut.PInt(docAttachList[i].DocNum)+"'";
			}
			command+=" ORDER BY DateCreated";
			return command;
		}

		///<summary>This gets a list of all documents referred to in the docAttachList.  Basically, all docs for a patient.  It is done this way because patients will later be sharing documents.</summary>
		public static Document[] Refresh(List<DocAttach> docAttachList) {
			if(docAttachList.Count<1) {
				return new Document[0];
			}
			return RefreshAndFill(BuildRefreshCommand(docAttachList));
		}

		public static DataTable RefreshTable(List<DocAttach> docAttachList) {
			if(docAttachList.Count<1) {
				return new DataTable();
			}
			return General.GetTable(BuildRefreshCommand(docAttachList));
		}

		/*
		///<summary></summary>
		public static Document[] GetAllWithPat(int patNum) {
			string command="SELECT * FROM document WHERE PatNum="+POut.PInt(patNum);
			return RefreshAndFill(command);
		}*/

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
			doc.DegreesRotated=PIn.PInt(document[8].ToString());
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
			return Fill(General.GetTable(command));
		}

		///<summary>For updating and/or adding single documents to the tree view. This reduces network traffic, but most importantly prevents the image module from having to rebuild the tree document list every time a change is made.</summary>
		public static DataRow GetDocumentRow(string docNum){
			string command="SELECT * FROM document WHERE DocNum = '"+docNum+"'";
			DataTable docRow=General.GetTable(command);//This can only return 0 or 1 results, since document numbers are unique (primary keys).
			if(docRow==null || docRow.Rows==null || docRow.Rows.Count<1){
				return null;
			}
			return docRow.Rows[0];
		}

		///<summary>Inserts a new document into db, creates a filename based on Cur.DocNum, and then updates the db with this filename.  Also attaches the document to the current patient.</summary>
		public static void Insert(Document doc,Patient pat){
			int insertID=0;
			try {
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
			}
			catch(Exception e) {
				MessageBox.Show(e.Message);
			}
			doc.DocNum=insertID;
		}

		///<summary></summary>
		public static void Update(Document doc){
			try {
				if(RemotingClient.OpenDentBusinessIsLocal) {
					DocumentB.Update(doc);
				}
				else {
					DtoDocumentUpdate dto=new DtoDocumentUpdate();
					dto.Doc=doc;
					RemotingClient.ProcessCommand(dto);
				}
			}
			catch(Exception e) {
				MessageBox.Show(e.Message);
			}		
		}

		///<summary></summary>
		public static void Delete(Document doc){
			string command= "DELETE from document WHERE DocNum = '"+doc.DocNum.ToString()+"'";
			General.NonQ(command);	
			command= "DELETE from docattach WHERE DocNum = '"+doc.DocNum.ToString()+"'";
			General.NonQ(command);	
		}

		///<summary>Loops through List to find Cur based on docNum.</summary>
		public static Document GetDocument(string docNum,Document[] docList){
			for(int i=0;i<docList.Length;i++){
				if(docList[i].DocNum.ToString()==docNum){
					return docList[i];
				}
			}
			return null;//should never happen
		}

		///<summary>Used when dragging to a new category. Loops through List to find the defNum of the category based on docNum.</summary>
		public static int GetCategory(string docNum,Document[] docList){
			for(int i=0;i<docList.Length;i++){
				if(docList[i].DocNum.ToString()==docNum){
					return docList[i].DocCategory;
				}
			}
			return -1;
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
			DataTable table=General.GetTable(command);
			Hashtable hList=new Hashtable();//key=docNum, value=path
			//one row for each document, but in the wrong order
			for(int i=0;i<table.Rows.Count;i++){
				//We do not need to check if A to Z folders are being used here, because
				//thumbnails are not visible from the chart module when A to Z are disabled,
				//making it impossible to launch the form image viewer (the only place this
				//function is called from.
				hList.Add(PIn.PInt(table.Rows[i][0].ToString()),
					ODFileUtils.CombinePaths(new string[] {	FormPath.GetPreferredImagePath(),
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
		public static bool GetPatPict(int patNum,string patFolder,out Bitmap patientPict){
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
			if(FormChooseDatabase.DBtype==DatabaseType.Oracle){
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
					MessageBox.Show(Lan.g("Documents","Error.  Could not create thumbnails folder. "));
					return false;
				}
			}
			string thumbFileName=ODFileUtils.CombinePaths(new string[] { patFolder,"Thumbnails",shortFileName });
			if(!ContrDocs.HasImageExtension(thumbFileName)){
				return false;
			}
			if(File.Exists(thumbFileName)) {//use existing thumbnail
				patientPict=(Bitmap)Bitmap.FromFile(thumbFileName);
				return true;
			}
			//add thumbnail
			int thumbSize=100;//All thumbnails are square.
			Bitmap thumbBitmap;
			//Gets the cropped/flipped/rotated image with any color filtering applied.
			Bitmap sourceImage=new Bitmap(fullName);
			Bitmap fullImage=ContrDocs.ApplyDocumentSettingsToImage(pictureDocs[0],sourceImage,ContrDocs.ApplySettings.ALL);
			sourceImage.Dispose();
			thumbBitmap=ContrDocs.GetThumbnail(fullImage,100);
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

		///<summary>Returns the document which corresponds to the given mountitem.</summary>
		public static Document GetDocumentForMountItem(int mountItem) {
			string command="SELECT * FROM document WHERE MountItemNum='"+mountItem+"'";
			DataTable result=General.GetTable(command);
			if(result.Rows.Count!=1) {
				Logger.openlog.Log("Documents.GetDocumentForMountItem: There are "+result.Rows.Count+
					" documents associated with mount item "+mountItem+" when exactly 1 "+
					"document was expected.",Logger.Severity.ERROR);
			}
			return Fill(result.Rows[0]);
		}

	}	
  
}