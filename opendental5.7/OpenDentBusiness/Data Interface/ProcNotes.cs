using System;
using System.Data;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;

namespace OpenDentBusiness {
	public class ProcNotes{
		public static void Insert(ProcNote procNote){
			if(PrefC.RandomKeys) {
				procNote.ProcNoteNum=MiscData.GetKey("procnote","ProcNoteNum");
			}
			string command= "INSERT INTO procnote (";
			if(PrefC.RandomKeys) {
				command+="ProcNoteNum,";
			}
			command+="PatNum, ProcNum, EntryDateTime, UserNum, Note, SigIsTopaz, Signature) VALUES(";
			if(PrefC.RandomKeys) {
				command+="'"+POut.PInt(procNote.ProcNoteNum)+"', ";
			}
			command+=
				 "'"+POut.PInt   (procNote.PatNum)+"', "
				+"'"+POut.PInt   (procNote.ProcNum)+"', ";
			if(DataConnection.DBtype==DatabaseType.Oracle) {
				command+=POut.PDateT(MiscData.GetNowDateTime());
			}
			else {//Assume MySQL
				command+="NOW()";
			}
			command+=", "//EntryDateTime
				+"'"+POut.PInt   (procNote.UserNum)+"', "
				+"'"+POut.PString(procNote.Note)+"', "
				+"'"+POut.PBool  (procNote.SigIsTopaz)+"', "
				+"'"+POut.Base64 (procNote.Signature)+"')";
			//MessageBox.Show(cmd.CommandText);
			DataConnection dcon=new DataConnection();
			dcon.NonQ(command);
			//Debug.WriteLine("Sig length: "+procNote.Signature.Length.ToString());
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
