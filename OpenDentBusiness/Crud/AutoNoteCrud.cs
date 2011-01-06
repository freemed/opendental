//This file is automatically generated.
//Do not attempt to make changes to this file because the changes will be erased and overwritten.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;

namespace OpenDentBusiness.Crud{
	internal class AutoNoteCrud {
		///<summary>Gets one AutoNote object from the database using the primary key.  Returns null if not found.</summary>
		internal static AutoNote SelectOne(long autoNoteNum){
			string command="SELECT * FROM autonote "
				+"WHERE AutoNoteNum = "+POut.Long(autoNoteNum);
			List<AutoNote> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets one AutoNote object from the database using a query.</summary>
		internal static AutoNote SelectOne(string command){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<AutoNote> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets a list of AutoNote objects from the database using a query.</summary>
		internal static List<AutoNote> SelectMany(string command){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<AutoNote> list=TableToList(Db.GetTable(command));
			return list;
		}

		///<summary>Converts a DataTable to a list of objects.</summary>
		internal static List<AutoNote> TableToList(DataTable table){
			List<AutoNote> retVal=new List<AutoNote>();
			AutoNote autoNote;
			for(int i=0;i<table.Rows.Count;i++) {
				autoNote=new AutoNote();
				autoNote.AutoNoteNum = PIn.Long  (table.Rows[i]["AutoNoteNum"].ToString());
				autoNote.AutoNoteName= PIn.String(table.Rows[i]["AutoNoteName"].ToString());
				autoNote.MainText    = PIn.String(table.Rows[i]["MainText"].ToString());
				retVal.Add(autoNote);
			}
			return retVal;
		}

		///<summary>Inserts one AutoNote into the database.  Returns the new priKey.</summary>
		internal static long Insert(AutoNote autoNote){
			return Insert(autoNote,false);
		}

		///<summary>Inserts one AutoNote into the database.  Provides option to use the existing priKey.</summary>
		internal static long Insert(AutoNote autoNote,bool useExistingPK){
			if(!useExistingPK && PrefC.RandomKeys) {
				autoNote.AutoNoteNum=ReplicationServers.GetKey("autonote","AutoNoteNum");
			}
			string command="INSERT INTO autonote (";
			if(useExistingPK || PrefC.RandomKeys) {
				command+="AutoNoteNum,";
			}
			command+="AutoNoteName,MainText) VALUES(";
			if(useExistingPK || PrefC.RandomKeys) {
				command+=POut.Long(autoNote.AutoNoteNum)+",";
			}
			command+=
				 "'"+POut.String(autoNote.AutoNoteName)+"',"
				+"'"+POut.String(autoNote.MainText)+"')";
			if(useExistingPK || PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				autoNote.AutoNoteNum=Db.NonQ(command,true);
			}
			return autoNote.AutoNoteNum;
		}

		///<summary>Updates one AutoNote in the database.</summary>
		internal static void Update(AutoNote autoNote){
			string command="UPDATE autonote SET "
				+"AutoNoteName= '"+POut.String(autoNote.AutoNoteName)+"', "
				+"MainText    = '"+POut.String(autoNote.MainText)+"' "
				+"WHERE AutoNoteNum = "+POut.Long(autoNote.AutoNoteNum);
			Db.NonQ(command);
		}

		///<summary>Updates one AutoNote in the database.  Uses an old object to compare to, and only alters changed fields.  This prevents collisions and concurrency problems in heavily used tables.</summary>
		internal static void Update(AutoNote autoNote,AutoNote oldAutoNote){
			string command="";
			if(autoNote.AutoNoteName != oldAutoNote.AutoNoteName) {
				if(command!=""){ command+=",";}
				command+="AutoNoteName = '"+POut.String(autoNote.AutoNoteName)+"'";
			}
			if(autoNote.MainText != oldAutoNote.MainText) {
				if(command!=""){ command+=",";}
				command+="MainText = '"+POut.String(autoNote.MainText)+"'";
			}
			if(command==""){
				return;
			}
			command="UPDATE autonote SET "+command
				+" WHERE AutoNoteNum = "+POut.Long(autoNote.AutoNoteNum);
			Db.NonQ(command);
		}

		///<summary>Deletes one AutoNote from the database.</summary>
		internal static void Delete(long autoNoteNum){
			string command="DELETE FROM autonote "
				+"WHERE AutoNoteNum = "+POut.Long(autoNoteNum);
			Db.NonQ(command);
		}

	}
}