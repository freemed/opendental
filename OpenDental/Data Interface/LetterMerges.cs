using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	///<summary></summary>
	public class LetterMerges {
		///<summary>List of all lettermerges.</summary>
		public static LetterMerge[] List;

#if !DISABLE_MICROSOFT_OFFICE
		private static Word.Application wordApp;

		///<summary>This is a static reference to a word application.  That way, we can reuse it instead of having to reopen Word each time.</summary>
		public static Word.Application WordApp {
			get {
				if(wordApp==null) {
					wordApp=new Word.Application();
					wordApp.Visible=true;
				}
				try {
					wordApp.Activate();
				}
				catch {
					wordApp=new Word.Application();
					wordApp.Visible=true;
					wordApp.Activate();
				}
				return wordApp;
			}
		}
#endif
		
		///<summary>Must have run LetterMergeFields first.</summary>
		public static void Refresh() {
			string command=
				"SELECT * FROM lettermerge ORDER BY Description";
			DataTable table=General.GetTable(command);
			List=new LetterMerge[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				List[i]=new LetterMerge();
				List[i].LetterMergeNum= PIn.PInt(table.Rows[i][0].ToString());
				List[i].Description   = PIn.PString(table.Rows[i][1].ToString());
				List[i].TemplateName  = PIn.PString(table.Rows[i][2].ToString());
				List[i].DataFileName  = PIn.PString(table.Rows[i][3].ToString());
				List[i].Category      = PIn.PInt(table.Rows[i][4].ToString());
				List[i].Fields=LetterMergeFields.GetForLetter(List[i].LetterMergeNum);
			}
		}

		///<summary>Inserts this lettermerge into database.</summary>
		public static void Insert(LetterMerge merge){
			if(PrefB.RandomKeys){
				merge.LetterMergeNum=MiscData.GetKey("lettermerge","LetterMergeNum");
			}
			string command= "INSERT INTO lettermerge (";
			if(PrefB.RandomKeys){
				command+="LetterMergeNum,";
			}
			command+="Description,TemplateName,DataFileName,"
				+"Category) VALUES(";
			if(PrefB.RandomKeys){
				command+="'"+POut.PInt(merge.LetterMergeNum)+"', ";
			}
			command+=
				 "'"+POut.PString(merge.Description)+"', "
				+"'"+POut.PString(merge.TemplateName)+"', "
				+"'"+POut.PString(merge.DataFileName)+"', "
				+"'"+POut.PInt   (merge.Category)+"')";
			//MessageBox.Show(string command);
 			if(PrefB.RandomKeys){
				General.NonQ(command);
			}
			else{
 				merge.LetterMergeNum=General.NonQ(command,true);
			}
		}

		///<summary></summary>
		public static void Update(LetterMerge merge){
			string command="UPDATE lettermerge SET "
				+"Description = '"   +POut.PString(merge.Description)+"' "
				+",TemplateName = '" +POut.PString(merge.TemplateName)+"' "
				+",DataFileName = '" +POut.PString(merge.DataFileName)+"' "
				+",Category = '"     +POut.PInt   (merge.Category)+"' "
				+"WHERE LetterMergeNum = '"+POut.PInt(merge.LetterMergeNum)+"'";
 			General.NonQ(command);
		}

		///<summary></summary>
		public static void Delete(LetterMerge merge){
			string command="DELETE FROM lettermerge "
				+"WHERE LetterMergeNum = "+POut.PInt(merge.LetterMergeNum);
			General.NonQ(command);
		}

		///<summary>Supply the index of the cat within DefB.Short.</summary>
		public static LetterMerge[] GetListForCat(int catIndex){
			ArrayList AL=new ArrayList();
			for(int i=0;i<List.Length;i++){
				if(List[i].Category==DefB.Short[(int)DefCat.LetterMergeCats][catIndex].DefNum){
					AL.Add(List[i]);
				}
			}
			LetterMerge[] retVal=new LetterMerge[AL.Count];
			for(int i=0;i<AL.Count;i++){
				retVal[i]=(LetterMerge)AL[i];
			}
			return retVal;
		}

	
		


	}

	



}









