using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Windows.Forms;

namespace OpenDentBusiness{
	///<summary></summary>
	public class Automations {
		public static List<Automation> Listt;

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
			Listt=new List<Automation>();
			Automation auto;
			for(int i=0;i<table.Rows.Count;i++) {
				auto=new Automation();
				auto.AutomationNum = PIn.PLong(table.Rows[i][0].ToString());
				auto.Description   = PIn.PString(table.Rows[i][1].ToString());
				auto.AutoTrigger   = (AutomationTrigger)PIn.PInt(table.Rows[i][2].ToString());
				auto.ProcCodes     = PIn.PString(table.Rows[i][3].ToString());
				auto.AutoAction    = (AutomationAction)PIn.PInt(table.Rows[i][4].ToString());
				auto.SheetNum      = PIn.PLong(table.Rows[i][5].ToString());
				auto.CommType      = PIn.PLong(table.Rows[i][6].ToString());
				auto.MessageContent= PIn.PString(table.Rows[i][7].ToString());
				Listt.Add(auto);
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
			command+="Description,AutoTrigger,ProcCodes,AutoAction,SheetNum,CommType,MessageContent) VALUES(";
			if(PrefC.RandomKeys) {
				command+=POut.PLong(auto.AutomationNum)+", ";
			}
			command+=
				 "'"+POut.PString(auto.Description)+"', "
				+"'"+POut.PInt((int)auto.AutoTrigger)+"', "
				+"'"+POut.PString(auto.ProcCodes)+"', "
				+"'"+POut.PInt((int)auto.AutoAction)+"', "
				+"'"+POut.PLong(auto.SheetNum)+"', "
				+"'"+POut.PLong(auto.CommType)+"', "
				+"'"+POut.PString(auto.MessageContent)+"')";
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
				+ "Description = '"   +POut.PString(auto.Description)+"'"
				+ ",AutoTrigger = '"  +POut.PInt((int)auto.AutoTrigger)+"'"
				+ ",ProcCodes = '"    +POut.PString(auto.ProcCodes)+"'"
				+ ",AutoAction = '"   +POut.PInt((int)auto.AutoAction)+"'"
				+ ",SheetNum = '"     +POut.PLong(auto.SheetNum)+"'"
				+ ",CommType = '"     +POut.PLong(auto.CommType)+"'"
				+ ",MessageContent = '" +POut.PString(auto.MessageContent)+"'"
				+" WHERE AutomationNum = '" +POut.PLong   (auto.AutomationNum)+"'";
 			Db.NonQ(command);
		}

		///<summary></summary>
		public static void Delete(Automation auto) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),auto);
				return;
			}
			string command="DELETE FROM automation" 
				+" WHERE AutomationNum = "+POut.PLong(auto.AutomationNum);
 			Db.NonQ(command);
		}


		
		

	}
	


}













