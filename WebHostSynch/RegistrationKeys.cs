using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using OpenDentBusiness;

namespace WebHostSynch {
	///<summary>Used to keep track of which product keys have been assigned to which customers. This class is only used if the program is being run from a distributor installation.</summary>
	public class RegistrationKeys {

		private WebHostSynch.Db db = new WebHostSynch.Db();

		public void SetDb(string connectStr){
			db.setConn(connectStr);
		}
		///<summary>Retrieves all registration keys for a particular customer's family. There can be multiple keys assigned to a single customer, or keys assigned to individual family members, since the customer may have multiple physical locations of business.</summary>
		public RegistrationKey[] GetForPatient(long patNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<RegistrationKey[]>(MethodBase.GetCurrentMethod(),patNum);
			}
			string command="SELECT * FROM registrationkey WHERE ";
			Family fam=Patients.GetFamily(patNum);
			for(int i=0;i<fam.ListPats.Length;i++){
				command+="PatNum="+POut.Long(fam.ListPats[i].PatNum)+" ";
				if(i<fam.ListPats.Length-1){
					command+="OR ";
				}
			}
			DataTable table=db.GetTable(command);
			RegistrationKey[] keys=new RegistrationKey[table.Rows.Count];
			for(int i=0;i<keys.Length;i++){
				keys[i]=new RegistrationKey();
				keys[i].RegistrationKeyNum	=PIn.Long(table.Rows[i][0].ToString());
				keys[i].PatNum							=PIn.Long(table.Rows[i][1].ToString());
				keys[i].RegKey							=PIn.String(table.Rows[i][2].ToString());
				keys[i].Note								=PIn.String(table.Rows[i][3].ToString());
				keys[i].DateStarted 				=PIn.Date(table.Rows[i][4].ToString());
				keys[i].DateDisabled				=PIn.Date(table.Rows[i][5].ToString());
				keys[i].DateEnded   				=PIn.Date(table.Rows[i][6].ToString());
				keys[i].IsForeign   				=PIn.Bool(table.Rows[i][7].ToString());
				keys[i].UsesServerVersion		=PIn.Bool(table.Rows[i][8].ToString());
				keys[i].IsFreeVersion		    =PIn.Bool(table.Rows[i][9].ToString());
				keys[i].IsOnlyForTesting		=PIn.Bool(table.Rows[i][10].ToString());
				keys[i].VotesAllotted   		=PIn.Int (table.Rows[i][11].ToString());
			}
			return keys;
		}

		///<summary>Returns true if the given registration key is currently in use by a customer, false otherwise.</summary>
		public bool KeyIsInUse(string regKey) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetBool(MethodBase.GetCurrentMethod(),regKey);
			}
			string command="SELECT RegKey FROM registrationkey WHERE RegKey='"+POut.String(regKey)+"'";
			DataTable table=db.GetTable(command);
			return (table.Rows.Count>0);
		}


		public RegistrationKey GetByKey(string regKey) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<RegistrationKey>(MethodBase.GetCurrentMethod(),regKey);
			}
			if(!Regex.IsMatch(regKey,@"^[A-Z0-9]{16}$")) {
				throw new ApplicationException("Invalid registration key format.");
			}
			string command="SELECT * FROM  registrationkey WHERE RegKey='"+POut.String(regKey)+"'";
			DataTable table=db.GetTable(command);
			if(table.Rows.Count==0) {
				throw new ApplicationException("Invalid registration key.");
			}
			RegistrationKey key=null;
			for(int i=0;i<table.Rows.Count;i++) {
				key=new RegistrationKey();
				key.RegistrationKeyNum	=PIn.Int(table.Rows[i][0].ToString());
				key.PatNum							=PIn.Int(table.Rows[i][1].ToString());
				key.RegKey							=PIn.String(table.Rows[i][2].ToString());
				key.Note								=PIn.String(table.Rows[i][3].ToString());
				key.DateStarted 				=PIn.Date(table.Rows[i][4].ToString());
				key.DateDisabled				=PIn.Date(table.Rows[i][5].ToString());
				key.DateEnded   				=PIn.Date(table.Rows[i][6].ToString());
				key.IsForeign   				=PIn.Bool(table.Rows[i][7].ToString());
				//key.UsesServerVersion  	=PIn.PBool(table.Rows[i][8].ToString());
				key.IsFreeVersion  			=PIn.Bool(table.Rows[i][9].ToString());
				key.IsOnlyForTesting  	=PIn.Bool(table.Rows[i][10].ToString());
				//key.VotesAllotted     	=PIn.PInt(table.Rows[i][11].ToString());
			}
			//if(key.DateDisabled.Year>1880){
			//	throw new ApplicationException("This key has been disabled.  Please call for assistance.");
			//}
			return key;
		}
	

	}
}
