using System;

namespace OpenDentBusiness{
	
	///<summary>One row can hold up to six measurements for one tooth, all of the same type.  Always attached to a perioexam.</summary>
	public class PerioMeasure{
		///<summary>Primary key.</summary>
		public int PerioMeasureNum;
		///<summary>FK to perioexam.PerioExamNum.</summary>
		public int PerioExamNum;
		///<summary>Enum:PerioSequenceType  eg probing, mobility, recession, etc.</summary>
		public PerioSequenceType SequenceType;
		///<summary>Valid values are 1-32. Every measurement must be associated with a tooth.</summary>
		public int IntTooth;
		///<summary>This is used when the measurement does not apply to a surface(mobility and skiptooth).  Valid values for all surfaces are 0 through 19, or -1 to represent no measurement taken.</summary>
		public int ToothValue;
		///<summary>.</summary>
		public int MBvalue;
		///<summary>.</summary>
		public int Bvalue;
		///<summary>.</summary>
		public int DBvalue;
		///<summary>.</summary>
		public int MLvalue;
		///<summary>.</summary>
		public int Lvalue;
		///<summary>.</summary>
		public int DLvalue;

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















