using System;
using System.Collections;
using System.Drawing;

namespace OpenDentBusiness{
	///<summary>Multiple fields per segment.</summary>
	[Serializable()]
	public class HL7DefField:TableBase{
		///<summary>Primary key.</summary>
		[CrudColumn(IsPriKey=true)]
		public long HL7DefFieldNum;
		///<summary>FK to HL7DefSegment.HL7DefSegmentNum</summary>
		public long HL7DefSegmentNum;
		///<summary>Position within the segment.</summary>
		public int OrdinalPos;
		///<summary>HL7 table Id, if applicable. Example: 0234. Example: 1234/2345.  DataType will be ID.</summary>
		public string TableId;
		///<summary>The DataTypeHL7 enum will be unlinked from the db by storing as string in db. As it's loaded into OD, it will become an enum.</summary>
		[CrudColumn(SpecialType=CrudSpecialColType.EnumAsString)]
		public DataTypeHL7 DataType;
		///<summary>User will get to pick from a list of fields that we will maintain. Example: guarantor.LName, provider.Abbr, or patient.addressFull.  This will be blank if this is a fixed text field.</summary>
		public string FieldName;
		///<summary>User will need to insert fixed text for some fields.  Either FixedText or FieldName will have a value, not both.</summary>
		public string FixedText;

		///<summary></summary>
		public HL7DefField Clone() {
			return (HL7DefField)this.MemberwiseClone();
		}

	}

	///<summary>Data types are listed in HL7 docs section 2.15.  The items in this enumeration can be freely rearranged without damaging the database.  But can't change spelling or remove existing item.</summary>
	public enum DataTypeHL7 {
		///<summary>Coded with no exceptions.  Examples: ProcCode (Dxxxx) or TreatmentArea like tooth^surface</summary>
		CNE,
		///<summary>Composite price.  Example: 125.00</summary>
		CP,
		///<summary>Coded with exceptions.  Example: Race: American Indian or Alaska Native,Asian,Black or African American,Native Hawaiian or Other Pacific,White, Hispanic,Other Race.</summary>
		CWE,
		///<summary>Extended composite ID with check digit.  Example: patient.PatNum or patient.ChartNumber or appointment.AptNum.</summary>
		CX,
		///<summary>Date.  8 digit yyyyMMdd</summary>
		DT,
		///<summary>Date/Time.  8 or 14 digit yyyyMMdd or yyyyMMddHHmmss</summary>
		DTM,
		///<summary>Entity identifier.  Example: appointment.AptNum</summary>
		EI,
		/// <summary>Hierarchic designator.  Application identifier.  Example: "OD" for OpenDental.</summary>
		HD,
		/// <summary>Coded value for HL7 defined tables.  Must include TableId.  Example: 0003 is eCW's event type table id.</summary>
		ID,
		///<summary>Coded value for user-defined tables.  Example: Administrative Sex, F=Female, M=Male,U=Unknown.</summary>
		IS,
		/// <summary>Message type.  Example: composed of messageType^eventType like DFT^P03</summary>
		MSG,
		/// <summary>Numeric.  Example: transaction quantity of 1.0</summary>
		NM,
		/// <summary>Processing type.  Examples: P-Production, T-Test.</summary>
		PT,
		///<summary>Sequence ID.  Example: for repeating segments number that begins with 1.</summary>
		SI,
		///<summary>String, alphanumeric.  Example: SSN or patient.FeeSchedule</summary>
		ST,
		///<summary>Timing quantity.  Example: for eCW appointment ^^duration^startTime^endTime like ^^1200^20120101112000^20120101114000 for 20 minute (1200 second) appointment starting at 11:20 on 01/01/2012</summary>
		TQ,
		/// <summary>Version identifier.  Example: 2.3</summary>
		VID,
		///<summary>Extended address.  Example: Addr1^Addr2^City^State^Zip like 120 Main St.^Suite 3^Salem^OR^97302</summary>
		XAD,
		///<summary>Extended composite ID number and name for person.  Example: provider.EcwID^provider.LName^provider.FName^provider.MI</summary>
		XCN,
		///<summary>Extended person name.  Composite data type.  Example: LName^FName^MI).</summary>
		XPN,
		///<summary>Extended telecommunication number.  Example: 5033635432</summary>
		XTN
	}

	
}
