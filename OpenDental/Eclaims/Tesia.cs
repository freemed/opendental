using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental.Eclaims
{
	/// <summary></summary>
	public class Tesia{
		///<summary></summary>
		public Tesia()
		{
			
		}

		///<summary>Returns true if the communications were successful, and false if they failed. If they failed, a rollback will happen automatically by deleting the previously created X12 file. The batchnum is supplied for the possible rollback.</summary>
		public static bool Launch(Clearinghouse clearhouse,int batchNum){
			try{
				//
			}
			catch(Exception e) {
				MessageBox.Show(e.Message);
				X12.Rollback(clearhouse,batchNum);
				return false;
			}
			return true;
		}

		


	}
}
