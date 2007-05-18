using System;
using System.Collections.Generic;
using System.Text;
using OpenDentBusiness;
using System.Data;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using CodeBase;

namespace OpenDental {
	///<summary>Used to keep track of which product keys have been assigned to which customers. This class is only used if the program is being run from a distributor installation.</summary>
	class RegistrationKeys {
		///<summary>Retrieves all registration keys for a particular customer. There can be multiple keys assigned to a single customer, since the customer may have multiple physical locations of business.</summary>
		public static RegistrationKey[] GetForPatient(int patNum){
			string command="SELECT * FROM registrationkey WHERE PatNum='"+patNum+"'";
			DataTable table=General.GetTable(command);
			RegistrationKey[] keys=new RegistrationKey[table.Rows.Count];
			for(int i=0;i<keys.Length;i++){
				keys[i]=new RegistrationKey();
				keys[i].RegistrationKeyNum	=PIn.PInt(table.Rows[i][0].ToString());
				keys[i].PatNum							=PIn.PInt(table.Rows[i][1].ToString());
				keys[i].RegKey							=PIn.PString(table.Rows[i][2].ToString());
				keys[i].Note								=PIn.PString(table.Rows[i][3].ToString());
			}
			return keys;
		}

		///<summary>Returns true if the given registration key is currently in use by a customer, false otherwise.</summary>
		public static bool KeyIsInUse(string regKey){
			string command="SELECT RegKey FROM registrationkey WHERE RegKey='"+POut.PString(regKey)+"'";
			DataTable table=General.GetTable(command);
			return(table.Rows.Count>0);
		}

		///<summary>Inserts a new and unique registration key into the database.</summary>
		public static void Create(RegistrationKey registrationKey){
			Match m;
			do{
				registrationKey.RegKey="ODKEY";
				Random rand=new Random();
				for(int i=0;i<16-registrationKey.RegKey.Length;i++){
					registrationKey.RegKey+=(char)rand.Next(0,255);
				}
				//TODO: update with encryption.

				//Key must be generated and stored in db!


				DSACryptoServiceProvider dsa=new DSACryptoServiceProvider();
				dsa.FromXmlString(PrefB.GetString("DistributorKey"));
				byte[] signedHash=dsa.SignData(Encoding.UTF8.GetBytes(registrationKey.RegKey));
				MsgBoxCopyPaste msg=new MsgBoxCopyPaste(signedHash.ToString());
				msg.ShowDialog();

				

				/*
				 * static String BytesToHexString(byte[] bytes) 
        {
            StringBuilder hexString = new StringBuilder(64);

            for (int counter = 0; counter < bytes.Length; counter++) 
            {
                hexString.Append(String.Format("{0:X2}", bytes[counter]));
            }
            return hexString.ToString();
        }
				 */
				








				//Convert the stored key to base64 to both make the key user-readable and to 
				//ensure that there are no semi-colons in it (otherwise, the semi-colons are removed 
				//when connecting to an Oracle database, in which case the stored key would be invalid).
				registrationKey.RegKey=Convert.ToBase64String(Encoding.UTF8.GetBytes(registrationKey.RegKey));
				m=Regex.Match(registrationKey.RegKey,"^([ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789]*)$");
			}while(!m.Success || m.Result("$1")!=registrationKey.RegKey || KeyIsInUse(registrationKey.RegKey));
			string command="INSERT INTO registrationkey (PatNum,RegKey,Note) VALUES ("+
				"'"+POut.PInt(registrationKey.PatNum)+"',"+
				"'"+POut.PString(registrationKey.RegKey)+"',"+
				"'"+POut.PString(registrationKey.Note)+"')";
			General.NonQ(command);
		}

	}
}
