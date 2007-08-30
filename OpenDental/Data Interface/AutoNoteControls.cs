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

		/// <summary>A list with all the control settings</summary>
		public static List<AutoNoteControl> Refresh() {
			string command = "SELECT * FROM autonotecontrol ";
			DataTable table = General.GetTable(command);
			List<AutoNoteControl> Listt=new List<AutoNoteControl>();
			//List = new AutoNote[table.Rows.Count];
			AutoNoteControl noteCont;
			for (int i = 0; i < table.Rows.Count; i++) {
				noteCont = new AutoNoteControl();
				noteCont.AutoNoteControlNum = PIn.PInt(table.Rows[i][0].ToString());
				noteCont.Descript = PIn.PString(table.Rows[i]["Descript"].ToString());
				noteCont.ControlType = PIn.PString(table.Rows[i]["ControlType"].ToString());
				noteCont.MultiLineText = PIn.PString(table.Rows[i]["MultiLineText"].ToString());
				noteCont.ControlOptions = PIn.PString(table.Rows[i]["ControlOptions"].ToString());
				Listt.Add(noteCont);
			}
			return Listt;
		}

		/// <summary>
		/// Returns all the control info about the selected control
		/// </summary>
		/// <param name="ControlToShow"></param>
		/// The control info to return
		/// <returns></returns>
		public static List<AutoNoteControl> RefreshControlEdit(string ControlToShow) {
			string command = "SELECT * FROM autonotecontrol "
				+"WHERE descript = "+"'"+ControlToShow+"'";
			DataTable table = General.GetTable(command);
			List<AutoNoteControl> Listt=new List<AutoNoteControl>();
			//List = new AutoNote[table.Rows.Count];
			AutoNoteControl noteCont;
			for (int i = 0; i < table.Rows.Count; i++) {
				noteCont = new AutoNoteControl();
				noteCont.AutoNoteControlNum = PIn.PInt(table.Rows[i][0].ToString());
				noteCont.Descript = PIn.PString(table.Rows[i]["Descript"].ToString());
				noteCont.ControlType = PIn.PString(table.Rows[i]["ControlType"].ToString());
				noteCont.ControlLabel = PIn.PString(table.Rows[i]["ControlLabel"].ToString());
				noteCont.PrefaceText = PIn.PString(table.Rows[i]["PrefaceText"].ToString());
				noteCont.MultiLineText = PIn.PString(table.Rows[i]["MultiLineText"].ToString());
				noteCont.ControlOptions = PIn.PString(table.Rows[i]["ControlOptions"].ToString());
				Listt.Add(noteCont);
			}
			return Listt;
		}

		/// <summary>
		/// Converts the Name of the control to its Num.
		/// </summary>
		/// <param name="ControlName"></param>
		/// <returns></returns>
		public static List<AutoNoteControl> ControlNameToNum(string ControlName) {
			string command="SELECT AutoNoteControlNum FROM autonotecontrol "
			+"WHERE Descript = "+"'"+ControlName+"'";
			DataTable table=General.GetTable(command);
			List<AutoNoteControl> Listt=new List<AutoNoteControl>();
			AutoNoteControl note;
			note = new AutoNoteControl();
			note.AutoNoteControlNum=PIn.PInt(table.Rows[0]["AutoNoteControlNum"].ToString());
			Listt.Add(note);
			return Listt;
		}

		/// <summary>
		/// Converts the Num of the control to its Name.
		/// </summary>		
		/// <returns></returns>
		public static List<AutoNoteControl> ControlNumToName(string controlNum) {
			string command="SELECT Descript FROM autonotecontrol "
			+"WHERE AutoNoteControlNum = "+"'"+controlNum+"'";
			DataTable table=General.GetTable(command);
			List<AutoNoteControl> Listt=new List<AutoNoteControl>();
			AutoNoteControl note;
			note = new AutoNoteControl();
			note.Descript=PIn.PString(table.Rows[0]["Descript"].ToString());
			Listt.Add(note);
			return Listt;
		}

		public static void Insert(string controlType, string controlName, string controlLabel, string Prefacetext, string MultiLinetext, Array controlOptions, int arraySize) {
			string controlOptionsString=null;
			for (int i=0; i<arraySize; i++) {
				controlOptionsString = controlOptionsString + controlOptions.GetValue(i).ToString()+"|";
			}
			string command = "INSERT INTO autonotecontrol (AutoNoteControlNum,Descript,ControlType,ControlLabel,PrefaceText,MultiLineText,ControlOptions)"
			+"VALUES ("			
			+"'DEFAULT', " 
			+"'"+POut.PString(controlName)+"', " 
			+"'"+POut.PString(controlType)+"', "
			+"'"+POut.PString(controlLabel)+"' ,"			
			+"'"+POut.PString(Prefacetext)+"', "
			+"'"+POut.PString(MultiLinetext)+"', "
			+"'"+POut.PString(controlOptionsString)+"')";
			General.NonQ(command);
		}

		/// <summary>
		/// Used to save the changes to the Control to the database
		/// </summary>
		/// <param name="ControlToUpdate"></param>
		/// The Name of the control to Update
		/// <param name="ControlType"></param> 
		/// <param name="Descripept"></param>
		/// <param name="Label"></param>
		/// <param name="Preface"></param>
		/// <param name="MultiLineText"></param>
		/// <param name="ControlOptions"></param>
		/// <param name="ArraySize"></param>
		/// The number of control options that will be added
		public static void ControlUpdate(string ControlToUpdate, string ControlType, string Descript, string Label, string Preface, string MultiLineText, Array ControlOptions, int ArraySize) {
			string controlOptions="";
			for (int x=0; x<ArraySize; x++) {
				controlOptions=controlOptions + ControlOptions.GetValue(x).ToString()+"|";
			}
			string command="UPDATE autonotecontrol "
            +"SET ControlType = '"+POut.PString(ControlType)+"', "
			+"Descript = '"+POut.PString(Descript)+"', "
			+"ControlLabel = '"+POut.PString(Label)+"', "
			+"PrefaceText = '"+POut.PString(Preface)+"', "
			+"MultiLineText = '"+POut.PString(MultiLineText)+"', "
			+"ControlOptions = '"+POut.PString(controlOptions)+"' "
			+"WHERE Descript = '"+POut.PString(ControlToUpdate)+"'";
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
		public static bool ControlNameUsed(string ControlName,  string OriginalControlName) {
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

