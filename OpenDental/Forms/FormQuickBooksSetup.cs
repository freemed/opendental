using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormQuickBooksSetup:Form {
		public Program ProgramCur;

		public FormQuickBooksSetup() {
			InitializeComponent();
			Lan.F(this);
		}

		private void butOK_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}
	}
}



/* Code to go into ConvertDatabase2.cs when ready for users to see QuickBooks within Program Links:
 * //QuickBooks---------------------------------------------------------------------------
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="INSERT INTO program (ProgName,ProgDesc,Enabled,Path,CommandLine,Note"
						+") VALUES("
						+"'QuickBooks', "
						+"'QuickBooks from quickbooks.intuit.com', "
						+"'0', "
						+"'', "
						+"'', "
						+"'')";
					Db.NonQ(command);
				}
				else {//oracle
					command="INSERT INTO program (ProgramNum,ProgName,ProgDesc,Enabled,Path,CommandLine,Note"
						+") VALUES("
						+"(SELECT MAX(ProgramNum)+1 FROM program),"
						+"'QuickBooks', "
						+"'QuickBooks from quickbooks.intuit.com', "
						+"'0', "
						+"'', "
						+"'', "
						+"'')";
					Db.NonQ(command,true);
				}*/