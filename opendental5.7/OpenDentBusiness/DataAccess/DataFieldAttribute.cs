using System;
using System.Collections.Generic;
using System.Text;

namespace OpenDental.DataAccess {
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
	public class DataFieldAttribute : Attribute {
		public DataFieldAttribute(string databaseName) {
			this.databaseName = databaseName;
		}

		public DataFieldAttribute(string databaseName, bool primaryKey, bool autoNumber) {
			this.autoNumber = autoNumber;
			this.databaseName = databaseName;
			this.primaryKey = primaryKey;
		}

		public DataFieldAttribute(string databaseName, bool primaryKey, bool autoNumber, bool readOnly, bool writeOnly) {
			this.databaseName = databaseName;
			this.primaryKey = primaryKey;
			this.readOnly = readOnly;
			this.writeOnly = writeOnly;
		}

		private bool autoNumber;
		public bool AutoNumber {
			get { return autoNumber; }
			set { autoNumber = value; }
		}

		private bool primaryKey;
		public bool PrimaryKey {
			get { return primaryKey; }
			set { primaryKey = value; }
		}

		private bool readOnly;
		public bool ReadOnly {
			get { return readOnly; }
			set { readOnly = value; }
		}

		private bool writeOnly;
		public bool WriteOnly {
			get { return writeOnly; }
			set { writeOnly = value; }
		}

		private string databaseName;
		public string DatabaseName {
			get { return databaseName; }
			set { databaseName = value; }
		}
	}
}
