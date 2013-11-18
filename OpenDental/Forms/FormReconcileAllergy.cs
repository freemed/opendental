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
	public partial class FormReconcileAllergy:Form {
		public List<AllergyDef> ListAllergyDefNew;
		public List<Allergy> ListAllergyNew;
		private List<Allergy> _listAllergyReconcile;
		private List<AllergyDef> _listAllergyDefCur;
		private List<Allergy> _listAllergyCur;
		public Patient PatCur;

		public FormReconcileAllergy() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormReconcileAllergy_Load(object sender,EventArgs e) {
			PatCur=Patients.GetPat(FormOpenDental.CurPatNum);
			for(int index=0;index<ListAllergyNew.Count;index++) {
				ListAllergyNew[index].PatNum=PatCur.PatNum;
			}
			FillExistingGrid();//Done first so that _listAllergyCur and _listAllergyDefCur are populated.
			_listAllergyReconcile=new List<Allergy>(_listAllergyCur);
			#region Delete after testing
			//-------------------------------Delete after testing
			//ListAllergyNew=new List<Allergy>();
			//Allergy al=new Allergy();
			//al.DateTStamp=DateTime.Now.Subtract(TimeSpan.FromDays(5));
			//al.DateAdverseReaction=DateTime.Now.Subtract(TimeSpan.FromDays(5));
			//al.SnomedReaction="51242-b";
			//al.Reaction="Hives";
			//al.StatusIsActive=true;
			//al.PatNum=PatCur.PatNum;
			//al.IsNew=true;
			//ListAllergyNew.Add(al);
			//al=new Allergy();
			//al.DateTStamp=DateTime.Now.Subtract(TimeSpan.FromDays(2));
			//al.DateAdverseReaction=DateTime.Now.Subtract(TimeSpan.FromDays(2));
			//al.SnomedReaction="66452-b";
			//al.Reaction="Chaffing";
			//al.StatusIsActive=true;
			//al.PatNum=PatCur.PatNum;
			//al.IsNew=true;
			//ListAllergyNew.Add(al);
			//al=new Allergy();
			//al.DateTStamp=DateTime.Now.Subtract(TimeSpan.FromDays(10));
			//al.DateAdverseReaction=DateTime.Now.Subtract(TimeSpan.FromDays(10));
			//al.SnomedReaction="48518475-b";
			//al.Reaction="Shivers";
			//al.StatusIsActive=true;
			//al.PatNum=PatCur.PatNum;
			//al.IsNew=true;
			//ListAllergyNew.Add(al);
			//al=new Allergy();
			//al.DateTStamp=DateTime.Now.Subtract(TimeSpan.FromDays(4));
			//al.DateAdverseReaction=DateTime.Now.Subtract(TimeSpan.FromDays(4));
			//al.SnomedReaction="5984145-b";
			//al.Reaction="Vomiting";
			//al.StatusIsActive=true;
			//al.PatNum=PatCur.PatNum;
			//al.IsNew=true;
			//ListAllergyNew.Add(al);
			//al=new Allergy();
			//al.DateTStamp=DateTime.Now.Subtract(TimeSpan.FromDays(9));
			//al.DateAdverseReaction=DateTime.Now.Subtract(TimeSpan.FromDays(9));
			//al.SnomedReaction="5461238-b";
			//al.Reaction="Swelling";
			//al.StatusIsActive=true;
			//al.PatNum=PatCur.PatNum;
			//al.IsNew=true;
			//ListAllergyNew.Add(al);
			//al=new Allergy();
			//al.DateTStamp=DateTime.Now.Subtract(TimeSpan.FromDays(7));
			//al.DateAdverseReaction=DateTime.Now.Subtract(TimeSpan.FromDays(7));
			//al.SnomedReaction="253-b";
			//al.Reaction="Yuck";
			//al.StatusIsActive=true;
			//al.PatNum=PatCur.PatNum;
			//al.IsNew=true;
			//ListAllergyNew.Add(al);
			//al=new Allergy();
			//al.DateTStamp=DateTime.Now.Subtract(TimeSpan.FromDays(12));
			//al.DateAdverseReaction=DateTime.Now.Subtract(TimeSpan.FromDays(12));
			//al.SnomedReaction="45451-b";
			//al.Reaction="Epic Swelling";
			//al.StatusIsActive=true;
			//al.PatNum=PatCur.PatNum;
			//al.IsNew=true;
			//ListAllergyNew.Add(al);
			//al=new Allergy();
			//al.DateTStamp=DateTime.Now.Subtract(TimeSpan.FromDays(11));
			//al.DateAdverseReaction=DateTime.Now.Subtract(TimeSpan.FromDays(11));
			//al.SnomedReaction="511232-b";
			//al.Reaction="Rashes";
			//al.StatusIsActive=true;
			//al.PatNum=PatCur.PatNum;
			//al.IsNew=true;
			//ListAllergyNew.Add(al);
			//al=new Allergy();
			//al.DateTStamp=DateTime.Now.Subtract(TimeSpan.FromDays(8));
			//al.DateAdverseReaction=DateTime.Now.Subtract(TimeSpan.FromDays(8));
			//al.SnomedReaction="986321-b";
			//al.Reaction="Death";
			//al.StatusIsActive=true;
			//al.PatNum=PatCur.PatNum;
			//al.IsNew=true;
			//ListAllergyNew.Add(al);
			//ListAllergyDefNew=new List<AllergyDef>();
			//AllergyDef ald=new AllergyDef();
			//ald.DateTStamp=DateTime.Now.Subtract(TimeSpan.FromDays(5));
			//ald.SnomedAllergyTo="51242";
			//ald.SnomedType=SnomedAllergy.FoodIntolerance;
			//ald.Description="Allergy - Milk";
			//ald.IsNew=true;
			//ListAllergyDefNew.Add(ald);
			//ald=new AllergyDef();
			//ald.DateTStamp=DateTime.Now.Subtract(TimeSpan.FromDays(2));
			//ald.SnomedAllergyTo="66452";
			//ald.SnomedType=SnomedAllergy.DrugIntolerance;
			//ald.Description="Allergy - Ibuprofen";
			//ald.IsNew=true;
			//ListAllergyDefNew.Add(ald);
			//ald=new AllergyDef();
			//ald.DateTStamp=DateTime.Now.Subtract(TimeSpan.FromDays(10));
			//ald.SnomedAllergyTo="48518475";
			//ald.SnomedType=SnomedAllergy.AllergyToSubstance;
			//ald.Description="Allergy - Alcohol";
			//ald.IsNew=true;
			//ListAllergyDefNew.Add(ald);
			//ald=new AllergyDef();
			//ald.DateTStamp=DateTime.Now.Subtract(TimeSpan.FromDays(4));
			//ald.SnomedAllergyTo="5984145";
			//ald.SnomedType=SnomedAllergy.DrugAllergy;
			//ald.Description="Allergy - Morphine";
			//ald.IsNew=true;
			//ListAllergyDefNew.Add(ald);
			//ald=new AllergyDef();
			//ald.DateTStamp=DateTime.Now.Subtract(TimeSpan.FromDays(9));
			//ald.SnomedAllergyTo="5461238";
			//ald.SnomedType=SnomedAllergy.AdverseReactionsToFood;
			//ald.Description="Allergy - Nuts";
			//ald.IsNew=true;
			//ListAllergyDefNew.Add(ald);
			//ald=new AllergyDef();
			//ald.DateTStamp=DateTime.Now.Subtract(TimeSpan.FromDays(7));
			//ald.SnomedAllergyTo="253";
			//ald.SnomedType=SnomedAllergy.FoodAllergy;
			//ald.Description="Allergy - Tomatoes";
			//ald.IsNew=true;
			//ListAllergyDefNew.Add(ald);
			//ald=new AllergyDef();
			//ald.DateTStamp=DateTime.Now.Subtract(TimeSpan.FromDays(12));
			//ald.SnomedAllergyTo="45451";
			//ald.SnomedType=SnomedAllergy.AllergyToSubstance;
			//ald.Description="Allergy - Bees";
			//ald.IsNew=true;
			//ListAllergyDefNew.Add(ald);
			//ald=new AllergyDef();
			//ald.DateTStamp=DateTime.Now.Subtract(TimeSpan.FromDays(11));
			//ald.SnomedAllergyTo="511232";
			//ald.SnomedType=SnomedAllergy.AllergyToSubstance;
			//ald.Description="Allergy - Latex";
			//ald.IsNew=true;
			//ListAllergyDefNew.Add(ald);
			//ald=new AllergyDef();
			//ald.DateTStamp=DateTime.Now.Subtract(TimeSpan.FromDays(8));
			//ald.SnomedAllergyTo="986321";
			//ald.SnomedType=SnomedAllergy.AdverseReactionsToSubstance;
			//ald.Description="Allergy - Air";
			//ald.IsNew=true;
			//ListAllergyDefNew.Add(ald);
			//-------------------------------
			#endregion
			//Automation to initially fill reconcile grid with "recommended" rows.
			bool isValid;
			for(int i=0;i<ListAllergyNew.Count;i++) {
				isValid=true;
				for(int j=0;j<_listAllergyDefCur.Count;j++) {
					if(_listAllergyDefCur[j].SnomedAllergyTo==ListAllergyDefNew[i].SnomedAllergyTo) {//Check SNOMEDS to determine if the Reconcile list already has that SNOMED code
						isValid=false;
						break;
					}
				}
				if(isValid) {
					_listAllergyReconcile.Add(ListAllergyNew[i]);
				}
			}
			FillImportGrid();
			FillReconcileGrid();
		}

		private void FillImportGrid() {
			gridAllergyImport.BeginUpdate();
			gridAllergyImport.Columns.Clear();
			ODGridColumn col=new ODGridColumn("Last Modified",90,HorizontalAlignment.Center);
			gridAllergyImport.Columns.Add(col);
			col=new ODGridColumn("SNOMED",80,HorizontalAlignment.Center);
			gridAllergyImport.Columns.Add(col);
			col=new ODGridColumn("Description",200);
			gridAllergyImport.Columns.Add(col);
			col=new ODGridColumn("Reaction",100);
			gridAllergyImport.Columns.Add(col);
			gridAllergyImport.Rows.Clear();
			ODGridRow row;
			//ListAllergyNew and ListAllergyDefNew should be a 1:1 ratio so we can use the same loop for both.
			for(int i=0;i<ListAllergyNew.Count;i++) {
				row=new ODGridRow();
				row.Cells.Add(DateTime.Now.ToShortDateString());
				if(ListAllergyDefNew[i].SnomedAllergyTo==null) {
					row.Cells.Add("");
				}
				else {
					row.Cells.Add(ListAllergyDefNew[i].SnomedAllergyTo);
				}
				if(ListAllergyDefNew[i].Description==null) {
					row.Cells.Add("");
				}
				else {
					row.Cells.Add(ListAllergyDefNew[i].Description);
				}
				if(ListAllergyNew[i].Reaction==null) {
					row.Cells.Add("");
				}
				else {
					row.Cells.Add(ListAllergyNew[i].Reaction);
				}
				gridAllergyImport.Rows.Add(row);
			}
			gridAllergyImport.EndUpdate();
		}

		private void FillExistingGrid() {
			gridAllergyExisting.BeginUpdate();
			gridAllergyExisting.Columns.Clear();
			ODGridColumn col=new ODGridColumn("Last Modified",90,HorizontalAlignment.Center);
			gridAllergyExisting.Columns.Add(col);
			col=new ODGridColumn("SNOMED",80,HorizontalAlignment.Center);
			gridAllergyExisting.Columns.Add(col);
			col=new ODGridColumn("Description",200);
			gridAllergyExisting.Columns.Add(col);
			col=new ODGridColumn("Reaction",100);
			gridAllergyExisting.Columns.Add(col);
			gridAllergyExisting.Rows.Clear();
			_listAllergyCur=Allergies.GetAll(PatCur.PatNum,false);
			List<long> allergyDefNums=new List<long>();
			for(int h=0;h<_listAllergyCur.Count;h++) {
				if(_listAllergyCur[h].AllergyDefNum > 0) {
					allergyDefNums.Add(_listAllergyCur[h].AllergyDefNum);
				}
			}
			_listAllergyDefCur=AllergyDefs.GetMultAllergyDefs(allergyDefNums);
			ODGridRow row;
			AllergyDef ald;
			for(int i=0;i<_listAllergyCur.Count;i++) {
				row=new ODGridRow();
				ald=new AllergyDef();
				ald=AllergyDefs.GetOne(_listAllergyCur[i].AllergyDefNum,_listAllergyDefCur);
				row.Cells.Add(_listAllergyCur[i].DateTStamp.ToShortDateString());
				if(ald.SnomedAllergyTo==null) {
					row.Cells.Add("");
				}
				else {
					row.Cells.Add(ald.SnomedAllergyTo);
				}
				if(ald.Description==null) {
					row.Cells.Add("");
				}
				else {
					row.Cells.Add(ald.Description);
				}
				if(_listAllergyCur[i].Reaction==null) {
					row.Cells.Add("");
				}
				else {
					row.Cells.Add(_listAllergyCur[i].Reaction);
				}
				gridAllergyExisting.Rows.Add(row);
			}
			gridAllergyExisting.EndUpdate();
		}

		private void FillReconcileGrid() {
			gridAllergyReconcile.BeginUpdate();
			gridAllergyReconcile.Columns.Clear();
			ODGridColumn col=new ODGridColumn("Last Modified",90,HorizontalAlignment.Center);
			gridAllergyReconcile.Columns.Add(col);
			col=new ODGridColumn("SNOMED",80,HorizontalAlignment.Center);
			gridAllergyReconcile.Columns.Add(col);
			col=new ODGridColumn("Description",400);
			gridAllergyReconcile.Columns.Add(col);
			col=new ODGridColumn("Reaction",300);
			gridAllergyReconcile.Columns.Add(col);
			col=new ODGridColumn("Is Incoming",100,HorizontalAlignment.Center);
			gridAllergyReconcile.Columns.Add(col);
			gridAllergyReconcile.Rows.Clear();
			ODGridRow row;
			AllergyDef ald=new AllergyDef();
			for(int i=0;i<_listAllergyReconcile.Count;i++) {
				row=new ODGridRow();
				ald=new AllergyDef();
				for(int j=0;j<_listAllergyDefCur.Count;j++) {
					if(_listAllergyReconcile[i].IsNew) {
						//To find the allergy def for new allergies, get the index of the matching allergy in ListAllergyNew, and use that index in ListAllergyDefNew because they are 1 to 1 lists.
						ald=ListAllergyDefNew[ListAllergyNew.IndexOf(_listAllergyReconcile[i])];
						break;
					}
					if(_listAllergyReconcile[i].AllergyDefNum > 0 && _listAllergyReconcile[i].AllergyDefNum==_listAllergyDefCur[j].AllergyDefNum) {
						ald=_listAllergyDefCur[j];//Gets the allergydef matching the allergy so we can use it to populate the grid
						break;
					}
				}
				row.Cells.Add(DateTime.Now.ToShortDateString());
				if(ald.SnomedAllergyTo==null) {
					row.Cells.Add("");
				}
				else {
					row.Cells.Add(ald.SnomedAllergyTo);
				}
				if(ald.Description==null) {
					row.Cells.Add("");
				}
				else {
					row.Cells.Add(ald.Description);
				}
				if(_listAllergyReconcile[i].Reaction==null) {
					row.Cells.Add("");
				}
				else {
					row.Cells.Add(_listAllergyReconcile[i].Reaction);
				}
				row.Cells.Add(_listAllergyReconcile[i].IsNew?"X":"");
				gridAllergyReconcile.Rows.Add(row);
			}
			gridAllergyReconcile.EndUpdate();
		}

		private void butAddNew_Click(object sender,EventArgs e) {
			if(gridAllergyImport.SelectedIndices.Length==0) {
				MsgBox.Show(this,"A row must be selected to add");
				return;
			}
			Allergy al;
			AllergyDef alD;
			AllergyDef alDR=null;
			int skipCount=0;
			bool isValid;
			for(int i=0;i<gridAllergyImport.SelectedIndices.Length;i++) {
				isValid=true;
				//Since gridAllergyImport and ListAllergyNew are a 1:1 list we can use the selected index position to get our allergy
				al=ListAllergyNew[gridAllergyImport.SelectedIndices[i]];
				alD=ListAllergyDefNew[gridAllergyImport.SelectedIndices[i]];//ListAllergyDefNew is also a 1:1 to gridAllergyImport.
				for(int j=0;j<gridAllergyReconcile.Rows.Count;j++) {
					if(_listAllergyReconcile[j].IsNew) {
						alDR=ListAllergyDefNew[ListAllergyNew.IndexOf(_listAllergyReconcile[j])];
					}
					else {
						alDR=AllergyDefs.GetOne(_listAllergyReconcile[j].AllergyDefNum);
					}
					if(alDR==null) {
						continue;
					}
					if(alDR.SnomedAllergyTo!="" && alDR.SnomedAllergyTo==alD.SnomedAllergyTo) {
						isValid=false;
						skipCount++;
						break;
					}
				}
				if(isValid) {
					_listAllergyReconcile.Add(al);
				}
			}
			if(skipCount>0) {
				MessageBox.Show(Lan.g(this," Row(s) skipped because allergy already present in the reconcile list")+": "+skipCount);
			}
			FillReconcileGrid();
		}

		private void butAddExist_Click(object sender,EventArgs e) {
			if(gridAllergyExisting.SelectedIndices.Length==0) {
				MsgBox.Show(this,"A row must be selected to add");
				return;
			}
			Allergy al;
			AllergyDef alD;
			int skipCount=0;
			bool isValid;
			for(int i=0;i<gridAllergyExisting.SelectedIndices.Length;i++) {
				isValid=true;
				//Since gridAllergyExisting and _listAllergyCur are a 1:1 list we can use the selected index position to get our allergy
				al=_listAllergyCur[gridAllergyExisting.SelectedIndices[i]];
				alD=AllergyDefs.GetOne(al.AllergyDefNum,_listAllergyDefCur);
				for(int j=0;j<_listAllergyCur.Count;j++) {
					if(_listAllergyCur[j].IsNew) {
						for(int k=0;k<ListAllergyDefNew.Count;k++) {
							if(alD.SnomedAllergyTo!="" && alD.SnomedAllergyTo==ListAllergyDefNew[k].SnomedAllergyTo) {
								isValid=false;
								skipCount++;
								break;
							}
						}
					}
					if(!isValid) {
						break;
					}
					if(al.AllergyDefNum==_listAllergyReconcile[j].AllergyDefNum) {
						isValid=false;
						skipCount++;
						break;
					}
				}
				if(isValid) {
					_listAllergyReconcile.Add(al);
				}
			}
			if(skipCount>0) {
				MessageBox.Show(Lan.g(this," Row(s) skipped because allergy already present in the reconcile list")+": "+skipCount);
			}
			FillReconcileGrid();
		}

		private void butRemoveRec_Click(object sender,EventArgs e) {
			if(gridAllergyReconcile.SelectedIndices.Length==0) {
				MsgBox.Show(this,"A row must be selected to remove");
				return;
			}
			Allergy al;
			for(int i=gridAllergyReconcile.SelectedIndices.Length-1;i>-1;i--) {//Loop backwards so that we can remove from the list as we go
				al=_listAllergyReconcile[gridAllergyReconcile.SelectedIndices[i]];
				_listAllergyReconcile.Remove(al);
			}
			FillReconcileGrid();
		}

		private void butOK_Click(object sender,EventArgs e) {
			if(_listAllergyReconcile.Count==0) {
				if(!MsgBox.Show(this,true,"The reconcile list is empty which will cause all existing allergies to be removed.  Continue?")) {
					return;
				}
			}
			Allergy al;
			AllergyDef alD;
			bool isActive;
			//Discontinue any current medications that are not present in the reconcile list.
			for(int i=0;i<_listAllergyCur.Count;i++) {//Start looping through all current allergies
				isActive=false;
				al=_listAllergyCur[i];
				alD=AllergyDefs.GetOne(al.AllergyDefNum,_listAllergyDefCur);
				for(int j=0;j<_listAllergyReconcile.Count;j++) {//Compare each reconcile allergy to the current allergy
					AllergyDef alDR=AllergyDefs.GetOne(_listAllergyReconcile[j].AllergyDefNum,_listAllergyDefCur);
					if(_listAllergyReconcile[j].AllergyDefNum==_listAllergyCur[i].AllergyDefNum) {//Has identical AllergyDefNums
						isActive=true;
						break;
					}
					if(alDR==null) {
						continue;
					}
					if(alDR.SnomedAllergyTo!="" && alDR.SnomedAllergyTo==alD.SnomedAllergyTo) {//Has a Snomed code and they are equal
						isActive=true;
						break;
					}
				}
				if(!isActive) {
					_listAllergyCur[i].StatusIsActive=isActive;
					if(Security.IsAuthorized(Permissions.EhrShowCDS)) {
						FormCDSIntervention FormCDSI=new FormCDSIntervention();
						FormCDSI.DictEhrTriggerResults=EhrTriggers.TriggerMatch(_listAllergyCur[i],Patients.GetPat(FormOpenDental.CurPatNum));
						FormCDSI.ShowIfRequired();
						if(FormCDSI.DialogResult==DialogResult.Cancel) {//using ==DialogResult.Cancel instead of !=DialogResult.OK
							return;//effectively canceling the action.
						}
					}
					Allergies.Update(_listAllergyCur[i]);
				}
			}
			//Always update every current allergy for the patient so that DateTStamp reflects the last reconcile date.
			Allergies.ResetTimeStamps(PatCur.PatNum, true);
			AllergyDef alDU;
			int index;
			for(int j=0;j<_listAllergyReconcile.Count;j++) {
				if(!_listAllergyReconcile[j].IsNew) {//TODO: implement this in the other classes.
					continue;
				}
				index=ListAllergyNew.IndexOf(_listAllergyReconcile[j]);//Returns -1 if not found.
				if(index<0) {
					continue;
				}
				//Insert the AllergyDef and Allergy if needed.
				alDU=AllergyDefs.GetAllergyDefFromCode(ListAllergyDefNew[index].SnomedAllergyTo);//Check if the Def already exists.
				if(alDU==null) {//db is missing the def
					ListAllergyNew[index].AllergyDefNum=AllergyDefs.Insert(ListAllergyDefNew[index]);
				}
				else {
					ListAllergyNew[index].AllergyDefNum=alDU.AllergyDefNum;//Set the allergydefnum on the allergy.
				}
				if(Security.IsAuthorized(Permissions.EhrShowCDS)) {
					FormCDSIntervention FormCDSI=new FormCDSIntervention();
					FormCDSI.DictEhrTriggerResults=EhrTriggers.TriggerMatch(ListAllergyNew[index],PatCur);
					FormCDSI.ShowIfRequired();
					if(FormCDSI.DialogResult==DialogResult.Cancel) {//using ==DialogResult.Cancel instead of !=DialogResult.OK
						return;//effectively canceling the action.
					}
				}
				Allergies.Insert(ListAllergyNew[index]);
			}
			//TODO: Make an allergy measure event if one is needed for MU.
			//EhrMeasureEvent newMeasureEvent = new EhrMeasureEvent();
			//newMeasureEvent.DateTEvent=DateTime.Now;
			//newMeasureEvent.EventType=EhrMeasureEventType.AllergyReconcile;
			//newMeasureEvent.PatNum=PatCur.PatNum;
			//newMeasureEvent.MoreInfo="";
			//EhrMeasureEvents.Insert(newMeasureEvent);
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}
	}
}