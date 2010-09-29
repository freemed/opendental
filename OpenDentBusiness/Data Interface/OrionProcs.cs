using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace OpenDentBusiness{
	///<summary></summary>
	public class OrionProcs{
		
		#region CachePattern
		/*
		//This region can be eliminated if this is not a table type with cached data.
		//If leaving this region in place, be sure to add RefreshCache and FillCache 
		//to the Cache.cs file with all the other Cache types.

		///<summary>A list of all OrionProcs.</summary>
		private static List<OrionProc> listt;

		///<summary>A list of all OrionProcs.</summary>
		public static List<OrionProc> Listt{
			get {
				if(listt==null) {
					RefreshCache();
				}
				return listt;
			}
			set {
				listt=value;
			}
		}

		///<summary></summary>
		public static DataTable RefreshCache(){
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string command="SELECT * FROM orionproc ORDER BY ItemOrder";//stub query probably needs to be changed
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="OrionProc";
			FillCache(table);
			return table;
		}

		///<summary></summary>
		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			listt=Crud.OrionProcCrud.TableToList(table);
		}
		*/
		#endregion

		///<summary>Gets one OrionProc from the db.</summary>
		public static OrionProc GetOne(long orionProcNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<OrionProc>(MethodBase.GetCurrentMethod(),orionProcNum);
			}
			return Crud.OrionProcCrud.SelectOne(orionProcNum);
		}

		///<summary>Gets one OrionProc from the db by ProcNum</summary>
		public static OrionProc GetOneByProcNum(long ProcNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<OrionProc>(MethodBase.GetCurrentMethod(),ProcNum);
			}
			string command="SELECT * FROM orionproc "
				+"WHERE ProcNum="+POut.Long(ProcNum)+" LIMIT 1";
			return Crud.OrionProcCrud.SelectOne(command);
		}

		///<summary></summary>
		public static long Insert(OrionProc orionProc) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				orionProc.OrionProcNum=Meth.GetLong(MethodBase.GetCurrentMethod(),orionProc);
				return orionProc.OrionProcNum;
			}
			return Crud.OrionProcCrud.Insert(orionProc);
		}

		///<summary></summary>
		public static void Update(OrionProc orionProc) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),orionProc);
				return;
			}
			Crud.OrionProcCrud.Update(orionProc);
		}

		public static DataTable GetCancelledScheduleDateByToothOrSurf(long PatNum, string ToothNum, string Surf) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetTable(MethodBase.GetCurrentMethod(),PatNum,ToothNum,Surf);
			}
			DataConnection dcon=new DataConnection();
			DataTable table=new DataTable("OrionSchedDate");
			DataRow row;
			table.Columns.Add("ProcNum");
			table.Columns.Add("Status2");
			table.Columns.Add("DateTP",typeof(DateTime));
			table.Columns.Add("DateScheduleBy",typeof(DateTime));
			table.Columns.Add("ToothNum");
			table.Columns.Add("Surf");
			string command="SELECT orionproc.ProcNum,orionproc.Status2,procedurelog.DateTP,orionproc.DateScheduleBy,procedurelog.ToothNum,procedurelog.Surf "
				+"FROM orionproc "
				+"LEFT JOIN procedurelog ON orionproc.ProcNum=procedurelog.ProcNum "
				+"WHERE procedurelog.PatNum="+POut.Long(PatNum)
				+" AND orionproc.Status2=128"
				+" AND procedurelog.ToothNum='"+POut.String(ToothNum)+"'"
				+" OR procedurelog.Surf='"+POut.String(Surf)+"'"
				+" ORDER BY orionproc.DateScheduleBy DESC "
				+"LIMIT 1"; 
			DataTable rawOrion=dcon.GetTable(command);
			row=table.NewRow();
			row["ProcNum"]=rawOrion.Rows[0]["ProcNum"].ToString();
			row["Status2"]=((OrionStatus)PIn.Int(rawOrion.Rows[0]["Status2"].ToString())).ToString();
			row["DateTP"]=rawOrion.Rows[0]["DateTP"].ToString();
			row["DateScheduleBy"]=rawOrion.Rows[0]["DateScheduleBy"].ToString();
			row["ToothNum"]=rawOrion.Rows[0]["ToothNum"].ToString();
			row["Surf"]=rawOrion.Rows[0]["Surf"].ToString();
			table.Rows.Add(row);
			return table;
		}

		/*
		Only pull out the methods below as you need them.  Otherwise, leave them commented out.

		///<summary></summary>
		public static List<OrionProc> Refresh(long patNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<OrionProc>>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM orionproc WHERE PatNum = "+POut.Long(patNum);
			return Crud.OrionProcCrud.SelectMany(command);
		}
		
		///<summary></summary>
		public static void Delete(long orionProcNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),orionProcNum);
				return;
			}
			string command= "DELETE FROM orionproc WHERE OrionProcNum = "+POut.Long(orionProcNum);
			Db.NonQ(command);
		}
		

		
		*/



	}
}