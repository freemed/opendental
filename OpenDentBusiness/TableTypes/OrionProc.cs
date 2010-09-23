using System;
using System.Collections;
using System.Drawing;

namespace OpenDentBusiness{
	///<summary></summary>
	[Serializable]
	public class OrionProc:TableBase{
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long OrionProcNum;
		///<summary>FK to procedurelog.ProcNum</summary>
		public long ProcNum;
		///<summary>Enum:OrionDPC None=0,1A=1,1B=2,1C=3,2=4,3=5,4=6,5=7</summary>
		public OrionDPC DPC;
		///<summary>System adds days to the diagnosis date based upon the DPC entered for that procedure. If DPC = none the system will return “No Schedule by Date”. </summary>
		public DateTime DateScheduleBy;
		///<summary> Default to current date.  Provider shall have to ability to edit with a previous date, but not a future date.</summary>
		public DateTime DateStopClock;
		///<summarty>Enum:OrionStatus None=0,TP=1,C=2,E=4,R=8,RO=16,CS=32,CR=64,CA-Tx=128,CA-ERPD=256,CA-P/D=512,S=1024,ST=2048,W=4096</summarty>
		public OrionStatus Status2;
		///<summary></summary>
		public bool IsOnCall;
		///<summary>Indicates in the clinical note that effective communication was used for this encounter.</summary>
		public bool IsEffectiveComm;
		///<summary></summary>
		public bool IsRepair;
		


		public OrionProc Copy() {
			return (OrionProc)this.MemberwiseClone();
		}
	}

	public enum OrionDPC{
	  ///<summary>0- None</summary>
	  None,
	  ///<summary>1- Treatment to be scheduled within 1 calendar day</summary>
	  _1A,
	  ///<summary>2- Treatment to be scheduled within 30 calendar days</summary>
	  _1B,
	  ///<summary>3- Treatment to be scheduled within 60 calendar days</summary>
	  _1C,
	  ///<summary>4– Treatment to be scheduled within 120 calendar days</summary>
	  _2,
	  ///<summary>5– Treatment to be scheduled within 1 year</summary>
	  _3,
	  ///<summary>6– No further treatment is needed, no appointment needed</summary>
	  _4,
	  ///<summary>7– No appointment needed </summary>
	  _5
	}

	[Flags]
	public enum OrionStatus {
		///<summary>0- None</summary>
		None=0,
		///<summary>1– Treatment planned</summary>
		TP=1,
		///<summary>2– Treatment completed</summary>
		C=2,
		///<summary>4– Condition existing prior to incarceration</summary>
		E=4,
		///<summary>8– Patient refused treatment</summary>
		R=8,
		///<summary>16– Planned treatment to be done by a specialist</summary>
		RO=16,
		///<summary>32– Completed by a specialist</summary>
		CS=32,
		///<summary>64– Completed by a registry</summary>
		CR=64,
		///<summary>128- CA-Tx:Indicates the planned treatment has been cancelled due to a change in the treatment plan</summary>
		CA_Tx=128,
		///<summary>256- CA-EPRD:Planned treatment has been cancelled because the patient is no longer eligible due to upcoming parole</summary>
		CA_EPRD=256,
		///<summary>512- CA-P/D:Planned treatment has been cancelled because the patient has left the system </summary>
		CA_PD=512,
		///<summary>1024– Planned treatment has been suspended</summary>
		S=1024,
		///<summary>2048- Treatment shall require several visits to complete</summary>
		ST=2048,
		///<summary>4096– Condition is not planned for treatment at this time, but provider is monitoring its status “Watch"</summary>
		W=4096
	}


}

