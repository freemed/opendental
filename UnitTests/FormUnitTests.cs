using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental;
using OpenDental.UI;

namespace UnitTests {
	public partial class FormUnitTests:Form {
		public FormUnitTests() {
			InitializeComponent();
		}

		private void FormUnitTests_Load(object sender,EventArgs e) {
			BenefitComputeRenewDate();
			//ToothFormatRanges();
			//SerializeDeserialize();
			textResults.Text+="Done.";
			textResults.SelectionStart=textResults.Text.Length;
		}

		private void BenefitComputeRenewDate(){
			DateTime asofDate=new DateTime(2006,3,19);
			//bool isCalendarYear=true;
			//DateTime insStartDate=new DateTime(2003,3,1);
			DateTime result=BenefitLogic.ComputeRenewDate(asofDate,0);
			if(result!=new DateTime(2006,1,1)){
				textResults.Text+="BenefitComputeRenewDate 1 failed.\r\n";
			}
			//isCalendarYear=false;//for the remaining tests
			//earlier in same month
			result=BenefitLogic.ComputeRenewDate(asofDate,3);
			if(result!=new DateTime(2006,3,1)) {
				textResults.Text+="BenefitComputeRenewDate 2 failed.\r\n";
			}
			//earlier month in year
			asofDate=new DateTime(2006,5,1);
			result=BenefitLogic.ComputeRenewDate(asofDate,3);
			if(result!=new DateTime(2006,3,1)) {
				textResults.Text+="BenefitComputeRenewDate 3 failed.\r\n";
			}
			asofDate=new DateTime(2006,12,1);
			result=BenefitLogic.ComputeRenewDate(asofDate,3);
			if(result!=new DateTime(2006,3,1)) {
				textResults.Text+="BenefitComputeRenewDate 4 failed.\r\n";
			}
			//later month in year
			asofDate=new DateTime(2006,2,1);
			result=BenefitLogic.ComputeRenewDate(asofDate,3);
			if(result!=new DateTime(2005,3,1)) {
				textResults.Text+="BenefitComputeRenewDate 5 failed.\r\n";
			}
			asofDate=new DateTime(2006,2,12);
			result=BenefitLogic.ComputeRenewDate(asofDate,3);
			if(result!=new DateTime(2005,3,1)) {
				textResults.Text+="BenefitComputeRenewDate 6 failed.\r\n";
			}
			/*
			//Insurance start date not on the 1st.
			asofDate=new DateTime(2008,5,10);
			insStartDate=new DateTime(2007,1,12);
			result=BenefitLogic.ComputeRenewDate(asofDate,isCalendarYear,insStartDate);
			if(result!=new DateTime(2008,1,1)) {
				textResults.Text+="BenefitComputeRenewDate 7 failed.\r\n";
			}*/
			textResults.Text+="BenefitRenewDates complete.\r\n";
		}

