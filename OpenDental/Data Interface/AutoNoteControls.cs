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
		/// Checks to see if the Control Name Already Exists.
		/// </summary>
		/// <param name="ControlName"></param>
		/// <returns></returns>
		public static bool ControlNameUsed(string ControlName) {
			string command="SELECT Descript FROM autonotecontrol WHERE "
			+"Descript = '"+ControlName+"'";
			DataTable table=General.GetTable(command);
			bool IsUsed=false;
			if (table.Rows.Count!=0) {//found duplicate control name
				IsUsed=true;
			}
			return IsUsed;
		}
	}
	}

