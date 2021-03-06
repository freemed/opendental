//This file is automatically generated.
//Do not attempt to make changes to this file because the changes will be erased and overwritten.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;

namespace OpenDentBusiness.Crud{
	public class AccountCrud {
		///<summary>Gets one Account object from the database using the primary key.  Returns null if not found.</summary>
		public static Account SelectOne(long accountNum){
			string command="SELECT * FROM account "
				+"WHERE AccountNum = "+POut.Long(accountNum);
			List<Account> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets one Account object from the database using a query.</summary>
		public static Account SelectOne(string command){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<Account> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets a list of Account objects from the database using a query.</summary>
		public static List<Account> SelectMany(string command){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<Account> list=TableToList(Db.GetTable(command));
			return list;
		}

		///<summary>Converts a DataTable to a list of objects.</summary>
		public static List<Account> TableToList(DataTable table){
			List<Account> retVal=new List<Account>();
			Account account;
			for(int i=0;i<table.Rows.Count;i++) {
				account=new Account();
				account.AccountNum  = PIn.Long  (table.Rows[i]["AccountNum"].ToString());
				account.Description = PIn.String(table.Rows[i]["Description"].ToString());
				account.AcctType    = (AccountType)PIn.Int(table.Rows[i]["AcctType"].ToString());
				account.BankNumber  = PIn.String(table.Rows[i]["BankNumber"].ToString());
				account.Inactive    = PIn.Bool  (table.Rows[i]["Inactive"].ToString());
				account.AccountColor= Color.FromArgb(PIn.Int(table.Rows[i]["AccountColor"].ToString()));
				retVal.Add(account);
			}
			return retVal;
		}

		///<summary>Inserts one Account into the database.  Returns the new priKey.</summary>
		public static long Insert(Account account){
			if(DataConnection.DBtype==DatabaseType.Oracle) {
				account.AccountNum=DbHelper.GetNextOracleKey("account","AccountNum");
				int loopcount=0;
				while(loopcount<100){
					try {
						return Insert(account,true);
					}
					catch(Oracle.DataAccess.Client.OracleException ex){
						if(ex.Number==1 && ex.Message.ToLower().Contains("unique constraint") && ex.Message.ToLower().Contains("violated")){
							account.AccountNum++;
							loopcount++;
						}
						else{
							throw ex;
						}
					}
				}
				throw new ApplicationException("Insert failed.  Could not generate primary key.");
			}
			else {
				return Insert(account,false);
			}
		}

		///<summary>Inserts one Account into the database.  Provides option to use the existing priKey.</summary>
		public static long Insert(Account account,bool useExistingPK){
			if(!useExistingPK && PrefC.RandomKeys) {
				account.AccountNum=ReplicationServers.GetKey("account","AccountNum");
			}
			string command="INSERT INTO account (";
			if(useExistingPK || PrefC.RandomKeys) {
				command+="AccountNum,";
			}
			command+="Description,AcctType,BankNumber,Inactive,AccountColor) VALUES(";
			if(useExistingPK || PrefC.RandomKeys) {
				command+=POut.Long(account.AccountNum)+",";
			}
			command+=
				 "'"+POut.String(account.Description)+"',"
				+    POut.Int   ((int)account.AcctType)+","
				+"'"+POut.String(account.BankNumber)+"',"
				+    POut.Bool  (account.Inactive)+","
				+    POut.Int   (account.AccountColor.ToArgb())+")";
			if(useExistingPK || PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				account.AccountNum=Db.NonQ(command,true);
			}
			return account.AccountNum;
		}

		///<summary>Updates one Account in the database.</summary>
		public static void Update(Account account){
			string command="UPDATE account SET "
				+"Description = '"+POut.String(account.Description)+"', "
				+"AcctType    =  "+POut.Int   ((int)account.AcctType)+", "
				+"BankNumber  = '"+POut.String(account.BankNumber)+"', "
				+"Inactive    =  "+POut.Bool  (account.Inactive)+", "
				+"AccountColor=  "+POut.Int   (account.AccountColor.ToArgb())+" "
				+"WHERE AccountNum = "+POut.Long(account.AccountNum);
			Db.NonQ(command);
		}

		///<summary>Updates one Account in the database.  Uses an old object to compare to, and only alters changed fields.  This prevents collisions and concurrency problems in heavily used tables.</summary>
		public static void Update(Account account,Account oldAccount){
			string command="";
			if(account.Description != oldAccount.Description) {
				if(command!=""){ command+=",";}
				command+="Description = '"+POut.String(account.Description)+"'";
			}
			if(account.AcctType != oldAccount.AcctType) {
				if(command!=""){ command+=",";}
				command+="AcctType = "+POut.Int   ((int)account.AcctType)+"";
			}
			if(account.BankNumber != oldAccount.BankNumber) {
				if(command!=""){ command+=",";}
				command+="BankNumber = '"+POut.String(account.BankNumber)+"'";
			}
			if(account.Inactive != oldAccount.Inactive) {
				if(command!=""){ command+=",";}
				command+="Inactive = "+POut.Bool(account.Inactive)+"";
			}
			if(account.AccountColor != oldAccount.AccountColor) {
				if(command!=""){ command+=",";}
				command+="AccountColor = "+POut.Int(account.AccountColor.ToArgb())+"";
			}
			if(command==""){
				return;
			}
			command="UPDATE account SET "+command
				+" WHERE AccountNum = "+POut.Long(account.AccountNum);
			Db.NonQ(command);
		}

		///<summary>Deletes one Account from the database.</summary>
		public static void Delete(long accountNum){
			string command="DELETE FROM account "
				+"WHERE AccountNum = "+POut.Long(accountNum);
			Db.NonQ(command);
		}

	}
}