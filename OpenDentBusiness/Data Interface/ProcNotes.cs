using System;
using System.Data;
using System.Diagnostics;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness {
	public class ProcNotes{
		public static long Insert(ProcNote procNote){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				procNote.ProcNoteNum=Meth.GetLong(MethodBase.GetCurrentMethod(),procNote);
				return procNote.ProcNoteNum;
			}
			if(PrefC.RandomKeys) {
				procNote.ProcNoteNum=ReplicationServers.GetKey("procnote","ProcNoteNum");
			}
			string command="INSERT INTO procnote (";
			if(PrefC.RandomKeys) {
				command+="ProcNoteNum,";
			}
			command+="PatNum, ProcNum, EntryDateTime, UserNum, Note, SigIsTopaz, Signature) VALUES(";
			if(PrefC.RandomKeys) {
				command+=POut.PLong(procNote.ProcNoteNum)+", ";
			}
			command+=
				 "'"+POut.PLong   (procNote.PatNum)+"', "
				+"'"+POut.PLong   (procNote.ProcNum)+"', "
				+"NOW(), "//EntryDateTime
				+"'"+POut.PLong   (procNote.UserNum)+"', "
				+"'"+POut.PString(procNote.Note)+"', "
				+"'"+POut.PBool  (procNote.SigIsTopaz)+"', "
				+"'"+POut.Base64 (procNote.Signature)+"')";
			if(PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else{
				procNote.ProcNoteNum=Db.NonQ(command,true);
			}
			return procNote.ProcNoteNum;
		}
		
		/*
		///<summary></summary>
		internal static bool PreviousNoteExists(int procNum){
			string command="SELECT COUNT(*) FROM procnote WHERE ProcNum="+POut.PInt(procNum);
			DataConnection dcon=new DataConnection();
			if(dcon.GetCount(command)=="0"){
				return false;
			}
			return true;
		}*/






	}



}
