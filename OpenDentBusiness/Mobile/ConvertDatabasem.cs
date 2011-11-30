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
					command="ALTER TABLE allergydefm ADD Snomed tinyint NOT NULL";
					Db.NonQ(command);
				}
				else {//oracle
					command="ALTER TABLE allergydefm ADD Snomed number(3)";
					Db.NonQ(command);
					command="UPDATE allergydefm SET Snomed = 0 WHERE Snomed IS NULL";
					Db.NonQ(command);
					command="ALTER TABLE allergydefm MODIFY Snomed NOT NULL";
					Db.NonQ(command);
				}				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="ALTER TABLE allergydefm ADD MedicationNum bigint NOT NULL";
					Db.NonQ(command);
					command="ALTER TABLE allergydefm ADD INDEX (MedicationNum)";
					Db.NonQ(command);
				}
				else {//oracle
					command="ALTER TABLE allergydefm ADD MedicationNum number(20)";
					Db.NonQ(command);
					command="UPDATE allergydefm SET MedicationNum = 0 WHERE MedicationNum IS NULL";
					Db.NonQ(command);
					command="ALTER TABLE allergydefm MODIFY MedicationNum NOT NULL";
					Db.NonQ(command);
					command=@"CREATE INDEX allergydefm_MedicationNum ON allergydefm (MedicationNum)";
					Db.NonQ(command);
				}
				*/

				/*				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="ALTER TABLE allergym ADD DateAdverseReaction date NOT NULL DEFAULT '0001-01-01')";
					Db.NonQ(command);
				}
				else {//oracle
					command="ALTER TABLE allergym ADD DateAdverseReaction date";
					Db.NonQ(command);
					command="UPDATE allergym SET DateAdverseReaction = TO_DATE('0001-01-01','YYYY-MM-DD') WHERE DateAdverseReaction IS NULL";
					Db.NonQ(command);
					command="ALTER TABLE allergym MODIFY DateAdverseReaction NOT NULL";
					Db.NonQ(command);
				}
				*/

				/*				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="ALTER TABLE diseasem ADD DateStart date NOT NULL DEFAULT '0001-01-01')";
					Db.NonQ(command);
				}
				else {//oracle
					command="ALTER TABLE diseasem ADD DateStart date";
					Db.NonQ(command);
					command="UPDATE diseasem SET DateStart = TO_DATE('0001-01-01','YYYY-MM-DD') WHERE DateStart IS NULL";
					Db.NonQ(command);
					command="ALTER TABLE diseasem MODIFY DateStart NOT NULL";
					Db.NonQ(command);
				}				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="ALTER TABLE diseasem ADD DateStop date NOT NULL DEFAULT '0001-01-01')";
					Db.NonQ(command);
				}
				else {//oracle
					command="ALTER TABLE diseasem ADD DateStop date";
					Db.NonQ(command);
					command="UPDATE diseasem SET DateStop = TO_DATE('0001-01-01','YYYY-MM-DD') WHERE DateStop IS NULL";
					Db.NonQ(command);
					command="ALTER TABLE diseasem MODIFY DateStop NOT NULL";
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
				}				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="ALTER TABLE labpanelm ADD MedicalOrderNum bigint NOT NULL";
					Db.NonQ(command);
					command="ALTER TABLE labpanelm ADD INDEX (MedicalOrderNum)";
					Db.NonQ(command);
				}
				else {//oracle
					command="ALTER TABLE labpanelm ADD MedicalOrderNum number(20)";
					Db.NonQ(command);
					command="UPDATE labpanelm SET MedicalOrderNum = 0 WHERE MedicalOrderNum IS NULL";
					Db.NonQ(command);
					command="ALTER TABLE labpanelm MODIFY MedicalOrderNum NOT NULL";
					Db.NonQ(command);
					command=@"CREATE INDEX labpanelm_MedicalOrderNum ON labpanelm (MedicalOrderNum)";
					Db.NonQ(command);
				}
				*/

				/*				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="ALTER TABLE labresultm ADD AbnormalFlag tinyint NOT NULL";
					Db.NonQ(command);
				}
				else {//oracle
					command="ALTER TABLE labresultm ADD AbnormalFlag number(3)";
					Db.NonQ(command);
					command="UPDATE labresultm SET AbnormalFlag = 0 WHERE AbnormalFlag IS NULL";
					Db.NonQ(command);
					command="ALTER TABLE labresultm MODIFY AbnormalFlag NOT NULL";
					Db.NonQ(command);
				}
				*/

				/*				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="ALTER TABLE medicationm ADD RxCui bigint NOT NULL";
					Db.NonQ(command);
					command="ALTER TABLE medicationm ADD INDEX (RxCui)";
					Db.NonQ(command);
				}
				else {//oracle
					command="ALTER TABLE medicationm ADD RxCui number(20)";
					Db.NonQ(command);
					command="UPDATE medicationm SET RxCui = 0 WHERE RxCui IS NULL";
					Db.NonQ(command);
					command="ALTER TABLE medicationm MODIFY RxCui NOT NULL";
					Db.NonQ(command);
					command=@"CREATE INDEX medicationm_RxCui ON medicationm (RxCui)";
					Db.NonQ(command);
				}
				*/

				/*				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="ALTER TABLE medicationpatm ADD DateStart date NOT NULL DEFAULT '0001-01-01')";
					Db.NonQ(command);
				}
				else {//oracle
					command="ALTER TABLE medicationpatm ADD DateStart date";
					Db.NonQ(command);
					command="UPDATE medicationpatm SET DateStart = TO_DATE('0001-01-01','YYYY-MM-DD') WHERE DateStart IS NULL";
					Db.NonQ(command);
					command="ALTER TABLE medicationpatm MODIFY DateStart NOT NULL";
					Db.NonQ(command);
				}				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="ALTER TABLE medicationpatm ADD DateStop date NOT NULL DEFAULT '0001-01-01')";
					Db.NonQ(command);
				}
				else {//oracle
					command="ALTER TABLE medicationpatm ADD DateStop date";
					Db.NonQ(command);
					command="UPDATE medicationpatm SET DateStop = TO_DATE('0001-01-01','YYYY-MM-DD') WHERE DateStop IS NULL";
					Db.NonQ(command);
					command="ALTER TABLE medicationpatm MODIFY DateStop NOT NULL";
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
					command="ALTER TABLE patientm ADD InsEst double NOT NULL";
					Db.NonQ(command);
				}
				else {//oracle
					command="ALTER TABLE patientm ADD InsEst number(38,8)";
					Db.NonQ(command);
					command="UPDATE patientm SET InsEst = 0 WHERE InsEst IS NULL";
					Db.NonQ(command);
					command="ALTER TABLE patientm MODIFY InsEst NOT NULL";
					Db.NonQ(command);
				}				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="ALTER TABLE patientm ADD BalTotal double NOT NULL";
					Db.NonQ(command);
				}
				else {//oracle
					command="ALTER TABLE patientm ADD BalTotal number(38,8)";
					Db.NonQ(command);
					command="UPDATE patientm SET BalTotal = 0 WHERE BalTotal IS NULL";
					Db.NonQ(command);
					command="ALTER TABLE patientm MODIFY BalTotal NOT NULL";
					Db.NonQ(command);
				}
				*/

				/*
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="DROP TABLE IF EXISTS documentm";
					Db.NonQ(command);
					command=@"CREATE TABLE documentm (
						CustomerNum bigint NOT NULL,
						DocNum bigint NOT NULL,
						PatNum bigint NOT NULL,
						RawBase64 text NOT NULL,
						INDEX(CustomerNum),,
						INDEX(DocNum),
						INDEX(PatNum)
						) DEFAULT CHARSET=utf8";
					Db.NonQ(command);
				}
				*/

				/*
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="DROP TABLE IF EXISTS statementm";
					Db.NonQ(command);
					command=@"CREATE TABLE statementm (
						CustomerNum bigint NOT NULL,
						StatementNum bigint NOT NULL,
						PatNum bigint NOT NULL,
						DateSent date NOT NULL DEFAULT '0001-01-01',
						DocNum bigint NOT NULL,
						INDEX(CustomerNum),,
						INDEX(StatementNum),,
						INDEX(PatNum),
						INDEX(DocNum)
						) DEFAULT CHARSET=utf8";
					Db.NonQ(command);
				}
				*/

				/*
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="DROP TABLE IF EXISTS recallm";
					Db.NonQ(command);
					command=@"CREATE TABLE recallm (
						CustomerNum bigint NOT NULL,
						RecallNum bigint NOT NULL,
						PatNum bigint NOT NULL,
						DateDue date NOT NULL DEFAULT '0001-01-01',
						DatePrevious date NOT NULL DEFAULT '0001-01-01',
						RecallStatus bigint NOT NULL,
						Note varchar(255) NOT NULL,
						IsDisabled tinyint NOT NULL,
						DisableUntilBalance double NOT NULL,
						DisableUntilDate date NOT NULL DEFAULT '0001-01-01',
						INDEX(CustomerNum),,
						INDEX(RecallNum),,
						INDEX(PatNum),
						INDEX(RecallStatus)
						) DEFAULT CHARSET=utf8";
					Db.NonQ(command);
				}
				*/