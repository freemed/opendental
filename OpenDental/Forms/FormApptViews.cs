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
	public class FormApptViews : System.Windows.Forms.Form{
		private System.Windows.Forms.Label label1;
		private OpenDental.UI.Button butDown;
		private OpenDental.UI.Button butUp;
		private OpenDental.UI.Button butAdd;
		private System.Windows.Forms.ListBox listViews;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.RadioButton radioTen;
		private System.Windows.Forms.RadioButton radioFifteen;
		private System.Windows.Forms.CheckBox checkTwoRows;
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private System.Windows.Forms.Label label2;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private bool viewChanged;

		///<summary></summary>
		public FormApptViews()
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormApptViews));
			this.butCancel = new OpenDental.UI.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.listViews = new System.Windows.Forms.ListBox();
			this.butDown = new OpenDental.UI.Button();
			this.butUp = new OpenDental.UI.Button();
			this.butAdd = new OpenDental.UI.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.radioFifteen = new System.Windows.Forms.RadioButton();
			this.radioTen = new System.Windows.Forms.RadioButton();
			this.checkTwoRows = new System.Windows.Forms.CheckBox();
			this.butOK = new OpenDental.UI.Button();
			this.label2 = new System.Windows.Forms.Label();
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
			this.butCancel.Location = new System.Drawing.Point(447,433);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,26);
			this.butCancel.TabIndex = 0;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(57,32);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(158,23);
			this.label1.TabIndex = 1;
			this.label1.Text = "Views";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// listViews
			// 
			this.listViews.Location = new System.Drawing.Point(56,60);
			this.listViews.Name = "listViews";
			this.listViews.Size = new System.Drawing.Size(183,329);
			this.listViews.TabIndex = 2;
			this.listViews.DoubleClick += new System.EventHandler(this.listViews_DoubleClick);
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
			this.butDown.Location = new System.Drawing.Point(151,437);
			this.butDown.Name = "butDown";
			this.butDown.Size = new System.Drawing.Size(89,26);
			this.butDown.TabIndex = 38;
			this.butDown.Text = "&Down";
			this.butDown.Click += new System.EventHandler(this.butDown_Click);
			// 
			// butUp
			// 
			this.butUp.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butUp.Autosize = true;
			this.butUp.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butUp.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butUp.CornerRadius = 4F;
			this.butUp.Image = global::OpenDental.Properties.Resources.up;
			this.butUp.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.butUp.Location = new System.Drawing.Point(151,399);
			this.butUp.Name = "butUp";
			this.butUp.Size = new System.Drawing.Size(89,26);
			this.butUp.TabIndex = 39;
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
			this.butAdd.Location = new System.Drawing.Point(55,399);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(89,26);
			this.butAdd.TabIndex = 36;
			this.butAdd.Text = "&Add";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.radioFifteen);
			this.groupBox1.Controls.Add(this.radioTen);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(279,54);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(169,70);
			this.groupBox1.TabIndex = 40;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Time Increments";
			// 
			// radioFifteen
			// 
			this.radioFifteen.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioFifteen.Location = new System.Drawing.Point(23,42);
			this.radioFifteen.Name = "radioFifteen";
			this.radioFifteen.Size = new System.Drawing.Size(100,18);
			this.radioFifteen.TabIndex = 1;
			this.radioFifteen.Text = "15 Min";
			// 
			// radioTen
			// 
			this.radioTen.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.radioTen.Location = new System.Drawing.Point(23,21);
			this.radioTen.Name = "radioTen";
			this.radioTen.Size = new System.Drawing.Size(100,18);
			this.radioTen.TabIndex = 0;
			this.radioTen.Text = "10 Min";
			// 
			// checkTwoRows
			// 
			this.checkTwoRows.Location = new System.Drawing.Point(0,0);
			this.checkTwoRows.Name = "checkTwoRows";
			this.checkTwoRows.Size = new System.Drawing.Size(104,24);
			this.checkTwoRows.TabIndex = 0;
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(447,394);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 41;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(299,413);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(126,44);
			this.label2.TabIndex = 42;
			this.label2.Text = "Changes to the Views will always be saved";
			this.label2.TextAlign = System.Drawing.ContentAlignment.BottomRight;
			// 
			// FormApptViews
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(546,485);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.butDown);
			this.Controls.Add(this.butUp);
			this.Controls.Add(this.butAdd);
			this.Controls.Add(this.listViews);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormApptViews";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Appointment Views";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.FormApptViews_Closing);
			this.Load += new System.EventHandler(this.FormApptViews_Load);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormApptViews_Load(object sender, System.EventArgs e) {
			FillViewList();
			if(PrefB.GetInt("AppointmentTimeIncrement")==10){
				radioTen.Checked=true;
			}
			else{
				radioFifteen.Checked=true;
			}
		}

		private void FillViewList(){
			ApptViews.Refresh();
			ApptViewItems.Refresh();
			listViews.Items.Clear();
			string F;
			for(int i=0;i<ApptViews.List.Length;i++){
				if(i<12)
					F="F"+(i+1).ToString()+"-";
				else
					F="";
				listViews.Items.Add(F+ApptViews.List[i].Description);
			}
		}
		
		private void butAdd_Click(object sender, System.EventArgs e) {
			ApptView ApptViewCur=new ApptView();
			ApptViewCur.ItemOrder=ApptViews.List.Length;
			ApptViews.Insert(ApptViewCur);//this also gets the primary key
			FormApptViewEdit FormAVE=new FormApptViewEdit();
			FormAVE.ApptViewCur=ApptViewCur;
			FormAVE.IsNew=true;
			FormAVE.ShowDialog();
			if(FormAVE.DialogResult!=DialogResult.OK){
				return;
			}
			viewChanged=true;
			FillViewList();
			listViews.SelectedIndex=listViews.Items.Count-1;//this works even if no items
		}

		private void listViews_DoubleClick(object sender, System.EventArgs e) {
			if(listViews.SelectedIndex==-1){
				return;
			}
			int selected=listViews.SelectedIndex;
			ApptView ApptViewCur=ApptViews.List[listViews.SelectedIndex];
			FormApptViewEdit FormAVE=new FormApptViewEdit();
			FormAVE.ApptViewCur=ApptViewCur;
			FormAVE.ShowDialog();
			if(FormAVE.DialogResult!=DialogResult.OK){
				return;
			}
			viewChanged=true;
			FillViewList();
			if(selected<listViews.Items.Count){
				listViews.SelectedIndex=selected;
			}
		}

		private void butUp_Click(object sender, System.EventArgs e) {
			if(listViews.SelectedIndex==-1){
				MessageBox.Show(Lan.g(this,"Please select a category first."));
				return;
			}
			if(listViews.SelectedIndex==0){
				return;//can't go up any more
			}
			int selected=listViews.SelectedIndex;
			//it will flip flop with the one above it
			ApptView ApptViewCur=ApptViews.List[listViews.SelectedIndex];
			ApptViewCur.ItemOrder=ApptViewCur.ItemOrder-1;
			ApptViews.Update(ApptViewCur);
			//now the other
			ApptViewCur=ApptViews.List[listViews.SelectedIndex-1];
			ApptViewCur.ItemOrder=ApptViewCur.ItemOrder+1;
			ApptViews.Update(ApptViewCur);
			viewChanged=true;
			FillViewList();
			listViews.SelectedIndex=selected-1;
		}

		private void butDown_Click(object sender, System.EventArgs e) {
			if(listViews.SelectedIndex==-1){
				MessageBox.Show(Lan.g(this,"Please select a category first."));
				return;
			}
			if(listViews.SelectedIndex==ApptViews.List.Length-1){
				return;//can't go down any more
			}
			int selected=listViews.SelectedIndex;
			//it will flip flop with the one below it
			ApptView ApptViewCur=ApptViews.List[listViews.SelectedIndex];
			ApptViewCur.ItemOrder=ApptViewCur.ItemOrder+1;
			ApptViews.Update(ApptViewCur);
			//now the other
			ApptViewCur=ApptViews.List[listViews.SelectedIndex+1];
			ApptViewCur.ItemOrder=ApptViewCur.ItemOrder-1;
			ApptViews.Update(ApptViewCur);
			viewChanged=true;
			FillViewList();
			listViews.SelectedIndex=selected+1;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(PrefB.GetInt("AppointmentTimeIncrement")==15
				&& radioTen.Checked)
			{
				Prefs.UpdateInt("AppointmentTimeIncrement",10);
				DataValid.SetInvalid(InvalidTypes.Prefs);
			}
			if(PrefB.GetInt("AppointmentTimeIncrement")==10
				&& radioFifteen.Checked)
			{
				Prefs.UpdateInt("AppointmentTimeIncrement",15);
				DataValid.SetInvalid(InvalidTypes.Prefs);
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			//all this cancels is the 10 vs 15 selection
			DialogResult=DialogResult.Cancel;
		}

		private void FormApptViews_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			if(viewChanged){
				DataValid.SetInvalid(InvalidTypes.Views);
			}
		}


	

		


	}
}





















