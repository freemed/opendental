using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using MySql.Data.MySqlClient;

namespace DatabaseIntegrityCheck {
	public partial class FormDatabaseCheck:Form {
		//private string logData;

		public FormDatabaseCheck() {
			InitializeComponent();
		}

		private void FormDatabaseCheck_Load(object sender,EventArgs e) {
			XmlDocument document=new XmlDocument();
			if(!File.Exists("FreeDentalConfig.xml")) {
				textComputerName.Text="localhost";
				#if(TRIALONLY)
					textDatabase.Text="demo";
				#else
					textDatabase.Text="opendental";
				#endif
				textUser.Text="root";
				return;
			}
			try {
				document.Load("FreeDentalConfig.xml");
				XmlNodeReader reader=new XmlNodeReader(document);
				string currentElement="";
				while(reader.Read()) {
					if(reader.NodeType==XmlNodeType.Element) {
						currentElement=reader.Name;
					}
					else if(reader.NodeType==XmlNodeType.Text) {
						switch(currentElement) {
							case "ComputerName":
								textComputerName.Text=reader.Value;
								break;
							case "Database":
								textDatabase.Text=reader.Value;
								break;
							case "User":
								textUser.Text=reader.Value;
								break;
							case "Password":
								textPassword.Text=reader.Value;
								break;
						}
					}
				}
				reader.Close();
			}
			catch {//Exception e) {
				//MessageBox.Show(e.Message);
				textComputerName.Text="localhost";
				textDatabase.Text="opendental";
				textUser.Text="root";
			}
		}


		private void butRun_Click(object sender,EventArgs e) {
			//this tool would only be used with MySQL, so the current code is just fine.
			MySqlDataAdapter da;
			MySqlConnection con=new MySqlConnection("Server="+textComputerName.Text
				+";Database="+textDatabase.Text
				+";User ID="+textUser.Text
				+";Password="+textPassword.Text
				+";CharSet=utf8");
			//MySqlDataReader dr;
			MySqlCommand cmd=new MySqlCommand();
			//int InsertID; 
			cmd.Connection=con;
			cmd.CommandText="SHOW TABLES";
			DataTable table=new DataTable();
			try {
				Cursor=Cursors.WaitCursor;
				da=new MySqlDataAdapter(cmd);
				da.Fill(table);
				string[] tableName=new string[table.Rows.Count];
				int lastRow;
				ArrayList corruptTables=new ArrayList();
				for(int i=0;i<table.Rows.Count;i++) {
					tableName[i]=table.Rows[i][0].ToString();
				}
				for(int i=0;i<tableName.Length;i++) {
					cmd.CommandText="CHECK TABLE "+tableName[i];
					table=new DataTable();
					da=new MySqlDataAdapter(cmd);
					da.Fill(table);
					lastRow=table.Rows.Count-1;
					if(table.Rows[lastRow][3].ToString()!="OK") {
						corruptTables.Add(tableName[i]);
					}
				}
				Cursor=Cursors.Default;
				if(corruptTables.Count==0) {
					MessageBox.Show("You have no corrupted tables.");
					return;
				}
				string corruptS="";
				for(int i=0;i<corruptTables.Count;i++) {
					corruptS+=corruptTables[i]+"\r";
				}
				if(MessageBox.Show("You have the following corrupt tables:\r"
					+corruptS
					+"It is strongly suggested that you select Cancel and make a backup before continuing.  Select OK to repair tables.","",MessageBoxButtons.OKCancel)!=DialogResult.OK) 
				{
					return;
				}
				Cursor=Cursors.WaitCursor;
				for(int i=0;i<corruptTables.Count;i++) {
					cmd.CommandText="REPAIR TABLE "+corruptTables[i];
					table=new DataTable();
					da=new MySqlDataAdapter(cmd);
					da.Fill(table);
					SaveToLog((string)corruptTables[i],table);
				}
				Cursor=Cursors.Default;
				//PrintLog();
				MessageBox.Show("The tables have probably been repaired, but the repairlog must be analyzed.  Open RepairLog.txt to view.");
			}
			catch(Exception ex){
				Cursor=Cursors.Default;
				MessageBox.Show(ex.Message);
			}
			finally {
				Cursor=Cursors.Default;
				con.Close();
			}
	
		}

		private void SaveToLog(string corruptTable,DataTable table) {
			FileStream fs=new FileStream("RepairLog.txt",FileMode.Append,FileAccess.Write,FileShare.Read);
			StreamWriter sw=new StreamWriter(fs);
			String line="";
			line=corruptTable+" "+DateTime.Now.ToString()+"\r\n";
			sw.Write(line);
			//logData+=line;
			for(int i=0;i<table.Rows.Count;i++) {
				line="";
				for(int j=0;j<table.Columns.Count;j++) {
					line+=table.Rows[i][j].ToString()+",";
				}
				line+="\r\n";
				sw.Write(line);
				//logData+=line;
			}
			sw.Close();
			sw=null;
			fs.Close();
			fs=null;
		}

		

	}
}