using System;
using System.Collections.Generic;
using System.Text;

namespace OpenDentMobile {
	///<summary>Unknown,Yes, or No.</summary>
	public enum YN{
		///<summary>0</summary>
		Unknown,
		///<summary>1</summary>
		Yes,
		///<summary>2</summary>
		No
	}

	///<summary></summary>
	public enum PatientStatus{
		///<summary>0</summary>
		Patient,
		///<summary>1</summary>
		NonPatient,
		///<summary>2</summary>
		Inactive,
		///<summary>3</summary>
		Archived,
		///<summary>4</summary>
		Deleted,
		///<summary>5</summary>
		Deceased
	}

	///<summary></summary>
	public enum PatientGender{
		///<summary>0</summary>
		Male,
		///<summary>1</summary>
		Female,
		///<summary>2- This is not a joke. Required by HIPAA for privacy.</summary>
		Unknown
	}

	///<summary></summary>
	public enum PatientPosition{
		///<summary>0</summary>
		Single,
		///<summary>1</summary>
		Married,
		///<summary>2</summary>
		Child,
		///<summary>3</summary>
		Widowed,
		///<summary>4</summary>
		Divorced
	}
}
