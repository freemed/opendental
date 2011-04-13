using System;
using System.Collections.Generic;
using System.Text;
using OpenDentBusiness;

namespace OpenDentBusiness.HL7 {
	///<summary>A DFT message is a Charge Specification.  There are different kinds.  The kind we have implemented passes information about completed procedures and their charges to external programs for billing purposes.</summary>
	public class DFT {
		private MessageHL7 msg;
		private SegmentHL7 seg;

		///<summary></summary>
		public DFT() {
			
		}

		///<summary>Creates the Message object and fills it with data.</summary>
		public void InitializeEcw(Appointment apt,Patient pat,string pdfDataAsBase64,string pdfDescription,bool justPDF){
			msg=new MessageHL7(MessageType.DFT);
			MSH();
			EVN();
			PID(pat);
			PV1(apt);
			FT1(apt,justPDF);
			DG1();
			ZX1(pdfDataAsBase64,pdfDescription);
		}

		///<summary>Message Header Segment</summary>
		private void MSH(){
			seg=new SegmentHL7(@"MSH|^~\&|OD||ECW||"+DateTime.Now.ToString("yyyyMMddHHmmss")+"||DFT^P03||P|2.3");
			msg.Segments.Add(seg);
		}

		///<summary>Event type segment.</summary>
		private void EVN(){
			seg=new SegmentHL7("EVN|P03|"+DateTime.Now.ToString("yyyyMMddHHmmss")+"|");
			msg.Segments.Add(seg);
		}

		///<summary>Patient identification.</summary>
		private void PID(Patient pat){
			seg=new SegmentHL7(SegmentName.PID);
			seg.SetField(0,"PID");
			seg.SetField(1,"1");
			seg.SetField(2,pat.ChartNumber);//Account number.  eCW requires this to be the same # as came in on PID.4.
			seg.SetField(3,pat.PatNum.ToString());//??what is this MRN?
			seg.SetField(5,pat.LName,pat.FName,pat.MiddleI);
			//we assume that dob is always valid because eCW should always pass us a dob.
			seg.SetField(7,pat.Birthdate.ToString("yyyyMMdd"));
			seg.SetField(8,ConvertGender(pat.Gender));
			seg.SetField(10,ConvertRace(pat.Race));
			seg.SetField(11,pat.Address,pat.Address2,pat.City,pat.State,pat.Zip);
			seg.SetField(13,ConvertPhone(pat.HmPhone));
			seg.SetField(14,ConvertPhone(pat.WkPhone));
			seg.SetField(16,ConvertMaritalStatus(pat.Position));
			seg.SetField(19,pat.SSN);
			msg.Segments.Add(seg);
		}

		///<summary>Patient visit.</summary>
		private void PV1(Appointment apt){
			seg=new SegmentHL7(SegmentName.PV1);
			seg.SetField(0,"PV1");
			Provider prov=Providers.GetProv(apt.ProvNum);
			seg.SetField(7,prov.Abbr,prov.LName,prov.FName,prov.MI);
			seg.SetField(19,apt.AptNum.ToString());
			msg.Segments.Add(seg);
		}

