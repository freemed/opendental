using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using OpenDentBusiness;
using System.Text.RegularExpressions;

namespace OpenDental{
	///<summary></summary>
	public class ProcCodeNotes{
		///<summary>All notes for all procedurecodes.</summary>
		public static List<ProcCodeNote> Listt;

		public static void Refresh(){
			string command="SELECT * FROM proccodenote";
			Listt=RefreshAndFill(command);
		}

		///<summary></summary>
		public static List<ProcCodeNote> GetList(int codeNum) {
			string command="SELECT * FROM proccodenote WHERE CodeNum="+POut.PInt(codeNum);
			return RefreshAndFill(command);
		}

		private static List<ProcCodeNote> RefreshAndFill(string command){
			DataTable table=General.GetTable(command);
			List<ProcCodeNote> retVal=new List<ProcCodeNote>();
			ProcCodeNote note;
			for(int i=0;i<table.Rows.Count;i++) {
				note=new ProcCodeNote();
				note.ProcCodeNoteNum=PIn.PInt(table.Rows[i][0].ToString());
				note.CodeNum      	=PIn.PInt(table.Rows[i][1].ToString());
				note.ProvNum        =PIn.PInt(table.Rows[i][2].ToString());
				note.Note           =PIn.PString(table.Rows[i][3].ToString());
				note.ProcTime       =PIn.PString(table.Rows[i][4].ToString());
				retVal.Add(note);
			}
			return retVal;
		}

		///<summary></summary>
		public static void Insert(ProcCodeNote note){
			string command="INSERT INTO proccodenote (CodeNum,ProvNum,Note,ProcTime) VALUES("
				+"'"+POut.PInt   (note.CodeNum)+"', "
				+"'"+POut.PInt   (note.ProvNum)+"', "
				+"'"+POut.PString(note.Note)+"', "
				+"'"+POut.PString(note.ProcTime)+"')";
			note.ProcCodeNoteNum=General.NonQ(command,true);
		}

		///<summary></summary>
		public static void Update(ProcCodeNote note){
			string command="UPDATE proccodenote SET " 
				+ "CodeNum = '"    +POut.PInt   (note.CodeNum)+"'"
				+ ",ProvNum = '"   +POut.PInt   (note.ProvNum)+"'"
				+ ",Note = '"      +POut.PString(note.Note)+"'"
				+ ",ProcTime = '"  +POut.PString(note.ProcTime)+"'"
				+" WHERE ProcCodeNoteNum = "+POut.PInt(note.ProcCodeNoteNum);
			General.NonQ(command);
		}

		public static void Delete(int procCodeNoteNum){
			string command="DELETE FROM proccodenote WHERE ProcCodeNoteNum = "+POut.PInt(procCodeNoteNum);
			General.NonQ(command);
		}

		///<summary>Gets the note for the given provider, if one exists.  Otherwise, gets the proccode.defaultnote.</summary>
		public static string GetNote(int provNum,int codeNum){
			for(int i=0;i<Listt.Count;i++){
				if(Listt[i].ProvNum!=provNum){
					continue;
				}
				if(Listt[i].CodeNum!=codeNum){
					continue;
				}
				return Listt[i].Note;
			}
			return ProcedureCodes.GetProcCode(codeNum).DefaultNote;
		}

		///<summary>Gets the time pattern for the given provider, if one exists.  Otherwise, gets the proccode.ProcTime.</summary>
		public static string GetTimePattern(int provNum,int codeNum) {
			for(int i=0;i<Listt.Count;i++) {
				if(Listt[i].ProvNum!=provNum) {
					continue;
				}
				if(Listt[i].CodeNum!=codeNum) {
					continue;
				}
				return Listt[i].ProcTime;
			}
			return ProcedureCodes.GetProcCode(codeNum).ProcTime;
		}



	}

	
	
	


}