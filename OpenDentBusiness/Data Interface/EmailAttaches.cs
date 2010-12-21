using System;
using System.Collections;
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


	}

	


}









