using System;

namespace OpenDentBusiness {
	///<summary></summary>
	[Serializable]
	public class OIDInternal:TableBase {
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long EhrOIDNum;
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

	public enum IdentifierType {
		///<summary>Will most likely be the root of all other OIDs.  Represents the organization.</summary>
		root,
		///<summary>FK to Patient.PatNum</summary>
		Patient,
		///<summary>FK to Provider.ProvNum</summary>
		Provider
	}
}
