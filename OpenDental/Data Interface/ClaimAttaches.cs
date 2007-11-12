using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	///<summary></summary>
	public class ClaimAttaches{

		public static void Insert(ClaimAttach attach) {
			if(PrefB.RandomKeys) {
				attach.ClaimAttachNum=MiscData.GetKey("claimattach","ClaimAttachNum");
			}
			string command= "INSERT INTO claimattach (";
			if(PrefB.RandomKeys) {
				command+="ClaimAttachNum,";
			}
			command+="ClaimNum, DisplayedFileName, ActualFileName) VALUES(";
			if(PrefB.RandomKeys) {
				command+="'"+POut.PInt(attach.ClaimAttachNum)+"', ";
			}
			command+=
				 "'"+POut.PInt(attach.ClaimNum)+"', "
				+"'"+POut.PString(attach.DisplayedFileName)+"', "
				+"'"+POut.PString(attach.ActualFileName)+"')";
			if(PrefB.RandomKeys) {
				General.NonQ(command);
			}
			else {
				attach.ClaimAttachNum=General.NonQ(command,true);
			}
		}


	}

	


}









