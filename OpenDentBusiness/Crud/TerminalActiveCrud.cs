//This file is automatically generated.
//Do not attempt to make changes to this file because the changes will be erased and overwritten.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;

namespace OpenDentBusiness.Crud{
	internal class TerminalActiveCrud {
		///<summary>Gets one TerminalActive object from the database using the primary key.  Returns null if not found.</summary>
		internal static TerminalActive SelectOne(long terminalActiveNum){
			string command="SELECT * FROM terminalactive "
				+"WHERE TerminalActiveNum = "+POut.Long(terminalActiveNum);
			List<TerminalActive> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets one TerminalActive object from the database using a query.</summary>
		internal static TerminalActive SelectOne(string command){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<TerminalActive> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets a list of TerminalActive objects from the database using a query.</summary>
		internal static List<TerminalActive> SelectMany(string command){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<TerminalActive> list=TableToList(Db.GetTable(command));
			return list;
		}

		///<summary>Converts a DataTable to a list of objects.</summary>
		internal static List<TerminalActive> TableToList(DataTable table){
			List<TerminalActive> retVal=new List<TerminalActive>();
			TerminalActive terminalActive;
			for(int i=0;i<table.Rows.Count;i++) {
				terminalActive=new TerminalActive();
				terminalActive.TerminalActiveNum= PIn.Long  (table.Rows[i]["TerminalActiveNum"].ToString());
				terminalActive.ComputerName     = PIn.String(table.Rows[i]["ComputerName"].ToString());
				terminalActive.TerminalStatus   = (TerminalStatusEnum)PIn.Int(table.Rows[i]["TerminalStatus"].ToString());
				terminalActive.PatNum           = PIn.Long  (table.Rows[i]["PatNum"].ToString());
				retVal.Add(terminalActive);
			}
			return retVal;
		}

		///<summary>Inserts one TerminalActive into the database.  Returns the new priKey.</summary>
		internal static long Insert(TerminalActive terminalActive){
			return Insert(terminalActive,false);
		}

		///<summary>Inserts one TerminalActive into the database.  Provides option to use the existing priKey.</summary>
		internal static long Insert(TerminalActive terminalActive,bool useExistingPK){
			if(!useExistingPK && PrefC.RandomKeys) {
				terminalActive.TerminalActiveNum=ReplicationServers.GetKey("terminalactive","TerminalActiveNum");
			}
			string command="INSERT INTO terminalactive (";
			if(useExistingPK || PrefC.RandomKeys) {
				command+="TerminalActiveNum,";
			}
			command+="ComputerName,TerminalStatus,PatNum) VALUES(";
			if(useExistingPK || PrefC.RandomKeys) {
				command+=POut.Long(terminalActive.TerminalActiveNum)+",";
			}
			command+=
				 "'"+POut.String(terminalActive.ComputerName)+"',"
				+    POut.Int   ((int)terminalActive.TerminalStatus)+","
				+    POut.Long  (terminalActive.PatNum)+")";
			if(useExistingPK || PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				terminalActive.TerminalActiveNum=Db.NonQ(command,true);
			}
			return terminalActive.TerminalActiveNum;
		}

		///<summary>Updates one TerminalActive in the database.</summary>
		internal static void Update(TerminalActive terminalActive){
			string command="UPDATE terminalactive SET "
				+"ComputerName     = '"+POut.String(terminalActive.ComputerName)+"', "
				+"TerminalStatus   =  "+POut.Int   ((int)terminalActive.TerminalStatus)+", "
				+"PatNum           =  "+POut.Long  (terminalActive.PatNum)+" "
				+"WHERE TerminalActiveNum = "+POut.Long(terminalActive.TerminalActiveNum);
			Db.NonQ(command);
		}

		///<summary>Updates one TerminalActive in the database.  Uses an old object to compare to, and only alters changed fields.  This prevents collisions and concurrency problems in heavily used tables.</summary>
		internal static void Update(TerminalActive terminalActive,TerminalActive oldTerminalActive){
			string command="";
			if(terminalActive.ComputerName != oldTerminalActive.ComputerName) {
				if(command!=""){ command+=",";}
				command+="ComputerName = '"+POut.String(terminalActive.ComputerName)+"'";
			}
			if(terminalActive.TerminalStatus != oldTerminalActive.TerminalStatus) {
				if(command!=""){ command+=",";}
				command+="TerminalStatus = "+POut.Int   ((int)terminalActive.TerminalStatus)+"";
			}
			if(terminalActive.PatNum != oldTerminalActive.PatNum) {
				if(command!=""){ command+=",";}
				command+="PatNum = "+POut.Long(terminalActive.PatNum)+"";
			}
			if(command==""){
				return;
			}
			command="UPDATE terminalactive SET "+command
				+" WHERE TerminalActiveNum = "+POut.Long(terminalActive.TerminalActiveNum);
			Db.NonQ(command);
		}

		///<summary>Deletes one TerminalActive from the database.</summary>
		internal static void Delete(long terminalActiveNum){
			string command="DELETE FROM terminalactive "
				+"WHERE TerminalActiveNum = "+POut.Long(terminalActiveNum);
			Db.NonQ(command);
		}

	}
}