using System;

namespace OpenDentBusiness{
	
	///<summary>One row can hold up to six measurements for one tooth, all of the same type.  Always attached to a perioexam.</summary>
	public class PerioMeasure{
		///<summary>Primary key.</summary>
		public long PerioMeasureNum;
		///<summary>FK to perioexam.PerioExamNum.</summary>
		public long PerioExamNum;
		///<summary>Enum:PerioSequenceType  eg probing, mobility, recession, etc.</summary>
		public PerioSequenceType SequenceType;
		///<summary>Valid values are 1-32. Every measurement must be associated with a tooth.</summary>
		public long IntTooth;
		///<summary>This is used when the measurement does not apply to a surface(mobility and skiptooth).  Valid values for all surfaces are 0 through 19, or -1 to represent no measurement taken.</summary>
		public long ToothValue;
		///<summary>.</summary>
		public long MBvalue;
		///<summary>.</summary>
		public long Bvalue;
		///<summary>.</summary>
		public long DBvalue;
		///<summary>.</summary>
		public long MLvalue;
		///<summary>.</summary>
		public long Lvalue;
		///<summary>.</summary>
		public long DLvalue;

		public PerioMeasure Copy(){
			PerioMeasure p=new PerioMeasure();
			p.PerioMeasureNum=PerioMeasureNum;
			p.PerioExamNum=PerioExamNum;
			p.SequenceType=SequenceType;
			p.IntTooth=IntTooth;
			p.ToothValue=ToothValue;
			p.MBvalue=MBvalue;
			p.Bvalue=Bvalue;
			p.DBvalue=DBvalue;
			p.MLvalue=MLvalue;
			p.Lvalue=Lvalue;
			p.DLvalue=DLvalue;
			return p;
		}



	}

	
	

}















