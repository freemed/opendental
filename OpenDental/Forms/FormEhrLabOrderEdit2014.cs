using System;
using System.Windows.Forms;
using OpenDentBusiness;
using OpenDental.UI;
using System.Collections.Generic;
using EhrLaboratories;

namespace OpenDental {
	public partial class FormEhrLabOrderEdit2014:Form {
		long PatCurNum;
		public EhrLab EhrLabCur;
		private EhrLab ParentLab;
		public bool IsNew;
		public bool IsImport;
		public bool IsViewOnly;

		public FormEhrLabOrderEdit2014() {
			InitializeComponent();
		}

		private void FormLabPanelEdit_Load(object sender,EventArgs e) {
			Height=System.Windows.Forms.Screen.GetWorkingArea(this).Height;
			this.SetDesktopLocation(DesktopLocation.X,0);
			if(IsImport || IsViewOnly) {
				foreach(Control c in this.Controls) {
					c.Enabled=false;
				}
				butViewParent.Enabled=true;
				gridMain.Enabled=true;
				gridNotes.Enabled=true;
				gridSpecimen.Enabled=true;
				butCancel.Text="Close";
				butCancel.Enabled=true;
				//butAddNote.Enabled=false;
				//butAddCopyTo.Enabled=false;
				//butAddClinicalInfo.Enabled=false;
				//butAdd.Enabled=false;
				//butAddSpecimens.Enabled=false;
				//butPatientPick.Enabled=false;
				//textName.Enabled=false;
				//butProvPicker.Enabled=false;
				//butServicePicker.Enabled=false;
				//butParentPicker.Enabled=false;
				//butDelete.Enabled=false;
				////combos
				//comboOrderingProvIdType.Enabled=false;
				//comboOrderingProvNameType.Enabled=false;
				//comboResultStatus.Enabled=false;
				//comboSpecimenActionCode.Enabled=false;
				//TODO:hide the rest of the controls that shouldn't show if importing or view only.
			}
			//Parent Result
			if(EhrLabCur.ParentPlacerOrderNum!="") {
				textParentOrderNum.Text=EhrLabCur.ParentPlacerOrderNum;
			}
			else if(EhrLabCur.ParentFillerOrderNum!="") {
				textParentOrderNum.Text=EhrLabCur.ParentFillerOrderNum;
			}
			FillOrderNums();
			FillProvInfo();
			//Results Handling
			checkResultsHandlingF.Checked=EhrLabCur.ListEhrLabResultsHandlingF;
			checkResultsHandlingN.Checked=EhrLabCur.ListEhrLabResultsHandlingN;
			//UpdateTime
			textResultDateTime.Text=EhrLab.formatDateFromHL7(EhrLabCur.ResultDateTime);
			FillUsi();//Service Identifier
			FillGrid();//LabResults
			//TQ1
			textTQ1Start.Text=EhrLab.formatDateFromHL7(EhrLabCur.TQ1DateTimeStart);
			textTQ1Stop.Text=EhrLab.formatDateFromHL7(EhrLabCur.TQ1DateTimeEnd);
			FillGridNotes();
			FillGridResultsCopyTo();
			FillGridClinicalInformation();
			FillGridSpecimen();
			//Specimen Action Code
			FillComboSpecimenActionCode();
			//Result Status
			FillComboResultStatus();
		}

		private void FillComboSpecimenActionCode() {
			comboSpecimenActionCode.Items.Clear();
			comboSpecimenActionCode.BeginUpdate();
			List<string> listSpecActionCodes=EhrLabs.GetHL70065Descriptions();
			comboSpecimenActionCode.Items.AddRange(listSpecActionCodes.ToArray());
			comboSpecimenActionCode.EndUpdate();
			comboSpecimenActionCode.SelectedIndex=(int)Enum.Parse(typeof(HL70065),EhrLabCur.SpecimenActionCode.ToString(),true)+1;
		}

		private void FillComboResultStatus() {
			comboResultStatus.Items.Clear();
			comboResultStatus.BeginUpdate();
			List<string> listResStatCodes=EhrLabs.GetHL70123Descriptions();
			comboResultStatus.Items.AddRange(listResStatCodes.ToArray());
			comboResultStatus.EndUpdate();
			comboResultStatus.SelectedIndex=(int)Enum.Parse(typeof(HL70123),EhrLabCur.ResultStatus.ToString(),true)+1;
		}

