using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using OpenDentalWebService;

namespace xCrudGenerator {
	public class CrudGenDataInterfaceWebService {

		private static string GetSname(string typeClassName){
			string Sname=typeClassName;
			if(typeClassName=="Etrans") {
				return "Etranss";
			}
			//if(typeClassName=="RegistrationKey") {
			//	return "RegistrationKeys";
			//}
			if(typeClassName=="Language") {
				return "Lans";
			}
			if(Sname.EndsWith("s")){
				Sname=Sname+"es";
			}
			else if(Sname.EndsWith("ch")){
				Sname=Sname+"es";
			}
			else if(Sname.EndsWith("ay")) {
				Sname=Sname+"s";
			}
			else if(Sname.EndsWith("ey")) {//eg key
				Sname=Sname+"s";
			}
			else if(Sname.EndsWith("y")) {
				Sname=Sname.TrimEnd('y')+"ies";
			}
			else {
				Sname=Sname+"s";
			}
			return Sname;
		}

		///<summary>Creates the WebService Data Interface "s" classes for new tables, complete with typical stubs.  Asks user first.</summary>
		public static void Create(Type typeClass) {
			string Sname=GetSname(typeClass.Name);
			string fileName=@"..\..\..\OpenDentalWebService\Data Interface\"+Sname+".cs";
			if(File.Exists(fileName)) {
				return;
			}
			if(MessageBox.Show("Create stub for "+fileName+"?","",MessageBoxButtons.YesNo)!=DialogResult.Yes) {
				return;
			}
			string snippet=GetEntireSclass(Sname);
			File.WriteAllText(fileName,snippet);
			MessageBox.Show(fileName+" has been created.  Be sure to add it to the project and to SVN");
		}

		private static string GetEntireSclass(string Sname){
			string str=@"using System;
using System.Collections.Generic;
using System.Data;

namespace OpenDentalWebService{
	///<summary></summary>
	public class "+Sname+@"{


	}
}";
			return str;			
		}
	}
}
