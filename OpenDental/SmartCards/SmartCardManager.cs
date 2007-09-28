using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApplication1 {
	public static class SmartCardManager {
		public static ISmartCardManager Load() {
			if(Environment.OSVersion.Platform == PlatformID.Win32NT) {
				return new WindowsSmartCardManager();
			}
			else {
				throw new NotSupportedException();
			}
		}
	}
}
