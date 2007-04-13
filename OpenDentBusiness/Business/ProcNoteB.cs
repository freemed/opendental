using System;
using System.Data;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;

namespace OpenDentBusiness {
	internal class ProcNoteB{
		internal static void Insert(ProcNote procNote){
			if(PrefB.RandomKeys) {
				procNote.ProcNoteNum=MiscDataB.GetKey("procnote","ProcNoteNum");
			}
			string command= "INSERT INTO procnote (";
			if(PrefB.RandomKeys) {
				command+="ProcNoteNum,";
			}
			command+="PatNum, ProcNum, EntryDateTime, UserNum, Note, SigIsTopaz, Signature) VALUES(";
			if(PrefB.RandomKeys) {
				command+="'"+POut.PInt(procNote.ProcNoteNum)+"', ";
			}
			command+=
				 "'"+POut.PInt   (procNote.PatNum)+"', "
				+"'"+POut.PInt   (procNote.ProcNum)+"', ";
			if(DataConnection.DBtype==DatabaseType.Oracle) {
				command+=POut.PDateT(MiscDataB.GetNowDateTime());
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
