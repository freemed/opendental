using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	///<summary></summary>
	public class ClearinghouseL {

		///<summary>Returns the clearinghouse specified by the given num.</summary>
		public static Clearinghouse GetClearinghouse(long clearinghouseNum) {
			for(int i=0;i<Clearinghouses.Listt.Length;i++){
				if(Clearinghouses.Listt[i].ClearinghouseNum==clearinghouseNum) {
					return Clearinghouses.Listt[i];
				}
			}
			MessageBox.Show("Error. Could not locate Clearinghouse.");
			return null;
		}

		///<summary>Gets the index of this clearinghouse within List</summary>
		public static int GetIndex(long clearinghouseNum) {
			for(int i=0;i<Clearinghouses.Listt.Length;i++) {
				if(Clearinghouses.Listt[i].ClearinghouseNum==clearinghouseNum) {
					return i;
				}
			}
			MessageBox.Show("Clearinghouses.GetIndex failed.");
			return -1;
		}

		///<summary></summary>
		public static string GetDescript(long clearinghouseNum) {
			if(clearinghouseNum==0) {
				return "";
			}
			return GetClearinghouse(clearinghouseNum).Description;
		}

	}
}