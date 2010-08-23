using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.CodeDom;
using System.Windows.Forms;

namespace CodeBase {
	public class ODFileUtils {

		///<summary>Removes a trailing path separator from the given string if one exists.</summary>
		public static string RemoveTrailingSeparators(string path){
			while(path!=null && path.Length>0 && (path[path.Length-1]=='\\' || path[path.Length-1]=='/')) {
				path=path.Substring(0,path.Length-1);
			}
			return path;
		}

		public static string CombinePaths(string path1,string path2) {
			return CombinePaths(new string[] { path1,path2 });
		}

		public static string CombinePaths(string path1,string path2,string path3) {
			return CombinePaths(new string[] { path1,path2,path3 });
		}

		public static string CombinePaths(string path1,string path2,string path3,string path4) {
			return CombinePaths(new string[] { path1,path2,path3,path4 });
		}

		///<summary>OS independent path cominations. Ensures that each of the given path pieces are separated by the correct path separator for the current operating system. There is guaranteed not to be a trailing path separator at the end of the returned string (to accomodate file paths), unless the last specified path piece in the array is the empty string.</summary>
		public static string CombinePaths(string[] paths){
			string finalPath="";
			for(int i=0;i<paths.Length;i++){
				string path=RemoveTrailingSeparators(paths[i]);
				//Add an appropriate slash to divide the path peices, but do not use a trailing slash on the last piece.
				if(i<paths.Length-1){
					if(path!=null && path.Length>0){
						path=path+Path.DirectorySeparatorChar;
					}
				}
				finalPath=finalPath+path;
			}
			return finalPath;
		}

		///<summary>Returns the directory in which the program executable rests. To get the full path for the program executable, use Applicaiton.ExecutablePath.</summary>
		public static string GetProgramDirectory(){
				int endPos=Application.ExecutablePath.LastIndexOf(Path.DirectorySeparatorChar);
				return Application.ExecutablePath.Substring(0,endPos+1);
		}

		///<summary>Creates a new randomly named file in the given directory path with the given extension and returns the full path to the new file.</summary>
		public static string CreateRandomFile(string dir,string ext){
			if(ext.Length>0 && ext[0]!='.'){
				ext='.'+ext;
			}
			bool fileCreated=false;
			string filePath="";
			const string randChrs="ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
			Random rand=new Random();
			do{
				string fileName="";
				for(int i=0;i<6;i++){
					fileName+=randChrs[rand.Next(0,randChrs.Length-1)];
				}
				fileName+=DateTime.Now.ToString("yyyyMMddhhmmss");
				filePath=CombinePaths(dir,fileName+ext);
				FileStream fs=null;
				try{
					fs=File.Create(filePath);
					fs.Dispose();
					fileCreated=true;
				}catch{
				}
			}while(!fileCreated);
			return filePath;
		}

	}
}
