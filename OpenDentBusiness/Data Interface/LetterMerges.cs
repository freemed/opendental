using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace OpenDentBusiness{
	///<summary></summary>
	public class LetterMerges {
		///<summary>List of all lettermerges.</summary>
		private static LetterMerge[] list;

		public static LetterMerge[] Listt {
			//No need to check RemotingRole; no call to db.
			get {
				if(list==null) {
					RefreshCache();
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
			//No need to check RemotingRole; no call to db.
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

		public static DataTable RefreshCache() {
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string command=
				"SELECT * FROM lettermerge ORDER BY Description";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="LetterMerge";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table) {
			//No need to check RemotingRole; no call to db.
			Listt=new LetterMerge[table.Rows.Count];
			for(int i=0;i<table.Rows.Count;i++) {
				Listt[i]=new LetterMerge();
				Listt[i].LetterMergeNum=PIn.PInt(table.Rows[i][0].ToString());
				Listt[i].Description=PIn.PString(table.Rows[i][1].ToString());
				Listt[i].TemplateName=PIn.PString(table.Rows[i][2].ToString());
				Listt[i].DataFileName=PIn.PString(table.Rows[i][3].ToString());
				Listt[i].Category=PIn.PInt(table.Rows[i][4].ToString());
				Listt[i].Fields=LetterMergeFields.GetForLetter(Listt[i].LetterMergeNum);
			}
		}

		///<summary>Inserts this lettermerge into database.</summary>
		public static long Insert(LetterMerge merge) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				merge.LetterMergeNum=Meth.GetInt(MethodBase.GetCurrentMethod(),merge);
				return merge.LetterMergeNum;
			}
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
			return merge.LetterMergeNum;
		}

		///<summary></summary>
		public static void Update(LetterMerge merge){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),merge);
				return;
			}
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
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),merge);
				return;
			}
			string command="DELETE FROM lettermerge "
				+"WHERE LetterMergeNum = "+POut.PInt(merge.LetterMergeNum);
			Db.NonQ(command);
		}

		///<summary>Supply the index of the cat within DefC.Short.</summary>
		public static List<LetterMerge> GetListForCat(int catIndex){
			//No need to check RemotingRole; no call to db.
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









