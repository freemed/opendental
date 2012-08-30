using System;
using System.Collections;
using System.Collections.Generic;
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
		///<summary>User will get to pick from a list of fields that we will maintain. Example: guar.nameLFM, prov.provIdName, or pat.addressCityStateZip.  See below for the full list.  This will be blank if this is a fixed text field.</summary>
		public string FieldName;
		///<summary>User will need to insert fixed text for some fields.  Either FixedText or FieldName will have a value, not both.</summary>
		public string FixedText;

		///<summary></summary>
		public HL7DefField Clone() {
			return (HL7DefField)this.MemberwiseClone();
		}

	}

	public class FieldNameAndType{
		public string Name;
		public string TableId;
		public DataTypeHL7 DataType;

		public FieldNameAndType(string name,DataTypeHL7 datatype) {
			Name=name;
			DataType=datatype;
			TableId="";
		}

		public FieldNameAndType(string name,DataTypeHL7 datatype,string tableid) {
			Name=name;
			DataType=datatype;
			TableId=tableid;
		}

		public static List<FieldNameAndType> GetFullList() {
			List<FieldNameAndType> retVal=new List<FieldNameAndType>();
			retVal.Add(new FieldNameAndType("apt.AptNum",DataTypeHL7.CX));
			retVal.Add(new FieldNameAndType("apt.lengthStartEnd",DataTypeHL7.TQ));
			retVal.Add(new FieldNameAndType("apt.Note",DataTypeHL7.CWE));
			retVal.Add(new FieldNameAndType("dateTime.Now",DataTypeHL7.DTM));
			retVal.Add(new FieldNameAndType("eventType",DataTypeHL7.ID,"0003"));
			retVal.Add(new FieldNameAndType("guar.addressCityStateZip",DataTypeHL7.XAD));
			retVal.Add(new FieldNameAndType("guar.birthdateTime",DataTypeHL7.DTM));
			retVal.Add(new FieldNameAndType("guar.ChartNumber",DataTypeHL7.CX));
			retVal.Add(new FieldNameAndType("guar.Gender",DataTypeHL7.IS));
			retVal.Add(new FieldNameAndType("guar.HmPhone",DataTypeHL7.XTN));
			retVal.Add(new FieldNameAndType("guar.nameLFM",DataTypeHL7.XPN));
			retVal.Add(new FieldNameAndType("guar.PatNum",DataTypeHL7.CX));
			retVal.Add(new FieldNameAndType("guar.SSN",DataTypeHL7.ST));
			retVal.Add(new FieldNameAndType("guar.WkPhone",DataTypeHL7.XTN));
			retVal.Add(new FieldNameAndType("messageType",DataTypeHL7.MSG));
			retVal.Add(new FieldNameAndType("pat.addressCityStateZip",DataTypeHL7.XAD));
			retVal.Add(new FieldNameAndType("pat.birthdateTime",DataTypeHL7.DTM));
			retVal.Add(new FieldNameAndType("pat.ChartNumber",DataTypeHL7.CX));
			retVal.Add(new FieldNameAndType("pat.FeeSched",DataTypeHL7.ST));
			retVal.Add(new FieldNameAndType("pat.Gender",DataTypeHL7.IS,"0001"));//M,F,U,etc.
			retVal.Add(new FieldNameAndType("pat.HmPhone",DataTypeHL7.XTN));
			retVal.Add(new FieldNameAndType("pat.nameLFM",DataTypeHL7.XPN));
			retVal.Add(new FieldNameAndType("pat.PatNum",DataTypeHL7.CX));
			retVal.Add(new FieldNameAndType("pat.Position",DataTypeHL7.CWE,"0002"));
			retVal.Add(new FieldNameAndType("pat.Race",DataTypeHL7.CWE,"0005"));
			retVal.Add(new FieldNameAndType("pat.SSN",DataTypeHL7.ST));
			retVal.Add(new FieldNameAndType("pat.WkPhone",DataTypeHL7.XTN));
			retVal.Add(new FieldNameAndType("pdfDescription",DataTypeHL7.ST));
			retVal.Add(new FieldNameAndType("pdfDataAsBase64",DataTypeHL7.ST));
			retVal.Add(new FieldNameAndType("proc.DiagnosticCode",DataTypeHL7.CWE,"0051"));
			retVal.Add(new FieldNameAndType("proc.procDateTime",DataTypeHL7.DTM));
			retVal.Add(new FieldNameAndType("proc.ProcFee",DataTypeHL7.CP));
			retVal.Add(new FieldNameAndType("proc.toothSurfRange",DataTypeHL7.CNE,"0340"));
			retVal.Add(new FieldNameAndType("proccode.ProcCode",DataTypeHL7.CNE,"0088"));
			retVal.Add(new FieldNameAndType("prov.provIdNameLFM",DataTypeHL7.XCN));//Provider id table is user defined table and different number depending on what segment it is pulled from.  Example: FT1 Performed By Code table is 0084, 
			retVal.Add(new FieldNameAndType("prov.provIdName",DataTypeHL7.XCN));
			retVal.Add(new FieldNameAndType("separators^~\\&",DataTypeHL7.ST));
			retVal.Add(new FieldNameAndType("sequenceNum",DataTypeHL7.SI));
			return retVal;
		}

		public static DataTypeHL7 GetTypeFromName(string name) {
			List<FieldNameAndType> list=GetFullList();
			for(int i=0;i<list.Count;i++) {
				if(list[i].Name==name) {
					return list[i].DataType;
				}
			}
			throw new ApplicationException("Unknown field name: "+name);
		}

		public static string GetTableNumFromName(string name) {
			List<FieldNameAndType> list=GetFullList();
			for(int i=0;i<list.Count;i++) {
				if(list[i].Name==name) {
					return list[i].TableId;
				}
			}
			throw new ApplicationException("Unknown field name: "+name);
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
		///<summary>Date.  yyyyMMdd.  4,6,or 8</summary>
		DT,
		///<summary>Date/Time.  yyyyMMddHHmmss etc.  Allowed 4,6,8,10,12,14.  Possibly more, but unlikely.</summary>
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
