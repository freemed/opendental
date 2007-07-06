using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace OpenDental.DataAccess {
	public class DataFieldInfo {
		public DataFieldInfo(FieldInfo field, DataFieldAttribute attribute) {
			this.field = field;
			this.autoNumber = attribute.AutoNumber;
			this.primaryKey = attribute.PrimaryKey;
			this.databaseName = attribute.DatabaseName;
			this.readOnly = attribute.ReadOnly;
			this.writeOnly = attribute.WriteOnly;
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

		private FieldInfo field;
		public FieldInfo Field {
			get { return field; }
		}
	}
}
