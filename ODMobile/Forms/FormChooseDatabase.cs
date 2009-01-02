using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlServerCe;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace OpenDentMobile {
	public partial class FormChooseDatabase:Form {
		public FormChooseDatabase() {
			InitializeComponent();
		}

		///<summary>Only called at startup if this dialog is not supposed to be shown.  Must call GetConfig first.</summary>
		public static bool TryToConnect(){
			//DataConnection dcon=new DataConnection();
			try{
				Dcon.SetDb("OpenDental");//This will fail if the database does not yet exist.
				return true;
			}
			catch{
				return false;
			}
		}





	}
}