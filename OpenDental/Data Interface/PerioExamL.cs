using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	///<summary></summary>
	public class PerioExamL{

		///<summary>Used by PerioMeasures when refreshing to organize array.</summary>
		public static int GetExamIndex(int perioExamNum){
			for(int i=0;i<PerioExams.List.Length;i++){
				if(PerioExams.List[i].PerioExamNum==perioExamNum) {
					return i;
				}
			}
			MessageBox.Show("Error. PerioExamNum not in list: "+perioExamNum.ToString());
			return 0;
		}
	
	}
}