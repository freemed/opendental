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
		///<summary>The phone extension for the employee.  e.g. 101,102,etc.  Used to be in the employee table.  This can be changed daily by staff who float from workstation to workstation.</summary>
		public int PhoneExt;
		///<summary>Enum:PhoneEmpStatusOverride </summary>
		public PhoneEmpStatusOverride StatusOverride;
		///<summary>Used to be stored as phoneoverride.Explanation.</summary>
		public string Notes;
		///<summary>This is used by the cameras.  It's actually used for computer name, not ip address.  Only necessary when the ip address doesn't match the 192.168.0.2xx pattern that we normally use.  For example, if Jordan sets this value to JORDANS, then the camera on JORDANS(.186) will send its images to the phone table where extension=104.  The second consequence is that .204 will not send any camera images.  This is used heavily by remote users working from home.  If a staff floats to another .2xx workstation, then this does not need to be set since it will match their changed extension with their current workstation ip address because if follows the normal pattern.  If there are multiple ip addresses, and the camera picks up the wrong one, setting this field can fix it.</summary>
		public string ComputerName;
		///<summary>Can only be used by management when handling personnel issues.</summary>
		public bool IsPrivateScreen;

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

	public enum PhoneEmpStatusOverride {
		///<summary>0 - None.</summary>
		None,
		///<summary>1 </summary>
		Unavailable,
		///<summary>2</summary>
		OfflineAssist
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




