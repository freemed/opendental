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
		private List<EhrCode> listCodes;
		private string Description;
		private Dictionary<string,string> dictValueCodeSets;

		public FormInterventionEdit() {
			InitializeComponent();
			Lan.F(this);
		}

		///<summary>If launched from a EHR CQM form we will set the IntervCodeSetIndex based on the situation.  For example, if launched from FormVitalsignEdit2014 due to overweight, we will set IntervCodeSetIndex=InterventionCodeSet.AboveNormalWeight and set IsRecommend=true.  If the user changes the code list to one of the other sub-sets, with IsRecommend=true, we will warn the user that the code will no longer apply to the measure.  If they add an intervention by opening FormInterventions and pressing Add, set IsRecommend=false and IntervCodeSetIndex will default to 0.  We will not warn the user for changing the code set and allow them to enter any of the available intervention codes.</summary>
		private void FormInterventionEdit_Load(object sender,EventArgs e) {
			dictValueCodeSets=new Dictionary<string,string>();
			comboCodeSet.Items.Add("All");
			if(IsAllTypes || InterventionCur.CodeSet==InterventionCodeSet.AboveNormalWeight) {
				comboCodeSet.Items.Add(InterventionCodeSet.AboveNormalWeight.ToString()+" Follow-up");
				dictValueCodeSets.Add("2.16.840.1.113883.3.600.1.1525",InterventionCodeSet.AboveNormalWeight.ToString()+" Follow-up");
				comboCodeSet.Items.Add(InterventionCodeSet.AboveNormalWeight.ToString()+" Referral");
				dictValueCodeSets.Add("2.16.840.1.113883.3.600.1.1527",InterventionCodeSet.AboveNormalWeight.ToString()+" Referral");
				comboCodeSet.Items.Add(InterventionCodeSet.AboveNormalWeight.ToString()+" Medication");
				dictValueCodeSets.Add("2.16.840.1.113883.3.600.1.1498",InterventionCodeSet.AboveNormalWeight.ToString()+" Medication");
			}
			if(IsAllTypes || InterventionCur.CodeSet==InterventionCodeSet.BelowNormalWeight) {
				comboCodeSet.Items.Add(InterventionCodeSet.BelowNormalWeight.ToString()+" Follow-up");
				dictValueCodeSets.Add("2.16.840.1.113883.3.600.1.1528",InterventionCodeSet.BelowNormalWeight.ToString()+" Follow-up");
				comboCodeSet.Items.Add(InterventionCodeSet.BelowNormalWeight.ToString()+" Referral");
				dictValueCodeSets.Add("2.16.840.1.113883.3.600.1.1527",InterventionCodeSet.BelowNormalWeight.ToString()+" Referral");
				comboCodeSet.Items.Add(InterventionCodeSet.BelowNormalWeight.ToString()+" Medication");
				dictValueCodeSets.Add("2.16.840.1.113883.3.600.1.1499",InterventionCodeSet.BelowNormalWeight.ToString()+" Medication");
			}
			if(IsAllTypes || InterventionCur.CodeSet==InterventionCodeSet.Nutrition || InterventionCur.CodeSet==InterventionCodeSet.PhysicalActivity) {
				comboCodeSet.Items.Add(InterventionCodeSet.Nutrition.ToString()+" Counseling");
				dictValueCodeSets.Add("2.16.840.1.113883.3.464.1003.195.12.1003",InterventionCodeSet.Nutrition.ToString()+" Counseling");
				comboCodeSet.Items.Add(InterventionCodeSet.PhysicalActivity.ToString()+" Counseling");
				dictValueCodeSets.Add("2.16.840.1.113883.3.464.1003.118.12.1035",InterventionCodeSet.PhysicalActivity.ToString()+" Counseling");
			}
			if(IsAllTypes || InterventionCur.CodeSet==InterventionCodeSet.TobaccoCessation) {
				comboCodeSet.Items.Add(InterventionCodeSet.TobaccoCessation.ToString()+" Counseling");
				dictValueCodeSets.Add("2.16.840.1.113883.3.526.3.509",InterventionCodeSet.TobaccoCessation.ToString()+" Counseling");
				comboCodeSet.Items.Add(InterventionCodeSet.TobaccoCessation.ToString()+" Medication");
				dictValueCodeSets.Add("2.16.840.1.113883.3.526.3.1190",InterventionCodeSet.TobaccoCessation.ToString()+" Medication");
			}
			if(IsAllTypes || InterventionCur.CodeSet==InterventionCodeSet.Dialysis) {
				comboCodeSet.Items.Add(InterventionCodeSet.Dialysis.ToString()+" Education");
				dictValueCodeSets.Add("2.16.840.1.113883.3.464.1003.109.12.1016",InterventionCodeSet.Dialysis.ToString()+" Education");
				comboCodeSet.Items.Add(InterventionCodeSet.Dialysis.ToString()+" Related Services");
				dictValueCodeSets.Add("2.16.840.1.113883.3.464.1003.109.12.1015",InterventionCodeSet.Dialysis.ToString()+" Related Services");
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
			string selectedValue=comboCodeSet.SelectedItem.ToString();
			List<string> listValSetOIDs=new List<string>();
			if(selectedValue=="All") {
				listValSetOIDs=new List<string>(dictValueCodeSets.Keys);
			}
			else {
				foreach(KeyValuePair<string,string> kvp in dictValueCodeSets) {
					if(kvp.Value==selectedValue) {
						listValSetOIDs.Add(kvp.Key);
					}
				}
			}
			listCodes=EhrCodes.GetForValueSetOIDs(listValSetOIDs,true);//this can be all codes in the available set of codes or one that they selected
			gridMain.Rows.Clear();
			ODGridRow row;
			int selectedIdx=-1;
			for(int i=0;i<listCodes.Count;i++) {
				//We might not be able to display and let the user select codes from systems that require purchasing the code system, like CPT codes.  Might have to hide unless they exist in the CPT (or other code system) table.
				row=new ODGridRow();
				row.Cells.Add(listCodes[i].CodeValue);
				row.Cells.Add(listCodes[i].CodeSystem);
				//Since we require them to select from allowed codes from the table ehrcode we do not need to retrieve the description from the big tables.  If we ever allow the user to select a code from the master list, we will have to get the description from the associated table. (i.e. get the descript from the snomed, icd9, ...etc tables)
				row.Cells.Add(listCodes[i].Description);
				gridMain.Rows.Add(row);
				if(listCodes[i].CodeValue==InterventionCur.CodeValue && listCodes[i].CodeSystem==InterventionCur.CodeSystem) {
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
				codeVal=listCodes[gridMain.GetSelectedIndex()].CodeValue;
				codeSys=listCodes[gridMain.GetSelectedIndex()].CodeSystem;
				Description=gridMain.Rows[gridMain.GetSelectedIndex()].Cells[2].Text;
			}
			//save--------------------------------------
			//Intervention grid may contain medications, have to insert a new med if necessary and load FormMedPat for user to input data
			if(codeSys=="RXNORM") {
				//codeVal will be RxCui of medication, see if it already exists in Medication table
				Medication medCur=Medications.GetMedicationFromDbByRxCui(PIn.Long(codeVal));
				if(medCur==null) {//no med with this RxCui, create one
					medCur=new Medication();
					Medications.Insert(medCur);//so that we will have the primary key
					medCur.GenericNum=medCur.MedicationNum;
					medCur.RxCui=PIn.Long(codeVal);
					medCur.MedName=RxNorms.GetDescByRxCui(codeVal);
					Medications.Update(medCur);
				}
				MedicationPat medPatCur=new MedicationPat();
				medPatCur.PatNum=InterventionCur.PatNum;
				medPatCur.ProvNum=InterventionCur.ProvNum;
				medPatCur.MedicationNum=medCur.MedicationNum;
				medPatCur.RxCui=medCur.RxCui;
				medPatCur.DateStart=date;
				medPatCur.PatNote="Enter instructions for this to be considered a medication order.";
				FormMedPat FormMP=new FormMedPat();
				FormMP.MedicationPatCur=medPatCur;
				FormMP.IsNew=true;
				FormMP.ShowDialog();
				if(FormMP.DialogResult!=DialogResult.OK) {
					return;
				}
				if(FormMP.MedicationPatCur.PatNote=="" || FormMP.MedicationPatCur.DateStart.Year<1880) {
					MsgBox.Show(this,"For the medication just entered to be considerd a medication order for CQM's, it must have a start date and instructions.  You can modify the medication in the patient's medical history window.");
				}
				DialogResult=DialogResult.OK;
			}
			InterventionCur.DateTimeEntry=date;
			InterventionCur.CodeValue=codeVal;
			InterventionCur.CodeSystem=codeSys;
			InterventionCur.Note=textNote.Text;
			if(IsAllTypes) {//CodeSet will be set by calling function unless showing all types, in which case we need to determine which InterventionCodeSet to assign
				string selectedValue=comboCodeSet.SelectedItem.ToString();
				if(selectedValue=="All") {//All types showing and set to All, have to determine which InterventionCodeSet this code belongs to
					foreach(KeyValuePair<string,string> kvp in dictValueCodeSets) {
						List<EhrCode> listCodes=EhrCodes.GetForValueSetOIDs(new List<string> { kvp.Key });
						bool found=false;
						for(int i=0;i<listCodes.Count;i++) {
							if(listCodes[i].CodeValue==codeVal) {
								found=true;
								break;
							}
						}
						if(found) {
							InterventionCur.CodeSet=(InterventionCodeSet)Enum.Parse(typeof(InterventionCodeSet),kvp.Value.Split(new char[' '])[0],true);
							break;
						}
					}
				}
				else {
					for(int i=0;i<Enum.GetNames(typeof(InterventionCodeSet)).Length;i++) {
						if(comboCodeSet.SelectedItem.ToString().StartsWith(((InterventionCodeSet)i).ToString())) {
							InterventionCur.CodeSet=(InterventionCodeSet)i;
							break;
						}
					}
				}
			}
			if(InterventionCur.IsNew) {
				Interventions.Insert(InterventionCur);;
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