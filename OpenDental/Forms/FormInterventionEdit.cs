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
		public bool IsCodeSetLocked;
		List<EhrCode> listCode;
		public string Description;

		public FormInterventionEdit() {
			InitializeComponent();
			Lan.F(this);
		}

		///<summary>If launched from a EHR CQM form we will set the IntervCodeSetIndex based on the situation.  For example, if launched from FormVitalsignEdit2014 due to overweight, we will set IntervCodeSetIndex=InterventionCodeSet.AboveNormalWeight and set IsRecommend=true.  If the user changes the code list to one of the other sub-sets, with IsRecommend=true, we will warn the user that the code will no longer apply to the measure.  If they add an intervention by opening FormInterventions and pressing Add, set IsRecommend=false and IntervCodeSetIndex will default to 0.  We will not warn the user for changing the code set and allow them to enter any of the available intervention codes.</summary>
		private void FormInterventionEdit_Load(object sender,EventArgs e) {
			for(int i=0;i<Enum.GetNames(typeof(InterventionCodeSet)).Length;i++) {
				comboCodeSet.Items.Add(Enum.GetNames(typeof(InterventionCodeSet))[i].ToString());
			}
			comboCodeSet.SelectedIndex=(int)InterventionCur.CodeSet;
			if(IsCodeSetLocked) {
				comboCodeSet.Enabled=false;
			}
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
			List<string> listValueSetOIDs=new List<string>();
			switch(InterventionCur.CodeSet) {
				case InterventionCodeSet.AboveNormalWeight:
					listValueSetOIDs=new List<string> { "2.16.840.1.113883.3.600.1.1525","2.16.840.1.113883.3.600.1.1527" };//'Above Normal Follow-up' and 'Referrals where weight assessment may occur' value sets
					break;
				case InterventionCodeSet.BelowNormalWeight:
					listValueSetOIDs=new List<string> { "2.16.840.1.113883.3.600.1.1528","2.16.840.1.113883.3.600.1.1527" };//'Below Normal Follow up' and 'Referrals where weight assessment may occur' value sets
					break;
				case InterventionCodeSet.TobaccoCessation:
					listValueSetOIDs=new List<string> { "2.16.840.1.113883.3.526.3.509" };//'Tobacco Use Cessation Counseling' value set
					break;
				case InterventionCodeSet.Nutrition:
					listValueSetOIDs=new List<string> { "2.16.840.1.113883.3.464.1003.195.12.1003" };//'Counseling for Nutrition' value set
					break;
				case InterventionCodeSet.PhysicalActivity:
					listValueSetOIDs=new List<string> { "2.16.840.1.113883.3.464.1003.118.12.1035" };//'Counseling for Physical Activity' value set
					break;
				case InterventionCodeSet.Dialysis:
					listValueSetOIDs=new List<string> { "2.16.840.1.113883.3.464.1003.109.12.1016","2.16.840.1.113883.3.464.1003.109.12.1015" };//'Dialysis Education' and 'Other Services Related to Dialysis' value sets
					break;
				default://this index should default to 0, so this should never happen, but just in case load same value set as InterventionCodeSet.AboveNormalWeight
					listValueSetOIDs=new List<string> { "2.16.840.1.113883.3.600.1.1525","2.16.840.1.113883.3.600.1.1527" };
					break;
			}
			listCode=EhrCodes.GetForValueSetOIDs(listValueSetOIDs);
			gridMain.Rows.Clear();
			ODGridRow row;
			int selectedIdx=-1;
			for(int i=0;i<listCode.Count;i++) {
				row=new ODGridRow();
				row.Cells.Add(listCode[i].CodeValue);
				row.Cells.Add(listCode[i].CodeSystem);
				//to get description, first determine which table the code is from.  Interventions are allowed to be SNOMEDCT, ICD9, ICD10, HCPCS, or CPT.
				string descript="";
				switch(listCode[i].CodeSystem) {
					case "SNOMEDCT":
						Snomed sCur=Snomeds.GetByCode(listCode[i].CodeValue);
						if(sCur!=null) {
							descript=sCur.Description;
						}
						break;
					case "ICD9CM":
						ICD9 i9Cur=ICD9s.GetByCode(listCode[i].CodeValue);
						if(i9Cur!=null) {
							descript=i9Cur.Description;
						}
						break;
					case "ICD10CM":
						Icd10 i10Cur=Icd10s.GetByCode(listCode[i].CodeValue);
						if(i10Cur!=null) {
							descript=i10Cur.Description;
						}
						break;
					case "HCPCS":
						Hcpcs hCur=Hcpcses.GetByCode(listCode[i].CodeValue);
						if(hCur!=null) {
							descript=hCur.DescriptionShort;
						}
						break;
					case "CPT":
						//no need to check for null, return new ProcedureCode object if not found, Descript will be blank
						descript=ProcedureCodes.GetProcCode(listCode[i].CodeValue).Descript;
						break;
				}
				row.Cells.Add(descript);
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
			InterventionCur.CodeSet=(InterventionCodeSet)comboCodeSet.SelectedIndex;
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