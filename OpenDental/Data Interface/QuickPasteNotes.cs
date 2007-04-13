using System;
using System.Collections;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	///<summary></summary>
	public class QuickPasteNotes {
		///<summary>list of all notes for all categories. Not very useful.</summary>
		private static QuickPasteNote[] List;

		///<summary></summary>
		public static void Refresh() {
			string command=
				"SELECT * from quickpastenote "
				+"ORDER BY ItemOrder";
			DataTable table=General.GetTable(command);
			List=new QuickPasteNote[table.Rows.Count];
			for(int i=0;i<List.Length;i++) {
				List[i]=new QuickPasteNote();
				List[i].QuickPasteNoteNum= PIn.PInt(table.Rows[i][0].ToString());
				List[i].QuickPasteCatNum = PIn.PInt(table.Rows[i][1].ToString());
				List[i].ItemOrder        = PIn.PInt(table.Rows[i][2].ToString());
				List[i].Note             = PIn.PString(table.Rows[i][3].ToString());
				List[i].Abbreviation     = PIn.PString(table.Rows[i][4].ToString());
			}
		}
	

		///<summary></summary>
		public static void Insert(QuickPasteNote note){
			if(PrefB.RandomKeys){
				note.QuickPasteNoteNum=MiscData.GetKey("quickpastenote","QuickPasteNoteNum");
			}
			string command= "INSERT INTO quickpastenote (";
			if(PrefB.RandomKeys){
				command+="QuickPasteNoteNum,";
			}
			command+="QuickPasteCatNum,ItemOrder,Note,Abbreviation) VALUES(";
			if(PrefB.RandomKeys){
				command+="'"+POut.PInt(note.QuickPasteNoteNum)+"', ";
			}
			command+=
				 "'"+POut.PInt   (note.QuickPasteCatNum)+"', "
				+"'"+POut.PInt   (note.ItemOrder)+"', "
				+"'"+POut.PString(note.Note)+"', "
				+"'"+POut.PString(note.Abbreviation)+"')";
 			if(PrefB.RandomKeys){
				General.NonQ(command);
			}
			else{
 				note.QuickPasteNoteNum=General.NonQ(command,true);
			}
		}

		///<summary></summary>
		public static void Update(QuickPasteNote note){
			string command="UPDATE quickpastenote SET "
				+"QuickPasteCatNum='" +POut.PInt   (note.QuickPasteCatNum)+"'"
				+",ItemOrder = '"     +POut.PInt   (note.ItemOrder)+"'"
				+",Note = '"          +POut.PString(note.Note)+"'"
				+",Abbreviation = '"  +POut.PString(note.Abbreviation)+"'"
				+" WHERE QuickPasteNoteNum = '"+POut.PInt (note.QuickPasteNoteNum)+"'";
 			General.NonQ(command);
		}

		
		///<summary></summary>
		public static void Delete(QuickPasteNote note){
			string command="DELETE from quickpastenote WHERE QuickPasteNoteNum = '"
				+POut.PInt(note.QuickPasteNoteNum)+"'";
 			General.NonQ(command);
		}

		///<summary>When saving an abbrev, this makes sure that the abbreviation is not already in use.</summary>
		public static bool AbbrAlreadyInUse(QuickPasteNote note){
			string command="SELECT * FROM quickpastenote WHERE "
				+"Abbreviation='"+POut.PString(note.Abbreviation)+"' "
				+"AND QuickPasteNoteNum != '"+POut.PInt (note.QuickPasteNoteNum)+"'";
 			DataTable table=General.GetTable(command);
			if(table.Rows.Count==0){
				return false;
			}
			return true;
		}




	

	

		///<summary>Only used from FormQuickPaste to get all notes for the selected cat.</summary>
		public static QuickPasteNote[] GetForCat(int cat){
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









