using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using CodeBase;
using OpenDental.UI;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormEhrSummaryCcdEdit:Form {
		public string StrXmlFilePath;
		public bool DidPrint;
		private bool IsReconcile;

		public FormEhrSummaryCcdEdit(string strXmlFilePath,bool isReconcile) {
			InitializeComponent();
			StrXmlFilePath=strXmlFilePath;
			IsReconcile=isReconcile;
		}

		private void FormEhrSummaryCcdEdit_Load(object sender,EventArgs e) {
			//if(IsReconcile) { //Cannot reconcile until version 14.1.
			//	labelReconcile.Visible=true;
			//	butReconcileMedications.Visible=true;
			//	butReconcileProblems.Visible=true;
			//	butReconcileAllergies.Visible=true;
			//}
			Cursor=Cursors.WaitCursor;
			webBrowser1.Url=new Uri(StrXmlFilePath);
			Cursor=Cursors.Default;
		}

		///<summary>Can only be called if IsReconcile is true.  This function is for EHR module b.4.</summary>
		private void butReconcileMedications_Click(object sender,EventArgs e) {
			XmlDocument xmlDocCcd=new XmlDocument();
			try {
				string strXmlText=File.ReadAllText(StrXmlFilePath);
				xmlDocCcd.LoadXml(strXmlText);				
			}
			catch(Exception ex) {
				MessageBox.Show(Lan.g(this,"Error reading file")+": "+ex.Message);
				return;
			}
			FormReconcileMedication formRM=new FormReconcileMedication();
			formRM.ListMedicationPatNew=new List<MedicationPat>();
			List<Medication> listMedications=new List<Medication>();//TODO: Set formRM.ListMedicationsNew.
			EhrCCD.GetListMedicationPats(xmlDocCcd,formRM.ListMedicationPatNew,listMedications);
			formRM.ShowDialog();
		}

		///<summary>Can only be called if IsReconcile is true.  This function is for EHR module b.4.</summary>
		private void butReconcileProblems_Click(object sender,EventArgs e) {
			XmlDocument xmlDocCcd=new XmlDocument();
			try {
				string strXmlText=File.ReadAllText(StrXmlFilePath);
				xmlDocCcd.LoadXml(strXmlText);
			}
			catch(Exception ex) {
				MessageBox.Show(Lan.g(this,"Error reading file")+": "+ex.Message);
				return;
			}
			FormReconcileProblem formRP=new FormReconcileProblem();
			formRP.ListProblemNew=new List<Disease>();
			List<DiseaseDef> listDiseaseDef=new List<DiseaseDef>();//TODO: Set formRP.ListProblemDefNew
			EhrCCD.GetListDiseases(xmlDocCcd,formRP.ListProblemNew,listDiseaseDef);
			formRP.ShowDialog();
		}

		///<summary>Can only be called if IsReconcile is true.  This function is for EHR module b.4.</summary>
		private void butReconcileAllergies_Click(object sender,EventArgs e) {
			XmlDocument xmlDocCcd=new XmlDocument();
			try {
				string strXmlText=File.ReadAllText(StrXmlFilePath);
				xmlDocCcd.LoadXml(strXmlText);
			}
			catch(Exception ex) {
				MessageBox.Show(Lan.g(this,"Error reading file")+": "+ex.Message);
				return;
			}
			FormReconcileAllergy formRA=new FormReconcileAllergy();
			formRA.ListAllergyNew=new List<Allergy>();
			List<AllergyDef> listAllergyDef=new List<AllergyDef>();//TODO: Set formRA ListAllergyDefNew
			EhrCCD.GetListAllergies(xmlDocCcd,formRA.ListAllergyNew,listAllergyDef);
			formRA.ShowDialog();
		}

		private void butShowXml_Click(object sender,EventArgs e) {
			string strCcd=File.ReadAllText(StrXmlFilePath);
			//Reformat to add newlines after each element to make more readable.
			strCcd=strCcd.Replace("\r\n","").Replace("\n","").Replace("\r","");//Remove existsing newlines.
			strCcd=strCcd.Replace(">",">\r\n");
			MsgBoxCopyPaste msgbox=new MsgBoxCopyPaste(strCcd);
			msgbox.ShowDialog();
		}

		private void butPrint_Click(object sender,EventArgs e) {
			//use the modeless version, which also allows user to choose printer
			webBrowser1.ShowPrintDialog();
			DidPrint = true;			
		}

		private void butClose_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

	}
}
