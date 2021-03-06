//This file is automatically generated.
//Do not attempt to make changes to this file because the changes will be erased and overwritten.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;

namespace OpenDentBusiness.Crud{
	public class UcumCrud {
		///<summary>Gets one Ucum object from the database using the primary key.  Returns null if not found.</summary>
		public static Ucum SelectOne(long ucumNum){
			string command="SELECT * FROM ucum "
				+"WHERE UcumNum = "+POut.Long(ucumNum);
			List<Ucum> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets one Ucum object from the database using a query.</summary>
		public static Ucum SelectOne(string command){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<Ucum> list=TableToList(Db.GetTable(command));
			if(list.Count==0) {
				return null;
			}
			return list[0];
		}

		///<summary>Gets a list of Ucum objects from the database using a query.</summary>
		public static List<Ucum> SelectMany(string command){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				throw new ApplicationException("Not allowed to send sql directly.  Rewrite the calling class to not use this query:\r\n"+command);
			}
			List<Ucum> list=TableToList(Db.GetTable(command));
			return list;
		}

		///<summary>Converts a DataTable to a list of objects.</summary>
		public static List<Ucum> TableToList(DataTable table){
			List<Ucum> retVal=new List<Ucum>();
			Ucum ucum;
			for(int i=0;i<table.Rows.Count;i++) {
				ucum=new Ucum();
				ucum.UcumNum    = PIn.Long  (table.Rows[i]["UcumNum"].ToString());
				ucum.UcumCode   = PIn.String(table.Rows[i]["UcumCode"].ToString());
				ucum.Description= PIn.String(table.Rows[i]["Description"].ToString());
				ucum.IsInUse    = PIn.Bool  (table.Rows[i]["IsInUse"].ToString());
				retVal.Add(ucum);
			}
			return retVal;
		}

		///<summary>Inserts one Ucum into the database.  Returns the new priKey.</summary>
		public static long Insert(Ucum ucum){
			if(DataConnection.DBtype==DatabaseType.Oracle) {
				ucum.UcumNum=DbHelper.GetNextOracleKey("ucum","UcumNum");
				int loopcount=0;
				while(loopcount<100){
					try {
						return Insert(ucum,true);
					}
					catch(Oracle.DataAccess.Client.OracleException ex){
						if(ex.Number==1 && ex.Message.ToLower().Contains("unique constraint") && ex.Message.ToLower().Contains("violated")){
							ucum.UcumNum++;
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
				return Insert(ucum,false);
			}
		}

		///<summary>Inserts one Ucum into the database.  Provides option to use the existing priKey.</summary>
		public static long Insert(Ucum ucum,bool useExistingPK){
			if(!useExistingPK && PrefC.RandomKeys) {
				ucum.UcumNum=ReplicationServers.GetKey("ucum","UcumNum");
			}
			string command="INSERT INTO ucum (";
			if(useExistingPK || PrefC.RandomKeys) {
				command+="UcumNum,";
			}
			command+="UcumCode,Description,IsInUse) VALUES(";
			if(useExistingPK || PrefC.RandomKeys) {
				command+=POut.Long(ucum.UcumNum)+",";
			}
			command+=
				 "'"+POut.String(ucum.UcumCode)+"',"
				+"'"+POut.String(ucum.Description)+"',"
				+    POut.Bool  (ucum.IsInUse)+")";
			if(useExistingPK || PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				ucum.UcumNum=Db.NonQ(command,true);
			}
			return ucum.UcumNum;
		}

		///<summary>Updates one Ucum in the database.</summary>
		public static void Update(Ucum ucum){
			string command="UPDATE ucum SET "
				+"UcumCode   = '"+POut.String(ucum.UcumCode)+"', "
				+"Description= '"+POut.String(ucum.Description)+"', "
				+"IsInUse    =  "+POut.Bool  (ucum.IsInUse)+" "
				+"WHERE UcumNum = "+POut.Long(ucum.UcumNum);
			Db.NonQ(command);
		}

		///<summary>Updates one Ucum in the database.  Uses an old object to compare to, and only alters changed fields.  This prevents collisions and concurrency problems in heavily used tables.</summary>
		public static void Update(Ucum ucum,Ucum oldUcum){
			string command="";
			if(ucum.UcumCode != oldUcum.UcumCode) {
				if(command!=""){ command+=",";}
				command+="UcumCode = '"+POut.String(ucum.UcumCode)+"'";
			}
			if(ucum.Description != oldUcum.Description) {
				if(command!=""){ command+=",";}
				command+="Description = '"+POut.String(ucum.Description)+"'";
			}
			if(ucum.IsInUse != oldUcum.IsInUse) {
				if(command!=""){ command+=",";}
				command+="IsInUse = "+POut.Bool(ucum.IsInUse)+"";
			}
			if(command==""){
				return;
			}
			command="UPDATE ucum SET "+command
				+" WHERE UcumNum = "+POut.Long(ucum.UcumNum);
			Db.NonQ(command);
		}

		///<summary>Deletes one Ucum from the database.</summary>
		public static void Delete(long ucumNum){
			string command="DELETE FROM ucum "
				+"WHERE UcumNum = "+POut.Long(ucumNum);
			Db.NonQ(command);
		}

	}
}