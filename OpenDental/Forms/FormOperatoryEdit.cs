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
	public class FormOperatoryEdit : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private System.Windows.Forms.Label label1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		///<summary></summary>
		public bool IsNew;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.ComboBox comboClinic;
		private System.Windows.Forms.Label labelClinic;
		private System.Windows.Forms.TextBox textOpName;
		private System.Windows.Forms.TextBox textAbbrev;
		private System.Windows.Forms.CheckBox checkIsHidden;
		private System.Windows.Forms.ComboBox comboProvHygienist;
		private System.Windows.Forms.ComboBox comboProvDentist;
		private System.Windows.Forms.CheckBox checkIsHygiene;
		private Operatory OpCur;

		///<summary></summary>
		public FormOperatoryEdit(Operatory opCur)
		{
			//
			// Required for Windows Form Designer support
			//
			OpCur=opCur;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormOperatoryEdit));
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.textOpName = new System.Windows.Forms.TextBox();
			this.textAbbrev = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.checkIsHidden = new System.Windows.Forms.CheckBox();
			this.comboProvHygienist = new System.Windows.Forms.ComboBox();
			this.comboProvDentist = new System.Windows.Forms.ComboBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.checkIsHygiene = new System.Windows.Forms.CheckBox();
			this.comboClinic = new System.Windows.Forms.ComboBox();
			this.labelClinic = new System.Windows.Forms.Label();
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
			this.butCancel.Location = new System.Drawing.Point(473,204);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,26);
			this.butCancel.TabIndex = 9;
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
			this.butOK.Location = new System.Drawing.Point(382,204);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 8;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(9,21);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(148,17);
			this.label1.TabIndex = 2;
			this.label1.Text = "Op Name";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textOpName
			// 
			this.textOpName.Location = new System.Drawing.Point(160,20);
			this.textOpName.MaxLength = 255;
			this.textOpName.Name = "textOpName";
			this.textOpName.Size = new System.Drawing.Size(241,20);
			this.textOpName.TabIndex = 0;
			// 
			// textAbbrev
			// 
			this.textAbbrev.Location = new System.Drawing.Point(160,40);
			this.textAbbrev.MaxLength = 5;
			this.textAbbrev.Name = "textAbbrev";
			this.textAbbrev.Size = new System.Drawing.Size(78,20);
			this.textAbbrev.TabIndex = 1;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8,43);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(151,17);
			this.label2.TabIndex = 99;
			this.label2.Text = "Abbrev (max 5 char)";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// checkIsHidden
			// 
			this.checkIsHidden.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkIsHidden.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkIsHidden.Location = new System.Drawing.Point(69,63);
			this.checkIsHidden.Name = "checkIsHidden";
			this.checkIsHidden.Size = new System.Drawing.Size(104,16);
			this.checkIsHidden.TabIndex = 110;
			this.checkIsHidden.Text = "Is Hidden";
			this.checkIsHidden.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboProvHygienist
			// 
			this.comboProvHygienist.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboProvHygienist.Location = new System.Drawing.Point(160,123);
			this.comboProvHygienist.MaxDropDownItems = 30;
			this.comboProvHygienist.Name = "comboProvHygienist";
			this.comboProvHygienist.Size = new System.Drawing.Size(126,21);
			this.comboProvHygienist.TabIndex = 114;
			// 
			// comboProvDentist
			// 
			this.comboProvDentist.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboProvDentist.Location = new System.Drawing.Point(160,102);
			this.comboProvDentist.MaxDropDownItems = 100;
			this.comboProvDentist.Name = "comboProvDentist";
			this.comboProvDentist.Size = new System.Drawing.Size(126,21);
			this.comboProvDentist.TabIndex = 113;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(34,127);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(123,16);
			this.label6.TabIndex = 112;
			this.label6.Text = "Hygienist";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(23,106);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(134,16);
			this.label7.TabIndex = 111;
			this.label7.Text = "Dentist";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(178,64);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(316,16);
			this.label8.TabIndex = 115;
			this.label8.Text = "(because you can\'t delete an operatory)";
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(178,148);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(363,16);
			this.label9.TabIndex = 117;
			this.label9.Text = "The hygeinist will be considered the main provider for this op.";
			// 
			// checkIsHygiene
			// 
			this.checkIsHygiene.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkIsHygiene.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkIsHygiene.Location = new System.Drawing.Point(69,147);
			this.checkIsHygiene.Name = "checkIsHygiene";
			this.checkIsHygiene.Size = new System.Drawing.Size(104,16);
			this.checkIsHygiene.TabIndex = 116;
			this.checkIsHygiene.Text = "Is Hygiene";
			this.checkIsHygiene.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboClinic
			// 
			this.comboClinic.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboClinic.Location = new System.Drawing.Point(160,81);
			this.comboClinic.MaxDropDownItems = 100;
			this.comboClinic.Name = "comboClinic";
			this.comboClinic.Size = new System.Drawing.Size(126,21);
			this.comboClinic.TabIndex = 119;
			// 
			// labelClinic
			// 
			this.labelClinic.Location = new System.Drawing.Point(59,85);
			this.labelClinic.Name = "labelClinic";
			this.labelClinic.Size = new System.Drawing.Size(98,16);
			this.labelClinic.TabIndex = 118;
			this.labelClinic.Text = "Clinic";
			this.labelClinic.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// FormOperatoryEdit
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(574,248);
			this.Controls.Add(this.comboClinic);
			this.Controls.Add(this.labelClinic);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.checkIsHygiene);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.comboProvHygienist);
			this.Controls.Add(this.comboProvDentist);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.checkIsHidden);
			this.Controls.Add(this.textAbbrev);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textOpName);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.label1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormOperatoryEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Operatory";
			this.Load += new System.EventHandler(this.FormOperatoryEdit_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormOperatoryEdit_Load(object sender, System.EventArgs e) {
			textOpName.Text=OpCur.OpName;
			textAbbrev.Text=OpCur.Abbrev;
			checkIsHidden.Checked=OpCur.IsHidden;
			if(PrefB.GetBool("EasyNoClinics")){
				labelClinic.Visible=false;
				comboClinic.Visible=false;
			}
			comboClinic.Items.Add(Lan.g(this,"none"));
			comboClinic.SelectedIndex=0;
			for(int i=0;i<Clinics.List.Length;i++){
				comboClinic.Items.Add(Clinics.List[i].Description);
				if(Clinics.List[i].ClinicNum==OpCur.ClinicNum)
					comboClinic.SelectedIndex=i+1;
			}
			comboProvDentist.Items.Add(Lan.g(this,"none"));
			comboProvDentist.SelectedIndex=0;
			for(int i=0;i<Providers.List.Length;i++){
				comboProvDentist.Items.Add(Providers.List[i].Abbr);
				if(Providers.List[i].ProvNum==OpCur.ProvDentist)
					comboProvDentist.SelectedIndex=i+1;
			}
			comboProvHygienist.Items.Add(Lan.g(this,"none"));
			comboProvHygienist.SelectedIndex=0;
			for(int i=0;i<Providers.List.Length;i++){
				comboProvHygienist.Items.Add(Providers.List[i].Abbr);
				if(Providers.List[i].ProvNum==OpCur.ProvHygienist)
					comboProvHygienist.SelectedIndex=i+1;
			}
			checkIsHygiene.Checked=OpCur.IsHygiene;
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(textOpName.Text==""){
				MessageBox.Show(Lan.g(this,"Op Name cannot be blank."));
				return;
			}
			OpCur.OpName=textOpName.Text;
			OpCur.Abbrev=textAbbrev.Text;
			OpCur.IsHidden=checkIsHidden.Checked;
			if(comboClinic.SelectedIndex==0)//none
				OpCur.ClinicNum=0;
			else
				OpCur.ClinicNum=Clinics.List[comboClinic.SelectedIndex-1].ClinicNum;
			if(comboProvDentist.SelectedIndex==0)//none
				OpCur.ProvDentist=0;
			else
				OpCur.ProvDentist=Providers.List[comboProvDentist.SelectedIndex-1].ProvNum;
			if(comboProvHygienist.SelectedIndex==0)//none
				OpCur.ProvHygienist=0;
			else
				OpCur.ProvHygienist=Providers.List[comboProvHygienist.SelectedIndex-1].ProvNum;
			OpCur.IsHygiene=checkIsHygiene.Checked;
			try{
				Operatories.InsertOrUpdate(OpCur,IsNew);
			}
			catch(ApplicationException ex){
				MessageBox.Show(ex.Message);
				return;
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		

		

		


	}
}





















