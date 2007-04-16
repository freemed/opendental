using System;
using System.Collections;
using System.Text.RegularExpressions;

namespace OpenDentBusiness{

	///<summary>Every InsPlan has a Carrier.  The carrier stores the name and address.</summary>
	public class Carrier{
		///<summary>Primary key.</summary>
		public int CarrierNum;
		///<summary>Name of the carrier.</summary>
		public string CarrierName;
		///<summary>.</summary>
		public string Address;
		///<summary>Second line of address.</summary>
		public string Address2;
		///<summary>.</summary>
		public string City;
		///<summary>2 char in the US.</summary>
		public string State;
		///<summary>Postal code.</summary>
		public string Zip;
		///<summary>Includes any punctuation.</summary>
		public string Phone;
		///<summary>E-claims electronic payer id.  5 char in USA.  6 digits in Canada.  I've seen an ID this long before: "LA-DHH-MEDICAID".  The user interface currently limits length to 20, although db limits length to 255.  X12 requires length between 2 and 80.</summary>
		public string ElectID;
		///<summary>Do not send electronically.  It's just a default; you can still send electronically.</summary>
		public bool NoSendElect;
		///<summary>Canada: True if a CDAnet carrier.  This has significant implications:  1. It can be filtered for in the list of carriers.  2. An ElectID is required.  3. The ElectID can never be used by another carrier.  4. If the carrier is attached to any etrans, then the ElectID cannot be changed (and, of course, the carrier cannot be deleted or combined).</summary>
		public bool IsCDA;
		///<summary>Canada: True if Provincial Medical Plan. We might need to make this a plan field instead.</summary>
		public bool IsPMP;
		///<summary>The version of CDAnet supported.  Either 2, 3, or 4.</summary>
		public string CDAnetVersion;
		///<summary>FK to canadiannetwork.CanadianNetworkNum.  Only used in Canada.</summary>
		public int CanadianNetworkNum;
	}
	

	
	

}













