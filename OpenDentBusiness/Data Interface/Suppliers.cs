using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using OpenDentBusiness.DataAccess;

namespace OpenDentBusiness{
	///<summary></summary>
	public class Suppliers {

		///<summary>Gets all Suppliers.</summary>
		public static List<Supplier> CreateObjects() {
			string command="SELECT * FROM supplier ORDER BY Name";
			return new List<Supplier>(DataObjectFactory<Supplier>.CreateObjects(command));
		}

		///<summary></summary>
		public static void WriteObject(Supplier supp){
			DataObjectFactory<Supplier>.WriteObject(supp);
		}

		///<summary>Surround with try-catch.</summary>
		public static void DeleteObject(Supplier supp){
			//validate that not already in use.
			string command="SELECT COUNT(*) FROM supplyorder WHERE SupplierNum="+POut.PInt(supp.SupplierNum);
			int count=PIn.PInt(Db.GetCount(command));
			if(count>0) {
				throw new ApplicationException(Lan.g("Supplies","Supplier is already in use on an order. Not allowed to delete."));
			}
			command="SELECT COUNT(*) FROM supply WHERE SupplierNum="+POut.PInt(supp.SupplierNum);
			count=PIn.PInt(Db.GetCount(command));
			if(count>0) {
				throw new ApplicationException(Lan.g("Supplies","Supplier is already in use on a supply. Not allowed to delete."));
			}
			DataObjectFactory<Supplier>.DeleteObject(supp);
		}

		public static string GetName(List<Supplier> listSupplier,int supplierNum){
			for(int i=0;i<listSupplier.Count;i++){
				if(listSupplier[i].SupplierNum==supplierNum){
					return listSupplier[i].Name;
				}
			}
			return "";
		}


	}

	


	


}









