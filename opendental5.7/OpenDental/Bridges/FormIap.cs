using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDental.Bridges;

namespace OpenDental{
	/// <summary>
	/// Summary description for FormBasicTemplate.
	/// </summary>
	public class FormIap : System.Windows.Forms.Form{
		private OpenDental.UI.Button butCancel;
		private OpenDental.UI.Button butOK;
		private System.Windows.Forms.ListBox listEmployers;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textEmpSearch;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textCarrier;
		private System.Windows.Forms.TextBox textEmp;
		private System.Windows.Forms.Label label3;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		///<summary>The user will have selected an employer.  This will be the exact text representation of that employer as it is in the iap database.</summary>
		public string selectedEmployer;

		///<summary></summary>
		public FormIap()
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormIap));
			this.butCancel = new OpenDental.UI.Button();
			this.butOK = new OpenDental.UI.Button();
			this.listEmployers = new System.Windows.Forms.ListBox();
			this.label1 = new System.Windows.Forms.Label();
			this.textEmpSearch = new System.Windows.Forms.TextBox();
			this.textCarrier = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textEmp = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
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
			this.butCancel.Location = new System.Drawing.Point(566,554);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,26);
			this.butCancel.TabIndex = 3;
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
			this.butOK.Location = new System.Drawing.Point(566,513);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,26);
			this.butOK.TabIndex = 2;
			this.butOK.Text = "&OK";
			this.butOK.Click += new System.EventHandler(this.butOK_Click);
			// 
			// listEmployers
			// 
			this.listEmployers.HorizontalScrollbar = true;
			this.listEmployers.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "31",
            "32",
            "33",
            "34",
            "35",
            "36",
            "37",
            "38",
            "39",
            "40"});
			this.listEmployers.Location = new System.Drawing.Point(11,61);
			this.listEmployers.Name = "listEmployers";
			this.listEmployers.Size = new System.Drawing.Size(314,524);
			this.listEmployers.TabIndex = 1;
			this.listEmployers.SelectedIndexChanged += new System.EventHandler(this.listEmployers_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(10,13);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100,18);
			this.label1.TabIndex = 3;
			this.label1.Text = "Search";
			this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// textEmpSearch
			// 
			this.textEmpSearch.Location = new System.Drawing.Point(11,32);
			this.textEmpSearch.Name = "textEmpSearch";
			this.textEmpSearch.Size = new System.Drawing.Size(100,20);
			this.textEmpSearch.TabIndex = 0;
			this.textEmpSearch.TextChanged += new System.EventHandler(this.textEmpSearch_TextChanged);
			// 
			// textCarrier
			// 
			this.textCarrier.Location = new System.Drawing.Point(361,126);
			this.textCarrier.Name = "textCarrier";
			this.textCarrier.ReadOnly = true;
			this.textCarrier.Size = new System.Drawing.Size(292,20);
			this.textCarrier.TabIndex = 4;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(360,107);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100,18);
			this.label2.TabIndex = 5;
			this.label2.Text = "Carrier";
			this.label2.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// textEmp
			// 
			this.textEmp.Location = new System.Drawing.Point(361,77);
			this.textEmp.Name = "textEmp";
			this.textEmp.ReadOnly = true;
			this.textEmp.Size = new System.Drawing.Size(292,20);
			this.textEmp.TabIndex = 6;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(360,58);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(100,18);
			this.label3.TabIndex = 7;
			this.label3.Text = "Employer";
			this.label3.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// FormIap
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.ClientSize = new System.Drawing.Size(693,605);
			this.Controls.Add(this.textEmp);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.textCarrier);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textEmpSearch);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.listEmployers);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.butCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormIap";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Insurance Answers Plus";
			this.Load += new System.EventHandler(this.FormIap_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormIap_Load(object sender, System.EventArgs e) {
			listEmployers.Items.Clear();
		}

		private void textEmpSearch_TextChanged(object sender, System.EventArgs e) {
			listEmployers.Items.Clear();
			if(textEmpSearch.Text==""){
				return;
			}
			ArrayList list=Bridges.Iap.GetList(textEmpSearch.Text.ToUpper());
			for(int i=0;i<list.Count;i++){
				listEmployers.Items.Add(list[i]);
			}
		}

		private void listEmployers_SelectedIndexChanged(object sender, System.EventArgs e) {
			if(listEmployers.SelectedIndex==-1){
				textCarrier.Text="";
			}
			else{
				try{
					Iap.ReadRecord((string)listEmployers.SelectedItem);
				}
				catch(ApplicationException ex){
					Iap.CloseDatabase();
					MessageBox.Show(ex.Message);
					textCarrier.Text="";
					textEmp.Text="";
					return;
				}
				textCarrier.Text=Iap.ReadField(Iap.Carrier);
				textEmp.Text=Iap.ReadField(Iap.Employer);
				Iap.CloseDatabase();
			}
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(listEmployers.SelectedIndex==-1){
				MessageBox.Show("Please select a plan first.");
				return;
			}
			selectedEmployer=(string)listEmployers.SelectedItem;
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		
		

		


	}
}





















