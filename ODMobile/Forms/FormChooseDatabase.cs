using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlServerCe;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using System.Windows.Forms;

namespace OpenDentMobile {
	public partial class FormChooseDatabase:Form {
		public bool CloseApplication;

		public FormChooseDatabase() {
			InitializeComponent();
		}

		///<summary>Only called at startup if this dialog is not supposed to be shown.  Must call GetConfig first.</summary>
		public static bool TryToConnect(string dbName){
			//DataConnection dcon=new DataConnection();
			try{
				Dcon.SetDb(dbName);//This will fail if the database does not yet exist.
				return true;
			}
			catch{
				return false;
			}
		}

		private void FormChooseDatabase_Load(object sender,EventArgs e) {
			//this form will only load if "OpenDentalConfig.xml" is detected in the application directory.
			System.Xml.XmlDocument doc=new XmlDocument();
			string strAppDir=Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
			doc.Load(Path.Combine(strAppDir,"OpenDentalConfig.xml"));
			XmlNode mainNode=doc.ChildNodes[1];//node 0 is the xml declaration
			XmlNodeList mainNodeList=mainNode.ChildNodes;
			listDb.Items.Clear();
			for(int i=0;i<mainNodeList.Count;i++){
				listDb.Items.Add(mainNodeList[i].InnerText);
			}
		}

		private void listDb_SelectedIndexChanged(object sender,EventArgs e) {
			Cursor.Current=Cursors.WaitCursor;
			if(!FormChooseDatabase.TryToConnect(listDb.SelectedItem.ToString())){
				if(!MsgBox.Show("Database does not exist and will now be created.",true)){
					Cursor.Current=Cursors.Default;
					MsgBox.Show("Application will now exit.");
					CloseApplication=true;
					DialogResult=DialogResult.Cancel;
					return;
				}
				try{
					ClassConvertDatabase.CreateNewDatabase(listDb.SelectedItem.ToString());
				}
				catch{
					Cursor.Current=Cursors.Default;
					MessageBox.Show("Please install SQL Server Compact 3.5 first.");
					CloseApplication=true;
					DialogResult=DialogResult.Cancel;
					return;
				}
				if(!FormChooseDatabase.TryToConnect(listDb.SelectedItem.ToString())){
					Cursor.Current=Cursors.Default;
					MessageBox.Show("Could not connect to database.  Application will now exit.");
					CloseApplication=true;
					DialogResult=DialogResult.Cancel;
					return;
				}
				Cursor.Current=Cursors.Default;
				MessageBox.Show("Database has been created.  Please do a full sync from the workstation, then reopen the program.");
				CloseApplication=true;
				DialogResult=DialogResult.Cancel;
				return;
			}
			Cursor.Current=Cursors.Default;
			DialogResult=DialogResult.OK;
		}

		private void FormChooseDatabase_Closing(object sender,CancelEventArgs e) {
			if(listDb.SelectedIndex==-1){
				if(!MsgBox.Show("Please select a database.  Click Cancel to close program.",true)){
					CloseApplication=true;
					return;
				}
				e.Cancel=true;
			}
		}





	}
}