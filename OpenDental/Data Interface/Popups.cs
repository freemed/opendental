using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.DataAccess;

namespace OpenDental{
	///<summary></summary>
	public class Popups {

		///<summary>Gets all Popups for a single patient.  There will actually only be one or zero for now.</summary>
		public static List<Popup> CreateObjects(int patNum) {
			string command="SELECT * FROM popup WHERE PatNum = "+POut.PInt(patNum);
			return new List<Popup>(DataObjectFactory<Popup>.CreateObjects(command));
		}

		///<summary></summary>
		public static void WriteObject(Popup popup){
			DataObjectFactory<Popup>.WriteObject(popup);
		}

		///<summary></summary>
		public static void DeleteObject(Popup popup){
			DataObjectFactory<Popup>.DeleteObject(popup);
		}


	}

	


	


}









