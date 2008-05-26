using System;
using System.Collections.Generic;
using System.Text;
using OpenDentBusiness;

namespace OpenDental.DataAccess {
	/// <summary>
	/// Informs the client that an IDataObject has been inserted into the database. It contains
	/// the primary key of the inserted object (which may be auto-generated).
	/// </summary>
	public class DtoObjectInsertedAck : DtoServerAck {
		public DtoObjectInsertedAck() {
		}

		public DtoObjectInsertedAck(object[] primaryKeys) {
			this.primaryKeys = primaryKeys;
		}

		private object[] primaryKeys;
		public object[] PrimaryKeys {
			get { return primaryKeys; }
			set { primaryKeys = value; }
		}
	}
}
