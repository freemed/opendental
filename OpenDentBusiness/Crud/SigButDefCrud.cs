//This file is automatically generated.
//Do not attempt to make changes to this file because the changes will be erased and overwritten.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;

namespace OpenDentBusiness.Crud{
	public class SigButDefCrud {
		///<summary>Gets one SigButDef object from the database using the primary key.  Returns null if not found.</summary>
		public static SigButDef SelectOne(long sigButDefNum){
			string command="SELECT * FROM sigbutdef "
				+"WHERE SigButDefNum = "+POut.Long(sigButDefNum);
			List<SigButDef> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets one SigButDef object from the database using a query.</summary>
		public static SigButDef SelectOne(string command){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<SigButDef> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets a list of SigButDef objects from the database using a query.</summary>
		public static List<SigButDef> SelectMany(string command){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<SigButDef> list=TableToList(Db.GetTable(command));
			return list;
		}

		///<summary>Converts a DataTable to a list of objects.</summary>
		public static List<SigButDef> TableToList(DataTable table){
			List<SigButDef> retVal=new List<SigButDef>();
			SigButDef sigButDef;
			for(int i=0;i<table.Rows.Count;i++) {
				sigButDef=new SigButDef();
				sigButDef.SigButDefNum= PIn.Long  (table.Rows[i]["SigButDefNum"].ToString());
				sigButDef.ButtonText  = PIn.String(table.Rows[i]["ButtonText"].ToString());
				sigButDef.ButtonIndex = PIn.Int   (table.Rows[i]["ButtonIndex"].ToString());
				sigButDef.SynchIcon   = PIn.Byte  (table.Rows[i]["SynchIcon"].ToString());
				sigButDef.ComputerName= PIn.String(table.Rows[i]["ComputerName"].ToString());
				retVal.Add(sigButDef);
			}
			return retVal;
		}

		///<summary>Inserts one SigButDef into the database.  Returns the new priKey.</summary>
		public static long Insert(SigButDef sigButDef){
			if(DataConnection.DBtype==DatabaseType.Oracle) {
				sigButDef.SigButDefNum=DbHelper.GetNextOracleKey("sigbutdef","SigButDefNum");
				int loopcount=0;
				while(loopcount<100){
					try {
						return Insert(sigButDef,true);
					}
					catch(Oracle.DataAccess.Client.OracleException ex){
						if(ex.Number==1 && ex.Message.ToLower().Contains("unique constraint") && ex.Message.ToLower().Contains("violated")){
							sigButDef.SigButDefNum++;
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
				return Insert(sigButDef,false);
			}
		}

		///<summary>Inserts one SigButDef into the database.  Provides option to use the existing priKey.</summary>
		public static long Insert(SigButDef sigButDef,bool useExistingPK){
			if(!useExistingPK && PrefC.RandomKeys) {
				sigButDef.SigButDefNum=ReplicationServers.GetKey("sigbutdef","SigButDefNum");
			}
			string command="INSERT INTO sigbutdef (";
			if(useExistingPK || PrefC.RandomKeys) {
				command+="SigButDefNum,";
			}
			command+="ButtonText,ButtonIndex,SynchIcon,ComputerName) VALUES(";
			if(useExistingPK || PrefC.RandomKeys) {
				command+=POut.Long(sigButDef.SigButDefNum)+",";
			}
			command+=
				 "'"+POut.String(sigButDef.ButtonText)+"',"
				+    POut.Int   (sigButDef.ButtonIndex)+","
				+    POut.Byte  (sigButDef.SynchIcon)+","
				+"'"+POut.String(sigButDef.ComputerName)+"')";
			if(useExistingPK || PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				sigButDef.SigButDefNum=Db.NonQ(command,true);
			}
			return sigButDef.SigButDefNum;
		}

		///<summary>Updates one SigButDef in the database.</summary>
		public static void Update(SigButDef sigButDef){
			string command="UPDATE sigbutdef SET "
				+"ButtonText  = '"+POut.String(sigButDef.ButtonText)+"', "
				+"ButtonIndex =  "+POut.Int   (sigButDef.ButtonIndex)+", "
				+"SynchIcon   =  "+POut.Byte  (sigButDef.SynchIcon)+", "
				+"ComputerName= '"+POut.String(sigButDef.ComputerName)+"' "
				+"WHERE SigButDefNum = "+POut.Long(sigButDef.SigButDefNum);
			Db.NonQ(command);
		}

		///<summary>Updates one SigButDef in the database.  Uses an old object to compare to, and only alters changed fields.  This prevents collisions and concurrency problems in heavily used tables.</summary>
		public static void Update(SigButDef sigButDef,SigButDef oldSigButDef){
			string command="";
			if(sigButDef.ButtonText != oldSigButDef.ButtonText) {
				if(command!=""){ command+=",";}
				command+="ButtonText = '"+POut.String(sigButDef.ButtonText)+"'";
			}
			if(sigButDef.ButtonIndex != oldSigButDef.ButtonIndex) {
				if(command!=""){ command+=",";}
				command+="ButtonIndex = "+POut.Int(sigButDef.ButtonIndex)+"";
			}
			if(sigButDef.SynchIcon != oldSigButDef.SynchIcon) {
				if(command!=""){ command+=",";}
				command+="SynchIcon = "+POut.Byte(sigButDef.SynchIcon)+"";
			}
			if(sigButDef.ComputerName != oldSigButDef.ComputerName) {
				if(command!=""){ command+=",";}
				command+="ComputerName = '"+POut.String(sigButDef.ComputerName)+"'";
			}
			if(command==""){
				return;
			}
			command="UPDATE sigbutdef SET "+command
				+" WHERE SigButDefNum = "+POut.Long(sigButDef.SigButDefNum);
			Db.NonQ(command);
		}

		///<summary>Deletes one SigButDef from the database.</summary>
		public static void Delete(long sigButDefNum){
			string command="DELETE FROM sigbutdef "
				+"WHERE SigButDefNum = "+POut.Long(sigButDefNum);
			Db.NonQ(command);
		}

	}
}