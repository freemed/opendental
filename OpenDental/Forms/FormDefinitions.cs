using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
///<summary></summary>
	public class FormDefinitions : System.Windows.Forms.Form{
		private OpenDental.UI.Button butClose;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.TextBox textGuide;
		private System.Windows.Forms.GroupBox groupEdit;
		private OpenDental.TableDefs tbDefs;
		private System.Windows.Forms.ListBox listCategory;
		private System.Windows.Forms.Label label13;
		private System.ComponentModel.Container components = null;
		private OpenDental.UI.Button butAdd;
		private OpenDental.UI.Button butUp;
		private OpenDental.UI.Button butDown;
		private OpenDental.UI.Button butHide;
		//<summary>This is the index of the selected cat.</summary>
		//private int InitialCat;
		///<summary>this is (int)DefCat, not the index of the selected Cat.</summary>
		private int SelectedCat;
		private bool changed;
		///<summary>Gives the DefCat for each item in the list.</summary>
		private DefCat[] lookupCat;
		//private User user;
		private bool DefsIsSelected;
		private Def[] DefsList;
		private int DefsSelected;

		///<summary></summary>
		public FormDefinitions(DefCat selectedCat){
			InitializeComponent();// Required for Windows Form Designer support
			SelectedCat=(int)selectedCat;
			tbDefs.CellDoubleClicked += new OpenDental.ContrTable.CellEventHandler(tbDefs_CellDoubleClicked);
			tbDefs.CellClicked += new OpenDental.ContrTable.CellEventHandler(tbDefs_CellClicked);
			Lan.F(this);
		}

		///<summary></summary>
		protected override void Dispose( bool disposing ){
			if( disposing ){
				if(components != null){
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDefinitions));
			this.butClose = new OpenDental.UI.Button();
			this.label14 = new System.Windows.Forms.Label();
			this.textGuide = new System.Windows.Forms.TextBox();
			this.groupEdit = new System.Windows.Forms.GroupBox();
			this.butHide = new OpenDental.UI.Button();
			this.butDown = new OpenDental.UI.Button();
			this.butUp = new OpenDental.UI.Button();
			this.butAdd = new OpenDental.UI.Button();
			this.tbDefs = new OpenDental.TableDefs();
			this.listCategory = new System.Windows.Forms.ListBox();
			this.label13 = new System.Windows.Forms.Label();
			this.groupEdit.SuspendLayout();
			this.SuspendLayout();
			// 
			// butClose
			// 
			this.butClose.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butClose.Autosize = true;
			this.butClose.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butClose.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butClose.CornerRadius = 4F;
			this.butClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butClose.Location = new System.Drawing.Point(671,637);
			this.butClose.Name = "butClose";
			this.butClose.Size = new System.Drawing.Size(75,26);
			this.butClose.TabIndex = 3;
			this.butClose.Text = "&Close";
			this.butClose.Click += new System.EventHandler(this.butClose_Click);
			// 
			// label14
			// 
			this.label14.Location = new System.Drawing.Point(76,604);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(100,18);
			this.label14.TabIndex = 22;
			this.label14.Text = "Guidelines";
			this.label14.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textGuide
			// 
			this.textGuide.Location = new System.Drawing.Point(182,604);
			this.textGuide.Multiline = true;
			this.textGuide.Name = "textGuide";
			this.textGuide.Size = new System.Drawing.Size(460,63);
			this.textGuide.TabIndex = 2;
			// 
			// groupEdit
			// 
			this.groupEdit.Controls.Add(this.butHide);
			this.groupEdit.Controls.Add(this.butDown);
			this.groupEdit.Controls.Add(this.butUp);
			this.groupEdit.Controls.Add(this.butAdd);
			this.groupEdit.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupEdit.Location = new System.Drawing.Point(182,549);
			this.groupEdit.Name = "groupEdit";
			this.groupEdit.Size = new System.Drawing.Size(460,51);
			this.groupEdit.TabIndex = 1;
			this.groupEdit.TabStop = false;
			this.groupEdit.Text = "Edit Items";
			// 
			// butHide
			// 
			this.butHide.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butHide.Autosize = true;
			this.butHide.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butHide.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butHide.CornerRadius = 4F;
			this.butHide.Location = new System.Drawing.Point(140,17);
			this.butHide.Name = "butHide";
			this.butHide.Size = new System.Drawing.Size(75,26);
			this.butHide.TabIndex = 10;
			this.butHide.Text = "&Hide";
			this.butHide.Click += new System.EventHandler(this.butHide_Click);
			// 
			// butDown
			// 
			this.butDown.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butDown.Autosize = true;
			this.butDown.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDown.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDown.CornerRadius = 4F;
			this.butDown.Image = global::OpenDental.Properties.Resources.down;
			this.butDown.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butDown.Location = new System.Drawing.Point(348,17);
			this.butDown.Name = "butDown";
			this.butDown.Size = new System.Drawing.Size(79,26);
			this.butDown.TabIndex = 9;
			this.butDown.Text = "&Down";
			this.butDown.Click += new System.EventHandler(this.butDown_Click);
			// 
			// butUp
			// 
			this.butUp.AdjustImageLocation = new System.Drawing.Point(0,1);
			this.butUp.Autosize = true;
			this.butUp.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butUp.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butUp.CornerRadius = 4F;
			this.butUp.Image = global::OpenDental.Properties.Resources.up;
			this.butUp.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butUp.Location = new System.Drawing.Point(242,17);
			this.butUp.Name = "butUp";
			this.butUp.Size = new System.Drawing.Size(79,26);
			this.butUp.TabIndex = 8;
			this.butUp.Text = "&Up";
			this.butUp.Click += new System.EventHandler(this.butUp_Click);
			// 
			// butAdd
			// 
			this.butAdd.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAdd.Autosize = true;
			this.butAdd.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAdd.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAdd.CornerRadius = 4F;
			this.butAdd.Image = global::OpenDental.Properties.Resources.Add;
			this.butAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAdd.Location = new System.Drawing.Point(34,17);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(79,26);
			this.butAdd.TabIndex = 6;
			this.butAdd.Text = "&Add";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// tbDefs
			// 
			this.tbDefs.BackColor = System.Drawing.SystemColors.Window;
			this.tbDefs.Location = new System.Drawing.Point(183,6);
			this.tbDefs.Name = "tbDefs";
			this.tbDefs.ScrollValue = 1;
			this.tbDefs.SelectedIndices = new int[0];
			this.tbDefs.SelectionMode = System.Windows.Forms.SelectionMode.None;
			this.tbDefs.Size = new System.Drawing.Size(459,538);
			this.tbDefs.TabIndex = 19;
			// 
			// listCategory
			// 
			this.listCategory.Items.AddRange(new object[] {
            "Account Colors",
            "Adj Types",
            "Appointment Colors",
            "Appt Confirmed",
            "Appt Procs Quick Add",
            "Billing Types",
            "Blockout Types",
            "Chart Graphic Colors",
            "Commlog Types",
            "Contact Categories",
            "Diagnosis",
            "Fee Sched Names",
            "Image Categories",
            "Letter Merge Cats",
            "Misc Colors",
            "Payment Types",
            "Proc Button Categories",
            "Proc Code Categories",
            "Prog Notes Colors",
            "Recall/Unsch Status",
            "Treat\' Plan Priorities"});
			this.listCategory.Location = new System.Drawing.Point(22,36);
			this.listCategory.Name = "listCategory";
			this.listCategory.Size = new System.Drawing.Size(133,277);
			this.listCategory.TabIndex = 0;
			this.listCategory.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listCategory_MouseDown);
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(22,18);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(162,17);
			this.label13.TabIndex = 17;
			this.label13.Text = "Select Category:";
			this.label13.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// FormDefinitions
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.CancelButton = this.butClose;
			this.ClientSize = new System.Drawing.Size(774,675);
			this.Controls.Add(this.label14);
			this.Controls.Add(this.textGuide);
			this.Controls.Add(this.butClose);
			this.Controls.Add(this.groupEdit);
			this.Controls.Add(this.tbDefs);
			this.Controls.Add(this.listCategory);
			this.Controls.Add(this.label13);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormDefinitions";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Definitions";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.FormDefinitions_Closing);
			this.Load += new System.EventHandler(this.FormDefinitions_Load);
			this.groupEdit.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormDefinitions_Load(object sender, System.EventArgs e) {
			/*if(PermissionsOld.AuthorizationRequired("Definitions")){
				user=Users.Authenticate("Definitions");
				if(!UserPermissions.IsAuthorized("Definitions",user)){
					MsgBox.Show(this,"You do not have permission for this feature");
					DialogResult=DialogResult.Cancel;
					return;
				}	
			}*/
			lookupCat=new DefCat[listCategory.Items.Count];
			lookupCat[0]=DefCat.AccountColors;
			lookupCat[1]=DefCat.AdjTypes;
			lookupCat[2]=DefCat.AppointmentColors;
			lookupCat[3]=DefCat.ApptConfirmed;
			//lookupCat[4]=DefCat.ApptPhoneNotes;
			lookupCat[4]=DefCat.ApptProcsQuickAdd;
			lookupCat[5]=DefCat.BillingTypes;
			lookupCat[6]=DefCat.BlockoutTypes;
			lookupCat[7]=DefCat.ChartGraphicColors;
			//lookupCat[9]=DefCat.ClaimFormats;
			lookupCat[8]=DefCat.CommLogTypes;
			lookupCat[9]=DefCat.ContactCategories;
			lookupCat[10]=DefCat.Diagnosis;
			//lookupCat[12]=DefCat.DiscountTypes;
			//lookupCat[13]=DefCat.DunningMessages;
			lookupCat[11]=DefCat.FeeSchedNames;
			lookupCat[12]=DefCat.ImageCats;
			lookupCat[13]=DefCat.LetterMergeCats;
			//lookupCat[17]=DefCat.MedicalNotes;
			lookupCat[14]=DefCat.MiscColors;
			//lookupCat[19]=DefCat.OperatoriesOld;
			lookupCat[15]=DefCat.PaymentTypes;
			lookupCat[16]=DefCat.ProcButtonCats;
			lookupCat[17]=DefCat.ProcCodeCats;
			lookupCat[18]=DefCat.ProgNoteColors;
			lookupCat[19]=DefCat.RecallUnschedStatus;
			//lookupCat[25]=DefCat.ServiceNotes;
			lookupCat[20]=DefCat.TxPriorities;
			for(int i=0;i<listCategory.Items.Count;i++){
				listCategory.Items[i]=Lan.g(this,(string)listCategory.Items[i]);
				if((int)lookupCat[i]==SelectedCat){
					listCategory.SelectedIndex=i;
				}
			}
			FillCats();
		}

		private void listCategory_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e){
			listCategory.SelectedIndex=listCategory.IndexFromPoint(e.X,e.Y);
			//test for -1 only necessary if there is whitespace, which there is not.
			SelectedCat=(int)lookupCat[listCategory.SelectedIndex];
			FillCats();
		}

		private void FillCats(){
			//a category is ALWAYS selected; never -1.
			DefsIsSelected=false;
			FormDefEdit.EnableColor=false;
			FormDefEdit.EnableValue=false;
			FormDefEdit.CanEditName=true;//false;
			tbDefs.Fields[1]="";
			FormDefEdit.ValueText="";
			switch(listCategory.SelectedIndex){
				case 0://"Account Colors":
					//SelectedCat=0;
					FormDefEdit.CanEditName=false;
					FormDefEdit.EnableColor=true;
					FormDefEdit.HelpText=Lan.g(this,"Changes the color of text for different types of entries in Account Module");
					break;
				case 1://"Adj Types":
					//SelectedCat=1;
					FormDefEdit.ValueText=Lan.g(this,"+ or -");
					FormDefEdit.EnableValue=true;
					FormDefEdit.HelpText=Lan.g(this,"Plus increases the patient balance.  Minus decreases it.  Not allowed to change value after creating new type since changes affect all patient accounts.");
					break;
				case 2://"Appointment Colors":
					//SelectedCat=17;
					FormDefEdit.EnableColor=true;
					FormDefEdit.CanEditName=false;
					FormDefEdit.HelpText=Lan.g(this,"Changes colors of background in Appointments Module, and colors for completed appointments.");
					break;
				case 3://"Appt Confirmed":
					//SelectedCat=2;
					FormDefEdit.EnableValue=true;
					FormDefEdit.ValueText=Lan.g(this,"Abbrev");
					FormDefEdit.EnableColor=true;
					//tbDefs.Fields[2]="Color";
					FormDefEdit.HelpText=Lan.g(this,"Color shows in bar on left of each appointment.  Changes affect all appointments.");
					break;
				case 4://"Appt Procs Quick Add":
					//SelectedCat=3;
					FormDefEdit.EnableValue=true;
					FormDefEdit.ValueText=Lan.g(this,"ADA Code(s)");
					FormDefEdit.HelpText=Lan.g(this,"These are the procedures that you can quickly add to the treatment plan from within the appointment editing window.  They must not require a tooth number. Multiple procedures may be separated by commas with no spaces. These definitions may be freely edited without affecting any patient records.");
					break;
				case 5://"Billing Types":
					//SelectedCat=4;
					FormDefEdit.HelpText=Lan.g(this,"It is recommended to use as few billing types as possible.  They can be useful when running reports to separate delinquent accounts, but can cause 'forgotten accounts' if used without good office procedures. Changes affect all patients.");
					break;
				case 6://"Blockout Types":
					FormDefEdit.EnableColor=true;
					FormDefEdit.EnableValue=false;
					FormDefEdit.HelpText=Lan.g(this,"Blockout types are used in the appointments module.");
					break;
				case 7://"Chart Graphic Colors":
					//SelectedCat=22;
					FormDefEdit.EnableColor=true;
					FormDefEdit.CanEditName=false;
					FormDefEdit.HelpText=Lan.g(this,"These colors will be used on the graphical tooth chart to draw restorations.");
					break;
				case 8://"Commlog Types"
					FormDefEdit.EnableValue=true;
					FormDefEdit.ValueText=Lan.g(this,"");
					FormDefEdit.HelpText=Lan.g(this,"Changes affect all current commlog entries.  In the second column, you can optionally specify APPT,FIN,RECALL,or MISC. Only one of each. This helps automate new entries.");
					break;
				case 9://"Contact Categories":
					//SelectedCat=(int)DefCat.ContactCategories;
					FormDefEdit.HelpText=Lan.g(this,"You can add as many categories as you want.  Changes affect all current contact records.");
					break;
				case 10://"Diagnosis":
					//SelectedCat=16;
					FormDefEdit.EnableValue=true;
					FormDefEdit.ValueText=Lan.g(this,"1 or 2 letter abbreviation");
					FormDefEdit.HelpText=Lan.g(this,"The diagnosis list is shown when entering a procedure.  Ones that are less used should go lower on the list.  The abbreviation is shown in the progress notes.  BE VERY CAREFUL.  Changes affect all patients.");
					break;
				case 11://"Fee Sched Names":
					//SelectedCat=7;
					FormDefEdit.EnableValue=true;
					FormDefEdit.ValueText=Lan.g(this,"C=CoPay, A=Allowed");
					FormDefEdit.HelpText=Lan.g(this,"Fee Schedule names.  Caution: any changes to the names affect all patients. Changing the order does not cause any problems.");
					break;
				case 12://"Image Categories":
					//SelectedCat=18;
					FormDefEdit.EnableValue=true;
					FormDefEdit.ValueText=Lan.g(this,"X=Chart,P=Patient Picture");
					FormDefEdit.HelpText=Lan.g(this,"These are the categories that will be available in the image and chart modules.  If you hide a category, images in that category will be hidden, so only hide a category if you are certain it has never been used.  If you want the category to show in the Chart module, enter an X in the second column.  One category can be used for patient pictures, marked with P.  Affects all patient records.");
					break;
				case 13://"Letter Merge Cats"
					//SelectedCat=(int)DefCat.LetterMergeCats;
					FormDefEdit.HelpText=Lan.g(this,"Categories for Letter Merge.  You can safely make any changes you want.");
					break;
				case 14://"Misc Colors":
					//SelectedCat=21;
					FormDefEdit.EnableColor=true;
					FormDefEdit.CanEditName=false;
					FormDefEdit.HelpText="";
					break;
				case 15://"Payment Types":
					//SelectedCat=10;
					FormDefEdit.HelpText=Lan.g(this,"Types of payments that patients might make. Any changes will affect all patients.");
					break;
				case 16://"Proc Button Categories":
					FormDefEdit.HelpText=Lan.g(this,"These are similar to the procedure code categories, but are only used for organizing and grouping the procedure buttons in the Chart module.");
					break;
				case 17://"Proc Code Categories":
					//SelectedCat=11;
					FormDefEdit.HelpText=Lan.g(this,"These are the categories for organizing procedure codes. They do not have to follow ADA categories.  There is no relationship to insurance categories which are setup in the Ins Categories section.  Does not affect any patient records.");
					break;
				case 18://"Prog Notes Colors":
					//SelectedCat=12;
					FormDefEdit.EnableColor=true;
					FormDefEdit.CanEditName=false;
					FormDefEdit.HelpText=Lan.g(this,"Changes color of text for different types of entries in the Chart Module Progress Notes.");
					break;
				case 19://"Recall/Unsch Status":
					//SelectedCat=13;
					FormDefEdit.EnableValue=true;
					FormDefEdit.ValueText=Lan.g(this,"Abbreviation");
					FormDefEdit.HelpText=Lan.g(this,"Recall/Unsched Status.  Abbreviation must be 7 characters or less.  Changes affect all patients.");
					break;
				case 20://"Treat' Plan Priorities":
					//SelectedCat=20;
					FormDefEdit.EnableColor=true;
					FormDefEdit.HelpText=Lan.g(this,"Priorities available for selection in the Treatment Plan module.  They can be simple numbers or descriptive abbreviations 7 letters or less.  Changes affect all procedures where the definition is used.");
					break;
			}
			FillDefs();
		}

		private void FillDefs(){
			//Defs.IsSelected=false;
			int scroll=tbDefs.ScrollValue;
			DefsList=Defs.GetCatList(SelectedCat);
			tbDefs.ResetRows(DefsList.Length);
			tbDefs.SetBackGColor(Color.White);
			for(int i=0;i<DefsList.Length;i++){
				tbDefs.Cell[0,i]=DefsList[i].ItemName;
				tbDefs.Cell[1,i]=DefsList[i].ItemValue;
				if(FormDefEdit.EnableColor){
					tbDefs.BackGColor[2,i]=DefsList[i].ItemColor;
				}
				if(DefsList[i].IsHidden)
					tbDefs.Cell[3,i]="X";
				//else tbDefs.Cell[3,i]="";
			}
			if(DefsIsSelected){
				tbDefs.BackGColor[0,DefsSelected]=Color.LightGray;
				tbDefs.BackGColor[1,DefsSelected]=Color.LightGray;
			}
			tbDefs.Fields[1]=FormDefEdit.ValueText;
			if(FormDefEdit.EnableColor){
				tbDefs.Fields[2]="Color";
			}
			else{
				tbDefs.Fields[2]="";
			}
			tbDefs.LayoutTables();
			tbDefs.ScrollValue=scroll;
			//the following do not require a refresh of the table:
			if(FormDefEdit.CanEditName){
				groupEdit.Enabled=true;
				groupEdit.Text="Edit Items";
			}
			else{
				groupEdit.Enabled=false;
				groupEdit.Text="Not allowed";
			}
			textGuide.Text=FormDefEdit.HelpText;
		}

		private void tbDefs_CellClicked(object sender, CellEventArgs e){
			//Can't move this logic into the Table control because we never want to paint on col 3
			if(DefsIsSelected){
				if(DefsSelected==e.Row){
					tbDefs.BackGColor[0,e.Row]=Color.White;
					tbDefs.BackGColor[1,e.Row]=Color.White;
					DefsIsSelected=false;
				}
				else{
					tbDefs.BackGColor[0,DefsSelected]=Color.White;
					tbDefs.BackGColor[1,DefsSelected]=Color.White;
					tbDefs.BackGColor[0,e.Row]=Color.LightGray;
					tbDefs.BackGColor[1,e.Row]=Color.LightGray;
					DefsSelected=e.Row;
					DefsIsSelected=true;
				}
			}
			else{
				tbDefs.BackGColor[0,e.Row]=Color.LightGray;
				tbDefs.BackGColor[1,e.Row]=Color.LightGray;
				DefsSelected=e.Row;
				DefsIsSelected=true;
			}
			tbDefs.Refresh();
		}

		private void tbDefs_CellDoubleClicked(object sender, CellEventArgs e){
			tbDefs.BackGColor[0,e.Row]=SystemColors.Highlight;
			tbDefs.BackGColor[1,e.Row]=SystemColors.Highlight;
			tbDefs.Refresh();
			DefsIsSelected=true;
			DefsSelected=e.Row;
			FormDefEdit FormDefEdit2 = new FormDefEdit(DefsList[e.Row]);
			//Defs.Cur = Defs.List[e.Row];
			FormDefEdit2.IsNew=false;
			FormDefEdit2.ShowDialog();
			//Preferences2.GetCatList(listCategory.SelectedIndex);
			changed=true;
			FillDefs();
		}

		private void butAdd_Click(object sender, System.EventArgs e) {
			//if(SelectedCat==-1){//never -1.
			//	MessageBox.Show(Lan.g(this,"Please select item first."));
			//	return;
			//}
			Def DefCur=new Def();
			DefCur.ItemOrder=DefsList.Length;
			DefCur.Category=(DefCat)SelectedCat;
			FormDefEdit FormDE=new FormDefEdit(DefCur);
			FormDE.IsNew=true;
			FormDE.ShowDialog();
			if(FormDE.DialogResult!=DialogResult.OK){
				return;
			}
			DefsSelected=DefsList.Length;//this is one more than allowed, but it's ok
			DefsIsSelected=true;
			changed=true;
			FillDefs();
		}

		private void butHide_Click(object sender, System.EventArgs e) {
			if(!DefsIsSelected){
				MessageBox.Show(Lan.g(this,"Please select item first,"));
				return;
			}
			Defs.HideDef(DefsList[DefsSelected]);
			changed=true;
			FillDefs();
		}

		private void butUp_Click(object sender, System.EventArgs e) {
			DefsSelected=Defs.MoveUp(DefsIsSelected,DefsSelected,DefsList);
			changed=true;
			FillDefs();
		}

		private void butDown_Click(object sender, System.EventArgs e) {
			DefsSelected=Defs.MoveDown(DefsIsSelected,DefsSelected,DefsList);
			changed=true;
			FillDefs();
		}

		private void butClose_Click(object sender, System.EventArgs e) {
			Close();
		}

		private void FormDefinitions_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			if(changed){
				DataValid.SetInvalid(InvalidTypes.Defs | InvalidTypes.Fees);
			}
			DefsIsSelected=false;
			//if(user!=null){
				//SecurityLogs.MakeLogEntry("Definitions","Altered Definitions",user);
			//}
		}



		



	}
}
