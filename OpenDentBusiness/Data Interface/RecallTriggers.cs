using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Reflection;

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
				trig.RecallTriggerNum = PIn.Long   (table.Rows[i][0].ToString());
				trig.RecallTypeNum    = PIn.Long   (table.Rows[i][1].ToString());
				trig.CodeNum          = PIn.Long   (table.Rows[i][2].ToString());
				RecallTriggerC.Listt.Add(trig);
			}
		}

		///<summary></summary>
		public static long Insert(RecallTrigger trigger) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				trigger.RecallTriggerNum=Meth.GetLong(MethodBase.GetCurrentMethod(),trigger);
				return trigger.RecallTriggerNum;
			}
			return Crud.RecallTriggerCrud.Insert(trigger);
		}

		/*
		///<summary></summary>
		public static void Update(RecallTrigger trigger) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),trigger);
				return;
			}
			Crud.RecallTriggerCrud.Update(trigger);
		}*/

		public static List<RecallTrigger> GetForType(long recallTypeNum) {
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

		public static void SetForType(long recallTypeNum,List<RecallTrigger> triggerList) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),recallTypeNum,triggerList);
				return;
			}
			string command="DELETE FROM recalltrigger WHERE RecallTypeNum="+POut.Long(recallTypeNum);
			Db.NonQ(command);
			for(int i=0;i<triggerList.Count;i++){
				triggerList[i].RecallTypeNum=recallTypeNum;
				Insert(triggerList[i]);
			}
		}

		

	}
}