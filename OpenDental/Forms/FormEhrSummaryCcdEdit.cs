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
			if(IsReconcile) {
				labelReconcile.Visible=true;
				butReconcileMedications.Visible=true;
				butReconcileProblems.Visible=true;
				butReconcileAllergies.Visible=true;
			}
			Cursor=Cursors.WaitCursor;
			webBrowser1.Url=new Uri(StrXmlFilePath);
			Cursor=Cursors.Default;
		}

		///<summary>Can only be called if IsReconcile is true.</summary>
		private void butReconcileMedications_Click(object sender,EventArgs e) {
			//TODO: This function is for EHR module b.4.  Create and call the new medications reconcile window using the medication information contained within the CCD XML text located in the file StrXmlFilePath.  A public static function must be created in EhrCCD to parse the medications out of the CCD XML, similar to EhrCCD.GetCCDpat().
		}

		///<summary>Can only be called if IsReconcile is true.</summary>
		private void butReconcileProblems_Click(object sender,EventArgs e) {
			//TODO: This function is for EHR module b.4.  Create and call the new problems reconcile window using the problem information contained within the CCD XML text located in the file StrXmlFilePath.  A public static function must be created in EhrCCD to parse the problems out of the CCD XML, similar to EhrCCD.GetCCDpat().
		}

		///<summary>Can only be called if IsReconcile is true.</summary>
		private void butReconcileAllergies_Click(object sender,EventArgs e) {
			//TODO: This function is for EHR module b.4.  Create and call the new allergies reconcile window using the allergy information contained within the CCD XML text located in the file StrXmlFilePath.  A public static function must be created in EhrCCD to parse the allergies out of the CCD XML, similar to EhrCCD.GetCCDpat().
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
