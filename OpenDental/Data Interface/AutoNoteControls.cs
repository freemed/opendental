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
			string command = "SELECT * FROM autonotecontrol";
			DataTable table = General.GetTable(command);
			List<AutoNoteControl> Listt=new List<AutoNoteControl>();
			//List = new AutoNote[table.Rows.Count];
			AutoNoteControl noteCont;
			for (int i = 0; i < table.Rows.Count; i++) {
				noteCont = new AutoNoteControl();
				noteCont.AutoNoteControlNum = PIn.PInt(table.Rows[i][0].ToString());
				noteCont.Descript = PIn.PString(table.Rows[i][1].ToString());
				noteCont.ControlType = PIn.PString(table.Rows[i][2].ToString());
				noteCont.ControlOptions = PIn.PString(table.Rows[i][3].ToString());
				Listt.Add(noteCont);
			}
			return Listt;
		}
	}
}
