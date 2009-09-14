using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Reflection;

namespace OpenDentBusiness {
	public class AutoNotes {
		///<summary>A list of all Auto Notes.  Caching could be handled better for fewer refreshes.</summary>
		public static List<AutoNote> Listt;

		///<summary></summary>
		public static DataTable RefreshCache() {
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string command="SELECT * FROM autonote ORDER BY AutoNoteName";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="AutoNote";
			FillCache(table);
			return table;
		}

		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			Listt=new List<AutoNote>();
			AutoNote note;
			for(int i=0;i<table.Rows.Count;i++){
				note = new AutoNote();
				note.AutoNoteNum = PIn.PLong(table.Rows[i][0].ToString());
				note.AutoNoteName = PIn.PString(table.Rows[i][1].ToString());
				note.MainText = PIn.PString(table.Rows[i][2].ToString());
				Listt.Add(note);
			}
		}

		///<summary></summary>
		public static long Insert(AutoNote autonote) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				autonote.AutoNoteNum=Meth.GetInt(MethodBase.GetCurrentMethod(),autonote);
				return autonote.AutoNoteNum;
			}
			if(PrefC.RandomKeys) {
				autonote.AutoNoteNum=ReplicationServers.GetKey("autonote","AutoNoteNum");
			}
			string command="INSERT INTO autonote (";
			if(PrefC.RandomKeys) {
				command+="AutoNoteNum,";
			}
			command+="AutoNoteName, MainText) VALUES(";
			if(PrefC.RandomKeys) {
				command+=POut.PLong(autonote.AutoNoteNum)+", ";
			}
			command+=			
				 "'"+POut.PString(autonote.AutoNoteName)+"'," 
				+"'"+POut.PString(autonote.MainText)+"')";
			if(PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				autonote.AutoNoteNum=Db.NonQ(command,true);
			}
			return autonote.AutoNoteNum;
		}

		///<summary></summary>
		public static void Update(AutoNote autonote) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),autonote);
				return;
			}
			string command="UPDATE autonote SET "
				+"AutoNoteName = '"+POut.PString(autonote.AutoNoteName)+"', "
				+"MainText = '"+POut.PString(autonote.MainText)+"' "
				+"WHERE AutoNoteNum = '"+POut.PLong(autonote.AutoNoteNum)+"'";
			Db.NonQ(command);
		}

		///<summary></summary>
		public static void Delete(long autoNoteNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),autoNoteNum);
				return;
			}
			string command="DELETE FROM autonote "
				+"WHERE AutoNoteNum = "+POut.PLong(autoNoteNum);
			Db.NonQ(command);
		}

		/*
		public static bool AutoNoteNameUsed(string AutoNoteName, string OriginalAutoNoteName) {
			string command="SELECT AutoNoteName FROM autonote WHERE "
			+"AutoNoteName = '"+AutoNoteName+"'"+" AND AutoNoteName != '"+OriginalAutoNoteName+"'";
			DataTable table=Db.GetTable(command);
			bool IsUsed=false;
			if (table.Rows.Count!=0) {//found duplicate control name				
				IsUsed=true;
			}
			return IsUsed;
		}*/

		/*
		/// <summary></summary>
		public static List<AutoNote> AutoNoteEdit(string AutoNoteName) { 
			string command="SELECT AutoNoteName, AutoNoteNum, ControlsToInc FROM autonote "
				+"WHERE AutoNoteName = "+"'"+AutoNoteName+"'";
			DataTable table=Db.GetTable(command);
			List<AutoNote> Listt=new List<AutoNote>();
			//List = new AutoNote[table.Rows.Count];
			AutoNote note= new AutoNote();
			note.AutoNoteNum=PIn.PInt(table.Rows[0]["AutoNoteNum"].ToString());
			note.AutoNoteName=PIn.PString(table.Rows[0]["AutoNoteName"].ToString());
			note.ControlsToInc=PIn.PString(table.Rows[0]["ControlsToInc"].ToString());
			Listt.Add(note);			
			return Listt;
		}*/
	}
}
