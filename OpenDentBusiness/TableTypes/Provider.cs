using System;
using System.Collections;
using System.Drawing;
using System.Xml.Serialization;

namespace OpenDentBusiness{
	
	///<summary>A provider is usually a dentist or a hygienist.  But a provider might also be a denturist, a dental student, or a dental hygiene student.  A provider might also be a 'dummy', used only for billing purposes or for notes in the Appointments module.  There is no limit to the number of providers that can be added.</summary>
	[Serializable()]
	public class Provider:TableBase{
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long ProvNum;
		///<summary>Abbreviation.  There was a limit of 5 char before version 5.4.  The new limit is 255 char.  This will allow more elegant solutions to various problems.  Providers will no longer be referred to by FName and LName.  Abbr is used as a human readable primary key.</summary>
		public string Abbr;
		///<summary>Order that provider will show in lists. Was 1-based, now 0-based.</summary>
		public int ItemOrder;
		///<summary>Last name.</summary>
		public string LName;
		///<summary>First name.</summary>
		public string FName;
		///<summary>Middle inital or name.</summary>
		public string MI;
		///<summary>eg. DMD or DDS. Was 'title' in previous versions.</summary>
		public string Suffix;
		///<summary>FK to feesched.FeeSchedNum.</summary>
		public long FeeSched;
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
		[XmlIgnore]
		public Color ProvColor;
		///<summary>If true, provider will not show on any lists</summary>
		public bool IsHidden;
		///<summary>True if the SSN field is actually a Tax ID Num</summary>
		public bool UsingTIN;
		///<summary>No longer used since each state assigns a different ID.  Use the providerident instead which allows you to assign a different BCBS ID for each Payor ID.</summary>
		public string BlueCrossID;
		///<summary>Signature on file.</summary>
		public bool SigOnFile;
		///<summary>.</summary>
		public string MedicaidID;
		///<summary>Color that shows in appointments as outline when highlighted.</summary>
		[XmlIgnore]
		public Color OutlineColor;
		///<summary>FK to schoolclass.SchoolClassNum Used in dental schools.  Each student is a provider.  This keeps track of which class they are in.</summary>
		public long SchoolClassNum;
		///<summary>US NPI, and Canadian CDA provider number.</summary>
		public string NationalProvID;
		///<summary>Canadian field required for e-claims.  Assigned by CDA.  It's OK to have multiple providers with the same OfficeNum.  Max length should be 4.</summary>
		public string CanadianOfficeNum;
		/// <summary></summary>
		[CrudColumn(SpecialType=CrudSpecialColType.TimeStamp)]
		public DateTime DateTStamp;
		/// <summary> FK to ??. Field used to set the Anesthesia Provider type. Used to filter the provider dropdowns on FormAnestheticRecord</summary>
		public long AnesthProvType;
		///<summary>If none of the supplied taxonomies works.  This will show on claims.</summary>
		public string TaxonomyCodeOverride;
		///<summary>For Canada. Set to true if CDA Net provider.</summary>
		public bool IsCDAnet;

		//Need: StateRxNum varchar255

		///<summary>Used only for serialization purposes</summary>
		[XmlElement("ProvColor",typeof(int))]
		public int ProvColorXml {
			get {
				return ProvColor.ToArgb();
			}
			set {
				ProvColor = Color.FromArgb(value);
			}
		}

		///<summary>Used only for serialization purposes</summary>
		[XmlElement("OutlineColor",typeof(int))]
		public int OutlineColorXml {
			get {
				return OutlineColor.ToArgb();
			}
			set {
				OutlineColor = Color.FromArgb(value);
			}
		}

		///<summary>Returns a copy of this Provider.</summary>
		public Provider Copy(){
			return (Provider)MemberwiseClone();
		}

		public Provider(){

		}

		public Provider(long provNum,string abbr,int itemOrder,string lName,string fName,string mI,string suffix,long feeSched,
			DentalSpecialty specialty,string sSN,string stateLicense,string dEANum,bool isSecondary,Color provColor,bool isHidden,
			bool usingTIN,string blueCrossID,bool sigOnFile,string medicaidID,Color outlineColor,long schoolClassNum,
			string nationalProvID,string canadianOfficeNum,long anesthProvType)
		{
			ProvNum=provNum;
			Abbr=abbr;
			ItemOrder=itemOrder;
			LName=lName;
			FName=fName;
			MI=mI;
			Suffix=suffix;
			FeeSched=feeSched;
			Specialty=specialty;
			SSN=sSN;
			StateLicense=stateLicense;
			DEANum=dEANum;
			IsSecondary=isSecondary;
			ProvColor=provColor;
			IsHidden=isHidden;
			UsingTIN=usingTIN;
			BlueCrossID=blueCrossID;
			SigOnFile=sigOnFile;
			MedicaidID=medicaidID;
			OutlineColor=outlineColor;
			SchoolClassNum=schoolClassNum;
			NationalProvID=nationalProvID;
			CanadianOfficeNum=canadianOfficeNum;
			//DateTStamp
			AnesthProvType = anesthProvType;
		}


		///<Summary>For use in areas of the program where we have more room than just simple abbr.  Such as pick boxes in reports.  This will give Abbr - LName, FName (hidden).</Summary>
		public string GetLongDesc(){
			string retval=Abbr+" - "+LName+", "+FName;
			if(IsHidden){
				retval+=" "+Lans.g("Providers","(hidden)");
			}
			return retval;
		}

		///<summary>FName MI. LName, Suffix</summary>
		public string GetFormalName() {
			string retVal=FName+" "+MI;
			if(MI.Length==1){
				retVal+=".";
			}
			if(MI!=""){
				retVal+=" ";
			}
			retVal+=LName;
			if(Suffix!=""){
				retVal+=", "+Suffix;
			}
			return retVal;
		}

		
	}
	
	

}










