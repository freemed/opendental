using System;
using System.Collections;
using System.Data;
using System.Drawing;

namespace OpenDentBusiness{

	///<summary></summary>
	public class CanadianNetwork{
		///<summary>Primary key.</summary>
		public int CanadianNetworkNum;
		///<Summary>This will also be the folder name</Summary>
		public string Abbrev;
		///<Summary></Summary>
		public string Descript;

		///<summary></summary>
		public CanadianNetwork Copy() {
			CanadianNetwork c=new CanadianNetwork();
			c.CanadianNetworkNum=CanadianNetworkNum;
			c.Abbrev=Abbrev;
			c.Descript=Descript;
			return c;
		}
	}

	
}




