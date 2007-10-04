using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental.Eclaims{
	///<summary>Summary description for Renaissance.</summary>
	public class Renaissance{
		private static ClaimFormItem[] items;
		private static FormClaimFormItemEdit FormCFI;
		private static string[][] DisplayStrings;

		///<summary>Summary description for Renaissance.</summary>
		public Renaissance(){
			
		}

		///<summary>Called from Eclaims and includes multiple claims.</summary>
		public static string SendBatch(List<ClaimSendQueueItem> queueItems,int batchNum){
			for(int i=0;i<queueItems.Count;i++){
				if(!CreateClaim(queueItems[i].PatNum,queueItems[i].ClaimNum,batchNum))
				{
					return "";
				}
			}
			return "Sent";
		}

		///<summary>Called once for each claim to be created.  For claims with a lot of procedures, this may actually create multiple claims.</summary>
		public static bool CreateClaim(int patNum, int claimNum,int batchNum){
			//this can be eliminated later after error checking complete:
				FormCFI=new FormClaimFormItemEdit();
				FormCFI.FillFieldNames();
			items=new ClaimFormItem[241];//0 is not used 
			Fill(1,"IsPreAuth");
			Fill(2,"IsStandardClaim");
			Fill(3,"IsMedicaidClaim");
			Fill(4,"");//EPSTD
			Fill(5,"TreatingProviderSpecialty");
			Fill(6,"PreAuthString");
			Fill(7,"PriInsCarrierName");
			Fill(8,"PriInsAddressComplete");
			Fill(9,"PriInsCity");
			Fill(10,"PriInsST");
			Fill(11,"PriInsZip");
			Fill(12,"PatientLastFirst");
			Fill(13,"PatientAddressComplete");
			Fill(14,"PatientCity");
			Fill(15,"PatientST");
			Fill(16,"PatientDOB","MM/dd/yyyy");
			Fill(17,"PatientID-MedicaidOrSSN");
			Fill(18,"PatientIsMale");
			Fill(19,"PatientIsFemale");
			Fill(20,"PatientPhone");
			Fill(21,"PatientZip");
			Fill(22,"RelatIsSelf");
			Fill(23,"RelatIsSpouse");
			Fill(24,"RelatIsChild");
			Fill(25,"RelatIsOther");
			Fill(26,"");//relat other descript
			Fill(27,"");//pat employer/school
			Fill(28,"");//pat employer/school address
			Fill(29,"SubscrID");
			Fill(30,"EmployerName");
			Fill(31,"GroupNum");
			Fill(32,"OtherInsNotExists");
			Fill(33,"OtherInsExists");
			Fill(34,"OtherInsExists");//dental
			Fill(35,"");//other ins exists medical
			Fill(36,"OtherInsSubscrID");
			Fill(37,"SubscrLastFirst");
			Fill(38,"OtherInsSubscrLastFirst");
			Fill(39,"SubscrAddressComplete");
			Fill(40,"SubscrPhone");
			Fill(41,"OtherInsSubscrDOB","MM/dd/yyyy");
			Fill(42,"OtherInsSubscrIsMale");
			Fill(43,"OtherInsSubscrIsFemale");
			Fill(44,"OtherInsGroupNum");//Other Plan/Program name
			Fill(45,"SubscrCity");
			Fill(46,"SubscrST");
			Fill(47,"SubscrZip");
			Fill(48,"");//other subscr employer/school
			Fill(49,"");//other subscr emp/school address
			Fill(50,"SubscrDOB","MM/dd/yyyy");
			Fill(51,"SubscrIsMarried");
			Fill(52,"SubscrIsSingle");
			Fill(53,"");//subscr marital status other
			Fill(54,"SubscrIsMale");
			Fill(55,"SubscrIsFemale");
			Fill(56,"");//subscr is employed
			Fill(57,"");//subscr is part time
			Fill(58,"SubscrIsFTStudent");
			Fill(59,"SubscrIsPTStudent");
			Fill(60,"");//subsc employer/school
			Fill(61,"");//subsc employer/school address
			Fill(62,"PatientRelease");
			Fill(63,"PatientReleaseDate","MM/dd/yyyy");
			Fill(64,"PatientAssignment");
			Fill(65,"PatientAssignmentDate","MM/dd/yyyy");
			Fill(66,"BillingDentist");
			Fill(67,"BillingDentistPhoneFormatted");
			Fill(68,"BillingDentistMedicaidID");
			Fill(69,"BillingDentistSSNorTIN");
			Fill(70,"BillingDentistAddress");
			Fill(71,"BillingDentistAddress2");
			Fill(72,"BillingDentistLicenseNum");
			Fill(73,"DateService","MM/dd/yyyy");
			Fill(74,"PlaceIsOffice");
			Fill(75,"PlaceIsHospADA2002");
			Fill(76,"PlaceIsExtCareFacilityADA2002");
			Fill(77,"PlaceIsOtherADA2002");
			Fill(78,"BillingDentistCity");
			Fill(79,"BillingDentistST");
			Fill(80,"BillingDentistZip");
			Fill(81,"IsRadiographsAttached");
			Fill(82,"RadiographsNumAttached");
			Fill(83,"RadiographsNotAttached");
			Fill(84,"IsOrtho");
			Fill(85,"IsNotOrtho");
			Fill(86,"IsInitialProsth");
			Fill(87,"IsReplacementProsth");
			Fill(88,"");//reason for replacement
			Fill(89,"DatePriorProsthPlaced","MM/dd/yyyy");
			Fill(90,"DateOrthoPlaced");
			Fill(91,"MonthsOrthoRemaining");
			Fill(92,"IsNotOccupational");
			Fill(93,"IsOccupational");
			Fill(94,"");//description of occupational injury
			Fill(95,"IsAutoAccident");
			Fill(96,"IsOtherAccident");
			Fill(97,"IsNotAccident");
			Fill(98,"");//description of accident
			Fill(99,"TreatingDentistNPI");//Individual NPI
			Fill(100,"");//''
			Fill(101,"BillingDentistNPI");//Group NPI
			Fill(102,"");//''
			Fill(103,"");//''
			Fill(104,"");//''
			Fill(105,"");//''
			Fill(106,"");//''
			//proc 1
			Fill(107,"P1Date","MM/dd/yyyy");
			Fill(108,"P1ToothNumOrArea");
			Fill(109,"P1Surface");
			Fill(110,"");//diag index
			Fill(111,"P1Code");
			Fill(112,"");//quantity
			Fill(113,"P1Description");
			Fill(114,"P1Fee");
			//proc 2
			Fill(115,"P2Date","MM/dd/yyyy");
			Fill(116,"P2ToothNumOrArea");
			Fill(117,"P2Surface");
			Fill(118,"");
			Fill(119,"P2Code");
			Fill(120,"");
			Fill(121,"P2Description");
			Fill(122,"P2Fee");
			//proc 3
			Fill(123,"P3Date","MM/dd/yyyy");
			Fill(124,"P3ToothNumOrArea");
			Fill(125,"P3Surface");
			Fill(126,"");
			Fill(127,"P3Code");
			Fill(128,"");
			Fill(129,"P3Description");
			Fill(130,"P3Fee");
			//proc 4
			Fill(131,"P4Date","MM/dd/yyyy");
			Fill(132,"P4ToothNumOrArea");
			Fill(133,"P4Surface");
			Fill(134,"");
			Fill(135,"P4Code");
			Fill(136,"");
			Fill(137,"P4Description");
			Fill(138,"P4Fee");
			//proc 5
			Fill(139,"P5Date","MM/dd/yyyy");
			Fill(140,"P5ToothNumOrArea");
			Fill(141,"P5Surface");
			Fill(142,"");
			Fill(143,"P5Code");
			Fill(144,"");
			Fill(145,"P5Description");
			Fill(146,"P5Fee");
			//proc 6
			Fill(147,"P6Date","MM/dd/yyyy");
			Fill(148,"P6ToothNumOrArea");
			Fill(149,"P6Surface");
			Fill(150,"");
			Fill(151,"P6Code");
			Fill(152,"");
			Fill(153,"P6Description");
			Fill(154,"P6Fee");
			//proc 7
			Fill(155,"P7Date","MM/dd/yyyy");
			Fill(156,"P7ToothNumOrArea");
			Fill(157,"P7Surface");
			Fill(158,"");
			Fill(159,"P7Code");
			Fill(160,"");
			Fill(161,"P7Description");
			Fill(162,"P7Fee");
			//proc 8
			Fill(163,"P8Date","MM/dd/yyyy");
			Fill(164,"P8ToothNumOrArea");
			Fill(165,"P8Surface");
			Fill(166,"");
			Fill(167,"P8Code");
			Fill(168,"");
			Fill(169,"P8Description");
			Fill(170,"P8Fee");
			//end of procs
			Fill(171,"TotalFee");
			Fill(172,"");//payment by other plan. They want us to fill this in, but we don't know how.
			Fill(173,"");//max allowable
			Fill(174,"");//deductible
			Fill(175,"");//carrier percent
			Fill(176,"");//carrier pays
			Fill(177,"");//patient pays
			Fill(178,"Miss1");
			Fill(179,"Miss2");
			Fill(180,"Miss3");
			Fill(181,"Miss4");
			Fill(182,"Miss5");
			Fill(183,"Miss6");
			Fill(184,"Miss7");
			Fill(185,"Miss8");
			Fill(186,"Miss9");
			Fill(187,"Miss10");
			Fill(188,"Miss11");
			Fill(189,"Miss12");
			Fill(190,"Miss13");
			Fill(191,"Miss14");
			Fill(192,"Miss15");
			Fill(193,"Miss16");
			Fill(194,"");//A through
			Fill(195,"");
			Fill(196,"");
			Fill(197,"");
			Fill(198,"");
			Fill(199,"");
			Fill(200,"");
			Fill(201,"");
			Fill(202,"");
			Fill(203,"");//through J
			Fill(204,"Miss32");
			Fill(205,"Miss31");
			Fill(206,"Miss30");
			Fill(207,"Miss29");
			Fill(208,"Miss28");
			Fill(209,"Miss27");
			Fill(210,"Miss26");
			Fill(211,"Miss25");
			Fill(212,"Miss24");
			Fill(213,"Miss23");
			Fill(214,"Miss22");
			Fill(215,"Miss21");
			Fill(216,"OtherInsCarrierName");
			Fill(217,"OtherInsAddress");
			Fill(218,"Miss18");
			Fill(219,"Miss17");
			Fill(220,"");//T through
			Fill(221,"");
			Fill(222,"");
			Fill(223,"");
			Fill(224,"");
			Fill(225,"");
			Fill(226,"");
			Fill(227,"");
			Fill(228,"");
			Fill(229,"");//through K
			Fill(230,"Remarks");
			Fill(231,"");//remarks line 2
			Fill(232,"");//remarks line 3
			Fill(233,"");//remarks line 4
			Fill(234,"TreatingDentistSignature");
			Fill(235,"TreatingDentistLicense");
			Fill(236,"TreatingDentistSigDate");
			Fill(237,"TreatingDentistAddress");
			Fill(238,"TreatingDentistCity");
			Fill(239,"TreatingDentistST");
			Fill(240,"TreatingDentistZip");
			ClaimFormItem[] listForForm=new ClaimFormItem[241];
			//items.CopyTo(ClaimFormItems.ListForForm,0);
			FormClaimPrint FormCP=new FormClaimPrint();
			FormCP.ThisClaimNum=claimNum;
			FormCP.ThisPatNum=patNum;
			FormCP.ClaimFormCur=new ClaimForm();
			FormCP.ClaimFormCur.Items=new ClaimFormItem[items.Length];
			items.CopyTo(FormCP.ClaimFormCur.Items,0);
			DisplayStrings=FormCP.FillRenaissance();
			SaveFile(batchNum);
			return true;
		}

		private static void Fill(int index,string fieldName,string formatString){
			if(fieldName!=""){
				for(int i=0;i<FormCFI.FieldNames.Length;i++){
					if(FormCFI.FieldNames[i]==fieldName){
						break;
					}
					if(i==FormCFI.FieldNames.Length-1){
						MessageBox.Show(fieldName+" is not valid");
						return;
					}
				}
			}
			items[index]=new ClaimFormItem();
			items[index].FieldName=fieldName;
			items[index].FormatString=formatString;
		}

		private static void Fill(int index,string fieldName){
			Fill(index,fieldName,"");
		}

		private static void SaveFile(int batchNum){
			//this actually gets the current batch number since it was already incremented
			//int batchNum=PIn.PInt(((Pref)PrefB.HList["RenaissanceLastBatchNumber"]).ValueString);
			for(int i=0;i<DisplayStrings.GetLength(0);i++){//usually 1, but sometimes 2 or 3
				string uploadPath=@"C:\Program Files\Renaissance\dotr\upload\";
				if(!Directory.Exists(uploadPath)){
					MessageBox.Show("Error. Renaissance not installed.  "+uploadPath+" not valid");
					return;
				}
				int fileEnd=1;
				string fileName;
				do{//loop to find the next available filename
					fileName="C"+batchNum.ToString().PadLeft(3,'0')
						+"C"+fileEnd.ToString().PadLeft(3,'0')+".rss";
					fileEnd++;
				}
				while(File.Exists(uploadPath+fileName));//Since this is a windows program, no need to use ODFileUtils.CombinePaths()
				using (StreamWriter sw = new StreamWriter(uploadPath+fileName)){
					for(int ii=1;ii<DisplayStrings[i].Length;ii++){
						sw.WriteLine(ii.ToString().PadLeft(3,'0')+":"+DisplayStrings[i][ii]);
					}
				}
			}
		}

		///<summary>Returns a string describing all missing data on this claim.  Claim will not be allowed to be sent electronically unless this string comes back empty.</summary>
		public static string GetMissingData(ClaimSendQueueItem queueItem){
			return "";
		}




	}


}
