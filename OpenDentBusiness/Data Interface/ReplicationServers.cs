using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Net;
using System.Reflection;
using OpenDentBusiness.DataAccess;

namespace OpenDentBusiness{
	///<summary></summary>
	public class ReplicationServers{
		///<summary></summary>
		private static List<ReplicationServer> listt;
		///<summary>This value is only retrieved once upon startup.</summary>
		private static int server_id=-1;

		public static int Server_id {
			get{
				if(server_id==-1) {
					server_id=GetServer_id();
				}
				return server_id;
			}
		}

		public static List<ReplicationServer> Listt {
			get {
				if(listt==null) {
					RefreshCache();
				}
				return listt;
			}
		}
		
		///<summary></summary>
		public static DataTable RefreshCache(){
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string cmd="SELECT * FROM replicationserver ORDER BY ServerId";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),cmd);
			table.TableName="replicationserver";
			FillCache(table);
			return table;
		}

		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			listt=new List<ReplicationServer>();
			ReplicationServer serv;
			for(int i=0;i<table.Rows.Count;i++){
				serv=new ReplicationServer();
				serv.IsNew=false;
				serv.ReplicationServerNum= PIn.PInt   (table.Rows[i][0].ToString());
				serv.Descript            = PIn.PString(table.Rows[i][1].ToString());
				serv.ServerId            = PIn.PInt32 (table.Rows[i][2].ToString());
				serv.RangeStart          = PIn.PInt   (table.Rows[i][3].ToString());
				serv.RangeEnd            = PIn.PInt   (table.Rows[i][4].ToString());
				listt.Add(serv);
			}
		}

		/*
		///<Summary>Gets one replicationServer from the database.</Summary>
		public static replicationServer CreateObject(int replicationServerNum){
			return DataObjectFactory<replicationServer>.CreateObject(replicationServerNum);
		}*/

		///<summary></summary>
		public static long WriteObject(ReplicationServer serv) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				serv.ReplicationServerNum=Meth.GetInt(MethodBase.GetCurrentMethod(),serv);
				return serv.ReplicationServerNum;
			}
			DataObjectFactory<ReplicationServer>.WriteObject(serv);
			return serv.ReplicationServerNum;
		}

		/*
		///<summary></summary>
		public static void DeleteObject(int replicationServerNum){
			//validate that not already in use.
			string command="SELECT LName,FName FROM patient WHERE replicationServerNum="+POut.PInt(replicationServerNum);
			DataTable table=Db.GetTable(command);
			//int count=PIn.PInt(Db.GetCount(command));
			string pats="";
			for(int i=0;i<table.Rows.Count;i++){
				if(i>0){
					pats+=", ";
				}
				pats+=table.Rows[i]["FName"].ToString()+" "+table.Rows[i]["LName"].ToString();
			}
			if(table.Rows.Count>0){
				throw new ApplicationException(Lans.g("replicationServers","replicationServer is already in use by patient(s). Not allowed to delete. ")+pats);
			}
			DataObjectFactory<replicationServer>.DeleteObject(replicationServerNum);
		}*/

		public static void DeleteObject(long replicationServerNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),replicationServerNum);
				return;
			}
			DataObjectFactory<ReplicationServer>.DeleteObject(replicationServerNum);
		}

		private static int GetServer_id() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetInt32(MethodBase.GetCurrentMethod());
			}
			string command="SHOW VARIABLES LIKE 'server_id'";
			DataTable table=Db.GetTable(command);
			return PIn.PInt32(table.Rows[0][1].ToString());
		}

		///<summary>Generates a random primary key.  Tests to see if that key already exists before returning it for use.  The range of returned values is greater than 0, and less than or equal to 9223372036854775807.</summary>
		public static long GetKey(string tablename,string field) {
			//No need to check RemotingRole; no call to db.
			//establish the range for this server
			long rangeStart=10000;
			long rangeEnd=long.MaxValue;
			//the following line triggers a separate call to db if server_id=-1.
			if(server_id!=0) {//if it IS 0, then there is no server_id set.
				ReplicationServer thisServer=null;
				for(int i=0;i<Listt.Count;i++) {
					if(Listt[i].ServerId==server_id) {
						thisServer=Listt[i];
						break;
					}
				}
				if(thisServer!=null) {//a ReplicationServer row was found for this server_id
					if(thisServer.RangeEnd-thisServer.RangeStart > 1000000){//and a valid range was entered that was at least 1,000,000
						rangeStart=thisServer.RangeStart;
						rangeEnd=thisServer.RangeEnd;
					}
				}
			}
			Random random=new Random();
			long rndLong;
			long span=rangeEnd-rangeStart;
			do {
				rndLong=(long)(random.NextDouble()*span) + rangeStart;
				//rnd=random.Next(myPartitionStart,myPartitionEnd);
			}
			while(rndLong==0  
				|| rndLong < rangeStart
				|| rndLong > rangeEnd
				|| KeyInUse(tablename,field,rndLong));
			return rndLong;
		}

		/*
		private static long GetNumComputers() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetInt(MethodBase.GetCurrentMethod());
			}
			string command="SELECT COUNT(*) FROM computer";
			DataTable table=Db.GetTable(command);
			return PIn.PInt(table.Rows[0][0].ToString());
		}

		private static long GetComputerNumForName(string computerName) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetInt(MethodBase.GetCurrentMethod(),computerName);
			}
			string command="SELECT COUNT(*) FROM computer "+
				"WHERE ComputerNum <=(SELECT ComputerNum FROM computer AS temp WHERE CompName "+
				"LIKE '"+computerName+"')";
			DataTable table=Db.GetTable(command);
			return PIn.PInt(table.Rows[0][0].ToString());
		}*/

		private static bool KeyInUse(string tablename,string field,long keynum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetBool(MethodBase.GetCurrentMethod(),tablename,field,keynum);
			}
			string command="SELECT COUNT(*) FROM "+tablename+" WHERE "+field+"="+keynum.ToString();
			if(Db.GetCount(command)=="0") {
				return false;
			}
			return true;//already in use
		}
	
		

	}
}