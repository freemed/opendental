using System;
using System.Collections.Generic;
using System.Text;

namespace OpenDentBusiness {
	///<summary>Keeps track of which product keys have been assigned to which customers. This datatype is only used if the program is being run from a distributor installation. A single customer is allowed to have more than one key, to accomadate for various circumstances, including having multiple physical business locations.</summary>
	public class RegistrationKey {
		///<summary>Primary Key.</summary>
		public int RegistrationKeyNum;
		///<summary>The customer to which this registration key applies.</summary>
		public int PatNum;
		///<summary>The registration key as stored in the customer database.</summary>
		public string RegKey;
		///<summary>General note about the registration key. Specifically, the note must include information about the location to which this key pertains, since once at least one key must be assigned to each location to be legal.</summary>
		public string Note;

		public RegistrationKey Copy(){
			return (RegistrationKey)this.MemberwiseClone();
		}
	}
}
