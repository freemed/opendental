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

		/// <summary>The first time this is accessed, the value is obtained using a query.  Will be 0 unless a server id was set in my.ini.</summary>
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
				serv.ReplicationServerNum= PIn.Long  (table.Rows[i][0].ToString());
				serv.Descript            = PIn.String(table.Rows[i][1].ToString());
				serv.ServerId            = PIn.Int   (table.Rows[i][2].ToString());
				serv.RangeStart          = PIn.Long  (table.Rows[i][3].ToString());
				serv.RangeEnd            = PIn.Long  (table.Rows[i][4].ToString());
				serv.AtoZpath            = PIn.String(table.Rows[i][5].ToString());
				serv.UpdateBlocked       = PIn.Bool  (table.Rows[i][6].ToString());
				listt.Add(serv);
			}
		}

		///<summary></summary>
		public static long WriteObject(ReplicationServer serv) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				serv.ReplicationServerNum=Meth.GetLong(MethodBase.GetCurrentMethod(),serv);
				return serv.ReplicationServerNum;
			}
//DataObjectFactory<ReplicationServer>.WriteObject(serv);
//return serv.ReplicationServerNum;
			return 0;
		}

		public static void DeleteObject(long replicationServerNum){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),replicationServerNum);
				return;
			}
//DataObjectFactory<ReplicationServer>.DeleteObject(replicationServerNum);
		}

		public static int GetServer_id() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetInt(MethodBase.GetCurrentMethod());
			}
			string command="SHOW VARIABLES LIKE 'server_id'";
			DataTable table=Db.GetTable(command);
			return PIn.Int(table.Rows[0][1].ToString());
		}

		///<summary>Generates a random primary key.  Tests to see if that key already exists before returning it for use.  The range of returned values is greater than 0, and less than or equal to 9223372036854775807.</summary>
		public static long GetKey(string tablename,string field) {
			//No need to check RemotingRole; no call to db.
			//establish the range for this server
			long rangeStart=10000;
			long rangeEnd=long.MaxValue;
			//the following line triggers a separate call to db if server_id=-1.  Must be cap.
			if(Server_id!=0) {//if it IS 0, then there is no server_id set.
				ReplicationServer thisServer=null;
				for(int i=0;i<Listt.Count;i++) {
					if(Listt[i].ServerId==Server_id) {
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

		///<summary>If this server id is 0, or if no AtoZ entered for this server, then returns empty string.</summary>
		public static string GetAtoZpath() {
			//No need to check RemotingRole; no call to db.
			if(Server_id==0) {
				return "";
			}
			for(int i=0;i<Listt.Count;i++) {
				if(Listt[i].ServerId==Server_id) {
					return Listt[i].AtoZpath;//could be empty string.
				}
			}
			return "";
		}

		///<summary>If this server id is 0, this returns null.  Or if there is no ReplicationServer object for this server id, then this returns null.</summary>
		public static ReplicationServer GetForLocalComputer() {
			//No need to check RemotingRole; no call to db.
			if(Server_id==0) {
				return null;
			}
			for(int i=0;i<Listt.Count;i++) {
				if(Listt[i].ServerId==Server_id) {
					return Listt[i];
				}
			}
			return null;
		}

		///<summary>Used during database maint and from update window. We cannot use objects.</summary>
		public static bool ServerIsBlocked() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				//even though we are supposed to be guaranteed to not be a web client
				return true;
			}
			string command="SELECT COUNT(*) FROM replicationserver WHERE ServerId="+POut.Int(Server_id)//does trigger another query if during startup
				+" AND UpdateBlocked=1";
			try {
				if(Db.GetScalar(command)=="0") {
					return false;
				}
				else {
					return true;
				}
			}
			catch {
				return false;
			}
		}
		

	}
}