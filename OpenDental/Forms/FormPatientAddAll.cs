using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental {
	public partial class FormPatientAddAll:Form {
		public string LName;
		public string FName;
		private string mostRecentLName;
		private List<Referral> similarReferrals;
		private string referralOriginal;
		private System.Windows.Forms.ListBox listReferral;
		private bool mouseIsInListReferral;
		private Referral selectedReferral;
		//private int selectedSubscriberIndex1;
		/// <summary>displayed from within code, not designer</summary>
		private System.Windows.Forms.ListBox listEmps1;
		private bool mouseIsInListEmps1;
		private string empOriginal1;
		/// <summary>displayed from within code, not designer</summary>
		private System.Windows.Forms.ListBox listEmps2;
		private bool mouseIsInListEmps2;
		private string empOriginal2;
		private List<Carrier> similarCars1;
		private string carOriginal1;
		private System.Windows.Forms.ListBox listCars1;
		private bool mouseIsInListCars1;
		private Carrier selectedCarrier1;
		private List<Carrier> similarCars2;
		private string carOriginal2;
		private System.Windows.Forms.ListBox listCars2;
		private bool mouseIsInListCars2;
		private Carrier selectedCarrier2;
		private InsPlan selectedPlan1;
		private InsPlan selectedPlan2;

		public FormPatientAddAll() {
			InitializeComponent();
			Lan.F(this);
			listReferral=new ListBox();
			listReferral.Location=new Point(textReferral.Left,textReferral.Bottom);
			listReferral.Size=new Size(400,140);
			listReferral.HorizontalScrollbar=true;
			listReferral.Visible=false;
			listReferral.Click += new System.EventHandler(listReferral_Click);
			listReferral.DoubleClick += new System.EventHandler(listReferral_DoubleClick);
			listReferral.MouseEnter += new System.EventHandler(listReferral_MouseEnter);
			listReferral.MouseLeave += new System.EventHandler(listReferral_MouseLeave);
			Controls.Add(listReferral);
			listReferral.BringToFront();
			listEmps1=new ListBox();
			listEmps1.Location=new Point(groupIns1.Left+textEmployer1.Left,
				groupIns1.Top+textEmployer1.Bottom);
			listEmps1.Size=new Size(254,100);
			listEmps1.Visible=false;
			listEmps1.Click += new System.EventHandler(listEmps1_Click);
			listEmps1.DoubleClick += new System.EventHandler(listEmps1_DoubleClick);
			listEmps1.MouseEnter += new System.EventHandler(listEmps1_MouseEnter);
			listEmps1.MouseLeave += new System.EventHandler(listEmps1_MouseLeave);
			Controls.Add(listEmps1);
			listEmps1.BringToFront();
			listEmps2=new ListBox();
			listEmps2.Location=new Point(groupIns2.Left+textEmployer2.Left,
				groupIns2.Top+textEmployer2.Bottom);
			listEmps2.Size=new Size(254,100);
			listEmps2.Visible=false;
			listEmps2.Click += new System.EventHandler(listEmps2_Click);
			listEmps2.DoubleClick += new System.EventHandler(listEmps2_DoubleClick);
			listEmps2.MouseEnter += new System.EventHandler(listEmps2_MouseEnter);
			listEmps2.MouseLeave += new System.EventHandler(listEmps2_MouseLeave);
			Controls.Add(listEmps2);
			listEmps2.BringToFront();
			listCars1=new ListBox();
			listCars1.Location=new Point(groupIns1.Left+textCarrier1.Left,
				groupIns1.Top+textCarrier1.Bottom);
			listCars1.Size=new Size(700,100);
			listCars1.HorizontalScrollbar=true;
			listCars1.Visible=false;
			listCars1.Click += new System.EventHandler(listCars1_Click);
			listCars1.DoubleClick += new System.EventHandler(listCars1_DoubleClick);
			listCars1.MouseEnter += new System.EventHandler(listCars1_MouseEnter);
			listCars1.MouseLeave += new System.EventHandler(listCars1_MouseLeave);
			Controls.Add(listCars1);
			listCars1.BringToFront();
			listCars2=new ListBox();
			listCars2.Location=new Point(groupIns2.Left+textCarrier2.Left,
				groupIns2.Top+textCarrier2.Bottom);
			listCars2.Size=new Size(700,100);
			listCars2.HorizontalScrollbar=true;
			listCars2.Visible=false;
			listCars2.Click += new System.EventHandler(listCars2_Click);
			listCars2.DoubleClick += new System.EventHandler(listCars2_DoubleClick);
			listCars2.MouseEnter += new System.EventHandler(listCars2_MouseEnter);
			listCars2.MouseLeave += new System.EventHandler(listCars2_MouseLeave);
			Controls.Add(listCars2);
			listCars2.BringToFront();
		}

		private void FormPatientAddAll_Load(object sender,EventArgs e) {
			textLName1.Text=LName;
			textFName1.Text=FName;
			listGender1.SelectedIndex=0;
			listGender2.SelectedIndex=0;
			listGender3.SelectedIndex=0;
			listGender4.SelectedIndex=0;
			listGender5.SelectedIndex=0;
			listPosition1.SelectedIndex=1;
			listPosition2.SelectedIndex=1;
			comboSecProv1.Items.Add(Lan.g(this,"none"));
			comboSecProv1.SelectedIndex=0;
			comboSecProv2.Items.Add(Lan.g(this,"none"));
			comboSecProv2.SelectedIndex=0;
			comboSecProv3.Items.Add(Lan.g(this,"none"));
			comboSecProv3.SelectedIndex=0;
			comboSecProv4.Items.Add(Lan.g(this,"none"));
			comboSecProv4.SelectedIndex=0;
			comboSecProv5.Items.Add(Lan.g(this,"none"));
			comboSecProv5.SelectedIndex=0;
			for(int i=0;i<ProviderC.List.Length;i++){
				comboPriProv1.Items.Add(ProviderC.List[i].GetLongDesc());
				comboSecProv1.Items.Add(ProviderC.List[i].GetLongDesc());
				comboPriProv2.Items.Add(ProviderC.List[i].GetLongDesc());
				comboSecProv2.Items.Add(ProviderC.List[i].GetLongDesc());
				comboPriProv3.Items.Add(ProviderC.List[i].GetLongDesc());
				comboSecProv3.Items.Add(ProviderC.List[i].GetLongDesc());
				comboPriProv4.Items.Add(ProviderC.List[i].GetLongDesc());
				comboSecProv4.Items.Add(ProviderC.List[i].GetLongDesc());
				comboPriProv5.Items.Add(ProviderC.List[i].GetLongDesc());
				comboSecProv5.Items.Add(ProviderC.List[i].GetLongDesc());
			}
			int defaultindex=Providers.GetIndex(PrefC.GetInt("PracticeDefaultProv"));
			if(defaultindex==-1) {//default provider hidden
				defaultindex=0;
			}
			comboPriProv1.SelectedIndex=defaultindex;
			comboPriProv2.SelectedIndex=defaultindex;
			comboPriProv3.SelectedIndex=defaultindex;
			comboPriProv4.SelectedIndex=defaultindex;
			comboPriProv5.SelectedIndex=defaultindex;
			FillComboZip();
			ResetSubscriberLists();
		}

		#region Names
		private void textLName1_TextChanged(object sender,EventArgs e) {
			if(textLName1.Text.Length==1){
				textLName1.Text=textLName1.Text.ToUpper();
				textLName1.SelectionStart=1;
			}
			SetLNames();
		}

		private void textLName2_TextChanged(object sender,EventArgs e) {
			if(textLName2.Text.Length==1){
				textLName2.Text=textLName2.Text.ToUpper();
				textLName2.SelectionStart=1;
			}
		}

		private void textLName3_TextChanged(object sender,EventArgs e) {
			if(textLName3.Text.Length==1){
				textLName3.Text=textLName3.Text.ToUpper();
				textLName3.SelectionStart=1;
			}
		}

		private void textLName4_TextChanged(object sender,EventArgs e) {
			if(textLName4.Text.Length==1){
				textLName4.Text=textLName4.Text.ToUpper();
				textLName4.SelectionStart=1;
			}
		}

		private void textLName5_TextChanged(object sender,EventArgs e) {
			if(textLName5.Text.Length==1){
				textLName5.Text=textLName5.Text.ToUpper();
				textLName5.SelectionStart=1;
			}
		}

		private void textFName1_TextChanged(object sender,EventArgs e) {
			if(textFName1.Text.Length==1){
				textFName1.Text=textFName1.Text.ToUpper();
				textFName1.SelectionStart=1;
			}
			SetLNames();
		}

		private void textFName2_TextChanged(object sender,EventArgs e) {
			if(textFName2.Text.Length==1){
				textFName2.Text=textFName2.Text.ToUpper();
				textFName2.SelectionStart=1;
			}
			SetLNames();
		}

		private void textFName3_TextChanged(object sender,EventArgs e) {
			if(textFName3.Text.Length==1){
				textFName3.Text=textFName3.Text.ToUpper();
				textFName3.SelectionStart=1;
			}
			SetLNames();
		}

		private void textFName4_TextChanged(object sender,EventArgs e) {
			if(textFName4.Text.Length==1){
				textFName4.Text=textFName4.Text.ToUpper();
				textFName4.SelectionStart=1;
			}
			SetLNames();
		}

		private void textFName5_TextChanged(object sender,EventArgs e) {
			if(textFName5.Text.Length==1){
				textFName5.Text=textFName5.Text.ToUpper();
				textFName5.SelectionStart=1;
			}
			SetLNames();
		}

		private void SetLNames(){
			if(textLName2.Text=="" || textLName2.Text==mostRecentLName){
				if(textFName2.Text==""){
					textLName2.Text="";
				}
				else{
					textLName2.Text=textLName1.Text;
				}
			}
			if(textLName3.Text=="" || textLName3.Text==mostRecentLName){
				if(textFName3.Text==""){
					textLName3.Text="";
				}
				else{
					textLName3.Text=textLName1.Text;
				}
			}
			if(textLName4.Text=="" || textLName4.Text==mostRecentLName){
				if(textFName4.Text==""){
					textLName4.Text="";
				}
				else{
					textLName4.Text=textLName1.Text;
				}
			}
			if(textLName5.Text=="" || textLName5.Text==mostRecentLName){
				if(textFName5.Text==""){
					textLName5.Text="";
				}
				else{
					textLName5.Text=textLName1.Text;
				}
			}
			mostRecentLName=textLName1.Text;
			ResetSubscriberLists();
		}
		#endregion Names

		#region BirthdateAndAge
		private void textBirthdate1_Validated(object sender,EventArgs e) {
			if(textBirthdate1.errorProvider1.GetError(textBirthdate1)!=""){
				textAge1.Text="";
				return;
			}
			DateTime birthdate=PIn.PDate(textBirthdate1.Text);
			if(birthdate>DateTime.Today){
				birthdate=birthdate.AddYears(-100);
			}
			textAge1.Text=PatientLogic.DateToAgeString(birthdate);
		}

		private void textBirthdate2_Validated(object sender,EventArgs e) {
			if(textBirthdate2.errorProvider1.GetError(textBirthdate2)!=""){
				textAge2.Text="";
				return;
			}
			DateTime birthdate=PIn.PDate(textBirthdate2.Text);
			if(birthdate>DateTime.Today){
				birthdate=birthdate.AddYears(-100);
			}
			textAge2.Text=PatientLogic.DateToAgeString(birthdate);
		}

		private void textBirthdate3_Validated(object sender,EventArgs e) {
			if(textBirthdate3.errorProvider1.GetError(textBirthdate3)!=""){
				textAge3.Text="";
				return;
			}
			DateTime birthdate=PIn.PDate(textBirthdate3.Text);
			if(birthdate>DateTime.Today){
				birthdate=birthdate.AddYears(-100);
			}
			textAge3.Text=PatientLogic.DateToAgeString(birthdate);
		}

		private void textBirthdate4_Validated(object sender,EventArgs e) {
			if(textBirthdate4.errorProvider1.GetError(textBirthdate4)!=""){
				textAge4.Text="";
				return;
			}
			DateTime birthdate=PIn.PDate(textBirthdate4.Text);
			if(birthdate>DateTime.Today){
				birthdate=birthdate.AddYears(-100);
			}
			textAge4.Text=PatientLogic.DateToAgeString(birthdate);
		}

		private void textBirthdate5_Validated(object sender,EventArgs e) {
			if(textBirthdate5.errorProvider1.GetError(textBirthdate5)!=""){
				textAge5.Text="";
				return;
			}
			DateTime birthdate=PIn.PDate(textBirthdate5.Text);
			if(birthdate>DateTime.Today){
				birthdate=birthdate.AddYears(-100);
			}
			textAge5.Text=PatientLogic.DateToAgeString(birthdate);
		}
		#endregion BirthdateAndAge

		#region InsCheckProvAutomation
		private void checkInsOne1_Click(object sender,EventArgs e) {
			if(textFName2.Text!="" && checkInsOne1.Checked){
				checkInsOne2.Checked=true;
			}
			else{
				checkInsOne2.Checked=false;
			}
			if(textFName3.Text!="" && checkInsOne1.Checked){
				checkInsOne3.Checked=true;
			}
			else{
				checkInsOne3.Checked=false;
			}
			if(textFName4.Text!="" && checkInsOne1.Checked){
				checkInsOne4.Checked=true;
			}
			else{
				checkInsOne4.Checked=false;
			}
			if(textFName5.Text!="" && checkInsOne1.Checked){
				checkInsOne5.Checked=true;
			}
			else{
				checkInsOne5.Checked=false;
			}
		}

		private void checkInsTwo1_Click(object sender,EventArgs e) {
			if(textFName2.Text!="" && checkInsTwo1.Checked){
				checkInsTwo2.Checked=true;
			}
			else{
				checkInsTwo2.Checked=false;
			}
			if(textFName3.Text!="" && checkInsTwo1.Checked){
				checkInsTwo3.Checked=true;
			}
			else{
				checkInsTwo3.Checked=false;
			}
			if(textFName4.Text!="" && checkInsTwo1.Checked){
				checkInsTwo4.Checked=true;
			}
			else{
				checkInsTwo4.Checked=false;
			}
			if(textFName5.Text!="" && checkInsTwo1.Checked){
				checkInsTwo5.Checked=true;
			}
			else{
				checkInsTwo5.Checked=false;
			}
		}

		private void comboPriProv1_SelectionChangeCommitted(object sender,EventArgs e) {
			comboPriProv2.SelectedIndex=comboPriProv1.SelectedIndex;
			comboPriProv3.SelectedIndex=comboPriProv1.SelectedIndex;
			comboPriProv4.SelectedIndex=comboPriProv1.SelectedIndex;
			comboPriProv5.SelectedIndex=comboPriProv1.SelectedIndex;
		}

		private void comboSecProv1_SelectionChangeCommitted(object sender,EventArgs e) {
			comboSecProv2.SelectedIndex=comboSecProv1.SelectedIndex;
			comboSecProv3.SelectedIndex=comboSecProv1.SelectedIndex;
			comboSecProv4.SelectedIndex=comboSecProv1.SelectedIndex;
			comboSecProv5.SelectedIndex=comboSecProv1.SelectedIndex;
		}
		#endregion InsCheckProvAutomation

		#region AddressPhone
		private void textHmPhone_TextChanged(object sender, System.EventArgs e) {
		 	int cursor=textHmPhone.SelectionStart;
			int length=textHmPhone.Text.Length;
			textHmPhone.Text=TelephoneNumbers.AutoFormat(textHmPhone.Text);
			if(textHmPhone.Text.Length>length){
				cursor++;
			}
			textHmPhone.SelectionStart=cursor;		
		}

		private void textAddress_TextChanged(object sender, System.EventArgs e) {
			if(textAddress.Text.Length==1){
				textAddress.Text=textAddress.Text.ToUpper();
				textAddress.SelectionStart=1;
			}
		}

		private void textAddress2_TextChanged(object sender, System.EventArgs e) {
			if(textAddress2.Text.Length==1){
				textAddress2.Text=textAddress2.Text.ToUpper();
				textAddress2.SelectionStart=1;
			}
		}

		private void textCity_TextChanged(object sender, System.EventArgs e) {
			if(textCity.Text.Length==1){
				textCity.Text=textCity.Text.ToUpper();
				textCity.SelectionStart=1;
			}
		}

		private void textState_TextChanged(object sender, System.EventArgs e) {
			if(CultureInfo.CurrentCulture.Name=="en-US" //if USA or Canada, capitalize first 2 letters
				|| (CultureInfo.CurrentCulture.Name.Length>=4 && CultureInfo.CurrentCulture.Name.Substring(3)=="CA")){
				if(textState.Text.Length==1 || textState.Text.Length==2){
					textState.Text=textState.Text.ToUpper();
					textState.SelectionStart=2;
				}
			}
			else{
				if(textState.Text.Length==1){
					textState.Text=textState.Text.ToUpper();
					textState.SelectionStart=1;
				}
			}
		}

		private void textZip_TextChanged(object sender, System.EventArgs e) {
			comboZip.SelectedIndex=-1;
		}

		private void comboZip_SelectionChangeCommitted(object sender, System.EventArgs e) {
			//this happens when a zipcode is selected from the combobox of frequent zips.
			//The combo box is tucked under textZip because Microsoft makes stupid controls.
			textCity.Text=((ZipCode)ZipCodes.ALFrequent[comboZip.SelectedIndex]).City;
			textState.Text=((ZipCode)ZipCodes.ALFrequent[comboZip.SelectedIndex]).State;
			textZip.Text=((ZipCode)ZipCodes.ALFrequent[comboZip.SelectedIndex]).ZipCodeDigits;
		}

		private void textZip_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
			//fired as soon as control loses focus.
			//it's here to validate if zip is typed in to text box instead of picked from list.
			//if(textZip.Text=="" && (textCity.Text!="" || textState.Text!="")){
			//	if(MessageBox.Show(Lan.g(this,"Delete the City and State?"),"",MessageBoxButtons.OKCancel)
			//		==DialogResult.OK){
			//		textCity.Text="";
			//		textState.Text="";
			//	}	
			//	return;
			//}
			if(textZip.Text.Length<5){
				return;
			}
			if(comboZip.SelectedIndex!=-1){
				return;
			}
			//the autofill only works if both city and state are left blank
			if(textCity.Text!="" || textState.Text!=""){
				return;
			}
			ZipCodes.GetALMatches(textZip.Text);
			if(ZipCodes.ALMatches.Count==0){
				//No match found. Must enter info for new zipcode
				ZipCode ZipCodeCur=new ZipCode();
				ZipCodeCur.ZipCodeDigits=textZip.Text;
				FormZipCodeEdit FormZE=new FormZipCodeEdit();
				FormZE.ZipCodeCur=ZipCodeCur;
				FormZE.IsNew=true;
				FormZE.ShowDialog();
				if(FormZE.DialogResult!=DialogResult.OK){
					return;
				}
				DataValid.SetInvalid(InvalidType.ZipCodes);//FormZipCodeEdit does not contain internal refresh
				FillComboZip();
				textCity.Text=ZipCodeCur.City;
				textState.Text=ZipCodeCur.State;
				textZip.Text=ZipCodeCur.ZipCodeDigits;
			}
			else if(ZipCodes.ALMatches.Count==1){
				//only one match found.  Use it.
				textCity.Text=((ZipCode)ZipCodes.ALMatches[0]).City;
				textState.Text=((ZipCode)ZipCodes.ALMatches[0]).State;
			}
			else{
				//multiple matches found.  Pick one
				FormZipSelect FormZS=new FormZipSelect();
				FormZS.ShowDialog();
				FillComboZip();
				if(FormZS.DialogResult!=DialogResult.OK){
					return;
				}
				DataValid.SetInvalid(InvalidType.ZipCodes);
				textCity.Text=FormZS.ZipSelected.City;
				textState.Text=FormZS.ZipSelected.State;
				textZip.Text=FormZS.ZipSelected.ZipCodeDigits;
			}
		}

		private void FillComboZip(){
			comboZip.Items.Clear();
			for(int i=0;i<ZipCodes.ALFrequent.Count;i++){
				comboZip.Items.Add(((ZipCode)ZipCodes.ALFrequent[i]).ZipCodeDigits
					+"("+((ZipCode)ZipCodes.ALFrequent[i]).City+")");
			}
		}
		#endregion AddressPhone

		#region Referral
		///<summary>Fills the referral fields based on the specified referralNum.</summary>
		private void FillReferral(int referralNum) {
			selectedReferral=Referrals.GetReferral(referralNum);
			textReferral.Text=selectedReferral.LName;
			textReferralFName.Text=selectedReferral.FName;
		}

		private void textReferral_KeyUp(object sender,System.Windows.Forms.KeyEventArgs e) {
			if(e.KeyCode==Keys.Return) {
				if(listReferral.SelectedIndex==-1) {
					textReferralFName.Focus();
				}
				else {
					FillReferral(similarReferrals[listReferral.SelectedIndex].ReferralNum);
					textReferral.Focus();
					textReferral.SelectionStart=textReferral.Text.Length;
				}
				listReferral.Visible=false;
				return;
			}
			if(textReferral.Text=="") {
				listReferral.Visible=false;
				return;
			}
			if(e.KeyCode==Keys.Down) {
				if(listReferral.Items.Count==0) {
					return;
				}
				if(listReferral.SelectedIndex==-1) {
					listReferral.SelectedIndex=0;
					textReferral.Text=similarReferrals[listReferral.SelectedIndex].LName;
				}
				else if(listReferral.SelectedIndex==listReferral.Items.Count-1) {
					listReferral.SelectedIndex=-1;
					textReferral.Text=referralOriginal;
				}
				else {
					listReferral.SelectedIndex++;
					textReferral.Text=similarReferrals[listReferral.SelectedIndex].LName;
				}
				textReferral.SelectionStart=textReferral.Text.Length;
				return;
			}
			if(e.KeyCode==Keys.Up) {
				if(listReferral.Items.Count==0) {
					return;
				}
				if(listReferral.SelectedIndex==-1) {
					listReferral.SelectedIndex=listReferral.Items.Count-1;
					textReferral.Text=similarReferrals[listReferral.SelectedIndex].LName;
				}
				else if(listReferral.SelectedIndex==0) {
					listReferral.SelectedIndex=-1;
					textReferral.Text=referralOriginal;
				}
				else {
					listReferral.SelectedIndex--;
					textReferral.Text=similarReferrals[listReferral.SelectedIndex].LName;
				}
				textReferral.SelectionStart=textReferral.Text.Length;
				return;
			}
			if(textReferral.Text.Length==1) {
				textReferral.Text=textReferral.Text.ToUpper();
				textReferral.SelectionStart=1;
			}
			referralOriginal=textReferral.Text;//the original text is preserved when using up and down arrows
			listReferral.Items.Clear();
			similarReferrals=Referrals.GetSimilarNames(textReferral.Text);
			for(int i=0;i<similarReferrals.Count;i++) {
				listReferral.Items.Add(similarReferrals[i].LName+", "
					+similarReferrals[i].FName+", "
					+similarReferrals[i].Title+", "
					+similarReferrals[i].Note);
			}
			int h=13*similarReferrals.Count+5;
			if(h > ClientSize.Height-listReferral.Top){
				h=ClientSize.Height-listReferral.Top;
			}
			listReferral.Size=new Size(listReferral.Width,h);
			listReferral.Visible=true;
		}

		private void textReferral_Leave(object sender,System.EventArgs e) {
			if(mouseIsInListReferral) {
				return;
			}
			//or if user clicked on a different text box.
			if(listReferral.SelectedIndex!=-1) {
				FillReferral(similarReferrals[listReferral.SelectedIndex].ReferralNum);
			}
			listReferral.Visible=false;
		}

		private void listReferral_Click(object sender,System.EventArgs e) {
			FillReferral(similarReferrals[listReferral.SelectedIndex].ReferralNum);
			textReferral.Focus();
			textReferral.SelectionStart=textReferral.Text.Length;
			listReferral.Visible=false;
		}

		private void listReferral_DoubleClick(object sender,System.EventArgs e) {
			//no longer used
		}

		private void listReferral_MouseEnter(object sender,System.EventArgs e) {
			mouseIsInListReferral=true;
		}

		private void listReferral_MouseLeave(object sender,System.EventArgs e) {
			mouseIsInListReferral=false;
		}
		#endregion Referral

		#region Subscriber
		///<summary>Resets the text for each of the six options in the dropdown.  Does this without changing the selected index.</summary>
		private void ResetSubscriberLists(){
			int selectedIndex1=comboSubscriber1.SelectedIndex;
			int selectedIndex2=comboSubscriber2.SelectedIndex;
			comboSubscriber1.Items.Clear();
			comboSubscriber2.Items.Clear();
			comboSubscriber1.Items.Add(Lan.g(this,"none"));
			comboSubscriber2.Items.Add(Lan.g(this,"none"));
			string str;
			for(int i=0;i<5;i++){
				str=(i+1).ToString()+" - ";
				switch(i){
					case 0:
						str+=textLName1.Text+", "+textFName1.Text;
						break;
					case 1:
						str+=textLName2.Text+", "+textFName2.Text;
						break;
					case 2:
						str+=textLName3.Text+", "+textFName3.Text;
						break;
					case 3:
						str+=textLName4.Text+", "+textFName4.Text;
						break;
					case 4:
						str+=textLName5.Text+", "+textFName5.Text;
						break;
				}
				comboSubscriber1.Items.Add(str);
				comboSubscriber2.Items.Add(str);
			}
			if(selectedIndex1==-1){
				comboSubscriber1.SelectedIndex=0;
			}
			else{
				comboSubscriber1.SelectedIndex=selectedIndex1;
			}
			if(selectedIndex2==-1){
				comboSubscriber2.SelectedIndex=0;
			}
			else{
				comboSubscriber2.SelectedIndex=selectedIndex2;
			}
		}
		#endregion Subscriber

		#region Employer
		private void textEmployer1_KeyUp(object sender,System.Windows.Forms.KeyEventArgs e) {
			//key up is used because that way it will trigger AFTER the textBox has been changed.
			if(e.KeyCode==Keys.Return) {
				listEmps1.Visible=false;
				textCarrier1.Focus();
				return;
			}
			if(textEmployer1.Text=="") {
				listEmps1.Visible=false;
				return;
			}
			if(e.KeyCode==Keys.Down) {
				if(listEmps1.Items.Count==0) {
					return;
				}
				if(listEmps1.SelectedIndex==-1) {
					listEmps1.SelectedIndex=0;
					textEmployer1.Text=listEmps1.SelectedItem.ToString();
				}
				else if(listEmps1.SelectedIndex==listEmps1.Items.Count-1) {
					listEmps1.SelectedIndex=-1;
					textEmployer1.Text=empOriginal1;
				}
				else {
					listEmps1.SelectedIndex++;
					textEmployer1.Text=listEmps1.SelectedItem.ToString();
				}
				textEmployer1.SelectionStart=textEmployer1.Text.Length;
				return;
			}
			if(e.KeyCode==Keys.Up) {
				if(listEmps1.Items.Count==0) {
					return;
				}
				if(listEmps1.SelectedIndex==-1) {
					listEmps1.SelectedIndex=listEmps1.Items.Count-1;
					textEmployer1.Text=listEmps1.SelectedItem.ToString();
				}
				else if(listEmps1.SelectedIndex==0) {
					listEmps1.SelectedIndex=-1;
					textEmployer1.Text=empOriginal1;
				}
				else {
					listEmps1.SelectedIndex--;
					textEmployer1.Text=listEmps1.SelectedItem.ToString();
				}
				textEmployer1.SelectionStart=textEmployer1.Text.Length;
				return;
			}
			if(textEmployer1.Text.Length==1) {
				textEmployer1.Text=textEmployer1.Text.ToUpper();
				textEmployer1.SelectionStart=1;
			}
			empOriginal1=textEmployer1.Text;//the original text is preserved when using up and down arrows
			listEmps1.Items.Clear();
			List<Employer> similarEmps=Employers.GetSimilarNames(textEmployer1.Text);
			for(int i=0;i<similarEmps.Count;i++) {
				listEmps1.Items.Add(similarEmps[i].EmpName);
			}
			int h=13*similarEmps.Count+5;
			if(h > ClientSize.Height-listEmps1.Top){
				h=ClientSize.Height-listEmps1.Top;
			}
			listEmps1.Size=new Size(231,h);
			listEmps1.Visible=true;
		}

		private void textEmployer1_Leave(object sender,System.EventArgs e) {
			if(mouseIsInListEmps1) {
				return;
			}
			listEmps1.Visible=false;
		}

		private void listEmps1_Click(object sender,System.EventArgs e) {
			textEmployer1.Text=listEmps1.SelectedItem.ToString();
			textEmployer1.Focus();
			textEmployer1.SelectionStart=textEmployer1.Text.Length;
			listEmps1.Visible=false;
		}

		private void listEmps1_DoubleClick(object sender,System.EventArgs e) {
			//no longer used
			textEmployer1.Text=listEmps1.SelectedItem.ToString();
			textEmployer1.Focus();
			textEmployer1.SelectionStart=textEmployer1.Text.Length;
			listEmps1.Visible=false;
		}

		private void listEmps1_MouseEnter(object sender,System.EventArgs e) {
			mouseIsInListEmps1=true;
		}

		private void listEmps1_MouseLeave(object sender,System.EventArgs e) {
			mouseIsInListEmps1=false;
		}

		private void textEmployer2_KeyUp(object sender,System.Windows.Forms.KeyEventArgs e) {
			//key up is used because that way it will trigger AFTER the textBox has been changed.
			if(e.KeyCode==Keys.Return) {
				listEmps2.Visible=false;
				textCarrier2.Focus();
				return;
			}
			if(textEmployer2.Text=="") {
				listEmps2.Visible=false;
				return;
			}
			if(e.KeyCode==Keys.Down) {
				if(listEmps2.Items.Count==0) {
					return;
				}
				if(listEmps2.SelectedIndex==-1) {
					listEmps2.SelectedIndex=0;
					textEmployer2.Text=listEmps2.SelectedItem.ToString();
				}
				else if(listEmps2.SelectedIndex==listEmps2.Items.Count-1) {
					listEmps2.SelectedIndex=-1;
					textEmployer2.Text=empOriginal2;
				}
				else {
					listEmps2.SelectedIndex++;
					textEmployer2.Text=listEmps2.SelectedItem.ToString();
				}
				textEmployer2.SelectionStart=textEmployer2.Text.Length;
				return;
			}
			if(e.KeyCode==Keys.Up) {
				if(listEmps2.Items.Count==0) {
					return;
				}
				if(listEmps2.SelectedIndex==-1) {
					listEmps2.SelectedIndex=listEmps2.Items.Count-1;
					textEmployer2.Text=listEmps2.SelectedItem.ToString();
				}
				else if(listEmps2.SelectedIndex==0) {
					listEmps2.SelectedIndex=-1;
					textEmployer2.Text=empOriginal2;
				}
				else {
					listEmps2.SelectedIndex--;
					textEmployer2.Text=listEmps2.SelectedItem.ToString();
				}
				textEmployer2.SelectionStart=textEmployer2.Text.Length;
				return;
			}
			if(textEmployer2.Text.Length==1) {
				textEmployer2.Text=textEmployer2.Text.ToUpper();
				textEmployer2.SelectionStart=1;
			}
			empOriginal2=textEmployer2.Text;//the original text is preserved when using up and down arrows
			listEmps2.Items.Clear();
			List<Employer> similarEmps2=Employers.GetSimilarNames(textEmployer2.Text);
			for(int i=0;i<similarEmps2.Count;i++) {
				listEmps2.Items.Add(similarEmps2[i].EmpName);
			}
			int h=13*similarEmps2.Count+5;
			if(h > ClientSize.Height-listEmps2.Top){
				h=ClientSize.Height-listEmps2.Top;
			}
			listEmps2.Size=new Size(231,h);
			listEmps2.Visible=true;
		}

		private void textEmployer2_Leave(object sender,System.EventArgs e) {
			if(mouseIsInListEmps2) {
				return;
			}
			listEmps2.Visible=false;
		}

		private void listEmps2_Click(object sender,System.EventArgs e) {
			textEmployer2.Text=listEmps2.SelectedItem.ToString();
			textEmployer2.Focus();
			textEmployer2.SelectionStart=textEmployer2.Text.Length;
			listEmps2.Visible=false;
		}

		private void listEmps2_DoubleClick(object sender,System.EventArgs e) {
			//no longer used
			textEmployer2.Text=listEmps2.SelectedItem.ToString();
			textEmployer2.Focus();
			textEmployer2.SelectionStart=textEmployer2.Text.Length;
			listEmps2.Visible=false;
		}

		private void listEmps2_MouseEnter(object sender,System.EventArgs e) {
			mouseIsInListEmps2=true;
		}

		private void listEmps2_MouseLeave(object sender,System.EventArgs e) {
			mouseIsInListEmps2=false;
		}
		#endregion Employer

		#region Carrier
		///<summary>Fills the carrier fields on the form based on the specified carrierNum.</summary>
		private void FillCarrier1(int carrierNum) {
			selectedCarrier1=Carriers.GetCarrier(carrierNum);
			textCarrier1.Text=selectedCarrier1.CarrierName;
			textPhone1.Text=selectedCarrier1.Phone;
		}

		private void textCarrier1_KeyUp(object sender,System.Windows.Forms.KeyEventArgs e) {
			if(e.KeyCode==Keys.Return) {
				if(listCars1.SelectedIndex==-1) {
					textPhone1.Focus();
				}
				else {
					FillCarrier1(similarCars1[listCars1.SelectedIndex].CarrierNum);
					textCarrier1.Focus();
					textCarrier1.SelectionStart=textCarrier1.Text.Length;
				}
				listCars1.Visible=false;
				return;
			}
			if(textCarrier1.Text=="") {
				listCars1.Visible=false;
				return;
			}
			if(e.KeyCode==Keys.Down) {
				if(listCars1.Items.Count==0) {
					return;
				}
				if(listCars1.SelectedIndex==-1) {
					listCars1.SelectedIndex=0;
					textCarrier1.Text=similarCars1[listCars1.SelectedIndex].CarrierName;
				}
				else if(listCars1.SelectedIndex==listCars1.Items.Count-1) {
					listCars1.SelectedIndex=-1;
					textCarrier1.Text=carOriginal1;
				}
				else {
					listCars1.SelectedIndex++;
					textCarrier1.Text=similarCars1[listCars1.SelectedIndex].CarrierName;
				}
				textCarrier1.SelectionStart=textCarrier1.Text.Length;
				return;
			}
			if(e.KeyCode==Keys.Up) {
				if(listCars1.Items.Count==0) {
					return;
				}
				if(listCars1.SelectedIndex==-1) {
					listCars1.SelectedIndex=listCars1.Items.Count-1;
					textCarrier1.Text=similarCars1[listCars1.SelectedIndex].CarrierName;
				}
				else if(listCars1.SelectedIndex==0) {
					listCars1.SelectedIndex=-1;
					textCarrier1.Text=carOriginal1;
				}
				else {
					listCars1.SelectedIndex--;
					textCarrier1.Text=similarCars1[listCars1.SelectedIndex].CarrierName;
				}
				textCarrier1.SelectionStart=textCarrier1.Text.Length;
				return;
			}
			if(textCarrier1.Text.Length==1) {
				textCarrier1.Text=textCarrier1.Text.ToUpper();
				textCarrier1.SelectionStart=1;
			}
			carOriginal1=textCarrier1.Text;//the original text is preserved when using up and down arrows
			listCars1.Items.Clear();
			similarCars1=Carriers.GetSimilarNames(textCarrier1.Text);
			for(int i=0;i<similarCars1.Count;i++) {
				listCars1.Items.Add(similarCars1[i].CarrierName+", "
					+similarCars1[i].Phone+", "
					+similarCars1[i].Address+", "
					+similarCars1[i].Address2+", "
					+similarCars1[i].City+", "
					+similarCars1[i].State+", "
					+similarCars1[i].Zip);
			}
			int h=13*similarCars1.Count+5;
			if(h > ClientSize.Height-listCars1.Top){
				h=ClientSize.Height-listCars1.Top;
			}
			listCars1.Size=new Size(listCars1.Width,h);
			listCars1.Visible=true;
		}

		private void textCarrier1_Leave(object sender,System.EventArgs e) {
			if(mouseIsInListCars1) {
				return;
			}
			//or if user clicked on a different text box.
			if(listCars1.SelectedIndex!=-1) {
				FillCarrier1(similarCars1[listCars1.SelectedIndex].CarrierNum);
			}
			listCars1.Visible=false;
		}

		private void listCars1_Click(object sender,System.EventArgs e) {
			FillCarrier1(similarCars1[listCars1.SelectedIndex].CarrierNum);
			textCarrier1.Focus();
			textCarrier1.SelectionStart=textCarrier1.Text.Length;
			listCars1.Visible=false;
		}

		private void listCars1_DoubleClick(object sender,System.EventArgs e) {
			//no longer used
		}

		private void listCars1_MouseEnter(object sender,System.EventArgs e) {
			mouseIsInListCars1=true;
		}

		private void listCars1_MouseLeave(object sender,System.EventArgs e) {
			mouseIsInListCars1=false;
		}

		///<summary>Fills the carrier fields on the form based on the specified carrierNum.</summary>
		private void FillCarrier2(int carrierNum) {
			selectedCarrier2=Carriers.GetCarrier(carrierNum);
			textCarrier2.Text=selectedCarrier2.CarrierName;
			textPhone2.Text=selectedCarrier2.Phone;
		}

		private void textCarrier2_KeyUp(object sender,System.Windows.Forms.KeyEventArgs e) {
			if(e.KeyCode==Keys.Return) {
				if(listCars2.SelectedIndex==-1) {
					textPhone2.Focus();
				}
				else {
					FillCarrier2(similarCars2[listCars2.SelectedIndex].CarrierNum);
					textCarrier2.Focus();
					textCarrier2.SelectionStart=textCarrier2.Text.Length;
				}
				listCars2.Visible=false;
				return;
			}
			if(textCarrier2.Text=="") {
				listCars2.Visible=false;
				return;
			}
			if(e.KeyCode==Keys.Down) {
				if(listCars2.Items.Count==0) {
					return;
				}
				if(listCars2.SelectedIndex==-1) {
					listCars2.SelectedIndex=0;
					textCarrier2.Text=similarCars2[listCars2.SelectedIndex].CarrierName;
				}
				else if(listCars2.SelectedIndex==listCars2.Items.Count-1) {
					listCars2.SelectedIndex=-1;
					textCarrier2.Text=carOriginal2;
				}
				else {
					listCars2.SelectedIndex++;
					textCarrier2.Text=similarCars2[listCars2.SelectedIndex].CarrierName;
				}
				textCarrier2.SelectionStart=textCarrier2.Text.Length;
				return;
			}
			if(e.KeyCode==Keys.Up) {
				if(listCars2.Items.Count==0) {
					return;
				}
				if(listCars2.SelectedIndex==-1) {
					listCars2.SelectedIndex=listCars2.Items.Count-1;
					textCarrier2.Text=similarCars2[listCars2.SelectedIndex].CarrierName;
				}
				else if(listCars2.SelectedIndex==0) {
					listCars2.SelectedIndex=-1;
					textCarrier2.Text=carOriginal2;
				}
				else {
					listCars2.SelectedIndex--;
					textCarrier2.Text=similarCars2[listCars2.SelectedIndex].CarrierName;
				}
				textCarrier2.SelectionStart=textCarrier2.Text.Length;
				return;
			}
			if(textCarrier2.Text.Length==1) {
				textCarrier2.Text=textCarrier2.Text.ToUpper();
				textCarrier2.SelectionStart=1;
			}
			carOriginal2=textCarrier2.Text;//the original text is preserved when using up and down arrows
			listCars2.Items.Clear();
			similarCars2=Carriers.GetSimilarNames(textCarrier2.Text);
			for(int i=0;i<similarCars2.Count;i++) {
				listCars2.Items.Add(similarCars2[i].CarrierName+", "
					+similarCars2[i].Phone+", "
					+similarCars2[i].Address+", "
					+similarCars2[i].Address2+", "
					+similarCars2[i].City+", "
					+similarCars2[i].State+", "
					+similarCars2[i].Zip);
			}
			int h=13*similarCars2.Count+5;
			if(h > ClientSize.Height-listCars2.Top){
				h=ClientSize.Height-listCars2.Top;
			}
			listCars2.Size=new Size(listCars2.Width,h);
			listCars2.Visible=true;
		}

		private void textCarrier2_Leave(object sender,System.EventArgs e) {
			if(mouseIsInListCars2) {
				return;
			}
			//or if user clicked on a different text box.
			if(listCars2.SelectedIndex!=-1) {
				FillCarrier2(similarCars2[listCars2.SelectedIndex].CarrierNum);
			}
			listCars2.Visible=false;
		}

		private void listCars2_Click(object sender,System.EventArgs e) {
			FillCarrier2(similarCars2[listCars2.SelectedIndex].CarrierNum);
			textCarrier2.Focus();
			textCarrier2.SelectionStart=textCarrier2.Text.Length;
			listCars2.Visible=false;
		}

		private void listCars2_DoubleClick(object sender,System.EventArgs e) {
			//no longer used
		}

		private void listCars2_MouseEnter(object sender,System.EventArgs e) {
			mouseIsInListCars2=true;
		}

		private void listCars2_MouseLeave(object sender,System.EventArgs e) {
			mouseIsInListCars2=false;
		}
		#endregion Carrier

		#region InsPlanPick
		private void butPick1_Click(object sender,EventArgs e) {
			FormInsPlans FormIP=new FormInsPlans();
			FormIP.empText=textEmployer1.Text;
			FormIP.carrierText=textCarrier1.Text;
			FormIP.IsSelectMode=true;
			FormIP.ShowDialog();
			if(FormIP.DialogResult==DialogResult.Cancel) {
				return;
			}
			selectedPlan1=FormIP.SelectedPlan.Copy();
			//Non-synched fields:
			//selectedPlan1.SubscriberID=textSubscriberID.Text;//later
			selectedPlan1.DateEffective=DateTime.MinValue;
			selectedPlan1.DateTerm=DateTime.MinValue;
			//PlanCur.ReleaseInfo=checkRelease.Checked;
			//PlanCur.AssignBen=checkAssign.Checked;
			//PlanCur.SubscNote=textSubscNote.Text;


			/*
			//PlanCur.Update();//updates to the db so that the synch info will show correctly
			FillFormWithPlanCur();
			//need to clear benefits for this plan now.  That way they won't be available as benefits of an 'identical' plan.
			Benefits.DeleteForPlan(PlanCur.PlanNum);
			benefitList=Benefits.RefreshForAll(PlanCur);//these benefits are only typical, not precise.
			//set all benefitNums to 0 to trigger having them saved as new
			for(int i=0;i<benefitList.Count;i++) {
				((Benefit)benefitList[i]).BenefitNum=0;
			}
			Benefits.UpdateList(benefitListOld,benefitList);//replaces all benefits for this plan in the database
			FillBenefits();
			//The new data on the form should be the basis for any 'changes to all'.
			PlanCurOld=PlanCur.Copy();
			benefitListOld=new List<Benefit>(benefitList);*/
			//It's as if we've just opened a new form now.
		}

		private void butPick2_Click(object sender,EventArgs e) {

		}
		#endregion InsPlanPick

		private void butOK_Click(object sender,EventArgs e) {
			if(  textBirthdate1.errorProvider1.GetError(textBirthdate1)!=""
				|| textBirthdate2.errorProvider1.GetError(textBirthdate2)!=""
				|| textBirthdate3.errorProvider1.GetError(textBirthdate3)!=""
				|| textBirthdate4.errorProvider1.GetError(textBirthdate4)!=""
				|| textBirthdate5.errorProvider1.GetError(textBirthdate5)!=""
				){
				MsgBox.Show(this,"Please fix data entry errors first.");
				return;
			}
			if(textLName1.Text=="" || textFName1.Text==""){
				MsgBox.Show(this,"Guarantor name must be entered.");
				return;
			}
			if((comboSubscriber1.SelectedIndex==2 || comboSubscriber2.SelectedIndex==2) && (textFName2.Text=="" || textLName2.Text=="")){
				MsgBox.Show(this,"Subscriber must have name entered.");
				return;
			}
			if((comboSubscriber1.SelectedIndex==3 || comboSubscriber2.SelectedIndex==3) && (textFName3.Text=="" || textLName3.Text=="")){
				MsgBox.Show(this,"Subscriber must have name entered.");
				return;
			}
			if((comboSubscriber1.SelectedIndex==4 || comboSubscriber2.SelectedIndex==4) && (textFName4.Text=="" || textLName4.Text=="")){
				MsgBox.Show(this,"Subscriber must have name entered.");
				return;
			}
			if((comboSubscriber1.SelectedIndex==5 || comboSubscriber2.SelectedIndex==5) && (textFName5.Text=="" || textLName5.Text=="")){
				MsgBox.Show(this,"Subscriber must have name entered.");
				return;
			}








			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender,EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		

		

		

		





		

		

		

		

		




	

		
	}
}