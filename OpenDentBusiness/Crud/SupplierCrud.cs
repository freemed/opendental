//This file is automatically generated.
//Do not attempt to make changes to this file because the changes will be erased and overwritten.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;

namespace OpenDentBusiness.Crud{
	internal class SupplierCrud {
		///<summary>Gets one Supplier object from the database using the primary key.  Returns null if not found.</summary>
		internal static Supplier SelectOne(long supplierNum){
			string command="SELECT * FROM supplier "
				+"WHERE SupplierNum = "+POut.Long(supplierNum);
			List<Supplier> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets one Supplier object from the database using a query.</summary>
		internal static Supplier SelectOne(string command){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<Supplier> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets a list of Supplier objects from the database using a query.</summary>
		internal static List<Supplier> SelectMany(string command){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<Supplier> list=TableToList(Db.GetTable(command));
			return list;
		}

		///<summary>Converts a DataTable to a list of objects.</summary>
		internal static List<Supplier> TableToList(DataTable table){
			List<Supplier> retVal=new List<Supplier>();
			Supplier supplier;
			for(int i=0;i<table.Rows.Count;i++) {
				supplier=new Supplier();
				supplier.SupplierNum= PIn.Long  (table.Rows[i]["SupplierNum"].ToString());
				supplier.Name       = PIn.String(table.Rows[i]["Name"].ToString());
				supplier.Phone      = PIn.String(table.Rows[i]["Phone"].ToString());
				supplier.CustomerId = PIn.String(table.Rows[i]["CustomerId"].ToString());
				supplier.Website    = PIn.String(table.Rows[i]["Website"].ToString());
				supplier.UserName   = PIn.String(table.Rows[i]["UserName"].ToString());
				supplier.Password   = PIn.String(table.Rows[i]["Password"].ToString());
				supplier.Note       = PIn.String(table.Rows[i]["Note"].ToString());
				retVal.Add(supplier);
			}
			return retVal;
		}

		///<summary>Inserts one Supplier into the database.  Returns the new priKey.</summary>
		internal static long Insert(Supplier supplier){
			return Insert(supplier,false);
		}

		///<summary>Inserts one Supplier into the database.  Provides option to use the existing priKey.</summary>
		internal static long Insert(Supplier supplier,bool useExistingPK){
			if(!useExistingPK && PrefC.RandomKeys) {
				supplier.SupplierNum=ReplicationServers.GetKey("supplier","SupplierNum");
			}
			string command="INSERT INTO supplier (";
			if(useExistingPK || PrefC.RandomKeys) {
				command+="SupplierNum,";
			}
			command+="Name,Phone,CustomerId,Website,UserName,Password,Note) VALUES(";
			if(useExistingPK || PrefC.RandomKeys) {
				command+=POut.Long(supplier.SupplierNum)+",";
			}
			command+=
				 "'"+POut.String(supplier.Name)+"',"
				+"'"+POut.String(supplier.Phone)+"',"
				+"'"+POut.String(supplier.CustomerId)+"',"
				+"'"+POut.String(supplier.Website)+"',"
				+"'"+POut.String(supplier.UserName)+"',"
				+"'"+POut.String(supplier.Password)+"',"
				+"'"+POut.String(supplier.Note)+"')";
			if(useExistingPK || PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				supplier.SupplierNum=Db.NonQ(command,true);
			}
			return supplier.SupplierNum;
		}

		///<summary>Updates one Supplier in the database.</summary>
		internal static void Update(Supplier supplier){
			string command="UPDATE supplier SET "
				+"Name       = '"+POut.String(supplier.Name)+"', "
				+"Phone      = '"+POut.String(supplier.Phone)+"', "
				+"CustomerId = '"+POut.String(supplier.CustomerId)+"', "
				+"Website    = '"+POut.String(supplier.Website)+"', "
				+"UserName   = '"+POut.String(supplier.UserName)+"', "
				+"Password   = '"+POut.String(supplier.Password)+"', "
				+"Note       = '"+POut.String(supplier.Note)+"' "
				+"WHERE SupplierNum = "+POut.Long(supplier.SupplierNum);
			Db.NonQ(command);
		}

		///<summary>Updates one Supplier in the database.  Uses an old object to compare to, and only alters changed fields.  This prevents collisions and concurrency problems in heavily used tables.</summary>
		internal static void Update(Supplier supplier,Supplier oldSupplier){
			string command="";
			if(supplier.Name != oldSupplier.Name) {
				if(command!=""){ command+=",";}
				command+="Name = '"+POut.String(supplier.Name)+"'";
			}
			if(supplier.Phone != oldSupplier.Phone) {
				if(command!=""){ command+=",";}
				command+="Phone = '"+POut.String(supplier.Phone)+"'";
			}
			if(supplier.CustomerId != oldSupplier.CustomerId) {
				if(command!=""){ command+=",";}
				command+="CustomerId = '"+POut.String(supplier.CustomerId)+"'";
			}
			if(supplier.Website != oldSupplier.Website) {
				if(command!=""){ command+=",";}
				command+="Website = '"+POut.String(supplier.Website)+"'";
			}
			if(supplier.UserName != oldSupplier.UserName) {
				if(command!=""){ command+=",";}
				command+="UserName = '"+POut.String(supplier.UserName)+"'";
			}
			if(supplier.Password != oldSupplier.Password) {
				if(command!=""){ command+=",";}
				command+="Password = '"+POut.String(supplier.Password)+"'";
			}
			if(supplier.Note != oldSupplier.Note) {
				if(command!=""){ command+=",";}
				command+="Note = '"+POut.String(supplier.Note)+"'";
			}
			if(command==""){
				return;
			}
			command="UPDATE supplier SET "+command
				+" WHERE SupplierNum = "+POut.Long(supplier.SupplierNum);
			Db.NonQ(command);
		}

		///<summary>Deletes one Supplier from the database.</summary>
		internal static void Delete(long supplierNum){
			string command="DELETE FROM supplier "
				+"WHERE SupplierNum = "+POut.Long(supplierNum);
			Db.NonQ(command);
		}

	}
}