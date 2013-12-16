using EhrLaboratories;
using System;
using System.Collections.Generic;

namespace OpenDentBusiness {
	///<summary>For EHR module, the specimen upon which the lab orders were/are to be performed on.  NTE.*</summary>
	[Serializable]
	public class EhrLabSpecimen:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long EhrLabSpecimenNum;
		///<summary>FK to EhrLab.EhrLabNum.  May be 0.</summary>
		public long EhrLabNum;
		///<summary>Enumerates the SPM segments within a single message starting with 1.  SPM.1</summary>
		public long SetIdSPM;
		///<summary>SPM.1</summary>
		public string SpecimenTypeID;
		///<summary>Description of SpecimenTypeId.  SPM.2</summary>
		public string SpecimenTypeText;
		///<summary>CodeSystem that SpecimenTypeId came from.  SPM.3</summary>
		[CrudColumn(SpecialType=CrudSpecialColType.EnumAsString)]
		public HL70369 SpecimenTypeCodeSystemName;
		///<summary>SPM.4</summary>
		public string SpecimenTypeIDAlt;
		///<summary>Description of SpecimenTypeIdAlt.  SPM.5</summary>
		public string SpecimenTypeTextAlt;
		///<summary>CodeSystem that SpecimenTypeId came from.  SPM.6</summary>
		[CrudColumn(SpecialType=CrudSpecialColType.EnumAsString)]
		public HL70369 SpecimenTypeCodeSystemNameAlt;
		///<summary>Optional text that describes the original text used to encode the values above.  SPM.7</summary>
		public string SpecimenTypeTextOriginal;
		///<summary>Stored as string in the format YYYYMMDD[HH[MM[SS]]] where bracketed values are optional.  When time is not known will be valued "0000".  SPM.17.1.1</summary>
		public string CollectionDateTimeStart;
		///<summary>May be empty.  Stored as string in the format YYYYMMDD[HH[MM[SS]]] where bracketed values are optional.  SPM.17.2.1</summary>
		public string CollectionDateTimeEnd;
		///<summary>[0..*]This is not a data column but is stored in a seperate table named EhrLabSpecimenRejectReason.  SPM.21</summary>
		[CrudColumn(IsNotDbColumn=true)]
		public List<EhrLabSpecimenRejectReason> ListEhrLabSpecimenRejectReason;
		///<summary>[0..*]This is not a data column but is stored in a seperate table named EhrLabSpecimenCondition.  SPM.24</summary>
		[CrudColumn(IsNotDbColumn=true)]
		public List<EhrLabSpecimenCondition> ListRelevantClinicalInformation;


		///<summary></summary>
		public EhrLabSpecimen Copy() {
			return (EhrLabSpecimen)MemberwiseClone();
		}

	}

}