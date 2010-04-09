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
				+"WHERE PatNum="+POut.Long(patNum)
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
				+"WHERE TreatPlanNum="+POut.Long(tpNum)
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
				proc.ProcTPNum   = PIn.Long(table.Rows[i][0].ToString());
				proc.TreatPlanNum= PIn.Long(table.Rows[i][1].ToString());
				proc.PatNum      = PIn.Long(table.Rows[i][2].ToString());
				proc.ProcNumOrig = PIn.Long(table.Rows[i][3].ToString());
				proc.ItemOrder   = PIn.Int(table.Rows[i][4].ToString());
				proc.Priority    = PIn.Long(table.Rows[i][5].ToString());
				proc.ToothNumTP  = PIn.String(table.Rows[i][6].ToString());
				proc.Surf        = PIn.String(table.Rows[i][7].ToString());
				proc.ProcCode    = PIn.String(table.Rows[i][8].ToString());
				proc.Descript    = PIn.String(table.Rows[i][9].ToString());
				proc.FeeAmt      = PIn.Double(table.Rows[i][10].ToString());
				proc.PriInsAmt   = PIn.Double(table.Rows[i][11].ToString());
				proc.SecInsAmt   = PIn.Double(table.Rows[i][12].ToString());
				proc.PatAmt      = PIn.Double(table.Rows[i][13].ToString());
				proc.Discount    = PIn.Double(table.Rows[i][14].ToString());
				retVal.Add(proc);
			}
			return retVal;
		}
		
		///<summary></summary>
		public static void Update(ProcTP proc){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),proc);
				return;
			}
			string command= "UPDATE proctp SET "
				+"TreatPlanNum = '"+POut.Long   (proc.TreatPlanNum)+"'"
				+",PatNum = '"     +POut.Long   (proc.PatNum)+"'"
				+",ProcNumOrig = '"+POut.Long   (proc.ProcNumOrig)+"'"
				+",ItemOrder = '"  +POut.Long   (proc.ItemOrder)+"'"
				+",Priority = '"   +POut.Long   (proc.Priority)+"'"
				+",ToothNumTP = '" +POut.String(proc.ToothNumTP)+"'"
				+",Surf = '"       +POut.String(proc.Surf)+"'"
				+",ProcCode = '"   +POut.String(proc.ProcCode)+"'"
				+",Descript = '"   +POut.String(proc.Descript)+"'"
				+",FeeAmt = '"     +POut.Double(proc.FeeAmt)+"'"
				+",PriInsAmt = '"  +POut.Double(proc.PriInsAmt)+"'"
				+",SecInsAmt = '"  +POut.Double(proc.SecInsAmt)+"'"
				+",PatAmt = '"     +POut.Double(proc.PatAmt)+"'"
				+",Discount = '"   +POut.Double(proc.Discount)+"'"
				+" WHERE ProcTPNum = '"+POut.Long(proc.ProcTPNum)+"'";
 			Db.NonQ(command);
		}

		///<summary></summary>
		public static long Insert(ProcTP proc){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				proc.ProcTPNum=Meth.GetLong(MethodBase.GetCurrentMethod(),proc);
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
				command+="'"+POut.Long(proc.ProcTPNum)+"', ";
			}
			command+=
				 "'"+POut.Long   (proc.TreatPlanNum)+"', "
				+"'"+POut.Long   (proc.PatNum)+"', "
				+"'"+POut.Long   (proc.ProcNumOrig)+"', "
				+"'"+POut.Long   (proc.ItemOrder)+"', "
				+"'"+POut.Long   (proc.Priority)+"', "
				+"'"+POut.String(proc.ToothNumTP)+"', "
				+"'"+POut.String(proc.Surf)+"', "
				+"'"+POut.String(proc.ProcCode)+"', "
				+"'"+POut.String(proc.Descript)+"', "
				+"'"+POut.Double(proc.FeeAmt)+"', "
				+"'"+POut.Double(proc.PriInsAmt)+"', "
				+"'"+POut.Double(proc.SecInsAmt)+"', "
				+"'"+POut.Double(proc.PatAmt)+"', "
				+"'"+POut.Double(proc.Discount)+"')";
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
			string command= "DELETE from proctp WHERE ProcTPNum = '"+POut.Long(proc.ProcTPNum)+"'";
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
				+"WHERE TreatPlanNum="+POut.Long(treatPlanNum);
			Db.NonQ(command);
		}

	
	}

	

	


}




















