using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.UI;

namespace OpenDental {
	public partial class FormPatListEHR2014:Form {
		private List<EhrPatListElement2014> _elementList;

		public FormPatListEHR2014() {
			InitializeComponent();
		}

		private void FormPatListEHR2014_Load(object sender,EventArgs e) {
			_elementList=new List<EhrPatListElement2014>();
		}

		private void FillGrid() {
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col;
			col=new ODGridColumn("Restriction",70);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Compare string",120);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Operand",120);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Lab value",80);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("After Date",120);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Before Date",120);
			gridMain.Columns.Add(col);
			//col=new ODGridColumn("Order",120,HorizontalAlignment.Center);
			//gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<_elementList.Count;i++) {
				row=new ODGridRow();
				row.Cells.Add(_elementList[i].Restriction.ToString());
				if(_elementList[i].Restriction==EhrRestrictionType.Problem) {
					if(Snomeds.CodeExists(_elementList[i].CompareString)) {
						row.Cells.Add(_elementList[i].CompareString+" - "+Snomeds.GetByCode(_elementList[i].CompareString).Description);
					}
					else {
						row.Cells.Add(_elementList[i].CompareString+" - NON-SNOMED CODE");
					}
				}
				else {
					row.Cells.Add(_elementList[i].CompareString);
				}
				if(_elementList[i].Restriction==EhrRestrictionType.Gender
					|| _elementList[i].Restriction==EhrRestrictionType.Problem
					|| _elementList[i].Restriction==EhrRestrictionType.Medication
					|| _elementList[i].Restriction==EhrRestrictionType.CommPref
					|| _elementList[i].Restriction==EhrRestrictionType.Allergy) 
				{
					row.Cells.Add("");
				}
				else {
					row.Cells.Add(_elementList[i].Operand.ToString());
				}
				row.Cells.Add(_elementList[i].LabValue);
				if(_elementList[i].StartDate.Year>1880) {
					row.Cells.Add(_elementList[i].StartDate.ToShortDateString());
				}
				else {
					row.Cells.Add("");
				}
				if(_elementList[i].EndDate.Year>1880) {
					row.Cells.Add(_elementList[i].EndDate.ToShortDateString());
				}
				else {
					row.Cells.Add("");
				}
				//if(ElementList[i].OrderBy) {
				//  row.Cells.Add("X");
				//}
				//else {
				//  row.Cells.Add("");
				//}
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}

		private void AddElement(EhrRestrictionType restriction) {
			FormPatListElementEditEHR2014 FormPLEE=new FormPatListElementEditEHR2014();
			FormPLEE.Element=new EhrPatListElement2014();
			FormPLEE.Element.Restriction=restriction;
			FormPLEE.IsNew=true;
			FormPLEE.ShowDialog();
			if(FormPLEE.DialogResult==DialogResult.OK) {
				_elementList.Add(FormPLEE.Element);
			}
			FillGrid();
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			int index=gridMain.GetSelectedIndex();
			if(index==-1) {
				MessageBox.Show("Please select a data element first.");
				return;
			}
			FormPatListElementEditEHR2014 FormPLEE=new FormPatListElementEditEHR2014();
			FormPLEE.Element=_elementList[index];
			FormPLEE.ShowDialog();
			if(FormPLEE.DialogResult==DialogResult.Cancel && FormPLEE.Delete) {
				_elementList.Remove(_elementList[index]);
			}
			FillGrid();
		}

		private void butResults_Click(object sender,EventArgs e) {
			if(gridMain.Rows.Count<1) {
				MessageBox.Show(Lans.g(this,"Please add a data element."));
				return;
			}
			//bool hasOrder=false;
			//for(int i=0;i<ElementList.Count;i++) {
			//  if(hasOrder && ElementList[i].OrderBy) {
			//    MessageBox.Show(Lans.g(this,"You can only 'Order By' exactly one data element."));
			//    return;
			//  }
			//  if(ElementList[i].OrderBy) {
			//    hasOrder=true;
			//  }
			//}
			FormPatListResultsEHR2014 FormPLR14=new FormPatListResultsEHR2014(_elementList);
			FormPLR14.ShowDialog();
		}

		private void butClear_Click(object sender,EventArgs e) {
			_elementList.Clear();
			FillGrid();
		}

		private void butBirthdate_Click(object sender,EventArgs e) {
			AddElement(EhrRestrictionType.Birthdate);
		}

		private void butDisease_Click(object sender,EventArgs e) {
			AddElement(EhrRestrictionType.Problem);
		}

		private void butMedication_Click(object sender,EventArgs e) {
			AddElement(EhrRestrictionType.Medication);
		}

		private void butLabResult_Click(object sender,EventArgs e) {
			AddElement(EhrRestrictionType.LabResult);
		}

		private void butGender_Click(object sender,EventArgs e) {
			AddElement(EhrRestrictionType.Gender);
		}

		private void butAllergy_Click(object sender,EventArgs e) {
			AddElement(EhrRestrictionType.Allergy);
		}

		private void butCommPref_Click(object sender,EventArgs e) {
			AddElement(EhrRestrictionType.CommPref);
		}

		private void butOK_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.OK;
		}

		private void butClose_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}
	}
}
