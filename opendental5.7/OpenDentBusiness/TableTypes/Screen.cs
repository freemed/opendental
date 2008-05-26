using System;

namespace OpenDentBusiness{

	///<summary>Used in public health.  This screening table is meant to be general purpose.  It is compliant with the popular Basic Screening Survey.  It is also designed with minimal foreign keys and can be easily adapted to a palm or pocketPC.  This table can be used with only the screengroup table, but is more efficient if provider, school, and county tables are also available.</summary>
	public class Screen{
		///<summary>Primary key</summary>
		public int ScreenNum;
		///<summary>The date of the screening.</summary>
		public DateTime ScreenDate;
		///<summary>FK to school.SchoolName, although it will not crash if key absent.</summary>
		public string GradeSchool;
		///<summary>FK to county.CountyName, although it will not crash if key absent.</summary>
		public string County;
		///<summary>Enum:PlaceOfService</summary>
		public PlaceOfService PlaceService;
		///<summary>FK to provider.ProvNum.  ProvNAME is always entered, but ProvNum supplements it by letting user select from list.  When entering a provNum, the name will be filled in automatically. Can be 0 if the provider is not in the list, but provName is required.</summary>
		public int ProvNum;
		///<summary>Required.</summary>
		public string ProvName;
		///<summary>Enum:PatientGender</summary>
		public PatientGender Gender;
		///<summary>Enum:PatientRace and ethnicity.</summary>
		public PatientRace Race;
		///<summary>Enum:PatientGrade</summary>
		public PatientGrade GradeLevel;
		///<summary>Age of patient at the time the screening was done. Faster than recording birthdates.</summary>
		public int Age;
		///<summary>Enum:TreatmentUrgency</summary>
		public TreatmentUrgency Urgency;
		///<summary>Enum:YN Set to true if patient has cavities.</summary>
		public YN HasCaries;
		///<summary>Enum:YN Set to true if patient needs sealants.</summary>
		public YN NeedsSealants;
		///<summary>Enum:YN</summary>
		public YN CariesExperience;
		///<summary>Enum:YN</summary>
		public YN EarlyChildCaries;
		///<summary>Enum:YN</summary>
		public YN ExistingSealants;
		///<summary>Enum:YN</summary>
		public YN MissingAllTeeth;
		///<summary>Optional</summary>
		public DateTime Birthdate;
		///<summary>FK to screengroup.ScreenGroupNum.</summary>
		public int ScreenGroupNum;
		///<summary>The order of this item within its group.</summary>
		public int ScreenGroupOrder;
		///<summary>.</summary>
		public string Comments;
	}

	

	

}













