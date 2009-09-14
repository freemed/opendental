using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace OpenDentBusiness{
	///<summary></summary>
	public class ProcTPs {
		///<summary>Gets all ProcTPs for a given Patient ordered by ItemOrder.</summary>
		public static ProcTP[] Refresh(long patNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<ProcTP[]>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM proctp "
				+"WHERE PatNum="+POut.PLong(patNum)
				+" ORDER BY ItemOrder";
			DataTable table=Db.GetTable(command);
			return RefreshAndFill(table).ToArray();
		}

		///<summary>Only used when obtaining the signature data.  Ordered by ItemOrder.</summary>
		public static List<ProcTP> RefreshForTP(long tpNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<ProcTP>>(MethodBase.GetCurrentMethod(),tpNum);
			}
			string command="SELECT * FROM proctp "
				+"WHERE TreatPlanNum="+POut.PLong(tpNum)
				+" ORDER BY ItemOrder";
			DataTable table=Db.GetTable(command);
			return RefreshAndFill(table);
		}

		private static List<ProcTP> RefreshAndFill(DataTable table){
			//No need to check RemotingRole; no call to db.
			List<ProcTP> retVal=new List<ProcTP>();
			ProcTP proc;
			for(int i=0;i<table.Rows.Count;i++) {
				proc=new ProcTP();
				proc.ProcTPNum   = PIn.PLong(table.Rows[i][0].ToString());
				proc.TreatPlanNum= PIn.PLong(table.Rows[i][1].ToString());
				proc.PatNum      = PIn.PLong(table.Rows[i][2].ToString());
				proc.ProcNumOrig = PIn.PLong(table.Rows[i][3].ToString());
				proc.ItemOrder   = PIn.PInt(table.Rows[i][4].ToString());
				proc.Priority    = PIn.PLong(table.Rows[i][5].ToString());
				proc.ToothNumTP  = PIn.PString(table.Rows[i][6].ToString());
				proc.Surf        = PIn.PString(table.Rows[i][7].ToString());
				proc.ProcCode    = PIn.PString(table.Rows[i][8].ToString());
				proc.Descript    = PIn.PString(table.Rows[i][9].ToString());
				proc.FeeAmt      = PIn.PDouble(table.Rows[i][10].ToString());
				proc.PriInsAmt   = PIn.PDouble(table.Rows[i][11].ToString());
				proc.SecInsAmt   = PIn.PDouble(table.Rows[i][12].ToString());
				proc.PatAmt      = PIn.PDouble(table.Rows[i][13].ToString());
				proc.Discount    = PIn.PDouble(table.Rows[i][14].ToString());
				retVal.Add(proc);
			}
			return retVal;
		}
		
		///<summary></summary>
		private static void Update(ProcTP proc){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),proc);
				return;
			}
			string command= "UPDATE proctp SET "
				+"TreatPlanNum = '"+POut.PLong   (proc.TreatPlanNum)+"'"
				+",PatNum = '"     +POut.PLong   (proc.PatNum)+"'"
				+",ProcNumOrig = '"+POut.PLong   (proc.ProcNumOrig)+"'"
				+",ItemOrder = '"  +POut.PLong   (proc.ItemOrder)+"'"
				+",Priority = '"   +POut.PLong   (proc.Priority)+"'"
				+",ToothNumTP = '" +POut.PString(proc.ToothNumTP)+"'"
				+",Surf = '"       +POut.PString(proc.Surf)+"'"
				+",ProcCode = '"   +POut.PString(proc.ProcCode)+"'"
				+",Descript = '"   +POut.PString(proc.Descript)+"'"
				+",FeeAmt = '"     +POut.PDouble(proc.FeeAmt)+"'"
				+",PriInsAmt = '"  +POut.PDouble(proc.PriInsAmt)+"'"
				+",SecInsAmt = '"  +POut.PDouble(proc.SecInsAmt)+"'"
				+",PatAmt = '"     +POut.PDouble(proc.PatAmt)+"'"
				+",Discount = '"   +POut.PDouble(proc.Discount)+"'"
				+" WHERE ProcTPNum = '"+POut.PLong(proc.ProcTPNum)+"'";
 			Db.NonQ(command);
		}

		///<summary></summary>
		private static long Insert(ProcTP proc){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				proc.ProcTPNum=Meth.GetInt(MethodBase.GetCurrentMethod(),proc);
				return proc.ProcTPNum;
			}
			if(PrefC.RandomKeys){
				proc.ProcTPNum=ReplicationServers.GetKey("proctp","ProcTPNum");
			}
			string command= "INSERT INTO proctp (";
			if(PrefC.RandomKeys){
				command+="ProcTPNum,";
			}
			command+="TreatPlanNum,PatNum,ProcNumOrig,ItemOrder,Priority,ToothNumTP,Surf,ProcCode,Descript,FeeAmt,"
				+"PriInsAmt,SecInsAmt,PatAmt,Discount) VALUES(";
			if(PrefC.RandomKeys){
				command+="'"+POut.PLong(proc.ProcTPNum)+"', ";
			}
			command+=
				 "'"+POut.PLong   (proc.TreatPlanNum)+"', "
				+"'"+POut.PLong   (proc.PatNum)+"', "
				+"'"+POut.PLong   (proc.ProcNumOrig)+"', "
				+"'"+POut.PLong   (proc.ItemOrder)+"', "
				+"'"+POut.PLong   (proc.Priority)+"', "
				+"'"+POut.PString(proc.ToothNumTP)+"', "
				+"'"+POut.PString(proc.Surf)+"', "
				+"'"+POut.PString(proc.ProcCode)+"', "
				+"'"+POut.PString(proc.Descript)+"', "
				+"'"+POut.PDouble(proc.FeeAmt)+"', "
				+"'"+POut.PDouble(proc.PriInsAmt)+"', "
				+"'"+POut.PDouble(proc.SecInsAmt)+"', "
				+"'"+POut.PDouble(proc.PatAmt)+"', "
				+"'"+POut.PDouble(proc.Discount)+"')";
 			if(PrefC.RandomKeys){
				Db.NonQ(command);
			}
			else{
 				proc.ProcTPNum=Db.NonQ(command,true);
			}
			return proc.ProcTPNum;
		}

		///<summary></summary>
		public static void InsertOrUpdate(ProcTP proc, bool isNew){
			//No need to check RemotingRole; no call to db.
			if(isNew){
				Insert(proc);
			}
			else{
				Update(proc);
			}
		}

		///<summary>There are no dependencies.</summary>
		public static void Delete(ProcTP proc){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),proc);
				return;
			}
			string command= "DELETE from proctp WHERE ProcTPNum = '"+POut.PLong(proc.ProcTPNum)+"'";
 			Db.NonQ(command);
		}

		///<summary>Gets a list for just one tp.  Used in TP module.  Supply a list of all ProcTPs for pt.</summary>
		public static ProcTP[] GetListForTP(long treatPlanNum,ProcTP[] listAll) {
			//No need to check RemotingRole; no call to db.
			ArrayList AL=new ArrayList();
			for(int i=0;i<listAll.Length;i++){
				if(listAll[i].TreatPlanNum!=treatPlanNum){
					continue;
				}
				AL.Add(listAll[i]);
			}
			ProcTP[] retVal=new ProcTP[AL.Count];
			AL.CopyTo(retVal);
			return retVal;
		}

		

		///<summary>No dependencies to worry about.</summary>
		public static void DeleteForTP(long treatPlanNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),treatPlanNum);
				return;
			}
			string command="DELETE FROM proctp "
				+"WHERE TreatPlanNum="+POut.PLong(treatPlanNum);
			Db.NonQ(command);
		}

	
	}

	

	


}




















