using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Windows.Forms;

namespace OpenDentBusiness{
	///<summary></summary>
	public class Equipments {

		///<summary></summary>
		public static List<Equipment> GetList() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<Equipment>>(MethodBase.GetCurrentMethod());
			}
			string command="SELECT * FROM equipment ORDER BY DatePurchased";
			DataTable table=Db.GetTable(command);
			List<Equipment> list=new List<Equipment>();
			Equipment equip;
			for(int i=0;i<table.Rows.Count;i++) {
				equip=new Equipment();
				equip.EquipmentNum = PIn.PLong(table.Rows[i][0].ToString());
				equip.Description  = PIn.PString(table.Rows[i][1].ToString());
				equip.SerialNumber = PIn.PString(table.Rows[i][2].ToString());
				equip.ModelYear    = PIn.PString(table.Rows[i][3].ToString());
				equip.DatePurchased= PIn.PDate(table.Rows[i][4].ToString());
				equip.DateSold     = PIn.PDate(table.Rows[i][5].ToString());
				equip.PurchaseCost = PIn.PDouble(table.Rows[i][6].ToString());
				equip.MarketValue  = PIn.PDouble(table.Rows[i][7].ToString());
				equip.Location     = PIn.PString(table.Rows[i][8].ToString());
				list.Add(equip);
			}
			return list;
		}


		/*
		///<summary></summary>
		public static long Insert(equipment auto) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				auto.equipmentNum=Meth.GetLong(MethodBase.GetCurrentMethod(),auto);
				return auto.equipmentNum;
			}
			if(PrefC.RandomKeys) {
				auto.equipmentNum=ReplicationServers.GetKey("equipment","equipmentNum");
			}
			string command="INSERT INTO equipment (";
			if(PrefC.RandomKeys) {
				command+="equipmentNum,";
			}
			command+="Description,AutoTrigger,ProcCodes,AutoAction,SheetDefNum,CommType,MessageContent) VALUES(";
			if(PrefC.RandomKeys) {
				command+=POut.PLong(auto.equipmentNum)+", ";
			}
			command+=
				 "'"+POut.PString(auto.Description)+"', "
				+"'"+POut.PInt((int)auto.AutoTrigger)+"', "
				+"'"+POut.PString(auto.ProcCodes)+"', "
				+"'"+POut.PInt((int)auto.AutoAction)+"', "
				+"'"+POut.PLong(auto.SheetDefNum)+"', "
				+"'"+POut.PLong(auto.CommType)+"', "
				+"'"+POut.PString(auto.MessageContent)+"')";
			if(PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				auto.equipmentNum=Db.NonQ(command,true);
			}
			return auto.equipmentNum;
		}

		///<summary></summary>
		public static void Update(equipment auto) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),auto);
				return;
			}
			string command= "UPDATE equipment SET " 
				+ "Description = '"   +POut.PString(auto.Description)+"'"
				+ ",AutoTrigger = '"  +POut.PInt((int)auto.AutoTrigger)+"'"
				+ ",ProcCodes = '"    +POut.PString(auto.ProcCodes)+"'"
				+ ",AutoAction = '"   +POut.PInt((int)auto.AutoAction)+"'"
				+ ",SheetDefNum = '"  +POut.PLong(auto.SheetDefNum)+"'"
				+ ",CommType = '"     +POut.PLong(auto.CommType)+"'"
				+ ",MessageContent = '" +POut.PString(auto.MessageContent)+"'"
				+" WHERE equipmentNum = '" +POut.PLong   (auto.equipmentNum)+"'";
 			Db.NonQ(command);
		}

		///<summary></summary>
		public static void Delete(equipment auto) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),auto);
				return;
			}
			string command="DELETE FROM equipment" 
				+" WHERE equipmentNum = "+POut.PLong(auto.equipmentNum);
 			Db.NonQ(command);
		}
		*/

		
		

	}
	


}













