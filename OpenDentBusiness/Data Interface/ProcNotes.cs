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

		public static ProcNote GetProcNotesForPat(long patNum, DateTime dateStart, DateTime dateEnd) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<ProcNote>(MethodBase.GetCurrentMethod(),patNum,dateStart,dateEnd);
			}
			string command="SELECT procnote.* FROM procnote "
				+"INNER JOIN procedurelog ON procedurelog.ProcNum=procnote.ProcNum "
				+"WHERE procnote.PatNum="+POut.Long(patNum)+" "
				+"AND procnote.EntryDateTime BETWEEN "+POut.Date(dateStart)+" AND "+POut.Date(dateEnd)+" "
				+"AND procedurelog.ProcStatus!="+POut.Int((int)ProcStat.D)+" "
				+"ORDER BY procnote.EntryDateTime DESC LIMIT 1";
			return Crud.ProcNoteCrud.SelectOne(command);
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
