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
	public partial class FormSheetFieldStatic:Form {
		///<summary>This is the object we are editing.</summary>
		public SheetFieldDef SheetFieldDefCur;
		///<summary>We need access to a few other fields of the sheetDef.</summary>
		public SheetDef SheetDefCur;
		public bool IsReadOnly;
		private int textSelectionStart;

		public FormSheetFieldStatic() {
			InitializeComponent();
			Lan.F(this);
		}

		private void FormSheetFieldStatic_Load(object sender,EventArgs e) {
			if(IsReadOnly){
				butOK.Enabled=false;
			}
			textFieldValue.Text=SheetFieldDefCur.FieldValue;
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
			FillFields();
		}

		private void FillFields(){
			string[] fieldArray=new string[] {
				"address",
				"age",
				"balTotal",
				"bal_0_30",
				"bal_31_60",
				"bal_61_90",
				"balOver90",
				"balInsEst",
				"balTotalMinusInsEst",
				"BillingType",
				"Birthdate",
				"carrierName",
				"carrier2Name",
				"ChartNumber",
				"carrierAddress",
				"carrierCityStZip",
				"cityStateZip",
				"clinicDescription",
				"clinicAddress",
				"clinicCityStZip",
				"clinicPhone",
				"DateFirstVisit",
				"dateOfLastSavedTP",
				"dateRecallDue",
				"dateTimeLastAppt",
				"dateToday",
				"Email",
				"famFinUrgNote",
				"HmPhone",
				"guarantorNameFL",
				"insAnnualMax",
				"insDeductible",
				"insDeductibleUsed",
				"insPending",
				"insPercentages",
				"insUsed",
				"ins2AnnualMax",
				"ins2Deductible",
				"ins2DeductibleUsed",
				"ins2Pending",
				"ins2Percentages",
				"ins2Used",
				"MedUrgNote",
				"nameF",
				"nameFL",
				"nameFLFormal",
				"nameL",
				"nameLF",
				"nextSchedApptDateT",
				"PatNum",
				"priProvNameFormal",
				"recallInterval",
				"salutation",
				"siteDescription",
				"subscriberID",
				"subscriberNameFL",
				"subscriber2NameFL",
				"tpResponsPartyAddress",
				"tpResponsPartyCityStZip",
				"tpResponsPartyNameFL",
				"treatmentPlanProcs",
				"WirelessPhone",
				"WkPhone"
			};
			listFields.Items.Clear();
			for(int i=0;i<fieldArray.Length;i++){
				listFields.Items.Add(fieldArray[i]);
			}
		}

		private void listFields_MouseClick(object sender,MouseEventArgs e) {
			string fieldStr="";
			for(int i=0;i<listFields.Items.Count;i++) {
				if(listFields.GetItemRectangle(i).Contains(e.Location)) {
					fieldStr=listFields.Items[i].ToString();
				}
			}
			if(fieldStr=="") {
				return;
			}
			if(textSelectionStart < textFieldValue.Text.Length-1) {
				textFieldValue.Text=textFieldValue.Text.Substring(0,textSelectionStart)
					+"["+fieldStr+"]"
					+textFieldValue.Text.Substring(textSelectionStart);
			}
			else{//otherwise, just tack it on the end
				textFieldValue.Text+="["+fieldStr+"]";
			}
			textFieldValue.Select(textSelectionStart+fieldStr.Length+2,0);
			textFieldValue.Focus();
			//if(!textFieldValue.Focused){
			//	textFieldValue.Text+="["+fieldStr+"]";
			//	return;
			//}
			//MessageBox.Show(textFieldValue.SelectionStart.ToString());
		}

		private void textFieldValue_Leave(object sender,EventArgs e) {
			textSelectionStart=textFieldValue.SelectionStart;
		}

		private void textFieldValue_TextChanged(object sender,EventArgs e) {
			int textW=0;
			float fontSize=10f;
			try{
				fontSize=float.Parse(textFontSize.Text);
			}
			catch{
			}
			using(Graphics g=this.CreateGraphics()){
				using(Font font=new Font(comboFontName.Text,fontSize)){
					textW=(int)g.MeasureString(textFieldValue.Text,font).Width;
				}
			}
			labelTextW.Text=Lan.g(this,"TextW: ")+textW.ToString();
		}

		private void butDelete_Click(object sender,EventArgs e) {
			SheetFieldDefCur=null;
			DialogResult=DialogResult.OK;
		}

		private void butOK_Click(object sender,EventArgs e) {
			if(textXPos.errorProvider1.GetError(textXPos)!=""
				|| textYPos.errorProvider1.GetError(textYPos)!=""
				|| textWidth.errorProvider1.GetError(textWidth)!=""
				|| textHeight.errorProvider1.GetError(textHeight)!="")
			{
				MsgBox.Show(this,"Please fix data entry errors first.");
				return;
			}
			if(textFieldValue.Text==""){
				MsgBox.Show(this,"Please set a field value first.");
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
			SheetFieldDefCur.FieldValue=textFieldValue.Text;
			SheetFieldDefCur.FontName=comboFontName.Text;
			SheetFieldDefCur.FontSize=fontSize;
			SheetFieldDefCur.FontIsBold=checkFontIsBold.Checked;
			SheetFieldDefCur.XPos=PIn.Int(textXPos.Text);
			SheetFieldDefCur.YPos=PIn.Int(textYPos.Text);
			SheetFieldDefCur.Width=PIn.Int(textWidth.Text);
			SheetFieldDefCur.Height=PIn.Int(textHeight.Text);
			SheetFieldDefCur.GrowthBehavior=(GrowthBehaviorEnum)comboGrowthBehavior.SelectedIndex;
			//don't save to database here.
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		

	

		

		

		
	}
}