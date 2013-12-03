using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using CodeBase;
using OpenDental.UI;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormEhrPatientExport:Form {
		DataTable table;

		public FormEhrPatientExport() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormEhrPatientExport_Load(object sender,EventArgs e) {
			comboProv.Items.Add(Lan.g(this,"All"));
			comboProv.SelectedIndex=0;
			for(int i=0;i<ProviderC.ListShort.Count;i++) {
				comboProv.Items.Add(ProviderC.ListShort[i].GetLongDesc());
			}
			if(PrefC.GetBool(PrefName.EasyNoClinics)) {
				comboClinic.Visible=false;
				labelClinic.Visible=false;
			}
			else {
				comboClinic.Items.Add(Lan.g(this,"All"));
				comboClinic.SelectedIndex=0;
				for(int i=0;i<Clinics.List.Length;i++) {
					comboClinic.Items.Add(Clinics.List[i].Description);
				}
			}
			if(PrefC.GetBool(PrefName.EasyHidePublicHealth)) {
				comboSite.Visible=false;
				labelSite.Visible=false;
			}
			else {
				comboSite.Items.Add(Lan.g(this,"All"));
				comboSite.SelectedIndex=0;
				for(int i=0;i<SiteC.List.Length;i++) {
					comboSite.Items.Add(SiteC.List[i].Description);
				}
			}
		}

		private void butSearch_Click(object sender,EventArgs e) {
			gridMain.SetSelected(false);
			FillGridMain();
		}

		private void FillGridMain() {
			//Get filters from user input
			string firstName="";
			if(textFName.Text!="") {
				firstName=textFName.Text;
			}
			string lastName="";
			if(textLName.Text!="") {
				lastName=textLName.Text;
			}
			int patNum=0;
			try {
				if(textPatNum.Text!="") {
					patNum=PIn.Int(textPatNum.Text);
				}
			}
			catch {
				MsgBox.Show(this,"Invalid PatNum");
				return;
			}
			long provNum=0;
			if(comboProv.SelectedIndex!=0) {
				provNum=ProviderC.ListShort[comboProv.SelectedIndex-1].ProvNum;
			}
			long clinicNum=0;
			if(!PrefC.GetBool(PrefName.EasyNoClinics) && comboClinic.SelectedIndex!=0) {
				clinicNum=Clinics.List[comboClinic.SelectedIndex-1].ClinicNum;
			}
			long siteNum=0;
			if(!PrefC.GetBool(PrefName.EasyHidePublicHealth) && comboSite.SelectedIndex!=0) {
				siteNum=SiteC.List[comboSite.SelectedIndex-1].SiteNum;
			}
			//Get table
			table=Patients.GetExportList(patNum,firstName,lastName,provNum,clinicNum,siteNum);
			//Create grid			
			//Patient Name | Primary Provider | Date Last Visit | Clinic | Site 
			gridMain.Columns.Clear();
			ODGridColumn col;
			col=new ODGridColumn("PatNum",60);
			col.SortingStrategy=GridSortingStrategy.AmountParse;
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Patient Name",200);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Primary Provider",110);
			gridMain.Columns.Add(col);
			if(!PrefC.GetBool(PrefName.EasyNoClinics)) {
				col=new ODGridColumn("Clinic",110);
				gridMain.Columns.Add(col);
			}
			if(!PrefC.GetBool(PrefName.EasyHidePublicHealth)) {
				col=new ODGridColumn("Site",110);
				gridMain.Columns.Add(col);
			}
			//Fill grid
			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<table.Rows.Count;i++) {
				row=new ODGridRow();
				row.Cells.Add(table.Rows[i]["PatNum"].ToString());
				row.Cells.Add(table.Rows[i]["LName"].ToString()+", "+table.Rows[i]["FName"].ToString());
				row.Cells.Add(table.Rows[i]["Provider"].ToString());
				if(!PrefC.GetBool(PrefName.EasyNoClinics)) {
					row.Cells.Add(table.Rows[i]["Clinic"].ToString());
				}
				if(!PrefC.GetBool(PrefName.EasyHidePublicHealth)) {
					row.Cells.Add(table.Rows[i]["Site"].ToString());
				}
				row.Tag=PIn.Long(table.Rows[i]["PatNum"].ToString());
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}

		private void butSelectAll_Click(object sender,EventArgs e) {
			gridMain.SetSelected(true);
		}

		private void butExport_Click(object sender,EventArgs e) {
			FolderBrowserDialog dlg=new FolderBrowserDialog();
			DialogResult result=dlg.ShowDialog();
			if(result!=DialogResult.OK) {
				return;
			}
			DateTime dateNow=DateTime.Now;
			string folderPath=ODFileUtils.CombinePaths(dlg.SelectedPath,(dateNow.Year+"_"+dateNow.Month+"_"+dateNow.Day));
			if(Directory.Exists(folderPath)) {
				int loopCount=1;
				while(Directory.Exists(folderPath+"_"+loopCount)) {
					loopCount++;
				}
				folderPath=folderPath+"_"+loopCount;
			}
			try {
				Directory.CreateDirectory(folderPath);
			}
			catch {
				MessageBox.Show("Error, Could not create folder");
				return;
			}
			Patient patCur;
			string fileName;
			for(int i=0;i<gridMain.SelectedIndices.Length;i++) {
				patCur=Patients.GetPat((long)gridMain.Rows[gridMain.SelectedIndices[i]].Tag);//Cannot use GetLim because more information is needed in the CCD message generation below.
				fileName="";
				string lName=patCur.LName;
				for(int j=0;j<lName.Length;j++) {  //Strip all non-letters from FName
					if(Char.IsLetter(lName,j)) {
						fileName+=lName.Substring(j,1);
					}
				}
				fileName+="_";
				string fName=patCur.FName;
				for(int k=0;k<fName.Length;k++) {  //Strip all non-letters from LName
					if(Char.IsLetter(fName,k)) {
						fileName+=fName.Substring(k,1);
					}
				}
				fileName+="_"+patCur.PatNum;  //LName_FName_PatNum
				string ccd=EhrCCD.GeneratePatientExport(patCur);
				try {
					File.WriteAllText(ODFileUtils.CombinePaths(folderPath,fileName+".xml"),ccd);
				}
				catch {
					MessageBox.Show("Error, Could not create xml file");
					return;
				}
				//File.WriteAllText(Path.Combine(folderpath,filename+".xsl"),FormEHR.GetEhrResource("CCD"));
			}
			MessageBox.Show("Exported");
		}

		private void butExportAll_Click(object sender,EventArgs e) {
			//Export all active patients
		}

		private void butClose_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.OK;
		}

	}
}