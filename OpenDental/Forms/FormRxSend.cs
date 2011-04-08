using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.UI;
using System.IO;

namespace OpenDental {
	public partial class FormRxSend:Form {
		private List<RxPat> listRx;

		public FormRxSend() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormRxSend_Load(object sender,EventArgs e) {
			for(int i=0;i<PharmacyC.Listt.Count;i++) {
				comboPharmacy.Items.Add(PharmacyC.Listt[i].StoreName);
			}
			comboPharmacy.SelectedIndex=0;
			FillGrid();
			gridMain.SetSelected(true);
		}

		private void FillGrid() {
			if(PharmacyC.Listt.Count<1) {
				MsgBox.Show(this,"Need to set up at least one pharmacy.");
				return;
			}
			listRx=RxPats.GetMultElectQueueRx(PharmacyC.Listt[comboPharmacy.SelectedIndex].PharmacyNum);
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g("TableQueue","Patient"),150);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableQueue","Provider"),150);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableQueue","Rx"),150);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g("TableQueue","Pharmacy"),150);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<listRx.Count;i++) {
				Patient patCur=Patients.GetLim(listRx[i].PatNum);
				row=new ODGridRow();
				row.Cells.Add(Patients.GetNameLF(patCur.LName,patCur.FName,patCur.Preferred,patCur.MiddleI));
				row.Cells.Add(Providers.GetAbbr(listRx[i].ProvNum));
				row.Cells.Add(listRx[i].Drug);
				row.Cells.Add(Pharmacies.GetDescription(listRx[i].PharmacyNum));
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}
		
		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			if(gridMain.SelectedIndices.Length<1) {
				MsgBox.Show(this,"Must select at least one Rx.");
				return;
			}
			Patient patCur=Patients.GetLim(listRx[gridMain.SelectedIndices[0]].PatNum);
			FormRxEdit FormRE=new FormRxEdit(patCur,listRx[gridMain.SelectedIndices[0]]);
			FormRE.ShowDialog();
			FillGrid();
		}

		private void comboPharmacy_SelectionChangeCommitted(object sender,EventArgs e) {
			FillGrid();
		}

		private void butRefresh_Click(object sender,EventArgs e) {
			FillGrid();
		}
		
		private void butAll_Click(object sender,EventArgs e) {
			gridMain.SetSelected(true);
		}

		private void butNone_Click(object sender,EventArgs e) {
			gridMain.SetSelected(false);
		}

		private void butSend_Click(object sender,EventArgs ea) {
			if(gridMain.SelectedIndices.Length<1) {
				MsgBox.Show(this,"Must select at least one Rx.");
				return;
			}
			//TODO: Loop through selected indicies and send rx's.
			//Create a document that has the correct format per the script document.
			//Ask Jordan about information like Clinic ID and where we will get/store this stuff.
			
			StringBuilder strb=new StringBuilder();
			char f=':';//separates fields within a composite element
			char e='+';//(separates composite elements) SureScripts may require an unprintable character here.
			char s='\'';
			//etc.
			//UNA:+./*'------------------------------------------------------------------------------------------------
			strb.AppendLine("UNA"+f+e+"./*'");
			//UIB+UNOA:Ø++1234567+++77777777:C:PASSWORDQ+77Ø163Ø:P+19971ØØ1:Ø81522’-----------------------------------
			strb.Append("UIB"+e);//000
			strb.Append("UNOA"+f+"0"+e);//010
			strb.Append(e);//020 not used
			strb.Append("1234567"+e);//030  
			strb.Append(e);//040 not used
			strb.Append(e);//050 not used



			string filePath=Path.Combine(Application.StartupPath,"RxScript.txt");
			try {
				File.WriteAllText(filePath,strb.ToString());
			}
			catch(Exception ex) {
				MessageBox.Show(ex.Message);
			}
			/*
			string line="";
			sw.WriteLine(line);
			line="";
			sw.WriteLine(line);
			line="UIH+SCRIPT:Ø1Ø:ØØ6:NEWRX+11ØØ72+++19971ØØ1:Ø81522’";
			sw.WriteLine(line);
			line="PVD+P1+77Ø163Ø:D3+++++MAIN STREET PHARMACY++61522Ø5656:TE’";
			sw.WriteLine(line);
			line="PVD+PC+6666666:ØB+++JONES:MARK++++61522198ØØ:TE’";
			sw.WriteLine(line);
			line="PTT++19541225+SMITH:MARY+F+333445555:SY’";
			sw.WriteLine(line);
			line="COO+123456:BO+INSURANCE COMPANY NAME++123456789++AA112’";
			sw.WriteLine(line);
			line="DRU+P:CALAN SR 24ØMG::::24Ø:::::::AA:C42998:AB:C28253+::6Ø:38:AC:C48542+:1 TID -TAKE ONE"
				+"TABLET TWO TIMES A DAY UNTIL GONE+85:19971ØØ1:1Ø2*ZDS:3Ø:8Ø4+Ø+R:1’";
			sw.WriteLine(line);
			line="UIT+11ØØ72+6’";
			sw.WriteLine(line);
			line="UIZ++1’";
			sw.WriteLine(line);
			*/
     
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	}
}