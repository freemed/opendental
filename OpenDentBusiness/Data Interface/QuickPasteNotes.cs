using System;
using System.Collections;
using System.Data;
using System.Reflection;
using System.Text.RegularExpressions;

namespace OpenDentBusiness{
	///<summary></summary>
	public class QuickPasteNotes {
		///<summary>list of all notes for all categories. Not very useful.</summary>
		private static QuickPasteNote[] List;

		public static DataTable RefreshCache() {
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string command=
				"SELECT * from quickpastenote "
				+"ORDER BY ItemOrder";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="QuickPasteNote";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table) {
			//No need to check RemotingRole; no call to db.
			List=new QuickPasteNote[table.Rows.Count];
			for(int i=0;i<List.Length;i++) {
				List[i]=new QuickPasteNote();
				List[i].QuickPasteNoteNum=PIn.PInt(table.Rows[i][0].ToString());
				List[i].QuickPasteCatNum=PIn.PInt(table.Rows[i][1].ToString());
				List[i].ItemOrder=PIn.PInt32(table.Rows[i][2].ToString());
				List[i].Note=PIn.PString(table.Rows[i][3].ToString());
				List[i].Abbreviation=PIn.PString(table.Rows[i][4].ToString());
			}
		}

		///<summary></summary>
		public static long Insert(QuickPasteNote note) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				note.QuickPasteNoteNum=Meth.GetInt(MethodBase.GetCurrentMethod(),note);
				return note.QuickPasteNoteNum;
			}
			if(PrefC.RandomKeys){
				note.QuickPasteNoteNum=MiscData.GetKey("quickpastenote","QuickPasteNoteNum");
			}
			string command= "INSERT INTO quickpastenote (";
			if(PrefC.RandomKeys){
				command+="QuickPasteNoteNum,";
			}
			command+="QuickPasteCatNum,ItemOrder,Note,Abbreviation) VALUES(";
			if(PrefC.RandomKeys){
				command+="'"+POut.PInt(note.QuickPasteNoteNum)+"', ";
			}
			command+=
				 "'"+POut.PInt   (note.QuickPasteCatNum)+"', "
				+"'"+POut.PInt   (note.ItemOrder)+"', "
				+"'"+POut.PString(note.Note)+"', "
				+"'"+POut.PString(note.Abbreviation)+"')";
 			if(PrefC.RandomKeys){
				Db.NonQ(command);
			}
			else{
 				note.QuickPasteNoteNum=Db.NonQ(command,true);
			}
			return note.QuickPasteNoteNum;
		}

		///<summary></summary>
		public static void Update(QuickPasteNote note){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),note);
				return;
			}
			string command="UPDATE quickpastenote SET "
				+"QuickPasteCatNum='" +POut.PInt   (note.QuickPasteCatNum)+"'"
				+",ItemOrder = '"     +POut.PInt   (note.ItemOrder)+"'"
				+",Note = '"          +POut.PString(note.Note)+"'"
				+",Abbreviation = '"  +POut.PString(note.Abbreviation)+"'"
				+" WHERE QuickPasteNoteNum = '"+POut.PInt (note.QuickPasteNoteNum)+"'";
 			Db.NonQ(command);
		}
		
		///<summary></summary>
		public static void Delete(QuickPasteNote note){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),note);
				return;
			}
			string command="DELETE from quickpastenote WHERE QuickPasteNoteNum = '"
				+POut.PInt(note.QuickPasteNoteNum)+"'";
 			Db.NonQ(command);
		}

		///<summary>When saving an abbrev, this makes sure that the abbreviation is not already in use.</summary>
		public static bool AbbrAlreadyInUse(QuickPasteNote note){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetBool(MethodBase.GetCurrentMethod(),note);
			}
			string command="SELECT * FROM quickpastenote WHERE "
				+"Abbreviation='"+POut.PString(note.Abbreviation)+"' "
				+"AND QuickPasteNoteNum != '"+POut.PInt (note.QuickPasteNoteNum)+"'";
 			DataTable table=Db.GetTable(command);
			if(table.Rows.Count==0){
				return false;
			}
			return true;
		}

		///<summary>Only used from FormQuickPaste to get all notes for the selected cat.</summary>
		public static QuickPasteNote[] GetForCat(int cat){
			//No need to check RemotingRole; no call to db.
			if(List==null) {
				RefreshCache();
			}
			ArrayList ALnotes=new ArrayList();
			for(int i=0;i<List.Length;i++){
				if(List[i].QuickPasteCatNum==cat){
					ALnotes.Add(List[i]);
				}
			}
			QuickPasteNote[] retArray=new QuickPasteNote[ALnotes.Count];
			for(int i=0;i<ALnotes.Count;i++){
				retArray[i]=(QuickPasteNote)ALnotes[i];
			}
			return retArray;
		}

		///<summary>Called on KeyUp from various textBoxes in the program to look for a ?abbrev and attempt to substitute.  Substitutes the text if found.</summary>
		public static string Substitute(string text,QuickPasteType type){
			//No need to check RemotingRole; no call to db.
			if(List==null) {
				RefreshCache();
			}
			int typeIndex=QuickPasteCats.GetDefaultType(type);
			for(int i=0;i<List.Length;i++){
				if(List[i].Abbreviation==""){
					continue;
				}
				if(List[i].QuickPasteCatNum!=QuickPasteCats.List[typeIndex].QuickPasteCatNum){
					continue;
				}
				text=Regex.Replace(text,@"\?"+List[i].Abbreviation,List[i].Note);
			}
			return text;
		}


		


		


	}

	


}









