using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.UI;
using System.Data;
using System.Drawing.Imaging;
using System.IO;

namespace OpenDental {
	public partial class FormAnesthesiaScore:Form {

		public static Userod CurUser;
		private AnestheticRecord AnestheticRecordCur;
		private Patient PatCur;
		private int patNum;
		private string curDate;

		public FormAnesthesiaScore(Patient patCur) {
			InitializeComponent();
			Lan.F(this);
			PatCur = patCur;
		}

		private void FormAnesthesiaScore_Load(object sender, EventArgs e)
		{
			//display Patient name
			textPatient.Text = Patients.GetPat(PatCur.PatNum).GetNameFL();
			//display Patient ID number
			textPatID.Text = PatCur.PatNum.ToString();

			textDate.Text = DateTime.Now.ToString();

		}
		private void butOK_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}


		private void radioButDischUnstable_CheckedChanged(object sender, EventArgs e)
		{

		}

		private void textPatient_TextChanged(object sender, EventArgs e)
		{

		}
	}
}