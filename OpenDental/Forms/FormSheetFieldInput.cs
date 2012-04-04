using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormSheetFieldInput:Form {
		///<summary>This is the object we are editing.</summary>
		public SheetFieldDef SheetFieldDefCur;
		///<summary>We need access to a few other fields of the sheetDef.</summary>
		public SheetDef SheetDefCur;
		private List<SheetFieldDef> AvailFields;
		public bool IsReadOnly;
		///<summary>Only for medical history sheet.  If this field is not new, we want to keep the beginning of the FieldName. Ex: inputMed##</summary>
		private string inputMedOld;

		public FormSheetFieldInput() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormSheetFieldInput_Load(object sender,EventArgs e) {
			if(IsReadOnly){
				butOK.Enabled=false;
				butDelete.Enabled=false;
			}
			if(SheetDefCur.SheetType==SheetTypeEnum.MedicalHistory && SheetFieldDefCur.FieldName.StartsWith("inputMed")) {
				inputMedOld=SheetFieldDefCur.FieldName;
			}
			//not allowed to change sheettype or fieldtype once created.  So get all avail fields for this sheettype
			AvailFields=SheetFieldsAvailable.GetList(SheetDefCur.SheetType,OutInCheck.In);
			listFields.Items.Clear();
			for(int i=0;i<AvailFields.Count;i++){
				//static text is not one of the options.
				listFields.Items.Add(AvailFields[i].FieldName);
				if(SheetFieldDefCur.FieldName.StartsWith(AvailFields[i].FieldName)){
					listFields.SelectedIndex=i;
				}
			}
			InstalledFontCollection fColl=new InstalledFontCollection();
			for(int i=0;i<fColl.Families.Length;i++){
				comboFontName.Items.Add(fColl.Families[i].Name);
			}
			comboFontName.Text=SheetFieldDefCur.FontName;
			textFontSize.Text=SheetFieldDefCur.FontSize.ToString();
			checkFontIsBold.Checked=SheetFieldDefCur.FontIsBold;
			for(int i=0;i<Enum.GetNames(typeof(GrowthBehaviorEnum)).Length;i++){
				comboGrowthBehavior.Items.Add(Enum.GetNames(typeof(GrowthBehaviorEnum))[i]);
				if((int)SheetFieldDefCur.GrowthBehavior==i){
					comboGrowthBehavior.SelectedIndex=i;
				}
			}
			textXPos.Text=SheetFieldDefCur.XPos.ToString();
			textYPos.Text=SheetFieldDefCur.YPos.ToString();
			textWidth.Text=SheetFieldDefCur.Width.ToString();
			textHeight.Text=SheetFieldDefCur.Height.ToString();
			checkRequired.Checked=SheetFieldDefCur.IsRequired;
			textTabOrder.Text=SheetFieldDefCur.TabOrder.ToString();
		}

		private void listFields_DoubleClick(object sender,EventArgs e) {
			SaveAndClose();
		}

		private void listMeds_DoubleClick(object sender,EventArgs e) {
			SaveAndClose();
		}

		private void butDelete_Click(object sender,EventArgs e) {
			SheetFieldDefCur=null;
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender,EventArgs e) {
			SaveAndClose();
		}

		private void SaveAndClose(){
			if(textXPos.errorProvider1.GetError(textXPos)!=""
				|| textYPos.errorProvider1.GetError(textYPos)!=""
				|| textWidth.errorProvider1.GetError(textWidth)!=""
				|| textHeight.errorProvider1.GetError(textHeight)!=""
				|| textTabOrder.errorProvider1.GetError(textTabOrder)!="")
			{
				MsgBox.Show(this,"Please fix data entry errors first.");
				return;
			}
			if(listFields.SelectedIndex==-1){
				MsgBox.Show(this,"Please select a field name first.");
				return;
			}
			if(comboFontName.Text==""){
				//not going to bother testing for validity unless it will cause a crash.
				MsgBox.Show(this,"Please select a font name first.");
				return;
			}
			float fontSize;
			try{
				fontSize=float.Parse(textFontSize.Text);
				if(fontSize<2){
					MsgBox.Show(this,"Font size is invalid.");
					return;
				}
			}
			catch{
				MsgBox.Show(this,"Font size is invalid.");
				return;
			}
			string fieldName=AvailFields[listFields.SelectedIndex].FieldName;
			if(SheetDefCur.SheetType==SheetTypeEnum.MedicalHistory && AvailFields[listFields.SelectedIndex].FieldName=="inputMed") {
				if(inputMedOld==null) {
					List<string> inputFieldNamesList=new List<string>();
					//Loop through the current sheet and figure out how many inputMed sheet field defs there are. 
					for(int i=0;i<SheetDefCur.SheetFieldDefs.Count;i++) {
						if(SheetDefCur.SheetFieldDefs[i].FieldName.StartsWith("inputMed")
						&& SheetDefCur.SheetFieldDefs[i]!=SheetFieldDefCur) 
						{
							inputFieldNamesList.Add(SheetDefCur.SheetFieldDefs[i].FieldName);
						}
					}
					if(inputFieldNamesList.Count>=20) {
						MsgBox.Show(this,"Not allowed to have more than 20 medication input fields per sheet.");
						return;
					}
					//Now figure out what number to use for the new inputMed##. Always loop through at least once in case none exist yet.
					for(int i=0;i==0 || i<inputFieldNamesList.Count;i++) {
						if(inputFieldNamesList.Contains("inputMed"+i.ToString("00"))) {
							if(i==inputFieldNamesList.Count-1) {//Every number exists, so increment by 1 for the next number. 
								i++;
								fieldName+=i.ToString("00");
								break;
							}
							continue;
						}
						//User could have deleted an inputMed in the middle of the "list" so we want to use that number instead of a count.
						fieldName+=i.ToString("00");
						break;
					}
				}
				else {
					//User double clicked on an inputMed field. We want the number to stay the same.
					fieldName=inputMedOld;//inputMedOld is set on load.
				}
			}
			SheetFieldDefCur.FieldName=fieldName;
			SheetFieldDefCur.FontName=comboFontName.Text;
			SheetFieldDefCur.FontSize=fontSize;
			SheetFieldDefCur.FontIsBold=checkFontIsBold.Checked;
			SheetFieldDefCur.XPos=PIn.Int(textXPos.Text);
			SheetFieldDefCur.YPos=PIn.Int(textYPos.Text);
			SheetFieldDefCur.Width=PIn.Int(textWidth.Text);
			SheetFieldDefCur.Height=PIn.Int(textHeight.Text);
			SheetFieldDefCur.GrowthBehavior=(GrowthBehaviorEnum)comboGrowthBehavior.SelectedIndex;
			SheetFieldDefCur.IsRequired=checkRequired.Checked;
			SheetFieldDefCur.TabOrder=PIn.Int(textTabOrder.Text);
			//don't save to database here.
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		

		
	}
}