using System;
using System.Collections.Generic;
using System.Text;

namespace OpenDental.DataAccess {
	[Flags]
	public enum DataFieldMask {
		// List Identity fields
		PrimaryKey = 0x002,

		// List Data fields
		Data = 0x004,

		// List All fields
		All = PrimaryKey | Data
	}
}
