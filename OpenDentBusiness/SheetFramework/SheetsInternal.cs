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
				case SheetInternalType.PatientRegistration:
					return PatientRegistration();
				case SheetInternalType.RoutingSlip:
					return RoutingSlip();
				case SheetInternalType.FinancialAgreement:
					return FinancialAgreement();
				case SheetInternalType.HIPAA:
					return HIPAA();
				case SheetInternalType.MedicalHistory:
					return MedicalHistory();
				case SheetInternalType.LabSlip:
					return LabSlip();
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
			sheet.Description="Label Patient Mail";
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
			sheet.Description="Label PatientLFAddress";
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
			sheet.Description="Label PatientLFChartNumber";
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
			sheet.Description="Label PatientLFPatNum";
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
			sheet.Description="Label Patient Radiograph";
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
			sheet.Description="Label Text";
			sheet.FontName="Microsoft Sans Serif";
			sheet.FontSize=12f;
			sheet.Width=108;
			sheet.Height=346;
			sheet.IsLandscape=true;
			//int rowH=19;
			int yPos=30;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("text",sheet.FontSize,sheet.FontName,false,25,yPos,300,315,GrowthBehaviorEnum.None));
			return sheet;
		}

		private static SheetDef LabelCarrier(){
			SheetDef sheet=new SheetDef(SheetTypeEnum.LabelCarrier);
			sheet.Description="Label Carrier";
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
			sheet.Description="Label Referral";
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
			sheet.Description="Referral Slip";
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
			sheet.Description="Label Appointment";
			sheet.FontName="Microsoft Sans Serif";
			sheet.FontSize=10f;
			sheet.Width=108;
			sheet.Height=346;
			sheet.IsLandscape=true;
			int rowH=19;
			int yPos=15;
			//if(PrefC.GetBool(PrefName.FuchsOptionsOn")) yPos = 50;
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
			sheet.Description="Extraction Consent";
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
			sheet.Description="Patient Letter";
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
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText("letter text",sheet.FontSize,sheet.FontName,false,x,y,650,200,
				GrowthBehaviorEnum.DownGlobal));
			y+=200;
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
			sheet.Description="Referral Letter";
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

		private static SheetDef PatientRegistration() {
			SheetDef sheet=new SheetDef(SheetTypeEnum.PatientForm);
			sheet.Description="Registration Form";
			sheet.FontName="Microsoft Sans Serif";
			sheet.FontSize=10f;
			sheet.Width=850;
			sheet.Height=1100;
			int rowH=16;
			int y=31;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewImage("Patient Info.gif",39,y,761,988));
			y=204;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewInput("LName",sheet.FontSize,sheet.FontName,false,126,y,150,rowH));
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewInput("FName",sheet.FontSize,sheet.FontName,false,293,y,145,rowH));
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewInput("MiddleI",sheet.FontSize,sheet.FontName,false,447,y,50,rowH));
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewInput("Preferred",sheet.FontSize,sheet.FontName,false,507,y,150,rowH));
			y=236;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewInput("Birthdate",sheet.FontSize,sheet.FontName,false,133,y,105,rowH));
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewInput("SSN",sheet.FontSize,sheet.FontName,false,292,y,140,rowH));
			y=241;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewRadioButton("Gender","Male",499,y,10,10));
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewRadioButton("Gender","Female",536,y,10,10));
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewRadioButton("Position","Married",649,y,10,10));
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewRadioButton("Position","Single",683,y,10,10));
			y=255;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewInput("WkPhone",sheet.FontSize,sheet.FontName,false,152,y,120,rowH));
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewInput("WirelessPhone",sheet.FontSize,sheet.FontName,false,381,y,120,rowH));
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewInput("wirelessCarrier",sheet.FontSize,sheet.FontName,false,631,y,130,rowH));
			y=274;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewInput("Email",sheet.FontSize,sheet.FontName,false,114,y,575,rowH));
			y=299;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewRadioButton("PreferContactMethod","HmPhone",345,y,10,10));
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewRadioButton("PreferContactMethod","WkPhone",429,y,10,10));
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewRadioButton("PreferContactMethod","WirelessPh",513,y,10,10));
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewRadioButton("PreferContactMethod","Email",607,y,10,10));
			//sheet.SheetFieldDefs.Add(SheetFieldDef.NewCheckBox("PreferContactMethodIsTextMessage",666,y,10,10));
			y=318;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewRadioButton("PreferConfirmMethod","HmPhone",343,y,10,10));
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewRadioButton("PreferConfirmMethod","WkPhone",428,y,10,10));
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewRadioButton("PreferConfirmMethod","WirelessPh",511,y,10,10));
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewRadioButton("PreferConfirmMethod","Email",605,y,10,10));
			//sheet.SheetFieldDefs.Add(SheetFieldDef.NewCheckBox("PreferConfirmMethodIsTextMessage",664,y,10,10));
			y=337;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewRadioButton("PreferRecallMethod","HmPhone",343,y,10,10));
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewRadioButton("PreferRecallMethod","WkPhone",428,y,10,10));
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewRadioButton("PreferRecallMethod","WirelessPh",512,y,10,10));
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewRadioButton("PreferRecallMethod","Email",605,y,10,10));
			//sheet.SheetFieldDefs.Add(SheetFieldDef.NewCheckBox("PreferRecallMethodIsTextMessage",665,y,10,10));
			//cover up the options for text messages since we don't support that yet
			//sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText(".",sheet.FontSize,sheet.FontName,false,660,293,100,70));
			y=356;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewRadioButton("StudentStatus","Nonstudent",346,y,10,10));
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewRadioButton("StudentStatus","Fulltime",443,y,10,10));
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewRadioButton("StudentStatus","Parttime",520,y,10,10));
			y+=33;//375;
			//sheet.SheetFieldDefs.Add(SheetFieldDef.NewCheckBox("guarantorIsSelf",270,y,10,10));
			//sheet.SheetFieldDefs.Add(SheetFieldDef.NewCheckBox("guarantorIsOther",320,y,10,10));
			//sheet.SheetFieldDefs.Add(SheetFieldDef.NewInput("guarantorNameF",sheet.FontSize,sheet.FontName,false,378,370,150,rowH));
			//y=409;-34
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewInput("referredFrom",sheet.FontSize,sheet.FontName,false,76,y,600,rowH));
			y+=64;//439;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewCheckBox("addressAndHmPhoneIsSameEntireFamily",283,y,10,10));
			y+=15;//453;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewInput("Address",sheet.FontSize,sheet.FontName,false,128,y,425,rowH));
			y+=19;//472;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewInput("Address2",sheet.FontSize,sheet.FontName,false,141,y,425,rowH));
			y+=19;//491;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewInput("City",sheet.FontSize,sheet.FontName,false,103,y,200,rowH));
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewInput("State",sheet.FontSize,sheet.FontName,false,359,y,45,rowH));
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewInput("Zip",sheet.FontSize,sheet.FontName,false,439,y,100,rowH));
			y+=19;//510;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewInput("HmPhone",sheet.FontSize,sheet.FontName,false,156,y,120,rowH));
			//Ins 1--------------------------------------------------------------------------------------------------------------
			y+=58;//569;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewRadioButton("ins1Relat","Self",267,y,10,10));
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewRadioButton("ins1Relat","Spouse",320,y,10,10));
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewRadioButton("ins1Relat","Child",394,y,10,10));
			//sheet.SheetFieldDefs.Add(SheetFieldDef.NewRadioButton("ins1RelatIsNotSelfSpouseChild",457,y,10,10));
			//sheet.SheetFieldDefs.Add(SheetFieldDef.NewInput("ins1RelatDescript",sheet.FontSize,sheet.FontName,false,515,598,200,rowH));
			y+=16;//585;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewInput("ins1SubscriberNameF",sheet.FontSize,sheet.FontName,false,184,y,250,rowH));
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewInput("ins1SubscriberID",sheet.FontSize,sheet.FontName,false,565,y,140,rowH));
			y+=20;//604;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewInput("ins1CarrierName",sheet.FontSize,sheet.FontName,false,201,y,290,rowH));
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewInput("ins1CarrierPhone",sheet.FontSize,sheet.FontName,false,552,y,170,rowH));
			y+=19;//623;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewInput("ins1EmployerName",sheet.FontSize,sheet.FontName,false,136,y,190,rowH));
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewInput("ins1GroupName",sheet.FontSize,sheet.FontName,false,419,y,160,rowH));
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewInput("ins1GroupNum",sheet.FontSize,sheet.FontName,false,638,y,120,rowH));
			//Ins 2-------------------------------------------------------------------------------------------------------------
			y+=72;//695;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewRadioButton("ins2Relat","Self",267,y,10,10));
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewRadioButton("ins2Relat","Spouse",320,y,10,10));
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewRadioButton("ins2Relat","Child",394,y,10,10));
			//sheet.SheetFieldDefs.Add(SheetFieldDef.NewRadioButton("ins2RelatIsNotSelfSpouseChild",457,y,10,10));
			//sheet.SheetFieldDefs.Add(SheetFieldDef.NewInput("ins2RelatDescript",sheet.FontSize,sheet.FontName,false,515,598+126,200,rowH));
			y+=16;//585+126;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewInput("ins2SubscriberNameF",sheet.FontSize,sheet.FontName,false,184,y,250,rowH));
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewInput("ins2SubscriberID",sheet.FontSize,sheet.FontName,false,565,y,140,rowH));
			y+=19;//604+126;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewInput("ins2CarrierName",sheet.FontSize,sheet.FontName,false,201,y,290,rowH));
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewInput("ins2CarrierPhone",sheet.FontSize,sheet.FontName,false,552,y,170,rowH));
			y+=19;//623+126;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewInput("ins2EmployerName",sheet.FontSize,sheet.FontName,false,136,y,190,rowH));
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewInput("ins2GroupName",sheet.FontSize,sheet.FontName,false,419,y,160,rowH));
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewInput("ins2GroupNum",sheet.FontSize,sheet.FontName,false,638,y,120,rowH));
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewInput("misc",sheet.FontSize,sheet.FontName,false,136,821,600,200));
			return sheet;
		}

		private static SheetDef RoutingSlip() {
			SheetDef sheet=new SheetDef(SheetTypeEnum.RoutingSlip);
			sheet.Description="Routing Slip";
			sheet.FontName="Microsoft Sans Serif";
			sheet.FontSize=10f;
			sheet.Width=850;
			sheet.Height=1100;
			int rowH=18;
			int x=75;
			int y=50;
			//Title----------------------------------------------------------------------------------------------------------
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText("Routing Slip",12f,sheet.FontName,true,373,y,200,22));
			y+=35;
			//Today's appointment, including procedures-----------------------------------------------------------------------
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText("[nameFL]",sheet.FontSize,sheet.FontName,true,x,y,500,19));
			y+=19;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("appt.timeDate",sheet.FontSize,sheet.FontName,false,x,y,500,rowH));
			y+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("appt.length",sheet.FontSize,sheet.FontName,false,x,y,500,rowH));
			y+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("appt.providers",sheet.FontSize,sheet.FontName,false,x,y,500,rowH,GrowthBehaviorEnum.DownGlobal));
			y+=rowH-2;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText("Procedures:",sheet.FontSize,sheet.FontName,false,x,y,500,rowH));
			y+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("appt.procedures",sheet.FontSize,sheet.FontName,false,x+10,y,490,rowH,GrowthBehaviorEnum.DownGlobal));
			y+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText("Note:",sheet.FontSize,sheet.FontName,false,x,y,40,rowH));
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("appt.Note",sheet.FontSize,sheet.FontName,false,x+40,y,460,rowH,GrowthBehaviorEnum.DownGlobal));
			y+=rowH;
			y+=3;
			//Patient/Family Info---------------------------------------------------------------------------------------------
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewLine(x,y,725,0));
			y+=3;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText("Patient Info",sheet.FontSize,sheet.FontName,true,x,y,500,19));
			y+=19;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText("PatNum: [PatNum]",sheet.FontSize,sheet.FontName,false,x,y,500,rowH));
			y+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText("Age: [age]",sheet.FontSize,sheet.FontName,false,x,y,500,rowH));
			y+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText("Date of First Visit: [DateFirstVisit]",sheet.FontSize,sheet.FontName,false,x,y,500,rowH));
			y+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText("Billing Type: [BillingType]",sheet.FontSize,sheet.FontName,false,x,y,500,rowH));
			y+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText("Recall Due Date: [dateRecallDue]",sheet.FontSize,sheet.FontName,false,x,y,500,rowH));
			y+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText("Medical notes: [MedUrgNote]",sheet.FontSize,sheet.FontName,false,x,y,725,rowH,GrowthBehaviorEnum.DownGlobal));
			y+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText("Other Family Members",sheet.FontSize,sheet.FontName,true,x,y,500,19));
			y+=19;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("otherFamilyMembers",sheet.FontSize,sheet.FontName,false,x,y,500,rowH,GrowthBehaviorEnum.DownGlobal));
			y+=rowH;
			y+=3;
			//Insurance Info---------------------------------------------------------------------------------------------
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewLine(x,y,725,0));
			y+=3;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText("Primary Insurance",sheet.FontSize,sheet.FontName,true,x,y,360,19));
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText("Secondary Insurance",sheet.FontSize,sheet.FontName,true,x+365,y,360,19));
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewLine(x+362,y,0,124));
			y+=19;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText(
@"[carrierName]
Subscriber: [subscriberNameFL]
Annual Max: [insAnnualMax], Pending: [insPending], Used: [insUsed]
Deductible: [insDeductible], Ded Used: [insDeductibleUsed]
[insPercentages]"
				,sheet.FontSize,sheet.FontName,false,x,y,360,105,GrowthBehaviorEnum.DownGlobal));
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText(
@"[carrier2Name]
Subscriber: [subscriber2NameFL]
Annual Max: [ins2AnnualMax], Pending: [ins2Pending], Used: [ins2Used]
Deductible: [ins2Deductible], Ded Used: [ins2DeductibleUsed]
[ins2Percentages]"
				,sheet.FontSize,sheet.FontName,false,x+365,y,360,105,GrowthBehaviorEnum.DownGlobal));
			y+=105;
			//Account Info---------------------------------------------------------------------------------------------
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewLine(x,y,725,0));
			y+=3;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText("Account Info",sheet.FontSize,sheet.FontName,true,x,y,500,19));
			y+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText(
				@"Guarantor: [guarantorNameFL]
Balance: [balTotal]
-Ins Est: [balInsEst]
=Total: [balTotalMinusInsEst]
Aging: 0-30:[bal_0_30]  31-60:[bal_31_60]  61-90:[bal_61_90]  90+:[balOver90]
Fam Urgent Fin Note: [famFinUrgNote]"
				,sheet.FontSize,sheet.FontName,false,x,y,725,rowH,GrowthBehaviorEnum.DownGlobal));
			y+=rowH;
			y+=3;
			//Insurance Info---------------------------------------------------------------------------------------------
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewLine(x,y,725,0));
			y+=3;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText("Treatment Plan",sheet.FontSize,sheet.FontName,true,x,y,500,19,GrowthBehaviorEnum.DownGlobal));
			y+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText("[treatmentPlanProcs]",sheet.FontSize,sheet.FontName,false,x,y,500,19,GrowthBehaviorEnum.DownGlobal));
			y+=rowH;
			return sheet;
		}

		private static SheetDef FinancialAgreement(){
			SheetDef sheet=new SheetDef(SheetTypeEnum.PatientForm);
			sheet.Description="Financial Agreement";
			sheet.FontName="Microsoft Sans Serif";
			sheet.FontSize=10f;
			sheet.Width=850;
			sheet.Height=550;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText("Financial Agreement",12f,sheet.FontName,true,332,65,200,20));
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText("Date: [dateToday]",sheet.FontSize,sheet.FontName,false,92,135,120,18));
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText(@"* For my convenience, this office may release my information to my insurance company, and receive payment directly from them.
* I understand that if I begin major treatment that involves lab work, I will be responsible for the fee at that time.
* If sent to collections, I agree to pay all related fees and court costs.
* Every effort will be made to help me with my insurance, but if they do not pay as expected, I will still be responsible.
* I agree to pay finance charges of 1.5% per month (18% APR) on any balance 90 days past due.
* I will pay a fee for appointments broken without 24 hours notice. 
* Treatment plans may change, and I will be responsible for the work actually done.",sheet.FontSize,sheet.FontName,false,92,167,670,155));
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText("I agree to let this office run a credit report.  If no, then all fees are due at time of service.",sheet.FontSize,sheet.FontName,false,92,337,550,18));
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewRect(93,360,11,11));
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewRect(93,378,11,11));
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewCheckBox("misc",94,361,10,10));
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewCheckBox("misc",94,379,10,10));
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText("Yes",sheet.FontSize,sheet.FontName,false,108,358,40,18));
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText("No",sheet.FontSize,sheet.FontName,false,108,376,40,18));
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewSigBox(258,416,364,81));
			return sheet;
		}

		private static SheetDef HIPAA(){
			SheetDef sheet=new SheetDef(SheetTypeEnum.PatientForm);
			sheet.Description="HIPAA";
			sheet.FontName="Microsoft Sans Serif";
			sheet.FontSize=10f;
			sheet.Width=850;
			sheet.Height=550;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText("Notice of Privacy Policies",12f,sheet.FontName,true,332,65,220,20));
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText("Date: [dateToday]",sheet.FontSize,sheet.FontName,false,92,135,120,18));
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText("I have had full opportunity to read and consider the contents of  the Notice of Privacy Practices.  I understand that I am giving my permission to your use and disclosure of my protected health information in order to carry out treatment, payment activities, and healthcare operations.  I also understand that I have the right to revoke permission.",sheet.FontSize,sheet.FontName,false,92,167,670,80));
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewSigBox(261,295,364,81));
			return sheet;
		}

		private static SheetDef MedicalHistory() {
			SheetDef sheet=new SheetDef(SheetTypeEnum.MedicalHistory);
			sheet.Description="Medical History";
			sheet.FontName="Microsoft Sans Serif";
			sheet.FontSize=10f;
			sheet.Width=850;
			sheet.Height=1100;
			int rowH=18;
			int y=60;
			int x=75;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText("Medical History",12f,sheet.FontName,true,345,y,180,20));
			y=105;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText("Name of Medical Doctor:",sheet.FontSize,sheet.FontName,false,x,y,155,rowH));
			x=230;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewLine(x,y+rowH,265,0));
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewInput("misc",sheet.FontSize,sheet.FontName,false,x,y,265,rowH));
			x=500;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText("City/State:",sheet.FontSize,sheet.FontName,false,x,y,67,rowH));
			x=567;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewLine(x,y+rowH,190,0));
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewInput("misc",sheet.FontSize,sheet.FontName,false,x,y,190,rowH));
			x=75;
			y+=rowH+2;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText("Emergency Contact",sheet.FontSize,sheet.FontName,false,x,y,124,rowH));
			x=199;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewLine(x,y+rowH,138,0));
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewInput("misc",sheet.FontSize,sheet.FontName,false,x,y,138,rowH));
			x=342;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText("Phone",sheet.FontSize,sheet.FontName,false,x,y,44,rowH));
			x=386;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewLine(x,y+rowH,99,0));
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewInput("misc",sheet.FontSize,sheet.FontName,false,x,y,99,rowH));
			x=490;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText("Relationship",sheet.FontSize,sheet.FontName,false,x,y,80,rowH));
			x=570;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewLine(x,y+rowH,187,0));
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewInput("misc",sheet.FontSize,sheet.FontName,false,x,y,187,rowH));
			x=75;
			y+=rowH+2;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText("List all medications or drugs you are now taking:",sheet.FontSize,sheet.FontName,false,x,y,292,rowH));
			y+=rowH+1;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewRect(x,y+2,11,11));
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewCheckBox("misc",x+1,y+3,10,10));
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText("None",sheet.FontSize,sheet.FontName,false,x+13,y,37,rowH));
			y+=rowH+2;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewInput("misc",sheet.FontSize,sheet.FontName,false,x,y,682,140));
			y+=142;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText("List all medications or drugs you are allergic to:",sheet.FontSize,sheet.FontName,false,x,y,286,rowH));
			y+=rowH+1;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewRect(x,y+2,11,11));
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewCheckBox("misc",x+1,y+3,10,10));
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText("None",sheet.FontSize,sheet.FontName,false,x+13,y,37,rowH));
			y+=rowH+2;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewInput("misc",sheet.FontSize,sheet.FontName,false,x,y,682,140));
			y+=142;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText("List any medical conditions you may have including: asthma, bleeding problems, cancer, diabetes, heart murmur, heart trouble, high blood pressure, joint replacement, kidney disease, liver disease, pregnancy, psychiatric treatment, sinus trouble, stroke, ulcers, or history of  rheumatic fever or of taking fen-phen:",sheet.FontSize,sheet.FontName,false,x,y,682,55));
			y+=56;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewRect(x,y+2,11,11));
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewCheckBox("misc",x+1,y+3,10,10));
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText("None",sheet.FontSize,sheet.FontName,false,x+13,y,37,rowH));
			y+=rowH+2;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewInput("misc",sheet.FontSize,sheet.FontName,false,x,y,682,140));
			y+=142;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText("Tobacco use?  If so, what kind and how much?",sheet.FontSize,sheet.FontName,false,x,y,289,rowH));
			x=364;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewLine(x,y+rowH,393,0));
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewInput("misc",sheet.FontSize,sheet.FontName,false,x,y,393,rowH));
			y+=rowH+2;
			x=75;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText("Unusual reaction to dental injections?",sheet.FontSize,sheet.FontName,false,x,y,232,rowH));
			x=307;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewLine(x,y+rowH,450,0));
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewInput("misc",sheet.FontSize,sheet.FontName,false,x,y,450,rowH));
			y+=rowH+2;
			x=75;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText("Reason for today's visit",sheet.FontSize,sheet.FontName,false,x,y,145,rowH));
			x=220;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewLine(x,y+rowH,275,0));
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewInput("misc",sheet.FontSize,sheet.FontName,false,x,y,275,rowH));
			x=500;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText("Are you in pain?",sheet.FontSize,sheet.FontName,false,x,y,103,rowH));
			x=603;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewLine(x,y+rowH,154,0));
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewInput("misc",sheet.FontSize,sheet.FontName,false,x,y,154,rowH));
			y+=rowH+2;
			x=75;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText("New patients:",sheet.FontSize,sheet.FontName,false,x,y,87,rowH));
			y+=rowH+2;
			x=95;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText("Do you have a Panoramic x-ray or Full Mouth x-rays that are less than 5 years old?",sheet.FontSize,sheet.FontName,false,x,y,507,rowH));
			x=602;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewLine(x,y+rowH,155,0));
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewInput("misc",sheet.FontSize,sheet.FontName,false,x,y,155,rowH));
			y+=rowH+2;
			x=95;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText("Do you have BiteWing x-rays that are less than 1 year old?",sheet.FontSize,sheet.FontName,false,x,y,360,rowH));
			x=455;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewLine(x,y+rowH,302,0));
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewInput("misc",sheet.FontSize,sheet.FontName,false,x,y,302,rowH));
			y+=rowH+2;
			x=95;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText("Name of former dentist",sheet.FontSize,sheet.FontName,false,x,y,143,rowH));
			x=238;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewLine(x,y+rowH,275,0));
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewInput("misc",sheet.FontSize,sheet.FontName,false,x,y,275,rowH));
			x=518;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText("City/State",sheet.FontSize,sheet.FontName,false,x,y,64,rowH));
			x=582;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewLine(x,y+rowH,175,0));
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewInput("misc",sheet.FontSize,sheet.FontName,false,x,y,175,rowH));
			y+=rowH+2;
			x=95;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText("Date of last cleaning and exam",sheet.FontSize,sheet.FontName,false,x,y,192,rowH));
			x=287;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewLine(x,y+rowH,470,0));
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewInput("misc",sheet.FontSize,sheet.FontName,false,x,y,470,rowH));
			y+=40;
			x=75;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText("Date: [dateToday]",sheet.FontSize,sheet.FontName,false,x,y,120,rowH));
			y+=40;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewSigBox(261,y,364,81));
			return sheet;
		}

		public static SheetDef LabSlip() {
			SheetDef sheet=new SheetDef(SheetTypeEnum.LabSlip);
			sheet.Description="Lab Slip";
			sheet.FontName="Microsoft Sans Serif";
			sheet.FontSize=10f;
			sheet.Width=850;
			sheet.Height=1100;
			int rowH=18;
			int x=75;
			int y=50;
			//Title----------------------------------------------------------------------------------------------------------
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText("Lab Slip",12f,sheet.FontName,true,270,y,200,22));
			y+=35;
			//Lab-----------------------------------------------------------------------
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("lab.Description",sheet.FontSize,sheet.FontName,true,x,y,300,rowH));
			y+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("lab.Address",sheet.FontSize,sheet.FontName,false,x,y,300,rowH));
			y+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("lab.CityStZip",sheet.FontSize,sheet.FontName,false,x,y,300,rowH));
			y+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("lab.Phone",sheet.FontSize,sheet.FontName,false,x,y,300,rowH));
			y+=rowH;
			y+=15;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText("Date:  [dateToday]",sheet.FontSize,sheet.FontName,false,x,y,140,rowH));
			y+=rowH;
			//Prov-----------------------------------------------------------------------
			y+=15;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText("Doctor:",sheet.FontSize,sheet.FontName,false,x,y,50,rowH));
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("prov.nameFormal",sheet.FontSize,sheet.FontName,false,x+50,y,300,rowH));
			y+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText("License No:",sheet.FontSize,sheet.FontName,false,x,y,78,rowH));
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("prov.stateLicence",sheet.FontSize,sheet.FontName,false,x+78,y,200,rowH));
			y+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText("Address:  [clinicAddress],  [clinicCityStZip]",sheet.FontSize,sheet.FontName,false,x,y,600,rowH));
			y+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText("Phone:  [clinicPhone]",sheet.FontSize,sheet.FontName,false,x,y,300,rowH));
			y+=rowH;
			//Patient-----------------------------------------------------------------------
			y+=15;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText("Patient:  [nameFL]",sheet.FontSize,sheet.FontName,false,x,y,300,rowH));
			y+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText("Age: [age]      Gender: [gender]",sheet.FontSize,sheet.FontName,false,x,y,200,rowH));
			y+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText("Due Date/Time:",sheet.FontSize,sheet.FontName,false,x,y,100,rowH));
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewOutput("labcase.DateTimeDue",sheet.FontSize,sheet.FontName,false,x+100,y,200,rowH));
			y+=rowH;
			//Instructions-----------------------------------------------------------------------
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText("Instructions:",sheet.FontSize,sheet.FontName,false,x,y,200,rowH));
			y+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewInput("labcase.Instructions",sheet.FontSize,sheet.FontName,false,x,y,600,200));
			y+=220;
			//sig-------------------------------------------------------------------------------
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewStaticText("Dr. Signature:",sheet.FontSize,sheet.FontName,false,x,y,200,rowH));
			y+=rowH;
			sheet.SheetFieldDefs.Add(SheetFieldDef.NewSigBox(x,y,364,81));
			return sheet;
		}

	}
}
