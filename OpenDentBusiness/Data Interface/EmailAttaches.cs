using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace OpenDentBusiness{
	///<summary></summary>
	public class EmailAttaches{

		public static long Insert(EmailAttach attach) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				attach.EmailAttachNum=Meth.GetLong(MethodBase.GetCurrentMethod(),attach);
				return attach.EmailAttachNum;
			}
			return Crud.EmailAttachCrud.Insert(attach);
		}

		public static List<EmailAttach> GetForEmail(long emailMessageNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<EmailAttach>>(MethodBase.GetCurrentMethod(),emailMessageNum);
			}
			string command="SELECT * FROM emailattach WHERE EmailMessageNum="+POut.Long(emailMessageNum);
			return Crud.EmailAttachCrud.SelectMany(command);
		}

	}

	


}









