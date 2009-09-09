using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Reflection;
using OpenDentBusiness.DataAccess;

namespace OpenDentBusiness{
	///<summary></summary>
	public class ReplicationServers{
		///<summary></summary>
		public static List<ReplicationServer> Listt;
		
		///<summary></summary>
		public static DataTable RefreshCache(){
			string cmd="SELECT * FROM replicationserver ORDER BY ServerId";
			DataTable table=Meth.GetTable(MethodInfo.GetCurrentMethod(),cmd);
			table.TableName="replicationserver";
			FillCache(table);
			return table;
		}

		public static void FillCache(DataTable table){
			Listt=new List<ReplicationServer>();
			ReplicationServer serv;
			for(int i=0;i<table.Rows.Count;i++){
				serv=new ReplicationServer();
				serv.IsNew=false;
				serv.ReplicationServerNum= PIn.PInt   (table.Rows[i][0].ToString());
				serv.Descript            = PIn.PString(table.Rows[i][1].ToString());
				serv.ServerId            = PIn.PInt32 (table.Rows[i][2].ToString());
				serv.RangeStart          = PIn.PInt   (table.Rows[i][3].ToString());
				serv.RangeEnd            = PIn.PInt   (table.Rows[i][4].ToString());
				Listt.Add(serv);
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

		public static void DeleteObject(int replicationServerNum){
			DataObjectFactory<ReplicationServer>.DeleteObject(replicationServerNum);
		}
	
		

	}
}