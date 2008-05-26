using System;
using System.Collections;

namespace OpenDentBusiness{
	
	/// <summary>Used to track missing teeth, primary teeth, and movements</summary>
	public class ToothInitial{
		///<summary>Primary key.</summary>
		public int ToothInitialNum;
		///<summary>FK to patient.PatNum</summary>
		public int PatNum;
		///<summary>1-32 or A-Z. Supernumeraries not supported here yet.</summary>
		public string ToothNum;
		///<summary>Enum:ToothInitialType</summary>
		public ToothInitialType InitialType;
		///<summary>Shift in mm, or rotation / tipping in degrees.</summary>
		public float Movement;

		///<summary></summary>
		public ToothInitial Copy(){
			ToothInitial t=new ToothInitial();
			t.ToothInitialNum=ToothInitialNum;
			t.PatNum=PatNum;
			t.ToothNum=ToothNum;
			t.InitialType=InitialType;
			t.Movement=Movement;
			return t;
		}


	}

	




}

















