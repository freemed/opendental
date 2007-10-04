using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public class AutoNotes {
		///<summary>A list of all Auto Notes</summary>
		public static List<AutoNote> Listt;
		/// <summary>This is what is used to store the output of the Auto Note</summary>
		public string autoNoteOutput;

		public static void Refresh() {
			string command = "SELECT * FROM autonote ORDER BY AutoNoteNum";
			DataTable table = General.GetTable(command);
			Listt=new List<AutoNote>();
			AutoNote note;
			for(int i=0;i<table.Rows.Count;i++){
				note = new AutoNote();
				note.AutoNoteNum = PIn.PInt(table.Rows[i][0].ToString());
				note.AutoNoteName = PIn.PString(table.Rows[i][1].ToString());
				note.ControlsToInc = PIn.PString(table.Rows[i][2].ToString());
				Listt.Add(note);
			}
		}

		///<summary></summary>
		public static void Insert(AutoNote autonote) {		
		string command = "INSERT INTO autonote (AutoNoteName, ControlsToInc)"
			+"VALUES ("			
			+"'"+POut.PString(autonote.AutoNoteName)+"'," 
			+"'"+POut.PString(autonote.ControlsToInc)+"')";
		General.NonQ(command);
		}

		///<summary></summary>
		public static void Update(AutoNote autonote) {			
			string command="UPDATE autonote SET "
			+"AutoNoteName = '"+POut.PString(autonote.AutoNoteName)+"', "
			+"ControlsToInc = '"+POut.PString(autonote.ControlsToInc)+"' "
			+"WHERE AutoNoteNum = '"+POut.PInt(autonote.AutoNoteNum)+"'";
			General.NonQ(command);
		}

		public static bool AutoNoteNameUsed(string AutoNoteName, string OriginalAutoNoteName) {
			string command="SELECT AutoNoteName FROM autonote WHERE "
			+"AutoNoteName = '"+AutoNoteName+"'"+" AND AutoNoteName != '"+OriginalAutoNoteName+"'";
			DataTable table=General.GetTable(command);
			bool IsUsed=false;
			if (table.Rows.Count!=0) {//found duplicate control name				
				IsUsed=true;
			}
			return IsUsed;
		}

		

		/// <summary></summary>
		public static List<AutoNote> AutoNoteEdit(string AutoNoteName) { 
			string command="SELECT AutoNoteName, AutoNoteNum, ControlsToInc FROM autonote "
				+"WHERE AutoNoteName = "+"'"+AutoNoteName+"'";
			DataTable table=General.GetTable(command);
			List<AutoNote> Listt=new List<AutoNote>();
			//List = new AutoNote[table.Rows.Count];
			AutoNote note= new AutoNote();
			note.AutoNoteNum=PIn.PInt(table.Rows[0]["AutoNoteNum"].ToString());
			note.AutoNoteName=PIn.PString(table.Rows[0]["AutoNoteName"].ToString());
			note.ControlsToInc=PIn.PString(table.Rows[0]["ControlsToInc"].ToString());
			Listt.Add(note);			
			return Listt;
		}
	}
}
