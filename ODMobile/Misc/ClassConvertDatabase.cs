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
		public static void CreateNewDatabase(string dbName){
			SqlCeEngine engine=new SqlCeEngine(Dcon.ConnectionString);
			engine.CreateDatabase();
			if(!FormChooseDatabase.TryToConnect(dbName)){
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
			command=@"INSERT INTO preference VALUES('ImportPath','\My Documents\Business\Open Dental','')";
			Dcon.NonQ(command);
			command=@"CREATE TABLE patient (  
				PatNum int NOT NULL PRIMARY KEY,
				LName nvarchar(255) NOT NULL DEFAULT '',
				FName nvarchar(255) NOT NULL DEFAULT '',
				Preferred nvarchar(255) NOT NULL DEFAULT '',
				PatStatus int NOT NULL DEFAULT 0,
				Gender int NOT NULL DEFAULT 0,
				Position int NOT NULL DEFAULT 0,
				Birthdate nchar(10) NOT NULL DEFAULT '0001-01-01',
				Address nvarchar(255) NOT NULL DEFAULT '',
				Address2 nvarchar(255) NOT NULL DEFAULT '',
				City nvarchar(255) NOT NULL DEFAULT '',
				State nvarchar(255) NOT NULL DEFAULT '',
				HmPhone nvarchar(255) NOT NULL DEFAULT '',
				WkPhone nvarchar(255) NOT NULL DEFAULT '',
				WirelessPhone nvarchar(255) NOT NULL DEFAULT '',
				Guarantor int NOT NULL DEFAULT 0,
				CreditType nvarchar(255) NOT NULL DEFAULT '',
				FamFinUrgNote nvarchar(4000) NOT NULL DEFAULT '',
				MedUrgNote nvarchar(4000) NOT NULL DEFAULT '',
				PrimaryInsurance nvarchar(4000) NOT NULL DEFAULT ''
				);
				CREATE INDEX IdxGuarantor ON patient (Guarantor);
				CREATE INDEX IdxLName ON patient (LName)";
			Dcon.NonQs(command);
			command=@"CREATE TABLE appointment (
				AptNum int NOT NULL PRIMARY KEY,
				PatNum int NOT NULL DEFAULT 0,
				AptStatus int NOT NULL DEFAULT 0,
				Pattern nvarchar(255) NOT NULL,
				Confirmed int NOT NULL DEFAULT 0,
				Op int NOT NULL DEFAULT 0,
				Note nvarchar(4000) NOT NULL DEFAULT '',
				ProvNum int NOT NULL DEFAULT 0,
				ProvHyg int NOT NULL DEFAULT 0,
				AptDateTime datetime NOT NULL DEFAULT '1850-01-01',
				ProcDescript nvarchar(255) NOT NULL DEFAULT '',
				IsHygiene bit NOT NULL DEFAULT 0
				);
				CREATE INDEX IdxPatNum ON appointment (PatNum);
				CREATE INDEX IdxAptDateTime ON appointment (AptDateTime)
				";
			Dcon.NonQs(command);
			

		}


	}
}
