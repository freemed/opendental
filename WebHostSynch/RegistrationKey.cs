using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Text.RegularExpressions;

namespace WebHostSynch {
	public class RegistrationKey {
		///<summary>Primary Key.</summary>
		public int RegistrationKeyNum;
		///<summary>The customer to which this registration key applies.</summary>
		public int PatNum;
		///<summary>The registration key as stored in the customer database.</summary>
		public string RegKey;
		///<summary>General note about the registration key. Specifically, the note must include information about the location to which this key pertains, since once at least one key must be assigned to each location to be legal.</summary>
		public string Note;
		///<summary>This will help later with tracking for licensing.</summary>
		public DateTime DateStarted;
		///<summary>This is used to completely disable a key.  Might possibly even cripple the user's program.  Usually only used if reassigning another key due to abuse or error.  If no date specified, then this key is still valid.</summary>
		public DateTime DateDisabled;
		///<summary>This is used when the customer cancels monthly support.  This still allows the customer to get downloads for bug fixes, but only up through a certain version.  Our web server program will use this date to deduce which version they are allowed to have.  Any version that was released as a beta before this date is allowed to be downloaded.</summary>
		public DateTime DateEnded;
		///<summary>This is assigned automatically based on whether the registration key is a US version vs. a foreign version.  The foreign version is not able to unlock the procedure codes.  There are muliple layers of safeguards in place.</summary>
		public bool IsForeign;
		//<summary>True if this customer uses the server version of OD.  They will end up with a different download.  Can't use this until Customer db is updated to version 6.6.</summary>
		//public bool UsesServerVersion;
		///<summary>We have given this customer a free version.  Typically in India.</summary>
		public bool IsFreeVersion;
		///<summary>This customer is not using the software with live patient data, but only for testing and development purposes.</summary>
		public bool IsOnlyForTesting;
		//<summary>Typically 100, although it can be more for multilocation offices.</summary>
		//public int VotesAllotted;
	}

	public class RegistrationKeys{

		public static RegistrationKey GetByKey(string regKey) {
			if(!Regex.IsMatch(regKey,@"^[A-Z0-9]{16}$")){
				throw new ApplicationException("Invalid registration key format.");
			}
			string command="SELECT * FROM  registrationkey WHERE RegKey='"+POut.PString(regKey)+"'";
			DataTable table=GeneralB.GetTable(command);
			if(table.Rows.Count==0){
				throw new ApplicationException("Invalid registration key.");
			}
			RegistrationKey key=null;
			for(int i=0;i<table.Rows.Count;i++) {
				key=new RegistrationKey();
				key.RegistrationKeyNum	=PIn.PInt(table.Rows[i][0].ToString());
				key.PatNum							=PIn.PInt(table.Rows[i][1].ToString());
				key.RegKey							=PIn.PString(table.Rows[i][2].ToString());
				key.Note								=PIn.PString(table.Rows[i][3].ToString());
				key.DateStarted 				=PIn.PDate(table.Rows[i][4].ToString());
				key.DateDisabled				=PIn.PDate(table.Rows[i][5].ToString());
				key.DateEnded   				=PIn.PDate(table.Rows[i][6].ToString());
				key.IsForeign   				=PIn.PBool(table.Rows[i][7].ToString());
				//key.UsesServerVersion  	=PIn.PBool(table.Rows[i][8].ToString());
				key.IsFreeVersion  			=PIn.PBool(table.Rows[i][9].ToString());
				key.IsOnlyForTesting  	=PIn.PBool(table.Rows[i][10].ToString());
				//key.VotesAllotted     	=PIn.PInt(table.Rows[i][11].ToString());
			}
			//if(key.DateDisabled.Year>1880){
			//	throw new ApplicationException("This key has been disabled.  Please call for assistance.");
			//}
			return key;
		}


	}
}
