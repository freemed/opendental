using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDental.UI;
using OpenDentBusiness;

namespace OpenDental{
	///<summary></summary>
	public class FormBillingOptions : System.Windows.Forms.Form{
		private System.ComponentModel.Container components = null;
		private OpenDental.UI.Button butCancel;
		//private FormQuery FormQuery2;
		private System.Windows.Forms.ListBox listBillType;
		private System.Windows.Forms.Label label2;
		private OpenDental.UI.Button butAll;
		private System.Windows.Forms.Label label1;
		private OpenDental.UI.Button butSaveDefault;
		private OpenDental.ValidDouble textExcludeLessThan;
		private System.Windows.Forms.CheckBox checkExcludeInactive;
		private System.Windows.Forms.GroupBox groupBox2;
		private OpenDental.UI.Button butAdd;
		private System.Windows.Forms.Label label3;
		private OpenDental.UI.ODGrid gridDun;
		private System.Windows.Forms.Label label4;
		private OpenDental.ODtextBox textNote;
		private System.Windows.Forms.CheckBox checkBadAddress;
		private System.Windows.Forms.CheckBox checkExcludeNegative;
		private System.Windows.Forms.RadioButton radio30;
		private System.Windows.Forms.RadioButton radio90;
		private System.Windows.Forms.RadioButton radio60;
		private System.Windows.Forms.RadioButton radioAny;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.CheckBox checkIncludeChanged;
		private OpenDental.ValidDate textLastStatement;
		private OpenDental.UI.Button butCreate;
		private CheckBox checkExcludeInsPending;
		private Dunning[] dunningList;

		///<summary></summary>
		public FormBillingOptions(){
			InitializeComponent();
			Lan.F(this);
		}

