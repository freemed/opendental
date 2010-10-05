/*
SQLyog Community Edition- MySQL GUI v8.0 
MySQL - 5.1.30-community : Database - odwebservice
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

CREATE DATABASE /*!32312 IF NOT EXISTS*/`odwebservice` /*!40100 DEFAULT CHARACTER SET latin1 */;

USE `odwebservice`;

/*Table structure for table `webforms_preference` */

DROP TABLE IF EXISTS `webforms_preference`;

CREATE TABLE `webforms_preference` (
  `DentalOfficeID` bigint(20) NOT NULL,
  `ColorBorder` int(11) NOT NULL,
  `Heading1` text NOT NULL,
  `Heading2` text NOT NULL,
  PRIMARY KEY (`DentalOfficeID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Table structure for table `webforms_sheet` */

DROP TABLE IF EXISTS `webforms_sheet`;

CREATE TABLE `webforms_sheet` (
  `SheetID` bigint(20) NOT NULL AUTO_INCREMENT,
  `DentalOfficeID` bigint(20) NOT NULL,
  `DateTimeSubmitted` datetime NOT NULL,
  PRIMARY KEY (`SheetID`),
  KEY `FK_webforms_sheet_DentalOfficeID` (`DentalOfficeID`),
  CONSTRAINT `FK_webforms_sheet_DentalOfficeID` FOREIGN KEY (`DentalOfficeID`) REFERENCES `webforms_preference` (`DentalOfficeID`)
) ENGINE=InnoDB AUTO_INCREMENT=148 DEFAULT CHARSET=utf8;

/*Table structure for table `webforms_sheetdef` */

DROP TABLE IF EXISTS `webforms_sheetdef`;

CREATE TABLE `webforms_sheetdef` (
  `WebSheetDefNum` bigint(20) NOT NULL AUTO_INCREMENT,
  `DentalOfficeID` bigint(20) NOT NULL,
  `SheetDefNum` bigint(20) NOT NULL,
  `Description` varchar(255) NOT NULL,
  `SheetType` int(11) NOT NULL,
  `FontSize` float NOT NULL,
  `FontName` varchar(255) NOT NULL,
  `Width` int(11) NOT NULL,
  `Height` int(11) NOT NULL,
  `IsLandscape` tinyint(4) NOT NULL,
  PRIMARY KEY (`WebSheetDefNum`),
  KEY `FK_webforms_sheetdef_DentalOfficeID` (`DentalOfficeID`),
  CONSTRAINT `FK_webforms_sheetdef_DentalOfficeID` FOREIGN KEY (`DentalOfficeID`) REFERENCES `webforms_preference` (`DentalOfficeID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Table structure for table `webforms_sheetfield` */

DROP TABLE IF EXISTS `webforms_sheetfield`;

CREATE TABLE `webforms_sheetfield` (
  `SheetFieldID` bigint(20) NOT NULL AUTO_INCREMENT,
  `SheetID` bigint(20) NOT NULL,
  `FieldName` varchar(255) NOT NULL,
  `FieldValue` text NOT NULL,
  PRIMARY KEY (`SheetFieldID`),
  KEY `FK_webforms_sheetfield_SheetID` (`SheetID`),
  CONSTRAINT `FK_webforms_sheetfield_SheetID` FOREIGN KEY (`SheetID`) REFERENCES `webforms_sheet` (`SheetID`)
) ENGINE=InnoDB AUTO_INCREMENT=2024 DEFAULT CHARSET=utf8;

/*Table structure for table `webforms_sheetfielddef` */

DROP TABLE IF EXISTS `webforms_sheetfielddef`;

CREATE TABLE `webforms_sheetfielddef` (
  `WebSheetFieldDefNum` bigint(20) NOT NULL AUTO_INCREMENT,
  `SheetFieldDefNum` bigint(20) NOT NULL,
  `WebSheetDefNum` bigint(20) NOT NULL,
  `SheetDefNum` bigint(20) NOT NULL,
  `FieldType` int(11) NOT NULL,
  `FieldName` varchar(255) NOT NULL,
  `FieldValue` text NOT NULL,
  `FontSize` float NOT NULL,
  `FontName` varchar(255) NOT NULL,
  `FontIsBold` tinyint(4) NOT NULL,
  `XPos` int(11) NOT NULL,
  `YPos` int(11) NOT NULL,
  `Width` int(11) NOT NULL,
  `Height` int(11) NOT NULL,
  `GrowthBehavior` int(11) NOT NULL,
  `RadioButtonValue` varchar(255) NOT NULL,
  `ImageData` mediumtext NOT NULL,
  PRIMARY KEY (`WebSheetFieldDefNum`),
  KEY `FK_webforms_sheetfielddef_WebSheetDefNum` (`WebSheetDefNum`),
  CONSTRAINT `FK_webforms_sheetfielddef_WebSheetDefNum` FOREIGN KEY (`WebSheetDefNum`) REFERENCES `webforms_sheetdef` (`WebSheetDefNum`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
