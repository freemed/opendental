using System;
using System.Collections.Generic;
using System.Text;

namespace OpenDentBusiness.Imaging {
	public enum ApplySettings {
		NONE = 0x00,
		ALL = 0xFF,
		CROP = 0x01,
		FLIP = 0x02,
		ROTATE = 0x04,
		COLORFUNCTION = 0x08,
	};
}