		private void ToothFormatRanges(){
			PrefC.DictRef=new Dictionary<string,Pref>();
			Pref pref=new Pref();
			pref.PrefName="UseInternationalToothNumbers";
			pref.ValueString="0";
			PrefC.DictRef.Add(pref.PrefName,pref);
			//for display----------------------------------------------------------------
			string inputrange="1,2,3,4,5,7,8,9,11,12,15,16,17,18,21,22,23";
			string result=Tooth.FormatRangeForDisplay(inputrange);
			string desired="1-5,7-9,11,12,15,16,17,18,21-23";
			if(result!=desired){
				textResults.Text+="ToothFormatRangeForDisplay failed.  Desired: "+desired+" Result: "+result+"\r\n";
			}
			inputrange="2,4,5,7,8,9,11,12,13,14,15,16,17,18,19,20,21,22,23,25";
			result=Tooth.FormatRangeForDisplay(inputrange);
			desired="2,4,5,7-9,11-16,17-23,25";
			if(result!=desired) {
				textResults.Text+="ToothFormatRangeForDisplay failed.  Desired: "+desired+" Result: "+result+"\r\n";
			}
			inputrange="4,5,2, 7,8,9,11 ,13,12,14,15,16,17,18 ,20, 21,22,23,19,25";
			result=Tooth.FormatRangeForDisplay(inputrange);
			desired="2,4,5,7-9,11-16,17-23,25";
			if(result!=desired) {
				textResults.Text+="ToothFormatRangeForDisplay failed.  Desired: "+desired+" Result: "+result+"\r\n";
			}
			//for database------------------------------------------------------------------
			inputrange="1-5,7-9,11,12,15,16,17,18,21-23";
			result=Tooth.FormatRangeForDb(inputrange);
			desired="1,2,3,4,5,7,8,9,11,12,15,16,17,18,21,22,23";
			if(result!=desired) {
				textResults.Text+="ToothFormatRangeForDb failed.  Desired: "+desired+" Result: "+result+"\r\n";
			}
			inputrange="2,4,5,7-9,11-16,17-23,25";
			result=Tooth.FormatRangeForDb(inputrange);
			desired="2,4,5,7,8,9,11,12,13,14,15,16,17,18,19,20,21,22,23,25";
			if(result!=desired) {
				textResults.Text+="ToothFormatRangeForDb failed.  Desired: "+desired+" Result: "+result+"\r\n";
			}
			inputrange="4,2,5,7-9 ,11-16,25 ,  17-23";
			result=Tooth.FormatRangeForDb(inputrange);
			desired="2,4,5,7,8,9,11,12,13,14,15,16,17,18,19,20,21,22,23,25";
			if(result!=desired) {
				textResults.Text+="ToothFormatRangeForDb failed.  Desired: "+desired+" Result: "+result+"\r\n";
			}
			textResults.Text+="ToothFormatRanges complete.\r\n";
			//we still haven't tested really bad input.
		}


		/*
		private void LabDueDate(){
			DateTime startdate=new DateTime(2007,5,3);//this is a Thursday
			DateTime result=LabTurnarounds.ComputeDueDate(startdate,1);
			DateTime desired=new DateTime(2007,5,4,17,0,0);//Friday, 5pm
			if(result!=desired) {
				textResults.Text+="LabDueDate failed.  Desired: "+desired.ToString()+" Result: "+result.ToString()+"\r\n";
			}
			result=LabTurnarounds.ComputeDueDate(startdate,2);
			desired=new DateTime(2007,5,7,17,0,0);//Monday, 5pm
			if(result!=desired) {
				textResults.Text+="LabDueDate failed.  Desired: "+desired.ToString()+" Result: "+result.ToString()+"\r\n";
			}
			result=LabTurnarounds.ComputeDueDate(startdate,5);
			desired=new DateTime(2007,5,10,17,0,0);//Thurs, 5pm
			if(result!=desired) {
				textResults.Text+="LabDueDate failed.  Desired: "+desired.ToString()+" Result: "+result.ToString()+"\r\n";
			}
			result=LabTurnarounds.ComputeDueDate(startdate,10);
			desired=new DateTime(2007,5,17,17,0,0);//Thurs, 5pm
			if(result!=desired) {
				textResults.Text+="LabDueDate failed.  Desired: "+desired.ToString()+" Result: "+result.ToString()+"\r\n";
			}
		}*/

