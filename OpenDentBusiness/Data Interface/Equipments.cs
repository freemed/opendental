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
				equip.EquipmentNum = PIn.Long(table.Rows[i][0].ToString());
				equip.Description  = PIn.String(table.Rows[i][1].ToString());
				equip.SerialNumber = PIn.String(table.Rows[i][2].ToString());
				equip.ModelYear    = PIn.String(table.Rows[i][3].ToString());
				equip.DatePurchased= PIn.Date(table.Rows[i][4].ToString());
				equip.DateSold     = PIn.Date(table.Rows[i][5].ToString());
				equip.PurchaseCost = PIn.Double(table.Rows[i][6].ToString());
				equip.MarketValue  = PIn.Double(table.Rows[i][7].ToString());
				equip.Location     = PIn.String(table.Rows[i][8].ToString());
				list.Add(equip);
			}
			return list;
		}

		///<summary></summary>
		public static long Insert(Equipment equip) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				equip.EquipmentNum=Meth.GetLong(MethodBase.GetCurrentMethod(),equip);
				return equip.EquipmentNum;
			}
			if(PrefC.RandomKeys) {
				equip.EquipmentNum=ReplicationServers.GetKey("equipment","EquipmentNum");
			}
			string command="INSERT INTO equipment (";
			if(PrefC.RandomKeys) {
				command+="EquipmentNum,";
			}
			command+="Description,SerialNumber,ModelYear,DatePurchased,DateSold,PurchaseCost,MarketValue,Location) VALUES(";
			if(PrefC.RandomKeys) {
				command+=POut.Long(equip.EquipmentNum)+", ";
			}
			/*
			command+=
				 "'"+POut.PString(equip.Description)+"', "
				+"'"+POut.PString(equip.SerialNumber)+"', "
				+"'"+POut(equip.ModelYear)+"', "
				+"'"+POut(equip.DatePurchased)+"', "
				+"'"+POut(equip.DateSold)+"', "
				+"'"+POut(equip.PurchaseCost)+"', "
				+"'"+POut(equip.MarketValue)+"', "
				+"'"+POut(equip.Location)+"', "
				+"'"+POut(equip)+"', "


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
			}*/
			return equip.EquipmentNum;
		}

		/*
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













