using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Reflection;

namespace OpenDentBusiness{
	///<summary></summary>
	public class Supplies {

		/////<summary>Gets all Supplies, ordered by category and itemOrder.  Optionally hides those marked IsHidden.  FindText must only include alphanumeric characters.</summary>
		//public static List<Supply> CreateObjects(bool includeHidden,long supplierNum,string findText) {
		//	if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
		//		return Meth.GetObject<List<Supply>>(MethodBase.GetCurrentMethod(),includeHidden,supplierNum,findText);
		//	}
		//	string command="SELECT supply.* FROM supply,definition "
		//		+"WHERE definition.DefNum=supply.Category "
		//		+"AND supply.SupplierNum="+POut.Long(supplierNum)+" ";
		//	if(findText!=""){
		//		command+="AND ("+DbHelper.Regexp("supply.CatalogNumber",POut.String(findText))+" OR "+DbHelper.Regexp("supply.Descript",POut.String(findText))+") ";
		//	}
		//	if(!includeHidden){
		//		command+="AND supply.IsHidden=0 ";
		//	}
		//	command+="ORDER BY definition.ItemOrder,supply.ItemOrder";
		//	return Crud.SupplyCrud.SelectMany(command);
		//}

		///<summary>Gets all Supplies, ordered by category and itemOrder.</summary>
		public static List<Supply> GetAll() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<Supply>>(MethodBase.GetCurrentMethod());
			}
			string command="SELECT supply.* FROM supply, definition "
				+"WHERE definition.DefNum=supply.Category "
				+"ORDER BY definition.ItemOrder,supply.ItemOrder";
			return Crud.SupplyCrud.SelectMany(command);
		}

		///<Summary>Gets one supply from the database.  Used for display in SupplyOrderItemEdit window.</Summary>
		public static Supply GetSupply(long supplyNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<Supply>(MethodBase.GetCurrentMethod(),supplyNum);
			}
			return Crud.SupplyCrud.SelectOne(supplyNum);
		}

		///<summary>Insert new item at bottom of category.</summary>
		public static long Insert(Supply supp){
			return Insert(supp, int.MaxValue);
		}

		///<summary>Inserts item at corresponding itemOrder. If item order is out of range, item will be placed at beginning or end of category.</summary>
		public static long Insert(Supply supp, int itemOrder) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				supp.SupplyNum=Meth.GetLong(MethodBase.GetCurrentMethod(),supp,itemOrder);
				return supp.SupplyNum;
			}
			string command="";
			if(itemOrder<0) {
				itemOrder=0;
			}
			else {
				command="SELECT MAX(ItemOrder) FROM supply WHERE Category="+POut.Long(supp.Category);
				int itemOrderMax=PIn.Int(Db.GetScalar(command));
				if(itemOrder>itemOrderMax) {
					itemOrder=itemOrderMax+1;
				}
			}
			//Set new item order.
			supp.ItemOrder=itemOrder;
			//move other items
			command="UPDATE supply SET ItemOrder=(ItemOrder+1) WHERE Category="+POut.Long(supp.Category)+" AND ItemOrder>="+POut.Int(supp.ItemOrder);
			Db.NonQ(command);
			//insert and return new supply
			return Crud.SupplyCrud.Insert(supp);
		}

		///<summary>Standard update logic.</summary>
		public static void Update(Supply supp) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),supp);
				return;
			}
			Crud.SupplyCrud.Update(supp);
		}

		///<summary>Updates and also fixes item order issues that may arise from changing categories.</summary>
		public static void Update(Supply suppOld, Supply suppNew) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),suppOld,suppNew);
				return;
			}
			string command="";
			if(suppNew.ItemOrder<0 || suppNew.ItemOrder==int.MaxValue) {
				command="SELECT MAX(ItemOrder) FROM supply WHERE Category="+POut.Long(suppNew.Category);
				int itemOrderMax=PIn.Int(Db.GetScalar(command));
				if(suppNew.ItemOrder>itemOrderMax) {
					suppNew.ItemOrder=itemOrderMax+1;
				}
			}
			//move other items
			if(suppNew.Category==suppOld.Category) {//move within same category
				command="UPDATE supply SET ItemOrder=(ItemOrder+1)"
				+" WHERE Category="+POut.Long(suppNew.Category)
				+" AND ItemOrder>="+POut.Int(suppNew.ItemOrder)
				+" AND ItemOrder<="+POut.Int(suppOld.ItemOrder);
				Db.NonQ(command);//may have overlapping item orders until suppNew is updated at the end of this function.
			}
			else {//move between categories
				//Move all item orders down one form the old list
				command="UPDATE supply SET ItemOrder=(ItemOrder-1)"
				+" WHERE Category="+POut.Long(suppOld.Category)
				+" AND ItemOrder>="+POut.Int(suppOld.ItemOrder);
				Db.NonQ(command);
				//Assuming item order is correct adjust new list item orders
				command="UPDATE supply SET ItemOrder=(ItemOrder+1)"
				+" WHERE Category="+POut.Long(suppNew.Category)
				+" AND ItemOrder>="+POut.Int(suppNew.ItemOrder);
				Db.NonQ(command);
			}
			Crud.SupplyCrud.Update(suppNew);
		}

		///<summary>Updates item orders only.  One query is run per supply item. If supplyOriginal is null will insert supplyNew.</summary>
		public static void UpdateOrInsertIfNeeded(Supply supplyOriginal,Supply supplyNew) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),supplyOriginal,supplyNew);
				return;
			}
			if(supplyNew.IsNew
				||supplyNew.SupplyNum==0//if for some reason the SupplyNum didn't get set.
				||supplyOriginal==null) 
			{
				Crud.SupplyCrud.Insert(supplyNew);//probably wont happen but I'm not sure.
			}
			else if(supplyOriginal.CatalogNumber!=supplyNew.CatalogNumber
						||supplyOriginal.Category			!=supplyNew.Category
						||supplyOriginal.Descript			!=supplyNew.Descript
						||supplyOriginal.IsHidden			!=supplyNew.IsHidden
						||supplyOriginal.ItemOrder		!=supplyNew.ItemOrder
						||supplyOriginal.LevelDesired	!=supplyNew.LevelDesired
						||supplyOriginal.Price				!=supplyNew.Price
						||supplyOriginal.SupplierNum	!=supplyNew.SupplierNum)
			{
				Crud.SupplyCrud.Update(supplyNew);
			}
			//No update or insert needed.
		}

		///<summary>Removes gaps and overlaps in item orders with a single call to the DB. Example: 1,2,5,5,13 becomes 0,1,2,3,4; the overlaps are sorted arbitrarily.</summary>
