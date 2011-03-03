/*
SQLyog Community Edition- MySQL GUI v8.0 
MySQL - 5.1.53-community : Database - mobile_dev
*********************************************************************
*/


/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

CREATE DATABASE /*!32312 IF NOT EXISTS*/`mobile_dev` /*!40100 DEFAULT CHARACTER SET utf8 */;

USE `mobile_dev`;

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
  `Note` varchar(255) NOT NULL,
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
  `AddrNote` varchar(255) NOT NULL,
  `ClinicNum` bigint(20) NOT NULL,
  `PreferContactMethod` tinyint(4) NOT NULL,
  PRIMARY KEY (`CustomerNum`,`PatNum`),
  KEY `Guarantor` (`Guarantor`),
  KEY `ClinicNum` (`ClinicNum`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Table structure for table `preference` */

DROP TABLE IF EXISTS `preference`;

CREATE TABLE `preference` (
  `PrefmName` varchar(255) NOT NULL,
  `ValueString` text NOT NULL,
  PRIMARY KEY (`PrefmName`)
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

/*Table structure for table `userm` */

DROP TABLE IF EXISTS `userm`;

CREATE TABLE `userm` (
  `CustomerNum` bigint(20) NOT NULL,
  `UsermNum` bigint(20) NOT NULL,
  `UserName` varchar(255) NOT NULL,
  `Password` varchar(255) NOT NULL,
  PRIMARY KEY (`CustomerNum`,`UsermNum`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

DROP TABLE IF EXISTS `providerm`;

CREATE TABLE `providerm` (
  `CustomerNum` bigint(20) NOT NULL,
  `ProvNum` bigint(20) NOT NULL,
  `Abbr` varchar(255) NOT NULL,
  `IsSecondary` tinyint(4) NOT NULL,
  `ProvColor` int(11) NOT NULL,
  KEY `CustomerNum` (`CustomerNum`),
  KEY `ProvNum` (`ProvNum`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
