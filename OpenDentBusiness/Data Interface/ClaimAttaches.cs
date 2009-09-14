using System;
using System.Collections;
using System.Data;
using System.Reflection;

namespace OpenDentBusiness{
	///<summary></summary>
	public class ClaimAttaches{

		public static long Insert(ClaimAttach attach) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				attach.ClaimAttachNum=Meth.GetInt(MethodBase.GetCurrentMethod(),attach);
				return attach.ClaimAttachNum;
			}
			if(PrefC.RandomKeys) {
				attach.ClaimAttachNum=ReplicationServers.GetKey("claimattach","ClaimAttachNum");
			}
			string command= "INSERT INTO claimattach (";
			if(PrefC.RandomKeys) {
				command+="ClaimAttachNum,";
			}
			command+="ClaimNum, DisplayedFileName, ActualFileName) VALUES(";
			if(PrefC.RandomKeys) {
				command+="'"+POut.PLong(attach.ClaimAttachNum)+"', ";
			}
			command+=
				 "'"+POut.PLong(attach.ClaimNum)+"', "
				+"'"+POut.PString(attach.DisplayedFileName)+"', "
				+"'"+POut.PString(attach.ActualFileName)+"')";
			if(PrefC.RandomKeys) {
				Db.NonQ(command);
			}
			else {
				attach.ClaimAttachNum=Db.NonQ(command,true);
			}
			return attach.ClaimAttachNum;
		}


	}

	


}









