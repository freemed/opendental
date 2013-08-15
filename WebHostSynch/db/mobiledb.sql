/*
SQLyog Community Edition- MySQL GUI v8.02 
MySQL - 5.5.15 : Database - mobile_dev
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

CREATE DATABASE /*!32312 IF NOT EXISTS*/`mobile_dev` /*!40100 DEFAULT CHARACTER SET utf8 */;

USE `mobile_dev`;

/*Table structure for table `allergydefm` */

DROP TABLE IF EXISTS `allergydefm`;

CREATE TABLE `allergydefm` (
  `CustomerNum` bigint(20) NOT NULL,
  `AllergyDefNum` bigint(20) NOT NULL,
  `Description` varchar(255) NOT NULL,
  `Snomed` tinyint(4) NOT NULL,
  `MedicationNum` bigint(20) NOT NULL,
  PRIMARY KEY (`CustomerNum`,`AllergyDefNum`),
  KEY `CustomerNum` (`CustomerNum`),
  KEY `AllergyDefNum` (`AllergyDefNum`),
  KEY `MedicationNum` (`MedicationNum`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Table structure for table `allergym` */

DROP TABLE IF EXISTS `allergym`;

CREATE TABLE `allergym` (
  `CustomerNum` bigint(20) NOT NULL,
  `AllergyNum` bigint(20) NOT NULL,
  `AllergyDefNum` bigint(20) NOT NULL,
  `PatNum` bigint(20) NOT NULL,
  `Reaction` varchar(255) NOT NULL,
  `StatusIsActive` tinyint(4) NOT NULL,
  `DateAdverseReaction` date NOT NULL DEFAULT '0001-01-01',
  PRIMARY KEY (`CustomerNum`,`AllergyNum`),
  KEY `CustomerNum` (`CustomerNum`),
  KEY `AllergyNum` (`AllergyNum`),
  KEY `AllergyDefNum` (`AllergyDefNum`),
  KEY `PatNum` (`PatNum`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Table structure for table `appointmentm` */

DROP TABLE IF EXISTS `appointmentm`;

CREATE TABLE `appointmentm` (
  `CustomerNum` bigint(20) NOT NULL,
  `AptNum` bigint(20) NOT NULL,
  `PatNum` bigint(20) NOT NULL,
  `AptStatus` tinyint(4) NOT NULL,
  `Pattern` varchar(255) NOT NULL,
  `Confirmed` bigint(20) NOT NULL,
  `Op` bigint(20) NOT NULL,
  `Note` text,
  `ProvNum` bigint(20) NOT NULL,
  `ProvHyg` bigint(20) NOT NULL,
  `AptDateTime` datetime NOT NULL DEFAULT '0001-01-01 00:00:00',
  `IsNewPatient` tinyint(4) NOT NULL,
  `ProcDescript` varchar(255) NOT NULL,
  `ClinicNum` bigint(20) NOT NULL,
  `IsHygiene` tinyint(4) NOT NULL,
  PRIMARY KEY (`CustomerNum`,`AptNum`),
  KEY `PatNum` (`PatNum`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Table structure for table `diseasedefm` */

DROP TABLE IF EXISTS `diseasedefm`;

CREATE TABLE `diseasedefm` (
  `CustomerNum` bigint(20) NOT NULL,
  `DiseaseDefNum` bigint(20) NOT NULL,
  `DiseaseName` varchar(255) NOT NULL,
  PRIMARY KEY (`CustomerNum`,`DiseaseDefNum`),
  KEY `CustomerNum` (`CustomerNum`),
  KEY `DiseaseDefNum` (`DiseaseDefNum`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Table structure for table `diseasem` */

DROP TABLE IF EXISTS `diseasem`;

CREATE TABLE `diseasem` (
  `CustomerNum` bigint(20) NOT NULL,
  `DiseaseNum` bigint(20) NOT NULL,
  `PatNum` bigint(20) NOT NULL,
  `DiseaseDefNum` bigint(20) NOT NULL,
  `PatNote` varchar(255) NOT NULL,
  `ICD9Num` bigint(20) NOT NULL,
  `ProbStatus` tinyint(4) NOT NULL,
  `DateStart` date NOT NULL DEFAULT '0001-01-01',
  `DateStop` date NOT NULL DEFAULT '0001-01-01',
  PRIMARY KEY (`CustomerNum`,`DiseaseNum`),
  KEY `CustomerNum` (`CustomerNum`),
  KEY `DiseaseNum` (`DiseaseNum`),
  KEY `PatNum` (`PatNum`),
  KEY `DiseaseDefNum` (`DiseaseDefNum`),
  KEY `ICD9Num` (`ICD9Num`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Table structure for table `documentm` */

DROP TABLE IF EXISTS `documentm`;

CREATE TABLE `documentm` (
  `CustomerNum` bigint(20) NOT NULL,
  `DocNum` bigint(20) NOT NULL,
  `PatNum` bigint(20) NOT NULL,
  `RawBase64` mediumtext NOT NULL,
  PRIMARY KEY (`CustomerNum`,`DocNum`),
  KEY `PatNum` (`PatNum`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Table structure for table `icd9m` */

DROP TABLE IF EXISTS `icd9m`;

CREATE TABLE `icd9m` (
  `CustomerNum` bigint(20) NOT NULL,
  `ICD9Num` bigint(20) NOT NULL,
  `ICD9Code` varchar(255) NOT NULL,
  `Description` varchar(255) NOT NULL,
  PRIMARY KEY (`CustomerNum`,`ICD9Num`),
  KEY `CustomerNum` (`CustomerNum`),
  KEY `ICD9Num` (`ICD9Num`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Table structure for table `labpanelm` */

DROP TABLE IF EXISTS `labpanelm`;

CREATE TABLE `labpanelm` (
  `CustomerNum` bigint(20) NOT NULL,
  `LabPanelNum` bigint(20) NOT NULL,
  `PatNum` bigint(20) NOT NULL,
  `LabNameAddress` varchar(255) NOT NULL,
  `SpecimenCondition` varchar(255) NOT NULL,
  `SpecimenSource` varchar(255) NOT NULL,
  `ServiceId` varchar(255) NOT NULL,
  `ServiceName` varchar(255) NOT NULL,
  `MedicalOrderNum` bigint(20) NOT NULL,
  PRIMARY KEY (`CustomerNum`,`LabPanelNum`),
  KEY `CustomerNum` (`CustomerNum`),
  KEY `LabPanelNum` (`LabPanelNum`),
  KEY `PatNum` (`PatNum`),
  KEY `MedicalOrderNum` (`MedicalOrderNum`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Table structure for table `labresultm` */

DROP TABLE IF EXISTS `labresultm`;

CREATE TABLE `labresultm` (
  `CustomerNum` bigint(20) NOT NULL,
  `LabResultNum` bigint(20) NOT NULL,
  `LabPanelNum` bigint(20) NOT NULL,
  `DateTimeTest` datetime NOT NULL DEFAULT '0001-01-01 00:00:00',
  `TestName` varchar(255) NOT NULL,
  `TestID` varchar(255) NOT NULL,
  `ObsValue` varchar(255) NOT NULL,
  `ObsUnits` varchar(255) NOT NULL,
  `ObsRange` varchar(255) NOT NULL,
  `AbnormalFlag` tinyint(4) NOT NULL,
  PRIMARY KEY (`CustomerNum`,`LabResultNum`),
  KEY `CustomerNum` (`CustomerNum`),
  KEY `LabResultNum` (`LabResultNum`),
  KEY `LabPanelNum` (`LabPanelNum`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Table structure for table `medicationm` */

DROP TABLE IF EXISTS `medicationm`;

CREATE TABLE `medicationm` (
  `CustomerNum` bigint(20) NOT NULL,
  `MedicationNum` bigint(20) NOT NULL,
  `MedName` varchar(255) NOT NULL,
  `GenericNum` bigint(20) NOT NULL,
  `RxCui` bigint(20) NOT NULL,
  PRIMARY KEY (`CustomerNum`,`MedicationNum`),
  KEY `CustomerNum` (`CustomerNum`),
  KEY `MedicationNum` (`MedicationNum`),
  KEY `GenericNum` (`GenericNum`),
  KEY `RxCui` (`RxCui`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Table structure for table `medicationpatm` */

DROP TABLE IF EXISTS `medicationpatm`;

CREATE TABLE `medicationpatm` (
  `CustomerNum` bigint(20) NOT NULL,
  `MedicationPatNum` bigint(20) NOT NULL,
  `PatNum` bigint(20) NOT NULL,
  `MedicationNum` bigint(20) NOT NULL,
  `PatNote` varchar(255) NOT NULL,
  `DateStart` date NOT NULL DEFAULT '0001-01-01',
  `DateStop` date NOT NULL DEFAULT '0001-01-01',
  PRIMARY KEY (`CustomerNum`,`MedicationPatNum`),
  KEY `CustomerNum` (`CustomerNum`),
  KEY `MedicationPatNum` (`MedicationPatNum`),
  KEY `PatNum` (`PatNum`),
  KEY `MedicationNum` (`MedicationNum`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Table structure for table `patientm` */

DROP TABLE IF EXISTS `patientm`;

CREATE TABLE `patientm` (
  `CustomerNum` bigint(20) NOT NULL,
  `PatNum` bigint(20) NOT NULL,
  `LName` varchar(255) NOT NULL,
  `FName` varchar(255) NOT NULL,
  `MiddleI` varchar(255) NOT NULL,
  `Preferred` varchar(255) NOT NULL,
  `PatStatus` tinyint(4) NOT NULL,
  `Gender` tinyint(4) NOT NULL,
  `Position` tinyint(4) NOT NULL,
  `Birthdate` date NOT NULL DEFAULT '0001-01-01',
  `Address` varchar(255) NOT NULL,
  `Address2` varchar(255) NOT NULL,
  `City` varchar(255) NOT NULL,
  `State` varchar(255) NOT NULL,
  `Zip` varchar(255) NOT NULL,
  `HmPhone` varchar(255) NOT NULL,
  `WkPhone` varchar(255) NOT NULL,
  `WirelessPhone` varchar(255) NOT NULL,
  `Guarantor` bigint(20) NOT NULL,
  `Email` varchar(255) NOT NULL,
  `AddrNote` text ,
  `ClinicNum` bigint(20) NOT NULL,
  `PreferContactMethod` tinyint(4) NOT NULL,
  `OnlinePassword` varchar(255) NOT NULL,
  `InsEst` double NOT NULL,
  `BalTotal` double NOT NULL,
  PRIMARY KEY (`CustomerNum`,`PatNum`),
  KEY `Guarantor` (`Guarantor`),
  KEY `ClinicNum` (`ClinicNum`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Table structure for table `pharmacym` */

DROP TABLE IF EXISTS `pharmacym`;

CREATE TABLE `pharmacym` (
  `CustomerNum` bigint(20) NOT NULL,
  `PharmacyNum` bigint(20) NOT NULL,
  `StoreName` varchar(255) NOT NULL,
  `Phone` varchar(255) NOT NULL,
  `Fax` varchar(255) NOT NULL,
  `Address` varchar(255) NOT NULL,
  `Address2` varchar(255) NOT NULL,
  `City` varchar(255) NOT NULL,
  `State` varchar(255) NOT NULL,
  `Zip` varchar(255) NOT NULL,
  `Note` varchar(255) NOT NULL,
  PRIMARY KEY (`CustomerNum`,`PharmacyNum`),
  KEY `CustomerNum` (`CustomerNum`),
  KEY `PharmacyNum` (`PharmacyNum`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Table structure for table `preferencem` */

DROP TABLE IF EXISTS `preferencem`;

CREATE TABLE `preferencem` (
  `CustomerNum` bigint(20) NOT NULL,
  `PrefNum` bigint(20) NOT NULL,
  `PrefName` varchar(255) NOT NULL,
  `ValueString` text NOT NULL,
  PRIMARY KEY (`CustomerNum`,`PrefNum`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Table structure for table `providerm` */

DROP TABLE IF EXISTS `providerm`;

CREATE TABLE `providerm` (
  `CustomerNum` bigint(20) NOT NULL,
  `ProvNum` bigint(20) NOT NULL,
  `Abbr` varchar(255) NOT NULL,
  `IsSecondary` tinyint(4) NOT NULL,
  `ProvColor` int(11) NOT NULL,
  PRIMARY KEY (`CustomerNum`,`ProvNum`),
  KEY `CustomerNum` (`CustomerNum`),
  KEY `ProvNum` (`ProvNum`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Table structure for table `recallm` */

DROP TABLE IF EXISTS `recallm`;

CREATE TABLE `recallm` (
  `CustomerNum` bigint(20) NOT NULL,
  `RecallNum` bigint(20) NOT NULL,
  `PatNum` bigint(20) NOT NULL,
  `DateDue` date NOT NULL DEFAULT '0001-01-01',
  `DatePrevious` date NOT NULL DEFAULT '0001-01-01',
  `RecallStatus` bigint(20) NOT NULL,
  `Note` text,
  `IsDisabled` tinyint(3) unsigned NOT NULL DEFAULT '0',
  `DisableUntilBalance` double NOT NULL,
  `DisableUntilDate` date NOT NULL DEFAULT '0001-01-01',
  PRIMARY KEY (`CustomerNum`,`RecallNum`),
  KEY `PatNum` (`PatNum`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Table structure for table `rxpatm` */

DROP TABLE IF EXISTS `rxpatm`;

CREATE TABLE `rxpatm` (
  `CustomerNum` bigint(20) NOT NULL,
  `RxNum` bigint(20) NOT NULL,
  `PatNum` bigint(20) NOT NULL,
  `RxDate` date NOT NULL DEFAULT '0001-01-01',
  `Drug` varchar(255) NOT NULL,
  `Sig` varchar(255) NOT NULL,
  `Disp` varchar(255) NOT NULL,
  `Refills` varchar(255) NOT NULL,
  `ProvNum` bigint(20) NOT NULL,
  PRIMARY KEY (`CustomerNum`,`RxNum`),
  KEY `PatNum` (`PatNum`),
  KEY `ProvNum` (`ProvNum`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Table structure for table `statementm` */

DROP TABLE IF EXISTS `statementm`;

CREATE TABLE `statementm` (
  `CustomerNum` bigint(20) NOT NULL,
  `StatementNum` bigint(20) NOT NULL,
  `PatNum` bigint(20) NOT NULL,
  `DateSent` date NOT NULL DEFAULT '0001-01-01',
  `DocNum` bigint(20) NOT NULL,
  PRIMARY KEY (`CustomerNum`,`StatementNum`),
  KEY `PatNum` (`PatNum`),
  KEY `DocNum` (`DocNum`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Table structure for table `userm` */

DROP TABLE IF EXISTS `userm`;

CREATE TABLE `userm` (
  `CustomerNum` bigint(20) NOT NULL,
  `UsermNum` bigint(20) NOT NULL,
  `UserName` varchar(255) NOT NULL,
  `Password` varchar(255) NOT NULL,
  PRIMARY KEY (`CustomerNum`,`UsermNum`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;



/* to enable registrationkey related stuff use below statements*/

INSERT INTO `registrationkey` (`RegistrationKeyNum`, `PatNum`, `RegKey`, `Note`, `DateStarted`, `DateDisabled`, `DateEnded`, `IsForeign`, `UsesServerVersion`, `IsFreeVersion`, `IsOnlyForTesting`, `VotesAllotted`) VALUES('1604','6219','CWX7HIOQBJ73GMFU','','2011-03-01','0001-01-01','0001-01-01','0','0','0','1','100');
INSERT INTO `registrationkey` (`RegistrationKeyNum`, `PatNum`, `RegKey`, `Note`, `DateStarted`, `DateDisabled`, `DateEnded`, `IsForeign`, `UsesServerVersion`, `IsFreeVersion`, `IsOnlyForTesting`, `VotesAllotted`) VALUES('1672','6566','3QTNG1DFX680P50V','','2011-04-16','0001-01-01','0001-01-01','0','0','0','1','100');

INSERT INTO `repeatcharge` (`RepeatChargeNum`, `PatNum`, `ProcCode`, `ChargeAmt`, `DateStart`, `DateStop`, `Note`) VALUES('3767','6219','027','0','2011-03-01','0001-01-01','');
INSERT INTO `repeatcharge` (`RepeatChargeNum`, `PatNum`, `ProcCode`, `ChargeAmt`, `DateStart`, `DateStop`, `Note`) VALUES('3931','6566','027','0','2011-04-16','0001-01-01','');

INSERT INTO `patient` (`PatNum`, `LName`, `FName`, `MiddleI`, `Preferred`, `PatStatus`, `Gender`, `Position`, `Birthdate`, `SSN`, `Address`, `Address2`, `City`, `State`, `Zip`, `HmPhone`, `WkPhone`, `WirelessPhone`, `Guarantor`, `CreditType`, `Email`, `Salutation`, `EstBalance`, `PriProv`, `SecProv`, `FeeSched`, `BillingType`, `ImageFolder`, `AddrNote`, `FamFinUrgNote`, `MedUrgNote`, `ApptModNote`, `StudentStatus`, `SchoolName`, `ChartNumber`, `MedicaidID`, `Bal_0_30`, `Bal_31_60`, `Bal_61_90`, `BalOver90`, `InsEst`, `BalTotal`, `EmployerNum`, `EmploymentNote`, `Race`, `County`, `GradeLevel`, `Urgency`, `DateFirstVisit`, `ClinicNum`, `HasIns`, `TrophyFolder`, `PlannedIsDone`, `Premed`, `Ward`, `PreferConfirmMethod`, `PreferContactMethod`, `PreferRecallMethod`, `SchedBeforeTime`, `SchedAfterTime`, `SchedDayOfWeek`, `Language`, `AdmitDate`, `Title`, `PayPlanDue`, `SiteNum`, `DateTStamp`, `ResponsParty`, `CanadianEligibilityCode`, `AskToArriveEarly`, `OnlinePassword`, `SmokingSnoMed`) VALUES('6219','MobileDemo','MobileDemo','','','0','0','0','0001-01-01','','','','','','','','','','6219','','','','0','1','0','0','164','MobileDemoMobileDemo6219','','','','','','','','','0','0','0','0','0','0','0','','0','','0','0','0001-01-01','0','','','0','0','','0','0','0','00:00:00','00:00:00','0','','0001-01-01','','0','0','2011-03-01 15:30:12','0','0','0','','');
INSERT INTO `patient` (`PatNum`, `LName`, `FName`, `MiddleI`, `Preferred`, `PatStatus`, `Gender`, `Position`, `Birthdate`, `SSN`, `Address`, `Address2`, `City`, `State`, `Zip`, `HmPhone`, `WkPhone`, `WirelessPhone`, `Guarantor`, `CreditType`, `Email`, `Salutation`, `EstBalance`, `PriProv`, `SecProv`, `FeeSched`, `BillingType`, `ImageFolder`, `AddrNote`, `FamFinUrgNote`, `MedUrgNote`, `ApptModNote`, `StudentStatus`, `SchoolName`, `ChartNumber`, `MedicaidID`, `Bal_0_30`, `Bal_31_60`, `Bal_61_90`, `BalOver90`, `InsEst`, `BalTotal`, `EmployerNum`, `EmploymentNote`, `Race`, `County`, `GradeLevel`, `Urgency`, `DateFirstVisit`, `ClinicNum`, `HasIns`, `TrophyFolder`, `PlannedIsDone`, `Premed`, `Ward`, `PreferConfirmMethod`, `PreferContactMethod`, `PreferRecallMethod`, `SchedBeforeTime`, `SchedAfterTime`, `SchedDayOfWeek`, `Language`, `AdmitDate`, `Title`, `PayPlanDue`, `SiteNum`, `DateTStamp`, `ResponsParty`, `CanadianEligibilityCode`, `AskToArriveEarly`, `OnlinePassword`, `SmokingSnoMed`) VALUES('6566','OnlineTest','','','','0','0','0','0001-01-01','','','','','','','','','','6566','','','','0','1','0','0','164','MobileTest6566','','','','','','','','','0','0','0','0','0','0','0','','0','','0','0','0001-01-01','0','','','0','0','','0','0','0','00:00:00','00:00:00','0','','0001-01-01','','0','0','2011-04-16 22:04:56','0','0','0','','');

/* data for mobile demo*/

delete from  allergy;
delete from  allergydef;
delete from  appointment;
delete from  disease;
delete from  diseasedef;
delete from  labpanel;
delete from  labresult;
delete from  medication;
delete from  medicationpat;
delete from  patient;
delete from  pharmacy;
delete from  rxpat;



insert  into `allergy`(`AllergyNum`,`AllergyDefNum`,`PatNum`,`Reaction`,`StatusIsActive`,`DateTStamp`,`DateAdverseReaction`) values (1,1,1,'Nausea ',1,'2011-06-14 11:51:45','2003-05-05'),(2,2,7,'Hives ',1,'2011-06-14 11:51:46','2004-03-03'),(3,1,7,'Wheezing ',1,'2011-06-14 11:51:46','0001-01-01'),(5,3,7,'Wheezing ',1,'2011-06-14 11:51:46','2003-05-05'),(6,2,1,'Hives ',1,'2011-06-14 11:51:46','2004-03-03'),(7,1,10,'Wheezing',1,'2011-06-14 11:51:49','2003-05-05'),(8,2,10,'Hives',1,'2011-06-14 11:51:46','2004-03-03'),(9,1,11,'Wheezing',1,'2011-06-14 11:51:47','2003-05-05'),(10,2,11,'Hives',1,'2011-06-14 11:51:47','2004-03-03'),(11,1,12,'Wheezing',1,'2011-06-14 11:51:47','2003-05-05'),(12,1,13,'Nausea',1,'2011-06-14 11:51:50','2003-05-05'),(13,2,13,'Hives',1,'2011-06-14 11:51:51','2003-05-05'),(14,3,13,'Wheezing',1,'2011-06-14 11:51:51','2003-05-05'),(15,1,14,'Nausea',1,'2011-06-14 11:51:51','2003-05-05'),(16,2,14,'Nausea',1,'2011-06-14 11:51:51','2003-05-05'),(17,3,14,'Wheezing',1,'2011-06-14 11:51:51','2003-05-05'),(18,1,15,'Nausea',1,'2011-06-14 11:51:52','2003-05-05'),(19,2,8,'Hives',1,'2011-06-14 11:51:53','2003-05-05'),(20,1,9,'Wheezing',1,'2011-06-14 11:51:54','2003-05-05');

/*Data for the table `allergydef` */

insert  into `allergydef`(`AllergyDefNum`,`Description`,`IsHidden`,`DateTStamp`,`Snomed`,`MedicationNum`) values (1,'Penicillin',0,'2011-06-14 10:06:34',0,0),(2,'Aspirin',0,'2011-06-14 10:06:38',0,0),(3,'Codeine',0,'2011-06-14 10:59:20',0,0);

/*Data for the table `appointment` */

insert  into `appointment`(`AptNum`,`PatNum`,`AptStatus`,`Pattern`,`Confirmed`,`TimeLocked`,`Op`,`Note`,`ProvNum`,`ProvHyg`,`AptDateTime`,`NextAptNum`,`UnschedStatus`,`IsNewPatient`,`ProcDescript`,`Assistant`,`ClinicNum`,`IsHygiene`,`DateTStamp`,`DateTimeArrived`,`DateTimeSeated`,`DateTimeDismissed`,`InsPlan1`,`InsPlan2`,`DateTimeAskedToArrive`,`ProcsColored`) values (28,14,1,'//XX//',19,0,4,'pt has braces',1,0,'2011-02-15 14:00:00',0,0,0,'4-BWX',0,0,0,'2011-04-20 15:52:38','0001-01-01 00:00:00','2011-02-04 00:00:00','2011-02-04 00:00:00',0,0,'0001-01-01 00:00:00','\"<span color=\"\"-16777216\"\">4-BWX</span>\"\r'),(26,11,1,'//XX////',21,1,4,'crown fell off',1,0,'2011-02-14 09:30:00',0,0,0,'',0,0,0,'2011-06-14 10:00:24','2011-02-14 00:00:00','2011-02-14 00:00:00','2011-02-14 00:00:00',0,0,'0001-01-01 00:00:00',''),(27,13,1,'//XXXXXX//',19,0,4,'discuss finances',2,0,'2011-02-14 16:00:00',0,0,0,'Flo, Ex, Pro',0,0,0,'2011-04-20 15:52:32','0001-01-01 00:00:00','0001-01-01 00:00:00','0001-01-01 00:00:00',0,0,'0001-01-01 00:00:00','\"<span color=\"\"-16777216\"\">Flo</span><span color=\"\"-16\"\r'),(25,15,1,'//XX//',19,0,4,'added 20 minutes for discussion',2,0,'2011-02-18 12:50:00',0,0,0,'Pano',0,0,0,'2011-04-20 15:52:20','0001-01-01 00:00:00','2011-04-04 00:00:00','2011-04-04 00:00:00',0,0,'0001-01-01 00:00:00','\"<span color=\"\"-16777216\"\">Pano</span>\"\r'),(24,16,5,'//XXXXXX//',19,0,3,'Moved from last month',1,0,'2011-02-13 13:00:00',0,0,0,'Flo, Ex, Pro',0,0,0,'2011-04-20 15:52:13','2011-02-07 00:00:00','2011-02-07 00:00:00','2011-02-07 00:00:00',0,0,'0001-01-01 00:00:00','<span color=\"-16777216\">Flo</span><span color=\"-16777216\">Ex</span><span color=\"-16777216\">Pro</span>'),(23,10,1,'//XX//',19,0,4,'Start early',1,0,'2011-02-16 14:50:00',0,0,0,'Pro',0,0,0,'2011-04-20 15:52:05','2011-02-16 00:00:00','2011-02-16 00:00:00','2011-02-16 00:00:00',0,0,'0001-01-01 00:00:00','<span color=\"-16777216\">Pro</span>'),(22,20,1,'//XX',19,1,4,'In pain',1,0,'2011-02-13 09:00:00',0,0,0,'Pro',0,0,0,'2011-04-20 15:51:21','2011-02-13 00:00:00','2011-02-13 00:00:00','2011-02-13 00:00:00',0,0,'0001-01-01 00:00:00','<span color=\"-16777216\">Pro</span>'),(21,19,1,'//XXXXXX//',19,0,4,'Check labcase',2,0,'2011-02-16 10:40:00',0,0,0,'Flo, Ex, Pro',0,0,0,'2011-04-20 15:51:36','2011-02-16 00:00:00','2011-02-16 00:00:00','2011-02-16 00:00:00',0,0,'0001-01-01 00:00:00','<span color=\"-16777216\">Flo</span><span color=\"-16777216\">Ex</span><span color=\"-16777216\">Pro</span>'),(20,18,1,'//XXXXXX//////',21,1,3,'Running late',5,5,'2011-02-13 09:30:00',0,92,0,'2-BWX',0,0,1,'2011-06-14 11:28:29','2011-02-13 00:00:00','2011-02-13 00:00:00','2011-02-13 00:00:00',0,0,'0001-01-01 00:00:00','<span color=\"-16777216\">2-BWX</span>'),(19,16,1,'//XX//////',0,0,3,'In pain',5,0,'2011-02-12 15:00:00',0,0,0,'Ex',0,0,0,'2011-06-14 11:17:27','2011-02-12 00:00:00','2011-02-12 11:00:00','2011-02-12 11:00:00',0,0,'2011-02-12 11:00:00','<span color=\"-16777216\">Ex</span>'),(18,15,1,'//XXXXXX//',19,0,4,'In pain',2,0,'2011-02-17 08:10:00',0,0,0,'Flo, Ex, Pro',0,0,0,'2011-04-20 15:51:00','0001-01-01 00:00:00','0001-01-01 00:00:00','0001-01-01 00:00:00',0,0,'0001-01-01 00:00:00','\"<span color=\"\"-16777216\"\">Flo</span><span color=\"\"-16\"\r'),(17,14,1,'//XXXXXX//',19,0,4,'Collect payment before sending back.',2,0,'2011-02-12 11:00:00',0,0,0,'Flo, Ex, Pro',0,0,0,'2011-04-20 15:50:30','0001-01-01 00:00:00','0001-01-01 00:00:00','0001-01-01 00:00:00',0,0,'0001-01-01 00:00:00','\"<span color=\"\"-16777216\"\">Flo</span><span color=\"\"-16\"\r'),(11,13,1,'//XX//',19,0,2,'Review TP',1,0,'2011-02-12 09:10:00',0,0,0,'4-BWX',0,0,0,'2011-06-14 10:04:20','0001-01-01 00:00:00','0001-01-01 00:00:00','0001-01-01 00:00:00',0,0,'0001-01-01 00:00:00','\"<span color=\"\"-16777216\"\">4-BWX</span>\"\r'),(9,12,1,'//XXXXXX//',19,0,4,'In pain',2,0,'2011-02-13 16:00:00',0,0,0,'Flo, Ex, Pro',0,0,0,'2011-06-14 11:32:28','2011-02-13 00:00:00','2011-02-13 00:00:00','2011-02-13 00:00:00',0,0,'0001-01-01 00:00:00','<span color=\"-16777216\">Flo</span><span color=\"-16777216\">Ex</span><span color=\"-16777216\">Pro</span>'),(8,11,1,'//XXXXXX//',21,0,4,'Moved from last week.',1,1,'2011-02-14 11:10:00',0,92,0,'Flo, Ex, Pro',0,0,0,'2011-04-20 15:49:33','2011-02-14 00:00:00','2011-02-14 00:00:00','2011-02-14 00:00:00',0,0,'0001-01-01 00:00:00','<span color=\"-16777216\">Flo</span><span color=\"-16777216\">Ex</span><span color=\"-16777216\">Pro</span>'),(7,10,1,'//////////////////////////////////////////',21,1,2,'need to call him',2,0,'2011-02-15 09:20:00',0,92,0,'2-BWX',0,0,0,'2011-04-20 09:05:15','2011-02-05 00:00:00','2011-02-05 00:00:00','2011-02-05 00:00:00',0,0,'0001-01-01 00:00:00','<span color=\"-16777216\">2-BWX</span>'),(6,9,1,'////XXXX//////',19,1,3,'may be late',1,2,'2011-02-13 11:10:00',0,0,0,'Pano',0,0,0,'2011-06-14 11:16:54','2011-02-13 00:00:00','2011-02-13 00:00:00','2011-02-13 00:00:00',0,0,'0001-01-01 00:00:00','<span color=\"-16777216\">Pano</span>'),(5,8,1,'//XX//',19,0,4,'Pain',4,0,'2011-02-16 12:20:00',0,92,0,'Ex',0,0,0,'2011-04-20 15:48:56','2011-02-16 00:00:00','2011-02-16 00:00:00','2011-02-16 00:00:00',0,0,'2011-02-16 12:15:00','<span color=\"-16777216\">Ex</span>'),(1,7,1,'//XXXXXX////////////////////',19,1,4,'Pt. paid for in advance!',1,1,'2011-02-22 12:30:00',0,92,0,'Flo, Ex',0,0,1,'2011-06-14 09:20:48','2011-02-22 00:00:00','2011-02-22 00:00:00','2011-02-22 00:00:00',0,0,'0001-01-01 00:00:00','<span color=\"-16777216\">Flo</span><span color=\"-16777216\">Ex</span>'),(655,17,1,'//XX//////',20,1,4,'Moved from last month',1,0,'2011-02-17 09:10:00',0,97,1,'4-BWX',0,0,0,'2011-04-20 15:53:28','2011-02-14 18:13:00','2011-02-14 18:13:00','2011-02-14 18:13:00',0,0,'0001-01-01 00:00:00','<span color=\"-16777216\">4-BWX</span>'),(656,7,1,'//XX//',19,0,5,'',1,0,'2011-06-15 13:20:00',0,0,0,'2-BWX',0,0,0,'2011-06-15 12:48:12','0001-01-01 00:00:00','0001-01-01 00:00:00','0001-01-01 00:00:00',0,0,'0001-01-01 00:00:00','<span color=\"-16777216\">2-BWX</span>');
/*Data for the table `disease` */

insert  into `disease`(`DiseaseNum`,`PatNum`,`DiseaseDefNum`,`PatNote`,`DateTStamp`,`ICD9Num`,`ProbStatus`,`DateStart`,`DateStop`) values (1,7,1,'','2011-06-09 15:33:57',0,0,'0001-01-01','0001-01-01'),(2,7,0,'','2011-06-11 10:26:48',147,0,'2011-01-09','2011-06-09'),(3,7,0,'','2011-06-11 10:24:15',2412,0,'0001-01-01','0001-01-01'),(4,7,0,'','2011-06-10 16:55:17',14765,0,'0001-01-01','0001-01-01'),(5,7,2,'','2011-06-11 10:20:51',0,0,'0001-01-01','0001-01-01'),(6,7,0,'','2011-06-11 10:21:46',14608,0,'0001-01-01','0001-01-01');

/*Data for the table `diseasedef` */

insert  into `diseasedef`(`DiseaseDefNum`,`DiseaseName`,`ItemOrder`,`IsHidden`,`DateTStamp`) values (1,'prob1',0,0,'2011-06-09 13:41:50'),(2,'problems galore',1,0,'2011-06-11 10:20:48');

/*Data for the table `labpanel` */

insert  into `labpanel`(`LabPanelNum`,`PatNum`,`RawMessage`,`LabNameAddress`,`DateTStamp`,`SpecimenCondition`,`SpecimenSource`,`ServiceId`,`ServiceName`,`MedicalOrderNum`) values (1,7,'','lab name 1 address1','2011-06-09 16:41:52','specimen condition1','0958&Location 1&HL70070','serviceloinc1','servicename1',0),(2,7,'','lab2','2011-06-11 10:19:53','asdsad','0052&location x&HL70070','service2','loinc service2',0);

/*Data for the table `labresult` */

insert  into `labresult`(`LabResultNum`,`LabPanelNum`,`DateTimeTest`,`TestName`,`DateTStamp`,`TestID`,`ObsValue`,`ObsUnits`,`ObsRange`,`AbnormalFlag`) values (1,1,'2011-06-09 16:41:59','Test name1','2011-06-09 16:47:39','Loinc1','34','cm3','50-45',0),(2,2,'2011-06-10 16:22:57','result 3 ','2011-06-11 10:20:10','loinc3','87','nautical miles','50 miles',0),(3,2,'2011-06-10 16:26:12','dsds','2011-06-11 10:20:03','loin2','436','657','47',0);

/*Data for the table `medication` */

insert  into `medication`(`MedicationNum`,`MedName`,`GenericNum`,`Notes`,`DateTStamp`,`RxCui`) values (1,'drug1',1,'','2011-06-09 13:42:33',0),(2,'drug2',2,'dsf','2011-06-09 13:46:01',0),(3,'drug1 brand1',1,'','2011-06-09 15:36:18',0),(4,'drug3',4,'aaaa','2011-06-09 15:41:38',0),(5,'drug4',5,'','2011-06-09 15:41:48',0);

/*Data for the table `medicationpat` */

insert  into `medicationpat`(`MedicationPatNum`,`PatNum`,`MedicationNum`,`PatNote`,`DateTStamp`,`DateStart`,`DateStop`) values (1,7,1,'drug1 med1','2011-06-09 15:40:30','0001-01-01','0001-01-01'),(2,7,2,'med2','2011-06-09 15:33:57','2011-06-03','2011-07-09'),(3,7,2,'discont','2011-06-09 15:46:19','2011-05-09','0001-01-01'),(4,7,4,'refill 4 etc.','2011-06-09 15:42:03','0001-01-01','0001-01-01'),(5,7,5,'','2011-06-09 15:42:22','2011-01-09','2011-06-09');

/*Data for the table `patient` */

insert  into `patient`(`PatNum`,`LName`,`FName`,`MiddleI`,`Preferred`,`PatStatus`,`Gender`,`Position`,`Birthdate`,`SSN`,`Address`,`Address2`,`City`,`State`,`Zip`,`HmPhone`,`WkPhone`,`WirelessPhone`,`Guarantor`,`CreditType`,`Email`,`Salutation`,`EstBalance`,`PriProv`,`SecProv`,`FeeSched`,`BillingType`,`ImageFolder`,`AddrNote`,`FamFinUrgNote`,`MedUrgNote`,`ApptModNote`,`StudentStatus`,`SchoolName`,`ChartNumber`,`MedicaidID`,`Bal_0_30`,`Bal_31_60`,`Bal_61_90`,`BalOver90`,`InsEst`,`BalTotal`,`EmployerNum`,`EmploymentNote`,`Race`,`County`,`GradeLevel`,`Urgency`,`DateFirstVisit`,`ClinicNum`,`HasIns`,`TrophyFolder`,`PlannedIsDone`,`Premed`,`Ward`,`PreferConfirmMethod`,`PreferContactMethod`,`PreferRecallMethod`,`SchedBeforeTime`,`SchedAfterTime`,`SchedDayOfWeek`,`Language`,`AdmitDate`,`Title`,`PayPlanDue`,`SiteNum`,`DateTStamp`,`ResponsParty`,`CanadianEligibilityCode`,`AskToArriveEarly`,`OnlinePassword`,`SmokingSnoMed`,`PreferContactConfidential`) values (1,'Jonathan','White','','',0,0,0,'1975-03-23','','','','','','','','','',1,'','','',0,0,0,0,0,'','','','','','','','','',0,0,0,0,0,0,0,'',0,'',0,0,'0001-01-01',0,'','',0,0,'',0,0,0,'00:00:00','00:00:00',0,'','0001-01-01','',0,0,'2011-06-14 11:50:30',0,0,0,'dE!2N7q_3',0,0),(7,'Mathew','Dennis','','',0,0,1,'1975-04-04','','W','Wet','Ewt','EW','we','416-268-5212','416-268-5212','416-268-5212',7,'','dennis_mathew2000@yahoo.com','',0,0,0,0,40,'MathewDennis7','','','','','','','','',0,0,0,0,0,0,0,'',0,'',0,0,'2011-01-22',0,'','',0,0,'',0,0,0,'00:00:00','00:00:00',0,'','0001-01-01','',0,0,'2011-06-09 18:11:12',0,0,0,'dE!2N7q_3',0,0),(8,'Spander','Jason','M','Jay',0,0,1,'1968-03-10','','31 Drewry Av','','','','1256','503-363-7246','503-363-1246','503-585-6421',9,'','jspander@gmail.com','',0,1,0,0,40,'SpanderJason9','','','','','','','','',0,0,0,0,0,0,0,'',0,'',0,0,'2011-02-02',0,'','',0,0,'',0,2,0,'00:00:00','00:00:00',0,'','0001-01-01','Mr',0,0,'2011-06-14 11:50:31',0,0,5,'dE!2N7q_3',0,0),(9,'Adams','Mike','R','',0,0,0,'1985-04-17','','','','','','','503-363-7246','503-363-1246','503-585-6421',10,'','mike34@yahoo.com','',0,1,0,0,40,'ReynoldsMike10','','','','','','','','',0,0,0,0,0,0,0,'',0,'',0,0,'2011-02-05',0,'','',0,0,'',0,0,0,'00:00:00','00:00:00',0,'','0001-01-01','',0,0,'2011-06-14 11:50:32',0,0,0,'dE!2N7q_3',0,0),(10,'Williams','John','A','',0,0,0,'1974-02-05','','','','','','','503-363-7246','503-363-1246','503-585-6421',11,'','wjohn@hotmail.com','',0,1,0,0,40,'WilliamsJohn11','','','','','','','','',0,0,0,0,0,0,0,'',0,'',0,0,'2011-02-16',0,'','',0,0,'',0,0,0,'00:00:00','00:00:00',0,'','0001-01-01','',0,0,'2011-06-14 11:50:33',0,0,0,'dE!2N7q_3',0,0),(11,'Allen','Sarah','S','',0,1,0,'1990-11-23','','','','','','','503-363-7246','503-363-1246','503-585-6421',13,'','scarpenter@rogers.com','',0,1,0,0,40,'CarpenterSarah13','','','','','','','','',0,0,0,0,0,0,0,'',0,'',0,0,'2011-02-10',0,'','',0,0,'',0,0,0,'00:00:00','00:00:00',0,'','0001-01-01','',0,0,'2011-06-14 11:50:34',0,0,0,'dE!2N7q_3',0,0),(15,'White','Rose','B','',0,1,0,'1981-01-20','','','','','','','503-363-7246','503-363-1246','503-585-6421',15,'','rose.white@yahoo.com','',0,1,0,0,40,'','','','','','','','','',0,0,0,0,0,0,0,'',0,'',0,0,'2011-04-04',0,'','',0,0,'',0,0,0,'00:00:00','00:00:00',0,'','0001-01-01','',0,0,'2011-06-14 11:50:35',0,0,0,'dE!2N7q_3',0,0),(16,'Stump','Anna','B','',0,1,0,'1983-10-16','','','','','','','503-363-7246','503-363-1246','503-585-6421',16,'','anna4562@gmail.com','',0,1,0,0,40,'StumpAnna16','','','','','','','','',0,0,0,0,0,0,0,'',0,'',0,0,'2011-02-07',0,'','',0,0,'',0,0,0,'00:00:00','00:00:00',0,'','0001-01-01','',0,0,'2011-06-14 11:50:35',0,0,0,'dE!2N7q_3',0,0),(14,'Stump','Roger','','',0,0,0,'1961-11-04','','','','','','','503-363-7246','503-363-1246','503-585-6421',17,'','rogerstump53@gmail.com','',0,1,0,0,40,'','','','','','','','','',0,0,0,0,0,0,0,'',0,'',0,0,'2011-02-04',0,'','',0,0,'',0,0,0,'00:00:00','00:00:00',0,'','0001-01-01','',0,0,'2011-06-14 11:50:36',0,0,0,'dE!2N7q_3',0,0),(12,'Roberts','Barbara','','Barb',0,1,3,'1950-04-07','','','','','','','503-363-7246','503-363-1246','503-585-6421',18,'','barb_2000@yahoo.com','',0,1,0,0,40,'RobertsBarbara18','','','','','','','','',0,0,0,0,0,0,0,'',0,'',0,0,'2011-02-13',0,'','',0,0,'',0,0,0,'00:00:00','00:00:00',0,'','0001-01-01','',0,0,'2011-06-14 11:50:36',0,0,0,'dE!2N7q_3',0,0),(13,'Shulfer','Mary','','',0,1,0,'1964-06-03','','','','','','','503-363-7246','503-363-1246','503-585-6421',19,'','shulferm@hotmail.com','',0,1,0,0,40,'ShulferMary19','','','','','','','','',0,0,0,0,0,0,0,'',0,'',0,0,'0001-01-01',0,'','',0,0,'',0,0,0,'00:00:00','00:00:00',0,'','0001-01-01','',0,0,'2011-06-14 11:50:37',0,0,0,'dE!2N7q_3',0,0),(17,'Anderson','Bill','','',0,0,0,'1976-05-02','','','','','','','403-545-4464','403-554-4384','403-439-8023',20,'','anderbil@tech.com','',0,1,0,0,40,'AndersonBill17',NULL,NULL,'','','','','','',0,0,0,0,0,0,0,'',0,'',0,0,'2011-02-17',0,'','',0,0,'',0,0,0,'00:00:00','00:00:00',0,'','0001-01-01','Mr',0,0,'2011-06-14 11:50:37',0,0,0,'dE!2N7q_3',0,0),(18,'Abbot','Bejamin','','',0,0,0,'1945-04-04','','','','','','','403-545-4464','403-554-4384','403-439-8023',20,'','','',0,1,0,0,40,'','','','','','','','','',0,0,0,0,0,0,0,'',0,'',0,0,'2011-02-12',0,'','',0,0,'',0,0,0,'00:00:00','00:00:00',0,'','0001-01-01','',0,0,'2011-06-14 11:50:38',0,0,0,'dE!2N7q_3',0,0),(19,'Andrews','Patrick','','',0,0,0,'1940-08-02','','','','','','','403-545-4464','403-554-4384','403-439-8023',21,'','','',0,1,0,0,40,'AndrewsPatrick19','','','','','','','','',0,0,0,0,0,0,0,'',0,'',0,0,'2011-02-16',0,'','',0,0,'',0,0,0,'00:00:00','00:00:00',0,'','0001-01-01','',0,0,'2011-06-14 11:50:39',0,0,0,'dE!2N7q_3',0,0),(20,'Bailey','Kevin','','',0,0,0,'1965-12-17','','','','','','','403-545-4464','403-554-4384','403-439-8023',22,'','','',0,1,0,0,40,'BaileyKevin20','','','','','','','','',0,0,0,0,0,0,0,'',0,'',0,0,'2011-02-13',0,'','',0,0,'',0,0,0,'00:00:00','00:00:00',0,'','0001-01-01','',0,0,'2011-06-14 11:50:41',0,0,0,'dE!2N7q_3','',0);

/*Data for the table `pharmacy` */

insert  into `pharmacy`(`PharmacyNum`,`PharmID`,`StoreName`,`Phone`,`Fax`,`Address`,`Address2`,`City`,`State`,`Zip`,`Note`,`DateTStamp`) values (1,'','pharmacy1','(444)444-4444','(666)666-6666','blah blah street','blah blah street','asdddddddddddd','','','note2... note2... note2... note2... ','2011-06-02 16:58:49'),(2,'','Pharmacy 2','(444)444-4444','(777)777-7777','ddddddddddd','hhhhhhhhhhh','hhhhhhhhhhhhhhhhhhhh','','','note2... note2... note2... note2... note2... note2... note2... note2... note2... note2... note2... note2... note2... note2... note2... note2... note2... note2... note2... note2... note2... note2... note2... note2... note2... note2... note2... note2... not','2011-06-02 16:59:59');

/*Data for the table `provider` */

/*
insert  into `provider`(`ProvNum`,`Abbr`,`ItemOrder`,`LName`,`FName`,`MI`,`Suffix`,`FeeSched`,`Specialty`,`SSN`,`StateLicense`,`DEANum`,`IsSecondary`,`ProvColor`,`IsHidden`,`UsingTIN`,`BlueCrossID`,`SigOnFile`,`MedicaidID`,`OutlineColor`,`SchoolClassNum`,`NationalProvID`,`CanadianOfficeNum`,`DateTStamp`,`AnesthProvType`,`TaxonomyCodeOverride`,`IsCDAnet`,`EcwID`,`EhrKey`) values (5,'DOC1',2,'Priestly','Martin','','',53,0,'','','',0,-8355585,0,0,'',0,'',-2830136,0,'','','2011-06-14 10:04:55',0,'',0,'',''),(2,'HYG',1,'Jones','Tina','','',53,1,'','','',1,-17956,0,0,'',1,'',-11711155,0,'','','2008-12-04 11:16:42',0,'',0,'',''),(1,'DOC',0,'Albert','Brian','L','',53,0,'1522864182','52186144','TL12265',0,-4602138,0,1,'',1,'',-11711155,0,'51236994785','','2008-12-19 16:16:07',0,'',0,'','');
*/
/*Data for the table `rxpat` */

insert  into `rxpat`(`RxNum`,`PatNum`,`RxDate`,`Drug`,`Sig`,`Disp`,`Refills`,`ProvNum`,`Notes`,`PharmacyNum`,`IsControlled`,`DateTStamp`,`SendStatus`) values (1,9,'2011-02-03','Penicillin VK','','40','3',1,'Notes 1',0,0,'2011-02-08 20:34:51',0),(2,9,'2011-02-03','Vicodin','','16 (sixteen)','12',1,'',0,1,'2011-02-03 17:38:26',0),(6,9,'2011-02-03','Peridex','','1','12',1,'',0,1,'2011-02-08 22:06:03',0),(7,10,'2011-02-03','Vicodin','','16 (sixteen)','12',1,'',0,1,'2011-02-03 17:38:32',0),(8,10,'2011-02-03','Peridex','','1','12',1,'',0,1,'2011-02-08 22:06:04',0),(9,11,'2011-02-03','Vicodin','','16 (sixteen)','12',1,'',0,1,'2011-02-03 17:38:37',0),(10,12,'2011-02-03','Peridex','','1','12',1,'',0,1,'2011-02-08 22:06:05',0),(11,13,'2011-02-03','Vicodin','','20 (twenty)','12',1,'',0,1,'2011-02-08 20:34:13',0),(12,14,'2011-02-03','Penicillin VK','','20','12',1,'',0,1,'2011-02-08 22:06:31',0),(13,15,'2011-02-03','Vicodin','','16 (sixteen)','12',1,'',0,1,'2011-02-08 22:28:54',0),(14,16,'2011-02-03','Penicillin VK','','18','12',1,'',0,1,'2011-02-08 22:28:57',0),(15,17,'2011-02-03','Penicillin VK','','20','12',1,'',0,1,'2011-02-08 22:29:01',0),(16,18,'2011-02-03','Vicodin','','16 (sixteen)','12',1,'',0,1,'2011-02-08 22:29:03',0),(17,19,'2011-02-03','Penicillin VK','','10','12',1,'',0,1,'2011-02-08 22:29:06',0);

UPDATE allergy SET DateTStamp=NOW();
UPDATE allergydef SET DateTStamp=NOW();
UPDATE appointment SET DateTStamp=NOW();
UPDATE disease SET DateTStamp=NOW();
UPDATE diseasedef SET DateTStamp=NOW();
UPDATE labpanel SET DateTStamp=NOW();
UPDATE labresult SET DateTStamp=NOW();
UPDATE medication SET DateTStamp=NOW();
UPDATE medicationpat SET DateTStamp=NOW();
UPDATE patient SET DateTStamp=NOW();
UPDATE pharmacy SET DateTStamp=NOW();
UPDATE rxpat SET DateTStamp=NOW();