		private void SerializeDeserialize() {
			//int
			int myInt=4;
			string xmlData=XmlConverter.Serialize<int>(myInt);
			int myInt2=XmlConverter.Deserialize<int>(xmlData);
			if(myInt2!=4) {
				textResults.Text+="SerializeDeserialize int failed.  XmlData:\r\n\r\n"+xmlData+"\r\n\r\n";
			}
			//Patient (DataObjectBase)
			Patient pat=new Patient();
			pat.LName="Sparks";
			pat.PatNum=22;
			pat.PatStatus=PatientStatus.Deceased;
			xmlData=XmlConverter.Serialize<Patient>(pat);
			Patient pat2=XmlConverter.Deserialize<Patient>(xmlData);
			if(pat.LName!=pat2.LName || pat.PatNum!=pat2.PatNum || pat.PatStatus!=pat2.PatStatus) {
				textResults.Text+="SerializeDeserialize Patient failed.  XmlData:\r\n\r\n"+xmlData+"\r\n\r\n";
			}
			//ArrayOfPatient
			Patient[] pats=new Patient[2];
			pats[0]=new Patient();
			pats[0].LName="Sparks";
			pats[0].PatStatus=PatientStatus.Deceased;
			pats[0].PatNum=22;
			pats[1]=new Patient();
			pats[1].LName="Spander";
			pats[1].PatStatus=PatientStatus.Inactive;
			pats[1].PatNum=23;
			xmlData=XmlConverter.Serialize<Patient[]>(pats);
			Patient[] pats2=XmlConverter.Deserialize<Patient[]>(xmlData);
			if(pats.Length!=pats2.Length) {
				textResults.Text+="SerializeDeserialize Patient failed.  XmlData:\r\n\r\n"+xmlData+"\r\n\r\n";
			}
			//ArrayOfDocument
			xmlData="<?xml version=\"1.0\" encoding=\"utf-16\"?><ArrayOfDocument xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"><Document><DocNum>11449</DocNum><Description></Description><DateCreated>2004-09-08T00:00:00</DateCreated><DocCategory>130</DocCategory><PatNum>4390</PatNum><FileName>LuskDouglas11449.jpg</FileName><ImgType>Document</ImgType><IsFlipped>False</IsFlipped><DegreesRotated>0</DegreesRotated><ToothNumbers></ToothNumbers><Note></Note><SigIsTopaz>False</SigIsTopaz><Signature></Signature><CropX>0</CropX><CropY>0</CropY><CropW>0</CropW><CropH>0</CropH><WindowingMin>0</WindowingMin><WindowingMax>0</WindowingMax><MountItemNum>0</MountItemNum><IsDirty>True</IsDirty><IsDeleted>False</IsDeleted><IsNew>True</IsNew></Document><Document><DocNum>11553</DocNum><Description>Lab slip</Description><DateCreated>2004-09-20T00:00:00</DateCreated><DocCategory>137</DocCategory><PatNum>4390</PatNum><FileName>LuskDouglas11553.jpg</FileName><ImgType>Document</ImgType><IsFlipped>False</IsFlipped><DegreesRotated>0</DegreesRotated><ToothNumbers></ToothNumbers><Note></Note><SigIsTopaz>False</SigIsTopaz><Signature></Signature><CropX>0</CropX><CropY>0</CropY><CropW>0</CropW><CropH>0</CropH><WindowingMin>0</WindowingMin><WindowingMax>0</WindowingMax><MountItemNum>0</MountItemNum><IsDirty>True</IsDirty><IsDeleted>False</IsDeleted><IsNew>True</IsNew></Document><Document><DocNum>20898</DocNum><Description></Description><DateCreated>2007-07-10T00:00:00</DateCreated><DocCategory>226</DocCategory><PatNum>4390</PatNum><FileName>LuskDouglas20898.jpg</FileName><ImgType>Document</ImgType><IsFlipped>False</IsFlipped><DegreesRotated>0</DegreesRotated><ToothNumbers></ToothNumbers><Note></Note><SigIsTopaz>False</SigIsTopaz><Signature></Signature><CropX>0</CropX><CropY>0</CropY><CropW>0</CropW><CropH>0</CropH><WindowingMin>0</WindowingMin><WindowingMax>0</WindowingMax><MountItemNum>0</MountItemNum><IsDirty>True</IsDirty><IsDeleted>False</IsDeleted><IsNew>True</IsNew></Document></ArrayOfDocument>";
			Document[] docs=XmlConverter.Deserialize<Document[]>(xmlData);
			if(docs.Length!=3) {
				textResults.Text+="SerializeDeserialize Document[] failed.\r\n\r\n";
			}





			textResults.Text+="SerializeDeserialize complete.\r\n";
		}



	}
}