		private void FillUsi() {
			textUsiID.Text=EhrLabCur.UsiID;
			textUsiText.Text=EhrLabCur.UsiText;
			textUsiCodeSystemName.Text=EhrLabCur.UsiCodeSystemName;
			textUsiIDAlt.Text=EhrLabCur.UsiIDAlt;
			textUsiTextAlt.Text=EhrLabCur.UsiTextAlt;
			textUsiCodeSystemNameAlt.Text=EhrLabCur.UsiCodeSystemNameAlt;
			textUsiTextOriginal.Text=EhrLabCur.UsiTextOriginal;
		}

		private void FillProvInfo() {
			textOrderingProvIdentifier.Text=EhrLabCur.OrderingProviderID;
			textOrderingProvLastName.Text=EhrLabCur.OrderingProviderLName;
			textOrderingProvFirstName.Text=EhrLabCur.OrderingProviderFName;
			textOrderingProvMiddleName.Text=EhrLabCur.OrderingProviderMiddleNames;
			textOrderingProvSuffix.Text=EhrLabCur.OrderingProviderSuffix;
			textOrderingProvPrefix.Text=EhrLabCur.OrderingProviderPrefix;
			textOrderingProvAANID.Text=EhrLabCur.OrderingProviderAssigningAuthorityNamespaceID;
			textOrderingProvAAUID.Text=EhrLabCur.OrderingProviderAssigningAuthorityUniversalID;
			textOrderingProvAAUIDType.Text=EhrLabCur.OrderingProviderAssigningAuthorityIDType;
			#region Name Type
			comboOrderingProvNameType.Items.Clear();
			comboOrderingProvNameType.BeginUpdate();
			//Fill medical director name combo with HL70200 enum.  Not sure if blank is acceptable.
			List<string> listOrderingProvNameType=EhrLabResults.GetHL70200Descriptions();
			comboOrderingProvNameType.Items.AddRange(listOrderingProvNameType.ToArray());
			comboOrderingProvNameType.EndUpdate();
			comboOrderingProvNameType.SelectedIndex=(int)Enum.Parse(typeof(HL70200),EhrLabCur.OrderingProviderNameTypeCode.ToString(),true)+1;
			#endregion
			#region Identifier Type
			comboOrderingProvIdType.Items.Clear();
			comboOrderingProvIdType.BeginUpdate();
			//Fill medical director type combo with HL70203 enum.  Not sure if blank is acceptable.
			List<string> listOrderingProvIdType=EhrLabs.GetHL70203Descriptions();
			comboOrderingProvIdType.Items.AddRange(listOrderingProvIdType.ToArray());
			comboOrderingProvIdType.EndUpdate();
			comboOrderingProvIdType.SelectedIndex=(int)Enum.Parse(typeof(HL70203),EhrLabCur.OrderingProviderIdentifierTypeCode.ToString(),true)+1;
			#endregion
		}

		private void FillOrderNums() {
			//Placer Order Num
			textPlacerOrderNum.Text=EhrLabCur.PlacerOrderNum;
			textPlacerOrderNamespace.Text=EhrLabCur.PlacerOrderNamespace;
			textPlacerOrderUniversalID.Text=EhrLabCur.PlacerOrderUniversalID;
			textPlacerOrderUniversalIDType.Text=EhrLabCur.PlacerOrderUniversalIDType;
			//Placer Order Group Num
			textPlacerGroupNum.Text=EhrLabCur.PlacerGroupNum;
			textPlacerGroupNamespace.Text=EhrLabCur.PlacerGroupNamespace;
			textPlacerGroupUniversalID.Text=EhrLabCur.PlacerGroupUniversalID;
			textPlacerGroupUniversalIDType.Text=EhrLabCur.PlacerGroupUniversalIDType;
			//Filler Order Num
			textFillerOrderNum.Text=EhrLabCur.FillerOrderNum;
			textFillerOrderNamespace.Text=EhrLabCur.FillerOrderNamespace;
			textFillerOrderUniversalID.Text=EhrLabCur.FillerOrderUniversalID;
			textFillerOrderUniversalIDType.Text=EhrLabCur.FillerOrderUniversalIDType;
			return;
		}
	