//		public static void NormalizeItemOrders() {
//			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
//				Meth.GetVoid(MethodBase.GetCurrentMethod());
//				return;
//			}
//			string command="SET @newOrder=0";
//			Db.NonQ(command);
//			command=@"SELECT @newOrder:=@newOrder+1 AS newOrder, t.supplynum, t.* 
//								FROM 
//									(SELECT supply.* 
//									FROM supply 
//									LEFT JOIN definition ON supply.category=definition.defnum 
//									ORDER BY definition.itemorder,supply.itemorder) t";
//			Db.NonQ(command);
//		}

		///<summary>Surround with try-catch.</summary>
		public static void DeleteObject(Supply supp){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),supp);
				return;
			}
			//validate that not already in use.
			string command="SELECT COUNT(*) FROM supplyorderitem WHERE SupplyNum="+POut.Long(supp.SupplyNum);
			int count=PIn.Int(Db.GetCount(command));
			if(count>0){
				throw new ApplicationException(Lans.g("Supplies","Supply is already in use on an order. Not allowed to delete."));
			}
			Crud.SupplyCrud.Delete(supp.SupplyNum);
			command="UPDATE supply SET ItemOrder=(ItemOrder-1) WHERE Category="+POut.Long(supp.Category)+" AND ItemOrder>="+POut.Int(supp.ItemOrder);
			Db.NonQ(command);
		}

		///<Summary>Loops through the supplied list and verifies that the ItemOrders are not corrupted.  If they are, then it fixes them.  Returns true if fix was required.  It makes a few assumptions about the quality of the list supplied.  Specifically, that there are no missing items, and that categories are grouped and sorted.</Summary>
		public static bool CleanupItemOrders(List<Supply> listSupply){
			//No need to check RemotingRole; no call to db.
			long catCur=-1;
			int previousOrder=-1;
			bool retVal=false;
			for(int i=0;i<listSupply.Count;i++){
				if(listSupply[i].Category!=catCur){//start of new category
					catCur=listSupply[i].Category;
					previousOrder=-1;
				}
				if(listSupply[i].ItemOrder!=previousOrder+1){
					listSupply[i].ItemOrder=previousOrder+1;
					Update(listSupply[i]);
					retVal=true;
				}
				previousOrder++;
			}
			return retVal;
		}

		///<Summary>Gets from the database the next available itemOrder for the specified category.  Returns 0 if no items found in category.</Summary>
		public static int GetNextItemOrder(long catNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetInt(MethodBase.GetCurrentMethod(),catNum);
			}
			string command="SELECT MAX(ItemOrder+1) FROM supply WHERE Category="+POut.Long(catNum);
			try {
				return PIn.Int(Db.GetScalar(command));
			}
			catch(Exception ex){
				return 0;
			}
		}

		/////<Summary>Deprecated.  Gets from the database the last itemOrder for the specified category.  Used to send un unhidden supply to the end of the list.</Summary>
		//public static int GetLastItemOrder(long supplierNum,long catNum) {
		//	if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
		//		return Meth.GetInt(MethodBase.GetCurrentMethod(),supplierNum,catNum);
		//	}
		//	string command="SELECT MAX(ItemOrder) FROM supply WHERE SupplierNum="+POut.Long(supplierNum)
		//		+" AND Category="+POut.Long(catNum);// +" AND IsHidden=0";
		//	DataTable table=Db.GetTable(command);
		//	if(table.Rows.Count==0){
		//		return -1;
		//	}
		//	return PIn.Int(table.Rows[0][0].ToString());
		//}

		

		
		


	}

	


	


}









