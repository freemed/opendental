using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace OpenDentBusiness{
	///<summary></summary>
	public class LetterMerges {
		///<summary>List of all lettermerges.</summary>
		private static LetterMerge[] list;

		public static LetterMerge[] Listt {
			get {
				if(list==null) {
					Refresh();
				}
				return list;
			}
			set {
				list=value;
			}
		}

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
			DataTable table=Db.GetTable(command);
			Listt=new LetterMerge[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				Listt[i]=new LetterMerge();
				Listt[i].LetterMergeNum= PIn.PInt(table.Rows[i][0].ToString());
				Listt[i].Description   = PIn.PString(table.Rows[i][1].ToString());
				Listt[i].TemplateName  = PIn.PString(table.Rows[i][2].ToString());
				Listt[i].DataFileName  = PIn.PString(table.Rows[i][3].ToString());
				Listt[i].Category      = PIn.PInt(table.Rows[i][4].ToString());
				Listt[i].Fields=LetterMergeFields.GetForLetter(Listt[i].LetterMergeNum);
			}
		}

		///<summary>Inserts this lettermerge into database.</summary>
		public static void Insert(LetterMerge merge){
			if(PrefC.RandomKeys){
				merge.LetterMergeNum=MiscData.GetKey("lettermerge","LetterMergeNum");
			}
			string command= "INSERT INTO lettermerge (";
			if(PrefC.RandomKeys){
				command+="LetterMergeNum,";
			}
			command+="Description,TemplateName,DataFileName,"
				+"Category) VALUES(";
			if(PrefC.RandomKeys){
				command+="'"+POut.PInt(merge.LetterMergeNum)+"', ";
			}
			command+=
				 "'"+POut.PString(merge.Description)+"', "
				+"'"+POut.PString(merge.TemplateName)+"', "
				+"'"+POut.PString(merge.DataFileName)+"', "
				+"'"+POut.PInt   (merge.Category)+"')";
			//MessageBox.Show(string command);
 			if(PrefC.RandomKeys){
				Db.NonQ(command);
			}
			else{
 				merge.LetterMergeNum=Db.NonQ(command,true);
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
 			Db.NonQ(command);
		}

		///<summary></summary>
		public static void Delete(LetterMerge merge){
			string command="DELETE FROM lettermerge "
				+"WHERE LetterMergeNum = "+POut.PInt(merge.LetterMergeNum);
			Db.NonQ(command);
		}

		///<summary>Supply the index of the cat within DefC.Short.</summary>
		public static List<LetterMerge> GetListForCat(int catIndex){
			List<LetterMerge> retVal=new List<LetterMerge>();
			for(int i=0;i<Listt.Length;i++){
				if(Listt[i].Category==DefC.Short[(int)DefCat.LetterMergeCats][catIndex].DefNum){
					retVal.Add(Listt[i]);
				}
			}
			return retVal;
		}

	
		


	}

	



}









