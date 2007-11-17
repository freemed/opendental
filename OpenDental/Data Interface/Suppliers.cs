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
	public class Suppliers {

		///<summary>Gets all Suppliers.</summary>
		public static List<Supplier> CreateObjects() {
			string command="SELECT * FROM supplier ORDER BY Name";
			return new List<Supplier>(DataObjectFactory<Supplier>.CreateObjects(command));
		}

		///<summary></summary>
		public static void WriteObject(Supplier supp){
			DataObjectFactory<Supplier>.WriteObject(supp);
		}

		///<summary>Surround with try-catch.</summary>
		public static void DeleteObject(Supplier supp){
			//validate that not already in use.

			DataObjectFactory<Supplier>.DeleteObject(supp);
		}


	}

	


	


}









