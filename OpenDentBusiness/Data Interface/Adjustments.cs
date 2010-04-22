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
			Crud.AdjustmentCrud.Update(adj);
		}

		///<summary></summary>
		public static long Insert(Adjustment adj) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				adj.AdjNum=Meth.GetLong(MethodBase.GetCurrentMethod(),adj);
				return adj.AdjNum;
			}
			return Crud.AdjustmentCrud.Insert(adj);
		}

		///<summary>This will soon be eliminated or changed to only allow deleting on same day as EntryDate.</summary>
		public static void Delete(Adjustment adj){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),adj);
				return;
			}
			Crud.AdjustmentCrud.Delete(adj.AdjNum);
		}

		///<summary>Gets all adjustments for a single patient.</summary>
		public static Adjustment[] Refresh(long patNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<Adjustment[]>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command=
				"SELECT * FROM adjustment"
				+" WHERE PatNum = "+POut.Long(patNum)+" ORDER BY AdjDate";
			return Crud.AdjustmentCrud.SelectMany(command).ToArray();
		}

		///<summary>Gets one adjustment from the db.</summary>
		public static Adjustment GetOne(long adjNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<Adjustment>(MethodBase.GetCurrentMethod(),adjNum);
			}
			string command=
				"SELECT * FROM adjustment"
				+" WHERE AdjNum = "+POut.Long(adjNum);
			return Crud.AdjustmentCrud.SelectOne(adjNum);
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










