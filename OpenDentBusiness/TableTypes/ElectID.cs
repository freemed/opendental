using System;
using System.Collections;

namespace OpenDentBusiness{
	
	///<summary>Corresponds to the electid table in the database. Never editable by users.  We keep this table updated with new numbers as part of upgrades, so never edit this table as it will mess up our primary keys.  Helps with entering elecronic/payor id's as well as keeping track of the specific carrier requirements. Only used by the X12 format.</summary>
	public class ElectID{
		///<summary>Primary key.</summary>
		public int ElectIDNum;
		///<summary>aka Electronic ID.  A simple string.</summary>
		public string PayorID;
		///<summary>As published. Not editable. Used when doing a search.</summary>
		public string CarrierName;
		///<summary>True if medicaid. Then, the billing and treating providers will have their Medicaid ID's attached.</summary>
		public bool IsMedicaid;
		///<summary>Integers separated by commas. Each int represents a ProviderSupplementalID type that is required by this insurance. Usually only used for BCBS or other carriers that require supplemental provider id's.  Even if we don't put the supplemental types in here, the user can still add them.  This just helps by doing an additional check for known required types.</summary>
		public string ProviderTypes;
		///<summary>Any comments. Usually includes enrollment requirements and descriptions of how to use the provider id's supplied by the carrier because they might call them by different names.</summary>
		public string Comments;



	
	}
	
	

}










