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
		//<summary>A list of all Auto Notes</summary>
		//public static List<AutoNote> Listt;

		public static List<AutoNote> Refresh() {
			string command = "SELECT * FROM autonote";
			DataTable table = General.GetTable(command);
			List<AutoNote> Listt=new List<AutoNote>();
			//List = new AutoNote[table.Rows.Count];
			AutoNote note;
			for(int i=0;i<table.Rows.Count;i++){
				note = new AutoNote();
				note.AutoNoteNum = PIn.PInt(table.Rows[i][0].ToString());
				note.AutoNoteName = PIn.PString(table.Rows[i][1].ToString());
				note.ControlsToInc = PIn.PString(table.Rows[i][2].ToString());
				Listt.Add(note);
			}
			return Listt;
		}		
	}
}
