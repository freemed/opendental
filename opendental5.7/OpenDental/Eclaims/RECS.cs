using System;
using System.Diagnostics;
using System.IO;
using OpenDentBusiness;

namespace OpenDental.Eclaims
{
	/// <summary>
	/// Summary description for RECS.
	/// </summary>
	public class RECS{
		///<summary></summary>
		public RECS()
		{
			
		}

		///<summary>Returns true if the communications were successful, and false if they failed.</summary>
		public static bool Launch(Clearinghouse clearhouse,int batchNum){
			try{
				//call the client program
				//Process process=
				Process.Start(clearhouse.ClientProgram);
				//process.EnableRaisingEvents=true;
				//process.WaitForExit();
			}
			catch{
				//X12.Rollback(clearhouse,batchNum);//doesn't actually do anything
				return false;
			}
			return true;
		}


	}
}
