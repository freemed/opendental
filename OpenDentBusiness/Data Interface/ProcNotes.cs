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
			return Crud.ProcNoteCrud.Insert(procNote);
		}

		public static List<ProcNote> GetProcNotesForPat(long patNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<ProcNote>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM procnote WHERE procnote.PatNum="+POut.Long(patNum);
			return Crud.ProcNoteCrud.SelectMany(command);
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
