using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Data;
using OpenDentBusiness;
using OpenDental;

namespace OpenDentBusiness {
	/// <summary>Handles database commands to bind the comboboxes comboAnesthMed,comboBoxAnesthetist,comboBoxSurgeon,comboBoxAsst,comboBoxCirc</summary>
	public class bindComboQueries {
		public static DataSet ds = new DataSet();
		public static string cmd;

		/// <summary>
		/// Gets the AnestheticMed from anesthmedsinventory table.
		/// </summary>
		public static DataSet bindAMedName() {
			cmd = "SELECT AnesthMedName FROM anesthmedsinventory ORDER BY AnesthMedName";
			ds=Db.GetDataSet(cmd);
			if(ds != null) {
				ds.Dispose();
				return ds;
			}
			else
				return null;
		}

		/// <summary>
		/// Gets the UserName from userod table.
		/// </summary>
		public static DataSet bindDropDowns() {
			cmd = "SELECT UserName FROM userod where IsHidden = 0";
			ds = Db.GetDataSet(cmd);
			if(ds != null) {
				ds.Dispose();
				return ds;
			}
			else
				return null;
		}

		/// <summary>
		/// Gets the supplier name from supplier table.
		/// </summary>
		public static DataSet bindSuppliers() {
			cmd = "SELECT SupplierName FROM anesthmedsuppliers ORDER BY SupplierName";
			ds = Db.GetDataSet(cmd);
			if(ds != null) {
				ds.Dispose();
				return ds;
			}
			else
				return null;
		}


	}
}
