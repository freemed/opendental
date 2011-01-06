//This file is automatically generated.
//Do not attempt to make changes to this file because the changes will be erased and overwritten.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;

namespace OpenDentBusiness.Crud{
	internal class JournalEntryCrud {
		///<summary>Gets one JournalEntry object from the database using the primary key.  Returns null if not found.</summary>
		internal static JournalEntry SelectOne(long journalEntryNum){
			string command="SELECT * FROM journalentry "
				+"WHERE JournalEntryNum = "+POut.Long(journalEntryNum);
			List<JournalEntry> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets one JournalEntry object from the database using a query.</summary>
		internal static JournalEntry SelectOne(string command){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<JournalEntry> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets a list of JournalEntry objects from the database using a query.</summary>
		internal static List<JournalEntry> SelectMany(string command){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<JournalEntry> list=TableToList(Db.GetTable(command));
			return list;
		}

		///<summary>Converts a DataTable to a list of objects.</summary>
		internal static List<JournalEntry> TableToList(DataTable table){
			List<JournalEntry> retVal=new List<JournalEntry>();
			JournalEntry journalEntry;
			for(int i=0;i<table.Rows.Count;i++) {
				journalEntry=new JournalEntry();
				journalEntry.JournalEntryNum= PIn.Long  (table.Rows[i]["JournalEntryNum"].ToString());
				journalEntry.TransactionNum = PIn.Long  (table.Rows[i]["TransactionNum"].ToString());
				journalEntry.AccountNum     = PIn.Long  (table.Rows[i]["AccountNum"].ToString());
				journalEntry.DateDisplayed  = PIn.Date  (table.Rows[i]["DateDisplayed"].ToString());
				journalEntry.DebitAmt       = PIn.Double(table.Rows[i]["DebitAmt"].ToString());
				journalEntry.CreditAmt      = PIn.Double(table.Rows[i]["CreditAmt"].ToString());
				journalEntry.Memo           = PIn.String(table.Rows[i]["Memo"].ToString());
				journalEntry.Splits         = PIn.String(table.Rows[i]["Splits"].ToString());
				journalEntry.CheckNumber    = PIn.String(table.Rows[i]["CheckNumber"].ToString());
				journalEntry.ReconcileNum   = PIn.Long  (table.Rows[i]["ReconcileNum"].ToString());
				retVal.Add(journalEntry);
			}
			return retVal;
		}

		///<summary>Inserts one JournalEntry into the database.  Returns the new priKey.</summary>
		internal static long Insert(JournalEntry journalEntry){
			return Insert(journalEntry,false);
		}

		///<summary>Inserts one JournalEntry into the database.  Provides option to use the existing priKey.</summary>
		internal static long Insert(JournalEntry journalEntry,bool useExistingPK){
			if(!useExistingPK && PrefC.RandomKeys) {
				journalEntry.JournalEntryNum=ReplicationServers.GetKey("journalentry","JournalEntryNum");
			}
			string command="INSERT INTO journalentry (";
			if(useExistingPK || PrefC.RandomKeys) {
				command+="JournalEntryNum,";
			}
			command+="TransactionNum,AccountNum,DateDisplayed,DebitAmt,CreditAmt,Memo,Splits,CheckNumber,ReconcileNum) VALUES(";
			if(useExistingPK || PrefC.RandomKeys) {
				command+=POut.Long(journalEntry.JournalEntryNum)+",";
			}
			command+=
				     POut.Long  (journalEntry.TransactionNum)+","
				+    POut.Long  (journalEntry.AccountNum)+","
				+    POut.Date  (journalEntry.DateDisplayed)+","
				+"'"+POut.Double(journalEntry.DebitAmt)+"',"
				+"'"+POut.Double(journalEntry.CreditAmt)+"',"
				+"'"+POut.String(journalEntry.Memo)+"',"
				+"'"+POut.String(journalEntry.Splits)+"',"
				+"'"+POut.String(journalEntry.CheckNumber)+"',"
				+    POut.Long  (journalEntry.ReconcileNum)+")";
			if(useExistingPK || PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				journalEntry.JournalEntryNum=Db.NonQ(command,true);
			}
			return journalEntry.JournalEntryNum;
		}

		///<summary>Updates one JournalEntry in the database.</summary>
		internal static void Update(JournalEntry journalEntry){
			string command="UPDATE journalentry SET "
				+"TransactionNum =  "+POut.Long  (journalEntry.TransactionNum)+", "
				+"AccountNum     =  "+POut.Long  (journalEntry.AccountNum)+", "
				+"DateDisplayed  =  "+POut.Date  (journalEntry.DateDisplayed)+", "
				+"DebitAmt       = '"+POut.Double(journalEntry.DebitAmt)+"', "
				+"CreditAmt      = '"+POut.Double(journalEntry.CreditAmt)+"', "
				+"Memo           = '"+POut.String(journalEntry.Memo)+"', "
				+"Splits         = '"+POut.String(journalEntry.Splits)+"', "
				+"CheckNumber    = '"+POut.String(journalEntry.CheckNumber)+"', "
				+"ReconcileNum   =  "+POut.Long  (journalEntry.ReconcileNum)+" "
				+"WHERE JournalEntryNum = "+POut.Long(journalEntry.JournalEntryNum);
			Db.NonQ(command);
		}

		///<summary>Updates one JournalEntry in the database.  Uses an old object to compare to, and only alters changed fields.  This prevents collisions and concurrency problems in heavily used tables.</summary>
		internal static void Update(JournalEntry journalEntry,JournalEntry oldJournalEntry){
			string command="";
			if(journalEntry.TransactionNum != oldJournalEntry.TransactionNum) {
				if(command!=""){ command+=",";}
				command+="TransactionNum = "+POut.Long(journalEntry.TransactionNum)+"";
			}
			if(journalEntry.AccountNum != oldJournalEntry.AccountNum) {
				if(command!=""){ command+=",";}
				command+="AccountNum = "+POut.Long(journalEntry.AccountNum)+"";
			}
			if(journalEntry.DateDisplayed != oldJournalEntry.DateDisplayed) {
				if(command!=""){ command+=",";}
				command+="DateDisplayed = "+POut.Date(journalEntry.DateDisplayed)+"";
			}
			if(journalEntry.DebitAmt != oldJournalEntry.DebitAmt) {
				if(command!=""){ command+=",";}
				command+="DebitAmt = '"+POut.Double(journalEntry.DebitAmt)+"'";
			}
			if(journalEntry.CreditAmt != oldJournalEntry.CreditAmt) {
				if(command!=""){ command+=",";}
				command+="CreditAmt = '"+POut.Double(journalEntry.CreditAmt)+"'";
			}
			if(journalEntry.Memo != oldJournalEntry.Memo) {
				if(command!=""){ command+=",";}
				command+="Memo = '"+POut.String(journalEntry.Memo)+"'";
			}
			if(journalEntry.Splits != oldJournalEntry.Splits) {
				if(command!=""){ command+=",";}
				command+="Splits = '"+POut.String(journalEntry.Splits)+"'";
			}
			if(journalEntry.CheckNumber != oldJournalEntry.CheckNumber) {
				if(command!=""){ command+=",";}
				command+="CheckNumber = '"+POut.String(journalEntry.CheckNumber)+"'";
			}
			if(journalEntry.ReconcileNum != oldJournalEntry.ReconcileNum) {
				if(command!=""){ command+=",";}
				command+="ReconcileNum = "+POut.Long(journalEntry.ReconcileNum)+"";
			}
			if(command==""){
				return;
			}
			command="UPDATE journalentry SET "+command
				+" WHERE JournalEntryNum = "+POut.Long(journalEntry.JournalEntryNum);
			Db.NonQ(command);
		}

		///<summary>Deletes one JournalEntry from the database.</summary>
		internal static void Delete(long journalEntryNum){
			string command="DELETE FROM journalentry "
				+"WHERE JournalEntryNum = "+POut.Long(journalEntryNum);
			Db.NonQ(command);
		}

	}
}