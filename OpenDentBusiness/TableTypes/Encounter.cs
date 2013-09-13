using System;
using System.Collections;

namespace OpenDentBusiness {

	///<summary>Mostly used for EHR.  This rigorously records encounters using rich automation, so that reporting can be easy and meaningful.  Encounters can also be tracked separately using billable procedures.  In contrast, encounters in this table are not billable.  There can be multiple encounters at one appointment because there can be different types.</summary>
	[Serializable]
	public class Encounter:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long EncounterNum;
		///<summary>Date the encounter occurred</summary>
		public DateTime DateEncounter;
		///<summary>FK to Patient. </summary>
		public long PatNum;
		///<summary>FK to Provider. </summary>
		public long ProvNum;
		///<summary>Code value that acts as FK.</summary>
		public string CodeValue;
		///<summary>We only allow the following CodeSystemNames in this table: CDT, CPT, HCPCS, and SNOMEDCT. </summary>
		public string CodeSystemName;
		///<summary>Max length 2000.</summary>
		public string Note;

		///<summary>Returns a copy of this Encounter.</summary>
		public Encounter Clone() {
			return (Encounter)this.MemberwiseClone();
		}

	}



}