using System;
using System.Collections;
using System.Drawing;
using System.Xml.Serialization;

namespace OpenDentBusiness{
	
	///<summary>A list setup ahead of time with all the procedure codes used by the office.  Every procedurelog entry which is attached to a patient is also linked to this table.</summary>
	public class ProcedureCode{
		///<summary>Primary Key.  This happened in version 4.8.7.</summary>
		[XmlIgnore]
		public int CodeNum;
		///<summary>Was Primary key, but now CodeNum is primary key.  Can hold dental codes, medical codes, custom codes, etc.</summary>
		public string ProcCode;
		///<summary>The main description.</summary>
		public string Descript;
		///<summary>Abbreviated description.</summary>
		public string AbbrDesc;
		///<summary>X's and /'s describe Dr's time and assistant's time in the same increments as the user has set.</summary>
		public string ProcTime;
		///<summary>FK to definition.DefNum.  The category that this code will be found under in the search window.  Has nothing to do with insurance categories.</summary>
		[XmlIgnore]
		public int ProcCat;
		///<summary>Enum:TreatmentArea</summary>
		public TreatmentArea TreatArea;
		///<summary>No longer used. Extraction paint type is used instead to show missing teeth.</summary>
		[XmlIgnore]
		public bool RemoveToothOld;
		///<summary>Triggers recall in 6 months or as defined.</summary>
		public bool SetRecall;
		///<summary>If true, do not usually bill this procedure to insurance.</summary>
		public bool NoBillIns;
		///<summary>True if Crown,Bridge,Denture, or RPD. Forces user to enter Initial or Replacement and Date.</summary>
		public bool IsProsth;
		///<summary>The default procedure note to copy when marking complete.</summary>
		[XmlIgnore]
		public string DefaultNote;
		///<summary>Identifies hygiene procedures so that the correct provider can be selected.</summary>
		public bool IsHygiene;
		///<summary>No longer used.</summary>
		[XmlIgnore]
		public int GTypeNum;
		///<summary>For Medicaid.  There may be more later.</summary>
		[XmlIgnore]
		public string AlternateCode1;
		///<summary>FK to procedurecode.ProcCode.  The actual medical code that is being referenced must be setup first.  Anytime a procedure it added, this medical code will also be added to that procedure.  The user can change it in procedurelog.</summary>
		[XmlIgnore]
		public string MedicalCode;
		///<summary>Used by some offices even though no user interface built yet.  SalesTaxPercentage has been added to the preference table to store the amount of sales tax to apply as an adjustment attached to a procedurelog entry.</summary>
		[XmlIgnore]
		public bool IsTaxed;
		///<summary>Enum:ToothPaintingType</summary>
		public ToothPaintingType PaintType;
		///<summary>If set to anything but 0, then this will override the graphic color for all procedures of this code, regardless of the status.</summary>
		[XmlIgnore]
		public Color GraphicColor;
		///<summary>When creating treatment plans, this description will be used instead of the technical description.</summary>
		[XmlIgnore]
		public string LaymanTerm;
		///<summary>Only used in Canada.  Set to true if this procedure code is only used as an adjunct to track the lab fee.</summary>
		[XmlIgnore]
		public bool IsCanadianLab;
		///<summary>This is true if this procedure code existed before ADA code distribution changed at version 4.8, false otherwise.</summary>
		[XmlIgnore]
		public bool PreExisting;
		///<summary>Support for Base Units for a Code (like anesthesia)</summary>
		[XmlIgnore]
		public int BaseUnits;
		///<summary>FK to procedurecode.ProcCode.  Used for posterior composites because insurance substitutes the amalgam code when figuring the coverage.</summary>
		[XmlIgnore]
		public string SubstitutionCode;
		///<Summary>Enum:SubstitutionCondition Used so that posterior composites only substitute if tooth is molar.  Ins usually pays for premolar composites.</Summary>
		[XmlIgnore]
		public SubstitutionCondition SubstOnlyIf;

		
		///<summary>Not a database column.  Only used for xml import function.</summary>
		private string procCatDescript;

		public ProcedureCode(){
			ProcTime="/X/";
			//procCode.ProcCat=DefB.Short[(int)DefCat.ProcCodeCats][0].DefNum;
			GraphicColor=Color.FromArgb(0);
		}

		//[XmlElement(DataType="string",ElementName="ProcCatDescript")]
		public string ProcCatDescript{
			get{
				if(ProcCat==0){//only used in xml import. We have an incomplete object.
					return procCatDescript;
				}
				return DefB.GetName(DefCat.ProcCodeCats,ProcCat);
			}
			set{
				procCatDescript=value;
			}
		}

		///<summary>Returns a copy of this Procedurecode.</summary>
		public ProcedureCode Copy(){
			ProcedureCode p=new ProcedureCode();
			p.CodeNum=CodeNum;
			p.ProcCode=ProcCode;
			p.Descript=Descript;
			p.AbbrDesc=AbbrDesc;
			p.ProcTime=ProcTime;
			p.ProcCat=ProcCat;
			p.TreatArea=TreatArea;
			//p.RemoveTooth=RemoveTooth;
			p.SetRecall=SetRecall;
			p.NoBillIns=NoBillIns;
			p.IsProsth=IsProsth;
			p.DefaultNote=DefaultNote;
			p.IsHygiene=IsHygiene;
			p.GTypeNum=GTypeNum;
			p.AlternateCode1=AlternateCode1;
			p.MedicalCode=MedicalCode;
			p.IsTaxed=IsTaxed;
			p.PaintType=PaintType;
			p.GraphicColor=GraphicColor;
			p.LaymanTerm=LaymanTerm;
			p.IsCanadianLab=IsCanadianLab;
			p.PreExisting=PreExisting;
			p.BaseUnits=BaseUnits;
			p.SubstitutionCode=SubstitutionCode;
			p.SubstOnlyIf=SubstOnlyIf;
			return p;
		}


	}

	
	
	


}










