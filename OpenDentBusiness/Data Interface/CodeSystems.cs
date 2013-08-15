using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Windows.Forms;

namespace OpenDentBusiness{
	///<summary></summary>
	public class CodeSystems {

		public List<CodeSystem> GetAllCodeSystems() {
			List<CodeSystem> retVal=new List<CodeSystem>();
			retVal.Add(new CodeSystem("AdministrativeSex","2.16.840.1.113883.18.2"));
			retVal.Add(new CodeSystem("CDCREC","2.16.840.1.113883.6.238"));
			retVal.Add(new CodeSystem("CDT","2.16.840.1.113883.6.13"));
			retVal.Add(new CodeSystem("CPT","2.16.840.1.113883.6.12"));
			retVal.Add(new CodeSystem("CVX","2.16.840.1.113883.12.292"));
			retVal.Add(new CodeSystem("HCPCS","2.16.840.1.113883.6.285"));
			retVal.Add(new CodeSystem("ICD10CM","2.16.840.1.113883.6.90"));
			retVal.Add(new CodeSystem("ICD9CM","2.16.840.1.113883.6.103"));
			retVal.Add(new CodeSystem("LOINC","2.16.840.1.113883.6.1"));
			retVal.Add(new CodeSystem("RXNORM","2.16.840.1.113883.6.88"));
			retVal.Add(new CodeSystem("SNOMEDCT","2.16.840.1.113883.6.96"));
			retVal.Add(new CodeSystem("SOP","2.16.840.1.113883.3.221.5"));
			return retVal;
		}

		///<summary>Will return OID</summary>
		public string GetHL7OID(string codeSystemName) {
			return "";
		}

		///<summary>Used to fill code lists with specific lists of codes to choose from depending on what field is being filled. 
		///For instance if a user is trying to pick a code for a diseaseDef, we should only show them ICD9CM, ICD10CM, and SNOMEDCT as available code sets.</summary>
		public List<CodeSystem> GetSubset(CodeSystemSubset subSet) {
			List<CodeSystem> retVal=new List<CodeSystem>();
			switch(subSet) {
				case CodeSystemSubset.Location1://Might be disease def
					retVal.Add(new CodeSystem("ICD10CM","2.16.840.1.113883.6.90"));
					retVal.Add(new CodeSystem("ICD9CM","2.16.840.1.113883.6.103"));
					retVal.Add(new CodeSystem("SNOMEDCT","2.16.840.1.113883.6.96"));
					break;
				case CodeSystemSubset.Location2://Might be encounter type
					retVal.Add(new CodeSystem("CPT","2.16.840.1.113883.6.12"));
					retVal.Add(new CodeSystem("CVX","2.16.840.1.113883.12.292"));
					retVal.Add(new CodeSystem("HCPCS","2.16.840.1.113883.6.285"));
					retVal.Add(new CodeSystem("ICD10CM","2.16.840.1.113883.6.90"));
					retVal.Add(new CodeSystem("ICD9CM","2.16.840.1.113883.6.103"));
					retVal.Add(new CodeSystem("LOINC","2.16.840.1.113883.6.1"));
					retVal.Add(new CodeSystem("RXNORM","2.16.840.1.113883.6.88"));
					retVal.Add(new CodeSystem("SNOMEDCT","2.16.840.1.113883.6.96"));
					retVal.Add(new CodeSystem("SOP","2.16.840.1.113883.3.221.5"));
					break;
				case CodeSystemSubset.Location3://Might be Vaccine type
					retVal.Add(new CodeSystem("CPT","2.16.840.1.113883.6.12"));
					retVal.Add(new CodeSystem("CVX","2.16.840.1.113883.12.292"));
					retVal.Add(new CodeSystem("HCPCS","2.16.840.1.113883.6.285"));
					retVal.Add(new CodeSystem("RXNORM","2.16.840.1.113883.6.88"));
					retVal.Add(new CodeSystem("SNOMEDCT","2.16.840.1.113883.6.96"));
					break;
			}
			return retVal;
		}

	}

	///<summary>Used internally for filling list of code systems to allow the user to pick from.</summary>
	public enum CodeSystemSubset{
		///<summary>Not acutally Used. Demo. Delete me. Add to list below.</summary>
		Location1,
		///<summary>Not acutally Used. Demo. Delete me. Add to list below.</summary>
		Location2,
		///<summary>Not acutally Used. Demo. Delete me. Add to list below.</summary>
		Location3
	}

	///<summary>Not a database table.</summary>
	public class CodeSystem {
		string CodeSystemName;
		string HL7OID;

		public CodeSystem(string name,string id) {
			CodeSystemName=name;
			HL7OID=id;
		}
	}

}