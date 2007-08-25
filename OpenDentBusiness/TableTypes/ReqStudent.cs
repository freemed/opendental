using System;
using System.Collections.Generic;
using System.Text;

namespace OpenDentBusiness{
		///<summary>For Dental Schools.  One requirement for one student, whether completed or not.  Copied from reqneeded, and usually kept synchronized.  Future improvements might include allowing extra requirements that are not synched with reqneeded.</summary>
	public class ReqStudent{
		///<summary>Primary key.</summary>
		public int ReqStudentNum;
		///<summary>FK to reqneeded.ReqNeededNum.  Used for synchronization.</summary>
		public int ReqNeededNum;
		///<summary>.</summary>
		public string Descript;
		///<summary>FK to schoolcourse.SchoolCourseNum.  Never 0.</summary>
		public int SchoolCourseNum;
		///<summary>FK to provider.ProvNum.  The student.  Never 0.</summary>
		public int ProvNum;
		///<summary>FK to appointment.AptNum.</summary>
		public int AptNum;
		///<summary>FK to patient.PatNum</summary>
		public int PatNum;
		///<summary>FK to provider.ProvNum</summary>
		public int InstructorNum;
		///<summary>The date that the requirement was completed.</summary>
		public DateTime DateCompleted;

		///<summary></summary>
		public ReqStudent Copy(){
			ReqStudent r=new ReqStudent();
			r.ReqStudentNum=ReqStudentNum;
			r.ReqNeededNum=ReqNeededNum;
			r.Descript=Descript;
			r.SchoolCourseNum=SchoolCourseNum;
			r.ProvNum=ProvNum;
			r.AptNum=AptNum;
			r.PatNum=PatNum;
			r.InstructorNum=InstructorNum;
			r.DateCompleted=DateCompleted;
			return r;
		}
	}


}
