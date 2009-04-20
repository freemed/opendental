using System;
using System.Collections;
using System.Data;

namespace OpenDentBusiness{
	///<summary></summary>
	public class ClaimAttaches{

		public static void Insert(ClaimAttach attach) {
			if(PrefC.RandomKeys) {
				attach.ClaimAttachNum=MiscData.GetKey("claimattach","ClaimAttachNum");
			}
			string command= "INSERT INTO claimattach (";
			if(PrefC.RandomKeys) {
				command+="ClaimAttachNum,";
			}
			command+="ClaimNum, DisplayedFileName, ActualFileName) VALUES(";
			if(PrefC.RandomKeys) {
				command+="'"+POut.PInt(attach.ClaimAttachNum)+"', ";
			}
			command+=
				 "'"+POut.PInt(attach.ClaimNum)+"', "
				+"'"+POut.PString(attach.DisplayedFileName)+"', "
				+"'"+POut.PString(attach.ActualFileName)+"')";
			if(PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				attach.ClaimAttachNum=Db.NonQ(command,true);
			}
		}


	}

	


}









