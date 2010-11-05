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
  PRIMARY KEY (`DentalOfficeID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Table structure for table `webforms_sheet` */

DROP TABLE IF EXISTS `webforms_sheet`;


CREATE TABLE `webforms_sheet` (
  `SheetID` bigint(20) NOT NULL AUTO_INCREMENT,
  `DentalOfficeID` bigint(20) NOT NULL,
  `Description` varchar(255) NOT NULL,
  `SheetType` int(11) NOT NULL,
  `DateTimeSheet` datetime NOT NULL DEFAULT '0001-01-01 00:00:00',
  `FontSize` float NOT NULL,
  `FontName` varchar(255) DEFAULT NULL,
  `Width` int(11) NOT NULL,
  `Height` int(11) NOT NULL,
  `IsLandscape` tinyint(4) NOT NULL,
  PRIMARY KEY (`SheetID`),
  KEY `FK_webforms_sheet_DentalOfficeID` (`DentalOfficeID`),
  CONSTRAINT `FK_webforms_sheet_DentalOfficeID` FOREIGN KEY (`DentalOfficeID`) REFERENCES `webforms_preference` (`DentalOfficeID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;


/*Table structure for table `webforms_sheetdef` */

DROP TABLE IF EXISTS `webforms_sheetdef`;

CREATE TABLE `webforms_sheetdef` (
  `WebSheetDefID` bigint(20) NOT NULL AUTO_INCREMENT,
  `DentalOfficeID` bigint(20) NOT NULL,
  `Description` varchar(255) NOT NULL,
  `SheetType` int(11) NOT NULL,
  `FontSize` float NOT NULL,
  `FontName` varchar(255) NOT NULL,
  `Width` int(11) NOT NULL,
  `Height` int(11) NOT NULL,
  `IsLandscape` tinyint(4) NOT NULL,
  PRIMARY KEY (`WebSheetDefID`),
  KEY `FK_webforms_sheetdef_DentalOfficeID` (`DentalOfficeID`),
  CONSTRAINT `FK_webforms_sheetdef_DentalOfficeID` FOREIGN KEY (`DentalOfficeID`) REFERENCES `webforms_preference` (`DentalOfficeID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Table structure for table `webforms_sheetfield` */


DROP TABLE IF EXISTS `webforms_sheetfield`;


CREATE TABLE `webforms_sheetfield` (
  `SheetFieldID` bigint(20) NOT NULL AUTO_INCREMENT,
  `SheetID` bigint(20) NOT NULL,
  `FieldType` int(11) NOT NULL,
  `FieldName` varchar(255) DEFAULT NULL,
  `FieldValue` text NOT NULL,
  `FontSize` float NOT NULL,
  `FontName` varchar(255) DEFAULT NULL,
  `FontIsBold` tinyint(4) NOT NULL,
  `XPos` int(11) NOT NULL,
  `YPos` int(11) NOT NULL,
  `Width` int(11) NOT NULL,
  `Height` int(11) NOT NULL,
  `GrowthBehavior` int(11) NOT NULL,
  `RadioButtonValue` varchar(255) NOT NULL,
  `RadioButtonGroup` varchar(255) NOT NULL,
  `IsRequired` tinyint(4) NOT NULL,
  PRIMARY KEY (`SheetFieldID`),
   KEY `FK_webforms_sheetfield_SheetID` (`SheetID`),
  CONSTRAINT `FK_webforms_sheetfield_SheetID` FOREIGN KEY (`SheetID`) REFERENCES `webforms_sheet` (`SheetID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;




/*Table structure for table `webforms_sheetfielddef` */

DROP TABLE IF EXISTS `webforms_sheetfielddef`;

CREATE TABLE `webforms_sheetfielddef` (
  `WebSheetFieldDefID` bigint(20) NOT NULL AUTO_INCREMENT,
  `WebSheetDefID` bigint(20) NOT NULL,
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
  `RadioButtonGroup` varchar(255) NOT NULL,
  `IsRequired` tinyint(4) NOT NULL,
  `ImageData` mediumtext NOT NULL,
  PRIMARY KEY (`WebSheetFieldDefID`),
  KEY `FK_webforms_sheetfielddef_WebSheetDefID` (`WebSheetDefID`),
  CONSTRAINT `FK_webforms_sheetfielddef_WebSheetDefID` FOREIGN KEY (`WebSheetDefID`) REFERENCES `webforms_sheetdef` (`WebSheetDefID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