		///<summary>Lab Results</summary>
		private void FillGrid() {
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col;
			if(Security.IsAuthorized(Permissions.EhrInfoButton,true)) {
				col=new ODGridColumn("",18);//infoButton
				col.ImageList=imageListInfoButton;
				gridMain.Columns.Add(col);
			}
			col=new ODGridColumn("Test Date",70);
			col.SortingStrategy=GridSortingStrategy.DateParse;
			gridMain.Columns.Add(col);
			col=new ODGridColumn("LOINC",60);//LoincCode
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Test Performed",230);//ShortDescription
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Result Value",160);//Complicated
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Units",60);
			gridMain.Columns.Add(col);
			col=new ODGridColumn("Flags",40);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<EhrLabCur.ListEhrLabResults.Count;i++) {
				row=new ODGridRow();
				if(Security.IsAuthorized(Permissions.EhrInfoButton,true)) {
					row.Cells.Add("0");//index of infobutton
				}
				string dateSt=EhrLabCur.ListEhrLabResults[i].ObservationDateTime.Substring(0,8);//stored in DB as yyyyMMdd[hh[mm[ss]]], []==optional components
				DateTime dateT=PIn.Date(dateSt.Substring(4,2)+"/"+dateSt.Substring(6,2)+"/"+dateSt.Substring(0,4));
				row.Cells.Add(dateT.ToShortDateString());//date only
				if(EhrLabCur.ListEhrLabResults[i].ObservationIdentifierID!="") {
					row.Cells.Add(EhrLabCur.ListEhrLabResults[i].ObservationIdentifierID);
					row.Cells.Add(EhrLabCur.ListEhrLabResults[i].ObservationIdentifierText);
				}
				else if(EhrLabCur.ListEhrLabResults[i].ObservationIdentifierIDAlt!="") {
					row.Cells.Add(EhrLabCur.ListEhrLabResults[i].ObservationIdentifierIDAlt);
					row.Cells.Add(EhrLabCur.ListEhrLabResults[i].ObservationIdentifierTextAlt);
				}
				else {
					row.Cells.Add("UNK");
					row.Cells.Add("Unknown, could not find valid test code.");
				}
				switch(EhrLabCur.ListEhrLabResults[i].ValueType) {
					case HL70125.CE:
					case HL70125.CWE:
						row.Cells.Add(EhrLabCur.ListEhrLabResults[i].ObservationValueCodedElementText);
						break;
					case HL70125.DT:
					case HL70125.TS:
						row.Cells.Add(EhrLabCur.ListEhrLabResults[i].ObservationValueDateTime);
						break;
					case HL70125.TM:
						row.Cells.Add(EhrLabCur.ListEhrLabResults[i].ObservationValueTime.ToString());
						break;
					case HL70125.NM:
						row.Cells.Add(EhrLabCur.ListEhrLabResults[i].ObservationValueNumeric.ToString());
						break;
					case HL70125.SN:
						row.Cells.Add(
							EhrLabCur.ListEhrLabResults[i].ObservationValueComparator
							+EhrLabCur.ListEhrLabResults[i].ObservationValueNumber1
							+(EhrLabCur.ListEhrLabResults[i].ObservationValueSeparatorOrSuffix==""
									?"":EhrLabCur.ListEhrLabResults[i].ObservationValueSeparatorOrSuffix+EhrLabCur.ListEhrLabResults[i].ObservationValueNumber2)
							);
						break;
					case HL70125.FT:
					case HL70125.ST:
					case HL70125.TX:
						row.Cells.Add(EhrLabCur.ListEhrLabResults[i].ObservationValueText);
						break;
				}
				row.Cells.Add(EhrLabCur.ListEhrLabResults[i].UnitsID);
				row.Cells.Add(EhrLabCur.ListEhrLabResults[i].AbnormalFlags.Replace("N",""));//abnormal flags, show blank if flag is "Normal"
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
		}

		///<summary>Lab Result Notes. Currently includes notes for results too... TODO: seperate notes for labs and results.</summary>
		private void FillGridNotes() {
			gridNotes.BeginUpdate();
			gridNotes.Columns.Clear();
			ODGridColumn col=new ODGridColumn("Note Num",60);
			gridNotes.Columns.Add(col);
			col=new ODGridColumn("Comments",300);
			gridNotes.Columns.Add(col);
			gridNotes.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<EhrLabCur.ListEhrLabNotes.Count;i++) {
				for(int j=0;j<EhrLabCur.ListEhrLabNotes[i].Comments.Split('^').Length;j++) {
					row=new ODGridRow();
					row.Cells.Add((j==0?(i+1).ToString():""));//add note number if this is first comment for the note, otherwise add blank cell.
					row.Cells.Add(EhrLabCur.ListEhrLabNotes[i].Comments.Split('^')[j]);//Add each comment.
					gridNotes.Rows.Add(row);
				}
			}
			gridNotes.EndUpdate();
		}

