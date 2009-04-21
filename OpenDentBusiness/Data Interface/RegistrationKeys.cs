using CDT;
using CodeBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace OpenDentBusiness {
	///<summary>Used to keep track of which product keys have been assigned to which customers. This class is only used if the program is being run from a distributor installation.</summary>
	public class RegistrationKeys {
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
			DataTable table=Db.GetTable(command);
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
			Db.NonQ(command);
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
			Db.NonQ(command);
		}

		public static void Delete(int registrationKeyNum){
			string command="DELETE FROM registrationkey WHERE RegistrationKeyNum='"
				+POut.PInt(registrationKeyNum)+"'";
			Db.NonQ(command);
		}

		///<summary>Returns true if the given registration key is currently in use by a customer, false otherwise.</summary>
		public static bool KeyIsInUse(string regKey) {
			string command="SELECT RegKey FROM registrationkey WHERE RegKey='"+POut.PString(regKey)+"'";
			DataTable table=Db.GetTable(command);
			return (table.Rows.Count>0);
		}

		///<Summary></Summary>
		public static DataTable GetAll() {
			DataTable table=new DataTable();
			table.Columns.Add("dateStop");
			table.Columns.Add("family");
			table.Columns.Add("PatNum");
			table.Columns.Add("RegKey");
			string command=@"
				DROP TABLE IF EXISTS tempRegKeys;
				CREATE TABLE tempRegKeys(
					tempRegKeyId int auto_increment NOT NULL,
					PatNum int NOT NULL,
					RegKey VARCHAR(255) NOT NULL,
					IsMissing tinyint NOT NULL,
					Date_ DATE NOT NULL DEFAULT '0001-01-01',
					PRIMARY KEY(tempRegKeyId),
					KEY(PatNum));
				/*Fill table with patnums for all guarantors of regkeys that are still active.*/
				INSERT INTO tempRegKeys (PatNum,RegKey,Date_) 
				SELECT patient.Guarantor,RegKey,'0001-01-01'
				FROM registrationkey
				LEFT JOIN patient ON registrationkey.PatNum=patient.PatNum
				WHERE DateDisabled='0001-01-01'
				AND DateEnded='0001-01-01';
				/*Set indicators on keys with missing repeatcharges*/
				UPDATE tempRegKeys
				SET IsMissing=1
				WHERE NOT EXISTS(SELECT * FROM repeatcharge WHERE repeatcharge.PatNum=tempRegKeys.PatNum);

				/*Now, look for expired repeating charges.  This is done in two steps.*/
				/*Step 1: Mark all keys that have expired repeating charges.*/
				/*Step 2: Then, remove those markings for all keys that also have unexpired repeating charges.*/
				UPDATE tempRegKeys
				SET Date_=(
				SELECT IFNULL(MAX(DateStop),'0001-01-01')
				FROM repeatcharge
				WHERE repeatcharge.PatNum=tempRegKeys.PatNum
				AND DateStop < NOW() AND DateStop > '0001-01-01');
				/*Step 2:*/
				UPDATE tempRegKeys
				SET Date_='0001-01-01'
				WHERE EXISTS(
				SELECT * FROM repeatcharge
				WHERE repeatcharge.PatNum=tempRegKeys.PatNum
				AND DateStop = '0001-01-01');

				SELECT LName,FName,tempRegKeys.PatNum,tempRegKeys.RegKey,IsMissing,Date_
				FROM tempRegKeys
				LEFT JOIN patient ON patient.PatNum=tempRegKeys.PatNum
				WHERE IsMissing=1
				OR Date_ > '0001-01-01'
				ORDER BY tempRegKeys.PatNum;
				DROP TABLE IF EXISTS tempRegKeys;";
			DataTable raw=Db.GetTable(command);
			DataRow row;
			DateTime dateRepeatStop;
			for(int i=0;i<raw.Rows.Count;i++) {
				row=table.NewRow();
				if(raw.Rows[i]["IsMissing"].ToString()=="1") {
					row["dateStop"]="Missing Repeat Charge";
				}
				else {
					row["dateStop"]="";
				}
				dateRepeatStop=PIn.PDate(raw.Rows[i]["Date_"].ToString());
				if(dateRepeatStop.Year>1880) {
					if(row["dateStop"].ToString()!="") {
						row["dateStop"]+="\r\n";
					}
					row["dateStop"]+="Expired Repeat Charge:"+dateRepeatStop.ToShortDateString();
				}
				row["family"]=raw.Rows[i]["LName"].ToString()+", "+raw.Rows[i]["FName"].ToString();
				row["PatNum"]=raw.Rows[i]["PatNum"].ToString();
				row["RegKey"]=raw.Rows[i]["RegKey"].ToString();
				table.Rows.Add(row);
			}
			return table;
		}

	}
}
