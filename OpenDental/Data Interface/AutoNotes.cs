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

		///<summary>This is wrong.  It needs to be reworked to be just like insert in the other similar classes.  The only passed parameter should be the AutoNote.</summary>
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

		///<summary>This is wrong.  It needs to be reworked to be just like insert in the other similar classes.  The only passed parameter should be the AutoNote.</summary>
		public static void Update(string AutoNoteToUpdate,string AutoNoteName,Array ControlsToInc,int ArraySize) {
			string controlsToIncText="";
			for(int i=0;i<ArraySize;i++) {
				controlsToIncText = controlsToIncText + ControlsToInc.GetValue(i).ToString()+"|";
			}
			string command="UPDATE autonote "
			+"SET AutoNoteName = '"+POut.PString(AutoNoteName)+"', "
			+"ControlsToInc = '"+POut.PString(controlsToIncText)+"' "
			+"WHERE AutoNoteName = '"+POut.PString(AutoNoteToUpdate)+"'";
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
