using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.UI;

namespace OpenDental {
	public partial class FormInterventionEdit:Form {
		public Intervention InterventionCur;
		///<summary>If launching from another form, like FormVitalsignEdit2014, we will give the user a list of codes from which to choose that are in the value sets above/below normal follow-up and referrals where weight assessment may occur.  This bool will indicate that we have given them a recommended set of codes to choose from and if they select a code from a different set of codes we need to warn them about the affect on CQM's.</summary>
		public bool IsAllTypes;
		private List<EhrCode> listCode;
		private string Description;
		private List<string> listValueSetOIDs;

		public FormInterventionEdit() {
			InitializeComponent();
			Lan.F(this);
		}

		///<summary>If launched from a EHR CQM form we will set the IntervCodeSetIndex based on the situation.  For example, if launched from FormVitalsignEdit2014 due to overweight, we will set IntervCodeSetIndex=InterventionCodeSet.AboveNormalWeight and set IsRecommend=true.  If the user changes the code list to one of the other sub-sets, with IsRecommend=true, we will warn the user that the code will no longer apply to the measure.  If they add an intervention by opening FormInterventions and pressing Add, set IsRecommend=false and IntervCodeSetIndex will default to 0.  We will not warn the user for changing the code set and allow them to enter any of the available intervention codes.</summary>
		private void FormInterventionEdit_Load(object sender,EventArgs e) {
			listValueSetOIDs=new List<string>();
			comboCodeSet.Items.Add("All");
			if(IsAllTypes || InterventionCur.CodeSet==InterventionCodeSet.AboveNormalWeight) {
				comboCodeSet.Items.Add(InterventionCodeSet.AboveNormalWeight.ToString()+" Follow-up");
				listValueSetOIDs.Add("2.16.840.1.113883.3.600.1.1525");
				comboCodeSet.Items.Add(InterventionCodeSet.AboveNormalWeight.ToString()+" Referral");
				listValueSetOIDs.Add("2.16.840.1.113883.3.600.1.1527");
				comboCodeSet.Items.Add(InterventionCodeSet.AboveNormalWeight.ToString()+" Medication");
				listValueSetOIDs.Add("2.16.840.1.113883.3.600.1.1498");
			}
			if(IsAllTypes || InterventionCur.CodeSet==InterventionCodeSet.BelowNormalWeight) {
				comboCodeSet.Items.Add(InterventionCodeSet.BelowNormalWeight.ToString()+" Follow-up");
				listValueSetOIDs.Add("2.16.840.1.113883.3.600.1.1528");
				comboCodeSet.Items.Add(InterventionCodeSet.BelowNormalWeight.ToString()+" Referral");
				if(!listValueSetOIDs.Contains("2.16.840.1.113883.3.600.1.1527")) {
					listValueSetOIDs.Add("2.16.840.1.113883.3.600.1.1527");
				}
				comboCodeSet.Items.Add(InterventionCodeSet.BelowNormalWeight.ToString()+" Medication");
				listValueSetOIDs.Add("2.16.840.1.113883.3.600.1.1499");
			}
			if(IsAllTypes || InterventionCur.CodeSet==InterventionCodeSet.Nutrition || InterventionCur.CodeSet==InterventionCodeSet.PhysicalActivity) {
				comboCodeSet.Items.Add(InterventionCodeSet.Nutrition.ToString()+" Counseling");
				listValueSetOIDs.Add("2.16.840.1.113883.3.464.1003.195.12.1003");
				comboCodeSet.Items.Add(InterventionCodeSet.PhysicalActivity.ToString()+" Counseling");
				listValueSetOIDs.Add("2.16.840.1.113883.3.464.1003.118.12.1035");
			}
			if(IsAllTypes || InterventionCur.CodeSet==InterventionCodeSet.TobaccoCessation) {
				comboCodeSet.Items.Add(InterventionCur.CodeSet.ToString()+" Counseling");
				listValueSetOIDs.Add("2.16.840.1.113883.3.526.3.509");
				comboCodeSet.Items.Add(InterventionCur.CodeSet.ToString()+" Medication");
				listValueSetOIDs.Add("2.16.840.1.113883.3.526.3.1190");
			}
			if(IsAllTypes || InterventionCur.CodeSet==InterventionCodeSet.Dialysis) {
				comboCodeSet.Items.Add(InterventionCur.CodeSet.ToString()+" Education");
				listValueSetOIDs.Add("2.16.840.1.113883.3.464.1003.109.12.1016");
				comboCodeSet.Items.Add(InterventionCur.CodeSet.ToString()+" Related Services");
				listValueSetOIDs.Add("2.16.840.1.113883.3.464.1003.109.12.1015");
			}
			comboCodeSet.SelectedIndex=0;
			textDate.Text=InterventionCur.DateTimeEntry.ToShortDateString();
			textNote.Text=InterventionCur.Note;
			Description="";
			FillGrid();
		}

