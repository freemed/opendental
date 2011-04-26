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
					command="DROP TABLE IF EXISTS labpanelm";
					Db.NonQ(command);
					command=@"CREATE TABLE labpanelm (
						CustomerNum bigint NOT NULL,
						LabPanelNum bigint NOT NULL,
						PatNum bigint NOT NULL,
						MedicalOrderNum bigint NOT NULL,
						RawMessage text NOT NULL,
						LabNameAddress varchar(255) NOT NULL,
						SpecimenCode varchar(255) NOT NULL,
						SpecimenDesc varchar(255) NOT NULL,
						INDEX(CustomerNum),,
						INDEX(LabPanelNum),,
						INDEX(PatNum),
						INDEX(MedicalOrderNum)
						) DEFAULT CHARSET=utf8";
					Db.NonQ(command);
				}
				*/

				/*
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="DROP TABLE IF EXISTS labresultm";
					Db.NonQ(command);
					command=@"CREATE TABLE labresultm (
						CustomerNum bigint NOT NULL,
						LabResultNum bigint NOT NULL,
						LabPanelNum bigint NOT NULL,
						DateTimeTest datetime NOT NULL DEFAULT '0001-01-01 00:00:00',
						TestName varchar(255) NOT NULL,
						TestID varchar(255) NOT NULL,
						ValueType tinyint NOT NULL,
						ObsValue varchar(255) NOT NULL,
						DrugUnitNum bigint NOT NULL,
						INDEX(CustomerNum),,
						INDEX(LabResultNum),,
						INDEX(LabPanelNum),
						INDEX(DrugUnitNum)
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

				/*				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="ALTER TABLE patientm ADD OnlinePassword varchar(255) NOT NULL";
					Db.NonQ(command);
				}
				else {//oracle
					command="ALTER TABLE patientm ADD OnlinePassword varchar2(255)";
					Db.NonQ(command);
				}
				*/