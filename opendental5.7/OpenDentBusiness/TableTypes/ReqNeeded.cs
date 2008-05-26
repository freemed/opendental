using System;
using System.Collections.Generic;
using System.Text;

namespace OpenDentBusiness{
		///<summary>For Dental Schools.  Requirements needed in order to complete a course.</summary>
	public class ReqNeeded{
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
