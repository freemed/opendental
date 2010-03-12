using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests {
	///<summary>You can put unit test here that seem to be broken and which we don't immediately care about.  Maybe it was a unit test that was never quite finished and you will come back to it some day.</summary>
	public class RecyclingBin {
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

		/*
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
		}*/
	}
}
