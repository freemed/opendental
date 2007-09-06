using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using OpenDentBusiness;
using CodeBase;

namespace OpenDental.Imaging {
	public static class FileStoreSettings {
		public static string GetPreferredImagePath {
#warning Hard-coded path
			get { //return @"C:\OpenDentImages\"; }
				if(!PrefB.UsingAtoZfolder) {
					return null;
				}
				return ElucidatePreferredImagePath(PrefB.GetString("DocPath"));
			}
		}

		private static string ElucidatePreferredImagePath(string documentPaths) {
			string[] preferredPathsByOrder=documentPaths.Split(new char[] { ';' });
			for(int i=0;i<preferredPathsByOrder.Length;i++) {
				string path=preferredPathsByOrder[i];
				string tryPath=ODFileUtils.CombinePaths(path,"A");
				if(Directory.Exists(tryPath)) {
					return path;
				}
			}
			return null;
		}

		


	}
}
