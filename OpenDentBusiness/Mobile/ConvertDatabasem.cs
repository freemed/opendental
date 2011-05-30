using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenDentBusiness.Mobile {
	/// <summary>This file contains some useful queries, although it is not automated like the main program.  It is expected that these queries will need to be run manually, and that there will be additional management and tuning.  As we get nearer to the production version, we may decide to automate these queries.</summary>
	public class ConvertDatabasem {


	}
}



				/*				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="ALTER TABLE labpanelm ADD ServiceId varchar(255) NOT NULL";
					Db.NonQ(command);
				}
				else {//oracle
					command="ALTER TABLE labpanelm ADD ServiceId varchar2(255)";
					Db.NonQ(command);
				}				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="ALTER TABLE labpanelm ADD ServiceName varchar(255) NOT NULL";
					Db.NonQ(command);
				}
				else {//oracle
					command="ALTER TABLE labpanelm ADD ServiceName varchar2(255)";
					Db.NonQ(command);
				}
				*/

				/*
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="DROP TABLE IF EXISTS pharmacym";
					Db.NonQ(command);
					command=@"CREATE TABLE pharmacym (
						CustomerNum bigint NOT NULL,
						PharmacyNum bigint NOT NULL,
						StoreName varchar(255) NOT NULL,
						Phone varchar(255) NOT NULL,
						Fax varchar(255) NOT NULL,
						Address varchar(255) NOT NULL,
						Address2 varchar(255) NOT NULL,
						City varchar(255) NOT NULL,
						State varchar(255) NOT NULL,
						Zip varchar(255) NOT NULL,
						Note varchar(255) NOT NULL,
						INDEX(CustomerNum),
						INDEX(PharmacyNum)
						) DEFAULT CHARSET=utf8";
					Db.NonQ(command);
				}
				*/

				/*				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="ALTER TABLE labpanelm ADD ServiceId varchar(255) NOT NULL";
					Db.NonQ(command);
				}
				else {//oracle
					command="ALTER TABLE labpanelm ADD ServiceId varchar2(255)";
					Db.NonQ(command);
				}				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="ALTER TABLE labpanelm ADD ServiceName varchar(255) NOT NULL";
					Db.NonQ(command);
				}
				else {//oracle
					command="ALTER TABLE labpanelm ADD ServiceName varchar2(255)";
					Db.NonQ(command);
				}
				*/

				/*
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="DROP TABLE IF EXISTS pharmacym";
					Db.NonQ(command);
					command=@"CREATE TABLE pharmacym (
						CustomerNum bigint NOT NULL,
						PharmacyNum bigint NOT NULL,
						StoreName varchar(255) NOT NULL,
						Phone varchar(255) NOT NULL,
						Fax varchar(255) NOT NULL,
						Address varchar(255) NOT NULL,
						Address2 varchar(255) NOT NULL,
						City varchar(255) NOT NULL,
						State varchar(255) NOT NULL,
						Zip varchar(255) NOT NULL,
						Note varchar(255) NOT NULL,
						INDEX(CustomerNum),
						INDEX(PharmacyNum)
						) DEFAULT CHARSET=utf8";
					Db.NonQ(command);
				}
				*/