		///<summary></summary>
		protected override void Dispose(bool disposing){
			if(disposing){
				if(components != null){
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		private void InitializeComponent(){
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormBillingOptions));
			this.butCancel = new OpenDental.UI.Button();
			this.butCreate = new OpenDental.UI.Button();
			this.listBillType = new System.Windows.Forms.ListBox();
			this.label2 = new System.Windows.Forms.Label();
			this.butAll = new OpenDental.UI.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.butSaveDefault = new OpenDental.UI.Button();
			this.textExcludeLessThan = new OpenDental.ValidDouble();
			this.checkExcludeInactive = new System.Windows.Forms.CheckBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.checkExcludeInsPending = new System.Windows.Forms.CheckBox();
			this.checkIncludeChanged = new System.Windows.Forms.CheckBox();
			this.textLastStatement = new OpenDental.ValidDate();
			this.label5 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.radio30 = new System.Windows.Forms.RadioButton();
			this.radio90 = new System.Windows.Forms.RadioButton();
			this.radio60 = new System.Windows.Forms.RadioButton();
			this.radioAny = new System.Windows.Forms.RadioButton();
			this.checkExcludeNegative = new System.Windows.Forms.CheckBox();
			this.checkBadAddress = new System.Windows.Forms.CheckBox();
			this.gridDun = new OpenDental.UI.ODGrid();
			this.butAdd = new OpenDental.UI.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.textNote = new OpenDental.ODtextBox();
			this.groupBox2.SuspendLayout();
			this.groupBox1.SuspendLayout();
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
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.Location = new System.Drawing.Point(806,631);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(79,24);
			this.butCancel.TabIndex = 4;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// butCreate
			// 
			this.butCreate.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCreate.Autosize = true;
			this.butCreate.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCreate.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCreate.CornerRadius = 4F;
			this.butCreate.Location = new System.Drawing.Point(693,631);
			this.butCreate.Name = "butCreate";
			this.butCreate.Size = new System.Drawing.Size(92,24);
			this.butCreate.TabIndex = 3;
			this.butCreate.Text = "Create &List";
			this.butCreate.Click += new System.EventHandler(this.butCreate_Click);
			// 
			// listBillType
			// 
			this.listBillType.Location = new System.Drawing.Point(21,346);
			this.listBillType.Name = "listBillType";
			this.listBillType.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.listBillType.Size = new System.Drawing.Size(158,186);
			this.listBillType.TabIndex = 2;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(19,328);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(147,16);
			this.label2.TabIndex = 14;
			this.label2.Text = "Billing Types";
			this.label2.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// butAll
			// 
			this.butAll.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAll.Autosize = true;
			this.butAll.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAll.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAll.CornerRadius = 4F;
			this.butAll.Location = new System.Drawing.Point(21,536);
			this.butAll.Name = "butAll";
			this.butAll.Size = new System.Drawing.Size(68,24);
			this.butAll.TabIndex = 15;
			this.butAll.Text = "&All";
			this.butAll.Click += new System.EventHandler(this.butAll_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(19,286);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(232,16);
			this.label1.TabIndex = 18;
			this.label1.Text = "Exclude if Balance is less than:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// butSaveDefault
			// 
			this.butSaveDefault.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butSaveDefault.Autosize = true;
			this.butSaveDefault.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSaveDefault.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSaveDefault.CornerRadius = 4F;
			this.butSaveDefault.Location = new System.Drawing.Point(20,574);
			this.butSaveDefault.Name = "butSaveDefault";
			this.butSaveDefault.Size = new System.Drawing.Size(108,24);
			this.butSaveDefault.TabIndex = 20;
			this.butSaveDefault.Text = "&Save As Default";
			this.butSaveDefault.Click += new System.EventHandler(this.butSaveDefault_Click);
			// 
			// textExcludeLessThan
			// 
			this.textExcludeLessThan.Location = new System.Drawing.Point(21,303);
			this.textExcludeLessThan.Name = "textExcludeLessThan";
			this.textExcludeLessThan.Size = new System.Drawing.Size(77,20);
			this.textExcludeLessThan.TabIndex = 22;
			// 
			// checkExcludeInactive
			// 
			this.checkExcludeInactive.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkExcludeInactive.Location = new System.Drawing.Point(22,223);
			this.checkExcludeInactive.Name = "checkExcludeInactive";
			this.checkExcludeInactive.Size = new System.Drawing.Size(231,18);
			this.checkExcludeInactive.TabIndex = 23;
			this.checkExcludeInactive.Text = "Exclude inactive patients";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.checkExcludeInsPending);
			this.groupBox2.Controls.Add(this.checkIncludeChanged);
			this.groupBox2.Controls.Add(this.textLastStatement);
			this.groupBox2.Controls.Add(this.label5);
			this.groupBox2.Controls.Add(this.groupBox1);
			this.groupBox2.Controls.Add(this.checkExcludeNegative);
			this.groupBox2.Controls.Add(this.checkBadAddress);
			this.groupBox2.Controls.Add(this.checkExcludeInactive);
			this.groupBox2.Controls.Add(this.label1);
			this.groupBox2.Controls.Add(this.textExcludeLessThan);
			this.groupBox2.Controls.Add(this.butSaveDefault);
			this.groupBox2.Controls.Add(this.listBillType);
			this.groupBox2.Controls.Add(this.butAll);
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox2.Location = new System.Drawing.Point(7,12);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(263,609);
			this.groupBox2.TabIndex = 24;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Filter";
			// 
			// checkExcludeInsPending
			// 
			this.checkExcludeInsPending.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkExcludeInsPending.Location = new System.Drawing.Point(22,263);
			this.checkExcludeInsPending.Name = "checkExcludeInsPending";
			this.checkExcludeInsPending.Size = new System.Drawing.Size(231,18);
			this.checkExcludeInsPending.TabIndex = 27;
			this.checkExcludeInsPending.Text = "Exclude if insurance pending";
			// 
			// checkIncludeChanged
			// 
			this.checkIncludeChanged.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkIncludeChanged.Location = new System.Drawing.Point(21,61);
			this.checkIncludeChanged.Name = "checkIncludeChanged";
			this.checkIncludeChanged.Size = new System.Drawing.Size(238,28);
			this.checkIncludeChanged.TabIndex = 26;
			this.checkIncludeChanged.Text = "Include any accounts with insurance payments or procedures since the last bill";
			// 
			// textLastStatement
			// 
			this.textLastStatement.Location = new System.Drawing.Point(22,37);
			this.textLastStatement.Name = "textLastStatement";
			this.textLastStatement.Size = new System.Drawing.Size(94,20);
			this.textLastStatement.TabIndex = 25;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(21,17);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(232,16);
			this.label5.TabIndex = 24;
			this.label5.Text = "Include anyone not billed since";
			this.label5.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.radio30);
			this.groupBox1.Controls.Add(this.radio90);
			this.groupBox1.Controls.Add(this.radio60);
			this.groupBox1.Controls.Add(this.radioAny);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(21,94);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(144,106);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Age of Account";
			// 
			// radio30
			// 
			this.radio30.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radio30.Location = new System.Drawing.Point(12,41);
			this.radio30.Name = "radio30";
			this.radio30.Size = new System.Drawing.Size(120,16);
			this.radio30.TabIndex = 1;
			this.radio30.Text = "Over 30 Days";
			// 
			// radio90
			// 
			this.radio90.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radio90.Location = new System.Drawing.Point(12,83);
			this.radio90.Name = "radio90";
			this.radio90.Size = new System.Drawing.Size(120,18);
			this.radio90.TabIndex = 3;
			this.radio90.Text = "Over 90 Days";
			// 
			// radio60
			// 
			this.radio60.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radio60.Location = new System.Drawing.Point(12,61);
			this.radio60.Name = "radio60";
			this.radio60.Size = new System.Drawing.Size(117,18);
			this.radio60.TabIndex = 2;
			this.radio60.Text = "Over 60 Days";
			// 
			// radioAny
			// 
			this.radioAny.Checked = true;
			this.radioAny.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioAny.Location = new System.Drawing.Point(12,19);
			this.radioAny.Name = "radioAny";
			this.radioAny.Size = new System.Drawing.Size(117,18);
			this.radioAny.TabIndex = 0;
			this.radioAny.TabStop = true;
			this.radioAny.Text = "Any Balance";
			// 
			// checkExcludeNegative
			// 
			this.checkExcludeNegative.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkExcludeNegative.Location = new System.Drawing.Point(22,243);
			this.checkExcludeNegative.Name = "checkExcludeNegative";
			this.checkExcludeNegative.Size = new System.Drawing.Size(231,18);
			this.checkExcludeNegative.TabIndex = 17;
			this.checkExcludeNegative.Text = "Exclude negative balances (credits)";
			// 
			// checkBadAddress
			// 
			this.checkBadAddress.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkBadAddress.Location = new System.Drawing.Point(22,203);
			this.checkBadAddress.Name = "checkBadAddress";
			this.checkBadAddress.Size = new System.Drawing.Size(231,18);
			this.checkBadAddress.TabIndex = 16;
			this.checkBadAddress.Text = "Exclude bad addresses (no zipcode)";
			// 
			// gridDun
			// 
			this.gridDun.HScrollVisible = false;
			this.gridDun.Location = new System.Drawing.Point(276,31);
			this.gridDun.Name = "gridDun";
			this.gridDun.ScrollValue = 0;
			this.gridDun.Size = new System.Drawing.Size(616,242);
			this.gridDun.TabIndex = 0;
			this.gridDun.Title = "Dunning Messages";
			this.gridDun.TranslationName = "TableBillingMessages";
			this.gridDun.CellDoubleClick += new OpenDental.UI.ODGridClickEventHandler(this.gridDun_CellDoubleClick);
			// 
			// butAdd
			// 
			this.butAdd.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butAdd.Autosize = true;
			this.butAdd.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAdd.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAdd.CornerRadius = 4F;
			this.butAdd.Image = global::OpenDental.Properties.Resources.Add;
			this.butAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butAdd.Location = new System.Drawing.Point(276,279);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(124,24);
			this.butAdd.TabIndex = 5;
			this.butAdd.Text = "Add Dunning Msg";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(299,10);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(581,16);
			this.label3.TabIndex = 25;
			this.label3.Text = "Items higher in the list are more general.  Items lower in the list take preceden" +
    "ce .";
			this.label3.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(277,498);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(575,16);
			this.label4.TabIndex = 26;
			this.label4.Text = "General Message (in addition to any dunning messages and appointment messages)";
			this.label4.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// textNote
			// 
			this.textNote.AcceptsReturn = true;
			this.textNote.Location = new System.Drawing.Point(276,518);
			this.textNote.Multiline = true;
			this.textNote.Name = "textNote";
			this.textNote.QuickPasteType = OpenDentBusiness.QuickPasteType.Statement;
			this.textNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textNote.Size = new System.Drawing.Size(616,102);
			this.textNote.TabIndex = 28;
			// 
			// FormBillingOptions
			// 
			this.AcceptButton = this.butCreate;
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(898,666);
			this.Controls.Add(this.textNote);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butCreate);
			this.Controls.Add(this.butAdd);
			this.Controls.Add(this.gridDun);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormBillingOptions";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Billing Options";
			this.Load += new System.EventHandler(this.FormBillingOptions_Load);
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormBillingOptions_Load(object sender, System.EventArgs e) {
			if(PIn.PDate(PrefB.GetString("DateLastAging")) < DateTime.Today){
				if(MessageBox.Show(Lan.g(this,"Update aging first?"),"",MessageBoxButtons.YesNo)==DialogResult.Yes){
					FormAging FormA=new FormAging();
					FormA.ShowDialog();
				}
			}
			for(int i=0;i<DefB.Short[(int)DefCat.BillingTypes].Length;i++){
				listBillType.Items.Add(DefB.Short[(int)DefCat.BillingTypes][i].ItemName);
			}
			textLastStatement.Text=DateTime.Today.AddMonths(-1).ToShortDateString();
			checkIncludeChanged.Checked=PrefB.GetBool("BillingIncludeChanged");
			string[] selectedBillTypes=PrefB.GetString("BillingSelectBillingTypes").Split(',');
			for(int i=0;i<selectedBillTypes.Length;i++){
				try{
					int order=DefB.GetOrder(DefCat.BillingTypes,Convert.ToInt32(selectedBillTypes[i]));
					if(order!=-1){
						listBillType.SetSelected(order,true);
					}
				}
				catch{}
			}
			if(listBillType.SelectedIndices.Count==0)
				listBillType.SelectedIndex=0;
			switch(PrefB.GetString("BillingAgeOfAccount")){
				default:
					radioAny.Checked=true;
					break;
				case "30":
					radio30.Checked=true;
					break;
				case "60":
					radio60.Checked=true;
					break;
				case "90":
					radio90.Checked=true;
					break;
			}
			checkBadAddress.Checked=PrefB.GetBool("BillingExcludeBadAddresses");
			checkExcludeInactive.Checked=PrefB.GetBool("BillingExcludeInactive");
			checkExcludeNegative.Checked=PrefB.GetBool("BillingExcludeNegative");
			checkExcludeInsPending.Checked=PrefB.GetBool("BillingExcludeInsPending");
			textExcludeLessThan.Text=PrefB.GetString("BillingExcludeLessThan");
			//blank is allowed
			FillDunning();
		}

