using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlServerCe;
using System.Text;
using System.Windows.Forms;

namespace OpenDentMobile {
	public class ClassConvertDatabase {
		
		///<summary>This ONLY runs when first opening the program</summary>
		public static bool ConvertDB() {
			string pref=PrefC.GetString("MobileDataBaseVersion");
			if(Convert(pref)){
				return true;
			}
			else {
				Application.Exit();
				return false;
			}
		}

		private static bool Convert(string fromVersion){
			return true;
		}

		///<summary>This only gets run if the entire database is missing.</summary>
		public static void CreateNewDatabase(){
			SqlCeEngine engine=new SqlCeEngine(Dcon.ConnectionString);
			engine.CreateDatabase();
			if(!FormChooseDatabase.TryToConnect()){
				throw new ApplicationException("Could not connect to database.");
			}
			string command=@"CREATE TABLE preference (                     
				PrefName nvarchar(255) NOT NULL,  
				ValueString ntext NOT NULL,                  
				Comments ntext NOT NULL,                              
				PRIMARY KEY  (PrefName)                     
				) ";
			Dcon.NonQ(command);
			command="INSERT INTO preference VALUES('MobileDataBaseVersion','6.3.0.0','')";
			Dcon.NonQ(command);
			command="INSERT INTO preference VALUES('CorruptedDatabase','0','')";
			Dcon.NonQ(command);
			command=@"CREATE TABLE patient (  
				PatNum int NOT NULL PRIMARY KEY,
				LName nvarchar(255) NOT NULL DEFAULT '',
				FName nvarchar(255) NOT NULL DEFAULT '',
				Preferred nvarchar(255) NOT NULL DEFAULT '',
				PatStatus int NOT NULL DEFAULT 0,
				Gender int NOT NULL DEFAULT 0,
				Position int NOT NULL DEFAULT 0,
				Birthdate nchar(10) DEFAULT '0001-01-01',
				Address nvarchar(255) NOT NULL DEFAULT '',
				Address2 nvarchar(255) NOT NULL DEFAULT '',
				City nvarchar(255) NOT NULL DEFAULT '',
				State nvarchar(255) NOT NULL DEFAULT '',
				HmPhone nvarchar(255) NOT NULL DEFAULT '',
				WkPhone nvarchar(255) NOT NULL DEFAULT '',
				WirelessPhone nvarchar(255) NOT NULL DEFAULT '',
				Guarantor int NOT NULL DEFAULT 0,
				CreditType nvarchar(255) NOT NULL DEFAULT '',
				FamFinUrgNote ntext NOT NULL DEFAULT '',
				MedUrgNote ntext NOT NULL DEFAULT '',
				PrimaryInsurance nvarchar(255) NOT NULL DEFAULT ''
				);
				CREATE INDEX IdxGuarantor ON patient (Guarantor);
				CREATE INDEX IdxLName ON patient (LName)";
			Dcon.NonQs(command);
			



		}


	}
}
