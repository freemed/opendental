using System;
using System.Collections.Generic;
using System.Text;

namespace OpenDentBusiness{
		///<summary>For Dental Schools.  One requirement for one student, whether completed or not.  Copied from reqneeded, and usually kept synchronized.  Future improvements might include allowing extra requirements that are not synched with reqneeded.</summary>
	public class ReqStudent{
		///<summary>Primary key.</summary>
		public int ReqStudentNum;
		///<Summary>FK to reqneeded.ReqNeededNum.  Used for synchronization.</Summary>
		public int ReqNeededNum;
		///<summary>.</summary>
		public string Descript;
		///<summary>FK to schoolcourse.SchoolCourseNum.  Never 0.</summary>
		public int SchoolCourseNum;
		///<summary>FK to provider.ProvNum.  The student.  Never 0.</summary>
		public int ProvNum;
		///<Summary>FK to appointment.AptNum.</Summary>
		public int AptNum;
		///<Summary>FK to patient.PatNum</Summary>
		public int PatNum;
		///<Summary>FK to provider.ProvNum</Summary>
		public int InstructorNum;
		///<Summary>For now, either 0 or 1.  Later, grades like 3.5 will be supported.</Summary>
		public float GradePoint;
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
			r.GradePoint=GradePoint;
			return r;
		}
	}


}
