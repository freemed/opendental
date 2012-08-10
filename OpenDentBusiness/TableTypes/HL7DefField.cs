﻿using System;
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
		///<summary>User will get to pick from a list of fields that we will maintain. Example: guarantor.LName, provider.Abbr, or patient.addressFull.</summary>
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
		///<summary>Coded with no exceptions.</summary>
		CNE,
		///<summary>Composite price.</summary>
		CP,
		///<summary>Coded with exceptions.  Example: Race: American Indian or Alaska Native,Asian,Black or African American,Native Hawaiian or Other Pacific,White, Hispanic,Other Race.</summary>
		CWE,
		///<summary>Extended composite ID with check digit.</summary>
		CX,
		///<summary>Date.</summary>
		DT,
		///<summary>Date/Time.</summary>
		DTM,
		///<summary>Entity identifier.</summary>
		EI,
		/// <summary>Hierarchic designator.  Application identifier.</summary>
		HD,
		/// <summary>Coded value for HL7 defined tables.  Must include TableId.</summary>
		ID,
		///<summary>Coded value for user-defined tables.  Example: Administrative Sex, F=Female, M=Male,U=Unknown.</summary>
		IS,
		/// <summary>Message type.</summary>
		MSG,
		/// <summary>Numeric.</summary>
		NM,
		/// <summary>Processing type.  Examples: P-Production, T-Test.</summary>
		PT,
		///<summary>Sequence ID.</summary>
		SI,
		///<summary>String, alphanumeric.</summary>
		ST,
		///<summary>Timing quantity.</summary>
		TQ,
		/// <summary>Version identifier.</summary>
		VID,
		///<summary>Extended address.</summary>
		XAD,
		///<summary>Extended composite ID number and name for person.</summary>
		XCN,
		///<summary>Extended person name.  Composite data type.  Example: component 0 is LName, 1 is FName, 2 is second and further given names or initials (MiddleI).</summary>
		XPN,
		///<summary>Extended telecommunication number.</summary>
		XTN
	}

	
}
