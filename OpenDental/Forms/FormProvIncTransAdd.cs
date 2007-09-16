using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDentBusiness;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormProvIncTransAdd : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private Label label5;
		private ComboBox comboProvFrom;
		private TextBox textFromPat;
		private Label label1;
		private ComboBox comboFromPat;
		private OpenDental.UI.Button butSearchFrom;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private int FromPatNum;
		private int ToPatNum;
		public Family FamCur;
		private OpenDental.UI.Button butSearchTo;
		private ComboBox comboToPat;
		private Label label2;
		private TextBox textToPat;
		private ComboBox comboProvTo;
		private Label label3;
		private ValidDouble textAmount;
		private Label labelAmount;
		///<summary>If dialogResult.OK, then this will contain exactly two paysplits.</summary>
		public List<PaySplit> SplitList;
		public int PayNum;
		public int PatNum;

		///<summary>This form is not currently used.  A better way was found.</summary>
		public FormProvIncTransAdd()
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormProvIncTransAdd));
			this.label5 = new System.Windows.Forms.Label();
			this.comboProvFrom = new System.Windows.Forms.ComboBox();
			this.textFromPat = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.comboFromPat = new System.Windows.Forms.ComboBox();
			this.comboToPat = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textToPat = new System.Windows.Forms.TextBox();
			this.comboProvTo = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.labelAmount = new System.Windows.Forms.Label();
			this.textAmount = new OpenDental.ValidDouble();
			this.butSearchTo = new OpenDental.UI.Button();
			this.butSearchFrom = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.SuspendLayout();
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(57,22);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(100,16);
			this.label5.TabIndex = 12;
			this.label5.Text = "From Provider";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboProvFrom
			// 
			this.comboProvFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboProvFrom.FormattingEnabled = true;
			this.comboProvFrom.Location = new System.Drawing.Point(160,21);
			this.comboProvFrom.MaxDropDownItems = 40;
			this.comboProvFrom.Name = "comboProvFrom";
			this.comboProvFrom.Size = new System.Drawing.Size(154,21);
			this.comboProvFrom.TabIndex = 13;
			// 
			// textFromPat
			// 
			this.textFromPat.Location = new System.Drawing.Point(320,49);
			this.textFromPat.Name = "textFromPat";
			this.textFromPat.Size = new System.Drawing.Size(193,20);
			this.textFromPat.TabIndex = 111;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(57,49);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100,16);
			this.label1.TabIndex = 114;
			this.label1.Text = "From Patient";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboFromPat
			// 
			this.comboFromPat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboFromPat.FormattingEnabled = true;
			this.comboFromPat.Location = new System.Drawing.Point(160,48);
			this.comboFromPat.MaxDropDownItems = 40;
			this.comboFromPat.Name = "comboFromPat";
			this.comboFromPat.Size = new System.Drawing.Size(154,21);
			this.comboFromPat.TabIndex = 115;
			this.comboFromPat.SelectionChangeCommitted += new System.EventHandler(this.comboFromPat_SelectionChangeCommitted);
			// 
			// comboToPat
			// 
			this.comboToPat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboToPat.FormattingEnabled = true;
			this.comboToPat.Location = new System.Drawing.Point(160,135);
			this.comboToPat.MaxDropDownItems = 40;
			this.comboToPat.Name = "comboToPat";
			this.comboToPat.Size = new System.Drawing.Size(154,21);
			this.comboToPat.TabIndex = 122;
			this.comboToPat.SelectionChangeCommitted += new System.EventHandler(this.comboToPat_SelectionChangeCommitted);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(57,136);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100,16);
			this.label2.TabIndex = 121;
			this.label2.Text = "To Patient";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textToPat
			// 
			this.textToPat.Location = new System.Drawing.Point(320,136);
			this.textToPat.Name = "textToPat";
			this.textToPat.Size = new System.Drawing.Size(193,20);
			this.textToPat.TabIndex = 120;
			// 
			// comboProvTo
			// 
			this.comboProvTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboProvTo.FormattingEnabled = true;
			this.comboProvTo.Location = new System.Drawing.Point(160,108);
			this.comboProvTo.MaxDropDownItems = 40;
			this.comboProvTo.Name = "comboProvTo";
			this.comboProvTo.Size = new System.Drawing.Size(154,21);
			this.comboProvTo.TabIndex = 119;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(57,109);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(100,16);
			this.label3.TabIndex = 118;
			this.label3.Text = "To Provider";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelAmount
			// 
			this.labelAmount.Location = new System.Drawing.Point(56,190);
			this.labelAmount.Name = "labelAmount";
			this.labelAmount.Size = new System.Drawing.Size(104,16);
			this.labelAmount.TabIndex = 126;
			this.labelAmount.Text = "Amount";
			this.labelAmount.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textAmount
			// 
			this.textAmount.Location = new System.Drawing.Point(160,187);
			this.textAmount.Name = "textAmount";
			this.textAmount.Size = new System.Drawing.Size(77,20);
			this.textAmount.TabIndex = 125;
			// 
			// butSearchTo
			// 
			this.butSearchTo.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butSearchTo.Autosize = true;
			this.butSearchTo.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSearchTo.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSearchTo.CornerRadius = 4F;
			this.butSearchTo.Location = new System.Drawing.Point(519,133);
			this.butSearchTo.Name = "butSearchTo";
			this.butSearchTo.Size = new System.Drawing.Size(75,24);
			this.butSearchTo.TabIndex = 123;
			this.butSearchTo.Text = "Search";
			this.butSearchTo.Click += new System.EventHandler(this.butSearchTo_Click);
			// 
			// butSearchFrom
			// 
			this.butSearchFrom.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butSearchFrom.Autosize = true;
			this.butSearchFrom.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butSearchFrom.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butSearchFrom.CornerRadius = 4F;
			this.butSearchFrom.Location = new System.Drawing.Point(519,46);
			this.butSearchFrom.Name = "butSearchFrom";
			this.butSearchFrom.Size = new System.Drawing.Size(75,24);
			this.butSearchFrom.TabIndex = 116;
			this.butSearchFrom.Text = "Search";
			this.butSearchFrom.Click += new System.EventHandler(this.butSearchFrom_Click);
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(438,242);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 1;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// butCancel
			// 
			this.butCancel.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butCancel.Autosize = true;
			this.butCancel.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butCancel.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butCancel.CornerRadius = 4F;
			this.butCancel.Location = new System.Drawing.Point(519,242);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,26);
			this.butCancel.TabIndex = 0;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// FormProvIncTransAdd
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(638,293);
			this.Controls.Add(this.textAmount);
			this.Controls.Add(this.labelAmount);
			this.Controls.Add(this.butSearchTo);
			this.Controls.Add(this.comboToPat);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textToPat);
			this.Controls.Add(this.comboProvTo);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.butSearchFrom);
			this.Controls.Add(this.comboFromPat);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textFromPat);
			this.Controls.Add(this.comboProvFrom);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormProvIncTransAdd";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Add Transfer";
			this.Load += new System.EventHandler(this.FormProvIncTransAdd_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormProvIncTransAdd_Load(object sender,EventArgs e) {
			for(int i=0;i<Providers.List.Length;i++) {
				comboProvFrom.Items.Add(Providers.List[i].Abbr);
				comboProvTo.Items.Add(Providers.List[i].Abbr);
			}
			for(int i=0;i<FamCur.List.Length;i++) {
				comboFromPat.Items.Add(FamCur.GetNameInFamFL(i));
				comboToPat.Items.Add(FamCur.GetNameInFamFL(i));
				if(FamCur.List[i].PatNum==PatNum){
					comboFromPat.SelectedIndex=i;
					comboToPat.SelectedIndex=i;
				}
			}
		}

		private void butSearchFrom_Click(object sender,EventArgs e) {
			FormPatientSelect FormPS=new FormPatientSelect();
			FormPS.SelectionModeOnly=true;
			FormPS.ShowDialog();
			if(FormPS.DialogResult!=DialogResult.OK) {
				return;
			}
			FromPatNum=FormPS.SelectedPatNum;
			ShowPats();
		}

		private void butSearchTo_Click(object sender,EventArgs e) {
			FormPatientSelect FormPS=new FormPatientSelect();
			FormPS.SelectionModeOnly=true;
			FormPS.ShowDialog();
			if(FormPS.DialogResult!=DialogResult.OK) {
				return;
			}
			ToPatNum=FormPS.SelectedPatNum;
			ShowPats();
		}

		///<summary>After user selects a different patient, this changes the display to show it properly.</summary>
		private void ShowPats(){
			comboFromPat.SelectedIndex=-1;
			comboToPat.SelectedIndex=-1;
			textFromPat.Text="";
			textToPat.Text="";
			for(int i=0;i<FamCur.List.Length;i++){
				if(FamCur.List[i].PatNum==FromPatNum){
					comboFromPat.SelectedIndex=i;
				}
				if(FamCur.List[i].PatNum==ToPatNum) {
					comboToPat.SelectedIndex=i;
				}
			}
			if(FromPatNum!=0 && comboFromPat.SelectedIndex==-1){
				textFromPat.Text=Patients.GetLim(FromPatNum).GetNameFL();
			}
			if(ToPatNum!=0 && comboToPat.SelectedIndex==-1) {
				textToPat.Text=Patients.GetLim(ToPatNum).GetNameFL();
			}
		}

		private void comboFromPat_SelectionChangeCommitted(object sender,EventArgs e) {
			FromPatNum=FamCur.List[comboFromPat.SelectedIndex].PatNum;
			ShowPats();
		}

		private void comboToPat_SelectionChangeCommitted(object sender,EventArgs e) {
			ToPatNum=FamCur.List[comboToPat.SelectedIndex].PatNum;
			ShowPats();
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(textAmount.errorProvider1.GetError(textAmount)!="") {
				MsgBox.Show(this,"Please fix data entry errors first.");
				return;
			}
			if(textAmount.Text=="") {
				MsgBox.Show(this,"Please enter an amount.");
				return;
			}
			double amt=PIn.PDouble(textAmount.Text);
			if(amt<=0) {
				MsgBox.Show(this,"Amount must be be greater than zero.");
				return;
			}
			if(comboProvFrom.SelectedIndex==-1 || comboProvTo.SelectedIndex==-1){
				MsgBox.Show(this,"Providers must be selected");
				return;
			}
			if(FromPatNum==0 || ToPatNum==0){
				MsgBox.Show(this,"Patients must be selected");
				return;
			}
			SplitList=new List<PaySplit>();
			//From-----------------------------------------------------------------------------------
			PaySplit split=new PaySplit();
			split.PatNum=FromPatNum;
			split.PayNum=PayNum;
			split.ProcDate=DateTime.Today;//will be updated in parent form.
			split.DatePay=DateTime.Today;//will be updated when closing parent form.
			split.ProvNum=Providers.List[comboProvFrom.SelectedIndex].ProvNum;
			split.SplitAmt=-amt;
			SplitList.Add(split);
			//To-----------------------------------------------------------------------------------
			split=new PaySplit();
			split.PatNum=ToPatNum;
			split.PayNum=PayNum;
			split.ProcDate=DateTime.Today;//will be updated in parent form.
			split.DatePay=DateTime.Today;//will be updated when closing parent form.
			split.ProvNum=Providers.List[comboProvTo.SelectedIndex].ProvNum;
			split.SplitAmt=amt;
			SplitList.Add(split);
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		

	

		

		


	}
}





















