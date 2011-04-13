using System;
using System.Collections.Generic;
using System.Text;
using OpenDentBusiness;

namespace OpenDentBusiness.HL7 {
	///<summary>A VXU message is an Unsolicited Vaccination Record Update.  It is a message sent out from Open Dental detailing vaccinations that were given.</summary>
	public class VXU {
		private MessageHL7 msg;
		private SegmentHL7 seg;

		///<summary></summary>
		public VXU() {
			
		}
		
		///<summary>Creates the Message object and fills it with data.  Vaccines must all be for the same patient.</summary>
		public void Initialize(Patient pat,List<VaccinePat> vaccines){
			if(vaccines.Count==0) {
				throw new ApplicationException("Must be at least one vaccine.");
			}
			msg=new MessageHL7(MessageType.VXU);
			MSH();
			PID(pat);
			for(int i=0;i<vaccines.Count;i++) {
				ORC();
				RXA();
			}
		}

		///<summary>Message Header Segment</summary>
		private void MSH(){
			seg=new SegmentHL7(@"MSH|^~\&|OD||ECW||"+DateTime.Now.ToString("yyyyMMddHHmmss")+"||VXU^V04||P|2.5");
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
			//need to validate dob first
			seg.SetField(7,pat.Birthdate.ToString("yyyyMMdd"));
			//seg.SetField(8,ConvertGender(pat.Gender));
			//seg.SetField(10,ConvertRace(pat.Race));
			seg.SetField(11,pat.Address,pat.Address2,pat.City,pat.State,pat.Zip);
			//seg.SetField(13,ConvertPhone(pat.HmPhone));
			//seg.SetField(14,ConvertPhone(pat.WkPhone));
			//seg.SetField(16,ConvertMaritalStatus(pat.Position));
			seg.SetField(19,pat.SSN);
			msg.Segments.Add(seg);
		}

		private void ORC() {
			seg=new SegmentHL7(SegmentName.ORC);
			//seg.SetField...

			msg.Segments.Add(seg);
		}

		private void RXA() {
			seg=new SegmentHL7(SegmentName.RXA);
			//seg.SetField...

			msg.Segments.Add(seg);
		}

		public string GenerateMessage() {
			return msg.ToString();
		}

		




	}
}
