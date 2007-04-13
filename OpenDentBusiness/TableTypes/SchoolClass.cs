using System;
using System.Collections;

namespace OpenDentBusiness{

	///<summary>Used in dental schools.  eg. Dental 2009 or Hygiene 2007.</summary>
	public class SchoolClass{
		///<summary>Primary key.</summary>
		public int SchoolClassNum;
		///<summary>The year this class will graduate</summary>
		public int GradYear;
		///<summary>Description of this class. eg Dental or Hygiene</summary>
		public string Descript;

		///<summary></summary>
		public SchoolClass Copy(){
			SchoolClass sc=new SchoolClass();
			sc.SchoolClassNum=SchoolClassNum;
			sc.GradYear=GradYear;
			sc.Descript=Descript;
			return sc;
		}

	
	}

	

	


}




















