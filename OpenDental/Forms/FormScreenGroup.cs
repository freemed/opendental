using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormScreenGroup : System.Windows.Forms.Form{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		///<summary></summary>
		public bool IsNew;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label24;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox comboProv;
		private System.Windows.Forms.ComboBox comboPlaceService;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private OpenDental.UI.Button butOK;
		private System.Windows.Forms.ComboBox comboCounty;
		private System.Windows.Forms.TextBox textDescription;
		private System.Windows.Forms.Label label4;
		private OpenDental.UI.Button butEdit;
		private System.Windows.Forms.Panel panelEdit;
		private System.Windows.Forms.ListView listMain;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.ColumnHeader columnHeader7;
		private System.Windows.Forms.ColumnHeader columnHeader8;
		private System.Windows.Forms.ColumnHeader columnHeader9;
		private System.Windows.Forms.ColumnHeader columnHeader10;
		private System.Windows.Forms.ColumnHeader columnHeader11;
		private System.Windows.Forms.ColumnHeader columnHeader12;
		private System.Windows.Forms.TextBox textScreenDate;
		private System.Windows.Forms.TextBox textProvName;
		private OpenDental.UI.Button butAdd;
		private OpenDental.UI.Button butDelete;
		private System.Windows.Forms.ColumnHeader columnHeader13;
		private System.Windows.Forms.ComboBox comboGradeSchool;
		public ScreenGroup ScreenGroupCur;

		///<summary></summary>
		public FormScreenGroup()
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
			this.label14 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.label24 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.textScreenDate = new System.Windows.Forms.TextBox();
			this.textDescription = new System.Windows.Forms.TextBox();
			this.comboProv = new System.Windows.Forms.ComboBox();
			this.comboPlaceService = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.butOK = new OpenDental.UI.Button();
			this.comboCounty = new System.Windows.Forms.ComboBox();
			this.comboGradeSchool = new System.Windows.Forms.ComboBox();
			this.textProvName = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.butEdit = new OpenDental.UI.Button();
			this.panelEdit = new System.Windows.Forms.Panel();
			this.listMain = new System.Windows.Forms.ListView();
			this.columnHeader13 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader9 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader10 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader11 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader12 = new System.Windows.Forms.ColumnHeader();
			this.butAdd = new OpenDental.UI.Button();
			this.butDelete = new OpenDental.UI.Button();
			this.panelEdit.SuspendLayout();
			this.SuspendLayout();
			// 
			// label14
			// 
			this.label14.Location = new System.Drawing.Point(12,77);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(44,16);
			this.label14.TabIndex = 12;
			this.label14.Text = "School";
			this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(3,56);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(52,15);
			this.label13.TabIndex = 11;
			this.label13.Text = "County";
			this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label24
			// 
			this.label24.Location = new System.Drawing.Point(4,35);
			this.label24.Name = "label24";
			this.label24.Size = new System.Drawing.Size(52,16);
			this.label24.TabIndex = 50;
			this.label24.Text = "Or Prov";
			this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(-2,-4);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(61,17);
			this.label1.TabIndex = 51;
			this.label1.Text = "Date";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// textScreenDate
			// 
			this.textScreenDate.Location = new System.Drawing.Point(0,0);
			this.textScreenDate.Name = "textScreenDate";
			this.textScreenDate.Size = new System.Drawing.Size(64,20);
			this.textScreenDate.TabIndex = 0;
			this.textScreenDate.Validating += new System.ComponentModel.CancelEventHandler(this.textScreenDate_Validating);
			// 
			// textDescription
			// 
			this.textDescription.Location = new System.Drawing.Point(64,0);
			this.textDescription.Name = "textDescription";
			this.textDescription.Size = new System.Drawing.Size(151,20);
			this.textDescription.TabIndex = 1;
			this.textDescription.TextChanged += new System.EventHandler(this.textProvName_TextChanged);
			// 
			// comboProv
			// 
			this.comboProv.BackColor = System.Drawing.SystemColors.Window;
			this.comboProv.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboProv.Location = new System.Drawing.Point(55,33);
			this.comboProv.MaxDropDownItems = 25;
			this.comboProv.Name = "comboProv";
			this.comboProv.Size = new System.Drawing.Size(173,21);
			this.comboProv.TabIndex = 2;
			this.comboProv.SelectionChangeCommitted += new System.EventHandler(this.comboProv_SelectionChangeCommitted);
			this.comboProv.SelectedIndexChanged += new System.EventHandler(this.comboProv_SelectedIndexChanged);
			this.comboProv.KeyDown += new System.Windows.Forms.KeyEventHandler(this.comboProv_KeyDown);
			// 
			// comboPlaceService
			// 
			this.comboPlaceService.BackColor = System.Drawing.SystemColors.Window;
			this.comboPlaceService.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboPlaceService.Location = new System.Drawing.Point(55,96);
			this.comboPlaceService.MaxDropDownItems = 25;
			this.comboPlaceService.Name = "comboPlaceService";
			this.comboPlaceService.Size = new System.Drawing.Size(173,21);
			this.comboPlaceService.TabIndex = 3;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(3,97);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(52,17);
			this.label2.TabIndex = 119;
			this.label2.Text = "Location";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(61,-3);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(103,16);
			this.label3.TabIndex = 128;
			this.label3.Text = "Descript";
			this.label3.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.Location = new System.Drawing.Point(180,565);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(48,21);
			this.butOK.TabIndex = 24;
			this.butOK.Text = "OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// comboCounty
			// 
			this.comboCounty.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboCounty.Location = new System.Drawing.Point(55,54);
			this.comboCounty.Name = "comboCounty";
			this.comboCounty.Size = new System.Drawing.Size(173,21);
			this.comboCounty.TabIndex = 4;
			this.comboCounty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.comboCounty_KeyDown);
			// 
			// comboGradeSchool
			// 
			this.comboGradeSchool.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboGradeSchool.Location = new System.Drawing.Point(55,75);
			this.comboGradeSchool.Name = "comboGradeSchool";
			this.comboGradeSchool.Size = new System.Drawing.Size(173,21);
			this.comboGradeSchool.TabIndex = 140;
			this.comboGradeSchool.KeyDown += new System.Windows.Forms.KeyEventHandler(this.comboGradeSchool_KeyDown);
			// 
			// textProvName
			// 
			this.textProvName.Location = new System.Drawing.Point(55,13);
			this.textProvName.Name = "textProvName";
			this.textProvName.Size = new System.Drawing.Size(173,20);
			this.textProvName.TabIndex = 141;
			this.textProvName.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textProvName_KeyUp);
			this.textProvName.TextChanged += new System.EventHandler(this.textProvName_TextChanged);
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(5,15);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(50,16);
			this.label4.TabIndex = 142;
			this.label4.Text = "Screener";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// butEdit
			// 
			this.butEdit.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butEdit.Autosize = true;
			this.butEdit.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butEdit.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butEdit.Location = new System.Drawing.Point(216,0);
			this.butEdit.Name = "butEdit";
			this.butEdit.Size = new System.Drawing.Size(18,21);
			this.butEdit.TabIndex = 143;
			this.butEdit.Text = "V";
			this.butEdit.Click += new System.EventHandler(this.butEdit_Click);
			// 
			// panelEdit
			// 
			this.panelEdit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panelEdit.Controls.Add(this.comboProv);
			this.panelEdit.Controls.Add(this.textProvName);
			this.panelEdit.Controls.Add(this.label24);
			this.panelEdit.Controls.Add(this.label4);
			this.panelEdit.Controls.Add(this.comboGradeSchool);
			this.panelEdit.Controls.Add(this.comboCounty);
			this.panelEdit.Controls.Add(this.label13);
			this.panelEdit.Controls.Add(this.label14);
			this.panelEdit.Controls.Add(this.label1);
			this.panelEdit.Controls.Add(this.label3);
			this.panelEdit.Controls.Add(this.comboPlaceService);
			this.panelEdit.Controls.Add(this.label2);
			this.panelEdit.ForeColor = System.Drawing.SystemColors.ControlText;
			this.panelEdit.Location = new System.Drawing.Point(0,21);
			this.panelEdit.Name = "panelEdit";
			this.panelEdit.Size = new System.Drawing.Size(234,121);
			this.panelEdit.TabIndex = 144;
			// 
			// listMain
			// 
			this.listMain.AutoArrange = false;
			this.listMain.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader13,
            this.columnHeader1,
            this.columnHeader3,
            this.columnHeader2,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9,
            this.columnHeader10,
            this.columnHeader11,
            this.columnHeader12});
			this.listMain.FullRowSelect = true;
			this.listMain.GridLines = true;
			this.listMain.Location = new System.Drawing.Point(0,143);
			this.listMain.Name = "listMain";
			this.listMain.Size = new System.Drawing.Size(710,420);
			this.listMain.TabIndex = 145;
			this.listMain.UseCompatibleStateImageBehavior = false;
			this.listMain.View = System.Windows.Forms.View.Details;
			this.listMain.DoubleClick += new System.EventHandler(this.listMain_DoubleClick);
			// 
			// columnHeader13
			// 
			this.columnHeader13.Text = "#";
			this.columnHeader13.Width = 30;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Grade";
			this.columnHeader1.Width = 50;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Age";
			this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.columnHeader3.Width = 38;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Race";
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "Sex";
			this.columnHeader4.Width = 38;
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "Urgency";
			// 
			// columnHeader6
			// 
			this.columnHeader6.Text = "Caries";
			this.columnHeader6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.columnHeader6.Width = 46;
			// 
			// columnHeader7
			// 
			this.columnHeader7.Text = "ECC";
			this.columnHeader7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.columnHeader7.Width = 40;
			// 
			// columnHeader8
			// 
			this.columnHeader8.Text = "CarExp";
			this.columnHeader8.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.columnHeader8.Width = 50;
			// 
			// columnHeader9
			// 
			this.columnHeader9.Text = "ExSeal";
			this.columnHeader9.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.columnHeader9.Width = 50;
			// 
			// columnHeader10
			// 
			this.columnHeader10.Text = "NeedSeal";
			this.columnHeader10.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.columnHeader10.Width = 64;
			// 
			// columnHeader11
			// 
			this.columnHeader11.Text = "NoTeeth";
			this.columnHeader11.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// columnHeader12
			// 
			this.columnHeader12.Text = "Comments";
			this.columnHeader12.Width = 100;
			// 
			// butAdd
			// 
			this.butAdd.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butAdd.Autosize = true;
			this.butAdd.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butAdd.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butAdd.Location = new System.Drawing.Point(9,565);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(56,21);
			this.butAdd.TabIndex = 146;
			this.butAdd.Text = "Add Item";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// butDelete
			// 
			this.butDelete.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.butDelete.Autosize = true;
			this.butDelete.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butDelete.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butDelete.Location = new System.Drawing.Point(71,565);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(68,21);
			this.butDelete.TabIndex = 147;
			this.butDelete.Text = "Delete Item";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// FormScreenGroup
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(714,588);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.butAdd);
			this.Controls.Add(this.listMain);
			this.Controls.Add(this.panelEdit);
			this.Controls.Add(this.butEdit);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.textDescription);
			this.Controls.Add(this.textScreenDate);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormScreenGroup";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Screening Group";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.FormScreenGroup_Closing);
			this.Load += new System.EventHandler(this.FormScreenGroup_Load);
			this.panelEdit.ResumeLayout(false);
			this.panelEdit.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormScreenGroup_Load(object sender, System.EventArgs e) {
			Location=new Point(200,200);
			if(IsNew){
				ScreenGroups.Insert(ScreenGroupCur);
			}
			FillGrid();
			if(Screens.List.Length>0){
				OpenDentBusiness.Screen ScreenCur=Screens.List[0];
				ScreenGroupCur.SGDate=ScreenCur.ScreenDate;
				ScreenGroupCur.ProvName=ScreenCur.ProvName;
				ScreenGroupCur.ProvNum=ScreenCur.ProvNum;
				ScreenGroupCur.County=ScreenCur.County;
				ScreenGroupCur.GradeSchool=ScreenCur.GradeSchool;
				ScreenGroupCur.PlaceService=ScreenCur.PlaceService;
			}
			textScreenDate.Text=ScreenGroupCur.SGDate.ToShortDateString();
			textDescription.Text=ScreenGroupCur.Description;
			textProvName.Text=ScreenGroupCur.ProvName;//has to be filled before provnum
			for(int i=0;i<Providers.List.Length;i++){
				comboProv.Items.Add(Providers.List[i].Abbr);
				if(ScreenGroupCur.ProvNum==Providers.List[i].ProvNum){
					comboProv.SelectedIndex=i;
				}
			}
			comboCounty.Items.AddRange(Counties.ListNames);
			if(ScreenGroupCur.County==null)
				ScreenGroupCur.County="";//prevents the next line from crashing
			comboCounty.SelectedIndex=comboCounty.Items.IndexOf(ScreenGroupCur.County);//"" etc OK
			comboGradeSchool.Items.AddRange(Schools.ListNames);
			if(ScreenGroupCur.GradeSchool==null)
				ScreenGroupCur.GradeSchool="";//prevents the next line from crashing
			comboGradeSchool.SelectedIndex=comboGradeSchool.Items.IndexOf(ScreenGroupCur.GradeSchool);//"" etc OK
			comboPlaceService.Items.AddRange(Enum.GetNames(typeof(PlaceOfService)));
			comboPlaceService.SelectedIndex=(int)ScreenGroupCur.PlaceService;
		}

		private void FillGrid(){
			Screens.Refresh(ScreenGroupCur.ScreenGroupNum);
			ListViewItem[] items=new ListViewItem[Screens.List.Length];
			for(int i=0;i<items.Length;i++){
				items[i]=new ListViewItem(Screens.List[i].ScreenGroupOrder.ToString());
				items[i].SubItems.Add(Screens.List[i].GradeLevel.ToString());
				if(Screens.List[i].Age==0)
					items[i].SubItems.Add("");
				else
					items[i].SubItems.Add(Screens.List[i].Age.ToString());
				items[i].SubItems.Add(Screens.List[i].Race.ToString());
				items[i].SubItems.Add(Screens.List[i].Gender.ToString());
				items[i].SubItems.Add(Screens.List[i].Urgency.ToString());
				items[i].SubItems.Add(getX(Screens.List[i].HasCaries));
				items[i].SubItems.Add(getX(Screens.List[i].EarlyChildCaries));
				items[i].SubItems.Add(getX(Screens.List[i].CariesExperience));
				items[i].SubItems.Add(getX(Screens.List[i].ExistingSealants));
				items[i].SubItems.Add(getX(Screens.List[i].NeedsSealants));
				items[i].SubItems.Add(getX(Screens.List[i].MissingAllTeeth));				
				items[i].SubItems.Add(Screens.List[i].Comments);
			}
			listMain.Items.Clear();
			listMain.Items.AddRange(items);
		}

		private string getX(YN ynValue){
			if(ynValue==YN.Yes)
				return "X";
			return "";
		}

		private void listMain_DoubleClick(object sender, System.EventArgs e) {
			FormScreenEdit FormSE=new FormScreenEdit();
			FormSE.ScreenCur=Screens.List[listMain.SelectedIndices[0]];
			FormSE.ScreenGroupCur=ScreenGroupCur;
			FormSE.ShowDialog();
			if(FormSE.DialogResult!=DialogResult.OK){
				return;
			}
			FillGrid();
		}

		private void textScreenDate_Validating(object sender, System.ComponentModel.CancelEventArgs e) {
			try{
				DateTime.Parse(textScreenDate.Text);
			}
			catch{
				MessageBox.Show("Date invalid");
				e.Cancel=true;
			}
		}

		private void textProvName_TextChanged(object sender, System.EventArgs e) {
			/*if(textProvName.Text!=""){    //if a prov name was entered
				comboProv.SelectedIndex=-1;//then set the provnum to none.
			}*/
		}

		private void textProvName_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e) {
			comboProv.SelectedIndex=-1;//set the provnum to none.
		}

		private void comboProv_SelectedIndexChanged(object sender, System.EventArgs e) {
			if(comboProv.SelectedIndex!=-1){//if a prov was selected
				//set the provname accordingly
				textProvName.Text=Providers.List[comboProv.SelectedIndex].LName+", "
					+Providers.List[comboProv.SelectedIndex].FName;
			}
		}

		private void comboProv_SelectionChangeCommitted(object sender, System.EventArgs e) {
			
		}

		private void comboProv_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e) {
			if(e.KeyCode==Keys.Back || e.KeyCode==Keys.Delete){
				comboProv.SelectedIndex=-1;
			}
		}

		private void comboCounty_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e) {
			if(e.KeyCode==Keys.Back || e.KeyCode==Keys.Delete){
				comboCounty.SelectedIndex=-1;
			}
		}

		private void comboGradeSchool_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e) {
			if(e.KeyCode==Keys.Back || e.KeyCode==Keys.Delete){
				comboGradeSchool.SelectedIndex=-1;
			}
		}

		private void butEdit_Click(object sender, System.EventArgs e) {
			if(panelEdit.Visible){//if panel was down
				panelEdit.Visible=false;
				listMain.Top=panelEdit.Top;
			}
			else{//panel was up, so drop it down
				panelEdit.Visible=true;
				listMain.Top=panelEdit.Bottom;
			}
			listMain.Height=this.ClientSize.Height-listMain.Top-25;
		}

		private void butAdd_Click(object sender, System.EventArgs e) {
			FormScreenEdit FormSE=new FormScreenEdit();
			FormSE.ScreenGroupCur=ScreenGroupCur;
			FormSE.IsNew=true;
			if(Screens.List.Length==0){
				FormSE.ScreenCur=new OpenDentBusiness.Screen();
				FormSE.ScreenCur.ScreenGroupOrder=1;
			}
			else{
				FormSE.ScreenCur=Screens.List[Screens.List.Length-1];//'remembers' the last entry
				FormSE.ScreenCur.ScreenGroupOrder=FormSE.ScreenCur.ScreenGroupOrder+1;//increments for next
			}
			while(true){
				FormSE.ShowDialog();
				if(FormSE.DialogResult!=DialogResult.OK){
					return;
				}
				FillGrid();
			}
		}

		private void butDelete_Click(object sender, System.EventArgs e) {
			if(listMain.SelectedIndices.Count==0){
				MessageBox.Show("Please select items first.");
				return;
			}
			for(int i=0;i<listMain.SelectedIndices.Count;i++){
				Screens.Delete(Screens.List[listMain.SelectedIndices[i]]);
			}
			FillGrid();
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(Screens.List.Length==0){
				if(MessageBox.Show("Since you have no items in the list, the screener and location information cannot be saved. Continue?","",MessageBoxButtons.OKCancel)==DialogResult.Cancel){
					return;
				}
			}
			ScreenGroupCur.SGDate=PIn.PDate(textScreenDate.Text);
			ScreenGroupCur.Description=textDescription.Text;
			ScreenGroupCur.ProvName=textProvName.Text;
			ScreenGroupCur.ProvNum=comboProv.SelectedIndex+1;//this works for -1 also.
			if(comboCounty.SelectedIndex==-1)
				ScreenGroupCur.County="";
			else
				ScreenGroupCur.County=comboCounty.SelectedItem.ToString();
			if(comboGradeSchool.SelectedIndex==-1)
				ScreenGroupCur.GradeSchool="";
			else
				ScreenGroupCur.GradeSchool=comboGradeSchool.SelectedItem.ToString();
			ScreenGroupCur.PlaceService=(PlaceOfService)comboPlaceService.SelectedIndex;
			ScreenGroups.Update(ScreenGroupCur);
			Screens.UpdateForGroup(ScreenGroupCur);
			DialogResult=DialogResult.OK;
		}

		private void FormScreenGroup_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			if(DialogResult==DialogResult.OK)
				return;
			if(IsNew){
				ScreenGroups.Delete(ScreenGroupCur);
			}
		}

		

		

		

		

		

		

		

		

		

		

		

		

		

		

		

		


		

		

		


	}
}





















