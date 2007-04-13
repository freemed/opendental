using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using OpenDentBusiness;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormImportXML : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		///<summary></summary>
		public TextBox textMain;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private Patient pat;
		private Patient guar;
		private Patient subsc;
		private InsPlan plan;
		private Carrier carrier;
		private string targetVersion="";
		private string warnings="";
		///<summary>self, parent, or other</summary>
		private string guarRelat;
		private string GuarEmp;
		private string InsEmp;
		///<summary>self, parent, spouse, or guardian</summary>
		private string insRelat;
		private string NoteMedicalComp;
		private bool insPresent;
		private double annualMax;
		private double deductible;
		///<summary>Public for NewPatientForm.com functionality</summary>
		public Patient existingPatOld;

		///<summary></summary>
		public FormImportXML()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			Lan.F(this);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormImportXML));
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.textMain = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.Location = new System.Drawing.Point(756,649);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,26);
			this.butCancel.TabIndex = 0;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(756,613);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 1;
			this.butOK.Text = "Import";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// textMain
			// 
			this.textMain.Location = new System.Drawing.Point(7,7);
			this.textMain.Multiline = true;
			this.textMain.Name = "textMain";
			this.textMain.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textMain.Size = new System.Drawing.Size(737,673);
			this.textMain.TabIndex = 2;
			// 
			// FormImportXML
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(847,687);
			this.Controls.Add(this.textMain);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormImportXML";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Import Patient";
			this.Load += new System.EventHandler(this.FormImportXML_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormImportXML_Load(object sender, System.EventArgs e) {
			
		}

		///<summary></summary>
		public void butOK_Click(object sender, System.EventArgs e) {//this is public for NewPatientForm bridge
			if(textMain.Text==""){
				MsgBox.Show(this,"Please paste the text generated by the other program into the large box first.");
				return;
			}
			pat=new Patient();
			pat.PriProv=PrefB.GetInt("PracticeDefaultProv");
			pat.BillingType=PrefB.GetInt("PracticeDefaultBillType");
			guar=new Patient();
			guar.PriProv=PrefB.GetInt("PracticeDefaultProv");
			guar.BillingType=PrefB.GetInt("PracticeDefaultBillType");
			subsc=new Patient();
			subsc.PriProv=PrefB.GetInt("PracticeDefaultProv");
			subsc.BillingType=PrefB.GetInt("PracticeDefaultBillType");
			plan=new InsPlan();
			plan.ReleaseInfo=true;
			plan.AssignBen=true;
			carrier=new Carrier();
			insRelat="self";//this is the default if not included
			guarRelat="self";
			InsEmp="";
			GuarEmp="";
			NoteMedicalComp="";
			insPresent=false;
			annualMax=-1;
			deductible=-1;
			XmlTextReader reader=new XmlTextReader(new StringReader(textMain.Text));
			reader.WhitespaceHandling=WhitespaceHandling.None;
			string element="";
			string textValue="";
			string rootElement="";
			string segment="";//eg PatientIdentification
			string field="";//eg NameLast
			string endelement="";
			warnings="";
			try{
				while(reader.Read()){
					switch(reader.NodeType){
						case XmlNodeType.Element:
							element=reader.Name;
							if(rootElement==""){//should be the first node
								if(element=="Message"){
									rootElement="Message";
								}
								else{
									throw new Exception(element+" should not be the first element.");
								}
							}
							else if(segment==""){//expecting a new segment
								segment=element;
								if(segment!="MessageHeader"
									&& segment!="PatientIdentification"
									&& segment!="Guarantor"
									&& segment!="Insurance")
								{
									throw new Exception(segment+" is not a recognized segment.");							
								}
							}
							else{//expecting a new field
								field=element;
							}
							if(segment=="Insurance"){
								insPresent=true;
							}
							break;
						case XmlNodeType.Text:
							textValue=reader.Value;
							if(field==""){
								throw new Exception("Unexpected text: "+textValue);	
							}
							break;
						case XmlNodeType.EndElement:
							endelement=reader.Name;
							if(field==""){//we're not in a field, so we must be closing a segment or rootelement
								if(segment==""){//we're not in a segment, so we must be closing the rootelement
									if(rootElement=="Message"){
										rootElement="";
									}
									else{
										throw new Exception("Message closing element expected.");
									}
								}
								else{//must be closing a segment
									segment="";
								}
							}
							else{//closing a field
								field="";
								textValue="";
							}
							break;
					}//switch 
					if(rootElement==""){
						break;//this will ignore anything after the message endelement
					}
					if(field!="" && textValue!=""){
						if(segment=="MessageHeader"){
							ProcessMSH(field,textValue);
						}
						else if(segment=="PatientIdentification"){
							ProcessPID(field,textValue);
						}
						else if(segment=="Guarantor"){
							ProcessGT(field,textValue);
						}
						else if(segment=="Insurance"){
							ProcessINS(field,textValue);
						}
					}
				}//while
			}
			catch(Exception ex){
				MessageBox.Show(ex.Message);
				//MsgBox.Show(this,"Error in the XML format.");
				reader.Close();
				return;
			}
			finally{
				reader.Close();
			}
			//Warnings and errors-----------------------------------------------------------------------------
			if(pat.LName=="" || pat.FName=="" || pat.Birthdate.Year<1880){
				MsgBox.Show(this,"Patient first and last name and birthdate are required.  Could not import.");
				return;
			}
			//if guarRelat is not self, and name and birthdate not supplied, no error.  Just make guar self.
			if(guarRelat!="self"){
				if(guar.LName=="" || guar.FName=="" || guar.Birthdate.Year<1880){
					warnings+="Guarantor information incomplete.  Guarantor will be self.\r\n";
					guarRelat="self";
				}
			}
			if(insPresent){
				if(carrier.CarrierName==""){
					warnings+="Insurance CompanyName is missing. No insurance info will be imported.\r\n";
					insPresent=false;
				}
				else if(insRelat!="self"){
					if(subsc.LName=="" || subsc.FName=="" || subsc.Birthdate.Year<1880){
						warnings+="Subscriber name or birthdate is missing. No insurance info will be imported.\r\n";
						insPresent=false;
					}
				}
				else if(plan.SubscriberID==""){
					warnings+="PolicyNumber/SubscriberID missing.\r\n";
					plan.SubscriberID=" ";
				}
			}
			if(warnings!=""){
				if(MessageBox.Show("It's safe to import, but you should be aware of the following issues:\r\n"+warnings+"\r\nContinue with Import?","Warnings",MessageBoxButtons.OKCancel)!=DialogResult.OK){
					return;
				}
			}
			
			//Patient-------------------------------------------------------------------------------------
			string command;
			DataTable table;
			command="SELECT PatNum FROM patient WHERE "
				+"LName='"+POut.PString(pat.LName)+"' "
				+"AND FName='"+POut.PString(pat.FName)+"' "
				+"AND Birthdate="+POut.PDate(pat.Birthdate)+" "
				+"AND PatStatus!=4";//not deleted
			table=General.GetTable(command);
			Patient existingPat=null;
			existingPatOld=null;//we will need this to do an update.
			if(table.Rows.Count>0){//a patient already exists, so only add missing fields
				existingPat=Patients.GetPat(PIn.PInt(table.Rows[0][0].ToString()));
				existingPatOld=existingPat.Copy();
				if(existingPat.MiddleI==""){//only alter existing if blank
					existingPat.MiddleI=pat.MiddleI;
				}
				if(pat.Gender!=PatientGender.Unknown){
					existingPat.Gender=pat.Gender;
				}
				if(existingPat.Preferred==""){
					existingPat.Preferred=pat.Preferred;
				}
				if(existingPat.Address==""){
					existingPat.Address=pat.Address;
				}
				if(existingPat.Address2==""){
					existingPat.Address2=pat.Address2;
				}
				if(existingPat.City==""){
					existingPat.City=pat.City;
				}
				if(existingPat.State==""){
					existingPat.State=pat.State;
				}
				if(existingPat.Zip==""){
					existingPat.Zip=pat.Zip;
				}
				if(existingPat.HmPhone==""){
					existingPat.HmPhone=pat.HmPhone;
				}
				if(existingPat.Email==""){
					existingPat.Email=pat.Email;
				}
				if(existingPat.WkPhone==""){
					existingPat.WkPhone=pat.WkPhone;
				}
				if(existingPat.Position==PatientPosition.Single){
					existingPat.Position=pat.Position;
				}
				if(existingPat.SSN==""){
					existingPat.SSN=pat.SSN;
				}
				existingPat.AddrNote+=pat.AddrNote;//concat
				Patients.Update(existingPat,existingPatOld);
				PatientNote PatientNoteCur=PatientNotes.Refresh(existingPat.PatNum,existingPat.Guarantor);
				PatientNoteCur.MedicalComp+=NoteMedicalComp;
				PatientNotes.Update(PatientNoteCur,existingPat.Guarantor);
				//guarantor will not be altered in any way
			}//if patient already exists
			else{//patient is new, so insert
				Patients.Insert(pat,false);
				existingPatOld=pat.Copy();
				pat.Guarantor=pat.PatNum;//this can be changed later.
				Patients.Update(pat,existingPatOld);
				PatientNote PatientNoteCur=PatientNotes.Refresh(pat.PatNum,pat.Guarantor);
				PatientNoteCur.MedicalComp+=NoteMedicalComp;
				PatientNotes.Update(PatientNoteCur,pat.Guarantor);
			}
			//guar-----------------------------------------------------------------------------------------------------
			if(existingPat==null){//only add or alter guarantor for new patients
				if(guarRelat=="self"){
					//pat is already set with guar as self
					//ignore all guar fields except EmployerName
					existingPatOld=pat.Copy();
					pat.EmployerNum=Employers.GetEmployerNum(GuarEmp);
					Patients.Update(pat,existingPatOld);
				}
				else{
					//if guarRelat is not self, and name and birthdate not supplied, a warning was issued, and relat was changed to self.
					//add guarantor or attach to an existing guarantor
					command="SELECT PatNum FROM patient WHERE "
						+"LName='"+POut.PString(guar.LName)+"' "
						+"AND FName='"+POut.PString(guar.FName)+"' "
						+"AND Birthdate="+POut.PDate(guar.Birthdate)+" "
						+"AND PatStatus!=4";//not deleted
					table=General.GetTable(command);
					if(table.Rows.Count>0){//a guar already exists, so simply attach. Make no other changes
						existingPatOld=pat.Copy();
						pat.Guarantor=PIn.PInt(table.Rows[0][0].ToString());
						if(guarRelat=="parent"){
							pat.Position=PatientPosition.Child;
						}
						Patients.Update(pat,existingPatOld);
					}
					else{//we need to completely create guar, then attach
						Patients.Insert(guar,false);
						//set guar for guar
						existingPatOld=guar.Copy();
						guar.Guarantor=guar.PatNum;
						guar.EmployerNum=Employers.GetEmployerNum(GuarEmp);
						Patients.Update(guar,existingPatOld);
						//set guar for pat
						existingPatOld=pat.Copy();
						pat.Guarantor=guar.PatNum;
						if(guarRelat=="parent"){
							pat.Position=PatientPosition.Child;
						}
						Patients.Update(pat,existingPatOld);
					}
				}
			}
			//subsc--------------------------------------------------------------------------------------------------
			if(!insPresent){
				//this takes care of missing carrier name or subscriber info.
				MsgBox.Show(this,"Done");
				DialogResult=DialogResult.OK;
			}
			if(insRelat=="self"){
				plan.Subscriber=pat.PatNum;
			}
			else{//we need to find or add the subscriber
				command="SELECT PatNum FROM patient WHERE "
					+"LName='"+POut.PString(subsc.LName)+"' "
					+"AND FName='"+POut.PString(subsc.FName)+"' "
					+"AND Birthdate="+POut.PDate(subsc.Birthdate)+" "
					+"AND PatStatus!=4";//not deleted
				table=General.GetTable(command);
				if(table.Rows.Count>0){//a subsc already exists, so simply attach. Make no other changes
					plan.Subscriber=PIn.PInt(table.Rows[0][0].ToString());
				}
				else{//need to create and attach a subscriber
					Patients.Insert(subsc,false);
					//set guar to same guar as patient
					existingPatOld=subsc.Copy();
					subsc.Guarantor=pat.Guarantor;
					Patients.Update(subsc,existingPatOld);
					plan.Subscriber=subsc.PatNum;
				}
			}
			//carrier-------------------------------------------------------------------------------------------------
			//Carriers.Cur=carrier;
			Carriers.GetCurSame(carrier);//this automatically finds or creates a carrier
			//plan------------------------------------------------------------------------------------------------------			
			plan.EmployerNum=Employers.GetEmployerNum(InsEmp);
			plan.CarrierNum=carrier.CarrierNum;
			InsPlans.Insert(plan);
			//Then attach plan
			PatPlan[] PatPlanList=PatPlans.Refresh(pat.PatNum);
			PatPlan patplan=new PatPlan();
			patplan.Ordinal=PatPlanList.Length+1;//so the ordinal of the first entry will be 1, NOT 0.
			patplan.PatNum=pat.PatNum;
			patplan.PlanNum=plan.PlanNum;
			switch(insRelat){
			  case "self":
					patplan.Relationship=Relat.Self;
					break;
				case "parent":
					patplan.Relationship=Relat.Child;
					break;
				case "spouse":
					patplan.Relationship=Relat.Spouse;
					break;
				case "guardian":
					patplan.Relationship=Relat.Dependent;
					break;
			}
			PatPlans.Insert(patplan);
			//benefits
			if(annualMax!=-1 && CovCatB.ListShort.Length>0){
				Benefit ben=new Benefit();
				ben.BenefitType=InsBenefitType.Limitations;
				ben.CovCatNum=CovCatB.ListShort[0].CovCatNum;
				ben.MonetaryAmt=annualMax;
				ben.PlanNum=plan.PlanNum;
				ben.TimePeriod=BenefitTimePeriod.CalendarYear;
				Benefits.Insert(ben);
			}
			if(deductible!=-1 && CovCatB.ListShort.Length>0) {
				Benefit ben=new Benefit();
				ben.BenefitType=InsBenefitType.Deductible;
				ben.CovCatNum=CovCatB.ListShort[0].CovCatNum;
				ben.MonetaryAmt=deductible;
				ben.PlanNum=plan.PlanNum;
				ben.TimePeriod=BenefitTimePeriod.CalendarYear;
				Benefits.Insert(ben);
			}
			MsgBox.Show(this,"Done");
			DialogResult=DialogResult.OK;
		}

		private void ProcessMSH(string field,string textValue){
			//MessageBox.Show("MSH, "+field+", "+textValue);
			switch(field){
			  case "DateTimeOfMessage"://ignore
					break;
				case "MessageType":
					if(textValue!="AdmitPatient"){
						throw new Exception("MessageType must be AdmitPatient");
					}
					break;
				case "OpenDentalVersion":
					targetVersion=textValue;
					break;
				default:
					warnings+="Unrecognized field: "+field+"\r\n";
					break;
			}
		}

		private void ProcessPID(string field,string textValue){
			//MessageBox.Show("PID, "+field+", "+textValue);
			switch(field){
				case "NameLast":
					pat.LName=textValue;
					break;
				case "NameFirst":
					pat.FName=textValue;
					break;
				case "NameMiddle":
					pat.MiddleI=textValue;
					break;
				case "DateOfBirth":
					pat.Birthdate=DateTime.MinValue;
					if(textValue.Length>0){
						try{
							pat.Birthdate=DateTime.Parse(textValue);
						}
						catch{
							warnings+="Invalid DateOfBirth\r\n";
						}
					}
					break;
				case "Sex":
					pat.Gender=PatientGender.Unknown;
					if(textValue.Length>0){
						switch(textValue.Substring(0,1).ToUpper()){
							case "M":
								pat.Gender=PatientGender.Male;
								break;
							case "F":
								pat.Gender=PatientGender.Female;
								break;
							case "U":
								pat.Gender=PatientGender.Unknown;
								break;
							default:
								warnings+="Invalid Sex\r\n";
								break;
						}
					}
					break;
				case "AliasFirst":
					pat.Preferred=textValue;
					break;
				case "AddressStreet":
					pat.Address=textValue;
					break;				
				case "AddressOtherDesignation":
					pat.Address2=textValue;
					break;				
				case "AddressCity":
					pat.City=textValue;
					break;				
				case "AddressStateOrProvince":
					pat.State=textValue;//we won't enforce two letters
					break;				
				case "AddressZipOrPostalCode":
					pat.Zip=textValue;
					break;				
				case "PhoneHome":
					pat.HmPhone=TelephoneNumbers.ReFormat(textValue);
					break;				
				case "EmailAddressHome":
					pat.Email=textValue;
					break;				
				case "PhoneBusiness":
					pat.WkPhone=TelephoneNumbers.ReFormat(textValue);
					break;
				case "MaritalStatus":
					pat.Position=PatientPosition.Single;
					if(textValue.Length>0){
						switch(textValue.Substring(0,1).ToUpper()){
							case "M":
								pat.Position=PatientPosition.Married;
								break;
							case "S":
								pat.Position=PatientPosition.Single;
								break;
							case "W":
								pat.Position=PatientPosition.Widowed;
								break;
							default:
								warnings+="Invalid MaritalStatus\r\n";
								break;
						}
					}
					break;				
				case "SSN":
					pat.SSN=textValue;
					if(CultureInfo.CurrentCulture.Name=="en-US"){
						if(Regex.IsMatch(pat.SSN,@"^\d\d\d-\d\d-\d\d\d\d$")){
							pat.SSN=pat.SSN.Replace("-","");
						}
						if(!Regex.IsMatch(pat.SSN,@"^\d{9}$")){//if not exactly 9 digits
							warnings+="Invalid SSN\r\n";
						}
					}
					break;				
				case "NotePhoneAddress":
					pat.AddrNote=textValue;
					break;
				case "NoteMedicalComplete":
					NoteMedicalComp=textValue;
					break;
				default:
					warnings+="Unrecognized field: "+field+"\r\n";
					break;
			}
		}

		private void ProcessGT(string field,string textValue){
			//MessageBox.Show("GT, "+field+", "+textValue);
			switch(field){
				case "NameLast":
					guar.LName=textValue;
					break;
				case "NameFirst":
					guar.FName=textValue;
					break;
				case "NameMiddle":
					guar.MiddleI=textValue;
					break;
				case "AddressStreet":
					guar.Address=textValue;
					break;				
				case "AddressOtherDesignation":
					guar.Address2=textValue;
					break;				
				case "AddressCity":
					guar.City=textValue;
					break;				
				case "AddressStateOrProvince":
					guar.State=textValue;//we won't enforce two letters
					break;				
				case "AddressZipOrPostalCode":
					guar.Zip=textValue;
					break;				
				case "PhoneHome":
					guar.HmPhone=TelephoneNumbers.ReFormat(textValue);
					break;		
				case "EmailAddressHome":
					guar.Email=textValue;
					break;	
				case "PhoneBusiness":
					guar.WkPhone=TelephoneNumbers.ReFormat(textValue);
					break;
				case "DateOfBirth":
					guar.Birthdate=DateTime.MinValue;
					if(textValue.Length>0){
						try{
							guar.Birthdate=DateTime.Parse(textValue);
						}
						catch{
							warnings+="Invalid DateOfBirth\r\n";
						}
					}
					break;
				case "Sex":
					guar.Gender=PatientGender.Unknown;
					if(textValue.Length>0){
						switch(textValue.Substring(0,1).ToUpper()){
							case "M":
								guar.Gender=PatientGender.Male;
								break;
							case "F":
								guar.Gender=PatientGender.Female;
								break;
							case "U":
								guar.Gender=PatientGender.Unknown;
								break;
							default:
								warnings+="Invalid Sex\r\n";
								break;
						}
					}
					break;
				case "GuarantorRelationship":
					switch(textValue.ToLower()){
						case "self":
							guarRelat="self";
							break;
						case "parent":
							guarRelat="parent";
							break;
						case "other":
							guarRelat="other";
							break;
						case "":
							guarRelat="self";
							break;
						default:
							guarRelat="self";
							warnings+="Invalid GuarantorRelationship\r\n";
							break;
					}
					break;
				case "SSN":
					guar.SSN=textValue;
					if(CultureInfo.CurrentCulture.Name=="en-US"){
						if(Regex.IsMatch(guar.SSN,@"^\d\d\d-\d\d-\d\d\d\d$")){
							guar.SSN=guar.SSN.Replace("-","");
						}
						if(!Regex.IsMatch(guar.SSN,@"^\d{9}$")){//if not exactly 9 digits
							warnings+="Invalid SSN\r\n";
						}
					}
					break;		
				case "EmployerName":
					GuarEmp=textValue;
					break;
				case "MaritalStatus":
					guar.Position=PatientPosition.Single;
					if(textValue.Length>0){
						switch(textValue.Substring(0,1).ToUpper()){
							case "M":
								guar.Position=PatientPosition.Married;
								break;
							case "S":
								guar.Position=PatientPosition.Single;
								break;
							case "W":
								guar.Position=PatientPosition.Widowed;
								break;
							default:
								warnings+="Invalid MaritalStatus\r\n";
								break;
						}
					}
					break;				
				default:
					warnings+="Unrecognized field: "+field+"\r\n";
					break;
			}
		}

		private void ProcessINS(string field,string textValue){
			//MessageBox.Show("INS, "+field+", "+textValue);
			switch(field){
				case "CompanyName":
					carrier.CarrierName=textValue;
					break;
				case "AddressStreet":
					carrier.Address=textValue;
					break;
				case "AddressOtherDesignation":
					carrier.Address2=textValue;
					break;
				case "AddressCity":
					carrier.City=textValue;
					break;
				case "AddressStateOrProvince":
					carrier.State=textValue;//we won't enforce two letters
					break;
				case "AddressZipOrPostalCode":
					carrier.Zip=textValue;
					break;
				case "PhoneNumber":
					carrier.Phone=TelephoneNumbers.ReFormat(textValue);
					break;
				case "GroupNumber":
					plan.GroupNum=textValue;
					break;
				case "GroupName":
					plan.GroupName=textValue;
					break;
				case "InsuredGroupEmpName":
					InsEmp=textValue;
					break;
				case "PlanEffectiveDate":
					plan.DateEffective=DateTime.MinValue;
					if(textValue.Length>0){
						try{
							plan.DateEffective=DateTime.Parse(textValue);
						}
						catch{
							warnings+="Invalid PlanEffectiveDate\r\n";
						}
					}
					break;
				case "PlanExpirationDate":
					plan.DateTerm=DateTime.MinValue;
					if(textValue.Length>0){
						try{
							plan.DateTerm=DateTime.Parse(textValue);
						}
						catch{
							warnings+="Invalid PlanExpirationDate\r\n";
						}
					}
					break;
				case "InsuredsNameLast":
					subsc.LName=textValue;
					break;
				case "InsuredsNameFirst":
					subsc.FName=textValue;
					break;
				case "InsuredsNameMiddle":
					subsc.MiddleI=textValue;
					break;
				case "InsuredsRelationToPat"://Self, Parent, Spouse, or Guardian
					switch(textValue.ToLower()){
						case "self":
							insRelat="self";
							break;
						case "parent":
							insRelat="parent";
							break;
						case "spouse":
							insRelat="spouse";
							break;
						case "guardian":
							insRelat="guardian";
							break;
						case "":
							insRelat="self";
							break;
						default:
							insRelat="self";
							warnings+="Invalid InsuredsRelationToPat\r\n";
							break;
					}
					break;
				case "InsuredsDateOfBirth":
					subsc.Birthdate=DateTime.MinValue;
					if(textValue.Length>0){
						try{
							subsc.Birthdate=DateTime.Parse(textValue);
						}
						catch{
							warnings+="Invalid InsuredsDateOfBirth\r\n";
						}
					}
					break;
				case "InsuredsAddressStreet":
					subsc.Address=textValue;
					break;
				case "InsuredsAddressOtherDesignation":
					subsc.Address2=textValue;
					break;
				case "InsuredsAddressCity":
					subsc.City=textValue;
					break;
				case "InsuredsAddressStateOrProvince":
					subsc.State=textValue;//we won't enforce two letters
					break;
				case "InsuredsAddressZipOrPostalCode":
					subsc.Zip=textValue;
					break;
				case "AssignmentOfBenefits":
					switch(textValue.ToUpper()){
						case "Y":
							plan.AssignBen=true;
							break;
						case "N":
							plan.AssignBen=false;
							break;
						case "":
							plan.AssignBen=true;
							break;
						default:
							plan.AssignBen=true;
							warnings+="Invalid AssignmentOfBenefits\r\n";
							break;
					}
					break;
				case "ReleaseInformationCode":
					switch(textValue.ToUpper()){
						case "Y":
							plan.ReleaseInfo=true;
							break;
						case "N":
							plan.ReleaseInfo=false;
							break;
						case "":
							plan.ReleaseInfo=true;
							break;
						default:
							plan.ReleaseInfo=true;
							warnings+="Invalid ReleaseInformationCode\r\n";
							break;
					}
					break;
				case "PolicyNumber":
					plan.SubscriberID=textValue;
					break;
				case "PolicyDeductible":
					deductible=-1;//unknown
					if(textValue.Length>0){
						try{
							deductible=System.Convert.ToInt32(textValue);
						}
						catch{
							warnings+="Invalid PolicyDeductible\r\n";
						}
					}
					break;
				case "PolicyLimitAmount":
					annualMax=-1;//unknown
					if(textValue.Length>0){
						try{
							annualMax=System.Convert.ToInt32(textValue);
						}
						catch{
							warnings+="Invalid PolicyLimitAmount\r\n";
						}
					}
					break;
				case "InsuredsSex":
					subsc.Gender=PatientGender.Unknown;
					if(textValue.Length>0){
						switch(textValue.Substring(0,1).ToUpper()){
							case "M":
								subsc.Gender=PatientGender.Male;
								break;
							case "F":
								subsc.Gender=PatientGender.Female;
								break;
							case "U":
								subsc.Gender=PatientGender.Unknown;
								break;
							default:
								warnings+="Invalid InsuredsSex\r\n";
								break;
						}
					}
					break;
				case "InsuredsSSN":
					subsc.SSN=textValue;
					if(CultureInfo.CurrentCulture.Name=="en-US"){
						if(Regex.IsMatch(subsc.SSN,@"^\d\d\d-\d\d-\d\d\d\d$")){
							subsc.SSN=subsc.SSN.Replace("-","");
						}
						if(!Regex.IsMatch(subsc.SSN,@"^\d{9}$")){//if not exactly 9 digits
							warnings+="Invalid InsuredsSSN\r\n";
						}
					}
					break;
				case "InsuredsPhoneHome":
					subsc.HmPhone=TelephoneNumbers.ReFormat(textValue);
					break;
				case "NotePlan":
					plan.BenefitNotes=textValue;
					break;
				default:
					warnings+="Unrecognized field: "+field+"\r\n";
					break;
			}
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		


	}
}





















