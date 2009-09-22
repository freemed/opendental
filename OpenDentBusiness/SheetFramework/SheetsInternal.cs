using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace OpenDentBusiness{
	public class SheetsInternal {

		public static SheetDef GetSheetDef(SheetInternalType internalType){
			switch(internalType){
				case SheetInternalType.LabelPatientMail:
					return LabelPatientMail();
				case SheetInternalType.LabelPatientLFAddress:
					return LabelPatientLFAddress();
				case SheetInternalType.LabelPatientLFChartNumber:
					return LabelPatientLFChartNumber();
				case SheetInternalType.LabelPatientLFPatNum:
					return LabelPatientLFPatNum();
				case SheetInternalType.LabelPatientRadiograph:
					return LabelPatientRadiograph();
				case SheetInternalType.LabelText:
					return LabelText();
				case SheetInternalType.LabelCarrier:
					return LabelCarrier();
				case SheetInternalType.LabelReferral:
					return LabelReferral();
				case SheetInternalType.ReferralSlip:
					return ReferralSlip();
				case SheetInternalType.LabelAppointment:
					return LabelAppointment();
				case SheetInternalType.Rx:
					return Rx();
				case SheetInternalType.Consent:
					return Consent();
				case SheetInternalType.PatientLetter:
					return PatientLetter();
				case SheetInternalType.ReferralLetter:
					return ReferralLetter();
				//case SheetInternalType.PatientRegistration:
				//	return PatientRegistration();
				default:
					throw new ApplicationException("Invalid SheetInternalType.");
			}
		}

		public static List<SheetDef> GetAllInternal(){
			List<SheetDef> list=new List<SheetDef>();
			for(int i=0;i<Enum.GetValues(typeof(SheetInternalType)).Length;i++){
				list.Add(GetSheetDef((SheetInternalType)i));
			}
			return list;
		}
		
		private static SheetDef LabelPatientMail(){
			SheetDef sheet=new SheetDef(SheetTypeEnum.LabelPatient);
			sheet.Description="LabelPatientMail";
			sheet.FontName="Microsoft Sans Serif";
			sheet.FontSize=12f;
			sheet.Width=108;
			sheet.Height=346;
			sheet.IsLandscape=true;
			int rowH=19;
			int yPos=10;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("nameFL",sheet.FontSize,sheet.FontName,false,25,yPos,300,rowH));
			yPos+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("address",sheet.FontSize,sheet.FontName,false,25,yPos,300,rowH,GrowthBehaviorEnum.DownLocal));
			yPos+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("cityStateZip",sheet.FontSize,sheet.FontName,false,25,yPos,300,rowH));
			return sheet;
		}

		private static SheetDef LabelPatientLFAddress() {
			SheetDef sheet=new SheetDef(SheetTypeEnum.LabelPatient);
			sheet.Description="LabelPatientLFAddress";
			sheet.FontName="Microsoft Sans Serif";
			sheet.FontSize=12f;
			sheet.Width=108;
			sheet.Height=346;
			sheet.IsLandscape=true;
			int rowH=19;
			int yPos=10;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("nameLF",sheet.FontSize,sheet.FontName,false,25,yPos,300,rowH));
			yPos+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("address",sheet.FontSize,sheet.FontName,false,25,yPos,300,rowH,GrowthBehaviorEnum.DownLocal));
			yPos+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("cityStateZip",sheet.FontSize,sheet.FontName,false,25,yPos,300,rowH));
			return sheet;
		}

		private static SheetDef LabelPatientLFChartNumber(){
			SheetDef sheet=new SheetDef(SheetTypeEnum.LabelPatient);
			sheet.Description="LabelPatientLFChartNumber";
			sheet.FontName="Microsoft Sans Serif";
			sheet.FontSize=12f;
			sheet.Width=108;
			sheet.Height=346;
			sheet.IsLandscape=true;
			int rowH=19;
			int yPos=30;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("nameLF",sheet.FontSize,sheet.FontName,false,25,yPos,300,rowH));
			yPos+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("ChartNumber",sheet.FontSize,sheet.FontName,false,25,yPos,300,rowH));
			return sheet;
		}

		private static SheetDef LabelPatientLFPatNum() {
			SheetDef sheet=new SheetDef(SheetTypeEnum.LabelPatient);
			sheet.Description="LabelPatientLFPatNum";
			sheet.FontName="Microsoft Sans Serif";
			sheet.FontSize=12f;
			sheet.Width=108;
			sheet.Height=346;
			sheet.IsLandscape=true;
			int rowH=19;
			int yPos=30;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("nameLF",sheet.FontSize,sheet.FontName,false,25,yPos,300,rowH));
			yPos+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("PatNum",sheet.FontSize,sheet.FontName,false,25,yPos,300,rowH));
			return sheet;
		}

		private static SheetDef LabelPatientRadiograph(){
			SheetDef sheet=new SheetDef(SheetTypeEnum.LabelPatient);
			sheet.Description="LabelPatientRadiograph";
			sheet.FontName="Microsoft Sans Serif";
			sheet.FontSize=12f;
			sheet.Width=108;
			sheet.Height=346;
			sheet.IsLandscape=true;
			int rowH=19;
			int yPos=30;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput( "nameLF",sheet.FontSize,sheet.FontName,false,25,yPos,150,rowH,GrowthBehaviorEnum.None));
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput( "dateTime.Today",sheet.FontSize,sheet.FontName,false,180,yPos,100,rowH,GrowthBehaviorEnum.None));
			yPos += rowH;
			//smallfont:
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput( "birthdate",9,sheet.FontName,false,25,yPos,105,rowH, GrowthBehaviorEnum.None));
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput( "priProvName",9,sheet.FontName,false,130,yPos,150,rowH, GrowthBehaviorEnum.None));
			return sheet;
		}

		private static SheetDef LabelText() {
			SheetDef sheet=new SheetDef(SheetTypeEnum.LabelPatient);
			sheet.Description="LabelText";
			sheet.FontName="Microsoft Sans Serif";
			sheet.FontSize=12f;
			sheet.Width=108;
			sheet.Height=346;
			sheet.IsLandscape=true;
			int rowH=19;
			int yPos=30;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("text",sheet.FontSize,sheet.FontName,false,25,yPos,300,315,GrowthBehaviorEnum.None));
			return sheet;
		}

		private static SheetDef LabelCarrier(){
			SheetDef sheet=new SheetDef(SheetTypeEnum.LabelCarrier);
			sheet.Description="LabelCarrier";
			sheet.FontName="Microsoft Sans Serif";
			sheet.FontSize=12f;
			sheet.Width=108;
			sheet.Height=346;
			sheet.IsLandscape=true;
			int rowH=19;
			int yPos=10;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("CarrierName",sheet.FontSize,sheet.FontName,false,25,yPos,300,rowH));
			yPos+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("address",sheet.FontSize,sheet.FontName,false,25,yPos,300,rowH,GrowthBehaviorEnum.DownLocal));
			yPos+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("cityStateZip",sheet.FontSize,sheet.FontName,false,25,yPos,300,rowH));
			return sheet;
		}

		private static SheetDef LabelReferral(){
			SheetDef sheet=new SheetDef(SheetTypeEnum.LabelReferral);
			sheet.Description="LabelReferral";
			sheet.FontName="Microsoft Sans Serif";
			sheet.FontSize=12f;
			sheet.Width=108;
			sheet.Height=346;
			sheet.IsLandscape=true;
			int rowH=19;
			int yPos=10;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("nameFL",sheet.FontSize,sheet.FontName,false,25,yPos,300,rowH));
			yPos+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("address",sheet.FontSize,sheet.FontName,false,25,yPos,300,rowH,GrowthBehaviorEnum.DownLocal));
			yPos+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("cityStateZip",sheet.FontSize,sheet.FontName,false,25,yPos,300,rowH));
			return sheet;
		}

		private static SheetDef ReferralSlip(){
			SheetDef sheet=new SheetDef(SheetTypeEnum.ReferralSlip);
			sheet.Description="ReferralSlip";
			sheet.FontName="Microsoft Sans Serif";
			sheet.FontSize=9f;
			sheet.Width=450;
			sheet.Height=650;
			int rowH=17;
			int yPos=50;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText("Referral to",10,sheet.FontName,true,170,yPos,200,19));
			yPos+=rowH+5;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("referral.nameFL",sheet.FontSize,sheet.FontName,false,150,yPos,200,rowH));
			yPos+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("referral.address",sheet.FontSize,sheet.FontName,false,150,yPos,200,rowH,GrowthBehaviorEnum.DownLocal));
			yPos+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("referral.cityStateZip",sheet.FontSize,sheet.FontName,false,150,yPos,200,rowH));
			yPos+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("referral.phone",sheet.FontSize,sheet.FontName,false,150,yPos,200,rowH));
			yPos+=rowH+30;
			//Patient--------------------------------------------------------------------------------------------
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText("Patient",9,sheet.FontName,true,25,yPos,100,rowH));
			yPos+=rowH+5;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("patient.nameFL",sheet.FontSize,sheet.FontName,false,25,yPos,200,rowH));
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("dateTime.Today",sheet.FontSize,sheet.FontName,false,300, yPos,100,rowH));
			yPos+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText("Work:",sheet.FontSize,sheet.FontName,false,25,yPos,38,rowH));
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("patient.WkPhone",sheet.FontSize,sheet.FontName,false,63,yPos,300,rowH));
			yPos+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText("Home:",sheet.FontSize,sheet.FontName,false,25,yPos,42,rowH));
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("patient.HmPhone",sheet.FontSize,sheet.FontName,false,67,yPos,300,rowH));
			yPos+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText("Wireless:",sheet.FontSize,sheet.FontName,false,25,yPos,58,rowH));
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("patient.WirelessPhone",sheet.FontSize,sheet.FontName,false,83,yPos,300,rowH));
			yPos+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("patient.address",sheet.FontSize,sheet.FontName,false,25,yPos,300,rowH,GrowthBehaviorEnum.DownLocal));
			yPos+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("patient.cityStateZip",sheet.FontSize,sheet.FontName,false,25,yPos,300,rowH));
			yPos+=rowH+30;
			//Provider--------------------------------------------------------------------------------------------
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText("Referred by",9,sheet.FontName,true,25,yPos,300,rowH));
			yPos+=rowH+5;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("patient.provider",sheet.FontSize,sheet.FontName,false,25,yPos,300,rowH));
			yPos+=rowH+20;
			//Notes--------------------------------------------------------------------------------------------
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText("Notes",9,sheet.FontName,true,25,yPos,300,rowH));
			yPos+=rowH+5;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewInput("notes",sheet.FontSize,sheet.FontName,false,25,yPos,400,240));
			return sheet;
		}

		private static SheetDef LabelAppointment() {
			SheetDef sheet=new SheetDef(SheetTypeEnum.LabelAppointment);
			sheet.Description="LabelAppointment";
			sheet.FontName="Microsoft Sans Serif";
			sheet.FontSize=10f;
			sheet.Width=108;
			sheet.Height=346;
			sheet.IsLandscape=true;
			int rowH=19;
			int yPos=15;
			//if(PrefC.GetBool("FuchsOptionsOn")) yPos = 50;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("nameFL",sheet.FontSize,sheet.FontName,false,25,yPos,300,rowH));
			yPos+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText("Your appointment is scheduled for:",sheet.FontSize,sheet.FontName,false,25,yPos,300,rowH));
			yPos+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("weekdayDateTime",sheet.FontSize,sheet.FontName,false,25,yPos,300,rowH));
			yPos+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText("Appointment length:",sheet.FontSize,sheet.FontName,false,25,yPos,300,rowH));
			yPos+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("length",sheet.FontSize,sheet.FontName,false,25,yPos,300,rowH));
			return sheet;
		}

		private static SheetDef Rx() {
			SheetDef sheet=new SheetDef(SheetTypeEnum.Rx);
			sheet.Description="Rx";
			sheet.FontName="Microsoft Sans Serif";
			sheet.FontSize=8f;
			sheet.Width=425;
			sheet.Height=550;
			sheet.IsLandscape=true;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewLine(0,0,550,0));//top
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewLine(0,0,0,425));//left
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewLine(549,0,0,425));//right
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewLine(0,424,550,0));//bottom
			int x;
			int y;
			int rowH=14;//for font of 8.
			//Dr--------------------------------------------------------------------------------------------------
			//Left Side
			x=50;
			y=37;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("prov.nameFL",sheet.FontSize,sheet.FontName,true,x,y,170,rowH));
			y+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("prov.address",sheet.FontSize,sheet.FontName,false,x,y,170,rowH,
				GrowthBehaviorEnum.DownLocal));
			y+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("prov.cityStateZip",sheet.FontSize,sheet.FontName,false,x,y,170,rowH));
			y=100;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewLine(25,y,500,0));
			//Right Side
			x=280;
			y=38;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("prov.phone",sheet.FontSize,sheet.FontName,false,x,y,170,rowH));
			y+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("RxDate",sheet.FontSize,sheet.FontName,false,x,y,170,rowH));
			y+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText("DEA#:",sheet.FontSize,sheet.FontName,true,x,y,40,rowH));
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("prov.dEANum",sheet.FontSize,sheet.FontName,false,x+40,y,130,rowH));
			//Patient---------------------------------------------------------------------------------------------------
			//Upper Left
			x=90;
			y=105;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("pat.nameFL",sheet.FontSize,sheet.FontName,true,x,y,150,rowH));
			y+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText("DOB:",sheet.FontSize,sheet.FontName,true,x,y,40,rowH));
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("pat.Birthdate",sheet.FontSize,sheet.FontName,true,x+40,y,110,rowH));
			y+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("pat.HmPhone",sheet.FontSize,sheet.FontName,false,x,y,150,rowH));
			y+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("pat.address",sheet.FontSize,sheet.FontName,false,x,y,170,rowH,
				GrowthBehaviorEnum.DownLocal));
			y+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("pat.cityStateZip",sheet.FontSize,sheet.FontName,false,x,y,170,rowH));
			//RX-----------------------------------------------------------------------------------------------------
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText("Rx",24,"Times New Roman",true,35,190,55,33));
			y=205;
			x=90;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("Drug",sheet.FontSize,sheet.FontName,true,x,y,300,rowH));
			y+=(int)(rowH*1.5);
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText("Disp:",sheet.FontSize,sheet.FontName,false,x,y,35,rowH));
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("Disp",sheet.FontSize,sheet.FontName,false,x+35,y,300,rowH));
			y+=(int)(rowH*1.5);
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText("Sig:",sheet.FontSize,sheet.FontName,false,x,y,30,rowH));
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("Sig",sheet.FontSize,sheet.FontName,false,x+30,y,325,rowH*2));
			y+=(int)(rowH*2.5);
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText("Refills:",sheet.FontSize,sheet.FontName,false,x,y,45,rowH));
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("Refills",sheet.FontSize,sheet.FontName,false,x+45,y,110,rowH));
			//Generic Subst----------------------------------------------------------------------------------------------
			x=50;
			y=343;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewRect(x,y,12,12));
			x+=17;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText("Dispense as Written",sheet.FontSize,sheet.FontName,false,x,y,200,rowH));
			x-=17;
			y+=25;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewRect(x,y,12,12));
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewLine(x,y,12,12));
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewLine(x+12,y,-12,12));
			x+=17;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText("Generic Substitution Permitted",sheet.FontSize,sheet.FontName,false,x,y,200,rowH));
			//Signature Line--------------------------------------------------------------------------------------------
			y=360;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewLine(295,y,230,0));
			y+=4;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText("Signature of Prescriber",sheet.FontSize,sheet.FontName,false,340,y,150,rowH));
			return sheet;
		}

		private static SheetDef Consent(){
			SheetDef sheet=new SheetDef(SheetTypeEnum.Consent);
			sheet.Description="ExtractionConsent";
			sheet.FontName="Microsoft Sans Serif";
			sheet.FontSize=11f;
			sheet.Width=850;
			sheet.Height=1100;
			int rowH=19;
			int x=200;
			int y=40;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText("Extraction Consent",12,sheet.FontName,true,x,y,170,20));
			y+=35;
			x=50;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("dateTime.Today",sheet.FontSize,sheet.FontName,false,x,y,120,rowH));
			y+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("patient.nameFL",sheet.FontSize,sheet.FontName,false,x,y,220,rowH));
			y+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText("Tooth number(s): ",sheet.FontSize,sheet.FontName,false,x,y,120,rowH));
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewInput("toothNum",sheet.FontSize,sheet.FontName,false,x+120,y,100,rowH));
			y+=rowH;
			y+=20;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText(@"Extraction(s) are to be peformed on the tooth/teeth listed above.  While we expect no complications, there are some risks involved with this procedure.  The more common complications are:

Pain, infection, swelling, bruising, and discoloration.  Adjacent teeth may be chipped or damaged during the extraction.

Nerves that run near the area of extraction may be bruised or damaged.  You may experience some temporary numbness and tingling of the lip and chin, or in rare cases, the tongue.  In some extremely rare instances, the lack of sensation could be permanent.

In the upper arch, sinus complications can occur because the roots of some upper teeth extend near or into the sinuses.  After extraction, a hole may be present between the sinus and the mouth.  If this happens, you will be informed and the area repaired.

By signing below you acknowledge that you understand the information presented, have had all your questions answered satisfactorily, and give consent to perform this procedure.",sheet.FontSize,sheet.FontName,false,x,y,500,360));
			y+=360;
			y+=20;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewSigBox(x,y,364,81));
			y+=82;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText("Signature",sheet.FontSize,sheet.FontName,false,x,y,90,rowH));
			return sheet;
		}

		private static SheetDef PatientLetter(){
			SheetDef sheet=new SheetDef(SheetTypeEnum.PatientLetter);
			sheet.Description="PatientLetter";
			sheet.FontName="Microsoft Sans Serif";
			sheet.FontSize=11f;
			sheet.Width=850;
			sheet.Height=1100;
			int rowH=19;
			int x=100;
			int y=100;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("PracticeTitle",sheet.FontSize,sheet.FontName,false,x,y,200,rowH));
			y+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("PracticeAddress",sheet.FontSize,sheet.FontName,false,x,y,200,rowH,
				GrowthBehaviorEnum.DownLocal));
			y+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("practiceCityStateZip",sheet.FontSize,sheet.FontName,false,x,y,200,rowH));
			y+=rowH;
			y+=rowH;
			y+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("patient.nameFL",sheet.FontSize,sheet.FontName,false,x,y,200,rowH));
			y+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("patient.address",sheet.FontSize,sheet.FontName,false,x,y,200,rowH,
				GrowthBehaviorEnum.DownLocal));
			y+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("patient.cityStateZip",sheet.FontSize,sheet.FontName,false,x,y,200,rowH));
			y+=rowH;
			y+=rowH;
			y+=rowH;
			y+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("today.DayDate",sheet.FontSize,sheet.FontName,false,x,y,200,rowH));
			y+=rowH;
			y+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("patient.salutation",sheet.FontSize,sheet.FontName,false,x,y,280,rowH));
			y+=rowH;
			y+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText("letter text",sheet.FontSize,sheet.FontName,false,x,y,650,rowH,
				GrowthBehaviorEnum.DownGlobal));
			y+=rowH;
			y+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText("Sincerely,",sheet.FontSize,sheet.FontName,false,x,y,100,rowH));
			y+=rowH;
			y+=rowH;
			y+=rowH;
			y+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("patient.priProvNameFL",sheet.FontSize,sheet.FontName,false,x,y,240,rowH));
			return sheet;
		}

		private static SheetDef ReferralLetter(){
			SheetDef sheet=new SheetDef(SheetTypeEnum.ReferralLetter);
			sheet.Description="ReferralLetter";
			sheet.FontName="Microsoft Sans Serif";
			sheet.FontSize=11f;
			sheet.Width=850;
			sheet.Height=1100;
			int rowH=19;
			int x=100;
			int y=100;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("PracticeTitle",sheet.FontSize,sheet.FontName,false,x,y,200,rowH));
			y+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("PracticeAddress",sheet.FontSize,sheet.FontName,false,x,y,200,rowH,
				GrowthBehaviorEnum.DownLocal));
			y+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("practiceCityStateZip",sheet.FontSize,sheet.FontName,false,x,y,200,rowH));
			y+=rowH;
			y+=rowH;
			y+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("referral.nameFL",sheet.FontSize,sheet.FontName,false,x,y,200,rowH));
			y+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("referral.address",sheet.FontSize,sheet.FontName,false,x,y,200,rowH,
				GrowthBehaviorEnum.DownLocal));
			y+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("referral.cityStateZip",sheet.FontSize,sheet.FontName,false,x,y,200,rowH));
			y+=rowH;
			y+=rowH;
			y+=rowH;
			y+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("today.DayDate",sheet.FontSize,sheet.FontName,false,x,y,200,rowH));
			y+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText("RE patient:",sheet.FontSize,sheet.FontName,false,x,y,90,rowH));
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("patient.nameFL",sheet.FontSize,sheet.FontName,false,x+90,y,200,rowH));
			y+=rowH;
			y+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("referral.salutation",sheet.FontSize,sheet.FontName,false,x,y,280,rowH));
			y+=rowH;
			y+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText("letter text",sheet.FontSize,sheet.FontName,false,x,y,650,rowH,
				GrowthBehaviorEnum.DownGlobal));
			y+=rowH;
			y+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText("Sincerely,",sheet.FontSize,sheet.FontName,false,x,y,100,rowH));
			y+=rowH;
			y+=rowH;
			y+=rowH;
			y+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("patient.priProvNameFL",sheet.FontSize,sheet.FontName,false,x,y,250,rowH));
			return sheet;
		}


		/*private static SheetDef PatientRegistration(){
			SheetDef sheet=new SheetDef(SheetTypeEnum.PatientRegistration);
			sheet.FontName="Microsoft Sans Serif";
			sheet.FontSize=9f;
			sheet.Width=850;
			sheet.Height=1100;
			int rowH=14;
			int y=50;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewImage("Patient Info.gif",0,0,770,999));
			return sheet;
		}*/

		

	}
}
