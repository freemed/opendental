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
	public partial class FormSheetDef:Form {
		///<summary></summary>
		public SheetDef SheetDefCur;
		//private List<SheetFieldDef> AvailFields;
		public bool IsReadOnly;
		///<summary>On creation of a new sheetdef, the user must pick a description and a sheettype before allowing to start editing the sheet.  After the initial sheettype selection, this will be false, indicating that the user may not change the type.</summary>
		public bool IsInitial;

		public FormSheetDef() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormSheetDef_Load(object sender,EventArgs e) {
			if(IsReadOnly){
				butOK.Enabled=false;
			}
			if(!IsInitial){
				listSheetType.Enabled=false;
			}
			textDescription.Text=SheetDefCur.Description;
			//not allowed to change sheettype once created.
			for(int i=0;i<Enum.GetNames(typeof(SheetTypeEnum)).Length;i++){
				listSheetType.Items.Add(Enum.GetNames(typeof(SheetTypeEnum))[i]);
				if((int)SheetDefCur.SheetType==i && !IsInitial){
					listSheetType.SelectedIndex=i;
				}
			}
			InstalledFontCollection fColl=new InstalledFontCollection();
			for(int i=0;i<fColl.Families.Length;i++){
				comboFontName.Items.Add(fColl.Families[i].Name);
			}
			comboFontName.Text=SheetDefCur.FontName;
			textFontSize.Text=SheetDefCur.FontSize.ToString();
			textWidth.Text=SheetDefCur.Width.ToString();
			textHeight.Text=SheetDefCur.Height.ToString();
			checkIsLandscape.Checked=SheetDefCur.IsLandscape;
		}

		private void listSheetType_Click(object sender,EventArgs e) {
			if(!IsInitial){
				return;
			}
			if(listSheetType.SelectedIndex==-1){
				return;
			}
			SheetDef sheetdef=null;
			switch((SheetTypeEnum)listSheetType.SelectedIndex){
				case SheetTypeEnum.LabelCarrier:
				case SheetTypeEnum.LabelPatient:
				case SheetTypeEnum.LabelReferral:
					sheetdef=SheetsInternal.GetSheetDef(SheetInternalType.LabelPatientMail);
					if(textDescription.Text==""){
						textDescription.Text=((SheetTypeEnum)listSheetType.SelectedIndex).ToString();
					}
					comboFontName.Text=sheetdef.FontName;
					textFontSize.Text=sheetdef.FontSize.ToString();
					textWidth.Text=sheetdef.Width.ToString();
					textHeight.Text=sheetdef.Height.ToString();
					checkIsLandscape.Checked=sheetdef.IsLandscape;
					break;
				case SheetTypeEnum.ReferralSlip:
					sheetdef=SheetsInternal.GetSheetDef(SheetInternalType.ReferralSlip);
					if(textDescription.Text==""){
						textDescription.Text=((SheetTypeEnum)listSheetType.SelectedIndex).ToString();
					}
					comboFontName.Text=sheetdef.FontName;
					textFontSize.Text=sheetdef.FontSize.ToString();
					textWidth.Text=sheetdef.Width.ToString();
					textHeight.Text=sheetdef.Height.ToString();
					checkIsLandscape.Checked=sheetdef.IsLandscape;
					break;
			}
		}

		private void butOK_Click(object sender,EventArgs e) {
			if(textWidth.errorProvider1.GetError(textWidth)!=""
				|| textHeight.errorProvider1.GetError(textHeight)!="")
			{
				MsgBox.Show(this,"Please fix data entry errors first.");
				return;
			}
			if(listSheetType.SelectedIndex==-1){
				MsgBox.Show(this,"Please select a sheet type first.");
				return;
			}
			if(textDescription.Text==""){
				MsgBox.Show(this,"Description may not be blank.");
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
			SheetDefCur.Description=textDescription.Text;
			SheetDefCur.SheetType=(SheetTypeEnum)listSheetType.SelectedIndex;
			SheetDefCur.FontName=comboFontName.Text;
			SheetDefCur.FontSize=fontSize;
			SheetDefCur.Width=PIn.PInt32(textWidth.Text);
			SheetDefCur.Height=PIn.PInt32(textHeight.Text);
			SheetDefCur.IsLandscape=checkIsLandscape.Checked;
			//don't save to database here.
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		

		
	}
}