		private void FillGrid() {
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col=new ODGridColumn("Code",70);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("CodeSystem",90);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Description",200);
			gridMain.Columns.Add(col);
			if(comboCodeSet.SelectedIndex==0) {
				listCode=EhrCodes.GetForValueSetOIDs(listValueSetOIDs,true);//this is all codes in the available set of codes (e.g. all AboveNormalWeight Interventions, Referrals, and Medications)
			}
			else {
				listCode=EhrCodes.GetForValueSetOIDs(new List<string> { listValueSetOIDs[comboCodeSet.SelectedIndex-1] },true);//they only want one subset (e.g. only the AboveNormalWeight Medications) 
			}
			gridMain.Rows.Clear();
			ODGridRow row;
			int selectedIdx=-1;
			for(int i=0;i<listCode.Count;i++) {
				//We might not be able to display and let the user select codes from systems that require purchasing the code system, like CPT codes.  Might have to hide unless they exist in the CPT (or other code system) table.
				row=new ODGridRow();
				row.Cells.Add(listCode[i].CodeValue);
				row.Cells.Add(listCode[i].CodeSystem);
				//Since we require them to select from allowed codes from the table ehrcode we do not need to retrieve the description from the big tables.  If we ever allow the user to select a code from the master list, we will have to get the description from the associated table. (i.e. get the descript from the snomed, icd9, ...etc tables)
				row.Cells.Add(listCode[i].Description);
				gridMain.Rows.Add(row);
				if(listCode[i].CodeValue==InterventionCur.CodeValue && listCode[i].CodeSystem==InterventionCur.CodeSystem) {
					selectedIdx=i;
				}
			}
			gridMain.EndUpdate();
			if(selectedIdx>-1) {
				gridMain.SetSelected(selectedIdx,true);
				gridMain.ScrollToIndex(selectedIdx);
			}
		}

		private void comboCodeSet_SelectionChangeCommitted(object sender,EventArgs e) {
			FillGrid();
		}

		private void butDelete_Click(object sender,EventArgs e) {
			if(InterventionCur.IsNew) {
				DialogResult=DialogResult.Cancel;
				return;
			}
			if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"Delete?")) {
				return;
			}
			Interventions.Delete(InterventionCur.InterventionNum);
			DialogResult=DialogResult.Cancel;
		}

		private void butOK_Click(object sender,EventArgs e) {
			//validate--------------------------------------
			DateTime date;
			if(textDate.Text=="") {
				MsgBox.Show(this,"Please enter a date.");
				return;
			}
			try {
				date=DateTime.Parse(textDate.Text);
			}
			catch {
				MsgBox.Show(this,"Please fix date first.");
				return;
			}
			string codeVal="";
			string codeSys="";
			if(gridMain.GetSelectedIndex()==-1) {//no intervention code selected
				MsgBox.Show(this,"You must select a code for this intervention.");
				return;
			}
			else {
				codeVal=listCode[gridMain.GetSelectedIndex()].CodeValue;
				codeSys=listCode[gridMain.GetSelectedIndex()].CodeSystem;
				Description=gridMain.Rows[gridMain.GetSelectedIndex()].Cells[2].Text;
			}
			//save--------------------------------------
			InterventionCur.DateTimeEntry=date;
			InterventionCur.CodeValue=codeVal;
			InterventionCur.CodeSystem=codeSys;
			InterventionCur.Note=textNote.Text;
			InterventionCur.CodeSet=(InterventionCodeSet)comboCodeSet.SelectedIndex;
			if(InterventionCur.IsNew) {
				InterventionCur.InterventionNum=Interventions.Insert(InterventionCur);
				InterventionCur.DateTimeEntry=date;
				Interventions.Update(InterventionCur);
			}
			else {
				Interventions.Update(InterventionCur);
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}
	}
}