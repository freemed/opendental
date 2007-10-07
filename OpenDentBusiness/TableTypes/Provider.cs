
using System;
using System.Collections;
using System.Drawing;

namespace OpenDentBusiness{
	
	///<summary>A provider is usually a dentist or a hygienist.  But a provider might also be a denturist, a dental student, or a dental hygiene student.  A provider might also be a 'dummy', used only for billing purposes or for notes in the Appointments module.  There is no limit to the number of providers that can be added.</summary>
	public class Provider{
		///<summary>Primary key.</summary>
		public int ProvNum;
		///<summary>Abbreviation.</summary>
		public string Abbr;
		///<summary>Order that provider will show in lists.</summary>
		public int ItemOrder;
		///<summary>Last name.</summary>
		public string LName;
		///<summary>First name.</summary>
		public string FName;
		///<summary>Middle inital or name.</summary>
		public string MI;
		///<summary>eg. DMD or DDS. Was 'title' in previous versions.</summary>
		public string Suffix;
		///<summary>FK to Definition.DefNum.</summary>
		public int FeeSched;
		///<summary>Enum:DentalSpecialty</summary>
		public DentalSpecialty Specialty;
		///<summary>or TIN.  No punctuation</summary>
		public string SSN;
		///<summary>can include punctuation</summary>
		public string StateLicense;
		///<summary>.</summary>
		public string DEANum;
		///<summary>True if hygienist.</summary>
		public bool IsSecondary;//
		///<summary>Color that shows in appointments</summary>
		public Color ProvColor;
		///<summary>If true, provider will not show on any lists</summary>
		public bool IsHidden;
		///<summary>True if the SSN field is actually a Tax ID Num</summary>
		public bool UsingTIN;
		///<summary>No longer used since each state assigns a different ID.  Use the providerident instead which allows you to assign a different BCBS ID for each Payor ID.</summary>
		public string BlueCrossIDOld;
		///<summary>Signature on file.</summary>
		public bool SigOnFile;
		///<summary>.</summary>
		public string MedicaidID;
		///<summary>Color that shows in appointments as outline when highlighted.</summary>
		public Color OutlineColor;
		///<summary>FK to schoolclass.SchoolClassNum Used in dental schools.  Each student is a provider.  This keeps track of which class they are in.</summary>
		public int SchoolClassNum;
		///<summary>Used for Canadian claims right now as CDA number.  Will be required in US within a year.  Goes out on e-claims if available.</summary>
		public string NationalProvID;
		///<summary>Canadian field required for e-claims.  Assigned by CDA.  It's OK to have multiple providers with the same OfficeNum.  Max length should be 4.</summary>
		public string CanadianOfficeNum;

		///<summary>Returns a copy of this Provider.</summary>
		public Provider Copy(){
			Provider p=new Provider();
			p.ProvNum=ProvNum;
			p.Abbr=Abbr;
			p.ItemOrder=ItemOrder;
			p.LName=LName;
			p.FName=FName;
			p.MI=MI;
			p.Suffix=Suffix;
			p.FeeSched=FeeSched;
			p.Specialty=Specialty;
			p.SSN=SSN;
			p.StateLicense=StateLicense;
			p.DEANum=DEANum;
			p.IsSecondary=IsSecondary;
			p.ProvColor=ProvColor;
			p.IsHidden=IsHidden;
			p.UsingTIN=UsingTIN;
			//bluecross
			p.SigOnFile=SigOnFile;
			p.MedicaidID=MedicaidID;
			p.OutlineColor=OutlineColor;
			p.SchoolClassNum=SchoolClassNum;
			p.NationalProvID=NationalProvID;
			p.CanadianOfficeNum=CanadianOfficeNum;
			return p;
		}

		///<summary></summary>
		public string GetNameLF() {
			return LName+", "+FName;
		}

	}
	
	

}










