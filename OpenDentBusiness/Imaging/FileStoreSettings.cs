using System;
using System.Collections.Generic;
using System.Text;

namespace OpenDental.Imaging {
	public static class FileStoreSettings {
		public static string GetPreferredImagePath {
#warning Hard-coded path
			get { return @"C:\OpenDentImages\"; }
		}
	}
}
