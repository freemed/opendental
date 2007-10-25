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

		///<summary>Gets all Popups for a single patient.  There will only be one for now.</summary>
		public static List<Popup> Refresh(int patNum) {
			string command="SELECT * FROM popup WHERE PatNum = "+POut.PInt(patNum);
			return new List<Popup>(DataObjectFactory<Popup>.CreateObjects(command));
		}

		///<summary></summary>
		public static void Update(Popup popup){
			try{
				DataObjectFactory<Popup>.WriteObject(popup);
			}
			catch(Exception e){
				MessageBox.Show(e.Message);
			}
		}

		///<summary></summary>
		public static void Insert(Popup popup){
			try {
				DataObjectFactory<Popup>.WriteObject(popup);
			}
			catch(Exception e) {
				MessageBox.Show(e.Message);
			}
		}

		///<summary></summary>
		public static void Delete(Popup popup){
			try {
				DataObjectFactory<Popup>.DeleteObject(popup);
			}
			catch(Exception e) {
				MessageBox.Show(e.Message);
			}
		}


	}

	


	


}









