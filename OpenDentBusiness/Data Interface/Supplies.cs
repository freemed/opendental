using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Reflection;

namespace OpenDentBusiness{
	///<summary></summary>
	public class Supplies {

		///<summary>Gets all Supplies, ordered by category and itemOrder.  Optionally hides those marked IsHidden.  FindText must only include alphanumeric characters.</summary>
		public static List<Supply> CreateObjects(bool includeHidden,long supplierNum,string findText) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<Supply>>(MethodBase.GetCurrentMethod(),includeHidden,supplierNum,findText);
			}
			string command="SELECT supply.* FROM supply,definition "
				+"WHERE definition.DefNum=supply.Category "
				+"AND supply.SupplierNum="+POut.Long(supplierNum)+" ";
			if(findText!=""){
				command+="AND ("+DbHelper.Regexp("supply.CatalogNumber",POut.String(findText))+" OR "+DbHelper.Regexp("supply.Descript",POut.String(findText))+") ";
			}
			if(!includeHidden){
				command+="AND supply.IsHidden=0 ";
			}
			command+="ORDER BY definition.ItemOrder,supply.ItemOrder";
			return Crud.SupplyCrud.SelectMany(command);
		}

		///<Summary>Gets one supply from the database.  Used for display in SupplyOrderItemEdit window.</Summary>
		public static Supply GetSupply(long supplyNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<Supply>(MethodBase.GetCurrentMethod(),supplyNum);
			}
			return Crud.SupplyCrud.SelectOne(supplyNum);
		}

		///<summary></summary>
		public static long Insert(Supply supp){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				supp.SupplyNum=Meth.GetLong(MethodBase.GetCurrentMethod(),supp);
				return supp.SupplyNum;
			}
			return Crud.SupplyCrud.Insert(supp);
		}

		///<summary></summary>
		public static void Update(Supply supp) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),supp);
				return;
			}
			Crud.SupplyCrud.Update(supp);
		}

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

		///<Summary>Gets from the database the last itemOrder for the specified category.  Used to send un unhidden supply to the end of the list.</Summary>
		public static int GetLastItemOrder(long supplierNum,long catNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetInt(MethodBase.GetCurrentMethod(),supplierNum,catNum);
			}
			string command="SELECT MAX(ItemOrder) FROM supply WHERE SupplierNum="+POut.Long(supplierNum)
				+" AND Category="+POut.Long(catNum)+" AND IsHidden=0";
			DataTable table=Db.GetTable(command);
			if(table.Rows.Count==0){
				return -1;
			}
			return PIn.Int(table.Rows[0][0].ToString());
		}

		

		
		


	}

	


	


}









