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
	public partial class FormSheetImport:Form {
		public Sheet SheetCur;
		private List<SheetImportRow> rows;
		//<summary>As the user makes a few choices about how to import certain rows, their choices and edits are stored here.</summary>
		//private List<SheetImportRow> edits;
		private Patient pat;

		public FormSheetImport() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormSheetImport_Load(object sender,EventArgs e) {
			pat=Patients.GetPat(SheetCur.PatNum);
			//edits=new List<SheetImportRow>();
			FillRows();
			FillGrid();
		}

		///<summary>This can only be run once when the form first opens.  After that, the rows are just edited.</summary>
		private void FillRows() {
			rows=new List<SheetImportRow>();
			SheetImportRow row;
			string fieldVal;
			row=new SheetImportRow();
			row.FieldName="Personal";
			row.IsSeparator=true;
			rows.Add(row);
			//LName---------------------------------------------
			fieldVal=GetInputValue("LName");
			if(fieldVal!=null) {
				row=new SheetImportRow();
				row.FieldName="LName";
				row.OldValDisplay=pat.LName;
				row.OldValObj=pat.LName;
				row.NewValDisplay=fieldVal;
				row.NewValObj=row.NewValDisplay;
				row.ObjType=typeof(string);
				if(row.OldValDisplay!=row.NewValDisplay) {
					row.DoImport=true;
				}
				rows.Add(row);
			}
			//FName---------------------------------------------
			fieldVal=GetInputValue("FName");
			if(fieldVal!=null) {
				row=new SheetImportRow();
				row.FieldName="FName";
				row.OldValDisplay=pat.FName;
				row.OldValObj=pat.FName;
				row.NewValDisplay=fieldVal;
				row.NewValObj=row.NewValDisplay;
				row.ObjType=typeof(string);
				if(row.OldValDisplay!=row.NewValDisplay) {
					row.DoImport=true;
				}
				rows.Add(row);
			}
			//MiddleI---------------------------------------------
			fieldVal=GetInputValue("MiddleI");
			if(fieldVal!=null) {
				row=new SheetImportRow();
				row.FieldName="MiddleI";
				row.OldValDisplay=pat.MiddleI;
				row.OldValObj=pat.MiddleI;
				row.NewValDisplay=fieldVal;
				row.NewValObj=row.NewValDisplay;
				row.ObjType=typeof(string);
				if(row.OldValDisplay!=row.NewValDisplay) {
					row.DoImport=true;
				}
				rows.Add(row);
			}
			//Preferred---------------------------------------------
			fieldVal=GetInputValue("Preferred");
			if(fieldVal!=null) {
				row=new SheetImportRow();
				row.FieldName="Preferred";
				row.OldValDisplay=pat.Preferred;
				row.OldValObj=pat.Preferred;
				row.NewValDisplay=fieldVal;
				row.NewValObj=row.NewValDisplay;
				row.ObjType=typeof(string);
				if(row.OldValDisplay!=row.NewValDisplay) {
					row.DoImport=true;
				}
				rows.Add(row);
			}
			//Gender---------------------------------------------
			if(ContainsOneOfFields("GenderIsMale","GenderIsFemale")) {
				PatientGender gender=PatientGender.Unknown;
				if(IsChecked("GenderIsMale")) {
					gender=PatientGender.Male;
				}
				else if(IsChecked("GenderIsFemale")) {
					gender=PatientGender.Female;
				}
				row=new SheetImportRow();
				row.FieldName="Gender";
				row.OldValDisplay=Lan.g("enumPatientGender",pat.Gender.ToString());
				row.OldValObj=pat.Gender;
				if(gender==PatientGender.Unknown) {
					row.NewValDisplay="";
				}
				else {
					row.NewValDisplay=Lan.g("enumPatientGender",gender.ToString());
				}
				row.NewValObj=gender;
				row.ObjType=typeof(PatientGender);
				if(gender!=PatientGender.Unknown
					&& pat.Gender!=gender) 
				{
					row.DoImport=true;
				}
				rows.Add(row);
			}
			//Position---------------------------------------------
			if(ContainsOneOfFields("PositionIsMarried","positionIsNotMarried")) {
				YN isMarried=YN.Unknown;
				if(IsChecked("PositionIsMarried")) {
					isMarried=YN.Yes;
				}
				else if(IsChecked("positionIsNotMarried")) {
					isMarried=YN.No;
				}
				row=new SheetImportRow();
				row.FieldName="Position";
				row.OldValDisplay=Lan.g("enumPatientPosition",pat.Position.ToString());
				row.OldValObj=pat.Position;
				if(isMarried==YN.Unknown) {
					row.NewValObj=null;
					row.NewValDisplay="";
					row.DoImport=false;
				}
				else if(isMarried==YN.Yes) {
					row.NewValObj=PatientPosition.Married;
					row.NewValDisplay=Lan.g("enumPatientPosition",row.NewValObj.ToString());
					if(pat.Position==PatientPosition.Married) {
						row.DoImport=false;
					}
					else {
						row.DoImport=true;
					}
				}
				else {//indicated not married
					if(pat.Position==PatientPosition.Married) {
						row.NewValObj=PatientPosition.Single;
						row.NewValDisplay=Lan.g("enumPatientPosition",row.NewValObj.ToString());
						row.DoImport=true;
					}
					else {//no change needs to be made.
						row.NewValObj=null;
						row.NewValDisplay=Lan.g(this,"NotMarried");
						row.DoImport=false;
					}
				}
				row.ObjType=typeof(PatientPosition);
				rows.Add(row);
			}
			#region temp
			//Birthdate---------------------------------------------
			fieldVal=GetInputValue("Birthdate");
			if(fieldVal!=null) {
				row=new SheetImportRow();
				row.FieldName="Birthdate";
				if(pat.Birthdate.Year<1880) {
					row.OldValDisplay="";
				}
				else {
					row.OldValDisplay=pat.Birthdate.ToShortDateString();
				}
				row.OldValObj=pat.Birthdate;
				row.NewValObj=PIn.Date(fieldVal);
				if(((DateTime)row.NewValObj).Year<1880) {
					row.NewValDisplay="";
				}
				else {
					row.NewValDisplay=((DateTime)row.NewValObj).ToShortDateString();
				}
				row.ObjType=typeof(DateTime);
				if(row.OldValDisplay!=row.NewValDisplay) {
					row.DoImport=true;
				}
				rows.Add(row);
			}
			//SSN---------------------------------------------
			fieldVal=GetInputValue("SSN");
			if(fieldVal!=null) {
				row=new SheetImportRow();
				row.FieldName="SSN";
				row.OldValDisplay=pat.SSN;
				row.OldValObj=pat.SSN;
				row.NewValDisplay=fieldVal.Replace("-","");//quickly strip dashes
				row.NewValObj=row.NewValDisplay;
				row.ObjType=typeof(string);
				if(row.OldValDisplay!=row.NewValDisplay) {
					row.DoImport=true;
				}
				rows.Add(row);
			}
			//WkPhone---------------------------------------------
			fieldVal=GetInputValue("WkPhone");
			if(fieldVal!=null) {
				row=new SheetImportRow();
				row.FieldName="WkPhone";
				row.OldValDisplay=pat.WkPhone;
				row.OldValObj=pat.WkPhone;
				row.NewValDisplay=fieldVal;
				row.NewValObj=row.NewValDisplay;
				row.ObjType=typeof(string);
				if(row.OldValDisplay!=row.NewValDisplay) {
					row.DoImport=true;
				}
				rows.Add(row);
			}
			//WirelessPhone---------------------------------------------
			fieldVal=GetInputValue("WirelessPhone");
			if(fieldVal!=null) {
				row=new SheetImportRow();
				row.FieldName="WirelessPhone";
				row.OldValDisplay=pat.WirelessPhone;
				row.OldValObj=pat.WirelessPhone;
				row.NewValDisplay=fieldVal;
				row.NewValObj=row.NewValDisplay;
				row.ObjType=typeof(string);
				if(row.OldValDisplay!=row.NewValDisplay) {
					row.DoImport=true;
				}
				rows.Add(row);
			}
			//wirelessCarrier---------------------------------------------
			fieldVal=GetInputValue("wirelessCarrier");
			if(fieldVal!=null) {
				row=new SheetImportRow();
				row.FieldName="wirelessCarrier";
				row.OldValDisplay="";
				row.OldValObj="";
				row.NewValDisplay=fieldVal;
				row.NewValObj=row.NewValDisplay;
				row.ObjType=typeof(string);
				row.DoImport=false;
				rows.Add(row);
			}
			//Email---------------------------------------------
			fieldVal=GetInputValue("Email");
			if(fieldVal!=null) {
				row=new SheetImportRow();
				row.FieldName="Email";
				row.OldValDisplay=pat.Email;
				row.OldValObj=pat.Email;
				row.NewValDisplay=fieldVal;
				row.NewValObj=row.NewValDisplay;
				row.ObjType=typeof(string);
				if(row.OldValDisplay!=row.NewValDisplay) {
					row.DoImport=true;
				}
				rows.Add(row);
			}
			#endregion
			//PreferContactMethod---------------------------------------------
			if(ContainsFieldThatStartsWith("PreferContactMethod")) {
				ContactMethod cmeth=pat.PreferContactMethod;
				if(IsChecked("PreferContactMethodIsEmail")) {
					cmeth=ContactMethod.Email;
				}
				if(IsChecked("PreferContactMethodIsHmPhone")) {
					cmeth=ContactMethod.HmPhone;
				}
				if(IsChecked("PreferContactMethodIsTextMessage")) {
					cmeth=ContactMethod.TextMessage;
				}
				if(IsChecked("PreferContactMethodIsWirelessPh")) {
					cmeth=ContactMethod.WirelessPh;
				}
				if(IsChecked("PreferContactMethodIsWkPhone")) {
					cmeth=ContactMethod.WkPhone;
				}
				row=new SheetImportRow();
				row.FieldName="PreferContactMethod";
				row.OldValDisplay=Lan.g("enumContactMethod",pat.PreferContactMethod.ToString());
				row.OldValObj=pat.PreferContactMethod;
				row.NewValDisplay=Lan.g("enumContactMethod",cmeth.ToString());
				row.NewValObj=cmeth;
				row.ObjType=typeof(ContactMethod);
				if(pat.PreferContactMethod!=cmeth) {
					row.DoImport=true;
				}
				rows.Add(row);
			}
			//PreferConfirmMethod---------------------------------------------
			if(ContainsFieldThatStartsWith("PreferConfirmMethod")) {
				ContactMethod cmeth=pat.PreferConfirmMethod;
				if(IsChecked("PreferConfirmMethodIsEmail")) {
					cmeth=ContactMethod.Email;
				}
				if(IsChecked("PreferConfirmMethodIsHmPhone")) {
					cmeth=ContactMethod.HmPhone;
				}
				if(IsChecked("PreferConfirmMethodIsTextMessage")) {
					cmeth=ContactMethod.TextMessage;
				}
				if(IsChecked("PreferConfirmMethodIsWirelessPh")) {
					cmeth=ContactMethod.WirelessPh;
				}
				if(IsChecked("PreferConfirmMethodIsWkPhone")) {
					cmeth=ContactMethod.WkPhone;
				}
				row=new SheetImportRow();
				row.FieldName="PreferConfirmMethod";
				row.OldValDisplay=Lan.g("enumContactMethod",pat.PreferConfirmMethod.ToString());
				row.OldValObj=pat.PreferConfirmMethod;
				row.NewValDisplay=Lan.g("enumContactMethod",cmeth.ToString());
				row.NewValObj=cmeth;
				row.ObjType=typeof(ContactMethod);
				if(pat.PreferConfirmMethod!=cmeth) {
					row.DoImport=true;
				}
				rows.Add(row);
			}
			//PreferRecallMethod---------------------------------------------
			if(ContainsFieldThatStartsWith("PreferRecallMethod")) {
				ContactMethod cmeth=pat.PreferRecallMethod;
				if(IsChecked("PreferRecallMethodIsEmail")) {
					cmeth=ContactMethod.Email;
				}
				if(IsChecked("PreferRecallMethodIsHmPhone")) {
					cmeth=ContactMethod.HmPhone;
				}
				if(IsChecked("PreferRecallMethodIsTextMessage")) {
					cmeth=ContactMethod.TextMessage;
				}
				if(IsChecked("PreferRecallMethodIsWirelessPh")) {
					cmeth=ContactMethod.WirelessPh;
				}
				if(IsChecked("PreferRecallMethodIsWkPhone")) {
					cmeth=ContactMethod.WkPhone;
				}
				row=new SheetImportRow();
				row.FieldName="PreferRecallMethod";
				row.OldValDisplay=Lan.g("enumContactMethod",pat.PreferRecallMethod.ToString());
				row.OldValObj=pat.PreferRecallMethod;
				row.NewValDisplay=Lan.g("enumContactMethod",cmeth.ToString());
				row.NewValObj=cmeth;
				row.ObjType=typeof(ContactMethod);
				if(pat.PreferRecallMethod!=cmeth) {
					row.DoImport=true;
				}
				rows.Add(row);
			}
			/*
			//---------------------------------------------
			fieldVal=GetInputValue("");
			if(fieldVal!=null) {
				row=new SheetImportRow();
				row.FieldName="";
				row.OldValDisplay=pat;
				row.OldValObj=pat;
				row.NewValDisplay=fieldVal;
				row.NewValObj=row.NewValDisplay;
				row.ObjType=typeof(string);
				if(row.OldValDisplay!=row.NewValDisplay) {
					row.DoImport=true;
				}
				rows.Add(row);
			}*/
			row=new SheetImportRow();
			row.FieldName="Address and Home Phone";
			row.IsSeparator=true;
			rows.Add(row);







		}

		private void FillGrid() {
			int scrollVal=gridMain.ScrollValue;
			gridMain.BeginUpdate();
			gridMain.Columns.Clear();
			ODGridColumn col; 
			col=new ODGridColumn(Lan.g(this,"FieldName"),150);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Current Value"),260);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"New Value"),260);
			gridMain.Columns.Add(col);
			col=new ODGridColumn(Lan.g(this,"Do Import"),60,HorizontalAlignment.Center);
			gridMain.Columns.Add(col);
			gridMain.Rows.Clear();
			ODGridRow row;
			for(int i=0;i<rows.Count;i++) {
				row=new ODGridRow();
				if(rows[i].IsSeparator) {
					row.Cells.Add(rows[i].FieldName);
					row.Cells.Add("");
					row.Cells.Add("");
					row.Cells.Add("");
					row.ColorBackG=Color.DarkSlateGray;
					row.ColorText=Color.White;
				}
				else {
					row.Cells.Add(rows[i].FieldName);
					row.Cells.Add(rows[i].OldValDisplay);
					row.Cells.Add(rows[i].NewValDisplay);
					if(rows[i].DoImport) {
						row.Cells.Add("X");
						row.ColorBackG=Color.FromArgb(225,225,225);
					}
					else {
						row.Cells.Add("");
					}
				}
				gridMain.Rows.Add(row);
			}
			gridMain.EndUpdate();
			gridMain.ScrollValue=scrollVal;
		}

		///<summary>If the specified fieldName does not exist, returns null</summary>
		private string GetInputValue(string fieldName) {
			for(int i=0;i<SheetCur.SheetFields.Count;i++) {
				if(SheetCur.SheetFields[i].FieldType!=SheetFieldType.InputField){
					continue;
				}
				if(SheetCur.SheetFields[i].FieldName != fieldName){
					continue;
				}
				return SheetCur.SheetFields[i].FieldValue;
			}
			return null;
		}

		///<summary>Only the true condition is tested.  If the specified fieldName does not exist, returns false.</summary>
		private bool IsChecked(string fieldName) {
			for(int i=0;i<SheetCur.SheetFields.Count;i++) {
				if(SheetCur.SheetFields[i].FieldType!=SheetFieldType.CheckBox){
					continue;
				}
				if(SheetCur.SheetFields[i].FieldName != fieldName){
					continue;
				}
				//if(SheetCur.SheetFields[i].FieldValue=="") {
				//	return YN.No;
				//}
				if(SheetCur.SheetFields[i].FieldValue=="X") {
					return true;
				}
			}
			return false;
		}

		private bool ContainsOneOfFields(params string[] fieldNames) {
			for(int i=0;i<SheetCur.SheetFields.Count;i++) {
				if(SheetCur.SheetFields[i].FieldType!=SheetFieldType.CheckBox
					&& SheetCur.SheetFields[i].FieldType!=SheetFieldType.InputField) 
				{
					continue;
				}
				for(int f=0;f<fieldNames.Length;f++) {
					if(SheetCur.SheetFields[i].FieldName==fieldNames[f]) {
						return true;
					}
				}
			}
			return false;
		}

		private bool ContainsFieldThatStartsWith(string fieldName) {
			for(int i=0;i<SheetCur.SheetFields.Count;i++) {
				if(SheetCur.SheetFields[i].FieldType!=SheetFieldType.CheckBox
					&& SheetCur.SheetFields[i].FieldType!=SheetFieldType.InputField) {
					continue;
				}
				if(SheetCur.SheetFields[i].FieldName.StartsWith(fieldName)){
					return true;
				}
			}
			return false;
		}

		private void gridMain_CellClick(object sender,ODGridClickEventArgs e) {
			if(e.Col!=3) {
				return;
			}
			if(rows[e.Row].IsSeparator) {
				return;
			}
			if(rows[e.Row].FieldName=="wirelessCarrier") {
				MsgBox.Show(this,"This field cannot be imported");
				return;
			}
			rows[e.Row].DoImport=!rows[e.Row].DoImport;
			FillGrid();
		}

		private void gridMain_CellDoubleClick(object sender,ODGridClickEventArgs e) {
			if(e.Col!=2) {
				return;
			}
			if(rows[e.Row].IsSeparator) {
				return;
			}
			if(rows[e.Row].ObjType==typeof(string)) {
				InputBox inputbox=new InputBox(rows[e.Row].FieldName);
				inputbox.textResult.Text=rows[e.Row].NewValDisplay;
				inputbox.ShowDialog();
				if(inputbox.DialogResult==DialogResult.OK){
					if(rows[e.Row].NewValDisplay==inputbox.textResult.Text) {//value is now same as original
						rows[e.Row].DoImport=false;
					}
					else {
						rows[e.Row].DoImport=true;
					}
					rows[e.Row].NewValDisplay=inputbox.textResult.Text;
					rows[e.Row].NewValObj=inputbox.textResult.Text;
				}
			}
			else if(rows[e.Row].ObjType.IsEnum) {
				//Note.  This only works for zero-indexed enums.
				FormSheetImportEnumPicker formEnum=new FormSheetImportEnumPicker(rows[e.Row].FieldName);
				for(int i=0;i<Enum.GetNames(rows[e.Row].ObjType).Length;i++) {
					formEnum.comboResult.Items.Add(Enum.GetNames(rows[e.Row].ObjType)[i]);
					if(rows[e.Row].NewValObj!=null && i==(int)rows[e.Row].NewValObj){
						formEnum.comboResult.SelectedIndex=i;
					}
				}
				formEnum.ShowDialog();
				if(formEnum.DialogResult==DialogResult.OK) {
					int selectedI=formEnum.comboResult.SelectedIndex;
					if(rows[e.Row].NewValObj==null) {//was initially null
						if(selectedI==-1) {//user made no change.  Still null
							rows[e.Row].DoImport=false;//impossible to import a null
						}
						else{//an item was selected
							rows[e.Row].NewValObj=Enum.ToObject(rows[e.Row].ObjType,selectedI);
							rows[e.Row].NewValDisplay=rows[e.Row].NewValObj.ToString();
							if((int)rows[e.Row].NewValObj==(int)rows[e.Row].OldValObj) {//but they just changed it back to the old setting for the patient.
								rows[e.Row].DoImport=false;//so no need to import
							}
							else {
								rows[e.Row].DoImport=true;
							}
						}
					}
					else{//was not initially null
						if((int)rows[e.Row].NewValObj==selectedI) {//but value not changed
							rows[e.Row].DoImport=false;
						}
						else{//value must have changed.  There's no way for the use to set it to null, so we do not need to test that
							rows[e.Row].NewValObj=Enum.ToObject(rows[e.Row].ObjType,selectedI);
							rows[e.Row].NewValDisplay=rows[e.Row].NewValObj.ToString();
							if((int)rows[e.Row].NewValObj==(int)rows[e.Row].OldValObj) {//but they just changed it back to the old setting for the patient.
								rows[e.Row].DoImport=false;//so no need to import
							}
							else {
								rows[e.Row].DoImport=true;
							}
						}
					}
				}
			}
			else if(rows[e.Row].ObjType==typeof(DateTime)) {
				InputBox inputbox=new InputBox(rows[e.Row].FieldName);
				inputbox.textResult.Text=rows[e.Row].NewValDisplay;
				inputbox.ShowDialog();
				if(inputbox.DialogResult!=DialogResult.OK) {
					return;
				}
				DateTime enteredDate;
				if(inputbox.textResult.Text=="") {
					enteredDate=DateTime.MinValue;
					rows[e.Row].NewValObj=enteredDate;
					rows[e.Row].NewValDisplay="";
				}
				else {
					try {
						enteredDate=DateTime.Parse(inputbox.textResult.Text);
					}
					catch {
						MsgBox.Show(this,"Invalid date");
						return;
					}
					if(enteredDate.Year<1880 || enteredDate.Year>2050) {
						MsgBox.Show(this,"Invalid date");
						return;
					}
					rows[e.Row].NewValObj=enteredDate;
					rows[e.Row].NewValDisplay=enteredDate.ToShortDateString();
				}
				if(rows[e.Row].NewValDisplay==rows[e.Row].OldValDisplay) {//value is now same as original
					rows[e.Row].DoImport=false;
				}
				else {
					rows[e.Row].DoImport=true;
				}
			}
			FillGrid();
		}

		private void butOK_Click(object sender,EventArgs e) {
			bool importsPresent=false;
			for(int i=0;i<rows.Count;i++) {
				if(rows[i].DoImport) {
					importsPresent=true;
					break;
				}
			}
			if(!importsPresent) {
				MsgBox.Show(this,"No rows are set for import.");
				return;
			}
			Patient patientOld=pat.Copy();
			for(int i=0;i<rows.Count;i++) {
				if(!rows[i].DoImport) {
					continue;
				}
				switch(rows[i].FieldName){
					case "LName":
						pat.LName=rows[i].NewValDisplay;
						break;
					case "FName":
						pat.FName=rows[i].NewValDisplay;
						break;
					case "MiddleI":
						pat.MiddleI=rows[i].NewValDisplay;
						break;
					case "Preferred":
						pat.Preferred=rows[i].NewValDisplay;
						break;
					case "Gender":
						pat.Gender=(PatientGender)rows[i].NewValObj;
						break;
					case "Position":
						pat.Position=(PatientPosition)rows[i].NewValObj;
						break;
					case "Birthdate":
						pat.Birthdate=(DateTime)rows[i].NewValObj;
						break;
					case "SSN":
						pat.SSN=rows[i].NewValDisplay;
						break;
					case "WkPhone":
						pat.WkPhone=rows[i].NewValDisplay;
						break;
					case "WirelessPhone":
						pat.WirelessPhone=rows[i].NewValDisplay;
						break;
					case "Email":
						pat.Email=rows[i].NewValDisplay;
						break;
					case "PreferContactMethod":
						pat.PreferContactMethod=(ContactMethod)rows[i].NewValObj;
						break;
					case "PreferConfirmMethod":
						pat.PreferConfirmMethod=(ContactMethod)rows[i].NewValObj;
						break;
					case "PreferRecallMethod":
						pat.PreferRecallMethod=(ContactMethod)rows[i].NewValObj;
						break;


						/*
					case "Address":
						pat=rows[i].NewValDisplay;
						break;
					case "":
						pat=rows[i].NewValDisplay;
						break;
					case "":
						pat=rows[i].NewValDisplay;
						break;
					case "":
						pat=rows[i].NewValDisplay;
						break;
					case "":
						pat=rows[i].NewValDisplay;
						break;
					case "":
						pat=rows[i].NewValDisplay;
						break;*/
				}
			}
			Patients.Update(pat,patientOld);
			MsgBox.Show(this,"Done.");
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		private class SheetImportRow {
			public string FieldName;
			public string OldValDisplay;
			public object OldValObj;
			public string NewValDisplay;
			public object NewValObj;
			public bool DoImport;
			public bool IsSeparator;
			///<summary>This is needed because the NewValObj might be null.</summary>
			public Type ObjType;
		}

		

		
	}

	
}