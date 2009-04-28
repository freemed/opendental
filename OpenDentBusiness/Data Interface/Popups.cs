using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Reflection;
using OpenDentBusiness.DataAccess;

namespace OpenDentBusiness{
	///<summary></summary>
	public class Popups {

		///<summary>Gets all Popups for a single patient.  There will actually only be one or zero for now.</summary>
		public static List<Popup> CreateObjects(int patNum) {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetObject<List<Popup>>(MethodBase.GetCurrentMethod(),patNum);
			} 
			string command="SELECT * FROM popup WHERE PatNum = "+POut.PInt(patNum);
			return new List<Popup>(DataObjectFactory<Popup>.CreateObjects(command));
		}

		///<summary></summary>
		public static void WriteObject(Popup popup){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),popup);
				return;
			}
			DataObjectFactory<Popup>.WriteObject(popup);
		}

		///<summary></summary>
		public static void DeleteObject(Popup popup){
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				Meth.GetVoid(MethodBase.GetCurrentMethod(),popup);
				return;
			}
			DataObjectFactory<Popup>.DeleteObject(popup);
		}


	}

	


	


}









