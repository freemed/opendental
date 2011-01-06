//This file is automatically generated.
//Do not attempt to make changes to this file because the changes will be erased and overwritten.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;

namespace OpenDentBusiness.Crud{
	internal class PlannedApptCrud {
		///<summary>Gets one PlannedAppt object from the database using the primary key.  Returns null if not found.</summary>
		internal static PlannedAppt SelectOne(long plannedApptNum){
			string command="SELECT * FROM plannedappt "
				+"WHERE PlannedApptNum = "+POut.Long(plannedApptNum);
			List<PlannedAppt> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets one PlannedAppt object from the database using a query.</summary>
		internal static PlannedAppt SelectOne(string command){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<PlannedAppt> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets a list of PlannedAppt objects from the database using a query.</summary>
		internal static List<PlannedAppt> SelectMany(string command){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<PlannedAppt> list=TableToList(Db.GetTable(command));
			return list;
		}

		///<summary>Converts a DataTable to a list of objects.</summary>
		internal static List<PlannedAppt> TableToList(DataTable table){
			List<PlannedAppt> retVal=new List<PlannedAppt>();
			PlannedAppt plannedAppt;
			for(int i=0;i<table.Rows.Count;i++) {
				plannedAppt=new PlannedAppt();
				plannedAppt.PlannedApptNum= PIn.Long  (table.Rows[i]["PlannedApptNum"].ToString());
				plannedAppt.PatNum        = PIn.Long  (table.Rows[i]["PatNum"].ToString());
				plannedAppt.AptNum        = PIn.Long  (table.Rows[i]["AptNum"].ToString());
				plannedAppt.ItemOrder     = PIn.Int   (table.Rows[i]["ItemOrder"].ToString());
				retVal.Add(plannedAppt);
			}
			return retVal;
		}

		///<summary>Inserts one PlannedAppt into the database.  Returns the new priKey.</summary>
		internal static long Insert(PlannedAppt plannedAppt){
			return Insert(plannedAppt,false);
		}

		///<summary>Inserts one PlannedAppt into the database.  Provides option to use the existing priKey.</summary>
		internal static long Insert(PlannedAppt plannedAppt,bool useExistingPK){
			if(!useExistingPK && PrefC.RandomKeys) {
				plannedAppt.PlannedApptNum=ReplicationServers.GetKey("plannedappt","PlannedApptNum");
			}
			string command="INSERT INTO plannedappt (";
			if(useExistingPK || PrefC.RandomKeys) {
				command+="PlannedApptNum,";
			}
			command+="PatNum,AptNum,ItemOrder) VALUES(";
			if(useExistingPK || PrefC.RandomKeys) {
				command+=POut.Long(plannedAppt.PlannedApptNum)+",";
			}
			command+=
				     POut.Long  (plannedAppt.PatNum)+","
				+    POut.Long  (plannedAppt.AptNum)+","
				+    POut.Int   (plannedAppt.ItemOrder)+")";
			if(useExistingPK || PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				plannedAppt.PlannedApptNum=Db.NonQ(command,true);
			}
			return plannedAppt.PlannedApptNum;
		}

		///<summary>Updates one PlannedAppt in the database.</summary>
		internal static void Update(PlannedAppt plannedAppt){
			string command="UPDATE plannedappt SET "
				+"PatNum        =  "+POut.Long  (plannedAppt.PatNum)+", "
				+"AptNum        =  "+POut.Long  (plannedAppt.AptNum)+", "
				+"ItemOrder     =  "+POut.Int   (plannedAppt.ItemOrder)+" "
				+"WHERE PlannedApptNum = "+POut.Long(plannedAppt.PlannedApptNum);
			Db.NonQ(command);
		}

		///<summary>Updates one PlannedAppt in the database.  Uses an old object to compare to, and only alters changed fields.  This prevents collisions and concurrency problems in heavily used tables.</summary>
		internal static void Update(PlannedAppt plannedAppt,PlannedAppt oldPlannedAppt){
			string command="";
			if(plannedAppt.PatNum != oldPlannedAppt.PatNum) {
				if(command!=""){ command+=",";}
				command+="PatNum = "+POut.Long(plannedAppt.PatNum)+"";
			}
			if(plannedAppt.AptNum != oldPlannedAppt.AptNum) {
				if(command!=""){ command+=",";}
				command+="AptNum = "+POut.Long(plannedAppt.AptNum)+"";
			}
			if(plannedAppt.ItemOrder != oldPlannedAppt.ItemOrder) {
				if(command!=""){ command+=",";}
				command+="ItemOrder = "+POut.Int(plannedAppt.ItemOrder)+"";
			}
			if(command==""){
				return;
			}
			command="UPDATE plannedappt SET "+command
				+" WHERE PlannedApptNum = "+POut.Long(plannedAppt.PlannedApptNum);
			Db.NonQ(command);
		}

		///<summary>Deletes one PlannedAppt from the database.</summary>
		internal static void Delete(long plannedApptNum){
			string command="DELETE FROM plannedappt "
				+"WHERE PlannedApptNum = "+POut.Long(plannedApptNum);
			Db.NonQ(command);
		}

	}
}