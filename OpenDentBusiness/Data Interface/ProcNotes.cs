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
