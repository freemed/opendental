using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace OpenDentBusiness{
	///<summary>Handles database commands related to the adjustment table in the db.</summary>
	public class Adjustments {

		///<summary></summary>
		public static void Update(Adjustment adj){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),adj);
				return;
			}
			string command="UPDATE adjustment SET " 
				+ "adjdate = "      +POut.Date  (adj.AdjDate)
				+ ",adjamt = '"      +POut.Double(adj.AdjAmt)+"'"
				+ ",patnum = '"      +POut.Long   (adj.PatNum)+"'"
				+ ",adjtype = '"     +POut.Long   (adj.AdjType)+"'"
				+ ",provnum = '"     +POut.Long   (adj.ProvNum)+"'"
				+ ",adjnote = '"     +POut.String(adj.AdjNote)+"'"
				+ ",ProcDate = "     +POut.Date  (adj.ProcDate)
				+ ",ProcNum = '"     +POut.Long   (adj.ProcNum)+"'"
				//DateEntry not allowed to change
				+ ",ClinicNum = '"   +POut.Long   (adj.ClinicNum)+"'"
				+" WHERE adjNum = '" +POut.Long   (adj.AdjNum)+"'";
			Db.NonQ(command);
		}

		///<summary></summary>
		public static long Insert(Adjustment adj) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				adj.AdjNum=Meth.GetLong(MethodBase.GetCurrentMethod(),adj);
				return adj.AdjNum;
			}
			if(PrefC.RandomKeys){
				adj.AdjNum=ReplicationServers.GetKey("adjustment","AdjNum");
			}
			string command= "INSERT INTO adjustment (";
			if(PrefC.RandomKeys){
				command+="AdjNum,";
			}
			command+="AdjDate,AdjAmt,PatNum, "
				+"AdjType,ProvNum,AdjNote,ProcDate,ProcNum,DateEntry,ClinicNum) VALUES(";
			if(PrefC.RandomKeys){
				command+="'"+POut.Long(adj.AdjNum)+"', ";
			}
			command+=
				 POut.Date(adj.AdjDate)+", "
				+"'"+POut.Double(adj.AdjAmt)+"', "
				+"'"+POut.Long(adj.PatNum)+"', "
				+"'"+POut.Long(adj.AdjType)+"', "
				+"'"+POut.Long(adj.ProvNum)+"', "
				+"'"+POut.String(adj.AdjNote)+"', "
				+POut.Date(adj.ProcDate)+", "
				+"'"+POut.Long(adj.ProcNum)+"', "
				+"NOW(),"//DateEntry set to server date
				+"'"+POut.Long(adj.ClinicNum)+"')";
			if(PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				adj.AdjNum=Db.NonQ(command,true);
			}
			return adj.AdjNum;
		}

		/*
		///<summary></summary>
		public static void InsertOrUpdate(Adjustment adj, bool IsNew){
			//if(){
				//throw new Exception(Lans.g(this,""));
			//}
			if(IsNew){
				Insert(adj);
			}
			else{
				Update(adj);
			}
		}*/

		///<summary>This will soon be eliminated or changed to only allow deleting on same day as EntryDate.</summary>
		public static void Delete(Adjustment adj){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),adj);
				return;
			}
			string command="DELETE FROM adjustment "
				+"WHERE AdjNum = '"+adj.AdjNum.ToString()+"'";
 			Db.NonQ(command);
		}

		///<summary>Gets all adjustments for a single patient.</summary>
		public static Adjustment[] Refresh(long patNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<Adjustment[]>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command=
				"SELECT * FROM adjustment"
				+" WHERE PatNum = "+POut.Long(patNum)+" ORDER BY AdjDate";
			return RefreshAndFill(Db.GetTable(command)).ToArray();
		}

		///<summary>Gets one adjustment from the db.</summary>
		public static Adjustment GetOne(long adjNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<Adjustment>(MethodBase.GetCurrentMethod(),adjNum);
			}
			string command=
				"SELECT * FROM adjustment"
				+" WHERE AdjNum = "+POut.Long(adjNum);
			return RefreshAndFill(Db.GetTable(command))[0];
		}

		private static List<Adjustment> RefreshAndFill(DataTable table){
			//No need to check RemotingRole; no call to db.
			List<Adjustment> retVal=new List<Adjustment>();
			Adjustment adj;
			for(int i=0;i<table.Rows.Count;i++){
				adj=new Adjustment();
				adj.AdjNum   = PIn.Long   (table.Rows[i][0].ToString());
				adj.AdjDate  = PIn.Date  (table.Rows[i][1].ToString());
				adj.AdjAmt   = PIn.Double(table.Rows[i][2].ToString());
				adj.PatNum   = PIn.Long   (table.Rows[i][3].ToString());
				adj.AdjType  = PIn.Long   (table.Rows[i][4].ToString());
				adj.ProvNum  = PIn.Long   (table.Rows[i][5].ToString());
				adj.AdjNote  = PIn.String(table.Rows[i][6].ToString());
				adj.ProcDate = PIn.Date  (table.Rows[i][7].ToString());
				adj.ProcNum  = PIn.Long   (table.Rows[i][8].ToString());
				adj.DateEntry= PIn.Date(table.Rows[i][9].ToString());
				adj.ClinicNum= PIn.Long  (table.Rows[i][10].ToString());
				retVal.Add(adj);
			}
			return retVal;
		}

		///<summary>Loops through the supplied list of adjustments and returns an ArrayList of adjustments for the given proc.</summary>
		public static ArrayList GetForProc(long procNum,Adjustment[] List) {
			//No need to check RemotingRole; no call to db.
			ArrayList retVal=new ArrayList();
			for(int i=0;i<List.Length;i++){
				if(List[i].ProcNum==procNum){
					retVal.Add(List[i]);
				}
			}
			return retVal;
		}

		///<summary>Used from ContrAccount and ProcEdit to display and calculate adjustments attached to procs.</summary>
		public static double GetTotForProc(long procNum,Adjustment[] List) {
			//No need to check RemotingRole; no call to db.
			double retVal=0;
			for(int i=0;i<List.Length;i++){
				if(List[i].ProcNum==procNum){
					retVal+=List[i].AdjAmt;
				}
			}
			return retVal;
		}

		/*
		///<summary>Must make sure Refresh is done first.  Returns the sum of all adjustments for this patient.  Amount might be pos or neg.</summary>
		public static double ComputeBal(Adjustment[] List){
			double retVal=0;
			for(int i=0;i<List.Length;i++){
				retVal+=List[i].AdjAmt;
			}
			return retVal;
		}*/

		///<summary>Returns the number of finance charges deleted.</summary>
		public static long UndoFinanceCharges(DateTime dateUndo) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetLong(MethodBase.GetCurrentMethod(),dateUndo);
			}
			string command;
			long numAdj;
			DataTable table;
			command="SELECT ValueString FROM preference WHERE PrefName = 'FinanceChargeAdjustmentType'";
			table=Db.GetTable(command);
			numAdj=PIn.Long(table.Rows[0][0].ToString());
			command="DELETE FROM adjustment WHERE AdjDate="+POut.Date(dateUndo)
				+" AND AdjType="+POut.Long(numAdj);
			return Db.NonQ(command);
		}

		///<summary>Returns the number of billing charges deleted.</summary>
		public static long UndoBillingCharges(DateTime dateUndo) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetLong(MethodBase.GetCurrentMethod(),dateUndo);
			}
			string command;
			long numAdj;
			DataTable table;
			command="SELECT ValueString FROM preference WHERE PrefName = 'BillingChargeAdjustmentType'";
			table=Db.GetTable(command);
			numAdj=PIn.Long(table.Rows[0][0].ToString());
			command="DELETE FROM adjustment WHERE AdjDate="+POut.Date(dateUndo)
				+" AND AdjType="+POut.Long(numAdj);
			return Db.NonQ(command);
		}

	}

	


	


}










