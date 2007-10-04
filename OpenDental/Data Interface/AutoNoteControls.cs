using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	class AutoNoteControls {
		/// <summary>A list of all the Controls</summary>
		public static List<AutoNoteControl> Listt;

		/// <summary>A list with all the control settings</summary>
		public static void Refresh() {
			string command = "SELECT * FROM autonotecontrol ORDER BY AutoNoteControlNum";
			DataTable table = General.GetTable(command);
			Listt=new List<AutoNoteControl>();
			//List = new AutoNote[table.Rows.Count];
			AutoNoteControl noteCont;
			for (int i = 0; i < table.Rows.Count; i++) {
				noteCont = new AutoNoteControl();
				noteCont.AutoNoteControlNum = PIn.PInt(table.Rows[i][0].ToString());
				noteCont.Descript = PIn.PString(table.Rows[i]["Descript"].ToString());
				noteCont.ControlType = PIn.PString(table.Rows[i]["ControlType"].ToString());
				noteCont.ControlLabel =PIn.PString(table.Rows[i]["ControlLabel"].ToString());
				noteCont.PrefaceText=PIn.PString(table.Rows[i]["PrefaceText"].ToString());
				noteCont.MultiLineText = PIn.PString(table.Rows[i]["MultiLineText"].ToString());
				noteCont.ControlOptions = PIn.PString(table.Rows[i]["ControlOptions"].ToString());
				Listt.Add(noteCont);
			}
		}

		/// <summary>
		/// Returns all the control info about the selected control
		/// </summary>
		/// <param name="ControlToShow"></param>
		/// The control info to return
		/// <returns></returns>
		public static void RefreshControl(string ControlNumToShow) {
			string command = "SELECT * FROM autonotecontrol "
				+"WHERE AutoNoteControlNum = "+"'"+ControlNumToShow+"'";
			DataTable table = General.GetTable(command);
			Listt=new List<AutoNoteControl>();
			//List = new AutoNote[table.Rows.Count];
			AutoNoteControl noteCont;
			noteCont = new AutoNoteControl();
			for (int i = 0; i < table.Rows.Count; i++) {
				noteCont.AutoNoteControlNum = PIn.PInt(table.Rows[i][0].ToString());
				noteCont.Descript = PIn.PString(table.Rows[i]["Descript"].ToString());
				noteCont.ControlType = PIn.PString(table.Rows[i]["ControlType"].ToString());
				noteCont.ControlLabel = PIn.PString(table.Rows[i]["ControlLabel"].ToString());
				noteCont.PrefaceText = PIn.PString(table.Rows[i]["PrefaceText"].ToString());
				noteCont.MultiLineText = PIn.PString(table.Rows[i]["MultiLineText"].ToString());
				noteCont.ControlOptions = PIn.PString(table.Rows[i]["ControlOptions"].ToString());
				Listt.Add(noteCont);
			}

		}

		/// <summary>
		/// Converts the Num of the control to it^s Name.
		/// </summary>		
		/// <returns></returns>
		public static List<AutoNoteControl> ControlNumToName(string controlNum) {
			string command="SELECT Descript FROM autonotecontrol "
			+"WHERE AutoNoteControlNum = "+"'"+controlNum+"'";
			DataTable table=General.GetTable(command);
			Listt=new List<AutoNoteControl>();
			AutoNoteControl note;
			note = new AutoNoteControl();
			for (int i = 0; i < table.Rows.Count; i++) {
				note.Descript=PIn.PString(table.Rows[0]["Descript"].ToString());
				Listt.Add(note);
			}
			return Listt;
		}

		public static void Insert(AutoNoteControl autonotecontrol) {
			string command = "INSERT INTO autonotecontrol (AutoNoteControlNum,Descript,ControlType,ControlLabel,PrefaceText,MultiLineText,ControlOptions)"
			+"VALUES ("			
			+"'DEFAULT', " 
			+"'"+POut.PString(autonotecontrol.Descript)+"', " 
			+"'"+POut.PString(autonotecontrol.ControlType)+"', "
			+"'"+POut.PString(autonotecontrol.ControlLabel)+"' ,"			
			+"'"+POut.PString(autonotecontrol.PrefaceText)+"', "
			+"'"+POut.PString(autonotecontrol.MultiLineText)+"', "
			+"'"+POut.PString(autonotecontrol.ControlOptions)+"')";
			General.NonQ(command);
		}


		public static void ControlUpdate(AutoNoteControl autonotecontrol) {
			string command="UPDATE autonotecontrol "
            +"SET ControlType = '"+POut.PString(autonotecontrol.ControlType)+"', "
			+"Descript = '"+POut.PString(autonotecontrol.Descript)+"', "
			+"ControlLabel = '"+POut.PString(autonotecontrol.ControlLabel)+"', "
			+"PrefaceText = '"+POut.PString(autonotecontrol.PrefaceText)+"', "
			+"MultiLineText = '"+POut.PString(autonotecontrol.MultiLineText)+"', "
			+"ControlOptions = '"+POut.PString(autonotecontrol.ControlOptions)+"' "
			+"WHERE AutoNoteControlNum = '"+POut.PInt(autonotecontrol.AutoNoteControlNum)+"'";
			General.NonQ(command);
		}

		/// <summary>
		/// Checks to see if the Control Name Already Exists.
		/// </summary>
		/// <param name="ControlName"></param>
		/// The control name to search for		
		/// <param name="OriginalControl"></param>
		/// If you are editing a control you would add the name of the control you are editing hear.
		/// This name will be ignored in the search. If else set to NULL
		/// <returns></returns>
		public static bool ControlNameUsed(string ControlName, string OriginalControlName) {
			string command="SELECT Descript FROM autonotecontrol WHERE "
			+"Descript = '"+ControlName+"'"+"And Descript != '"+OriginalControlName+"'";
			DataTable table=General.GetTable(command);
			bool IsUsed=false;
			if (table.Rows.Count!=0) {//found duplicate control name				
				IsUsed=true;
			}
			return IsUsed;
		}
	}
}

