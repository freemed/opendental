using System;
using System.Collections;
using System.Text.RegularExpressions;

namespace OpenDentBusiness{

	///<summary>A dental laboratory. Will be attached to lab cases.</summary>
	public class Laboratory{
		///<summary>Primary key.</summary>
		public int LaboratoryNum;
		///<summary>Description of lab.</summary>
		public string Description;
		///<summary>Freeform text includes punctuation.</summary>
		public string Phone;
		///<summary>Any notes.  No practical limit to amount of text.</summary>
		public string Notes;
		///<summary>Not used yet.  The text base64 representation of the background scanned labslip.  Will be converted to an image at the last minute as needed.  Limit of image size will be about 16 MB.  Typical image size will be about 200 KB.</summary>
		public string LabSlip;

		public Laboratory Copy(){
			Laboratory l=new Laboratory();
			l.LaboratoryNum=LaboratoryNum;
			l.Description=Description;
			l.Phone=Phone;
			l.Notes=Notes;
			l.LabSlip=LabSlip;
			return l;
		}
		
	}
	
	
	

}













