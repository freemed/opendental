using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Reflection;
using OpenDentBusiness.DataAccess;

namespace OpenDentBusiness{
	///<summary></summary>
	public class Suppliers {

		///<summary>Gets all Suppliers.</summary>
		public static List<Supplier> CreateObjects() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<Supplier>>(MethodBase.GetCurrentMethod());
			}
			string command="SELECT * FROM supplier ORDER BY Name";
			return new List<Supplier>(DataObjectFactory<Supplier>.CreateObjects(command));
		}

		///<summary></summary>
		public static int WriteObject(Supplier supp){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				supp.SupplierNum=Meth.GetInt(MethodBase.GetCurrentMethod(),supp);
				return supp.SupplierNum;
			}
			DataObjectFactory<Supplier>.WriteObject(supp);
			return supp.SupplierNum;
		}

		///<summary>Surround with try-catch.</summary>
		public static void DeleteObject(Supplier supp){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),supp);
				return;
			}
			//validate that not already in use.
			string command="SELECT COUNT(*) FROM supplyorder WHERE SupplierNum="+POut.PInt(supp.SupplierNum);
			int count=PIn.PInt(Db.GetCount(command));
			if(count>0) {
				throw new ApplicationException(Lans.g("Supplies","Supplier is already in use on an order. Not allowed to delete."));
			}
			command="SELECT COUNT(*) FROM supply WHERE SupplierNum="+POut.PInt(supp.SupplierNum);
			count=PIn.PInt(Db.GetCount(command));
			if(count>0) {
				throw new ApplicationException(Lans.g("Supplies","Supplier is already in use on a supply. Not allowed to delete."));
			}
			DataObjectFactory<Supplier>.DeleteObject(supp);
		}

		public static string GetName(List<Supplier> listSupplier,int supplierNum){
			//No need to check RemotingRole; no call to db.
			for(int i=0;i<listSupplier.Count;i++){
				if(listSupplier[i].SupplierNum==supplierNum){
					return listSupplier[i].Name;
				}
			}
			return "";
		}


	}

	


	


}









