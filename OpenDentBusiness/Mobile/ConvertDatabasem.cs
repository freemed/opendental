using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenDentBusiness.Mobile {
	/// <summary>This file contains some useful queries, although it is not automated like the main program.  It is expected that these queries will need to be run manually, and that there will be additional management and tuning.  As we get nearer to the production version, we may decide to automate these queries.</summary>
	public class ConvertDatabasem {


	}
}


				/*
				command="DROP TABLE IF EXISTS appointmentm";
				Db.NonQ(command);
				command=@"CREATE TABLE appointmentm (
					CustomerNum bigint NOT NULL,
					AptNum bigint NOT NULL,
					PatNum bigint NOT NULL,
					AptStatus tinyint NOT NULL,
					Pattern varchar(255) NOT NULL,
					Confirmed bigint NOT NULL,
					Op bigint NOT NULL,
					Note varchar(255) NOT NULL,
					ProvNum bigint NOT NULL,
					ProvHyg bigint NOT NULL,
					AptDateTime datetime NOT NULL default '0001-01-01 00:00:00',
					IsNewPatient tinyint NOT NULL,
					ProcDescript varchar(255) NOT NULL,
					ClinicNum bigint NOT NULL,
					IsHygiene tinyint NOT NULL,
					PRIMARY KEY (CustomerNum,AptNum),
					INDEX(PatNum),
					INDEX(ClinicNum)
					) DEFAULT CHARSET=utf8";
				Db.NonQ(command);
				*/

				/*
				command="DROP TABLE IF EXISTS patientm";
				Db.NonQ(command);
				command=@"CREATE TABLE patientm (
					CustomerNum bigint NOT NULL,
					PatNum bigint NOT NULL,
					LName varchar(255) NOT NULL,
					FName varchar(255) NOT NULL,
					MiddleI varchar(255) NOT NULL,
					Preferred varchar(255) NOT NULL,
					PatStatus tinyint NOT NULL,
					Gender tinyint NOT NULL,
					Position tinyint NOT NULL,
					Birthdate date NOT NULL default '0001-01-01',
					Address varchar(255) NOT NULL,
					Address2 varchar(255) NOT NULL,
					City varchar(255) NOT NULL,
					State varchar(255) NOT NULL,
					Zip varchar(255) NOT NULL,
					HmPhone varchar(255) NOT NULL,
					WkPhone varchar(255) NOT NULL,
					WirelessPhone varchar(255) NOT NULL,
					Guarantor bigint NOT NULL,
					Email varchar(255) NOT NULL,
					AddrNote varchar(255) NOT NULL,
					ClinicNum bigint NOT NULL,
					PreferContactMethod tinyint NOT NULL,
					PRIMARY KEY (CustomerNum,PatNum),
					INDEX(Guarantor),
					INDEX(ClinicNum)
					) DEFAULT CHARSET=utf8";
				Db.NonQ(command);
				*/

				/*
				command="DROP TABLE IF EXISTS rxpatm";
				Db.NonQ(command);
				command=@"CREATE TABLE rxpatm (
					CustomerNum bigint NOT NULL,
					RxNum bigint NOT NULL,
					PatNum bigint NOT NULL,
					RxDate date NOT NULL DEFAULT '0001-01-01',
					Drug varchar(255) NOT NULL,
					Sig varchar(255) NOT NULL,
					Disp varchar(255) NOT NULL,
					Refills varchar(255) NOT NULL,
					ProvNum bigint NOT NULL,
					PRIMARY KEY (CustomerNum,RxNum),
					INDEX(PatNum),
					INDEX(ProvNum)
					) DEFAULT CHARSET=utf8";
				Db.NonQ(command);
				*/

				/*
				command="DROP TABLE IF EXISTS userm";
				Db.NonQ(command);
				command=@"CREATE TABLE userm (
					CustomerNum bigint NOT NULL,
					UsermNum bigint NOT NULL,
					UserName varchar(255) NOT NULL,
					Password varchar(255) NOT NULL,
					PRIMARY KEY (CustomerNum,UsermNum)
					) DEFAULT CHARSET=utf8";
				Db.NonQ(command);
				*/

				/*
				command="DROP TABLE IF EXISTS providerm";
				Db.NonQ(command);
				command=@"CREATE TABLE providerm (
					CustomerNum bigint NOT NULL,
					ProvNum bigint NOT NULL,
					Abbr varchar(255) NOT NULL,
					IsSecondary tinyint NOT NULL,
					ProvColor int NOT NULL,
					INDEX(CustomerNum),
					INDEX(ProvNum)
					) DEFAULT CHARSET=utf8";
				Db.NonQ(command);
				*/

				/*
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="DROP TABLE IF EXISTS medicationm";
					Db.NonQ(command);
					command=@"CREATE TABLE medicationm (
						CustomerNum bigint NOT NULL,
						MedicationNum bigint NOT NULL,
						MedName varchar(255) NOT NULL,
						GenericNum bigint NOT NULL,
						INDEX(CustomerNum),,
						INDEX(MedicationNum),
						INDEX(GenericNum)
						) DEFAULT CHARSET=utf8";
					Db.NonQ(command);
				}
				*/

				/*
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="DROP TABLE IF EXISTS medicationpatm";
					Db.NonQ(command);
					command=@"CREATE TABLE medicationpatm (
						CustomerNum bigint NOT NULL,
						MedicationPatNum bigint NOT NULL,
						PatNum bigint NOT NULL,
						MedicationNum bigint NOT NULL,
						PatNote varchar(255) NOT NULL,
						IsDiscontinued tinyint NOT NULL,
						INDEX(CustomerNum),,
						INDEX(MedicationPatNum),,
						INDEX(PatNum),
						INDEX(MedicationNum)
						) DEFAULT CHARSET=utf8";
					Db.NonQ(command);
				}
				*/

				/*
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="DROP TABLE IF EXISTS allergydefm";
					Db.NonQ(command);
					command=@"CREATE TABLE allergydefm (
						CustomerNum bigint NOT NULL,
						AllergyDefNum bigint NOT NULL,
						Description varchar(255) NOT NULL,
						INDEX(CustomerNum),
						INDEX(AllergyDefNum)
						) DEFAULT CHARSET=utf8";
					Db.NonQ(command);
				}
				*/

				/*
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="DROP TABLE IF EXISTS allergym";
					Db.NonQ(command);
					command=@"CREATE TABLE allergym (
						CustomerNum bigint NOT NULL,
						AllergyNum bigint NOT NULL,
						AllergyDefNum bigint NOT NULL,
						PatNum bigint NOT NULL,
						Reaction varchar(255) NOT NULL,
						StatusIsActive tinyint NOT NULL,
						INDEX(CustomerNum),,
						INDEX(AllergyNum),,
						INDEX(AllergyDefNum),
						INDEX(PatNum)
						) DEFAULT CHARSET=utf8";
					Db.NonQ(command);
				}
				*/

				/*
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="DROP TABLE IF EXISTS diseasedefm";
					Db.NonQ(command);
					command=@"CREATE TABLE diseasedefm (
						CustomerNum bigint NOT NULL,
						DiseaseDefNum bigint NOT NULL,
						DiseaseName varchar(255) NOT NULL,
						INDEX(CustomerNum),
						INDEX(DiseaseDefNum)
						) DEFAULT CHARSET=utf8";
					Db.NonQ(command);
				}
				*/

				/*
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="DROP TABLE IF EXISTS diseasem";
					Db.NonQ(command);
					command=@"CREATE TABLE diseasem (
						CustomerNum bigint NOT NULL,
						DiseaseNum bigint NOT NULL,
						PatNum bigint NOT NULL,
						DiseaseDefNum bigint NOT NULL,
						PatNote varchar(255) NOT NULL,
						ICD9Num bigint NOT NULL,
						ProbStatus tinyint NOT NULL,
						INDEX(CustomerNum),,
						INDEX(DiseaseNum),,
						INDEX(PatNum),,
						INDEX(DiseaseDefNum),
						INDEX(ICD9Num)
						) DEFAULT CHARSET=utf8";
					Db.NonQ(command);
				}
				*/

				/*
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="DROP TABLE IF EXISTS icd9m";
					Db.NonQ(command);
					command=@"CREATE TABLE icd9m (
						CustomerNum bigint NOT NULL,
						ICD9Num bigint NOT NULL,
						ICD9Code varchar(255) NOT NULL,
						Description varchar(255) NOT NULL,
						INDEX(CustomerNum),
						INDEX(ICD9Num)
						) DEFAULT CHARSET=utf8";
					Db.NonQ(command);
				}
				*/

				/*
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="DROP TABLE IF EXISTS medicationm";
					Db.NonQ(command);
					command=@"CREATE TABLE medicationm (
						CustomerNum bigint NOT NULL,
						MedicationNum bigint NOT NULL,
						MedName varchar(255) NOT NULL,
						GenericNum bigint NOT NULL,
						INDEX(CustomerNum),,
						INDEX(MedicationNum),
						INDEX(GenericNum)
						) DEFAULT CHARSET=utf8";
					Db.NonQ(command);
				}
				*/

				/*
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="DROP TABLE IF EXISTS medicationpatm";
					Db.NonQ(command);
					command=@"CREATE TABLE medicationpatm (
						CustomerNum bigint NOT NULL,
						MedicationPatNum bigint NOT NULL,
						PatNum bigint NOT NULL,
						MedicationNum bigint NOT NULL,
						PatNote varchar(255) NOT NULL,
						IsDiscontinued tinyint NOT NULL,
						INDEX(CustomerNum),,
						INDEX(MedicationPatNum),,
						INDEX(PatNum),
						INDEX(MedicationNum)
						) DEFAULT CHARSET=utf8";
					Db.NonQ(command);
				}
				*/

				/*
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="DROP TABLE IF EXISTS allergydefm";
					Db.NonQ(command);
					command=@"CREATE TABLE allergydefm (
						CustomerNum bigint NOT NULL,
						AllergyDefNum bigint NOT NULL,
						Description varchar(255) NOT NULL,
						INDEX(CustomerNum),
						INDEX(AllergyDefNum)
						) DEFAULT CHARSET=utf8";
					Db.NonQ(command);
				}
				*/

				/*
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="DROP TABLE IF EXISTS allergym";
					Db.NonQ(command);
					command=@"CREATE TABLE allergym (
						CustomerNum bigint NOT NULL,
						AllergyNum bigint NOT NULL,
						AllergyDefNum bigint NOT NULL,
						PatNum bigint NOT NULL,
						Reaction varchar(255) NOT NULL,
						StatusIsActive tinyint NOT NULL,
						INDEX(CustomerNum),,
						INDEX(AllergyNum),,
						INDEX(AllergyDefNum),
						INDEX(PatNum)
						) DEFAULT CHARSET=utf8";
					Db.NonQ(command);
				}
				*/

				/*
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="DROP TABLE IF EXISTS diseasedefm";
					Db.NonQ(command);
					command=@"CREATE TABLE diseasedefm (
						CustomerNum bigint NOT NULL,
						DiseaseDefNum bigint NOT NULL,
						DiseaseName varchar(255) NOT NULL,
						INDEX(CustomerNum),
						INDEX(DiseaseDefNum)
						) DEFAULT CHARSET=utf8";
					Db.NonQ(command);
				}
				*/

				/*
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="DROP TABLE IF EXISTS diseasem";
					Db.NonQ(command);
					command=@"CREATE TABLE diseasem (
						CustomerNum bigint NOT NULL,
						DiseaseNum bigint NOT NULL,
						PatNum bigint NOT NULL,
						DiseaseDefNum bigint NOT NULL,
						PatNote varchar(255) NOT NULL,
						ICD9Num bigint NOT NULL,
						ProbStatus tinyint NOT NULL,
						INDEX(CustomerNum),,
						INDEX(DiseaseNum),,
						INDEX(PatNum),,
						INDEX(DiseaseDefNum),
						INDEX(ICD9Num)
						) DEFAULT CHARSET=utf8";
					Db.NonQ(command);
				}
				*/

				/*
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="DROP TABLE IF EXISTS icd9m";
					Db.NonQ(command);
					command=@"CREATE TABLE icd9m (
						CustomerNum bigint NOT NULL,
						ICD9Num bigint NOT NULL,
						ICD9Code varchar(255) NOT NULL,
						Description varchar(255) NOT NULL,
						INDEX(CustomerNum),
						INDEX(ICD9Num)
						) DEFAULT CHARSET=utf8";
					Db.NonQ(command);
				}
				*/

				/*
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="DROP TABLE IF EXISTS medicationm";
					Db.NonQ(command);
					command=@"CREATE TABLE medicationm (
						CustomerNum bigint NOT NULL,
						MedicationNum bigint NOT NULL,
						MedName varchar(255) NOT NULL,
						GenericNum bigint NOT NULL,
						INDEX(CustomerNum),,
						INDEX(MedicationNum),
						INDEX(GenericNum)
						) DEFAULT CHARSET=utf8";
					Db.NonQ(command);
				}
				*/

				/*
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="DROP TABLE IF EXISTS medicationpatm";
					Db.NonQ(command);
					command=@"CREATE TABLE medicationpatm (
						CustomerNum bigint NOT NULL,
						MedicationPatNum bigint NOT NULL,
						PatNum bigint NOT NULL,
						MedicationNum bigint NOT NULL,
						PatNote varchar(255) NOT NULL,
						IsDiscontinued tinyint NOT NULL,
						INDEX(CustomerNum),,
						INDEX(MedicationPatNum),,
						INDEX(PatNum),
						INDEX(MedicationNum)
						) DEFAULT CHARSET=utf8";
					Db.NonQ(command);
				}
				*/