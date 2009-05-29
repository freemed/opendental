using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Reflection;
using OpenDentBusiness.DataAccess;

namespace OpenDentBusiness{
	///<summary></summary>
	public class RecallTriggers{
		///<summary></summary>
		public static DataTable RefreshCache() {
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string c="SELECT * FROM recalltrigger";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),c);
			table.TableName="RecallTrigger";
			FillCache(table);
			return table;
		}

		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			RecallTriggerC.Listt=new List<RecallTrigger>();
			RecallTrigger trig;
			for(int i=0;i<table.Rows.Count;i++){
				trig=new RecallTrigger();
				trig.IsNew=false;
				trig.RecallTriggerNum = PIn.PInt   (table.Rows[i][0].ToString());
				trig.RecallTypeNum    = PIn.PInt   (table.Rows[i][1].ToString());
				trig.CodeNum          = PIn.PInt   (table.Rows[i][2].ToString());
				RecallTriggerC.Listt.Add(trig);
			}
		}

		/*
		///<Summary>Gets one RecallTrigger from the database.</Summary>
		public static RecallTrigger CreateObject(int RecallTriggerNum){
			return DataObjectFactory<RecallTrigger>.CreateObject(RecallTriggerNum);
		}

		public static List<RecallTrigger> GetRecallTriggers(int[] RecallTriggerNums){
			Collection<RecallTrigger> collectState=DataObjectFactory<RecallTrigger>.CreateObjects(RecallTriggerNums);
			return new List<RecallTrigger>(collectState);		
		}*/

		///<summary></summary>
		public static int WriteObject(RecallTrigger trigger){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				trigger.RecallTriggerNum=Meth.GetInt(MethodBase.GetCurrentMethod(),trigger);
				return trigger.RecallTriggerNum;
			}
			DataObjectFactory<RecallTrigger>.WriteObject(trigger);
			return trigger.RecallTriggerNum;
		}

		/*//<summary></summary>
		public static void DeleteObject(int RecallTriggerNum){
			//validate that not already in use.
			string command="SELECT LName,FName FROM patient WHERE RecallTriggerNum="+POut.PInt(RecallTriggerNum);
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
				throw new ApplicationException(Lans.g("RecallTriggers","RecallTrigger is already in use by patient(s). Not allowed to delete. ")+pats);
			}
			DataObjectFactory<RecallTrigger>.DeleteObject(RecallTriggerNum);
		}*/

		//public static void DeleteObject(int RecallTriggerNum){
		//	DataObjectFactory<RecallTrigger>.DeleteObject(RecallTriggerNum);
		//}

		public static List<RecallTrigger> GetForType(int recallTypeNum){
			//No need to check RemotingRole; no call to db.
			List<RecallTrigger> triggerList=new List<RecallTrigger>();
			if(recallTypeNum==0){
				return triggerList;
			}
			for(int i=0;i<RecallTriggerC.Listt.Count;i++){
				if(RecallTriggerC.Listt[i].RecallTypeNum==recallTypeNum){
					triggerList.Add(RecallTriggerC.Listt[i].Copy());
				}
			}
			return triggerList;
		}

		public static void SetForType(int recallTypeNum,List<RecallTrigger> triggerList){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),recallTypeNum,triggerList);
				return;
			}
			string command="DELETE FROM recalltrigger WHERE RecallTypeNum="+POut.PInt(recallTypeNum);
			Db.NonQ(command);
			for(int i=0;i<triggerList.Count;i++){
				triggerList[i].IsNew=true;
				triggerList[i].RecallTypeNum=recallTypeNum;
				WriteObject(triggerList[i]);
			}
		}

		

	}
}