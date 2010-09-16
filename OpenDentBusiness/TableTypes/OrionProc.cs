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
		///<summary>Enum:DPC None=0,1A=1,1B=2,1C=3,2=4,3=5,4=6,5=7</summary>
		public DPC DPCenum;
		///<summary>System adds days to the diagnosis date based upon the DPC entered for that procedure. If DPC = none the system will return “No Schedule by Date”. </summary>
		public DateTime DateScheduleBy;
		///<summary> Default to current date.  Provider shall have to ability to edit with a previous date, but not a future date.</summary>
		public DateTime DateStopClock;
		///<summarty>Enum:Status2 TP=0,C=1,E=2,R=3,RO=4,CS=5,CR=6,CA-Tx=7,CA-ERPD=8,CA-P/D=9,S=10,ST=11,W=12</summarty>
		public Status2 Status2;
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

	public enum DPC{
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

	public enum Status2{
		///<summary>0– Treatment planned</summary>
		TP,
		///<summary>1– Treatment completed</summary>
		C,
		///<summary>2– Condition existing prior to incarceration</summary>
		E,
		///<summary>3– Patient refused treatment</summary>
		R,
		///<summary>4– Planned treatment to be done by a specialist</summary>
		RO,
		///<summary>5– Completed by a specialist</summary>
		CS,
		///<summary>6– Completed by a registry</summary>
		CR,
		///<summary>7- CA-Tx:Indicates the planned treatment has been cancelled due to a change in the treatment plan</summary>
		CA_Tx,
		///<summary>8- CA-EPRD:Planned treatment has been cancelled because the patient is no longer eligible due to upcoming parole</summary>
		CA_EPRD,
		///<summary>9- CA-P/D:Planned treatment has been cancelled because the patient has left the system </summary>
		CA_PD,
		///<summary>10– Planned treatment has been suspended</summary>
		S,
		///<summary>11- Treatment shall require several visits to complete</summary>
		ST,
		///<summary>12– Condition is not planned for treatment at this time, but provider is monitoring its status “Watch"</summary>
		W
	}


}

