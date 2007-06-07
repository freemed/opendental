/*using System;
using System.Collections.Generic;
using System.Text;

namespace OpenDentBusiness{
		///<summary>For Dental Schools.  One requirement for one student, whether completed or not.  Copied from reqneeded, and usually kept synchronized.  Future improvements might include allowing extra requirements that are not synched with reqneeded.</summary>
	public class ReqStudent{
		///<summary>Primary key.</summary>
		public int ReqNeededNum;
		///<summary>.</summary>
		public string Descript;
		///<summary>FK to schoolcourse.SchoolCourseNum.  Never 0.</summary>
		public int SchoolCourseNum;
		///<summary>FK to schoolclass.SchoolClassNum.  Never 0.</summary>
		public int SchoolClassNum;

		///<summary></summary>
		public ReqNeeded Copy(){
			ReqNeeded r=new ReqNeeded();
			r.ReqNeededNum=ReqNeededNum;
			r.Descript=Descript;
			r.SchoolCourseNum=SchoolCourseNum;
			r.SchoolClassNum=SchoolClassNum;
			return r;
		}
	}


}
*/