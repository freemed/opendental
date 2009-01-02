using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Xml;
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
			//textDateStart.Text=DateTime.Today.AddDays(-14).ToShortDateString();
			//textDateEnd.Text=DateTime.Today.AddDays(14).ToShortDateString();
		}

		private void textPath_TextChanged(object sender,EventArgs e) {
			if(Directory.Exists(textPath.Text)){
				labelValid.Visible=false;
				butSync.Enabled=true;
				butFullSync.Enabled=true;
			}
			else{
				labelValid.Visible=true;
				butSync.Enabled=false;
				butFullSync.Enabled=false;
			}
		}

		private void butSync_Click(object sender,EventArgs e) {
			//obsolete


			//no need to see if directory is valid because button will be disabled if not.
			//if( textDateStart.errorProvider1.GetError(textDateStart)!=""
			//	|| textDateEnd.errorProvider1.GetError(textDateEnd)!=""
			//	){
			//	MsgBox.Show(this,"Please fix data entry errors first.");
			//	return;
			//}
			Cursor=Cursors.WaitCursor;
			string path=textPath.Text;
			#if DEBUG
				path=@"E:\My Documents\HTC emulator My Documents\Business\Open Dental";
			#endif
			//patients-------------------------------------------------------------------------------------
			string command=
				@"SELECT patient.LName,patient.FName,patient.Preferred,patient.Birthdate,patient.PatNum,
				patient.PatStatus,patient.Gender,patient.Position,patient.Address,patient.City,patient.State,
				patient.HmPhone,patient.WkPhone,patient.WirelessPhone,patient.Guarantor,
				patguar.LName guarLName,patguar.FName guarFName,patient.CreditType,
				carrier.CarrierName primaryInsurance,patguar.FamFinUrgNote,patient.MedUrgNote
				FROM patient
				LEFT JOIN patient patguar ON patient.Guarantor=patguar.PatNum
				LEFT JOIN patplan ON patient.PatNum=patplan.PatNum
				LEFT JOIN insplan ON insplan.PlanNum=patplan.PlanNum
				LEFT JOIN carrier ON carrier.CarrierNum=insplan.CarrierNum
				WHERE patient.PatStatus=0/*patient*/
				OR patient.PatStatus=2/*inactive*/
				OR patient.PatStatus=1/*nonpatient*/
				ORDER BY patient.LName,patient.FName";
			DataTable table=General.GetTable(command);
			string data=TableToXML(table);
			string fileName=Path.Combine(path,"patient.txt");
			try{
				File.WriteAllText(fileName,data);
      }
      catch(Exception ex){
				Cursor=Cursors.Default;
        MessageBox.Show(fileName+"\r\n"+ex.Message);
				return;
			}
			//appointments----------------------------------------------------------------------------------
			//DateTime dateStart=PIn.PDate(textDateStart.Text);
			//DateTime dateEnd=PIn.PDate(textDateEnd.Text).AddDays(1);//midnight following specified day
			/*command="SET @dateStart="+POut.PDate(dateStart)+";"
				+"SET @dateEnd="+POut.PDate(dateEnd)+";"
				+@"SELECT AptDateTime,LName,FName,ProcDescript,appointment.PatNum
				FROM appointment,patient
				WHERE patient.PatNum=appointment.PatNum
				AND AptDateTime > @dateStart
				AND AptDateTime < @dateEnd
				ORDER BY AptDateTime";*/
			table=General.GetTable(command);
			data=TableToXML(table);
			fileName=Path.Combine(path,"appointment.txt");
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

		private string TableToXML(DataTable table){
			//table=FormQuery.MakeReadable(table);//takes a long long time
			StringBuilder strBuild=new StringBuilder();
			for(int i=0;i<table.Columns.Count;i++){
				if(i>0){
					strBuild.Append("\t");
				}
				//strBuild.Append("\"");
				strBuild.Append(table.Columns[i].ColumnName);
				//strBuild.Append("\"");
			}
			strBuild.Append("\r\n");
			string cell;
			DateTime dt;
			for(int i=0;i<table.Rows.Count;i++){
				for(int j=0;j<table.Columns.Count;j++){
					if(j>0){
						strBuild.Append("\t");
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
					//strBuild.Append("\"");
					strBuild.Append(cell);
					//strBuild.Append("\"");
				}
				strBuild.Append("\r\n");
			}
			return strBuild.ToString();
		}

		private void butFullSync_Click(object sender,EventArgs e) {
			//no need to see if directory is valid because button will be disabled if not.
			//if( textDateStart.errorProvider1.GetError(textDateStart)!=""
			//	|| textDateEnd.errorProvider1.GetError(textDateEnd)!=""
			//	){
			//	MsgBox.Show(this,"Please fix data entry errors first.");
			//	return;
			//}
			Cursor=Cursors.WaitCursor;
			string path=textPath.Text;
			#if DEBUG
				path=@"E:\My Documents\HTC emulator My Documents\Business\Open Dental";
			#endif
			XmlWriterSettings settings=new XmlWriterSettings();
			//settings.ConformanceLevel=ConformanceLevel.Fragment;
			settings.OmitXmlDeclaration=true;
			settings.Encoding=Encoding.UTF8;
			settings.Indent=true;
			settings.IndentChars="   ";
			StringBuilder strBuild=new StringBuilder();
			XmlWriter writer=XmlWriter.Create(strBuild,settings);
			writer.WriteRaw("<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n");//It kept writing utf-16, so this is my workaround.
			writer.WriteStartElement("InToMobile");
			writer.WriteAttributeString("MainVersion",Application.ProductVersion);
			writer.WriteAttributeString("MinimumMobileVersion","6.3.0.0");
			writer.WriteAttributeString("FullSync","true");
			#region patients
			List<Patient> patientsToSynch=Patients.GetUAppoint(DateTime.MinValue);//dateTimeLastUploaded);
			Dictionary<int,string> carrierNames=Carriers.GetCarrierNames(patientsToSynch);
			Patient pat;
			for(int i=0;i<patientsToSynch.Count;i++){
				pat=patientsToSynch[i];
				writer.WriteStartElement("patient");
				if(pat.PatStatus==PatientStatus.Deleted){
					//we won't store deleted patients
					writer.WriteAttributeString("action","delete");
				}
				else{
					writer.WriteAttributeString("action","write");
				}
				writer.WriteElementString("PatNum",pat.PatNum.ToString());
				writer.WriteElementString("LName",pat.LName);
				writer.WriteElementString("FName",pat.FName);
				writer.WriteElementString("Preferred",pat.Preferred);
				writer.WriteElementString("PatStatus",((int)pat.PatStatus).ToString());
				writer.WriteElementString("Gender",((int)pat.Gender).ToString());
				writer.WriteElementString("Position",((int)pat.Position).ToString());
				writer.WriteElementString("Birthdate",POut.PDate(pat.Birthdate,false));
				writer.WriteElementString("Address",pat.Address);
				writer.WriteElementString("Address2",pat.Address2);
				writer.WriteElementString("City",pat.City);
				writer.WriteElementString("State",pat.State);
				writer.WriteElementString("HmPhone",pat.HmPhone);
				writer.WriteElementString("WkPhone",pat.WkPhone);
				writer.WriteElementString("WirelessPhone",pat.WirelessPhone);
				writer.WriteElementString("Guarantor",pat.Guarantor.ToString());
				writer.WriteElementString("CreditType",pat.CreditType);
				writer.WriteElementString("FamFinUrgNote",pat.FamFinUrgNote);
				writer.WriteElementString("MedUrgNote",pat.MedUrgNote);
				writer.WriteElementString("PrimaryInsurance",carrierNames[pat.PatNum]);
				writer.WriteEndElement();//patient
			}
			#endregion patients
			writer.WriteEndElement();//InToMobile
			writer.WriteEndDocument();
			writer.Close();
			int fileNumber=PrefC.GetInt("MobileSyncLastFileNumber")+1;
			string filePath=Path.Combine(path,"in"+fileNumber+".xml");
			File.WriteAllText(filePath,strBuild.ToString(),Encoding.UTF8);
			Prefs.UpdateInt("MobileSyncLastFileNumber",fileNumber);
			//we will not trigger a refresh on other computers because this is the only one doing the sync.
			Cursor=Cursors.Default;
			MsgBox.Show(this,"Done");
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