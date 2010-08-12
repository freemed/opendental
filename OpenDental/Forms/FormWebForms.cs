using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using OpenDental.UI;
using OpenDentBusiness;


namespace OpenDental {
	public partial class FormWebForms:Form {
		public FormWebForms() {
			InitializeComponent();
			Lan.F(this);

		}
		private void FillGrid(){


			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn(Lan.g(this,"Last Name"),100);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"First Name"),100);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Birth Date"),100);
			gridMain.Columns.Add(col);

			gridMain.Rows.Clear();


			DateTime dateFrom=PIn.Date(textDateFrom.Text);
			DateTime dateTo=PIn.Date(textDateTo.Text);

			WebHostSynch.WebHostSynch wh = new WebHostSynch.WebHostSynch();

			//The url  is to be obtained from the preferenes table in the db
			//wh.Url ="";
			string RegistrationKey = PrefC.GetString(PrefName.RegistrationKey);
			if(wh.CheckRegistrationKey(RegistrationKey)==false) {
				
				MessageBox.Show(Lan.g(this,"Registration key provided by the dental office is incorrect"));

				return;

			}



			OpenDental.WebHostSynch.webforms_sheetfield[] wbsf = wh.GetSheetData(1,"RegistrationKeyxxxxx",dateFrom,dateTo);

			if(wbsf.Count()==0) {
			
				MessageBox.Show(Lan.g(this,"No Patient Forms retrieved"));
				return;

			}

			// Select distinct Web sheet ids
			var wbs = (from w in wbsf select w.webforms_sheetReference.EntityKey.EntityKeyValues.First().Value).Distinct();

			var SheetIdArray = wbs.ToArray();






			// loop through each sheet
			for(int LoopVariable = 0;LoopVariable < SheetIdArray.Length;LoopVariable++) {

				long SheetID=(long)SheetIdArray[LoopVariable];
				var SingleSheet = from w in wbsf where (long)w.webforms_sheetReference.EntityKey.EntityKeyValues.First().Value ==  SheetID
								  select w;
				ODGridRow row=new ODGridRow();
				
				string LastName=null;
				string FirstName=null;
				string BirthDate=null;

				for(int LoopVariable1 = 0;LoopVariable1 < SingleSheet.Count();LoopVariable1++) {
					
					String FieldName = SingleSheet.ElementAt(LoopVariable1).FieldName;
					String FieldValue = SingleSheet.ElementAt(LoopVariable1).FieldValue;


					if(FieldName.ToLower().Contains("lastname")) {
						
						LastName=FieldValue;
					}
					if(FieldName.ToLower().Contains("firstname")) {
						FirstName=FieldValue;
					}
					if(FieldName.ToLower().Contains("birthdate")) {
						BirthDate=FieldValue;
					}

					

				}

				row.Cells.Add(LastName);
				row.Cells.Add(FirstName);
				row.Cells.Add(BirthDate);
					
				gridMain.Rows.Add(row);
				gridMain.EndUpdate();

			}

			

			string a = "You bozo!";


		}
		private void butOK_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private void FormWebForms_Load(object sender,EventArgs e) {
			SetDates();
		}

		private void butRetrieve_Click(object sender,EventArgs e) {
			if(textDateFrom.errorProvider1.GetError(textDateFrom)!=""
				|| textDateTo.errorProvider1.GetError(textDateTo)!=""
				) {
				MessageBox.Show(Lan.g(this,"Please fix data entry errors first."));
				return;
			}

			FillGrid();
		}
		private void SetDates(){
			
				textDateFrom.Text=DateTime.Today.ToShortDateString();
				textDateTo.Text=DateTime.Today.ToShortDateString();
				
			}
	}
}