using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Text.RegularExpressions;

namespace OpenDentBusiness{
	///<summary></summary>
	public class ProcCodeNotes{
		///<summary>All notes for all procedurecodes.</summary>
		private static List<ProcCodeNote> list;

		public static List<ProcCodeNote> Listt {
			//No need to check RemotingRole; no call to db.
			get {
				if(list==null) {
					RefreshCache();
				}
				return list;
			}
			set {
				list=value;
			}
		}

		public static DataTable RefreshCache() {
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string command="SELECT * FROM proccodenote";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="ProcCodeNote";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table) {
			//No need to check RemotingRole; no call to db.
			list=new List<ProcCodeNote>();
			for(int i=0;i<table.Rows.Count;i++) {
				ProcCodeNote note=new ProcCodeNote();
				note.ProcCodeNoteNum=PIn.PInt(table.Rows[i][0].ToString());
				note.CodeNum=PIn.PInt(table.Rows[i][1].ToString());
				note.ProvNum=PIn.PInt(table.Rows[i][2].ToString());
				note.Note=PIn.PString(table.Rows[i][3].ToString());
				note.ProcTime=PIn.PString(table.Rows[i][4].ToString());
				list.Add(note);
			}
		}

		///<summary></summary>
		public static List<ProcCodeNote> GetList(long codeNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<ProcCodeNote>>(MethodBase.GetCurrentMethod(),codeNum);
			}
			List<ProcCodeNote> tempList=list;
			string command="SELECT * FROM proccodenote WHERE CodeNum="+POut.PInt(codeNum);
			DataTable table=Db.GetTable(command);
			FillCache(table);
			List<ProcCodeNote> result=list;
			list=tempList;
			return result;
		}

		///<summary></summary>
		public static long Insert(ProcCodeNote note) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				note.ProcCodeNoteNum=Meth.GetInt(MethodBase.GetCurrentMethod(),note);
				return note.ProcCodeNoteNum;
			}
			if(PrefC.RandomKeys) {
				note.ProcCodeNoteNum=ReplicationServers.GetKey("proccodenote","ProcCodeNoteNum");
			}
			string command="INSERT INTO proccodenote (";
			if(PrefC.RandomKeys) {
				command+="ProcCodeNoteNum,";
			}
			command+="CodeNum,ProvNum,Note,ProcTime) VALUES(";
			if(PrefC.RandomKeys) {
				command+=POut.PInt(note.ProcCodeNoteNum)+", ";
			}
			command+=
				 "'"+POut.PInt   (note.CodeNum)+"', "
				+"'"+POut.PInt   (note.ProvNum)+"', "
				+"'"+POut.PString(note.Note)+"', "
				+"'"+POut.PString(note.ProcTime)+"')";
			if(PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else{
				note.ProcCodeNoteNum=Db.NonQ(command,true);
			}
			return note.ProcCodeNoteNum;
		}

		///<summary></summary>
		public static void Update(ProcCodeNote note){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),note);
				return;
			}
			string command="UPDATE proccodenote SET " 
				+ "CodeNum = '"    +POut.PInt   (note.CodeNum)+"'"
				+ ",ProvNum = '"   +POut.PInt   (note.ProvNum)+"'"
				+ ",Note = '"      +POut.PString(note.Note)+"'"
				+ ",ProcTime = '"  +POut.PString(note.ProcTime)+"'"
				+" WHERE ProcCodeNoteNum = "+POut.PInt(note.ProcCodeNoteNum);
			Db.NonQ(command);
		}

		public static void Delete(long procCodeNoteNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),procCodeNoteNum);
				return;
			}
			string command="DELETE FROM proccodenote WHERE ProcCodeNoteNum = "+POut.PInt(procCodeNoteNum);
			Db.NonQ(command);
		}

		///<summary>Gets the note for the given provider, if one exists.  Otherwise, gets the proccode.defaultnote.</summary>
		public static string GetNote(long provNum,long codeNum) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<Listt.Count;i++){
				if(Listt[i].ProvNum!=provNum){
					continue;
				}
				if(Listt[i].CodeNum!=codeNum){
					continue;
				}
				return Listt[i].Note;
			}
			return ProcedureCodes.GetProcCode(codeNum).DefaultNote;
		}

		///<summary>Gets the time pattern for the given provider, if one exists.  Otherwise, gets the proccode.ProcTime.</summary>
		public static string GetTimePattern(long provNum,long codeNum) {
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<Listt.Count;i++) {
				if(Listt[i].ProvNum!=provNum) {
					continue;
				}
				if(Listt[i].CodeNum!=codeNum) {
					continue;
				}
				return Listt[i].ProcTime;
			}
			return ProcedureCodes.GetProcCode(codeNum).ProcTime;
		}



	}

	
	
	


}