		///<summary></summary>
		private void FillGridResultsCopyTo() {
			gridResultsCopyTo.BeginUpdate();
			gridResultsCopyTo.Columns.Clear();
			ODGridColumn col=new ODGridColumn("Name",60);
			gridResultsCopyTo.Columns.Add(col);
			gridResultsCopyTo.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<EhrLabCur.ListEhrLabResultsCopyTo.Count;i++) {
				row=new ODGridRow();
				row.Cells.Add(EhrLabCur.ListEhrLabResultsCopyTo[i].CopyToPrefix+" "
					+EhrLabCur.ListEhrLabResultsCopyTo[i].CopyToFName+" "
					+EhrLabCur.ListEhrLabResultsCopyTo[i].CopyToLName+" "
					+EhrLabCur.ListEhrLabResultsCopyTo[i].CopyToSuffix);
				//TODO: Make this neater. Will display extra spaces if missing prefix suffix or middle names.
				gridResultsCopyTo.Rows.Add(row);
			}
			gridResultsCopyTo.EndUpdate();
		}

		private void FillGridClinicalInformation() {
			gridClinicalInformation.BeginUpdate();
			gridClinicalInformation.Columns.Clear();
			ODGridColumn col=new ODGridColumn("",60);//arbitrary width, only column in grid.
			gridClinicalInformation.Columns.Add(col);
			gridClinicalInformation.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<EhrLabCur.ListRelevantClinicalInformations.Count;i++) {
				row=new ODGridRow();
				row.Cells.Add(EhrLabCur.ListRelevantClinicalInformations[i].ClinicalInfoText);//may be blank, if so, check the "alt" text
				gridClinicalInformation.Rows.Add(row);
			}
			gridClinicalInformation.EndUpdate();
		}

		///<summary>Lab Results</summary>
		private void FillGridSpecimen() {
			gridSpecimen.BeginUpdate();
			gridSpecimen.Columns.Clear();
			ODGridColumn col=new ODGridColumn("Rej",30,HorizontalAlignment.Center);//arbitrary width, only column in grid.
			gridSpecimen.Columns.Add(col);
			col=new ODGridColumn("Specimen Type",60);//arbitrary width, only column in grid.
			gridSpecimen.Columns.Add(col);
			gridSpecimen.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<EhrLabCur.ListEhrLabSpecimens.Count;i++) {
				row=new ODGridRow();
				row.Cells.Add((EhrLabCur.ListEhrLabSpecimens[i].ListEhrLabSpecimenRejectReason.Count==0?"":"X"));//X is specimen rejected.
				row.Cells.Add(EhrLabCur.ListEhrLabSpecimens[i].SpecimenTypeText);//may be blank, if so, check the "alt" text
				gridSpecimen.Rows.Add(row);
			}
			gridSpecimen.EndUpdate();
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			FormEhrLabResultEdit2014 FormLRE=new FormEhrLabResultEdit2014();
			FormLRE.EhrLabResultCur=EhrLabCur.ListEhrLabResults[e.Row];
			FormLRE.IsImport=IsImport;
			FormLRE.IsViewOnly=IsViewOnly;
			FormLRE.ShowDialog();
			if(IsImport || IsViewOnly || FormLRE.DialogResult!=DialogResult.OK) {
				return;
			}
			EhrLabCur.ListEhrLabResults[e.Row]=FormLRE.EhrLabResultCur;
			FillGrid();
		}

		private void butAdd_Click(object sender,EventArgs e) {
			FormEhrLabResultEdit2014 FormLRE=new FormEhrLabResultEdit2014();
			FormLRE.EhrLabResultCur=new EhrLabResult();
			FormLRE.EhrLabResultCur.IsNew=true;
			FormLRE.ShowDialog();
			if(FormLRE.DialogResult!=DialogResult.OK) {
				return;
			}
			EhrLabCur.ListEhrLabResults.Add(FormLRE.EhrLabResultCur);
			FillGrid();
		}

		private void butAddNote_Click(object sender,EventArgs e) {
			FormEhrLabNoteEdit FormLNE=new FormEhrLabNoteEdit();
			FormLNE.LabNoteCur=new EhrLabNote();
			FormLNE.ShowDialog();
			if(FormLNE.DialogResult!=DialogResult.OK) {
				return;
			}
			FormLNE.LabNoteCur.EhrLabNum=EhrLabCur.EhrLabNum;
			EhrLabCur.ListEhrLabNotes.Add(FormLNE.LabNoteCur);
			FillGridNotes();
		}

		private void gridNotes_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			FormEhrLabNoteEdit FormLNE=new FormEhrLabNoteEdit();
			FormLNE.IsViewOnly=IsViewOnly;
			FormLNE.IsImport=IsImport;
			FormLNE.LabNoteCur=EhrLabCur.ListEhrLabNotes[e.Row];
			FormLNE.ShowDialog();
			if(FormLNE.DialogResult!=DialogResult.OK) {
				return;
			}
			EhrLabCur.ListEhrLabNotes[e.Row]=FormLNE.LabNoteCur;
			FillGridNotes();
		}

		private void butDelete_Click(object sender,EventArgs e) {
			if(!MsgBox.Show(this,MsgBoxButtons.OKCancel,"This will delete the entire lab order and all attached lab results. This cannot be undone. Would you like to continue?")) {
				return;
			}
			EhrLabs.Delete(EhrLabCur.EhrLabNum);
			DialogResult=DialogResult.OK;
		}

		private void butViewParent_Click(object sender,EventArgs e) {
			EhrLab ehrLabParent=null;
			ehrLabParent=EhrLabs.GetByGUID(EhrLabCur.ParentPlacerOrderUniversalID,EhrLabCur.ParentPlacerOrderNum);
			if(ehrLabParent==null) {
				ehrLabParent=EhrLabs.GetByGUID(EhrLabCur.ParentFillerOrderUniversalID,EhrLabCur.ParentFillerOrderNum);
			}
			if(ehrLabParent==null) {
				return;
			}
			FormEhrLabOrderEdit2014 FormELOE=new FormEhrLabOrderEdit2014();
			FormELOE.EhrLabCur=ehrLabParent;
			FormELOE.IsViewOnly=true;
			FormELOE.Text=Lan.g(this,"Parent Lab Order - READ ONLY");
			FormELOE.ShowDialog();
		}

		///<summary></summary>
		private bool EntriesAreValid() {
			//TODO: validate the controls
			return true;
		}

		private void gridMain_CellClick(object sender,ODGridClickEventArgs e) {
			if(!Security.IsAuthorized(Permissions.EhrInfoButton,true)) {
				return;
			}
			if(e.Col!=0) {
				return;
			}
			FormInfobutton FormIB=new FormInfobutton();
			if(PatCurNum!=null && PatCurNum>0) {
				FormIB.PatCur=Patients.GetPat(PatCurNum);
			}
			FormIB.ListObjects.Add(EhrLabCur.ListEhrLabResults[e.Row]);
			FormIB.ShowDialog();
		}

		private void gridSpecimen_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			FormEhrLabSpecimenEdit FormLSE=new FormEhrLabSpecimenEdit();
			FormLSE.EhrLabSpecimenCur=EhrLabCur.ListEhrLabSpecimens[e.Row];
			FormLSE.ShowDialog();
		}

		private void butOk_Click(object sender,EventArgs e) {
			if(IsImport || IsViewOnly) {
				DialogResult=DialogResult.OK;
				return;
			}
			if(!EntriesAreValid()) {
				return;
			}
			if(EhrLabCur.PatNum==0 && PatCurNum!=null) {
				EhrLabCur.PatNum=PatCurNum;
			}
			//EhrLabCur.OrderControlCode=((HL70119)comb);//TODO:UI and this value.
			EhrLabCur.PlacerOrderNum=textPlacerOrderNum.Text;
			EhrLabCur.PlacerOrderNamespace=textPlacerOrderNamespace.Text;
			EhrLabCur.PlacerOrderUniversalID=textPlacerOrderUniversalID.Text;
			EhrLabCur.PlacerOrderUniversalIDType=textPlacerOrderUniversalIDType.Text;
			EhrLabCur.FillerOrderNum=textFillerOrderNum.Text;
			EhrLabCur.FillerOrderNamespace=textFillerOrderNamespace.Text;
			EhrLabCur.FillerOrderUniversalID=textFillerOrderUniversalID.Text;
			EhrLabCur.FillerOrderUniversalIDType=textFillerOrderUniversalIDType.Text;
			EhrLabCur.PlacerGroupNum=textPlacerGroupNum.Text;
			EhrLabCur.PlacerGroupNamespace=textPlacerGroupNamespace.Text;
			EhrLabCur.PlacerGroupUniversalID=textPlacerGroupUniversalID.Text;
			EhrLabCur.PlacerGroupUniversalIDType=textPlacerGroupUniversalIDType.Text;
			EhrLabCur.OrderingProviderID=textOrderingProvIdentifier.Text;
			EhrLabCur.OrderingProviderLName=textOrderingProvLastName.Text;
			EhrLabCur.OrderingProviderFName=textOrderingProvFirstName.Text;
			EhrLabCur.OrderingProviderMiddleNames=textOrderingProvMiddleName.Text;
			EhrLabCur.OrderingProviderSuffix=textOrderingProvSuffix.Text;
			EhrLabCur.OrderingProviderPrefix=textOrderingProvPrefix.Text;
			EhrLabCur.OrderingProviderAssigningAuthorityNamespaceID=textOrderingProvAANID.Text;
			EhrLabCur.OrderingProviderAssigningAuthorityUniversalID=textOrderingProvAAUID.Text;
			EhrLabCur.OrderingProviderAssigningAuthorityIDType=textOrderingProvAAUIDType.Text;
			EhrLabCur.OrderingProviderNameTypeCode=((HL70200)comboOrderingProvNameType.SelectedIndex-1);
			EhrLabCur.OrderingProviderIdentifierTypeCode=((HL70203)comboOrderingProvIdType.SelectedIndex-1);
			//EhrLabCur.SetIdOBR=PIn.Long("");//TODO: UI and Save
			EhrLabCur.UsiID=textUsiID.Text;
			EhrLabCur.UsiText=textUsiText.Text;
			EhrLabCur.UsiCodeSystemName=textUsiCodeSystemName.Text;
			EhrLabCur.UsiIDAlt=textUsiIDAlt.Text;
			EhrLabCur.UsiTextAlt=textUsiTextAlt.Text;
			EhrLabCur.UsiCodeSystemNameAlt=textUsiCodeSystemNameAlt.Text;
			EhrLabCur.UsiTextOriginal=textUsiTextOriginal.Text;
			//EhrLabCur.ObservationDateTimeStart=;//TODO: UI and Save
			//EhrLabCur.ObservationDateTimeEnd=//TODO: UI and Save
			EhrLabCur.SpecimenActionCode=((HL70065)comboSpecimenActionCode.SelectedIndex-1);
			EhrLabCur.ResultDateTime=EhrLab.formatDateToHL7(textResultDateTime.Text);//upper right hand corner of form.
			EhrLabCur.ResultStatus=((HL70123)comboResultStatus.SelectedIndex-1);
			//TODO: parent result.
			/*
			EhrLabCur.ParentObservationID=
			EhrLabCur.ParentObservationText=
			EhrLabCur.ParentObservationCodeSystemName=
			EhrLabCur.ParentObservationIDAlt=
			EhrLabCur.ParentObservationTextAlt=
			EhrLabCur.ParentObservationCodeSystemNameAlt=
			EhrLabCur.ParentObservationTextOriginal=
			EhrLabCur.ParentObservationSubID=
			EhrLabCur.ParentPlacerOrderNum=
			EhrLabCur.ParentPlacerOrderNamespace=
			EhrLabCur.ParentPlacerOrderUniversalID=
			EhrLabCur.ParentPlacerOrderUniversalIDType=
			EhrLabCur.ParentFillerOrderNum=
			EhrLabCur.ParentFillerOrderNamespace=
			EhrLabCur.ParentFillerOrderUniversalID=
			EhrLabCur.ParentFillerOrderUniversalIDType=
			*/
			EhrLabCur.ListEhrLabResultsHandlingF=checkResultsHandlingF.Checked;
			EhrLabCur.ListEhrLabResultsHandlingN=checkResultsHandlingN.Checked;
			//EhrLabCur.TQ1SetId=//TODO:this
			EhrLabCur.TQ1DateTimeStart=EhrLab.formatDateToHL7(textTQ1Start.Text);
			EhrLabCur.TQ1DateTimeEnd=EhrLab.formatDateToHL7(textTQ1Stop.Text);
			EhrLabs.SaveToDB(EhrLabCur);
			Patient patCur=Patients.GetPat(EhrLabCur.PatNum);
			for(int i=0;i<EhrLabCur.ListEhrLabResults.Count;i++) {
				if(CDSPermissions.GetForUser(Security.CurUser.UserNum).ShowCDS && CDSPermissions.GetForUser(Security.CurUser.UserNum).LabTestCDS) {
					FormCDSIntervention FormCDSI=new FormCDSIntervention();
					FormCDSI.ListCDSI=EhrTriggers.TriggerMatch(EhrLabCur.ListEhrLabResults[i],patCur);
					FormCDSI.ShowIfRequired(false);
				}
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		

	}
}