		///<summary>Financial transaction segment.</summary>
		private void FT1(Appointment apt,bool justPDF){
			if(justPDF){
				seg=new SegmentHL7(SegmentName.FT1);
				seg.SetField(0,"FT1");
				seg.SetField(1,"0");
				seg.SetField(4,DateTime.Now.ToString("yyyyMMddHHmmss"));
				seg.SetField(5,DateTime.Now.ToString("yyyyMMddHHmmss"));
				seg.SetField(6,"CG");
				seg.SetField(10,"1.0");
				seg.SetField(16,"");//location code and description???
				Provider prov=Providers.GetProv(apt.ProvNum);
				seg.SetField(20,prov.Abbr,prov.LName,prov.FName,prov.MI);//performed by provider.
				seg.SetField(21,prov.Abbr,prov.LName,prov.FName,prov.MI);//ordering provider.
				seg.SetField(22,"0.00");//fee
				seg.SetField(25,"D0999");
				seg.SetField(26,"","");//no tooth ranges
				msg.Segments.Add(seg);
				return;
			}
			List<Procedure> procs=Procedures.GetProcsForSingle(apt.AptNum,false);
			ProcedureCode procCode;
			for(int i=0;i<procs.Count;i++) {
				seg=new SegmentHL7(SegmentName.FT1);
				seg.SetField(0,"FT1");
				seg.SetField(1,(i+1).ToString());
				seg.SetField(4,procs[i].ProcDate.ToString("yyyyMMddHHmmss"));
				seg.SetField(5,procs[i].ProcDate.ToString("yyyyMMddHHmmss"));
				seg.SetField(6,"CG");
				seg.SetField(10,"1.0");
				seg.SetField(16,"");//location code and description???
				seg.SetField(19,procs[i].DiagnosticCode);
				Provider prov=Providers.GetProv(procs[i].ProvNum);
				seg.SetField(20,prov.Abbr,prov.LName,prov.FName,prov.MI);//performed by provider.
				seg.SetField(21,prov.Abbr,prov.LName,prov.FName,prov.MI);//ordering provider.
				seg.SetField(22,procs[i].ProcFee.ToString("F2"));
				procCode=ProcedureCodes.GetProcCode(procs[i].CodeNum);
				seg.SetField(25,procCode.ProcCode);
				if(procCode.TreatArea==TreatmentArea.ToothRange){
					seg.SetField(26,procs[i].ToothRange,"");
				}
				else if(procCode.TreatArea==TreatmentArea.Surf){//probably not necessary
					seg.SetField(26,Tooth.ToInternat(procs[i].ToothNum),Tooth.SurfTidyForClaims(procs[i].Surf,procs[i].ToothNum));
				}
				else{
					seg.SetField(26,Tooth.ToInternat(procs[i].ToothNum),procs[i].Surf);
				}
				msg.Segments.Add(seg);
			}
		}

		///<summary>Diagnosis segment. Optional.</summary>
		private void DG1(){
			//DG1 optional, so we'll skip for now---------------------------------
			//seg=new SegmentHL7(SegmentName.DG1);
			//msg.Segments.Add(seg);
		}

		///<summary>PDF data segment.</summary>
		private void ZX1(string pdfDataAsBase64,string pdfDescription){
			seg=new SegmentHL7(SegmentName.ZX1);
			seg.SetField(0,"ZX1");
			seg.SetField(1,"6");
			seg.SetField(2,"PDF");
			seg.SetField(3,"PATHOLOGY^Pathology Report^L");
			seg.SetField(4,pdfDescription);
			seg.SetField(5,pdfDataAsBase64);
			msg.Segments.Add(seg);
		}

		public string GenerateMessage() {
			return msg.ToString();
		}

		private string ConvertGender(PatientGender gender){
			if(gender==PatientGender.Female) {
				return "F";
			}
			if(gender==PatientGender.Male) {
				return "M";
			}
			return "U";
		}

		//=======================================================================================================================================
		//DO NOT ALTER any of these Convert... methods for use with any other HL7 bridge.  
		//Each bridge tends to have slightly different implementation.  
		//No bridge can share any of these.
		//Instead, copy them into other classes.
		//This set of methods is ONLY for ECW, and will have to be renamed and grouped if any other DFT bridge is built.
		//=======================================================================================================================================

		private string ConvertRace(PatientRace race) {
			switch(race) {
				case PatientRace.AmericanIndian:
					return "American Indian Or Alaska Native";
				case PatientRace.Asian:
					return "Asian";
				case PatientRace.HawaiiOrPacIsland:
					return "Native Hawaiian or Other Pacific";
				case PatientRace.AfricanAmerican:
					return "Black or African American";
				case PatientRace.White:
					return "White";
				case PatientRace.HispanicLatino:
					return "Hispanic";
				case PatientRace.Other:
					return "Other Race";
				default:
					return "Other Race";
			}
		}

		private string ConvertPhone(string phone) {
			string retVal="";
			for(int i=0;i<phone.Length;i++){
				if(Char.IsNumber(phone,i)){
					if(retVal=="" && phone.Substring(i,1)=="1"){
						continue;//skip leading 1.
					}
					retVal+=phone.Substring(i,1);
				}
				if(retVal.Length==10){
					return retVal;
				}
			}
			//never made it to 10
			return "";
		}

		private string ConvertMaritalStatus(PatientPosition patpos) {
			switch(patpos){
				case PatientPosition.Single:
					return "Single";
				case PatientPosition.Married:
					return "Married";
				case PatientPosition.Divorced:
					return "Divorced";
				case PatientPosition.Widowed:
					return "Widowed";
				case PatientPosition.Child:
					return "Single";
				default:
					return "Single";
			}
		}






	}
}
