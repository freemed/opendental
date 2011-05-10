DROP TABLE IF EXISTS `allergydefm`;

CREATE TABLE `allergydefm` (
  `CustomerNum` bigint(20) NOT NULL,
  `AllergyDefNum` bigint(20) NOT NULL,
  `Description` varchar(255) NOT NULL,
  PRIMARY KEY (`CustomerNum`,`AllergyDefNum`),
  KEY `CustomerNum` (`CustomerNum`),
  KEY `AllergyDefNum` (`AllergyDefNum`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

/*Table structure for table `allergym` */

DROP TABLE IF EXISTS `allergym`;

CREATE TABLE `allergym` (
  `CustomerNum` bigint(20) NOT NULL,
  `AllergyNum` bigint(20) NOT NULL,
  `AllergyDefNum` bigint(20) NOT NULL,
  `PatNum` bigint(20) NOT NULL,
  `Reaction` varchar(255) NOT NULL,
  `StatusIsActive` tinyint(4) NOT NULL,
  PRIMARY KEY (`CustomerNum`,`AllergyNum`),
  KEY `CustomerNum` (`CustomerNum`),
  KEY `AllergyNum` (`AllergyNum`),
  KEY `AllergyDefNum` (`AllergyDefNum`),
  KEY `PatNum` (`PatNum`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

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

/*Table structure for table `diseasedefm` */

DROP TABLE IF EXISTS `diseasedefm`;

CREATE TABLE `diseasedefm` (
  `CustomerNum` bigint(20) NOT NULL,
  `DiseaseDefNum` bigint(20) NOT NULL,
  `DiseaseName` varchar(255) NOT NULL,
  PRIMARY KEY (`CustomerNum`,`DiseaseDefNum`),
  KEY `CustomerNum` (`CustomerNum`),
  KEY `DiseaseDefNum` (`DiseaseDefNum`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

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
  PRIMARY KEY (`CustomerNum`,`DiseaseNum`),
  KEY `CustomerNum` (`CustomerNum`),
  KEY `DiseaseNum` (`DiseaseNum`),
  KEY `PatNum` (`PatNum`),
  KEY `DiseaseDefNum` (`DiseaseDefNum`),
  KEY `ICD9Num` (`ICD9Num`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

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
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

/*Table structure for table `labpanelm` */

DROP TABLE IF EXISTS `labpanelm`;

CREATE TABLE `labpanelm` (
  `CustomerNum` bigint(20) NOT NULL,
  `LabPanelNum` bigint(20) NOT NULL,
  `PatNum` bigint(20) NOT NULL,
  `LabNameAddress` varchar(255) NOT NULL,
  `SpecimenCondition` varchar(255) NOT NULL,
  `SpecimenSource` varchar(255) NOT NULL,
  PRIMARY KEY (`CustomerNum`,`LabPanelNum`),
  KEY `CustomerNum` (`CustomerNum`),
  KEY `LabPanelNum` (`LabPanelNum`),
  KEY `PatNum` (`PatNum`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

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
  PRIMARY KEY (`CustomerNum`,`LabResultNum`),
  KEY `CustomerNum` (`CustomerNum`),
  KEY `LabResultNum` (`LabResultNum`),
  KEY `LabPanelNum` (`LabPanelNum`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

/*Table structure for table `medicationm` */

DROP TABLE IF EXISTS `medicationm`;

CREATE TABLE `medicationm` (
  `CustomerNum` bigint(20) NOT NULL,
  `MedicationNum` bigint(20) NOT NULL,
  `MedName` varchar(255) NOT NULL,
  `GenericNum` bigint(20) NOT NULL,
  PRIMARY KEY (`CustomerNum`,`MedicationNum`),
  KEY `CustomerNum` (`CustomerNum`),
  KEY `MedicationNum` (`MedicationNum`),
  KEY `GenericNum` (`GenericNum`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

/*Table structure for table `medicationpatm` */

DROP TABLE IF EXISTS `medicationpatm`;

CREATE TABLE `medicationpatm` (
  `CustomerNum` bigint(20) NOT NULL,
  `MedicationPatNum` bigint(20) NOT NULL,
  `PatNum` bigint(20) NOT NULL,
  `MedicationNum` bigint(20) NOT NULL,
  `PatNote` varchar(255) NOT NULL,
  `IsDiscontinued` tinyint(4) NOT NULL,
  PRIMARY KEY (`CustomerNum`,`MedicationPatNum`),
  KEY `CustomerNum` (`CustomerNum`),
  KEY `MedicationPatNum` (`MedicationPatNum`),
  KEY `PatNum` (`PatNum`),
  KEY `MedicationNum` (`MedicationNum`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

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
  `OnlinePassword` varchar(255) NOT NULL,
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

/*Table structure for table `providerm` */

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