		private void butAll_Click(object sender, System.EventArgs e) {
			for(int i=0;i<listBillType.Items.Count;i++){
				listBillType.SetSelected(i,true);
			}
		}

		private void butSaveDefault_Click(object sender, System.EventArgs e) {
			if( textExcludeLessThan.errorProvider1.GetError(textExcludeLessThan)!=""
				|| textLastStatement.errorProvider1.GetError(textLastStatement)!=""
				)
			{
				MsgBox.Show(this,"Please fix data entry errors first.");
				return;
			}
			string selectedBillingTypes="";
			for(int i=0;i<listBillType.SelectedIndices.Count;i++){//will always be at least 1
				if(i>0){
					selectedBillingTypes+=",";
				}
				selectedBillingTypes+=DefB.Short[(int)DefCat.BillingTypes][listBillType.SelectedIndices[i]].DefNum.ToString();
			}
			string ageOfAccount="";
			if(radioAny.Checked){
				ageOfAccount="";//the default
			}
			else if(radio30.Checked){
				ageOfAccount="30";
			}
			else if(radio60.Checked){
				ageOfAccount="60";
			}
			else if(radio90.Checked){
				ageOfAccount="90";
			}
			if(Prefs.UpdateBool("BillingIncludeChanged",checkIncludeChanged.Checked)
				| Prefs.UpdateString("BillingSelectBillingTypes",selectedBillingTypes)
				| Prefs.UpdateString("BillingAgeOfAccount",ageOfAccount)
				| Prefs.UpdateBool("BillingExcludeBadAddresses",checkBadAddress.Checked)
				| Prefs.UpdateBool("BillingExcludeInactive",checkExcludeInactive.Checked)
				| Prefs.UpdateBool("BillingExcludeNegative",checkExcludeNegative.Checked)
				| Prefs.UpdateBool("BillingExcludeInsPending",checkExcludeInsPending.Checked)
				| Prefs.UpdateString("BillingExcludeLessThan",textExcludeLessThan.Text))
			{
				DataValid.SetInvalid(InvalidTypes.Prefs);
			}
		}

