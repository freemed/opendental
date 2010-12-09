using System;
using System.Collections;
using System.Drawing;

namespace OpenDentBusiness{
	///<summary>This table is not part of the general release.  User would have to add it manually.</summary>
	[Serializable]
	[CrudTable(IsMissingInGeneral=true)]
	public class PhoneEmpDefault:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long EmployeeNum;
		///<summary></summary>
		public bool NoGraph;
		///<summary></summary>
		public bool NoColor;
		///<summary>Enum:AsteriskRingGroups 0=all, 1=none, 2=backup</summary>
		public AsteriskRingGroups RingGroups;
		///<summary>Just makes management easier.  Not used by the program.</summary>
		public string EmpName;

		///<summary></summary>
		public PhoneEmpDefault Clone() {
			return (PhoneEmpDefault)this.MemberwiseClone();
		}
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
		EmployeeNum BIGINT NOT NULL,      
		NoGraph TINYINT NOT NULL,      
		NoColor TINYINT NOT NULL,      
		RingGroups INT NOT NULL,      
		EmpName VARCHAR(255) NOT NULL,      
		PRIMARY KEY (EmployeeNum)      
		) DEFAULT CHARSET=utf8; */
}




