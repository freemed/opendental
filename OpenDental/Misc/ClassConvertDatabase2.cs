using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Design;
using System.Drawing.Text;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.Net;
using System.Resources;
using System.Text; 
using System.Windows.Forms;
using OpenDentBusiness;
using CodeBase;

namespace OpenDental{
	//The other file was simply getting too big.  It was bogging down VS speed.
	///<summary></summary>
	public partial class ClassConvertDatabase{
		private System.Version LatestVersion=new Version("5.9.0.0");//This value must be changed when a new conversion is to be triggered.
		
		private void To5_9_0() {
			if(FromVersion<new Version("5.9.0.0")) {
				string command;
				if(DataConnection.DBtype==DatabaseType.MySql) {
					command="DELETE FROM preference WHERE PrefName='RxOrientVert'";
					General.NonQ(command);
					command="DELETE FROM preference WHERE PrefName='RxAdjustRight'";
					General.NonQ(command);
					command="DELETE FROM preference WHERE PrefName='RxAdjustDown'";
					General.NonQ(command);
					command="DELETE FROM preference WHERE PrefName='RxGeneric'";
					General.NonQ(command);
					command="ALTER TABLE rxdef ADD IsControlled tinyint NOT NULL";
					General.NonQ(command);
					command="ALTER TABLE rxpat ADD IsControlled tinyint NOT NULL";
					General.NonQ(command);


				} 
				else {//oracle
					
				}
				command="UPDATE preference SET ValueString = '5.9.0.0' WHERE PrefName = 'DataBaseVersion'";
				General.NonQ(command);
			}
			//To5_9_?();
		}


		/*For 5.9:
		 * ALTER TABLE schedule ADD INDEX (EmployeeNum)
ALTER TABLE schedule ADD INDEX (ProvNum)
ALTER TABLE schedule ADD INDEX (SchedDate)*/


	}

}