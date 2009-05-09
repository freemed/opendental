using System;
using System.Collections;
using System.Data;
using System.Reflection;
using MySql.Data.MySqlClient;

namespace OpenDentBusiness {
	/// <summary>Handles database commands to bind the comboboxes comboAnesthMed,comboBoxAnesthetist,comboBoxSurgeon,comboBoxAsst,comboBoxCirc</summary>
	public class AnestheticQueries {
		public static DataSet ds = new DataSet();
		public static string cmd;

		/// <summary>
		/// Gets the AnestheticMed from anesthmedsinventory table.
		/// </summary>
		public static DataSet bindAMedName() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetDS(MethodBase.GetCurrentMethod());
			}
			cmd = "SELECT AnesthMedName FROM anesthmedsinventory ORDER BY AnesthMedName";
			ds=Db.GetDataSet(cmd);
			if(ds != null) {
				ds.Dispose();
				return ds;
			}
			return null;
		}

		/// <summary>
		/// Gets the UserName from userod table.
		/// </summary>
		public static DataSet bindDropDowns() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetDS(MethodBase.GetCurrentMethod());
			}
			cmd = "SELECT UserName FROM userod where IsHidden = 0";
			ds = Db.GetDataSet(cmd);
			if(ds != null) {
				ds.Dispose();
				return ds;
			}
			return null;
		}

		/// <summary>
		/// Gets the supplier name from supplier table.
		/// </summary>
		public static DataSet bindSuppliers() {
			if(RemotingClient.RemotingRole==RemotingRole.ClientWeb) {
				return Meth.GetDS(MethodBase.GetCurrentMethod());
			}
			cmd = "SELECT SupplierName FROM anesthmedsuppliers ORDER BY SupplierName";
			ds = Db.GetDataSet(cmd);
			if(ds != null) {
				ds.Dispose();
				return ds;
			}
			return null;
		}


	}
}
