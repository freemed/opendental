using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using OpenDentBusiness;
using CodeBase;

namespace OpenDental.Imaging {
	public static class FileStoreSettings {
		///<summary>If set, this path will override all other paths.</summary>
		public static string LocalAtoZpath;

		public static string GetPreferredImagePath {
			get {
				if(!PrefC.UsingAtoZfolder) {
					return null;
				}
				if(LocalAtoZpath!=""){
					return LocalAtoZpath;
				}
				//use this to handle possible multiple paths separated by semicolons.
				return ElucidatePreferredImagePath(PrefC.GetString("DocPath"));
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
