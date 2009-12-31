using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Windows.Forms;

namespace OpenDentBusiness{
	///<summary></summary>
	public class Automations {
		private static List<Automation> listt;

		public static List<Automation> Listt {
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
		public static DataTable RefreshCache() {
			//No need to check RemotingRole; Calls GetTableRemotelyIfNeeded().
			string command="SELECT * FROM automation";
			DataTable table=Cache.GetTableRemotelyIfNeeded(MethodBase.GetCurrentMethod(),command);
			table.TableName="Automation";
			FillCache(table);
			return table;
		}

		public static void FillCache(DataTable table){
			//No need to check RemotingRole; no call to db.
			listt=new List<Automation>();
			Automation auto;
			for(int i=0;i<table.Rows.Count;i++) {
				auto=new Automation();
				auto.AutomationNum = PIn.Long(table.Rows[i][0].ToString());
				auto.Description   = PIn.String(table.Rows[i][1].ToString());
				auto.AutoTrigger   = (AutomationTrigger)PIn.Int(table.Rows[i][2].ToString());
				auto.ProcCodes     = PIn.String(table.Rows[i][3].ToString());
				auto.AutoAction    = (AutomationAction)PIn.Int(table.Rows[i][4].ToString());
				auto.SheetDefNum   = PIn.Long(table.Rows[i][5].ToString());
				auto.CommType      = PIn.Long(table.Rows[i][6].ToString());
				auto.MessageContent= PIn.String(table.Rows[i][7].ToString());
				listt.Add(auto);
			}
		}

		///<summary></summary>
		public static long Insert(Automation auto) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				auto.AutomationNum=Meth.GetLong(MethodBase.GetCurrentMethod(),auto);
				return auto.AutomationNum;
			}
			if(PrefC.RandomKeys) {
				auto.AutomationNum=ReplicationServers.GetKey("automation","AutomationNum");
			}
			string command="INSERT INTO automation (";
			if(PrefC.RandomKeys) {
				command+="AutomationNum,";
			}
			command+="Description,AutoTrigger,ProcCodes,AutoAction,SheetDefNum,CommType,MessageContent) VALUES(";
			if(PrefC.RandomKeys) {
				command+=POut.Long(auto.AutomationNum)+", ";
			}
			command+=
				 "'"+POut.String(auto.Description)+"', "
				+"'"+POut.Int((int)auto.AutoTrigger)+"', "
				+"'"+POut.String(auto.ProcCodes)+"', "
				+"'"+POut.Int((int)auto.AutoAction)+"', "
				+"'"+POut.Long(auto.SheetDefNum)+"', "
				+"'"+POut.Long(auto.CommType)+"', "
				+"'"+POut.String(auto.MessageContent)+"')";
			if(PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				auto.AutomationNum=Db.NonQ(command,true);
			}
			return auto.AutomationNum;
		}

		///<summary></summary>
		public static void Update(Automation auto) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),auto);
				return;
			}
			string command= "UPDATE automation SET " 
				+ "Description = '"   +POut.String(auto.Description)+"'"
				+ ",AutoTrigger = '"  +POut.Int((int)auto.AutoTrigger)+"'"
				+ ",ProcCodes = '"    +POut.String(auto.ProcCodes)+"'"
				+ ",AutoAction = '"   +POut.Int((int)auto.AutoAction)+"'"
				+ ",SheetDefNum = '"  +POut.Long(auto.SheetDefNum)+"'"
				+ ",CommType = '"     +POut.Long(auto.CommType)+"'"
				+ ",MessageContent = '" +POut.String(auto.MessageContent)+"'"
				+" WHERE AutomationNum = '" +POut.Long   (auto.AutomationNum)+"'";
 			Db.NonQ(command);
		}

		///<summary></summary>
		public static void Delete(Automation auto) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),auto);
				return;
			}
			string command="DELETE FROM automation" 
				+" WHERE AutomationNum = "+POut.Long(auto.AutomationNum);
 			Db.NonQ(command);
		}


		
		

	}
	


}