		private void FillDunning(){
			dunningList=Dunnings.Refresh();
			gridDun.BeginUpdate();
			gridDun.Columns.Clear();
			ODGridColumn col=new ODGridColumn("Billing Type",100);
			gridDun.Columns.Add(col);
			col=new ODGridColumn("Aging",70);
			gridDun.Columns.Add(col);
			col=new ODGridColumn("Ins",40);
			gridDun.Columns.Add(col);
			col=new ODGridColumn("Message",200);
			gridDun.Columns.Add(col);
			col=new ODGridColumn("Bold Message",200);
			gridDun.Columns.Add(col);
			gridDun.Rows.Clear();
			OpenDental.UI.ODGridRow row;
			//string text;
			OpenDental.UI.ODGridCell cell;
			for(int i=0;i<dunningList.Length;i++){
				row=new OpenDental.UI.ODGridRow();
				if(dunningList[i].BillingType==0){
					row.Cells.Add(Lan.g(this,"all"));
				}
				else{
					row.Cells.Add(DefB.GetName(DefCat.BillingTypes,dunningList[i].BillingType));
				}
				if(dunningList[i].AgeAccount==0){
					row.Cells.Add(Lan.g(this,"any"));
				}
				else{
					row.Cells.Add(Lan.g(this,"Over ")+dunningList[i].AgeAccount.ToString());
				}
				if(dunningList[i].InsIsPending==YN.Unknown){
					row.Cells.Add(Lan.g(this,"any"));
				}
				else if(dunningList[i].InsIsPending==YN.Yes){
					row.Cells.Add(Lan.g(this,"Y"));
				}
				else if(dunningList[i].InsIsPending==YN.No){
					row.Cells.Add(Lan.g(this,"N"));
				}
				row.Cells.Add(dunningList[i].DunMessage);
				cell=new ODGridCell(dunningList[i].MessageBold);
				cell.Bold=YN.Yes;
				cell.ColorText=Color.DarkRed;
				row.Cells.Add(cell);
				gridDun.Rows.Add(row);
			}
			gridDun.EndUpdate();
		}

