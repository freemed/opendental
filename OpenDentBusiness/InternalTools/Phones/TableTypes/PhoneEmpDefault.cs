using System;
using System.Collections;
using System.Drawing;

namespace OpenDentBusiness{
	///<summary>This table is not part of the general release.  User would have to add it manually.</summary>
	public class PhoneEmpDefault{
		///<summary></summary>
		public long EmployeeNum;
		///<summary></summary>
		public bool NoGraph;
		///<summary></summary>
		public bool NoColor;
		///<summary>0=all, 1=none, 2=backup</summary>
		public AsteriskRingGroups RingGroups;
		///<summary>Just makes management easier.  Not used by the program.</summary>
		public string EmpName;
	}

	public enum AsteriskRingGroups {
		///<summary>0 - All really means both regular and backup. Most techs.  Default. This setting is used for employees with no entries in this table</summary>
		All,
		///<summary>1 - For example, Jordan and developers.</summary>
		None,
		///<summary>2 - For example, Nathan.</summary>
		Backup
	}

	/*CREATE TABLE phoneempdefault (
					EmployeeNum bigint NOT NULL,
					NoGraph tinyint NOT NULL,
					NoColor tinyint NOT NULL,
					RingGroups int NOT NULL,
					EmpName varchar(255) NOT NULL,
					PRIMARY KEY (EmployeeNum)
					) DEFAULT CHARSET=utf8*/

	
}




