using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace Crud {
	public partial class Form1:Form {
		public Form1() {
			InitializeComponent();
		}

		private void Form1_Load(object sender,EventArgs e) {

		}

		private void butRun_Click(object sender,EventArgs e) {
			string crudDir=@"..\..\..\OpenDentBusiness\Crud";
			if(!Directory.Exists(crudDir)) {
				MessageBox.Show(crudDir+" is an invalid path.");
				Application.Exit();
				return;
			}
			string[] files=Directory.GetFiles(crudDir);
			for(int i=0;i<files.Length;i++) {
				File.Delete(files[i]);
			}
			Type typeTableBase=typeof(TableBase);
			Assembly assembly=Assembly.GetAssembly(typeTableBase);
			foreach(Type type in assembly.GetTypes()){
				if(type.BaseType!=typeTableBase){
					continue;
				}
				File.Create(Path.Combine(crudDir,type.Name+".cs"));
			}







			MessageBox.Show("Done");
			Application.Exit();
		}
	}
}
