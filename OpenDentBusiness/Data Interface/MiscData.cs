using System;
using System.Collections;
using System.Data;
using OpenDentBusiness;
using OpenDental.DataAccess;

namespace OpenDental {

	///<summary>Miscellaneous database functions.</summary>
	internal class MiscData {
		///<summary>Generates a random primary key.  Tests to see if that key already exists before returning it for use.  Currently, the range of returned values is greater than 0, and less than or equal to 16777215, the limit for mysql medium int.  This will eventually change to a max of 18446744073709551615.  Then, the return value would have to be a ulong and the mysql type would have to be bigint.</summary>
		public static int GetKey(string tablename, string field) {
			Random random = new Random();
			double rnd = random.NextDouble();
			while (rnd == 0 || KeyInUse(tablename, field, (int)(rnd * 16777215))) {
				rnd = random.NextDouble();
			}
			return (int)(rnd * 16777215);
		}

		private static bool KeyInUse(string tablename, string field, int keynum) {
			string commandText = "SELECT COUNT(*) FROM " + tablename + " WHERE " + field + "=" + keynum.ToString();
			string value;
			using (IDbConnection connection = DataSettings.GetConnection())
			using (IDbCommand command = connection.CreateCommand()) {
				command.CommandText = commandText;
				connection.Open();
				value = command.ExecuteScalar().ToString();
				connection.Close();
			}

			if (value == "0") {
				return false;
			}
			return true;//already in use
		}
	}
}































