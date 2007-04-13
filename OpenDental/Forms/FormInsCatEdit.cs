using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using OpenDental.UI;
using OpenDentBusiness;

namespace OpenDental {
	///<summary></summary>
	public class FormInsCatEdit:System.Windows.Forms.Form {
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox textDescription;
		private OpenDental.UI.Button butOK;
		private OpenDental.UI.Button butCancel;
		private System.Windows.Forms.CheckBox checkHidden;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.ComponentModel.Container components=null;
		///<summary></summary>
		public bool IsNew;
		private OpenDental.ValidNumber textPercent;
		private ComboBox comboCat;
		private Label label4;
		///<summary>A list of CovSpans just for this category</summary>
		//private CovSpan[] CovSpanList;
		private CovCat CovCatCur;

		///<summary></summary>
		public FormInsCatEdit(CovCat covCatCur) {
			InitializeComponent();
			CovCatCur=covCatCur.Copy();
			Lan.F(this);
		}

		///<summary></summary>
		protected override void Dispose(bool disposing) {
			if(disposing) {
				if(components!=null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormInsCatEdit));
			this.label1 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.textDescription = new System.Windows.Forms.TextBox();
			this.butOK = new OpenDental.UI.Button();
			this.butCancel = new OpenDental.UI.Button();
			this.checkHidden = new System.Windows.Forms.CheckBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.textPercent = new OpenDental.ValidNumber();
			this.comboCat = new System.Windows.Forms.ComboBox();
			this.label4 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(111,26);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(95,18);
			this.label1.TabIndex = 0;
			this.label1.Text = "Description";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(107,51);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(100,18);
			this.label6.TabIndex = 5;
			this.label6.Text = "Default Percent";
			this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// textDescription
			// 
			this.textDescription.Location = new System.Drawing.Point(209,23);
			this.textDescription.MaxLength = 50;
			this.textDescription.Name = "textDescription";
			this.textDescription.Size = new System.Drawing.Size(100,20);
			this.textDescription.TabIndex = 0;
			// 
			// butOK
			// 
			this.butOK.AdjustImageLocation = new System.Drawing.Point(0,0);
			this.butOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.butOK.Autosize = true;
			this.butOK.BtnShape = OpenDental.UI.enumType.BtnShape.Rectangle;
			this.butOK.BtnStyle = OpenDental.UI.enumType.XPStyle.Silver;
			this.butOK.CornerRadius = 4F;
			this.butOK.Location = new System.Drawing.Point(497,246);
			this.butOK.Name = "butOK";
			this.butOK.Size = new System.Drawing.Size(75,25);
			this.butOK.TabIndex = 4;
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
			this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.butCancel.Location = new System.Drawing.Point(497,284);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(75,25);
			this.butCancel.TabIndex = 5;
			this.butCancel.Text = "&Cancel";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// checkHidden
			// 
			this.checkHidden.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.checkHidden.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkHidden.Location = new System.Drawing.Point(118,79);
			this.checkHidden.Name = "checkHidden";
			this.checkHidden.Size = new System.Drawing.Size(105,16);
			this.checkHidden.TabIndex = 3;
			this.checkHidden.Text = "Is Hidden";
			this.checkHidden.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(229,80);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(343,21);
			this.label2.TabIndex = 12;
			this.label2.Text = "You cannot delete a category, but you can hide it.";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(12,288);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(203,23);
			this.label3.TabIndex = 13;
			this.label3.Text = "Changes affect all patients";
			// 
			// textPercent
			// 
			this.textPercent.Location = new System.Drawing.Point(209,47);
			this.textPercent.MaxVal = 255;
			this.textPercent.MinVal = 0;
			this.textPercent.Name = "textPercent";
			this.textPercent.Size = new System.Drawing.Size(40,20);
			this.textPercent.TabIndex = 1;
			// 
			// comboCat
			// 
			this.comboCat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboCat.FormattingEnabled = true;
			this.comboCat.Location = new System.Drawing.Point(209,106);
			this.comboCat.MaxDropDownItems = 30;
			this.comboCat.Name = "comboCat";
			this.comboCat.Size = new System.Drawing.Size(197,21);
			this.comboCat.TabIndex = 14;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(7,109);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(200,18);
			this.label4.TabIndex = 15;
			this.label4.Text = "Electronic Benefit Category";
			this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// FormInsCatEdit
			// 
			this.AcceptButton = this.butOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
			this.CancelButton = this.butCancel;
			this.ClientSize = new System.Drawing.Size(581,316);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.comboCat);
			this.Controls.Add(this.textPercent);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.checkHidden);
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.butOK);
			this.Controls.Add(this.textDescription);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormInsCatEdit";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Edit Insurance Categories";
			this.Load += new System.EventHandler(this.FormInsCatEdit_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void FormInsCatEdit_Load(object sender,System.EventArgs e) {
			textDescription.Text=CovCatCur.Description;
			if(CovCatCur.DefaultPercent==-1)
				textPercent.Text="";
			else
				textPercent.Text=CovCatCur.DefaultPercent.ToString();
			textPercent.MaxVal=100;
			checkHidden.Checked=CovCatCur.IsHidden;
			for(int i=0;i<Enum.GetNames(typeof(EbenefitCategory)).Length;i++){
				comboCat.Items.Add(Enum.GetNames(typeof(EbenefitCategory))[i]);
				if(Enum.GetNames(typeof(EbenefitCategory))[i]==CovCatCur.EbenefitCat.ToString()){
					comboCat.SelectedIndex=i;
				}
			}
		}

		private void butOK_Click(object sender, System.EventArgs e) {
			if(  textPercent.errorProvider1.GetError(textPercent)!=""
				//|| textPriBasicPercent.errorProvider1.GetError(textPriBasicPercent)!=""
				){
				MessageBox.Show(Lan.g(this,"Please fix data entry errors first."));
				return;
			}
			CovCatCur.Description=textDescription.Text;
			if(textPercent.Text=="")
				CovCatCur.DefaultPercent=-1;
			else
				CovCatCur.DefaultPercent=PIn.PInt(textPercent.Text);
			CovCatCur.IsHidden=checkHidden.Checked;
			CovCatCur.EbenefitCat=(EbenefitCategory)comboCat.SelectedIndex;
			if(IsNew){
				CovCats.Insert(CovCatCur);
			}
			else{
				CovCats.Update(CovCatCur);
			}
			DialogResult=DialogResult.OK;
		}

		private void butCancel_Click(object sender, System.EventArgs e) {
			DialogResult=DialogResult.Cancel;
		}

		


	}
}
