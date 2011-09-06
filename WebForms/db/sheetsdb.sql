-- MySQL dump 10.13  Distrib 5.5.9, for Win32 (x86)
--
-- Host: localhost    Database: odwebservice
-- ------------------------------------------------------
-- Server version	5.5.15

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Current Database: `odwebservice`
--

CREATE DATABASE /*!32312 IF NOT EXISTS*/ `odwebservice` /*!40100 DEFAULT CHARACTER SET utf8 */;

USE `odwebservice`;

--
-- Table structure for table `webforms_preference`
--

DROP TABLE IF EXISTS `webforms_preference`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `webforms_preference` (
  `DentalOfficeID` bigint(20) NOT NULL,
  `ColorBorder` int(11) NOT NULL,
  `CultureName` varchar(255) NOT NULL,
  PRIMARY KEY (`DentalOfficeID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `webforms_sheet`
--

DROP TABLE IF EXISTS `webforms_sheet`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
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
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `webforms_sheetdef`
--

DROP TABLE IF EXISTS `webforms_sheetdef`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
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
) ENGINE=InnoDB AUTO_INCREMENT=32 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `webforms_sheetfield`
--

DROP TABLE IF EXISTS `webforms_sheetfield`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
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
  `TabOrder` int(11) NOT NULL,
  PRIMARY KEY (`SheetFieldID`),
  KEY `FK_webforms_sheetfield_SheetID` (`SheetID`),
  CONSTRAINT `FK_webforms_sheetfield_SheetID` FOREIGN KEY (`SheetID`) REFERENCES `webforms_sheet` (`SheetID`)
) ENGINE=InnoDB AUTO_INCREMENT=81 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `webforms_sheetfielddef`
--

DROP TABLE IF EXISTS `webforms_sheetfielddef`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
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
  `TabOrder` int(11) NOT NULL,
  PRIMARY KEY (`WebSheetFieldDefID`),
  KEY `FK_webforms_sheetfielddef_WebSheetDefID` (`WebSheetDefID`),
  CONSTRAINT `FK_webforms_sheetfielddef_WebSheetDefID` FOREIGN KEY (`WebSheetDefID`) REFERENCES `webforms_sheetdef` (`WebSheetDefID`)
) ENGINE=InnoDB AUTO_INCREMENT=874 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2011-09-06 17:34:01
