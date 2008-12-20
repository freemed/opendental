using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormMobile:Form {
		public FormMobile() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormMobile_Load(object sender,EventArgs e) {
			textPath.Text=PrefC.GetString("MobileSyncPath");
			textDateStart.Text=DateTime.Today.AddDays(-14).ToShortDateString();
			textDateEnd.Text=DateTime.Today.AddDays(14).ToShortDateString();
		}

		private void textPath_TextChanged(object sender,EventArgs e) {
			if(Directory.Exists(textPath.Text)){
				labelValid.Visible=false;
				butSync.Enabled=true;
			}
			else{
				labelValid.Visible=true;
				butSync.Enabled=false;
			}
		}

		private void butSync_Click(object sender,EventArgs e) {
			//no need to see if directory is valid because button will be disabled if not.
			if( textDateStart.errorProvider1.GetError(textDateStart)!=""
				|| textDateEnd.errorProvider1.GetError(textDateEnd)!=""
				){
				MsgBox.Show(this,"Please fix data entry errors first.");
				return;
			}
			Cursor=Cursors.WaitCursor;
			//patients-------------------------------------------------------------------------------------
			string command=
				@"SELECT LName,FName,Birthdate,PatNum
				FROM patient
				WHERE PatStatus=0/*patient*/
				OR PatStatus=2/*inactive*/
				OR PatStatus=1/*nonpatient*/
				ORDER BY LName,FName";
			DataTable table=General.GetTable(command);
			string data=TableToCSV(table);
			string fileName=Path.Combine(textPath.Text,"patient.txt");
			try{
				File.WriteAllText(fileName,data);
      }
      catch(Exception ex){
				Cursor=Cursors.Default;
        MessageBox.Show(fileName+"\r\n"+ex.Message);
				return;
			}
			//appointments----------------------------------------------------------------------------------
			DateTime dateStart=PIn.PDate(textDateStart.Text);
			DateTime dateEnd=PIn.PDate(textDateEnd.Text).AddDays(1);//midnight following specified day
			command="SET @dateStart="+POut.PDate(dateStart)+";"
				+"SET @dateEnd="+POut.PDate(dateEnd)+";"
				+@"SELECT AptDateTime,LName,FName,ProcDescript,appointment.PatNum
				FROM appointment,patient
				WHERE patient.PatNum=appointment.PatNum
				AND AptDateTime > @dateStart
				AND AptDateTime < @dateEnd
				ORDER BY AptDateTime";
			table=General.GetTable(command);
			data=TableToCSV(table);
			fileName=Path.Combine(textPath.Text,"appointment.txt");
			try{
				File.WriteAllText(fileName,data);
      }
      catch(Exception ex){
				Cursor=Cursors.Default;
        MessageBox.Show(fileName+"\r\n"+ex.Message);
				return;
			}
			Cursor=Cursors.Default;
			MsgBox.Show(this,"Done");
		}

		private string TableToCSV(DataTable table){
			//table=FormQuery.MakeReadable(table);//takes a long long time
			StringBuilder strBuild=new StringBuilder();
			for(int i=0;i<table.Columns.Count;i++){
				if(i>0){
					strBuild.Append(",");
				}
				strBuild.Append("\"");
				strBuild.Append(table.Columns[i].ColumnName);
				strBuild.Append("\"");
			}
			strBuild.Append("\r\n");
			string cell;
			DateTime dt;
			for(int i=0;i<table.Rows.Count;i++){
				for(int j=0;j<table.Columns.Count;j++){
					if(j>0){
						strBuild.Append(",");
					}
					if(table.Columns[j].ColumnName=="Birthdate"){
						dt=(DateTime)table.Rows[i][j];
						if(dt.Year<1880){
							cell="";
						}
						else{
							cell=dt.ToShortDateString();
						}
					}
					else{
						cell=table.Rows[i][j].ToString();
					}
					cell=cell.Replace("\r","");
					cell=cell.Replace("\n","");
					cell=cell.Replace("\t","");
					cell=cell.Replace("\"","");
					strBuild.Append("\"");
					strBuild.Append(cell);
					strBuild.Append("\"");
				}
				strBuild.Append("\r\n");
			}
			return strBuild.ToString();
		}

		private void butClose_Click(object sender,EventArgs e) {
			Close();
		}

		private void FormMobile_FormClosing(object sender,FormClosingEventArgs e) {
			if(textPath.Text!=PrefC.GetString("MobileSyncPath")){
				if(MsgBox.Show(this,true,"Save changes to path?")){
					Prefs.UpdateString("MobileSyncPath",textPath.Text);
					DataValid.SetInvalid(InvalidType.Prefs);
				}
			}
		}

		

		

		
	}
}