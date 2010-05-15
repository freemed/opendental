using System;
using System.Collections;
using System.Drawing;

namespace OpenDentBusiness{
	///<summary>This table is not part of the general release.  User would have to add it manually.</summary>
	public class PhoneExclusion{
		///<summary></summary>
		public long PhoneExclusionNum;
		///<summary></summary>
		public long EmployeeNum;
		///<summary></summary>
		public bool NoGraph;
		///<summary></summary>
		public bool NoColor;
	}

	/*CREATE TABLE phoneexclusion (
					PhoneExclusionNum bigint NOT NULL auto_increment,
					EmployeeNum bigint NOT NULL,
					NoGraph tinyint NOT NULL,
					NoColor tinyint NOT NULL,
					PRIMARY KEY (PhoneExclusionNum)
					) DEFAULT CHARSET=utf8*/

	
}




