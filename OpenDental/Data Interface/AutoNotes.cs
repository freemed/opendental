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

		public static void Insert(string autoNoteName,Array controlsToInc,int ArraySize) {
		string controlsToIncText="";
		for (int i=0; i<ArraySize; i++) {
			controlsToIncText = controlsToIncText + controlsToInc.GetValue(i).ToString()+"|";
		}     
		string command = "INSERT INTO autonote (AutoNoteNum,AutoNoteName, ControlsToInc)"
			+"VALUES ("			
		    +"'DEFAULT'," 
			+"'"+POut.PString(autoNoteName)+"'," 
			+"'"+POut.PString(controlsToIncText)+"')";
		General.NonQ(command);
		}		

		public static void AutoNoteUpdate(string AutoNoteToUpdate, string AutoNoteName, Array ControlsToInc, int ArraySize) { 
		string controlsToIncText="";
		for (int i=0; i<ArraySize; i++) {
			controlsToIncText = controlsToIncText + ControlsToInc.GetValue(i).ToString()+"|";
		}     
			string command="UPDATE autonote "
			+"SET AutoNoteName = '"+POut.PString(AutoNoteName)+"', "
			+"ControlsToInc = '"+POut.PString(controlsToIncText)+"', "
			+"WHERE AutoNoteName = '"+POut.PString(AutoNoteToUpdate)+"'";
			General.NonQ(command);
		}

		/// <summary>
		/// Used when there is an Auto Note to edit
		/// </summary>
		/// <param name="AutoNoteName"></param>
		/// The name of the Auto Note to edit
		public static List<AutoNote> AutoNoteEdit(string AutoNoteName) { 
		string command="SELECT AutoNoteName, AutoNoteNum, ControlsToInc FROM autonote "
			+"WHERE AutoNoteName = "+"'"+AutoNoteName+"'";
			DataTable table=General.GetTable(command);
			List<AutoNote> Listt=new List<AutoNote>();
			//List = new AutoNote[table.Rows.Count];
			AutoNote note;			
				note = new AutoNote();
				note.AutoNoteNum=PIn.PInt(table.Rows[0]["AutoNoteNum"].ToString());
				note.AutoNoteName=PIn.PString(table.Rows[0]["AutoNoteName"].ToString());
				note.ControlsToInc=PIn.PString(table.Rows[0]["ControlsToInc"].ToString());
				Listt.Add(note);			
			return Listt;
		}
	}
}
