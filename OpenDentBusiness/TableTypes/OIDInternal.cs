using System;

namespace OpenDentBusiness {
	///<summary></summary>
	[Serializable]
	public class OIDInternal:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long OIDInternalNum;
		///<summary>Internal data type to be associated with OIDRoot</summary>
		[CrudColumn(SpecialType=CrudSpecialColType.EnumAsString)]
		public IdentifierType IDType;
		///<summary>This is the root OID for this data type, when combined with extension, uniquely identifies a single object.</summary>
		public string IDRoot;


		///<summary></summary>
		public OIDInternal Copy() {
			return (OIDInternal)MemberwiseClone();
		}

	}

	///<summary>Stored as string. Sorted and displayed in the order they are present in this enum.  Root should always be first.</summary>
	public enum IdentifierType {
		///<summary>Will most likely be the root of all other OIDs.  Represents the organization.</summary>
		Root,
		///<summary>FK to EhrLab.EhrLabNum.  root+".1"</summary>
		LabOrder,
		///<summary>FK to Patient.PatNum.  root+".2"</summary>
		Patient,
		///<summary>FK to Provider.ProvNum.  root+".3"</summary>
		Provider
	}
}
