using System;
using System.Data;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace OpenDentBusiness {
	public class DocumentB{
		///<summary>Inserts a new document into db, creates a filename based on Cur.DocNum, and then updates the db with this filename.  Also attaches the document to the current patient.</summary>
		public static int Insert(Document doc, string patLF, int patNum){
			if(PrefB.RandomKeys) {
				doc.DocNum=MiscDataB.GetKey("document","DocNum");
			}
			string command="INSERT INTO document (";
			if(PrefB.RandomKeys) {
				command+="DocNum,";
			}
			command+="Description,DateCreated,DocCategory,PatNum,FileName,ImgType,"
				+"IsFlipped,DegreesRotated,ToothNumbers,Note,SigIsTopaz,Signature,CropX,CropY,CropW,CropH,"
				+"WindowingMin,WindowingMax,MountItemNum) VALUES(";
			if(PrefB.RandomKeys) {
				command+="'"+POut.PInt(doc.DocNum)+"', ";
			}
			command+=
				 "'"+POut.PString(doc.Description)+"', "
				+POut.PDate  (doc.DateCreated)+", "
				+"'"+POut.PInt    (doc.DocCategory)+"', "
				+"'"+POut.PInt    (doc.PatNum)+"', "
				+"'"+POut.PString(doc.FileName)+"', "//this may simply be the extension at this point, or it may be the full filename.
				+"'"+POut.PInt    ((int)doc.ImgType)+"', "
				+"'"+POut.PBool  (doc.IsFlipped)+"', "
				+"'"+POut.PInt    (doc.DegreesRotated)+"', "
				+"'"+POut.PString(doc.ToothNumbers)+"', "
				+"'"+POut.PString(doc.Note)+"', "
				+"'"+POut.PBool  (doc.SigIsTopaz)+"', "
				+"'"+POut.PString(doc.Signature)+"',"
				+"'"+POut.PInt(doc.CropX)+"',"
				+"'"+POut.PInt(doc.CropY)+"',"
				+"'"+POut.PInt(doc.CropW)+"',"
				+"'"+POut.PInt(doc.CropH)+"',"
				+"'"+POut.PInt(doc.WindowingMin)+"',"
				+"'"+POut.PInt(doc.WindowingMax)+"',"
				+"'"+POut.PInt(doc.MountItemNum)+"')";
			/*+"'"+POut.PDate  (LastAltered)+"', "//will later be used in backups
					+"'"+POut.PBool  (IsDeleted)+"')";//ditto*/
			//MessageBox.Show(cmd.CommandText);
			DataConnection dcon=new DataConnection();
			if(PrefB.RandomKeys) {
				dcon.NonQ(command);
			}
			else {
				dcon.NonQ(command,true);
				doc.DocNum=dcon.InsertID;
			}
			//If the current filename is just an extension, then assign it a unique name.
			if(doc.FileName==Path.GetExtension(doc.FileName)) {
				string extension=doc.FileName;
				doc.FileName="";
				string s=patLF;//pat.LName+pat.FName;
				for(int i=0;i<s.Length;i++) {
					if(Char.IsLetter(s,i)) {
						doc.FileName+=s.Substring(i,1);
					}
				}
				doc.FileName+=doc.DocNum.ToString()+extension;//ensures unique name
				//there is still a slight chance that someone manually added a file with this name, so quick fix:
				command="SELECT FileName FROM document WHERE PatNum="+POut.PInt(doc.PatNum);
				DataTable table=dcon.GetTable(command);
				string[] usedNames=new string[table.Rows.Count];
				for(int i=0;i<table.Rows.Count;i++){
					usedNames[i]=PIn.PString(table.Rows[i][0].ToString());
				}
				while(IsFileNameInList(doc.FileName,usedNames)) {
					doc.FileName="x"+doc.FileName;
				}
				/*Document[] docList=GetAllWithPat(doc.PatNum);
				while(IsFileNameInList(doc.FileName,docList)) {
					doc.FileName="x"+doc.FileName;
				}*/
				Update(doc);
			}
			DocAttach docAttach=new DocAttach();
			docAttach.DocNum=doc.DocNum;
			docAttach.PatNum=patNum;
			DocAttachB.Insert(docAttach);
			return doc.DocNum;
		}

		///<summary>When first opening the image module, this tests to see whether a given filename is in the database. Also used when naming a new document to ensure unique name.</summary>
		public static bool IsFileNameInList(string fileName,string[] usedNames) {
			for(int i=0;i<usedNames.Length;i++) {
				if(usedNames[i]==fileName)
					return true;
			}
			return false;
		}

		///<summary></summary>
		public static int Update(Document doc) {
			string command="UPDATE document SET " 
				+ "Description = '"      +POut.PString(doc.Description)+"'"
				+ ",DateCreated = "     +POut.PDate  (doc.DateCreated)
				+ ",DocCategory = '"     +POut.PInt    (doc.DocCategory)+"'"
				+ ",PatNum = '"         +POut.PInt    (doc.PatNum)+"'"
				+ ",FileName    = '"     +POut.PString(doc.FileName)+"'"
				+ ",ImgType    = '"      +POut.PInt    ((int)doc.ImgType)+"'"
				+ ",IsFlipped   = '"     +POut.PBool  (doc.IsFlipped)+"'"
				+ ",DegreesRotated   = '"+POut.PInt    (doc.DegreesRotated)+"'"
				+ ",ToothNumbers   = '"  +POut.PString(doc.ToothNumbers)+"'"
				+ ",Note   = '"          +POut.PString(doc.Note)+"'"
				+ ",SigIsTopaz    = '"   +POut.PBool  (doc.SigIsTopaz)+"'"
				+ ",Signature   = '"     +POut.PString(doc.Signature)+"'"
				+ ",CropX       ='"			 +POut.PInt(doc.CropX)+"'"
				+ ",CropY       ='"			 +POut.PInt(doc.CropY)+"'"
				+ ",CropW       ='"			 +POut.PInt(doc.CropW)+"'"
				+ ",CropH       ='"			 +POut.PInt(doc.CropH)+"'"
				+ ",WindowingMin ='"		 +POut.PInt(doc.WindowingMin)+"'"
				+ ",WindowingMax ='"		 +POut.PInt(doc.WindowingMax)+"'"
				+ ",MountItemNum ='"		 +POut.PInt(doc.MountItemNum)+"'"
				+" WHERE DocNum = '"     +POut.PInt    (doc.DocNum)+"'";
			//MessageBox.Show(cmd.CommandText);
			DataConnection dcon=new DataConnection();
			dcon.NonQ(command);
			return 0;
		}
	}

}