		private void gridDun_CellDoubleClick(object sender, OpenDental.UI.ODGridClickEventArgs e) {
			FormDunningEdit formD=new FormDunningEdit(dunningList[e.Row]);
			formD.ShowDialog();
			FillDunning();
		}

		private void butAdd_Click(object sender, System.EventArgs e) {
			Dunning dun=new Dunning();
			FormDunningEdit FormD=new FormDunningEdit(dun);
			FormD.IsNew=true;
			FormD.ShowDialog();
			if(FormD.DialogResult==DialogResult.Cancel){
				return;
			}
			FillDunning();
		}

		//private void butUndo_Click(object sender,EventArgs e) {
		//	FormBillingUndo FormB=new FormBillingUndo();
		//	FormB.ShowDialog();
		//	DialogResult=DialogResult.OK;
		//}

		private void butCreate_Click(object sender, System.EventArgs e) {
			if( textExcludeLessThan.errorProvider1.GetError(textExcludeLessThan)!=""
				|| textLastStatement.errorProvider1.GetError(textLastStatement)!=""
				)
			{
				MsgBox.Show(this,"Please fix data entry errors first.");
				return;
			}
			DateTime lastStatement=PIn.PDate(textLastStatement.Text);
			string getAge="";
			if(radio30.Checked) getAge="30";
			else if(radio60.Checked) getAge="60";
			else if(radio90.Checked) getAge="90";
			int[] billingIndices=new int[listBillType.SelectedIndices.Count];
			for(int i=0;i<billingIndices.Length;i++){
				billingIndices[i]=listBillType.SelectedIndices[i];
			}
			Cursor=Cursors.WaitCursor;
			FormBilling FormB=new FormBilling();
			FormB.AgingList=Patients.GetAgingList(getAge,lastStatement,billingIndices,checkBadAddress.Checked
				,checkExcludeNegative.Checked,PIn.PDouble(textExcludeLessThan.Text)
				,checkExcludeInactive.Checked,checkIncludeChanged.Checked,checkExcludeInsPending.Checked);
			FormB.GeneralNote=textNote.Text;
			Cursor=Cursors.Default;
			FormB.ShowDialog();
			DialogResult=DialogResult.OK;			
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		

		

		

		

	}




}
