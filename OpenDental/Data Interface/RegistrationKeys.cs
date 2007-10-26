using System;
using System.Collections.Generic;
using System.Text;
using OpenDentBusiness;
using System.Data;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using CodeBase;
using System.IO;
using CDT;

namespace OpenDental {
	///<summary>Used to keep track of which product keys have been assigned to which customers. This class is only used if the program is being run from a distributor installation.</summary>
	class RegistrationKeys {
		///<summary>Retrieves all registration keys for a particular customer's family. There can be multiple keys assigned to a single customer, or keys assigned to individual family members, since the customer may have multiple physical locations of business.</summary>
		public static RegistrationKey[] GetForPatient(int patNum){
			string command="SELECT * FROM registrationkey WHERE ";
			Family fam=Patients.GetFamily(patNum);
			for(int i=0;i<fam.List.Length;i++){
				command+="PatNum="+POut.PInt(fam.List[i].PatNum)+" ";
				if(i<fam.List.Length-1){
					command+="OR ";
				}
			}
			DataTable table=General.GetTable(command);
			RegistrationKey[] keys=new RegistrationKey[table.Rows.Count];
			for(int i=0;i<keys.Length;i++){
				keys[i]=new RegistrationKey();
				keys[i].RegistrationKeyNum	=PIn.PInt(table.Rows[i][0].ToString());
				keys[i].PatNum							=PIn.PInt(table.Rows[i][1].ToString());
				keys[i].RegKey							=PIn.PString(table.Rows[i][2].ToString());
				keys[i].Note								=PIn.PString(table.Rows[i][3].ToString());
				keys[i].DateStarted 				=PIn.PDate(table.Rows[i][4].ToString());
				keys[i].DateDisabled				=PIn.PDate(table.Rows[i][5].ToString());
				keys[i].DateEnded   				=PIn.PDate(table.Rows[i][6].ToString());
				keys[i].IsForeign   				=PIn.PBool(table.Rows[i][7].ToString());
			}
			return keys;
		}

		///<summary>Updates the given key data to the database.</summary>
		public static void Update(RegistrationKey registrationKey){
			string command="UPDATE registrationkey SET "
				+"PatNum='"+POut.PInt(registrationKey.PatNum)+"' "
				+",RegKey='"+POut.PString(registrationKey.RegKey)+"' "
				+",Note='"+POut.PString(registrationKey.Note)+"' "
				+",DateStarted="+POut.PDate(registrationKey.DateStarted)+" "
				+",DateDisabled="+POut.PDate(registrationKey.DateDisabled)+" "
				+",DateEnded="+POut.PDate(registrationKey.DateEnded)+" "
				+",IsForeign='"+POut.PBool(registrationKey.IsForeign)+"' "
				+" WHERE RegistrationKeyNum='"+POut.PInt(registrationKey.RegistrationKeyNum)+"'";
			General.NonQ(command);
		}

		///<summary>Inserts a new and unique registration key into the database.</summary>
		public static void Create(RegistrationKey registrationKey){
			do{
				if(registrationKey.IsForeign){
					Random rand=new Random();
					StringBuilder strBuild=new StringBuilder();
					for(int i=0;i<16;i++){
						int r=rand.Next(0,35);
						if(r<10){
							strBuild.Append((char)('0'+r));
						}
						else{
							strBuild.Append((char)('A'+r-10));
						}
					}
					registrationKey.RegKey=strBuild.ToString();
				}
				else{
					registrationKey.RegKey=CDT.Class1.GenerateRandKey();
				}
				if(registrationKey.RegKey==""){
					//Don't loop forever when software is unverified.
					return;
				}
			} while(KeyIsInUse(registrationKey.RegKey));
			string command="INSERT INTO registrationkey (PatNum,RegKey,Note,DateStarted,DateDisabled,DateEnded,"
				+"IsForeign) VALUES ("
				+"'"+POut.PInt(registrationKey.PatNum)+"',"
				+"'"+POut.PString(registrationKey.RegKey)+"',"
				+"'"+POut.PString(registrationKey.Note)+"',"
				+POut.PDate(registrationKey.DateStarted)+","
				+POut.PDate(registrationKey.DateDisabled)+","
				+POut.PDate(registrationKey.DateEnded)+","
				+"'"+POut.PBool(registrationKey.IsForeign)+"')";
			General.NonQ(command);
		}

		public static void Delete(int registrationKeyNum){
			string command="DELETE FROM registrationkey WHERE RegistrationKeyNum='"
				+POut.PInt(registrationKeyNum)+"'";
			General.NonQ(command);
		}

		///<summary>Returns true if the given registration key is currently in use by a customer, false otherwise.</summary>
		public static bool KeyIsInUse(string regKey) {
			string command="SELECT RegKey FROM registrationkey WHERE RegKey='"+POut.PString(regKey)+"'";
			DataTable table=General.GetTable(command);
			return (table.Rows.Count>0);
		}

	}
}
