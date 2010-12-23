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
			List=Crud.QuickPasteNoteCrud.TableToList(table).ToArray();
		}

		///<summary></summary>
		public static long Insert(QuickPasteNote note) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				note.QuickPasteNoteNum=Meth.GetLong(MethodBase.GetCurrentMethod(),note);
				return note.QuickPasteNoteNum;
			}
			return Crud.QuickPasteNoteCrud.Insert(note);
		}

		///<summary></summary>
		public static void Update(QuickPasteNote note){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),note);
				return;
			}
			Crud.QuickPasteNoteCrud.Update(note);
		}
		
		///<summary></summary>
		public static void Delete(QuickPasteNote note){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),note);
				return;
			}
			string command="DELETE from quickpastenote WHERE QuickPasteNoteNum = '"
				+POut.Long(note.QuickPasteNoteNum)+"'";
 			Db.NonQ(command);
		}

		///<summary>When saving an abbrev, this makes sure that the abbreviation is not already in use.</summary>
		public static bool AbbrAlreadyInUse(QuickPasteNote note){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetBool(MethodBase.GetCurrentMethod(),note);
			}
			string command="SELECT * FROM quickpastenote WHERE "
				+"Abbreviation='"+POut.String(note.Abbreviation)+"' "
				+"AND QuickPasteNoteNum != '"+POut.Long (note.QuickPasteNoteNum)+"'";
 			DataTable table=Db.GetTable(command);
			if(table.Rows.Count==0){
				return false;
			}
			return true;
		}

		///<summary>Only used from FormQuickPaste to get all notes for the selected cat.</summary>
		public static QuickPasteNote[] GetForCat(long cat) {
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









