using System;
using System.Collections.Generic;
using System.Text;

namespace OpenDentMobile {
	///<summary></summary>
	public class PatientSelectedEventArgs{
		private int patNum;
		private string patName;

		///<summary></summary>
		public PatientSelectedEventArgs(int patNum,string patName){
			this.patNum=patNum;
			this.patName=patName;
		}

		///<summary></summary>
		public int PatNum{
			get{ 
				return patNum;
			}
		}

		///<summary></summary>
		public string PatName {
			get {
				return patName;
			}
		}


	}

	///<summary></summary>
	public delegate void PatientSelectedEventHandler(object sender,PatientSelectedEventArgs e);
}
